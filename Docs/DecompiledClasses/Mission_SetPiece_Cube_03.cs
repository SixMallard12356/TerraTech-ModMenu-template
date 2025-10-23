using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("", "")]
public class Mission_SetPiece_Cube_03 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public uScript_PlayDialogue.Dialogue CompletelyNoMoreAmbushDialogue;

	public uScript_AddMessage.MessageSpeaker CrazedLeaderSpeaker;

	public SpawnTechData[] CrazedMinionTechData = new SpawnTechData[0];

	public uScript_AddMessage.MessageSpeaker CrazedMinionTechSpeaker;

	[Multiline(3)]
	public string CrazedMsgTag = "";

	public SpawnTechData[] CrazedTechData = new SpawnTechData[0];

	public SpawnTechData[] CubeAmbushTechData = new SpawnTechData[0];

	[Multiline(3)]
	public string CubeAreaTrigger = "";

	public uScript_PlayDialogue.Dialogue CubeDefeatedDialogue;

	public uScript_PlayDialogue.Dialogue CubeLeaveAreaFailDialogue;

	public BlockTypes CubeShieldBlockData;

	public SpawnTechData[] CubeTechData = new SpawnTechData[0];

	public uScript_PlayDialogue.Dialogue FailedToKillInvincibleCubeDialogue;

	[Multiline(3)]
	public string FillerNPCRange01 = "";

	[Multiline(3)]
	public string FillerNPCRange02 = "";

	public SpawnTechData[] FillerTechData = new SpawnTechData[0];

	public uScript_AddMessage.MessageSpeaker GCSpeaker;

	public uScript_PlayDialogue.Dialogue InvulnerableCubeIntro;

	public uScript_PlayDialogue.Dialogue LeaderIntroDialogue;

	[Multiline(3)]
	public string LeaderIntroStartTrigger = "";

	[Multiline(3)]
	public string LeaderOutOfRangeTrigger = "";

	public uScript_PlayDialogue.Dialogue LeaderTryAgainDialogue;

	private string local_199_System_String = "Stage:";

	private string local_200_System_String = "";

	private string local_201_System_String = ",Round:";

	private string local_203_System_String = "";

	private string local_207_System_String = "";

	private string local_209_System_String = ",Dialogue:";

	private string local_210_System_String = "";

	private string local_213_System_String = "";

	private string local_215_System_String = "";

	private string local_216_System_String = "";

	private string local_217_System_String = ",InRange:";

	private float local_233_System_Single;

	private TechSequencer.ChainType local_418_TechSequencer_ChainType = TechSequencer.ChainType.ShieldBubble;

	private TechSequencer.ChainType local_439_TechSequencer_ChainType = TechSequencer.ChainType.ShieldBubble;

	private TechSequencer.ChainType local_84_TechSequencer_ChainType = TechSequencer.ChainType.ShieldBubble;

	private Tank[] local_AmbushTechs_TankArray = new Tank[0];

	private bool local_AmbushTechsAllDead_System_Boolean;

	private Tank local_CrazedMinion1_Tank;

	private Tank local_CrazedMinion2_Tank;

	private bool local_CrazedMinionsAllAlive_System_Boolean;

	private bool local_CrazedMinionsAllDead_System_Boolean;

	private Tank[] local_CrazedMinionTechs_TankArray = new Tank[0];

	private Tank local_CrazedTech_Tank;

	private bool local_CrazedTechAlive_System_Boolean;

	private Tank[] local_CrazedTechs_TankArray = new Tank[0];

	private bool local_CubeAlive_System_Boolean;

	private TankBlock local_CubeShieldBlock_TankBlock;

	private Tank local_CubeTech_Tank;

	private Tank[] local_CubeTechs_TankArray = new Tank[0];

	private bool local_CubeTooEarlyMsgPlayed_System_Boolean;

	private int local_DialogueProgress_System_Int32;

	private int local_DialogueProgressExtra_System_Int32;

	private bool local_FillerMsgPlayed01_System_Boolean;

	private bool local_FillerMsgPlayed02_System_Boolean;

	private Tank local_FillerTech01_Tank;

	private Tank local_FillerTech02_Tank;

	private Tank local_FillerTech03_Tank;

	private Tank[] local_FillerTechs_TankArray = new Tank[0];

	private bool local_FillerTechsAllAlive_System_Boolean;

	private bool local_FinalDialoguePlayed_System_Boolean;

	private bool local_InRange_System_Boolean;

	private bool local_MidDialogueMsgPlayed_System_Boolean;

	private int local_Objective_System_Int32 = 1;

	private int local_Round_System_Int32 = 1;

	private int local_RoundCubeInvincible_System_Int32 = 1;

	private int local_RoundNoAmbush_System_Int32 = 3;

	private int local_RoundSpawnAmbush_System_Int32 = 2;

	private int local_Stage_System_Int32 = 1;

	private int local_StageApproachLeader_System_Int32 = 3;

	private int local_StageConfigureTechs_System_Int32 = 2;

	private int local_StageCubeBattleFailed_System_Int32 = 8;

	private int local_StageCubeBattleIntro_System_Int32 = 5;

	private int local_StageCubeBattleRunning_System_Int32 = 6;

	private int local_StageCubeBattleSetupAmbush_System_Int32 = 7;

	private int local_StageCubeDefeated_System_Int32 = 10;

	private int local_StageFinalBattle_System_Int32 = 11;

	private int local_StageResetCube_System_Int32 = 9;

	private int local_StageStart_System_Int32 = 1;

	private int local_StageTalkToLeader_System_Int32 = 4;

	public uScript_AddMessage.MessageData MsgCrazedInterrupt;

	public uScript_AddMessage.MessageData msgCubeTooEarly;

	public uScript_AddMessage.MessageData MsgFillerNPC01;

	public uScript_AddMessage.MessageData MsgFillerNPC02;

	public uScript_PlayDialogue.Dialogue MsgFinalCubeDeathDialogue;

	public uScript_AddMessage.MessageData MsgInvincibleCubeSwitchOff;

	public uScript_AddMessage.MessageData MsgLeaderTryAgainFollow;

	public uScript_AddMessage.MessageData MsgMissionCompleteNoTrigger;

	public uScript_AddMessage.MessageData MsgMultiplayerLeaveArea;

	public uScript_AddMessage.MessageData MsgOutOfTime;

	public uScript_AddMessage.MessageData MsgStartBossFight;

	public uScript_PlayDialogue.Dialogue NoMoreAmbushDialogue;

	public uScript_PlayDialogue.Dialogue OutofTimeDialogue;

	public uScript_AddMessage.MessageSpeaker SingleMinionTechSpeaker;

	public uScript_PlayDialogue.Dialogue SpawnAmbushDeadDialogue;

	public uScript_PlayDialogue.Dialogue SpawnAmbushDialogue;

	public uScript_PlayDialogue.Dialogue StartBossFightDialogue;

	public ExternalBehaviorTree TechFlyAI;

	public Transform TechFlyParticles;

	public float TimeLimit;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_13;

	private GameObject owner_Connection_15;

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_20;

	private GameObject owner_Connection_28;

	private GameObject owner_Connection_31;

	private GameObject owner_Connection_35;

	private GameObject owner_Connection_44;

	private GameObject owner_Connection_63;

	private GameObject owner_Connection_136;

	private GameObject owner_Connection_174;

	private GameObject owner_Connection_223;

	private GameObject owner_Connection_230;

	private GameObject owner_Connection_235;

	private GameObject owner_Connection_281;

	private GameObject owner_Connection_297;

	private GameObject owner_Connection_304;

	private GameObject owner_Connection_323;

	private GameObject owner_Connection_339;

	private GameObject owner_Connection_356;

	private GameObject owner_Connection_410;

	private GameObject owner_Connection_449;

	private GameObject owner_Connection_464;

	private GameObject owner_Connection_468;

	private GameObject owner_Connection_485;

	private GameObject owner_Connection_492;

	private GameObject owner_Connection_555;

	private GameObject owner_Connection_556;

	private GameObject owner_Connection_557;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_5;

	private int logic_SubGraph_SaveLoadInt_integer_5;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_5 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_5 = "Stage";

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_12 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_12 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_12;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_12 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_12;

	private bool logic_uScript_SpawnTechsFromData_Out_12 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_14 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_14 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_14;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_14 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_14;

	private bool logic_uScript_SpawnTechsFromData_Out_14 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_16 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_16 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_16;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_16 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_16;

	private bool logic_uScript_SpawnTechsFromData_Out_16 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_18 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_18;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_18 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_19 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_19 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_19;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_19 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_19 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_19 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_24 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_24 = new Tank[0];

	private int logic_uScript_AccessListTech_index_24 = 1;

	private Tank logic_uScript_AccessListTech_value_24;

	private bool logic_uScript_AccessListTech_Out_24 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_25 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_25;

	private object logic_uScript_SetEncounterTarget_visibleObject_25 = "";

	private bool logic_uScript_SetEncounterTarget_Out_25 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_29 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_29 = new Tank[0];

	private int logic_uScript_AccessListTech_index_29;

	private Tank logic_uScript_AccessListTech_value_29;

	private bool logic_uScript_AccessListTech_Out_29 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_30 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_30 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_30;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_30 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_30;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_30 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_30 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_30 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_30 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_36 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_36 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_36;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_36 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_36;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_36 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_36 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_36 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_36 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_38 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_38 = new Tank[0];

	private int logic_uScript_AccessListTech_index_38;

	private Tank logic_uScript_AccessListTech_value_38;

	private bool logic_uScript_AccessListTech_Out_38 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_41 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_41;

	private bool logic_uScriptAct_SetBool_Out_41 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_41 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_41 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_45 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_45 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_45;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_45 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_45;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_45 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_45 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_45 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_45 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_48 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_48 = new Tank[0];

	private int logic_uScript_AccessListTech_index_48;

	private Tank logic_uScript_AccessListTech_value_48;

	private bool logic_uScript_AccessListTech_Out_48 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_49 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_49;

	private bool logic_uScriptAct_SetBool_Out_49 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_49 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_49 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_56 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_56 = new Tank[0];

	private int logic_uScript_AccessListTech_index_56 = 1;

	private Tank logic_uScript_AccessListTech_value_56;

	private bool logic_uScript_AccessListTech_Out_56 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_59 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_59 = new Tank[0];

	private int logic_uScript_AccessListTech_index_59;

	private Tank logic_uScript_AccessListTech_value_59;

	private bool logic_uScript_AccessListTech_Out_59 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_60 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_60 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_60;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_60 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_60;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_60 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_60 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_60 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_60 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_66 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_66 = new Tank[0];

	private int logic_uScript_AccessListTech_index_66 = 2;

	private Tank logic_uScript_AccessListTech_value_66;

	private bool logic_uScript_AccessListTech_Out_66 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_68 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_68;

	private int logic_uScriptAct_SetInt_Target_68;

	private bool logic_uScriptAct_SetInt_Out_68 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_69 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_69;

	private bool logic_uScriptCon_CompareBool_True_69 = true;

	private bool logic_uScriptCon_CompareBool_False_69 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_70 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_70;

	private bool logic_uScriptCon_CompareBool_True_70 = true;

	private bool logic_uScriptCon_CompareBool_False_70 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_74 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_74 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_74;

	private bool logic_uScript_SetTankInvulnerable_Out_74 = true;

	private uScript_SetTechExplodeDetachingBlocks logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_76 = new uScript_SetTechExplodeDetachingBlocks();

	private Tank logic_uScript_SetTechExplodeDetachingBlocks_tech_76;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_76 = true;

	private float logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_76 = 0.1f;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_Out_76 = true;

	private uScript_SetTechExplodeDetachingBlocks logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_79 = new uScript_SetTechExplodeDetachingBlocks();

	private Tank logic_uScript_SetTechExplodeDetachingBlocks_tech_79;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_79 = true;

	private float logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_79 = 0.1f;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_Out_79 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_82 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_82;

	private BlockTypes logic_uScript_GetTankBlock_blockType_82;

	private TankBlock logic_uScript_GetTankBlock_Return_82;

	private bool logic_uScript_GetTankBlock_Out_82 = true;

	private bool logic_uScript_GetTankBlock_Returned_82 = true;

	private bool logic_uScript_GetTankBlock_NotFound_82 = true;

	private uScript_AllowTechEnergyModuleSimultaneousActivation logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_83 = new uScript_AllowTechEnergyModuleSimultaneousActivation();

	private Tank logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_83;

	private TechSequencer.ChainType logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_83;

	private bool logic_uScript_AllowTechEnergyModuleSimultaneousActivation_Out_83 = true;

	private uScript_SetTechExplodeDetachingBlocks logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_85 = new uScript_SetTechExplodeDetachingBlocks();

	private Tank logic_uScript_SetTechExplodeDetachingBlocks_tech_85;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_85 = true;

	private float logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_85 = 0.1f;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_Out_85 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_86 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_86;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_86 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_86 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_89 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_89 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_89;

	private bool logic_uScript_SetTankInvulnerable_Out_89 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_90 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_90 = "";

	private bool logic_uScript_SetShieldEnabled_enable_90 = true;

	private bool logic_uScript_SetShieldEnabled_Out_90 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_96 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_96 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_96 = -2;

	private bool logic_uScript_SetTechsTeam_Out_96 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_99 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_99 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_99 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_99 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_99 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_99 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_99 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_101 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_101;

	private int logic_uScriptAct_SetInt_Target_101;

	private bool logic_uScriptAct_SetInt_Out_101 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_104 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_104;

	private int logic_uScriptAct_SetInt_Target_104;

	private bool logic_uScriptAct_SetInt_Out_104 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_106 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_106;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_106;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_106;

	private bool logic_uScript_AddMessage_Out_106 = true;

	private bool logic_uScript_AddMessage_Shown_106 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_108 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_108;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_108;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_108;

	private bool logic_uScript_AddMessage_Out_108 = true;

	private bool logic_uScript_AddMessage_Shown_108 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_109 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_109 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_109 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_109 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_109 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_109 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_109 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_112 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_112;

	private bool logic_uScriptAct_SetBool_Out_112 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_112 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_112 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_113 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_113;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_113;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_113;

	private bool logic_uScript_AddMessage_Out_113 = true;

	private bool logic_uScript_AddMessage_Shown_113 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_118 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_118;

	private bool logic_uScriptAct_SetBool_Out_118 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_118 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_118 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_119 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_119;

	private bool logic_uScriptCon_CompareBool_True_119 = true;

	private bool logic_uScriptCon_CompareBool_False_119 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_123 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_123;

	private bool logic_uScriptAct_SetBool_Out_123 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_123 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_123 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_125 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_125 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_125 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_125 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_125 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_125 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_125 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_127 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_127;

	private bool logic_uScriptCon_CompareBool_True_127 = true;

	private bool logic_uScriptCon_CompareBool_False_127 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_128 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_128;

	private bool logic_uScriptCon_CompareBool_True_128 = true;

	private bool logic_uScriptCon_CompareBool_False_128 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_131 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_131 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_131 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_131 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_131 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_131 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_131 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_132 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_132 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_138 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_138;

	private object logic_uScript_SetEncounterTarget_visibleObject_138 = "";

	private bool logic_uScript_SetEncounterTarget_Out_138 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_141 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_141;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_141;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_141;

	private bool logic_uScript_AddMessage_Out_141 = true;

	private bool logic_uScript_AddMessage_Shown_141 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_144 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_144;

	private int logic_uScript_PlayDialogue_progress_144;

	private bool logic_uScript_PlayDialogue_Out_144 = true;

	private bool logic_uScript_PlayDialogue_Shown_144 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_144 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_145 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_145;

	private int logic_uScriptAct_SetInt_Target_145;

	private bool logic_uScriptAct_SetInt_Out_145 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_150 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_150;

	private int logic_uScriptAct_SetInt_Target_150;

	private bool logic_uScriptAct_SetInt_Out_150 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_151 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_151 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_151;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_151 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_154 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_154;

	private bool logic_uScriptAct_SetBool_Out_154 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_154 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_154 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_157;

	private int logic_SubGraph_SaveLoadInt_integer_157;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_157 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_157 = "DialogueProgress";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_159;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_159 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_159 = "InRange";

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_161 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_161 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_161 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_161;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_161 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_161 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_161 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_161 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_161 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_161 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_161 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_163 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_163;

	private bool logic_uScriptAct_SetBool_Out_163 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_163 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_163 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_165 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_165;

	private bool logic_uScriptAct_SetBool_Out_165 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_165 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_165 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_167 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_167;

	private bool logic_uScriptCon_CompareBool_True_167 = true;

	private bool logic_uScriptCon_CompareBool_False_167 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_169 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_169 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_169 = 1;

	private bool logic_uScript_SetTechsTeam_Out_169 = true;

	private uScript_PlayMiscSFX logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_171 = new uScript_PlayMiscSFX();

	private ManSFX.MiscSfxType logic_uScript_PlayMiscSFX_miscSFXType_171 = ManSFX.MiscSfxType.StuntRingStart;

	private bool logic_uScript_PlayMiscSFX_Out_171 = true;

	private uScript_StartMissionTimer logic_uScript_StartMissionTimer_uScript_StartMissionTimer_173 = new uScript_StartMissionTimer();

	private GameObject logic_uScript_StartMissionTimer_owner_173;

	private float logic_uScript_StartMissionTimer_startTime_173;

	private bool logic_uScript_StartMissionTimer_Out_173 = true;

	private uScript_ShowMissionTimerUI logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_175 = new uScript_ShowMissionTimerUI();

	private GameObject logic_uScript_ShowMissionTimerUI_owner_175;

	private bool logic_uScript_ShowMissionTimerUI_showBestTime_175;

	private bool logic_uScript_ShowMissionTimerUI_Out_175 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_177 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_177 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_177 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_177;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_177 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_177 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_177 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_177 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_177 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_177 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_177 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_178 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_178;

	private int logic_uScriptAct_SetInt_Target_178;

	private bool logic_uScriptAct_SetInt_Out_178 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_181 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_181;

	private bool logic_uScriptAct_SetBool_Out_181 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_181 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_181 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_182 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_182;

	private int logic_uScript_PlayDialogue_progress_182;

	private bool logic_uScript_PlayDialogue_Out_182 = true;

	private bool logic_uScript_PlayDialogue_Shown_182 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_182 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_185;

	private int logic_SubGraph_SaveLoadInt_integer_185;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_185 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_185 = "Round";

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_188 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_188;

	private int logic_uScriptAct_SetInt_Target_188;

	private bool logic_uScriptAct_SetInt_Out_188 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_192 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_192;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_192;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_192;

	private bool logic_uScript_AddMessage_Out_192 = true;

	private bool logic_uScript_AddMessage_Shown_192 = true;

	private uScriptAct_PrintText logic_uScriptAct_PrintText_uScriptAct_PrintText_195 = new uScriptAct_PrintText();

	private string logic_uScriptAct_PrintText_Text_195 = "";

	private int logic_uScriptAct_PrintText_FontSize_195 = 16;

	private FontStyle logic_uScriptAct_PrintText_FontStyle_195;

	private Color logic_uScriptAct_PrintText_FontColor_195 = new Color(0f, 0f, 0f, 1f);

	private TextAnchor logic_uScriptAct_PrintText_textAnchor_195;

	private int logic_uScriptAct_PrintText_EdgePadding_195 = 8;

	private float logic_uScriptAct_PrintText_time_195;

	private bool logic_uScriptAct_PrintText_Out_195 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_196 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_196 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_196 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_196 = "";

	private string logic_uScriptAct_Concatenate_Result_196;

	private bool logic_uScriptAct_Concatenate_Out_196 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_197 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_197 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_197 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_197 = "";

	private string logic_uScriptAct_Concatenate_Result_197;

	private bool logic_uScriptAct_Concatenate_Out_197 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_202 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_202 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_202 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_202 = "";

	private string logic_uScriptAct_Concatenate_Result_202;

	private bool logic_uScriptAct_Concatenate_Out_202 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_208 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_208 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_208 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_208 = "";

	private string logic_uScriptAct_Concatenate_Result_208;

	private bool logic_uScriptAct_Concatenate_Out_208 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_211 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_211 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_211 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_211 = "";

	private string logic_uScriptAct_Concatenate_Result_211;

	private bool logic_uScriptAct_Concatenate_Out_211 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_212 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_212 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_212 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_212 = "";

	private string logic_uScriptAct_Concatenate_Result_212;

	private bool logic_uScriptAct_Concatenate_Out_212 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_214 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_214 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_214 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_214 = "";

	private string logic_uScriptAct_Concatenate_Result_214;

	private bool logic_uScriptAct_Concatenate_Out_214 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_220 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_220 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_220 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_220;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_220 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_220 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_220 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_220 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_220 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_220 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_220 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_224 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_224;

	private object logic_uScript_SetEncounterTarget_visibleObject_224 = "";

	private bool logic_uScript_SetEncounterTarget_Out_224 = true;

	private uScript_HideMissionTimerUI logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_226 = new uScript_HideMissionTimerUI();

	private GameObject logic_uScript_HideMissionTimerUI_owner_226;

	private bool logic_uScript_HideMissionTimerUI_Out_226 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_227 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_227 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_227 = -2;

	private bool logic_uScript_SetTechsTeam_Out_227 = true;

	private uScript_StopMissionTimer logic_uScript_StopMissionTimer_uScript_StopMissionTimer_229 = new uScript_StopMissionTimer();

	private GameObject logic_uScript_StopMissionTimer_owner_229;

	private bool logic_uScript_StopMissionTimer_Out_229 = true;

	private uScript_PlayMiscSFX logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_231 = new uScript_PlayMiscSFX();

	private ManSFX.MiscSfxType logic_uScript_PlayMiscSFX_miscSFXType_231 = ManSFX.MiscSfxType.StuntFailed;

	private bool logic_uScript_PlayMiscSFX_Out_231 = true;

	private uScript_GetMissionTimerDisplayTime logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_232 = new uScript_GetMissionTimerDisplayTime();

	private GameObject logic_uScript_GetMissionTimerDisplayTime_owner_232;

	private float logic_uScript_GetMissionTimerDisplayTime_Return_232;

	private bool logic_uScript_GetMissionTimerDisplayTime_Out_232 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_234 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_234;

	private float logic_uScriptCon_CompareFloat_B_234;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_234 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_234 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_234 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_234 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_234 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_234 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_238 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_238;

	private bool logic_uScriptCon_CompareBool_True_238 = true;

	private bool logic_uScriptCon_CompareBool_False_238 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_260 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_260;

	private int logic_uScriptAct_SetInt_Target_260;

	private bool logic_uScriptAct_SetInt_Out_260 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_264 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_264 = true;

	private uScriptCon_BigManualSwitch logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265 = new uScriptCon_BigManualSwitch();

	private int logic_uScriptCon_BigManualSwitch_CurrentOutput_265;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_266 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_266;

	private int logic_uScriptCon_CheckIntEquals_B_266;

	private bool logic_uScriptCon_CheckIntEquals_True_266 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_266 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_267 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_267;

	private int logic_uScriptCon_CheckIntEquals_B_267;

	private bool logic_uScriptCon_CheckIntEquals_True_267 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_267 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_270 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_270;

	private int logic_uScriptCon_CheckIntEquals_B_270;

	private bool logic_uScriptCon_CheckIntEquals_True_270 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_270 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_273 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_273;

	private int logic_uScriptCon_CheckIntEquals_B_273;

	private bool logic_uScriptCon_CheckIntEquals_True_273 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_273 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_278 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_278;

	private int logic_uScriptAct_SetInt_Target_278;

	private bool logic_uScriptAct_SetInt_Out_278 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_280 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_280;

	private bool logic_uScriptAct_SetBool_Out_280 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_280 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_280 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_283 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_283 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_283;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_283 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_283;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_283 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_283 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_283 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_283 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_285 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_285;

	private int logic_uScript_PlayDialogue_progress_285;

	private bool logic_uScript_PlayDialogue_Out_285 = true;

	private bool logic_uScript_PlayDialogue_Shown_285 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_285 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_289 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_289;

	private int logic_uScript_PlayDialogue_progress_289;

	private bool logic_uScript_PlayDialogue_Out_289 = true;

	private bool logic_uScript_PlayDialogue_Shown_289 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_289 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_292 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_292;

	private int logic_uScriptAct_SetInt_Target_292;

	private bool logic_uScriptAct_SetInt_Out_292 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_294 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_294;

	private int logic_uScriptCon_CheckIntEquals_B_294 = 1;

	private bool logic_uScriptCon_CheckIntEquals_True_294 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_294 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_298 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_298 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_298;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_298 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_298;

	private bool logic_uScript_SpawnTechsFromData_Out_298 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_299 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_299;

	private bool logic_uScriptCon_CompareBool_True_299 = true;

	private bool logic_uScriptCon_CompareBool_False_299 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_300 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_300 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_301 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_301;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_301;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_301;

	private bool logic_uScript_AddMessage_Out_301 = true;

	private bool logic_uScript_AddMessage_Shown_301 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_305 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_305;

	private object logic_uScript_SetEncounterTarget_visibleObject_305 = "";

	private bool logic_uScript_SetEncounterTarget_Out_305 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_307 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_307;

	private bool logic_uScriptAct_SetBool_Out_307 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_307 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_307 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_310 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_310;

	private int logic_uScriptAct_SetInt_Target_310;

	private bool logic_uScriptAct_SetInt_Out_310 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_317 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_317 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_317 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_317;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_317 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_317 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_317 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_317 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_317 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_317 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_317 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_318 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_318;

	private Tank logic_uScript_SetTankInvulnerable_tank_318;

	private bool logic_uScript_SetTankInvulnerable_Out_318 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_320 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_320;

	private bool logic_uScript_RemoveTech_Out_320 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_321 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_321;

	private bool logic_uScriptCon_CompareBool_True_321 = true;

	private bool logic_uScriptCon_CompareBool_False_321 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_325 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_325 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_325;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_325 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_325 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_325 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_327 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_327;

	private int logic_uScriptAct_SetInt_Target_327;

	private bool logic_uScriptAct_SetInt_Out_327 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_328 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_328 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_331 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_331;

	private int logic_uScriptCon_CheckIntEquals_B_331;

	private bool logic_uScriptCon_CheckIntEquals_True_331 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_331 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_333 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_333;

	private int logic_uScriptAct_AddInt_v2_B_333 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_333;

	private float logic_uScriptAct_AddInt_v2_FloatResult_333;

	private bool logic_uScriptAct_AddInt_v2_Out_333 = true;

	private uScript_ResetMissionTimerTimeElapsed logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_338 = new uScript_ResetMissionTimerTimeElapsed();

	private GameObject logic_uScript_ResetMissionTimerTimeElapsed_owner_338;

	private float logic_uScript_ResetMissionTimerTimeElapsed_startTime_338;

	private bool logic_uScript_ResetMissionTimerTimeElapsed_Out_338 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_342 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_342;

	private int logic_uScriptCon_CheckIntEquals_B_342;

	private bool logic_uScriptCon_CheckIntEquals_True_342 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_342 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_345 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_345;

	private int logic_uScriptCon_CheckIntEquals_B_345;

	private bool logic_uScriptCon_CheckIntEquals_True_345 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_345 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_346 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_346;

	private int logic_uScript_PlayDialogue_progress_346;

	private bool logic_uScript_PlayDialogue_Out_346 = true;

	private bool logic_uScript_PlayDialogue_Shown_346 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_346 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_349 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_349 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_350 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_350 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_351 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_351;

	private int logic_uScriptAct_SetInt_Target_351;

	private bool logic_uScriptAct_SetInt_Out_351 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_354 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_354;

	private bool logic_uScriptAct_SetBool_Out_354 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_354 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_354 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_357 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_357;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_357 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_357;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_357 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_357 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_357 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_357 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_361 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_361;

	private int logic_uScriptCon_CheckIntEquals_B_361;

	private bool logic_uScriptCon_CheckIntEquals_True_361 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_361 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_363 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_363;

	private Tank logic_uScript_SetTankInvulnerable_tank_363;

	private bool logic_uScript_SetTankInvulnerable_Out_363 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_365 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_365 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_366 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_366;

	private Tank logic_uScript_SetTankInvulnerable_tank_366;

	private bool logic_uScript_SetTankInvulnerable_Out_366 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_367 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_367;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_367 = AITreeType.AITypes.Guard;

	private bool logic_uScript_SetTechAIType_Out_367 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_368 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_368;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_368 = 100f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_368 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_372 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_372;

	private int logic_uScript_PlayDialogue_progress_372;

	private bool logic_uScript_PlayDialogue_Out_372 = true;

	private bool logic_uScript_PlayDialogue_Shown_372 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_372 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_374 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_374 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_374 = 1;

	private bool logic_uScript_SetTechsTeam_Out_374 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_378 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_378;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_378 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_378 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_378;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_378;

	private bool logic_uScript_FlyTechUpAndAway_Out_378 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_380 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_380;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_380 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_380 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_380;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_380;

	private bool logic_uScript_FlyTechUpAndAway_Out_380 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_384 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_384;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_384 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_384 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_384;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_384;

	private bool logic_uScript_FlyTechUpAndAway_Out_384 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_391 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_391;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_391 = 100f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_391 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_392 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_392 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_392 = 1;

	private bool logic_uScript_SetTechsTeam_Out_392 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_393 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_393;

	private Tank logic_uScript_SetTankInvulnerable_tank_393;

	private bool logic_uScript_SetTankInvulnerable_Out_393 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_394 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_394;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_394 = AITreeType.AITypes.Guard;

	private bool logic_uScript_SetTechAIType_Out_394 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_395 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_395;

	private Tank logic_uScript_SetTankInvulnerable_tank_395;

	private bool logic_uScript_SetTankInvulnerable_Out_395 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_396 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_396;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_396 = 100f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_396 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_397 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_397;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_397 = AITreeType.AITypes.Guard;

	private bool logic_uScript_SetTechAIType_Out_397 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_400 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_400;

	private int logic_uScriptAct_SetInt_Target_400;

	private bool logic_uScriptAct_SetInt_Out_400 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_403 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_403;

	private bool logic_uScriptCon_CompareBool_True_403 = true;

	private bool logic_uScriptCon_CompareBool_False_403 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_406 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_406;

	private bool logic_uScriptCon_CompareBool_True_406 = true;

	private bool logic_uScriptCon_CompareBool_False_406 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_409 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_409;

	private bool logic_uScript_FinishEncounter_Out_409 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_411 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_411;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_411;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_411;

	private bool logic_uScript_AddMessage_Out_411 = true;

	private bool logic_uScript_AddMessage_Shown_411 = true;

	private uScript_AllowTechEnergyModuleSimultaneousActivation logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_419 = new uScript_AllowTechEnergyModuleSimultaneousActivation();

	private Tank logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_419;

	private TechSequencer.ChainType logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_419;

	private bool logic_uScript_AllowTechEnergyModuleSimultaneousActivation_Out_419 = true;

	private uScript_SetTechExplodeDetachingBlocks logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_421 = new uScript_SetTechExplodeDetachingBlocks();

	private Tank logic_uScript_SetTechExplodeDetachingBlocks_tech_421;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_421 = true;

	private float logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_421 = 0.1f;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_Out_421 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_422 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_422;

	private BlockTypes logic_uScript_GetTankBlock_blockType_422;

	private TankBlock logic_uScript_GetTankBlock_Return_422;

	private bool logic_uScript_GetTankBlock_Out_422 = true;

	private bool logic_uScript_GetTankBlock_Returned_422 = true;

	private bool logic_uScript_GetTankBlock_NotFound_422 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_425 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_425 = "";

	private bool logic_uScript_SetShieldEnabled_enable_425 = true;

	private bool logic_uScript_SetShieldEnabled_Out_425 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_430 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_430;

	private int logic_uScriptCon_CheckIntEquals_B_430;

	private bool logic_uScriptCon_CheckIntEquals_True_430 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_430 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_432 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_432;

	private BlockTypes logic_uScript_GetTankBlock_blockType_432;

	private TankBlock logic_uScript_GetTankBlock_Return_432;

	private bool logic_uScript_GetTankBlock_Out_432 = true;

	private bool logic_uScript_GetTankBlock_Returned_432 = true;

	private bool logic_uScript_GetTankBlock_NotFound_432 = true;

	private uScript_SetTechExplodeDetachingBlocks logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_435 = new uScript_SetTechExplodeDetachingBlocks();

	private Tank logic_uScript_SetTechExplodeDetachingBlocks_tech_435;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_435 = true;

	private float logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_435 = 0.1f;

	private bool logic_uScript_SetTechExplodeDetachingBlocks_Out_435 = true;

	private uScript_AllowTechEnergyModuleSimultaneousActivation logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_437 = new uScript_AllowTechEnergyModuleSimultaneousActivation();

	private Tank logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_437;

	private TechSequencer.ChainType logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_437;

	private bool logic_uScript_AllowTechEnergyModuleSimultaneousActivation_Out_437 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_440 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_440 = "";

	private bool logic_uScript_SetShieldEnabled_enable_440;

	private bool logic_uScript_SetShieldEnabled_Out_440 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_445 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_445;

	private int logic_uScriptAct_SetInt_Target_445;

	private bool logic_uScriptAct_SetInt_Out_445 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_447 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_447;

	private bool logic_uScriptAct_SetBool_Out_447 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_447 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_447 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_448 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_448;

	private object logic_uScript_SetEncounterTarget_visibleObject_448 = "";

	private bool logic_uScript_SetEncounterTarget_Out_448 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_450 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_450;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_450;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_450;

	private bool logic_uScript_AddMessage_Out_450 = true;

	private bool logic_uScript_AddMessage_Shown_450 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_453 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_453;

	private bool logic_uScriptCon_CompareBool_True_453 = true;

	private bool logic_uScriptCon_CompareBool_False_453 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_456 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_456;

	private bool logic_uScriptAct_SetBool_Out_456 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_456 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_456 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_458;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_458 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_458 = "MidDialogueMsgPlayed";

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_460 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_460;

	private int logic_uScriptCon_CheckIntEquals_B_460;

	private bool logic_uScriptCon_CheckIntEquals_True_460 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_460 = true;

	private uScript_StopMissionTimer logic_uScript_StopMissionTimer_uScript_StopMissionTimer_462 = new uScript_StopMissionTimer();

	private GameObject logic_uScript_StopMissionTimer_owner_462;

	private bool logic_uScript_StopMissionTimer_Out_462 = true;

	private uScript_HideMissionTimerUI logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_463 = new uScript_HideMissionTimerUI();

	private GameObject logic_uScript_HideMissionTimerUI_owner_463;

	private bool logic_uScript_HideMissionTimerUI_Out_463 = true;

	private uScript_StopMissionTimer logic_uScript_StopMissionTimer_uScript_StopMissionTimer_466 = new uScript_StopMissionTimer();

	private GameObject logic_uScript_StopMissionTimer_owner_466;

	private bool logic_uScript_StopMissionTimer_Out_466 = true;

	private uScript_PlayMiscSFX logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_467 = new uScript_PlayMiscSFX();

	private ManSFX.MiscSfxType logic_uScript_PlayMiscSFX_miscSFXType_467 = ManSFX.MiscSfxType.StuntFailed;

	private bool logic_uScript_PlayMiscSFX_Out_467 = true;

	private uScript_HideMissionTimerUI logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_470 = new uScript_HideMissionTimerUI();

	private GameObject logic_uScript_HideMissionTimerUI_owner_470;

	private bool logic_uScript_HideMissionTimerUI_Out_470 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_471 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_471 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_471 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_471;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_471 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_471 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_471 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_471 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_471 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_471 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_471 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_474 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_474;

	private int logic_uScript_PlayDialogue_progress_474;

	private bool logic_uScript_PlayDialogue_Out_474 = true;

	private bool logic_uScript_PlayDialogue_Shown_474 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_474 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_477 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_477;

	private bool logic_uScriptAct_SetBool_Out_477 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_477 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_477 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_478 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_478;

	private bool logic_uScriptCon_CompareBool_True_478 = true;

	private bool logic_uScriptCon_CompareBool_False_478 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_481;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_481 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_481 = "FinalDialoguePlayed";

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_482 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_482;

	private int logic_uScriptAct_SetInt_Target_482;

	private bool logic_uScriptAct_SetInt_Out_482 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_484 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_484 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_484 = -2;

	private bool logic_uScript_SetTechsTeam_Out_484 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_486 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_486;

	private int logic_uScriptAct_SetInt_Target_486;

	private bool logic_uScriptAct_SetInt_Out_486 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_488 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_488;

	private object logic_uScript_SetEncounterTarget_visibleObject_488 = "";

	private bool logic_uScript_SetEncounterTarget_Out_488 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_491 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_491 = true;

	private uScript_SetTechsTeam logic_uScript_SetTechsTeam_uScript_SetTechsTeam_494 = new uScript_SetTechsTeam();

	private Tank[] logic_uScript_SetTechsTeam_techs_494 = new Tank[0];

	private int logic_uScript_SetTechsTeam_team_494 = -2;

	private bool logic_uScript_SetTechsTeam_Out_494 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_495 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_495;

	private object logic_uScript_SetEncounterTarget_visibleObject_495 = "";

	private bool logic_uScript_SetEncounterTarget_Out_495 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_498 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_498;

	private int logic_uScriptAct_SetInt_Target_498;

	private bool logic_uScriptAct_SetInt_Out_498 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_499 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_499;

	private int logic_uScriptCon_CheckIntEquals_B_499;

	private bool logic_uScriptCon_CheckIntEquals_True_499 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_499 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_502 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_502;

	private int logic_uScript_PlayDialogue_progress_502;

	private bool logic_uScript_PlayDialogue_Out_502 = true;

	private bool logic_uScript_PlayDialogue_Shown_502 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_502 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_506 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_506 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_507 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_507 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_507 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_508 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_508 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_508 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_509 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_509 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_509 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_510 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_510 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_510 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_512 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_512 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_512 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_512;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_512 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_512 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_512 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_512 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_512 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_512 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_512 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_514 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_514 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_516 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_516;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_516;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_516;

	private bool logic_uScript_AddMessage_Out_516 = true;

	private bool logic_uScript_AddMessage_Shown_516 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_519 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_519 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_520 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_520 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_524 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_524;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_524;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_524;

	private bool logic_uScript_AddMessage_Out_524 = true;

	private bool logic_uScript_AddMessage_Shown_524 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_525 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_525;

	private int logic_uScript_PlayDialogue_progress_525;

	private bool logic_uScript_PlayDialogue_Out_525 = true;

	private bool logic_uScript_PlayDialogue_Shown_525 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_525 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_531;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_531 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_531 = "FillerMsgPlayed01";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_532;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_532 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_532 = "FillerMsgPlayed02";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_533;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_533 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_533 = "CubeTooEarlyMsgPlayed";

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_534 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_534 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_535 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_535;

	private bool logic_uScriptCon_CompareBool_True_535 = true;

	private bool logic_uScriptCon_CompareBool_False_535 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_537 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_537 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_537 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_538 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_538;

	private bool logic_uScriptCon_CompareBool_True_538 = true;

	private bool logic_uScriptCon_CompareBool_False_538 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_539 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_539 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_539 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_541 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_541;

	private bool logic_uScriptCon_CompareBool_True_541 = true;

	private bool logic_uScriptCon_CompareBool_False_541 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_543 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_543;

	private bool logic_uScriptAct_SetBool_Out_543 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_543 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_543 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_545 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_545;

	private int logic_uScript_PlayDialogue_progress_545;

	private bool logic_uScript_PlayDialogue_Out_545 = true;

	private bool logic_uScript_PlayDialogue_Shown_545 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_545 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_547 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_547;

	private bool logic_uScriptCon_CompareBool_True_547 = true;

	private bool logic_uScriptCon_CompareBool_False_547 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_549 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_549;

	private bool logic_uScriptCon_CompareBool_True_549 = true;

	private bool logic_uScriptCon_CompareBool_False_549 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_551 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_551 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_552 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_552 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_553 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_553;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_558 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_558;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_558 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_558 = "Objective";

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_560 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_560;

	private int logic_uScript_PlayDialogue_progress_560;

	private bool logic_uScript_PlayDialogue_Out_560 = true;

	private bool logic_uScript_PlayDialogue_Shown_560 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_560 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_568 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_568;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_568;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_569 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_569;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_569;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_572 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_572;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_572 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_574 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_574 = 2;

	private int logic_uScriptAct_SetInt_Target_574;

	private bool logic_uScriptAct_SetInt_Out_574 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_575 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_575 = 3;

	private int logic_uScriptAct_SetInt_Target_575;

	private bool logic_uScriptAct_SetInt_Out_575 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_577 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_577;

	private int logic_uScript_PlayDialogue_progress_577;

	private bool logic_uScript_PlayDialogue_Out_577 = true;

	private bool logic_uScript_PlayDialogue_Shown_577 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_577 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_580;

	private int logic_SubGraph_SaveLoadInt_integer_580;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_580 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_580 = "DialogueProgressExtra";

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_582 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_582;

	private int logic_uScriptAct_SetInt_Target_582;

	private bool logic_uScriptAct_SetInt_Out_582 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_586 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_586;

	private int logic_uScript_PlayDialogue_progress_586;

	private bool logic_uScript_PlayDialogue_Out_586 = true;

	private bool logic_uScript_PlayDialogue_Shown_586 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_586 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_589 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_589;

	private int logic_uScript_PlayDialogue_progress_589;

	private bool logic_uScript_PlayDialogue_Out_589 = true;

	private bool logic_uScript_PlayDialogue_Shown_589 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_589 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_592 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_592;

	private int logic_uScriptCon_CheckIntEquals_B_592;

	private bool logic_uScriptCon_CheckIntEquals_True_592 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_592 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_593 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_593 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_596 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_596;

	private int logic_uScriptAct_SetInt_Target_596;

	private bool logic_uScriptAct_SetInt_Out_596 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_598 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_598;

	private int logic_uScriptAct_SetInt_Target_598;

	private bool logic_uScriptAct_SetInt_Out_598 = true;

	private uScriptCon_CheckIntEquals logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_604 = new uScriptCon_CheckIntEquals();

	private int logic_uScriptCon_CheckIntEquals_A_604;

	private int logic_uScriptCon_CheckIntEquals_B_604;

	private bool logic_uScriptCon_CheckIntEquals_True_604 = true;

	private bool logic_uScriptCon_CheckIntEquals_False_604 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_607 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_607;

	private int logic_uScript_PlayDialogue_progress_607;

	private bool logic_uScript_PlayDialogue_Out_607 = true;

	private bool logic_uScript_PlayDialogue_Shown_607 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_607 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_609 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_609 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_610 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_610;

	private int logic_uScriptAct_SetInt_Target_610;

	private bool logic_uScriptAct_SetInt_Out_610 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_611 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_611;

	private int logic_uScriptAct_SetInt_Target_611;

	private bool logic_uScriptAct_SetInt_Out_611 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_612 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_612;

	private bool logic_uScriptCon_CompareBool_True_612 = true;

	private bool logic_uScriptCon_CompareBool_False_612 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_614 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_614;

	private int logic_uScript_PlayDialogue_progress_614;

	private bool logic_uScript_PlayDialogue_Out_614 = true;

	private bool logic_uScript_PlayDialogue_Shown_614 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_614 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_620 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_620;

	private bool logic_uScriptCon_CompareBool_True_620 = true;

	private bool logic_uScriptCon_CompareBool_False_620 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_621 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_621;

	private int logic_uScript_PlayDialogue_progress_621;

	private bool logic_uScript_PlayDialogue_Out_621 = true;

	private bool logic_uScript_PlayDialogue_Shown_621 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_621 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_623 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_623;

	private bool logic_uScriptCon_CompareBool_True_623 = true;

	private bool logic_uScriptCon_CompareBool_False_623 = true;

	private uScript_PlayDialogue logic_uScript_PlayDialogue_uScript_PlayDialogue_624 = new uScript_PlayDialogue();

	private uScript_PlayDialogue.Dialogue logic_uScript_PlayDialogue_dialogue_624;

	private int logic_uScript_PlayDialogue_progress_624;

	private bool logic_uScript_PlayDialogue_Out_624 = true;

	private bool logic_uScript_PlayDialogue_Shown_624 = true;

	private bool logic_uScript_PlayDialogue_BeginMessage_624 = true;

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
		if (null == owner_Connection_6 || !m_RegisteredForEvents)
		{
			owner_Connection_6 = parentGameObject;
			if (null != owner_Connection_6)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_6.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_6.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_3;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_3;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_3;
				}
			}
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
		}
		if (null == owner_Connection_13 || !m_RegisteredForEvents)
		{
			owner_Connection_13 = parentGameObject;
		}
		if (null == owner_Connection_15 || !m_RegisteredForEvents)
		{
			owner_Connection_15 = parentGameObject;
		}
		if (null == owner_Connection_17 || !m_RegisteredForEvents)
		{
			owner_Connection_17 = parentGameObject;
		}
		if (null == owner_Connection_20 || !m_RegisteredForEvents)
		{
			owner_Connection_20 = parentGameObject;
		}
		if (null == owner_Connection_28 || !m_RegisteredForEvents)
		{
			owner_Connection_28 = parentGameObject;
		}
		if (null == owner_Connection_31 || !m_RegisteredForEvents)
		{
			owner_Connection_31 = parentGameObject;
		}
		if (null == owner_Connection_35 || !m_RegisteredForEvents)
		{
			owner_Connection_35 = parentGameObject;
		}
		if (null == owner_Connection_44 || !m_RegisteredForEvents)
		{
			owner_Connection_44 = parentGameObject;
		}
		if (null == owner_Connection_63 || !m_RegisteredForEvents)
		{
			owner_Connection_63 = parentGameObject;
		}
		if (null == owner_Connection_136 || !m_RegisteredForEvents)
		{
			owner_Connection_136 = parentGameObject;
		}
		if (null == owner_Connection_174 || !m_RegisteredForEvents)
		{
			owner_Connection_174 = parentGameObject;
		}
		if (null == owner_Connection_223 || !m_RegisteredForEvents)
		{
			owner_Connection_223 = parentGameObject;
		}
		if (null == owner_Connection_230 || !m_RegisteredForEvents)
		{
			owner_Connection_230 = parentGameObject;
		}
		if (null == owner_Connection_235 || !m_RegisteredForEvents)
		{
			owner_Connection_235 = parentGameObject;
		}
		if (null == owner_Connection_281 || !m_RegisteredForEvents)
		{
			owner_Connection_281 = parentGameObject;
		}
		if (null == owner_Connection_297 || !m_RegisteredForEvents)
		{
			owner_Connection_297 = parentGameObject;
		}
		if (null == owner_Connection_304 || !m_RegisteredForEvents)
		{
			owner_Connection_304 = parentGameObject;
		}
		if (null == owner_Connection_323 || !m_RegisteredForEvents)
		{
			owner_Connection_323 = parentGameObject;
		}
		if (null == owner_Connection_339 || !m_RegisteredForEvents)
		{
			owner_Connection_339 = parentGameObject;
		}
		if (null == owner_Connection_356 || !m_RegisteredForEvents)
		{
			owner_Connection_356 = parentGameObject;
		}
		if (null == owner_Connection_410 || !m_RegisteredForEvents)
		{
			owner_Connection_410 = parentGameObject;
		}
		if (null == owner_Connection_449 || !m_RegisteredForEvents)
		{
			owner_Connection_449 = parentGameObject;
		}
		if (null == owner_Connection_464 || !m_RegisteredForEvents)
		{
			owner_Connection_464 = parentGameObject;
		}
		if (null == owner_Connection_468 || !m_RegisteredForEvents)
		{
			owner_Connection_468 = parentGameObject;
		}
		if (null == owner_Connection_485 || !m_RegisteredForEvents)
		{
			owner_Connection_485 = parentGameObject;
		}
		if (null == owner_Connection_492 || !m_RegisteredForEvents)
		{
			owner_Connection_492 = parentGameObject;
		}
		if (null == owner_Connection_555 || !m_RegisteredForEvents)
		{
			owner_Connection_555 = parentGameObject;
		}
		if (null == owner_Connection_556 || !m_RegisteredForEvents)
		{
			owner_Connection_556 = parentGameObject;
		}
		if (null == owner_Connection_557 || !m_RegisteredForEvents)
		{
			owner_Connection_557 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_6)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_6.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_6.AddComponent<uScript_SaveLoad>();
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
		if (null != owner_Connection_6)
		{
			uScript_SaveLoad component2 = owner_Connection_6.GetComponent<uScript_SaveLoad>();
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
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_12.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_14.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_16.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_18.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_19.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_24.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_25.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_29.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_30.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_36.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_38.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_41.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_45.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_48.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_56.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_59.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_60.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_66.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_68.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_69.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_70.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_74.SetParent(g);
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_76.SetParent(g);
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_79.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_82.SetParent(g);
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_83.SetParent(g);
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_85.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_86.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_89.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_90.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_96.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_99.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_101.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_104.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_106.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_108.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_109.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_113.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_118.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_119.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_125.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_127.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_128.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_131.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_132.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_138.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_141.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_144.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_145.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_150.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_151.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_154.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_161.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_163.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_165.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_167.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_169.SetParent(g);
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_171.SetParent(g);
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_173.SetParent(g);
		logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_175.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_177.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_178.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_181.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_182.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_188.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_192.SetParent(g);
		logic_uScriptAct_PrintText_uScriptAct_PrintText_195.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_196.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_197.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_202.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_208.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_211.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_212.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_214.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_220.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_224.SetParent(g);
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_226.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_227.SetParent(g);
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_229.SetParent(g);
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_231.SetParent(g);
		logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_232.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_234.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_238.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_260.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_264.SetParent(g);
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_266.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_267.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_270.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_273.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_278.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_280.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_283.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_285.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_289.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_292.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_294.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_298.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_299.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_300.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_301.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_305.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_307.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_310.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_317.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_318.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_320.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_321.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_325.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_327.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_328.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_331.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_333.SetParent(g);
		logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_338.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_342.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_345.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_346.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_349.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_350.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_351.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_354.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_361.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_363.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_365.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_366.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_367.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_368.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_372.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_374.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_378.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_380.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_384.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_391.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_392.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_393.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_394.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_395.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_396.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_397.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_400.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_403.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_406.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_409.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_411.SetParent(g);
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_419.SetParent(g);
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_421.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_422.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_425.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_430.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_432.SetParent(g);
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_435.SetParent(g);
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_437.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_440.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_445.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_447.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_448.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_450.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_453.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_456.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_460.SetParent(g);
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_462.SetParent(g);
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_463.SetParent(g);
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_466.SetParent(g);
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_467.SetParent(g);
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_470.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_471.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_474.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_477.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_478.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_482.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_484.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_486.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_488.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_491.SetParent(g);
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_494.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_495.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_498.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_499.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_502.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_506.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_507.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_508.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_509.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_510.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_512.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_514.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_516.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_519.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_520.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_524.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_525.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_534.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_535.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_537.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_538.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_539.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_541.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_543.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_545.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_547.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_549.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_551.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_552.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_553.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_560.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_568.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_569.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_572.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_574.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_575.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_577.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_582.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_586.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_589.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_592.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_593.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_596.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_598.SetParent(g);
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_604.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_607.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_609.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_610.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_611.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_612.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_614.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_620.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_621.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_623.SetParent(g);
		logic_uScript_PlayDialogue_uScript_PlayDialogue_624.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_6 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_13 = parentGameObject;
		owner_Connection_15 = parentGameObject;
		owner_Connection_17 = parentGameObject;
		owner_Connection_20 = parentGameObject;
		owner_Connection_28 = parentGameObject;
		owner_Connection_31 = parentGameObject;
		owner_Connection_35 = parentGameObject;
		owner_Connection_44 = parentGameObject;
		owner_Connection_63 = parentGameObject;
		owner_Connection_136 = parentGameObject;
		owner_Connection_174 = parentGameObject;
		owner_Connection_223 = parentGameObject;
		owner_Connection_230 = parentGameObject;
		owner_Connection_235 = parentGameObject;
		owner_Connection_281 = parentGameObject;
		owner_Connection_297 = parentGameObject;
		owner_Connection_304 = parentGameObject;
		owner_Connection_323 = parentGameObject;
		owner_Connection_339 = parentGameObject;
		owner_Connection_356 = parentGameObject;
		owner_Connection_410 = parentGameObject;
		owner_Connection_449 = parentGameObject;
		owner_Connection_464 = parentGameObject;
		owner_Connection_468 = parentGameObject;
		owner_Connection_485 = parentGameObject;
		owner_Connection_492 = parentGameObject;
		owner_Connection_555 = parentGameObject;
		owner_Connection_556 = parentGameObject;
		owner_Connection_557 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_553.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_568.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_569.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_572.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5.Save_Out += SubGraph_SaveLoadInt_Save_Out_5;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5.Load_Out += SubGraph_SaveLoadInt_Load_Out_5;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_5;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157.Save_Out += SubGraph_SaveLoadInt_Save_Out_157;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157.Load_Out += SubGraph_SaveLoadInt_Load_Out_157;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_157;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.Save_Out += SubGraph_SaveLoadBool_Save_Out_159;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.Load_Out += SubGraph_SaveLoadBool_Load_Out_159;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_159;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Save_Out += SubGraph_SaveLoadInt_Save_Out_185;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Load_Out += SubGraph_SaveLoadInt_Load_Out_185;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_185;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output1 += uScriptCon_BigManualSwitch_Output1_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output2 += uScriptCon_BigManualSwitch_Output2_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output3 += uScriptCon_BigManualSwitch_Output3_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output4 += uScriptCon_BigManualSwitch_Output4_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output5 += uScriptCon_BigManualSwitch_Output5_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output6 += uScriptCon_BigManualSwitch_Output6_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output7 += uScriptCon_BigManualSwitch_Output7_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output8 += uScriptCon_BigManualSwitch_Output8_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output9 += uScriptCon_BigManualSwitch_Output9_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output10 += uScriptCon_BigManualSwitch_Output10_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output11 += uScriptCon_BigManualSwitch_Output11_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output12 += uScriptCon_BigManualSwitch_Output12_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output13 += uScriptCon_BigManualSwitch_Output13_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output14 += uScriptCon_BigManualSwitch_Output14_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output15 += uScriptCon_BigManualSwitch_Output15_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output16 += uScriptCon_BigManualSwitch_Output16_265;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Save_Out += SubGraph_SaveLoadBool_Save_Out_458;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Load_Out += SubGraph_SaveLoadBool_Load_Out_458;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_458;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.Save_Out += SubGraph_SaveLoadBool_Save_Out_481;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.Load_Out += SubGraph_SaveLoadBool_Load_Out_481;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_481;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Save_Out += SubGraph_SaveLoadBool_Save_Out_531;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Load_Out += SubGraph_SaveLoadBool_Load_Out_531;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_531;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.Save_Out += SubGraph_SaveLoadBool_Save_Out_532;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.Load_Out += SubGraph_SaveLoadBool_Load_Out_532;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_532;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.Save_Out += SubGraph_SaveLoadBool_Save_Out_533;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.Load_Out += SubGraph_SaveLoadBool_Load_Out_533;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_533;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_553.Out += SubGraph_LoadObjectiveStates_Out_553;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558.Save_Out += SubGraph_SaveLoadInt_Save_Out_558;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558.Load_Out += SubGraph_SaveLoadInt_Load_Out_558;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_558;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_568.Out += SubGraph_CompleteObjectiveStage_Out_568;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_569.Out += SubGraph_CompleteObjectiveStage_Out_569;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_572.Out += SubGraph_CompleteObjectiveStage_Out_572;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580.Save_Out += SubGraph_SaveLoadInt_Save_Out_580;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580.Load_Out += SubGraph_SaveLoadInt_Load_Out_580;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_580;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_553.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_568.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_569.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_572.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_18.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_144.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_182.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_285.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_289.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_346.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_372.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_378.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_380.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_384.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_474.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_502.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_525.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_545.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_553.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_560.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_568.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_569.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_572.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_577.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_586.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_589.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_607.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_614.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_621.OnEnable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_624.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_74.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_82.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_89.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_106.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_108.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_113.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_141.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_144.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_182.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_192.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_285.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_289.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_301.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_318.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_346.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_363.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_366.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_372.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_393.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_395.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_411.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_422.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_432.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_450.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_474.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_502.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_507.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_508.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_509.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_510.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_516.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_524.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_525.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_537.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_539.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_545.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_553.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_560.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_568.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_569.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_572.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_577.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_586.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_589.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_607.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_614.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_621.OnDisable();
		logic_uScript_PlayDialogue_uScript_PlayDialogue_624.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_553.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_568.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_569.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_572.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_553.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_568.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_569.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_572.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5.Save_Out -= SubGraph_SaveLoadInt_Save_Out_5;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5.Load_Out -= SubGraph_SaveLoadInt_Load_Out_5;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_5;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157.Save_Out -= SubGraph_SaveLoadInt_Save_Out_157;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157.Load_Out -= SubGraph_SaveLoadInt_Load_Out_157;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_157;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.Save_Out -= SubGraph_SaveLoadBool_Save_Out_159;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.Load_Out -= SubGraph_SaveLoadBool_Load_Out_159;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_159;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Save_Out -= SubGraph_SaveLoadInt_Save_Out_185;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Load_Out -= SubGraph_SaveLoadInt_Load_Out_185;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_185;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output1 -= uScriptCon_BigManualSwitch_Output1_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output2 -= uScriptCon_BigManualSwitch_Output2_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output3 -= uScriptCon_BigManualSwitch_Output3_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output4 -= uScriptCon_BigManualSwitch_Output4_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output5 -= uScriptCon_BigManualSwitch_Output5_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output6 -= uScriptCon_BigManualSwitch_Output6_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output7 -= uScriptCon_BigManualSwitch_Output7_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output8 -= uScriptCon_BigManualSwitch_Output8_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output9 -= uScriptCon_BigManualSwitch_Output9_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output10 -= uScriptCon_BigManualSwitch_Output10_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output11 -= uScriptCon_BigManualSwitch_Output11_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output12 -= uScriptCon_BigManualSwitch_Output12_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output13 -= uScriptCon_BigManualSwitch_Output13_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output14 -= uScriptCon_BigManualSwitch_Output14_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output15 -= uScriptCon_BigManualSwitch_Output15_265;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.Output16 -= uScriptCon_BigManualSwitch_Output16_265;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Save_Out -= SubGraph_SaveLoadBool_Save_Out_458;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Load_Out -= SubGraph_SaveLoadBool_Load_Out_458;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_458;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.Save_Out -= SubGraph_SaveLoadBool_Save_Out_481;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.Load_Out -= SubGraph_SaveLoadBool_Load_Out_481;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_481;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Save_Out -= SubGraph_SaveLoadBool_Save_Out_531;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Load_Out -= SubGraph_SaveLoadBool_Load_Out_531;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_531;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.Save_Out -= SubGraph_SaveLoadBool_Save_Out_532;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.Load_Out -= SubGraph_SaveLoadBool_Load_Out_532;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_532;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.Save_Out -= SubGraph_SaveLoadBool_Save_Out_533;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.Load_Out -= SubGraph_SaveLoadBool_Load_Out_533;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_533;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_553.Out -= SubGraph_LoadObjectiveStates_Out_553;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558.Save_Out -= SubGraph_SaveLoadInt_Save_Out_558;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558.Load_Out -= SubGraph_SaveLoadInt_Load_Out_558;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_558;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_568.Out -= SubGraph_CompleteObjectiveStage_Out_568;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_569.Out -= SubGraph_CompleteObjectiveStage_Out_569;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_572.Out -= SubGraph_CompleteObjectiveStage_Out_572;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580.Save_Out -= SubGraph_SaveLoadInt_Save_Out_580;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580.Load_Out -= SubGraph_SaveLoadInt_Load_Out_580;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_580;
	}

	public void OnGUI()
	{
		logic_uScriptAct_PrintText_uScriptAct_PrintText_195.OnGUI();
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

	private void SubGraph_SaveLoadInt_Save_Out_5(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_5 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_5;
		Relay_Save_Out_5();
	}

	private void SubGraph_SaveLoadInt_Load_Out_5(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_5 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_5;
		Relay_Load_Out_5();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_5(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_5 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_5;
		Relay_Restart_Out_5();
	}

	private void SubGraph_SaveLoadInt_Save_Out_157(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_157 = e.integer;
		local_DialogueProgress_System_Int32 = logic_SubGraph_SaveLoadInt_integer_157;
		Relay_Save_Out_157();
	}

	private void SubGraph_SaveLoadInt_Load_Out_157(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_157 = e.integer;
		local_DialogueProgress_System_Int32 = logic_SubGraph_SaveLoadInt_integer_157;
		Relay_Load_Out_157();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_157(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_157 = e.integer;
		local_DialogueProgress_System_Int32 = logic_SubGraph_SaveLoadInt_integer_157;
		Relay_Restart_Out_157();
	}

	private void SubGraph_SaveLoadBool_Save_Out_159(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_159 = e.boolean;
		local_InRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_159;
		Relay_Save_Out_159();
	}

	private void SubGraph_SaveLoadBool_Load_Out_159(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_159 = e.boolean;
		local_InRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_159;
		Relay_Load_Out_159();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_159(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_159 = e.boolean;
		local_InRange_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_159;
		Relay_Restart_Out_159();
	}

	private void SubGraph_SaveLoadInt_Save_Out_185(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_185 = e.integer;
		local_Round_System_Int32 = logic_SubGraph_SaveLoadInt_integer_185;
		Relay_Save_Out_185();
	}

	private void SubGraph_SaveLoadInt_Load_Out_185(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_185 = e.integer;
		local_Round_System_Int32 = logic_SubGraph_SaveLoadInt_integer_185;
		Relay_Load_Out_185();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_185(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_185 = e.integer;
		local_Round_System_Int32 = logic_SubGraph_SaveLoadInt_integer_185;
		Relay_Restart_Out_185();
	}

	private void uScriptCon_BigManualSwitch_Output1_265(object o, EventArgs e)
	{
		Relay_Output1_265();
	}

	private void uScriptCon_BigManualSwitch_Output2_265(object o, EventArgs e)
	{
		Relay_Output2_265();
	}

	private void uScriptCon_BigManualSwitch_Output3_265(object o, EventArgs e)
	{
		Relay_Output3_265();
	}

	private void uScriptCon_BigManualSwitch_Output4_265(object o, EventArgs e)
	{
		Relay_Output4_265();
	}

	private void uScriptCon_BigManualSwitch_Output5_265(object o, EventArgs e)
	{
		Relay_Output5_265();
	}

	private void uScriptCon_BigManualSwitch_Output6_265(object o, EventArgs e)
	{
		Relay_Output6_265();
	}

	private void uScriptCon_BigManualSwitch_Output7_265(object o, EventArgs e)
	{
		Relay_Output7_265();
	}

	private void uScriptCon_BigManualSwitch_Output8_265(object o, EventArgs e)
	{
		Relay_Output8_265();
	}

	private void uScriptCon_BigManualSwitch_Output9_265(object o, EventArgs e)
	{
		Relay_Output9_265();
	}

	private void uScriptCon_BigManualSwitch_Output10_265(object o, EventArgs e)
	{
		Relay_Output10_265();
	}

	private void uScriptCon_BigManualSwitch_Output11_265(object o, EventArgs e)
	{
		Relay_Output11_265();
	}

	private void uScriptCon_BigManualSwitch_Output12_265(object o, EventArgs e)
	{
		Relay_Output12_265();
	}

	private void uScriptCon_BigManualSwitch_Output13_265(object o, EventArgs e)
	{
		Relay_Output13_265();
	}

	private void uScriptCon_BigManualSwitch_Output14_265(object o, EventArgs e)
	{
		Relay_Output14_265();
	}

	private void uScriptCon_BigManualSwitch_Output15_265(object o, EventArgs e)
	{
		Relay_Output15_265();
	}

	private void uScriptCon_BigManualSwitch_Output16_265(object o, EventArgs e)
	{
		Relay_Output16_265();
	}

	private void SubGraph_SaveLoadBool_Save_Out_458(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_458 = e.boolean;
		local_MidDialogueMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_458;
		Relay_Save_Out_458();
	}

	private void SubGraph_SaveLoadBool_Load_Out_458(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_458 = e.boolean;
		local_MidDialogueMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_458;
		Relay_Load_Out_458();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_458(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_458 = e.boolean;
		local_MidDialogueMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_458;
		Relay_Restart_Out_458();
	}

	private void SubGraph_SaveLoadBool_Save_Out_481(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_481 = e.boolean;
		local_FinalDialoguePlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_481;
		Relay_Save_Out_481();
	}

	private void SubGraph_SaveLoadBool_Load_Out_481(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_481 = e.boolean;
		local_FinalDialoguePlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_481;
		Relay_Load_Out_481();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_481(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_481 = e.boolean;
		local_FinalDialoguePlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_481;
		Relay_Restart_Out_481();
	}

	private void SubGraph_SaveLoadBool_Save_Out_531(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_531 = e.boolean;
		local_FillerMsgPlayed01_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_531;
		Relay_Save_Out_531();
	}

	private void SubGraph_SaveLoadBool_Load_Out_531(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_531 = e.boolean;
		local_FillerMsgPlayed01_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_531;
		Relay_Load_Out_531();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_531(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_531 = e.boolean;
		local_FillerMsgPlayed01_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_531;
		Relay_Restart_Out_531();
	}

	private void SubGraph_SaveLoadBool_Save_Out_532(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_532 = e.boolean;
		local_FillerMsgPlayed02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_532;
		Relay_Save_Out_532();
	}

	private void SubGraph_SaveLoadBool_Load_Out_532(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_532 = e.boolean;
		local_FillerMsgPlayed02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_532;
		Relay_Load_Out_532();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_532(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_532 = e.boolean;
		local_FillerMsgPlayed02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_532;
		Relay_Restart_Out_532();
	}

	private void SubGraph_SaveLoadBool_Save_Out_533(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_533 = e.boolean;
		local_CubeTooEarlyMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_533;
		Relay_Save_Out_533();
	}

	private void SubGraph_SaveLoadBool_Load_Out_533(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_533 = e.boolean;
		local_CubeTooEarlyMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_533;
		Relay_Load_Out_533();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_533(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_533 = e.boolean;
		local_CubeTooEarlyMsgPlayed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_533;
		Relay_Restart_Out_533();
	}

	private void SubGraph_LoadObjectiveStates_Out_553(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_553();
	}

	private void SubGraph_SaveLoadInt_Save_Out_558(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_558 = e.integer;
		local_Objective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_558;
		Relay_Save_Out_558();
	}

	private void SubGraph_SaveLoadInt_Load_Out_558(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_558 = e.integer;
		local_Objective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_558;
		Relay_Load_Out_558();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_558(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_558 = e.integer;
		local_Objective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_558;
		Relay_Restart_Out_558();
	}

	private void SubGraph_CompleteObjectiveStage_Out_568(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_568 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_568;
		Relay_Out_568();
	}

	private void SubGraph_CompleteObjectiveStage_Out_569(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_569 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_569;
		Relay_Out_569();
	}

	private void SubGraph_CompleteObjectiveStage_Out_572(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_572 = e.objectiveStage;
		local_Objective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_572;
		Relay_Out_572();
	}

	private void SubGraph_SaveLoadInt_Save_Out_580(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_580 = e.integer;
		local_DialogueProgressExtra_System_Int32 = logic_SubGraph_SaveLoadInt_integer_580;
		Relay_Save_Out_580();
	}

	private void SubGraph_SaveLoadInt_Load_Out_580(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_580 = e.integer;
		local_DialogueProgressExtra_System_Int32 = logic_SubGraph_SaveLoadInt_integer_580;
		Relay_Load_Out_580();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_580(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_580 = e.integer;
		local_DialogueProgressExtra_System_Int32 = logic_SubGraph_SaveLoadInt_integer_580;
		Relay_Restart_Out_580();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_45();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_SaveEvent_3()
	{
		Relay_Save_5();
	}

	private void Relay_LoadEvent_3()
	{
		Relay_Load_5();
	}

	private void Relay_RestartEvent_3()
	{
		Relay_Restart_5();
	}

	private void Relay_Save_Out_5()
	{
		Relay_Save_185();
	}

	private void Relay_Load_Out_5()
	{
		Relay_Load_185();
	}

	private void Relay_Restart_Out_5()
	{
		Relay_Restart_185();
	}

	private void Relay_Save_5()
	{
		logic_SubGraph_SaveLoadInt_restartValue_5 = local_StageStart_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_5 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_5 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5.Save(logic_SubGraph_SaveLoadInt_restartValue_5, ref logic_SubGraph_SaveLoadInt_integer_5, logic_SubGraph_SaveLoadInt_intAsVariable_5, logic_SubGraph_SaveLoadInt_uniqueID_5);
	}

	private void Relay_Load_5()
	{
		logic_SubGraph_SaveLoadInt_restartValue_5 = local_StageStart_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_5 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_5 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5.Load(logic_SubGraph_SaveLoadInt_restartValue_5, ref logic_SubGraph_SaveLoadInt_integer_5, logic_SubGraph_SaveLoadInt_intAsVariable_5, logic_SubGraph_SaveLoadInt_uniqueID_5);
	}

	private void Relay_Restart_5()
	{
		logic_SubGraph_SaveLoadInt_restartValue_5 = local_StageStart_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_5 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_5 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_5.Restart(logic_SubGraph_SaveLoadInt_restartValue_5, ref logic_SubGraph_SaveLoadInt_integer_5, logic_SubGraph_SaveLoadInt_intAsVariable_5, logic_SubGraph_SaveLoadInt_uniqueID_5);
	}

	private void Relay_InitialSpawn_12()
	{
		int num = 0;
		Array crazedMinionTechData = CrazedMinionTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_12.Length != num + crazedMinionTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_12, num + crazedMinionTechData.Length);
		}
		Array.Copy(crazedMinionTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_12, num, crazedMinionTechData.Length);
		num += crazedMinionTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_12 = owner_Connection_17;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_12.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_12, logic_uScript_SpawnTechsFromData_ownerNode_12, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_12, logic_uScript_SpawnTechsFromData_allowResurrection_12);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_12.Out)
		{
			Relay_InitialSpawn_16();
		}
	}

	private void Relay_InitialSpawn_14()
	{
		int num = 0;
		Array crazedTechData = CrazedTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_14.Length != num + crazedTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_14, num + crazedTechData.Length);
		}
		Array.Copy(crazedTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_14, num, crazedTechData.Length);
		num += crazedTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_14 = owner_Connection_11;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_14.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_14, logic_uScript_SpawnTechsFromData_ownerNode_14, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_14, logic_uScript_SpawnTechsFromData_allowResurrection_14);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_14.Out)
		{
			Relay_InitialSpawn_12();
		}
	}

	private void Relay_InitialSpawn_16()
	{
		int num = 0;
		Array fillerTechData = FillerTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_16.Length != num + fillerTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_16, num + fillerTechData.Length);
		}
		Array.Copy(fillerTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_16, num, fillerTechData.Length);
		num += fillerTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_16 = owner_Connection_15;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_16.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_16, logic_uScript_SpawnTechsFromData_ownerNode_16, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_16, logic_uScript_SpawnTechsFromData_allowResurrection_16);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_16.Out)
		{
			Relay_InitialSpawn_19();
		}
	}

	private void Relay_In_18()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_18 = owner_Connection_13;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_18.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_18);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_18.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_InitialSpawn_19()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_19.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_19, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_19, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_19 = owner_Connection_20;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_19.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_19, logic_uScript_SpawnTechsFromData_ownerNode_19, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_19, logic_uScript_SpawnTechsFromData_allowResurrection_19);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_19.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_AtIndex_24()
	{
		int num = 0;
		Array array = local_CrazedMinionTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_24.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_24, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_24, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_24.AtIndex(ref logic_uScript_AccessListTech_techList_24, logic_uScript_AccessListTech_index_24, out logic_uScript_AccessListTech_value_24);
		local_CrazedMinionTechs_TankArray = logic_uScript_AccessListTech_techList_24;
		local_CrazedMinion2_Tank = logic_uScript_AccessListTech_value_24;
	}

	private void Relay_In_25()
	{
		logic_uScript_SetEncounterTarget_owner_25 = owner_Connection_28;
		logic_uScript_SetEncounterTarget_visibleObject_25 = local_CrazedTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_25.In(logic_uScript_SetEncounterTarget_owner_25, logic_uScript_SetEncounterTarget_visibleObject_25);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_25.Out)
		{
			Relay_In_101();
		}
	}

	private void Relay_AtIndex_29()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_29.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_29, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_29, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_29.AtIndex(ref logic_uScript_AccessListTech_techList_29, logic_uScript_AccessListTech_index_29, out logic_uScript_AccessListTech_value_29);
		local_CrazedTechs_TankArray = logic_uScript_AccessListTech_techList_29;
		local_CrazedTech_Tank = logic_uScript_AccessListTech_value_29;
	}

	private void Relay_In_30()
	{
		int num = 0;
		Array crazedMinionTechData = CrazedMinionTechData;
		if (logic_uScript_GetAndCheckTechs_techData_30.Length != num + crazedMinionTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_30, num + crazedMinionTechData.Length);
		}
		Array.Copy(crazedMinionTechData, 0, logic_uScript_GetAndCheckTechs_techData_30, num, crazedMinionTechData.Length);
		num += crazedMinionTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_30 = owner_Connection_31;
		int num2 = 0;
		Array array = local_CrazedMinionTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_30.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_30, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_30, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_30 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_30.In(logic_uScript_GetAndCheckTechs_techData_30, logic_uScript_GetAndCheckTechs_ownerNode_30, ref logic_uScript_GetAndCheckTechs_techs_30);
		local_CrazedMinionTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_30;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_30.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_30.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_30.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_30.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_163();
		}
		if (someAlive)
		{
			Relay_False_163();
		}
		if (allDead)
		{
			Relay_False_163();
		}
		if (waitingToSpawn)
		{
			Relay_False_163();
		}
	}

	private void Relay_In_36()
	{
		int num = 0;
		Array crazedTechData = CrazedTechData;
		if (logic_uScript_GetAndCheckTechs_techData_36.Length != num + crazedTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_36, num + crazedTechData.Length);
		}
		Array.Copy(crazedTechData, 0, logic_uScript_GetAndCheckTechs_techData_36, num, crazedTechData.Length);
		num += crazedTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_36 = owner_Connection_35;
		int num2 = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_36.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_36, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_36, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_36 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_36.In(logic_uScript_GetAndCheckTechs_techData_36, logic_uScript_GetAndCheckTechs_ownerNode_36, ref logic_uScript_GetAndCheckTechs_techs_36);
		local_CrazedTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_36;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_36.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_36.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_36.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_36.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_41();
		}
		if (someAlive)
		{
			Relay_True_41();
		}
		if (allDead)
		{
			Relay_False_41();
		}
		if (waitingToSpawn)
		{
			Relay_False_41();
		}
	}

	private void Relay_AtIndex_38()
	{
		int num = 0;
		Array array = local_CrazedMinionTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_38.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_38, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_38, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_38.AtIndex(ref logic_uScript_AccessListTech_techList_38, logic_uScript_AccessListTech_index_38, out logic_uScript_AccessListTech_value_38);
		local_CrazedMinionTechs_TankArray = logic_uScript_AccessListTech_techList_38;
		local_CrazedMinion1_Tank = logic_uScript_AccessListTech_value_38;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_38.Out)
		{
			Relay_AtIndex_24();
		}
	}

	private void Relay_True_41()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_41.True(out logic_uScriptAct_SetBool_Target_41);
		local_CrazedTechAlive_System_Boolean = logic_uScriptAct_SetBool_Target_41;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_41.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_41.SetTrue;
		if (num)
		{
			Relay_In_30();
		}
		if (setTrue)
		{
			Relay_AtIndex_29();
		}
	}

	private void Relay_False_41()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_41.False(out logic_uScriptAct_SetBool_Target_41);
		local_CrazedTechAlive_System_Boolean = logic_uScriptAct_SetBool_Target_41;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_41.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_41.SetTrue;
		if (num)
		{
			Relay_In_30();
		}
		if (setTrue)
		{
			Relay_AtIndex_29();
		}
	}

	private void Relay_In_45()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_GetAndCheckTechs_techData_45.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_45, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_GetAndCheckTechs_techData_45, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_45 = owner_Connection_44;
		int num2 = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_45.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_45, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_45, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_45 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_45.In(logic_uScript_GetAndCheckTechs_techData_45, logic_uScript_GetAndCheckTechs_ownerNode_45, ref logic_uScript_GetAndCheckTechs_techs_45);
		local_CubeTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_45;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_45.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_45.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_45.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_45.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_49();
		}
		if (someAlive)
		{
			Relay_True_49();
		}
		if (allDead)
		{
			Relay_False_49();
		}
		if (waitingToSpawn)
		{
			Relay_False_49();
		}
	}

	private void Relay_AtIndex_48()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_48.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_48, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_48, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_48.AtIndex(ref logic_uScript_AccessListTech_techList_48, logic_uScript_AccessListTech_index_48, out logic_uScript_AccessListTech_value_48);
		local_CubeTechs_TankArray = logic_uScript_AccessListTech_techList_48;
		local_CubeTech_Tank = logic_uScript_AccessListTech_value_48;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_48.Out)
		{
			Relay_In_430();
		}
	}

	private void Relay_True_49()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.True(out logic_uScriptAct_SetBool_Target_49);
		local_CubeAlive_System_Boolean = logic_uScriptAct_SetBool_Target_49;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_49.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_49.SetTrue;
		if (num)
		{
			Relay_In_36();
		}
		if (setTrue)
		{
			Relay_AtIndex_48();
		}
	}

	private void Relay_False_49()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.False(out logic_uScriptAct_SetBool_Target_49);
		local_CubeAlive_System_Boolean = logic_uScriptAct_SetBool_Target_49;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_49.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_49.SetTrue;
		if (num)
		{
			Relay_In_36();
		}
		if (setTrue)
		{
			Relay_AtIndex_48();
		}
	}

	private void Relay_AtIndex_56()
	{
		int num = 0;
		Array array = local_FillerTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_56.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_56, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_56, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_56.AtIndex(ref logic_uScript_AccessListTech_techList_56, logic_uScript_AccessListTech_index_56, out logic_uScript_AccessListTech_value_56);
		local_FillerTechs_TankArray = logic_uScript_AccessListTech_techList_56;
		local_FillerTech02_Tank = logic_uScript_AccessListTech_value_56;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_56.Out)
		{
			Relay_AtIndex_66();
		}
	}

	private void Relay_AtIndex_59()
	{
		int num = 0;
		Array array = local_FillerTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_59.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_59, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_59, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_59.AtIndex(ref logic_uScript_AccessListTech_techList_59, logic_uScript_AccessListTech_index_59, out logic_uScript_AccessListTech_value_59);
		local_FillerTechs_TankArray = logic_uScript_AccessListTech_techList_59;
		local_FillerTech01_Tank = logic_uScript_AccessListTech_value_59;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_59.Out)
		{
			Relay_AtIndex_56();
		}
	}

	private void Relay_In_60()
	{
		int num = 0;
		Array fillerTechData = FillerTechData;
		if (logic_uScript_GetAndCheckTechs_techData_60.Length != num + fillerTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_60, num + fillerTechData.Length);
		}
		Array.Copy(fillerTechData, 0, logic_uScript_GetAndCheckTechs_techData_60, num, fillerTechData.Length);
		num += fillerTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_60 = owner_Connection_63;
		int num2 = 0;
		Array array = local_FillerTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_60.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_60, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_60, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_60 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_60.In(logic_uScript_GetAndCheckTechs_techData_60, logic_uScript_GetAndCheckTechs_ownerNode_60, ref logic_uScript_GetAndCheckTechs_techs_60);
		local_FillerTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_60;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_60.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_60.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_60.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_60.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_165();
		}
		if (someAlive)
		{
			Relay_False_165();
		}
		if (allDead)
		{
			Relay_False_165();
		}
		if (waitingToSpawn)
		{
			Relay_False_165();
		}
	}

	private void Relay_AtIndex_66()
	{
		int num = 0;
		Array array = local_FillerTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_66.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_66, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_66, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_66.AtIndex(ref logic_uScript_AccessListTech_techList_66, logic_uScript_AccessListTech_index_66, out logic_uScript_AccessListTech_value_66);
		local_FillerTechs_TankArray = logic_uScript_AccessListTech_techList_66;
		local_FillerTech03_Tank = logic_uScript_AccessListTech_value_66;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_66.Out)
		{
			Relay_In_357();
		}
	}

	private void Relay_In_68()
	{
		logic_uScriptAct_SetInt_Value_68 = local_StageConfigureTechs_System_Int32;
		logic_uScriptAct_SetInt_uScriptAct_SetInt_68.In(logic_uScriptAct_SetInt_Value_68, out logic_uScriptAct_SetInt_Target_68);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_68;
	}

	private void Relay_In_69()
	{
		logic_uScriptCon_CompareBool_Bool_69 = local_CubeAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_69.In(logic_uScriptCon_CompareBool_Bool_69);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_69.True)
		{
			Relay_In_70();
		}
	}

	private void Relay_In_70()
	{
		logic_uScriptCon_CompareBool_Bool_70 = local_CrazedTechAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_70.In(logic_uScriptCon_CompareBool_Bool_70);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_70.True)
		{
			Relay_In_167();
		}
	}

	private void Relay_In_74()
	{
		logic_uScript_SetTankInvulnerable_tank_74 = local_CrazedTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_74.In(logic_uScript_SetTankInvulnerable_invulnerable_74, logic_uScript_SetTankInvulnerable_tank_74);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_74.Out)
		{
			Relay_In_25();
		}
	}

	private void Relay_In_76()
	{
		logic_uScript_SetTechExplodeDetachingBlocks_tech_76 = local_CrazedMinion1_Tank;
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_76.In(logic_uScript_SetTechExplodeDetachingBlocks_tech_76, logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_76, logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_76);
		if (logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_76.Out)
		{
			Relay_In_79();
		}
	}

	private void Relay_In_79()
	{
		logic_uScript_SetTechExplodeDetachingBlocks_tech_79 = local_CrazedMinion2_Tank;
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_79.In(logic_uScript_SetTechExplodeDetachingBlocks_tech_79, logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_79, logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_79);
	}

	private void Relay_In_82()
	{
		logic_uScript_GetTankBlock_tank_82 = local_CubeTech_Tank;
		logic_uScript_GetTankBlock_blockType_82 = CubeShieldBlockData;
		logic_uScript_GetTankBlock_Return_82 = logic_uScript_GetTankBlock_uScript_GetTankBlock_82.In(logic_uScript_GetTankBlock_tank_82, logic_uScript_GetTankBlock_blockType_82);
		local_CubeShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_82;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_82.Returned)
		{
			Relay_In_90();
		}
	}

	private void Relay_In_83()
	{
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_83 = local_CubeTech_Tank;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_83 = local_84_TechSequencer_ChainType;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_83.In(logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_83, logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_83);
		if (logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_83.Out)
		{
			Relay_In_82();
		}
	}

	private void Relay_In_85()
	{
		logic_uScript_SetTechExplodeDetachingBlocks_tech_85 = local_CubeTech_Tank;
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_85.In(logic_uScript_SetTechExplodeDetachingBlocks_tech_85, logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_85, logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_85);
	}

	private void Relay_In_86()
	{
		logic_uScript_SetBatteryChargeAmount_tech_86 = local_CubeTech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_86.In(logic_uScript_SetBatteryChargeAmount_tech_86, logic_uScript_SetBatteryChargeAmount_chargeAmount_86);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_86.Out)
		{
			Relay_In_83();
		}
	}

	private void Relay_In_89()
	{
		logic_uScript_SetTankInvulnerable_tank_89 = local_CubeTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_89.In(logic_uScript_SetTankInvulnerable_invulnerable_89, logic_uScript_SetTankInvulnerable_tank_89);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_89.Out)
		{
			Relay_In_86();
		}
	}

	private void Relay_In_90()
	{
		logic_uScript_SetShieldEnabled_targetObject_90 = local_CubeShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_90.In(logic_uScript_SetShieldEnabled_targetObject_90, logic_uScript_SetShieldEnabled_enable_90);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_90.Out)
		{
			Relay_In_74();
		}
	}

	private void Relay_In_96()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_96.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_96, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_96, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_96.In(ref logic_uScript_SetTechsTeam_techs_96, logic_uScript_SetTechsTeam_team_96);
		local_CubeTechs_TankArray = logic_uScript_SetTechsTeam_techs_96;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_96.Out)
		{
			Relay_In_361();
		}
	}

	private void Relay_In_99()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_99 = LeaderIntroStartTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_99.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_99);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_99.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_99.OutOfRange;
		if (inRange)
		{
			Relay_In_132();
		}
		if (outOfRange)
		{
			Relay_In_507();
		}
	}

	private void Relay_In_101()
	{
		logic_uScriptAct_SetInt_Value_101 = local_StageApproachLeader_System_Int32;
		logic_uScriptAct_SetInt_uScriptAct_SetInt_101.In(logic_uScriptAct_SetInt_Value_101, out logic_uScriptAct_SetInt_Target_101);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_101;
	}

	private void Relay_In_104()
	{
		logic_uScriptAct_SetInt_Value_104 = local_StageTalkToLeader_System_Int32;
		logic_uScriptAct_SetInt_uScriptAct_SetInt_104.In(logic_uScriptAct_SetInt_Value_104, out logic_uScriptAct_SetInt_Target_104);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_104;
	}

	private void Relay_In_106()
	{
		logic_uScript_AddMessage_messageData_106 = MsgFillerNPC02;
		logic_uScript_AddMessage_speaker_106 = SingleMinionTechSpeaker;
		logic_uScript_AddMessage_Return_106 = logic_uScript_AddMessage_uScript_AddMessage_106.In(logic_uScript_AddMessage_messageData_106, logic_uScript_AddMessage_speaker_106);
		if (logic_uScript_AddMessage_uScript_AddMessage_106.Out)
		{
			Relay_True_112();
		}
	}

	private void Relay_In_108()
	{
		logic_uScript_AddMessage_messageData_108 = msgCubeTooEarly;
		logic_uScript_AddMessage_speaker_108 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_108 = logic_uScript_AddMessage_uScript_AddMessage_108.In(logic_uScript_AddMessage_messageData_108, logic_uScript_AddMessage_speaker_108);
		if (logic_uScript_AddMessage_uScript_AddMessage_108.Out)
		{
			Relay_True_118();
		}
	}

	private void Relay_In_109()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_109 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_109.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_109);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_109.InRange)
		{
			Relay_In_119();
		}
	}

	private void Relay_True_112()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.True(out logic_uScriptAct_SetBool_Target_112);
		local_FillerMsgPlayed02_System_Boolean = logic_uScriptAct_SetBool_Target_112;
	}

	private void Relay_False_112()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.False(out logic_uScriptAct_SetBool_Target_112);
		local_FillerMsgPlayed02_System_Boolean = logic_uScriptAct_SetBool_Target_112;
	}

	private void Relay_In_113()
	{
		logic_uScript_AddMessage_messageData_113 = MsgFillerNPC01;
		logic_uScript_AddMessage_speaker_113 = CrazedMinionTechSpeaker;
		logic_uScript_AddMessage_Return_113 = logic_uScript_AddMessage_uScript_AddMessage_113.In(logic_uScript_AddMessage_messageData_113, logic_uScript_AddMessage_speaker_113);
		if (logic_uScript_AddMessage_uScript_AddMessage_113.Out)
		{
			Relay_True_123();
		}
	}

	private void Relay_True_118()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_118.True(out logic_uScriptAct_SetBool_Target_118);
		local_CubeTooEarlyMsgPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_118;
	}

	private void Relay_False_118()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_118.False(out logic_uScriptAct_SetBool_Target_118);
		local_CubeTooEarlyMsgPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_118;
	}

	private void Relay_In_119()
	{
		logic_uScriptCon_CompareBool_Bool_119 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_119.In(logic_uScriptCon_CompareBool_Bool_119);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_119.False)
		{
			Relay_In_108();
		}
	}

	private void Relay_True_123()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.True(out logic_uScriptAct_SetBool_Target_123);
		local_FillerMsgPlayed01_System_Boolean = logic_uScriptAct_SetBool_Target_123;
	}

	private void Relay_False_123()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.False(out logic_uScriptAct_SetBool_Target_123);
		local_FillerMsgPlayed01_System_Boolean = logic_uScriptAct_SetBool_Target_123;
	}

	private void Relay_In_125()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_125 = FillerNPCRange01;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_125.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_125);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_125.Out;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_125.InRange;
		if (num)
		{
			Relay_In_131();
		}
		if (inRange)
		{
			Relay_In_127();
		}
	}

	private void Relay_In_127()
	{
		logic_uScriptCon_CompareBool_Bool_127 = local_FillerMsgPlayed01_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_127.In(logic_uScriptCon_CompareBool_Bool_127);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_127.False)
		{
			Relay_In_113();
		}
	}

	private void Relay_In_128()
	{
		logic_uScriptCon_CompareBool_Bool_128 = local_FillerMsgPlayed02_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_128.In(logic_uScriptCon_CompareBool_Bool_128);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_128.False)
		{
			Relay_In_106();
		}
	}

	private void Relay_In_131()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_131 = FillerNPCRange02;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_131.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_131);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_131.Out;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_131.InRange;
		if (num)
		{
			Relay_In_109();
		}
		if (inRange)
		{
			Relay_In_128();
		}
	}

	private void Relay_In_132()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_132.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_132.Out)
		{
			Relay_In_145();
		}
	}

	private void Relay_In_138()
	{
		logic_uScript_SetEncounterTarget_owner_138 = owner_Connection_136;
		logic_uScript_SetEncounterTarget_visibleObject_138 = local_CubeTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_138.In(logic_uScript_SetEncounterTarget_owner_138, logic_uScript_SetEncounterTarget_visibleObject_138);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_138.Out)
		{
			Relay_In_178();
		}
	}

	private void Relay_In_141()
	{
		logic_uScript_AddMessage_messageData_141 = MsgCrazedInterrupt;
		logic_uScript_AddMessage_speaker_141 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_141 = logic_uScript_AddMessage_uScript_AddMessage_141.In(logic_uScript_AddMessage_messageData_141, logic_uScript_AddMessage_speaker_141);
	}

	private void Relay_In_144()
	{
		logic_uScript_PlayDialogue_dialogue_144 = LeaderIntroDialogue;
		logic_uScript_PlayDialogue_progress_144 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_144.In(logic_uScript_PlayDialogue_dialogue_144, ref logic_uScript_PlayDialogue_progress_144);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_144;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_144.Shown)
		{
			Relay_In_569();
		}
	}

	private void Relay_In_145()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_145.In(logic_uScriptAct_SetInt_Value_145, out logic_uScriptAct_SetInt_Target_145);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_145;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_145.Out)
		{
			Relay_True_154();
		}
	}

	private void Relay_In_150()
	{
		logic_uScriptAct_SetInt_Value_150 = local_StageCubeBattleIntro_System_Int32;
		logic_uScriptAct_SetInt_uScriptAct_SetInt_150.In(logic_uScriptAct_SetInt_Value_150, out logic_uScriptAct_SetInt_Target_150);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_150;
	}

	private void Relay_In_151()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_151 = CrazedMsgTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_151.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_151, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_151);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_151.Out)
		{
			Relay_In_141();
		}
	}

	private void Relay_True_154()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_154.True(out logic_uScriptAct_SetBool_Target_154);
		local_InRange_System_Boolean = logic_uScriptAct_SetBool_Target_154;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_154.Out)
		{
			Relay_In_104();
		}
	}

	private void Relay_False_154()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_154.False(out logic_uScriptAct_SetBool_Target_154);
		local_InRange_System_Boolean = logic_uScriptAct_SetBool_Target_154;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_154.Out)
		{
			Relay_In_104();
		}
	}

	private void Relay_Save_Out_157()
	{
		Relay_Save_558();
	}

	private void Relay_Load_Out_157()
	{
		Relay_Load_558();
	}

	private void Relay_Restart_Out_157()
	{
		Relay_Restart_558();
	}

	private void Relay_Save_157()
	{
		logic_SubGraph_SaveLoadInt_integer_157 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_157 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157.Save(logic_SubGraph_SaveLoadInt_restartValue_157, ref logic_SubGraph_SaveLoadInt_integer_157, logic_SubGraph_SaveLoadInt_intAsVariable_157, logic_SubGraph_SaveLoadInt_uniqueID_157);
	}

	private void Relay_Load_157()
	{
		logic_SubGraph_SaveLoadInt_integer_157 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_157 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157.Load(logic_SubGraph_SaveLoadInt_restartValue_157, ref logic_SubGraph_SaveLoadInt_integer_157, logic_SubGraph_SaveLoadInt_intAsVariable_157, logic_SubGraph_SaveLoadInt_uniqueID_157);
	}

	private void Relay_Restart_157()
	{
		logic_SubGraph_SaveLoadInt_integer_157 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_157 = local_DialogueProgress_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_157.Restart(logic_SubGraph_SaveLoadInt_restartValue_157, ref logic_SubGraph_SaveLoadInt_integer_157, logic_SubGraph_SaveLoadInt_intAsVariable_157, logic_SubGraph_SaveLoadInt_uniqueID_157);
	}

	private void Relay_Save_Out_159()
	{
		Relay_Save_458();
	}

	private void Relay_Load_Out_159()
	{
		Relay_Load_458();
	}

	private void Relay_Restart_Out_159()
	{
		Relay_Set_False_458();
	}

	private void Relay_Save_159()
	{
		logic_SubGraph_SaveLoadBool_boolean_159 = local_InRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_159 = local_InRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.Save(ref logic_SubGraph_SaveLoadBool_boolean_159, logic_SubGraph_SaveLoadBool_boolAsVariable_159, logic_SubGraph_SaveLoadBool_uniqueID_159);
	}

	private void Relay_Load_159()
	{
		logic_SubGraph_SaveLoadBool_boolean_159 = local_InRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_159 = local_InRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.Load(ref logic_SubGraph_SaveLoadBool_boolean_159, logic_SubGraph_SaveLoadBool_boolAsVariable_159, logic_SubGraph_SaveLoadBool_uniqueID_159);
	}

	private void Relay_Set_True_159()
	{
		logic_SubGraph_SaveLoadBool_boolean_159 = local_InRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_159 = local_InRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_159, logic_SubGraph_SaveLoadBool_boolAsVariable_159, logic_SubGraph_SaveLoadBool_uniqueID_159);
	}

	private void Relay_Set_False_159()
	{
		logic_SubGraph_SaveLoadBool_boolean_159 = local_InRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_159 = local_InRange_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_159.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_159, logic_SubGraph_SaveLoadBool_boolAsVariable_159, logic_SubGraph_SaveLoadBool_uniqueID_159);
	}

	private void Relay_In_161()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_161 = LeaderIntroStartTrigger;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_161 = LeaderOutOfRangeTrigger;
		logic_uScript_IsPlayerInTriggerSmart_inside_161 = local_InRange_System_Boolean;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_161.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_161, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_161, ref logic_uScript_IsPlayerInTriggerSmart_inside_161);
		local_InRange_System_Boolean = logic_uScript_IsPlayerInTriggerSmart_inside_161;
		bool allInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_161.AllInside;
		bool lastExited = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_161.LastExited;
		if (allInside)
		{
			Relay_In_267();
		}
		if (lastExited)
		{
			Relay_In_508();
		}
	}

	private void Relay_True_163()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_163.True(out logic_uScriptAct_SetBool_Target_163);
		local_CrazedMinionsAllAlive_System_Boolean = logic_uScriptAct_SetBool_Target_163;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_163.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_163.SetTrue;
		if (num)
		{
			Relay_In_283();
		}
		if (setTrue)
		{
			Relay_AtIndex_38();
		}
	}

	private void Relay_False_163()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_163.False(out logic_uScriptAct_SetBool_Target_163);
		local_CrazedMinionsAllAlive_System_Boolean = logic_uScriptAct_SetBool_Target_163;
		bool num = logic_uScriptAct_SetBool_uScriptAct_SetBool_163.Out;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_163.SetTrue;
		if (num)
		{
			Relay_In_283();
		}
		if (setTrue)
		{
			Relay_AtIndex_38();
		}
	}

	private void Relay_True_165()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_165.True(out logic_uScriptAct_SetBool_Target_165);
		local_FillerTechsAllAlive_System_Boolean = logic_uScriptAct_SetBool_Target_165;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_165.Out)
		{
			Relay_AtIndex_59();
		}
	}

	private void Relay_False_165()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_165.False(out logic_uScriptAct_SetBool_Target_165);
		local_FillerTechsAllAlive_System_Boolean = logic_uScriptAct_SetBool_Target_165;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_165.Out)
		{
			Relay_AtIndex_59();
		}
	}

	private void Relay_In_167()
	{
		logic_uScriptCon_CompareBool_Bool_167 = local_CrazedMinionsAllAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_167.In(logic_uScriptCon_CompareBool_Bool_167);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_167.True)
		{
			Relay_In_96();
		}
	}

	private void Relay_In_169()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_169.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_169, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_169, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_169.In(ref logic_uScript_SetTechsTeam_techs_169, logic_uScript_SetTechsTeam_team_169);
		local_CubeTechs_TankArray = logic_uScript_SetTechsTeam_techs_169;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_169.Out)
		{
			Relay_In_171();
		}
	}

	private void Relay_In_171()
	{
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_171.In(logic_uScript_PlayMiscSFX_miscSFXType_171);
		if (logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_171.Out)
		{
			Relay_In_188();
		}
	}

	private void Relay_In_173()
	{
		logic_uScript_StartMissionTimer_owner_173 = owner_Connection_174;
		logic_uScript_StartMissionTimer_startTime_173 = TimeLimit;
		logic_uScript_StartMissionTimer_uScript_StartMissionTimer_173.In(logic_uScript_StartMissionTimer_owner_173, logic_uScript_StartMissionTimer_startTime_173);
		if (logic_uScript_StartMissionTimer_uScript_StartMissionTimer_173.Out)
		{
			Relay_In_175();
		}
	}

	private void Relay_In_175()
	{
		logic_uScript_ShowMissionTimerUI_owner_175 = owner_Connection_174;
		logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_175.In(logic_uScript_ShowMissionTimerUI_owner_175, logic_uScript_ShowMissionTimerUI_showBestTime_175);
		if (logic_uScript_ShowMissionTimerUI_uScript_ShowMissionTimerUI_175.Out)
		{
			Relay_In_338();
		}
	}

	private void Relay_In_177()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_177 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_177 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTriggerSmart_inside_177 = local_InRange_System_Boolean;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_177.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_177, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_177, ref logic_uScript_IsPlayerInTriggerSmart_inside_177);
		local_InRange_System_Boolean = logic_uScript_IsPlayerInTriggerSmart_inside_177;
		if (logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_177.AllInside)
		{
			Relay_In_266();
		}
	}

	private void Relay_In_178()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_178.In(logic_uScriptAct_SetInt_Value_178, out logic_uScriptAct_SetInt_Target_178);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_178;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_178.Out)
		{
			Relay_False_181();
		}
	}

	private void Relay_True_181()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_181.True(out logic_uScriptAct_SetBool_Target_181);
		local_InRange_System_Boolean = logic_uScriptAct_SetBool_Target_181;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_181.Out)
		{
			Relay_In_150();
		}
	}

	private void Relay_False_181()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_181.False(out logic_uScriptAct_SetBool_Target_181);
		local_InRange_System_Boolean = logic_uScriptAct_SetBool_Target_181;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_181.Out)
		{
			Relay_In_150();
		}
	}

	private void Relay_In_182()
	{
		logic_uScript_PlayDialogue_dialogue_182 = InvulnerableCubeIntro;
		logic_uScript_PlayDialogue_progress_182 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_182.In(logic_uScript_PlayDialogue_dialogue_182, ref logic_uScript_PlayDialogue_progress_182);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_182;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_182.Shown)
		{
			Relay_In_173();
		}
	}

	private void Relay_Save_Out_185()
	{
		Relay_Save_580();
	}

	private void Relay_Load_Out_185()
	{
		Relay_Load_580();
	}

	private void Relay_Restart_Out_185()
	{
		Relay_Restart_580();
	}

	private void Relay_Save_185()
	{
		logic_SubGraph_SaveLoadInt_restartValue_185 = local_RoundCubeInvincible_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_185 = local_Round_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_185 = local_Round_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Save(logic_SubGraph_SaveLoadInt_restartValue_185, ref logic_SubGraph_SaveLoadInt_integer_185, logic_SubGraph_SaveLoadInt_intAsVariable_185, logic_SubGraph_SaveLoadInt_uniqueID_185);
	}

	private void Relay_Load_185()
	{
		logic_SubGraph_SaveLoadInt_restartValue_185 = local_RoundCubeInvincible_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_185 = local_Round_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_185 = local_Round_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Load(logic_SubGraph_SaveLoadInt_restartValue_185, ref logic_SubGraph_SaveLoadInt_integer_185, logic_SubGraph_SaveLoadInt_intAsVariable_185, logic_SubGraph_SaveLoadInt_uniqueID_185);
	}

	private void Relay_Restart_185()
	{
		logic_SubGraph_SaveLoadInt_restartValue_185 = local_RoundCubeInvincible_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_185 = local_Round_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_185 = local_Round_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Restart(logic_SubGraph_SaveLoadInt_restartValue_185, ref logic_SubGraph_SaveLoadInt_integer_185, logic_SubGraph_SaveLoadInt_intAsVariable_185, logic_SubGraph_SaveLoadInt_uniqueID_185);
	}

	private void Relay_In_188()
	{
		logic_uScriptAct_SetInt_Value_188 = local_StageCubeBattleRunning_System_Int32;
		logic_uScriptAct_SetInt_uScriptAct_SetInt_188.In(logic_uScriptAct_SetInt_Value_188, out logic_uScriptAct_SetInt_Target_188);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_188;
	}

	private void Relay_In_192()
	{
		logic_uScript_AddMessage_messageData_192 = MsgStartBossFight;
		logic_uScript_AddMessage_speaker_192 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_192 = logic_uScript_AddMessage_uScript_AddMessage_192.In(logic_uScript_AddMessage_messageData_192, logic_uScript_AddMessage_speaker_192);
	}

	private void Relay_ShowLabel_195()
	{
		logic_uScriptAct_PrintText_Text_195 = local_216_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_195.ShowLabel(logic_uScriptAct_PrintText_Text_195, logic_uScriptAct_PrintText_FontSize_195, logic_uScriptAct_PrintText_FontStyle_195, logic_uScriptAct_PrintText_FontColor_195, logic_uScriptAct_PrintText_textAnchor_195, logic_uScriptAct_PrintText_EdgePadding_195, logic_uScriptAct_PrintText_time_195);
	}

	private void Relay_HideLabel_195()
	{
		logic_uScriptAct_PrintText_Text_195 = local_216_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_195.HideLabel(logic_uScriptAct_PrintText_Text_195, logic_uScriptAct_PrintText_FontSize_195, logic_uScriptAct_PrintText_FontStyle_195, logic_uScriptAct_PrintText_FontColor_195, logic_uScriptAct_PrintText_textAnchor_195, logic_uScriptAct_PrintText_EdgePadding_195, logic_uScriptAct_PrintText_time_195);
	}

	private void Relay_In_196()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_196.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_196, num + 1);
		}
		logic_uScriptAct_Concatenate_A_196[num++] = local_199_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_196.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_196, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_196[num2++] = local_Stage_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_196.In(logic_uScriptAct_Concatenate_A_196, logic_uScriptAct_Concatenate_B_196, logic_uScriptAct_Concatenate_Separator_196, out logic_uScriptAct_Concatenate_Result_196);
		local_200_System_String = logic_uScriptAct_Concatenate_Result_196;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_196.Out)
		{
			Relay_In_197();
		}
	}

	private void Relay_In_197()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_197.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_197, num + 1);
		}
		logic_uScriptAct_Concatenate_A_197[num++] = local_200_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_197.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_197, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_197[num2++] = local_201_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_197.In(logic_uScriptAct_Concatenate_A_197, logic_uScriptAct_Concatenate_B_197, logic_uScriptAct_Concatenate_Separator_197, out logic_uScriptAct_Concatenate_Result_197);
		local_203_System_String = logic_uScriptAct_Concatenate_Result_197;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_197.Out)
		{
			Relay_In_202();
		}
	}

	private void Relay_In_202()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_202.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_202, num + 1);
		}
		logic_uScriptAct_Concatenate_A_202[num++] = local_203_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_202.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_202, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_202[num2++] = local_Round_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_202.In(logic_uScriptAct_Concatenate_A_202, logic_uScriptAct_Concatenate_B_202, logic_uScriptAct_Concatenate_Separator_202, out logic_uScriptAct_Concatenate_Result_202);
		local_207_System_String = logic_uScriptAct_Concatenate_Result_202;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_202.Out)
		{
			Relay_In_208();
		}
	}

	private void Relay_In_208()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_208.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_208, num + 1);
		}
		logic_uScriptAct_Concatenate_A_208[num++] = local_207_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_208.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_208, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_208[num2++] = local_209_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_208.In(logic_uScriptAct_Concatenate_A_208, logic_uScriptAct_Concatenate_B_208, logic_uScriptAct_Concatenate_Separator_208, out logic_uScriptAct_Concatenate_Result_208);
		local_210_System_String = logic_uScriptAct_Concatenate_Result_208;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_208.Out)
		{
			Relay_In_211();
		}
	}

	private void Relay_In_211()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_211.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_211, num + 1);
		}
		logic_uScriptAct_Concatenate_A_211[num++] = local_210_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_211.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_211, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_211[num2++] = local_DialogueProgress_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_211.In(logic_uScriptAct_Concatenate_A_211, logic_uScriptAct_Concatenate_B_211, logic_uScriptAct_Concatenate_Separator_211, out logic_uScriptAct_Concatenate_Result_211);
		local_213_System_String = logic_uScriptAct_Concatenate_Result_211;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_211.Out)
		{
			Relay_In_212();
		}
	}

	private void Relay_In_212()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_212.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_212, num + 1);
		}
		logic_uScriptAct_Concatenate_A_212[num++] = local_213_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_212.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_212, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_212[num2++] = local_217_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_212.In(logic_uScriptAct_Concatenate_A_212, logic_uScriptAct_Concatenate_B_212, logic_uScriptAct_Concatenate_Separator_212, out logic_uScriptAct_Concatenate_Result_212);
		local_215_System_String = logic_uScriptAct_Concatenate_Result_212;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_212.Out)
		{
			Relay_In_214();
		}
	}

	private void Relay_In_214()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_214.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_214, num + 1);
		}
		logic_uScriptAct_Concatenate_A_214[num++] = local_215_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_214.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_214, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_214[num2++] = local_InRange_System_Boolean;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_214.In(logic_uScriptAct_Concatenate_A_214, logic_uScriptAct_Concatenate_B_214, logic_uScriptAct_Concatenate_Separator_214, out logic_uScriptAct_Concatenate_Result_214);
		local_216_System_String = logic_uScriptAct_Concatenate_Result_214;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_214.Out)
		{
			Relay_ShowLabel_195();
		}
	}

	private void Relay_In_220()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_220 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_220 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTriggerSmart_inside_220 = local_InRange_System_Boolean;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_220.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_220, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_220, ref logic_uScript_IsPlayerInTriggerSmart_inside_220);
		local_InRange_System_Boolean = logic_uScript_IsPlayerInTriggerSmart_inside_220;
		bool allInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_220.AllInside;
		bool lastExited = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_220.LastExited;
		bool allOutside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_220.AllOutside;
		if (allInside)
		{
			Relay_In_232();
		}
		if (lastExited)
		{
			Relay_In_509();
		}
		if (allOutside)
		{
			Relay_In_537();
		}
	}

	private void Relay_In_224()
	{
		logic_uScript_SetEncounterTarget_owner_224 = owner_Connection_223;
		logic_uScript_SetEncounterTarget_visibleObject_224 = local_CrazedTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_224.In(logic_uScript_SetEncounterTarget_owner_224, logic_uScript_SetEncounterTarget_visibleObject_224);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_224.Out)
		{
			Relay_In_278();
		}
	}

	private void Relay_In_226()
	{
		logic_uScript_HideMissionTimerUI_owner_226 = owner_Connection_557;
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_226.In(logic_uScript_HideMissionTimerUI_owner_226);
		if (logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_226.Out)
		{
			Relay_In_227();
		}
	}

	private void Relay_In_227()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_227.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_227, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_227, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_227.In(ref logic_uScript_SetTechsTeam_techs_227, logic_uScript_SetTechsTeam_team_227);
		local_CubeTechs_TankArray = logic_uScript_SetTechsTeam_techs_227;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_227.Out)
		{
			Relay_In_224();
		}
	}

	private void Relay_In_229()
	{
		logic_uScript_StopMissionTimer_owner_229 = owner_Connection_230;
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_229.In(logic_uScript_StopMissionTimer_owner_229);
		if (logic_uScript_StopMissionTimer_uScript_StopMissionTimer_229.Out)
		{
			Relay_In_226();
		}
	}

	private void Relay_In_231()
	{
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_231.In(logic_uScript_PlayMiscSFX_miscSFXType_231);
		if (logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_231.Out)
		{
			Relay_In_462();
		}
	}

	private void Relay_In_232()
	{
		logic_uScript_GetMissionTimerDisplayTime_owner_232 = owner_Connection_235;
		logic_uScript_GetMissionTimerDisplayTime_Return_232 = logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_232.In(logic_uScript_GetMissionTimerDisplayTime_owner_232);
		local_233_System_Single = logic_uScript_GetMissionTimerDisplayTime_Return_232;
		if (logic_uScript_GetMissionTimerDisplayTime_uScript_GetMissionTimerDisplayTime_232.Out)
		{
			Relay_In_234();
		}
	}

	private void Relay_In_234()
	{
		logic_uScriptCon_CompareFloat_A_234 = local_233_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_234.In(logic_uScriptCon_CompareFloat_A_234, logic_uScriptCon_CompareFloat_B_234);
		bool greaterThan = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_234.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_234.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_238();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_264();
		}
	}

	private void Relay_In_238()
	{
		logic_uScriptCon_CompareBool_Bool_238 = local_CubeAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_238.In(logic_uScriptCon_CompareBool_Bool_238);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_238.False)
		{
			Relay_In_260();
		}
	}

	private void Relay_In_260()
	{
		logic_uScriptAct_SetInt_Value_260 = local_StageCubeDefeated_System_Int32;
		logic_uScriptAct_SetInt_uScriptAct_SetInt_260.In(logic_uScriptAct_SetInt_Value_260, out logic_uScriptAct_SetInt_Target_260);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_260;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_260.Out)
		{
			Relay_In_229();
		}
	}

	private void Relay_In_264()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_264.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_264.Out)
		{
			Relay_In_231();
		}
	}

	private void Relay_Output1_265()
	{
		Relay_InitialSpawn_14();
	}

	private void Relay_Output2_265()
	{
		Relay_In_69();
	}

	private void Relay_Output3_265()
	{
		Relay_In_99();
	}

	private void Relay_Output4_265()
	{
		Relay_In_161();
	}

	private void Relay_Output5_265()
	{
		Relay_In_177();
	}

	private void Relay_Output6_265()
	{
		Relay_In_220();
	}

	private void Relay_Output7_265()
	{
		Relay_In_270();
	}

	private void Relay_Output8_265()
	{
		Relay_In_342();
	}

	private void Relay_Output9_265()
	{
		Relay_In_317();
	}

	private void Relay_Output10_265()
	{
		Relay_In_372();
	}

	private void Relay_Output11_265()
	{
		Relay_In_471();
	}

	private void Relay_Output12_265()
	{
	}

	private void Relay_Output13_265()
	{
	}

	private void Relay_Output14_265()
	{
	}

	private void Relay_Output15_265()
	{
	}

	private void Relay_Output16_265()
	{
	}

	private void Relay_In_265()
	{
		logic_uScriptCon_BigManualSwitch_CurrentOutput_265 = local_Stage_System_Int32;
		logic_uScriptCon_BigManualSwitch_uScriptCon_BigManualSwitch_265.In(logic_uScriptCon_BigManualSwitch_CurrentOutput_265);
	}

	private void Relay_In_266()
	{
		logic_uScriptCon_CheckIntEquals_A_266 = local_Round_System_Int32;
		logic_uScriptCon_CheckIntEquals_B_266 = local_RoundCubeInvincible_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_266.In(logic_uScriptCon_CheckIntEquals_A_266, logic_uScriptCon_CheckIntEquals_B_266);
		bool num = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_266.True;
		bool flag = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_266.False;
		if (num)
		{
			Relay_In_182();
		}
		if (flag)
		{
			Relay_In_560();
		}
	}

	private void Relay_In_267()
	{
		logic_uScriptCon_CheckIntEquals_A_267 = local_Round_System_Int32;
		logic_uScriptCon_CheckIntEquals_B_267 = local_RoundCubeInvincible_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_267.In(logic_uScriptCon_CheckIntEquals_A_267, logic_uScriptCon_CheckIntEquals_B_267);
		bool num = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_267.True;
		bool flag = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_267.False;
		if (num)
		{
			Relay_In_144();
		}
		if (flag)
		{
			Relay_In_525();
		}
	}

	private void Relay_In_270()
	{
		logic_uScriptCon_CheckIntEquals_A_270 = local_Round_System_Int32;
		logic_uScriptCon_CheckIntEquals_B_270 = local_RoundCubeInvincible_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_270.In(logic_uScriptCon_CheckIntEquals_A_270, logic_uScriptCon_CheckIntEquals_B_270);
		bool num = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_270.True;
		bool flag = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_270.False;
		if (num)
		{
			Relay_In_350();
		}
		if (flag)
		{
			Relay_In_273();
		}
	}

	private void Relay_In_273()
	{
		logic_uScriptCon_CheckIntEquals_A_273 = local_Round_System_Int32;
		logic_uScriptCon_CheckIntEquals_B_273 = local_RoundSpawnAmbush_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_273.In(logic_uScriptCon_CheckIntEquals_A_273, logic_uScriptCon_CheckIntEquals_B_273);
		bool num = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_273.True;
		bool flag = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_273.False;
		if (num)
		{
			Relay_In_620();
		}
		if (flag)
		{
			Relay_In_499();
		}
	}

	private void Relay_In_278()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_278.In(logic_uScriptAct_SetInt_Value_278, out logic_uScriptAct_SetInt_Target_278);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_278;
	}

	private void Relay_True_280()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_280.True(out logic_uScriptAct_SetBool_Target_280);
		local_CrazedMinionsAllDead_System_Boolean = logic_uScriptAct_SetBool_Target_280;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_280.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_False_280()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_280.False(out logic_uScriptAct_SetBool_Target_280);
		local_CrazedMinionsAllDead_System_Boolean = logic_uScriptAct_SetBool_Target_280;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_280.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_283()
	{
		int num = 0;
		Array crazedMinionTechData = CrazedMinionTechData;
		if (logic_uScript_GetAndCheckTechs_techData_283.Length != num + crazedMinionTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_283, num + crazedMinionTechData.Length);
		}
		Array.Copy(crazedMinionTechData, 0, logic_uScript_GetAndCheckTechs_techData_283, num, crazedMinionTechData.Length);
		num += crazedMinionTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_283 = owner_Connection_281;
		int num2 = 0;
		Array array = local_CrazedMinionTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_283.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_283, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_283, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_283 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_283.In(logic_uScript_GetAndCheckTechs_techData_283, logic_uScript_GetAndCheckTechs_ownerNode_283, ref logic_uScript_GetAndCheckTechs_techs_283);
		local_CrazedMinionTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_283;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_283.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_283.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_283.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_283.WaitingToSpawn;
		if (allAlive)
		{
			Relay_False_280();
		}
		if (someAlive)
		{
			Relay_False_280();
		}
		if (allDead)
		{
			Relay_True_280();
		}
		if (waitingToSpawn)
		{
			Relay_False_280();
		}
	}

	private void Relay_In_285()
	{
		logic_uScript_PlayDialogue_dialogue_285 = SpawnAmbushDialogue;
		logic_uScript_PlayDialogue_progress_285 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_285.In(logic_uScript_PlayDialogue_dialogue_285, ref logic_uScript_PlayDialogue_progress_285);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_285;
		bool shown = logic_uScript_PlayDialogue_uScript_PlayDialogue_285.Shown;
		bool beginMessage = logic_uScript_PlayDialogue_uScript_PlayDialogue_285.BeginMessage;
		if (shown)
		{
			Relay_In_299();
		}
		if (beginMessage)
		{
			Relay_In_294();
		}
	}

	private void Relay_In_289()
	{
		logic_uScript_PlayDialogue_dialogue_289 = NoMoreAmbushDialogue;
		logic_uScript_PlayDialogue_progress_289 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_289.In(logic_uScript_PlayDialogue_dialogue_289, ref logic_uScript_PlayDialogue_progress_289);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_289;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_289.Shown)
		{
			Relay_In_300();
		}
	}

	private void Relay_In_292()
	{
		logic_uScriptAct_SetInt_Value_292 = local_StageCubeBattleFailed_System_Int32;
		logic_uScriptAct_SetInt_uScriptAct_SetInt_292.In(logic_uScriptAct_SetInt_Value_292, out logic_uScriptAct_SetInt_Target_292);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_292;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_292.Out)
		{
			Relay_In_351();
		}
	}

	private void Relay_In_294()
	{
		logic_uScriptCon_CheckIntEquals_A_294 = local_DialogueProgress_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_294.In(logic_uScriptCon_CheckIntEquals_A_294, logic_uScriptCon_CheckIntEquals_B_294);
		if (logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_294.True)
		{
			Relay_InitialSpawn_298();
		}
	}

	private void Relay_InitialSpawn_298()
	{
		int num = 0;
		Array cubeAmbushTechData = CubeAmbushTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_298.Length != num + cubeAmbushTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_298, num + cubeAmbushTechData.Length);
		}
		Array.Copy(cubeAmbushTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_298, num, cubeAmbushTechData.Length);
		num += cubeAmbushTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_298 = owner_Connection_297;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_298.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_298, logic_uScript_SpawnTechsFromData_ownerNode_298, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_298, logic_uScript_SpawnTechsFromData_allowResurrection_298);
	}

	private void Relay_In_299()
	{
		logic_uScriptCon_CompareBool_Bool_299 = local_AmbushTechsAllDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_299.In(logic_uScriptCon_CompareBool_Bool_299);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_299.True)
		{
			Relay_In_577();
		}
	}

	private void Relay_In_300()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_300.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_300.Out)
		{
			Relay_In_349();
		}
	}

	private void Relay_In_301()
	{
		logic_uScript_AddMessage_messageData_301 = MsgOutOfTime;
		logic_uScript_AddMessage_speaker_301 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_301 = logic_uScript_AddMessage_uScript_AddMessage_301.In(logic_uScript_AddMessage_messageData_301, logic_uScript_AddMessage_speaker_301);
	}

	private void Relay_In_305()
	{
		logic_uScript_SetEncounterTarget_owner_305 = owner_Connection_304;
		logic_uScript_SetEncounterTarget_visibleObject_305 = local_CrazedTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_305.In(logic_uScript_SetEncounterTarget_owner_305, logic_uScript_SetEncounterTarget_visibleObject_305);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_305.Out)
		{
			Relay_False_307();
		}
	}

	private void Relay_True_307()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_307.True(out logic_uScriptAct_SetBool_Target_307);
		local_InRange_System_Boolean = logic_uScriptAct_SetBool_Target_307;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_307.Out)
		{
			Relay_True_456();
		}
	}

	private void Relay_False_307()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_307.False(out logic_uScriptAct_SetBool_Target_307);
		local_InRange_System_Boolean = logic_uScriptAct_SetBool_Target_307;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_307.Out)
		{
			Relay_True_456();
		}
	}

	private void Relay_In_310()
	{
		logic_uScriptAct_SetInt_Value_310 = local_StageResetCube_System_Int32;
		logic_uScriptAct_SetInt_uScriptAct_SetInt_310.In(logic_uScriptAct_SetInt_Value_310, out logic_uScriptAct_SetInt_Target_310);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_310;
	}

	private void Relay_In_317()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_317 = LeaderIntroStartTrigger;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_317 = LeaderOutOfRangeTrigger;
		logic_uScript_IsPlayerInTriggerSmart_inside_317 = local_InRange_System_Boolean;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_317.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_317, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_317, ref logic_uScript_IsPlayerInTriggerSmart_inside_317);
		local_InRange_System_Boolean = logic_uScript_IsPlayerInTriggerSmart_inside_317;
		bool allInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_317.AllInside;
		bool someInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_317.SomeInside;
		if (allInside)
		{
			Relay_In_510();
		}
		if (someInside)
		{
			Relay_In_510();
		}
	}

	private void Relay_In_318()
	{
		logic_uScript_SetTankInvulnerable_tank_318 = local_CubeTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_318.In(logic_uScript_SetTankInvulnerable_invulnerable_318, logic_uScript_SetTankInvulnerable_tank_318);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_318.Out)
		{
			Relay_In_320();
		}
	}

	private void Relay_In_320()
	{
		logic_uScript_RemoveTech_tech_320 = local_CubeTech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_320.In(logic_uScript_RemoveTech_tech_320);
	}

	private void Relay_In_321()
	{
		logic_uScriptCon_CompareBool_Bool_321 = local_CubeAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_321.In(logic_uScriptCon_CompareBool_Bool_321);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_321.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_321.False;
		if (num)
		{
			Relay_In_318();
		}
		if (flag)
		{
			Relay_InitialSpawn_325();
		}
	}

	private void Relay_InitialSpawn_325()
	{
		int num = 0;
		Array cubeTechData = CubeTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_325.Length != num + cubeTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_325, num + cubeTechData.Length);
		}
		Array.Copy(cubeTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_325, num, cubeTechData.Length);
		num += cubeTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_325 = owner_Connection_323;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_325.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_325, logic_uScript_SpawnTechsFromData_ownerNode_325, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_325, logic_uScript_SpawnTechsFromData_allowResurrection_325);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_325.Out)
		{
			Relay_In_327();
		}
	}

	private void Relay_In_327()
	{
		logic_uScriptAct_SetInt_Value_327 = local_StageConfigureTechs_System_Int32;
		logic_uScriptAct_SetInt_uScriptAct_SetInt_327.In(logic_uScriptAct_SetInt_Value_327, out logic_uScriptAct_SetInt_Target_327);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_327;
	}

	private void Relay_In_328()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_328.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_328.Out)
		{
			Relay_In_265();
		}
	}

	private void Relay_In_331()
	{
		logic_uScriptCon_CheckIntEquals_A_331 = local_Round_System_Int32;
		logic_uScriptCon_CheckIntEquals_B_331 = local_RoundCubeInvincible_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_331.In(logic_uScriptCon_CheckIntEquals_A_331, logic_uScriptCon_CheckIntEquals_B_331);
		if (logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_331.True)
		{
			Relay_In_125();
		}
	}

	private void Relay_In_333()
	{
		logic_uScriptAct_AddInt_v2_A_333 = local_Round_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_333.In(logic_uScriptAct_AddInt_v2_A_333, logic_uScriptAct_AddInt_v2_B_333, out logic_uScriptAct_AddInt_v2_IntResult_333, out logic_uScriptAct_AddInt_v2_FloatResult_333);
		local_Round_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_333;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_333.Out)
		{
			Relay_In_327();
		}
	}

	private void Relay_In_338()
	{
		logic_uScript_ResetMissionTimerTimeElapsed_owner_338 = owner_Connection_339;
		logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_338.In(logic_uScript_ResetMissionTimerTimeElapsed_owner_338, logic_uScript_ResetMissionTimerTimeElapsed_startTime_338);
		if (logic_uScript_ResetMissionTimerTimeElapsed_uScript_ResetMissionTimerTimeElapsed_338.Out)
		{
			Relay_In_169();
		}
	}

	private void Relay_In_342()
	{
		logic_uScriptCon_CheckIntEquals_A_342 = local_Round_System_Int32;
		logic_uScriptCon_CheckIntEquals_B_342 = local_RoundCubeInvincible_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_342.In(logic_uScriptCon_CheckIntEquals_A_342, logic_uScriptCon_CheckIntEquals_B_342);
		bool num = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_342.True;
		bool flag = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_342.False;
		if (num)
		{
			Relay_In_506();
		}
		if (flag)
		{
			Relay_In_345();
		}
	}

	private void Relay_In_345()
	{
		logic_uScriptCon_CheckIntEquals_A_345 = local_Round_System_Int32;
		logic_uScriptCon_CheckIntEquals_B_345 = local_RoundSpawnAmbush_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_345.In(logic_uScriptCon_CheckIntEquals_A_345, logic_uScriptCon_CheckIntEquals_B_345);
		bool num = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_345.True;
		bool flag = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_345.False;
		if (num)
		{
			Relay_In_448();
		}
		if (flag)
		{
			Relay_In_365();
		}
	}

	private void Relay_In_346()
	{
		logic_uScript_PlayDialogue_dialogue_346 = FailedToKillInvincibleCubeDialogue;
		logic_uScript_PlayDialogue_progress_346 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_346.In(logic_uScript_PlayDialogue_dialogue_346, ref logic_uScript_PlayDialogue_progress_346);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_346;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_346.Shown)
		{
			Relay_In_305();
		}
	}

	private void Relay_In_349()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_349.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_349.Out)
		{
			Relay_In_292();
		}
	}

	private void Relay_In_350()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_350.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_350.Out)
		{
			Relay_In_292();
		}
	}

	private void Relay_In_351()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_351.In(logic_uScriptAct_SetInt_Value_351, out logic_uScriptAct_SetInt_Target_351);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_351;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_351.Out)
		{
			Relay_In_582();
		}
	}

	private void Relay_True_354()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_354.True(out logic_uScriptAct_SetBool_Target_354);
		local_AmbushTechsAllDead_System_Boolean = logic_uScriptAct_SetBool_Target_354;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_354.Out)
		{
			Relay_In_535();
		}
	}

	private void Relay_False_354()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_354.False(out logic_uScriptAct_SetBool_Target_354);
		local_AmbushTechsAllDead_System_Boolean = logic_uScriptAct_SetBool_Target_354;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_354.Out)
		{
			Relay_In_535();
		}
	}

	private void Relay_In_357()
	{
		int num = 0;
		Array cubeAmbushTechData = CubeAmbushTechData;
		if (logic_uScript_GetAndCheckTechs_techData_357.Length != num + cubeAmbushTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_357, num + cubeAmbushTechData.Length);
		}
		Array.Copy(cubeAmbushTechData, 0, logic_uScript_GetAndCheckTechs_techData_357, num, cubeAmbushTechData.Length);
		num += cubeAmbushTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_357 = owner_Connection_356;
		int num2 = 0;
		Array array = local_AmbushTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_357.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_357, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_357, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_357 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.In(logic_uScript_GetAndCheckTechs_techData_357, logic_uScript_GetAndCheckTechs_ownerNode_357, ref logic_uScript_GetAndCheckTechs_techs_357);
		local_AmbushTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_357;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_357.WaitingToSpawn;
		if (allAlive)
		{
			Relay_False_354();
		}
		if (someAlive)
		{
			Relay_False_354();
		}
		if (allDead)
		{
			Relay_True_354();
		}
		if (waitingToSpawn)
		{
			Relay_False_354();
		}
	}

	private void Relay_In_361()
	{
		logic_uScriptCon_CheckIntEquals_A_361 = local_Round_System_Int32;
		logic_uScriptCon_CheckIntEquals_B_361 = local_RoundCubeInvincible_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_361.In(logic_uScriptCon_CheckIntEquals_A_361, logic_uScriptCon_CheckIntEquals_B_361);
		bool num = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_361.True;
		bool flag = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_361.False;
		if (num)
		{
			Relay_In_89();
		}
		if (flag)
		{
			Relay_In_363();
		}
	}

	private void Relay_In_363()
	{
		logic_uScript_SetTankInvulnerable_tank_363 = local_CubeTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_363.In(logic_uScript_SetTankInvulnerable_invulnerable_363, logic_uScript_SetTankInvulnerable_tank_363);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_363.Out)
		{
			Relay_In_86();
		}
	}

	private void Relay_In_365()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_365.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_365.Out)
		{
			Relay_In_448();
		}
	}

	private void Relay_In_366()
	{
		logic_uScript_SetTankInvulnerable_tank_366 = local_CrazedTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_366.In(logic_uScript_SetTankInvulnerable_invulnerable_366, logic_uScript_SetTankInvulnerable_tank_366);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_366.Out)
		{
			Relay_In_368();
		}
	}

	private void Relay_In_367()
	{
		logic_uScript_SetTechAIType_tech_367 = local_CrazedTech_Tank;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_367.In(logic_uScript_SetTechAIType_tech_367, logic_uScript_SetTechAIType_aiType_367);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_367.Out)
		{
			Relay_In_392();
		}
	}

	private void Relay_In_368()
	{
		logic_uScript_SetBatteryChargeAmount_tech_368 = local_CrazedTech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_368.In(logic_uScript_SetBatteryChargeAmount_tech_368, logic_uScript_SetBatteryChargeAmount_chargeAmount_368);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_368.Out)
		{
			Relay_In_367();
		}
	}

	private void Relay_In_372()
	{
		logic_uScript_PlayDialogue_dialogue_372 = CubeDefeatedDialogue;
		logic_uScript_PlayDialogue_progress_372 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_372.In(logic_uScript_PlayDialogue_dialogue_372, ref logic_uScript_PlayDialogue_progress_372);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_372;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_372.Shown)
		{
			Relay_In_568();
		}
	}

	private void Relay_In_374()
	{
		int num = 0;
		Array array = local_CrazedTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_374.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_374, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_374, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_374.In(ref logic_uScript_SetTechsTeam_techs_374, logic_uScript_SetTechsTeam_team_374);
		local_CrazedTechs_TankArray = logic_uScript_SetTechsTeam_techs_374;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_374.Out)
		{
			Relay_In_366();
		}
	}

	private void Relay_In_378()
	{
		logic_uScript_FlyTechUpAndAway_tech_378 = local_FillerTech01_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_378 = TechFlyAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_378 = TechFlyParticles;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_378.In(logic_uScript_FlyTechUpAndAway_tech_378, logic_uScript_FlyTechUpAndAway_maxLifetime_378, logic_uScript_FlyTechUpAndAway_targetHeight_378, logic_uScript_FlyTechUpAndAway_aiTree_378, logic_uScript_FlyTechUpAndAway_removalParticles_378);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_378.Out)
		{
			Relay_In_380();
		}
	}

	private void Relay_In_380()
	{
		logic_uScript_FlyTechUpAndAway_tech_380 = local_FillerTech02_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_380 = TechFlyAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_380 = TechFlyParticles;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_380.In(logic_uScript_FlyTechUpAndAway_tech_380, logic_uScript_FlyTechUpAndAway_maxLifetime_380, logic_uScript_FlyTechUpAndAway_targetHeight_380, logic_uScript_FlyTechUpAndAway_aiTree_380, logic_uScript_FlyTechUpAndAway_removalParticles_380);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_380.Out)
		{
			Relay_In_384();
		}
	}

	private void Relay_In_384()
	{
		logic_uScript_FlyTechUpAndAway_tech_384 = local_FillerTech03_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_384 = TechFlyAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_384 = TechFlyParticles;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_384.In(logic_uScript_FlyTechUpAndAway_tech_384, logic_uScript_FlyTechUpAndAway_maxLifetime_384, logic_uScript_FlyTechUpAndAway_targetHeight_384, logic_uScript_FlyTechUpAndAway_aiTree_384, logic_uScript_FlyTechUpAndAway_removalParticles_384);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_384.Out)
		{
			Relay_In_400();
		}
	}

	private void Relay_In_391()
	{
		logic_uScript_SetBatteryChargeAmount_tech_391 = local_CrazedMinion1_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_391.In(logic_uScript_SetBatteryChargeAmount_tech_391, logic_uScript_SetBatteryChargeAmount_chargeAmount_391);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_391.Out)
		{
			Relay_In_394();
		}
	}

	private void Relay_In_392()
	{
		int num = 0;
		Array array = local_CrazedMinionTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_392.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_392, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_392, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_392.In(ref logic_uScript_SetTechsTeam_techs_392, logic_uScript_SetTechsTeam_team_392);
		local_CrazedMinionTechs_TankArray = logic_uScript_SetTechsTeam_techs_392;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_392.Out)
		{
			Relay_In_393();
		}
	}

	private void Relay_In_393()
	{
		logic_uScript_SetTankInvulnerable_tank_393 = local_CrazedMinion1_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_393.In(logic_uScript_SetTankInvulnerable_invulnerable_393, logic_uScript_SetTankInvulnerable_tank_393);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_393.Out)
		{
			Relay_In_391();
		}
	}

	private void Relay_In_394()
	{
		logic_uScript_SetTechAIType_tech_394 = local_CrazedMinion1_Tank;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_394.In(logic_uScript_SetTechAIType_tech_394, logic_uScript_SetTechAIType_aiType_394);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_394.Out)
		{
			Relay_In_395();
		}
	}

	private void Relay_In_395()
	{
		logic_uScript_SetTankInvulnerable_tank_395 = local_CrazedMinion2_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_395.In(logic_uScript_SetTankInvulnerable_invulnerable_395, logic_uScript_SetTankInvulnerable_tank_395);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_395.Out)
		{
			Relay_In_396();
		}
	}

	private void Relay_In_396()
	{
		logic_uScript_SetBatteryChargeAmount_tech_396 = local_CrazedMinion2_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_396.In(logic_uScript_SetBatteryChargeAmount_tech_396, logic_uScript_SetBatteryChargeAmount_chargeAmount_396);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_396.Out)
		{
			Relay_In_397();
		}
	}

	private void Relay_In_397()
	{
		logic_uScript_SetTechAIType_tech_397 = local_CrazedMinion2_Tank;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_397.In(logic_uScript_SetTechAIType_tech_397, logic_uScript_SetTechAIType_aiType_397);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_397.Out)
		{
			Relay_In_378();
		}
	}

	private void Relay_In_400()
	{
		logic_uScriptAct_SetInt_Value_400 = local_StageFinalBattle_System_Int32;
		logic_uScriptAct_SetInt_uScriptAct_SetInt_400.In(logic_uScriptAct_SetInt_Value_400, out logic_uScriptAct_SetInt_Target_400);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_400;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_400.Out)
		{
			Relay_In_482();
		}
	}

	private void Relay_In_403()
	{
		logic_uScriptCon_CompareBool_Bool_403 = local_CrazedTechAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_403.In(logic_uScriptCon_CompareBool_Bool_403);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_403.False)
		{
			Relay_In_572();
		}
	}

	private void Relay_In_406()
	{
		logic_uScriptCon_CompareBool_Bool_406 = local_CrazedMinionsAllDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_406.In(logic_uScriptCon_CompareBool_Bool_406);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_406.True)
		{
			Relay_In_403();
		}
	}

	private void Relay_Succeed_409()
	{
		logic_uScript_FinishEncounter_owner_409 = owner_Connection_410;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_409.Succeed(logic_uScript_FinishEncounter_owner_409);
	}

	private void Relay_Fail_409()
	{
		logic_uScript_FinishEncounter_owner_409 = owner_Connection_410;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_409.Fail(logic_uScript_FinishEncounter_owner_409);
	}

	private void Relay_In_411()
	{
		logic_uScript_AddMessage_messageData_411 = MsgMissionCompleteNoTrigger;
		logic_uScript_AddMessage_speaker_411 = GCSpeaker;
		logic_uScript_AddMessage_Return_411 = logic_uScript_AddMessage_uScript_AddMessage_411.In(logic_uScript_AddMessage_messageData_411, logic_uScript_AddMessage_speaker_411);
		if (logic_uScript_AddMessage_uScript_AddMessage_411.Out)
		{
			Relay_Succeed_409();
		}
	}

	private void Relay_In_419()
	{
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_419 = local_CubeTech_Tank;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_419 = local_418_TechSequencer_ChainType;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_419.In(logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_419, logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_419);
	}

	private void Relay_In_421()
	{
		logic_uScript_SetTechExplodeDetachingBlocks_tech_421 = local_CubeTech_Tank;
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_421.In(logic_uScript_SetTechExplodeDetachingBlocks_tech_421, logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_421, logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_421);
	}

	private void Relay_In_422()
	{
		logic_uScript_GetTankBlock_tank_422 = local_CubeTech_Tank;
		logic_uScript_GetTankBlock_blockType_422 = CubeShieldBlockData;
		logic_uScript_GetTankBlock_Return_422 = logic_uScript_GetTankBlock_uScript_GetTankBlock_422.In(logic_uScript_GetTankBlock_tank_422, logic_uScript_GetTankBlock_blockType_422);
		local_CubeShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_422;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_422.Returned)
		{
			Relay_In_425();
		}
	}

	private void Relay_In_425()
	{
		logic_uScript_SetShieldEnabled_targetObject_425 = local_CubeShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_425.In(logic_uScript_SetShieldEnabled_targetObject_425, logic_uScript_SetShieldEnabled_enable_425);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_425.Out)
		{
			Relay_In_419();
		}
	}

	private void Relay_In_430()
	{
		logic_uScriptCon_CheckIntEquals_A_430 = local_Round_System_Int32;
		logic_uScriptCon_CheckIntEquals_B_430 = local_RoundCubeInvincible_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_430.In(logic_uScriptCon_CheckIntEquals_A_430, logic_uScriptCon_CheckIntEquals_B_430);
		bool num = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_430.True;
		bool flag = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_430.False;
		if (num)
		{
			Relay_In_453();
		}
		if (flag)
		{
			Relay_In_432();
		}
	}

	private void Relay_In_432()
	{
		logic_uScript_GetTankBlock_tank_432 = local_CubeTech_Tank;
		logic_uScript_GetTankBlock_blockType_432 = CubeShieldBlockData;
		logic_uScript_GetTankBlock_Return_432 = logic_uScript_GetTankBlock_uScript_GetTankBlock_432.In(logic_uScript_GetTankBlock_tank_432, logic_uScript_GetTankBlock_blockType_432);
		local_CubeShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_432;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_432.Returned)
		{
			Relay_In_440();
		}
	}

	private void Relay_In_435()
	{
		logic_uScript_SetTechExplodeDetachingBlocks_tech_435 = local_CubeTech_Tank;
		logic_uScript_SetTechExplodeDetachingBlocks_uScript_SetTechExplodeDetachingBlocks_435.In(logic_uScript_SetTechExplodeDetachingBlocks_tech_435, logic_uScript_SetTechExplodeDetachingBlocks_explodeDetachingBlocks_435, logic_uScript_SetTechExplodeDetachingBlocks_explodeDelay_435);
	}

	private void Relay_In_437()
	{
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_437 = local_CubeTech_Tank;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_437 = local_439_TechSequencer_ChainType;
		logic_uScript_AllowTechEnergyModuleSimultaneousActivation_uScript_AllowTechEnergyModuleSimultaneousActivation_437.In(logic_uScript_AllowTechEnergyModuleSimultaneousActivation_tech_437, logic_uScript_AllowTechEnergyModuleSimultaneousActivation_chainType_437);
	}

	private void Relay_In_440()
	{
		logic_uScript_SetShieldEnabled_targetObject_440 = local_CubeShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_440.In(logic_uScript_SetShieldEnabled_targetObject_440, logic_uScript_SetShieldEnabled_enable_440);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_440.Out)
		{
			Relay_In_437();
		}
	}

	private void Relay_In_445()
	{
		logic_uScriptAct_SetInt_Value_445 = local_StageResetCube_System_Int32;
		logic_uScriptAct_SetInt_uScriptAct_SetInt_445.In(logic_uScriptAct_SetInt_Value_445, out logic_uScriptAct_SetInt_Target_445);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_445;
	}

	private void Relay_True_447()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_447.True(out logic_uScriptAct_SetBool_Target_447);
		local_InRange_System_Boolean = logic_uScriptAct_SetBool_Target_447;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_447.Out)
		{
			Relay_In_575();
		}
	}

	private void Relay_False_447()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_447.False(out logic_uScriptAct_SetBool_Target_447);
		local_InRange_System_Boolean = logic_uScriptAct_SetBool_Target_447;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_447.Out)
		{
			Relay_In_575();
		}
	}

	private void Relay_In_448()
	{
		logic_uScript_SetEncounterTarget_owner_448 = owner_Connection_449;
		logic_uScript_SetEncounterTarget_visibleObject_448 = local_CrazedTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_448.In(logic_uScript_SetEncounterTarget_owner_448, logic_uScript_SetEncounterTarget_visibleObject_448);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_448.Out)
		{
			Relay_False_447();
		}
	}

	private void Relay_In_450()
	{
		logic_uScript_AddMessage_messageData_450 = MsgInvincibleCubeSwitchOff;
		logic_uScript_AddMessage_speaker_450 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_450 = logic_uScript_AddMessage_uScript_AddMessage_450.In(logic_uScript_AddMessage_messageData_450, logic_uScript_AddMessage_speaker_450);
		if (logic_uScript_AddMessage_uScript_AddMessage_450.Out)
		{
			Relay_In_574();
		}
	}

	private void Relay_In_453()
	{
		logic_uScriptCon_CompareBool_Bool_453 = local_MidDialogueMsgPlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_453.In(logic_uScriptCon_CompareBool_Bool_453);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_453.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_453.False;
		if (num)
		{
			Relay_In_432();
		}
		if (flag)
		{
			Relay_In_422();
		}
	}

	private void Relay_True_456()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_456.True(out logic_uScriptAct_SetBool_Target_456);
		local_MidDialogueMsgPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_456;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_456.Out)
		{
			Relay_In_450();
		}
	}

	private void Relay_False_456()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_456.False(out logic_uScriptAct_SetBool_Target_456);
		local_MidDialogueMsgPlayed_System_Boolean = logic_uScriptAct_SetBool_Target_456;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_456.Out)
		{
			Relay_In_450();
		}
	}

	private void Relay_Save_Out_458()
	{
		Relay_Save_481();
	}

	private void Relay_Load_Out_458()
	{
		Relay_Load_481();
	}

	private void Relay_Restart_Out_458()
	{
		Relay_Set_False_481();
	}

	private void Relay_Save_458()
	{
		logic_SubGraph_SaveLoadBool_boolean_458 = local_MidDialogueMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_458 = local_MidDialogueMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Save(ref logic_SubGraph_SaveLoadBool_boolean_458, logic_SubGraph_SaveLoadBool_boolAsVariable_458, logic_SubGraph_SaveLoadBool_uniqueID_458);
	}

	private void Relay_Load_458()
	{
		logic_SubGraph_SaveLoadBool_boolean_458 = local_MidDialogueMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_458 = local_MidDialogueMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Load(ref logic_SubGraph_SaveLoadBool_boolean_458, logic_SubGraph_SaveLoadBool_boolAsVariable_458, logic_SubGraph_SaveLoadBool_uniqueID_458);
	}

	private void Relay_Set_True_458()
	{
		logic_SubGraph_SaveLoadBool_boolean_458 = local_MidDialogueMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_458 = local_MidDialogueMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_458, logic_SubGraph_SaveLoadBool_boolAsVariable_458, logic_SubGraph_SaveLoadBool_uniqueID_458);
	}

	private void Relay_Set_False_458()
	{
		logic_SubGraph_SaveLoadBool_boolean_458 = local_MidDialogueMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_458 = local_MidDialogueMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_458.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_458, logic_SubGraph_SaveLoadBool_boolAsVariable_458, logic_SubGraph_SaveLoadBool_uniqueID_458);
	}

	private void Relay_In_460()
	{
		logic_uScriptCon_CheckIntEquals_A_460 = local_Round_System_Int32;
		logic_uScriptCon_CheckIntEquals_B_460 = local_RoundCubeInvincible_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_460.In(logic_uScriptCon_CheckIntEquals_A_460, logic_uScriptCon_CheckIntEquals_B_460);
		bool num = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_460.True;
		bool flag = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_460.False;
		if (num)
		{
			Relay_In_232();
		}
		if (flag)
		{
			Relay_In_467();
		}
	}

	private void Relay_In_462()
	{
		logic_uScript_StopMissionTimer_owner_462 = owner_Connection_464;
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_462.In(logic_uScript_StopMissionTimer_owner_462);
		if (logic_uScript_StopMissionTimer_uScript_StopMissionTimer_462.Out)
		{
			Relay_In_463();
		}
	}

	private void Relay_In_463()
	{
		logic_uScript_HideMissionTimerUI_owner_463 = owner_Connection_555;
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_463.In(logic_uScript_HideMissionTimerUI_owner_463);
		if (logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_463.Out)
		{
			Relay_In_484();
		}
	}

	private void Relay_In_466()
	{
		logic_uScript_StopMissionTimer_owner_466 = owner_Connection_468;
		logic_uScript_StopMissionTimer_uScript_StopMissionTimer_466.In(logic_uScript_StopMissionTimer_owner_466);
		if (logic_uScript_StopMissionTimer_uScript_StopMissionTimer_466.Out)
		{
			Relay_In_470();
		}
	}

	private void Relay_In_467()
	{
		logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_467.In(logic_uScript_PlayMiscSFX_miscSFXType_467);
		if (logic_uScript_PlayMiscSFX_uScript_PlayMiscSFX_467.Out)
		{
			Relay_In_466();
		}
	}

	private void Relay_In_470()
	{
		logic_uScript_HideMissionTimerUI_owner_470 = owner_Connection_556;
		logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_470.In(logic_uScript_HideMissionTimerUI_owner_470);
		if (logic_uScript_HideMissionTimerUI_uScript_HideMissionTimerUI_470.Out)
		{
			Relay_In_491();
		}
	}

	private void Relay_In_471()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_471 = LeaderOutOfRangeTrigger;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_471 = LeaderOutOfRangeTrigger;
		logic_uScript_IsPlayerInTriggerSmart_inside_471 = local_InRange_System_Boolean;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_471.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_471, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_471, ref logic_uScript_IsPlayerInTriggerSmart_inside_471);
		local_InRange_System_Boolean = logic_uScript_IsPlayerInTriggerSmart_inside_471;
		bool firstEntered = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_471.FirstEntered;
		bool allInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_471.AllInside;
		bool allOutside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_471.AllOutside;
		if (firstEntered)
		{
			Relay_In_549();
		}
		if (allInside)
		{
			Relay_In_539();
		}
		if (allOutside)
		{
			Relay_In_539();
		}
	}

	private void Relay_In_474()
	{
		logic_uScript_PlayDialogue_dialogue_474 = MsgFinalCubeDeathDialogue;
		logic_uScript_PlayDialogue_progress_474 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_474.In(logic_uScript_PlayDialogue_dialogue_474, ref logic_uScript_PlayDialogue_progress_474);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_474;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_474.Out)
		{
			Relay_True_477();
		}
	}

	private void Relay_True_477()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_477.True(out logic_uScriptAct_SetBool_Target_477);
		local_FinalDialoguePlayed_System_Boolean = logic_uScriptAct_SetBool_Target_477;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_477.Out)
		{
			Relay_In_406();
		}
	}

	private void Relay_False_477()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_477.False(out logic_uScriptAct_SetBool_Target_477);
		local_FinalDialoguePlayed_System_Boolean = logic_uScriptAct_SetBool_Target_477;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_477.Out)
		{
			Relay_In_406();
		}
	}

	private void Relay_In_478()
	{
		logic_uScriptCon_CompareBool_Bool_478 = local_FinalDialoguePlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_478.In(logic_uScriptCon_CompareBool_Bool_478);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_478.False)
		{
			Relay_In_474();
		}
	}

	private void Relay_Save_Out_481()
	{
		Relay_Save_531();
	}

	private void Relay_Load_Out_481()
	{
		Relay_Load_531();
	}

	private void Relay_Restart_Out_481()
	{
		Relay_Set_False_531();
	}

	private void Relay_Save_481()
	{
		logic_SubGraph_SaveLoadBool_boolean_481 = local_FinalDialoguePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_481 = local_FinalDialoguePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.Save(ref logic_SubGraph_SaveLoadBool_boolean_481, logic_SubGraph_SaveLoadBool_boolAsVariable_481, logic_SubGraph_SaveLoadBool_uniqueID_481);
	}

	private void Relay_Load_481()
	{
		logic_SubGraph_SaveLoadBool_boolean_481 = local_FinalDialoguePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_481 = local_FinalDialoguePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.Load(ref logic_SubGraph_SaveLoadBool_boolean_481, logic_SubGraph_SaveLoadBool_boolAsVariable_481, logic_SubGraph_SaveLoadBool_uniqueID_481);
	}

	private void Relay_Set_True_481()
	{
		logic_SubGraph_SaveLoadBool_boolean_481 = local_FinalDialoguePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_481 = local_FinalDialoguePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_481, logic_SubGraph_SaveLoadBool_boolAsVariable_481, logic_SubGraph_SaveLoadBool_uniqueID_481);
	}

	private void Relay_Set_False_481()
	{
		logic_SubGraph_SaveLoadBool_boolean_481 = local_FinalDialoguePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_481 = local_FinalDialoguePlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_481.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_481, logic_SubGraph_SaveLoadBool_boolAsVariable_481, logic_SubGraph_SaveLoadBool_uniqueID_481);
	}

	private void Relay_In_482()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_482.In(logic_uScriptAct_SetInt_Value_482, out logic_uScriptAct_SetInt_Target_482);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_482;
	}

	private void Relay_In_484()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_484.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_484, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_484, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_484.In(ref logic_uScript_SetTechsTeam_techs_484, logic_uScript_SetTechsTeam_team_484);
		local_CubeTechs_TankArray = logic_uScript_SetTechsTeam_techs_484;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_484.Out)
		{
			Relay_In_488();
		}
	}

	private void Relay_In_486()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_486.In(logic_uScriptAct_SetInt_Value_486, out logic_uScriptAct_SetInt_Target_486);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_486;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_486.Out)
		{
			Relay_In_598();
		}
	}

	private void Relay_In_488()
	{
		logic_uScript_SetEncounterTarget_owner_488 = owner_Connection_485;
		logic_uScript_SetEncounterTarget_visibleObject_488 = local_CrazedTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_488.In(logic_uScript_SetEncounterTarget_owner_488, logic_uScript_SetEncounterTarget_visibleObject_488);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_488.Out)
		{
			Relay_In_592();
		}
	}

	private void Relay_In_491()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_491.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_491.Out)
		{
			Relay_In_494();
		}
	}

	private void Relay_In_494()
	{
		int num = 0;
		Array array = local_CubeTechs_TankArray;
		if (logic_uScript_SetTechsTeam_techs_494.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsTeam_techs_494, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsTeam_techs_494, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsTeam_uScript_SetTechsTeam_494.In(ref logic_uScript_SetTechsTeam_techs_494, logic_uScript_SetTechsTeam_team_494);
		local_CubeTechs_TankArray = logic_uScript_SetTechsTeam_techs_494;
		if (logic_uScript_SetTechsTeam_uScript_SetTechsTeam_494.Out)
		{
			Relay_In_495();
		}
	}

	private void Relay_In_495()
	{
		logic_uScript_SetEncounterTarget_owner_495 = owner_Connection_492;
		logic_uScript_SetEncounterTarget_visibleObject_495 = local_CrazedTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_495.In(logic_uScript_SetEncounterTarget_owner_495, logic_uScript_SetEncounterTarget_visibleObject_495);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_495.Out)
		{
			Relay_In_604();
		}
	}

	private void Relay_In_498()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_498.In(logic_uScriptAct_SetInt_Value_498, out logic_uScriptAct_SetInt_Target_498);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_498;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_498.Out)
		{
			Relay_In_596();
		}
	}

	private void Relay_In_499()
	{
		logic_uScriptCon_CheckIntEquals_A_499 = local_Round_System_Int32;
		logic_uScriptCon_CheckIntEquals_B_499 = local_RoundNoAmbush_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_499.In(logic_uScriptCon_CheckIntEquals_A_499, logic_uScriptCon_CheckIntEquals_B_499);
		bool num = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_499.True;
		bool flag = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_499.False;
		if (num)
		{
			Relay_In_612();
		}
		if (flag)
		{
			Relay_In_623();
		}
	}

	private void Relay_In_502()
	{
		logic_uScript_PlayDialogue_dialogue_502 = CompletelyNoMoreAmbushDialogue;
		logic_uScript_PlayDialogue_progress_502 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_502.In(logic_uScript_PlayDialogue_dialogue_502, ref logic_uScript_PlayDialogue_progress_502);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_502;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_502.Shown)
		{
			Relay_In_300();
		}
	}

	private void Relay_In_506()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_506.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_506.Out)
		{
			Relay_In_346();
		}
	}

	private void Relay_In_507()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_507.In();
		if (logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_507.SinglePlayer)
		{
			Relay_In_331();
		}
	}

	private void Relay_In_508()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_508.In();
		if (logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_508.SinglePlayer)
		{
			Relay_In_151();
		}
	}

	private void Relay_In_509()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_509.In();
		if (logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_509.SinglePlayer)
		{
			Relay_In_460();
		}
	}

	private void Relay_In_510()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_510.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_510.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_510.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_514();
		}
		if (multiplayer)
		{
			Relay_In_512();
		}
	}

	private void Relay_In_512()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_512 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_512 = CubeAreaTrigger;
		logic_uScript_IsPlayerInTriggerSmart_inside_512 = local_InRange_System_Boolean;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_512.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_512, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_512, ref logic_uScript_IsPlayerInTriggerSmart_inside_512);
		local_InRange_System_Boolean = logic_uScript_IsPlayerInTriggerSmart_inside_512;
		bool allInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_512.AllInside;
		bool allOutside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_512.AllOutside;
		bool someInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_512.SomeInside;
		if (allInside)
		{
			Relay_In_516();
		}
		if (allOutside)
		{
			Relay_In_519();
		}
		if (someInside)
		{
			Relay_In_516();
		}
	}

	private void Relay_In_514()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_514.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_514.Out)
		{
			Relay_In_321();
		}
	}

	private void Relay_In_516()
	{
		logic_uScript_AddMessage_messageData_516 = MsgMultiplayerLeaveArea;
		logic_uScript_AddMessage_speaker_516 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_516 = logic_uScript_AddMessage_uScript_AddMessage_516.In(logic_uScript_AddMessage_messageData_516, logic_uScript_AddMessage_speaker_516);
	}

	private void Relay_In_519()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_519.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_519.Out)
		{
			Relay_In_520();
		}
	}

	private void Relay_In_520()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_520.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_520.Out)
		{
			Relay_In_321();
		}
	}

	private void Relay_In_524()
	{
		logic_uScript_AddMessage_messageData_524 = MsgLeaderTryAgainFollow;
		logic_uScript_AddMessage_speaker_524 = CrazedLeaderSpeaker;
		logic_uScript_AddMessage_Return_524 = logic_uScript_AddMessage_uScript_AddMessage_524.In(logic_uScript_AddMessage_messageData_524, logic_uScript_AddMessage_speaker_524);
		if (logic_uScript_AddMessage_uScript_AddMessage_524.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_In_525()
	{
		logic_uScript_PlayDialogue_dialogue_525 = LeaderTryAgainDialogue;
		logic_uScript_PlayDialogue_progress_525 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_525.In(logic_uScript_PlayDialogue_dialogue_525, ref logic_uScript_PlayDialogue_progress_525);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_525;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_525.Shown)
		{
			Relay_In_524();
		}
	}

	private void Relay_Save_Out_531()
	{
		Relay_Save_532();
	}

	private void Relay_Load_Out_531()
	{
		Relay_Load_532();
	}

	private void Relay_Restart_Out_531()
	{
		Relay_Set_False_532();
	}

	private void Relay_Save_531()
	{
		logic_SubGraph_SaveLoadBool_boolean_531 = local_FillerMsgPlayed01_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_531 = local_FillerMsgPlayed01_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Save(ref logic_SubGraph_SaveLoadBool_boolean_531, logic_SubGraph_SaveLoadBool_boolAsVariable_531, logic_SubGraph_SaveLoadBool_uniqueID_531);
	}

	private void Relay_Load_531()
	{
		logic_SubGraph_SaveLoadBool_boolean_531 = local_FillerMsgPlayed01_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_531 = local_FillerMsgPlayed01_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Load(ref logic_SubGraph_SaveLoadBool_boolean_531, logic_SubGraph_SaveLoadBool_boolAsVariable_531, logic_SubGraph_SaveLoadBool_uniqueID_531);
	}

	private void Relay_Set_True_531()
	{
		logic_SubGraph_SaveLoadBool_boolean_531 = local_FillerMsgPlayed01_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_531 = local_FillerMsgPlayed01_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_531, logic_SubGraph_SaveLoadBool_boolAsVariable_531, logic_SubGraph_SaveLoadBool_uniqueID_531);
	}

	private void Relay_Set_False_531()
	{
		logic_SubGraph_SaveLoadBool_boolean_531 = local_FillerMsgPlayed01_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_531 = local_FillerMsgPlayed01_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_531.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_531, logic_SubGraph_SaveLoadBool_boolAsVariable_531, logic_SubGraph_SaveLoadBool_uniqueID_531);
	}

	private void Relay_Save_Out_532()
	{
		Relay_Save_533();
	}

	private void Relay_Load_Out_532()
	{
		Relay_Load_533();
	}

	private void Relay_Restart_Out_532()
	{
		Relay_Set_False_533();
	}

	private void Relay_Save_532()
	{
		logic_SubGraph_SaveLoadBool_boolean_532 = local_FillerMsgPlayed02_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_532 = local_FillerMsgPlayed02_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.Save(ref logic_SubGraph_SaveLoadBool_boolean_532, logic_SubGraph_SaveLoadBool_boolAsVariable_532, logic_SubGraph_SaveLoadBool_uniqueID_532);
	}

	private void Relay_Load_532()
	{
		logic_SubGraph_SaveLoadBool_boolean_532 = local_FillerMsgPlayed02_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_532 = local_FillerMsgPlayed02_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.Load(ref logic_SubGraph_SaveLoadBool_boolean_532, logic_SubGraph_SaveLoadBool_boolAsVariable_532, logic_SubGraph_SaveLoadBool_uniqueID_532);
	}

	private void Relay_Set_True_532()
	{
		logic_SubGraph_SaveLoadBool_boolean_532 = local_FillerMsgPlayed02_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_532 = local_FillerMsgPlayed02_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_532, logic_SubGraph_SaveLoadBool_boolAsVariable_532, logic_SubGraph_SaveLoadBool_uniqueID_532);
	}

	private void Relay_Set_False_532()
	{
		logic_SubGraph_SaveLoadBool_boolean_532 = local_FillerMsgPlayed02_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_532 = local_FillerMsgPlayed02_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_532.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_532, logic_SubGraph_SaveLoadBool_boolAsVariable_532, logic_SubGraph_SaveLoadBool_uniqueID_532);
	}

	private void Relay_Save_Out_533()
	{
	}

	private void Relay_Load_Out_533()
	{
		Relay_In_553();
	}

	private void Relay_Restart_Out_533()
	{
	}

	private void Relay_Save_533()
	{
		logic_SubGraph_SaveLoadBool_boolean_533 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_533 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.Save(ref logic_SubGraph_SaveLoadBool_boolean_533, logic_SubGraph_SaveLoadBool_boolAsVariable_533, logic_SubGraph_SaveLoadBool_uniqueID_533);
	}

	private void Relay_Load_533()
	{
		logic_SubGraph_SaveLoadBool_boolean_533 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_533 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.Load(ref logic_SubGraph_SaveLoadBool_boolean_533, logic_SubGraph_SaveLoadBool_boolAsVariable_533, logic_SubGraph_SaveLoadBool_uniqueID_533);
	}

	private void Relay_Set_True_533()
	{
		logic_SubGraph_SaveLoadBool_boolean_533 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_533 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_533, logic_SubGraph_SaveLoadBool_boolAsVariable_533, logic_SubGraph_SaveLoadBool_uniqueID_533);
	}

	private void Relay_Set_False_533()
	{
		logic_SubGraph_SaveLoadBool_boolean_533 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_533 = local_CubeTooEarlyMsgPlayed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_533.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_533, logic_SubGraph_SaveLoadBool_boolAsVariable_533, logic_SubGraph_SaveLoadBool_uniqueID_533);
	}

	private void Relay_Pause_534()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_534.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_534.Out)
		{
			Relay_In_328();
		}
	}

	private void Relay_UnPause_534()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_534.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_534.Out)
		{
			Relay_In_328();
		}
	}

	private void Relay_In_535()
	{
		logic_uScriptCon_CompareBool_Bool_535 = local_CrazedTechAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_535.In(logic_uScriptCon_CompareBool_Bool_535);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_535.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_535.False;
		if (num)
		{
			Relay_Pause_534();
		}
		if (flag)
		{
			Relay_UnPause_534();
		}
	}

	private void Relay_In_537()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_537.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_537.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_537.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_460();
		}
		if (multiplayer)
		{
			Relay_In_232();
		}
	}

	private void Relay_In_538()
	{
		logic_uScriptCon_CompareBool_Bool_538 = local_CrazedTechAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_538.In(logic_uScriptCon_CompareBool_Bool_538);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_538.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_538.False;
		if (num)
		{
			Relay_In_541();
		}
		if (flag)
		{
			Relay_In_551();
		}
	}

	private void Relay_In_539()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_539.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_539.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_539.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_538();
		}
		if (multiplayer)
		{
			Relay_In_552();
		}
	}

	private void Relay_In_541()
	{
		logic_uScriptCon_CompareBool_Bool_541 = local_CrazedMinionsAllDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_541.In(logic_uScriptCon_CompareBool_Bool_541);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_541.True)
		{
			Relay_In_547();
		}
	}

	private void Relay_True_543()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_543.True(out logic_uScriptAct_SetBool_Target_543);
		local_FinalDialoguePlayed_System_Boolean = logic_uScriptAct_SetBool_Target_543;
	}

	private void Relay_False_543()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_543.False(out logic_uScriptAct_SetBool_Target_543);
		local_FinalDialoguePlayed_System_Boolean = logic_uScriptAct_SetBool_Target_543;
	}

	private void Relay_In_545()
	{
		logic_uScript_PlayDialogue_dialogue_545 = MsgFinalCubeDeathDialogue;
		logic_uScript_PlayDialogue_progress_545 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_545.In(logic_uScript_PlayDialogue_dialogue_545, ref logic_uScript_PlayDialogue_progress_545);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_545;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_545.Out)
		{
			Relay_True_543();
		}
	}

	private void Relay_In_547()
	{
		logic_uScriptCon_CompareBool_Bool_547 = local_FinalDialoguePlayed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_547.In(logic_uScriptCon_CompareBool_Bool_547);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_547.False)
		{
			Relay_In_545();
		}
	}

	private void Relay_In_549()
	{
		logic_uScriptCon_CompareBool_Bool_549 = local_CrazedTechAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_549.In(logic_uScriptCon_CompareBool_Bool_549);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_549.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_549.False;
		if (num)
		{
			Relay_In_478();
		}
		if (flag)
		{
			Relay_In_551();
		}
	}

	private void Relay_In_551()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_551.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_551.Out)
		{
			Relay_In_406();
		}
	}

	private void Relay_In_552()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_552.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_552.Out)
		{
			Relay_In_551();
		}
	}

	private void Relay_Out_553()
	{
	}

	private void Relay_In_553()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_553 = local_Objective_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_553.In(logic_SubGraph_LoadObjectiveStates_currentObjective_553);
	}

	private void Relay_Save_Out_558()
	{
		Relay_Save_159();
	}

	private void Relay_Load_Out_558()
	{
		Relay_Load_159();
	}

	private void Relay_Restart_Out_558()
	{
		Relay_Set_False_159();
	}

	private void Relay_Save_558()
	{
		logic_SubGraph_SaveLoadInt_integer_558 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_558 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558.Save(logic_SubGraph_SaveLoadInt_restartValue_558, ref logic_SubGraph_SaveLoadInt_integer_558, logic_SubGraph_SaveLoadInt_intAsVariable_558, logic_SubGraph_SaveLoadInt_uniqueID_558);
	}

	private void Relay_Load_558()
	{
		logic_SubGraph_SaveLoadInt_integer_558 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_558 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558.Load(logic_SubGraph_SaveLoadInt_restartValue_558, ref logic_SubGraph_SaveLoadInt_integer_558, logic_SubGraph_SaveLoadInt_intAsVariable_558, logic_SubGraph_SaveLoadInt_uniqueID_558);
	}

	private void Relay_Restart_558()
	{
		logic_SubGraph_SaveLoadInt_integer_558 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_558 = local_Objective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_558.Restart(logic_SubGraph_SaveLoadInt_restartValue_558, ref logic_SubGraph_SaveLoadInt_integer_558, logic_SubGraph_SaveLoadInt_intAsVariable_558, logic_SubGraph_SaveLoadInt_uniqueID_558);
	}

	private void Relay_In_560()
	{
		logic_uScript_PlayDialogue_dialogue_560 = StartBossFightDialogue;
		logic_uScript_PlayDialogue_progress_560 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_560.In(logic_uScript_PlayDialogue_dialogue_560, ref logic_uScript_PlayDialogue_progress_560);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_560;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_560.Shown)
		{
			Relay_In_173();
		}
	}

	private void Relay_Out_568()
	{
		Relay_In_374();
	}

	private void Relay_In_568()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_568 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_568.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_568, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_568);
	}

	private void Relay_Out_569()
	{
		Relay_In_138();
	}

	private void Relay_In_569()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_569 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_569.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_569, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_569);
	}

	private void Relay_Out_572()
	{
		Relay_In_411();
	}

	private void Relay_In_572()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_572 = local_Objective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_572.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_572, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_572);
	}

	private void Relay_In_574()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_574.In(logic_uScriptAct_SetInt_Value_574, out logic_uScriptAct_SetInt_Target_574);
		local_Round_System_Int32 = logic_uScriptAct_SetInt_Target_574;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_574.Out)
		{
			Relay_In_310();
		}
	}

	private void Relay_In_575()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_575.In(logic_uScriptAct_SetInt_Value_575, out logic_uScriptAct_SetInt_Target_575);
		local_Round_System_Int32 = logic_uScriptAct_SetInt_Target_575;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_575.Out)
		{
			Relay_In_445();
		}
	}

	private void Relay_In_577()
	{
		logic_uScript_PlayDialogue_dialogue_577 = SpawnAmbushDeadDialogue;
		logic_uScript_PlayDialogue_progress_577 = local_DialogueProgressExtra_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_577.In(logic_uScript_PlayDialogue_dialogue_577, ref logic_uScript_PlayDialogue_progress_577);
		local_DialogueProgressExtra_System_Int32 = logic_uScript_PlayDialogue_progress_577;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_577.Shown)
		{
			Relay_In_292();
		}
	}

	private void Relay_Save_Out_580()
	{
		Relay_Save_157();
	}

	private void Relay_Load_Out_580()
	{
		Relay_Load_157();
	}

	private void Relay_Restart_Out_580()
	{
		Relay_Restart_157();
	}

	private void Relay_Save_580()
	{
		logic_SubGraph_SaveLoadInt_integer_580 = local_DialogueProgressExtra_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_580 = local_DialogueProgressExtra_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580.Save(logic_SubGraph_SaveLoadInt_restartValue_580, ref logic_SubGraph_SaveLoadInt_integer_580, logic_SubGraph_SaveLoadInt_intAsVariable_580, logic_SubGraph_SaveLoadInt_uniqueID_580);
	}

	private void Relay_Load_580()
	{
		logic_SubGraph_SaveLoadInt_integer_580 = local_DialogueProgressExtra_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_580 = local_DialogueProgressExtra_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580.Load(logic_SubGraph_SaveLoadInt_restartValue_580, ref logic_SubGraph_SaveLoadInt_integer_580, logic_SubGraph_SaveLoadInt_intAsVariable_580, logic_SubGraph_SaveLoadInt_uniqueID_580);
	}

	private void Relay_Restart_580()
	{
		logic_SubGraph_SaveLoadInt_integer_580 = local_DialogueProgressExtra_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_580 = local_DialogueProgressExtra_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_580.Restart(logic_SubGraph_SaveLoadInt_restartValue_580, ref logic_SubGraph_SaveLoadInt_integer_580, logic_SubGraph_SaveLoadInt_intAsVariable_580, logic_SubGraph_SaveLoadInt_uniqueID_580);
	}

	private void Relay_In_582()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_582.In(logic_uScriptAct_SetInt_Value_582, out logic_uScriptAct_SetInt_Target_582);
		local_DialogueProgressExtra_System_Int32 = logic_uScriptAct_SetInt_Target_582;
	}

	private void Relay_In_586()
	{
		logic_uScript_PlayDialogue_dialogue_586 = OutofTimeDialogue;
		logic_uScript_PlayDialogue_progress_586 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_586.In(logic_uScript_PlayDialogue_dialogue_586, ref logic_uScript_PlayDialogue_progress_586);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_586;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_586.Shown)
		{
			Relay_In_486();
		}
	}

	private void Relay_In_589()
	{
		logic_uScript_PlayDialogue_dialogue_589 = CubeLeaveAreaFailDialogue;
		logic_uScript_PlayDialogue_progress_589 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_589.In(logic_uScript_PlayDialogue_dialogue_589, ref logic_uScript_PlayDialogue_progress_589);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_589;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_589.Shown)
		{
			Relay_In_498();
		}
	}

	private void Relay_In_592()
	{
		logic_uScriptCon_CheckIntEquals_A_592 = local_Round_System_Int32;
		logic_uScriptCon_CheckIntEquals_B_592 = local_RoundSpawnAmbush_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_592.In(logic_uScriptCon_CheckIntEquals_A_592, logic_uScriptCon_CheckIntEquals_B_592);
		bool num = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_592.True;
		bool flag = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_592.False;
		if (num)
		{
			Relay_In_593();
		}
		if (flag)
		{
			Relay_In_586();
		}
	}

	private void Relay_In_593()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_593.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_593.Out)
		{
			Relay_In_486();
		}
	}

	private void Relay_In_596()
	{
		logic_uScriptAct_SetInt_Value_596 = local_StageCubeBattleSetupAmbush_System_Int32;
		logic_uScriptAct_SetInt_uScriptAct_SetInt_596.In(logic_uScriptAct_SetInt_Value_596, out logic_uScriptAct_SetInt_Target_596);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_596;
	}

	private void Relay_In_598()
	{
		logic_uScriptAct_SetInt_Value_598 = local_StageCubeBattleSetupAmbush_System_Int32;
		logic_uScriptAct_SetInt_uScriptAct_SetInt_598.In(logic_uScriptAct_SetInt_Value_598, out logic_uScriptAct_SetInt_Target_598);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_598;
	}

	private void Relay_In_604()
	{
		logic_uScriptCon_CheckIntEquals_A_604 = local_Round_System_Int32;
		logic_uScriptCon_CheckIntEquals_B_604 = local_RoundSpawnAmbush_System_Int32;
		logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_604.In(logic_uScriptCon_CheckIntEquals_A_604, logic_uScriptCon_CheckIntEquals_B_604);
		bool num = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_604.True;
		bool flag = logic_uScriptCon_CheckIntEquals_uScriptCon_CheckIntEquals_604.False;
		if (num)
		{
			Relay_In_609();
		}
		if (flag)
		{
			Relay_In_611();
		}
	}

	private void Relay_In_607()
	{
		logic_uScript_PlayDialogue_dialogue_607 = OutofTimeDialogue;
		logic_uScript_PlayDialogue_progress_607 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_607.In(logic_uScript_PlayDialogue_dialogue_607, ref logic_uScript_PlayDialogue_progress_607);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_607;
	}

	private void Relay_In_609()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_609.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_609.Out)
		{
			Relay_In_611();
		}
	}

	private void Relay_In_610()
	{
		logic_uScriptAct_SetInt_Value_610 = local_StageCubeBattleSetupAmbush_System_Int32;
		logic_uScriptAct_SetInt_uScriptAct_SetInt_610.In(logic_uScriptAct_SetInt_Value_610, out logic_uScriptAct_SetInt_Target_610);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_610;
	}

	private void Relay_In_611()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_611.In(logic_uScriptAct_SetInt_Value_611, out logic_uScriptAct_SetInt_Target_611);
		local_DialogueProgress_System_Int32 = logic_uScriptAct_SetInt_Target_611;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_611.Out)
		{
			Relay_In_610();
		}
	}

	private void Relay_In_612()
	{
		logic_uScriptCon_CompareBool_Bool_612 = local_InRange_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_612.In(logic_uScriptCon_CompareBool_Bool_612);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_612.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_612.False;
		if (num)
		{
			Relay_In_289();
		}
		if (flag)
		{
			Relay_In_614();
		}
	}

	private void Relay_In_614()
	{
		logic_uScript_PlayDialogue_dialogue_614 = CubeLeaveAreaFailDialogue;
		logic_uScript_PlayDialogue_progress_614 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_614.In(logic_uScript_PlayDialogue_dialogue_614, ref logic_uScript_PlayDialogue_progress_614);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_614;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_614.Shown)
		{
			Relay_In_289();
		}
	}

	private void Relay_In_620()
	{
		logic_uScriptCon_CompareBool_Bool_620 = local_InRange_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_620.In(logic_uScriptCon_CompareBool_Bool_620);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_620.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_620.False;
		if (num)
		{
			Relay_In_285();
		}
		if (flag)
		{
			Relay_In_621();
		}
	}

	private void Relay_In_621()
	{
		logic_uScript_PlayDialogue_dialogue_621 = CubeLeaveAreaFailDialogue;
		logic_uScript_PlayDialogue_progress_621 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_621.In(logic_uScript_PlayDialogue_dialogue_621, ref logic_uScript_PlayDialogue_progress_621);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_621;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_621.Shown)
		{
			Relay_In_285();
		}
	}

	private void Relay_In_623()
	{
		logic_uScriptCon_CompareBool_Bool_623 = local_InRange_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_623.In(logic_uScriptCon_CompareBool_Bool_623);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_623.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_623.False;
		if (num)
		{
			Relay_In_502();
		}
		if (flag)
		{
			Relay_In_624();
		}
	}

	private void Relay_In_624()
	{
		logic_uScript_PlayDialogue_dialogue_624 = CubeLeaveAreaFailDialogue;
		logic_uScript_PlayDialogue_progress_624 = local_DialogueProgress_System_Int32;
		logic_uScript_PlayDialogue_uScript_PlayDialogue_624.In(logic_uScript_PlayDialogue_dialogue_624, ref logic_uScript_PlayDialogue_progress_624);
		local_DialogueProgress_System_Int32 = logic_uScript_PlayDialogue_progress_624;
		if (logic_uScript_PlayDialogue_uScript_PlayDialogue_624.Shown)
		{
			Relay_In_502();
		}
	}
}
