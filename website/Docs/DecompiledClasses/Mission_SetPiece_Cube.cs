using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[FriendlyName("", "")]
[NodePath("Graphs")]
public class Mission_SetPiece_Cube : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public uScript_AddMessage.MessageSpeaker CrazedLeaderSpeaker;

	public SpawnTechData[] CrazedLeaderTechData = new SpawnTechData[0];

	[Multiline(1)]
	public string CrazedMsgTag = "";

	[Multiline(1)]
	public string CrazedNPCTrigger = "";

	public SpawnTechData[] CrazedTechData = new SpawnTechData[0];

	[Multiline(1)]
	public string CubeAreaTrigger = "";

	[Multiline(1)]
	public string CubeFailTrigger = "";

	public SpawnTechData[] CubeTechData = new SpawnTechData[0];

	public uScript_AddMessage.MessageSpeaker GCSpeaker;

	public uScript_AddMessage.MessageSpeaker GroupMinionTechSpeaker;

	[Multiline(1)]
	public string LeaderIntroStartTrigger = "";

	[Multiline(1)]
	public string LeaderOutOfRangeTrigger = "";

	private float local_175_System_Single;

	private AITreeType.AITypes local_481_AITreeType_AITypes = AITreeType.AITypes.Guard;

	private AITreeType.AITypes local_483_AITreeType_AITypes = AITreeType.AITypes.Guard;

	private float local_685_System_Single;

	private float local_691_System_Single;

	private TechSequencer.ChainType local_718_TechSequencer_ChainType = TechSequencer.ChainType.ShieldBubble;

	private float local_86_System_Single;

	private bool local_CrazedAmbushMsgTriggered_System_Boolean;

	private bool local_CrazedAmbushTriggered_System_Boolean;

	private int local_CrazedDialog_System_Int32 = 1;

	private bool local_CrazedIntroPlayed_System_Boolean;

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

	private bool local_CubeAlive_System_Boolean;

	private bool local_CubeDeadVictory_System_Boolean;

	private int local_CubeDestroyedDialog_System_Int32 = 1;

	private bool local_CubeDestroyedMsgPlayed_System_Boolean;

	private int local_CubeDialog_System_Int32 = 1;

	private bool local_CubeisOK_System_Boolean;

	private bool local_CubeNeedsReload_System_Boolean;

	private Tank local_CubeTech_Tank;

	private Tank[] local_CubeTechs_TankArray = new Tank[0];

	private int local_DialogueProgress_System_Int32;

	private bool local_FightRunning_System_Boolean;

	private bool local_FightStarted_System_Boolean;

	private bool local_FinalObjectiveComplete_System_Boolean;

	private bool local_FirstCubeSpawned_System_Boolean;

	private bool local_GetRidOfCube_System_Boolean;

	private bool local_HasBeenInterrupted_System_Boolean;

	private bool local_HasPlayerLeftMissionArea_System_Boolean;

	private bool local_IntroSkipped_System_Boolean;

	private bool local_IntroTechInRange_System_Boolean;

	private bool local_LeaderTechAlive_System_Boolean;

	private bool local_LeftAreaAfterLoss_System_Boolean;

	private bool local_MinionsAlive_System_Boolean;

	private bool local_MsgCubeIntroPlayed_System_Boolean;

	private bool local_msgTooEarlyPlayed_System_Boolean;

	private bool local_NPCIgnored_System_Boolean;

	private bool local_NPCInRange_System_Boolean;

	private int local_NPCIntro_System_Int32 = 1;

	private bool local_NPCIntroPlayed_System_Boolean;

	private Tank local_NPCTech_Tank;

	private bool local_NPCTechAlive_System_Boolean;

	private Tank[] local_NPCTechs_TankArray = new Tank[0];

	private bool local_NPCTechSetup_System_Boolean;

	private bool local_NPCTechSpawned_System_Boolean;

	private int local_Objective_System_Int32 = 1;

	private bool local_OutOfTime_System_Boolean;

	private int local_OutOfTimeMsg_System_Int32 = 1;

	private bool local_PlayedTryAgainMsg_System_Boolean;

	private bool local_PlayerAttemptedMission_System_Boolean;

	private bool local_PlayerLeftMissionArea_System_Boolean;

	private bool local_PlayInterruptOnce_System_Boolean;

	private bool local_TankInvul_System_Boolean;

	private bool local_TechInvulOnLoad_System_Boolean;

	private bool local_WaitingForTechClear_System_Boolean;

	private bool local_WentOutOfRange_System_Boolean;

	[Multiline(1)]
	public string MissionArea = "";

	public uScript_AddMessage.MessageData MsgCrazedAmbush;

	public uScript_AddMessage.MessageData MsgCrazedInterrupt;

	public uScript_AddMessage.MessageData MsgCrazedIntro01;

	public uScript_AddMessage.MessageData MsgCrazedIntro02;

	public uScript_AddMessage.MessageData MsgCrazedIntro03;

	public uScript_AddMessage.MessageData MsgCrazedIntro04;

	public uScript_AddMessage.MessageData MsgCrazedIntro05;

	public uScript_AddMessage.MessageData MsgCrazedLeaderB4Fight01;

	public uScript_AddMessage.MessageData MsgCrazedLeaderB4Fight02;

	public uScript_AddMessage.MessageData MsgCrazedLeaderB4Fight03;

	public uScript_AddMessage.MessageData MsgCrazedMinionInterrupt01;

	public uScript_AddMessage.MessageData MsgCrazedMinionInterrupt02;

	public uScript_AddMessage.MessageData MsgCrazedMinionInterrupt03;

	public uScript_AddMessage.MessageData MsgCrazedMinionInterruptB4Fight01;

	public uScript_AddMessage.MessageData MsgCubeDestroyed01;

	public uScript_AddMessage.MessageData MsgCubeDestroyed02;

	public uScript_AddMessage.MessageData MsgCubeLeaveAreaFail;

	public uScript_AddMessage.MessageData MsgLeaderTryAgain01;

	public uScript_AddMessage.MessageData MsgLeftAreaCompletely;

	public uScript_AddMessage.MessageData MsgMinionsDead;

	public uScript_AddMessage.MessageData MsgMissionComplete;

	public uScript_AddMessage.MessageData MsgNPCInterrupt;

	public uScript_AddMessage.MessageData MsgNPCIntro01;

	public uScript_AddMessage.MessageData MsgNPCIntro02;

	public uScript_AddMessage.MessageData MsgOutOfTime;

	public uScript_AddMessage.MessageData MsgOutOfTime2;

	public uScript_AddMessage.MessageData MsgOutOfTimeMultiplayer;

	public uScript_AddMessage.MessageData MsgOutOfTimeMultiplayerLeave;

	public uScript_AddMessage.MessageData MsgStartBossFight;

	public uScript_AddMessage.MessageData msgTooEarly;

	public uScript_AddMessage.MessageData msgTooEarly2;

	[Multiline(1)]
	public string NPCIntroStartTrigger = "";

	[Multiline(1)]
	public string NPCIntroTechInRange = "";

	[Multiline(1)]
	public string NPCMsgTag = "";

	public SpawnTechData[] NPCTechData = new SpawnTechData[0];

	public uScript_AddMessage.MessageSpeaker NPCTechSpeaker;

	public uScript_PlayDialogue.Dialogue StartBossFightDialogue;

	public ExternalBehaviorTree TechFlyAI;

	public Transform TechFlyParticles;

	public float TimeLimit;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_15;

	private GameObject owner_Connection_19;

	private GameObject owner_Connection_24;

	private GameObject owner_Connection_26;

	private GameObject owner_Connection_39;

	private GameObject owner_Connection_54;

	private GameObject owner_Connection_67;

	private GameObject owner_Connection_69;

	private GameObject owner_Connection_79;

	private GameObject owner_Connection_85;

	private GameObject owner_Connection_88;

	private GameObject owner_Connection_93;

	private GameObject owner_Connection_100;

	private GameObject owner_Connection_109;

	private GameObject owner_Connection_124;

	private GameObject owner_Connection_127;

	private GameObject owner_Connection_134;

	private GameObject owner_Connection_166;

	private GameObject owner_Connection_179;

	private GameObject owner_Connection_358;

	private GameObject owner_Connection_415;

	private GameObject owner_Connection_441;

	private GameObject owner_Connection_448;

	private GameObject owner_Connection_510;

	private GameObject owner_Connection_541;

	private GameObject owner_Connection_558;

	private GameObject owner_Connection_561;

	private GameObject owner_Connection_594;

	private GameObject owner_Connection_595;

	private GameObject owner_Connection_598;

	private GameObject owner_Connection_600;

	private GameObject owner_Connection_604;

	private GameObject owner_Connection_607;

	private GameObject owner_Connection_611;

	private GameObject owner_Connection_619;

	private GameObject owner_Connection_681;

	private GameObject owner_Connection_688;

	private GameObject owner_Connection_690;

	private GameObject owner_Connection_726;

	private GameObject owner_Connection_808;

	private GameObject owner_Connection_809;

	private GameObject owner_Connection_810;

	private GameObject owner_Connection_825;

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

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_13;

	private bool logic_uScript_SpawnTechsFromData_Out_13 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_16 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_16 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_16;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_16 = 0.5f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_16 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_16 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_17 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_17;

	private bool logic_uScriptAct_SetBool_Out_17 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_17 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_17 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_20 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_20 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_20;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_20 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_20;

	private bool logic_uScript_SpawnTechsFromData_Out_20 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_22 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_22;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_22 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_22;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_22 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_22 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_22 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_22 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_28 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_28 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_28;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_28 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_28;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_28 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_28 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_28 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_28 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_29 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_29 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_29;

	private bool logic_uScript_SetTankInvulnerable_Out_29 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_30 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_30 = new Tank[0];

	private int logic_uScript_AccessListTech_index_30;

	private Tank logic_uScript_AccessListTech_value_30;

	private bool logic_uScript_AccessListTech_Out_30 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_32 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_32 = new Tank[0];

	private int logic_uScript_AccessListTech_index_32;

	private Tank logic_uScript_AccessListTech_value_32;

	private bool logic_uScript_AccessListTech_Out_32 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_34 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_34 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_34;

	private bool logic_uScript_SetTankInvulnerable_Out_34 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_36;

	private bool logic_uScriptCon_CompareBool_True_36 = true;

	private bool logic_uScriptCon_CompareBool_False_36 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_40 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_40;

	private bool logic_uScriptAct_SetBool_Out_40 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_40 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_40 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_42;

	private bool logic_uScriptCon_CompareBool_True_42 = true;

	private bool logic_uScriptCon_CompareBool_False_42 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_44 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_44;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_44;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_44;

	private bool logic_uScript_AddMessage_Out_44 = true;

	private bool logic_uScript_AddMessage_Shown_44 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_45 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_45 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_45;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_45 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_47;

	private bool logic_uScriptCon_CompareBool_True_47 = true;

	private bool logic_uScriptCon_CompareBool_False_47 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_48;

	private bool logic_uScriptCon_CompareBool_True_48 = true;

	private bool logic_uScriptCon_CompareBool_False_48 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_53 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_53;

	private bool logic_uScript_FinishEncounter_Out_53 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_55;

	private bool logic_uScriptCon_CompareBool_True_55 = true;

	private bool logic_uScriptCon_CompareBool_False_55 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_57 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_57;

	private bool logic_uScriptAct_SetBool_Out_57 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_57 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_57 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_60 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_60;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_60;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_60;

	private bool logic_uScript_AddMessage_Out_60 = true;

	private bool logic_uScript_AddMessage_Shown_60 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_62 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_62;

	private object logic_uScript_SetEncounterTarget_visibleObject_62 = "";

	private bool logic_uScript_SetEncounterTarget_Out_62 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_64 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_64 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_65 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_65;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_65 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_66 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_66;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_66;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_68 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_68 = new Tank[0];

	private int logic_uScript_AccessListTech_index_68;

	private Tank logic_uScript_AccessListTech_value_68;

	private bool logic_uScript_AccessListTech_Out_68 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_70 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_70 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_70;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_70 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_70;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_70 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_70 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_70 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_70 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_74;

	private bool logic_uScriptCon_CompareBool_True_74 = true;

	private bool logic_uScriptCon_CompareBool_False_74 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_78 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_78;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_78;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_78;

	private bool logic_uScript_AddMessage_Out_78 = true;

	private bool logic_uScript_AddMessage_Shown_78 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_81;

	private bool logic_uScriptCon_CompareBool_True_81 = true;

	private bool logic_uScriptCon_CompareBool_False_81 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_82 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_82;

	private bool logic_uScriptAct_SetBool_Out_82 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_82 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_82 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_84 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_84;

	private object logic_uScript_SetEncounterTarget_visibleObject_84 = "";

	private bool logic_uScript_SetEncounterTarget_Out_84 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_89 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_89;

	private bool logic_uScriptCon_CompareBool_True_89 = true;

	private bool logic_uScriptCon_CompareBool_False_89 = true;

	private uScript_PlayMiscSFX logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_90 = new uScript_PlayMiscSFX();

	private ManSFX.MiscSfxType logic_uScript_PlayMiscSFX_miscSFXType_90 = ManSFX.MiscSfxType.StuntFailed;

	private bool logic_uScript_PlayMiscSFX_Out_90 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_91 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_91;

	private bool logic_uScriptAct_SetBool_Out_91 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_91 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_91 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_92 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_92;

	private bool logic_uScriptAct_SetBool_Out_92 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_92 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_92 = true;

	private uScript_ShowMissionTimerUI logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_94 = new uScript_ShowMissionTimerUI();

	private GameObject logic_uScript_ShowMissionTimerUI_owner_94;

	private bool logic_uScript_ShowMissionTimerUI_showBestTime_94;

	private bool logic_uScript_ShowMissionTimerUI_Out_94 = true;

	private uScript_PlayMiscSFX logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_96 = new uScript_PlayMiscSFX();

	private ManSFX.MiscSfxType logic_uScript_PlayMiscSFX_miscSFXType_96 = ManSFX.MiscSfxType.StuntRingStart;

	private bool logic_uScript_PlayMiscSFX_Out_96 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_97 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_97;

	private bool logic_uScriptAct_SetBool_Out_97 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_97 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_97 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_98 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_98;

	private bool logic_uScriptAct_SetBool_Out_98 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_98 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_98 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_99 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_99 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_99 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_99 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_99 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_99 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_99 = true;

	private uScript_StartMissionTimer logic_uScript_StartMissionTimer_uScript_StartMissionTimer_105 = new uScript_StartMissionTimer();

	private GameObject logic_uScript_StartMissionTimer_owner_105;

	private float logic_uScript_StartMissionTimer_startTime_105;

	private bool logic_uScript_StartMissionTimer_Out_105 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_106 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_106;

	private float logic_uScriptCon_CompareFloat_B_106;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_106 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_106 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_106 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_106 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_106 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_106 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_112 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_112 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_112;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_112 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_112;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_112 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_112 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_112 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_112 = true;

	private uScript_GetMissionTimerDisplayTime logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_113 = new uScript_GetMissionTimerDisplayTime();

	private GameObject logic_uScript_GetMissionTimerDisplayTime_owner_113;

	private float logic_uScript_GetMissionTimerDisplayTime_Return_113;

	private bool logic_uScript_GetMissionTimerDisplayTime_Out_113 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_115 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_115;

	private bool logic_uScriptAct_SetBool_Out_115 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_115 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_115 = true;

	private uScript_StopMissionTimer logic_uScript_StopMissionTimer_uScript_StopMissionTimer_116 = new uScript_StopMissionTimer();

	private GameObject logic_uScript_StopMissionTimer_owner_116;

	private bool logic_uScript_StopMissionTimer_Out_116 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_118;

	private bool logic_uScriptCon_CompareBool_True_118 = true;

	private bool logic_uScriptCon_CompareBool_False_118 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_120 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_120;

	private bool logic_uScriptCon_CompareBool_True_120 = true;

	private bool logic_uScriptCon_CompareBool_False_120 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_123 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_123 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_126 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_126 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_126;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_126 = 0.5f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_126 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_126 = true;

	private uScript_PlayMiscSFX logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_128 = new uScript_PlayMiscSFX();

	private ManSFX.MiscSfxType logic_uScript_PlayMiscSFX_miscSFXType_128 = ManSFX.MiscSfxType.StuntRingStart;

	private bool logic_uScript_PlayMiscSFX_Out_128 = true;

	private uScript_ShowMissionTimerUI logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_130 = new uScript_ShowMissionTimerUI();

	private GameObject logic_uScript_ShowMissionTimerUI_owner_130;

	private bool logic_uScript_ShowMissionTimerUI_showBestTime_130;

	private bool logic_uScript_ShowMissionTimerUI_Out_130 = true;

	private uScript_StartMissionTimer logic_uScript_StartMissionTimer_uScript_StartMissionTimer_131 = new uScript_StartMissionTimer();

	private GameObject logic_uScript_StartMissionTimer_owner_131;

	private float logic_uScript_StartMissionTimer_startTime_131;

	private bool logic_uScript_StartMissionTimer_Out_131 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_132 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_132;

	private bool logic_uScriptAct_SetBool_Out_132 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_132 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_132 = true;

	private uScript_StopMissionTimer logic_uScript_StopMissionTimer_uScript_StopMissionTimer_135 = new uScript_StopMissionTimer();

	private GameObject logic_uScript_StopMissionTimer_owner_135;

	private bool logic_uScript_StopMissionTimer_Out_135 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_137 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_137;

	private bool logic_uScriptCon_CompareBool_True_137 = true;

	private bool logic_uScriptCon_CompareBool_False_137 = true;

	private uScript_HideMissionTimerUI logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_139 = new uScript_HideMissionTimerUI();

	private GameObject logic_uScript_HideMissionTimerUI_owner_139;

	private bool logic_uScript_HideMissionTimerUI_Out_139 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_141 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_141;

	private bool logic_uScriptAct_SetBool_Out_141 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_141 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_141 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_143;

	private bool logic_uScriptCon_CompareBool_True_143 = true;

	private bool logic_uScriptCon_CompareBool_False_143 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_145 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_145;

	private bool logic_uScriptAct_SetBool_Out_145 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_145 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_145 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_148 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_148;

	private bool logic_uScriptAct_SetBool_Out_148 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_148 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_148 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_149 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_149 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_149 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_149 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_149 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_149 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_149 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_152 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_152 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_152 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_152 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_152 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_152 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_152 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_154 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_154;

	private bool logic_uScriptAct_SetBool_Out_154 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_154 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_154 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_156 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_156;

	private bool logic_uScriptAct_SetBool_Out_156 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_156 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_156 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_157 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_157;

	private bool logic_uScriptCon_CompareBool_True_157 = true;

	private bool logic_uScriptCon_CompareBool_False_157 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_160 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_160 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_160 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_160 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_160 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_160 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_160 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_162 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_162;

	private bool logic_uScriptAct_SetBool_Out_162 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_162 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_162 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_164 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_164;

	private bool logic_uScriptAct_SetBool_Out_164 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_164 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_164 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_165 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_165 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_165;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_165 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_165;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_165 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_165 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_165 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_165 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_169 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_169 = new Tank[0];

	private int logic_uScript_AccessListTech_index_169;

	private Tank logic_uScript_AccessListTech_value_169;

	private bool logic_uScript_AccessListTech_Out_169 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_170 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_170;

	private bool logic_uScriptAct_SetBool_Out_170 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_170 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_170 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_171 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_171;

	private bool logic_uScriptAct_SetBool_Out_171 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_171 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_171 = true;

	private uScript_ResetMissionTimerTimeElapsed logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_174 = new uScript_ResetMissionTimerTimeElapsed();

	private GameObject logic_uScript_ResetMissionTimerTimeElapsed_owner_174;

	private float logic_uScript_ResetMissionTimerTimeElapsed_startTime_174;

	private bool logic_uScript_ResetMissionTimerTimeElapsed_Out_174 = true;

	private uScript_HideMissionTimerUI logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_176 = new uScript_HideMissionTimerUI();

	private GameObject logic_uScript_HideMissionTimerUI_owner_176;

	private bool logic_uScript_HideMissionTimerUI_Out_176 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_178 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_178;

	private bool logic_uScriptAct_SetBool_Out_178 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_178 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_178 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_180 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_180 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_180 = 1;

	private bool logic_uScript_SetTechsTeam_Out_180 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_181 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_181;

	private Tank logic_uScript_SetTankInvulnerable_tank_181;

	private bool logic_uScript_SetTankInvulnerable_Out_181 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_183 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_183 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_183;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_183 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_183;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_183 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_183 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_183 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_183 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_185 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_185 = new Tank[0];

	private int logic_uScript_AccessListTech_index_185;

	private Tank logic_uScript_AccessListTech_value_185;

	private bool logic_uScript_AccessListTech_Out_185 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_189 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_189;

	private bool logic_uScriptCon_CompareBool_True_189 = true;

	private bool logic_uScriptCon_CompareBool_False_189 = true;

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

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_194;

	private bool logic_uScriptCon_CompareBool_True_194 = true;

	private bool logic_uScriptCon_CompareBool_False_194 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_196 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_196 = new Tank[0];

	private int logic_uScript_AccessListTech_index_196;

	private Tank logic_uScript_AccessListTech_value_196;

	private bool logic_uScript_AccessListTech_Out_196 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_199 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_199;

	private bool logic_uScriptCon_CompareBool_True_199 = true;

	private bool logic_uScriptCon_CompareBool_False_199 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_200 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_200;

	private bool logic_uScriptAct_SetBool_Out_200 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_200 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_200 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_202 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_202;

	private bool logic_uScriptAct_SetBool_Out_202 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_202 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_202 = true;

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

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_209 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_209;

	private bool logic_uScriptAct_SetBool_Out_209 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_209 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_209 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_212 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_212 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_212 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_212 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_212 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_212 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_212 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_214 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_214;

	private bool logic_uScriptAct_SetBool_Out_214 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_214 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_214 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_215 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_215 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_215;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_215 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_217 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_217;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_217;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_217;

	private bool logic_uScript_AddMessage_Out_217 = true;

	private bool logic_uScript_AddMessage_Shown_217 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_221 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_221;

	private bool logic_uScriptAct_SetBool_Out_221 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_221 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_221 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_222 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_222;

	private bool logic_uScriptAct_SetBool_Out_222 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_222 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_222 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_223;

	private bool logic_uScriptCon_CompareBool_True_223 = true;

	private bool logic_uScriptCon_CompareBool_False_223 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_224 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_224;

	private bool logic_uScriptAct_SetBool_Out_224 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_224 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_224 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_225;

	private bool logic_uScriptCon_CompareBool_True_225 = true;

	private bool logic_uScriptCon_CompareBool_False_225 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_226 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_226;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_226;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_226;

	private bool logic_uScript_AddMessage_Out_226 = true;

	private bool logic_uScript_AddMessage_Shown_226 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_229 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_229 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_229;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_229 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_230 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_230;

	private bool logic_uScriptAct_SetBool_Out_230 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_230 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_230 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_237 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_237;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_237;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_237;

	private bool logic_uScript_AddMessage_Out_237 = true;

	private bool logic_uScript_AddMessage_Shown_237 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_241 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_241;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_241;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_241;

	private bool logic_uScript_AddMessage_Out_241 = true;

	private bool logic_uScript_AddMessage_Shown_241 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_242 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_242;

	private bool logic_uScriptAct_SetBool_Out_242 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_242 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_242 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_244 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_244;

	private bool logic_uScriptAct_SetBool_Out_244 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_244 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_244 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_246 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_246;

	private bool logic_uScriptCon_CompareBool_True_246 = true;

	private bool logic_uScriptCon_CompareBool_False_246 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_249 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_249;

	private bool logic_uScriptCon_CompareBool_True_249 = true;

	private bool logic_uScriptCon_CompareBool_False_249 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_252 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_252;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_252;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_252;

	private bool logic_uScript_AddMessage_Out_252 = true;

	private bool logic_uScript_AddMessage_Shown_252 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_253 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_253;

	private bool logic_uScriptAct_SetBool_Out_253 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_253 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_253 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_257;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_257 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_257 = "NPCTechSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_259;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_259 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_259 = "NPCTechSetup";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_261;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_261 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_261 = "CrazedAmbushTriggered";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_263;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_263 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_263 = "FirstCubeSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_285;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_285 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_285 = "TankInvul";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_286;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_286 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_286 = "PlayInterruptOnce";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_287;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_287 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_287 = "CubeNeedsReload";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_288;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_288 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_288 = "NPCIgnored";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_289;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_289 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_289 = "CrazedIntroPlayed";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_290;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_290 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_290 = "NPCIntroPlayed";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_291;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_291 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_291 = "NPCInRange";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_292;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_292 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_292 = "CrazedNPCIgnored";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_293;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_293 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_293 = "CubeDeadVictory";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_294;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_294 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_294 = "HasBeenInterrupted";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_295;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_295 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_295 = "CrazedNPCInRange";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_296;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_296 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_296 = "CrazedPlayInterruptOnce";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_297;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_297 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_297 = "PlayerAttemptedMission";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_298;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_298 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_298 = "LeftAreaAfterLoss";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_299;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_299 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_299 = "MsgCubeIntroPlayed";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_300;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_300 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_300 = "CubeDestroyedMsgPlayed";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_301;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_301 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_301 = "WentOutOfRange";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_302;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_302 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_302 = "CubeDeadVictory";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_303;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_303 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_303 = "OutOfTime";

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_304 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_304 = new Tank[0];

	private int logic_uScript_AccessListTech_index_304 = 1;

	private Tank logic_uScript_AccessListTech_value_304;

	private bool logic_uScript_AccessListTech_Out_304 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_308 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_308;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_308;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_308;

	private bool logic_uScript_AddMessage_Out_308 = true;

	private bool logic_uScript_AddMessage_Shown_308 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_310 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_310;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_310;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_310;

	private bool logic_uScript_AddMessage_Out_310 = true;

	private bool logic_uScript_AddMessage_Shown_310 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_313 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_313;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_313;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_313;

	private bool logic_uScript_AddMessage_Out_313 = true;

	private bool logic_uScript_AddMessage_Shown_313 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_315 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_315;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_315;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_315;

	private bool logic_uScript_AddMessage_Out_315 = true;

	private bool logic_uScript_AddMessage_Shown_315 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_320 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_320;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_320;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_320;

	private bool logic_uScript_AddMessage_Out_320 = true;

	private bool logic_uScript_AddMessage_Shown_320 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_321 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_321;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_321;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_321;

	private bool logic_uScript_AddMessage_Out_321 = true;

	private bool logic_uScript_AddMessage_Shown_321 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_324 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_324;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_324;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_324;

	private bool logic_uScript_AddMessage_Out_324 = true;

	private bool logic_uScript_AddMessage_Shown_324 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_327 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_327;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_327;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_327;

	private bool logic_uScript_AddMessage_Out_327 = true;

	private bool logic_uScript_AddMessage_Shown_327 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_329 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_329;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_329;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_329;

	private bool logic_uScript_AddMessage_Out_329 = true;

	private bool logic_uScript_AddMessage_Shown_329 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_334 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_334;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_334;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_334;

	private bool logic_uScript_AddMessage_Out_334 = true;

	private bool logic_uScript_AddMessage_Shown_334 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_335 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_335;

	private bool logic_uScriptCon_CompareBool_True_335 = true;

	private bool logic_uScriptCon_CompareBool_False_335 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_336;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_339 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_339;

	private int logic_uScriptAct_AddInt_v2_B_339 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_339;

	private float logic_uScriptAct_AddInt_v2_FloatResult_339;

	private bool logic_uScriptAct_AddInt_v2_Out_339 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_342 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_342;

	private int logic_uScriptAct_AddInt_v2_B_342 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_342;

	private float logic_uScriptAct_AddInt_v2_FloatResult_342;

	private bool logic_uScriptAct_AddInt_v2_Out_342 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_344 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_344;

	private int logic_uScriptAct_AddInt_v2_B_344 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_344;

	private float logic_uScriptAct_AddInt_v2_FloatResult_344;

	private bool logic_uScriptAct_AddInt_v2_Out_344 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_345 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_345;

	private int logic_uScriptAct_AddInt_v2_B_345 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_345;

	private float logic_uScriptAct_AddInt_v2_FloatResult_345;

	private bool logic_uScriptAct_AddInt_v2_Out_345 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_347 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_347;

	private bool logic_uScriptCon_CompareBool_True_347 = true;

	private bool logic_uScriptCon_CompareBool_False_347 = true;

	private uScriptAct_SubtractInt logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_349 = new uScriptAct_SubtractInt();

	private int logic_uScriptAct_SubtractInt_A_349;

	private int logic_uScriptAct_SubtractInt_B_349 = 1;

	private int logic_uScriptAct_SubtractInt_IntResult_349;

	private float logic_uScriptAct_SubtractInt_FloatResult_349;

	private bool logic_uScriptAct_SubtractInt_Out_349 = true;

	private uScriptAct_SubtractInt logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_352 = new uScriptAct_SubtractInt();

	private int logic_uScriptAct_SubtractInt_A_352;

	private int logic_uScriptAct_SubtractInt_B_352 = 1;

	private int logic_uScriptAct_SubtractInt_IntResult_352;

	private float logic_uScriptAct_SubtractInt_FloatResult_352;

	private bool logic_uScriptAct_SubtractInt_Out_352 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_354 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_354;

	private int logic_uScriptAct_AddInt_v2_B_354 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_354;

	private float logic_uScriptAct_AddInt_v2_FloatResult_354;

	private bool logic_uScriptAct_AddInt_v2_Out_354 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_357 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_357;

	private object logic_uScript_SetEncounterTarget_visibleObject_357 = "";

	private bool logic_uScript_SetEncounterTarget_Out_357 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_359 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_359;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_359;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_361 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_361;

	private bool logic_uScriptAct_SetBool_Out_361 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_361 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_361 = true;

	private uScriptAct_SubtractInt logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_362 = new uScriptAct_SubtractInt();

	private int logic_uScriptAct_SubtractInt_A_362;

	private int logic_uScriptAct_SubtractInt_B_362 = 1;

	private int logic_uScriptAct_SubtractInt_IntResult_362;

	private float logic_uScriptAct_SubtractInt_FloatResult_362;

	private bool logic_uScriptAct_SubtractInt_Out_362 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_366 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_366;

	private bool logic_uScriptCon_CompareBool_True_366 = true;

	private bool logic_uScriptCon_CompareBool_False_366 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_367 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_367;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_367;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_367;

	private bool logic_uScript_AddMessage_Out_367 = true;

	private bool logic_uScript_AddMessage_Shown_367 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_368 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_368;

	private int logic_uScriptAct_AddInt_v2_B_368 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_368;

	private float logic_uScriptAct_AddInt_v2_FloatResult_368;

	private bool logic_uScriptAct_AddInt_v2_Out_368 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_372 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_372;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_372;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_372;

	private bool logic_uScript_AddMessage_Out_372 = true;

	private bool logic_uScript_AddMessage_Shown_372 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_375 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_375;

	private int logic_uScriptAct_AddInt_v2_B_375 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_375;

	private float logic_uScriptAct_AddInt_v2_FloatResult_375;

	private bool logic_uScriptAct_AddInt_v2_Out_375 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_376;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_378 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_378;

	private int logic_uScriptAct_AddInt_v2_B_378 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_378;

	private float logic_uScriptAct_AddInt_v2_FloatResult_378;

	private bool logic_uScriptAct_AddInt_v2_Out_378 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_380 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_380;

	private int logic_uScriptAct_AddInt_v2_B_380 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_380;

	private float logic_uScriptAct_AddInt_v2_FloatResult_380;

	private bool logic_uScriptAct_AddInt_v2_Out_380 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_382 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_382;

	private int logic_uScriptAct_AddInt_v2_B_382 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_382;

	private float logic_uScriptAct_AddInt_v2_FloatResult_382;

	private bool logic_uScriptAct_AddInt_v2_Out_382 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_384 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_384;

	private bool logic_uScriptAct_SetBool_Out_384 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_384 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_384 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_386 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_386;

	private bool logic_uScriptAct_SetBool_Out_386 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_386 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_386 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_388 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_388;

	private bool logic_uScriptAct_SetBool_Out_388 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_388 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_388 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_390 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_390 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_392 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_392;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_392 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_392 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_392;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_392;

	private bool logic_uScript_FlyTechUpAndAway_Out_392 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_395;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_396 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_396;

	private int logic_uScriptAct_AddInt_v2_B_396 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_396;

	private float logic_uScriptAct_AddInt_v2_FloatResult_396;

	private bool logic_uScriptAct_AddInt_v2_Out_396 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_399;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_400 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_400;

	private int logic_uScriptAct_AddInt_v2_B_400 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_400;

	private float logic_uScriptAct_AddInt_v2_FloatResult_400;

	private bool logic_uScriptAct_AddInt_v2_Out_400 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_403 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_403;

	private int logic_uScriptAct_AddInt_v2_B_403 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_403;

	private float logic_uScriptAct_AddInt_v2_FloatResult_403;

	private bool logic_uScriptAct_AddInt_v2_Out_403 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_405 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_405 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_405 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_405 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_405 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_405 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_405 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_407 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_407;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_407;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_411 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_411;

	private object logic_uScript_SetEncounterTarget_visibleObject_411 = "";

	private bool logic_uScript_SetEncounterTarget_Out_411 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_413 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_413;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_413 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_413 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_413;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_413;

	private bool logic_uScript_FlyTechUpAndAway_Out_413 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_417 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_417;

	private bool logic_uScriptAct_SetBool_Out_417 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_417 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_417 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_418 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_418;

	private bool logic_uScriptAct_SetBool_Out_418 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_418 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_418 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_419 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_419;

	private bool logic_uScriptCon_CompareBool_True_419 = true;

	private bool logic_uScriptCon_CompareBool_False_419 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_423 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_423;

	private bool logic_uScriptAct_SetBool_Out_423 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_423 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_423 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_424 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_424 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_424 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_424 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_424 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_424 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_424 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_426 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_426;

	private bool logic_uScriptCon_CompareBool_True_426 = true;

	private bool logic_uScriptCon_CompareBool_False_426 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_428 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_428 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_428 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_428 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_428 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_428 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_428 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_429 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_429 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_429 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_429 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_429 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_429 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_429 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_434 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_434;

	private bool logic_uScriptAct_SetBool_Out_434 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_434 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_434 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_437 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_437 = new Tank[0];

	private int logic_uScript_AccessListTech_index_437;

	private Tank logic_uScript_AccessListTech_value_437;

	private bool logic_uScript_AccessListTech_Out_437 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_438 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_438;

	private Tank logic_uScript_SetTankInvulnerable_tank_438;

	private bool logic_uScript_SetTankInvulnerable_Out_438 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_440 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_440 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_440 = 1;

	private bool logic_uScript_SetTechsTeam_Out_440 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_443 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_443 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_443;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_443 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_443;

	private bool logic_uScript_SpawnTechsFromData_Out_443 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_444 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_444 = new Tank[0];

	private int logic_uScript_AccessListTech_index_444;

	private Tank logic_uScript_AccessListTech_value_444;

	private bool logic_uScript_AccessListTech_Out_444 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_445 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_445 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_445;

	private bool logic_uScript_SetTankInvulnerable_Out_445 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_447 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_447 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_447;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_447 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_447;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_447 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_447 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_447 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_447 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_451 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_451 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_451;

	private bool logic_uScript_SetTankInvulnerable_Out_451 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_454 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_454;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_454;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_454;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_454;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_454;

	private bool logic_uScript_FlyTechUpAndAway_Out_454 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_456 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_456;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_456;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_456;

	private bool logic_uScript_AddMessage_Out_456 = true;

	private bool logic_uScript_AddMessage_Shown_456 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_459 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_459;

	private bool logic_uScriptCon_CompareBool_True_459 = true;

	private bool logic_uScriptCon_CompareBool_False_459 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_460 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_460;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_460;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_460;

	private bool logic_uScript_AddMessage_Out_460 = true;

	private bool logic_uScript_AddMessage_Shown_460 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_462 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_462 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_462 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_462 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_462 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_462 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_462 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_465 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_465;

	private bool logic_uScriptAct_SetBool_Out_465 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_465 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_465 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_469 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_469;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_469;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_469;

	private bool logic_uScript_AddMessage_Out_469 = true;

	private bool logic_uScript_AddMessage_Shown_469 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_472 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_472 = new Tank[0];

	private int logic_uScript_AccessListTech_index_472 = 1;

	private Tank logic_uScript_AccessListTech_value_472;

	private bool logic_uScript_AccessListTech_Out_472 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_473 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_473;

	private Tank logic_uScript_SetTankInvulnerable_tank_473;

	private bool logic_uScript_SetTankInvulnerable_Out_473 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_474 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_474 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_475 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_475 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_475 = -2;

	private bool logic_uScript_SetTechsTeam_Out_475 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_479 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_479;

	private bool logic_uScriptAct_SetBool_Out_479 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_479 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_479 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_480 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_480;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_480;

	private bool logic_uScript_SetTechAIType_Out_480 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_482 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_482;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_482;

	private bool logic_uScript_SetTechAIType_Out_482 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_485;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_487 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_487;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_487;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_487;

	private bool logic_uScript_AddMessage_Out_487 = true;

	private bool logic_uScript_AddMessage_Shown_487 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_489 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_489;

	private bool logic_uScriptAct_SetBool_Out_489 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_489 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_489 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_491 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_491;

	private int logic_uScriptAct_AddInt_v2_B_491 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_491;

	private float logic_uScriptAct_AddInt_v2_FloatResult_491;

	private bool logic_uScriptAct_AddInt_v2_Out_491 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_495 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_495;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_495;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_496 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_496;

	private bool logic_uScriptCon_CompareBool_True_496 = true;

	private bool logic_uScriptCon_CompareBool_False_496 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_499 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_499;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_499;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_499;

	private bool logic_uScript_AddMessage_Out_499 = true;

	private bool logic_uScript_AddMessage_Shown_499 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_501 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_501;

	private bool logic_uScriptAct_SetBool_Out_501 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_501 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_501 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_503 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_503 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_503 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_503 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_503 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_503 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_503 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_506 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_506;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_506;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_506;

	private bool logic_uScript_AddMessage_Out_506 = true;

	private bool logic_uScript_AddMessage_Shown_506 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_508 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_508 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_508;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_508 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_508;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_508 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_508 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_508 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_508 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_512 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_512;

	private bool logic_uScriptAct_SetBool_Out_512 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_512 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_512 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_514 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_514;

	private bool logic_uScriptCon_CompareBool_True_514 = true;

	private bool logic_uScriptCon_CompareBool_False_514 = true;

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

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_519 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_519;

	private bool logic_uScriptCon_CompareBool_True_519 = true;

	private bool logic_uScriptCon_CompareBool_False_519 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_522 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_522;

	private bool logic_uScriptAct_SetBool_Out_522 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_522 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_522 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_523 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_523;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_523;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_523;

	private bool logic_uScript_AddMessage_Out_523 = true;

	private bool logic_uScript_AddMessage_Shown_523 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_524 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_524;

	private bool logic_uScriptCon_CompareBool_True_524 = true;

	private bool logic_uScriptCon_CompareBool_False_524 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_528 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_528;

	private bool logic_uScriptCon_CompareBool_True_528 = true;

	private bool logic_uScriptCon_CompareBool_False_528 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_531 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_531;

	private bool logic_uScriptAct_SetBool_Out_531 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_531 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_531 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_532 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_532 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_532 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_532 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_532 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_532 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_532 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_533 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_533;

	private bool logic_uScriptAct_SetBool_Out_533 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_533 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_533 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_534 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_534;

	private bool logic_uScriptAct_SetBool_Out_534 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_534 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_534 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_536 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_536;

	private bool logic_uScriptAct_SetBool_Out_536 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_536 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_536 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_539 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_539;

	private bool logic_uScriptAct_SetBool_Out_539 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_539 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_539 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_543 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_543 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_543;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_543 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_543;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_543 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_543 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_543 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_543 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_547 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_547;

	private bool logic_uScriptCon_CompareBool_True_547 = true;

	private bool logic_uScriptCon_CompareBool_False_547 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_551 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_551;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_551;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_551;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_551;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_551;

	private bool logic_uScript_FlyTechUpAndAway_Out_551 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_552 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_552;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_552;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_552;

	private bool logic_uScript_AddMessage_Out_552 = true;

	private bool logic_uScript_AddMessage_Shown_552 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_555 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_555;

	private bool logic_uScriptAct_SetBool_Out_555 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_555 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_555 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_557 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_557;

	private object logic_uScript_SetEncounterTarget_visibleObject_557 = "";

	private bool logic_uScript_SetEncounterTarget_Out_557 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_560 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_560;

	private object logic_uScript_SetEncounterTarget_visibleObject_560 = "";

	private bool logic_uScript_SetEncounterTarget_Out_560 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_572;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_572 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_572 = "IntroSkipped";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_573;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_573 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_573 = "IntroTechInRange";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_574;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_574 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_574 = "HasPlayerLeftMissionArea";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_575;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_575 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_575 = "PlayerLeftMissionArea";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_576;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_576 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_576 = "msgTooEarlyPlayed";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_577;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_577 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_577 = "CrazedAmbushMsgTriggered";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_578;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_578 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_578 = "CubeDestroyedMsgPlayed";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_579;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_579 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_579 = "CrazedAmbushTriggered";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_580;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_580 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_580 = "MsgCubeIntroPlayed";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_581;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_581 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_581 = "PlayedTryAgainMsg";

	private uScript_AllowTechEnergyModuleSimultaneousActivation logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_585 = new uScript_AllowTechEnergyModuleSimultaneousActivation();

	private Tank logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_585;

	private TechSequencer.ChainType logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_585 = TechSequencer.ChainType.ShieldBubble;

	private bool logic_uScript_AllowTechEnergyModuleSimultaneousActivation_Out_585 = true;

	private uScript_AllowTechEnergyModuleSimultaneousActivation logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_586 = new uScript_AllowTechEnergyModuleSimultaneousActivation();

	private Tank logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_586;

	private TechSequencer.ChainType logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_586 = TechSequencer.ChainType.ShieldBubble;

	private bool logic_uScript_AllowTechEnergyModuleSimultaneousActivation_Out_586 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_587 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_587;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_587 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_587 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_588 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_588;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_588 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_588 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_589;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_589 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_589 = "FightRunning";

	private uScript_HideMissionTimerUI logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_590 = new uScript_HideMissionTimerUI();

	private GameObject logic_uScript_HideMissionTimerUI_owner_590;

	private bool logic_uScript_HideMissionTimerUI_Out_590 = true;

	private uScript_StopMissionTimer logic_uScript_StopMissionTimer_uScript_StopMissionTimer_591 = new uScript_StopMissionTimer();

	private GameObject logic_uScript_StopMissionTimer_owner_591;

	private bool logic_uScript_StopMissionTimer_Out_591 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_593;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_593 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_593 = "FightStarted";

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_597 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_597 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_597;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_597 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_597;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_597 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_597 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_597 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_597 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_601 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_601 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_601;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_601 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_601;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_601 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_601 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_601 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_601 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_603 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_603 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_603;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_603 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_603;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_603 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_603 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_603 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_603 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_605 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_605 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_605;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_605 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_605;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_605 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_605 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_605 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_605 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_609 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_609;

	private bool logic_uScriptCon_CompareBool_True_609 = true;

	private bool logic_uScriptCon_CompareBool_False_609 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_610 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_610 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_610;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_610 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_610;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_610 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_610 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_610 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_610 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_613 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_613;

	private bool logic_uScriptAct_SetBool_Out_613 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_613 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_613 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_616;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_616 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_616 = "GetRidOfCube";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_618 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_618;

	private bool logic_uScriptAct_SetBool_Out_618 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_618 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_618 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_620 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_620 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_620;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_620 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_620;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_620 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_620 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_620 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_620 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_621 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_621 = new Tank[0];

	private int logic_uScript_AccessListTech_index_621;

	private Tank logic_uScript_AccessListTech_value_621;

	private bool logic_uScript_AccessListTech_Out_621 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_625 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_625;

	private bool logic_uScriptAct_SetBool_Out_625 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_625 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_625 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_627;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_627 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_627 = "CubeisOK";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_634 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_634;

	private bool logic_uScriptAct_SetBool_Out_634 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_634 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_634 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_635 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_635 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_635;

	private bool logic_uScript_SetTankInvulnerable_Out_635 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_636 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_636 = new Tank[0];

	private int logic_uScript_AccessListTech_index_636;

	private Tank logic_uScript_AccessListTech_value_636;

	private bool logic_uScript_AccessListTech_Out_636 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_641 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_641 = new Tank[0];

	private int logic_uScript_AccessListTech_index_641;

	private Tank logic_uScript_AccessListTech_value_641;

	private bool logic_uScript_AccessListTech_Out_641 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_642 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_642 = new Tank[0];

	private int logic_uScript_AccessListTech_index_642;

	private Tank logic_uScript_AccessListTech_value_642;

	private bool logic_uScript_AccessListTech_Out_642 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_645 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_645 = new Tank[0];

	private int logic_uScript_AccessListTech_index_645 = 1;

	private Tank logic_uScript_AccessListTech_value_645;

	private bool logic_uScript_AccessListTech_Out_645 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_648 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_648 = new Tank[0];

	private int logic_uScript_AccessListTech_index_648;

	private Tank logic_uScript_AccessListTech_value_648;

	private bool logic_uScript_AccessListTech_Out_648 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_651 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_651;

	private bool logic_uScriptAct_SetBool_Out_651 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_651 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_651 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_653 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_653 = 0.5f;

	private bool logic_uScript_Wait_repeat_653;

	private bool logic_uScript_Wait_Waited_653 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_654 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_654;

	private bool logic_uScriptCon_CompareBool_True_654 = true;

	private bool logic_uScriptCon_CompareBool_False_654 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_657 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_657;

	private bool logic_uScriptAct_SetBool_Out_657 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_657 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_657 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_658 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_658;

	private Tank logic_uScript_SetTankInvulnerable_tank_658;

	private bool logic_uScript_SetTankInvulnerable_Out_658 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_661 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_661;

	private bool logic_uScriptAct_SetBool_Out_661 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_661 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_661 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_664;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_664 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_664 = "GetRidOfCube";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_665;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_665 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_665 = "WaitingForTechClear";

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_669 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_669 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_669;

	private bool logic_uScript_SetTankInvulnerable_Out_669 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_670 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_670 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_670 = -2;

	private bool logic_uScript_SetTechsTeam_Out_670 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_674 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_674;

	private bool logic_uScriptCon_CompareBool_True_674 = true;

	private bool logic_uScriptCon_CompareBool_False_674 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_675 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_675;

	private bool logic_uScriptAct_SetBool_Out_675 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_675 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_675 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_678;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_678 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_678 = "TechInvulOnLoad";

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_683 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_683 = new Tank[0];

	private int logic_uScript_AccessListTech_index_683;

	private Tank logic_uScript_AccessListTech_value_683;

	private bool logic_uScript_AccessListTech_Out_683 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_684 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_684 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_684;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_684 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_684;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_684 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_684 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_684 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_684 = true;

	private uScript_ResetMissionTimerTimeElapsed logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_686 = new uScript_ResetMissionTimerTimeElapsed();

	private GameObject logic_uScript_ResetMissionTimerTimeElapsed_owner_686;

	private float logic_uScript_ResetMissionTimerTimeElapsed_startTime_686;

	private bool logic_uScript_ResetMissionTimerTimeElapsed_Out_686 = true;

	private uScript_ResetMissionTimer logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_687 = new uScript_ResetMissionTimer();

	private GameObject logic_uScript_ResetMissionTimer_owner_687;

	private bool logic_uScript_ResetMissionTimer_Out_687 = true;

	private uScript_StartMissionTimer logic_uScript_StartMissionTimer_uScript_StartMissionTimer_689 = new uScript_StartMissionTimer();

	private GameObject logic_uScript_StartMissionTimer_owner_689;

	private float logic_uScript_StartMissionTimer_startTime_689;

	private bool logic_uScript_StartMissionTimer_Out_689 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_692 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_692;

	private bool logic_uScriptAct_SetBool_Out_692 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_692 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_692 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_693 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_693;

	private bool logic_uScriptAct_SetBool_Out_693 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_693 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_693 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_694 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_694;

	private bool logic_uScriptAct_SetBool_Out_694 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_694 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_694 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_695 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_695;

	private bool logic_uScriptAct_SetBool_Out_695 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_695 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_695 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_696 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_696;

	private bool logic_uScriptAct_SetBool_Out_696 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_696 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_696 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_697 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_697;

	private bool logic_uScriptAct_SetBool_Out_697 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_697 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_697 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_698 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_698;

	private bool logic_uScriptAct_SetBool_Out_698 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_698 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_698 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_699 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_699;

	private bool logic_uScriptAct_SetBool_Out_699 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_699 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_699 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_709 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_709 = 1;

	private int logic_uScriptAct_SetInt_Target_709;

	private bool logic_uScriptAct_SetInt_Out_709 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_714 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_714 = 1;

	private int logic_uScriptAct_SetInt_Target_714;

	private bool logic_uScriptAct_SetInt_Out_714 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_715 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_715 = 1;

	private int logic_uScriptAct_SetInt_Target_715;

	private bool logic_uScriptAct_SetInt_Out_715 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_716 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_716 = 1;

	private int logic_uScriptAct_SetInt_Target_716;

	private bool logic_uScriptAct_SetInt_Out_716 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_717 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_717 = 1;

	private int logic_uScriptAct_SetInt_Target_717;

	private bool logic_uScriptAct_SetInt_Out_717 = true;

	private uScript_AllowTechEnergyModuleSimultaneousActivation logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_720 = new uScript_AllowTechEnergyModuleSimultaneousActivation();

	private Tank logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_720;

	private TechSequencer.ChainType logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_720;

	private bool logic_uScript_AllowTechEnergyModuleSimultaneousActivation_Out_720 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_721 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_721;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_725 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_725 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_725;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_725 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_725;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_725 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_725 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_725 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_725 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_742 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_742 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_747 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_747 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_753 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_753 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_753 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_754 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_754 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_755 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_755 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_755 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_756 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_756 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_756 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_757 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_757 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_758 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_758 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_759 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_759 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_759 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_760 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_760 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_760 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_762 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_762;

	private bool logic_uScriptAct_SetBool_Out_762 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_762 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_762 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_763 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_763;

	private bool logic_uScriptAct_SetBool_Out_763 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_763 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_763 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_765 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_765 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_765 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_766 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_766 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_766 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_767 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_767 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_768 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_768 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_768 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_770 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_770 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_770 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_770 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_770 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_770 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_770 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_772 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_772;

	private bool logic_uScriptAct_SetBool_Out_772 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_772 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_772 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_775 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_775;

	private bool logic_uScriptAct_SetBool_Out_775 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_775 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_775 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_776 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_776;

	private bool logic_uScriptAct_SetBool_Out_776 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_776 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_776 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_777 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_777 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_777 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_780 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_780;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_780;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_780;

	private bool logic_uScript_AddMessage_Out_780 = true;

	private bool logic_uScript_AddMessage_Shown_780 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_782 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_782;

	private bool logic_uScriptAct_SetBool_Out_782 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_782 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_782 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_783 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_783;

	private bool logic_uScript_RemoveTech_Out_783 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_785 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_785 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_785 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_785 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_785 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_785 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_785 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_786 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_786 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_786 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_787 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_787;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_787;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_787;

	private bool logic_uScript_AddMessage_Out_787 = true;

	private bool logic_uScript_AddMessage_Shown_787 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_791 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_791 = true;

	private uScript_SetTechExplodeDetachingBlocks logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_792 = new uScript_SetTechExplodeDetachingBlocks();

	private Tank logic_uScript_SetTechExplodeDetachingBlocks_tech_792;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_792 = true;

	private float logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_792 = 0.1f;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_Out_792 = true;

	private uScript_SetTechExplodeDetachingBlocks logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_794 = new uScript_SetTechExplodeDetachingBlocks();

	private Tank logic_uScript_SetTechExplodeDetachingBlocks_tech_794;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_794 = true;

	private float logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_794 = 0.1f;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_Out_794 = true;

	private uScript_SetTechExplodeDetachingBlocks logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_796 = new uScript_SetTechExplodeDetachingBlocks();

	private Tank logic_uScript_SetTechExplodeDetachingBlocks_tech_796;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_796 = true;

	private float logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_796 = 0.1f;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_Out_796 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_798 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_798;

	private bool logic_uScriptCon_CompareBool_True_798 = true;

	private bool logic_uScriptCon_CompareBool_False_798 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_799 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_799;

	private bool logic_uScriptCon_CompareBool_True_799 = true;

	private bool logic_uScriptCon_CompareBool_False_799 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_800 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_800;

	private bool logic_uScriptAct_SetBool_Out_800 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_800 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_800 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_802 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_802;

	private bool logic_uScriptAct_SetBool_Out_802 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_802 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_802 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_804 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_804;

	private bool logic_uScriptAct_SetBool_Out_804 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_804 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_804 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_807 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_807 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_811 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_811;

	private int logic_uScript_PlayDialogue_progress_811;

	private bool logic_uScript_PlayDialogue_Out_811 = true;

	private bool logic_uScript_PlayDialogue_Shown_811 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_811 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_815 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_815;

	private int logic_uScriptAct_SetInt_Target_815;

	private bool logic_uScriptAct_SetInt_Out_815 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_816 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_816;

	private int logic_uScriptAct_SetInt_Target_816;

	private bool logic_uScriptAct_SetInt_Out_816 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_818 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_818;

	private int logic_uScriptAct_SetInt_Target_818;

	private bool logic_uScriptAct_SetInt_Out_818 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_824 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_824;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_824 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_824 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_824 = true;

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
		if (null == owner_Connection_15 || !m_RegisteredForEvents)
		{
			owner_Connection_15 = parentGameObject;
		}
		if (null == owner_Connection_19 || !m_RegisteredForEvents)
		{
			owner_Connection_19 = parentGameObject;
		}
		if (null == owner_Connection_24 || !m_RegisteredForEvents)
		{
			owner_Connection_24 = parentGameObject;
		}
		if (null == owner_Connection_26 || !m_RegisteredForEvents)
		{
			owner_Connection_26 = parentGameObject;
		}
		if (null == owner_Connection_39 || !m_RegisteredForEvents)
		{
			owner_Connection_39 = parentGameObject;
		}
		if (null == owner_Connection_54 || !m_RegisteredForEvents)
		{
			owner_Connection_54 = parentGameObject;
		}
		if (null == owner_Connection_67 || !m_RegisteredForEvents)
		{
			owner_Connection_67 = parentGameObject;
		}
		if (null == owner_Connection_69 || !m_RegisteredForEvents)
		{
			owner_Connection_69 = parentGameObject;
		}
		if (null == owner_Connection_79 || !m_RegisteredForEvents)
		{
			owner_Connection_79 = parentGameObject;
		}
		if (null == owner_Connection_85 || !m_RegisteredForEvents)
		{
			owner_Connection_85 = parentGameObject;
		}
		if (null == owner_Connection_88 || !m_RegisteredForEvents)
		{
			owner_Connection_88 = parentGameObject;
		}
		if (null == owner_Connection_93 || !m_RegisteredForEvents)
		{
			owner_Connection_93 = parentGameObject;
		}
		if (null == owner_Connection_100 || !m_RegisteredForEvents)
		{
			owner_Connection_100 = parentGameObject;
		}
		if (null == owner_Connection_109 || !m_RegisteredForEvents)
		{
			owner_Connection_109 = parentGameObject;
		}
		if (null == owner_Connection_124 || !m_RegisteredForEvents)
		{
			owner_Connection_124 = parentGameObject;
		}
		if (null == owner_Connection_127 || !m_RegisteredForEvents)
		{
			owner_Connection_127 = parentGameObject;
		}
		if (null == owner_Connection_134 || !m_RegisteredForEvents)
		{
			owner_Connection_134 = parentGameObject;
		}
		if (null == owner_Connection_166 || !m_RegisteredForEvents)
		{
			owner_Connection_166 = parentGameObject;
		}
		if (null == owner_Connection_179 || !m_RegisteredForEvents)
		{
			owner_Connection_179 = parentGameObject;
		}
		if (null == owner_Connection_358 || !m_RegisteredForEvents)
		{
			owner_Connection_358 = parentGameObject;
		}
		if (null == owner_Connection_415 || !m_RegisteredForEvents)
		{
			owner_Connection_415 = parentGameObject;
		}
		if (null == owner_Connection_441 || !m_RegisteredForEvents)
		{
			owner_Connection_441 = parentGameObject;
		}
		if (null == owner_Connection_448 || !m_RegisteredForEvents)
		{
			owner_Connection_448 = parentGameObject;
		}
		if (null == owner_Connection_510 || !m_RegisteredForEvents)
		{
			owner_Connection_510 = parentGameObject;
		}
		if (null == owner_Connection_541 || !m_RegisteredForEvents)
		{
			owner_Connection_541 = parentGameObject;
		}
		if (null == owner_Connection_558 || !m_RegisteredForEvents)
		{
			owner_Connection_558 = parentGameObject;
		}
		if (null == owner_Connection_561 || !m_RegisteredForEvents)
		{
			owner_Connection_561 = parentGameObject;
		}
		if (null == owner_Connection_594 || !m_RegisteredForEvents)
		{
			owner_Connection_594 = parentGameObject;
		}
		if (null == owner_Connection_595 || !m_RegisteredForEvents)
		{
			owner_Connection_595 = parentGameObject;
		}
		if (null == owner_Connection_598 || !m_RegisteredForEvents)
		{
			owner_Connection_598 = parentGameObject;
		}
		if (null == owner_Connection_600 || !m_RegisteredForEvents)
		{
			owner_Connection_600 = parentGameObject;
		}
		if (null == owner_Connection_604 || !m_RegisteredForEvents)
		{
			owner_Connection_604 = parentGameObject;
		}
		if (null == owner_Connection_607 || !m_RegisteredForEvents)
		{
			owner_Connection_607 = parentGameObject;
		}
		if (null == owner_Connection_611 || !m_RegisteredForEvents)
		{
			owner_Connection_611 = parentGameObject;
		}
		if (null == owner_Connection_619 || !m_RegisteredForEvents)
		{
			owner_Connection_619 = parentGameObject;
		}
		if (null == owner_Connection_681 || !m_RegisteredForEvents)
		{
			owner_Connection_681 = parentGameObject;
		}
		if (null == owner_Connection_688 || !m_RegisteredForEvents)
		{
			owner_Connection_688 = parentGameObject;
		}
		if (null == owner_Connection_690 || !m_RegisteredForEvents)
		{
			owner_Connection_690 = parentGameObject;
		}
		if (null == owner_Connection_726 || !m_RegisteredForEvents)
		{
			owner_Connection_726 = parentGameObject;
		}
		if (null == owner_Connection_808 || !m_RegisteredForEvents)
		{
			owner_Connection_808 = parentGameObject;
		}
		if (null == owner_Connection_809 || !m_RegisteredForEvents)
		{
			owner_Connection_809 = parentGameObject;
		}
		if (null == owner_Connection_810 || !m_RegisteredForEvents)
		{
			owner_Connection_810 = parentGameObject;
		}
		if (null == owner_Connection_825 || !m_RegisteredForEvents)
		{
			owner_Connection_825 = parentGameObject;
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
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_16.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_20.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_28.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_29.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_30.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_32.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_34.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_44.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_45.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_53.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_57.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_60.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_62.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_64.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_65.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_66.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_68.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_70.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_78.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_84.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_89.SetParent(g);
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_90.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_91.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.SetParent(g);
		logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_94.SetParent(g);
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_96.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_97.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_98.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_99.SetParent(g);
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_105.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_106.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_112.SetParent(g);
		logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_113.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_115.SetParent(g);
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_116.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_120.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_123.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_126.SetParent(g);
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_128.SetParent(g);
		logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_130.SetParent(g);
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_131.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.SetParent(g);
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_135.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_137.SetParent(g);
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_139.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_141.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_145.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_148.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_149.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_152.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_154.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_156.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_157.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_160.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_162.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_164.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_165.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_169.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_170.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_171.SetParent(g);
		logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_174.SetParent(g);
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_176.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_178.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_180.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_181.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_183.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_185.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_189.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_191.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_193.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_196.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_199.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_200.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_202.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_206.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_209.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_212.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_214.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_215.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_217.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_221.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_222.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_224.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_226.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_229.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_230.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_237.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_241.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_242.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_244.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_246.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_249.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_252.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_253.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_304.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_308.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_310.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_313.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_315.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_320.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_321.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_324.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_327.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_329.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_334.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_335.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_339.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_342.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_344.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_345.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_347.SetParent(g);
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_349.SetParent(g);
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_352.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_354.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_357.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_359.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_361.SetParent(g);
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_362.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_366.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_367.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_368.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_372.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_375.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_378.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_380.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_382.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_384.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_386.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_388.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_390.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_392.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_396.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_400.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_403.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_405.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_407.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_411.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_413.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_417.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_418.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_419.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_423.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_424.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_426.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_428.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_429.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_434.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_437.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_438.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_440.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_443.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_444.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_445.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_447.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_451.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_454.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_456.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_459.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_460.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_462.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_465.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_469.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_472.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_473.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_474.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_475.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_479.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_480.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_482.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_487.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_489.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_491.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_495.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_496.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_499.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_501.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_503.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_506.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_508.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_512.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_514.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_516.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_518.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_519.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_522.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_523.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_524.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_528.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_531.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_532.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_533.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_534.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_536.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_539.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_543.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_547.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_551.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_552.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_555.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_557.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_560.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.SetParent(g);
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_585.SetParent(g);
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_586.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_587.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_588.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.SetParent(g);
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_590.SetParent(g);
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_591.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_597.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_601.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_603.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_605.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_609.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_610.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_613.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_618.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_620.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_621.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_625.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_634.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_635.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_636.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_641.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_642.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_645.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_648.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_651.SetParent(g);
		logic_uScript_Wait_uScript_Wait_653.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_654.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_657.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_658.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_661.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_669.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_670.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_674.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_675.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_683.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_684.SetParent(g);
		logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_686.SetParent(g);
		logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_687.SetParent(g);
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_689.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_692.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_693.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_694.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_695.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_696.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_697.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_698.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_699.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_709.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_714.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_715.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_716.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_717.SetParent(g);
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_720.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_721.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_725.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_742.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_747.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_753.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_754.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_755.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_756.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_757.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_758.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_759.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_760.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_762.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_763.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_765.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_766.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_767.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_768.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_770.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_772.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_775.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_776.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_777.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_780.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_782.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_783.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_785.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_786.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_787.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_791.SetParent(g);
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_792.SetParent(g);
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_794.SetParent(g);
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_796.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_798.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_799.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_800.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_802.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_804.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_807.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_811.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_815.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_816.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_818.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_824.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_5 = parentGameObject;
		owner_Connection_15 = parentGameObject;
		owner_Connection_19 = parentGameObject;
		owner_Connection_24 = parentGameObject;
		owner_Connection_26 = parentGameObject;
		owner_Connection_39 = parentGameObject;
		owner_Connection_54 = parentGameObject;
		owner_Connection_67 = parentGameObject;
		owner_Connection_69 = parentGameObject;
		owner_Connection_79 = parentGameObject;
		owner_Connection_85 = parentGameObject;
		owner_Connection_88 = parentGameObject;
		owner_Connection_93 = parentGameObject;
		owner_Connection_100 = parentGameObject;
		owner_Connection_109 = parentGameObject;
		owner_Connection_124 = parentGameObject;
		owner_Connection_127 = parentGameObject;
		owner_Connection_134 = parentGameObject;
		owner_Connection_166 = parentGameObject;
		owner_Connection_179 = parentGameObject;
		owner_Connection_358 = parentGameObject;
		owner_Connection_415 = parentGameObject;
		owner_Connection_441 = parentGameObject;
		owner_Connection_448 = parentGameObject;
		owner_Connection_510 = parentGameObject;
		owner_Connection_541 = parentGameObject;
		owner_Connection_558 = parentGameObject;
		owner_Connection_561 = parentGameObject;
		owner_Connection_594 = parentGameObject;
		owner_Connection_595 = parentGameObject;
		owner_Connection_598 = parentGameObject;
		owner_Connection_600 = parentGameObject;
		owner_Connection_604 = parentGameObject;
		owner_Connection_607 = parentGameObject;
		owner_Connection_611 = parentGameObject;
		owner_Connection_619 = parentGameObject;
		owner_Connection_681 = parentGameObject;
		owner_Connection_688 = parentGameObject;
		owner_Connection_690 = parentGameObject;
		owner_Connection_726 = parentGameObject;
		owner_Connection_808 = parentGameObject;
		owner_Connection_809 = parentGameObject;
		owner_Connection_810 = parentGameObject;
		owner_Connection_825 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_66.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_359.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_407.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_495.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_721.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out += SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out += SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Save_Out += SubGraph_SaveLoadInt_Save_Out_11;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Load_Out += SubGraph_SaveLoadInt_Load_Out_11;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_11;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_66.Out += SubGraph_CompleteObjectiveStage_Out_66;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Save_Out += SubGraph_SaveLoadBool_Save_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Load_Out += SubGraph_SaveLoadBool_Load_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Save_Out += SubGraph_SaveLoadBool_Save_Out_259;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Load_Out += SubGraph_SaveLoadBool_Load_Out_259;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_259;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Save_Out += SubGraph_SaveLoadBool_Save_Out_261;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Load_Out += SubGraph_SaveLoadBool_Load_Out_261;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_261;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.Save_Out += SubGraph_SaveLoadBool_Save_Out_263;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.Load_Out += SubGraph_SaveLoadBool_Load_Out_263;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_263;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.Save_Out += SubGraph_SaveLoadBool_Save_Out_285;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.Load_Out += SubGraph_SaveLoadBool_Load_Out_285;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_285;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.Save_Out += SubGraph_SaveLoadBool_Save_Out_286;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.Load_Out += SubGraph_SaveLoadBool_Load_Out_286;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_286;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Save_Out += SubGraph_SaveLoadBool_Save_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Load_Out += SubGraph_SaveLoadBool_Load_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Save_Out += SubGraph_SaveLoadBool_Save_Out_288;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Load_Out += SubGraph_SaveLoadBool_Load_Out_288;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_288;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.Save_Out += SubGraph_SaveLoadBool_Save_Out_289;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.Load_Out += SubGraph_SaveLoadBool_Load_Out_289;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_289;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.Save_Out += SubGraph_SaveLoadBool_Save_Out_290;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.Load_Out += SubGraph_SaveLoadBool_Load_Out_290;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_290;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.Save_Out += SubGraph_SaveLoadBool_Save_Out_291;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.Load_Out += SubGraph_SaveLoadBool_Load_Out_291;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_291;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.Save_Out += SubGraph_SaveLoadBool_Save_Out_292;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.Load_Out += SubGraph_SaveLoadBool_Load_Out_292;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_292;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.Save_Out += SubGraph_SaveLoadBool_Save_Out_293;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.Load_Out += SubGraph_SaveLoadBool_Load_Out_293;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_293;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.Save_Out += SubGraph_SaveLoadBool_Save_Out_294;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.Load_Out += SubGraph_SaveLoadBool_Load_Out_294;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_294;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.Save_Out += SubGraph_SaveLoadBool_Save_Out_295;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.Load_Out += SubGraph_SaveLoadBool_Load_Out_295;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_295;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Save_Out += SubGraph_SaveLoadBool_Save_Out_296;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Load_Out += SubGraph_SaveLoadBool_Load_Out_296;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_296;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Save_Out += SubGraph_SaveLoadBool_Save_Out_297;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Load_Out += SubGraph_SaveLoadBool_Load_Out_297;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_297;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.Save_Out += SubGraph_SaveLoadBool_Save_Out_298;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.Load_Out += SubGraph_SaveLoadBool_Load_Out_298;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_298;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Save_Out += SubGraph_SaveLoadBool_Save_Out_299;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Load_Out += SubGraph_SaveLoadBool_Load_Out_299;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_299;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.Save_Out += SubGraph_SaveLoadBool_Save_Out_300;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.Load_Out += SubGraph_SaveLoadBool_Load_Out_300;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_300;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Save_Out += SubGraph_SaveLoadBool_Save_Out_301;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Load_Out += SubGraph_SaveLoadBool_Load_Out_301;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_301;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Save_Out += SubGraph_SaveLoadBool_Save_Out_302;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Load_Out += SubGraph_SaveLoadBool_Load_Out_302;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_302;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Save_Out += SubGraph_SaveLoadBool_Save_Out_303;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Load_Out += SubGraph_SaveLoadBool_Load_Out_303;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_303;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.Output1 += uScriptCon_ManualSwitch_Output1_336;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.Output2 += uScriptCon_ManualSwitch_Output2_336;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.Output3 += uScriptCon_ManualSwitch_Output3_336;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.Output4 += uScriptCon_ManualSwitch_Output4_336;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.Output5 += uScriptCon_ManualSwitch_Output5_336;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.Output6 += uScriptCon_ManualSwitch_Output6_336;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.Output7 += uScriptCon_ManualSwitch_Output7_336;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.Output8 += uScriptCon_ManualSwitch_Output8_336;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_359.Out += SubGraph_CompleteObjectiveStage_Out_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output1 += uScriptCon_ManualSwitch_Output1_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output2 += uScriptCon_ManualSwitch_Output2_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output3 += uScriptCon_ManualSwitch_Output3_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output4 += uScriptCon_ManualSwitch_Output4_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output5 += uScriptCon_ManualSwitch_Output5_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output6 += uScriptCon_ManualSwitch_Output6_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output7 += uScriptCon_ManualSwitch_Output7_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output8 += uScriptCon_ManualSwitch_Output8_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.Output1 += uScriptCon_ManualSwitch_Output1_395;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.Output2 += uScriptCon_ManualSwitch_Output2_395;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.Output3 += uScriptCon_ManualSwitch_Output3_395;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.Output4 += uScriptCon_ManualSwitch_Output4_395;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.Output5 += uScriptCon_ManualSwitch_Output5_395;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.Output6 += uScriptCon_ManualSwitch_Output6_395;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.Output7 += uScriptCon_ManualSwitch_Output7_395;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.Output8 += uScriptCon_ManualSwitch_Output8_395;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.Output1 += uScriptCon_ManualSwitch_Output1_399;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.Output2 += uScriptCon_ManualSwitch_Output2_399;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.Output3 += uScriptCon_ManualSwitch_Output3_399;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.Output4 += uScriptCon_ManualSwitch_Output4_399;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.Output5 += uScriptCon_ManualSwitch_Output5_399;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.Output6 += uScriptCon_ManualSwitch_Output6_399;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.Output7 += uScriptCon_ManualSwitch_Output7_399;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.Output8 += uScriptCon_ManualSwitch_Output8_399;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_407.Out += SubGraph_CompleteObjectiveStage_Out_407;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output1 += uScriptCon_ManualSwitch_Output1_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output2 += uScriptCon_ManualSwitch_Output2_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output3 += uScriptCon_ManualSwitch_Output3_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output4 += uScriptCon_ManualSwitch_Output4_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output5 += uScriptCon_ManualSwitch_Output5_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output6 += uScriptCon_ManualSwitch_Output6_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output7 += uScriptCon_ManualSwitch_Output7_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output8 += uScriptCon_ManualSwitch_Output8_485;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_495.Out += SubGraph_CompleteObjectiveStage_Out_495;
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
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.Save_Out += SubGraph_SaveLoadBool_Save_Out_578;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.Load_Out += SubGraph_SaveLoadBool_Load_Out_578;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_578;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.Save_Out += SubGraph_SaveLoadBool_Save_Out_579;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.Load_Out += SubGraph_SaveLoadBool_Load_Out_579;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_579;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.Save_Out += SubGraph_SaveLoadBool_Save_Out_580;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.Load_Out += SubGraph_SaveLoadBool_Load_Out_580;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_580;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.Save_Out += SubGraph_SaveLoadBool_Save_Out_581;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.Load_Out += SubGraph_SaveLoadBool_Load_Out_581;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_581;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.Save_Out += SubGraph_SaveLoadBool_Save_Out_589;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.Load_Out += SubGraph_SaveLoadBool_Load_Out_589;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_589;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Save_Out += SubGraph_SaveLoadBool_Save_Out_593;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Load_Out += SubGraph_SaveLoadBool_Load_Out_593;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_593;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Save_Out += SubGraph_SaveLoadBool_Save_Out_616;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Load_Out += SubGraph_SaveLoadBool_Load_Out_616;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_616;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.Save_Out += SubGraph_SaveLoadBool_Save_Out_627;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.Load_Out += SubGraph_SaveLoadBool_Load_Out_627;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_627;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Save_Out += SubGraph_SaveLoadBool_Save_Out_664;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Load_Out += SubGraph_SaveLoadBool_Load_Out_664;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_664;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Save_Out += SubGraph_SaveLoadBool_Save_Out_665;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Load_Out += SubGraph_SaveLoadBool_Load_Out_665;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_665;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.Save_Out += SubGraph_SaveLoadBool_Save_Out_678;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.Load_Out += SubGraph_SaveLoadBool_Load_Out_678;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_678;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_721.Out += SubGraph_LoadObjectiveStates_Out_721;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_66.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_359.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_407.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_495.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_721.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_65.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_66.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_359.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_392.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_407.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_413.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_454.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_495.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_551.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_721.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_811.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_29.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_34.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_44.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_60.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_66.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_78.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_181.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_206.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_217.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_226.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_237.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_241.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_252.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_308.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_310.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_313.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_315.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_320.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_321.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_324.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_327.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_329.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_334.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_359.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_367.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_372.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_407.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_438.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_445.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_451.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_456.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_460.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_469.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_473.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_487.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_495.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_499.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_506.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_523.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_552.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_635.OnDisable();
		logic_uScript_Wait_uScript_Wait_653.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_658.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_669.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_721.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_753.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_755.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_756.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_759.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_760.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_765.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_766.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_768.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_777.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_780.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_786.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_787.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_811.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_824.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_66.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_359.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_407.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_495.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_721.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_66.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_359.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_407.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_495.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_721.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out -= SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out -= SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Save_Out -= SubGraph_SaveLoadInt_Save_Out_11;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Load_Out -= SubGraph_SaveLoadInt_Load_Out_11;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_11;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_66.Out -= SubGraph_CompleteObjectiveStage_Out_66;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Save_Out -= SubGraph_SaveLoadBool_Save_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Load_Out -= SubGraph_SaveLoadBool_Load_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_257;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Save_Out -= SubGraph_SaveLoadBool_Save_Out_259;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Load_Out -= SubGraph_SaveLoadBool_Load_Out_259;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_259;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Save_Out -= SubGraph_SaveLoadBool_Save_Out_261;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Load_Out -= SubGraph_SaveLoadBool_Load_Out_261;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_261;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.Save_Out -= SubGraph_SaveLoadBool_Save_Out_263;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.Load_Out -= SubGraph_SaveLoadBool_Load_Out_263;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_263;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.Save_Out -= SubGraph_SaveLoadBool_Save_Out_285;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.Load_Out -= SubGraph_SaveLoadBool_Load_Out_285;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_285;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.Save_Out -= SubGraph_SaveLoadBool_Save_Out_286;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.Load_Out -= SubGraph_SaveLoadBool_Load_Out_286;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_286;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Save_Out -= SubGraph_SaveLoadBool_Save_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Load_Out -= SubGraph_SaveLoadBool_Load_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Save_Out -= SubGraph_SaveLoadBool_Save_Out_288;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Load_Out -= SubGraph_SaveLoadBool_Load_Out_288;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_288;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.Save_Out -= SubGraph_SaveLoadBool_Save_Out_289;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.Load_Out -= SubGraph_SaveLoadBool_Load_Out_289;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_289;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.Save_Out -= SubGraph_SaveLoadBool_Save_Out_290;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.Load_Out -= SubGraph_SaveLoadBool_Load_Out_290;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_290;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.Save_Out -= SubGraph_SaveLoadBool_Save_Out_291;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.Load_Out -= SubGraph_SaveLoadBool_Load_Out_291;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_291;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.Save_Out -= SubGraph_SaveLoadBool_Save_Out_292;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.Load_Out -= SubGraph_SaveLoadBool_Load_Out_292;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_292;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.Save_Out -= SubGraph_SaveLoadBool_Save_Out_293;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.Load_Out -= SubGraph_SaveLoadBool_Load_Out_293;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_293;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.Save_Out -= SubGraph_SaveLoadBool_Save_Out_294;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.Load_Out -= SubGraph_SaveLoadBool_Load_Out_294;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_294;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.Save_Out -= SubGraph_SaveLoadBool_Save_Out_295;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.Load_Out -= SubGraph_SaveLoadBool_Load_Out_295;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_295;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Save_Out -= SubGraph_SaveLoadBool_Save_Out_296;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Load_Out -= SubGraph_SaveLoadBool_Load_Out_296;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_296;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Save_Out -= SubGraph_SaveLoadBool_Save_Out_297;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Load_Out -= SubGraph_SaveLoadBool_Load_Out_297;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_297;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.Save_Out -= SubGraph_SaveLoadBool_Save_Out_298;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.Load_Out -= SubGraph_SaveLoadBool_Load_Out_298;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_298;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Save_Out -= SubGraph_SaveLoadBool_Save_Out_299;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Load_Out -= SubGraph_SaveLoadBool_Load_Out_299;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_299;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.Save_Out -= SubGraph_SaveLoadBool_Save_Out_300;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.Load_Out -= SubGraph_SaveLoadBool_Load_Out_300;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_300;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Save_Out -= SubGraph_SaveLoadBool_Save_Out_301;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Load_Out -= SubGraph_SaveLoadBool_Load_Out_301;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_301;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Save_Out -= SubGraph_SaveLoadBool_Save_Out_302;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Load_Out -= SubGraph_SaveLoadBool_Load_Out_302;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_302;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Save_Out -= SubGraph_SaveLoadBool_Save_Out_303;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Load_Out -= SubGraph_SaveLoadBool_Load_Out_303;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_303;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.Output1 -= uScriptCon_ManualSwitch_Output1_336;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.Output2 -= uScriptCon_ManualSwitch_Output2_336;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.Output3 -= uScriptCon_ManualSwitch_Output3_336;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.Output4 -= uScriptCon_ManualSwitch_Output4_336;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.Output5 -= uScriptCon_ManualSwitch_Output5_336;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.Output6 -= uScriptCon_ManualSwitch_Output6_336;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.Output7 -= uScriptCon_ManualSwitch_Output7_336;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.Output8 -= uScriptCon_ManualSwitch_Output8_336;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_359.Out -= SubGraph_CompleteObjectiveStage_Out_359;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output1 -= uScriptCon_ManualSwitch_Output1_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output2 -= uScriptCon_ManualSwitch_Output2_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output3 -= uScriptCon_ManualSwitch_Output3_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output4 -= uScriptCon_ManualSwitch_Output4_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output5 -= uScriptCon_ManualSwitch_Output5_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output6 -= uScriptCon_ManualSwitch_Output6_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output7 -= uScriptCon_ManualSwitch_Output7_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.Output8 -= uScriptCon_ManualSwitch_Output8_376;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.Output1 -= uScriptCon_ManualSwitch_Output1_395;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.Output2 -= uScriptCon_ManualSwitch_Output2_395;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.Output3 -= uScriptCon_ManualSwitch_Output3_395;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.Output4 -= uScriptCon_ManualSwitch_Output4_395;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.Output5 -= uScriptCon_ManualSwitch_Output5_395;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.Output6 -= uScriptCon_ManualSwitch_Output6_395;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.Output7 -= uScriptCon_ManualSwitch_Output7_395;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.Output8 -= uScriptCon_ManualSwitch_Output8_395;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.Output1 -= uScriptCon_ManualSwitch_Output1_399;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.Output2 -= uScriptCon_ManualSwitch_Output2_399;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.Output3 -= uScriptCon_ManualSwitch_Output3_399;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.Output4 -= uScriptCon_ManualSwitch_Output4_399;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.Output5 -= uScriptCon_ManualSwitch_Output5_399;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.Output6 -= uScriptCon_ManualSwitch_Output6_399;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.Output7 -= uScriptCon_ManualSwitch_Output7_399;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.Output8 -= uScriptCon_ManualSwitch_Output8_399;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_407.Out -= SubGraph_CompleteObjectiveStage_Out_407;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output1 -= uScriptCon_ManualSwitch_Output1_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output2 -= uScriptCon_ManualSwitch_Output2_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output3 -= uScriptCon_ManualSwitch_Output3_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output4 -= uScriptCon_ManualSwitch_Output4_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output5 -= uScriptCon_ManualSwitch_Output5_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output6 -= uScriptCon_ManualSwitch_Output6_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output7 -= uScriptCon_ManualSwitch_Output7_485;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.Output8 -= uScriptCon_ManualSwitch_Output8_485;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_495.Out -= SubGraph_CompleteObjectiveStage_Out_495;
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
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.Save_Out -= SubGraph_SaveLoadBool_Save_Out_578;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.Load_Out -= SubGraph_SaveLoadBool_Load_Out_578;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_578;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.Save_Out -= SubGraph_SaveLoadBool_Save_Out_579;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.Load_Out -= SubGraph_SaveLoadBool_Load_Out_579;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_579;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.Save_Out -= SubGraph_SaveLoadBool_Save_Out_580;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.Load_Out -= SubGraph_SaveLoadBool_Load_Out_580;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_580;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.Save_Out -= SubGraph_SaveLoadBool_Save_Out_581;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.Load_Out -= SubGraph_SaveLoadBool_Load_Out_581;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_581;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.Save_Out -= SubGraph_SaveLoadBool_Save_Out_589;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.Load_Out -= SubGraph_SaveLoadBool_Load_Out_589;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_589;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Save_Out -= SubGraph_SaveLoadBool_Save_Out_593;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Load_Out -= SubGraph_SaveLoadBool_Load_Out_593;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_593;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Save_Out -= SubGraph_SaveLoadBool_Save_Out_616;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Load_Out -= SubGraph_SaveLoadBool_Load_Out_616;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_616;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.Save_Out -= SubGraph_SaveLoadBool_Save_Out_627;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.Load_Out -= SubGraph_SaveLoadBool_Load_Out_627;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_627;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Save_Out -= SubGraph_SaveLoadBool_Save_Out_664;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Load_Out -= SubGraph_SaveLoadBool_Load_Out_664;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_664;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Save_Out -= SubGraph_SaveLoadBool_Save_Out_665;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Load_Out -= SubGraph_SaveLoadBool_Load_Out_665;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_665;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.Save_Out -= SubGraph_SaveLoadBool_Save_Out_678;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.Load_Out -= SubGraph_SaveLoadBool_Load_Out_678;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_678;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_721.Out -= SubGraph_LoadObjectiveStates_Out_721;
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

	private void SubGraph_CompleteObjectiveStage_Out_66(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_66 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_66;
		Relay_Out_66();
	}

	private void SubGraph_SaveLoadBool_Save_Out_257(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = e.boolean;
		local_NPCTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_257;
		Relay_Save_Out_257();
	}

	private void SubGraph_SaveLoadBool_Load_Out_257(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = e.boolean;
		local_NPCTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_257;
		Relay_Load_Out_257();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_257(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = e.boolean;
		local_NPCTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_257;
		Relay_Restart_Out_257();
	}

	private void SubGraph_SaveLoadBool_Save_Out_259(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_259 = e.boolean;
		local_NPCTechSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_259;
		Relay_Save_Out_259();
	}

	private void SubGraph_SaveLoadBool_Load_Out_259(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_259 = e.boolean;
		local_NPCTechSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_259;
		Relay_Load_Out_259();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_259(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_259 = e.boolean;
		local_NPCTechSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_259;
		Relay_Restart_Out_259();
	}

	private void SubGraph_SaveLoadBool_Save_Out_261(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_261 = e.boolean;
		local_CrazedAmbushTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_261;
		Relay_Save_Out_261();
	}

	private void SubGraph_SaveLoadBool_Load_Out_261(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_261 = e.boolean;
		local_CrazedAmbushTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_261;
		Relay_Load_Out_261();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_261(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_261 = e.boolean;
		local_CrazedAmbushTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_261;
		Relay_Restart_Out_261();
	}

	private void SubGraph_SaveLoadBool_Save_Out_263(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_263 = e.boolean;
		local_FirstCubeSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_263;
		Relay_Save_Out_263();
	}

	private void SubGraph_SaveLoadBool_Load_Out_263(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_263 = e.boolean;
		local_FirstCubeSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_263;
		Relay_Load_Out_263();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_263(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_263 = e.boolean;
		local_FirstCubeSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_263;
		Relay_Restart_Out_263();
	}

	private void SubGraph_SaveLoadBool_Save_Out_285(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_285 = e.boolean;
		local_TankInvul_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_285;
		Relay_Save_Out_285();
	}

	private void SubGraph_SaveLoadBool_Load_Out_285(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_285 = e.boolean;
		local_TankInvul_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_285;
		Relay_Load_Out_285();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_285(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_285 = e.boolean;
		local_TankInvul_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_285;
		Relay_Restart_Out_285();
	}

	private void SubGraph_SaveLoadBool_Save_Out_286(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_286 = e.boolean;
		local_PlayInterruptOnce_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_286;
		Relay_Save_Out_286();
	}

	private void SubGraph_SaveLoadBool_Load_Out_286(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_286 = e.boolean;
		local_PlayInterruptOnce_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_286;
		Relay_Load_Out_286();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_286(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_286 = e.boolean;
		local_PlayInterruptOnce_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_286;
		Relay_Restart_Out_286();
	}

	private void SubGraph_SaveLoadBool_Save_Out_287(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = e.boolean;
		local_CubeNeedsReload_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_287;
		Relay_Save_Out_287();
	}

	private void SubGraph_SaveLoadBool_Load_Out_287(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = e.boolean;
		local_CubeNeedsReload_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_287;
		Relay_Load_Out_287();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_287(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = e.boolean;
		local_CubeNeedsReload_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_287;
		Relay_Restart_Out_287();
	}

	private void SubGraph_SaveLoadBool_Save_Out_288(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_288 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_288;
		Relay_Save_Out_288();
	}

	private void SubGraph_SaveLoadBool_Load_Out_288(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_288 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_288;
		Relay_Load_Out_288();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_288(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_288 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_288;
		Relay_Restart_Out_288();
	}

	private void SubGraph_SaveLoadBool_Save_Out_289(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_289 = e.boolean;
		local_CrazedIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_289;
		Relay_Save_Out_289();
	}

	private void SubGraph_SaveLoadBool_Load_Out_289(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_289 = e.boolean;
		local_CrazedIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_289;
		Relay_Load_Out_289();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_289(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_289 = e.boolean;
		local_CrazedIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_289;
		Relay_Restart_Out_289();
	}

	private void SubGraph_SaveLoadBool_Save_Out_290(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_290 = e.boolean;
		local_NPCIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_290;
		Relay_Save_Out_290();
	}

	private void SubGraph_SaveLoadBool_Load_Out_290(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_290 = e.boolean;
		local_NPCIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_290;
		Relay_Load_Out_290();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_290(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_290 = e.boolean;
		local_NPCIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_290;
		Relay_Restart_Out_290();
	}

	private void SubGraph_SaveLoadBool_Save_Out_291(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_291 = e.boolean;
		local_NPCInRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_291;
		Relay_Save_Out_291();
	}

	private void SubGraph_SaveLoadBool_Load_Out_291(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_291 = e.boolean;
		local_NPCInRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_291;
		Relay_Load_Out_291();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_291(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_291 = e.boolean;
		local_NPCInRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_291;
		Relay_Restart_Out_291();
	}

	private void SubGraph_SaveLoadBool_Save_Out_292(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_292 = e.boolean;
		local_CrazedNPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_292;
		Relay_Save_Out_292();
	}

	private void SubGraph_SaveLoadBool_Load_Out_292(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_292 = e.boolean;
		local_CrazedNPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_292;
		Relay_Load_Out_292();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_292(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_292 = e.boolean;
		local_CrazedNPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_292;
		Relay_Restart_Out_292();
	}

	private void SubGraph_SaveLoadBool_Save_Out_293(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_293 = e.boolean;
		local_CubeDeadVictory_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_293;
		Relay_Save_Out_293();
	}

	private void SubGraph_SaveLoadBool_Load_Out_293(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_293 = e.boolean;
		local_CubeDeadVictory_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_293;
		Relay_Load_Out_293();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_293(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_293 = e.boolean;
		local_CubeDeadVictory_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_293;
		Relay_Restart_Out_293();
	}

	private void SubGraph_SaveLoadBool_Save_Out_294(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_294 = e.boolean;
		local_HasBeenInterrupted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_294;
		Relay_Save_Out_294();
	}

	private void SubGraph_SaveLoadBool_Load_Out_294(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_294 = e.boolean;
		local_HasBeenInterrupted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_294;
		Relay_Load_Out_294();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_294(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_294 = e.boolean;
		local_HasBeenInterrupted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_294;
		Relay_Restart_Out_294();
	}

	private void SubGraph_SaveLoadBool_Save_Out_295(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_295 = e.boolean;
		local_CrazedNPCInRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_295;
		Relay_Save_Out_295();
	}

	private void SubGraph_SaveLoadBool_Load_Out_295(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_295 = e.boolean;
		local_CrazedNPCInRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_295;
		Relay_Load_Out_295();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_295(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_295 = e.boolean;
		local_CrazedNPCInRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_295;
		Relay_Restart_Out_295();
	}

	private void SubGraph_SaveLoadBool_Save_Out_296(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_296 = e.boolean;
		local_CrazedPlayInterruptOnce_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_296;
		Relay_Save_Out_296();
	}

	private void SubGraph_SaveLoadBool_Load_Out_296(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_296 = e.boolean;
		local_CrazedPlayInterruptOnce_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_296;
		Relay_Load_Out_296();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_296(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_296 = e.boolean;
		local_CrazedPlayInterruptOnce_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_296;
		Relay_Restart_Out_296();
	}

	private void SubGraph_SaveLoadBool_Save_Out_297(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_297 = e.boolean;
		local_PlayerAttemptedMission_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_297;
		Relay_Save_Out_297();
	}

	private void SubGraph_SaveLoadBool_Load_Out_297(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_297 = e.boolean;
		local_PlayerAttemptedMission_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_297;
		Relay_Load_Out_297();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_297(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_297 = e.boolean;
		local_PlayerAttemptedMission_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_297;
		Relay_Restart_Out_297();
	}

	private void SubGraph_SaveLoadBool_Save_Out_298(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_298 = e.boolean;
		local_LeftAreaAfterLoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_298;
		Relay_Save_Out_298();
	}

	private void SubGraph_SaveLoadBool_Load_Out_298(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_298 = e.boolean;
		local_LeftAreaAfterLoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_298;
		Relay_Load_Out_298();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_298(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_298 = e.boolean;
		local_LeftAreaAfterLoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_298;
		Relay_Restart_Out_298();
	}

	private void SubGraph_SaveLoadBool_Save_Out_299(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_299 = e.boolean;
		local_MsgCubeIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_299;
		Relay_Save_Out_299();
	}

	private void SubGraph_SaveLoadBool_Load_Out_299(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_299 = e.boolean;
		local_MsgCubeIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_299;
		Relay_Load_Out_299();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_299(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_299 = e.boolean;
		local_MsgCubeIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_299;
		Relay_Restart_Out_299();
	}

	private void SubGraph_SaveLoadBool_Save_Out_300(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_300 = e.boolean;
		local_CubeDestroyedMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_300;
		Relay_Save_Out_300();
	}

	private void SubGraph_SaveLoadBool_Load_Out_300(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_300 = e.boolean;
		local_CubeDestroyedMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_300;
		Relay_Load_Out_300();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_300(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_300 = e.boolean;
		local_CubeDestroyedMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_300;
		Relay_Restart_Out_300();
	}

	private void SubGraph_SaveLoadBool_Save_Out_301(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_301 = e.boolean;
		local_WentOutOfRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_301;
		Relay_Save_Out_301();
	}

	private void SubGraph_SaveLoadBool_Load_Out_301(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_301 = e.boolean;
		local_WentOutOfRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_301;
		Relay_Load_Out_301();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_301(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_301 = e.boolean;
		local_WentOutOfRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_301;
		Relay_Restart_Out_301();
	}

	private void SubGraph_SaveLoadBool_Save_Out_302(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_302 = e.boolean;
		local_CubeDeadVictory_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_302;
		Relay_Save_Out_302();
	}

	private void SubGraph_SaveLoadBool_Load_Out_302(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_302 = e.boolean;
		local_CubeDeadVictory_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_302;
		Relay_Load_Out_302();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_302(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_302 = e.boolean;
		local_CubeDeadVictory_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_302;
		Relay_Restart_Out_302();
	}

	private void SubGraph_SaveLoadBool_Save_Out_303(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = e.boolean;
		local_OutOfTime_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_303;
		Relay_Save_Out_303();
	}

	private void SubGraph_SaveLoadBool_Load_Out_303(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = e.boolean;
		local_OutOfTime_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_303;
		Relay_Load_Out_303();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_303(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = e.boolean;
		local_OutOfTime_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_303;
		Relay_Restart_Out_303();
	}

	private void uScriptCon_ManualSwitch_Output1_336(object o, EventArgs e)
	{
		Relay_Output1_336();
	}

	private void uScriptCon_ManualSwitch_Output2_336(object o, EventArgs e)
	{
		Relay_Output2_336();
	}

	private void uScriptCon_ManualSwitch_Output3_336(object o, EventArgs e)
	{
		Relay_Output3_336();
	}

	private void uScriptCon_ManualSwitch_Output4_336(object o, EventArgs e)
	{
		Relay_Output4_336();
	}

	private void uScriptCon_ManualSwitch_Output5_336(object o, EventArgs e)
	{
		Relay_Output5_336();
	}

	private void uScriptCon_ManualSwitch_Output6_336(object o, EventArgs e)
	{
		Relay_Output6_336();
	}

	private void uScriptCon_ManualSwitch_Output7_336(object o, EventArgs e)
	{
		Relay_Output7_336();
	}

	private void uScriptCon_ManualSwitch_Output8_336(object o, EventArgs e)
	{
		Relay_Output8_336();
	}

	private void SubGraph_CompleteObjectiveStage_Out_359(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_359 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_359;
		Relay_Out_359();
	}

	private void uScriptCon_ManualSwitch_Output1_376(object o, EventArgs e)
	{
		Relay_Output1_376();
	}

	private void uScriptCon_ManualSwitch_Output2_376(object o, EventArgs e)
	{
		Relay_Output2_376();
	}

	private void uScriptCon_ManualSwitch_Output3_376(object o, EventArgs e)
	{
		Relay_Output3_376();
	}

	private void uScriptCon_ManualSwitch_Output4_376(object o, EventArgs e)
	{
		Relay_Output4_376();
	}

	private void uScriptCon_ManualSwitch_Output5_376(object o, EventArgs e)
	{
		Relay_Output5_376();
	}

	private void uScriptCon_ManualSwitch_Output6_376(object o, EventArgs e)
	{
		Relay_Output6_376();
	}

	private void uScriptCon_ManualSwitch_Output7_376(object o, EventArgs e)
	{
		Relay_Output7_376();
	}

	private void uScriptCon_ManualSwitch_Output8_376(object o, EventArgs e)
	{
		Relay_Output8_376();
	}

	private void uScriptCon_ManualSwitch_Output1_395(object o, EventArgs e)
	{
		Relay_Output1_395();
	}

	private void uScriptCon_ManualSwitch_Output2_395(object o, EventArgs e)
	{
		Relay_Output2_395();
	}

	private void uScriptCon_ManualSwitch_Output3_395(object o, EventArgs e)
	{
		Relay_Output3_395();
	}

	private void uScriptCon_ManualSwitch_Output4_395(object o, EventArgs e)
	{
		Relay_Output4_395();
	}

	private void uScriptCon_ManualSwitch_Output5_395(object o, EventArgs e)
	{
		Relay_Output5_395();
	}

	private void uScriptCon_ManualSwitch_Output6_395(object o, EventArgs e)
	{
		Relay_Output6_395();
	}

	private void uScriptCon_ManualSwitch_Output7_395(object o, EventArgs e)
	{
		Relay_Output7_395();
	}

	private void uScriptCon_ManualSwitch_Output8_395(object o, EventArgs e)
	{
		Relay_Output8_395();
	}

	private void uScriptCon_ManualSwitch_Output1_399(object o, EventArgs e)
	{
		Relay_Output1_399();
	}

	private void uScriptCon_ManualSwitch_Output2_399(object o, EventArgs e)
	{
		Relay_Output2_399();
	}

	private void uScriptCon_ManualSwitch_Output3_399(object o, EventArgs e)
	{
		Relay_Output3_399();
	}

	private void uScriptCon_ManualSwitch_Output4_399(object o, EventArgs e)
	{
		Relay_Output4_399();
	}

	private void uScriptCon_ManualSwitch_Output5_399(object o, EventArgs e)
	{
		Relay_Output5_399();
	}

	private void uScriptCon_ManualSwitch_Output6_399(object o, EventArgs e)
	{
		Relay_Output6_399();
	}

	private void uScriptCon_ManualSwitch_Output7_399(object o, EventArgs e)
	{
		Relay_Output7_399();
	}

	private void uScriptCon_ManualSwitch_Output8_399(object o, EventArgs e)
	{
		Relay_Output8_399();
	}

	private void SubGraph_CompleteObjectiveStage_Out_407(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_407 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_407;
		Relay_Out_407();
	}

	private void uScriptCon_ManualSwitch_Output1_485(object o, EventArgs e)
	{
		Relay_Output1_485();
	}

	private void uScriptCon_ManualSwitch_Output2_485(object o, EventArgs e)
	{
		Relay_Output2_485();
	}

	private void uScriptCon_ManualSwitch_Output3_485(object o, EventArgs e)
	{
		Relay_Output3_485();
	}

	private void uScriptCon_ManualSwitch_Output4_485(object o, EventArgs e)
	{
		Relay_Output4_485();
	}

	private void uScriptCon_ManualSwitch_Output5_485(object o, EventArgs e)
	{
		Relay_Output5_485();
	}

	private void uScriptCon_ManualSwitch_Output6_485(object o, EventArgs e)
	{
		Relay_Output6_485();
	}

	private void uScriptCon_ManualSwitch_Output7_485(object o, EventArgs e)
	{
		Relay_Output7_485();
	}

	private void uScriptCon_ManualSwitch_Output8_485(object o, EventArgs e)
	{
		Relay_Output8_485();
	}

	private void SubGraph_CompleteObjectiveStage_Out_495(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_495 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_495;
		Relay_Out_495();
	}

	private void SubGraph_SaveLoadBool_Save_Out_572(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_572 = e.boolean;
		local_IntroSkipped_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_572;
		Relay_Save_Out_572();
	}

	private void SubGraph_SaveLoadBool_Load_Out_572(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_572 = e.boolean;
		local_IntroSkipped_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_572;
		Relay_Load_Out_572();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_572(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_572 = e.boolean;
		local_IntroSkipped_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_572;
		Relay_Restart_Out_572();
	}

	private void SubGraph_SaveLoadBool_Save_Out_573(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_573 = e.boolean;
		local_IntroTechInRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_573;
		Relay_Save_Out_573();
	}

	private void SubGraph_SaveLoadBool_Load_Out_573(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_573 = e.boolean;
		local_IntroTechInRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_573;
		Relay_Load_Out_573();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_573(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_573 = e.boolean;
		local_IntroTechInRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_573;
		Relay_Restart_Out_573();
	}

	private void SubGraph_SaveLoadBool_Save_Out_574(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_574 = e.boolean;
		local_HasPlayerLeftMissionArea_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_574;
		Relay_Save_Out_574();
	}

	private void SubGraph_SaveLoadBool_Load_Out_574(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_574 = e.boolean;
		local_HasPlayerLeftMissionArea_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_574;
		Relay_Load_Out_574();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_574(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_574 = e.boolean;
		local_HasPlayerLeftMissionArea_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_574;
		Relay_Restart_Out_574();
	}

	private void SubGraph_SaveLoadBool_Save_Out_575(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_575 = e.boolean;
		local_PlayerLeftMissionArea_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_575;
		Relay_Save_Out_575();
	}

	private void SubGraph_SaveLoadBool_Load_Out_575(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_575 = e.boolean;
		local_PlayerLeftMissionArea_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_575;
		Relay_Load_Out_575();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_575(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_575 = e.boolean;
		local_PlayerLeftMissionArea_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_575;
		Relay_Restart_Out_575();
	}

	private void SubGraph_SaveLoadBool_Save_Out_576(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_576 = e.boolean;
		local_msgTooEarlyPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_576;
		Relay_Save_Out_576();
	}

	private void SubGraph_SaveLoadBool_Load_Out_576(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_576 = e.boolean;
		local_msgTooEarlyPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_576;
		Relay_Load_Out_576();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_576(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_576 = e.boolean;
		local_msgTooEarlyPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_576;
		Relay_Restart_Out_576();
	}

	private void SubGraph_SaveLoadBool_Save_Out_577(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_577 = e.boolean;
		local_CrazedAmbushMsgTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_577;
		Relay_Save_Out_577();
	}

	private void SubGraph_SaveLoadBool_Load_Out_577(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_577 = e.boolean;
		local_CrazedAmbushMsgTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_577;
		Relay_Load_Out_577();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_577(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_577 = e.boolean;
		local_CrazedAmbushMsgTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_577;
		Relay_Restart_Out_577();
	}

	private void SubGraph_SaveLoadBool_Save_Out_578(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_578 = e.boolean;
		local_CubeDestroyedMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_578;
		Relay_Save_Out_578();
	}

	private void SubGraph_SaveLoadBool_Load_Out_578(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_578 = e.boolean;
		local_CubeDestroyedMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_578;
		Relay_Load_Out_578();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_578(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_578 = e.boolean;
		local_CubeDestroyedMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_578;
		Relay_Restart_Out_578();
	}

	private void SubGraph_SaveLoadBool_Save_Out_579(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_579 = e.boolean;
		local_CrazedAmbushTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_579;
		Relay_Save_Out_579();
	}

	private void SubGraph_SaveLoadBool_Load_Out_579(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_579 = e.boolean;
		local_CrazedAmbushTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_579;
		Relay_Load_Out_579();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_579(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_579 = e.boolean;
		local_CrazedAmbushTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_579;
		Relay_Restart_Out_579();
	}

	private void SubGraph_SaveLoadBool_Save_Out_580(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_580 = e.boolean;
		local_MsgCubeIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_580;
		Relay_Save_Out_580();
	}

	private void SubGraph_SaveLoadBool_Load_Out_580(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_580 = e.boolean;
		local_MsgCubeIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_580;
		Relay_Load_Out_580();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_580(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_580 = e.boolean;
		local_MsgCubeIntroPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_580;
		Relay_Restart_Out_580();
	}

	private void SubGraph_SaveLoadBool_Save_Out_581(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_581 = e.boolean;
		local_PlayedTryAgainMsg_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_581;
		Relay_Save_Out_581();
	}

	private void SubGraph_SaveLoadBool_Load_Out_581(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_581 = e.boolean;
		local_PlayedTryAgainMsg_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_581;
		Relay_Load_Out_581();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_581(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_581 = e.boolean;
		local_PlayedTryAgainMsg_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_581;
		Relay_Restart_Out_581();
	}

	private void SubGraph_SaveLoadBool_Save_Out_589(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_589 = e.boolean;
		local_FightRunning_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_589;
		Relay_Save_Out_589();
	}

	private void SubGraph_SaveLoadBool_Load_Out_589(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_589 = e.boolean;
		local_FightRunning_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_589;
		Relay_Load_Out_589();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_589(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_589 = e.boolean;
		local_FightRunning_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_589;
		Relay_Restart_Out_589();
	}

	private void SubGraph_SaveLoadBool_Save_Out_593(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_593 = e.boolean;
		local_FightStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_593;
		Relay_Save_Out_593();
	}

	private void SubGraph_SaveLoadBool_Load_Out_593(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_593 = e.boolean;
		local_FightStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_593;
		Relay_Load_Out_593();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_593(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_593 = e.boolean;
		local_FightStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_593;
		Relay_Restart_Out_593();
	}

	private void SubGraph_SaveLoadBool_Save_Out_616(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_616 = e.boolean;
		local_GetRidOfCube_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_616;
		Relay_Save_Out_616();
	}

	private void SubGraph_SaveLoadBool_Load_Out_616(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_616 = e.boolean;
		local_GetRidOfCube_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_616;
		Relay_Load_Out_616();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_616(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_616 = e.boolean;
		local_GetRidOfCube_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_616;
		Relay_Restart_Out_616();
	}

	private void SubGraph_SaveLoadBool_Save_Out_627(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_627 = e.boolean;
		local_CubeisOK_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_627;
		Relay_Save_Out_627();
	}

	private void SubGraph_SaveLoadBool_Load_Out_627(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_627 = e.boolean;
		local_CubeisOK_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_627;
		Relay_Load_Out_627();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_627(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_627 = e.boolean;
		local_CubeisOK_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_627;
		Relay_Restart_Out_627();
	}

	private void SubGraph_SaveLoadBool_Save_Out_664(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_664 = e.boolean;
		local_GetRidOfCube_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_664;
		Relay_Save_Out_664();
	}

	private void SubGraph_SaveLoadBool_Load_Out_664(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_664 = e.boolean;
		local_GetRidOfCube_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_664;
		Relay_Load_Out_664();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_664(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_664 = e.boolean;
		local_GetRidOfCube_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_664;
		Relay_Restart_Out_664();
	}

	private void SubGraph_SaveLoadBool_Save_Out_665(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_665 = e.boolean;
		local_WaitingForTechClear_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_665;
		Relay_Save_Out_665();
	}

	private void SubGraph_SaveLoadBool_Load_Out_665(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_665 = e.boolean;
		local_WaitingForTechClear_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_665;
		Relay_Load_Out_665();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_665(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_665 = e.boolean;
		local_WaitingForTechClear_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_665;
		Relay_Restart_Out_665();
	}

	private void SubGraph_SaveLoadBool_Save_Out_678(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_678 = e.boolean;
		local_TechInvulOnLoad_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_678;
		Relay_Save_Out_678();
	}

	private void SubGraph_SaveLoadBool_Load_Out_678(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_678 = e.boolean;
		local_TechInvulOnLoad_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_678;
		Relay_Load_Out_678();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_678(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_678 = e.boolean;
		local_TechInvulOnLoad_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_678;
		Relay_Restart_Out_678();
	}

	private void SubGraph_LoadObjectiveStates_Out_721(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_721();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_9();
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
			Relay_In_824();
		}
		if (flag)
		{
			Relay_InitialSpawn_13();
		}
	}

	private void Relay_Save_Out_7()
	{
		Relay_Save_257();
	}

	private void Relay_Load_Out_7()
	{
		Relay_Load_257();
	}

	private void Relay_Restart_Out_7()
	{
		Relay_Set_False_257();
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
			Relay_In_547();
		}
		if (flag)
		{
			Relay_In_6();
		}
	}

	private void Relay_Save_Out_11()
	{
		Relay_Save_7();
	}

	private void Relay_Load_Out_11()
	{
		Relay_Load_7();
	}

	private void Relay_Restart_Out_11()
	{
		Relay_Set_False_7();
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
		Array nPCTechData = NPCTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_13.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_13, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_13, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_13 = owner_Connection_15;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_13.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_13, logic_uScript_SpawnTechsFromData_ownerNode_13, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_13, logic_uScript_SpawnTechsFromData_allowResurrection_13);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_13.Out)
		{
			Relay_InitialSpawn_20();
		}
	}

	private void Relay_InitialSpawn_16()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_16.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_16, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_16, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_16 = owner_Connection_39;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_16.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_16, logic_uScript_SpawnTechsFromData_ownerNode_16, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_16, logic_uScript_SpawnTechsFromData_allowResurrection_16);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_16.Out)
		{
			Relay_In_70();
		}
	}

	private void Relay_True_17()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.True(out logic_uScriptAct_SetBool_Target_17);
		local_NPCTechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_17;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_17.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_False_17()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.False(out logic_uScriptAct_SetBool_Target_17);
		local_NPCTechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_17;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_17.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_InitialSpawn_20()
	{
		int num = 0;
		Array crazedTechData = CrazedTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_20.Length != num + crazedTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_20, num + crazedTechData.Length);
		}
		Array.Copy(crazedTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_20, num, crazedTechData.Length);
		num += crazedTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_20 = owner_Connection_19;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_20.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_20, logic_uScript_SpawnTechsFromData_ownerNode_20, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_20, logic_uScript_SpawnTechsFromData_allowResurrection_20);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_20.Out)
		{
			Relay_InitialSpawn_443();
		}
	}

	private void Relay_In_22()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_GetAndCheckTechs_techData_22.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_22, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_GetAndCheckTechs_techData_22, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_22 = owner_Connection_24;
		int num2 = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_22.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_22, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_22, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_22 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22.In(logic_uScript_GetAndCheckTechs_techData_22, logic_uScript_GetAndCheckTechs_ownerNode_22, ref logic_uScript_GetAndCheckTechs_techs_22);
		local_NPCTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_22;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_22.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_30();
		}
		if (someAlive)
		{
			Relay_AtIndex_30();
		}
	}

	private void Relay_In_28()
	{
		int num = 0;
		Array crazedTechData = CrazedTechData;
		if (logic_uScript_GetAndCheckTechs_techData_28.Length != num + crazedTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_28, num + crazedTechData.Length);
		}
		Array.Copy(crazedTechData, 0, logic_uScript_GetAndCheckTechs_techData_28, num, crazedTechData.Length);
		num += crazedTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_28 = owner_Connection_26;
		int num2 = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_28.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_28, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_28, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_28 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_28.In(logic_uScript_GetAndCheckTechs_techData_28, logic_uScript_GetAndCheckTechs_ownerNode_28, ref logic_uScript_GetAndCheckTechs_techs_28);
		local_CrazedTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_28;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_28.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_28.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_32();
		}
		if (someAlive)
		{
			Relay_AtIndex_32();
		}
	}

	private void Relay_In_29()
	{
		logic_uScript_SetTankInvulnerable_tank_29 = local_NPCTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_29.In(logic_uScript_SetTankInvulnerable_invulnerable_29, logic_uScript_SetTankInvulnerable_tank_29);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_29.Out)
		{
			Relay_In_447();
		}
	}

	private void Relay_AtIndex_30()
	{
		int num = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_30.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_30, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_30, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_30.AtIndex(ref logic_uScript_AccessListTech_techList_30, logic_uScript_AccessListTech_index_30, out logic_uScript_AccessListTech_value_30);
		local_NPCTechs_TankArray = logic_uScript_AccessListTech_techList_30;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_30;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_30.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_AtIndex_32()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_32.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_32, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_32, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_32.AtIndex(ref logic_uScript_AccessListTech_techList_32, logic_uScript_AccessListTech_index_32, out logic_uScript_AccessListTech_value_32);
		local_CrazedTechs_TankArray = logic_uScript_AccessListTech_techList_32;
		local_CrazedTech2_Tank = logic_uScript_AccessListTech_value_32;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_32.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_In_34()
	{
		logic_uScript_SetTankInvulnerable_tank_34 = local_CrazedTech2_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_34.In(logic_uScript_SetTankInvulnerable_invulnerable_34, logic_uScript_SetTankInvulnerable_tank_34);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_34.Out)
		{
			Relay_AtIndex_304();
		}
	}

	private void Relay_In_36()
	{
		logic_uScriptCon_CompareBool_Bool_36 = local_FirstCubeSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.In(logic_uScriptCon_CompareBool_Bool_36);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.False;
		if (num)
		{
			Relay_In_597();
		}
		if (flag)
		{
			Relay_InitialSpawn_16();
		}
	}

	private void Relay_True_40()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.True(out logic_uScriptAct_SetBool_Target_40);
		local_FirstCubeSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_40;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_40.Out)
		{
			Relay_True_154();
		}
	}

	private void Relay_False_40()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.False(out logic_uScriptAct_SetBool_Target_40);
		local_FirstCubeSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_40;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_40.Out)
		{
			Relay_True_154();
		}
	}

	private void Relay_In_42()
	{
		logic_uScriptCon_CompareBool_Bool_42 = local_CubeDeadVictory_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.In(logic_uScriptCon_CompareBool_Bool_42);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.False;
		if (num)
		{
			Relay_In_143();
		}
		if (flag)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_44()
	{
		logic_uScript_AddMessage_messageData_44 = MsgNPCIntro01;
		logic_uScript_AddMessage_speaker_44 = NPCTechSpeaker;
		logic_uScript_AddMessage_Return_44 = logic_uScript_AddMessage_uScript_AddMessage_44.In(logic_uScript_AddMessage_messageData_44, logic_uScript_AddMessage_speaker_44);
		if (logic_uScript_AddMessage_uScript_AddMessage_44.Shown)
		{
			Relay_In_396();
		}
	}

	private void Relay_In_45()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_45 = CrazedMsgTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_45.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_45, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_45);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_45.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_47()
	{
		logic_uScriptCon_CompareBool_Bool_47 = local_NPCIntroPlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.In(logic_uScriptCon_CompareBool_Bool_47);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.False;
		if (num)
		{
			Relay_In_48();
		}
		if (flag)
		{
			Relay_In_474();
			Relay_In_755();
		}
	}

	private void Relay_In_48()
	{
		logic_uScriptCon_CompareBool_Bool_48 = local_CrazedIntroPlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.In(logic_uScriptCon_CompareBool_Bool_48);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.False;
		if (num)
		{
			Relay_In_753();
		}
		if (flag)
		{
			Relay_In_223();
			Relay_In_755();
		}
	}

	private void Relay_Succeed_53()
	{
		logic_uScript_FinishEncounter_owner_53 = owner_Connection_54;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_53.Succeed(logic_uScript_FinishEncounter_owner_53);
	}

	private void Relay_Fail_53()
	{
		logic_uScript_FinishEncounter_owner_53 = owner_Connection_54;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_53.Fail(logic_uScript_FinishEncounter_owner_53);
	}

	private void Relay_In_55()
	{
		logic_uScriptCon_CompareBool_Bool_55 = local_PlayerAttemptedMission_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.In(logic_uScriptCon_CompareBool_Bool_55);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.False;
		if (num)
		{
			Relay_In_74();
		}
		if (flag)
		{
			Relay_In_99();
		}
	}

	private void Relay_True_57()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_57.True(out logic_uScriptAct_SetBool_Target_57);
		local_NPCIntroPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_57;
	}

	private void Relay_False_57()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_57.False(out logic_uScriptAct_SetBool_Target_57);
		local_NPCIntroPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_57;
	}

	private void Relay_In_60()
	{
		logic_uScript_AddMessage_messageData_60 = MsgCubeDestroyed02;
		logic_uScript_AddMessage_speaker_60 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_60 = logic_uScript_AddMessage_uScript_AddMessage_60.In(logic_uScript_AddMessage_messageData_60, logic_uScript_AddMessage_speaker_60);
		if (logic_uScript_AddMessage_uScript_AddMessage_60.Shown)
		{
			Relay_In_403();
		}
	}

	private void Relay_In_62()
	{
		logic_uScript_SetEncounterTarget_owner_62 = owner_Connection_67;
		logic_uScript_SetEncounterTarget_visibleObject_62 = local_CrazedTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_62.In(logic_uScript_SetEncounterTarget_owner_62, logic_uScript_SetEncounterTarget_visibleObject_62);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_62.Out)
		{
			Relay_In_392();
		}
	}

	private void Relay_Pause_64()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_64.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_64.Out)
		{
			Relay_In_81();
		}
	}

	private void Relay_UnPause_64()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_64.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_64.Out)
		{
			Relay_In_81();
		}
	}

	private void Relay_In_65()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_65 = owner_Connection_79;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_65.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_65);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_65.Out)
		{
			Relay_In_81();
		}
	}

	private void Relay_Out_66()
	{
		Relay_True_57();
	}

	private void Relay_In_66()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_66 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_66.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_66, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_66);
	}

	private void Relay_AtIndex_68()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_68.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_68, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_68, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_68.AtIndex(ref logic_uScript_AccessListTech_techList_68, logic_uScript_AccessListTech_index_68, out logic_uScript_AccessListTech_value_68);
		local_CubeTechs_TankArray = logic_uScript_AccessListTech_techList_68;
		local_CubeTech_Tank = logic_uScript_AccessListTech_value_68;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_68.Out)
		{
			Relay_In_587();
		}
	}

	private void Relay_In_70()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_70.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_70, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_70, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_70 = owner_Connection_69;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_70.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_70, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_70, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_70 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_70.In(logic_uScript_GetAndCheckTechs_techData_70, logic_uScript_GetAndCheckTechs_ownerNode_70, ref logic_uScript_GetAndCheckTechs_techs_70);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_70;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_70.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_70.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_68();
		}
		if (someAlive)
		{
			Relay_AtIndex_68();
		}
	}

	private void Relay_In_74()
	{
		logic_uScriptCon_CompareBool_Bool_74 = local_LeftAreaAfterLoss_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.In(logic_uScriptCon_CompareBool_Bool_74);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.False;
		if (num)
		{
			Relay_In_519();
		}
		if (flag)
		{
			Relay_In_160();
		}
	}

	private void Relay_In_78()
	{
		logic_uScript_AddMessage_messageData_78 = MsgCubeLeaveAreaFail;
		logic_uScript_AddMessage_speaker_78 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_78 = logic_uScript_AddMessage_uScript_AddMessage_78.In(logic_uScript_AddMessage_messageData_78, logic_uScript_AddMessage_speaker_78);
		if (logic_uScript_AddMessage_uScript_AddMessage_78.Out)
		{
			Relay_False_253();
		}
	}

	private void Relay_In_81()
	{
		logic_uScriptCon_CompareBool_Bool_81 = local_NPCTechSetup_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.In(logic_uScriptCon_CompareBool_Bool_81);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.False;
		if (num)
		{
			Relay_In_36();
		}
		if (flag)
		{
			Relay_In_22();
		}
	}

	private void Relay_True_82()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.True(out logic_uScriptAct_SetBool_Target_82);
		local_NPCTechSetup_System_Boolean = logic_uScriptAct_SetBool_Target_82;
	}

	private void Relay_False_82()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.False(out logic_uScriptAct_SetBool_Target_82);
		local_NPCTechSetup_System_Boolean = logic_uScriptAct_SetBool_Target_82;
	}

	private void Relay_In_84()
	{
		logic_uScript_SetEncounterTarget_owner_84 = owner_Connection_85;
		logic_uScript_SetEncounterTarget_visibleObject_84 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_84.In(logic_uScript_SetEncounterTarget_owner_84, logic_uScript_SetEncounterTarget_visibleObject_84);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_84.Out)
		{
			Relay_True_82();
		}
	}

	private void Relay_In_89()
	{
		logic_uScriptCon_CompareBool_Bool_89 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_89.In(logic_uScriptCon_CompareBool_Bool_89);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_89.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_89.False;
		if (num)
		{
			Relay_In_105();
		}
		if (flag)
		{
			Relay_In_791();
		}
	}

	private void Relay_In_90()
	{
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_90.In(logic_uScript_PlayMiscSFX_miscSFXType_90);
		if (logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_90.Out)
		{
			Relay_In_174();
		}
	}

	private void Relay_True_91()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_91.True(out logic_uScriptAct_SetBool_Target_91);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_91;
	}

	private void Relay_False_91()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_91.False(out logic_uScriptAct_SetBool_Target_91);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_91;
	}

	private void Relay_True_92()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.True(out logic_uScriptAct_SetBool_Target_92);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_92;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_92.Out)
		{
			Relay_False_156();
		}
	}

	private void Relay_False_92()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.False(out logic_uScriptAct_SetBool_Target_92);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_92;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_92.Out)
		{
			Relay_False_156();
		}
	}

	private void Relay_In_94()
	{
		logic_uScript_ShowMissionTimerUI_owner_94 = owner_Connection_100;
		logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_94.In(logic_uScript_ShowMissionTimerUI_owner_94, logic_uScript_ShowMissionTimerUI_showBestTime_94);
		if (logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_94.Out)
		{
			Relay_In_96();
		}
	}

	private void Relay_In_96()
	{
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_96.In(logic_uScript_PlayMiscSFX_miscSFXType_96);
		if (logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_96.Out)
		{
			Relay_True_170();
		}
	}

	private void Relay_True_97()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_97.True(out logic_uScriptAct_SetBool_Target_97);
		local_CubeDeadVictory_System_Boolean = logic_uScriptAct_SetBool_Target_97;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_97.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_False_97()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_97.False(out logic_uScriptAct_SetBool_Target_97);
		local_CubeDeadVictory_System_Boolean = logic_uScriptAct_SetBool_Target_97;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_97.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_True_98()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_98.True(out logic_uScriptAct_SetBool_Target_98);
		local_MsgCubeIntroPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_98;
	}

	private void Relay_False_98()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_98.False(out logic_uScriptAct_SetBool_Target_98);
		local_MsgCubeIntroPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_98;
	}

	private void Relay_In_99()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_99 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_99.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_99);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_99.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_99.OutOfRange;
		if (inRange)
		{
			Relay_In_89();
		}
		if (outOfRange)
		{
			Relay_In_759();
		}
	}

	private void Relay_In_105()
	{
		logic_uScript_StartMissionTimer_owner_105 = owner_Connection_100;
		logic_uScript_StartMissionTimer_startTime_105 = TimeLimit;
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_105.In(logic_uScript_StartMissionTimer_owner_105, logic_uScript_StartMissionTimer_startTime_105);
		if (logic_uScript_StartMissionTimer_uScript_StartMissionTimer_105.Out)
		{
			Relay_In_94();
		}
	}

	private void Relay_In_106()
	{
		logic_uScriptCon_CompareFloat_A_106 = local_86_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_106.In(logic_uScriptCon_CompareFloat_A_106, logic_uScriptCon_CompareFloat_B_106);
		bool greaterThan = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_106.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_106.LessThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_106.LessThan;
		if (greaterThan)
		{
			Relay_In_765();
		}
		if (lessThanOrEqualTo)
		{
			Relay_True_242();
		}
		if (lessThan)
		{
			Relay_True_242();
		}
	}

	private void Relay_In_112()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_112.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_112, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_112, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_112 = owner_Connection_93;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_112.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_112, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_112, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_112 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_112.In(logic_uScript_GetAndCheckTechs_techData_112, logic_uScript_GetAndCheckTechs_ownerNode_112, ref logic_uScript_GetAndCheckTechs_techs_112);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_112;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_112.AllDead)
		{
			Relay_AtIndex_196();
		}
	}

	private void Relay_In_113()
	{
		logic_uScript_GetMissionTimerDisplayTime_owner_113 = owner_Connection_88;
		logic_uScript_GetMissionTimerDisplayTime_Return_113 = logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_113.In(logic_uScript_GetMissionTimerDisplayTime_owner_113);
		local_86_System_Single = logic_uScript_GetMissionTimerDisplayTime_Return_113;
		if (logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_113.Out)
		{
			Relay_In_106();
		}
	}

	private void Relay_True_115()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_115.True(out logic_uScriptAct_SetBool_Target_115);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_115;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_115.Out)
		{
			Relay_False_209();
		}
	}

	private void Relay_False_115()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_115.False(out logic_uScriptAct_SetBool_Target_115);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_115;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_115.Out)
		{
			Relay_False_209();
		}
	}

	private void Relay_In_116()
	{
		logic_uScript_StopMissionTimer_owner_116 = owner_Connection_109;
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_116.In(logic_uScript_StopMissionTimer_owner_116);
		if (logic_uScript_StopMissionTimer_uScript_StopMissionTimer_116.Out)
		{
			Relay_In_90();
		}
	}

	private void Relay_In_118()
	{
		logic_uScriptCon_CompareBool_Bool_118 = local_FightStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.In(logic_uScriptCon_CompareBool_Bool_118);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.False;
		if (num)
		{
			Relay_In_199();
		}
		if (flag)
		{
			Relay_In_674();
		}
	}

	private void Relay_In_120()
	{
		logic_uScriptCon_CompareBool_Bool_120 = local_CubeisOK_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_120.In(logic_uScriptCon_CompareBool_Bool_120);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_120.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_120.False;
		if (num)
		{
			Relay_In_123();
		}
		if (flag)
		{
			Relay_In_137();
		}
	}

	private void Relay_In_123()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_123.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_123.Out)
		{
			Relay_In_118();
		}
	}

	private void Relay_InitialSpawn_126()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_126.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_126, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_126, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_126 = owner_Connection_124;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_126.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_126, logic_uScript_SpawnTechsFromData_ownerNode_126, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_126, logic_uScript_SpawnTechsFromData_allowResurrection_126);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_126.Out)
		{
			Relay_False_651();
		}
	}

	private void Relay_In_128()
	{
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_128.In(logic_uScript_PlayMiscSFX_miscSFXType_128);
		if (logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_128.Out)
		{
			Relay_True_164();
		}
	}

	private void Relay_In_130()
	{
		logic_uScript_ShowMissionTimerUI_owner_130 = owner_Connection_127;
		logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_130.In(logic_uScript_ShowMissionTimerUI_owner_130, logic_uScript_ShowMissionTimerUI_showBestTime_130);
		if (logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_130.Out)
		{
			Relay_In_128();
		}
	}

	private void Relay_In_131()
	{
		logic_uScript_StartMissionTimer_owner_131 = owner_Connection_127;
		logic_uScript_StartMissionTimer_startTime_131 = TimeLimit;
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_131.In(logic_uScript_StartMissionTimer_owner_131, logic_uScript_StartMissionTimer_startTime_131);
		if (logic_uScript_StartMissionTimer_uScript_StartMissionTimer_131.Out)
		{
			Relay_In_130();
		}
	}

	private void Relay_True_132()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.True(out logic_uScriptAct_SetBool_Target_132);
		local_LeftAreaAfterLoss_System_Boolean = logic_uScriptAct_SetBool_Target_132;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_132.Out)
		{
			Relay_True_141();
		}
	}

	private void Relay_False_132()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.False(out logic_uScriptAct_SetBool_Target_132);
		local_LeftAreaAfterLoss_System_Boolean = logic_uScriptAct_SetBool_Target_132;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_132.Out)
		{
			Relay_True_141();
		}
	}

	private void Relay_In_135()
	{
		logic_uScript_StopMissionTimer_owner_135 = owner_Connection_134;
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_135.In(logic_uScript_StopMissionTimer_owner_135);
		if (logic_uScript_StopMissionTimer_uScript_StopMissionTimer_135.Out)
		{
			Relay_In_139();
		}
	}

	private void Relay_In_137()
	{
		logic_uScriptCon_CompareBool_Bool_137 = local_CubeNeedsReload_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_137.In(logic_uScriptCon_CompareBool_Bool_137);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_137.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_137.False;
		if (num)
		{
			Relay_In_609();
		}
		if (flag)
		{
			Relay_In_118();
		}
	}

	private void Relay_In_139()
	{
		logic_uScript_HideMissionTimerUI_owner_139 = owner_Connection_808;
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_139.In(logic_uScript_HideMissionTimerUI_owner_139);
		if (logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_139.Out)
		{
			Relay_False_115();
		}
	}

	private void Relay_True_141()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_141.True(out logic_uScriptAct_SetBool_Target_141);
		local_CubeNeedsReload_System_Boolean = logic_uScriptAct_SetBool_Target_141;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_141.Out)
		{
			Relay_False_162();
		}
	}

	private void Relay_False_141()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_141.False(out logic_uScriptAct_SetBool_Target_141);
		local_CubeNeedsReload_System_Boolean = logic_uScriptAct_SetBool_Target_141;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_141.Out)
		{
			Relay_False_162();
		}
	}

	private void Relay_In_143()
	{
		logic_uScriptCon_CompareBool_Bool_143 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.In(logic_uScriptCon_CompareBool_Bool_143);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.False;
		if (num)
		{
			Relay_In_514();
		}
		if (flag)
		{
			Relay_In_399();
		}
	}

	private void Relay_True_145()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_145.True(out logic_uScriptAct_SetBool_Target_145);
		local_CubeDestroyedMsgPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_145;
	}

	private void Relay_False_145()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_145.False(out logic_uScriptAct_SetBool_Target_145);
		local_CubeDestroyedMsgPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_145;
	}

	private void Relay_True_148()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_148.True(out logic_uScriptAct_SetBool_Target_148);
		local_CubeNeedsReload_System_Boolean = logic_uScriptAct_SetBool_Target_148;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_148.Out)
		{
			Relay_True_178();
		}
	}

	private void Relay_False_148()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_148.False(out logic_uScriptAct_SetBool_Target_148);
		local_CubeNeedsReload_System_Boolean = logic_uScriptAct_SetBool_Target_148;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_148.Out)
		{
			Relay_True_178();
		}
	}

	private void Relay_In_149()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_149 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_149.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_149);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_149.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_149.OutOfRange;
		if (inRange)
		{
			Relay_In_811();
		}
		if (outOfRange)
		{
			Relay_In_760();
		}
	}

	private void Relay_In_152()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_152 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_152.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_152);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_152.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_152.OutOfRange;
		if (inRange)
		{
			Relay_In_112();
		}
		if (outOfRange)
		{
			Relay_True_244();
		}
	}

	private void Relay_True_154()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_154.True(out logic_uScriptAct_SetBool_Target_154);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_154;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_154.Out)
		{
			Relay_False_661();
		}
	}

	private void Relay_False_154()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_154.False(out logic_uScriptAct_SetBool_Target_154);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_154;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_154.Out)
		{
			Relay_False_661();
		}
	}

	private void Relay_True_156()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_156.True(out logic_uScriptAct_SetBool_Target_156);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_156;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_156.Out)
		{
			Relay_In_816();
		}
	}

	private void Relay_False_156()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_156.False(out logic_uScriptAct_SetBool_Target_156);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_156;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_156.Out)
		{
			Relay_In_816();
		}
	}

	private void Relay_In_157()
	{
		logic_uScriptCon_CompareBool_Bool_157 = local_FightStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_157.In(logic_uScriptCon_CompareBool_Bool_157);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_157.False)
		{
			Relay_In_194();
		}
	}

	private void Relay_In_160()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_160 = CubeFailTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_160.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_160);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_160.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_160.OutOfRange;
		if (inRange)
		{
			Relay_In_747();
		}
		if (outOfRange)
		{
			Relay_In_768();
		}
	}

	private void Relay_True_162()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_162.True(out logic_uScriptAct_SetBool_Target_162);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_162;
	}

	private void Relay_False_162()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_162.False(out logic_uScriptAct_SetBool_Target_162);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_162;
	}

	private void Relay_True_164()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_164.True(out logic_uScriptAct_SetBool_Target_164);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_164;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_164.Out)
		{
			Relay_True_193();
		}
	}

	private void Relay_False_164()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_164.False(out logic_uScriptAct_SetBool_Target_164);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_164;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_164.Out)
		{
			Relay_True_193();
		}
	}

	private void Relay_In_165()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_165.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_165, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_165, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_165 = owner_Connection_166;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_165.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_165, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_165, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_165 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_165.In(logic_uScript_GetAndCheckTechs_techData_165, logic_uScript_GetAndCheckTechs_ownerNode_165, ref logic_uScript_GetAndCheckTechs_techs_165);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_165;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_165.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_165.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_169();
		}
		if (someAlive)
		{
			Relay_AtIndex_169();
		}
	}

	private void Relay_AtIndex_169()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_169.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_169, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_169, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_169.AtIndex(ref logic_uScript_AccessListTech_techList_169, logic_uScript_AccessListTech_index_169, out logic_uScript_AccessListTech_value_169);
		local_CubeTechs_TankArray = logic_uScript_AccessListTech_techList_169;
		local_CubeTech_Tank = logic_uScript_AccessListTech_value_169;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_169.Out)
		{
			Relay_In_635();
		}
	}

	private void Relay_True_170()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_170.True(out logic_uScriptAct_SetBool_Target_170);
		local_PlayerAttemptedMission_System_Boolean = logic_uScriptAct_SetBool_Target_170;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_170.Out)
		{
			Relay_True_191();
		}
	}

	private void Relay_False_170()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_170.False(out logic_uScriptAct_SetBool_Target_170);
		local_PlayerAttemptedMission_System_Boolean = logic_uScriptAct_SetBool_Target_170;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_170.Out)
		{
			Relay_True_191();
		}
	}

	private void Relay_True_171()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_171.True(out logic_uScriptAct_SetBool_Target_171);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_171;
	}

	private void Relay_False_171()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_171.False(out logic_uScriptAct_SetBool_Target_171);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_171;
	}

	private void Relay_In_174()
	{
		logic_uScript_ResetMissionTimerTimeElapsed_owner_174 = owner_Connection_109;
		logic_uScript_ResetMissionTimerTimeElapsed_startTime_174 = local_175_System_Single;
		logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_174.In(logic_uScript_ResetMissionTimerTimeElapsed_owner_174, logic_uScript_ResetMissionTimerTimeElapsed_startTime_174);
		if (logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_174.Out)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_176()
	{
		logic_uScript_HideMissionTimerUI_owner_176 = owner_Connection_809;
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_176.In(logic_uScript_HideMissionTimerUI_owner_176);
		if (logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_176.Out)
		{
			Relay_In_557();
		}
	}

	private void Relay_True_178()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_178.True(out logic_uScriptAct_SetBool_Target_178);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_178;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_178.Out)
		{
			Relay_False_618();
		}
	}

	private void Relay_False_178()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_178.False(out logic_uScriptAct_SetBool_Target_178);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_178;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_178.Out)
		{
			Relay_False_618();
		}
	}

	private void Relay_In_180()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_180.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_180, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_180, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_180.In(ref logic_uScript_SetTechsTeam_techs_180, logic_uScript_SetTechsTeam_team_180);
		local_CubeTechs_TankArray = logic_uScript_SetTechsTeam_techs_180;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_180.Out)
		{
			Relay_In_181();
		}
	}

	private void Relay_In_181()
	{
		logic_uScript_SetTankInvulnerable_tank_181 = local_CubeTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_181.In(logic_uScript_SetTankInvulnerable_invulnerable_181, logic_uScript_SetTankInvulnerable_tank_181);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_181.Out)
		{
			Relay_True_200();
		}
	}

	private void Relay_In_183()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_183.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_183, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_183, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_183 = owner_Connection_179;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_183.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_183, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_183, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_183 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_183.In(logic_uScript_GetAndCheckTechs_techData_183, logic_uScript_GetAndCheckTechs_ownerNode_183, ref logic_uScript_GetAndCheckTechs_techs_183);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_183;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_183.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_183.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_185();
		}
		if (someAlive)
		{
			Relay_AtIndex_185();
		}
	}

	private void Relay_AtIndex_185()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_185.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_185, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_185, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_185.AtIndex(ref logic_uScript_AccessListTech_techList_185, logic_uScript_AccessListTech_index_185, out logic_uScript_AccessListTech_value_185);
		local_CubeTechs_TankArray = logic_uScript_AccessListTech_techList_185;
		local_CubeTech_Tank = logic_uScript_AccessListTech_value_185;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_185.Out)
		{
			Relay_In_180();
		}
	}

	private void Relay_In_189()
	{
		logic_uScriptCon_CompareBool_Bool_189 = local_FightRunning_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_189.In(logic_uScriptCon_CompareBool_Bool_189);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_189.True)
		{
			Relay_In_113();
		}
	}

	private void Relay_True_191()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_191.True(out logic_uScriptAct_SetBool_Target_191);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_191;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_191.Out)
		{
			Relay_True_171();
		}
	}

	private void Relay_False_191()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_191.False(out logic_uScriptAct_SetBool_Target_191);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_191;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_191.Out)
		{
			Relay_True_171();
		}
	}

	private void Relay_True_193()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_193.True(out logic_uScriptAct_SetBool_Target_193);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_193;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_193.Out)
		{
			Relay_False_202();
		}
	}

	private void Relay_False_193()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_193.False(out logic_uScriptAct_SetBool_Target_193);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_193;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_193.Out)
		{
			Relay_False_202();
		}
	}

	private void Relay_In_194()
	{
		logic_uScriptCon_CompareBool_Bool_194 = local_FightRunning_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194.In(logic_uScriptCon_CompareBool_Bool_194);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194.False)
		{
			Relay_In_246();
		}
	}

	private void Relay_AtIndex_196()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_196.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_196, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_196, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_196.AtIndex(ref logic_uScript_AccessListTech_techList_196, logic_uScript_AccessListTech_index_196, out logic_uScript_AccessListTech_value_196);
		local_CubeTechs_TankArray = logic_uScript_AccessListTech_techList_196;
		local_CubeTech_Tank = logic_uScript_AccessListTech_value_196;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_196.Out)
		{
			Relay_True_97();
		}
	}

	private void Relay_In_199()
	{
		logic_uScriptCon_CompareBool_Bool_199 = local_TankInvul_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_199.In(logic_uScriptCon_CompareBool_Bool_199);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_199.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_199.False;
		if (num)
		{
			Relay_In_189();
		}
		if (flag)
		{
			Relay_In_183();
		}
	}

	private void Relay_True_200()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_200.True(out logic_uScriptAct_SetBool_Target_200);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_200;
	}

	private void Relay_False_200()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_200.False(out logic_uScriptAct_SetBool_Target_200);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_200;
	}

	private void Relay_True_202()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_202.True(out logic_uScriptAct_SetBool_Target_202);
		local_LeftAreaAfterLoss_System_Boolean = logic_uScriptAct_SetBool_Target_202;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_202.Out)
		{
			Relay_False_214();
		}
	}

	private void Relay_False_202()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_202.False(out logic_uScriptAct_SetBool_Target_202);
		local_LeftAreaAfterLoss_System_Boolean = logic_uScriptAct_SetBool_Target_202;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_202.Out)
		{
			Relay_False_214();
		}
	}

	private void Relay_In_206()
	{
		logic_uScript_AddMessage_messageData_206 = MsgCrazedAmbush;
		logic_uScript_AddMessage_speaker_206 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_206 = logic_uScript_AddMessage_uScript_AddMessage_206.In(logic_uScript_AddMessage_messageData_206, logic_uScript_AddMessage_speaker_206);
		if (logic_uScript_AddMessage_uScript_AddMessage_206.Out)
		{
			Relay_In_454();
		}
	}

	private void Relay_In_208()
	{
		logic_uScriptCon_CompareBool_Bool_208 = local_CrazedAmbushTriggered_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208.In(logic_uScriptCon_CompareBool_Bool_208);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208.False;
		if (num)
		{
			Relay_In_206();
		}
		if (flag)
		{
			Relay_In_212();
		}
	}

	private void Relay_True_209()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_209.True(out logic_uScriptAct_SetBool_Target_209);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_209;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_209.Out)
		{
			Relay_In_815();
		}
	}

	private void Relay_False_209()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_209.False(out logic_uScriptAct_SetBool_Target_209);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_209;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_209.Out)
		{
			Relay_In_815();
		}
	}

	private void Relay_In_212()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_212 = CrazedNPCTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_212.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_212);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_212.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_212.OutOfRange;
		if (inRange)
		{
			Relay_True_512();
		}
		if (outOfRange)
		{
			Relay_In_543();
		}
	}

	private void Relay_True_214()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_214.True(out logic_uScriptAct_SetBool_Target_214);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_214;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_214.Out)
		{
			Relay_False_522();
		}
	}

	private void Relay_False_214()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_214.False(out logic_uScriptAct_SetBool_Target_214);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_214;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_214.Out)
		{
			Relay_False_522();
		}
	}

	private void Relay_In_215()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_215 = NPCMsgTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_215.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_215, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_215);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_215.Out)
		{
			Relay_In_217();
		}
	}

	private void Relay_In_217()
	{
		logic_uScript_AddMessage_messageData_217 = MsgNPCInterrupt;
		logic_uScript_AddMessage_speaker_217 = NPCTechSpeaker;
		logic_uScript_AddMessage_Return_217 = logic_uScript_AddMessage_uScript_AddMessage_217.In(logic_uScript_AddMessage_messageData_217, logic_uScript_AddMessage_speaker_217);
		if (logic_uScript_AddMessage_uScript_AddMessage_217.Out)
		{
			Relay_In_407();
		}
	}

	private void Relay_True_221()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_221.True(out logic_uScriptAct_SetBool_Target_221);
		local_CrazedNPCInRange_System_Boolean = logic_uScriptAct_SetBool_Target_221;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_221.Out)
		{
			Relay_False_434();
		}
	}

	private void Relay_False_221()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_221.False(out logic_uScriptAct_SetBool_Target_221);
		local_CrazedNPCInRange_System_Boolean = logic_uScriptAct_SetBool_Target_221;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_221.Out)
		{
			Relay_False_434();
		}
	}

	private void Relay_True_222()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_222.True(out logic_uScriptAct_SetBool_Target_222);
		local_CrazedPlayInterruptOnce_System_Boolean = logic_uScriptAct_SetBool_Target_222;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_222.Out)
		{
			Relay_False_224();
		}
	}

	private void Relay_False_222()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_222.False(out logic_uScriptAct_SetBool_Target_222);
		local_CrazedPlayInterruptOnce_System_Boolean = logic_uScriptAct_SetBool_Target_222;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_222.Out)
		{
			Relay_False_224();
		}
	}

	private void Relay_In_223()
	{
		logic_uScriptCon_CompareBool_Bool_223 = local_CrazedNPCInRange_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.In(logic_uScriptCon_CompareBool_Bool_223);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.False;
		if (num)
		{
			Relay_In_756();
		}
		if (flag)
		{
			Relay_In_428();
		}
	}

	private void Relay_True_224()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_224.True(out logic_uScriptAct_SetBool_Target_224);
		local_CrazedNPCInRange_System_Boolean = logic_uScriptAct_SetBool_Target_224;
	}

	private void Relay_False_224()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_224.False(out logic_uScriptAct_SetBool_Target_224);
		local_CrazedNPCInRange_System_Boolean = logic_uScriptAct_SetBool_Target_224;
	}

	private void Relay_In_225()
	{
		logic_uScriptCon_CompareBool_Bool_225 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225.In(logic_uScriptCon_CompareBool_Bool_225);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225.False)
		{
			Relay_In_229();
		}
	}

	private void Relay_In_226()
	{
		logic_uScript_AddMessage_messageData_226 = MsgCrazedIntro01;
		logic_uScript_AddMessage_speaker_226 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_226 = logic_uScript_AddMessage_uScript_AddMessage_226.In(logic_uScript_AddMessage_messageData_226, logic_uScript_AddMessage_speaker_226);
		if (logic_uScript_AddMessage_uScript_AddMessage_226.Shown)
		{
			Relay_In_339();
		}
	}

	private void Relay_In_229()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_229 = CrazedMsgTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_229.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_229, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_229);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_229.Out)
		{
			Relay_In_237();
		}
	}

	private void Relay_True_230()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_230.True(out logic_uScriptAct_SetBool_Target_230);
		local_HasBeenInterrupted_System_Boolean = logic_uScriptAct_SetBool_Target_230;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_230.Out)
		{
			Relay_True_222();
		}
	}

	private void Relay_False_230()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_230.False(out logic_uScriptAct_SetBool_Target_230);
		local_HasBeenInterrupted_System_Boolean = logic_uScriptAct_SetBool_Target_230;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_230.Out)
		{
			Relay_True_222();
		}
	}

	private void Relay_In_237()
	{
		logic_uScript_AddMessage_messageData_237 = MsgCrazedInterrupt;
		logic_uScript_AddMessage_speaker_237 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_237 = logic_uScript_AddMessage_uScript_AddMessage_237.In(logic_uScript_AddMessage_messageData_237, logic_uScript_AddMessage_speaker_237);
		if (logic_uScript_AddMessage_uScript_AddMessage_237.Out)
		{
			Relay_True_230();
		}
	}

	private void Relay_In_241()
	{
		logic_uScript_AddMessage_messageData_241 = MsgStartBossFight;
		logic_uScript_AddMessage_speaker_241 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_241 = logic_uScript_AddMessage_uScript_AddMessage_241.In(logic_uScript_AddMessage_messageData_241, logic_uScript_AddMessage_speaker_241);
	}

	private void Relay_True_242()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_242.True(out logic_uScriptAct_SetBool_Target_242);
		local_OutOfTime_System_Boolean = logic_uScriptAct_SetBool_Target_242;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_242.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_False_242()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_242.False(out logic_uScriptAct_SetBool_Target_242);
		local_OutOfTime_System_Boolean = logic_uScriptAct_SetBool_Target_242;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_242.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_True_244()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_244.True(out logic_uScriptAct_SetBool_Target_244);
		local_WentOutOfRange_System_Boolean = logic_uScriptAct_SetBool_Target_244;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_244.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_False_244()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_244.False(out logic_uScriptAct_SetBool_Target_244);
		local_WentOutOfRange_System_Boolean = logic_uScriptAct_SetBool_Target_244;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_244.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_In_246()
	{
		logic_uScriptCon_CompareBool_Bool_246 = local_WentOutOfRange_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_246.In(logic_uScriptCon_CompareBool_Bool_246);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_246.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_246.False;
		if (num)
		{
			Relay_In_78();
		}
		if (flag)
		{
			Relay_In_249();
		}
	}

	private void Relay_In_249()
	{
		logic_uScriptCon_CompareBool_Bool_249 = local_OutOfTime_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_249.In(logic_uScriptCon_CompareBool_Bool_249);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_249.True)
		{
			Relay_In_777();
		}
	}

	private void Relay_In_252()
	{
		logic_uScript_AddMessage_messageData_252 = MsgOutOfTime;
		logic_uScript_AddMessage_speaker_252 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_252 = logic_uScript_AddMessage_uScript_AddMessage_252.In(logic_uScript_AddMessage_messageData_252, logic_uScript_AddMessage_speaker_252);
		if (logic_uScript_AddMessage_uScript_AddMessage_252.Shown)
		{
			Relay_In_491();
		}
	}

	private void Relay_True_253()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_253.True(out logic_uScriptAct_SetBool_Target_253);
		local_WentOutOfRange_System_Boolean = logic_uScriptAct_SetBool_Target_253;
	}

	private void Relay_False_253()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_253.False(out logic_uScriptAct_SetBool_Target_253);
		local_WentOutOfRange_System_Boolean = logic_uScriptAct_SetBool_Target_253;
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
		logic_SubGraph_SaveLoadBool_boolean_257 = local_NPCTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_257 = local_NPCTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Save(ref logic_SubGraph_SaveLoadBool_boolean_257, logic_SubGraph_SaveLoadBool_boolAsVariable_257, logic_SubGraph_SaveLoadBool_uniqueID_257);
	}

	private void Relay_Load_257()
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = local_NPCTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_257 = local_NPCTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Load(ref logic_SubGraph_SaveLoadBool_boolean_257, logic_SubGraph_SaveLoadBool_boolAsVariable_257, logic_SubGraph_SaveLoadBool_uniqueID_257);
	}

	private void Relay_Set_True_257()
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = local_NPCTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_257 = local_NPCTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_257, logic_SubGraph_SaveLoadBool_boolAsVariable_257, logic_SubGraph_SaveLoadBool_uniqueID_257);
	}

	private void Relay_Set_False_257()
	{
		logic_SubGraph_SaveLoadBool_boolean_257 = local_NPCTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_257 = local_NPCTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_257.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_257, logic_SubGraph_SaveLoadBool_boolAsVariable_257, logic_SubGraph_SaveLoadBool_uniqueID_257);
	}

	private void Relay_Save_Out_259()
	{
		Relay_Save_261();
	}

	private void Relay_Load_Out_259()
	{
		Relay_Load_261();
	}

	private void Relay_Restart_Out_259()
	{
		Relay_Set_False_261();
	}

	private void Relay_Save_259()
	{
		logic_SubGraph_SaveLoadBool_boolean_259 = local_NPCTechSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_259 = local_NPCTechSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Save(ref logic_SubGraph_SaveLoadBool_boolean_259, logic_SubGraph_SaveLoadBool_boolAsVariable_259, logic_SubGraph_SaveLoadBool_uniqueID_259);
	}

	private void Relay_Load_259()
	{
		logic_SubGraph_SaveLoadBool_boolean_259 = local_NPCTechSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_259 = local_NPCTechSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Load(ref logic_SubGraph_SaveLoadBool_boolean_259, logic_SubGraph_SaveLoadBool_boolAsVariable_259, logic_SubGraph_SaveLoadBool_uniqueID_259);
	}

	private void Relay_Set_True_259()
	{
		logic_SubGraph_SaveLoadBool_boolean_259 = local_NPCTechSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_259 = local_NPCTechSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_259, logic_SubGraph_SaveLoadBool_boolAsVariable_259, logic_SubGraph_SaveLoadBool_uniqueID_259);
	}

	private void Relay_Set_False_259()
	{
		logic_SubGraph_SaveLoadBool_boolean_259 = local_NPCTechSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_259 = local_NPCTechSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_259.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_259, logic_SubGraph_SaveLoadBool_boolAsVariable_259, logic_SubGraph_SaveLoadBool_uniqueID_259);
	}

	private void Relay_Save_Out_261()
	{
		Relay_Save_263();
	}

	private void Relay_Load_Out_261()
	{
		Relay_Load_263();
	}

	private void Relay_Restart_Out_261()
	{
		Relay_Set_False_263();
	}

	private void Relay_Save_261()
	{
		logic_SubGraph_SaveLoadBool_boolean_261 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_261 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Save(ref logic_SubGraph_SaveLoadBool_boolean_261, logic_SubGraph_SaveLoadBool_boolAsVariable_261, logic_SubGraph_SaveLoadBool_uniqueID_261);
	}

	private void Relay_Load_261()
	{
		logic_SubGraph_SaveLoadBool_boolean_261 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_261 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Load(ref logic_SubGraph_SaveLoadBool_boolean_261, logic_SubGraph_SaveLoadBool_boolAsVariable_261, logic_SubGraph_SaveLoadBool_uniqueID_261);
	}

	private void Relay_Set_True_261()
	{
		logic_SubGraph_SaveLoadBool_boolean_261 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_261 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_261, logic_SubGraph_SaveLoadBool_boolAsVariable_261, logic_SubGraph_SaveLoadBool_uniqueID_261);
	}

	private void Relay_Set_False_261()
	{
		logic_SubGraph_SaveLoadBool_boolean_261 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_261 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_261.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_261, logic_SubGraph_SaveLoadBool_boolAsVariable_261, logic_SubGraph_SaveLoadBool_uniqueID_261);
	}

	private void Relay_Save_Out_263()
	{
		Relay_Save_288();
	}

	private void Relay_Load_Out_263()
	{
		Relay_Load_288();
	}

	private void Relay_Restart_Out_263()
	{
		Relay_Set_False_288();
	}

	private void Relay_Save_263()
	{
		logic_SubGraph_SaveLoadBool_boolean_263 = local_FirstCubeSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_263 = local_FirstCubeSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.Save(ref logic_SubGraph_SaveLoadBool_boolean_263, logic_SubGraph_SaveLoadBool_boolAsVariable_263, logic_SubGraph_SaveLoadBool_uniqueID_263);
	}

	private void Relay_Load_263()
	{
		logic_SubGraph_SaveLoadBool_boolean_263 = local_FirstCubeSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_263 = local_FirstCubeSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.Load(ref logic_SubGraph_SaveLoadBool_boolean_263, logic_SubGraph_SaveLoadBool_boolAsVariable_263, logic_SubGraph_SaveLoadBool_uniqueID_263);
	}

	private void Relay_Set_True_263()
	{
		logic_SubGraph_SaveLoadBool_boolean_263 = local_FirstCubeSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_263 = local_FirstCubeSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_263, logic_SubGraph_SaveLoadBool_boolAsVariable_263, logic_SubGraph_SaveLoadBool_uniqueID_263);
	}

	private void Relay_Set_False_263()
	{
		logic_SubGraph_SaveLoadBool_boolean_263 = local_FirstCubeSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_263 = local_FirstCubeSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_263.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_263, logic_SubGraph_SaveLoadBool_boolAsVariable_263, logic_SubGraph_SaveLoadBool_uniqueID_263);
	}

	private void Relay_Save_Out_285()
	{
		Relay_Save_593();
	}

	private void Relay_Load_Out_285()
	{
		Relay_Load_593();
	}

	private void Relay_Restart_Out_285()
	{
		Relay_Set_False_593();
	}

	private void Relay_Save_285()
	{
		logic_SubGraph_SaveLoadBool_boolean_285 = local_TankInvul_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_285 = local_TankInvul_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.Save(ref logic_SubGraph_SaveLoadBool_boolean_285, logic_SubGraph_SaveLoadBool_boolAsVariable_285, logic_SubGraph_SaveLoadBool_uniqueID_285);
	}

	private void Relay_Load_285()
	{
		logic_SubGraph_SaveLoadBool_boolean_285 = local_TankInvul_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_285 = local_TankInvul_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.Load(ref logic_SubGraph_SaveLoadBool_boolean_285, logic_SubGraph_SaveLoadBool_boolAsVariable_285, logic_SubGraph_SaveLoadBool_uniqueID_285);
	}

	private void Relay_Set_True_285()
	{
		logic_SubGraph_SaveLoadBool_boolean_285 = local_TankInvul_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_285 = local_TankInvul_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_285, logic_SubGraph_SaveLoadBool_boolAsVariable_285, logic_SubGraph_SaveLoadBool_uniqueID_285);
	}

	private void Relay_Set_False_285()
	{
		logic_SubGraph_SaveLoadBool_boolean_285 = local_TankInvul_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_285 = local_TankInvul_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_285.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_285, logic_SubGraph_SaveLoadBool_boolAsVariable_285, logic_SubGraph_SaveLoadBool_uniqueID_285);
	}

	private void Relay_Save_Out_286()
	{
		Relay_Save_291();
	}

	private void Relay_Load_Out_286()
	{
		Relay_Load_291();
	}

	private void Relay_Restart_Out_286()
	{
		Relay_Set_False_291();
	}

	private void Relay_Save_286()
	{
		logic_SubGraph_SaveLoadBool_boolean_286 = local_PlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_286 = local_PlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.Save(ref logic_SubGraph_SaveLoadBool_boolean_286, logic_SubGraph_SaveLoadBool_boolAsVariable_286, logic_SubGraph_SaveLoadBool_uniqueID_286);
	}

	private void Relay_Load_286()
	{
		logic_SubGraph_SaveLoadBool_boolean_286 = local_PlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_286 = local_PlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.Load(ref logic_SubGraph_SaveLoadBool_boolean_286, logic_SubGraph_SaveLoadBool_boolAsVariable_286, logic_SubGraph_SaveLoadBool_uniqueID_286);
	}

	private void Relay_Set_True_286()
	{
		logic_SubGraph_SaveLoadBool_boolean_286 = local_PlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_286 = local_PlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_286, logic_SubGraph_SaveLoadBool_boolAsVariable_286, logic_SubGraph_SaveLoadBool_uniqueID_286);
	}

	private void Relay_Set_False_286()
	{
		logic_SubGraph_SaveLoadBool_boolean_286 = local_PlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_286 = local_PlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_286.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_286, logic_SubGraph_SaveLoadBool_boolAsVariable_286, logic_SubGraph_SaveLoadBool_uniqueID_286);
	}

	private void Relay_Save_Out_287()
	{
		Relay_Save_678();
	}

	private void Relay_Load_Out_287()
	{
		Relay_Load_678();
	}

	private void Relay_Restart_Out_287()
	{
		Relay_Set_False_678();
	}

	private void Relay_Save_287()
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = local_CubeNeedsReload_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_287 = local_CubeNeedsReload_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Save(ref logic_SubGraph_SaveLoadBool_boolean_287, logic_SubGraph_SaveLoadBool_boolAsVariable_287, logic_SubGraph_SaveLoadBool_uniqueID_287);
	}

	private void Relay_Load_287()
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = local_CubeNeedsReload_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_287 = local_CubeNeedsReload_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Load(ref logic_SubGraph_SaveLoadBool_boolean_287, logic_SubGraph_SaveLoadBool_boolAsVariable_287, logic_SubGraph_SaveLoadBool_uniqueID_287);
	}

	private void Relay_Set_True_287()
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = local_CubeNeedsReload_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_287 = local_CubeNeedsReload_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_287, logic_SubGraph_SaveLoadBool_boolAsVariable_287, logic_SubGraph_SaveLoadBool_uniqueID_287);
	}

	private void Relay_Set_False_287()
	{
		logic_SubGraph_SaveLoadBool_boolean_287 = local_CubeNeedsReload_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_287 = local_CubeNeedsReload_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_287.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_287, logic_SubGraph_SaveLoadBool_boolAsVariable_287, logic_SubGraph_SaveLoadBool_uniqueID_287);
	}

	private void Relay_Save_Out_288()
	{
		Relay_Save_286();
	}

	private void Relay_Load_Out_288()
	{
		Relay_Load_286();
	}

	private void Relay_Restart_Out_288()
	{
		Relay_Set_False_286();
	}

	private void Relay_Save_288()
	{
		logic_SubGraph_SaveLoadBool_boolean_288 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_288 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Save(ref logic_SubGraph_SaveLoadBool_boolean_288, logic_SubGraph_SaveLoadBool_boolAsVariable_288, logic_SubGraph_SaveLoadBool_uniqueID_288);
	}

	private void Relay_Load_288()
	{
		logic_SubGraph_SaveLoadBool_boolean_288 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_288 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Load(ref logic_SubGraph_SaveLoadBool_boolean_288, logic_SubGraph_SaveLoadBool_boolAsVariable_288, logic_SubGraph_SaveLoadBool_uniqueID_288);
	}

	private void Relay_Set_True_288()
	{
		logic_SubGraph_SaveLoadBool_boolean_288 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_288 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_288, logic_SubGraph_SaveLoadBool_boolAsVariable_288, logic_SubGraph_SaveLoadBool_uniqueID_288);
	}

	private void Relay_Set_False_288()
	{
		logic_SubGraph_SaveLoadBool_boolean_288 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_288 = local_NPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_288.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_288, logic_SubGraph_SaveLoadBool_boolAsVariable_288, logic_SubGraph_SaveLoadBool_uniqueID_288);
	}

	private void Relay_Save_Out_289()
	{
		Relay_Save_292();
	}

	private void Relay_Load_Out_289()
	{
		Relay_Load_292();
	}

	private void Relay_Restart_Out_289()
	{
		Relay_Set_False_292();
	}

	private void Relay_Save_289()
	{
		logic_SubGraph_SaveLoadBool_boolean_289 = local_CrazedIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_289 = local_CrazedIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.Save(ref logic_SubGraph_SaveLoadBool_boolean_289, logic_SubGraph_SaveLoadBool_boolAsVariable_289, logic_SubGraph_SaveLoadBool_uniqueID_289);
	}

	private void Relay_Load_289()
	{
		logic_SubGraph_SaveLoadBool_boolean_289 = local_CrazedIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_289 = local_CrazedIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.Load(ref logic_SubGraph_SaveLoadBool_boolean_289, logic_SubGraph_SaveLoadBool_boolAsVariable_289, logic_SubGraph_SaveLoadBool_uniqueID_289);
	}

	private void Relay_Set_True_289()
	{
		logic_SubGraph_SaveLoadBool_boolean_289 = local_CrazedIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_289 = local_CrazedIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_289, logic_SubGraph_SaveLoadBool_boolAsVariable_289, logic_SubGraph_SaveLoadBool_uniqueID_289);
	}

	private void Relay_Set_False_289()
	{
		logic_SubGraph_SaveLoadBool_boolean_289 = local_CrazedIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_289 = local_CrazedIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_289.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_289, logic_SubGraph_SaveLoadBool_boolAsVariable_289, logic_SubGraph_SaveLoadBool_uniqueID_289);
	}

	private void Relay_Save_Out_290()
	{
		Relay_Save_289();
	}

	private void Relay_Load_Out_290()
	{
		Relay_Load_289();
	}

	private void Relay_Restart_Out_290()
	{
		Relay_Set_False_289();
	}

	private void Relay_Save_290()
	{
		logic_SubGraph_SaveLoadBool_boolean_290 = local_NPCIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_290 = local_NPCIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.Save(ref logic_SubGraph_SaveLoadBool_boolean_290, logic_SubGraph_SaveLoadBool_boolAsVariable_290, logic_SubGraph_SaveLoadBool_uniqueID_290);
	}

	private void Relay_Load_290()
	{
		logic_SubGraph_SaveLoadBool_boolean_290 = local_NPCIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_290 = local_NPCIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.Load(ref logic_SubGraph_SaveLoadBool_boolean_290, logic_SubGraph_SaveLoadBool_boolAsVariable_290, logic_SubGraph_SaveLoadBool_uniqueID_290);
	}

	private void Relay_Set_True_290()
	{
		logic_SubGraph_SaveLoadBool_boolean_290 = local_NPCIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_290 = local_NPCIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_290, logic_SubGraph_SaveLoadBool_boolAsVariable_290, logic_SubGraph_SaveLoadBool_uniqueID_290);
	}

	private void Relay_Set_False_290()
	{
		logic_SubGraph_SaveLoadBool_boolean_290 = local_NPCIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_290 = local_NPCIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_290.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_290, logic_SubGraph_SaveLoadBool_boolAsVariable_290, logic_SubGraph_SaveLoadBool_uniqueID_290);
	}

	private void Relay_Save_Out_291()
	{
		Relay_Save_290();
	}

	private void Relay_Load_Out_291()
	{
		Relay_Load_290();
	}

	private void Relay_Restart_Out_291()
	{
		Relay_Set_False_290();
	}

	private void Relay_Save_291()
	{
		logic_SubGraph_SaveLoadBool_boolean_291 = local_NPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_291 = local_NPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.Save(ref logic_SubGraph_SaveLoadBool_boolean_291, logic_SubGraph_SaveLoadBool_boolAsVariable_291, logic_SubGraph_SaveLoadBool_uniqueID_291);
	}

	private void Relay_Load_291()
	{
		logic_SubGraph_SaveLoadBool_boolean_291 = local_NPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_291 = local_NPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.Load(ref logic_SubGraph_SaveLoadBool_boolean_291, logic_SubGraph_SaveLoadBool_boolAsVariable_291, logic_SubGraph_SaveLoadBool_uniqueID_291);
	}

	private void Relay_Set_True_291()
	{
		logic_SubGraph_SaveLoadBool_boolean_291 = local_NPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_291 = local_NPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_291, logic_SubGraph_SaveLoadBool_boolAsVariable_291, logic_SubGraph_SaveLoadBool_uniqueID_291);
	}

	private void Relay_Set_False_291()
	{
		logic_SubGraph_SaveLoadBool_boolean_291 = local_NPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_291 = local_NPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_291.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_291, logic_SubGraph_SaveLoadBool_boolAsVariable_291, logic_SubGraph_SaveLoadBool_uniqueID_291);
	}

	private void Relay_Save_Out_292()
	{
		Relay_Save_295();
	}

	private void Relay_Load_Out_292()
	{
		Relay_Load_295();
	}

	private void Relay_Restart_Out_292()
	{
		Relay_Set_False_295();
	}

	private void Relay_Save_292()
	{
		logic_SubGraph_SaveLoadBool_boolean_292 = local_CrazedNPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_292 = local_CrazedNPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.Save(ref logic_SubGraph_SaveLoadBool_boolean_292, logic_SubGraph_SaveLoadBool_boolAsVariable_292, logic_SubGraph_SaveLoadBool_uniqueID_292);
	}

	private void Relay_Load_292()
	{
		logic_SubGraph_SaveLoadBool_boolean_292 = local_CrazedNPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_292 = local_CrazedNPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.Load(ref logic_SubGraph_SaveLoadBool_boolean_292, logic_SubGraph_SaveLoadBool_boolAsVariable_292, logic_SubGraph_SaveLoadBool_uniqueID_292);
	}

	private void Relay_Set_True_292()
	{
		logic_SubGraph_SaveLoadBool_boolean_292 = local_CrazedNPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_292 = local_CrazedNPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_292, logic_SubGraph_SaveLoadBool_boolAsVariable_292, logic_SubGraph_SaveLoadBool_uniqueID_292);
	}

	private void Relay_Set_False_292()
	{
		logic_SubGraph_SaveLoadBool_boolean_292 = local_CrazedNPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_292 = local_CrazedNPCIgnored_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_292.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_292, logic_SubGraph_SaveLoadBool_boolAsVariable_292, logic_SubGraph_SaveLoadBool_uniqueID_292);
	}

	private void Relay_Save_Out_293()
	{
		Relay_Save_297();
	}

	private void Relay_Load_Out_293()
	{
		Relay_Load_297();
	}

	private void Relay_Restart_Out_293()
	{
		Relay_Set_False_297();
	}

	private void Relay_Save_293()
	{
		logic_SubGraph_SaveLoadBool_boolean_293 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_293 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.Save(ref logic_SubGraph_SaveLoadBool_boolean_293, logic_SubGraph_SaveLoadBool_boolAsVariable_293, logic_SubGraph_SaveLoadBool_uniqueID_293);
	}

	private void Relay_Load_293()
	{
		logic_SubGraph_SaveLoadBool_boolean_293 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_293 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.Load(ref logic_SubGraph_SaveLoadBool_boolean_293, logic_SubGraph_SaveLoadBool_boolAsVariable_293, logic_SubGraph_SaveLoadBool_uniqueID_293);
	}

	private void Relay_Set_True_293()
	{
		logic_SubGraph_SaveLoadBool_boolean_293 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_293 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_293, logic_SubGraph_SaveLoadBool_boolAsVariable_293, logic_SubGraph_SaveLoadBool_uniqueID_293);
	}

	private void Relay_Set_False_293()
	{
		logic_SubGraph_SaveLoadBool_boolean_293 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_293 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_293.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_293, logic_SubGraph_SaveLoadBool_boolAsVariable_293, logic_SubGraph_SaveLoadBool_uniqueID_293);
	}

	private void Relay_Save_Out_294()
	{
		Relay_Save_293();
	}

	private void Relay_Load_Out_294()
	{
		Relay_Load_293();
	}

	private void Relay_Restart_Out_294()
	{
		Relay_Set_False_293();
	}

	private void Relay_Save_294()
	{
		logic_SubGraph_SaveLoadBool_boolean_294 = local_HasBeenInterrupted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_294 = local_HasBeenInterrupted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.Save(ref logic_SubGraph_SaveLoadBool_boolean_294, logic_SubGraph_SaveLoadBool_boolAsVariable_294, logic_SubGraph_SaveLoadBool_uniqueID_294);
	}

	private void Relay_Load_294()
	{
		logic_SubGraph_SaveLoadBool_boolean_294 = local_HasBeenInterrupted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_294 = local_HasBeenInterrupted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.Load(ref logic_SubGraph_SaveLoadBool_boolean_294, logic_SubGraph_SaveLoadBool_boolAsVariable_294, logic_SubGraph_SaveLoadBool_uniqueID_294);
	}

	private void Relay_Set_True_294()
	{
		logic_SubGraph_SaveLoadBool_boolean_294 = local_HasBeenInterrupted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_294 = local_HasBeenInterrupted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_294, logic_SubGraph_SaveLoadBool_boolAsVariable_294, logic_SubGraph_SaveLoadBool_uniqueID_294);
	}

	private void Relay_Set_False_294()
	{
		logic_SubGraph_SaveLoadBool_boolean_294 = local_HasBeenInterrupted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_294 = local_HasBeenInterrupted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_294.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_294, logic_SubGraph_SaveLoadBool_boolAsVariable_294, logic_SubGraph_SaveLoadBool_uniqueID_294);
	}

	private void Relay_Save_Out_295()
	{
		Relay_Save_296();
	}

	private void Relay_Load_Out_295()
	{
		Relay_Load_296();
	}

	private void Relay_Restart_Out_295()
	{
		Relay_Set_False_296();
	}

	private void Relay_Save_295()
	{
		logic_SubGraph_SaveLoadBool_boolean_295 = local_CrazedNPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_295 = local_CrazedNPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.Save(ref logic_SubGraph_SaveLoadBool_boolean_295, logic_SubGraph_SaveLoadBool_boolAsVariable_295, logic_SubGraph_SaveLoadBool_uniqueID_295);
	}

	private void Relay_Load_295()
	{
		logic_SubGraph_SaveLoadBool_boolean_295 = local_CrazedNPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_295 = local_CrazedNPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.Load(ref logic_SubGraph_SaveLoadBool_boolean_295, logic_SubGraph_SaveLoadBool_boolAsVariable_295, logic_SubGraph_SaveLoadBool_uniqueID_295);
	}

	private void Relay_Set_True_295()
	{
		logic_SubGraph_SaveLoadBool_boolean_295 = local_CrazedNPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_295 = local_CrazedNPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_295, logic_SubGraph_SaveLoadBool_boolAsVariable_295, logic_SubGraph_SaveLoadBool_uniqueID_295);
	}

	private void Relay_Set_False_295()
	{
		logic_SubGraph_SaveLoadBool_boolean_295 = local_CrazedNPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_295 = local_CrazedNPCInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_295.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_295, logic_SubGraph_SaveLoadBool_boolAsVariable_295, logic_SubGraph_SaveLoadBool_uniqueID_295);
	}

	private void Relay_Save_Out_296()
	{
		Relay_Save_294();
	}

	private void Relay_Load_Out_296()
	{
		Relay_Load_294();
	}

	private void Relay_Restart_Out_296()
	{
		Relay_Set_False_294();
	}

	private void Relay_Save_296()
	{
		logic_SubGraph_SaveLoadBool_boolean_296 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_296 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Save(ref logic_SubGraph_SaveLoadBool_boolean_296, logic_SubGraph_SaveLoadBool_boolAsVariable_296, logic_SubGraph_SaveLoadBool_uniqueID_296);
	}

	private void Relay_Load_296()
	{
		logic_SubGraph_SaveLoadBool_boolean_296 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_296 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Load(ref logic_SubGraph_SaveLoadBool_boolean_296, logic_SubGraph_SaveLoadBool_boolAsVariable_296, logic_SubGraph_SaveLoadBool_uniqueID_296);
	}

	private void Relay_Set_True_296()
	{
		logic_SubGraph_SaveLoadBool_boolean_296 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_296 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_296, logic_SubGraph_SaveLoadBool_boolAsVariable_296, logic_SubGraph_SaveLoadBool_uniqueID_296);
	}

	private void Relay_Set_False_296()
	{
		logic_SubGraph_SaveLoadBool_boolean_296 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_296 = local_CrazedPlayInterruptOnce_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_296.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_296, logic_SubGraph_SaveLoadBool_boolAsVariable_296, logic_SubGraph_SaveLoadBool_uniqueID_296);
	}

	private void Relay_Save_Out_297()
	{
		Relay_Save_299();
	}

	private void Relay_Load_Out_297()
	{
		Relay_Load_299();
	}

	private void Relay_Restart_Out_297()
	{
		Relay_Set_False_299();
	}

	private void Relay_Save_297()
	{
		logic_SubGraph_SaveLoadBool_boolean_297 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_297 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Save(ref logic_SubGraph_SaveLoadBool_boolean_297, logic_SubGraph_SaveLoadBool_boolAsVariable_297, logic_SubGraph_SaveLoadBool_uniqueID_297);
	}

	private void Relay_Load_297()
	{
		logic_SubGraph_SaveLoadBool_boolean_297 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_297 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Load(ref logic_SubGraph_SaveLoadBool_boolean_297, logic_SubGraph_SaveLoadBool_boolAsVariable_297, logic_SubGraph_SaveLoadBool_uniqueID_297);
	}

	private void Relay_Set_True_297()
	{
		logic_SubGraph_SaveLoadBool_boolean_297 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_297 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_297, logic_SubGraph_SaveLoadBool_boolAsVariable_297, logic_SubGraph_SaveLoadBool_uniqueID_297);
	}

	private void Relay_Set_False_297()
	{
		logic_SubGraph_SaveLoadBool_boolean_297 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_297 = local_PlayerAttemptedMission_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_297, logic_SubGraph_SaveLoadBool_boolAsVariable_297, logic_SubGraph_SaveLoadBool_uniqueID_297);
	}

	private void Relay_Save_Out_298()
	{
		Relay_Save_301();
	}

	private void Relay_Load_Out_298()
	{
		Relay_Load_301();
	}

	private void Relay_Restart_Out_298()
	{
		Relay_Set_False_301();
	}

	private void Relay_Save_298()
	{
		logic_SubGraph_SaveLoadBool_boolean_298 = local_LeftAreaAfterLoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_298 = local_LeftAreaAfterLoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.Save(ref logic_SubGraph_SaveLoadBool_boolean_298, logic_SubGraph_SaveLoadBool_boolAsVariable_298, logic_SubGraph_SaveLoadBool_uniqueID_298);
	}

	private void Relay_Load_298()
	{
		logic_SubGraph_SaveLoadBool_boolean_298 = local_LeftAreaAfterLoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_298 = local_LeftAreaAfterLoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.Load(ref logic_SubGraph_SaveLoadBool_boolean_298, logic_SubGraph_SaveLoadBool_boolAsVariable_298, logic_SubGraph_SaveLoadBool_uniqueID_298);
	}

	private void Relay_Set_True_298()
	{
		logic_SubGraph_SaveLoadBool_boolean_298 = local_LeftAreaAfterLoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_298 = local_LeftAreaAfterLoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_298, logic_SubGraph_SaveLoadBool_boolAsVariable_298, logic_SubGraph_SaveLoadBool_uniqueID_298);
	}

	private void Relay_Set_False_298()
	{
		logic_SubGraph_SaveLoadBool_boolean_298 = local_LeftAreaAfterLoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_298 = local_LeftAreaAfterLoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_298.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_298, logic_SubGraph_SaveLoadBool_boolAsVariable_298, logic_SubGraph_SaveLoadBool_uniqueID_298);
	}

	private void Relay_Save_Out_299()
	{
		Relay_Save_300();
	}

	private void Relay_Load_Out_299()
	{
		Relay_Load_300();
	}

	private void Relay_Restart_Out_299()
	{
		Relay_Set_False_300();
	}

	private void Relay_Save_299()
	{
		logic_SubGraph_SaveLoadBool_boolean_299 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_299 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Save(ref logic_SubGraph_SaveLoadBool_boolean_299, logic_SubGraph_SaveLoadBool_boolAsVariable_299, logic_SubGraph_SaveLoadBool_uniqueID_299);
	}

	private void Relay_Load_299()
	{
		logic_SubGraph_SaveLoadBool_boolean_299 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_299 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Load(ref logic_SubGraph_SaveLoadBool_boolean_299, logic_SubGraph_SaveLoadBool_boolAsVariable_299, logic_SubGraph_SaveLoadBool_uniqueID_299);
	}

	private void Relay_Set_True_299()
	{
		logic_SubGraph_SaveLoadBool_boolean_299 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_299 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_299, logic_SubGraph_SaveLoadBool_boolAsVariable_299, logic_SubGraph_SaveLoadBool_uniqueID_299);
	}

	private void Relay_Set_False_299()
	{
		logic_SubGraph_SaveLoadBool_boolean_299 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_299 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_299.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_299, logic_SubGraph_SaveLoadBool_boolAsVariable_299, logic_SubGraph_SaveLoadBool_uniqueID_299);
	}

	private void Relay_Save_Out_300()
	{
		Relay_Save_298();
	}

	private void Relay_Load_Out_300()
	{
		Relay_Load_298();
	}

	private void Relay_Restart_Out_300()
	{
		Relay_Set_False_298();
	}

	private void Relay_Save_300()
	{
		logic_SubGraph_SaveLoadBool_boolean_300 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_300 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.Save(ref logic_SubGraph_SaveLoadBool_boolean_300, logic_SubGraph_SaveLoadBool_boolAsVariable_300, logic_SubGraph_SaveLoadBool_uniqueID_300);
	}

	private void Relay_Load_300()
	{
		logic_SubGraph_SaveLoadBool_boolean_300 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_300 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.Load(ref logic_SubGraph_SaveLoadBool_boolean_300, logic_SubGraph_SaveLoadBool_boolAsVariable_300, logic_SubGraph_SaveLoadBool_uniqueID_300);
	}

	private void Relay_Set_True_300()
	{
		logic_SubGraph_SaveLoadBool_boolean_300 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_300 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_300, logic_SubGraph_SaveLoadBool_boolAsVariable_300, logic_SubGraph_SaveLoadBool_uniqueID_300);
	}

	private void Relay_Set_False_300()
	{
		logic_SubGraph_SaveLoadBool_boolean_300 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_300 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_300.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_300, logic_SubGraph_SaveLoadBool_boolAsVariable_300, logic_SubGraph_SaveLoadBool_uniqueID_300);
	}

	private void Relay_Save_Out_301()
	{
		Relay_Save_302();
	}

	private void Relay_Load_Out_301()
	{
		Relay_Load_302();
	}

	private void Relay_Restart_Out_301()
	{
		Relay_Set_False_302();
	}

	private void Relay_Save_301()
	{
		logic_SubGraph_SaveLoadBool_boolean_301 = local_WentOutOfRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_301 = local_WentOutOfRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Save(ref logic_SubGraph_SaveLoadBool_boolean_301, logic_SubGraph_SaveLoadBool_boolAsVariable_301, logic_SubGraph_SaveLoadBool_uniqueID_301);
	}

	private void Relay_Load_301()
	{
		logic_SubGraph_SaveLoadBool_boolean_301 = local_WentOutOfRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_301 = local_WentOutOfRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Load(ref logic_SubGraph_SaveLoadBool_boolean_301, logic_SubGraph_SaveLoadBool_boolAsVariable_301, logic_SubGraph_SaveLoadBool_uniqueID_301);
	}

	private void Relay_Set_True_301()
	{
		logic_SubGraph_SaveLoadBool_boolean_301 = local_WentOutOfRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_301 = local_WentOutOfRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_301, logic_SubGraph_SaveLoadBool_boolAsVariable_301, logic_SubGraph_SaveLoadBool_uniqueID_301);
	}

	private void Relay_Set_False_301()
	{
		logic_SubGraph_SaveLoadBool_boolean_301 = local_WentOutOfRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_301 = local_WentOutOfRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_301, logic_SubGraph_SaveLoadBool_boolAsVariable_301, logic_SubGraph_SaveLoadBool_uniqueID_301);
	}

	private void Relay_Save_Out_302()
	{
		Relay_Save_572();
	}

	private void Relay_Load_Out_302()
	{
		Relay_Load_572();
	}

	private void Relay_Restart_Out_302()
	{
		Relay_Set_False_572();
	}

	private void Relay_Save_302()
	{
		logic_SubGraph_SaveLoadBool_boolean_302 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_302 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Save(ref logic_SubGraph_SaveLoadBool_boolean_302, logic_SubGraph_SaveLoadBool_boolAsVariable_302, logic_SubGraph_SaveLoadBool_uniqueID_302);
	}

	private void Relay_Load_302()
	{
		logic_SubGraph_SaveLoadBool_boolean_302 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_302 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Load(ref logic_SubGraph_SaveLoadBool_boolean_302, logic_SubGraph_SaveLoadBool_boolAsVariable_302, logic_SubGraph_SaveLoadBool_uniqueID_302);
	}

	private void Relay_Set_True_302()
	{
		logic_SubGraph_SaveLoadBool_boolean_302 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_302 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_302, logic_SubGraph_SaveLoadBool_boolAsVariable_302, logic_SubGraph_SaveLoadBool_uniqueID_302);
	}

	private void Relay_Set_False_302()
	{
		logic_SubGraph_SaveLoadBool_boolean_302 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_302 = local_CubeDeadVictory_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_302, logic_SubGraph_SaveLoadBool_boolAsVariable_302, logic_SubGraph_SaveLoadBool_uniqueID_302);
	}

	private void Relay_Save_Out_303()
	{
		Relay_Save_627();
	}

	private void Relay_Load_Out_303()
	{
		Relay_Load_627();
	}

	private void Relay_Restart_Out_303()
	{
		Relay_Set_False_627();
	}

	private void Relay_Save_303()
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = local_OutOfTime_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_303 = local_OutOfTime_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Save(ref logic_SubGraph_SaveLoadBool_boolean_303, logic_SubGraph_SaveLoadBool_boolAsVariable_303, logic_SubGraph_SaveLoadBool_uniqueID_303);
	}

	private void Relay_Load_303()
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = local_OutOfTime_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_303 = local_OutOfTime_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Load(ref logic_SubGraph_SaveLoadBool_boolean_303, logic_SubGraph_SaveLoadBool_boolAsVariable_303, logic_SubGraph_SaveLoadBool_uniqueID_303);
	}

	private void Relay_Set_True_303()
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = local_OutOfTime_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_303 = local_OutOfTime_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_303, logic_SubGraph_SaveLoadBool_boolAsVariable_303, logic_SubGraph_SaveLoadBool_uniqueID_303);
	}

	private void Relay_Set_False_303()
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = local_OutOfTime_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_303 = local_OutOfTime_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_303, logic_SubGraph_SaveLoadBool_boolAsVariable_303, logic_SubGraph_SaveLoadBool_uniqueID_303);
	}

	private void Relay_AtIndex_304()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_304.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_304, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_304, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_304.AtIndex(ref logic_uScript_AccessListTech_techList_304, logic_uScript_AccessListTech_index_304, out logic_uScript_AccessListTech_value_304);
		local_CrazedTechs_TankArray = logic_uScript_AccessListTech_techList_304;
		local_CrazedTech3_Tank = logic_uScript_AccessListTech_value_304;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_304.Out)
		{
			Relay_In_451();
		}
	}

	private void Relay_In_308()
	{
		logic_uScript_AddMessage_messageData_308 = MsgCrazedMinionInterrupt01;
		logic_uScript_AddMessage_speaker_308 = GroupMinionTechSpeaker;
		logic_uScript_AddMessage_Return_308 = logic_uScript_AddMessage_uScript_AddMessage_308.In(logic_uScript_AddMessage_messageData_308, logic_uScript_AddMessage_speaker_308);
		if (logic_uScript_AddMessage_uScript_AddMessage_308.Shown)
		{
			Relay_In_344();
		}
	}

	private void Relay_In_310()
	{
		logic_uScript_AddMessage_messageData_310 = MsgCrazedIntro04;
		logic_uScript_AddMessage_speaker_310 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_310 = logic_uScript_AddMessage_uScript_AddMessage_310.In(logic_uScript_AddMessage_messageData_310, logic_uScript_AddMessage_speaker_310);
		if (logic_uScript_AddMessage_uScript_AddMessage_310.Shown)
		{
			Relay_In_342();
		}
	}

	private void Relay_In_313()
	{
		logic_uScript_AddMessage_messageData_313 = MsgCrazedIntro03;
		logic_uScript_AddMessage_speaker_313 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_313 = logic_uScript_AddMessage_uScript_AddMessage_313.In(logic_uScript_AddMessage_messageData_313, logic_uScript_AddMessage_speaker_313);
		if (logic_uScript_AddMessage_uScript_AddMessage_313.Shown)
		{
			Relay_In_345();
		}
	}

	private void Relay_In_315()
	{
		logic_uScript_AddMessage_messageData_315 = MsgCrazedMinionInterrupt02;
		logic_uScript_AddMessage_speaker_315 = GroupMinionTechSpeaker;
		logic_uScript_AddMessage_Return_315 = logic_uScript_AddMessage_uScript_AddMessage_315.In(logic_uScript_AddMessage_messageData_315, logic_uScript_AddMessage_speaker_315);
		if (logic_uScript_AddMessage_uScript_AddMessage_315.Shown)
		{
			Relay_In_354();
		}
	}

	private void Relay_In_320()
	{
		logic_uScript_AddMessage_messageData_320 = MsgCrazedLeaderB4Fight01;
		logic_uScript_AddMessage_speaker_320 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_320 = logic_uScript_AddMessage_uScript_AddMessage_320.In(logic_uScript_AddMessage_messageData_320, logic_uScript_AddMessage_speaker_320);
		if (logic_uScript_AddMessage_uScript_AddMessage_320.Shown)
		{
			Relay_In_378();
		}
	}

	private void Relay_In_321()
	{
		logic_uScript_AddMessage_messageData_321 = MsgCrazedMinionInterruptB4Fight01;
		logic_uScript_AddMessage_speaker_321 = GroupMinionTechSpeaker;
		logic_uScript_AddMessage_Return_321 = logic_uScript_AddMessage_uScript_AddMessage_321.In(logic_uScript_AddMessage_messageData_321, logic_uScript_AddMessage_speaker_321);
		if (logic_uScript_AddMessage_uScript_AddMessage_321.Shown)
		{
			Relay_In_380();
		}
	}

	private void Relay_In_324()
	{
		logic_uScript_AddMessage_messageData_324 = MsgCrazedLeaderB4Fight02;
		logic_uScript_AddMessage_speaker_324 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_324 = logic_uScript_AddMessage_uScript_AddMessage_324.In(logic_uScript_AddMessage_messageData_324, logic_uScript_AddMessage_speaker_324);
		if (logic_uScript_AddMessage_uScript_AddMessage_324.Shown)
		{
			Relay_In_382();
		}
	}

	private void Relay_In_327()
	{
		logic_uScript_AddMessage_messageData_327 = MsgCrazedLeaderB4Fight03;
		logic_uScript_AddMessage_speaker_327 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_327 = logic_uScript_AddMessage_uScript_AddMessage_327.In(logic_uScript_AddMessage_messageData_327, logic_uScript_AddMessage_speaker_327);
		if (logic_uScript_AddMessage_uScript_AddMessage_327.Shown)
		{
			Relay_True_98();
		}
	}

	private void Relay_In_329()
	{
		logic_uScript_AddMessage_messageData_329 = MsgCubeDestroyed01;
		logic_uScript_AddMessage_speaker_329 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_329 = logic_uScript_AddMessage_uScript_AddMessage_329.In(logic_uScript_AddMessage_messageData_329, logic_uScript_AddMessage_speaker_329);
		if (logic_uScript_AddMessage_uScript_AddMessage_329.Shown)
		{
			Relay_In_400();
		}
	}

	private void Relay_In_334()
	{
		logic_uScript_AddMessage_messageData_334 = MsgCrazedIntro05;
		logic_uScript_AddMessage_speaker_334 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_334 = logic_uScript_AddMessage_uScript_AddMessage_334.In(logic_uScript_AddMessage_messageData_334, logic_uScript_AddMessage_speaker_334);
		if (logic_uScript_AddMessage_uScript_AddMessage_334.Out)
		{
			Relay_In_357();
		}
	}

	private void Relay_In_335()
	{
		logic_uScriptCon_CompareBool_Bool_335 = local_HasBeenInterrupted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_335.In(logic_uScriptCon_CompareBool_Bool_335);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_335.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_335.False;
		if (num)
		{
			Relay_In_349();
		}
		if (flag)
		{
			Relay_In_308();
		}
	}

	private void Relay_Output1_336()
	{
		Relay_In_226();
	}

	private void Relay_Output2_336()
	{
		Relay_In_372();
	}

	private void Relay_Output3_336()
	{
		Relay_In_335();
	}

	private void Relay_Output4_336()
	{
		Relay_In_313();
	}

	private void Relay_Output5_336()
	{
		Relay_In_347();
	}

	private void Relay_Output6_336()
	{
		Relay_In_310();
	}

	private void Relay_Output7_336()
	{
		Relay_In_366();
	}

	private void Relay_Output8_336()
	{
		Relay_In_334();
	}

	private void Relay_In_336()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_336 = local_CrazedDialog_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_336.In(logic_uScriptCon_ManualSwitch_CurrentOutput_336);
	}

	private void Relay_In_339()
	{
		logic_uScriptAct_AddInt_v2_A_339 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_339.In(logic_uScriptAct_AddInt_v2_A_339, logic_uScriptAct_AddInt_v2_B_339, out logic_uScriptAct_AddInt_v2_IntResult_339, out logic_uScriptAct_AddInt_v2_FloatResult_339);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_339;
	}

	private void Relay_In_342()
	{
		logic_uScriptAct_AddInt_v2_A_342 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_342.In(logic_uScriptAct_AddInt_v2_A_342, logic_uScriptAct_AddInt_v2_B_342, out logic_uScriptAct_AddInt_v2_IntResult_342, out logic_uScriptAct_AddInt_v2_FloatResult_342);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_342;
	}

	private void Relay_In_344()
	{
		logic_uScriptAct_AddInt_v2_A_344 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_344.In(logic_uScriptAct_AddInt_v2_A_344, logic_uScriptAct_AddInt_v2_B_344, out logic_uScriptAct_AddInt_v2_IntResult_344, out logic_uScriptAct_AddInt_v2_FloatResult_344);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_344;
	}

	private void Relay_In_345()
	{
		logic_uScriptAct_AddInt_v2_A_345 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_345.In(logic_uScriptAct_AddInt_v2_A_345, logic_uScriptAct_AddInt_v2_B_345, out logic_uScriptAct_AddInt_v2_IntResult_345, out logic_uScriptAct_AddInt_v2_FloatResult_345);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_345;
	}

	private void Relay_In_347()
	{
		logic_uScriptCon_CompareBool_Bool_347 = local_HasBeenInterrupted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_347.In(logic_uScriptCon_CompareBool_Bool_347);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_347.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_347.False;
		if (num)
		{
			Relay_In_352();
		}
		if (flag)
		{
			Relay_In_315();
		}
	}

	private void Relay_In_349()
	{
		logic_uScriptAct_SubtractInt_A_349 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_349.In(logic_uScriptAct_SubtractInt_A_349, logic_uScriptAct_SubtractInt_B_349, out logic_uScriptAct_SubtractInt_IntResult_349, out logic_uScriptAct_SubtractInt_FloatResult_349);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_SubtractInt_IntResult_349;
		if (logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_349.Out)
		{
			Relay_False_386();
		}
	}

	private void Relay_In_352()
	{
		logic_uScriptAct_SubtractInt_A_352 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_352.In(logic_uScriptAct_SubtractInt_A_352, logic_uScriptAct_SubtractInt_B_352, out logic_uScriptAct_SubtractInt_IntResult_352, out logic_uScriptAct_SubtractInt_FloatResult_352);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_SubtractInt_IntResult_352;
		if (logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_352.Out)
		{
			Relay_False_388();
		}
	}

	private void Relay_In_354()
	{
		logic_uScriptAct_AddInt_v2_A_354 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_354.In(logic_uScriptAct_AddInt_v2_A_354, logic_uScriptAct_AddInt_v2_B_354, out logic_uScriptAct_AddInt_v2_IntResult_354, out logic_uScriptAct_AddInt_v2_FloatResult_354);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_354;
	}

	private void Relay_In_357()
	{
		logic_uScript_SetEncounterTarget_owner_357 = owner_Connection_358;
		logic_uScript_SetEncounterTarget_visibleObject_357 = local_CubeTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_357.In(logic_uScript_SetEncounterTarget_owner_357, logic_uScript_SetEncounterTarget_visibleObject_357);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_357.Out)
		{
			Relay_In_359();
		}
	}

	private void Relay_Out_359()
	{
		Relay_True_361();
	}

	private void Relay_In_359()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_359 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_359.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_359, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_359);
	}

	private void Relay_True_361()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_361.True(out logic_uScriptAct_SetBool_Target_361);
		local_CrazedIntroPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_361;
	}

	private void Relay_False_361()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_361.False(out logic_uScriptAct_SetBool_Target_361);
		local_CrazedIntroPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_361;
	}

	private void Relay_In_362()
	{
		logic_uScriptAct_SubtractInt_A_362 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_362.In(logic_uScriptAct_SubtractInt_A_362, logic_uScriptAct_SubtractInt_B_362, out logic_uScriptAct_SubtractInt_IntResult_362, out logic_uScriptAct_SubtractInt_FloatResult_362);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_SubtractInt_IntResult_362;
		if (logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_362.Out)
		{
			Relay_False_384();
		}
	}

	private void Relay_In_366()
	{
		logic_uScriptCon_CompareBool_Bool_366 = local_HasBeenInterrupted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_366.In(logic_uScriptCon_CompareBool_Bool_366);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_366.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_366.False;
		if (num)
		{
			Relay_In_362();
		}
		if (flag)
		{
			Relay_In_367();
		}
	}

	private void Relay_In_367()
	{
		logic_uScript_AddMessage_messageData_367 = MsgCrazedMinionInterrupt03;
		logic_uScript_AddMessage_speaker_367 = GroupMinionTechSpeaker;
		logic_uScript_AddMessage_Return_367 = logic_uScript_AddMessage_uScript_AddMessage_367.In(logic_uScript_AddMessage_messageData_367, logic_uScript_AddMessage_speaker_367);
		if (logic_uScript_AddMessage_uScript_AddMessage_367.Shown)
		{
			Relay_In_368();
		}
	}

	private void Relay_In_368()
	{
		logic_uScriptAct_AddInt_v2_A_368 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_368.In(logic_uScriptAct_AddInt_v2_A_368, logic_uScriptAct_AddInt_v2_B_368, out logic_uScriptAct_AddInt_v2_IntResult_368, out logic_uScriptAct_AddInt_v2_FloatResult_368);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_368;
	}

	private void Relay_In_372()
	{
		logic_uScript_AddMessage_messageData_372 = MsgCrazedIntro02;
		logic_uScript_AddMessage_speaker_372 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_372 = logic_uScript_AddMessage_uScript_AddMessage_372.In(logic_uScript_AddMessage_messageData_372, logic_uScript_AddMessage_speaker_372);
		if (logic_uScript_AddMessage_uScript_AddMessage_372.Shown)
		{
			Relay_In_375();
		}
	}

	private void Relay_In_375()
	{
		logic_uScriptAct_AddInt_v2_A_375 = local_CrazedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_375.In(logic_uScriptAct_AddInt_v2_A_375, logic_uScriptAct_AddInt_v2_B_375, out logic_uScriptAct_AddInt_v2_IntResult_375, out logic_uScriptAct_AddInt_v2_FloatResult_375);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_375;
	}

	private void Relay_Output1_376()
	{
		Relay_In_320();
	}

	private void Relay_Output2_376()
	{
		Relay_In_321();
	}

	private void Relay_Output3_376()
	{
		Relay_In_324();
	}

	private void Relay_Output4_376()
	{
		Relay_In_327();
	}

	private void Relay_Output5_376()
	{
	}

	private void Relay_Output6_376()
	{
	}

	private void Relay_Output7_376()
	{
	}

	private void Relay_Output8_376()
	{
	}

	private void Relay_In_376()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_376 = local_CubeDialog_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_376.In(logic_uScriptCon_ManualSwitch_CurrentOutput_376);
	}

	private void Relay_In_378()
	{
		logic_uScriptAct_AddInt_v2_A_378 = local_CubeDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_378.In(logic_uScriptAct_AddInt_v2_A_378, logic_uScriptAct_AddInt_v2_B_378, out logic_uScriptAct_AddInt_v2_IntResult_378, out logic_uScriptAct_AddInt_v2_FloatResult_378);
		local_CubeDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_378;
	}

	private void Relay_In_380()
	{
		logic_uScriptAct_AddInt_v2_A_380 = local_CubeDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_380.In(logic_uScriptAct_AddInt_v2_A_380, logic_uScriptAct_AddInt_v2_B_380, out logic_uScriptAct_AddInt_v2_IntResult_380, out logic_uScriptAct_AddInt_v2_FloatResult_380);
		local_CubeDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_380;
	}

	private void Relay_In_382()
	{
		logic_uScriptAct_AddInt_v2_A_382 = local_CubeDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_382.In(logic_uScriptAct_AddInt_v2_A_382, logic_uScriptAct_AddInt_v2_B_382, out logic_uScriptAct_AddInt_v2_IntResult_382, out logic_uScriptAct_AddInt_v2_FloatResult_382);
		local_CubeDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_382;
	}

	private void Relay_True_384()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_384.True(out logic_uScriptAct_SetBool_Target_384);
		local_HasBeenInterrupted_System_Boolean = logic_uScriptAct_SetBool_Target_384;
	}

	private void Relay_False_384()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_384.False(out logic_uScriptAct_SetBool_Target_384);
		local_HasBeenInterrupted_System_Boolean = logic_uScriptAct_SetBool_Target_384;
	}

	private void Relay_True_386()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_386.True(out logic_uScriptAct_SetBool_Target_386);
		local_HasBeenInterrupted_System_Boolean = logic_uScriptAct_SetBool_Target_386;
	}

	private void Relay_False_386()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_386.False(out logic_uScriptAct_SetBool_Target_386);
		local_HasBeenInterrupted_System_Boolean = logic_uScriptAct_SetBool_Target_386;
	}

	private void Relay_True_388()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_388.True(out logic_uScriptAct_SetBool_Target_388);
		local_HasBeenInterrupted_System_Boolean = logic_uScriptAct_SetBool_Target_388;
	}

	private void Relay_False_388()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_388.False(out logic_uScriptAct_SetBool_Target_388);
		local_HasBeenInterrupted_System_Boolean = logic_uScriptAct_SetBool_Target_388;
	}

	private void Relay_Pause_390()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_390.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_390.Out)
		{
			Relay_Succeed_53();
		}
	}

	private void Relay_UnPause_390()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_390.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_390.Out)
		{
			Relay_Succeed_53();
		}
	}

	private void Relay_In_392()
	{
		logic_uScript_FlyTechUpAndAway_tech_392 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_392 = TechFlyAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_392 = TechFlyParticles;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_392.In(logic_uScript_FlyTechUpAndAway_tech_392, logic_uScript_FlyTechUpAndAway_maxLifetime_392, logic_uScript_FlyTechUpAndAway_targetHeight_392, logic_uScript_FlyTechUpAndAway_aiTree_392, logic_uScript_FlyTechUpAndAway_removalParticles_392);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_392.Out)
		{
			Relay_In_66();
		}
	}

	private void Relay_Output1_395()
	{
		Relay_In_45();
	}

	private void Relay_Output2_395()
	{
		Relay_In_469();
	}

	private void Relay_Output3_395()
	{
	}

	private void Relay_Output4_395()
	{
	}

	private void Relay_Output5_395()
	{
	}

	private void Relay_Output6_395()
	{
	}

	private void Relay_Output7_395()
	{
	}

	private void Relay_Output8_395()
	{
	}

	private void Relay_In_395()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_395 = local_NPCIntro_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_395.In(logic_uScriptCon_ManualSwitch_CurrentOutput_395);
	}

	private void Relay_In_396()
	{
		logic_uScriptAct_AddInt_v2_A_396 = local_NPCIntro_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_396.In(logic_uScriptAct_AddInt_v2_A_396, logic_uScriptAct_AddInt_v2_B_396, out logic_uScriptAct_AddInt_v2_IntResult_396, out logic_uScriptAct_AddInt_v2_FloatResult_396);
		local_NPCIntro_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_396;
	}

	private void Relay_Output1_399()
	{
		Relay_In_329();
	}

	private void Relay_Output2_399()
	{
		Relay_In_60();
	}

	private void Relay_Output3_399()
	{
		Relay_In_495();
	}

	private void Relay_Output4_399()
	{
	}

	private void Relay_Output5_399()
	{
	}

	private void Relay_Output6_399()
	{
	}

	private void Relay_Output7_399()
	{
	}

	private void Relay_Output8_399()
	{
	}

	private void Relay_In_399()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_399 = local_CubeDestroyedDialog_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_399.In(logic_uScriptCon_ManualSwitch_CurrentOutput_399);
	}

	private void Relay_In_400()
	{
		logic_uScriptAct_AddInt_v2_A_400 = local_CubeDestroyedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_400.In(logic_uScriptAct_AddInt_v2_A_400, logic_uScriptAct_AddInt_v2_B_400, out logic_uScriptAct_AddInt_v2_IntResult_400, out logic_uScriptAct_AddInt_v2_FloatResult_400);
		local_CubeDestroyedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_400;
	}

	private void Relay_In_403()
	{
		logic_uScriptAct_AddInt_v2_A_403 = local_CubeDestroyedDialog_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_403.In(logic_uScriptAct_AddInt_v2_A_403, logic_uScriptAct_AddInt_v2_B_403, out logic_uScriptAct_AddInt_v2_IntResult_403, out logic_uScriptAct_AddInt_v2_FloatResult_403);
		local_CubeDestroyedDialog_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_403;
	}

	private void Relay_In_405()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_405 = NPCIntroStartTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_405.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_405);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_405.InRange)
		{
			Relay_True_418();
		}
	}

	private void Relay_Out_407()
	{
		Relay_In_411();
	}

	private void Relay_In_407()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_407 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_407.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_407, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_407);
	}

	private void Relay_In_411()
	{
		logic_uScript_SetEncounterTarget_owner_411 = owner_Connection_415;
		logic_uScript_SetEncounterTarget_visibleObject_411 = local_CrazedTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_411.In(logic_uScript_SetEncounterTarget_owner_411, logic_uScript_SetEncounterTarget_visibleObject_411);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_411.Out)
		{
			Relay_In_413();
		}
	}

	private void Relay_In_413()
	{
		logic_uScript_FlyTechUpAndAway_tech_413 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_413 = TechFlyAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_413 = TechFlyParticles;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_413.In(logic_uScript_FlyTechUpAndAway_tech_413, logic_uScript_FlyTechUpAndAway_maxLifetime_413, logic_uScript_FlyTechUpAndAway_targetHeight_413, logic_uScript_FlyTechUpAndAway_aiTree_413, logic_uScript_FlyTechUpAndAway_removalParticles_413);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_413.Out)
		{
			Relay_True_417();
		}
	}

	private void Relay_True_417()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_417.True(out logic_uScriptAct_SetBool_Target_417);
		local_NPCIntroPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_417;
	}

	private void Relay_False_417()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_417.False(out logic_uScriptAct_SetBool_Target_417);
		local_NPCIntroPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_417;
	}

	private void Relay_True_418()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_418.True(out logic_uScriptAct_SetBool_Target_418);
		local_IntroTechInRange_System_Boolean = logic_uScriptAct_SetBool_Target_418;
	}

	private void Relay_False_418()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_418.False(out logic_uScriptAct_SetBool_Target_418);
		local_IntroTechInRange_System_Boolean = logic_uScriptAct_SetBool_Target_418;
	}

	private void Relay_In_419()
	{
		logic_uScriptCon_CompareBool_Bool_419 = local_IntroTechInRange_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_419.In(logic_uScriptCon_CompareBool_Bool_419);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_419.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_419.False;
		if (num)
		{
			Relay_In_766();
		}
		if (flag)
		{
			Relay_In_405();
		}
	}

	private void Relay_True_423()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_423.True(out logic_uScriptAct_SetBool_Target_423);
		local_IntroSkipped_System_Boolean = logic_uScriptAct_SetBool_Target_423;
	}

	private void Relay_False_423()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_423.False(out logic_uScriptAct_SetBool_Target_423);
		local_IntroSkipped_System_Boolean = logic_uScriptAct_SetBool_Target_423;
	}

	private void Relay_In_424()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_424 = NPCIntroTechInRange;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_424.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_424);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_424.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_424.OutOfRange;
		if (inRange)
		{
			Relay_In_395();
		}
		if (outOfRange)
		{
			Relay_True_423();
		}
	}

	private void Relay_In_426()
	{
		logic_uScriptCon_CompareBool_Bool_426 = local_IntroSkipped_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_426.In(logic_uScriptCon_CompareBool_Bool_426);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_426.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_426.False;
		if (num)
		{
			Relay_In_215();
		}
		if (flag)
		{
			Relay_In_419();
		}
	}

	private void Relay_In_428()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_428 = LeaderIntroStartTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_428.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_428);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_428.InRange)
		{
			Relay_True_221();
		}
	}

	private void Relay_In_429()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_429 = LeaderOutOfRangeTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_429.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_429);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_429.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_429.OutOfRange;
		if (inRange)
		{
			Relay_In_742();
		}
		if (outOfRange)
		{
			Relay_In_225();
		}
	}

	private void Relay_True_434()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_434.True(out logic_uScriptAct_SetBool_Target_434);
		local_CrazedPlayInterruptOnce_System_Boolean = logic_uScriptAct_SetBool_Target_434;
	}

	private void Relay_False_434()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_434.False(out logic_uScriptAct_SetBool_Target_434);
		local_CrazedPlayInterruptOnce_System_Boolean = logic_uScriptAct_SetBool_Target_434;
	}

	private void Relay_AtIndex_437()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_437.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_437, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_437, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_437.AtIndex(ref logic_uScript_AccessListTech_techList_437, logic_uScript_AccessListTech_index_437, out logic_uScript_AccessListTech_value_437);
		local_CrazedTechs_TankArray = logic_uScript_AccessListTech_techList_437;
		local_CrazedTech02_Tank = logic_uScript_AccessListTech_value_437;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_437.Out)
		{
			Relay_In_438();
		}
	}

	private void Relay_In_438()
	{
		logic_uScript_SetTankInvulnerable_tank_438 = local_CrazedTech02_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_438.In(logic_uScript_SetTankInvulnerable_invulnerable_438, logic_uScript_SetTankInvulnerable_tank_438);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_438.Out)
		{
			Relay_In_480();
		}
	}

	private void Relay_In_440()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_440.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_440, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_440, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_440.In(ref logic_uScript_SetTechsTeam_techs_440, logic_uScript_SetTechsTeam_team_440);
		local_CrazedTechs_TankArray = logic_uScript_SetTechsTeam_techs_440;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_440.Out)
		{
			Relay_AtIndex_437();
		}
	}

	private void Relay_InitialSpawn_443()
	{
		int num = 0;
		Array crazedLeaderTechData = CrazedLeaderTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_443.Length != num + crazedLeaderTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_443, num + crazedLeaderTechData.Length);
		}
		Array.Copy(crazedLeaderTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_443, num, crazedLeaderTechData.Length);
		num += crazedLeaderTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_443 = owner_Connection_441;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_443.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_443, logic_uScript_SpawnTechsFromData_ownerNode_443, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_443, logic_uScript_SpawnTechsFromData_allowResurrection_443);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_443.Out)
		{
			Relay_True_17();
		}
	}

	private void Relay_AtIndex_444()
	{
		int num = 0;
		Array array = local_CrazedLeaderTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_444.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_444, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_444, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_444.AtIndex(ref logic_uScript_AccessListTech_techList_444, logic_uScript_AccessListTech_index_444, out logic_uScript_AccessListTech_value_444);
		local_CrazedLeaderTechs_TankArray = logic_uScript_AccessListTech_techList_444;
		local_CrazedTech_Tank = logic_uScript_AccessListTech_value_444;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_444.Out)
		{
			Relay_In_445();
		}
	}

	private void Relay_In_445()
	{
		logic_uScript_SetTankInvulnerable_tank_445 = local_CrazedTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_445.In(logic_uScript_SetTankInvulnerable_invulnerable_445, logic_uScript_SetTankInvulnerable_tank_445);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_445.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_447()
	{
		int num = 0;
		Array crazedLeaderTechData = CrazedLeaderTechData;
		if (logic_uScript_GetAndCheckTechs_techData_447.Length != num + crazedLeaderTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_447, num + crazedLeaderTechData.Length);
		}
		Array.Copy(crazedLeaderTechData, 0, logic_uScript_GetAndCheckTechs_techData_447, num, crazedLeaderTechData.Length);
		num += crazedLeaderTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_447 = owner_Connection_448;
		int num2 = 0;
		Array array = local_CrazedLeaderTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_447.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_447, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_447, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_447 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_447.In(logic_uScript_GetAndCheckTechs_techData_447, logic_uScript_GetAndCheckTechs_ownerNode_447, ref logic_uScript_GetAndCheckTechs_techs_447);
		local_CrazedLeaderTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_447;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_447.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_447.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_444();
		}
		if (someAlive)
		{
			Relay_AtIndex_444();
		}
	}

	private void Relay_In_451()
	{
		logic_uScript_SetTankInvulnerable_tank_451 = local_CrazedTech3_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_451.In(logic_uScript_SetTankInvulnerable_invulnerable_451, logic_uScript_SetTankInvulnerable_tank_451);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_451.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_In_454()
	{
		logic_uScript_FlyTechUpAndAway_tech_454 = local_CrazedTech_Tank;
		logic_uScript_FlyTechUpAndAway_removalParticles_454 = TechFlyParticles;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_454.In(logic_uScript_FlyTechUpAndAway_tech_454, logic_uScript_FlyTechUpAndAway_maxLifetime_454, logic_uScript_FlyTechUpAndAway_targetHeight_454, logic_uScript_FlyTechUpAndAway_aiTree_454, logic_uScript_FlyTechUpAndAway_removalParticles_454);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_454.Out)
		{
			Relay_True_516();
		}
	}

	private void Relay_In_456()
	{
		logic_uScript_AddMessage_messageData_456 = MsgMissionComplete;
		logic_uScript_AddMessage_speaker_456 = GCSpeaker;
		logic_uScript_AddMessage_Return_456 = logic_uScript_AddMessage_uScript_AddMessage_456.In(logic_uScript_AddMessage_messageData_456, logic_uScript_AddMessage_speaker_456);
		if (logic_uScript_AddMessage_uScript_AddMessage_456.Out)
		{
			Relay_UnPause_390();
		}
	}

	private void Relay_In_459()
	{
		logic_uScriptCon_CompareBool_Bool_459 = local_msgTooEarlyPlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_459.In(logic_uScriptCon_CompareBool_Bool_459);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_459.False)
		{
			Relay_In_462();
		}
	}

	private void Relay_In_460()
	{
		logic_uScript_AddMessage_messageData_460 = msgTooEarly;
		logic_uScript_AddMessage_speaker_460 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_460 = logic_uScript_AddMessage_uScript_AddMessage_460.In(logic_uScript_AddMessage_messageData_460, logic_uScript_AddMessage_speaker_460);
		if (logic_uScript_AddMessage_uScript_AddMessage_460.Out)
		{
			Relay_True_465();
		}
	}

	private void Relay_In_462()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_462 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_462.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_462);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_462.InRange)
		{
			Relay_In_496();
		}
	}

	private void Relay_True_465()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_465.True(out logic_uScriptAct_SetBool_Target_465);
		local_msgTooEarlyPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_465;
	}

	private void Relay_False_465()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_465.False(out logic_uScriptAct_SetBool_Target_465);
		local_msgTooEarlyPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_465;
	}

	private void Relay_In_469()
	{
		logic_uScript_AddMessage_messageData_469 = MsgNPCIntro02;
		logic_uScript_AddMessage_speaker_469 = NPCTechSpeaker;
		logic_uScript_AddMessage_Return_469 = logic_uScript_AddMessage_uScript_AddMessage_469.In(logic_uScript_AddMessage_messageData_469, logic_uScript_AddMessage_speaker_469);
		if (logic_uScript_AddMessage_uScript_AddMessage_469.Out)
		{
			Relay_In_62();
		}
	}

	private void Relay_AtIndex_472()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_472.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_472, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_472, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_472.AtIndex(ref logic_uScript_AccessListTech_techList_472, logic_uScript_AccessListTech_index_472, out logic_uScript_AccessListTech_value_472);
		local_CrazedTechs_TankArray = logic_uScript_AccessListTech_techList_472;
		local_CrazedTech03_Tank = logic_uScript_AccessListTech_value_472;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_472.Out)
		{
			Relay_In_473();
		}
	}

	private void Relay_In_473()
	{
		logic_uScript_SetTankInvulnerable_tank_473 = local_CrazedTech03_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_473.In(logic_uScript_SetTankInvulnerable_invulnerable_473, logic_uScript_SetTankInvulnerable_tank_473);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_473.Out)
		{
			Relay_In_482();
		}
	}

	private void Relay_In_474()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_474.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_474.Out)
		{
			Relay_In_426();
		}
	}

	private void Relay_In_475()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_475.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_475, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_475, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_475.In(ref logic_uScript_SetTechsTeam_techs_475, logic_uScript_SetTechsTeam_team_475);
		local_CubeTechs_TankArray = logic_uScript_SetTechsTeam_techs_475;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_475.Out)
		{
			Relay_In_176();
		}
	}

	private void Relay_True_479()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_479.True(out logic_uScriptAct_SetBool_Target_479);
		local_FinalObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_479;
	}

	private void Relay_False_479()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_479.False(out logic_uScriptAct_SetBool_Target_479);
		local_FinalObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_479;
	}

	private void Relay_In_480()
	{
		logic_uScript_SetTechAIType_tech_480 = local_CrazedTech02_Tank;
		logic_uScript_SetTechAIType_aiType_480 = local_481_AITreeType_AITypes;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_480.In(logic_uScript_SetTechAIType_tech_480, logic_uScript_SetTechAIType_aiType_480);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_480.Out)
		{
			Relay_AtIndex_472();
		}
	}

	private void Relay_In_482()
	{
		logic_uScript_SetTechAIType_tech_482 = local_CrazedTech03_Tank;
		logic_uScript_SetTechAIType_aiType_482 = local_483_AITreeType_AITypes;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_482.In(logic_uScript_SetTechAIType_tech_482, logic_uScript_SetTechAIType_aiType_482);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_482.Out)
		{
			Relay_True_145();
		}
	}

	private void Relay_Output1_485()
	{
		Relay_In_252();
	}

	private void Relay_Output2_485()
	{
		Relay_In_487();
	}

	private void Relay_Output3_485()
	{
	}

	private void Relay_Output4_485()
	{
	}

	private void Relay_Output5_485()
	{
	}

	private void Relay_Output6_485()
	{
	}

	private void Relay_Output7_485()
	{
	}

	private void Relay_Output8_485()
	{
	}

	private void Relay_In_485()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_485 = local_OutOfTimeMsg_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_485.In(logic_uScriptCon_ManualSwitch_CurrentOutput_485);
	}

	private void Relay_In_487()
	{
		logic_uScript_AddMessage_messageData_487 = MsgOutOfTime2;
		logic_uScript_AddMessage_speaker_487 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_487 = logic_uScript_AddMessage_uScript_AddMessage_487.In(logic_uScript_AddMessage_messageData_487, logic_uScript_AddMessage_speaker_487);
		if (logic_uScript_AddMessage_uScript_AddMessage_487.Shown)
		{
			Relay_False_489();
		}
	}

	private void Relay_True_489()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_489.True(out logic_uScriptAct_SetBool_Target_489);
		local_OutOfTime_System_Boolean = logic_uScriptAct_SetBool_Target_489;
	}

	private void Relay_False_489()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_489.False(out logic_uScriptAct_SetBool_Target_489);
		local_OutOfTime_System_Boolean = logic_uScriptAct_SetBool_Target_489;
	}

	private void Relay_In_491()
	{
		logic_uScriptAct_AddInt_v2_A_491 = local_OutOfTimeMsg_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_491.In(logic_uScriptAct_AddInt_v2_A_491, logic_uScriptAct_AddInt_v2_B_491, out logic_uScriptAct_AddInt_v2_IntResult_491, out logic_uScriptAct_AddInt_v2_FloatResult_491);
		local_OutOfTimeMsg_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_491;
	}

	private void Relay_Out_495()
	{
		Relay_In_440();
	}

	private void Relay_In_495()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_495 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_495.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_495, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_495);
	}

	private void Relay_In_496()
	{
		logic_uScriptCon_CompareBool_Bool_496 = local_NPCIntroPlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_496.In(logic_uScriptCon_CompareBool_Bool_496);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_496.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_496.False;
		if (num)
		{
			Relay_In_499();
		}
		if (flag)
		{
			Relay_In_460();
		}
	}

	private void Relay_In_499()
	{
		logic_uScript_AddMessage_messageData_499 = msgTooEarly2;
		logic_uScript_AddMessage_speaker_499 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_499 = logic_uScript_AddMessage_uScript_AddMessage_499.In(logic_uScript_AddMessage_messageData_499, logic_uScript_AddMessage_speaker_499);
		if (logic_uScript_AddMessage_uScript_AddMessage_499.Out)
		{
			Relay_True_501();
		}
	}

	private void Relay_True_501()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_501.True(out logic_uScriptAct_SetBool_Target_501);
		local_msgTooEarlyPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_501;
	}

	private void Relay_False_501()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_501.False(out logic_uScriptAct_SetBool_Target_501);
		local_msgTooEarlyPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_501;
	}

	private void Relay_In_503()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_503 = LeaderIntroStartTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_503.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_503);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_503.InRange)
		{
			Relay_In_506();
		}
	}

	private void Relay_In_506()
	{
		logic_uScript_AddMessage_messageData_506 = MsgLeaderTryAgain01;
		logic_uScript_AddMessage_speaker_506 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_506 = logic_uScript_AddMessage_uScript_AddMessage_506.In(logic_uScript_AddMessage_messageData_506, logic_uScript_AddMessage_speaker_506);
		if (logic_uScript_AddMessage_uScript_AddMessage_506.Out)
		{
			Relay_In_560();
		}
	}

	private void Relay_In_508()
	{
		int num = 0;
		Array crazedTechData = CrazedTechData;
		if (logic_uScript_GetAndCheckTechs_techData_508.Length != num + crazedTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_508, num + crazedTechData.Length);
		}
		Array.Copy(crazedTechData, 0, logic_uScript_GetAndCheckTechs_techData_508, num, crazedTechData.Length);
		num += crazedTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_508 = owner_Connection_510;
		int num2 = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_508.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_508, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_508, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_508 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_508.In(logic_uScript_GetAndCheckTechs_techData_508, logic_uScript_GetAndCheckTechs_ownerNode_508, ref logic_uScript_GetAndCheckTechs_techs_508);
		local_CrazedTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_508;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_508.AllDead)
		{
			Relay_True_479();
		}
	}

	private void Relay_True_512()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_512.True(out logic_uScriptAct_SetBool_Target_512);
		local_CrazedAmbushTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_512;
	}

	private void Relay_False_512()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_512.False(out logic_uScriptAct_SetBool_Target_512);
		local_CrazedAmbushTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_512;
	}

	private void Relay_In_514()
	{
		logic_uScriptCon_CompareBool_Bool_514 = local_CrazedAmbushMsgTriggered_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_514.In(logic_uScriptCon_CompareBool_Bool_514);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_514.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_514.False;
		if (num)
		{
			Relay_In_508();
		}
		if (flag)
		{
			Relay_In_208();
		}
	}

	private void Relay_True_516()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_516.True(out logic_uScriptAct_SetBool_Target_516);
		local_CrazedAmbushMsgTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_516;
	}

	private void Relay_False_516()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_516.False(out logic_uScriptAct_SetBool_Target_516);
		local_CrazedAmbushMsgTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_516;
	}

	private void Relay_True_518()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_518.True(out logic_uScriptAct_SetBool_Target_518);
		local_PlayedTryAgainMsg_System_Boolean = logic_uScriptAct_SetBool_Target_518;
	}

	private void Relay_False_518()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_518.False(out logic_uScriptAct_SetBool_Target_518);
		local_PlayedTryAgainMsg_System_Boolean = logic_uScriptAct_SetBool_Target_518;
	}

	private void Relay_In_519()
	{
		logic_uScriptCon_CompareBool_Bool_519 = local_PlayedTryAgainMsg_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_519.In(logic_uScriptCon_CompareBool_Bool_519);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_519.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_519.False;
		if (num)
		{
			Relay_In_149();
		}
		if (flag)
		{
			Relay_In_503();
		}
	}

	private void Relay_True_522()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_522.True(out logic_uScriptAct_SetBool_Target_522);
		local_PlayedTryAgainMsg_System_Boolean = logic_uScriptAct_SetBool_Target_522;
	}

	private void Relay_False_522()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_522.False(out logic_uScriptAct_SetBool_Target_522);
		local_PlayedTryAgainMsg_System_Boolean = logic_uScriptAct_SetBool_Target_522;
	}

	private void Relay_In_523()
	{
		logic_uScript_AddMessage_messageData_523 = MsgLeftAreaCompletely;
		logic_uScript_AddMessage_speaker_523 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_523 = logic_uScript_AddMessage_uScript_AddMessage_523.In(logic_uScript_AddMessage_messageData_523, logic_uScript_AddMessage_speaker_523);
		if (logic_uScript_AddMessage_uScript_AddMessage_523.Out)
		{
			Relay_True_534();
		}
	}

	private void Relay_In_524()
	{
		logic_uScriptCon_CompareBool_Bool_524 = local_PlayerLeftMissionArea_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_524.In(logic_uScriptCon_CompareBool_Bool_524);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_524.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_524.False;
		if (num)
		{
			Relay_In_528();
		}
		if (flag)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_528()
	{
		logic_uScriptCon_CompareBool_Bool_528 = local_HasPlayerLeftMissionArea_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_528.In(logic_uScriptCon_CompareBool_Bool_528);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_528.False)
		{
			Relay_In_523();
		}
	}

	private void Relay_True_531()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_531.True(out logic_uScriptAct_SetBool_Target_531);
		local_PlayerLeftMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_531;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_531.Out)
		{
			Relay_In_524();
		}
	}

	private void Relay_False_531()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_531.False(out logic_uScriptAct_SetBool_Target_531);
		local_PlayerLeftMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_531;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_531.Out)
		{
			Relay_In_524();
		}
	}

	private void Relay_In_532()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_532 = MissionArea;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_532.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_532);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_532.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_532.OutOfRange;
		if (inRange)
		{
			Relay_False_533();
		}
		if (outOfRange)
		{
			Relay_True_531();
		}
	}

	private void Relay_True_533()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_533.True(out logic_uScriptAct_SetBool_Target_533);
		local_PlayerLeftMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_533;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_533.Out)
		{
			Relay_False_536();
		}
	}

	private void Relay_False_533()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_533.False(out logic_uScriptAct_SetBool_Target_533);
		local_PlayerLeftMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_533;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_533.Out)
		{
			Relay_False_536();
		}
	}

	private void Relay_True_534()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_534.True(out logic_uScriptAct_SetBool_Target_534);
		local_HasPlayerLeftMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_534;
	}

	private void Relay_False_534()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_534.False(out logic_uScriptAct_SetBool_Target_534);
		local_HasPlayerLeftMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_534;
	}

	private void Relay_True_536()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_536.True(out logic_uScriptAct_SetBool_Target_536);
		local_HasPlayerLeftMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_536;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_536.Out)
		{
			Relay_In_524();
		}
	}

	private void Relay_False_536()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_536.False(out logic_uScriptAct_SetBool_Target_536);
		local_HasPlayerLeftMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_536;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_536.Out)
		{
			Relay_In_524();
		}
	}

	private void Relay_True_539()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_539.True(out logic_uScriptAct_SetBool_Target_539);
		local_FinalObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_539;
	}

	private void Relay_False_539()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_539.False(out logic_uScriptAct_SetBool_Target_539);
		local_FinalObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_539;
	}

	private void Relay_In_543()
	{
		int num = 0;
		Array crazedTechData = CrazedTechData;
		if (logic_uScript_GetAndCheckTechs_techData_543.Length != num + crazedTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_543, num + crazedTechData.Length);
		}
		Array.Copy(crazedTechData, 0, logic_uScript_GetAndCheckTechs_techData_543, num, crazedTechData.Length);
		num += crazedTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_543 = owner_Connection_541;
		int num2 = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_543.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_543, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_543, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_543 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_543.In(logic_uScript_GetAndCheckTechs_techData_543, logic_uScript_GetAndCheckTechs_ownerNode_543, ref logic_uScript_GetAndCheckTechs_techs_543);
		local_CrazedTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_543;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_543.AllDead)
		{
			Relay_True_539();
		}
	}

	private void Relay_In_547()
	{
		logic_uScriptCon_CompareBool_Bool_547 = local_CrazedAmbushTriggered_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_547.In(logic_uScriptCon_CompareBool_Bool_547);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_547.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_547.False;
		if (num)
		{
			Relay_In_456();
		}
		if (flag)
		{
			Relay_In_552();
		}
	}

	private void Relay_In_551()
	{
		logic_uScript_FlyTechUpAndAway_tech_551 = local_CrazedTech_Tank;
		logic_uScript_FlyTechUpAndAway_removalParticles_551 = TechFlyParticles;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_551.In(logic_uScript_FlyTechUpAndAway_tech_551, logic_uScript_FlyTechUpAndAway_maxLifetime_551, logic_uScript_FlyTechUpAndAway_targetHeight_551, logic_uScript_FlyTechUpAndAway_aiTree_551, logic_uScript_FlyTechUpAndAway_removalParticles_551);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_551.Out)
		{
			Relay_True_555();
		}
	}

	private void Relay_In_552()
	{
		logic_uScript_AddMessage_messageData_552 = MsgMinionsDead;
		logic_uScript_AddMessage_speaker_552 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_552 = logic_uScript_AddMessage_uScript_AddMessage_552.In(logic_uScript_AddMessage_messageData_552, logic_uScript_AddMessage_speaker_552);
		if (logic_uScript_AddMessage_uScript_AddMessage_552.Shown)
		{
			Relay_In_551();
		}
	}

	private void Relay_True_555()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_555.True(out logic_uScriptAct_SetBool_Target_555);
		local_CrazedAmbushTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_555;
	}

	private void Relay_False_555()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_555.False(out logic_uScriptAct_SetBool_Target_555);
		local_CrazedAmbushTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_555;
	}

	private void Relay_In_557()
	{
		logic_uScript_SetEncounterTarget_owner_557 = owner_Connection_558;
		logic_uScript_SetEncounterTarget_visibleObject_557 = local_CrazedTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_557.In(logic_uScript_SetEncounterTarget_owner_557, logic_uScript_SetEncounterTarget_visibleObject_557);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_557.Out)
		{
			Relay_False_92();
		}
	}

	private void Relay_In_560()
	{
		logic_uScript_SetEncounterTarget_owner_560 = owner_Connection_561;
		logic_uScript_SetEncounterTarget_visibleObject_560 = local_CubeTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_560.In(logic_uScript_SetEncounterTarget_owner_560, logic_uScript_SetEncounterTarget_visibleObject_560);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_560.Out)
		{
			Relay_True_518();
		}
	}

	private void Relay_Save_Out_572()
	{
		Relay_Save_573();
	}

	private void Relay_Load_Out_572()
	{
		Relay_Load_573();
	}

	private void Relay_Restart_Out_572()
	{
		Relay_Set_False_573();
	}

	private void Relay_Save_572()
	{
		logic_SubGraph_SaveLoadBool_boolean_572 = local_IntroSkipped_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_572 = local_IntroSkipped_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Save(ref logic_SubGraph_SaveLoadBool_boolean_572, logic_SubGraph_SaveLoadBool_boolAsVariable_572, logic_SubGraph_SaveLoadBool_uniqueID_572);
	}

	private void Relay_Load_572()
	{
		logic_SubGraph_SaveLoadBool_boolean_572 = local_IntroSkipped_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_572 = local_IntroSkipped_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Load(ref logic_SubGraph_SaveLoadBool_boolean_572, logic_SubGraph_SaveLoadBool_boolAsVariable_572, logic_SubGraph_SaveLoadBool_uniqueID_572);
	}

	private void Relay_Set_True_572()
	{
		logic_SubGraph_SaveLoadBool_boolean_572 = local_IntroSkipped_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_572 = local_IntroSkipped_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_572.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_572, logic_SubGraph_SaveLoadBool_boolAsVariable_572, logic_SubGraph_SaveLoadBool_uniqueID_572);
	}

	private void Relay_Set_False_572()
	{
		logic_SubGraph_SaveLoadBool_boolean_572 = local_IntroSkipped_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_572 = local_IntroSkipped_System_Boolean;
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
		logic_SubGraph_SaveLoadBool_boolean_573 = local_IntroTechInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_573 = local_IntroTechInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Save(ref logic_SubGraph_SaveLoadBool_boolean_573, logic_SubGraph_SaveLoadBool_boolAsVariable_573, logic_SubGraph_SaveLoadBool_uniqueID_573);
	}

	private void Relay_Load_573()
	{
		logic_SubGraph_SaveLoadBool_boolean_573 = local_IntroTechInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_573 = local_IntroTechInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Load(ref logic_SubGraph_SaveLoadBool_boolean_573, logic_SubGraph_SaveLoadBool_boolAsVariable_573, logic_SubGraph_SaveLoadBool_uniqueID_573);
	}

	private void Relay_Set_True_573()
	{
		logic_SubGraph_SaveLoadBool_boolean_573 = local_IntroTechInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_573 = local_IntroTechInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_573.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_573, logic_SubGraph_SaveLoadBool_boolAsVariable_573, logic_SubGraph_SaveLoadBool_uniqueID_573);
	}

	private void Relay_Set_False_573()
	{
		logic_SubGraph_SaveLoadBool_boolean_573 = local_IntroTechInRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_573 = local_IntroTechInRange_System_Boolean;
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
		logic_SubGraph_SaveLoadBool_boolean_574 = local_HasPlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_574 = local_HasPlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Save(ref logic_SubGraph_SaveLoadBool_boolean_574, logic_SubGraph_SaveLoadBool_boolAsVariable_574, logic_SubGraph_SaveLoadBool_uniqueID_574);
	}

	private void Relay_Load_574()
	{
		logic_SubGraph_SaveLoadBool_boolean_574 = local_HasPlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_574 = local_HasPlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Load(ref logic_SubGraph_SaveLoadBool_boolean_574, logic_SubGraph_SaveLoadBool_boolAsVariable_574, logic_SubGraph_SaveLoadBool_uniqueID_574);
	}

	private void Relay_Set_True_574()
	{
		logic_SubGraph_SaveLoadBool_boolean_574 = local_HasPlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_574 = local_HasPlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_574.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_574, logic_SubGraph_SaveLoadBool_boolAsVariable_574, logic_SubGraph_SaveLoadBool_uniqueID_574);
	}

	private void Relay_Set_False_574()
	{
		logic_SubGraph_SaveLoadBool_boolean_574 = local_HasPlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_574 = local_HasPlayerLeftMissionArea_System_Boolean;
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
		logic_SubGraph_SaveLoadBool_boolean_575 = local_PlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_575 = local_PlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Save(ref logic_SubGraph_SaveLoadBool_boolean_575, logic_SubGraph_SaveLoadBool_boolAsVariable_575, logic_SubGraph_SaveLoadBool_uniqueID_575);
	}

	private void Relay_Load_575()
	{
		logic_SubGraph_SaveLoadBool_boolean_575 = local_PlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_575 = local_PlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Load(ref logic_SubGraph_SaveLoadBool_boolean_575, logic_SubGraph_SaveLoadBool_boolAsVariable_575, logic_SubGraph_SaveLoadBool_uniqueID_575);
	}

	private void Relay_Set_True_575()
	{
		logic_SubGraph_SaveLoadBool_boolean_575 = local_PlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_575 = local_PlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_575.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_575, logic_SubGraph_SaveLoadBool_boolAsVariable_575, logic_SubGraph_SaveLoadBool_uniqueID_575);
	}

	private void Relay_Set_False_575()
	{
		logic_SubGraph_SaveLoadBool_boolean_575 = local_PlayerLeftMissionArea_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_575 = local_PlayerLeftMissionArea_System_Boolean;
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
		logic_SubGraph_SaveLoadBool_boolean_576 = local_msgTooEarlyPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_576 = local_msgTooEarlyPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Save(ref logic_SubGraph_SaveLoadBool_boolean_576, logic_SubGraph_SaveLoadBool_boolAsVariable_576, logic_SubGraph_SaveLoadBool_uniqueID_576);
	}

	private void Relay_Load_576()
	{
		logic_SubGraph_SaveLoadBool_boolean_576 = local_msgTooEarlyPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_576 = local_msgTooEarlyPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Load(ref logic_SubGraph_SaveLoadBool_boolean_576, logic_SubGraph_SaveLoadBool_boolAsVariable_576, logic_SubGraph_SaveLoadBool_uniqueID_576);
	}

	private void Relay_Set_True_576()
	{
		logic_SubGraph_SaveLoadBool_boolean_576 = local_msgTooEarlyPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_576 = local_msgTooEarlyPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_576, logic_SubGraph_SaveLoadBool_boolAsVariable_576, logic_SubGraph_SaveLoadBool_uniqueID_576);
	}

	private void Relay_Set_False_576()
	{
		logic_SubGraph_SaveLoadBool_boolean_576 = local_msgTooEarlyPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_576 = local_msgTooEarlyPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_576.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_576, logic_SubGraph_SaveLoadBool_boolAsVariable_576, logic_SubGraph_SaveLoadBool_uniqueID_576);
	}

	private void Relay_Save_Out_577()
	{
		Relay_Save_578();
	}

	private void Relay_Load_Out_577()
	{
		Relay_Load_578();
	}

	private void Relay_Restart_Out_577()
	{
		Relay_Set_False_578();
	}

	private void Relay_Save_577()
	{
		logic_SubGraph_SaveLoadBool_boolean_577 = local_CrazedAmbushMsgTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_577 = local_CrazedAmbushMsgTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Save(ref logic_SubGraph_SaveLoadBool_boolean_577, logic_SubGraph_SaveLoadBool_boolAsVariable_577, logic_SubGraph_SaveLoadBool_uniqueID_577);
	}

	private void Relay_Load_577()
	{
		logic_SubGraph_SaveLoadBool_boolean_577 = local_CrazedAmbushMsgTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_577 = local_CrazedAmbushMsgTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Load(ref logic_SubGraph_SaveLoadBool_boolean_577, logic_SubGraph_SaveLoadBool_boolAsVariable_577, logic_SubGraph_SaveLoadBool_uniqueID_577);
	}

	private void Relay_Set_True_577()
	{
		logic_SubGraph_SaveLoadBool_boolean_577 = local_CrazedAmbushMsgTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_577 = local_CrazedAmbushMsgTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_577, logic_SubGraph_SaveLoadBool_boolAsVariable_577, logic_SubGraph_SaveLoadBool_uniqueID_577);
	}

	private void Relay_Set_False_577()
	{
		logic_SubGraph_SaveLoadBool_boolean_577 = local_CrazedAmbushMsgTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_577 = local_CrazedAmbushMsgTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_577.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_577, logic_SubGraph_SaveLoadBool_boolAsVariable_577, logic_SubGraph_SaveLoadBool_uniqueID_577);
	}

	private void Relay_Save_Out_578()
	{
		Relay_Save_579();
	}

	private void Relay_Load_Out_578()
	{
		Relay_Load_579();
	}

	private void Relay_Restart_Out_578()
	{
		Relay_Set_False_579();
	}

	private void Relay_Save_578()
	{
		logic_SubGraph_SaveLoadBool_boolean_578 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_578 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.Save(ref logic_SubGraph_SaveLoadBool_boolean_578, logic_SubGraph_SaveLoadBool_boolAsVariable_578, logic_SubGraph_SaveLoadBool_uniqueID_578);
	}

	private void Relay_Load_578()
	{
		logic_SubGraph_SaveLoadBool_boolean_578 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_578 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.Load(ref logic_SubGraph_SaveLoadBool_boolean_578, logic_SubGraph_SaveLoadBool_boolAsVariable_578, logic_SubGraph_SaveLoadBool_uniqueID_578);
	}

	private void Relay_Set_True_578()
	{
		logic_SubGraph_SaveLoadBool_boolean_578 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_578 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_578, logic_SubGraph_SaveLoadBool_boolAsVariable_578, logic_SubGraph_SaveLoadBool_uniqueID_578);
	}

	private void Relay_Set_False_578()
	{
		logic_SubGraph_SaveLoadBool_boolean_578 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_578 = local_CubeDestroyedMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_578.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_578, logic_SubGraph_SaveLoadBool_boolAsVariable_578, logic_SubGraph_SaveLoadBool_uniqueID_578);
	}

	private void Relay_Save_Out_579()
	{
		Relay_Save_580();
	}

	private void Relay_Load_Out_579()
	{
		Relay_Load_580();
	}

	private void Relay_Restart_Out_579()
	{
		Relay_Set_False_580();
	}

	private void Relay_Save_579()
	{
		logic_SubGraph_SaveLoadBool_boolean_579 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_579 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.Save(ref logic_SubGraph_SaveLoadBool_boolean_579, logic_SubGraph_SaveLoadBool_boolAsVariable_579, logic_SubGraph_SaveLoadBool_uniqueID_579);
	}

	private void Relay_Load_579()
	{
		logic_SubGraph_SaveLoadBool_boolean_579 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_579 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.Load(ref logic_SubGraph_SaveLoadBool_boolean_579, logic_SubGraph_SaveLoadBool_boolAsVariable_579, logic_SubGraph_SaveLoadBool_uniqueID_579);
	}

	private void Relay_Set_True_579()
	{
		logic_SubGraph_SaveLoadBool_boolean_579 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_579 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_579, logic_SubGraph_SaveLoadBool_boolAsVariable_579, logic_SubGraph_SaveLoadBool_uniqueID_579);
	}

	private void Relay_Set_False_579()
	{
		logic_SubGraph_SaveLoadBool_boolean_579 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_579 = local_CrazedAmbushTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_579.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_579, logic_SubGraph_SaveLoadBool_boolAsVariable_579, logic_SubGraph_SaveLoadBool_uniqueID_579);
	}

	private void Relay_Save_Out_580()
	{
		Relay_Save_581();
	}

	private void Relay_Load_Out_580()
	{
		Relay_Load_581();
	}

	private void Relay_Restart_Out_580()
	{
		Relay_Set_False_581();
	}

	private void Relay_Save_580()
	{
		logic_SubGraph_SaveLoadBool_boolean_580 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_580 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.Save(ref logic_SubGraph_SaveLoadBool_boolean_580, logic_SubGraph_SaveLoadBool_boolAsVariable_580, logic_SubGraph_SaveLoadBool_uniqueID_580);
	}

	private void Relay_Load_580()
	{
		logic_SubGraph_SaveLoadBool_boolean_580 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_580 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.Load(ref logic_SubGraph_SaveLoadBool_boolean_580, logic_SubGraph_SaveLoadBool_boolAsVariable_580, logic_SubGraph_SaveLoadBool_uniqueID_580);
	}

	private void Relay_Set_True_580()
	{
		logic_SubGraph_SaveLoadBool_boolean_580 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_580 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_580, logic_SubGraph_SaveLoadBool_boolAsVariable_580, logic_SubGraph_SaveLoadBool_uniqueID_580);
	}

	private void Relay_Set_False_580()
	{
		logic_SubGraph_SaveLoadBool_boolean_580 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_580 = local_MsgCubeIntroPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_580.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_580, logic_SubGraph_SaveLoadBool_boolAsVariable_580, logic_SubGraph_SaveLoadBool_uniqueID_580);
	}

	private void Relay_Save_Out_581()
	{
		Relay_Save_664();
	}

	private void Relay_Load_Out_581()
	{
		Relay_Load_664();
	}

	private void Relay_Restart_Out_581()
	{
		Relay_Set_False_664();
	}

	private void Relay_Save_581()
	{
		logic_SubGraph_SaveLoadBool_boolean_581 = local_PlayedTryAgainMsg_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_581 = local_PlayedTryAgainMsg_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.Save(ref logic_SubGraph_SaveLoadBool_boolean_581, logic_SubGraph_SaveLoadBool_boolAsVariable_581, logic_SubGraph_SaveLoadBool_uniqueID_581);
	}

	private void Relay_Load_581()
	{
		logic_SubGraph_SaveLoadBool_boolean_581 = local_PlayedTryAgainMsg_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_581 = local_PlayedTryAgainMsg_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.Load(ref logic_SubGraph_SaveLoadBool_boolean_581, logic_SubGraph_SaveLoadBool_boolAsVariable_581, logic_SubGraph_SaveLoadBool_uniqueID_581);
	}

	private void Relay_Set_True_581()
	{
		logic_SubGraph_SaveLoadBool_boolean_581 = local_PlayedTryAgainMsg_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_581 = local_PlayedTryAgainMsg_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_581, logic_SubGraph_SaveLoadBool_boolAsVariable_581, logic_SubGraph_SaveLoadBool_uniqueID_581);
	}

	private void Relay_Set_False_581()
	{
		logic_SubGraph_SaveLoadBool_boolean_581 = local_PlayedTryAgainMsg_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_581 = local_PlayedTryAgainMsg_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_581.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_581, logic_SubGraph_SaveLoadBool_boolAsVariable_581, logic_SubGraph_SaveLoadBool_uniqueID_581);
	}

	private void Relay_In_585()
	{
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_585 = local_CubeTech_Tank;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_585.In(logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_585, logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_585);
		if (logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_585.Out)
		{
			Relay_True_40();
		}
	}

	private void Relay_In_586()
	{
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_586 = local_CubeTech_Tank;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_586.In(logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_586, logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_586);
		if (logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_586.Out)
		{
			Relay_False_148();
		}
	}

	private void Relay_In_587()
	{
		logic_uScript_SetBatteryChargeAmount_tech_587 = local_CubeTech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_587.In(logic_uScript_SetBatteryChargeAmount_tech_587, logic_uScript_SetBatteryChargeAmount_chargeAmount_587);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_587.Out)
		{
			Relay_In_585();
		}
	}

	private void Relay_In_588()
	{
		logic_uScript_SetBatteryChargeAmount_tech_588 = local_CubeTech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_588.In(logic_uScript_SetBatteryChargeAmount_tech_588, logic_uScript_SetBatteryChargeAmount_chargeAmount_588);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_588.Out)
		{
			Relay_In_586();
		}
	}

	private void Relay_Save_Out_589()
	{
	}

	private void Relay_Load_Out_589()
	{
		Relay_In_721();
	}

	private void Relay_Restart_Out_589()
	{
		Relay_False_694();
	}

	private void Relay_Save_589()
	{
		logic_SubGraph_SaveLoadBool_boolean_589 = local_FightRunning_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_589 = local_FightRunning_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.Save(ref logic_SubGraph_SaveLoadBool_boolean_589, logic_SubGraph_SaveLoadBool_boolAsVariable_589, logic_SubGraph_SaveLoadBool_uniqueID_589);
	}

	private void Relay_Load_589()
	{
		logic_SubGraph_SaveLoadBool_boolean_589 = local_FightRunning_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_589 = local_FightRunning_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.Load(ref logic_SubGraph_SaveLoadBool_boolean_589, logic_SubGraph_SaveLoadBool_boolAsVariable_589, logic_SubGraph_SaveLoadBool_uniqueID_589);
	}

	private void Relay_Set_True_589()
	{
		logic_SubGraph_SaveLoadBool_boolean_589 = local_FightRunning_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_589 = local_FightRunning_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_589, logic_SubGraph_SaveLoadBool_boolAsVariable_589, logic_SubGraph_SaveLoadBool_uniqueID_589);
	}

	private void Relay_Set_False_589()
	{
		logic_SubGraph_SaveLoadBool_boolean_589 = local_FightRunning_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_589 = local_FightRunning_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_589.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_589, logic_SubGraph_SaveLoadBool_boolAsVariable_589, logic_SubGraph_SaveLoadBool_uniqueID_589);
	}

	private void Relay_In_590()
	{
		logic_uScript_HideMissionTimerUI_owner_590 = owner_Connection_810;
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_590.In(logic_uScript_HideMissionTimerUI_owner_590);
		if (logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_590.Out)
		{
			Relay_In_709();
		}
	}

	private void Relay_In_591()
	{
		logic_uScript_StopMissionTimer_owner_591 = owner_Connection_595;
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_591.In(logic_uScript_StopMissionTimer_owner_591);
		if (logic_uScript_StopMissionTimer_uScript_StopMissionTimer_591.Out)
		{
			Relay_In_687();
		}
	}

	private void Relay_Save_Out_593()
	{
		Relay_Save_589();
	}

	private void Relay_Load_Out_593()
	{
		Relay_Load_589();
	}

	private void Relay_Restart_Out_593()
	{
		Relay_Set_False_589();
	}

	private void Relay_Save_593()
	{
		logic_SubGraph_SaveLoadBool_boolean_593 = local_FightStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_593 = local_FightStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Save(ref logic_SubGraph_SaveLoadBool_boolean_593, logic_SubGraph_SaveLoadBool_boolAsVariable_593, logic_SubGraph_SaveLoadBool_uniqueID_593);
	}

	private void Relay_Load_593()
	{
		logic_SubGraph_SaveLoadBool_boolean_593 = local_FightStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_593 = local_FightStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Load(ref logic_SubGraph_SaveLoadBool_boolean_593, logic_SubGraph_SaveLoadBool_boolAsVariable_593, logic_SubGraph_SaveLoadBool_uniqueID_593);
	}

	private void Relay_Set_True_593()
	{
		logic_SubGraph_SaveLoadBool_boolean_593 = local_FightStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_593 = local_FightStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_593, logic_SubGraph_SaveLoadBool_boolAsVariable_593, logic_SubGraph_SaveLoadBool_uniqueID_593);
	}

	private void Relay_Set_False_593()
	{
		logic_SubGraph_SaveLoadBool_boolean_593 = local_FightStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_593 = local_FightStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_593.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_593, logic_SubGraph_SaveLoadBool_boolAsVariable_593, logic_SubGraph_SaveLoadBool_uniqueID_593);
	}

	private void Relay_In_597()
	{
		int num = 0;
		Array nPCTechData = NPCTechData;
		if (logic_uScript_GetAndCheckTechs_techData_597.Length != num + nPCTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_597, num + nPCTechData.Length);
		}
		Array.Copy(nPCTechData, 0, logic_uScript_GetAndCheckTechs_techData_597, num, nPCTechData.Length);
		num += nPCTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_597 = owner_Connection_598;
		int num2 = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_597.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_597, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_597, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_597 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_597.In(logic_uScript_GetAndCheckTechs_techData_597, logic_uScript_GetAndCheckTechs_ownerNode_597, ref logic_uScript_GetAndCheckTechs_techs_597);
		local_NPCTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_597;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_597.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_597.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_597.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_597.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_800();
		}
		if (someAlive)
		{
			Relay_True_800();
		}
		if (allDead)
		{
			Relay_False_800();
		}
		if (waitingToSpawn)
		{
			Relay_False_800();
		}
	}

	private void Relay_In_601()
	{
		int num = 0;
		Array crazedLeaderTechData = CrazedLeaderTechData;
		if (logic_uScript_GetAndCheckTechs_techData_601.Length != num + crazedLeaderTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_601, num + crazedLeaderTechData.Length);
		}
		Array.Copy(crazedLeaderTechData, 0, logic_uScript_GetAndCheckTechs_techData_601, num, crazedLeaderTechData.Length);
		num += crazedLeaderTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_601 = owner_Connection_600;
		int num2 = 0;
		Array array = local_CrazedLeaderTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_601.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_601, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_601, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_601 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_601.In(logic_uScript_GetAndCheckTechs_techData_601, logic_uScript_GetAndCheckTechs_ownerNode_601, ref logic_uScript_GetAndCheckTechs_techs_601);
		local_CrazedLeaderTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_601;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_601.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_601.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_601.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_601.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_802();
		}
		if (someAlive)
		{
			Relay_True_802();
		}
		if (allDead)
		{
			Relay_False_802();
		}
		if (waitingToSpawn)
		{
			Relay_False_802();
		}
	}

	private void Relay_In_603()
	{
		int num = 0;
		Array crazedTechData = CrazedTechData;
		if (logic_uScript_GetAndCheckTechs_techData_603.Length != num + crazedTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_603, num + crazedTechData.Length);
		}
		Array.Copy(crazedTechData, 0, logic_uScript_GetAndCheckTechs_techData_603, num, crazedTechData.Length);
		num += crazedTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_603 = owner_Connection_604;
		int num2 = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_603.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_603, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_603, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_603 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_603.In(logic_uScript_GetAndCheckTechs_techData_603, logic_uScript_GetAndCheckTechs_ownerNode_603, ref logic_uScript_GetAndCheckTechs_techs_603);
		local_CrazedTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_603;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_603.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_603.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_603.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_603.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_804();
		}
		if (someAlive)
		{
			Relay_True_804();
		}
		if (allDead)
		{
			Relay_False_804();
		}
		if (waitingToSpawn)
		{
			Relay_False_804();
		}
	}

	private void Relay_In_605()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_605.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_605, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_605, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_605 = owner_Connection_607;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_605.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_605, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_605, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_605 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_605.In(logic_uScript_GetAndCheckTechs_techData_605, logic_uScript_GetAndCheckTechs_ownerNode_605, ref logic_uScript_GetAndCheckTechs_techs_605);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_605;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_605.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_605.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_605.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_605.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_634();
		}
		if (someAlive)
		{
			Relay_True_634();
		}
		if (allDead)
		{
			Relay_False_634();
		}
		if (waitingToSpawn)
		{
			Relay_False_634();
		}
	}

	private void Relay_In_609()
	{
		logic_uScriptCon_CompareBool_Bool_609 = local_GetRidOfCube_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_609.In(logic_uScriptCon_CompareBool_Bool_609);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_609.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_609.False;
		if (num)
		{
			Relay_In_654();
		}
		if (flag)
		{
			Relay_In_786();
		}
	}

	private void Relay_In_610()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_610.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_610, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_610, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_610 = owner_Connection_611;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_610.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_610, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_610, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_610 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_610.In(logic_uScript_GetAndCheckTechs_techData_610, logic_uScript_GetAndCheckTechs_ownerNode_610, ref logic_uScript_GetAndCheckTechs_techs_610);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_610;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_610.AllDead)
		{
			Relay_InitialSpawn_126();
		}
	}

	private void Relay_True_613()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_613.True(out logic_uScriptAct_SetBool_Target_613);
		local_GetRidOfCube_System_Boolean = logic_uScriptAct_SetBool_Target_613;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_613.Out)
		{
			Relay_True_657();
		}
	}

	private void Relay_False_613()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_613.False(out logic_uScriptAct_SetBool_Target_613);
		local_GetRidOfCube_System_Boolean = logic_uScriptAct_SetBool_Target_613;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_613.Out)
		{
			Relay_True_657();
		}
	}

	private void Relay_Save_Out_616()
	{
		Relay_Save_285();
	}

	private void Relay_Load_Out_616()
	{
		Relay_Load_285();
	}

	private void Relay_Restart_Out_616()
	{
		Relay_Set_False_285();
	}

	private void Relay_Save_616()
	{
		logic_SubGraph_SaveLoadBool_boolean_616 = local_GetRidOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_616 = local_GetRidOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Save(ref logic_SubGraph_SaveLoadBool_boolean_616, logic_SubGraph_SaveLoadBool_boolAsVariable_616, logic_SubGraph_SaveLoadBool_uniqueID_616);
	}

	private void Relay_Load_616()
	{
		logic_SubGraph_SaveLoadBool_boolean_616 = local_GetRidOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_616 = local_GetRidOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Load(ref logic_SubGraph_SaveLoadBool_boolean_616, logic_SubGraph_SaveLoadBool_boolAsVariable_616, logic_SubGraph_SaveLoadBool_uniqueID_616);
	}

	private void Relay_Set_True_616()
	{
		logic_SubGraph_SaveLoadBool_boolean_616 = local_GetRidOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_616 = local_GetRidOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_616, logic_SubGraph_SaveLoadBool_boolAsVariable_616, logic_SubGraph_SaveLoadBool_uniqueID_616);
	}

	private void Relay_Set_False_616()
	{
		logic_SubGraph_SaveLoadBool_boolean_616 = local_GetRidOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_616 = local_GetRidOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_616.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_616, logic_SubGraph_SaveLoadBool_boolAsVariable_616, logic_SubGraph_SaveLoadBool_uniqueID_616);
	}

	private void Relay_True_618()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_618.True(out logic_uScriptAct_SetBool_Target_618);
		local_GetRidOfCube_System_Boolean = logic_uScriptAct_SetBool_Target_618;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_618.Out)
		{
			Relay_False_625();
		}
	}

	private void Relay_False_618()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_618.False(out logic_uScriptAct_SetBool_Target_618);
		local_GetRidOfCube_System_Boolean = logic_uScriptAct_SetBool_Target_618;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_618.Out)
		{
			Relay_False_625();
		}
	}

	private void Relay_In_620()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_620.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_620, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_620, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_620 = owner_Connection_619;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_620.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_620, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_620, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_620 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_620.In(logic_uScript_GetAndCheckTechs_techData_620, logic_uScript_GetAndCheckTechs_ownerNode_620, ref logic_uScript_GetAndCheckTechs_techs_620);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_620;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_620.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_620.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_621();
		}
		if (someAlive)
		{
			Relay_AtIndex_621();
		}
	}

	private void Relay_AtIndex_621()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_621.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_621, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_621, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_621.AtIndex(ref logic_uScript_AccessListTech_techList_621, logic_uScript_AccessListTech_index_621, out logic_uScript_AccessListTech_value_621);
		local_CubeTechs_TankArray = logic_uScript_AccessListTech_techList_621;
		local_CubeTech_Tank = logic_uScript_AccessListTech_value_621;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_621.Out)
		{
			Relay_In_588();
		}
	}

	private void Relay_True_625()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_625.True(out logic_uScriptAct_SetBool_Target_625);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_625;
	}

	private void Relay_False_625()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_625.False(out logic_uScriptAct_SetBool_Target_625);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_625;
	}

	private void Relay_Save_Out_627()
	{
		Relay_Save_287();
	}

	private void Relay_Load_Out_627()
	{
		Relay_Load_287();
	}

	private void Relay_Restart_Out_627()
	{
		Relay_Set_False_287();
	}

	private void Relay_Save_627()
	{
		logic_SubGraph_SaveLoadBool_boolean_627 = local_CubeisOK_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_627 = local_CubeisOK_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.Save(ref logic_SubGraph_SaveLoadBool_boolean_627, logic_SubGraph_SaveLoadBool_boolAsVariable_627, logic_SubGraph_SaveLoadBool_uniqueID_627);
	}

	private void Relay_Load_627()
	{
		logic_SubGraph_SaveLoadBool_boolean_627 = local_CubeisOK_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_627 = local_CubeisOK_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.Load(ref logic_SubGraph_SaveLoadBool_boolean_627, logic_SubGraph_SaveLoadBool_boolAsVariable_627, logic_SubGraph_SaveLoadBool_uniqueID_627);
	}

	private void Relay_Set_True_627()
	{
		logic_SubGraph_SaveLoadBool_boolean_627 = local_CubeisOK_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_627 = local_CubeisOK_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_627, logic_SubGraph_SaveLoadBool_boolAsVariable_627, logic_SubGraph_SaveLoadBool_uniqueID_627);
	}

	private void Relay_Set_False_627()
	{
		logic_SubGraph_SaveLoadBool_boolean_627 = local_CubeisOK_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_627 = local_CubeisOK_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_627.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_627, logic_SubGraph_SaveLoadBool_boolAsVariable_627, logic_SubGraph_SaveLoadBool_uniqueID_627);
	}

	private void Relay_True_634()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_634.True(out logic_uScriptAct_SetBool_Target_634);
		local_CubeAlive_System_Boolean = logic_uScriptAct_SetBool_Target_634;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_634.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_634.SetTrue;
		if (num)
		{
			Relay_In_799();
		}
		if (setTrue)
		{
			Relay_AtIndex_648();
		}
	}

	private void Relay_False_634()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_634.False(out logic_uScriptAct_SetBool_Target_634);
		local_CubeAlive_System_Boolean = logic_uScriptAct_SetBool_Target_634;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_634.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_634.SetTrue;
		if (num)
		{
			Relay_In_799();
		}
		if (setTrue)
		{
			Relay_AtIndex_648();
		}
	}

	private void Relay_In_635()
	{
		logic_uScript_SetTankInvulnerable_tank_635 = local_CubeTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_635.In(logic_uScript_SetTankInvulnerable_invulnerable_635, logic_uScript_SetTankInvulnerable_tank_635);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_635.Out)
		{
			Relay_In_475();
		}
	}

	private void Relay_AtIndex_636()
	{
		int num = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_636.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_636, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_636, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_636.AtIndex(ref logic_uScript_AccessListTech_techList_636, logic_uScript_AccessListTech_index_636, out logic_uScript_AccessListTech_value_636);
		local_NPCTechs_TankArray = logic_uScript_AccessListTech_techList_636;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_636;
	}

	private void Relay_AtIndex_641()
	{
		int num = 0;
		Array array = local_CrazedLeaderTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_641.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_641, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_641, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_641.AtIndex(ref logic_uScript_AccessListTech_techList_641, logic_uScript_AccessListTech_index_641, out logic_uScript_AccessListTech_value_641);
		local_CrazedLeaderTechs_TankArray = logic_uScript_AccessListTech_techList_641;
		local_CrazedTech_Tank = logic_uScript_AccessListTech_value_641;
	}

	private void Relay_AtIndex_642()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_642.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_642, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_642, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_642.AtIndex(ref logic_uScript_AccessListTech_techList_642, logic_uScript_AccessListTech_index_642, out logic_uScript_AccessListTech_value_642);
		local_CrazedTechs_TankArray = logic_uScript_AccessListTech_techList_642;
		local_CrazedTech2_Tank = logic_uScript_AccessListTech_value_642;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_642.Out)
		{
			Relay_AtIndex_645();
		}
	}

	private void Relay_AtIndex_645()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_645.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_645, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_645, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_645.AtIndex(ref logic_uScript_AccessListTech_techList_645, logic_uScript_AccessListTech_index_645, out logic_uScript_AccessListTech_value_645);
		local_CrazedTechs_TankArray = logic_uScript_AccessListTech_techList_645;
		local_CrazedTech3_Tank = logic_uScript_AccessListTech_value_645;
	}

	private void Relay_AtIndex_648()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_648.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_648, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_648, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_648.AtIndex(ref logic_uScript_AccessListTech_techList_648, logic_uScript_AccessListTech_index_648, out logic_uScript_AccessListTech_value_648);
		local_CubeTechs_TankArray = logic_uScript_AccessListTech_techList_648;
		local_CubeTech_Tank = logic_uScript_AccessListTech_value_648;
	}

	private void Relay_True_651()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_651.True(out logic_uScriptAct_SetBool_Target_651);
		local_WaitingForTechClear_System_Boolean = logic_uScriptAct_SetBool_Target_651;
	}

	private void Relay_False_651()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_651.False(out logic_uScriptAct_SetBool_Target_651);
		local_WaitingForTechClear_System_Boolean = logic_uScriptAct_SetBool_Target_651;
	}

	private void Relay_In_653()
	{
		logic_uScript_Wait_uScript_Wait_653.In(logic_uScript_Wait_seconds_653, logic_uScript_Wait_repeat_653);
		if (logic_uScript_Wait_uScript_Wait_653.Waited)
		{
			Relay_In_610();
		}
	}

	private void Relay_In_654()
	{
		logic_uScriptCon_CompareBool_Bool_654 = local_WaitingForTechClear_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_654.In(logic_uScriptCon_CompareBool_Bool_654);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_654.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_654.False;
		if (num)
		{
			Relay_In_653();
		}
		if (flag)
		{
			Relay_In_620();
		}
	}

	private void Relay_True_657()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_657.True(out logic_uScriptAct_SetBool_Target_657);
		local_WaitingForTechClear_System_Boolean = logic_uScriptAct_SetBool_Target_657;
	}

	private void Relay_False_657()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_657.False(out logic_uScriptAct_SetBool_Target_657);
		local_WaitingForTechClear_System_Boolean = logic_uScriptAct_SetBool_Target_657;
	}

	private void Relay_In_658()
	{
		logic_uScript_SetTankInvulnerable_tank_658 = local_CubeTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_658.In(logic_uScript_SetTankInvulnerable_invulnerable_658, logic_uScript_SetTankInvulnerable_tank_658);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_658.Out)
		{
			Relay_In_783();
		}
	}

	private void Relay_True_661()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_661.True(out logic_uScriptAct_SetBool_Target_661);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_661;
	}

	private void Relay_False_661()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_661.False(out logic_uScriptAct_SetBool_Target_661);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_661;
	}

	private void Relay_Save_Out_664()
	{
		Relay_Save_665();
	}

	private void Relay_Load_Out_664()
	{
		Relay_Load_665();
	}

	private void Relay_Restart_Out_664()
	{
		Relay_Set_False_665();
	}

	private void Relay_Save_664()
	{
		logic_SubGraph_SaveLoadBool_boolean_664 = local_GetRidOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_664 = local_GetRidOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Save(ref logic_SubGraph_SaveLoadBool_boolean_664, logic_SubGraph_SaveLoadBool_boolAsVariable_664, logic_SubGraph_SaveLoadBool_uniqueID_664);
	}

	private void Relay_Load_664()
	{
		logic_SubGraph_SaveLoadBool_boolean_664 = local_GetRidOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_664 = local_GetRidOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Load(ref logic_SubGraph_SaveLoadBool_boolean_664, logic_SubGraph_SaveLoadBool_boolAsVariable_664, logic_SubGraph_SaveLoadBool_uniqueID_664);
	}

	private void Relay_Set_True_664()
	{
		logic_SubGraph_SaveLoadBool_boolean_664 = local_GetRidOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_664 = local_GetRidOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_664, logic_SubGraph_SaveLoadBool_boolAsVariable_664, logic_SubGraph_SaveLoadBool_uniqueID_664);
	}

	private void Relay_Set_False_664()
	{
		logic_SubGraph_SaveLoadBool_boolean_664 = local_GetRidOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_664 = local_GetRidOfCube_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_664.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_664, logic_SubGraph_SaveLoadBool_boolAsVariable_664, logic_SubGraph_SaveLoadBool_uniqueID_664);
	}

	private void Relay_Save_Out_665()
	{
		Relay_Save_303();
	}

	private void Relay_Load_Out_665()
	{
		Relay_Load_303();
	}

	private void Relay_Restart_Out_665()
	{
		Relay_Set_False_303();
	}

	private void Relay_Save_665()
	{
		logic_SubGraph_SaveLoadBool_boolean_665 = local_WaitingForTechClear_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_665 = local_WaitingForTechClear_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Save(ref logic_SubGraph_SaveLoadBool_boolean_665, logic_SubGraph_SaveLoadBool_boolAsVariable_665, logic_SubGraph_SaveLoadBool_uniqueID_665);
	}

	private void Relay_Load_665()
	{
		logic_SubGraph_SaveLoadBool_boolean_665 = local_WaitingForTechClear_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_665 = local_WaitingForTechClear_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Load(ref logic_SubGraph_SaveLoadBool_boolean_665, logic_SubGraph_SaveLoadBool_boolAsVariable_665, logic_SubGraph_SaveLoadBool_uniqueID_665);
	}

	private void Relay_Set_True_665()
	{
		logic_SubGraph_SaveLoadBool_boolean_665 = local_WaitingForTechClear_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_665 = local_WaitingForTechClear_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_665, logic_SubGraph_SaveLoadBool_boolAsVariable_665, logic_SubGraph_SaveLoadBool_uniqueID_665);
	}

	private void Relay_Set_False_665()
	{
		logic_SubGraph_SaveLoadBool_boolean_665 = local_WaitingForTechClear_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_665 = local_WaitingForTechClear_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_665.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_665, logic_SubGraph_SaveLoadBool_boolAsVariable_665, logic_SubGraph_SaveLoadBool_uniqueID_665);
	}

	private void Relay_In_669()
	{
		logic_uScript_SetTankInvulnerable_tank_669 = local_CubeTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_669.In(logic_uScript_SetTankInvulnerable_invulnerable_669, logic_uScript_SetTankInvulnerable_tank_669);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_669.Out)
		{
			Relay_In_670();
		}
	}

	private void Relay_In_670()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_670.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_670, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_670, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_670.In(ref logic_uScript_SetTechsTeam_techs_670, logic_uScript_SetTechsTeam_team_670);
		local_CubeTechs_TankArray = logic_uScript_SetTechsTeam_techs_670;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_670.Out)
		{
			Relay_True_675();
		}
	}

	private void Relay_In_674()
	{
		logic_uScriptCon_CompareBool_Bool_674 = local_TechInvulOnLoad_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_674.In(logic_uScriptCon_CompareBool_Bool_674);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_674.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_674.False;
		if (num)
		{
			Relay_In_47();
		}
		if (flag)
		{
			Relay_In_684();
		}
	}

	private void Relay_True_675()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_675.True(out logic_uScriptAct_SetBool_Target_675);
		local_TechInvulOnLoad_System_Boolean = logic_uScriptAct_SetBool_Target_675;
	}

	private void Relay_False_675()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_675.False(out logic_uScriptAct_SetBool_Target_675);
		local_TechInvulOnLoad_System_Boolean = logic_uScriptAct_SetBool_Target_675;
	}

	private void Relay_Save_Out_678()
	{
		Relay_Save_616();
	}

	private void Relay_Load_Out_678()
	{
		Relay_Load_616();
	}

	private void Relay_Restart_Out_678()
	{
		Relay_Set_False_616();
	}

	private void Relay_Save_678()
	{
		logic_SubGraph_SaveLoadBool_boolean_678 = local_TechInvulOnLoad_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_678 = local_TechInvulOnLoad_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.Save(ref logic_SubGraph_SaveLoadBool_boolean_678, logic_SubGraph_SaveLoadBool_boolAsVariable_678, logic_SubGraph_SaveLoadBool_uniqueID_678);
	}

	private void Relay_Load_678()
	{
		logic_SubGraph_SaveLoadBool_boolean_678 = local_TechInvulOnLoad_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_678 = local_TechInvulOnLoad_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.Load(ref logic_SubGraph_SaveLoadBool_boolean_678, logic_SubGraph_SaveLoadBool_boolAsVariable_678, logic_SubGraph_SaveLoadBool_uniqueID_678);
	}

	private void Relay_Set_True_678()
	{
		logic_SubGraph_SaveLoadBool_boolean_678 = local_TechInvulOnLoad_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_678 = local_TechInvulOnLoad_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_678, logic_SubGraph_SaveLoadBool_boolAsVariable_678, logic_SubGraph_SaveLoadBool_uniqueID_678);
	}

	private void Relay_Set_False_678()
	{
		logic_SubGraph_SaveLoadBool_boolean_678 = local_TechInvulOnLoad_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_678 = local_TechInvulOnLoad_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_678.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_678, logic_SubGraph_SaveLoadBool_boolAsVariable_678, logic_SubGraph_SaveLoadBool_uniqueID_678);
	}

	private void Relay_AtIndex_683()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_683.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_683, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_683, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_683.AtIndex(ref logic_uScript_AccessListTech_techList_683, logic_uScript_AccessListTech_index_683, out logic_uScript_AccessListTech_value_683);
		local_CubeTechs_TankArray = logic_uScript_AccessListTech_techList_683;
		local_CubeTech_Tank = logic_uScript_AccessListTech_value_683;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_683.Out)
		{
			Relay_In_669();
		}
	}

	private void Relay_In_684()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_684.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_684, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_684, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_684 = owner_Connection_681;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_684.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_684, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_684, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_684 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_684.In(logic_uScript_GetAndCheckTechs_techData_684, logic_uScript_GetAndCheckTechs_ownerNode_684, ref logic_uScript_GetAndCheckTechs_techs_684);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_684;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_684.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_684.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_684.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_684.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_683();
		}
		if (someAlive)
		{
			Relay_AtIndex_683();
		}
		if (allDead)
		{
			Relay_In_47();
		}
		if (waitingToSpawn)
		{
			Relay_In_47();
		}
	}

	private void Relay_In_686()
	{
		logic_uScript_ResetMissionTimerTimeElapsed_owner_686 = owner_Connection_594;
		logic_uScript_ResetMissionTimerTimeElapsed_startTime_686 = local_685_System_Single;
		logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_686.In(logic_uScript_ResetMissionTimerTimeElapsed_owner_686, logic_uScript_ResetMissionTimerTimeElapsed_startTime_686);
		if (logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_686.Out)
		{
			Relay_In_590();
		}
	}

	private void Relay_In_687()
	{
		logic_uScript_ResetMissionTimer_owner_687 = owner_Connection_688;
		logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_687.In(logic_uScript_ResetMissionTimer_owner_687);
		if (logic_uScript_ResetMissionTimer_uScript_ResetMissionTimer_687.Out)
		{
			Relay_In_686();
		}
	}

	private void Relay_In_689()
	{
		logic_uScript_StartMissionTimer_owner_689 = owner_Connection_690;
		logic_uScript_StartMissionTimer_startTime_689 = local_691_System_Single;
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_689.In(logic_uScript_StartMissionTimer_owner_689, logic_uScript_StartMissionTimer_startTime_689);
		if (logic_uScript_StartMissionTimer_uScript_StartMissionTimer_689.Out)
		{
			Relay_In_591();
		}
	}

	private void Relay_True_692()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_692.True(out logic_uScriptAct_SetBool_Target_692);
		local_GetRidOfCube_System_Boolean = logic_uScriptAct_SetBool_Target_692;
	}

	private void Relay_False_692()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_692.False(out logic_uScriptAct_SetBool_Target_692);
		local_GetRidOfCube_System_Boolean = logic_uScriptAct_SetBool_Target_692;
	}

	private void Relay_True_693()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_693.True(out logic_uScriptAct_SetBool_Target_693);
		local_IntroSkipped_System_Boolean = logic_uScriptAct_SetBool_Target_693;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_693.Out)
		{
			Relay_In_689();
		}
	}

	private void Relay_False_693()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_693.False(out logic_uScriptAct_SetBool_Target_693);
		local_IntroSkipped_System_Boolean = logic_uScriptAct_SetBool_Target_693;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_693.Out)
		{
			Relay_In_689();
		}
	}

	private void Relay_True_694()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_694.True(out logic_uScriptAct_SetBool_Target_694);
		local_OutOfTime_System_Boolean = logic_uScriptAct_SetBool_Target_694;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_694.Out)
		{
			Relay_False_696();
		}
	}

	private void Relay_False_694()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_694.False(out logic_uScriptAct_SetBool_Target_694);
		local_OutOfTime_System_Boolean = logic_uScriptAct_SetBool_Target_694;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_694.Out)
		{
			Relay_False_696();
		}
	}

	private void Relay_True_695()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_695.True(out logic_uScriptAct_SetBool_Target_695);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_695;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_695.Out)
		{
			Relay_False_692();
		}
	}

	private void Relay_False_695()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_695.False(out logic_uScriptAct_SetBool_Target_695);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_695;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_695.Out)
		{
			Relay_False_692();
		}
	}

	private void Relay_True_696()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_696.True(out logic_uScriptAct_SetBool_Target_696);
		local_TechInvulOnLoad_System_Boolean = logic_uScriptAct_SetBool_Target_696;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_696.Out)
		{
			Relay_False_697();
		}
	}

	private void Relay_False_696()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_696.False(out logic_uScriptAct_SetBool_Target_696);
		local_TechInvulOnLoad_System_Boolean = logic_uScriptAct_SetBool_Target_696;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_696.Out)
		{
			Relay_False_697();
		}
	}

	private void Relay_True_697()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_697.True(out logic_uScriptAct_SetBool_Target_697);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_697;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_697.Out)
		{
			Relay_False_698();
		}
	}

	private void Relay_False_697()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_697.False(out logic_uScriptAct_SetBool_Target_697);
		local_TankInvul_System_Boolean = logic_uScriptAct_SetBool_Target_697;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_697.Out)
		{
			Relay_False_698();
		}
	}

	private void Relay_True_698()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_698.True(out logic_uScriptAct_SetBool_Target_698);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_698;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_698.Out)
		{
			Relay_False_699();
		}
	}

	private void Relay_False_698()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_698.False(out logic_uScriptAct_SetBool_Target_698);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_698;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_698.Out)
		{
			Relay_False_699();
		}
	}

	private void Relay_True_699()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_699.True(out logic_uScriptAct_SetBool_Target_699);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_699;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_699.Out)
		{
			Relay_False_693();
		}
	}

	private void Relay_False_699()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_699.False(out logic_uScriptAct_SetBool_Target_699);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_699;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_699.Out)
		{
			Relay_False_693();
		}
	}

	private void Relay_In_709()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_709.In(logic_uScriptAct_SetInt_Value_709, out logic_uScriptAct_SetInt_Target_709);
		local_NPCIntro_System_Int32 = logic_uScriptAct_SetInt_Target_709;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_709.Out)
		{
			Relay_In_714();
		}
	}

	private void Relay_In_714()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_714.In(logic_uScriptAct_SetInt_Value_714, out logic_uScriptAct_SetInt_Target_714);
		local_CubeDialog_System_Int32 = logic_uScriptAct_SetInt_Target_714;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_714.Out)
		{
			Relay_In_715();
		}
	}

	private void Relay_In_715()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_715.In(logic_uScriptAct_SetInt_Value_715, out logic_uScriptAct_SetInt_Target_715);
		local_OutOfTimeMsg_System_Int32 = logic_uScriptAct_SetInt_Target_715;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_715.Out)
		{
			Relay_In_716();
		}
	}

	private void Relay_In_716()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_716.In(logic_uScriptAct_SetInt_Value_716, out logic_uScriptAct_SetInt_Target_716);
		local_CrazedDialog_System_Int32 = logic_uScriptAct_SetInt_Target_716;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_716.Out)
		{
			Relay_In_717();
		}
	}

	private void Relay_In_717()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_717.In(logic_uScriptAct_SetInt_Value_717, out logic_uScriptAct_SetInt_Target_717);
		local_CubeDestroyedDialog_System_Int32 = logic_uScriptAct_SetInt_Target_717;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_717.Out)
		{
			Relay_In_818();
		}
	}

	private void Relay_In_720()
	{
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_720 = local_CubeTech_Tank;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_720 = local_718_TechSequencer_ChainType;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_720.In(logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_720, logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_720);
		if (logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_720.Out)
		{
			Relay_In_807();
		}
	}

	private void Relay_Out_721()
	{
		Relay_False_694();
	}

	private void Relay_In_721()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_721 = local_Objective_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_721.In(logic_SubGraph_LoadObjectiveStates_currentObjective_721);
	}

	private void Relay_In_725()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_725.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_725, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_725, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_725 = owner_Connection_726;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_725.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_725, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_725, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_725 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_725.In(logic_uScript_GetAndCheckTechs_techData_725, logic_uScript_GetAndCheckTechs_ownerNode_725, ref logic_uScript_GetAndCheckTechs_techs_725);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_725;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_725.AllDead)
		{
			Relay_True_613();
		}
	}

	private void Relay_In_742()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_742.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_742.Out)
		{
			Relay_In_336();
		}
	}

	private void Relay_In_747()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_747.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_747.Out)
		{
			Relay_In_157();
		}
	}

	private void Relay_In_753()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_753.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_753.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_753.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_532();
		}
		if (multiplayer)
		{
			Relay_In_754();
		}
	}

	private void Relay_In_754()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_754.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_754.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_755()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_755.In();
		if (logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_755.SinglePlayer)
		{
			Relay_In_459();
		}
	}

	private void Relay_In_756()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_756.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_756.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_756.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_429();
		}
		if (multiplayer)
		{
			Relay_In_757();
		}
	}

	private void Relay_In_757()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_757.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_757.Out)
		{
			Relay_In_758();
		}
	}

	private void Relay_In_758()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_758.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_758.Out)
		{
			Relay_In_336();
		}
	}

	private void Relay_In_759()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_759.In();
		if (logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_759.SinglePlayer)
		{
			Relay_False_91();
		}
	}

	private void Relay_In_760()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_760.In();
		if (logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_760.SinglePlayer)
		{
			Relay_False_762();
		}
	}

	private void Relay_True_762()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_762.True(out logic_uScriptAct_SetBool_Target_762);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_762;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_762.Out)
		{
			Relay_False_763();
		}
	}

	private void Relay_False_762()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_762.False(out logic_uScriptAct_SetBool_Target_762);
		local_FightStarted_System_Boolean = logic_uScriptAct_SetBool_Target_762;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_762.Out)
		{
			Relay_False_763();
		}
	}

	private void Relay_True_763()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_763.True(out logic_uScriptAct_SetBool_Target_763);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_763;
	}

	private void Relay_False_763()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_763.False(out logic_uScriptAct_SetBool_Target_763);
		local_FightRunning_System_Boolean = logic_uScriptAct_SetBool_Target_763;
	}

	private void Relay_In_765()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_765.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_765.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_765.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_152();
		}
		if (multiplayer)
		{
			Relay_In_112();
		}
	}

	private void Relay_In_766()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_766.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_766.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_766.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_424();
		}
		if (multiplayer)
		{
			Relay_In_767();
		}
	}

	private void Relay_In_767()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_767.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_767.Out)
		{
			Relay_In_395();
		}
	}

	private void Relay_In_768()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_768.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_768.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_768.Multiplayer;
		if (singlePlayer)
		{
			Relay_True_132();
		}
		if (multiplayer)
		{
			Relay_In_770();
		}
	}

	private void Relay_In_770()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_770 = LeaderIntroStartTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_770.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_770);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_770.InRange)
		{
			Relay_True_775();
		}
	}

	private void Relay_True_772()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_772.True(out logic_uScriptAct_SetBool_Target_772);
		local_CubeNeedsReload_System_Boolean = logic_uScriptAct_SetBool_Target_772;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_772.Out)
		{
			Relay_False_776();
		}
	}

	private void Relay_False_772()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_772.False(out logic_uScriptAct_SetBool_Target_772);
		local_CubeNeedsReload_System_Boolean = logic_uScriptAct_SetBool_Target_772;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_772.Out)
		{
			Relay_False_776();
		}
	}

	private void Relay_True_775()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_775.True(out logic_uScriptAct_SetBool_Target_775);
		local_LeftAreaAfterLoss_System_Boolean = logic_uScriptAct_SetBool_Target_775;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_775.Out)
		{
			Relay_True_772();
		}
	}

	private void Relay_False_775()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_775.False(out logic_uScriptAct_SetBool_Target_775);
		local_LeftAreaAfterLoss_System_Boolean = logic_uScriptAct_SetBool_Target_775;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_775.Out)
		{
			Relay_True_772();
		}
	}

	private void Relay_True_776()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_776.True(out logic_uScriptAct_SetBool_Target_776);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_776;
	}

	private void Relay_False_776()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_776.False(out logic_uScriptAct_SetBool_Target_776);
		local_CubeisOK_System_Boolean = logic_uScriptAct_SetBool_Target_776;
	}

	private void Relay_In_777()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_777.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_777.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_777.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_485();
		}
		if (multiplayer)
		{
			Relay_In_780();
		}
	}

	private void Relay_In_780()
	{
		logic_uScript_AddMessage_messageData_780 = MsgOutOfTimeMultiplayer;
		logic_uScript_AddMessage_speaker_780 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_780 = logic_uScript_AddMessage_uScript_AddMessage_780.In(logic_uScript_AddMessage_messageData_780, logic_uScript_AddMessage_speaker_780);
		if (logic_uScript_AddMessage_uScript_AddMessage_780.Shown)
		{
			Relay_False_782();
		}
	}

	private void Relay_True_782()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_782.True(out logic_uScriptAct_SetBool_Target_782);
		local_OutOfTime_System_Boolean = logic_uScriptAct_SetBool_Target_782;
	}

	private void Relay_False_782()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_782.False(out logic_uScriptAct_SetBool_Target_782);
		local_OutOfTime_System_Boolean = logic_uScriptAct_SetBool_Target_782;
	}

	private void Relay_In_783()
	{
		logic_uScript_RemoveTech_tech_783 = local_CubeTech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_783.In(logic_uScript_RemoveTech_tech_783);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_783.Out)
		{
			Relay_In_725();
		}
	}

	private void Relay_In_785()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_785 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_785.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_785);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_785.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_785.OutOfRange;
		if (inRange)
		{
			Relay_In_787();
		}
		if (outOfRange)
		{
			Relay_In_658();
		}
	}

	private void Relay_In_786()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_786.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_786.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_786.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_658();
		}
		if (multiplayer)
		{
			Relay_In_785();
		}
	}

	private void Relay_In_787()
	{
		logic_uScript_AddMessage_messageData_787 = MsgOutOfTimeMultiplayerLeave;
		logic_uScript_AddMessage_speaker_787 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_787 = logic_uScript_AddMessage_uScript_AddMessage_787.In(logic_uScript_AddMessage_messageData_787, logic_uScript_AddMessage_speaker_787);
	}

	private void Relay_In_791()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_791.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_791.Out)
		{
			Relay_In_376();
		}
	}

	private void Relay_In_792()
	{
		logic_uScript_SetTechExplodeDetachingBlocks_tech_792 = local_CubeTech_Tank;
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_792.In(logic_uScript_SetTechExplodeDetachingBlocks_tech_792, logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_792, logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_792);
	}

	private void Relay_In_794()
	{
		logic_uScript_SetTechExplodeDetachingBlocks_tech_794 = local_CrazedTech2_Tank;
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_794.In(logic_uScript_SetTechExplodeDetachingBlocks_tech_794, logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_794, logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_794);
		if (logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_794.Out)
		{
			Relay_In_796();
		}
	}

	private void Relay_In_796()
	{
		logic_uScript_SetTechExplodeDetachingBlocks_tech_796 = local_CrazedTech3_Tank;
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_796.In(logic_uScript_SetTechExplodeDetachingBlocks_tech_796, logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_796, logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_796);
	}

	private void Relay_In_798()
	{
		logic_uScriptCon_CompareBool_Bool_798 = local_MinionsAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_798.In(logic_uScriptCon_CompareBool_Bool_798);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_798.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_798.False;
		if (num)
		{
			Relay_In_605();
		}
		if (flag)
		{
			Relay_In_605();
		}
	}

	private void Relay_In_799()
	{
		logic_uScriptCon_CompareBool_Bool_799 = local_CubeAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_799.In(logic_uScriptCon_CompareBool_Bool_799);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_799.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_799.False;
		if (num)
		{
			Relay_In_720();
		}
		if (flag)
		{
			Relay_In_807();
		}
	}

	private void Relay_True_800()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_800.True(out logic_uScriptAct_SetBool_Target_800);
		local_NPCTechAlive_System_Boolean = logic_uScriptAct_SetBool_Target_800;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_800.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_800.SetTrue;
		if (num)
		{
			Relay_In_601();
		}
		if (setTrue)
		{
			Relay_AtIndex_636();
		}
	}

	private void Relay_False_800()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_800.False(out logic_uScriptAct_SetBool_Target_800);
		local_NPCTechAlive_System_Boolean = logic_uScriptAct_SetBool_Target_800;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_800.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_800.SetTrue;
		if (num)
		{
			Relay_In_601();
		}
		if (setTrue)
		{
			Relay_AtIndex_636();
		}
	}

	private void Relay_True_802()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_802.True(out logic_uScriptAct_SetBool_Target_802);
		local_LeaderTechAlive_System_Boolean = logic_uScriptAct_SetBool_Target_802;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_802.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_802.SetTrue;
		if (num)
		{
			Relay_In_603();
		}
		if (setTrue)
		{
			Relay_AtIndex_641();
		}
	}

	private void Relay_False_802()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_802.False(out logic_uScriptAct_SetBool_Target_802);
		local_LeaderTechAlive_System_Boolean = logic_uScriptAct_SetBool_Target_802;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_802.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_802.SetTrue;
		if (num)
		{
			Relay_In_603();
		}
		if (setTrue)
		{
			Relay_AtIndex_641();
		}
	}

	private void Relay_True_804()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_804.True(out logic_uScriptAct_SetBool_Target_804);
		local_MinionsAlive_System_Boolean = logic_uScriptAct_SetBool_Target_804;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_804.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_804.SetTrue;
		if (num)
		{
			Relay_In_798();
		}
		if (setTrue)
		{
			Relay_AtIndex_642();
		}
	}

	private void Relay_False_804()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_804.False(out logic_uScriptAct_SetBool_Target_804);
		local_MinionsAlive_System_Boolean = logic_uScriptAct_SetBool_Target_804;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_804.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_804.SetTrue;
		if (num)
		{
			Relay_In_798();
		}
		if (setTrue)
		{
			Relay_AtIndex_642();
		}
	}

	private void Relay_In_807()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_807.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_807.Out)
		{
			Relay_In_120();
		}
	}

	private void Relay_In_811()
	{
		logic_uScript_PlayDialogue_dialogue_811 = StartBossFightDialogue;
		logic_uScript_PlayDialogue_progress_811 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_811.In(logic_uScript_PlayDialogue_dialogue_811, ref logic_uScript_PlayDialogue_progress_811);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_811;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_811.Shown)
		{
			Relay_In_131();
		}
	}

	private void Relay_In_815()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_815.In(logic_uScriptAct_SetInt_Value_815, out logic_uScriptAct_SetInt_Target_815);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_815;
	}

	private void Relay_In_816()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_816.In(logic_uScriptAct_SetInt_Value_816, out logic_uScriptAct_SetInt_Target_816);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_816;
	}

	private void Relay_In_818()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_818.In(logic_uScriptAct_SetInt_Value_818, out logic_uScriptAct_SetInt_Target_818);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_818;
	}

	private void Relay_In_824()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_824 = owner_Connection_825;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_824.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_824);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_824.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_824.False;
		if (num)
		{
			Relay_Pause_64();
		}
		if (flag)
		{
			Relay_UnPause_64();
		}
	}
}
