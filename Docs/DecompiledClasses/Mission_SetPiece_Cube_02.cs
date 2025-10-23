using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("", "")]
public class Mission_SetPiece_Cube_02 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public uScript_AddMessage.MessageSpeaker CrazedLeaderSpeaker;

	public SpawnTechData[] CrazedLeaderTechData = new SpawnTechData[0];

	public uScript_AddMessage.MessageSpeaker CrazedMinionTechSpeaker;

	[Multiline(1)]
	public string CrazedMsgTag = "";

	public SpawnTechData[] CrazedTechData = new SpawnTechData[0];

	[Multiline(1)]
	public string CubeAreaTrigger = "";

	[Multiline(1)]
	public string CubeFailTrigger = "";

	public SpawnTechData[] CubeTechData = new SpawnTechData[0];

	public SpawnTechData[] EnemyMinionWaveData = new SpawnTechData[0];

	[Multiline(1)]
	public string FillerNPCRange01 = "";

	[Multiline(1)]
	public string FillerNPCRange02 = "";

	public SpawnTechData[] FillerTechData = new SpawnTechData[0];

	public uScript_AddMessage.MessageSpeaker GCSpeaker;

	public uScript_AddMessage.MessageSpeaker GroupMinionTechSpeaker;

	[Multiline(1)]
	public string LeaderIntroStartTrigger = "";

	[Multiline(1)]
	public string LeaderOutOfRangeTrigger = "";

	private float local_158_System_Single;

	private float local_58_System_Single;

	private float local_683_System_Single;

	private float local_687_System_Single;

	private string local_703_System_String = "Going through fly techs";

	private TechSequencer.ChainType local_844_TechSequencer_ChainType = TechSequencer.ChainType.ShieldBubble;

	private bool local_CrazedAmbushNotTriggered_System_Boolean;

	private bool local_CrazedAmbushTriggered_System_Boolean;

	private int local_CrazedDialog_System_Int32 = 1;

	private bool local_CrazedIntroPlayed_System_Boolean;

	private bool local_CrazedIntroPlayedDoubleCheck_System_Boolean;

	private Tank[] local_CrazedLeaderTechs_TankArray = new Tank[0];

	private bool local_CrazedNPCIgnored_System_Boolean;

	private bool local_CrazedNPCInRange_System_Boolean;

	private bool local_CrazedPlayInterruptOnce_System_Boolean;

	private Tank local_CrazedTech_Tank;

	private Tank local_CrazedTech02_Tank;

	private Tank local_CrazedTech03_Tank;

	private Tank local_CrazedTech2_Tank;

	private Tank local_CrazedTech3_Tank;

	private Tank[] local_CrazedTechs_TankArray = new Tank[0];

	private bool local_CrazedTechsAlive_System_Boolean;

	private bool local_CubeAlive_System_Boolean;

	private bool local_CubeDeadVictory_System_Boolean;

	private int local_CubeDestroyedDialog_System_Int32 = 1;

	private bool local_CubeDestroyedFly_System_Boolean;

	private bool local_CubeDestroyedMsgPlayed_System_Boolean;

	private int local_CubeDialog_System_Int32 = 1;

	private bool local_CubeisOK_System_Boolean;

	private bool local_CubeNeedsReload_System_Boolean;

	private Tank local_CubeTech_Tank;

	private Tank[] local_CubeTechs_TankArray = new Tank[0];

	private bool local_CubeTooEarlyMsgPlayed_System_Boolean;

	private bool local_DestroyCube_System_Boolean;

	private int local_DialogueProgress_System_Int32;

	private Tank local_EnemyMinionWaveTech_Tank;

	private Tank[] local_EnemyMinionWaveTechs_TankArray = new Tank[0];

	private bool local_FightRunning_System_Boolean;

	private bool local_FightStarted_System_Boolean;

	private bool local_FillerMsgPlayed01_System_Boolean;

	private bool local_FillerMsgPlayed02_System_Boolean;

	private Tank local_FillerTech01_Tank;

	private Tank local_FillerTech02_Tank;

	private Tank local_FillerTech03_Tank;

	private Tank[] local_FillerTechs_TankArray = new Tank[0];

	private bool local_FinalObjectiveComplete_System_Boolean;

	private bool local_FirstCubeSpawned_System_Boolean;

	private bool local_FlyLeaderAway_System_Boolean;

	private bool local_HasBeenInterrupted_System_Boolean;

	private bool local_HasPlayerLeftMissionArea_System_Boolean;

	private bool local_LeftAreaAfterLoss_System_Boolean;

	private bool local_MinionsAlive_System_Boolean;

	private bool local_MinionWaveAlive_System_Boolean;

	private Tank[] local_MinionWaveTechs_TankArray = new Tank[0];

	private bool local_MinionWaveTechsDead_System_Boolean;

	private bool local_MsgCubeIntroPlayed_System_Boolean;

	private bool local_NPCTechSetup_System_Boolean;

	private bool local_NPCTechSpawned_System_Boolean;

	private int local_Objective_System_Int32 = 1;

	private bool local_OutOfTime_System_Boolean;

	private bool local_PlayedTryAgainMsg_System_Boolean;

	private bool local_PlayerAttemptedMission_System_Boolean;

	private bool local_PlayerInRangeOfCube_System_Boolean;

	private bool local_PlayerLeftMissionArea_System_Boolean;

	private bool local_PlayInterruptOnce_System_Boolean;

	private bool local_TankInvul_System_Boolean;

	private bool local_TechInvulOnLoad_System_Boolean;

	private bool local_TurnEnemiesAfterCubeDeath_System_Boolean;

	private bool local_WaitingForCube_System_Boolean;

	private bool local_WentOutOfRange_System_Boolean;

	[Multiline(1)]
	public string MissionArea = "";

	[Multiline(1)]
	public string MissionCompleteArea = "";

	public uScript_AddMessage.MessageData MsgCrazedAmbush;

	public uScript_AddMessage.MessageData MsgCrazedInterrupt;

	public uScript_AddMessage.MessageData MsgCrazedIntro01;

	public uScript_AddMessage.MessageData MsgCrazedIntro02;

	public uScript_AddMessage.MessageData MsgCrazedIntro03;

	public uScript_AddMessage.MessageData MsgCrazedIntro04;

	public uScript_AddMessage.MessageData MsgCrazedIntro05;

	public uScript_AddMessage.MessageData MsgCrazedLeaderB4Fight01;

	public uScript_AddMessage.MessageData MsgCrazedLeaderB4Fight02;

	public uScript_AddMessage.MessageData MsgCrazedMinionInterrupt01;

	public uScript_AddMessage.MessageData MsgCrazedMinionInterrupt02;

	public uScript_AddMessage.MessageData MsgCrazedMinionInterrupt03;

	public uScript_AddMessage.MessageData MsgCrazedMinionInterrupt04;

	public uScript_AddMessage.MessageData MsgCrazedNPCFly;

	public uScript_AddMessage.MessageData MsgCubeDestroyed01;

	public uScript_AddMessage.MessageData MsgCubeDestroyed02;

	public uScript_AddMessage.MessageData MsgCubeIntro01;

	public uScript_AddMessage.MessageData MsgCubeLeaveAreaFail;

	public uScript_AddMessage.MessageData msgCubeTooEarly;

	public uScript_AddMessage.MessageData MsgFillerNPC01;

	public uScript_AddMessage.MessageData MsgFillerNPC02;

	public uScript_AddMessage.MessageData MsgFillerNPCFly;

	public uScript_AddMessage.MessageData MsgLeaderTryAgain01;

	public uScript_AddMessage.MessageData MsgLeftAreaCompletely;

	public uScript_AddMessage.MessageData MsgMissionCompleteNoTrigger;

	public uScript_AddMessage.MessageData MsgOutOfTime;

	public uScript_AddMessage.MessageData MsgOutOfTimeMultiplayer;

	public uScript_AddMessage.MessageData MsgOutOfTimeMultiplayerLeave;

	public uScript_AddMessage.MessageData MsgStartBossFight;

	[Multiline(1)]
	public string NPCMsgTag = "";

	public uScript_PlayDialogue.Dialogue StartBossFightDialogue;

	public ExternalBehaviorTree TechFlyAI;

	public Transform TechFlyParticles;

	public float TimeLimit;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_16;

	private GameObject owner_Connection_19;

	private GameObject owner_Connection_26;

	private GameObject owner_Connection_34;

	private GameObject owner_Connection_40;

	private GameObject owner_Connection_50;

	private GameObject owner_Connection_56;

	private GameObject owner_Connection_60;

	private GameObject owner_Connection_65;

	private GameObject owner_Connection_73;

	private GameObject owner_Connection_83;

	private GameObject owner_Connection_99;

	private GameObject owner_Connection_102;

	private GameObject owner_Connection_111;

	private GameObject owner_Connection_141;

	private GameObject owner_Connection_149;

	private GameObject owner_Connection_162;

	private GameObject owner_Connection_295;

	private GameObject owner_Connection_353;

	private GameObject owner_Connection_362;

	private GameObject owner_Connection_369;

	private GameObject owner_Connection_399;

	private GameObject owner_Connection_406;

	private GameObject owner_Connection_416;

	private GameObject owner_Connection_460;

	private GameObject owner_Connection_538;

	private GameObject owner_Connection_543;

	private GameObject owner_Connection_548;

	private GameObject owner_Connection_560;

	private GameObject owner_Connection_564;

	private GameObject owner_Connection_605;

	private GameObject owner_Connection_609;

	private GameObject owner_Connection_612;

	private GameObject owner_Connection_618;

	private GameObject owner_Connection_620;

	private GameObject owner_Connection_662;

	private GameObject owner_Connection_674;

	private GameObject owner_Connection_678;

	private GameObject owner_Connection_680;

	private GameObject owner_Connection_681;

	private GameObject owner_Connection_682;

	private GameObject owner_Connection_688;

	private GameObject owner_Connection_856;

	private GameObject owner_Connection_857;

	private GameObject owner_Connection_858;

	private GameObject owner_Connection_871;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_6 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_6;

	private bool logic_uScriptCon_CompareBool_True_6 = true;

	private bool logic_uScriptCon_CompareBool_False_6 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_7;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_7 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_7 = "FinalObjectiveComplete";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_9 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_9;

	private bool logic_uScriptCon_CompareBool_True_9 = true;

	private bool logic_uScriptCon_CompareBool_False_9 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_11 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_11;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_11 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_11 = "Objective";

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_13 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_13 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_13;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_13 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_13 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_13 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_14 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_14;

	private bool logic_uScriptAct_SetBool_Out_14 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_14 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_14 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_17 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_17 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_17;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_17 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_17;

	private bool logic_uScript_SpawnTechsFromData_Out_17 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_21 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_21;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_21 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_21;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_21 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_21 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_21 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_21 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_23;

	private bool logic_uScriptCon_CompareBool_True_23 = true;

	private bool logic_uScriptCon_CompareBool_False_23 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_27 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_27;

	private bool logic_uScriptAct_SetBool_Out_27 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_27 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_27 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_29;

	private bool logic_uScriptCon_CompareBool_True_29 = true;

	private bool logic_uScriptCon_CompareBool_False_29 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_31;

	private bool logic_uScriptCon_CompareBool_True_31 = true;

	private bool logic_uScriptCon_CompareBool_False_31 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_33 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_33;

	private bool logic_uScript_FinishEncounter_Out_33 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_35;

	private bool logic_uScriptCon_CompareBool_True_35 = true;

	private bool logic_uScriptCon_CompareBool_False_35 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_37 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_37 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_38 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_38;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_38 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_39 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_39 = new Tank[0];

	private int logic_uScript_AccessListTech_index_39;

	private Tank logic_uScript_AccessListTech_value_39;

	private bool logic_uScript_AccessListTech_Out_39 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_41 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_41 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_41;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_41 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_41;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_41 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_41 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_41 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_41 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_45;

	private bool logic_uScriptCon_CompareBool_True_45 = true;

	private bool logic_uScriptCon_CompareBool_False_45 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_49 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_49;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_49;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_49;

	private bool logic_uScript_AddMessage_Out_49 = true;

	private bool logic_uScript_AddMessage_Shown_49 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_52 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_52;

	private bool logic_uScriptCon_CompareBool_True_52 = true;

	private bool logic_uScriptCon_CompareBool_False_52 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_53 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_53;

	private bool logic_uScriptAct_SetBool_Out_53 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_53 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_53 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_55 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_55;

	private object logic_uScript_SetEncounterTarget_visibleObject_55 = "";

	private bool logic_uScript_SetEncounterTarget_Out_55 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_61 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_61;

	private bool logic_uScriptCon_CompareBool_True_61 = true;

	private bool logic_uScriptCon_CompareBool_False_61 = true;

	private uScript_PlayMiscSFX logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_62 = new uScript_PlayMiscSFX();

	private ManSFX.MiscSfxType logic_uScript_PlayMiscSFX_miscSFXType_62 = ManSFX.MiscSfxType.StuntFailed;

	private bool logic_uScript_PlayMiscSFX_Out_62 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_63 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_63;

	private bool logic_uScriptAct_SetBool_Out_63 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_63 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_63 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_64 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_64;

	private bool logic_uScriptAct_SetBool_Out_64 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_64 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_64 = true;

	private uScript_ShowMissionTimerUI logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_66 = new uScript_ShowMissionTimerUI();

	private GameObject logic_uScript_ShowMissionTimerUI_owner_66;

	private bool logic_uScript_ShowMissionTimerUI_showBestTime_66;

	private bool logic_uScript_ShowMissionTimerUI_Out_66 = true;

	private uScript_PlayMiscSFX logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_68 = new uScript_PlayMiscSFX();

	private ManSFX.MiscSfxType logic_uScript_PlayMiscSFX_miscSFXType_68 = ManSFX.MiscSfxType.StuntRingStart;

	private bool logic_uScript_PlayMiscSFX_Out_68 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_69 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_69;

	private bool logic_uScriptAct_SetBool_Out_69 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_69 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_69 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_70 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_70;

	private bool logic_uScriptAct_SetBool_Out_70 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_70 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_70 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_71 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_71 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_71 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_71 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_71 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_71 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_71 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_72 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_72;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_72;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_72;

	private bool logic_uScript_AddMessage_Out_72 = true;

	private bool logic_uScript_AddMessage_Shown_72 = true;

	private uScript_StartMissionTimer logic_uScript_StartMissionTimer_uScript_StartMissionTimer_78 = new uScript_StartMissionTimer();

	private GameObject logic_uScript_StartMissionTimer_owner_78;

	private float logic_uScript_StartMissionTimer_startTime_78;

	private bool logic_uScript_StartMissionTimer_Out_78 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_80 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_80;

	private float logic_uScriptCon_CompareFloat_B_80;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_80 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_80 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_80 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_80 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_80 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_80 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_86 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_86 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_86;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_86 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_86;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_86 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_86 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_86 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_86 = true;

	private uScript_GetMissionTimerDisplayTime logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_87 = new uScript_GetMissionTimerDisplayTime();

	private GameObject logic_uScript_GetMissionTimerDisplayTime_owner_87;

	private float logic_uScript_GetMissionTimerDisplayTime_Return_87;

	private bool logic_uScript_GetMissionTimerDisplayTime_Out_87 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_89 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_89;

	private bool logic_uScriptAct_SetBool_Out_89 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_89 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_89 = true;

	private uScript_StopMissionTimer logic_uScript_StopMissionTimer_uScript_StopMissionTimer_91 = new uScript_StopMissionTimer();

	private GameObject logic_uScript_StopMissionTimer_owner_91;

	private bool logic_uScript_StopMissionTimer_Out_91 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_93;

	private bool logic_uScriptCon_CompareBool_True_93 = true;

	private bool logic_uScriptCon_CompareBool_False_93 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_95;

	private bool logic_uScriptCon_CompareBool_True_95 = true;

	private bool logic_uScriptCon_CompareBool_False_95 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_98 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_98 = new Tank[0];

	private int logic_uScript_AccessListTech_index_98;

	private Tank logic_uScript_AccessListTech_value_98;

	private bool logic_uScript_AccessListTech_Out_98 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_101 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_101 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_101;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_101 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_101 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_101 = true;

	private uScript_PlayMiscSFX logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_103 = new uScript_PlayMiscSFX();

	private ManSFX.MiscSfxType logic_uScript_PlayMiscSFX_miscSFXType_103 = ManSFX.MiscSfxType.StuntRingStart;

	private bool logic_uScript_PlayMiscSFX_Out_103 = true;

	private uScript_ShowMissionTimerUI logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_105 = new uScript_ShowMissionTimerUI();

	private GameObject logic_uScript_ShowMissionTimerUI_owner_105;

	private bool logic_uScript_ShowMissionTimerUI_showBestTime_105;

	private bool logic_uScript_ShowMissionTimerUI_Out_105 = true;

	private uScript_StartMissionTimer logic_uScript_StartMissionTimer_uScript_StartMissionTimer_106 = new uScript_StartMissionTimer();

	private GameObject logic_uScript_StartMissionTimer_owner_106;

	private float logic_uScript_StartMissionTimer_startTime_106;

	private bool logic_uScript_StartMissionTimer_Out_106 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_107 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_107;

	private bool logic_uScriptAct_SetBool_Out_107 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_107 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_107 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_109 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_109;

	private bool logic_uScriptAct_SetBool_Out_109 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_109 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_109 = true;

	private uScript_StopMissionTimer logic_uScript_StopMissionTimer_uScript_StopMissionTimer_112 = new uScript_StopMissionTimer();

	private GameObject logic_uScript_StopMissionTimer_owner_112;

	private bool logic_uScript_StopMissionTimer_Out_112 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_114;

	private bool logic_uScriptCon_CompareBool_True_114 = true;

	private bool logic_uScriptCon_CompareBool_False_114 = true;

	private uScript_HideMissionTimerUI logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_116 = new uScript_HideMissionTimerUI();

	private GameObject logic_uScript_HideMissionTimerUI_owner_116;

	private bool logic_uScript_HideMissionTimerUI_Out_116 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_119 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_119;

	private bool logic_uScriptAct_SetBool_Out_119 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_119 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_119 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_121;

	private bool logic_uScriptCon_CompareBool_True_121 = true;

	private bool logic_uScriptCon_CompareBool_False_121 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_123 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_123;

	private bool logic_uScriptAct_SetBool_Out_123 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_123 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_123 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_124 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_124;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_124;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_124;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_124;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_124;

	private bool logic_uScript_FlyTechUpAndAway_Out_124 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_128 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_128;

	private bool logic_uScriptAct_SetBool_Out_128 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_128 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_128 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_129 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_129 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_129 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_129 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_129 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_129 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_129 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_132 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_132 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_132 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_132 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_132 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_132 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_132 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_134 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_134;

	private bool logic_uScriptAct_SetBool_Out_134 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_134 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_134 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_136 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_136;

	private bool logic_uScriptAct_SetBool_Out_136 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_136 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_136 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_137 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_137;

	private bool logic_uScriptCon_CompareBool_True_137 = true;

	private bool logic_uScriptCon_CompareBool_False_137 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_140 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_140 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_140;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_140 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_140;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_140 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_140 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_140 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_140 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_143 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_143 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_143 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_143 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_143 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_143 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_143 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_145 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_145;

	private bool logic_uScriptAct_SetBool_Out_145 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_145 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_145 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_147 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_147;

	private bool logic_uScriptAct_SetBool_Out_147 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_147 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_147 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_148 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_148 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_148;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_148 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_148;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_148 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_148 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_148 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_148 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_152 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_152 = new Tank[0];

	private int logic_uScript_AccessListTech_index_152;

	private Tank logic_uScript_AccessListTech_value_152;

	private bool logic_uScript_AccessListTech_Out_152 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_153 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_153;

	private bool logic_uScriptAct_SetBool_Out_153 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_153 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_153 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_154 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_154;

	private bool logic_uScriptAct_SetBool_Out_154 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_154 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_154 = true;

	private uScript_ResetMissionTimerTimeElapsed logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_157 = new uScript_ResetMissionTimerTimeElapsed();

	private GameObject logic_uScript_ResetMissionTimerTimeElapsed_owner_157;

	private float logic_uScript_ResetMissionTimerTimeElapsed_startTime_157;

	private bool logic_uScript_ResetMissionTimerTimeElapsed_Out_157 = true;

	private uScript_HideMissionTimerUI logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_159 = new uScript_HideMissionTimerUI();

	private GameObject logic_uScript_HideMissionTimerUI_owner_159;

	private bool logic_uScript_HideMissionTimerUI_Out_159 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_161 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_161;

	private bool logic_uScriptAct_SetBool_Out_161 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_161 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_161 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_163 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_163 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_163 = 1;

	private bool logic_uScript_SetTechsTeam_Out_163 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_164 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_164;

	private Tank logic_uScript_SetTankInvulnerable_tank_164;

	private bool logic_uScript_SetTankInvulnerable_Out_164 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_166 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_166 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_166;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_166 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_166;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_166 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_166 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_166 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_166 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_168 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_168 = new Tank[0];

	private int logic_uScript_AccessListTech_index_168;

	private Tank logic_uScript_AccessListTech_value_168;

	private bool logic_uScript_AccessListTech_Out_168 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_172 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_172;

	private bool logic_uScriptCon_CompareBool_True_172 = true;

	private bool logic_uScriptCon_CompareBool_False_172 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_174 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_174;

	private bool logic_uScriptAct_SetBool_Out_174 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_174 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_174 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_176 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_176;

	private bool logic_uScriptAct_SetBool_Out_176 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_176 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_176 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_178 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_178;

	private bool logic_uScriptAct_SetBool_Out_178 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_178 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_178 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_180 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_180;

	private bool logic_uScriptAct_SetBool_Out_180 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_180 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_180 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_181;

	private bool logic_uScriptCon_CompareBool_True_181 = true;

	private bool logic_uScriptCon_CompareBool_False_181 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_183 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_183 = new Tank[0];

	private int logic_uScript_AccessListTech_index_183;

	private Tank logic_uScript_AccessListTech_value_183;

	private bool logic_uScript_AccessListTech_Out_183 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_186 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_186;

	private bool logic_uScriptCon_CompareBool_True_186 = true;

	private bool logic_uScriptCon_CompareBool_False_186 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_187 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_187;

	private bool logic_uScriptAct_SetBool_Out_187 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_187 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_187 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_189 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_189;

	private bool logic_uScriptAct_SetBool_Out_189 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_189 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_189 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_191 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_191;

	private bool logic_uScriptAct_SetBool_Out_191 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_191 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_191 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_193 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_193;

	private bool logic_uScriptAct_SetBool_Out_193 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_193 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_193 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_195 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_195;

	private bool logic_uScriptCon_CompareBool_True_195 = true;

	private bool logic_uScriptCon_CompareBool_False_195 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_197 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_197;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_197;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_197;

	private bool logic_uScript_AddMessage_Out_197 = true;

	private bool logic_uScript_AddMessage_Shown_197 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_201 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_201;

	private bool logic_uScriptAct_SetBool_Out_201 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_201 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_201 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_202 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_202;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_202;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_202;

	private bool logic_uScript_AddMessage_Out_202 = true;

	private bool logic_uScript_AddMessage_Shown_202 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_207 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_207;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_207;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_207;

	private bool logic_uScript_AddMessage_Out_207 = true;

	private bool logic_uScript_AddMessage_Shown_207 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_208 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_208;

	private bool logic_uScriptAct_SetBool_Out_208 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_208 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_208 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_210 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_210;

	private bool logic_uScriptAct_SetBool_Out_210 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_210 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_210 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_212;

	private bool logic_uScriptCon_CompareBool_True_212 = true;

	private bool logic_uScriptCon_CompareBool_False_212 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_215 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_215;

	private bool logic_uScriptCon_CompareBool_True_215 = true;

	private bool logic_uScriptCon_CompareBool_False_215 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_216 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_216;

	private bool logic_uScriptAct_SetBool_Out_216 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_216 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_216 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_219;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_219 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_219 = "NPCTechSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_221;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_221 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_221 = "NPCTechSetup";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_223;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_223 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_223 = "CrazedAmbushTriggered";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_225;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_225 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_225 = "FirstCubeSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_227;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_227 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_227 = "CubeisOK";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_244;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_244 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_244 = "TankInvul";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_245;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_245 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_245 = "PlayInterruptOnce";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_246;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_246 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_246 = "CubeNeedsReload";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_247;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_247 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_247 = "CrazedIntroPlayed";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_248;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_248 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_248 = "CrazedNPCIgnored";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_249;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_249 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_249 = "CubeDeadVictory";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_250;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_250 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_250 = "HasBeenInterrupted";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_251;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_251 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_251 = "CrazedNPCInRange";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_252;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_252 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_252 = "CrazedPlayInterruptOnce";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_253;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_253 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_253 = "PlayerAttemptedMission";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_254;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_254 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_254 = "LeftAreaAfterLoss";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_255;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_255 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_255 = "MsgCubeIntroPlayed";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_256;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_256 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_256 = "CubeDestroyedMsgPlayed";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_257;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_257 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_257 = "WentOutOfRange";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_258;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_258 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_258 = "CubeDeadVictory";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_259;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_259 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_259 = "OutOfTime";

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_260 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_260;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_260;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_262 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_262 = new Tank[0];

	private int logic_uScript_AccessListTech_index_262;

	private Tank logic_uScript_AccessListTech_value_262;

	private bool logic_uScript_AccessListTech_Out_262 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_265 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_265 = new Tank[0];

	private int logic_uScript_AccessListTech_index_265 = 1;

	private Tank logic_uScript_AccessListTech_value_265;

	private bool logic_uScript_AccessListTech_Out_265 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_267 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_267;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_267;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_267;

	private bool logic_uScript_AddMessage_Out_267 = true;

	private bool logic_uScript_AddMessage_Shown_267 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_269 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_269;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_269;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_269;

	private bool logic_uScript_AddMessage_Out_269 = true;

	private bool logic_uScript_AddMessage_Shown_269 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_274 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_274;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_274;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_274;

	private bool logic_uScript_AddMessage_Out_274 = true;

	private bool logic_uScript_AddMessage_Shown_274 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_276 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_276;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_276;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_276;

	private bool logic_uScript_AddMessage_Out_276 = true;

	private bool logic_uScript_AddMessage_Shown_276 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_278 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_278;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_278;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_278;

	private bool logic_uScript_AddMessage_Out_278 = true;

	private bool logic_uScript_AddMessage_Shown_278 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_283 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_283;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_283;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_283;

	private bool logic_uScript_AddMessage_Out_283 = true;

	private bool logic_uScript_AddMessage_Shown_283 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_284;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_286 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_286;

	private int logic_uScriptAct_AddInt_v2_B_286 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_286;

	private float logic_uScriptAct_AddInt_v2_FloatResult_286;

	private bool logic_uScriptAct_AddInt_v2_Out_286 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_289 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_289;

	private int logic_uScriptAct_AddInt_v2_B_289 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_289;

	private float logic_uScriptAct_AddInt_v2_FloatResult_289;

	private bool logic_uScriptAct_AddInt_v2_Out_289 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_290 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_290;

	private int logic_uScriptAct_AddInt_v2_B_290 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_290;

	private float logic_uScriptAct_AddInt_v2_FloatResult_290;

	private bool logic_uScriptAct_AddInt_v2_Out_290 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_294 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_294;

	private object logic_uScript_SetEncounterTarget_visibleObject_294 = "";

	private bool logic_uScript_SetEncounterTarget_Out_294 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_296 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_296;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_296;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_298 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_298;

	private bool logic_uScriptAct_SetBool_Out_298 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_298 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_298 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_300 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_300;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_300;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_300;

	private bool logic_uScript_AddMessage_Out_300 = true;

	private bool logic_uScript_AddMessage_Shown_300 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_303 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_303;

	private int logic_uScriptAct_AddInt_v2_B_303 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_303;

	private float logic_uScriptAct_AddInt_v2_FloatResult_303;

	private bool logic_uScriptAct_AddInt_v2_Out_303 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_304;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_305 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_305;

	private int logic_uScriptAct_AddInt_v2_B_305 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_305;

	private float logic_uScriptAct_AddInt_v2_FloatResult_305;

	private bool logic_uScriptAct_AddInt_v2_Out_305 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_308 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_308;

	private int logic_uScriptAct_AddInt_v2_B_308 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_308;

	private float logic_uScriptAct_AddInt_v2_FloatResult_308;

	private bool logic_uScriptAct_AddInt_v2_Out_308 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_310 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_310 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_311;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_312 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_312;

	private int logic_uScriptAct_AddInt_v2_B_312 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_312;

	private float logic_uScriptAct_AddInt_v2_FloatResult_312;

	private bool logic_uScriptAct_AddInt_v2_Out_312 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_315 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_315;

	private bool logic_uScriptAct_SetBool_Out_315 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_315 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_315 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_316 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_316;

	private int logic_uScriptAct_AddInt_v2_B_316 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_316;

	private float logic_uScriptAct_AddInt_v2_FloatResult_316;

	private bool logic_uScriptAct_AddInt_v2_Out_316 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_317 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_317;

	private bool logic_uScriptCon_CompareBool_True_317 = true;

	private bool logic_uScriptCon_CompareBool_False_317 = true;

	private uScriptAct_SubtractInt logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_318 = new uScriptAct_SubtractInt();

	private int logic_uScriptAct_SubtractInt_A_318;

	private int logic_uScriptAct_SubtractInt_B_318 = 1;

	private int logic_uScriptAct_SubtractInt_IntResult_318;

	private float logic_uScriptAct_SubtractInt_FloatResult_318;

	private bool logic_uScriptAct_SubtractInt_Out_318 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_321 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_321;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_321;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_321;

	private bool logic_uScript_AddMessage_Out_321 = true;

	private bool logic_uScript_AddMessage_Shown_321 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_326 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_326;

	private bool logic_uScriptAct_SetBool_Out_326 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_326 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_326 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_327 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_327;

	private int logic_uScriptAct_AddInt_v2_B_327 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_327;

	private float logic_uScriptAct_AddInt_v2_FloatResult_327;

	private bool logic_uScriptAct_AddInt_v2_Out_327 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_328 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_328;

	private bool logic_uScriptCon_CompareBool_True_328 = true;

	private bool logic_uScriptCon_CompareBool_False_328 = true;

	private uScriptAct_SubtractInt logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_329 = new uScriptAct_SubtractInt();

	private int logic_uScriptAct_SubtractInt_A_329;

	private int logic_uScriptAct_SubtractInt_B_329 = 1;

	private int logic_uScriptAct_SubtractInt_IntResult_329;

	private float logic_uScriptAct_SubtractInt_FloatResult_329;

	private bool logic_uScriptAct_SubtractInt_Out_329 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_332 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_332;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_332;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_332;

	private bool logic_uScript_AddMessage_Out_332 = true;

	private bool logic_uScript_AddMessage_Shown_332 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_337 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_337;

	private bool logic_uScriptAct_SetBool_Out_337 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_337 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_337 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_338 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_338;

	private int logic_uScriptAct_AddInt_v2_B_338 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_338;

	private float logic_uScriptAct_AddInt_v2_FloatResult_338;

	private bool logic_uScriptAct_AddInt_v2_Out_338 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_339 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_339;

	private bool logic_uScriptCon_CompareBool_True_339 = true;

	private bool logic_uScriptCon_CompareBool_False_339 = true;

	private uScriptAct_SubtractInt logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_340 = new uScriptAct_SubtractInt();

	private int logic_uScriptAct_SubtractInt_A_340;

	private int logic_uScriptAct_SubtractInt_B_340 = 1;

	private int logic_uScriptAct_SubtractInt_IntResult_340;

	private float logic_uScriptAct_SubtractInt_FloatResult_340;

	private bool logic_uScriptAct_SubtractInt_Out_340 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_343 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_343;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_343;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_343;

	private bool logic_uScript_AddMessage_Out_343 = true;

	private bool logic_uScript_AddMessage_Shown_343 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_354 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_354 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_354;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_354 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_354;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_354 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_354 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_354 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_354 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_360 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_360;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_360;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_360;

	private bool logic_uScript_AddMessage_Out_360 = true;

	private bool logic_uScript_AddMessage_Shown_360 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_363 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_363 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_363;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_363 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_363;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_363 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_363 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_363 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_363 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_364 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_364;

	private bool logic_uScriptAct_SetBool_Out_364 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_364 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_364 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_368 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_368 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_368 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_368 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_368 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_368 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_368 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_370 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_370 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_370;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_370 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_370;

	private bool logic_uScript_SpawnTechsFromData_Out_370 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_372 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_372 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_372 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_372 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_372 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_372 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_372 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_374 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_374;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_374;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_374;

	private bool logic_uScript_AddMessage_Out_374 = true;

	private bool logic_uScript_AddMessage_Shown_374 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_376 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_376 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_376 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_376 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_376 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_376 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_376 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_380 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_380;

	private bool logic_uScriptCon_CompareBool_True_380 = true;

	private bool logic_uScriptCon_CompareBool_False_380 = true;

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

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_385 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_385;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_385;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_385;

	private bool logic_uScript_AddMessage_Out_385 = true;

	private bool logic_uScript_AddMessage_Shown_385 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_386 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_386;

	private bool logic_uScriptCon_CompareBool_True_386 = true;

	private bool logic_uScriptCon_CompareBool_False_386 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_388 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_388;

	private bool logic_uScriptAct_SetBool_Out_388 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_388 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_388 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_389 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_389;

	private bool logic_uScriptAct_SetBool_Out_389 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_389 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_389 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_392 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_392;

	private bool logic_uScriptAct_SetBool_Out_392 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_392 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_392 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_395 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_395;

	private bool logic_uScriptAct_SetBool_Out_395 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_395 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_395 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_397 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_397 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_397;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_397 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_397 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_397 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_400 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_400;

	private Tank logic_uScript_SetTankInvulnerable_tank_400;

	private bool logic_uScript_SetTankInvulnerable_Out_400 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_401 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_401 = new Tank[0];

	private int logic_uScript_AccessListTech_index_401;

	private Tank logic_uScript_AccessListTech_value_401;

	private bool logic_uScript_AccessListTech_Out_401 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_402 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_402 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_402 = 1;

	private bool logic_uScript_SetTechsTeam_Out_402 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_403 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_403 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_403;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_403 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_403;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_403 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_403 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_403 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_403 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_410 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_410;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_410 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_410 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_410;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_410;

	private bool logic_uScript_FlyTechUpAndAway_Out_410 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_414 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_414 = new Tank[0];

	private int logic_uScript_AccessListTech_index_414;

	private Tank logic_uScript_AccessListTech_value_414;

	private bool logic_uScript_AccessListTech_Out_414 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_415 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_415 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_415;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_415 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_415;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_415 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_415 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_415 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_415 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_419 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_419 = new Tank[0];

	private int logic_uScript_AccessListTech_index_419 = 1;

	private Tank logic_uScript_AccessListTech_value_419;

	private bool logic_uScript_AccessListTech_Out_419 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_421 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_421;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_421 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_421 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_421;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_421;

	private bool logic_uScript_FlyTechUpAndAway_Out_421 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_426 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_426 = new Tank[0];

	private int logic_uScript_AccessListTech_index_426 = 2;

	private Tank logic_uScript_AccessListTech_value_426;

	private bool logic_uScript_AccessListTech_Out_426 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_428 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_428;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_428 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_428 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_428;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_428;

	private bool logic_uScript_FlyTechUpAndAway_Out_428 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_429 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_429;

	private bool logic_uScriptCon_CompareBool_True_429 = true;

	private bool logic_uScriptCon_CompareBool_False_429 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_431 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_431;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_431 = AITreeType.AITypes.Guard;

	private bool logic_uScript_SetTechAIType_Out_431 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_435 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_435;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_435;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_435;

	private bool logic_uScript_AddMessage_Out_435 = true;

	private bool logic_uScript_AddMessage_Shown_435 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_437 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_437;

	private bool logic_uScriptAct_SetBool_Out_437 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_437 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_437 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_442 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_442;

	private bool logic_uScriptCon_CompareBool_True_442 = true;

	private bool logic_uScriptCon_CompareBool_False_442 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_443 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_443 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_443 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_443 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_443 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_443 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_443 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_445 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_445;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_445;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_445;

	private bool logic_uScript_AddMessage_Out_445 = true;

	private bool logic_uScript_AddMessage_Shown_445 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_449 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_449 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_449;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_449 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_450 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_450 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_450;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_450 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_451 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_451;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_451 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_451 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_452 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_452;

	private bool logic_uScriptCon_CompareBool_True_452 = true;

	private bool logic_uScriptCon_CompareBool_False_452 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_453 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_453 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_454 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_454 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_457 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_457;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_457 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_457 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_459 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_459 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_459 = 1;

	private bool logic_uScript_SetTechsTeam_Out_459 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_462 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_462 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_462;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_462 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_462;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_462 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_462 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_462 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_462 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_464 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_464 = new Tank[0];

	private int logic_uScript_AccessListTech_index_464;

	private Tank logic_uScript_AccessListTech_value_464;

	private bool logic_uScript_AccessListTech_Out_464 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_465 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_465;

	private Tank logic_uScript_SetTankInvulnerable_tank_465;

	private bool logic_uScript_SetTankInvulnerable_Out_465 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_466 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_466;

	private Tank logic_uScript_SetTankInvulnerable_tank_466;

	private bool logic_uScript_SetTankInvulnerable_Out_466 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_469 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_469;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_469 = AITreeType.AITypes.Guard;

	private bool logic_uScript_SetTechAIType_Out_469 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_471 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_471;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_471 = AITreeType.AITypes.Guard;

	private bool logic_uScript_SetTechAIType_Out_471 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_472 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_472;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_472 = 100f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_472 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_474 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_474;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_474 = 100f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_474 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_476 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_476 = new Tank[0];

	private int logic_uScript_AccessListTech_index_476 = 1;

	private Tank logic_uScript_AccessListTech_value_476;

	private bool logic_uScript_AccessListTech_Out_476 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_478 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_478;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_478;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_478;

	private bool logic_uScript_AddMessage_Out_478 = true;

	private bool logic_uScript_AddMessage_Shown_478 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_482 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_482;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_482;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_482;

	private bool logic_uScript_AddMessage_Out_482 = true;

	private bool logic_uScript_AddMessage_Shown_482 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_483 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_483;

	private bool logic_uScriptAct_SetBool_Out_483 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_483 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_483 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_488 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_488;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_488;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_488;

	private bool logic_uScript_AddMessage_Out_488 = true;

	private bool logic_uScript_AddMessage_Shown_488 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_490 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_490;

	private bool logic_uScriptAct_SetBool_Out_490 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_490 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_490 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_491 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_491 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_491 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_491 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_491 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_491 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_491 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_492 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_492;

	private bool logic_uScriptAct_SetBool_Out_492 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_492 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_492 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_493 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_493;

	private bool logic_uScriptAct_SetBool_Out_493 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_493 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_493 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_498 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_498 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_498;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_498 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_499 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_499 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_499 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_499 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_499 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_499 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_499 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_500 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_500;

	private bool logic_uScriptCon_CompareBool_True_500 = true;

	private bool logic_uScriptCon_CompareBool_False_500 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_502 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_502;

	private bool logic_uScriptCon_CompareBool_True_502 = true;

	private bool logic_uScriptCon_CompareBool_False_502 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_504 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_504 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_504 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_504 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_504 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_504 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_504 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_506 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_506;

	private bool logic_uScriptAct_SetBool_Out_506 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_506 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_506 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_509 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_509 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_509 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_509 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_509 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_509 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_509 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_511 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_511;

	private bool logic_uScriptCon_CompareBool_True_511 = true;

	private bool logic_uScriptCon_CompareBool_False_511 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_514 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_514;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_514;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_514;

	private bool logic_uScript_AddMessage_Out_514 = true;

	private bool logic_uScript_AddMessage_Shown_514 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_515 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_515;

	private bool logic_uScriptAct_SetBool_Out_515 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_515 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_515 = true;

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

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_522 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_522;

	private bool logic_uScriptAct_SetBool_Out_522 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_522 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_522 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_523 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_523;

	private bool logic_uScriptAct_SetBool_Out_523 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_523 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_523 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_525 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_525;

	private bool logic_uScriptAct_SetBool_Out_525 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_525 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_525 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_526 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_526 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_526 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_526 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_526 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_526 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_526 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_530 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_530;

	private bool logic_uScriptCon_CompareBool_True_530 = true;

	private bool logic_uScriptCon_CompareBool_False_530 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_532 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_532;

	private bool logic_uScriptCon_CompareBool_True_532 = true;

	private bool logic_uScriptCon_CompareBool_False_532 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_539 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_539 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_539;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_539 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_539;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_539 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_539 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_539 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_539 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_541 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_541 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_541;

	private bool logic_uScript_SetTankInvulnerable_Out_541 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_542 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_542 = new Tank[0];

	private int logic_uScript_AccessListTech_index_542;

	private Tank logic_uScript_AccessListTech_value_542;

	private bool logic_uScript_AccessListTech_Out_542 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_544 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_544 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_544;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_544 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_544;

	private bool logic_uScript_SpawnTechsFromData_Out_544 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_546 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_546;

	private bool logic_uScriptAct_SetBool_Out_546 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_546 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_546 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_549 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_549 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_549;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_549 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_549;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_549 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_549 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_549 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_549 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_552 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_552;

	private bool logic_uScriptAct_SetBool_Out_552 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_552 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_552 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_556 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_556;

	private int logic_uScriptAct_AddInt_v2_B_556 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_556;

	private float logic_uScriptAct_AddInt_v2_FloatResult_556;

	private bool logic_uScriptAct_AddInt_v2_Out_556 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_557 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_557;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_557;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_557;

	private bool logic_uScript_AddMessage_Out_557 = true;

	private bool logic_uScript_AddMessage_Shown_557 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_559 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_559;

	private object logic_uScript_SetEncounterTarget_visibleObject_559 = "";

	private bool logic_uScript_SetEncounterTarget_Out_559 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_563 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_563;

	private object logic_uScript_SetEncounterTarget_visibleObject_563 = "";

	private bool logic_uScript_SetEncounterTarget_Out_563 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_565 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_565 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_565 = -2;

	private bool logic_uScript_SetTechsTeam_Out_565 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_572;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_572 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_572 = "CubeDestroyedFly";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_573;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_573 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_573 = "CubeTooEarlyMsgPlayed";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_574;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_574 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_574 = "PlayerLeftMissionArea";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_575;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_575 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_575 = "PlayerAttemptedMission";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_576;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_576 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_576 = "PlayedTryAgainMsg";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_577;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_577 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_577 = "FlyLeaderAway";

	private uScript_AllowTechEnergyModuleSimultaneousActivation logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_580 = new uScript_AllowTechEnergyModuleSimultaneousActivation();

	private Tank logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_580;

	private TechSequencer.ChainType logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_580 = TechSequencer.ChainType.ShieldBubble;

	private bool logic_uScript_AllowTechEnergyModuleSimultaneousActivation_Out_580 = true;

	private uScript_AllowTechEnergyModuleSimultaneousActivation logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_581 = new uScript_AllowTechEnergyModuleSimultaneousActivation();

	private Tank logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_581;

	private TechSequencer.ChainType logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_581 = TechSequencer.ChainType.ShieldBubble;

	private bool logic_uScript_AllowTechEnergyModuleSimultaneousActivation_Out_581 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_584;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_584 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_584 = "CrazedAmbushNotTriggered";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_585 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_585;

	private bool logic_uScriptAct_SetBool_Out_585 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_585 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_585 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_590 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_590;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_590;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_590;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_590;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_590;

	private bool logic_uScript_FlyTechUpAndAway_Out_590 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_593;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_593 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_593 = "FightRunning";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_594;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_594 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_594 = "FightStarted";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_596 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_596;

	private bool logic_uScriptCon_CompareBool_True_596 = true;

	private bool logic_uScriptCon_CompareBool_False_596 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_598 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_598;

	private bool logic_uScriptAct_SetBool_Out_598 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_598 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_598 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_601;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_601 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_601 = "DestroyCube";

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_602 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_602 = new Tank[0];

	private int logic_uScript_AccessListTech_index_602;

	private Tank logic_uScript_AccessListTech_value_602;

	private bool logic_uScript_AccessListTech_Out_602 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_604 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_604 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_604;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_604 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_604;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_604 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_604 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_604 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_604 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_610 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_610 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_610;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_610 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_610;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_610 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_610 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_610 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_610 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_613 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_613 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_613;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_613 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_613;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_613 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_613 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_613 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_613 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_615 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_615 = new Tank[0];

	private int logic_uScript_AccessListTech_index_615;

	private Tank logic_uScript_AccessListTech_value_615;

	private bool logic_uScript_AccessListTech_Out_615 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_617 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_617 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_617;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_617 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_617;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_617 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_617 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_617 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_617 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_621 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_621 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_621;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_621 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_621;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_621 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_621 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_621 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_621 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_625;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_625 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_625 = "MinionWaveTechsDead";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_626;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_626 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_626 = "CrazedTechsAlive";

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_627 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_627;

	private Tank logic_uScript_SetTankInvulnerable_tank_627;

	private bool logic_uScript_SetTankInvulnerable_Out_627 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_628 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_628 = new Tank[0];

	private int logic_uScript_AccessListTech_index_628 = 1;

	private Tank logic_uScript_AccessListTech_value_628;

	private bool logic_uScript_AccessListTech_Out_628 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_629 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_629 = new Tank[0];

	private int logic_uScript_AccessListTech_index_629;

	private Tank logic_uScript_AccessListTech_value_629;

	private bool logic_uScript_AccessListTech_Out_629 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_635 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_635 = new Tank[0];

	private int logic_uScript_AccessListTech_index_635;

	private Tank logic_uScript_AccessListTech_value_635;

	private bool logic_uScript_AccessListTech_Out_635 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_637 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_637 = new Tank[0];

	private int logic_uScript_AccessListTech_index_637 = 1;

	private Tank logic_uScript_AccessListTech_value_637;

	private bool logic_uScript_AccessListTech_Out_637 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_642 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_642 = new Tank[0];

	private int logic_uScript_AccessListTech_index_642 = 2;

	private Tank logic_uScript_AccessListTech_value_642;

	private bool logic_uScript_AccessListTech_Out_642 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_643 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_643 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_644 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_644;

	private bool logic_uScriptAct_SetBool_Out_644 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_644 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_644 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_646 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_646 = 0.3f;

	private bool logic_uScript_Wait_repeat_646;

	private bool logic_uScript_Wait_Waited_646 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_648 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_648;

	private bool logic_uScriptCon_CompareBool_True_648 = true;

	private bool logic_uScriptCon_CompareBool_False_648 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_650 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_650;

	private bool logic_uScriptAct_SetBool_Out_650 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_650 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_650 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_651 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_651;

	private bool logic_uScriptAct_SetBool_Out_651 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_651 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_651 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_659 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_659 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_659 = -2;

	private bool logic_uScript_SetTechsTeam_Out_659 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_660 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_660;

	private bool logic_uScriptCon_CompareBool_True_660 = true;

	private bool logic_uScriptCon_CompareBool_False_660 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_663 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_663 = new Tank[0];

	private int logic_uScript_AccessListTech_index_663;

	private Tank logic_uScript_AccessListTech_value_663;

	private bool logic_uScript_AccessListTech_Out_663 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_665 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_665;

	private bool logic_uScriptAct_SetBool_Out_665 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_665 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_665 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_666 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_666 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_666;

	private bool logic_uScript_SetTankInvulnerable_Out_666 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_667 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_667 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_667;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_667 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_667;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_667 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_667 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_667 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_667 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_670;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_670 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_670 = "TechInvulOnLoad";

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_673 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_673 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_673;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_673 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_673;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_673 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_673 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_673 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_673 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_676 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_676 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_676;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_676 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_676;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_676 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_676 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_676 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_676 = true;

	private uScript_StartMissionTimer logic_uScript_StartMissionTimer_uScript_StartMissionTimer_679 = new uScript_StartMissionTimer();

	private GameObject logic_uScript_StartMissionTimer_owner_679;

	private float logic_uScript_StartMissionTimer_startTime_679;

	private bool logic_uScript_StartMissionTimer_Out_679 = true;

	private uScript_HideMissionTimerUI logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_684 = new uScript_HideMissionTimerUI();

	private GameObject logic_uScript_HideMissionTimerUI_owner_684;

	private bool logic_uScript_HideMissionTimerUI_Out_684 = true;

	private uScript_ResetMissionTimer logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_685 = new uScript_ResetMissionTimer();

	private GameObject logic_uScript_ResetMissionTimer_owner_685;

	private bool logic_uScript_ResetMissionTimer_Out_685 = true;

	private uScript_StopMissionTimer logic_uScript_StopMissionTimer_uScript_StopMissionTimer_686 = new uScript_StopMissionTimer();

	private GameObject logic_uScript_StopMissionTimer_owner_686;

	private bool logic_uScript_StopMissionTimer_Out_686 = true;

	private uScript_ResetMissionTimerTimeElapsed logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_689 = new uScript_ResetMissionTimerTimeElapsed();

	private GameObject logic_uScript_ResetMissionTimerTimeElapsed_owner_689;

	private float logic_uScript_ResetMissionTimerTimeElapsed_startTime_689;

	private bool logic_uScript_ResetMissionTimerTimeElapsed_Out_689 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_690 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_690;

	private bool logic_uScriptCon_CompareBool_True_690 = true;

	private bool logic_uScriptCon_CompareBool_False_690 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_695 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_695;

	private bool logic_uScriptAct_SetBool_Out_695 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_695 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_695 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_696;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_696 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_696 = "TurnEnemiesAfterCubeDeath";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_697 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_697;

	private bool logic_uScriptCon_CompareBool_True_697 = true;

	private bool logic_uScriptCon_CompareBool_False_697 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_700 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_700;

	private bool logic_uScriptAct_SetBool_Out_700 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_700 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_700 = true;

	private uScriptAct_Log logic_uScriptAct_Log_uScriptAct_Log_702 = new uScriptAct_Log();

	private object logic_uScriptAct_Log_Prefix_702 = "";

	private object[] logic_uScriptAct_Log_Target_702 = new object[0];

	private object logic_uScriptAct_Log_Postfix_702 = "";

	private bool logic_uScriptAct_Log_Out_702 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_704 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_704;

	private bool logic_uScriptAct_SetBool_Out_704 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_704 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_704 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_707 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_707;

	private bool logic_uScriptAct_SetBool_Out_707 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_707 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_707 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_709 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_709;

	private bool logic_uScriptCon_CompareBool_True_709 = true;

	private bool logic_uScriptCon_CompareBool_False_709 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_710 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_710 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_711 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_711 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_712 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_712 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_713 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_713;

	private bool logic_uScriptAct_SetBool_Out_713 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_713 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_713 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_716;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_716 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_716 = "CrazedIntroPlayedDoubleCheck";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_717 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_717;

	private bool logic_uScriptAct_SetBool_Out_717 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_717 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_717 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_718 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_718;

	private bool logic_uScriptAct_SetBool_Out_718 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_718 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_718 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_719 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_719;

	private bool logic_uScriptAct_SetBool_Out_719 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_719 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_719 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_720 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_720;

	private bool logic_uScriptAct_SetBool_Out_720 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_720 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_720 = true;

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

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_729 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_729;

	private bool logic_uScriptAct_SetBool_Out_729 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_729 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_729 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_734 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_734 = 1;

	private int logic_uScriptAct_SetInt_Target_734;

	private bool logic_uScriptAct_SetInt_Out_734 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_735 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_735 = 1;

	private int logic_uScriptAct_SetInt_Target_735;

	private bool logic_uScriptAct_SetInt_Out_735 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_736 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_736 = 1;

	private int logic_uScriptAct_SetInt_Target_736;

	private bool logic_uScriptAct_SetInt_Out_736 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_738 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_738;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_738 = 100f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_738 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_739 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_739;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_739 = AITreeType.AITypes.Guard;

	private bool logic_uScript_SetTechAIType_Out_739 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_741 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_741 = new Tank[0];

	private int logic_uScript_AccessListTech_index_741;

	private Tank logic_uScript_AccessListTech_value_741;

	private bool logic_uScript_AccessListTech_Out_741 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_742 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_742;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_742 = AITreeType.AITypes.Guard;

	private bool logic_uScript_SetTechAIType_Out_742 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_744 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_744;

	private Tank logic_uScript_SetTankInvulnerable_tank_744;

	private bool logic_uScript_SetTankInvulnerable_Out_744 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_745 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_745 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_745 = 1;

	private bool logic_uScript_SetTechsTeam_Out_745 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_746 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_746;

	private Tank logic_uScript_SetTankInvulnerable_tank_746;

	private bool logic_uScript_SetTankInvulnerable_Out_746 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_749 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_749 = new Tank[0];

	private int logic_uScript_AccessListTech_index_749 = 1;

	private Tank logic_uScript_AccessListTech_value_749;

	private bool logic_uScript_AccessListTech_Out_749 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_750 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_750;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_750 = 100f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_750 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_753 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_753;

	private bool logic_uScriptAct_SetBool_Out_753 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_753 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_753 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_754 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_754;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_769 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_769 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_772 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_772 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_774 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_774 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_775 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_775 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_786 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_786 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_786 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_787 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_787 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_787 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_788 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_788 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_789 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_789 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_789 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_790 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_790 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_790 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_791 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_791 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_792 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_792 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_792 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_793 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_793 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_793 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_793 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_793 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_793 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_793 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_795 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_795;

	private bool logic_uScriptAct_SetBool_Out_795 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_795 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_795 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_799 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_799;

	private bool logic_uScriptAct_SetBool_Out_799 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_799 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_799 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_800 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_800;

	private bool logic_uScriptAct_SetBool_Out_800 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_800 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_800 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_801 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_801 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_801 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_804 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_804;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_804;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_804;

	private bool logic_uScript_AddMessage_Out_804 = true;

	private bool logic_uScript_AddMessage_Shown_804 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_805 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_805;

	private bool logic_uScriptAct_SetBool_Out_805 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_805 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_805 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_808 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_808 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_808 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_809 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_809;

	private bool logic_uScript_RemoveTech_Out_809 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_812 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_812 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_812 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_813 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_813;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_813;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_813;

	private bool logic_uScript_AddMessage_Out_813 = true;

	private bool logic_uScript_AddMessage_Shown_813 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_815 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_815 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_815 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_815 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_815 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_815 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_815 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_817 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_817 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_818 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_818;

	private bool logic_uScriptAct_SetBool_Out_818 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_818 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_818 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_819 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_819 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_819 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_820 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_820;

	private bool logic_uScriptAct_SetBool_Out_820 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_820 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_820 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_821 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_821 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_821 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_823 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_823;

	private bool logic_uScriptCon_CompareBool_True_823 = true;

	private bool logic_uScriptCon_CompareBool_False_823 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_824 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_824 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_824 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_829;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_829 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_829 = "PlayerInRangeOfCube";

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_830 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_830 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_830;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_830 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_831 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_831 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_831;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_831 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_834 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_834 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_834 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_835 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_835 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_836 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_836 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_838 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_838 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_838 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_839 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_839 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_840 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_840 = true;

	private uScript_SetTechExplodeDetachingBlocks logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_843 = new uScript_SetTechExplodeDetachingBlocks();

	private Tank logic_uScript_SetTechExplodeDetachingBlocks_tech_843;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_843 = true;

	private float logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_843 = 0.1f;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_Out_843 = true;

	private uScript_AllowTechEnergyModuleSimultaneousActivation logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_847 = new uScript_AllowTechEnergyModuleSimultaneousActivation();

	private Tank logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_847;

	private TechSequencer.ChainType logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_847;

	private bool logic_uScript_AllowTechEnergyModuleSimultaneousActivation_Out_847 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_848 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_848;

	private bool logic_uScriptCon_CompareBool_True_848 = true;

	private bool logic_uScriptCon_CompareBool_False_848 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_849 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_849;

	private bool logic_uScriptAct_SetBool_Out_849 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_849 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_849 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_850 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_850;

	private bool logic_uScriptAct_SetBool_Out_850 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_850 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_850 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_852 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_852;

	private bool logic_uScriptAct_SetBool_Out_852 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_852 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_852 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_854 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_854;

	private bool logic_uScriptAct_SetBool_Out_854 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_854 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_854 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_861 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_861;

	private int logic_uScript_PlayDialogue_progress_861;

	private bool logic_uScript_PlayDialogue_Out_861 = true;

	private bool logic_uScript_PlayDialogue_Shown_861 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_861 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_862 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_862;

	private int logic_uScriptAct_SetInt_Target_862;

	private bool logic_uScriptAct_SetInt_Out_862 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_865 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_865;

	private int logic_uScriptAct_SetInt_Target_865;

	private bool logic_uScriptAct_SetInt_Out_865 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_867 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_867;

	private int logic_uScriptAct_SetInt_Target_867;

	private bool logic_uScriptAct_SetInt_Out_867 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_870 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_870;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_870 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_870 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_870 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_872 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_872 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_873 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_873 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
			if (null != owner_Connection_0)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_0.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_0.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_1;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_1;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_1;
				}
			}
		}
		if (null == owner_Connection_5 || !m_RegisteredForEvents)
		{
			owner_Connection_5 = parentGameObject;
			if (null != owner_Connection_5)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_5.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_5.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_3;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_3;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_3;
				}
			}
		}
		if (null == owner_Connection_16 || !m_RegisteredForEvents)
		{
			owner_Connection_16 = parentGameObject;
		}
		if (null == owner_Connection_19 || !m_RegisteredForEvents)
		{
			owner_Connection_19 = parentGameObject;
		}
		if (null == owner_Connection_26 || !m_RegisteredForEvents)
		{
			owner_Connection_26 = parentGameObject;
		}
		if (null == owner_Connection_34 || !m_RegisteredForEvents)
		{
			owner_Connection_34 = parentGameObject;
		}
		if (null == owner_Connection_40 || !m_RegisteredForEvents)
		{
			owner_Connection_40 = parentGameObject;
		}
		if (null == owner_Connection_50 || !m_RegisteredForEvents)
		{
			owner_Connection_50 = parentGameObject;
		}
		if (null == owner_Connection_56 || !m_RegisteredForEvents)
		{
			owner_Connection_56 = parentGameObject;
		}
		if (null == owner_Connection_60 || !m_RegisteredForEvents)
		{
			owner_Connection_60 = parentGameObject;
		}
		if (null == owner_Connection_65 || !m_RegisteredForEvents)
		{
			owner_Connection_65 = parentGameObject;
		}
		if (null == owner_Connection_73 || !m_RegisteredForEvents)
		{
			owner_Connection_73 = parentGameObject;
		}
		if (null == owner_Connection_83 || !m_RegisteredForEvents)
		{
			owner_Connection_83 = parentGameObject;
		}
		if (null == owner_Connection_99 || !m_RegisteredForEvents)
		{
			owner_Connection_99 = parentGameObject;
		}
		if (null == owner_Connection_102 || !m_RegisteredForEvents)
		{
			owner_Connection_102 = parentGameObject;
		}
		if (null == owner_Connection_111 || !m_RegisteredForEvents)
		{
			owner_Connection_111 = parentGameObject;
		}
		if (null == owner_Connection_141 || !m_RegisteredForEvents)
		{
			owner_Connection_141 = parentGameObject;
		}
		if (null == owner_Connection_149 || !m_RegisteredForEvents)
		{
			owner_Connection_149 = parentGameObject;
		}
		if (null == owner_Connection_162 || !m_RegisteredForEvents)
		{
			owner_Connection_162 = parentGameObject;
		}
		if (null == owner_Connection_295 || !m_RegisteredForEvents)
		{
			owner_Connection_295 = parentGameObject;
		}
		if (null == owner_Connection_353 || !m_RegisteredForEvents)
		{
			owner_Connection_353 = parentGameObject;
		}
		if (null == owner_Connection_362 || !m_RegisteredForEvents)
		{
			owner_Connection_362 = parentGameObject;
		}
		if (null == owner_Connection_369 || !m_RegisteredForEvents)
		{
			owner_Connection_369 = parentGameObject;
		}
		if (null == owner_Connection_399 || !m_RegisteredForEvents)
		{
			owner_Connection_399 = parentGameObject;
		}
		if (null == owner_Connection_406 || !m_RegisteredForEvents)
		{
			owner_Connection_406 = parentGameObject;
		}
		if (null == owner_Connection_416 || !m_RegisteredForEvents)
		{
			owner_Connection_416 = parentGameObject;
		}
		if (null == owner_Connection_460 || !m_RegisteredForEvents)
		{
			owner_Connection_460 = parentGameObject;
		}
		if (null == owner_Connection_538 || !m_RegisteredForEvents)
		{
			owner_Connection_538 = parentGameObject;
		}
		if (null == owner_Connection_543 || !m_RegisteredForEvents)
		{
			owner_Connection_543 = parentGameObject;
		}
		if (null == owner_Connection_548 || !m_RegisteredForEvents)
		{
			owner_Connection_548 = parentGameObject;
		}
		if (null == owner_Connection_560 || !m_RegisteredForEvents)
		{
			owner_Connection_560 = parentGameObject;
		}
		if (null == owner_Connection_564 || !m_RegisteredForEvents)
		{
			owner_Connection_564 = parentGameObject;
		}
		if (null == owner_Connection_605 || !m_RegisteredForEvents)
		{
			owner_Connection_605 = parentGameObject;
		}
		if (null == owner_Connection_609 || !m_RegisteredForEvents)
		{
			owner_Connection_609 = parentGameObject;
		}
		if (null == owner_Connection_612 || !m_RegisteredForEvents)
		{
			owner_Connection_612 = parentGameObject;
		}
		if (null == owner_Connection_618 || !m_RegisteredForEvents)
		{
			owner_Connection_618 = parentGameObject;
		}
		if (null == owner_Connection_620 || !m_RegisteredForEvents)
		{
			owner_Connection_620 = parentGameObject;
		}
		if (null == owner_Connection_662 || !m_RegisteredForEvents)
		{
			owner_Connection_662 = parentGameObject;
		}
		if (null == owner_Connection_674 || !m_RegisteredForEvents)
		{
			owner_Connection_674 = parentGameObject;
		}
		if (null == owner_Connection_678 || !m_RegisteredForEvents)
		{
			owner_Connection_678 = parentGameObject;
		}
		if (null == owner_Connection_680 || !m_RegisteredForEvents)
		{
			owner_Connection_680 = parentGameObject;
		}
		if (null == owner_Connection_681 || !m_RegisteredForEvents)
		{
			owner_Connection_681 = parentGameObject;
		}
		if (null == owner_Connection_682 || !m_RegisteredForEvents)
		{
			owner_Connection_682 = parentGameObject;
		}
		if (null == owner_Connection_688 || !m_RegisteredForEvents)
		{
			owner_Connection_688 = parentGameObject;
		}
		if (null == owner_Connection_856 || !m_RegisteredForEvents)
		{
			owner_Connection_856 = parentGameObject;
		}
		if (null == owner_Connection_857 || !m_RegisteredForEvents)
		{
			owner_Connection_857 = parentGameObject;
		}
		if (null == owner_Connection_858 || !m_RegisteredForEvents)
		{
			owner_Connection_858 = parentGameObject;
		}
		if (null == owner_Connection_871 || !m_RegisteredForEvents)
		{
			owner_Connection_871 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_0)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_0.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_0.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_1;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_1;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_1;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_5)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_5.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_5.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_3;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_3;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_3;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_0)
		{
			uScript_EncounterUpdate component = owner_Connection_0.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_1;
				component.OnSuspend -= Instance_OnSuspend_1;
				component.OnResume -= Instance_OnResume_1;
			}
		}
		if (null != owner_Connection_5)
		{
			uScript_SaveLoad component2 = owner_Connection_5.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_3;
				component2.LoadEvent -= Instance_LoadEvent_3;
				component2.RestartEvent -= Instance_RestartEvent_3;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_6.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_9.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_13.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_14.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_17.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_27.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_33.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_37.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_38.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_39.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_41.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_49.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_52.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_55.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_61.SetParent(g);
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_62.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_64.SetParent(g);
		logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_66.SetParent(g);
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_68.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_69.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_70.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_71.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_72.SetParent(g);
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_78.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_80.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_86.SetParent(g);
		logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_87.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_89.SetParent(g);
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_91.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_98.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_101.SetParent(g);
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_103.SetParent(g);
		logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_105.SetParent(g);
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_106.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_107.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.SetParent(g);
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_112.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114.SetParent(g);
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_116.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_119.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_124.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_128.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_129.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_132.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_134.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_136.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_137.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_140.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_143.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_145.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_147.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_148.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_152.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_153.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_154.SetParent(g);
		logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_157.SetParent(g);
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_159.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_161.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_163.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_164.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_166.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_168.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_172.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_174.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_176.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_178.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_180.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_183.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_186.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_187.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_189.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_191.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_193.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_195.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_197.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_201.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_202.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_207.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_208.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_210.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_215.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_216.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_260.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_262.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_265.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_267.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_269.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_274.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_276.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_278.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_283.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_286.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_289.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_290.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_294.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_296.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_298.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_300.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_303.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_305.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_308.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_310.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_312.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_315.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_316.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_317.SetParent(g);
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_318.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_321.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_326.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_327.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_328.SetParent(g);
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_329.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_332.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_337.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_338.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_339.SetParent(g);
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_340.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_343.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_354.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_360.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_363.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_364.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_368.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_370.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_372.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_374.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_376.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_380.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_381.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_382.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_385.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_386.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_388.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_389.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_392.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_395.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_397.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_400.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_401.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_402.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_403.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_410.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_414.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_415.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_419.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_421.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_426.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_428.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_429.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_431.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_435.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_437.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_442.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_443.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_445.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_449.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_450.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_451.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_452.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_453.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_454.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_457.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_459.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_462.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_464.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_465.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_466.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_469.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_471.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_472.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_474.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_476.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_478.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_482.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_483.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_488.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_490.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_491.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_492.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_493.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_498.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_499.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_500.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_502.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_504.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_506.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_509.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_511.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_514.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_515.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_518.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_521.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_522.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_523.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_525.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_526.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_530.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_532.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_539.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_541.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_542.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_544.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_546.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_549.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_552.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_556.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_557.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_559.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_563.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_565.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.SetParent(g);
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_580.SetParent(g);
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_581.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_585.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_590.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_596.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_598.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_602.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_604.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_610.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_613.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_615.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_617.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_621.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_627.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_628.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_629.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_635.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_637.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_642.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_643.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_644.SetParent(g);
		logic_uScript_Wait_uScript_Wait_646.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_648.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_650.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_651.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_659.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_660.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_663.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_665.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_666.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_667.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_673.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_676.SetParent(g);
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_679.SetParent(g);
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_684.SetParent(g);
		logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_685.SetParent(g);
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_686.SetParent(g);
		logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_689.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_690.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_695.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_697.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_700.SetParent(g);
		logic_uScriptAct_Log_uScriptAct_Log_702.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_704.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_707.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_709.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_710.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_711.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_712.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_713.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_717.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_718.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_719.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_720.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_726.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_727.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_729.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_734.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_735.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_736.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_738.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_739.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_741.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_742.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_744.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_745.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_746.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_749.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_750.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_753.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_754.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_769.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_772.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_774.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_775.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_786.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_787.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_788.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_789.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_790.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_791.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_792.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_793.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_795.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_799.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_800.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_801.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_804.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_805.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_808.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_809.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_812.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_813.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_815.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_817.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_818.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_819.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_820.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_821.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_823.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_824.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_830.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_831.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_834.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_835.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_836.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_838.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_839.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_840.SetParent(g);
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_843.SetParent(g);
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_847.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_848.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_849.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_850.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_852.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_854.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_861.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_862.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_865.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_867.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_870.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_872.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_873.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_5 = parentGameObject;
		owner_Connection_16 = parentGameObject;
		owner_Connection_19 = parentGameObject;
		owner_Connection_26 = parentGameObject;
		owner_Connection_34 = parentGameObject;
		owner_Connection_40 = parentGameObject;
		owner_Connection_50 = parentGameObject;
		owner_Connection_56 = parentGameObject;
		owner_Connection_60 = parentGameObject;
		owner_Connection_65 = parentGameObject;
		owner_Connection_73 = parentGameObject;
		owner_Connection_83 = parentGameObject;
		owner_Connection_99 = parentGameObject;
		owner_Connection_102 = parentGameObject;
		owner_Connection_111 = parentGameObject;
		owner_Connection_141 = parentGameObject;
		owner_Connection_149 = parentGameObject;
		owner_Connection_162 = parentGameObject;
		owner_Connection_295 = parentGameObject;
		owner_Connection_353 = parentGameObject;
		owner_Connection_362 = parentGameObject;
		owner_Connection_369 = parentGameObject;
		owner_Connection_399 = parentGameObject;
		owner_Connection_406 = parentGameObject;
		owner_Connection_416 = parentGameObject;
		owner_Connection_460 = parentGameObject;
		owner_Connection_538 = parentGameObject;
		owner_Connection_543 = parentGameObject;
		owner_Connection_548 = parentGameObject;
		owner_Connection_560 = parentGameObject;
		owner_Connection_564 = parentGameObject;
		owner_Connection_605 = parentGameObject;
		owner_Connection_609 = parentGameObject;
		owner_Connection_612 = parentGameObject;
		owner_Connection_618 = parentGameObject;
		owner_Connection_620 = parentGameObject;
		owner_Connection_662 = parentGameObject;
		owner_Connection_674 = parentGameObject;
		owner_Connection_678 = parentGameObject;
		owner_Connection_680 = parentGameObject;
		owner_Connection_681 = parentGameObject;
		owner_Connection_682 = parentGameObject;
		owner_Connection_688 = parentGameObject;
		owner_Connection_856 = parentGameObject;
		owner_Connection_857 = parentGameObject;
		owner_Connection_858 = parentGameObject;
		owner_Connection_871 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_260.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_296.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_754.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out += SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out += SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Save_Out += SubGraph_SaveLoadInt_Save_Out_11;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Load_Out += SubGraph_SaveLoadInt_Load_Out_11;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.Save_Out += SubGraph_SaveLoadBool_Save_Out_219;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.Load_Out += SubGraph_SaveLoadBool_Load_Out_219;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_219;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.Save_Out += SubGraph_SaveLoadBool_Save_Out_221;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.Load_Out += SubGraph_SaveLoadBool_Load_Out_221;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_221;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.Save_Out += SubGraph_SaveLoadBool_Save_Out_223;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.Load_Out += SubGraph_SaveLoadBool_Load_Out_223;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_223;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.Save_Out += SubGraph_SaveLoadBool_Save_Out_225;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.Load_Out += SubGraph_SaveLoadBool_Load_Out_225;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_225;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Save_Out += SubGraph_SaveLoadBool_Save_Out_227;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Load_Out += SubGraph_SaveLoadBool_Load_Out_227;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_227;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Save_Out += SubGraph_SaveLoadBool_Save_Out_244;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Load_Out += SubGraph_SaveLoadBool_Load_Out_244;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_244;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.Save_Out += SubGraph_SaveLoadBool_Save_Out_245;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.Load_Out += SubGraph_SaveLoadBool_Load_Out_245;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_245;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.Save_Out += SubGraph_SaveLoadBool_Save_Out_246;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.Load_Out += SubGraph_SaveLoadBool_Load_Out_246;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_246;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Save_Out += SubGraph_SaveLoadBool_Save_Out_247;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Load_Out += SubGraph_SaveLoadBool_Load_Out_247;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_247;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.Save_Out += SubGraph_SaveLoadBool_Save_Out_248;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.Load_Out += SubGraph_SaveLoadBool_Load_Out_248;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_248;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Save_Out += SubGraph_SaveLoadBool_Save_Out_249;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Load_Out += SubGraph_SaveLoadBool_Load_Out_249;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_249;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Save_Out += SubGraph_SaveLoadBool_Save_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Load_Out += SubGraph_SaveLoadBool_Load_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Save_Out += SubGraph_SaveLoadBool_Save_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Load_Out += SubGraph_SaveLoadBool_Load_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Save_Out += SubGraph_SaveLoadBool_Save_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Load_Out += SubGraph_SaveLoadBool_Load_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Save_Out += SubGraph_SaveLoadBool_Save_Out_253;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Load_Out += SubGraph_SaveLoadBool_Load_Out_253;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_253;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.Save_Out += SubGraph_SaveLoadBool_Save_Out_254;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.Load_Out += SubGraph_SaveLoadBool_Load_Out_254;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_254;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Save_Out += SubGraph_SaveLoadBool_Save_Out_255;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Load_Out += SubGraph_SaveLoadBool_Load_Out_255;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_255;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Save_Out += SubGraph_SaveLoadBool_Save_Out_256;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Load_Out += SubGraph_SaveLoadBool_Load_Out_256;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_256;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Save_Out += SubGraph_SaveLoadBool_Save_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Load_Out += SubGraph_SaveLoadBool_Load_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.Save_Out += SubGraph_SaveLoadBool_Save_Out_258;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.Load_Out += SubGraph_SaveLoadBool_Load_Out_258;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_258;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Save_Out += SubGraph_SaveLoadBool_Save_Out_259;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Load_Out += SubGraph_SaveLoadBool_Load_Out_259;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_259;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_260.Out += SubGraph_CompleteObjectiveStage_Out_260;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.Output1 += uScriptCon_ManualSwitch_Output1_284;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.Output2 += uScriptCon_ManualSwitch_Output2_284;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.Output3 += uScriptCon_ManualSwitch_Output3_284;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.Output4 += uScriptCon_ManualSwitch_Output4_284;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.Output5 += uScriptCon_ManualSwitch_Output5_284;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.Output6 += uScriptCon_ManualSwitch_Output6_284;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.Output7 += uScriptCon_ManualSwitch_Output7_284;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.Output8 += uScriptCon_ManualSwitch_Output8_284;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_296.Out += SubGraph_CompleteObjectiveStage_Out_296;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.Output1 += uScriptCon_ManualSwitch_Output1_304;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.Output2 += uScriptCon_ManualSwitch_Output2_304;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.Output3 += uScriptCon_ManualSwitch_Output3_304;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.Output4 += uScriptCon_ManualSwitch_Output4_304;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.Output5 += uScriptCon_ManualSwitch_Output5_304;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.Output6 += uScriptCon_ManualSwitch_Output6_304;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.Output7 += uScriptCon_ManualSwitch_Output7_304;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.Output8 += uScriptCon_ManualSwitch_Output8_304;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.Output1 += uScriptCon_ManualSwitch_Output1_311;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.Output2 += uScriptCon_ManualSwitch_Output2_311;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.Output3 += uScriptCon_ManualSwitch_Output3_311;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.Output4 += uScriptCon_ManualSwitch_Output4_311;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.Output5 += uScriptCon_ManualSwitch_Output5_311;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.Output6 += uScriptCon_ManualSwitch_Output6_311;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.Output7 += uScriptCon_ManualSwitch_Output7_311;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.Output8 += uScriptCon_ManualSwitch_Output8_311;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Save_Out += SubGraph_SaveLoadBool_Save_Out_572;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Load_Out += SubGraph_SaveLoadBool_Load_Out_572;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_572;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Save_Out += SubGraph_SaveLoadBool_Save_Out_573;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Load_Out += SubGraph_SaveLoadBool_Load_Out_573;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_573;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Save_Out += SubGraph_SaveLoadBool_Save_Out_574;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Load_Out += SubGraph_SaveLoadBool_Load_Out_574;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_574;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Save_Out += SubGraph_SaveLoadBool_Save_Out_575;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Load_Out += SubGraph_SaveLoadBool_Load_Out_575;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_575;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Save_Out += SubGraph_SaveLoadBool_Save_Out_576;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Load_Out += SubGraph_SaveLoadBool_Load_Out_576;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_576;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Save_Out += SubGraph_SaveLoadBool_Save_Out_577;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Load_Out += SubGraph_SaveLoadBool_Load_Out_577;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_577;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.Save_Out += SubGraph_SaveLoadBool_Save_Out_584;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.Load_Out += SubGraph_SaveLoadBool_Load_Out_584;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_584;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Save_Out += SubGraph_SaveLoadBool_Save_Out_593;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Load_Out += SubGraph_SaveLoadBool_Load_Out_593;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_593;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.Save_Out += SubGraph_SaveLoadBool_Save_Out_594;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.Load_Out += SubGraph_SaveLoadBool_Load_Out_594;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_594;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.Save_Out += SubGraph_SaveLoadBool_Save_Out_601;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.Load_Out += SubGraph_SaveLoadBool_Load_Out_601;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_601;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.Save_Out += SubGraph_SaveLoadBool_Save_Out_625;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.Load_Out += SubGraph_SaveLoadBool_Load_Out_625;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_625;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.Save_Out += SubGraph_SaveLoadBool_Save_Out_626;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.Load_Out += SubGraph_SaveLoadBool_Load_Out_626;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_626;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Save_Out += SubGraph_SaveLoadBool_Save_Out_670;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Load_Out += SubGraph_SaveLoadBool_Load_Out_670;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_670;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.Save_Out += SubGraph_SaveLoadBool_Save_Out_696;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.Load_Out += SubGraph_SaveLoadBool_Load_Out_696;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_696;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.Save_Out += SubGraph_SaveLoadBool_Save_Out_716;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.Load_Out += SubGraph_SaveLoadBool_Load_Out_716;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_716;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_754.Out += SubGraph_LoadObjectiveStates_Out_754;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.Save_Out += SubGraph_SaveLoadBool_Save_Out_829;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.Load_Out += SubGraph_SaveLoadBool_Load_Out_829;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_829;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_260.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_296.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_754.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_38.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_124.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_260.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_296.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_410.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_421.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_428.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_590.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_754.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_861.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_49.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_72.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_164.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_197.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_202.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_207.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_260.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_267.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_269.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_274.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_276.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_278.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_283.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_296.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_300.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_321.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_332.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_343.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_360.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_374.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_385.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_400.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_435.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_445.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_465.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_466.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_478.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_482.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_488.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_514.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_518.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_521.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_541.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_557.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_627.OnDisable();
		logic_uScript_Wait_uScript_Wait_646.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_666.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_744.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_746.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_754.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_786.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_787.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_789.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_790.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_792.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_801.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_804.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_808.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_812.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_813.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_819.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_821.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_824.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_834.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_838.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_861.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_870.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_260.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_296.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_754.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_260.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_296.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_754.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out -= SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out -= SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Save_Out -= SubGraph_SaveLoadInt_Save_Out_11;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Load_Out -= SubGraph_SaveLoadInt_Load_Out_11;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.Save_Out -= SubGraph_SaveLoadBool_Save_Out_219;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.Load_Out -= SubGraph_SaveLoadBool_Load_Out_219;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_219;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.Save_Out -= SubGraph_SaveLoadBool_Save_Out_221;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.Load_Out -= SubGraph_SaveLoadBool_Load_Out_221;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_221;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.Save_Out -= SubGraph_SaveLoadBool_Save_Out_223;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.Load_Out -= SubGraph_SaveLoadBool_Load_Out_223;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_223;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.Save_Out -= SubGraph_SaveLoadBool_Save_Out_225;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.Load_Out -= SubGraph_SaveLoadBool_Load_Out_225;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_225;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Save_Out -= SubGraph_SaveLoadBool_Save_Out_227;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Load_Out -= SubGraph_SaveLoadBool_Load_Out_227;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_227;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Save_Out -= SubGraph_SaveLoadBool_Save_Out_244;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Load_Out -= SubGraph_SaveLoadBool_Load_Out_244;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_244;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.Save_Out -= SubGraph_SaveLoadBool_Save_Out_245;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.Load_Out -= SubGraph_SaveLoadBool_Load_Out_245;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_245;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.Save_Out -= SubGraph_SaveLoadBool_Save_Out_246;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.Load_Out -= SubGraph_SaveLoadBool_Load_Out_246;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_246;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Save_Out -= SubGraph_SaveLoadBool_Save_Out_247;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Load_Out -= SubGraph_SaveLoadBool_Load_Out_247;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_247;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.Save_Out -= SubGraph_SaveLoadBool_Save_Out_248;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.Load_Out -= SubGraph_SaveLoadBool_Load_Out_248;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_248;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Save_Out -= SubGraph_SaveLoadBool_Save_Out_249;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Load_Out -= SubGraph_SaveLoadBool_Load_Out_249;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_249;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Save_Out -= SubGraph_SaveLoadBool_Save_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Load_Out -= SubGraph_SaveLoadBool_Load_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Save_Out -= SubGraph_SaveLoadBool_Save_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Load_Out -= SubGraph_SaveLoadBool_Load_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Save_Out -= SubGraph_SaveLoadBool_Save_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Load_Out -= SubGraph_SaveLoadBool_Load_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Save_Out -= SubGraph_SaveLoadBool_Save_Out_253;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Load_Out -= SubGraph_SaveLoadBool_Load_Out_253;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_253;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.Save_Out -= SubGraph_SaveLoadBool_Save_Out_254;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.Load_Out -= SubGraph_SaveLoadBool_Load_Out_254;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_254;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Save_Out -= SubGraph_SaveLoadBool_Save_Out_255;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Load_Out -= SubGraph_SaveLoadBool_Load_Out_255;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_255;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Save_Out -= SubGraph_SaveLoadBool_Save_Out_256;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Load_Out -= SubGraph_SaveLoadBool_Load_Out_256;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_256;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Save_Out -= SubGraph_SaveLoadBool_Save_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Load_Out -= SubGraph_SaveLoadBool_Load_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.Save_Out -= SubGraph_SaveLoadBool_Save_Out_258;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.Load_Out -= SubGraph_SaveLoadBool_Load_Out_258;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_258;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Save_Out -= SubGraph_SaveLoadBool_Save_Out_259;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Load_Out -= SubGraph_SaveLoadBool_Load_Out_259;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_259;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_260.Out -= SubGraph_CompleteObjectiveStage_Out_260;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.Output1 -= uScriptCon_ManualSwitch_Output1_284;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.Output2 -= uScriptCon_ManualSwitch_Output2_284;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.Output3 -= uScriptCon_ManualSwitch_Output3_284;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.Output4 -= uScriptCon_ManualSwitch_Output4_284;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.Output5 -= uScriptCon_ManualSwitch_Output5_284;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.Output6 -= uScriptCon_ManualSwitch_Output6_284;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.Output7 -= uScriptCon_ManualSwitch_Output7_284;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.Output8 -= uScriptCon_ManualSwitch_Output8_284;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_296.Out -= SubGraph_CompleteObjectiveStage_Out_296;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.Output1 -= uScriptCon_ManualSwitch_Output1_304;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.Output2 -= uScriptCon_ManualSwitch_Output2_304;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.Output3 -= uScriptCon_ManualSwitch_Output3_304;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.Output4 -= uScriptCon_ManualSwitch_Output4_304;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.Output5 -= uScriptCon_ManualSwitch_Output5_304;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.Output6 -= uScriptCon_ManualSwitch_Output6_304;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.Output7 -= uScriptCon_ManualSwitch_Output7_304;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.Output8 -= uScriptCon_ManualSwitch_Output8_304;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.Output1 -= uScriptCon_ManualSwitch_Output1_311;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.Output2 -= uScriptCon_ManualSwitch_Output2_311;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.Output3 -= uScriptCon_ManualSwitch_Output3_311;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.Output4 -= uScriptCon_ManualSwitch_Output4_311;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.Output5 -= uScriptCon_ManualSwitch_Output5_311;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.Output6 -= uScriptCon_ManualSwitch_Output6_311;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.Output7 -= uScriptCon_ManualSwitch_Output7_311;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.Output8 -= uScriptCon_ManualSwitch_Output8_311;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Save_Out -= SubGraph_SaveLoadBool_Save_Out_572;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Load_Out -= SubGraph_SaveLoadBool_Load_Out_572;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_572;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Save_Out -= SubGraph_SaveLoadBool_Save_Out_573;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Load_Out -= SubGraph_SaveLoadBool_Load_Out_573;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_573;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Save_Out -= SubGraph_SaveLoadBool_Save_Out_574;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Load_Out -= SubGraph_SaveLoadBool_Load_Out_574;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_574;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Save_Out -= SubGraph_SaveLoadBool_Save_Out_575;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Load_Out -= SubGraph_SaveLoadBool_Load_Out_575;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_575;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Save_Out -= SubGraph_SaveLoadBool_Save_Out_576;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Load_Out -= SubGraph_SaveLoadBool_Load_Out_576;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_576;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Save_Out -= SubGraph_SaveLoadBool_Save_Out_577;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Load_Out -= SubGraph_SaveLoadBool_Load_Out_577;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_577;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.Save_Out -= SubGraph_SaveLoadBool_Save_Out_584;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.Load_Out -= SubGraph_SaveLoadBool_Load_Out_584;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_584;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Save_Out -= SubGraph_SaveLoadBool_Save_Out_593;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Load_Out -= SubGraph_SaveLoadBool_Load_Out_593;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_593;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.Save_Out -= SubGraph_SaveLoadBool_Save_Out_594;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.Load_Out -= SubGraph_SaveLoadBool_Load_Out_594;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_594;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.Save_Out -= SubGraph_SaveLoadBool_Save_Out_601;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.Load_Out -= SubGraph_SaveLoadBool_Load_Out_601;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_601;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.Save_Out -= SubGraph_SaveLoadBool_Save_Out_625;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.Load_Out -= SubGraph_SaveLoadBool_Load_Out_625;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_625;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.Save_Out -= SubGraph_SaveLoadBool_Save_Out_626;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.Load_Out -= SubGraph_SaveLoadBool_Load_Out_626;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_626;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Save_Out -= SubGraph_SaveLoadBool_Save_Out_670;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Load_Out -= SubGraph_SaveLoadBool_Load_Out_670;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_670;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.Save_Out -= SubGraph_SaveLoadBool_Save_Out_696;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.Load_Out -= SubGraph_SaveLoadBool_Load_Out_696;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_696;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.Save_Out -= SubGraph_SaveLoadBool_Save_Out_716;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.Load_Out -= SubGraph_SaveLoadBool_Load_Out_716;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_716;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_754.Out -= SubGraph_LoadObjectiveStates_Out_754;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.Save_Out -= SubGraph_SaveLoadBool_Save_Out_829;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.Load_Out -= SubGraph_SaveLoadBool_Load_Out_829;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_829;
	}

	private void Instance_OnUpdate_1(object o, EventArgs e)
	{
		Relay_OnUpdate_1();
	}

	private void Instance_OnSuspend_1(object o, EventArgs e)
	{
		Relay_OnSuspend_1();
	}

	private void Instance_OnResume_1(object o, EventArgs e)
	{
		Relay_OnResume_1();
	}

	private void Instance_SaveEvent_3(object o, EventArgs e)
	{
		Relay_SaveEvent_3();
	}

	private void Instance_LoadEvent_3(object o, EventArgs e)
	{
		Relay_LoadEvent_3();
	}

	private void Instance_RestartEvent_3(object o, EventArgs e)
	{
		Relay_RestartEvent_3();
	}

	private void SubGraph_SaveLoadBool_Save_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_FinalObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Save_Out_7();
	}

	private void SubGraph_SaveLoadBool_Load_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_FinalObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Load_Out_7();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_FinalObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Restart_Out_7();
	}

	private void SubGraph_SaveLoadInt_Save_Out_11(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_11 = e.integer;
		local_Objective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_11;
		Relay_Save_Out_11();
	}

	private void SubGraph_SaveLoadInt_Load_Out_11(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_11 = e.integer;
		local_Objective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_11;
		Relay_Load_Out_11();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_11(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_11 = e.integer;
		local_Objective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_11;
		Relay_Restart_Out_11();
	}

	private void SubGraph_SaveLoadBool_Save_Out_219(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_219 = e.boolean;
		local_NPCTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_219;
		Relay_Save_Out_219();
	}

	private void SubGraph_SaveLoadBool_Load_Out_219(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_219 = e.boolean;
		local_NPCTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_219;
		Relay_Load_Out_219();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_219(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_219 = e.boolean;
		local_NPCTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_219;
		Relay_Restart_Out_219();
	}

	private void SubGraph_SaveLoadBool_Save_Out_221(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_221 = e.boolean;
		local_NPCTechSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_221;
		Relay_Save_Out_221();
	}

	private void SubGraph_SaveLoadBool_Load_Out_221(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_221 = e.boolean;
		local_NPCTechSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_221;
		Relay_Load_Out_221();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_221(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_221 = e.boolean;
		local_NPCTechSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_221;
		Relay_Restart_Out_221();
	}

	private void SubGraph_SaveLoadBool_Save_Out_223(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_223 = e.boolean;
		local_CrazedAmbushTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_223;
		Relay_Save_Out_223();
	}

	private void SubGraph_SaveLoadBool_Load_Out_223(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_223 = e.boolean;
		local_CrazedAmbushTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_223;
		Relay_Load_Out_223();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_223(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_223 = e.boolean;
		local_CrazedAmbushTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_223;
		Relay_Restart_Out_223();
	}

	private void SubGraph_SaveLoadBool_Save_Out_225(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_225 = e.boolean;
		local_FirstCubeSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_225;
		Relay_Save_Out_225();
	}

	private void SubGraph_SaveLoadBool_Load_Out_225(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_225 = e.boolean;
		local_FirstCubeSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_225;
		Relay_Load_Out_225();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_225(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_225 = e.boolean;
		local_FirstCubeSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_225;
		Relay_Restart_Out_225();
	}

	private void SubGraph_SaveLoadBool_Save_Out_227(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_227 = e.boolean;
		local_CubeisOK_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_227;
		Relay_Save_Out_227();
	}

	private void SubGraph_SaveLoadBool_Load_Out_227(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_227 = e.boolean;
		local_CubeisOK_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_227;
		Relay_Load_Out_227();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_227(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_227 = e.boolean;
		local_CubeisOK_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_227;
		Relay_Restart_Out_227();
	}

	private void SubGraph_SaveLoadBool_Save_Out_244(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_244 = e.boolean;
		local_TankInvul_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_244;
		Relay_Save_Out_244();
	}

	private void SubGraph_SaveLoadBool_Load_Out_244(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_244 = e.boolean;
		local_TankInvul_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_244;
		Relay_Load_Out_244();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_244(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_244 = e.boolean;
		local_TankInvul_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_244;
		Relay_Restart_Out_244();
	}

	private void SubGraph_SaveLoadBool_Save_Out_245(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_245 = e.boolean;
		local_PlayInterruptOnce_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_245;
		Relay_Save_Out_245();
	}

	private void SubGraph_SaveLoadBool_Load_Out_245(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_245 = e.boolean;
		local_PlayInterruptOnce_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_245;
		Relay_Load_Out_245();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_245(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_245 = e.boolean;
		local_PlayInterruptOnce_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_245;
		Relay_Restart_Out_245();
	}

	private void SubGraph_SaveLoadBool_Save_Out_246(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_246 = e.boolean;
		local_CubeNeedsReload_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_246;
		Relay_Save_Out_246();
	}

	private void SubGraph_SaveLoadBool_Load_Out_246(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_246 = e.boolean;
		local_CubeNeedsReload_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_246;
		Relay_Load_Out_246();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_246(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_246 = e.boolean;
		local_CubeNeedsReload_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_246;
		Relay_Restart_Out_246();
	}

	private void SubGraph_SaveLoadBool_Save_Out_247(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_247 = e.boolean;
		local_CrazedIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_247;
		Relay_Save_Out_247();
	}

	private void SubGraph_SaveLoadBool_Load_Out_247(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_247 = e.boolean;
		local_CrazedIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_247;
		Relay_Load_Out_247();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_247(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_247 = e.boolean;
		local_CrazedIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_247;
		Relay_Restart_Out_247();
	}

	private void SubGraph_SaveLoadBool_Save_Out_248(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_248 = e.boolean;
		local_CrazedNPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_248;
		Relay_Save_Out_248();
	}

	private void SubGraph_SaveLoadBool_Load_Out_248(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_248 = e.boolean;
		local_CrazedNPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_248;
		Relay_Load_Out_248();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_248(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_248 = e.boolean;
		local_CrazedNPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_248;
		Relay_Restart_Out_248();
	}

	private void SubGraph_SaveLoadBool_Save_Out_249(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_249 = e.boolean;
		local_CubeDeadVictory_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_249;
		Relay_Save_Out_249();
	}

	private void SubGraph_SaveLoadBool_Load_Out_249(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_249 = e.boolean;
		local_CubeDeadVictory_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_249;
		Relay_Load_Out_249();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_249(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_249 = e.boolean;
		local_CubeDeadVictory_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_249;
		Relay_Restart_Out_249();
	}

	private void SubGraph_SaveLoadBool_Save_Out_250(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = e.boolean;
		local_HasBeenInterrupted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_250;
		Relay_Save_Out_250();
	}

	private void SubGraph_SaveLoadBool_Load_Out_250(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = e.boolean;
		local_HasBeenInterrupted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_250;
		Relay_Load_Out_250();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_250(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = e.boolean;
		local_HasBeenInterrupted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_250;
		Relay_Restart_Out_250();
	}

	private void SubGraph_SaveLoadBool_Save_Out_251(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = e.boolean;
		local_CrazedNPCInRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_251;
		Relay_Save_Out_251();
	}

	private void SubGraph_SaveLoadBool_Load_Out_251(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = e.boolean;
		local_CrazedNPCInRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_251;
		Relay_Load_Out_251();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_251(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = e.boolean;
		local_CrazedNPCInRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_251;
		Relay_Restart_Out_251();
	}

	private void SubGraph_SaveLoadBool_Save_Out_252(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = e.boolean;
		local_CrazedPlayInterruptOnce_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_252;
		Relay_Save_Out_252();
	}

	private void SubGraph_SaveLoadBool_Load_Out_252(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = e.boolean;
		local_CrazedPlayInterruptOnce_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_252;
		Relay_Load_Out_252();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_252(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = e.boolean;
		local_CrazedPlayInterruptOnce_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_252;
		Relay_Restart_Out_252();
	}

	private void SubGraph_SaveLoadBool_Save_Out_253(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_253 = e.boolean;
		local_PlayerAttemptedMission_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_253;
		Relay_Save_Out_253();
	}

	private void SubGraph_SaveLoadBool_Load_Out_253(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_253 = e.boolean;
		local_PlayerAttemptedMission_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_253;
		Relay_Load_Out_253();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_253(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_253 = e.boolean;
		local_PlayerAttemptedMission_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_253;
		Relay_Restart_Out_253();
	}

	private void SubGraph_SaveLoadBool_Save_Out_254(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_254 = e.boolean;
		local_LeftAreaAfterLoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_254;
		Relay_Save_Out_254();
	}

	private void SubGraph_SaveLoadBool_Load_Out_254(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_254 = e.boolean;
		local_LeftAreaAfterLoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_254;
		Relay_Load_Out_254();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_254(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_254 = e.boolean;
		local_LeftAreaAfterLoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_254;
		Relay_Restart_Out_254();
	}

	private void SubGraph_SaveLoadBool_Save_Out_255(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = e.boolean;
		local_MsgCubeIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_255;
		Relay_Save_Out_255();
	}

	private void SubGraph_SaveLoadBool_Load_Out_255(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = e.boolean;
		local_MsgCubeIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_255;
		Relay_Load_Out_255();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_255(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = e.boolean;
		local_MsgCubeIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_255;
		Relay_Restart_Out_255();
	}

	private void SubGraph_SaveLoadBool_Save_Out_256(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_256 = e.boolean;
		local_CubeDestroyedMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_256;
		Relay_Save_Out_256();
	}

	private void SubGraph_SaveLoadBool_Load_Out_256(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_256 = e.boolean;
		local_CubeDestroyedMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_256;
		Relay_Load_Out_256();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_256(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_256 = e.boolean;
		local_CubeDestroyedMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_256;
		Relay_Restart_Out_256();
	}

	private void SubGraph_SaveLoadBool_Save_Out_257(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = e.boolean;
		local_WentOutOfRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_257;
		Relay_Save_Out_257();
	}

	private void SubGraph_SaveLoadBool_Load_Out_257(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = e.boolean;
		local_WentOutOfRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_257;
		Relay_Load_Out_257();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_257(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = e.boolean;
		local_WentOutOfRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_257;
		Relay_Restart_Out_257();
	}

	private void SubGraph_SaveLoadBool_Save_Out_258(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_258 = e.boolean;
		local_CubeDeadVictory_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_258;
		Relay_Save_Out_258();
	}

	private void SubGraph_SaveLoadBool_Load_Out_258(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_258 = e.boolean;
		local_CubeDeadVictory_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_258;
		Relay_Load_Out_258();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_258(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_258 = e.boolean;
		local_CubeDeadVictory_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_258;
		Relay_Restart_Out_258();
	}

	private void SubGraph_SaveLoadBool_Save_Out_259(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_259 = e.boolean;
		local_OutOfTime_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_259;
		Relay_Save_Out_259();
	}

	private void SubGraph_SaveLoadBool_Load_Out_259(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_259 = e.boolean;
		local_OutOfTime_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_259;
		Relay_Load_Out_259();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_259(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_259 = e.boolean;
		local_OutOfTime_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_259;
		Relay_Restart_Out_259();
	}

	private void SubGraph_CompleteObjectiveStage_Out_260(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_260 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_260;
		Relay_Out_260();
	}

	private void uScriptCon_ManualSwitch_Output1_284(object o, EventArgs e)
	{
		Relay_Output1_284();
	}

	private void uScriptCon_ManualSwitch_Output2_284(object o, EventArgs e)
	{
		Relay_Output2_284();
	}

	private void uScriptCon_ManualSwitch_Output3_284(object o, EventArgs e)
	{
		Relay_Output3_284();
	}

	private void uScriptCon_ManualSwitch_Output4_284(object o, EventArgs e)
	{
		Relay_Output4_284();
	}

	private void uScriptCon_ManualSwitch_Output5_284(object o, EventArgs e)
	{
		Relay_Output5_284();
	}

	private void uScriptCon_ManualSwitch_Output6_284(object o, EventArgs e)
	{
		Relay_Output6_284();
	}

	private void uScriptCon_ManualSwitch_Output7_284(object o, EventArgs e)
	{
		Relay_Output7_284();
	}

	private void uScriptCon_ManualSwitch_Output8_284(object o, EventArgs e)
	{
		Relay_Output8_284();
	}

	private void SubGraph_CompleteObjectiveStage_Out_296(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_296 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_296;
		Relay_Out_296();
	}

	private void uScriptCon_ManualSwitch_Output1_304(object o, EventArgs e)
	{
		Relay_Output1_304();
	}

	private void uScriptCon_ManualSwitch_Output2_304(object o, EventArgs e)
	{
		Relay_Output2_304();
	}

	private void uScriptCon_ManualSwitch_Output3_304(object o, EventArgs e)
	{
		Relay_Output3_304();
	}

	private void uScriptCon_ManualSwitch_Output4_304(object o, EventArgs e)
	{
		Relay_Output4_304();
	}

	private void uScriptCon_ManualSwitch_Output5_304(object o, EventArgs e)
	{
		Relay_Output5_304();
	}

	private void uScriptCon_ManualSwitch_Output6_304(object o, EventArgs e)
	{
		Relay_Output6_304();
	}

	private void uScriptCon_ManualSwitch_Output7_304(object o, EventArgs e)
	{
		Relay_Output7_304();
	}

	private void uScriptCon_ManualSwitch_Output8_304(object o, EventArgs e)
	{
		Relay_Output8_304();
	}

	private void uScriptCon_ManualSwitch_Output1_311(object o, EventArgs e)
	{
		Relay_Output1_311();
	}

	private void uScriptCon_ManualSwitch_Output2_311(object o, EventArgs e)
	{
		Relay_Output2_311();
	}

	private void uScriptCon_ManualSwitch_Output3_311(object o, EventArgs e)
	{
		Relay_Output3_311();
	}

	private void uScriptCon_ManualSwitch_Output4_311(object o, EventArgs e)
	{
		Relay_Output4_311();
	}

	private void uScriptCon_ManualSwitch_Output5_311(object o, EventArgs e)
	{
		Relay_Output5_311();
	}

	private void uScriptCon_ManualSwitch_Output6_311(object o, EventArgs e)
	{
		Relay_Output6_311();
	}

	private void uScriptCon_ManualSwitch_Output7_311(object o, EventArgs e)
	{
		Relay_Output7_311();
	}

	private void uScriptCon_ManualSwitch_Output8_311(object o, EventArgs e)
	{
		Relay_Output8_311();
	}

	private void SubGraph_SaveLoadBool_Save_Out_572(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_572 = e.boolean;
		local_CubeDestroyedFly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_572;
		Relay_Save_Out_572();
	}

	private void SubGraph_SaveLoadBool_Load_Out_572(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_572 = e.boolean;
		local_CubeDestroyedFly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_572;
		Relay_Load_Out_572();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_572(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_572 = e.boolean;
		local_CubeDestroyedFly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_572;
		Relay_Restart_Out_572();
	}

	private void SubGraph_SaveLoadBool_Save_Out_573(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_573 = e.boolean;
		local_CubeTooEarlyMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_573;
		Relay_Save_Out_573();
	}

	private void SubGraph_SaveLoadBool_Load_Out_573(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_573 = e.boolean;
		local_CubeTooEarlyMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_573;
		Relay_Load_Out_573();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_573(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_573 = e.boolean;
		local_CubeTooEarlyMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_573;
		Relay_Restart_Out_573();
	}

	private void SubGraph_SaveLoadBool_Save_Out_574(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_574 = e.boolean;
		local_PlayerLeftMissionArea_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_574;
		Relay_Save_Out_574();
	}

	private void SubGraph_SaveLoadBool_Load_Out_574(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_574 = e.boolean;
		local_PlayerLeftMissionArea_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_574;
		Relay_Load_Out_574();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_574(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_574 = e.boolean;
		local_PlayerLeftMissionArea_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_574;
		Relay_Restart_Out_574();
	}

	private void SubGraph_SaveLoadBool_Save_Out_575(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_575 = e.boolean;
		local_PlayerAttemptedMission_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_575;
		Relay_Save_Out_575();
	}

	private void SubGraph_SaveLoadBool_Load_Out_575(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_575 = e.boolean;
		local_PlayerAttemptedMission_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_575;
		Relay_Load_Out_575();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_575(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_575 = e.boolean;
		local_PlayerAttemptedMission_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_575;
		Relay_Restart_Out_575();
	}

	private void SubGraph_SaveLoadBool_Save_Out_576(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_576 = e.boolean;
		local_PlayedTryAgainMsg_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_576;
		Relay_Save_Out_576();
	}

	private void SubGraph_SaveLoadBool_Load_Out_576(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_576 = e.boolean;
		local_PlayedTryAgainMsg_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_576;
		Relay_Load_Out_576();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_576(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_576 = e.boolean;
		local_PlayedTryAgainMsg_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_576;
		Relay_Restart_Out_576();
	}

	private void SubGraph_SaveLoadBool_Save_Out_577(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_577 = e.boolean;
		local_FlyLeaderAway_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_577;
		Relay_Save_Out_577();
	}

	private void SubGraph_SaveLoadBool_Load_Out_577(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_577 = e.boolean;
		local_FlyLeaderAway_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_577;
		Relay_Load_Out_577();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_577(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_577 = e.boolean;
		local_FlyLeaderAway_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_577;
		Relay_Restart_Out_577();
	}

	private void SubGraph_SaveLoadBool_Save_Out_584(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_584 = e.boolean;
		local_CrazedAmbushNotTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_584;
		Relay_Save_Out_584();
	}

	private void SubGraph_SaveLoadBool_Load_Out_584(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_584 = e.boolean;
		local_CrazedAmbushNotTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_584;
		Relay_Load_Out_584();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_584(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_584 = e.boolean;
		local_CrazedAmbushNotTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_584;
		Relay_Restart_Out_584();
	}

	private void SubGraph_SaveLoadBool_Save_Out_593(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_593 = e.boolean;
		local_FightRunning_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_593;
		Relay_Save_Out_593();
	}

	private void SubGraph_SaveLoadBool_Load_Out_593(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_593 = e.boolean;
		local_FightRunning_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_593;
		Relay_Load_Out_593();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_593(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_593 = e.boolean;
		local_FightRunning_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_593;
		Relay_Restart_Out_593();
	}

	private void SubGraph_SaveLoadBool_Save_Out_594(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_594 = e.boolean;
		local_FightStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_594;
		Relay_Save_Out_594();
	}

	private void SubGraph_SaveLoadBool_Load_Out_594(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_594 = e.boolean;
		local_FightStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_594;
		Relay_Load_Out_594();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_594(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_594 = e.boolean;
		local_FightStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_594;
		Relay_Restart_Out_594();
	}

	private void SubGraph_SaveLoadBool_Save_Out_601(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_601 = e.boolean;
		local_DestroyCube_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_601;
		Relay_Save_Out_601();
	}

	private void SubGraph_SaveLoadBool_Load_Out_601(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_601 = e.boolean;
		local_DestroyCube_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_601;
		Relay_Load_Out_601();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_601(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_601 = e.boolean;
		local_DestroyCube_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_601;
		Relay_Restart_Out_601();
	}

	private void SubGraph_SaveLoadBool_Save_Out_625(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_625 = e.boolean;
		local_MinionWaveTechsDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_625;
		Relay_Save_Out_625();
	}

	private void SubGraph_SaveLoadBool_Load_Out_625(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_625 = e.boolean;
		local_MinionWaveTechsDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_625;
		Relay_Load_Out_625();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_625(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_625 = e.boolean;
		local_MinionWaveTechsDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_625;
		Relay_Restart_Out_625();
	}

	private void SubGraph_SaveLoadBool_Save_Out_626(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_626 = e.boolean;
		local_CrazedTechsAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_626;
		Relay_Save_Out_626();
	}

	private void SubGraph_SaveLoadBool_Load_Out_626(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_626 = e.boolean;
		local_CrazedTechsAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_626;
		Relay_Load_Out_626();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_626(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_626 = e.boolean;
		local_CrazedTechsAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_626;
		Relay_Restart_Out_626();
	}

	private void SubGraph_SaveLoadBool_Save_Out_670(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_670 = e.boolean;
		local_TechInvulOnLoad_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_670;
		Relay_Save_Out_670();
	}

	private void SubGraph_SaveLoadBool_Load_Out_670(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_670 = e.boolean;
		local_TechInvulOnLoad_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_670;
		Relay_Load_Out_670();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_670(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_670 = e.boolean;
		local_TechInvulOnLoad_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_670;
		Relay_Restart_Out_670();
	}

	private void SubGraph_SaveLoadBool_Save_Out_696(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_696 = e.boolean;
		local_TurnEnemiesAfterCubeDeath_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_696;
		Relay_Save_Out_696();
	}

	private void SubGraph_SaveLoadBool_Load_Out_696(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_696 = e.boolean;
		local_TurnEnemiesAfterCubeDeath_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_696;
		Relay_Load_Out_696();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_696(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_696 = e.boolean;
		local_TurnEnemiesAfterCubeDeath_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_696;
		Relay_Restart_Out_696();
	}

	private void SubGraph_SaveLoadBool_Save_Out_716(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_716 = e.boolean;
		local_CrazedIntroPlayedDoubleCheck_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_716;
		Relay_Save_Out_716();
	}

	private void SubGraph_SaveLoadBool_Load_Out_716(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_716 = e.boolean;
		local_CrazedIntroPlayedDoubleCheck_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_716;
		Relay_Load_Out_716();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_716(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_716 = e.boolean;
		local_CrazedIntroPlayedDoubleCheck_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_716;
		Relay_Restart_Out_716();
	}

	private void SubGraph_LoadObjectiveStates_Out_754(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_754();
	}

	private void SubGraph_SaveLoadBool_Save_Out_829(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_829 = e.boolean;
		local_PlayerInRangeOfCube_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_829;
		Relay_Save_Out_829();
	}

	private void SubGraph_SaveLoadBool_Load_Out_829(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_829 = e.boolean;
		local_PlayerInRangeOfCube_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_829;
		Relay_Load_Out_829();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_829(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_829 = e.boolean;
		local_PlayerInRangeOfCube_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_829;
		Relay_Restart_Out_829();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_6();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_SaveEvent_3()
	{
		Relay_Save_11();
	}

	private void Relay_LoadEvent_3()
	{
		Relay_Load_11();
	}

	private void Relay_RestartEvent_3()
	{
		Relay_Restart_11();
	}

	private void Relay_In_6()
	{
		logic_uScriptCon_CompareBool_Bool_6 = local_NPCTechSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_6.In(logic_uScriptCon_CompareBool_Bool_6);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_6.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_6.False;
		if (num)
		{
			Relay_In_870();
		}
		if (flag)
		{
			Relay_InitialSpawn_544();
		}
	}

	private void Relay_Save_Out_7()
	{
		Relay_Save_625();
	}

	private void Relay_Load_Out_7()
	{
		Relay_Load_625();
	}

	private void Relay_Restart_Out_7()
	{
		Relay_Set_False_625();
	}

	private void Relay_Save_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_FinalObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_FinalObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Load_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_FinalObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_FinalObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Set_True_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_FinalObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_FinalObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Set_False_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_FinalObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_FinalObjectiveComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_In_9()
	{
		logic_uScriptCon_CompareBool_Bool_9 = local_FinalObjectiveComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_9.In(logic_uScriptCon_CompareBool_Bool_9);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_9.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_9.False;
		if (num)
		{
			Relay_In_195();
		}
		if (flag)
		{
			Relay_In_95();
		}
	}

	private void Relay_Save_Out_11()
	{
		Relay_Save_829();
	}

	private void Relay_Load_Out_11()
	{
		Relay_Load_829();
	}

	private void Relay_Restart_Out_11()
	{
		Relay_Set_False_829();
	}

	private void Relay_Save_11()
	{
		logic_SubGraph_SaveLoadInt_integer_11 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_11 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Save(logic_SubGraph_SaveLoadInt_restartValue_11, ref logic_SubGraph_SaveLoadInt_integer_11, logic_SubGraph_SaveLoadInt_intAsVariable_11, logic_SubGraph_SaveLoadInt_uniqueID_11);
	}

	private void Relay_Load_11()
	{
		logic_SubGraph_SaveLoadInt_integer_11 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_11 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Load(logic_SubGraph_SaveLoadInt_restartValue_11, ref logic_SubGraph_SaveLoadInt_integer_11, logic_SubGraph_SaveLoadInt_intAsVariable_11, logic_SubGraph_SaveLoadInt_uniqueID_11);
	}

	private void Relay_Restart_11()
	{
		logic_SubGraph_SaveLoadInt_integer_11 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_11 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Restart(logic_SubGraph_SaveLoadInt_restartValue_11, ref logic_SubGraph_SaveLoadInt_integer_11, logic_SubGraph_SaveLoadInt_intAsVariable_11, logic_SubGraph_SaveLoadInt_uniqueID_11);
	}

	private void Relay_InitialSpawn_13()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_13.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_13, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_13, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_13 = owner_Connection_26;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_13.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_13, logic_uScript_SpawnTechsFromData_ownerNode_13, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_13, logic_uScript_SpawnTechsFromData_allowResurrection_13);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_13.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_True_14()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_14.True(out logic_uScriptAct_SetBool_Target_14);
		local_NPCTechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_14;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_14.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_False_14()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_14.False(out logic_uScriptAct_SetBool_Target_14);
		local_NPCTechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_14;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_14.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_InitialSpawn_17()
	{
		int num = 0;
		Array crazedTechData = CrazedTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_17.Length != num + crazedTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_17, num + crazedTechData.Length);
		}
		Array.Copy(crazedTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_17, num, crazedTechData.Length);
		num += crazedTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_17 = owner_Connection_16;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_17.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_17, logic_uScript_SpawnTechsFromData_ownerNode_17, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_17, logic_uScript_SpawnTechsFromData_allowResurrection_17);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_17.Out)
		{
			Relay_InitialSpawn_370();
		}
	}

	private void Relay_In_21()
	{
		int num = 0;
		Array crazedTechData = CrazedTechData;
		if (logic_uScript_GetAndCheckTechs_techData_21.Length != num + crazedTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_21, num + crazedTechData.Length);
		}
		Array.Copy(crazedTechData, 0, logic_uScript_GetAndCheckTechs_techData_21, num, crazedTechData.Length);
		num += crazedTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_21 = owner_Connection_19;
		int num2 = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_21.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_21, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_21, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_21 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21.In(logic_uScript_GetAndCheckTechs_techData_21, logic_uScript_GetAndCheckTechs_ownerNode_21, ref logic_uScript_GetAndCheckTechs_techs_21);
		local_CrazedTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_21;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_21.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_262();
		}
		if (someAlive)
		{
			Relay_AtIndex_262();
		}
	}

	private void Relay_In_23()
	{
		logic_uScriptCon_CompareBool_Bool_23 = local_FirstCubeSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.In(logic_uScriptCon_CompareBool_Bool_23);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.False;
		if (num)
		{
			Relay_In_604();
		}
		if (flag)
		{
			Relay_InitialSpawn_13();
		}
	}

	private void Relay_True_27()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_27.True(out logic_uScriptAct_SetBool_Target_27);
		local_FirstCubeSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_27;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_27.Out)
		{
			Relay_True_134();
		}
	}

	private void Relay_False_27()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_27.False(out logic_uScriptAct_SetBool_Target_27);
		local_FirstCubeSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_27;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_27.Out)
		{
			Relay_True_134();
		}
	}

	private void Relay_In_29()
	{
		logic_uScriptCon_CompareBool_Bool_29 = local_CubeDeadVictory_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.In(logic_uScriptCon_CompareBool_Bool_29);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.False;
		if (num)
		{
			Relay_In_121();
		}
		if (flag)
		{
			Relay_In_35();
		}
	}

	private void Relay_In_31()
	{
		logic_uScriptCon_CompareBool_Bool_31 = local_CrazedIntroPlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.In(logic_uScriptCon_CompareBool_Bool_31);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.False;
		if (num)
		{
			Relay_In_787();
		}
		if (flag)
		{
			Relay_In_502();
		}
	}

	private void Relay_Succeed_33()
	{
		logic_uScript_FinishEncounter_owner_33 = owner_Connection_34;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_33.Succeed(logic_uScript_FinishEncounter_owner_33);
	}

	private void Relay_Fail_33()
	{
		logic_uScript_FinishEncounter_owner_33 = owner_Connection_34;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_33.Fail(logic_uScript_FinishEncounter_owner_33);
	}

	private void Relay_In_35()
	{
		logic_uScriptCon_CompareBool_Bool_35 = local_PlayerAttemptedMission_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.In(logic_uScriptCon_CompareBool_Bool_35);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.False;
		if (num)
		{
			Relay_In_45();
		}
		if (flag)
		{
			Relay_In_824();
		}
	}

	private void Relay_Pause_37()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_37.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_37.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_UnPause_37()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_37.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_37.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_In_38()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_38 = owner_Connection_50;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_38.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_38);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_38.Out)
		{
			Relay_True_14();
		}
	}

	private void Relay_AtIndex_39()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_39.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_39, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_39, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_39.AtIndex(ref logic_uScript_AccessListTech_techList_39, logic_uScript_AccessListTech_index_39, out logic_uScript_AccessListTech_value_39);
		local_CubeTechs_TankArray = logic_uScript_AccessListTech_techList_39;
		local_CubeTech_Tank = logic_uScript_AccessListTech_value_39;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_39.Out)
		{
			Relay_In_457();
		}
	}

	private void Relay_In_41()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_41.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_41, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_41, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_41 = owner_Connection_40;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_41.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_41, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_41, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_41 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_41.In(logic_uScript_GetAndCheckTechs_techData_41, logic_uScript_GetAndCheckTechs_ownerNode_41, ref logic_uScript_GetAndCheckTechs_techs_41);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_41;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_41.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_41.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_39();
		}
		if (someAlive)
		{
			Relay_AtIndex_39();
		}
	}

	private void Relay_In_45()
	{
		logic_uScriptCon_CompareBool_Bool_45 = local_LeftAreaAfterLoss_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.In(logic_uScriptCon_CompareBool_Bool_45);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.False;
		if (num)
		{
			Relay_In_511();
		}
		if (flag)
		{
			Relay_In_774();
		}
	}

	private void Relay_In_49()
	{
		logic_uScript_AddMessage_messageData_49 = MsgCubeLeaveAreaFail;
		logic_uScript_AddMessage_speaker_49 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_49 = logic_uScript_AddMessage_uScript_AddMessage_49.In(logic_uScript_AddMessage_messageData_49, logic_uScript_AddMessage_speaker_49);
		if (logic_uScript_AddMessage_uScript_AddMessage_49.Out)
		{
			Relay_False_216();
		}
	}

	private void Relay_In_52()
	{
		logic_uScriptCon_CompareBool_Bool_52 = local_NPCTechSetup_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_52.In(logic_uScriptCon_CompareBool_Bool_52);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_52.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_52.False;
		if (num)
		{
			Relay_In_23();
		}
		if (flag)
		{
			Relay_In_643();
		}
	}

	private void Relay_True_53()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.True(out logic_uScriptAct_SetBool_Target_53);
		local_NPCTechSetup_System_Boolean = logic_uScriptAct_SetBool_Target_53;
	}

	private void Relay_False_53()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.False(out logic_uScriptAct_SetBool_Target_53);
		local_NPCTechSetup_System_Boolean = logic_uScriptAct_SetBool_Target_53;
	}

	private void Relay_In_55()
	{
		logic_uScript_SetEncounterTarget_owner_55 = owner_Connection_56;
		logic_uScript_SetEncounterTarget_visibleObject_55 = local_CrazedTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_55.In(logic_uScript_SetEncounterTarget_owner_55, logic_uScript_SetEncounterTarget_visibleObject_55);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_55.Out)
		{
			Relay_True_53();
		}
	}

	private void Relay_In_61()
	{
		logic_uScriptCon_CompareBool_Bool_61 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_61.In(logic_uScriptCon_CompareBool_Bool_61);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_61.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_61.False;
		if (num)
		{
			Relay_In_78();
		}
		if (flag)
		{
			Relay_In_772();
		}
	}

	private void Relay_In_62()
	{
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_62.In(logic_uScript_PlayMiscSFX_miscSFXType_62);
		if (logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_62.Out)
		{
			Relay_In_157();
		}
	}

	private void Relay_True_63()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.True(out logic_uScriptAct_SetBool_Target_63);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_63;
	}

	private void Relay_False_63()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.False(out logic_uScriptAct_SetBool_Target_63);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_63;
	}

	private void Relay_True_64()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_64.True(out logic_uScriptAct_SetBool_Target_64);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_64;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_64.Out)
		{
			Relay_False_136();
		}
	}

	private void Relay_False_64()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_64.False(out logic_uScriptAct_SetBool_Target_64);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_64;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_64.Out)
		{
			Relay_False_136();
		}
	}

	private void Relay_In_66()
	{
		logic_uScript_ShowMissionTimerUI_owner_66 = owner_Connection_73;
		logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_66.In(logic_uScript_ShowMissionTimerUI_owner_66, logic_uScript_ShowMissionTimerUI_showBestTime_66);
		if (logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_66.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_In_68()
	{
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_68.In(logic_uScript_PlayMiscSFX_miscSFXType_68);
		if (logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_68.Out)
		{
			Relay_True_153();
		}
	}

	private void Relay_True_69()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_69.True(out logic_uScriptAct_SetBool_Target_69);
		local_CubeDeadVictory_System_Boolean = logic_uScriptAct_SetBool_Target_69;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_69.Out)
		{
			Relay_In_112();
		}
	}

	private void Relay_False_69()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_69.False(out logic_uScriptAct_SetBool_Target_69);
		local_CubeDeadVictory_System_Boolean = logic_uScriptAct_SetBool_Target_69;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_69.Out)
		{
			Relay_In_112();
		}
	}

	private void Relay_True_70()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_70.True(out logic_uScriptAct_SetBool_Target_70);
		local_MsgCubeIntroPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_70;
	}

	private void Relay_False_70()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_70.False(out logic_uScriptAct_SetBool_Target_70);
		local_MsgCubeIntroPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_70;
	}

	private void Relay_In_71()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_71 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_71.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_71);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_71.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_71.OutOfRange;
		if (inRange)
		{
			Relay_In_821();
		}
		if (outOfRange)
		{
			Relay_In_789();
		}
	}

	private void Relay_In_72()
	{
		logic_uScript_AddMessage_messageData_72 = MsgCubeIntro01;
		logic_uScript_AddMessage_speaker_72 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_72 = logic_uScript_AddMessage_uScript_AddMessage_72.In(logic_uScript_AddMessage_messageData_72, logic_uScript_AddMessage_speaker_72);
		if (logic_uScript_AddMessage_uScript_AddMessage_72.Shown)
		{
			Relay_In_305();
		}
	}

	private void Relay_In_78()
	{
		logic_uScript_StartMissionTimer_owner_78 = owner_Connection_73;
		logic_uScript_StartMissionTimer_startTime_78 = TimeLimit;
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_78.In(logic_uScript_StartMissionTimer_owner_78, logic_uScript_StartMissionTimer_startTime_78);
		if (logic_uScript_StartMissionTimer_uScript_StartMissionTimer_78.Out)
		{
			Relay_In_66();
		}
	}

	private void Relay_In_80()
	{
		logic_uScriptCon_CompareFloat_A_80 = local_58_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_80.In(logic_uScriptCon_CompareFloat_A_80, logic_uScriptCon_CompareFloat_B_80);
		bool greaterThan = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_80.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_80.LessThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_80.LessThan;
		if (greaterThan)
		{
			Relay_In_790();
		}
		if (lessThanOrEqualTo)
		{
			Relay_True_208();
		}
		if (lessThan)
		{
			Relay_True_208();
		}
	}

	private void Relay_In_86()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_86.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_86, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_86, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_86 = owner_Connection_65;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_86.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_86, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_86, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_86 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_86.In(logic_uScript_GetAndCheckTechs_techData_86, logic_uScript_GetAndCheckTechs_ownerNode_86, ref logic_uScript_GetAndCheckTechs_techs_86);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_86;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_86.AllDead)
		{
			Relay_AtIndex_183();
		}
	}

	private void Relay_In_87()
	{
		logic_uScript_GetMissionTimerDisplayTime_owner_87 = owner_Connection_60;
		logic_uScript_GetMissionTimerDisplayTime_Return_87 = logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_87.In(logic_uScript_GetMissionTimerDisplayTime_owner_87);
		local_58_System_Single = logic_uScript_GetMissionTimerDisplayTime_Return_87;
		if (logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_87.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_True_89()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_89.True(out logic_uScriptAct_SetBool_Target_89);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_89;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_89.Out)
		{
			Relay_False_193();
		}
	}

	private void Relay_False_89()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_89.False(out logic_uScriptAct_SetBool_Target_89);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_89;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_89.Out)
		{
			Relay_False_193();
		}
	}

	private void Relay_In_91()
	{
		logic_uScript_StopMissionTimer_owner_91 = owner_Connection_83;
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_91.In(logic_uScript_StopMissionTimer_owner_91);
		if (logic_uScript_StopMissionTimer_uScript_StopMissionTimer_91.Out)
		{
			Relay_In_62();
		}
	}

	private void Relay_In_93()
	{
		logic_uScriptCon_CompareBool_Bool_93 = local_FightStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93.In(logic_uScriptCon_CompareBool_Bool_93);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93.False;
		if (num)
		{
			Relay_In_186();
		}
		if (flag)
		{
			Relay_In_660();
		}
	}

	private void Relay_In_95()
	{
		logic_uScriptCon_CompareBool_Bool_95 = local_CubeisOK_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.In(logic_uScriptCon_CompareBool_Bool_95);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.False;
		if (num)
		{
			Relay_In_93();
		}
		if (flag)
		{
			Relay_In_114();
		}
	}

	private void Relay_AtIndex_98()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_98.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_98, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_98, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_98.AtIndex(ref logic_uScript_AccessListTech_techList_98, logic_uScript_AccessListTech_index_98, out logic_uScript_AccessListTech_value_98);
		local_CubeTechs_TankArray = logic_uScript_AccessListTech_techList_98;
		local_CubeTech_Tank = logic_uScript_AccessListTech_value_98;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_98.Out)
		{
			Relay_In_451();
		}
	}

	private void Relay_InitialSpawn_101()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_101.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_101, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_101, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_101 = owner_Connection_99;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_101.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_101, logic_uScript_SpawnTechsFromData_ownerNode_101, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_101, logic_uScript_SpawnTechsFromData_allowResurrection_101);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_101.Out)
		{
			Relay_True_651();
		}
	}

	private void Relay_In_103()
	{
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_103.In(logic_uScript_PlayMiscSFX_miscSFXType_103);
		if (logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_103.Out)
		{
			Relay_True_147();
		}
	}

	private void Relay_In_105()
	{
		logic_uScript_ShowMissionTimerUI_owner_105 = owner_Connection_102;
		logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_105.In(logic_uScript_ShowMissionTimerUI_owner_105, logic_uScript_ShowMissionTimerUI_showBestTime_105);
		if (logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_105.Out)
		{
			Relay_In_103();
		}
	}

	private void Relay_In_106()
	{
		logic_uScript_StartMissionTimer_owner_106 = owner_Connection_102;
		logic_uScript_StartMissionTimer_startTime_106 = TimeLimit;
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_106.In(logic_uScript_StartMissionTimer_owner_106, logic_uScript_StartMissionTimer_startTime_106);
		if (logic_uScript_StartMissionTimer_uScript_StartMissionTimer_106.Out)
		{
			Relay_In_105();
		}
	}

	private void Relay_True_107()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_107.True(out logic_uScriptAct_SetBool_Target_107);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_107;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_107.Out)
		{
			Relay_False_178();
		}
	}

	private void Relay_False_107()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_107.False(out logic_uScriptAct_SetBool_Target_107);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_107;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_107.Out)
		{
			Relay_False_178();
		}
	}

	private void Relay_True_109()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.True(out logic_uScriptAct_SetBool_Target_109);
		local_LeftAreaAfterLoss_System_Boolean = logic_uScriptAct_SetBool_Target_109;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_109.Out)
		{
			Relay_True_119();
		}
	}

	private void Relay_False_109()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.False(out logic_uScriptAct_SetBool_Target_109);
		local_LeftAreaAfterLoss_System_Boolean = logic_uScriptAct_SetBool_Target_109;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_109.Out)
		{
			Relay_True_119();
		}
	}

	private void Relay_In_112()
	{
		logic_uScript_StopMissionTimer_owner_112 = owner_Connection_111;
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_112.In(logic_uScript_StopMissionTimer_owner_112);
		if (logic_uScript_StopMissionTimer_uScript_StopMissionTimer_112.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_In_114()
	{
		logic_uScriptCon_CompareBool_Bool_114 = local_CubeNeedsReload_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114.In(logic_uScriptCon_CompareBool_Bool_114);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114.False;
		if (num)
		{
			Relay_In_596();
		}
		if (flag)
		{
			Relay_In_93();
		}
	}

	private void Relay_In_116()
	{
		logic_uScript_HideMissionTimerUI_owner_116 = owner_Connection_856;
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_116.In(logic_uScript_HideMissionTimerUI_owner_116);
		if (logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_116.Out)
		{
			Relay_In_260();
		}
	}

	private void Relay_True_119()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_119.True(out logic_uScriptAct_SetBool_Target_119);
		local_CubeNeedsReload_System_Boolean = logic_uScriptAct_SetBool_Target_119;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_119.Out)
		{
			Relay_False_145();
		}
	}

	private void Relay_False_119()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_119.False(out logic_uScriptAct_SetBool_Target_119);
		local_CubeNeedsReload_System_Boolean = logic_uScriptAct_SetBool_Target_119;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_119.Out)
		{
			Relay_False_145();
		}
	}

	private void Relay_In_121()
	{
		logic_uScriptCon_CompareBool_Bool_121 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.In(logic_uScriptCon_CompareBool_Bool_121);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.False;
		if (num)
		{
			Relay_In_697();
		}
		if (flag)
		{
			Relay_In_311();
		}
	}

	private void Relay_True_123()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.True(out logic_uScriptAct_SetBool_Target_123);
		local_CubeDestroyedMsgPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_123;
	}

	private void Relay_False_123()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.False(out logic_uScriptAct_SetBool_Target_123);
		local_CubeDestroyedMsgPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_123;
	}

	private void Relay_In_124()
	{
		logic_uScript_FlyTechUpAndAway_tech_124 = local_CrazedTech_Tank;
		logic_uScript_FlyTechUpAndAway_removalParticles_124 = TechFlyParticles;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_124.In(logic_uScript_FlyTechUpAndAway_tech_124, logic_uScript_FlyTechUpAndAway_maxLifetime_124, logic_uScript_FlyTechUpAndAway_targetHeight_124, logic_uScript_FlyTechUpAndAway_aiTree_124, logic_uScript_FlyTechUpAndAway_removalParticles_124);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_124.Out)
		{
			Relay_In_745();
		}
	}

	private void Relay_True_128()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_128.True(out logic_uScriptAct_SetBool_Target_128);
		local_CubeNeedsReload_System_Boolean = logic_uScriptAct_SetBool_Target_128;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_128.Out)
		{
			Relay_True_161();
		}
	}

	private void Relay_False_128()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_128.False(out logic_uScriptAct_SetBool_Target_128);
		local_CubeNeedsReload_System_Boolean = logic_uScriptAct_SetBool_Target_128;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_128.Out)
		{
			Relay_True_161();
		}
	}

	private void Relay_In_129()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_129 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_129.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_129);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_129.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_129.OutOfRange;
		if (inRange)
		{
			Relay_In_775();
		}
		if (outOfRange)
		{
			Relay_In_808();
		}
	}

	private void Relay_In_132()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_132 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_132.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_132);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_132.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_132.OutOfRange;
		if (inRange)
		{
			Relay_In_86();
		}
		if (outOfRange)
		{
			Relay_True_210();
		}
	}

	private void Relay_True_134()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_134.True(out logic_uScriptAct_SetBool_Target_134);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_134;
	}

	private void Relay_False_134()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_134.False(out logic_uScriptAct_SetBool_Target_134);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_134;
	}

	private void Relay_True_136()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_136.True(out logic_uScriptAct_SetBool_Target_136);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_136;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_136.Out)
		{
			Relay_False_180();
		}
	}

	private void Relay_False_136()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_136.False(out logic_uScriptAct_SetBool_Target_136);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_136;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_136.Out)
		{
			Relay_False_180();
		}
	}

	private void Relay_In_137()
	{
		logic_uScriptCon_CompareBool_Bool_137 = local_FightStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_137.In(logic_uScriptCon_CompareBool_Bool_137);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_137.False)
		{
			Relay_In_181();
		}
	}

	private void Relay_In_140()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_140.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_140, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_140, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_140 = owner_Connection_141;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_140.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_140, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_140, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_140 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_140.In(logic_uScript_GetAndCheckTechs_techData_140, logic_uScript_GetAndCheckTechs_ownerNode_140, ref logic_uScript_GetAndCheckTechs_techs_140);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_140;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_140.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_140.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_98();
		}
		if (someAlive)
		{
			Relay_AtIndex_98();
		}
	}

	private void Relay_In_143()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_143 = CubeFailTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_143.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_143);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_143.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_143.OutOfRange;
		if (inRange)
		{
			Relay_In_137();
		}
		if (outOfRange)
		{
			Relay_In_792();
		}
	}

	private void Relay_True_145()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_145.True(out logic_uScriptAct_SetBool_Target_145);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_145;
	}

	private void Relay_False_145()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_145.False(out logic_uScriptAct_SetBool_Target_145);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_145;
	}

	private void Relay_True_147()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_147.True(out logic_uScriptAct_SetBool_Target_147);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_147;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_147.Out)
		{
			Relay_True_176();
		}
	}

	private void Relay_False_147()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_147.False(out logic_uScriptAct_SetBool_Target_147);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_147;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_147.Out)
		{
			Relay_True_176();
		}
	}

	private void Relay_In_148()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_148.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_148, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_148, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_148 = owner_Connection_149;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_148.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_148, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_148, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_148 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_148.In(logic_uScript_GetAndCheckTechs_techData_148, logic_uScript_GetAndCheckTechs_ownerNode_148, ref logic_uScript_GetAndCheckTechs_techs_148);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_148;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_148.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_148.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_152();
		}
		if (someAlive)
		{
			Relay_AtIndex_152();
		}
	}

	private void Relay_AtIndex_152()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_152.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_152, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_152, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_152.AtIndex(ref logic_uScript_AccessListTech_techList_152, logic_uScript_AccessListTech_index_152, out logic_uScript_AccessListTech_value_152);
		local_CubeTechs_TankArray = logic_uScript_AccessListTech_techList_152;
		local_CubeTech_Tank = logic_uScript_AccessListTech_value_152;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_152.Out)
		{
			Relay_In_159();
		}
	}

	private void Relay_True_153()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_153.True(out logic_uScriptAct_SetBool_Target_153);
		local_PlayerAttemptedMission_System_Boolean = logic_uScriptAct_SetBool_Target_153;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_153.Out)
		{
			Relay_True_174();
		}
	}

	private void Relay_False_153()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_153.False(out logic_uScriptAct_SetBool_Target_153);
		local_PlayerAttemptedMission_System_Boolean = logic_uScriptAct_SetBool_Target_153;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_153.Out)
		{
			Relay_True_174();
		}
	}

	private void Relay_True_154()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_154.True(out logic_uScriptAct_SetBool_Target_154);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_154;
	}

	private void Relay_False_154()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_154.False(out logic_uScriptAct_SetBool_Target_154);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_154;
	}

	private void Relay_In_157()
	{
		logic_uScript_ResetMissionTimerTimeElapsed_owner_157 = owner_Connection_83;
		logic_uScript_ResetMissionTimerTimeElapsed_startTime_157 = local_158_System_Single;
		logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_157.In(logic_uScript_ResetMissionTimerTimeElapsed_owner_157, logic_uScript_ResetMissionTimerTimeElapsed_startTime_157);
		if (logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_157.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_In_159()
	{
		logic_uScript_HideMissionTimerUI_owner_159 = owner_Connection_858;
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_159.In(logic_uScript_HideMissionTimerUI_owner_159);
		if (logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_159.Out)
		{
			Relay_In_565();
		}
	}

	private void Relay_True_161()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_161.True(out logic_uScriptAct_SetBool_Target_161);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_161;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_161.Out)
		{
			Relay_False_753();
		}
	}

	private void Relay_False_161()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_161.False(out logic_uScriptAct_SetBool_Target_161);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_161;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_161.Out)
		{
			Relay_False_753();
		}
	}

	private void Relay_In_163()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_163.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_163, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_163, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_163.In(ref logic_uScript_SetTechsTeam_techs_163, logic_uScript_SetTechsTeam_team_163);
		local_CubeTechs_TankArray = logic_uScript_SetTechsTeam_techs_163;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_163.Out)
		{
			Relay_In_164();
		}
	}

	private void Relay_In_164()
	{
		logic_uScript_SetTankInvulnerable_tank_164 = local_CubeTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_164.In(logic_uScript_SetTankInvulnerable_invulnerable_164, logic_uScript_SetTankInvulnerable_tank_164);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_164.Out)
		{
			Relay_True_187();
		}
	}

	private void Relay_In_166()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_166.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_166, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_166, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_166 = owner_Connection_162;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_166.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_166, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_166, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_166 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_166.In(logic_uScript_GetAndCheckTechs_techData_166, logic_uScript_GetAndCheckTechs_ownerNode_166, ref logic_uScript_GetAndCheckTechs_techs_166);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_166;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_166.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_166.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_166.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_166.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_168();
		}
		if (someAlive)
		{
			Relay_AtIndex_168();
		}
		if (allDead)
		{
			Relay_False_388();
		}
		if (waitingToSpawn)
		{
			Relay_False_388();
		}
	}

	private void Relay_AtIndex_168()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_168.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_168, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_168, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_168.AtIndex(ref logic_uScript_AccessListTech_techList_168, logic_uScript_AccessListTech_index_168, out logic_uScript_AccessListTech_value_168);
		local_CubeTechs_TankArray = logic_uScript_AccessListTech_techList_168;
		local_CubeTech_Tank = logic_uScript_AccessListTech_value_168;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_168.Out)
		{
			Relay_In_163();
		}
	}

	private void Relay_In_172()
	{
		logic_uScriptCon_CompareBool_Bool_172 = local_FightRunning_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_172.In(logic_uScriptCon_CompareBool_Bool_172);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_172.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_172.False;
		if (num)
		{
			Relay_In_87();
		}
		if (flag)
		{
			Relay_False_395();
		}
	}

	private void Relay_True_174()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_174.True(out logic_uScriptAct_SetBool_Target_174);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_174;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_174.Out)
		{
			Relay_True_154();
		}
	}

	private void Relay_False_174()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_174.False(out logic_uScriptAct_SetBool_Target_174);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_174;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_174.Out)
		{
			Relay_True_154();
		}
	}

	private void Relay_True_176()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_176.True(out logic_uScriptAct_SetBool_Target_176);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_176;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_176.Out)
		{
			Relay_False_191();
		}
	}

	private void Relay_False_176()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_176.False(out logic_uScriptAct_SetBool_Target_176);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_176;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_176.Out)
		{
			Relay_False_191();
		}
	}

	private void Relay_True_178()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_178.True(out logic_uScriptAct_SetBool_Target_178);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_178;
	}

	private void Relay_False_178()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_178.False(out logic_uScriptAct_SetBool_Target_178);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_178;
	}

	private void Relay_True_180()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_180.True(out logic_uScriptAct_SetBool_Target_180);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_180;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_180.Out)
		{
			Relay_False_189();
		}
	}

	private void Relay_False_180()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_180.False(out logic_uScriptAct_SetBool_Target_180);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_180;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_180.Out)
		{
			Relay_False_189();
		}
	}

	private void Relay_In_181()
	{
		logic_uScriptCon_CompareBool_Bool_181 = local_FightRunning_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.In(logic_uScriptCon_CompareBool_Bool_181);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.False)
		{
			Relay_In_212();
		}
	}

	private void Relay_AtIndex_183()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_183.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_183, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_183, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_183.AtIndex(ref logic_uScript_AccessListTech_techList_183, logic_uScript_AccessListTech_index_183, out logic_uScript_AccessListTech_value_183);
		local_CubeTechs_TankArray = logic_uScript_AccessListTech_techList_183;
		local_CubeTech_Tank = logic_uScript_AccessListTech_value_183;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_183.Out)
		{
			Relay_True_69();
		}
	}

	private void Relay_In_186()
	{
		logic_uScriptCon_CompareBool_Bool_186 = local_TankInvul_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_186.In(logic_uScriptCon_CompareBool_Bool_186);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_186.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_186.False;
		if (num)
		{
			Relay_In_172();
		}
		if (flag)
		{
			Relay_In_166();
		}
	}

	private void Relay_True_187()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_187.True(out logic_uScriptAct_SetBool_Target_187);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_187;
	}

	private void Relay_False_187()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_187.False(out logic_uScriptAct_SetBool_Target_187);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_187;
	}

	private void Relay_True_189()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_189.True(out logic_uScriptAct_SetBool_Target_189);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_189;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_189.Out)
		{
			Relay_In_865();
		}
	}

	private void Relay_False_189()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_189.False(out logic_uScriptAct_SetBool_Target_189);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_189;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_189.Out)
		{
			Relay_In_865();
		}
	}

	private void Relay_True_191()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_191.True(out logic_uScriptAct_SetBool_Target_191);
		local_LeftAreaAfterLoss_System_Boolean = logic_uScriptAct_SetBool_Target_191;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_191.Out)
		{
			Relay_False_201();
		}
	}

	private void Relay_False_191()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_191.False(out logic_uScriptAct_SetBool_Target_191);
		local_LeftAreaAfterLoss_System_Boolean = logic_uScriptAct_SetBool_Target_191;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_191.Out)
		{
			Relay_False_201();
		}
	}

	private void Relay_True_193()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_193.True(out logic_uScriptAct_SetBool_Target_193);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_193;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_193.Out)
		{
			Relay_In_862();
		}
	}

	private void Relay_False_193()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_193.False(out logic_uScriptAct_SetBool_Target_193);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_193;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_193.Out)
		{
			Relay_In_862();
		}
	}

	private void Relay_In_195()
	{
		logic_uScriptCon_CompareBool_Bool_195 = local_CrazedAmbushTriggered_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_195.In(logic_uScriptCon_CompareBool_Bool_195);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_195.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_195.False;
		if (num)
		{
			Relay_In_549();
		}
		if (flag)
		{
			Relay_In_834();
		}
	}

	private void Relay_In_197()
	{
		logic_uScript_AddMessage_messageData_197 = MsgCrazedNPCFly;
		logic_uScript_AddMessage_speaker_197 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_197 = logic_uScript_AddMessage_uScript_AddMessage_197.In(logic_uScript_AddMessage_messageData_197, logic_uScript_AddMessage_speaker_197);
		if (logic_uScript_AddMessage_uScript_AddMessage_197.Shown)
		{
			Relay_In_124();
		}
	}

	private void Relay_True_201()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_201.True(out logic_uScriptAct_SetBool_Target_201);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_201;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_201.Out)
		{
			Relay_False_552();
		}
	}

	private void Relay_False_201()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_201.False(out logic_uScriptAct_SetBool_Target_201);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_201;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_201.Out)
		{
			Relay_False_552();
		}
	}

	private void Relay_In_202()
	{
		logic_uScript_AddMessage_messageData_202 = MsgCrazedIntro01;
		logic_uScript_AddMessage_speaker_202 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_202 = logic_uScript_AddMessage_uScript_AddMessage_202.In(logic_uScript_AddMessage_messageData_202, logic_uScript_AddMessage_speaker_202);
		if (logic_uScript_AddMessage_uScript_AddMessage_202.Shown)
		{
			Relay_In_286();
		}
	}

	private void Relay_In_207()
	{
		logic_uScript_AddMessage_messageData_207 = MsgStartBossFight;
		logic_uScript_AddMessage_speaker_207 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_207 = logic_uScript_AddMessage_uScript_AddMessage_207.In(logic_uScript_AddMessage_messageData_207, logic_uScript_AddMessage_speaker_207);
	}

	private void Relay_True_208()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_208.True(out logic_uScriptAct_SetBool_Target_208);
		local_OutOfTime_System_Boolean = logic_uScriptAct_SetBool_Target_208;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_208.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_False_208()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_208.False(out logic_uScriptAct_SetBool_Target_208);
		local_OutOfTime_System_Boolean = logic_uScriptAct_SetBool_Target_208;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_208.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_True_210()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_210.True(out logic_uScriptAct_SetBool_Target_210);
		local_WentOutOfRange_System_Boolean = logic_uScriptAct_SetBool_Target_210;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_210.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_False_210()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_210.False(out logic_uScriptAct_SetBool_Target_210);
		local_WentOutOfRange_System_Boolean = logic_uScriptAct_SetBool_Target_210;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_210.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_In_212()
	{
		logic_uScriptCon_CompareBool_Bool_212 = local_WentOutOfRange_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.In(logic_uScriptCon_CompareBool_Bool_212);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.False;
		if (num)
		{
			Relay_In_49();
		}
		if (flag)
		{
			Relay_In_215();
		}
	}

	private void Relay_In_215()
	{
		logic_uScriptCon_CompareBool_Bool_215 = local_OutOfTime_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_215.In(logic_uScriptCon_CompareBool_Bool_215);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_215.True)
		{
			Relay_In_801();
		}
	}

	private void Relay_True_216()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_216.True(out logic_uScriptAct_SetBool_Target_216);
		local_WentOutOfRange_System_Boolean = logic_uScriptAct_SetBool_Target_216;
	}

	private void Relay_False_216()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_216.False(out logic_uScriptAct_SetBool_Target_216);
		local_WentOutOfRange_System_Boolean = logic_uScriptAct_SetBool_Target_216;
	}

	private void Relay_Save_Out_219()
	{
		Relay_Save_221();
	}

	private void Relay_Load_Out_219()
	{
		Relay_Load_221();
	}

	private void Relay_Restart_Out_219()
	{
		Relay_Set_False_221();
	}

	private void Relay_Save_219()
	{
		logic_SubGraph_SaveLoadBool_boolean_219 = local_NPCTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_219 = local_NPCTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.Save(ref logic_SubGraph_SaveLoadBool_boolean_219, logic_SubGraph_SaveLoadBool_boolAsVariable_219, logic_SubGraph_SaveLoadBool_uniqueID_219);
	}

	private void Relay_Load_219()
	{
		logic_SubGraph_SaveLoadBool_boolean_219 = local_NPCTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_219 = local_NPCTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.Load(ref logic_SubGraph_SaveLoadBool_boolean_219, logic_SubGraph_SaveLoadBool_boolAsVariable_219, logic_SubGraph_SaveLoadBool_uniqueID_219);
	}

	private void Relay_Set_True_219()
	{
		logic_SubGraph_SaveLoadBool_boolean_219 = local_NPCTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_219 = local_NPCTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_219, logic_SubGraph_SaveLoadBool_boolAsVariable_219, logic_SubGraph_SaveLoadBool_uniqueID_219);
	}

	private void Relay_Set_False_219()
	{
		logic_SubGraph_SaveLoadBool_boolean_219 = local_NPCTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_219 = local_NPCTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_219.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_219, logic_SubGraph_SaveLoadBool_boolAsVariable_219, logic_SubGraph_SaveLoadBool_uniqueID_219);
	}

	private void Relay_Save_Out_221()
	{
		Relay_Save_223();
	}

	private void Relay_Load_Out_221()
	{
		Relay_Load_223();
	}

	private void Relay_Restart_Out_221()
	{
		Relay_Set_False_223();
	}

	private void Relay_Save_221()
	{
		logic_SubGraph_SaveLoadBool_boolean_221 = local_NPCTechSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_221 = local_NPCTechSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.Save(ref logic_SubGraph_SaveLoadBool_boolean_221, logic_SubGraph_SaveLoadBool_boolAsVariable_221, logic_SubGraph_SaveLoadBool_uniqueID_221);
	}

	private void Relay_Load_221()
	{
		logic_SubGraph_SaveLoadBool_boolean_221 = local_NPCTechSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_221 = local_NPCTechSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.Load(ref logic_SubGraph_SaveLoadBool_boolean_221, logic_SubGraph_SaveLoadBool_boolAsVariable_221, logic_SubGraph_SaveLoadBool_uniqueID_221);
	}

	private void Relay_Set_True_221()
	{
		logic_SubGraph_SaveLoadBool_boolean_221 = local_NPCTechSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_221 = local_NPCTechSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_221, logic_SubGraph_SaveLoadBool_boolAsVariable_221, logic_SubGraph_SaveLoadBool_uniqueID_221);
	}

	private void Relay_Set_False_221()
	{
		logic_SubGraph_SaveLoadBool_boolean_221 = local_NPCTechSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_221 = local_NPCTechSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_221.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_221, logic_SubGraph_SaveLoadBool_boolAsVariable_221, logic_SubGraph_SaveLoadBool_uniqueID_221);
	}

	private void Relay_Save_Out_223()
	{
		Relay_Save_225();
	}

	private void Relay_Load_Out_223()
	{
		Relay_Load_225();
	}

	private void Relay_Restart_Out_223()
	{
		Relay_Set_False_225();
	}

	private void Relay_Save_223()
	{
		logic_SubGraph_SaveLoadBool_boolean_223 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_223 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.Save(ref logic_SubGraph_SaveLoadBool_boolean_223, logic_SubGraph_SaveLoadBool_boolAsVariable_223, logic_SubGraph_SaveLoadBool_uniqueID_223);
	}

	private void Relay_Load_223()
	{
		logic_SubGraph_SaveLoadBool_boolean_223 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_223 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.Load(ref logic_SubGraph_SaveLoadBool_boolean_223, logic_SubGraph_SaveLoadBool_boolAsVariable_223, logic_SubGraph_SaveLoadBool_uniqueID_223);
	}

	private void Relay_Set_True_223()
	{
		logic_SubGraph_SaveLoadBool_boolean_223 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_223 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_223, logic_SubGraph_SaveLoadBool_boolAsVariable_223, logic_SubGraph_SaveLoadBool_uniqueID_223);
	}

	private void Relay_Set_False_223()
	{
		logic_SubGraph_SaveLoadBool_boolean_223 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_223 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_223.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_223, logic_SubGraph_SaveLoadBool_boolAsVariable_223, logic_SubGraph_SaveLoadBool_uniqueID_223);
	}

	private void Relay_Save_Out_225()
	{
		Relay_Save_227();
	}

	private void Relay_Load_Out_225()
	{
		Relay_Load_227();
	}

	private void Relay_Restart_Out_225()
	{
		Relay_Set_False_227();
	}

	private void Relay_Save_225()
	{
		logic_SubGraph_SaveLoadBool_boolean_225 = local_FirstCubeSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_225 = local_FirstCubeSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.Save(ref logic_SubGraph_SaveLoadBool_boolean_225, logic_SubGraph_SaveLoadBool_boolAsVariable_225, logic_SubGraph_SaveLoadBool_uniqueID_225);
	}

	private void Relay_Load_225()
	{
		logic_SubGraph_SaveLoadBool_boolean_225 = local_FirstCubeSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_225 = local_FirstCubeSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.Load(ref logic_SubGraph_SaveLoadBool_boolean_225, logic_SubGraph_SaveLoadBool_boolAsVariable_225, logic_SubGraph_SaveLoadBool_uniqueID_225);
	}

	private void Relay_Set_True_225()
	{
		logic_SubGraph_SaveLoadBool_boolean_225 = local_FirstCubeSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_225 = local_FirstCubeSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_225, logic_SubGraph_SaveLoadBool_boolAsVariable_225, logic_SubGraph_SaveLoadBool_uniqueID_225);
	}

	private void Relay_Set_False_225()
	{
		logic_SubGraph_SaveLoadBool_boolean_225 = local_FirstCubeSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_225 = local_FirstCubeSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_225.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_225, logic_SubGraph_SaveLoadBool_boolAsVariable_225, logic_SubGraph_SaveLoadBool_uniqueID_225);
	}

	private void Relay_Save_Out_227()
	{
		Relay_Save_246();
	}

	private void Relay_Load_Out_227()
	{
		Relay_Load_246();
	}

	private void Relay_Restart_Out_227()
	{
		Relay_Set_False_246();
	}

	private void Relay_Save_227()
	{
		logic_SubGraph_SaveLoadBool_boolean_227 = local_CubeisOK_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_227 = local_CubeisOK_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Save(ref logic_SubGraph_SaveLoadBool_boolean_227, logic_SubGraph_SaveLoadBool_boolAsVariable_227, logic_SubGraph_SaveLoadBool_uniqueID_227);
	}

	private void Relay_Load_227()
	{
		logic_SubGraph_SaveLoadBool_boolean_227 = local_CubeisOK_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_227 = local_CubeisOK_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Load(ref logic_SubGraph_SaveLoadBool_boolean_227, logic_SubGraph_SaveLoadBool_boolAsVariable_227, logic_SubGraph_SaveLoadBool_uniqueID_227);
	}

	private void Relay_Set_True_227()
	{
		logic_SubGraph_SaveLoadBool_boolean_227 = local_CubeisOK_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_227 = local_CubeisOK_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_227, logic_SubGraph_SaveLoadBool_boolAsVariable_227, logic_SubGraph_SaveLoadBool_uniqueID_227);
	}

	private void Relay_Set_False_227()
	{
		logic_SubGraph_SaveLoadBool_boolean_227 = local_CubeisOK_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_227 = local_CubeisOK_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_227.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_227, logic_SubGraph_SaveLoadBool_boolAsVariable_227, logic_SubGraph_SaveLoadBool_uniqueID_227);
	}

	private void Relay_Save_Out_244()
	{
		Relay_Save_245();
	}

	private void Relay_Load_Out_244()
	{
		Relay_Load_245();
	}

	private void Relay_Restart_Out_244()
	{
		Relay_Set_False_245();
	}

	private void Relay_Save_244()
	{
		logic_SubGraph_SaveLoadBool_boolean_244 = local_TankInvul_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_244 = local_TankInvul_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Save(ref logic_SubGraph_SaveLoadBool_boolean_244, logic_SubGraph_SaveLoadBool_boolAsVariable_244, logic_SubGraph_SaveLoadBool_uniqueID_244);
	}

	private void Relay_Load_244()
	{
		logic_SubGraph_SaveLoadBool_boolean_244 = local_TankInvul_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_244 = local_TankInvul_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Load(ref logic_SubGraph_SaveLoadBool_boolean_244, logic_SubGraph_SaveLoadBool_boolAsVariable_244, logic_SubGraph_SaveLoadBool_uniqueID_244);
	}

	private void Relay_Set_True_244()
	{
		logic_SubGraph_SaveLoadBool_boolean_244 = local_TankInvul_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_244 = local_TankInvul_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_244, logic_SubGraph_SaveLoadBool_boolAsVariable_244, logic_SubGraph_SaveLoadBool_uniqueID_244);
	}

	private void Relay_Set_False_244()
	{
		logic_SubGraph_SaveLoadBool_boolean_244 = local_TankInvul_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_244 = local_TankInvul_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_244.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_244, logic_SubGraph_SaveLoadBool_boolAsVariable_244, logic_SubGraph_SaveLoadBool_uniqueID_244);
	}

	private void Relay_Save_Out_245()
	{
		Relay_Save_247();
	}

	private void Relay_Load_Out_245()
	{
		Relay_Load_247();
	}

	private void Relay_Restart_Out_245()
	{
		Relay_Set_False_247();
	}

	private void Relay_Save_245()
	{
		logic_SubGraph_SaveLoadBool_boolean_245 = local_PlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_245 = local_PlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.Save(ref logic_SubGraph_SaveLoadBool_boolean_245, logic_SubGraph_SaveLoadBool_boolAsVariable_245, logic_SubGraph_SaveLoadBool_uniqueID_245);
	}

	private void Relay_Load_245()
	{
		logic_SubGraph_SaveLoadBool_boolean_245 = local_PlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_245 = local_PlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.Load(ref logic_SubGraph_SaveLoadBool_boolean_245, logic_SubGraph_SaveLoadBool_boolAsVariable_245, logic_SubGraph_SaveLoadBool_uniqueID_245);
	}

	private void Relay_Set_True_245()
	{
		logic_SubGraph_SaveLoadBool_boolean_245 = local_PlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_245 = local_PlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_245, logic_SubGraph_SaveLoadBool_boolAsVariable_245, logic_SubGraph_SaveLoadBool_uniqueID_245);
	}

	private void Relay_Set_False_245()
	{
		logic_SubGraph_SaveLoadBool_boolean_245 = local_PlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_245 = local_PlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_245.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_245, logic_SubGraph_SaveLoadBool_boolAsVariable_245, logic_SubGraph_SaveLoadBool_uniqueID_245);
	}

	private void Relay_Save_Out_246()
	{
		Relay_Save_244();
	}

	private void Relay_Load_Out_246()
	{
		Relay_Load_244();
	}

	private void Relay_Restart_Out_246()
	{
		Relay_Set_False_244();
	}

	private void Relay_Save_246()
	{
		logic_SubGraph_SaveLoadBool_boolean_246 = local_CubeNeedsReload_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_246 = local_CubeNeedsReload_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.Save(ref logic_SubGraph_SaveLoadBool_boolean_246, logic_SubGraph_SaveLoadBool_boolAsVariable_246, logic_SubGraph_SaveLoadBool_uniqueID_246);
	}

	private void Relay_Load_246()
	{
		logic_SubGraph_SaveLoadBool_boolean_246 = local_CubeNeedsReload_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_246 = local_CubeNeedsReload_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.Load(ref logic_SubGraph_SaveLoadBool_boolean_246, logic_SubGraph_SaveLoadBool_boolAsVariable_246, logic_SubGraph_SaveLoadBool_uniqueID_246);
	}

	private void Relay_Set_True_246()
	{
		logic_SubGraph_SaveLoadBool_boolean_246 = local_CubeNeedsReload_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_246 = local_CubeNeedsReload_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_246, logic_SubGraph_SaveLoadBool_boolAsVariable_246, logic_SubGraph_SaveLoadBool_uniqueID_246);
	}

	private void Relay_Set_False_246()
	{
		logic_SubGraph_SaveLoadBool_boolean_246 = local_CubeNeedsReload_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_246 = local_CubeNeedsReload_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_246.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_246, logic_SubGraph_SaveLoadBool_boolAsVariable_246, logic_SubGraph_SaveLoadBool_uniqueID_246);
	}

	private void Relay_Save_Out_247()
	{
		Relay_Save_248();
	}

	private void Relay_Load_Out_247()
	{
		Relay_Load_248();
	}

	private void Relay_Restart_Out_247()
	{
		Relay_Set_False_248();
	}

	private void Relay_Save_247()
	{
		logic_SubGraph_SaveLoadBool_boolean_247 = local_CrazedIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_247 = local_CrazedIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Save(ref logic_SubGraph_SaveLoadBool_boolean_247, logic_SubGraph_SaveLoadBool_boolAsVariable_247, logic_SubGraph_SaveLoadBool_uniqueID_247);
	}

	private void Relay_Load_247()
	{
		logic_SubGraph_SaveLoadBool_boolean_247 = local_CrazedIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_247 = local_CrazedIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Load(ref logic_SubGraph_SaveLoadBool_boolean_247, logic_SubGraph_SaveLoadBool_boolAsVariable_247, logic_SubGraph_SaveLoadBool_uniqueID_247);
	}

	private void Relay_Set_True_247()
	{
		logic_SubGraph_SaveLoadBool_boolean_247 = local_CrazedIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_247 = local_CrazedIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_247, logic_SubGraph_SaveLoadBool_boolAsVariable_247, logic_SubGraph_SaveLoadBool_uniqueID_247);
	}

	private void Relay_Set_False_247()
	{
		logic_SubGraph_SaveLoadBool_boolean_247 = local_CrazedIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_247 = local_CrazedIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_247.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_247, logic_SubGraph_SaveLoadBool_boolAsVariable_247, logic_SubGraph_SaveLoadBool_uniqueID_247);
	}

	private void Relay_Save_Out_248()
	{
		Relay_Save_251();
	}

	private void Relay_Load_Out_248()
	{
		Relay_Load_251();
	}

	private void Relay_Restart_Out_248()
	{
		Relay_Set_False_251();
	}

	private void Relay_Save_248()
	{
		logic_SubGraph_SaveLoadBool_boolean_248 = local_CrazedNPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_248 = local_CrazedNPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.Save(ref logic_SubGraph_SaveLoadBool_boolean_248, logic_SubGraph_SaveLoadBool_boolAsVariable_248, logic_SubGraph_SaveLoadBool_uniqueID_248);
	}

	private void Relay_Load_248()
	{
		logic_SubGraph_SaveLoadBool_boolean_248 = local_CrazedNPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_248 = local_CrazedNPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.Load(ref logic_SubGraph_SaveLoadBool_boolean_248, logic_SubGraph_SaveLoadBool_boolAsVariable_248, logic_SubGraph_SaveLoadBool_uniqueID_248);
	}

	private void Relay_Set_True_248()
	{
		logic_SubGraph_SaveLoadBool_boolean_248 = local_CrazedNPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_248 = local_CrazedNPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_248, logic_SubGraph_SaveLoadBool_boolAsVariable_248, logic_SubGraph_SaveLoadBool_uniqueID_248);
	}

	private void Relay_Set_False_248()
	{
		logic_SubGraph_SaveLoadBool_boolean_248 = local_CrazedNPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_248 = local_CrazedNPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_248.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_248, logic_SubGraph_SaveLoadBool_boolAsVariable_248, logic_SubGraph_SaveLoadBool_uniqueID_248);
	}

	private void Relay_Save_Out_249()
	{
		Relay_Save_253();
	}

	private void Relay_Load_Out_249()
	{
		Relay_Load_253();
	}

	private void Relay_Restart_Out_249()
	{
		Relay_Set_False_253();
	}

	private void Relay_Save_249()
	{
		logic_SubGraph_SaveLoadBool_boolean_249 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_249 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Save(ref logic_SubGraph_SaveLoadBool_boolean_249, logic_SubGraph_SaveLoadBool_boolAsVariable_249, logic_SubGraph_SaveLoadBool_uniqueID_249);
	}

	private void Relay_Load_249()
	{
		logic_SubGraph_SaveLoadBool_boolean_249 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_249 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Load(ref logic_SubGraph_SaveLoadBool_boolean_249, logic_SubGraph_SaveLoadBool_boolAsVariable_249, logic_SubGraph_SaveLoadBool_uniqueID_249);
	}

	private void Relay_Set_True_249()
	{
		logic_SubGraph_SaveLoadBool_boolean_249 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_249 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_249, logic_SubGraph_SaveLoadBool_boolAsVariable_249, logic_SubGraph_SaveLoadBool_uniqueID_249);
	}

	private void Relay_Set_False_249()
	{
		logic_SubGraph_SaveLoadBool_boolean_249 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_249 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_249, logic_SubGraph_SaveLoadBool_boolAsVariable_249, logic_SubGraph_SaveLoadBool_uniqueID_249);
	}

	private void Relay_Save_Out_250()
	{
		Relay_Save_249();
	}

	private void Relay_Load_Out_250()
	{
		Relay_Load_249();
	}

	private void Relay_Restart_Out_250()
	{
		Relay_Set_False_249();
	}

	private void Relay_Save_250()
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = local_HasBeenInterrupted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_250 = local_HasBeenInterrupted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Save(ref logic_SubGraph_SaveLoadBool_boolean_250, logic_SubGraph_SaveLoadBool_boolAsVariable_250, logic_SubGraph_SaveLoadBool_uniqueID_250);
	}

	private void Relay_Load_250()
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = local_HasBeenInterrupted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_250 = local_HasBeenInterrupted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Load(ref logic_SubGraph_SaveLoadBool_boolean_250, logic_SubGraph_SaveLoadBool_boolAsVariable_250, logic_SubGraph_SaveLoadBool_uniqueID_250);
	}

	private void Relay_Set_True_250()
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = local_HasBeenInterrupted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_250 = local_HasBeenInterrupted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_250, logic_SubGraph_SaveLoadBool_boolAsVariable_250, logic_SubGraph_SaveLoadBool_uniqueID_250);
	}

	private void Relay_Set_False_250()
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = local_HasBeenInterrupted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_250 = local_HasBeenInterrupted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_250, logic_SubGraph_SaveLoadBool_boolAsVariable_250, logic_SubGraph_SaveLoadBool_uniqueID_250);
	}

	private void Relay_Save_Out_251()
	{
		Relay_Save_252();
	}

	private void Relay_Load_Out_251()
	{
		Relay_Load_252();
	}

	private void Relay_Restart_Out_251()
	{
		Relay_Set_False_252();
	}

	private void Relay_Save_251()
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = local_CrazedNPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_251 = local_CrazedNPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Save(ref logic_SubGraph_SaveLoadBool_boolean_251, logic_SubGraph_SaveLoadBool_boolAsVariable_251, logic_SubGraph_SaveLoadBool_uniqueID_251);
	}

	private void Relay_Load_251()
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = local_CrazedNPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_251 = local_CrazedNPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Load(ref logic_SubGraph_SaveLoadBool_boolean_251, logic_SubGraph_SaveLoadBool_boolAsVariable_251, logic_SubGraph_SaveLoadBool_uniqueID_251);
	}

	private void Relay_Set_True_251()
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = local_CrazedNPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_251 = local_CrazedNPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_251, logic_SubGraph_SaveLoadBool_boolAsVariable_251, logic_SubGraph_SaveLoadBool_uniqueID_251);
	}

	private void Relay_Set_False_251()
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = local_CrazedNPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_251 = local_CrazedNPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_251, logic_SubGraph_SaveLoadBool_boolAsVariable_251, logic_SubGraph_SaveLoadBool_uniqueID_251);
	}

	private void Relay_Save_Out_252()
	{
		Relay_Save_250();
	}

	private void Relay_Load_Out_252()
	{
		Relay_Load_250();
	}

	private void Relay_Restart_Out_252()
	{
		Relay_Set_False_250();
	}

	private void Relay_Save_252()
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_252 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Save(ref logic_SubGraph_SaveLoadBool_boolean_252, logic_SubGraph_SaveLoadBool_boolAsVariable_252, logic_SubGraph_SaveLoadBool_uniqueID_252);
	}

	private void Relay_Load_252()
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_252 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Load(ref logic_SubGraph_SaveLoadBool_boolean_252, logic_SubGraph_SaveLoadBool_boolAsVariable_252, logic_SubGraph_SaveLoadBool_uniqueID_252);
	}

	private void Relay_Set_True_252()
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_252 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_252, logic_SubGraph_SaveLoadBool_boolAsVariable_252, logic_SubGraph_SaveLoadBool_uniqueID_252);
	}

	private void Relay_Set_False_252()
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_252 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_252, logic_SubGraph_SaveLoadBool_boolAsVariable_252, logic_SubGraph_SaveLoadBool_uniqueID_252);
	}

	private void Relay_Save_Out_253()
	{
		Relay_Save_255();
	}

	private void Relay_Load_Out_253()
	{
		Relay_Load_255();
	}

	private void Relay_Restart_Out_253()
	{
		Relay_Set_False_255();
	}

	private void Relay_Save_253()
	{
		logic_SubGraph_SaveLoadBool_boolean_253 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_253 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Save(ref logic_SubGraph_SaveLoadBool_boolean_253, logic_SubGraph_SaveLoadBool_boolAsVariable_253, logic_SubGraph_SaveLoadBool_uniqueID_253);
	}

	private void Relay_Load_253()
	{
		logic_SubGraph_SaveLoadBool_boolean_253 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_253 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Load(ref logic_SubGraph_SaveLoadBool_boolean_253, logic_SubGraph_SaveLoadBool_boolAsVariable_253, logic_SubGraph_SaveLoadBool_uniqueID_253);
	}

	private void Relay_Set_True_253()
	{
		logic_SubGraph_SaveLoadBool_boolean_253 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_253 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_253, logic_SubGraph_SaveLoadBool_boolAsVariable_253, logic_SubGraph_SaveLoadBool_uniqueID_253);
	}

	private void Relay_Set_False_253()
	{
		logic_SubGraph_SaveLoadBool_boolean_253 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_253 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_253.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_253, logic_SubGraph_SaveLoadBool_boolAsVariable_253, logic_SubGraph_SaveLoadBool_uniqueID_253);
	}

	private void Relay_Save_Out_254()
	{
		Relay_Save_257();
	}

	private void Relay_Load_Out_254()
	{
		Relay_Load_257();
	}

	private void Relay_Restart_Out_254()
	{
		Relay_Set_False_257();
	}

	private void Relay_Save_254()
	{
		logic_SubGraph_SaveLoadBool_boolean_254 = local_LeftAreaAfterLoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_254 = local_LeftAreaAfterLoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.Save(ref logic_SubGraph_SaveLoadBool_boolean_254, logic_SubGraph_SaveLoadBool_boolAsVariable_254, logic_SubGraph_SaveLoadBool_uniqueID_254);
	}

	private void Relay_Load_254()
	{
		logic_SubGraph_SaveLoadBool_boolean_254 = local_LeftAreaAfterLoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_254 = local_LeftAreaAfterLoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.Load(ref logic_SubGraph_SaveLoadBool_boolean_254, logic_SubGraph_SaveLoadBool_boolAsVariable_254, logic_SubGraph_SaveLoadBool_uniqueID_254);
	}

	private void Relay_Set_True_254()
	{
		logic_SubGraph_SaveLoadBool_boolean_254 = local_LeftAreaAfterLoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_254 = local_LeftAreaAfterLoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_254, logic_SubGraph_SaveLoadBool_boolAsVariable_254, logic_SubGraph_SaveLoadBool_uniqueID_254);
	}

	private void Relay_Set_False_254()
	{
		logic_SubGraph_SaveLoadBool_boolean_254 = local_LeftAreaAfterLoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_254 = local_LeftAreaAfterLoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_254.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_254, logic_SubGraph_SaveLoadBool_boolAsVariable_254, logic_SubGraph_SaveLoadBool_uniqueID_254);
	}

	private void Relay_Save_Out_255()
	{
		Relay_Save_256();
	}

	private void Relay_Load_Out_255()
	{
		Relay_Load_256();
	}

	private void Relay_Restart_Out_255()
	{
		Relay_Set_False_256();
	}

	private void Relay_Save_255()
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_255 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Save(ref logic_SubGraph_SaveLoadBool_boolean_255, logic_SubGraph_SaveLoadBool_boolAsVariable_255, logic_SubGraph_SaveLoadBool_uniqueID_255);
	}

	private void Relay_Load_255()
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_255 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Load(ref logic_SubGraph_SaveLoadBool_boolean_255, logic_SubGraph_SaveLoadBool_boolAsVariable_255, logic_SubGraph_SaveLoadBool_uniqueID_255);
	}

	private void Relay_Set_True_255()
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_255 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_255, logic_SubGraph_SaveLoadBool_boolAsVariable_255, logic_SubGraph_SaveLoadBool_uniqueID_255);
	}

	private void Relay_Set_False_255()
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_255 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_255, logic_SubGraph_SaveLoadBool_boolAsVariable_255, logic_SubGraph_SaveLoadBool_uniqueID_255);
	}

	private void Relay_Save_Out_256()
	{
		Relay_Save_254();
	}

	private void Relay_Load_Out_256()
	{
		Relay_Load_254();
	}

	private void Relay_Restart_Out_256()
	{
		Relay_Set_False_254();
	}

	private void Relay_Save_256()
	{
		logic_SubGraph_SaveLoadBool_boolean_256 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_256 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Save(ref logic_SubGraph_SaveLoadBool_boolean_256, logic_SubGraph_SaveLoadBool_boolAsVariable_256, logic_SubGraph_SaveLoadBool_uniqueID_256);
	}

	private void Relay_Load_256()
	{
		logic_SubGraph_SaveLoadBool_boolean_256 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_256 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Load(ref logic_SubGraph_SaveLoadBool_boolean_256, logic_SubGraph_SaveLoadBool_boolAsVariable_256, logic_SubGraph_SaveLoadBool_uniqueID_256);
	}

	private void Relay_Set_True_256()
	{
		logic_SubGraph_SaveLoadBool_boolean_256 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_256 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_256, logic_SubGraph_SaveLoadBool_boolAsVariable_256, logic_SubGraph_SaveLoadBool_uniqueID_256);
	}

	private void Relay_Set_False_256()
	{
		logic_SubGraph_SaveLoadBool_boolean_256 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_256 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_256, logic_SubGraph_SaveLoadBool_boolAsVariable_256, logic_SubGraph_SaveLoadBool_uniqueID_256);
	}

	private void Relay_Save_Out_257()
	{
		Relay_Save_259();
	}

	private void Relay_Load_Out_257()
	{
		Relay_Load_259();
	}

	private void Relay_Restart_Out_257()
	{
		Relay_Set_False_259();
	}

	private void Relay_Save_257()
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = local_WentOutOfRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_257 = local_WentOutOfRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Save(ref logic_SubGraph_SaveLoadBool_boolean_257, logic_SubGraph_SaveLoadBool_boolAsVariable_257, logic_SubGraph_SaveLoadBool_uniqueID_257);
	}

	private void Relay_Load_257()
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = local_WentOutOfRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_257 = local_WentOutOfRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Load(ref logic_SubGraph_SaveLoadBool_boolean_257, logic_SubGraph_SaveLoadBool_boolAsVariable_257, logic_SubGraph_SaveLoadBool_uniqueID_257);
	}

	private void Relay_Set_True_257()
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = local_WentOutOfRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_257 = local_WentOutOfRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_257, logic_SubGraph_SaveLoadBool_boolAsVariable_257, logic_SubGraph_SaveLoadBool_uniqueID_257);
	}

	private void Relay_Set_False_257()
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = local_WentOutOfRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_257 = local_WentOutOfRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_257, logic_SubGraph_SaveLoadBool_boolAsVariable_257, logic_SubGraph_SaveLoadBool_uniqueID_257);
	}

	private void Relay_Save_Out_258()
	{
		Relay_Save_696();
	}

	private void Relay_Load_Out_258()
	{
		Relay_Load_696();
	}

	private void Relay_Restart_Out_258()
	{
		Relay_Set_False_696();
	}

	private void Relay_Save_258()
	{
		logic_SubGraph_SaveLoadBool_boolean_258 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_258 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.Save(ref logic_SubGraph_SaveLoadBool_boolean_258, logic_SubGraph_SaveLoadBool_boolAsVariable_258, logic_SubGraph_SaveLoadBool_uniqueID_258);
	}

	private void Relay_Load_258()
	{
		logic_SubGraph_SaveLoadBool_boolean_258 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_258 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.Load(ref logic_SubGraph_SaveLoadBool_boolean_258, logic_SubGraph_SaveLoadBool_boolAsVariable_258, logic_SubGraph_SaveLoadBool_uniqueID_258);
	}

	private void Relay_Set_True_258()
	{
		logic_SubGraph_SaveLoadBool_boolean_258 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_258 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_258, logic_SubGraph_SaveLoadBool_boolAsVariable_258, logic_SubGraph_SaveLoadBool_uniqueID_258);
	}

	private void Relay_Set_False_258()
	{
		logic_SubGraph_SaveLoadBool_boolean_258 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_258 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_258.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_258, logic_SubGraph_SaveLoadBool_boolAsVariable_258, logic_SubGraph_SaveLoadBool_uniqueID_258);
	}

	private void Relay_Save_Out_259()
	{
		Relay_Save_258();
	}

	private void Relay_Load_Out_259()
	{
		Relay_Load_258();
	}

	private void Relay_Restart_Out_259()
	{
		Relay_Set_False_258();
	}

	private void Relay_Save_259()
	{
		logic_SubGraph_SaveLoadBool_boolean_259 = local_OutOfTime_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_259 = local_OutOfTime_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Save(ref logic_SubGraph_SaveLoadBool_boolean_259, logic_SubGraph_SaveLoadBool_boolAsVariable_259, logic_SubGraph_SaveLoadBool_uniqueID_259);
	}

	private void Relay_Load_259()
	{
		logic_SubGraph_SaveLoadBool_boolean_259 = local_OutOfTime_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_259 = local_OutOfTime_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Load(ref logic_SubGraph_SaveLoadBool_boolean_259, logic_SubGraph_SaveLoadBool_boolAsVariable_259, logic_SubGraph_SaveLoadBool_uniqueID_259);
	}

	private void Relay_Set_True_259()
	{
		logic_SubGraph_SaveLoadBool_boolean_259 = local_OutOfTime_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_259 = local_OutOfTime_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_259, logic_SubGraph_SaveLoadBool_boolAsVariable_259, logic_SubGraph_SaveLoadBool_uniqueID_259);
	}

	private void Relay_Set_False_259()
	{
		logic_SubGraph_SaveLoadBool_boolean_259 = local_OutOfTime_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_259 = local_OutOfTime_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_259, logic_SubGraph_SaveLoadBool_boolAsVariable_259, logic_SubGraph_SaveLoadBool_uniqueID_259);
	}

	private void Relay_Out_260()
	{
		Relay_False_89();
	}

	private void Relay_In_260()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_260 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_260.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_260, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_260);
	}

	private void Relay_AtIndex_262()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_262.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_262, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_262, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_262.AtIndex(ref logic_uScript_AccessListTech_techList_262, logic_uScript_AccessListTech_index_262, out logic_uScript_AccessListTech_value_262);
		local_CrazedTechs_TankArray = logic_uScript_AccessListTech_techList_262;
		local_CrazedTech2_Tank = logic_uScript_AccessListTech_value_262;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_262.Out)
		{
			Relay_AtIndex_265();
		}
	}

	private void Relay_AtIndex_265()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_265.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_265, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_265, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_265.AtIndex(ref logic_uScript_AccessListTech_techList_265, logic_uScript_AccessListTech_index_265, out logic_uScript_AccessListTech_value_265);
		local_CrazedTechs_TankArray = logic_uScript_AccessListTech_techList_265;
		local_CrazedTech3_Tank = logic_uScript_AccessListTech_value_265;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_265.Out)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_267()
	{
		logic_uScript_AddMessage_messageData_267 = MsgCrazedIntro04;
		logic_uScript_AddMessage_speaker_267 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_267 = logic_uScript_AddMessage_uScript_AddMessage_267.In(logic_uScript_AddMessage_messageData_267, logic_uScript_AddMessage_speaker_267);
		if (logic_uScript_AddMessage_uScript_AddMessage_267.Shown)
		{
			Relay_In_289();
		}
	}

	private void Relay_In_269()
	{
		logic_uScript_AddMessage_messageData_269 = MsgCrazedIntro03;
		logic_uScript_AddMessage_speaker_269 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_269 = logic_uScript_AddMessage_uScript_AddMessage_269.In(logic_uScript_AddMessage_messageData_269, logic_uScript_AddMessage_speaker_269);
		if (logic_uScript_AddMessage_uScript_AddMessage_269.Shown)
		{
			Relay_In_290();
		}
	}

	private void Relay_In_274()
	{
		logic_uScript_AddMessage_messageData_274 = MsgCrazedLeaderB4Fight01;
		logic_uScript_AddMessage_speaker_274 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_274 = logic_uScript_AddMessage_uScript_AddMessage_274.In(logic_uScript_AddMessage_messageData_274, logic_uScript_AddMessage_speaker_274);
		if (logic_uScript_AddMessage_uScript_AddMessage_274.Shown)
		{
			Relay_In_308();
		}
	}

	private void Relay_In_276()
	{
		logic_uScript_AddMessage_messageData_276 = MsgCrazedLeaderB4Fight02;
		logic_uScript_AddMessage_speaker_276 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_276 = logic_uScript_AddMessage_uScript_AddMessage_276.In(logic_uScript_AddMessage_messageData_276, logic_uScript_AddMessage_speaker_276);
		if (logic_uScript_AddMessage_uScript_AddMessage_276.Shown)
		{
			Relay_True_70();
		}
	}

	private void Relay_In_278()
	{
		logic_uScript_AddMessage_messageData_278 = MsgCubeDestroyed01;
		logic_uScript_AddMessage_speaker_278 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_278 = logic_uScript_AddMessage_uScript_AddMessage_278.In(logic_uScript_AddMessage_messageData_278, logic_uScript_AddMessage_speaker_278);
		if (logic_uScript_AddMessage_uScript_AddMessage_278.Shown)
		{
			Relay_In_312();
		}
	}

	private void Relay_In_283()
	{
		logic_uScript_AddMessage_messageData_283 = MsgCrazedIntro05;
		logic_uScript_AddMessage_speaker_283 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_283 = logic_uScript_AddMessage_uScript_AddMessage_283.In(logic_uScript_AddMessage_messageData_283, logic_uScript_AddMessage_speaker_283);
		if (logic_uScript_AddMessage_uScript_AddMessage_283.Out)
		{
			Relay_In_294();
		}
	}

	private void Relay_Output1_284()
	{
		Relay_In_202();
	}

	private void Relay_Output2_284()
	{
		Relay_In_317();
	}

	private void Relay_Output3_284()
	{
		Relay_In_300();
	}

	private void Relay_Output4_284()
	{
		Relay_In_328();
	}

	private void Relay_Output5_284()
	{
		Relay_In_269();
	}

	private void Relay_Output6_284()
	{
		Relay_In_339();
	}

	private void Relay_Output7_284()
	{
		Relay_In_267();
	}

	private void Relay_Output8_284()
	{
		Relay_In_283();
	}

	private void Relay_In_284()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_284 = local_CrazedDialog_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_284.In(logic_uScriptCon_ManualSwitch_CurrentOutput_284);
	}

	private void Relay_In_286()
	{
		logic_uScriptAct_AddInt_v2_A_286 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_286.In(logic_uScriptAct_AddInt_v2_A_286, logic_uScriptAct_AddInt_v2_B_286, out logic_uScriptAct_AddInt_v2_IntResult_286, out logic_uScriptAct_AddInt_v2_FloatResult_286);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_286;
	}

	private void Relay_In_289()
	{
		logic_uScriptAct_AddInt_v2_A_289 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_289.In(logic_uScriptAct_AddInt_v2_A_289, logic_uScriptAct_AddInt_v2_B_289, out logic_uScriptAct_AddInt_v2_IntResult_289, out logic_uScriptAct_AddInt_v2_FloatResult_289);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_289;
	}

	private void Relay_In_290()
	{
		logic_uScriptAct_AddInt_v2_A_290 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_290.In(logic_uScriptAct_AddInt_v2_A_290, logic_uScriptAct_AddInt_v2_B_290, out logic_uScriptAct_AddInt_v2_IntResult_290, out logic_uScriptAct_AddInt_v2_FloatResult_290);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_290;
	}

	private void Relay_In_294()
	{
		logic_uScript_SetEncounterTarget_owner_294 = owner_Connection_295;
		logic_uScript_SetEncounterTarget_visibleObject_294 = local_CubeTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_294.In(logic_uScript_SetEncounterTarget_owner_294, logic_uScript_SetEncounterTarget_visibleObject_294);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_294.Out)
		{
			Relay_In_296();
		}
	}

	private void Relay_Out_296()
	{
		Relay_True_298();
	}

	private void Relay_In_296()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_296 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_296.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_296, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_296);
	}

	private void Relay_True_298()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_298.True(out logic_uScriptAct_SetBool_Target_298);
		local_CrazedIntroPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_298;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_298.Out)
		{
			Relay_True_707();
		}
	}

	private void Relay_False_298()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_298.False(out logic_uScriptAct_SetBool_Target_298);
		local_CrazedIntroPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_298;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_298.Out)
		{
			Relay_True_707();
		}
	}

	private void Relay_In_300()
	{
		logic_uScript_AddMessage_messageData_300 = MsgCrazedIntro02;
		logic_uScript_AddMessage_speaker_300 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_300 = logic_uScript_AddMessage_uScript_AddMessage_300.In(logic_uScript_AddMessage_messageData_300, logic_uScript_AddMessage_speaker_300);
		if (logic_uScript_AddMessage_uScript_AddMessage_300.Shown)
		{
			Relay_In_303();
		}
	}

	private void Relay_In_303()
	{
		logic_uScriptAct_AddInt_v2_A_303 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_303.In(logic_uScriptAct_AddInt_v2_A_303, logic_uScriptAct_AddInt_v2_B_303, out logic_uScriptAct_AddInt_v2_IntResult_303, out logic_uScriptAct_AddInt_v2_FloatResult_303);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_303;
	}

	private void Relay_Output1_304()
	{
		Relay_In_72();
	}

	private void Relay_Output2_304()
	{
		Relay_In_274();
	}

	private void Relay_Output3_304()
	{
		Relay_In_557();
	}

	private void Relay_Output4_304()
	{
		Relay_In_276();
	}

	private void Relay_Output5_304()
	{
	}

	private void Relay_Output6_304()
	{
	}

	private void Relay_Output7_304()
	{
	}

	private void Relay_Output8_304()
	{
	}

	private void Relay_In_304()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_304 = local_CubeDialog_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_304.In(logic_uScriptCon_ManualSwitch_CurrentOutput_304);
	}

	private void Relay_In_305()
	{
		logic_uScriptAct_AddInt_v2_A_305 = local_CubeDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_305.In(logic_uScriptAct_AddInt_v2_A_305, logic_uScriptAct_AddInt_v2_B_305, out logic_uScriptAct_AddInt_v2_IntResult_305, out logic_uScriptAct_AddInt_v2_FloatResult_305);
		local_CubeDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_305;
	}

	private void Relay_In_308()
	{
		logic_uScriptAct_AddInt_v2_A_308 = local_CubeDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_308.In(logic_uScriptAct_AddInt_v2_A_308, logic_uScriptAct_AddInt_v2_B_308, out logic_uScriptAct_AddInt_v2_IntResult_308, out logic_uScriptAct_AddInt_v2_FloatResult_308);
		local_CubeDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_308;
	}

	private void Relay_Pause_310()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_310.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_310.Out)
		{
			Relay_Succeed_33();
		}
	}

	private void Relay_UnPause_310()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_310.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_310.Out)
		{
			Relay_Succeed_33();
		}
	}

	private void Relay_Output1_311()
	{
		Relay_In_278();
	}

	private void Relay_Output2_311()
	{
		Relay_In_482();
	}

	private void Relay_Output3_311()
	{
	}

	private void Relay_Output4_311()
	{
	}

	private void Relay_Output5_311()
	{
	}

	private void Relay_Output6_311()
	{
	}

	private void Relay_Output7_311()
	{
	}

	private void Relay_Output8_311()
	{
	}

	private void Relay_In_311()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_311 = local_CubeDestroyedDialog_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_311.In(logic_uScriptCon_ManualSwitch_CurrentOutput_311);
	}

	private void Relay_In_312()
	{
		logic_uScriptAct_AddInt_v2_A_312 = local_CubeDestroyedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_312.In(logic_uScriptAct_AddInt_v2_A_312, logic_uScriptAct_AddInt_v2_B_312, out logic_uScriptAct_AddInt_v2_IntResult_312, out logic_uScriptAct_AddInt_v2_FloatResult_312);
		local_CubeDestroyedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_312;
	}

	private void Relay_True_315()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_315.True(out logic_uScriptAct_SetBool_Target_315);
		local_HasBeenInterrupted_System_Boolean = logic_uScriptAct_SetBool_Target_315;
	}

	private void Relay_False_315()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_315.False(out logic_uScriptAct_SetBool_Target_315);
		local_HasBeenInterrupted_System_Boolean = logic_uScriptAct_SetBool_Target_315;
	}

	private void Relay_In_316()
	{
		logic_uScriptAct_AddInt_v2_A_316 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_316.In(logic_uScriptAct_AddInt_v2_A_316, logic_uScriptAct_AddInt_v2_B_316, out logic_uScriptAct_AddInt_v2_IntResult_316, out logic_uScriptAct_AddInt_v2_FloatResult_316);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_316;
	}

	private void Relay_In_317()
	{
		logic_uScriptCon_CompareBool_Bool_317 = local_HasBeenInterrupted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_317.In(logic_uScriptCon_CompareBool_Bool_317);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_317.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_317.False;
		if (num)
		{
			Relay_In_318();
		}
		if (flag)
		{
			Relay_In_321();
		}
	}

	private void Relay_In_318()
	{
		logic_uScriptAct_SubtractInt_A_318 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_318.In(logic_uScriptAct_SubtractInt_A_318, logic_uScriptAct_SubtractInt_B_318, out logic_uScriptAct_SubtractInt_IntResult_318, out logic_uScriptAct_SubtractInt_FloatResult_318);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_SubtractInt_IntResult_318;
		if (logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_318.Out)
		{
			Relay_False_315();
		}
	}

	private void Relay_In_321()
	{
		logic_uScript_AddMessage_messageData_321 = MsgCrazedMinionInterrupt01;
		logic_uScript_AddMessage_speaker_321 = GroupMinionTechSpeaker;
		logic_uScript_AddMessage_Return_321 = logic_uScript_AddMessage_uScript_AddMessage_321.In(logic_uScript_AddMessage_messageData_321, logic_uScript_AddMessage_speaker_321);
		if (logic_uScript_AddMessage_uScript_AddMessage_321.Shown)
		{
			Relay_In_316();
		}
	}

	private void Relay_True_326()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_326.True(out logic_uScriptAct_SetBool_Target_326);
		local_HasBeenInterrupted_System_Boolean = logic_uScriptAct_SetBool_Target_326;
	}

	private void Relay_False_326()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_326.False(out logic_uScriptAct_SetBool_Target_326);
		local_HasBeenInterrupted_System_Boolean = logic_uScriptAct_SetBool_Target_326;
	}

	private void Relay_In_327()
	{
		logic_uScriptAct_AddInt_v2_A_327 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_327.In(logic_uScriptAct_AddInt_v2_A_327, logic_uScriptAct_AddInt_v2_B_327, out logic_uScriptAct_AddInt_v2_IntResult_327, out logic_uScriptAct_AddInt_v2_FloatResult_327);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_327;
	}

	private void Relay_In_328()
	{
		logic_uScriptCon_CompareBool_Bool_328 = local_HasBeenInterrupted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_328.In(logic_uScriptCon_CompareBool_Bool_328);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_328.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_328.False;
		if (num)
		{
			Relay_In_329();
		}
		if (flag)
		{
			Relay_In_332();
		}
	}

	private void Relay_In_329()
	{
		logic_uScriptAct_SubtractInt_A_329 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_329.In(logic_uScriptAct_SubtractInt_A_329, logic_uScriptAct_SubtractInt_B_329, out logic_uScriptAct_SubtractInt_IntResult_329, out logic_uScriptAct_SubtractInt_FloatResult_329);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_SubtractInt_IntResult_329;
		if (logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_329.Out)
		{
			Relay_False_326();
		}
	}

	private void Relay_In_332()
	{
		logic_uScript_AddMessage_messageData_332 = MsgCrazedMinionInterrupt02;
		logic_uScript_AddMessage_speaker_332 = GroupMinionTechSpeaker;
		logic_uScript_AddMessage_Return_332 = logic_uScript_AddMessage_uScript_AddMessage_332.In(logic_uScript_AddMessage_messageData_332, logic_uScript_AddMessage_speaker_332);
		if (logic_uScript_AddMessage_uScript_AddMessage_332.Shown)
		{
			Relay_In_327();
		}
	}

	private void Relay_True_337()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_337.True(out logic_uScriptAct_SetBool_Target_337);
		local_HasBeenInterrupted_System_Boolean = logic_uScriptAct_SetBool_Target_337;
	}

	private void Relay_False_337()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_337.False(out logic_uScriptAct_SetBool_Target_337);
		local_HasBeenInterrupted_System_Boolean = logic_uScriptAct_SetBool_Target_337;
	}

	private void Relay_In_338()
	{
		logic_uScriptAct_AddInt_v2_A_338 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_338.In(logic_uScriptAct_AddInt_v2_A_338, logic_uScriptAct_AddInt_v2_B_338, out logic_uScriptAct_AddInt_v2_IntResult_338, out logic_uScriptAct_AddInt_v2_FloatResult_338);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_338;
	}

	private void Relay_In_339()
	{
		logic_uScriptCon_CompareBool_Bool_339 = local_HasBeenInterrupted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_339.In(logic_uScriptCon_CompareBool_Bool_339);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_339.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_339.False;
		if (num)
		{
			Relay_In_340();
		}
		if (flag)
		{
			Relay_In_343();
		}
	}

	private void Relay_In_340()
	{
		logic_uScriptAct_SubtractInt_A_340 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_340.In(logic_uScriptAct_SubtractInt_A_340, logic_uScriptAct_SubtractInt_B_340, out logic_uScriptAct_SubtractInt_IntResult_340, out logic_uScriptAct_SubtractInt_FloatResult_340);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_SubtractInt_IntResult_340;
		if (logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_340.Out)
		{
			Relay_False_337();
		}
	}

	private void Relay_In_343()
	{
		logic_uScript_AddMessage_messageData_343 = MsgCrazedMinionInterrupt03;
		logic_uScript_AddMessage_speaker_343 = GroupMinionTechSpeaker;
		logic_uScript_AddMessage_Return_343 = logic_uScript_AddMessage_uScript_AddMessage_343.In(logic_uScript_AddMessage_messageData_343, logic_uScript_AddMessage_speaker_343);
		if (logic_uScript_AddMessage_uScript_AddMessage_343.Shown)
		{
			Relay_In_338();
		}
	}

	private void Relay_In_354()
	{
		int num = 0;
		Array enemyMinionWaveData = EnemyMinionWaveData;
		if (logic_uScript_GetAndCheckTechs_techData_354.Length != num + enemyMinionWaveData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_354, num + enemyMinionWaveData.Length);
		}
		Array.Copy(enemyMinionWaveData, 0, logic_uScript_GetAndCheckTechs_techData_354, num, enemyMinionWaveData.Length);
		num += enemyMinionWaveData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_354 = owner_Connection_353;
		int num2 = 0;
		Array array = local_MinionWaveTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_354.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_354, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_354, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_354 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_354.In(logic_uScript_GetAndCheckTechs_techData_354, logic_uScript_GetAndCheckTechs_ownerNode_354, ref logic_uScript_GetAndCheckTechs_techs_354);
		local_MinionWaveTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_354;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_354.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_354.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_354.AllDead;
		if (allAlive)
		{
			Relay_In_429();
		}
		if (someAlive)
		{
			Relay_In_429();
		}
		if (allDead)
		{
			Relay_In_368();
		}
	}

	private void Relay_In_360()
	{
		logic_uScript_AddMessage_messageData_360 = MsgCrazedAmbush;
		logic_uScript_AddMessage_speaker_360 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_360 = logic_uScript_AddMessage_uScript_AddMessage_360.In(logic_uScript_AddMessage_messageData_360, logic_uScript_AddMessage_speaker_360);
		if (logic_uScript_AddMessage_uScript_AddMessage_360.Out)
		{
			Relay_In_590();
		}
	}

	private void Relay_In_363()
	{
		int num = 0;
		Array crazedTechData = CrazedTechData;
		if (logic_uScript_GetAndCheckTechs_techData_363.Length != num + crazedTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_363, num + crazedTechData.Length);
		}
		Array.Copy(crazedTechData, 0, logic_uScript_GetAndCheckTechs_techData_363, num, crazedTechData.Length);
		num += crazedTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_363 = owner_Connection_362;
		int num2 = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_363.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_363, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_363, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_363 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_363.In(logic_uScript_GetAndCheckTechs_techData_363, logic_uScript_GetAndCheckTechs_ownerNode_363, ref logic_uScript_GetAndCheckTechs_techs_363);
		local_CrazedTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_363;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_363.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_363.SomeAlive;
		if (allAlive)
		{
			Relay_In_838();
		}
		if (someAlive)
		{
			Relay_In_838();
		}
	}

	private void Relay_True_364()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_364.True(out logic_uScriptAct_SetBool_Target_364);
		local_CrazedAmbushTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_364;
	}

	private void Relay_False_364()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_364.False(out logic_uScriptAct_SetBool_Target_364);
		local_CrazedAmbushTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_364;
	}

	private void Relay_In_368()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_368 = MissionCompleteArea;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_368.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_368);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_368.InRange)
		{
			Relay_True_585();
		}
	}

	private void Relay_InitialSpawn_370()
	{
		int num = 0;
		Array fillerTechData = FillerTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_370.Length != num + fillerTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_370, num + fillerTechData.Length);
		}
		Array.Copy(fillerTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_370, num, fillerTechData.Length);
		num += fillerTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_370 = owner_Connection_369;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_370.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_370, logic_uScript_SpawnTechsFromData_ownerNode_370, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_370, logic_uScript_SpawnTechsFromData_allowResurrection_370);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_370.Out)
		{
			Relay_InitialSpawn_397();
		}
	}

	private void Relay_In_372()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_372 = FillerNPCRange01;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_372.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_372);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_372.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_372.OutOfRange;
		if (inRange)
		{
			Relay_In_380();
		}
		if (outOfRange)
		{
			Relay_In_710();
		}
	}

	private void Relay_In_374()
	{
		logic_uScript_AddMessage_messageData_374 = MsgFillerNPC01;
		logic_uScript_AddMessage_speaker_374 = GroupMinionTechSpeaker;
		logic_uScript_AddMessage_Return_374 = logic_uScript_AddMessage_uScript_AddMessage_374.In(logic_uScript_AddMessage_messageData_374, logic_uScript_AddMessage_speaker_374);
		if (logic_uScript_AddMessage_uScript_AddMessage_374.Out)
		{
			Relay_True_381();
		}
	}

	private void Relay_In_376()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_376 = FillerNPCRange02;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_376.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_376);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_376.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_376.OutOfRange;
		if (inRange)
		{
			Relay_In_386();
		}
		if (outOfRange)
		{
			Relay_In_711();
		}
	}

	private void Relay_In_380()
	{
		logic_uScriptCon_CompareBool_Bool_380 = local_FillerMsgPlayed01_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_380.In(logic_uScriptCon_CompareBool_Bool_380);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_380.False)
		{
			Relay_In_374();
		}
	}

	private void Relay_True_381()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_381.True(out logic_uScriptAct_SetBool_Target_381);
		local_FillerMsgPlayed01_System_Boolean = logic_uScriptAct_SetBool_Target_381;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_381.Out)
		{
			Relay_In_376();
		}
	}

	private void Relay_False_381()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_381.False(out logic_uScriptAct_SetBool_Target_381);
		local_FillerMsgPlayed01_System_Boolean = logic_uScriptAct_SetBool_Target_381;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_381.Out)
		{
			Relay_In_376();
		}
	}

	private void Relay_True_382()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_382.True(out logic_uScriptAct_SetBool_Target_382);
		local_FillerMsgPlayed02_System_Boolean = logic_uScriptAct_SetBool_Target_382;
	}

	private void Relay_False_382()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_382.False(out logic_uScriptAct_SetBool_Target_382);
		local_FillerMsgPlayed02_System_Boolean = logic_uScriptAct_SetBool_Target_382;
	}

	private void Relay_In_385()
	{
		logic_uScript_AddMessage_messageData_385 = MsgFillerNPC02;
		logic_uScript_AddMessage_speaker_385 = CrazedMinionTechSpeaker;
		logic_uScript_AddMessage_Return_385 = logic_uScript_AddMessage_uScript_AddMessage_385.In(logic_uScript_AddMessage_messageData_385, logic_uScript_AddMessage_speaker_385);
		if (logic_uScript_AddMessage_uScript_AddMessage_385.Out)
		{
			Relay_True_382();
		}
	}

	private void Relay_In_386()
	{
		logic_uScriptCon_CompareBool_Bool_386 = local_FillerMsgPlayed02_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_386.In(logic_uScriptCon_CompareBool_Bool_386);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_386.False)
		{
			Relay_In_385();
		}
	}

	private void Relay_True_388()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_388.True(out logic_uScriptAct_SetBool_Target_388);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_388;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_388.Out)
		{
			Relay_True_389();
		}
	}

	private void Relay_False_388()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_388.False(out logic_uScriptAct_SetBool_Target_388);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_388;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_388.Out)
		{
			Relay_True_389();
		}
	}

	private void Relay_True_389()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_389.True(out logic_uScriptAct_SetBool_Target_389);
		local_CubeNeedsReload_System_Boolean = logic_uScriptAct_SetBool_Target_389;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_389.Out)
		{
			Relay_False_392();
		}
	}

	private void Relay_False_389()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_389.False(out logic_uScriptAct_SetBool_Target_389);
		local_CubeNeedsReload_System_Boolean = logic_uScriptAct_SetBool_Target_389;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_389.Out)
		{
			Relay_False_392();
		}
	}

	private void Relay_True_392()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_392.True(out logic_uScriptAct_SetBool_Target_392);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_392;
	}

	private void Relay_False_392()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_392.False(out logic_uScriptAct_SetBool_Target_392);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_392;
	}

	private void Relay_True_395()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_395.True(out logic_uScriptAct_SetBool_Target_395);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_395;
	}

	private void Relay_False_395()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_395.False(out logic_uScriptAct_SetBool_Target_395);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_395;
	}

	private void Relay_InitialSpawn_397()
	{
		int num = 0;
		Array enemyMinionWaveData = EnemyMinionWaveData;
		if (logic_uScript_SpawnTechsFromData_spawnData_397.Length != num + enemyMinionWaveData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_397, num + enemyMinionWaveData.Length);
		}
		Array.Copy(enemyMinionWaveData, 0, logic_uScript_SpawnTechsFromData_spawnData_397, num, enemyMinionWaveData.Length);
		num += enemyMinionWaveData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_397 = owner_Connection_399;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_397.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_397, logic_uScript_SpawnTechsFromData_ownerNode_397, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_397, logic_uScript_SpawnTechsFromData_allowResurrection_397);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_397.Out)
		{
			Relay_In_38();
		}
	}

	private void Relay_In_400()
	{
		logic_uScript_SetTankInvulnerable_tank_400 = local_EnemyMinionWaveTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_400.In(logic_uScript_SetTankInvulnerable_invulnerable_400, logic_uScript_SetTankInvulnerable_tank_400);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_400.Out)
		{
			Relay_In_431();
		}
	}

	private void Relay_AtIndex_401()
	{
		int num = 0;
		Array array = local_EnemyMinionWaveTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_401.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_401, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_401, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_401.AtIndex(ref logic_uScript_AccessListTech_techList_401, logic_uScript_AccessListTech_index_401, out logic_uScript_AccessListTech_value_401);
		local_EnemyMinionWaveTechs_TankArray = logic_uScript_AccessListTech_techList_401;
		local_EnemyMinionWaveTech_Tank = logic_uScript_AccessListTech_value_401;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_401.Out)
		{
			Relay_In_402();
		}
	}

	private void Relay_In_402()
	{
		int num = 0;
		Array array = local_EnemyMinionWaveTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_402.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_402, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_402, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_402.In(ref logic_uScript_SetTechsTeam_techs_402, logic_uScript_SetTechsTeam_team_402);
		local_EnemyMinionWaveTechs_TankArray = logic_uScript_SetTechsTeam_techs_402;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_402.Out)
		{
			Relay_In_400();
		}
	}

	private void Relay_In_403()
	{
		int num = 0;
		Array enemyMinionWaveData = EnemyMinionWaveData;
		if (logic_uScript_GetAndCheckTechs_techData_403.Length != num + enemyMinionWaveData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_403, num + enemyMinionWaveData.Length);
		}
		Array.Copy(enemyMinionWaveData, 0, logic_uScript_GetAndCheckTechs_techData_403, num, enemyMinionWaveData.Length);
		num += enemyMinionWaveData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_403 = owner_Connection_406;
		int num2 = 0;
		Array array = local_EnemyMinionWaveTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_403.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_403, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_403, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_403 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_403.In(logic_uScript_GetAndCheckTechs_techData_403, logic_uScript_GetAndCheckTechs_ownerNode_403, ref logic_uScript_GetAndCheckTechs_techs_403);
		local_EnemyMinionWaveTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_403;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_403.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_403.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_401();
		}
		if (someAlive)
		{
			Relay_AtIndex_401();
		}
	}

	private void Relay_In_410()
	{
		logic_uScript_FlyTechUpAndAway_tech_410 = local_FillerTech01_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_410 = TechFlyAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_410 = TechFlyParticles;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_410.In(logic_uScript_FlyTechUpAndAway_tech_410, logic_uScript_FlyTechUpAndAway_maxLifetime_410, logic_uScript_FlyTechUpAndAway_targetHeight_410, logic_uScript_FlyTechUpAndAway_aiTree_410, logic_uScript_FlyTechUpAndAway_removalParticles_410);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_410.Out)
		{
			Relay_AtIndex_419();
		}
	}

	private void Relay_AtIndex_414()
	{
		int num = 0;
		Array array = local_FillerTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_414.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_414, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_414, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_414.AtIndex(ref logic_uScript_AccessListTech_techList_414, logic_uScript_AccessListTech_index_414, out logic_uScript_AccessListTech_value_414);
		local_FillerTechs_TankArray = logic_uScript_AccessListTech_techList_414;
		local_FillerTech01_Tank = logic_uScript_AccessListTech_value_414;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_414.Out)
		{
			Relay_In_410();
		}
	}

	private void Relay_In_415()
	{
		int num = 0;
		Array fillerTechData = FillerTechData;
		if (logic_uScript_GetAndCheckTechs_techData_415.Length != num + fillerTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_415, num + fillerTechData.Length);
		}
		Array.Copy(fillerTechData, 0, logic_uScript_GetAndCheckTechs_techData_415, num, fillerTechData.Length);
		num += fillerTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_415 = owner_Connection_416;
		int num2 = 0;
		Array array = local_FillerTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_415.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_415, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_415, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_415 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_415.In(logic_uScript_GetAndCheckTechs_techData_415, logic_uScript_GetAndCheckTechs_ownerNode_415, ref logic_uScript_GetAndCheckTechs_techs_415);
		local_FillerTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_415;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_415.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_415.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_415.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_414();
		}
		if (someAlive)
		{
			Relay_AtIndex_414();
		}
		if (allDead)
		{
			Relay_In_872();
		}
	}

	private void Relay_AtIndex_419()
	{
		int num = 0;
		Array array = local_FillerTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_419.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_419, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_419, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_419.AtIndex(ref logic_uScript_AccessListTech_techList_419, logic_uScript_AccessListTech_index_419, out logic_uScript_AccessListTech_value_419);
		local_FillerTechs_TankArray = logic_uScript_AccessListTech_techList_419;
		local_FillerTech02_Tank = logic_uScript_AccessListTech_value_419;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_419.Out)
		{
			Relay_In_421();
		}
	}

	private void Relay_In_421()
	{
		logic_uScript_FlyTechUpAndAway_tech_421 = local_FillerTech02_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_421 = TechFlyAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_421 = TechFlyParticles;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_421.In(logic_uScript_FlyTechUpAndAway_tech_421, logic_uScript_FlyTechUpAndAway_maxLifetime_421, logic_uScript_FlyTechUpAndAway_targetHeight_421, logic_uScript_FlyTechUpAndAway_aiTree_421, logic_uScript_FlyTechUpAndAway_removalParticles_421);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_421.Out)
		{
			Relay_AtIndex_426();
		}
	}

	private void Relay_AtIndex_426()
	{
		int num = 0;
		Array array = local_FillerTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_426.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_426, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_426, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_426.AtIndex(ref logic_uScript_AccessListTech_techList_426, logic_uScript_AccessListTech_index_426, out logic_uScript_AccessListTech_value_426);
		local_FillerTechs_TankArray = logic_uScript_AccessListTech_techList_426;
		local_FillerTech03_Tank = logic_uScript_AccessListTech_value_426;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_426.Out)
		{
			Relay_In_428();
		}
	}

	private void Relay_In_428()
	{
		logic_uScript_FlyTechUpAndAway_tech_428 = local_FillerTech03_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_428 = TechFlyAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_428 = TechFlyParticles;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_428.In(logic_uScript_FlyTechUpAndAway_tech_428, logic_uScript_FlyTechUpAndAway_maxLifetime_428, logic_uScript_FlyTechUpAndAway_targetHeight_428, logic_uScript_FlyTechUpAndAway_aiTree_428, logic_uScript_FlyTechUpAndAway_removalParticles_428);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_428.Out)
		{
			Relay_True_700();
		}
	}

	private void Relay_In_429()
	{
		logic_uScriptCon_CompareBool_Bool_429 = local_CrazedAmbushTriggered_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_429.In(logic_uScriptCon_CompareBool_Bool_429);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_429.False)
		{
			Relay_In_504();
		}
	}

	private void Relay_In_431()
	{
		logic_uScript_SetTechAIType_tech_431 = local_EnemyMinionWaveTech_Tank;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_431.In(logic_uScript_SetTechAIType_tech_431, logic_uScript_SetTechAIType_aiType_431);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_431.Out)
		{
			Relay_In_478();
		}
	}

	private void Relay_In_435()
	{
		logic_uScript_AddMessage_messageData_435 = msgCubeTooEarly;
		logic_uScript_AddMessage_speaker_435 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_435 = logic_uScript_AddMessage_uScript_AddMessage_435.In(logic_uScript_AddMessage_messageData_435, logic_uScript_AddMessage_speaker_435);
		if (logic_uScript_AddMessage_uScript_AddMessage_435.Out)
		{
			Relay_True_437();
		}
	}

	private void Relay_True_437()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_437.True(out logic_uScriptAct_SetBool_Target_437);
		local_CubeTooEarlyMsgPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_437;
	}

	private void Relay_False_437()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_437.False(out logic_uScriptAct_SetBool_Target_437);
		local_CubeTooEarlyMsgPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_437;
	}

	private void Relay_In_442()
	{
		logic_uScriptCon_CompareBool_Bool_442 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_442.In(logic_uScriptCon_CompareBool_Bool_442);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_442.False)
		{
			Relay_In_435();
		}
	}

	private void Relay_In_443()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_443 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_443.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_443);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_443.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_443.OutOfRange;
		if (inRange)
		{
			Relay_In_442();
		}
		if (outOfRange)
		{
			Relay_In_712();
		}
	}

	private void Relay_In_445()
	{
		logic_uScript_AddMessage_messageData_445 = MsgMissionCompleteNoTrigger;
		logic_uScript_AddMessage_speaker_445 = GCSpeaker;
		logic_uScript_AddMessage_Return_445 = logic_uScript_AddMessage_uScript_AddMessage_445.In(logic_uScript_AddMessage_messageData_445, logic_uScript_AddMessage_speaker_445);
		if (logic_uScript_AddMessage_uScript_AddMessage_445.Out)
		{
			Relay_UnPause_310();
		}
	}

	private void Relay_In_449()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_449 = NPCMsgTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_449.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_449, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_449);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_449.Out)
		{
			Relay_In_360();
		}
	}

	private void Relay_In_450()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_450 = CrazedMsgTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_450.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_450, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_450);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_450.Out)
		{
			Relay_In_449();
		}
	}

	private void Relay_In_451()
	{
		logic_uScript_SetBatteryChargeAmount_tech_451 = local_CubeTech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_451.In(logic_uScript_SetBatteryChargeAmount_tech_451, logic_uScript_SetBatteryChargeAmount_chargeAmount_451);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_451.Out)
		{
			Relay_In_581();
		}
	}

	private void Relay_In_452()
	{
		logic_uScriptCon_CompareBool_Bool_452 = local_CrazedNPCInRange_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_452.In(logic_uScriptCon_CompareBool_Bool_452);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_452.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_452.False;
		if (num)
		{
			Relay_In_453();
		}
		if (flag)
		{
			Relay_In_372();
		}
	}

	private void Relay_In_453()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_453.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_453.Out)
		{
			Relay_In_454();
		}
	}

	private void Relay_In_454()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_454.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_454.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_457()
	{
		logic_uScript_SetBatteryChargeAmount_tech_457 = local_CubeTech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_457.In(logic_uScript_SetBatteryChargeAmount_tech_457, logic_uScript_SetBatteryChargeAmount_chargeAmount_457);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_457.Out)
		{
			Relay_In_580();
		}
	}

	private void Relay_In_459()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_459.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_459, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_459, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_459.In(ref logic_uScript_SetTechsTeam_techs_459, logic_uScript_SetTechsTeam_team_459);
		local_CrazedTechs_TankArray = logic_uScript_SetTechsTeam_techs_459;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_459.Out)
		{
			Relay_AtIndex_464();
		}
	}

	private void Relay_In_462()
	{
		int num = 0;
		Array enemyMinionWaveData = EnemyMinionWaveData;
		if (logic_uScript_GetAndCheckTechs_techData_462.Length != num + enemyMinionWaveData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_462, num + enemyMinionWaveData.Length);
		}
		Array.Copy(enemyMinionWaveData, 0, logic_uScript_GetAndCheckTechs_techData_462, num, enemyMinionWaveData.Length);
		num += enemyMinionWaveData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_462 = owner_Connection_460;
		int num2 = 0;
		Array array = local_MinionWaveTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_462.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_462, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_462, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_462 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_462.In(logic_uScript_GetAndCheckTechs_techData_462, logic_uScript_GetAndCheckTechs_ownerNode_462, ref logic_uScript_GetAndCheckTechs_techs_462);
		local_MinionWaveTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_462;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_462.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_462.SomeAlive;
		if (allAlive)
		{
			Relay_In_363();
		}
		if (someAlive)
		{
			Relay_In_363();
		}
	}

	private void Relay_AtIndex_464()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_464.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_464, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_464, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_464.AtIndex(ref logic_uScript_AccessListTech_techList_464, logic_uScript_AccessListTech_index_464, out logic_uScript_AccessListTech_value_464);
		local_CrazedTechs_TankArray = logic_uScript_AccessListTech_techList_464;
		local_CrazedTech02_Tank = logic_uScript_AccessListTech_value_464;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_464.Out)
		{
			Relay_In_466();
		}
	}

	private void Relay_In_465()
	{
		logic_uScript_SetTankInvulnerable_tank_465 = local_CrazedTech03_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_465.In(logic_uScript_SetTankInvulnerable_invulnerable_465, logic_uScript_SetTankInvulnerable_tank_465);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_465.Out)
		{
			Relay_In_472();
		}
	}

	private void Relay_In_466()
	{
		logic_uScript_SetTankInvulnerable_tank_466 = local_CrazedTech02_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_466.In(logic_uScript_SetTankInvulnerable_invulnerable_466, logic_uScript_SetTankInvulnerable_tank_466);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_466.Out)
		{
			Relay_In_474();
		}
	}

	private void Relay_In_469()
	{
		logic_uScript_SetTechAIType_tech_469 = local_CrazedTech03_Tank;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_469.In(logic_uScript_SetTechAIType_tech_469, logic_uScript_SetTechAIType_aiType_469);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_469.Out)
		{
			Relay_True_364();
		}
	}

	private void Relay_In_471()
	{
		logic_uScript_SetTechAIType_tech_471 = local_CrazedTech02_Tank;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_471.In(logic_uScript_SetTechAIType_tech_471, logic_uScript_SetTechAIType_aiType_471);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_471.Out)
		{
			Relay_AtIndex_476();
		}
	}

	private void Relay_In_472()
	{
		logic_uScript_SetBatteryChargeAmount_tech_472 = local_CrazedTech03_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_472.In(logic_uScript_SetBatteryChargeAmount_tech_472, logic_uScript_SetBatteryChargeAmount_chargeAmount_472);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_472.Out)
		{
			Relay_In_469();
		}
	}

	private void Relay_In_474()
	{
		logic_uScript_SetBatteryChargeAmount_tech_474 = local_CrazedTech02_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_474.In(logic_uScript_SetBatteryChargeAmount_tech_474, logic_uScript_SetBatteryChargeAmount_chargeAmount_474);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_474.Out)
		{
			Relay_In_471();
		}
	}

	private void Relay_AtIndex_476()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_476.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_476, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_476, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_476.AtIndex(ref logic_uScript_AccessListTech_techList_476, logic_uScript_AccessListTech_index_476, out logic_uScript_AccessListTech_value_476);
		local_CrazedTechs_TankArray = logic_uScript_AccessListTech_techList_476;
		local_CrazedTech03_Tank = logic_uScript_AccessListTech_value_476;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_476.Out)
		{
			Relay_In_465();
		}
	}

	private void Relay_In_478()
	{
		logic_uScript_AddMessage_messageData_478 = MsgCubeDestroyed02;
		logic_uScript_AddMessage_speaker_478 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_478 = logic_uScript_AddMessage_uScript_AddMessage_478.In(logic_uScript_AddMessage_messageData_478, logic_uScript_AddMessage_speaker_478);
		if (logic_uScript_AddMessage_uScript_AddMessage_478.Out)
		{
			Relay_True_695();
		}
	}

	private void Relay_In_482()
	{
		logic_uScript_AddMessage_messageData_482 = MsgFillerNPCFly;
		logic_uScript_AddMessage_speaker_482 = CrazedMinionTechSpeaker;
		logic_uScript_AddMessage_Return_482 = logic_uScript_AddMessage_uScript_AddMessage_482.In(logic_uScript_AddMessage_messageData_482, logic_uScript_AddMessage_speaker_482);
		if (logic_uScript_AddMessage_uScript_AddMessage_482.Shown)
		{
			Relay_True_123();
		}
	}

	private void Relay_True_483()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_483.True(out logic_uScriptAct_SetBool_Target_483);
		local_CrazedNPCInRange_System_Boolean = logic_uScriptAct_SetBool_Target_483;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_483.Out)
		{
			Relay_False_506();
		}
	}

	private void Relay_False_483()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_483.False(out logic_uScriptAct_SetBool_Target_483);
		local_CrazedNPCInRange_System_Boolean = logic_uScriptAct_SetBool_Target_483;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_483.Out)
		{
			Relay_False_506();
		}
	}

	private void Relay_In_488()
	{
		logic_uScript_AddMessage_messageData_488 = MsgCrazedInterrupt;
		logic_uScript_AddMessage_speaker_488 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_488 = logic_uScript_AddMessage_uScript_AddMessage_488.In(logic_uScript_AddMessage_messageData_488, logic_uScript_AddMessage_speaker_488);
		if (logic_uScript_AddMessage_uScript_AddMessage_488.Out)
		{
			Relay_True_493();
		}
	}

	private void Relay_True_490()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_490.True(out logic_uScriptAct_SetBool_Target_490);
		local_CrazedPlayInterruptOnce_System_Boolean = logic_uScriptAct_SetBool_Target_490;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_490.Out)
		{
			Relay_False_492();
		}
	}

	private void Relay_False_490()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_490.False(out logic_uScriptAct_SetBool_Target_490);
		local_CrazedPlayInterruptOnce_System_Boolean = logic_uScriptAct_SetBool_Target_490;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_490.Out)
		{
			Relay_False_492();
		}
	}

	private void Relay_In_491()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_491 = LeaderIntroStartTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_491.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_491);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_491.InRange)
		{
			Relay_True_483();
		}
	}

	private void Relay_True_492()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_492.True(out logic_uScriptAct_SetBool_Target_492);
		local_CrazedNPCInRange_System_Boolean = logic_uScriptAct_SetBool_Target_492;
	}

	private void Relay_False_492()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_492.False(out logic_uScriptAct_SetBool_Target_492);
		local_CrazedNPCInRange_System_Boolean = logic_uScriptAct_SetBool_Target_492;
	}

	private void Relay_True_493()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_493.True(out logic_uScriptAct_SetBool_Target_493);
		local_HasBeenInterrupted_System_Boolean = logic_uScriptAct_SetBool_Target_493;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_493.Out)
		{
			Relay_True_490();
		}
	}

	private void Relay_False_493()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_493.False(out logic_uScriptAct_SetBool_Target_493);
		local_HasBeenInterrupted_System_Boolean = logic_uScriptAct_SetBool_Target_493;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_493.Out)
		{
			Relay_True_490();
		}
	}

	private void Relay_In_498()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_498 = CrazedMsgTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_498.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_498, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_498);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_498.Out)
		{
			Relay_In_488();
		}
	}

	private void Relay_In_499()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_499 = LeaderOutOfRangeTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_499.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_499);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_499.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_499.OutOfRange;
		if (inRange)
		{
			Relay_In_769();
		}
		if (outOfRange)
		{
			Relay_In_500();
		}
	}

	private void Relay_In_500()
	{
		logic_uScriptCon_CompareBool_Bool_500 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_500.In(logic_uScriptCon_CompareBool_Bool_500);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_500.False)
		{
			Relay_In_498();
		}
	}

	private void Relay_In_502()
	{
		logic_uScriptCon_CompareBool_Bool_502 = local_CrazedNPCInRange_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_502.In(logic_uScriptCon_CompareBool_Bool_502);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_502.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_502.False;
		if (num)
		{
			Relay_In_786();
		}
		if (flag)
		{
			Relay_In_491();
		}
	}

	private void Relay_In_504()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_504 = LeaderOutOfRangeTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_504.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_504);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_504.InRange)
		{
			Relay_In_462();
		}
	}

	private void Relay_True_506()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_506.True(out logic_uScriptAct_SetBool_Target_506);
		local_CrazedPlayInterruptOnce_System_Boolean = logic_uScriptAct_SetBool_Target_506;
	}

	private void Relay_False_506()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_506.False(out logic_uScriptAct_SetBool_Target_506);
		local_CrazedPlayInterruptOnce_System_Boolean = logic_uScriptAct_SetBool_Target_506;
	}

	private void Relay_In_509()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_509 = LeaderIntroStartTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_509.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_509);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_509.InRange)
		{
			Relay_In_514();
		}
	}

	private void Relay_In_511()
	{
		logic_uScriptCon_CompareBool_Bool_511 = local_PlayedTryAgainMsg_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_511.In(logic_uScriptCon_CompareBool_Bool_511);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_511.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_511.False;
		if (num)
		{
			Relay_In_129();
		}
		if (flag)
		{
			Relay_In_509();
		}
	}

	private void Relay_In_514()
	{
		logic_uScript_AddMessage_messageData_514 = MsgLeaderTryAgain01;
		logic_uScript_AddMessage_speaker_514 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_514 = logic_uScript_AddMessage_uScript_AddMessage_514.In(logic_uScript_AddMessage_messageData_514, logic_uScript_AddMessage_speaker_514);
		if (logic_uScript_AddMessage_uScript_AddMessage_514.Out)
		{
			Relay_In_559();
		}
	}

	private void Relay_True_515()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_515.True(out logic_uScriptAct_SetBool_Target_515);
		local_PlayedTryAgainMsg_System_Boolean = logic_uScriptAct_SetBool_Target_515;
	}

	private void Relay_False_515()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_515.False(out logic_uScriptAct_SetBool_Target_515);
		local_PlayedTryAgainMsg_System_Boolean = logic_uScriptAct_SetBool_Target_515;
	}

	private void Relay_In_518()
	{
		logic_uScript_AddMessage_messageData_518 = MsgOutOfTime;
		logic_uScript_AddMessage_speaker_518 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_518 = logic_uScript_AddMessage_uScript_AddMessage_518.In(logic_uScript_AddMessage_messageData_518, logic_uScript_AddMessage_speaker_518);
		if (logic_uScript_AddMessage_uScript_AddMessage_518.Out)
		{
			Relay_False_546();
		}
	}

	private void Relay_In_521()
	{
		logic_uScript_AddMessage_messageData_521 = MsgLeftAreaCompletely;
		logic_uScript_AddMessage_speaker_521 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_521 = logic_uScript_AddMessage_uScript_AddMessage_521.In(logic_uScript_AddMessage_messageData_521, logic_uScript_AddMessage_speaker_521);
		if (logic_uScript_AddMessage_uScript_AddMessage_521.Out)
		{
			Relay_True_525();
		}
	}

	private void Relay_True_522()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_522.True(out logic_uScriptAct_SetBool_Target_522);
		local_PlayerLeftMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_522;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_522.Out)
		{
			Relay_In_532();
		}
	}

	private void Relay_False_522()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_522.False(out logic_uScriptAct_SetBool_Target_522);
		local_PlayerLeftMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_522;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_522.Out)
		{
			Relay_In_532();
		}
	}

	private void Relay_True_523()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_523.True(out logic_uScriptAct_SetBool_Target_523);
		local_PlayerLeftMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_523;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_523.Out)
		{
			Relay_In_532();
		}
	}

	private void Relay_False_523()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_523.False(out logic_uScriptAct_SetBool_Target_523);
		local_PlayerLeftMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_523;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_523.Out)
		{
			Relay_In_532();
		}
	}

	private void Relay_True_525()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_525.True(out logic_uScriptAct_SetBool_Target_525);
		local_HasPlayerLeftMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_525;
	}

	private void Relay_False_525()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_525.False(out logic_uScriptAct_SetBool_Target_525);
		local_HasPlayerLeftMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_525;
	}

	private void Relay_In_526()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_526 = MissionArea;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_526.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_526);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_526.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_526.OutOfRange;
		if (inRange)
		{
			Relay_False_523();
		}
		if (outOfRange)
		{
			Relay_True_522();
		}
	}

	private void Relay_In_530()
	{
		logic_uScriptCon_CompareBool_Bool_530 = local_HasPlayerLeftMissionArea_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_530.In(logic_uScriptCon_CompareBool_Bool_530);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_530.False)
		{
			Relay_In_521();
		}
	}

	private void Relay_In_532()
	{
		logic_uScriptCon_CompareBool_Bool_532 = local_PlayerLeftMissionArea_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_532.In(logic_uScriptCon_CompareBool_Bool_532);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_532.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_532.False;
		if (num)
		{
			Relay_In_530();
		}
		if (flag)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_539()
	{
		int num = 0;
		Array crazedLeaderTechData = CrazedLeaderTechData;
		if (logic_uScript_GetAndCheckTechs_techData_539.Length != num + crazedLeaderTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_539, num + crazedLeaderTechData.Length);
		}
		Array.Copy(crazedLeaderTechData, 0, logic_uScript_GetAndCheckTechs_techData_539, num, crazedLeaderTechData.Length);
		num += crazedLeaderTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_539 = owner_Connection_538;
		int num2 = 0;
		Array array = local_CrazedLeaderTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_539.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_539, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_539, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_539 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_539.In(logic_uScript_GetAndCheckTechs_techData_539, logic_uScript_GetAndCheckTechs_ownerNode_539, ref logic_uScript_GetAndCheckTechs_techs_539);
		local_CrazedLeaderTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_539;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_539.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_539.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_542();
		}
		if (someAlive)
		{
			Relay_AtIndex_542();
		}
	}

	private void Relay_In_541()
	{
		logic_uScript_SetTankInvulnerable_tank_541 = local_CrazedTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_541.In(logic_uScript_SetTankInvulnerable_invulnerable_541, logic_uScript_SetTankInvulnerable_tank_541);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_541.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_AtIndex_542()
	{
		int num = 0;
		Array array = local_CrazedLeaderTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_542.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_542, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_542, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_542.AtIndex(ref logic_uScript_AccessListTech_techList_542, logic_uScript_AccessListTech_index_542, out logic_uScript_AccessListTech_value_542);
		local_CrazedLeaderTechs_TankArray = logic_uScript_AccessListTech_techList_542;
		local_CrazedTech_Tank = logic_uScript_AccessListTech_value_542;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_542.Out)
		{
			Relay_In_541();
		}
	}

	private void Relay_InitialSpawn_544()
	{
		int num = 0;
		Array crazedLeaderTechData = CrazedLeaderTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_544.Length != num + crazedLeaderTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_544, num + crazedLeaderTechData.Length);
		}
		Array.Copy(crazedLeaderTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_544, num, crazedLeaderTechData.Length);
		num += crazedLeaderTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_544 = owner_Connection_543;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_544.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_544, logic_uScript_SpawnTechsFromData_ownerNode_544, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_544, logic_uScript_SpawnTechsFromData_allowResurrection_544);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_544.Out)
		{
			Relay_InitialSpawn_17();
		}
	}

	private void Relay_True_546()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_546.True(out logic_uScriptAct_SetBool_Target_546);
		local_OutOfTime_System_Boolean = logic_uScriptAct_SetBool_Target_546;
	}

	private void Relay_False_546()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_546.False(out logic_uScriptAct_SetBool_Target_546);
		local_OutOfTime_System_Boolean = logic_uScriptAct_SetBool_Target_546;
	}

	private void Relay_In_549()
	{
		int num = 0;
		Array crazedTechData = CrazedTechData;
		if (logic_uScript_GetAndCheckTechs_techData_549.Length != num + crazedTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_549, num + crazedTechData.Length);
		}
		Array.Copy(crazedTechData, 0, logic_uScript_GetAndCheckTechs_techData_549, num, crazedTechData.Length);
		num += crazedTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_549 = owner_Connection_548;
		int num2 = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_549.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_549, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_549, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_549 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_549.In(logic_uScript_GetAndCheckTechs_techData_549, logic_uScript_GetAndCheckTechs_ownerNode_549, ref logic_uScript_GetAndCheckTechs_techs_549);
		local_CrazedTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_549;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_549.AllDead)
		{
			Relay_In_445();
		}
	}

	private void Relay_True_552()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_552.True(out logic_uScriptAct_SetBool_Target_552);
		local_PlayedTryAgainMsg_System_Boolean = logic_uScriptAct_SetBool_Target_552;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_552.Out)
		{
			Relay_False_644();
		}
	}

	private void Relay_False_552()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_552.False(out logic_uScriptAct_SetBool_Target_552);
		local_PlayedTryAgainMsg_System_Boolean = logic_uScriptAct_SetBool_Target_552;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_552.Out)
		{
			Relay_False_644();
		}
	}

	private void Relay_In_556()
	{
		logic_uScriptAct_AddInt_v2_A_556 = local_CubeDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_556.In(logic_uScriptAct_AddInt_v2_A_556, logic_uScriptAct_AddInt_v2_B_556, out logic_uScriptAct_AddInt_v2_IntResult_556, out logic_uScriptAct_AddInt_v2_FloatResult_556);
		local_CubeDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_556;
	}

	private void Relay_In_557()
	{
		logic_uScript_AddMessage_messageData_557 = MsgCrazedMinionInterrupt04;
		logic_uScript_AddMessage_speaker_557 = GroupMinionTechSpeaker;
		logic_uScript_AddMessage_Return_557 = logic_uScript_AddMessage_uScript_AddMessage_557.In(logic_uScript_AddMessage_messageData_557, logic_uScript_AddMessage_speaker_557);
		if (logic_uScript_AddMessage_uScript_AddMessage_557.Shown)
		{
			Relay_In_556();
		}
	}

	private void Relay_In_559()
	{
		logic_uScript_SetEncounterTarget_owner_559 = owner_Connection_560;
		logic_uScript_SetEncounterTarget_visibleObject_559 = local_CubeTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_559.In(logic_uScript_SetEncounterTarget_owner_559, logic_uScript_SetEncounterTarget_visibleObject_559);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_559.Out)
		{
			Relay_True_515();
		}
	}

	private void Relay_In_563()
	{
		logic_uScript_SetEncounterTarget_owner_563 = owner_Connection_564;
		logic_uScript_SetEncounterTarget_visibleObject_563 = local_CrazedTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_563.In(logic_uScript_SetEncounterTarget_owner_563, logic_uScript_SetEncounterTarget_visibleObject_563);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_563.Out)
		{
			Relay_False_64();
		}
	}

	private void Relay_In_565()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_565.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_565, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_565, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_565.In(ref logic_uScript_SetTechsTeam_techs_565, logic_uScript_SetTechsTeam_team_565);
		local_CubeTechs_TankArray = logic_uScript_SetTechsTeam_techs_565;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_565.Out)
		{
			Relay_In_563();
		}
	}

	private void Relay_Save_Out_572()
	{
		Relay_Save_716();
	}

	private void Relay_Load_Out_572()
	{
		Relay_Load_716();
	}

	private void Relay_Restart_Out_572()
	{
		Relay_Set_False_716();
	}

	private void Relay_Save_572()
	{
		logic_SubGraph_SaveLoadBool_boolean_572 = local_CubeDestroyedFly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_572 = local_CubeDestroyedFly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Save(ref logic_SubGraph_SaveLoadBool_boolean_572, logic_SubGraph_SaveLoadBool_boolAsVariable_572, logic_SubGraph_SaveLoadBool_uniqueID_572);
	}

	private void Relay_Load_572()
	{
		logic_SubGraph_SaveLoadBool_boolean_572 = local_CubeDestroyedFly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_572 = local_CubeDestroyedFly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Load(ref logic_SubGraph_SaveLoadBool_boolean_572, logic_SubGraph_SaveLoadBool_boolAsVariable_572, logic_SubGraph_SaveLoadBool_uniqueID_572);
	}

	private void Relay_Set_True_572()
	{
		logic_SubGraph_SaveLoadBool_boolean_572 = local_CubeDestroyedFly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_572 = local_CubeDestroyedFly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_572, logic_SubGraph_SaveLoadBool_boolAsVariable_572, logic_SubGraph_SaveLoadBool_uniqueID_572);
	}

	private void Relay_Set_False_572()
	{
		logic_SubGraph_SaveLoadBool_boolean_572 = local_CubeDestroyedFly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_572 = local_CubeDestroyedFly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_572, logic_SubGraph_SaveLoadBool_boolAsVariable_572, logic_SubGraph_SaveLoadBool_uniqueID_572);
	}

	private void Relay_Save_Out_573()
	{
		Relay_Save_574();
	}

	private void Relay_Load_Out_573()
	{
		Relay_Load_574();
	}

	private void Relay_Restart_Out_573()
	{
		Relay_Set_False_574();
	}

	private void Relay_Save_573()
	{
		logic_SubGraph_SaveLoadBool_boolean_573 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_573 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Save(ref logic_SubGraph_SaveLoadBool_boolean_573, logic_SubGraph_SaveLoadBool_boolAsVariable_573, logic_SubGraph_SaveLoadBool_uniqueID_573);
	}

	private void Relay_Load_573()
	{
		logic_SubGraph_SaveLoadBool_boolean_573 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_573 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Load(ref logic_SubGraph_SaveLoadBool_boolean_573, logic_SubGraph_SaveLoadBool_boolAsVariable_573, logic_SubGraph_SaveLoadBool_uniqueID_573);
	}

	private void Relay_Set_True_573()
	{
		logic_SubGraph_SaveLoadBool_boolean_573 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_573 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_573, logic_SubGraph_SaveLoadBool_boolAsVariable_573, logic_SubGraph_SaveLoadBool_uniqueID_573);
	}

	private void Relay_Set_False_573()
	{
		logic_SubGraph_SaveLoadBool_boolean_573 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_573 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_573, logic_SubGraph_SaveLoadBool_boolAsVariable_573, logic_SubGraph_SaveLoadBool_uniqueID_573);
	}

	private void Relay_Save_Out_574()
	{
		Relay_Save_575();
	}

	private void Relay_Load_Out_574()
	{
		Relay_Load_575();
	}

	private void Relay_Restart_Out_574()
	{
		Relay_Set_False_575();
	}

	private void Relay_Save_574()
	{
		logic_SubGraph_SaveLoadBool_boolean_574 = local_PlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_574 = local_PlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Save(ref logic_SubGraph_SaveLoadBool_boolean_574, logic_SubGraph_SaveLoadBool_boolAsVariable_574, logic_SubGraph_SaveLoadBool_uniqueID_574);
	}

	private void Relay_Load_574()
	{
		logic_SubGraph_SaveLoadBool_boolean_574 = local_PlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_574 = local_PlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Load(ref logic_SubGraph_SaveLoadBool_boolean_574, logic_SubGraph_SaveLoadBool_boolAsVariable_574, logic_SubGraph_SaveLoadBool_uniqueID_574);
	}

	private void Relay_Set_True_574()
	{
		logic_SubGraph_SaveLoadBool_boolean_574 = local_PlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_574 = local_PlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_574, logic_SubGraph_SaveLoadBool_boolAsVariable_574, logic_SubGraph_SaveLoadBool_uniqueID_574);
	}

	private void Relay_Set_False_574()
	{
		logic_SubGraph_SaveLoadBool_boolean_574 = local_PlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_574 = local_PlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_574, logic_SubGraph_SaveLoadBool_boolAsVariable_574, logic_SubGraph_SaveLoadBool_uniqueID_574);
	}

	private void Relay_Save_Out_575()
	{
		Relay_Save_576();
	}

	private void Relay_Load_Out_575()
	{
		Relay_Load_576();
	}

	private void Relay_Restart_Out_575()
	{
		Relay_Set_False_576();
	}

	private void Relay_Save_575()
	{
		logic_SubGraph_SaveLoadBool_boolean_575 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_575 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Save(ref logic_SubGraph_SaveLoadBool_boolean_575, logic_SubGraph_SaveLoadBool_boolAsVariable_575, logic_SubGraph_SaveLoadBool_uniqueID_575);
	}

	private void Relay_Load_575()
	{
		logic_SubGraph_SaveLoadBool_boolean_575 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_575 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Load(ref logic_SubGraph_SaveLoadBool_boolean_575, logic_SubGraph_SaveLoadBool_boolAsVariable_575, logic_SubGraph_SaveLoadBool_uniqueID_575);
	}

	private void Relay_Set_True_575()
	{
		logic_SubGraph_SaveLoadBool_boolean_575 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_575 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_575, logic_SubGraph_SaveLoadBool_boolAsVariable_575, logic_SubGraph_SaveLoadBool_uniqueID_575);
	}

	private void Relay_Set_False_575()
	{
		logic_SubGraph_SaveLoadBool_boolean_575 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_575 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_575, logic_SubGraph_SaveLoadBool_boolAsVariable_575, logic_SubGraph_SaveLoadBool_uniqueID_575);
	}

	private void Relay_Save_Out_576()
	{
		Relay_Save_577();
	}

	private void Relay_Load_Out_576()
	{
		Relay_Load_577();
	}

	private void Relay_Restart_Out_576()
	{
		Relay_Set_False_577();
	}

	private void Relay_Save_576()
	{
		logic_SubGraph_SaveLoadBool_boolean_576 = local_PlayedTryAgainMsg_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_576 = local_PlayedTryAgainMsg_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Save(ref logic_SubGraph_SaveLoadBool_boolean_576, logic_SubGraph_SaveLoadBool_boolAsVariable_576, logic_SubGraph_SaveLoadBool_uniqueID_576);
	}

	private void Relay_Load_576()
	{
		logic_SubGraph_SaveLoadBool_boolean_576 = local_PlayedTryAgainMsg_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_576 = local_PlayedTryAgainMsg_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Load(ref logic_SubGraph_SaveLoadBool_boolean_576, logic_SubGraph_SaveLoadBool_boolAsVariable_576, logic_SubGraph_SaveLoadBool_uniqueID_576);
	}

	private void Relay_Set_True_576()
	{
		logic_SubGraph_SaveLoadBool_boolean_576 = local_PlayedTryAgainMsg_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_576 = local_PlayedTryAgainMsg_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_576, logic_SubGraph_SaveLoadBool_boolAsVariable_576, logic_SubGraph_SaveLoadBool_uniqueID_576);
	}

	private void Relay_Set_False_576()
	{
		logic_SubGraph_SaveLoadBool_boolean_576 = local_PlayedTryAgainMsg_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_576 = local_PlayedTryAgainMsg_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_576, logic_SubGraph_SaveLoadBool_boolAsVariable_576, logic_SubGraph_SaveLoadBool_uniqueID_576);
	}

	private void Relay_Save_Out_577()
	{
		Relay_Save_670();
	}

	private void Relay_Load_Out_577()
	{
		Relay_Load_670();
	}

	private void Relay_Restart_Out_577()
	{
		Relay_Set_False_670();
	}

	private void Relay_Save_577()
	{
		logic_SubGraph_SaveLoadBool_boolean_577 = local_FlyLeaderAway_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_577 = local_FlyLeaderAway_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Save(ref logic_SubGraph_SaveLoadBool_boolean_577, logic_SubGraph_SaveLoadBool_boolAsVariable_577, logic_SubGraph_SaveLoadBool_uniqueID_577);
	}

	private void Relay_Load_577()
	{
		logic_SubGraph_SaveLoadBool_boolean_577 = local_FlyLeaderAway_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_577 = local_FlyLeaderAway_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Load(ref logic_SubGraph_SaveLoadBool_boolean_577, logic_SubGraph_SaveLoadBool_boolAsVariable_577, logic_SubGraph_SaveLoadBool_uniqueID_577);
	}

	private void Relay_Set_True_577()
	{
		logic_SubGraph_SaveLoadBool_boolean_577 = local_FlyLeaderAway_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_577 = local_FlyLeaderAway_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_577, logic_SubGraph_SaveLoadBool_boolAsVariable_577, logic_SubGraph_SaveLoadBool_uniqueID_577);
	}

	private void Relay_Set_False_577()
	{
		logic_SubGraph_SaveLoadBool_boolean_577 = local_FlyLeaderAway_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_577 = local_FlyLeaderAway_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_577, logic_SubGraph_SaveLoadBool_boolAsVariable_577, logic_SubGraph_SaveLoadBool_uniqueID_577);
	}

	private void Relay_In_580()
	{
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_580 = local_CubeTech_Tank;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_580.In(logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_580, logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_580);
		if (logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_580.Out)
		{
			Relay_True_27();
		}
	}

	private void Relay_In_581()
	{
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_581 = local_CubeTech_Tank;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_581.In(logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_581, logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_581);
		if (logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_581.Out)
		{
			Relay_False_128();
		}
	}

	private void Relay_Save_Out_584()
	{
		Relay_Save_594();
	}

	private void Relay_Load_Out_584()
	{
		Relay_Load_594();
	}

	private void Relay_Restart_Out_584()
	{
		Relay_Set_False_594();
	}

	private void Relay_Save_584()
	{
		logic_SubGraph_SaveLoadBool_boolean_584 = local_CrazedAmbushNotTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_584 = local_CrazedAmbushNotTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.Save(ref logic_SubGraph_SaveLoadBool_boolean_584, logic_SubGraph_SaveLoadBool_boolAsVariable_584, logic_SubGraph_SaveLoadBool_uniqueID_584);
	}

	private void Relay_Load_584()
	{
		logic_SubGraph_SaveLoadBool_boolean_584 = local_CrazedAmbushNotTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_584 = local_CrazedAmbushNotTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.Load(ref logic_SubGraph_SaveLoadBool_boolean_584, logic_SubGraph_SaveLoadBool_boolAsVariable_584, logic_SubGraph_SaveLoadBool_uniqueID_584);
	}

	private void Relay_Set_True_584()
	{
		logic_SubGraph_SaveLoadBool_boolean_584 = local_CrazedAmbushNotTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_584 = local_CrazedAmbushNotTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_584, logic_SubGraph_SaveLoadBool_boolAsVariable_584, logic_SubGraph_SaveLoadBool_uniqueID_584);
	}

	private void Relay_Set_False_584()
	{
		logic_SubGraph_SaveLoadBool_boolean_584 = local_CrazedAmbushNotTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_584 = local_CrazedAmbushNotTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_584.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_584, logic_SubGraph_SaveLoadBool_boolAsVariable_584, logic_SubGraph_SaveLoadBool_uniqueID_584);
	}

	private void Relay_True_585()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_585.True(out logic_uScriptAct_SetBool_Target_585);
		local_FinalObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_585;
	}

	private void Relay_False_585()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_585.False(out logic_uScriptAct_SetBool_Target_585);
		local_FinalObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_585;
	}

	private void Relay_In_590()
	{
		logic_uScript_FlyTechUpAndAway_tech_590 = local_CrazedTech_Tank;
		logic_uScript_FlyTechUpAndAway_removalParticles_590 = TechFlyParticles;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_590.In(logic_uScript_FlyTechUpAndAway_tech_590, logic_uScript_FlyTechUpAndAway_maxLifetime_590, logic_uScript_FlyTechUpAndAway_targetHeight_590, logic_uScript_FlyTechUpAndAway_aiTree_590, logic_uScript_FlyTechUpAndAway_removalParticles_590);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_590.Out)
		{
			Relay_In_459();
		}
	}

	private void Relay_Save_Out_593()
	{
	}

	private void Relay_Load_Out_593()
	{
		Relay_In_754();
	}

	private void Relay_Restart_Out_593()
	{
		Relay_False_729();
	}

	private void Relay_Save_593()
	{
		logic_SubGraph_SaveLoadBool_boolean_593 = local_FightRunning_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_593 = local_FightRunning_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Save(ref logic_SubGraph_SaveLoadBool_boolean_593, logic_SubGraph_SaveLoadBool_boolAsVariable_593, logic_SubGraph_SaveLoadBool_uniqueID_593);
	}

	private void Relay_Load_593()
	{
		logic_SubGraph_SaveLoadBool_boolean_593 = local_FightRunning_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_593 = local_FightRunning_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Load(ref logic_SubGraph_SaveLoadBool_boolean_593, logic_SubGraph_SaveLoadBool_boolAsVariable_593, logic_SubGraph_SaveLoadBool_uniqueID_593);
	}

	private void Relay_Set_True_593()
	{
		logic_SubGraph_SaveLoadBool_boolean_593 = local_FightRunning_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_593 = local_FightRunning_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_593, logic_SubGraph_SaveLoadBool_boolAsVariable_593, logic_SubGraph_SaveLoadBool_uniqueID_593);
	}

	private void Relay_Set_False_593()
	{
		logic_SubGraph_SaveLoadBool_boolean_593 = local_FightRunning_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_593 = local_FightRunning_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_593, logic_SubGraph_SaveLoadBool_boolAsVariable_593, logic_SubGraph_SaveLoadBool_uniqueID_593);
	}

	private void Relay_Save_Out_594()
	{
		Relay_Save_593();
	}

	private void Relay_Load_Out_594()
	{
		Relay_Load_593();
	}

	private void Relay_Restart_Out_594()
	{
		Relay_Set_False_593();
	}

	private void Relay_Save_594()
	{
		logic_SubGraph_SaveLoadBool_boolean_594 = local_FightStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_594 = local_FightStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.Save(ref logic_SubGraph_SaveLoadBool_boolean_594, logic_SubGraph_SaveLoadBool_boolAsVariable_594, logic_SubGraph_SaveLoadBool_uniqueID_594);
	}

	private void Relay_Load_594()
	{
		logic_SubGraph_SaveLoadBool_boolean_594 = local_FightStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_594 = local_FightStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.Load(ref logic_SubGraph_SaveLoadBool_boolean_594, logic_SubGraph_SaveLoadBool_boolAsVariable_594, logic_SubGraph_SaveLoadBool_uniqueID_594);
	}

	private void Relay_Set_True_594()
	{
		logic_SubGraph_SaveLoadBool_boolean_594 = local_FightStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_594 = local_FightStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_594, logic_SubGraph_SaveLoadBool_boolAsVariable_594, logic_SubGraph_SaveLoadBool_uniqueID_594);
	}

	private void Relay_Set_False_594()
	{
		logic_SubGraph_SaveLoadBool_boolean_594 = local_FightStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_594 = local_FightStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_594.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_594, logic_SubGraph_SaveLoadBool_boolAsVariable_594, logic_SubGraph_SaveLoadBool_uniqueID_594);
	}

	private void Relay_In_596()
	{
		logic_uScriptCon_CompareBool_Bool_596 = local_DestroyCube_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_596.In(logic_uScriptCon_CompareBool_Bool_596);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_596.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_596.False;
		if (num)
		{
			Relay_In_648();
		}
		if (flag)
		{
			Relay_In_812();
		}
	}

	private void Relay_True_598()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_598.True(out logic_uScriptAct_SetBool_Target_598);
		local_DestroyCube_System_Boolean = logic_uScriptAct_SetBool_Target_598;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_598.Out)
		{
			Relay_False_650();
		}
	}

	private void Relay_False_598()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_598.False(out logic_uScriptAct_SetBool_Target_598);
		local_DestroyCube_System_Boolean = logic_uScriptAct_SetBool_Target_598;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_598.Out)
		{
			Relay_False_650();
		}
	}

	private void Relay_Save_Out_601()
	{
		Relay_Save_584();
	}

	private void Relay_Load_Out_601()
	{
		Relay_Load_584();
	}

	private void Relay_Restart_Out_601()
	{
		Relay_Set_False_584();
	}

	private void Relay_Save_601()
	{
		logic_SubGraph_SaveLoadBool_boolean_601 = local_DestroyCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_601 = local_DestroyCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.Save(ref logic_SubGraph_SaveLoadBool_boolean_601, logic_SubGraph_SaveLoadBool_boolAsVariable_601, logic_SubGraph_SaveLoadBool_uniqueID_601);
	}

	private void Relay_Load_601()
	{
		logic_SubGraph_SaveLoadBool_boolean_601 = local_DestroyCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_601 = local_DestroyCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.Load(ref logic_SubGraph_SaveLoadBool_boolean_601, logic_SubGraph_SaveLoadBool_boolAsVariable_601, logic_SubGraph_SaveLoadBool_uniqueID_601);
	}

	private void Relay_Set_True_601()
	{
		logic_SubGraph_SaveLoadBool_boolean_601 = local_DestroyCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_601 = local_DestroyCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_601, logic_SubGraph_SaveLoadBool_boolAsVariable_601, logic_SubGraph_SaveLoadBool_uniqueID_601);
	}

	private void Relay_Set_False_601()
	{
		logic_SubGraph_SaveLoadBool_boolean_601 = local_DestroyCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_601 = local_DestroyCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_601.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_601, logic_SubGraph_SaveLoadBool_boolAsVariable_601, logic_SubGraph_SaveLoadBool_uniqueID_601);
	}

	private void Relay_AtIndex_602()
	{
		int num = 0;
		Array array = local_CrazedLeaderTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_602.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_602, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_602, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_602.AtIndex(ref logic_uScript_AccessListTech_techList_602, logic_uScript_AccessListTech_index_602, out logic_uScript_AccessListTech_value_602);
		local_CrazedLeaderTechs_TankArray = logic_uScript_AccessListTech_techList_602;
		local_CrazedTech_Tank = logic_uScript_AccessListTech_value_602;
	}

	private void Relay_In_604()
	{
		int num = 0;
		Array crazedLeaderTechData = CrazedLeaderTechData;
		if (logic_uScript_GetAndCheckTechs_techData_604.Length != num + crazedLeaderTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_604, num + crazedLeaderTechData.Length);
		}
		Array.Copy(crazedLeaderTechData, 0, logic_uScript_GetAndCheckTechs_techData_604, num, crazedLeaderTechData.Length);
		num += crazedLeaderTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_604 = owner_Connection_605;
		int num2 = 0;
		Array array = local_CrazedLeaderTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_604.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_604, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_604, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_604 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_604.In(logic_uScript_GetAndCheckTechs_techData_604, logic_uScript_GetAndCheckTechs_ownerNode_604, ref logic_uScript_GetAndCheckTechs_techs_604);
		local_CrazedLeaderTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_604;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_604.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_604.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_604.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_604.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_850();
		}
		if (someAlive)
		{
			Relay_True_850();
		}
		if (allDead)
		{
			Relay_False_850();
		}
		if (waitingToSpawn)
		{
			Relay_False_850();
		}
	}

	private void Relay_In_610()
	{
		int num = 0;
		Array crazedTechData = CrazedTechData;
		if (logic_uScript_GetAndCheckTechs_techData_610.Length != num + crazedTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_610, num + crazedTechData.Length);
		}
		Array.Copy(crazedTechData, 0, logic_uScript_GetAndCheckTechs_techData_610, num, crazedTechData.Length);
		num += crazedTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_610 = owner_Connection_609;
		int num2 = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_610.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_610, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_610, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_610 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_610.In(logic_uScript_GetAndCheckTechs_techData_610, logic_uScript_GetAndCheckTechs_ownerNode_610, ref logic_uScript_GetAndCheckTechs_techs_610);
		local_CrazedTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_610;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_610.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_610.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_610.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_610.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_852();
		}
		if (someAlive)
		{
			Relay_True_852();
		}
		if (allDead)
		{
			Relay_False_852();
		}
		if (waitingToSpawn)
		{
			Relay_False_852();
		}
	}

	private void Relay_In_613()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_613.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_613, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_613, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_613 = owner_Connection_612;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_613.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_613, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_613, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_613 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_613.In(logic_uScript_GetAndCheckTechs_techData_613, logic_uScript_GetAndCheckTechs_ownerNode_613, ref logic_uScript_GetAndCheckTechs_techs_613);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_613;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_613.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_613.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_613.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_613.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_849();
		}
		if (someAlive)
		{
			Relay_True_849();
		}
		if (allDead)
		{
			Relay_False_849();
		}
		if (waitingToSpawn)
		{
			Relay_False_849();
		}
	}

	private void Relay_AtIndex_615()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_615.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_615, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_615, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_615.AtIndex(ref logic_uScript_AccessListTech_techList_615, logic_uScript_AccessListTech_index_615, out logic_uScript_AccessListTech_value_615);
		local_CubeTechs_TankArray = logic_uScript_AccessListTech_techList_615;
		local_CubeTech_Tank = logic_uScript_AccessListTech_value_615;
	}

	private void Relay_In_617()
	{
		int num = 0;
		Array fillerTechData = FillerTechData;
		if (logic_uScript_GetAndCheckTechs_techData_617.Length != num + fillerTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_617, num + fillerTechData.Length);
		}
		Array.Copy(fillerTechData, 0, logic_uScript_GetAndCheckTechs_techData_617, num, fillerTechData.Length);
		num += fillerTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_617 = owner_Connection_618;
		int num2 = 0;
		Array array = local_FillerTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_617.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_617, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_617, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_617 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_617.In(logic_uScript_GetAndCheckTechs_techData_617, logic_uScript_GetAndCheckTechs_ownerNode_617, ref logic_uScript_GetAndCheckTechs_techs_617);
		local_FillerTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_617;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_617.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_617.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_617.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_617.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_854();
		}
		if (someAlive)
		{
			Relay_True_854();
		}
		if (allDead)
		{
			Relay_False_854();
		}
		if (waitingToSpawn)
		{
			Relay_False_854();
		}
	}

	private void Relay_In_621()
	{
		int num = 0;
		Array enemyMinionWaveData = EnemyMinionWaveData;
		if (logic_uScript_GetAndCheckTechs_techData_621.Length != num + enemyMinionWaveData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_621, num + enemyMinionWaveData.Length);
		}
		Array.Copy(enemyMinionWaveData, 0, logic_uScript_GetAndCheckTechs_techData_621, num, enemyMinionWaveData.Length);
		num += enemyMinionWaveData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_621 = owner_Connection_620;
		int num2 = 0;
		Array array = local_EnemyMinionWaveTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_621.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_621, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_621, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_621 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_621.In(logic_uScript_GetAndCheckTechs_techData_621, logic_uScript_GetAndCheckTechs_ownerNode_621, ref logic_uScript_GetAndCheckTechs_techs_621);
		local_EnemyMinionWaveTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_621;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_621.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_621.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_621.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_621.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_818();
		}
		if (someAlive)
		{
			Relay_True_818();
		}
		if (allDead)
		{
			Relay_False_818();
		}
		if (waitingToSpawn)
		{
			Relay_False_818();
		}
	}

	private void Relay_Save_Out_625()
	{
		Relay_Save_626();
	}

	private void Relay_Load_Out_625()
	{
		Relay_Load_626();
	}

	private void Relay_Restart_Out_625()
	{
		Relay_Set_False_626();
	}

	private void Relay_Save_625()
	{
		logic_SubGraph_SaveLoadBool_boolean_625 = local_MinionWaveTechsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_625 = local_MinionWaveTechsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.Save(ref logic_SubGraph_SaveLoadBool_boolean_625, logic_SubGraph_SaveLoadBool_boolAsVariable_625, logic_SubGraph_SaveLoadBool_uniqueID_625);
	}

	private void Relay_Load_625()
	{
		logic_SubGraph_SaveLoadBool_boolean_625 = local_MinionWaveTechsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_625 = local_MinionWaveTechsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.Load(ref logic_SubGraph_SaveLoadBool_boolean_625, logic_SubGraph_SaveLoadBool_boolAsVariable_625, logic_SubGraph_SaveLoadBool_uniqueID_625);
	}

	private void Relay_Set_True_625()
	{
		logic_SubGraph_SaveLoadBool_boolean_625 = local_MinionWaveTechsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_625 = local_MinionWaveTechsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_625, logic_SubGraph_SaveLoadBool_boolAsVariable_625, logic_SubGraph_SaveLoadBool_uniqueID_625);
	}

	private void Relay_Set_False_625()
	{
		logic_SubGraph_SaveLoadBool_boolean_625 = local_MinionWaveTechsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_625 = local_MinionWaveTechsDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_625.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_625, logic_SubGraph_SaveLoadBool_boolAsVariable_625, logic_SubGraph_SaveLoadBool_uniqueID_625);
	}

	private void Relay_Save_Out_626()
	{
		Relay_Save_219();
	}

	private void Relay_Load_Out_626()
	{
		Relay_Load_219();
	}

	private void Relay_Restart_Out_626()
	{
		Relay_Set_False_219();
	}

	private void Relay_Save_626()
	{
		logic_SubGraph_SaveLoadBool_boolean_626 = local_CrazedTechsAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_626 = local_CrazedTechsAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.Save(ref logic_SubGraph_SaveLoadBool_boolean_626, logic_SubGraph_SaveLoadBool_boolAsVariable_626, logic_SubGraph_SaveLoadBool_uniqueID_626);
	}

	private void Relay_Load_626()
	{
		logic_SubGraph_SaveLoadBool_boolean_626 = local_CrazedTechsAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_626 = local_CrazedTechsAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.Load(ref logic_SubGraph_SaveLoadBool_boolean_626, logic_SubGraph_SaveLoadBool_boolAsVariable_626, logic_SubGraph_SaveLoadBool_uniqueID_626);
	}

	private void Relay_Set_True_626()
	{
		logic_SubGraph_SaveLoadBool_boolean_626 = local_CrazedTechsAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_626 = local_CrazedTechsAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_626, logic_SubGraph_SaveLoadBool_boolAsVariable_626, logic_SubGraph_SaveLoadBool_uniqueID_626);
	}

	private void Relay_Set_False_626()
	{
		logic_SubGraph_SaveLoadBool_boolean_626 = local_CrazedTechsAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_626 = local_CrazedTechsAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_626.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_626, logic_SubGraph_SaveLoadBool_boolAsVariable_626, logic_SubGraph_SaveLoadBool_uniqueID_626);
	}

	private void Relay_In_627()
	{
		logic_uScript_SetTankInvulnerable_tank_627 = local_CubeTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_627.In(logic_uScript_SetTankInvulnerable_invulnerable_627, logic_uScript_SetTankInvulnerable_tank_627);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_627.Out)
		{
			Relay_In_809();
		}
	}

	private void Relay_AtIndex_628()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_628.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_628, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_628, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_628.AtIndex(ref logic_uScript_AccessListTech_techList_628, logic_uScript_AccessListTech_index_628, out logic_uScript_AccessListTech_value_628);
		local_CrazedTechs_TankArray = logic_uScript_AccessListTech_techList_628;
		local_CrazedTech3_Tank = logic_uScript_AccessListTech_value_628;
	}

	private void Relay_AtIndex_629()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_629.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_629, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_629, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_629.AtIndex(ref logic_uScript_AccessListTech_techList_629, logic_uScript_AccessListTech_index_629, out logic_uScript_AccessListTech_value_629);
		local_CrazedTechs_TankArray = logic_uScript_AccessListTech_techList_629;
		local_CrazedTech2_Tank = logic_uScript_AccessListTech_value_629;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_629.Out)
		{
			Relay_AtIndex_628();
		}
	}

	private void Relay_AtIndex_635()
	{
		int num = 0;
		Array array = local_FillerTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_635.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_635, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_635, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_635.AtIndex(ref logic_uScript_AccessListTech_techList_635, logic_uScript_AccessListTech_index_635, out logic_uScript_AccessListTech_value_635);
		local_FillerTechs_TankArray = logic_uScript_AccessListTech_techList_635;
		local_FillerTech01_Tank = logic_uScript_AccessListTech_value_635;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_635.Out)
		{
			Relay_AtIndex_637();
		}
	}

	private void Relay_AtIndex_637()
	{
		int num = 0;
		Array array = local_FillerTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_637.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_637, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_637, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_637.AtIndex(ref logic_uScript_AccessListTech_techList_637, logic_uScript_AccessListTech_index_637, out logic_uScript_AccessListTech_value_637);
		local_FillerTechs_TankArray = logic_uScript_AccessListTech_techList_637;
		local_FillerTech02_Tank = logic_uScript_AccessListTech_value_637;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_637.Out)
		{
			Relay_AtIndex_642();
		}
	}

	private void Relay_AtIndex_642()
	{
		int num = 0;
		Array array = local_FillerTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_642.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_642, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_642, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_642.AtIndex(ref logic_uScript_AccessListTech_techList_642, logic_uScript_AccessListTech_index_642, out logic_uScript_AccessListTech_value_642);
		local_FillerTechs_TankArray = logic_uScript_AccessListTech_techList_642;
		local_FillerTech03_Tank = logic_uScript_AccessListTech_value_642;
	}

	private void Relay_In_643()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_643.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_643.Out)
		{
			Relay_In_539();
		}
	}

	private void Relay_True_644()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_644.True(out logic_uScriptAct_SetBool_Target_644);
		local_HasPlayerLeftMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_644;
	}

	private void Relay_False_644()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_644.False(out logic_uScriptAct_SetBool_Target_644);
		local_HasPlayerLeftMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_644;
	}

	private void Relay_In_646()
	{
		logic_uScript_Wait_uScript_Wait_646.In(logic_uScript_Wait_seconds_646, logic_uScript_Wait_repeat_646);
		if (logic_uScript_Wait_uScript_Wait_646.Waited)
		{
			Relay_In_676();
		}
	}

	private void Relay_In_648()
	{
		logic_uScriptCon_CompareBool_Bool_648 = local_WaitingForCube_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_648.In(logic_uScriptCon_CompareBool_Bool_648);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_648.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_648.False;
		if (num)
		{
			Relay_In_140();
		}
		if (flag)
		{
			Relay_In_646();
		}
	}

	private void Relay_True_650()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_650.True(out logic_uScriptAct_SetBool_Target_650);
		local_WaitingForCube_System_Boolean = logic_uScriptAct_SetBool_Target_650;
	}

	private void Relay_False_650()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_650.False(out logic_uScriptAct_SetBool_Target_650);
		local_WaitingForCube_System_Boolean = logic_uScriptAct_SetBool_Target_650;
	}

	private void Relay_True_651()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_651.True(out logic_uScriptAct_SetBool_Target_651);
		local_WaitingForCube_System_Boolean = logic_uScriptAct_SetBool_Target_651;
	}

	private void Relay_False_651()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_651.False(out logic_uScriptAct_SetBool_Target_651);
		local_WaitingForCube_System_Boolean = logic_uScriptAct_SetBool_Target_651;
	}

	private void Relay_In_659()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_659.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_659, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_659, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_659.In(ref logic_uScript_SetTechsTeam_techs_659, logic_uScript_SetTechsTeam_team_659);
		local_CubeTechs_TankArray = logic_uScript_SetTechsTeam_techs_659;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_659.Out)
		{
			Relay_True_665();
		}
	}

	private void Relay_In_660()
	{
		logic_uScriptCon_CompareBool_Bool_660 = local_TechInvulOnLoad_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_660.In(logic_uScriptCon_CompareBool_Bool_660);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_660.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_660.False;
		if (num)
		{
			Relay_In_819();
		}
		if (flag)
		{
			Relay_In_709();
		}
	}

	private void Relay_AtIndex_663()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_663.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_663, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_663, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_663.AtIndex(ref logic_uScript_AccessListTech_techList_663, logic_uScript_AccessListTech_index_663, out logic_uScript_AccessListTech_value_663);
		local_CubeTechs_TankArray = logic_uScript_AccessListTech_techList_663;
		local_CubeTech_Tank = logic_uScript_AccessListTech_value_663;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_663.Out)
		{
			Relay_In_666();
		}
	}

	private void Relay_True_665()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_665.True(out logic_uScriptAct_SetBool_Target_665);
		local_TechInvulOnLoad_System_Boolean = logic_uScriptAct_SetBool_Target_665;
	}

	private void Relay_False_665()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_665.False(out logic_uScriptAct_SetBool_Target_665);
		local_TechInvulOnLoad_System_Boolean = logic_uScriptAct_SetBool_Target_665;
	}

	private void Relay_In_666()
	{
		logic_uScript_SetTankInvulnerable_tank_666 = local_CubeTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_666.In(logic_uScript_SetTankInvulnerable_invulnerable_666, logic_uScript_SetTankInvulnerable_tank_666);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_666.Out)
		{
			Relay_In_659();
		}
	}

	private void Relay_In_667()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_667.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_667, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_667, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_667 = owner_Connection_662;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_667.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_667, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_667, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_667 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_667.In(logic_uScript_GetAndCheckTechs_techData_667, logic_uScript_GetAndCheckTechs_ownerNode_667, ref logic_uScript_GetAndCheckTechs_techs_667);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_667;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_667.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_667.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_667.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_667.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_663();
		}
		if (someAlive)
		{
			Relay_AtIndex_663();
		}
		if (allDead)
		{
			Relay_In_819();
		}
		if (waitingToSpawn)
		{
			Relay_In_819();
		}
	}

	private void Relay_Save_Out_670()
	{
		Relay_Save_601();
	}

	private void Relay_Load_Out_670()
	{
		Relay_Load_601();
	}

	private void Relay_Restart_Out_670()
	{
		Relay_Set_False_601();
	}

	private void Relay_Save_670()
	{
		logic_SubGraph_SaveLoadBool_boolean_670 = local_TechInvulOnLoad_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_670 = local_TechInvulOnLoad_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Save(ref logic_SubGraph_SaveLoadBool_boolean_670, logic_SubGraph_SaveLoadBool_boolAsVariable_670, logic_SubGraph_SaveLoadBool_uniqueID_670);
	}

	private void Relay_Load_670()
	{
		logic_SubGraph_SaveLoadBool_boolean_670 = local_TechInvulOnLoad_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_670 = local_TechInvulOnLoad_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Load(ref logic_SubGraph_SaveLoadBool_boolean_670, logic_SubGraph_SaveLoadBool_boolAsVariable_670, logic_SubGraph_SaveLoadBool_uniqueID_670);
	}

	private void Relay_Set_True_670()
	{
		logic_SubGraph_SaveLoadBool_boolean_670 = local_TechInvulOnLoad_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_670 = local_TechInvulOnLoad_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_670, logic_SubGraph_SaveLoadBool_boolAsVariable_670, logic_SubGraph_SaveLoadBool_uniqueID_670);
	}

	private void Relay_Set_False_670()
	{
		logic_SubGraph_SaveLoadBool_boolean_670 = local_TechInvulOnLoad_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_670 = local_TechInvulOnLoad_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_670.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_670, logic_SubGraph_SaveLoadBool_boolAsVariable_670, logic_SubGraph_SaveLoadBool_uniqueID_670);
	}

	private void Relay_In_673()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_673.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_673, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_673, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_673 = owner_Connection_674;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_673.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_673, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_673, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_673 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_673.In(logic_uScript_GetAndCheckTechs_techData_673, logic_uScript_GetAndCheckTechs_ownerNode_673, ref logic_uScript_GetAndCheckTechs_techs_673);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_673;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_673.AllDead)
		{
			Relay_True_598();
		}
	}

	private void Relay_In_676()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_676.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_676, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_676, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_676 = owner_Connection_678;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_676.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_676, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_676, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_676 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_676.In(logic_uScript_GetAndCheckTechs_techData_676, logic_uScript_GetAndCheckTechs_ownerNode_676, ref logic_uScript_GetAndCheckTechs_techs_676);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_676;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_676.AllDead)
		{
			Relay_InitialSpawn_101();
		}
	}

	private void Relay_In_679()
	{
		logic_uScript_StartMissionTimer_owner_679 = owner_Connection_682;
		logic_uScript_StartMissionTimer_startTime_679 = local_683_System_Single;
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_679.In(logic_uScript_StartMissionTimer_owner_679, logic_uScript_StartMissionTimer_startTime_679);
		if (logic_uScript_StartMissionTimer_uScript_StartMissionTimer_679.Out)
		{
			Relay_In_686();
		}
	}

	private void Relay_In_684()
	{
		logic_uScript_HideMissionTimerUI_owner_684 = owner_Connection_857;
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_684.In(logic_uScript_HideMissionTimerUI_owner_684);
		if (logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_684.Out)
		{
			Relay_In_734();
		}
	}

	private void Relay_In_685()
	{
		logic_uScript_ResetMissionTimer_owner_685 = owner_Connection_680;
		logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_685.In(logic_uScript_ResetMissionTimer_owner_685);
		if (logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_685.Out)
		{
			Relay_In_689();
		}
	}

	private void Relay_In_686()
	{
		logic_uScript_StopMissionTimer_owner_686 = owner_Connection_688;
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_686.In(logic_uScript_StopMissionTimer_owner_686);
		if (logic_uScript_StopMissionTimer_uScript_StopMissionTimer_686.Out)
		{
			Relay_In_685();
		}
	}

	private void Relay_In_689()
	{
		logic_uScript_ResetMissionTimerTimeElapsed_owner_689 = owner_Connection_681;
		logic_uScript_ResetMissionTimerTimeElapsed_startTime_689 = local_687_System_Single;
		logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_689.In(logic_uScript_ResetMissionTimerTimeElapsed_owner_689, logic_uScript_ResetMissionTimerTimeElapsed_startTime_689);
		if (logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_689.Out)
		{
			Relay_In_684();
		}
	}

	private void Relay_In_690()
	{
		logic_uScriptCon_CompareBool_Bool_690 = local_TurnEnemiesAfterCubeDeath_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_690.In(logic_uScriptCon_CompareBool_Bool_690);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_690.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_690.False;
		if (num)
		{
			Relay_In_354();
		}
		if (flag)
		{
			Relay_In_403();
		}
	}

	private void Relay_True_695()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_695.True(out logic_uScriptAct_SetBool_Target_695);
		local_TurnEnemiesAfterCubeDeath_System_Boolean = logic_uScriptAct_SetBool_Target_695;
	}

	private void Relay_False_695()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_695.False(out logic_uScriptAct_SetBool_Target_695);
		local_TurnEnemiesAfterCubeDeath_System_Boolean = logic_uScriptAct_SetBool_Target_695;
	}

	private void Relay_Save_Out_696()
	{
		Relay_Save_572();
	}

	private void Relay_Load_Out_696()
	{
		Relay_Load_572();
	}

	private void Relay_Restart_Out_696()
	{
		Relay_Set_False_572();
	}

	private void Relay_Save_696()
	{
		logic_SubGraph_SaveLoadBool_boolean_696 = local_TurnEnemiesAfterCubeDeath_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_696 = local_TurnEnemiesAfterCubeDeath_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.Save(ref logic_SubGraph_SaveLoadBool_boolean_696, logic_SubGraph_SaveLoadBool_boolAsVariable_696, logic_SubGraph_SaveLoadBool_uniqueID_696);
	}

	private void Relay_Load_696()
	{
		logic_SubGraph_SaveLoadBool_boolean_696 = local_TurnEnemiesAfterCubeDeath_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_696 = local_TurnEnemiesAfterCubeDeath_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.Load(ref logic_SubGraph_SaveLoadBool_boolean_696, logic_SubGraph_SaveLoadBool_boolAsVariable_696, logic_SubGraph_SaveLoadBool_uniqueID_696);
	}

	private void Relay_Set_True_696()
	{
		logic_SubGraph_SaveLoadBool_boolean_696 = local_TurnEnemiesAfterCubeDeath_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_696 = local_TurnEnemiesAfterCubeDeath_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_696, logic_SubGraph_SaveLoadBool_boolAsVariable_696, logic_SubGraph_SaveLoadBool_uniqueID_696);
	}

	private void Relay_Set_False_696()
	{
		logic_SubGraph_SaveLoadBool_boolean_696 = local_TurnEnemiesAfterCubeDeath_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_696 = local_TurnEnemiesAfterCubeDeath_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_696.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_696, logic_SubGraph_SaveLoadBool_boolAsVariable_696, logic_SubGraph_SaveLoadBool_uniqueID_696);
	}

	private void Relay_In_697()
	{
		logic_uScriptCon_CompareBool_Bool_697 = local_CubeDestroyedFly_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_697.In(logic_uScriptCon_CompareBool_Bool_697);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_697.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_697.False;
		if (num)
		{
			Relay_In_690();
		}
		if (flag)
		{
			Relay_In_415();
		}
	}

	private void Relay_True_700()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_700.True(out logic_uScriptAct_SetBool_Target_700);
		local_CubeDestroyedFly_System_Boolean = logic_uScriptAct_SetBool_Target_700;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_700.Out)
		{
			Relay_In_702();
		}
	}

	private void Relay_False_700()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_700.False(out logic_uScriptAct_SetBool_Target_700);
		local_CubeDestroyedFly_System_Boolean = logic_uScriptAct_SetBool_Target_700;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_700.Out)
		{
			Relay_In_702();
		}
	}

	private void Relay_In_702()
	{
		int num = 0;
		if (logic_uScriptAct_Log_Target_702.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Log_Target_702, num + 1);
		}
		logic_uScriptAct_Log_Target_702[num++] = local_703_System_String;
		logic_uScriptAct_Log_uScriptAct_Log_702.In(logic_uScriptAct_Log_Prefix_702, logic_uScriptAct_Log_Target_702, logic_uScriptAct_Log_Postfix_702);
	}

	private void Relay_True_704()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_704.True(out logic_uScriptAct_SetBool_Target_704);
		local_CrazedAmbushTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_704;
	}

	private void Relay_False_704()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_704.False(out logic_uScriptAct_SetBool_Target_704);
		local_CrazedAmbushTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_704;
	}

	private void Relay_True_707()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_707.True(out logic_uScriptAct_SetBool_Target_707);
		local_CrazedNPCInRange_System_Boolean = logic_uScriptAct_SetBool_Target_707;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_707.Out)
		{
			Relay_True_713();
		}
	}

	private void Relay_False_707()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_707.False(out logic_uScriptAct_SetBool_Target_707);
		local_CrazedNPCInRange_System_Boolean = logic_uScriptAct_SetBool_Target_707;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_707.Out)
		{
			Relay_True_713();
		}
	}

	private void Relay_In_709()
	{
		logic_uScriptCon_CompareBool_Bool_709 = local_CubeDeadVictory_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_709.In(logic_uScriptCon_CompareBool_Bool_709);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_709.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_709.False;
		if (num)
		{
			Relay_In_819();
		}
		if (flag)
		{
			Relay_In_667();
		}
	}

	private void Relay_In_710()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_710.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_710.Out)
		{
			Relay_In_376();
		}
	}

	private void Relay_In_711()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_711.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_711.Out)
		{
			Relay_In_443();
		}
	}

	private void Relay_In_712()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_712.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_712.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_True_713()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_713.True(out logic_uScriptAct_SetBool_Target_713);
		local_CrazedIntroPlayedDoubleCheck_System_Boolean = logic_uScriptAct_SetBool_Target_713;
	}

	private void Relay_False_713()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_713.False(out logic_uScriptAct_SetBool_Target_713);
		local_CrazedIntroPlayedDoubleCheck_System_Boolean = logic_uScriptAct_SetBool_Target_713;
	}

	private void Relay_Save_Out_716()
	{
		Relay_Save_573();
	}

	private void Relay_Load_Out_716()
	{
		Relay_Load_573();
	}

	private void Relay_Restart_Out_716()
	{
		Relay_Set_False_573();
	}

	private void Relay_Save_716()
	{
		logic_SubGraph_SaveLoadBool_boolean_716 = local_CrazedIntroPlayedDoubleCheck_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_716 = local_CrazedIntroPlayedDoubleCheck_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.Save(ref logic_SubGraph_SaveLoadBool_boolean_716, logic_SubGraph_SaveLoadBool_boolAsVariable_716, logic_SubGraph_SaveLoadBool_uniqueID_716);
	}

	private void Relay_Load_716()
	{
		logic_SubGraph_SaveLoadBool_boolean_716 = local_CrazedIntroPlayedDoubleCheck_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_716 = local_CrazedIntroPlayedDoubleCheck_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.Load(ref logic_SubGraph_SaveLoadBool_boolean_716, logic_SubGraph_SaveLoadBool_boolAsVariable_716, logic_SubGraph_SaveLoadBool_uniqueID_716);
	}

	private void Relay_Set_True_716()
	{
		logic_SubGraph_SaveLoadBool_boolean_716 = local_CrazedIntroPlayedDoubleCheck_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_716 = local_CrazedIntroPlayedDoubleCheck_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_716, logic_SubGraph_SaveLoadBool_boolAsVariable_716, logic_SubGraph_SaveLoadBool_uniqueID_716);
	}

	private void Relay_Set_False_716()
	{
		logic_SubGraph_SaveLoadBool_boolean_716 = local_CrazedIntroPlayedDoubleCheck_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_716 = local_CrazedIntroPlayedDoubleCheck_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_716.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_716, logic_SubGraph_SaveLoadBool_boolAsVariable_716, logic_SubGraph_SaveLoadBool_uniqueID_716);
	}

	private void Relay_True_717()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_717.True(out logic_uScriptAct_SetBool_Target_717);
		local_TechInvulOnLoad_System_Boolean = logic_uScriptAct_SetBool_Target_717;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_717.Out)
		{
			Relay_False_718();
		}
	}

	private void Relay_False_717()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_717.False(out logic_uScriptAct_SetBool_Target_717);
		local_TechInvulOnLoad_System_Boolean = logic_uScriptAct_SetBool_Target_717;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_717.Out)
		{
			Relay_False_718();
		}
	}

	private void Relay_True_718()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_718.True(out logic_uScriptAct_SetBool_Target_718);
		local_DestroyCube_System_Boolean = logic_uScriptAct_SetBool_Target_718;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_718.Out)
		{
			Relay_False_719();
		}
	}

	private void Relay_False_718()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_718.False(out logic_uScriptAct_SetBool_Target_718);
		local_DestroyCube_System_Boolean = logic_uScriptAct_SetBool_Target_718;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_718.Out)
		{
			Relay_False_719();
		}
	}

	private void Relay_True_719()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_719.True(out logic_uScriptAct_SetBool_Target_719);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_719;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_719.Out)
		{
			Relay_False_720();
		}
	}

	private void Relay_False_719()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_719.False(out logic_uScriptAct_SetBool_Target_719);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_719;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_719.Out)
		{
			Relay_False_720();
		}
	}

	private void Relay_True_720()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_720.True(out logic_uScriptAct_SetBool_Target_720);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_720;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_720.Out)
		{
			Relay_In_679();
		}
	}

	private void Relay_False_720()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_720.False(out logic_uScriptAct_SetBool_Target_720);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_720;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_720.Out)
		{
			Relay_In_679();
		}
	}

	private void Relay_True_726()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_726.True(out logic_uScriptAct_SetBool_Target_726);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_726;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_726.Out)
		{
			Relay_False_727();
		}
	}

	private void Relay_False_726()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_726.False(out logic_uScriptAct_SetBool_Target_726);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_726;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_726.Out)
		{
			Relay_False_727();
		}
	}

	private void Relay_True_727()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_727.True(out logic_uScriptAct_SetBool_Target_727);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_727;
	}

	private void Relay_False_727()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_727.False(out logic_uScriptAct_SetBool_Target_727);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_727;
	}

	private void Relay_True_729()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_729.True(out logic_uScriptAct_SetBool_Target_729);
		local_OutOfTime_System_Boolean = logic_uScriptAct_SetBool_Target_729;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_729.Out)
		{
			Relay_False_717();
		}
	}

	private void Relay_False_729()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_729.False(out logic_uScriptAct_SetBool_Target_729);
		local_OutOfTime_System_Boolean = logic_uScriptAct_SetBool_Target_729;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_729.Out)
		{
			Relay_False_717();
		}
	}

	private void Relay_In_734()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_734.In(logic_uScriptAct_SetInt_Value_734, out logic_uScriptAct_SetInt_Target_734);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_SetInt_Target_734;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_734.Out)
		{
			Relay_In_735();
		}
	}

	private void Relay_In_735()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_735.In(logic_uScriptAct_SetInt_Value_735, out logic_uScriptAct_SetInt_Target_735);
		local_CubeDialog_System_Int32 = logic_uScriptAct_SetInt_Target_735;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_735.Out)
		{
			Relay_In_736();
		}
	}

	private void Relay_In_736()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_736.In(logic_uScriptAct_SetInt_Value_736, out logic_uScriptAct_SetInt_Target_736);
		local_CubeDestroyedDialog_System_Int32 = logic_uScriptAct_SetInt_Target_736;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_736.Out)
		{
			Relay_In_867();
		}
	}

	private void Relay_In_738()
	{
		logic_uScript_SetBatteryChargeAmount_tech_738 = local_CrazedTech03_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_738.In(logic_uScript_SetBatteryChargeAmount_tech_738, logic_uScript_SetBatteryChargeAmount_chargeAmount_738);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_738.Out)
		{
			Relay_In_742();
		}
	}

	private void Relay_In_739()
	{
		logic_uScript_SetTechAIType_tech_739 = local_CrazedTech02_Tank;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_739.In(logic_uScript_SetTechAIType_tech_739, logic_uScript_SetTechAIType_aiType_739);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_739.Out)
		{
			Relay_AtIndex_749();
		}
	}

	private void Relay_AtIndex_741()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_741.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_741, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_741, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_741.AtIndex(ref logic_uScript_AccessListTech_techList_741, logic_uScript_AccessListTech_index_741, out logic_uScript_AccessListTech_value_741);
		local_CrazedTechs_TankArray = logic_uScript_AccessListTech_techList_741;
		local_CrazedTech02_Tank = logic_uScript_AccessListTech_value_741;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_741.Out)
		{
			Relay_In_744();
		}
	}

	private void Relay_In_742()
	{
		logic_uScript_SetTechAIType_tech_742 = local_CrazedTech03_Tank;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_742.In(logic_uScript_SetTechAIType_tech_742, logic_uScript_SetTechAIType_aiType_742);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_742.Out)
		{
			Relay_True_704();
		}
	}

	private void Relay_In_744()
	{
		logic_uScript_SetTankInvulnerable_tank_744 = local_CrazedTech02_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_744.In(logic_uScript_SetTankInvulnerable_invulnerable_744, logic_uScript_SetTankInvulnerable_tank_744);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_744.Out)
		{
			Relay_In_750();
		}
	}

	private void Relay_In_745()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_745.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_745, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_745, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_745.In(ref logic_uScript_SetTechsTeam_techs_745, logic_uScript_SetTechsTeam_team_745);
		local_CrazedTechs_TankArray = logic_uScript_SetTechsTeam_techs_745;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_745.Out)
		{
			Relay_AtIndex_741();
		}
	}

	private void Relay_In_746()
	{
		logic_uScript_SetTankInvulnerable_tank_746 = local_CrazedTech03_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_746.In(logic_uScript_SetTankInvulnerable_invulnerable_746, logic_uScript_SetTankInvulnerable_tank_746);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_746.Out)
		{
			Relay_In_738();
		}
	}

	private void Relay_AtIndex_749()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_749.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_749, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_749, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_749.AtIndex(ref logic_uScript_AccessListTech_techList_749, logic_uScript_AccessListTech_index_749, out logic_uScript_AccessListTech_value_749);
		local_CrazedTechs_TankArray = logic_uScript_AccessListTech_techList_749;
		local_CrazedTech03_Tank = logic_uScript_AccessListTech_value_749;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_749.Out)
		{
			Relay_In_746();
		}
	}

	private void Relay_In_750()
	{
		logic_uScript_SetBatteryChargeAmount_tech_750 = local_CrazedTech02_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_750.In(logic_uScript_SetBatteryChargeAmount_tech_750, logic_uScript_SetBatteryChargeAmount_chargeAmount_750);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_750.Out)
		{
			Relay_In_739();
		}
	}

	private void Relay_True_753()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_753.True(out logic_uScriptAct_SetBool_Target_753);
		local_DestroyCube_System_Boolean = logic_uScriptAct_SetBool_Target_753;
	}

	private void Relay_False_753()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_753.False(out logic_uScriptAct_SetBool_Target_753);
		local_DestroyCube_System_Boolean = logic_uScriptAct_SetBool_Target_753;
	}

	private void Relay_Out_754()
	{
		Relay_False_729();
	}

	private void Relay_In_754()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_754 = local_Objective_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_754.In(logic_SubGraph_LoadObjectiveStates_currentObjective_754);
	}

	private void Relay_In_769()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_769.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_769.Out)
		{
			Relay_In_284();
		}
	}

	private void Relay_In_772()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_772.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_772.Out)
		{
			Relay_In_304();
		}
	}

	private void Relay_In_774()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_774.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_774.Out)
		{
			Relay_In_143();
		}
	}

	private void Relay_In_775()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_775.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_775.Out)
		{
			Relay_In_861();
		}
	}

	private void Relay_In_786()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_786.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_786.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_786.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_499();
		}
		if (multiplayer)
		{
			Relay_In_817();
		}
	}

	private void Relay_In_787()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_787.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_787.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_787.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_526();
		}
		if (multiplayer)
		{
			Relay_In_788();
		}
	}

	private void Relay_In_788()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_788.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_788.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_789()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_789.In();
		if (logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_789.SinglePlayer)
		{
			Relay_False_63();
		}
	}

	private void Relay_In_790()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_790.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_790.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_790.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_132();
		}
		if (multiplayer)
		{
			Relay_In_791();
		}
	}

	private void Relay_In_791()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_791.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_791.Out)
		{
			Relay_In_86();
		}
	}

	private void Relay_In_792()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_792.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_792.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_792.Multiplayer;
		if (singlePlayer)
		{
			Relay_True_109();
		}
		if (multiplayer)
		{
			Relay_In_793();
		}
	}

	private void Relay_In_793()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_793 = LeaderIntroStartTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_793.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_793);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_793.InRange)
		{
			Relay_True_799();
		}
	}

	private void Relay_True_795()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_795.True(out logic_uScriptAct_SetBool_Target_795);
		local_CubeNeedsReload_System_Boolean = logic_uScriptAct_SetBool_Target_795;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_795.Out)
		{
			Relay_False_800();
		}
	}

	private void Relay_False_795()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_795.False(out logic_uScriptAct_SetBool_Target_795);
		local_CubeNeedsReload_System_Boolean = logic_uScriptAct_SetBool_Target_795;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_795.Out)
		{
			Relay_False_800();
		}
	}

	private void Relay_True_799()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_799.True(out logic_uScriptAct_SetBool_Target_799);
		local_LeftAreaAfterLoss_System_Boolean = logic_uScriptAct_SetBool_Target_799;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_799.Out)
		{
			Relay_True_795();
		}
	}

	private void Relay_False_799()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_799.False(out logic_uScriptAct_SetBool_Target_799);
		local_LeftAreaAfterLoss_System_Boolean = logic_uScriptAct_SetBool_Target_799;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_799.Out)
		{
			Relay_True_795();
		}
	}

	private void Relay_True_800()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_800.True(out logic_uScriptAct_SetBool_Target_800);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_800;
	}

	private void Relay_False_800()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_800.False(out logic_uScriptAct_SetBool_Target_800);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_800;
	}

	private void Relay_In_801()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_801.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_801.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_801.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_518();
		}
		if (multiplayer)
		{
			Relay_In_804();
		}
	}

	private void Relay_In_804()
	{
		logic_uScript_AddMessage_messageData_804 = MsgOutOfTimeMultiplayer;
		logic_uScript_AddMessage_speaker_804 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_804 = logic_uScript_AddMessage_uScript_AddMessage_804.In(logic_uScript_AddMessage_messageData_804, logic_uScript_AddMessage_speaker_804);
		if (logic_uScript_AddMessage_uScript_AddMessage_804.Out)
		{
			Relay_False_805();
		}
	}

	private void Relay_True_805()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_805.True(out logic_uScriptAct_SetBool_Target_805);
		local_OutOfTime_System_Boolean = logic_uScriptAct_SetBool_Target_805;
	}

	private void Relay_False_805()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_805.False(out logic_uScriptAct_SetBool_Target_805);
		local_OutOfTime_System_Boolean = logic_uScriptAct_SetBool_Target_805;
	}

	private void Relay_In_808()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_808.In();
		if (logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_808.SinglePlayer)
		{
			Relay_False_107();
		}
	}

	private void Relay_In_809()
	{
		logic_uScript_RemoveTech_tech_809 = local_CubeTech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_809.In(logic_uScript_RemoveTech_tech_809);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_809.Out)
		{
			Relay_In_673();
		}
	}

	private void Relay_In_812()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_812.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_812.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_812.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_627();
		}
		if (multiplayer)
		{
			Relay_In_815();
		}
	}

	private void Relay_In_813()
	{
		logic_uScript_AddMessage_messageData_813 = MsgOutOfTimeMultiplayerLeave;
		logic_uScript_AddMessage_speaker_813 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_813 = logic_uScript_AddMessage_uScript_AddMessage_813.In(logic_uScript_AddMessage_messageData_813, logic_uScript_AddMessage_speaker_813);
	}

	private void Relay_In_815()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_815 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_815.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_815);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_815.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_815.OutOfRange;
		if (inRange)
		{
			Relay_In_813();
		}
		if (outOfRange)
		{
			Relay_In_627();
		}
	}

	private void Relay_In_817()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_817.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_817.Out)
		{
			Relay_In_284();
		}
	}

	private void Relay_True_818()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_818.True(out logic_uScriptAct_SetBool_Target_818);
		local_MinionWaveAlive_System_Boolean = logic_uScriptAct_SetBool_Target_818;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_818.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_False_818()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_818.False(out logic_uScriptAct_SetBool_Target_818);
		local_MinionWaveAlive_System_Boolean = logic_uScriptAct_SetBool_Target_818;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_818.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_In_819()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_819.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_819.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_819.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_452();
		}
		if (multiplayer)
		{
			Relay_In_453();
		}
	}

	private void Relay_True_820()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_820.True(out logic_uScriptAct_SetBool_Target_820);
		local_PlayerInRangeOfCube_System_Boolean = logic_uScriptAct_SetBool_Target_820;
	}

	private void Relay_False_820()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_820.False(out logic_uScriptAct_SetBool_Target_820);
		local_PlayerInRangeOfCube_System_Boolean = logic_uScriptAct_SetBool_Target_820;
	}

	private void Relay_In_821()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_821.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_821.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_821.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_61();
		}
		if (multiplayer)
		{
			Relay_True_820();
		}
	}

	private void Relay_In_823()
	{
		logic_uScriptCon_CompareBool_Bool_823 = local_PlayerInRangeOfCube_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_823.In(logic_uScriptCon_CompareBool_Bool_823);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_823.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_823.False;
		if (num)
		{
			Relay_In_61();
		}
		if (flag)
		{
			Relay_In_71();
		}
	}

	private void Relay_In_824()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_824.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_824.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_824.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_71();
		}
		if (multiplayer)
		{
			Relay_In_823();
		}
	}

	private void Relay_Save_Out_829()
	{
		Relay_Save_7();
	}

	private void Relay_Load_Out_829()
	{
		Relay_Load_7();
	}

	private void Relay_Restart_Out_829()
	{
		Relay_Set_False_7();
	}

	private void Relay_Save_829()
	{
		logic_SubGraph_SaveLoadBool_boolean_829 = local_PlayerInRangeOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_829 = local_PlayerInRangeOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.Save(ref logic_SubGraph_SaveLoadBool_boolean_829, logic_SubGraph_SaveLoadBool_boolAsVariable_829, logic_SubGraph_SaveLoadBool_uniqueID_829);
	}

	private void Relay_Load_829()
	{
		logic_SubGraph_SaveLoadBool_boolean_829 = local_PlayerInRangeOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_829 = local_PlayerInRangeOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.Load(ref logic_SubGraph_SaveLoadBool_boolean_829, logic_SubGraph_SaveLoadBool_boolAsVariable_829, logic_SubGraph_SaveLoadBool_uniqueID_829);
	}

	private void Relay_Set_True_829()
	{
		logic_SubGraph_SaveLoadBool_boolean_829 = local_PlayerInRangeOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_829 = local_PlayerInRangeOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_829, logic_SubGraph_SaveLoadBool_boolAsVariable_829, logic_SubGraph_SaveLoadBool_uniqueID_829);
	}

	private void Relay_Set_False_829()
	{
		logic_SubGraph_SaveLoadBool_boolean_829 = local_PlayerInRangeOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_829 = local_PlayerInRangeOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_829.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_829, logic_SubGraph_SaveLoadBool_boolAsVariable_829, logic_SubGraph_SaveLoadBool_uniqueID_829);
	}

	private void Relay_In_830()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_830 = CrazedMsgTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_830.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_830, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_830);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_830.Out)
		{
			Relay_In_831();
		}
	}

	private void Relay_In_831()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_831 = NPCMsgTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_831.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_831, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_831);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_831.Out)
		{
			Relay_In_197();
		}
	}

	private void Relay_In_834()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_834.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_834.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_834.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_830();
		}
		if (multiplayer)
		{
			Relay_In_835();
		}
	}

	private void Relay_In_835()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_835.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_835.Out)
		{
			Relay_In_836();
		}
	}

	private void Relay_In_836()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_836.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_836.Out)
		{
			Relay_In_197();
		}
	}

	private void Relay_In_838()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_838.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_838.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_838.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_450();
		}
		if (multiplayer)
		{
			Relay_In_839();
		}
	}

	private void Relay_In_839()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_839.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_839.Out)
		{
			Relay_In_840();
		}
	}

	private void Relay_In_840()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_840.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_840.Out)
		{
			Relay_In_360();
		}
	}

	private void Relay_In_843()
	{
		logic_uScript_SetTechExplodeDetachingBlocks_tech_843 = local_CubeTech_Tank;
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_843.In(logic_uScript_SetTechExplodeDetachingBlocks_tech_843, logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_843, logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_843);
	}

	private void Relay_In_847()
	{
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_847 = local_CubeTech_Tank;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_847 = local_844_TechSequencer_ChainType;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_847.In(logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_847, logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_847);
		if (logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_847.Out)
		{
			Relay_In_617();
		}
	}

	private void Relay_In_848()
	{
		logic_uScriptCon_CompareBool_Bool_848 = local_CubeAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_848.In(logic_uScriptCon_CompareBool_Bool_848);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_848.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_848.False;
		if (num)
		{
			Relay_In_847();
		}
		if (flag)
		{
			Relay_In_617();
		}
	}

	private void Relay_True_849()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_849.True(out logic_uScriptAct_SetBool_Target_849);
		local_CubeAlive_System_Boolean = logic_uScriptAct_SetBool_Target_849;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_849.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_849.SetTrue;
		if (num)
		{
			Relay_In_848();
		}
		if (setTrue)
		{
			Relay_AtIndex_615();
		}
	}

	private void Relay_False_849()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_849.False(out logic_uScriptAct_SetBool_Target_849);
		local_CubeAlive_System_Boolean = logic_uScriptAct_SetBool_Target_849;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_849.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_849.SetTrue;
		if (num)
		{
			Relay_In_848();
		}
		if (setTrue)
		{
			Relay_AtIndex_615();
		}
	}

	private void Relay_True_850()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_850.True(out logic_uScriptAct_SetBool_Target_850);
		local_CubeAlive_System_Boolean = logic_uScriptAct_SetBool_Target_850;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_850.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_850.SetTrue;
		if (num)
		{
			Relay_In_610();
		}
		if (setTrue)
		{
			Relay_AtIndex_602();
		}
	}

	private void Relay_False_850()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_850.False(out logic_uScriptAct_SetBool_Target_850);
		local_CubeAlive_System_Boolean = logic_uScriptAct_SetBool_Target_850;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_850.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_850.SetTrue;
		if (num)
		{
			Relay_In_610();
		}
		if (setTrue)
		{
			Relay_AtIndex_602();
		}
	}

	private void Relay_True_852()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_852.True(out logic_uScriptAct_SetBool_Target_852);
		local_MinionsAlive_System_Boolean = logic_uScriptAct_SetBool_Target_852;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_852.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_852.SetTrue;
		if (num)
		{
			Relay_In_613();
		}
		if (setTrue)
		{
			Relay_AtIndex_629();
		}
	}

	private void Relay_False_852()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_852.False(out logic_uScriptAct_SetBool_Target_852);
		local_MinionsAlive_System_Boolean = logic_uScriptAct_SetBool_Target_852;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_852.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_852.SetTrue;
		if (num)
		{
			Relay_In_613();
		}
		if (setTrue)
		{
			Relay_AtIndex_629();
		}
	}

	private void Relay_True_854()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_854.True(out logic_uScriptAct_SetBool_Target_854);
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_854.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_854.SetTrue;
		if (num)
		{
			Relay_In_621();
		}
		if (setTrue)
		{
			Relay_AtIndex_635();
		}
	}

	private void Relay_False_854()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_854.False(out logic_uScriptAct_SetBool_Target_854);
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_854.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_854.SetTrue;
		if (num)
		{
			Relay_In_621();
		}
		if (setTrue)
		{
			Relay_AtIndex_635();
		}
	}

	private void Relay_In_861()
	{
		logic_uScript_PlayDialogue_dialogue_861 = StartBossFightDialogue;
		logic_uScript_PlayDialogue_progress_861 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_861.In(logic_uScript_PlayDialogue_dialogue_861, ref logic_uScript_PlayDialogue_progress_861);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_861;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_861.Shown)
		{
			Relay_In_106();
		}
	}

	private void Relay_In_862()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_862.In(logic_uScriptAct_SetInt_Value_862, out logic_uScriptAct_SetInt_Target_862);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_862;
	}

	private void Relay_In_865()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_865.In(logic_uScriptAct_SetInt_Value_865, out logic_uScriptAct_SetInt_Target_865);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_865;
	}

	private void Relay_In_867()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_867.In(logic_uScriptAct_SetInt_Value_867, out logic_uScriptAct_SetInt_Target_867);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_867;
	}

	private void Relay_In_870()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_870 = owner_Connection_871;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_870.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_870);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_870.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_870.False;
		if (num)
		{
			Relay_Pause_37();
		}
		if (flag)
		{
			Relay_UnPause_37();
		}
	}

	private void Relay_In_872()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_872.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_872.Out)
		{
			Relay_In_873();
		}
	}

	private void Relay_In_873()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_873.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_873.Out)
		{
			Relay_True_700();
		}
	}
}
