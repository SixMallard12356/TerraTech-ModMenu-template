using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_SetPiece_MineIslandRescue_01 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public BlockTypes blockTypeToAttach;

	public SpawnTechData[] BossSpawnData = new SpawnTechData[0];

	public TankPreset CompletedNPCPreset;

	public GhostBlockSpawnData[] ghostBlock = new GhostBlockSpawnData[0];

	public SpawnBlockData[] KeyBlock = new SpawnBlockData[0];

	private bool local_ArrivedAtMineIsland_System_Boolean;

	private bool local_AttemptedAlready_System_Boolean;

	private bool local_BlockAttachedToNPC_System_Boolean;

	private TankBlock local_BoosterBlock_TankBlock;

	private bool local_BossDead_System_Boolean;

	private Vector3 local_BossLastKnownPosition_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private bool local_BossResponds_System_Boolean;

	private Tank local_BossTech_Tank;

	private Tank[] local_BossTechs_TankArray = new Tank[0];

	private bool local_BossTriggered_System_Boolean;

	private bool local_EnemyHitAMine_System_Boolean;

	private TankBlock local_GhostBlock_TankBlock;

	private TankBlock[] local_GhostBlocks_TankBlockArray = new TankBlock[0];

	private bool local_GhostBlockSpawned_System_Boolean;

	private TankBlock[] local_KeyBlocks_TankBlockArray = new TankBlock[0];

	private bool local_MetBoss_System_Boolean;

	private Tank local_MinefieldTech_Tank;

	private bool local_msgArrivedAtMineIslandShown_System_Boolean;

	private bool local_MsgBackAgain_System_Boolean;

	private bool local_msgBossDeadShown_System_Boolean;

	private bool local_msgHowDareYouShown_System_Boolean;

	private bool local_msgIAmRepulsorShown_System_Boolean;

	private bool local_msgIntroShown_System_Boolean;

	private bool local_msgNearBrokenBridgeShown_System_Boolean;

	private bool local_msgNearLongJumpShown_System_Boolean;

	private bool local_msgNothingToSeeHereShown_System_Boolean;

	private bool local_msgReachedMineIslandShown_System_Boolean;

	private bool local_NPCCallsForHelp_System_Boolean;

	private Tank local_NPCTech_Tank;

	private Tank[] local_NPCTechs_TankArray = new Tank[0];

	private bool local_NPCToBuildSet_System_Boolean;

	private bool local_PlayerHitAMine_System_Boolean;

	private int local_PlayerTeam_System_Int32;

	private bool local_ReachedMineIsland_System_Boolean;

	private Tank local_RepulsorTech_Tank;

	private Tank[] local_RepulsorTechs_TankArray = new Tank[0];

	private bool local_SpawnedBooster_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private Tank[] local_TechsInMinefield_TankArray = new Tank[0];

	private bool local_TechsSetup_System_Boolean;

	private bool local_TechsSpawned_System_Boolean;

	public uScript_AddMessage.MessageSpeaker messageBossSpeaker;

	public uScript_AddMessage.MessageSpeaker messageNPCSpeaker;

	public uScript_AddMessage.MessageSpeaker messageRepulsorSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	[Multiline(3)]
	public string MinefieldTrigger = "";

	[Multiline(3)]
	public string MineIslandTrigger = "";

	public uScript_AddMessage.MessageData msgArrivedAtMineIsland;

	public uScript_AddMessage.MessageData msgAttachBlock;

	public uScript_AddMessage.MessageData msgBackAgain;

	public uScript_AddMessage.MessageData msgBossDead;

	public uScript_AddMessage.MessageData msgBossResponds;

	public uScript_AddMessage.MessageData msgComplete;

	public uScript_AddMessage.MessageData msgEnemyHitAMine;

	public uScript_AddMessage.MessageData msgHowDareYou;

	public uScript_AddMessage.MessageData msgIAmRepulsor;

	public uScript_AddMessage.MessageData msgMeetBoss;

	public uScript_AddMessage.MessageData msgMeetNPC;

	public uScript_AddMessage.MessageData msgNearBrokenBridge;

	public uScript_AddMessage.MessageData msgNearLongJump;

	public uScript_AddMessage.MessageData msgNothingToSeeHere;

	public uScript_AddMessage.MessageData msgNPCCallsForHelp;

	public uScript_AddMessage.MessageData msgPlayerHitAMine;

	public uScript_AddMessage.MessageData msgReachedMineIsland;

	public uScript_AddMessage.MessageData msgRepulsorBaseline;

	public uScript_AddMessage.MessageData msgShutUpWimpy;

	[Multiline(3)]
	public string NearBrokenBridgeTrigger = "";

	[Multiline(3)]
	public string NearLongJumpTrigger = "";

	[Multiline(3)]
	public string NearRepulsorTrigger = "";

	public ExternalBehaviorTree NPCFlyAwayBehaviour;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	public SpawnTechData[] RepulsorSpawnData = new SpawnTechData[0];

	[Multiline(3)]
	public string TagRepulsor = "";

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_21;

	private GameObject owner_Connection_37;

	private GameObject owner_Connection_46;

	private GameObject owner_Connection_51;

	private GameObject owner_Connection_96;

	private GameObject owner_Connection_107;

	private GameObject owner_Connection_114;

	private GameObject owner_Connection_115;

	private GameObject owner_Connection_155;

	private GameObject owner_Connection_241;

	private GameObject owner_Connection_245;

	private GameObject owner_Connection_330;

	private GameObject owner_Connection_333;

	private GameObject owner_Connection_354;

	private GameObject owner_Connection_355;

	private GameObject owner_Connection_368;

	private GameObject owner_Connection_374;

	private GameObject owner_Connection_384;

	private GameObject owner_Connection_386;

	private GameObject owner_Connection_397;

	private GameObject owner_Connection_440;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_5 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_5 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_5 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_5 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_6;

	private float logic_uScript_IsPlayerInRangeOfTech_range_6 = 75f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_6 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_6 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_6 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_6 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_7;

	private bool logic_uScriptCon_CompareBool_True_7 = true;

	private bool logic_uScriptCon_CompareBool_False_7 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_8;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_8 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_8 = "msgIntroShown";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_10 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_10;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_10;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_10;

	private bool logic_uScript_AddMessage_Out_10 = true;

	private bool logic_uScript_AddMessage_Shown_10 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_12 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_12;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_12;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_12;

	private bool logic_uScript_AddMessage_Out_12 = true;

	private bool logic_uScript_AddMessage_Shown_12 = true;

	private uScript_DoesTechHaveBlockAtPosition logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_15 = new uScript_DoesTechHaveBlockAtPosition();

	private Tank logic_uScript_DoesTechHaveBlockAtPosition_tech_15;

	private BlockTypes logic_uScript_DoesTechHaveBlockAtPosition_blockType_15;

	private Vector3 logic_uScript_DoesTechHaveBlockAtPosition_localPosition_15 = new Vector3(-2f, 3f, 0f);

	private bool logic_uScript_DoesTechHaveBlockAtPosition_True_15 = true;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_False_15 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_18;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_18 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_18 = "TechsSpawned";

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_19 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_19 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_19;

	private TankBlock logic_uScript_AccessListBlock_value_19;

	private bool logic_uScript_AccessListBlock_Out_19 = true;

	private uScript_RemoveAllGhostBlocksOnTech logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_20 = new uScript_RemoveAllGhostBlocksOnTech();

	private Tank logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_20;

	private bool logic_uScript_RemoveAllGhostBlocksOnTech_Out_20 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_23 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_23 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_23;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_23;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_23;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_23 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_23 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_24 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_24;

	private bool logic_uScriptAct_SetBool_Out_24 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_24 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_24 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_26;

	private bool logic_uScriptCon_CompareBool_True_26 = true;

	private bool logic_uScriptCon_CompareBool_False_26 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_29 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_29;

	private bool logic_uScriptAct_SetBool_Out_29 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_29 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_29 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_32;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_32 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_32 = "TechsSetup";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_33;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_33 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_33 = "NPCToBuildSet";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_34;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_34 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_34 = "BlockAttachedToNPC";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_35;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_35 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_35 = "GhostBlockSpawned";

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_39 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_39;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_39 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_40 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_40;

	private bool logic_uScriptAct_SetBool_Out_40 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_40 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_40 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_41 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_41;

	private bool logic_uScriptCon_CompareBool_True_41 = true;

	private bool logic_uScriptCon_CompareBool_False_41 = true;

	private uScript_SetTutorialTechToBuild logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_43 = new uScript_SetTutorialTechToBuild();

	private TankPreset logic_uScript_SetTutorialTechToBuild_completedTechPreset_43;

	private Tank logic_uScript_SetTutorialTechToBuild_tutorialBuildTech_43;

	private bool logic_uScript_SetTutorialTechToBuild_Out_43 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_44 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_44 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_44;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_44 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_44;

	private bool logic_uScript_SpawnTechsFromData_Out_44 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_45 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_45;

	private bool logic_uScriptAct_SetBool_Out_45 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_45 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_45 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_47;

	private bool logic_uScriptCon_CompareBool_True_47 = true;

	private bool logic_uScriptCon_CompareBool_False_47 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_50 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_50 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_50;

	private bool logic_uScript_SetTankInvulnerable_Out_50 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_53 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_53 = new Tank[0];

	private int logic_uScript_AccessListTech_index_53;

	private Tank logic_uScript_AccessListTech_value_53;

	private bool logic_uScript_AccessListTech_Out_53 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_54 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_54 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_54;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_54 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_54;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_54 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_54 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_54 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_54 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_58;

	private bool logic_uScriptCon_CompareBool_True_58 = true;

	private bool logic_uScriptCon_CompareBool_False_58 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_59 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_59;

	private bool logic_uScriptAct_SetBool_Out_59 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_59 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_59 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_68 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_68 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_68;

	private bool logic_uScript_SetTankHideBlockLimit_Out_68 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_69 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_69;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_69 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_69 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_74 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_74;

	private bool logic_uScriptAct_SetBool_Out_74 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_74 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_74 = true;

	private uScript_IsTechInTrigger logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_76 = new uScript_IsTechInTrigger();

	private string logic_uScript_IsTechInTrigger_triggerAreaName_76 = "";

	private Tank[] logic_uScript_IsTechInTrigger_techs_76 = new Tank[0];

	private bool logic_uScript_IsTechInTrigger_Out_76 = true;

	private bool logic_uScript_IsTechInTrigger_InRange_76 = true;

	private bool logic_uScript_IsTechInTrigger_OutOfRange_76 = true;

	private uScript_IsTechPlayer logic_uScript_IsTechPlayer_uScript_IsTechPlayer_79 = new uScript_IsTechPlayer();

	private Tank logic_uScript_IsTechPlayer_tech_79;

	private bool logic_uScript_IsTechPlayer_Out_79 = true;

	private bool logic_uScript_IsTechPlayer_True_79 = true;

	private bool logic_uScript_IsTechPlayer_False_79 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_80 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_80 = new Tank[0];

	private int logic_uScript_AccessListTech_index_80;

	private Tank logic_uScript_AccessListTech_value_80;

	private bool logic_uScript_AccessListTech_Out_80 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_82 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_82 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_82 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_82;

	private float logic_uScript_DamageTechs_leaveBlksPercent_82;

	private bool logic_uScript_DamageTechs_makeVulnerable_82;

	private bool logic_uScript_DamageTechs_Out_82 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_83 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_83 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_83 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_83;

	private float logic_uScript_DamageTechs_leaveBlksPercent_83;

	private bool logic_uScript_DamageTechs_makeVulnerable_83;

	private bool logic_uScript_DamageTechs_Out_83 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_86 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_86;

	private bool logic_uScriptAct_SetBool_Out_86 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_86 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_86 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_88 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_88;

	private bool logic_uScriptAct_SetBool_Out_88 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_88 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_88 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_92;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_92 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_92 = "PlayerHitAMine";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_93;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_93 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_93 = "EnemyHitAMine";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_94 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_94 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_95 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_95;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_95 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_95 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_95 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_97 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_97;

	private bool logic_uScriptAct_SetBool_Out_97 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_97 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_97 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_99 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_99;

	private bool logic_uScriptAct_SetBool_Out_99 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_99 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_99 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_101;

	private bool logic_uScriptCon_CompareBool_True_101 = true;

	private bool logic_uScriptCon_CompareBool_False_101 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_105;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_105 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_105 = "ReachedMineIsland";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_106;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_106 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_106 = "ArrivedAtMineIsland";

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_109 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_109 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_109;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_109 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_109;

	private bool logic_uScript_SpawnTechsFromData_Out_109 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_111 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_111 = new Tank[0];

	private int logic_uScript_AccessListTech_index_111;

	private Tank logic_uScript_AccessListTech_value_111;

	private bool logic_uScript_AccessListTech_Out_111 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_112 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_112;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_112 = 1;

	private bool logic_uScript_SetCustomRadarTeamID_Out_112 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_113 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_113;

	private object logic_uScript_SetEncounterTarget_visibleObject_113 = "";

	private bool logic_uScript_SetEncounterTarget_Out_113 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_116 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_116;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_116 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_116;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_116 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_116 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_116 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_116 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_121 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_121 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_122 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_122;

	private float logic_uScript_IsPlayerInRangeOfTech_range_122 = 75f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_122 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_122 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_122 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_122 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_123 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_123;

	private bool logic_uScriptCon_CompareBool_True_123 = true;

	private bool logic_uScriptCon_CompareBool_False_123 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_125 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_125;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_125;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_125;

	private bool logic_uScript_AddMessage_Out_125 = true;

	private bool logic_uScript_AddMessage_Shown_125 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_126 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_126;

	private bool logic_uScriptAct_SetBool_Out_126 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_126 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_126 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_131;

	private bool logic_uScriptCon_CompareBool_True_131 = true;

	private bool logic_uScriptCon_CompareBool_False_131 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_132 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_132;

	private bool logic_uScriptAct_SetBool_Out_132 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_132 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_132 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_134 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_134;

	private bool logic_uScriptCon_CompareBool_True_134 = true;

	private bool logic_uScriptCon_CompareBool_False_134 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_137 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_137;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_137;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_137;

	private bool logic_uScript_AddMessage_Out_137 = true;

	private bool logic_uScript_AddMessage_Shown_137 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_140 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_140;

	private bool logic_uScriptAct_SetBool_Out_140 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_140 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_140 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_142 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_142;

	private bool logic_uScriptCon_CompareBool_True_142 = true;

	private bool logic_uScriptCon_CompareBool_False_142 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_143 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_143;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_143;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_143;

	private bool logic_uScript_AddMessage_Out_143 = true;

	private bool logic_uScript_AddMessage_Shown_143 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_149;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_149 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_149 = "MetBoss";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_150;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_150 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_150 = "NPCCallsForHelp";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_151;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_151 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_151 = "BossResponds";

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_154 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_154;

	private int logic_uScript_SetTankTeam_team_154 = 1;

	private bool logic_uScript_SetTankTeam_Out_154 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_157 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_157 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_157;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_157 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_157;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_157 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_157 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_157 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_157 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_158 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_158;

	private bool logic_uScriptCon_CompareBool_True_158 = true;

	private bool logic_uScriptCon_CompareBool_False_158 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_160 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_160;

	private bool logic_uScriptAct_SetBool_Out_160 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_160 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_160 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_162;

	private bool logic_uScriptCon_CompareBool_True_162 = true;

	private bool logic_uScriptCon_CompareBool_False_162 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_164 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_164;

	private bool logic_uScriptAct_SetBool_Out_164 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_164 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_164 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_166 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_166;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_166;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_167;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_167;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_168 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_168;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_168;

	private uScript_GetPositionOfTech logic_uScript_GetPositionOfTech_uScript_GetPositionOfTech_169 = new uScript_GetPositionOfTech();

	private Tank logic_uScript_GetPositionOfTech_tech_169;

	private Vector3 logic_uScript_GetPositionOfTech_Return_169;

	private bool logic_uScript_GetPositionOfTech_Out_169 = true;

	private bool logic_uScript_GetPositionOfTech_TechValid_169 = true;

	private bool logic_uScript_GetPositionOfTech_TechNull_169 = true;

	private uScript_IsVisibleInTrigger logic_uScript_IsVisibleInTrigger_uScript_IsVisibleInTrigger_172 = new uScript_IsVisibleInTrigger();

	private object logic_uScript_IsVisibleInTrigger_visibleObject_172 = "";

	private string logic_uScript_IsVisibleInTrigger_triggerAreaName_172 = "";

	private bool logic_uScript_IsVisibleInTrigger_Out_172 = true;

	private bool logic_uScript_IsVisibleInTrigger_InRange_172 = true;

	private bool logic_uScript_IsVisibleInTrigger_OutOfRange_172 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_175 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_175;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_175 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_175 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_178 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_178;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_178 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_178 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_179 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_179;

	private int logic_uScript_SetTankTeam_team_179;

	private bool logic_uScript_SetTankTeam_Out_179 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_180 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_180;

	private bool logic_uScriptAct_SetBool_Out_180 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_180 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_180 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_183;

	private bool logic_uScriptCon_CompareBool_True_183 = true;

	private bool logic_uScriptCon_CompareBool_False_183 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_184 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_184 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_185 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_185;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_185;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_185;

	private bool logic_uScript_AddMessage_Out_185 = true;

	private bool logic_uScript_AddMessage_Shown_185 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_189;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_189 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_189 = "msgBossDeadShown";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_190 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_190 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_192;

	private bool logic_uScriptCon_CompareBool_True_192 = true;

	private bool logic_uScriptCon_CompareBool_False_192 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_193 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_193;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_193;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_193;

	private bool logic_uScript_AddMessage_Out_193 = true;

	private bool logic_uScript_AddMessage_Shown_193 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_194 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_194;

	private bool logic_uScriptAct_SetBool_Out_194 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_194 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_194 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_197;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_197 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_197 = "msgNothingToSeeHereShown";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_198 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_198;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_198;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_198;

	private bool logic_uScript_AddMessage_Out_198 = true;

	private bool logic_uScript_AddMessage_Shown_198 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_202;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_202 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_202 = "msgHowDareYouShown";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_204 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_204;

	private bool logic_uScriptAct_SetBool_Out_204 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_204 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_204 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_205;

	private bool logic_uScriptCon_CompareBool_True_205 = true;

	private bool logic_uScriptCon_CompareBool_False_205 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_208;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_208 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_208 = "AttemptedAlready";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_209 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_209;

	private bool logic_uScriptCon_CompareBool_True_209 = true;

	private bool logic_uScriptCon_CompareBool_False_209 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_212;

	private bool logic_uScriptCon_CompareBool_True_212 = true;

	private bool logic_uScriptCon_CompareBool_False_212 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_213 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_213;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_213;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_213;

	private bool logic_uScript_AddMessage_Out_213 = true;

	private bool logic_uScript_AddMessage_Shown_213 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_215 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_215;

	private bool logic_uScriptAct_SetBool_Out_215 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_215 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_215 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_220 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_220;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_220;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_220;

	private bool logic_uScript_AddMessage_Out_220 = true;

	private bool logic_uScript_AddMessage_Shown_220 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_223;

	private bool logic_uScriptCon_CompareBool_True_223 = true;

	private bool logic_uScriptCon_CompareBool_False_223 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_224 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_224;

	private bool logic_uScriptAct_SetBool_Out_224 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_224 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_224 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_228 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_228;

	private bool logic_uScriptAct_SetBool_Out_228 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_228 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_228 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_231 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_231;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_231;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_231;

	private bool logic_uScript_AddMessage_Out_231 = true;

	private bool logic_uScript_AddMessage_Shown_231 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_232 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_232;

	private bool logic_uScriptCon_CompareBool_True_232 = true;

	private bool logic_uScriptCon_CompareBool_False_232 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_234 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_234;

	private bool logic_uScriptAct_SetBool_Out_234 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_234 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_234 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_237 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_237;

	private bool logic_uScriptCon_CompareBool_True_237 = true;

	private bool logic_uScriptCon_CompareBool_False_237 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_239;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_239 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_239 = "msgReachedMineIslandShown";

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_240 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_240 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_240;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_240 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_240;

	private bool logic_uScript_SpawnTechsFromData_Out_240 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_243 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_243 = new Tank[0];

	private int logic_uScript_AccessListTech_index_243;

	private Tank logic_uScript_AccessListTech_value_243;

	private bool logic_uScript_AccessListTech_Out_243 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_244 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_244 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_244;

	private bool logic_uScript_SetTankInvulnerable_Out_244 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_246 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_246 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_246;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_246 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_246;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_246 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_246 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_246 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_246 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_251;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_251 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_251 = "msgIAmRepulsorShown";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_253 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_253;

	private bool logic_uScriptCon_CompareBool_True_253 = true;

	private bool logic_uScriptCon_CompareBool_False_253 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_254 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_254;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_254;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_254;

	private bool logic_uScript_AddMessage_Out_254 = true;

	private bool logic_uScript_AddMessage_Shown_254 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_257 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_257;

	private bool logic_uScriptAct_SetBool_Out_257 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_257 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_257 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_259 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_259 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_259 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_259 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_259 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_259 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_259 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_262;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_262 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_262 = "msgNearLongJumpShown";

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_263 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_263 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_263 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_263 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_263 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_263 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_263 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_264 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_264;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_264;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_264;

	private bool logic_uScript_AddMessage_Out_264 = true;

	private bool logic_uScript_AddMessage_Shown_264 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_270 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_270;

	private bool logic_uScriptAct_SetBool_Out_270 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_270 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_270 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_271;

	private bool logic_uScriptCon_CompareBool_True_271 = true;

	private bool logic_uScriptCon_CompareBool_False_271 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_273;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_273 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_273 = "msgNearBrokenBridgeShown";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_274 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_274;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_274;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_274;

	private bool logic_uScript_AddMessage_Out_274 = true;

	private bool logic_uScript_AddMessage_Shown_274 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_279 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_279;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_279;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_279;

	private bool logic_uScript_AddMessage_Out_279 = true;

	private bool logic_uScript_AddMessage_Shown_279 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_280 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_280 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_281 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_281 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_282 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_282 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_283 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_283 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_284 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_284 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_285 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_285 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_286 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_286 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_287 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_287 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_289 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_289;

	private bool logic_uScriptCon_CompareBool_True_289 = true;

	private bool logic_uScriptCon_CompareBool_False_289 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_291 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_291;

	private bool logic_uScriptCon_CompareBool_True_291 = true;

	private bool logic_uScriptCon_CompareBool_False_291 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_292 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_292;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_292 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_292 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_293 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_293;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_293;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_293;

	private bool logic_uScript_AddMessage_Out_293 = true;

	private bool logic_uScript_AddMessage_Shown_293 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_299 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_299;

	private bool logic_uScriptAct_SetBool_Out_299 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_299 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_299 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_301 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_301;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_301;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_301;

	private bool logic_uScript_AddMessage_Out_301 = true;

	private bool logic_uScript_AddMessage_Shown_301 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_303 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_303;

	private bool logic_uScriptAct_SetBool_Out_303 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_303 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_303 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_304 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_304;

	private bool logic_uScriptCon_CompareBool_True_304 = true;

	private bool logic_uScriptCon_CompareBool_False_304 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_305 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_305;

	private bool logic_uScriptCon_CompareBool_True_305 = true;

	private bool logic_uScriptCon_CompareBool_False_305 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_307 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_307 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_309;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_309 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_309 = "msgArrivedAtMineIslandShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_311;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_311 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_311 = "BossDead";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_313;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_313 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_313 = "BossTriggered";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_315;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_315 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_315 = "MsgBackAgain";

	private uScript_IsTechWheelGrounded logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_316 = new uScript_IsTechWheelGrounded();

	private Tank logic_uScript_IsTechWheelGrounded_tech_316;

	private bool logic_uScript_IsTechWheelGrounded_Out_316 = true;

	private bool logic_uScript_IsTechWheelGrounded_True_316 = true;

	private bool logic_uScript_IsTechWheelGrounded_False_316 = true;

	private uScript_IsTechTouchingTerrain logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_317 = new uScript_IsTechTouchingTerrain();

	private Tank logic_uScript_IsTechTouchingTerrain_tech_317;

	private bool logic_uScript_IsTechTouchingTerrain_Out_317 = true;

	private bool logic_uScript_IsTechTouchingTerrain_True_317 = true;

	private bool logic_uScript_IsTechTouchingTerrain_False_317 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_318 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_318 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_320 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_320;

	private bool logic_uScriptCon_CompareBool_True_320 = true;

	private bool logic_uScriptCon_CompareBool_False_320 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_322 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_322 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_323 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_323;

	private bool logic_uScriptAct_SetBool_Out_323 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_323 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_323 = true;

	private uScript_LockVisibleStackAccept logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_325 = new uScript_LockVisibleStackAccept();

	private object logic_uScript_LockVisibleStackAccept_targetObject_325 = "";

	private bool logic_uScript_LockVisibleStackAccept_Out_325 = true;

	private uScript_KeepBlockInvulnerable logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_326 = new uScript_KeepBlockInvulnerable();

	private TankBlock logic_uScript_KeepBlockInvulnerable_block_326;

	private bool logic_uScript_KeepBlockInvulnerable_Out_326 = true;

	private uScript_KeepVisibleInEncounterArea logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_329 = new uScript_KeepVisibleInEncounterArea();

	private GameObject logic_uScript_KeepVisibleInEncounterArea_ownerNode_329;

	private object logic_uScript_KeepVisibleInEncounterArea_objectToEnclose_329 = "";

	private string logic_uScript_KeepVisibleInEncounterArea_resetPosName_329 = "";

	private Vector3 logic_uScript_KeepVisibleInEncounterArea_positionBeforeReset_329;

	private bool logic_uScript_KeepVisibleInEncounterArea_Out_329 = true;

	private bool logic_uScript_KeepVisibleInEncounterArea_InsideArea_329 = true;

	private bool logic_uScript_KeepVisibleInEncounterArea_ResetFromOutsideArea_329 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_336 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_336 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_336;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_336 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_336;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_336 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_336 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_336 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_336 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_338 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_338 = new Tank[0];

	private int logic_uScript_AccessListTech_index_338;

	private Tank logic_uScript_AccessListTech_value_338;

	private bool logic_uScript_AccessListTech_Out_338 = true;

	private uScript_PointArrowAtBlock logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_340 = new uScript_PointArrowAtBlock();

	private TankBlock logic_uScript_PointArrowAtBlock_block_340;

	private float logic_uScript_PointArrowAtBlock_timeToShowFor_340 = -1f;

	private Vector3 logic_uScript_PointArrowAtBlock_offset_340 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtBlock_Out_340 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_342 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_342 = "";

	private bool logic_uScript_EnableGlow_enable_342 = true;

	private bool logic_uScript_EnableGlow_Out_342 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_343 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_343;

	private bool logic_uScriptAct_SetBool_Out_343 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_343 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_343 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_346 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_346;

	private bool logic_uScriptCon_CompareBool_True_346 = true;

	private bool logic_uScriptCon_CompareBool_False_346 = true;

	private uScript_RemoveAllGhostBlocksOnTech logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_347 = new uScript_RemoveAllGhostBlocksOnTech();

	private Tank logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_347;

	private bool logic_uScript_RemoveAllGhostBlocksOnTech_Out_347 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_349 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_349 = true;

	private uScript_GetAndCheckBlocks logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_350 = new uScript_GetAndCheckBlocks();

	private SpawnBlockData[] logic_uScript_GetAndCheckBlocks_blockData_350 = new SpawnBlockData[0];

	private GameObject logic_uScript_GetAndCheckBlocks_ownerNode_350;

	private TankBlock[] logic_uScript_GetAndCheckBlocks_blocks_350 = new TankBlock[0];

	private bool logic_uScript_GetAndCheckBlocks_AllAlive_350 = true;

	private bool logic_uScript_GetAndCheckBlocks_SomeAlive_350 = true;

	private bool logic_uScript_GetAndCheckBlocks_AllDead_350 = true;

	private bool logic_uScript_GetAndCheckBlocks_WaitingToSpawn_350 = true;

	private uScript_SpawnBlocksFromData logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_351 = new uScript_SpawnBlocksFromData();

	private SpawnBlockData[] logic_uScript_SpawnBlocksFromData_blockData_351 = new SpawnBlockData[0];

	private GameObject logic_uScript_SpawnBlocksFromData_ownerNode_351;

	private bool logic_uScript_SpawnBlocksFromData_Out_351 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_352 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_352 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_352;

	private TankBlock logic_uScript_AccessListBlock_value_352;

	private bool logic_uScript_AccessListBlock_Out_352 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_360 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_360 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_362 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_362 = "";

	private bool logic_uScript_EnableGlow_enable_362;

	private bool logic_uScript_EnableGlow_Out_362 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_364 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_364;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_364 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_364 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_364;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_364;

	private bool logic_uScript_FlyTechUpAndAway_Out_364 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_365 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_365;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_365;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_365;

	private bool logic_uScript_AddMessage_Out_365 = true;

	private bool logic_uScript_AddMessage_Shown_365 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_367 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_367;

	private bool logic_uScript_ClearEncounterTarget_Out_367 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_371 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_371;

	private bool logic_uScript_FinishEncounter_Out_371 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_372 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_372;

	private bool logic_uScriptCon_CompareBool_True_372 = true;

	private bool logic_uScriptCon_CompareBool_False_372 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_375 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_375;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_375 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_375 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_375;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_375;

	private bool logic_uScript_FlyTechUpAndAway_Out_375 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_379 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_379;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_379 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_380 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_380;

	private int logic_uScript_SetTankTeam_team_380 = -2;

	private bool logic_uScript_SetTankTeam_Out_380 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_381 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_381 = "";

	private bool logic_uScript_EnableGlow_enable_381;

	private bool logic_uScript_EnableGlow_Out_381 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_383 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_383;

	private object logic_uScript_SetEncounterTarget_visibleObject_383 = "";

	private bool logic_uScript_SetEncounterTarget_Out_383 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_387 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_387 = new Tank[0];

	private int logic_uScript_AccessListTech_index_387;

	private Tank logic_uScript_AccessListTech_value_387;

	private bool logic_uScript_AccessListTech_Out_387 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_388 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_388 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_388;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_388 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_388;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_388 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_388 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_388 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_388 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_390 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_390 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_393 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_393 = new Tank[0];

	private int logic_uScript_AccessListTech_index_393;

	private Tank logic_uScript_AccessListTech_value_393;

	private bool logic_uScript_AccessListTech_Out_393 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_394 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_394 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_394;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_394 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_394;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_394 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_394 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_394 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_394 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_399 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_399 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_400 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_400;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_406 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_406;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_406 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_406 = "Stage";

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_408 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_408 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_408 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_408 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_408 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_408 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_408 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_410 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_410 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_410 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_411 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_411 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_412 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_412 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_412 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_413 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_413 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_417 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_417;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_417;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_417;

	private bool logic_uScript_AddMessage_Out_417 = true;

	private bool logic_uScript_AddMessage_Shown_417 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_418 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_418;

	private bool logic_uScriptCon_CompareBool_True_418 = true;

	private bool logic_uScriptCon_CompareBool_False_418 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_419 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_419 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_419;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_419 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_420 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_420;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_420;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_420;

	private bool logic_uScript_AddMessage_Out_420 = true;

	private bool logic_uScript_AddMessage_Shown_420 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_423 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_423;

	private bool logic_uScriptAct_SetBool_Out_423 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_423 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_423 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_426 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_426 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_426 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_426 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_426 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_426 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_426 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_428 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_428 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_430 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_430 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_430;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_430 = true;

	private uScript_GetPlayerTeam logic_uScript_GetPlayerTeam_uScript_GetPlayerTeam_431 = new uScript_GetPlayerTeam();

	private int logic_uScript_GetPlayerTeam_Return_431;

	private bool logic_uScript_GetPlayerTeam_Out_431 = true;

	private uScript_LockTechSendToSCU logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_433 = new uScript_LockTechSendToSCU();

	private Tank logic_uScript_LockTechSendToSCU_tech_433;

	private bool logic_uScript_LockTechSendToSCU_lockSendToSCU_433 = true;

	private bool logic_uScript_LockTechSendToSCU_Out_433 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_435 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_435 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_435;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_435 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_435;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_435 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_435 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_435 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_435 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_437 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_437;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_437 = uScript_LockTech.TechLockType.LockDetachAndInteraction;

	private bool logic_uScript_LockTech_Out_437 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_439 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_439 = new Tank[0];

	private int logic_uScript_AccessListTech_index_439;

	private Tank logic_uScript_AccessListTech_value_439;

	private bool logic_uScript_AccessListTech_Out_439 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_441 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_441 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_442 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_442 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_444 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_444;

	private int logic_uScript_SetTankTeam_team_444;

	private bool logic_uScript_SetTankTeam_Out_444 = true;

	private uScript_GetPlayerTeam logic_uScript_GetPlayerTeam_uScript_GetPlayerTeam_446 = new uScript_GetPlayerTeam();

	private int logic_uScript_GetPlayerTeam_Return_446;

	private bool logic_uScript_GetPlayerTeam_Out_446 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_448 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_448;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_448 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_448 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_449 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_449 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_450 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_450 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_452 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_452;

	private bool logic_uScriptCon_CompareBool_True_452 = true;

	private bool logic_uScriptCon_CompareBool_False_452 = true;

	private uScript_IsTechAnchored logic_uScript_IsTechAnchored_uScript_IsTechAnchored_454 = new uScript_IsTechAnchored();

	private Tank logic_uScript_IsTechAnchored_tech_454;

	private bool logic_uScript_IsTechAnchored_Out_454 = true;

	private bool logic_uScript_IsTechAnchored_True_454 = true;

	private bool logic_uScript_IsTechAnchored_False_454 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_457;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_457 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_457 = "SpawnedBooster";

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
			if (null != owner_Connection_1)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_1.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
				}
			}
		}
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
			if (null != owner_Connection_3)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_3.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_3.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_2;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_2;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_2;
				}
			}
		}
		if (null == owner_Connection_21 || !m_RegisteredForEvents)
		{
			owner_Connection_21 = parentGameObject;
		}
		if (null == owner_Connection_37 || !m_RegisteredForEvents)
		{
			owner_Connection_37 = parentGameObject;
		}
		if (null == owner_Connection_46 || !m_RegisteredForEvents)
		{
			owner_Connection_46 = parentGameObject;
		}
		if (null == owner_Connection_51 || !m_RegisteredForEvents)
		{
			owner_Connection_51 = parentGameObject;
		}
		if (null == owner_Connection_96 || !m_RegisteredForEvents)
		{
			owner_Connection_96 = parentGameObject;
		}
		if (null == owner_Connection_107 || !m_RegisteredForEvents)
		{
			owner_Connection_107 = parentGameObject;
		}
		if (null == owner_Connection_114 || !m_RegisteredForEvents)
		{
			owner_Connection_114 = parentGameObject;
		}
		if (null == owner_Connection_115 || !m_RegisteredForEvents)
		{
			owner_Connection_115 = parentGameObject;
		}
		if (null == owner_Connection_155 || !m_RegisteredForEvents)
		{
			owner_Connection_155 = parentGameObject;
		}
		if (null == owner_Connection_241 || !m_RegisteredForEvents)
		{
			owner_Connection_241 = parentGameObject;
		}
		if (null == owner_Connection_245 || !m_RegisteredForEvents)
		{
			owner_Connection_245 = parentGameObject;
		}
		if (null == owner_Connection_330 || !m_RegisteredForEvents)
		{
			owner_Connection_330 = parentGameObject;
		}
		if (null == owner_Connection_333 || !m_RegisteredForEvents)
		{
			owner_Connection_333 = parentGameObject;
		}
		if (null == owner_Connection_354 || !m_RegisteredForEvents)
		{
			owner_Connection_354 = parentGameObject;
		}
		if (null == owner_Connection_355 || !m_RegisteredForEvents)
		{
			owner_Connection_355 = parentGameObject;
		}
		if (null == owner_Connection_368 || !m_RegisteredForEvents)
		{
			owner_Connection_368 = parentGameObject;
		}
		if (null == owner_Connection_374 || !m_RegisteredForEvents)
		{
			owner_Connection_374 = parentGameObject;
		}
		if (null == owner_Connection_384 || !m_RegisteredForEvents)
		{
			owner_Connection_384 = parentGameObject;
		}
		if (null == owner_Connection_386 || !m_RegisteredForEvents)
		{
			owner_Connection_386 = parentGameObject;
		}
		if (null == owner_Connection_397 || !m_RegisteredForEvents)
		{
			owner_Connection_397 = parentGameObject;
		}
		if (null == owner_Connection_440 || !m_RegisteredForEvents)
		{
			owner_Connection_440 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_1)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_1.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_3)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_3.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_3.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_2;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_2;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_2;
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
			uScript_EncounterUpdate component = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_0;
				component.OnSuspend -= Instance_OnSuspend_0;
				component.OnResume -= Instance_OnResume_0;
			}
		}
		if (null != owner_Connection_3)
		{
			uScript_SaveLoad component2 = owner_Connection_3.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_2;
				component2.LoadEvent -= Instance_LoadEvent_2;
				component2.RestartEvent -= Instance_RestartEvent_2;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_5.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_10.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_12.SetParent(g);
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_15.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_19.SetParent(g);
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_20.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_23.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_39.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_41.SetParent(g);
		logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_43.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_44.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_50.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_53.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_54.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_59.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_68.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_69.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_74.SetParent(g);
		logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_76.SetParent(g);
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_79.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_80.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_82.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_83.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_86.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_88.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_94.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_95.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_97.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_99.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_109.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_111.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_112.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_113.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_121.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_122.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_123.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_125.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_126.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_134.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_137.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_140.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_142.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_143.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_154.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_157.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_158.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_160.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_164.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_166.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_168.SetParent(g);
		logic_uScript_GetPositionOfTech_uScript_GetPositionOfTech_169.SetParent(g);
		logic_uScript_IsVisibleInTrigger_uScript_IsVisibleInTrigger_172.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_175.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_178.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_179.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_180.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_184.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_185.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_190.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_193.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_194.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_198.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_204.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_209.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_213.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_215.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_220.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_224.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_228.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_231.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_232.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_234.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_237.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_240.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_243.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_244.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_246.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_253.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_254.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_257.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_259.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_263.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_264.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_270.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_274.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_279.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_280.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_281.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_282.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_283.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_284.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_285.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_286.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_287.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_289.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_291.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_292.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_293.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_299.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_301.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_303.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_304.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_305.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_307.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.SetParent(g);
		logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_316.SetParent(g);
		logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_317.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_318.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_320.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_322.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_323.SetParent(g);
		logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_325.SetParent(g);
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_326.SetParent(g);
		logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_329.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_336.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_338.SetParent(g);
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_340.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_342.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_343.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_346.SetParent(g);
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_347.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_349.SetParent(g);
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_350.SetParent(g);
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_351.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_352.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_360.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_362.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_364.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_365.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_367.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_371.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_372.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_375.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_379.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_380.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_381.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_383.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_387.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_388.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_390.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_393.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_394.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_399.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_400.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_408.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_410.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_411.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_412.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_413.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_417.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_418.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_419.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_420.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_423.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_426.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_428.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_430.SetParent(g);
		logic_uScript_GetPlayerTeam_uScript_GetPlayerTeam_431.SetParent(g);
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_433.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_435.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_437.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_439.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_441.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_442.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_444.SetParent(g);
		logic_uScript_GetPlayerTeam_uScript_GetPlayerTeam_446.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_448.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_449.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_450.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_452.SetParent(g);
		logic_uScript_IsTechAnchored_uScript_IsTechAnchored_454.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_21 = parentGameObject;
		owner_Connection_37 = parentGameObject;
		owner_Connection_46 = parentGameObject;
		owner_Connection_51 = parentGameObject;
		owner_Connection_96 = parentGameObject;
		owner_Connection_107 = parentGameObject;
		owner_Connection_114 = parentGameObject;
		owner_Connection_115 = parentGameObject;
		owner_Connection_155 = parentGameObject;
		owner_Connection_241 = parentGameObject;
		owner_Connection_245 = parentGameObject;
		owner_Connection_330 = parentGameObject;
		owner_Connection_333 = parentGameObject;
		owner_Connection_354 = parentGameObject;
		owner_Connection_355 = parentGameObject;
		owner_Connection_368 = parentGameObject;
		owner_Connection_374 = parentGameObject;
		owner_Connection_384 = parentGameObject;
		owner_Connection_386 = parentGameObject;
		owner_Connection_397 = parentGameObject;
		owner_Connection_440 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_166.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_168.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_379.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_400.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Save_Out += SubGraph_SaveLoadBool_Save_Out_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Load_Out += SubGraph_SaveLoadBool_Load_Out_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save_Out += SubGraph_SaveLoadBool_Save_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load_Out += SubGraph_SaveLoadBool_Load_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Save_Out += SubGraph_SaveLoadBool_Save_Out_32;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Load_Out += SubGraph_SaveLoadBool_Load_Out_32;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_32;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Save_Out += SubGraph_SaveLoadBool_Save_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Load_Out += SubGraph_SaveLoadBool_Load_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.Save_Out += SubGraph_SaveLoadBool_Save_Out_34;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.Load_Out += SubGraph_SaveLoadBool_Load_Out_34;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_34;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Save_Out += SubGraph_SaveLoadBool_Save_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Load_Out += SubGraph_SaveLoadBool_Load_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Save_Out += SubGraph_SaveLoadBool_Save_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Load_Out += SubGraph_SaveLoadBool_Load_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Save_Out += SubGraph_SaveLoadBool_Save_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Load_Out += SubGraph_SaveLoadBool_Load_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Save_Out += SubGraph_SaveLoadBool_Save_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Load_Out += SubGraph_SaveLoadBool_Load_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Save_Out += SubGraph_SaveLoadBool_Save_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Load_Out += SubGraph_SaveLoadBool_Load_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Save_Out += SubGraph_SaveLoadBool_Save_Out_149;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Load_Out += SubGraph_SaveLoadBool_Load_Out_149;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_149;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Save_Out += SubGraph_SaveLoadBool_Save_Out_150;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Load_Out += SubGraph_SaveLoadBool_Load_Out_150;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_150;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Save_Out += SubGraph_SaveLoadBool_Save_Out_151;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Load_Out += SubGraph_SaveLoadBool_Load_Out_151;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_151;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_166.Out += SubGraph_CompleteObjectiveStage_Out_166;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.Out += SubGraph_CompleteObjectiveStage_Out_167;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_168.Out += SubGraph_CompleteObjectiveStage_Out_168;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.Save_Out += SubGraph_SaveLoadBool_Save_Out_189;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.Load_Out += SubGraph_SaveLoadBool_Load_Out_189;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_189;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Save_Out += SubGraph_SaveLoadBool_Save_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Load_Out += SubGraph_SaveLoadBool_Load_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Save_Out += SubGraph_SaveLoadBool_Save_Out_202;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Load_Out += SubGraph_SaveLoadBool_Load_Out_202;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_202;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Save_Out += SubGraph_SaveLoadBool_Save_Out_208;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Load_Out += SubGraph_SaveLoadBool_Load_Out_208;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_208;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.Save_Out += SubGraph_SaveLoadBool_Save_Out_239;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.Load_Out += SubGraph_SaveLoadBool_Load_Out_239;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_239;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Save_Out += SubGraph_SaveLoadBool_Save_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Load_Out += SubGraph_SaveLoadBool_Load_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.Save_Out += SubGraph_SaveLoadBool_Save_Out_262;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.Load_Out += SubGraph_SaveLoadBool_Load_Out_262;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_262;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.Save_Out += SubGraph_SaveLoadBool_Save_Out_273;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.Load_Out += SubGraph_SaveLoadBool_Load_Out_273;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_273;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.Save_Out += SubGraph_SaveLoadBool_Save_Out_309;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.Load_Out += SubGraph_SaveLoadBool_Load_Out_309;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_309;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.Save_Out += SubGraph_SaveLoadBool_Save_Out_311;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.Load_Out += SubGraph_SaveLoadBool_Load_Out_311;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_311;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.Save_Out += SubGraph_SaveLoadBool_Save_Out_313;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.Load_Out += SubGraph_SaveLoadBool_Load_Out_313;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_313;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Save_Out += SubGraph_SaveLoadBool_Save_Out_315;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Load_Out += SubGraph_SaveLoadBool_Load_Out_315;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_315;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_379.Out += SubGraph_CompleteObjectiveStage_Out_379;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_400.Out += SubGraph_LoadObjectiveStates_Out_400;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406.Save_Out += SubGraph_SaveLoadInt_Save_Out_406;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406.Load_Out += SubGraph_SaveLoadInt_Load_Out_406;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_406;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Save_Out += SubGraph_SaveLoadBool_Save_Out_457;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Load_Out += SubGraph_SaveLoadBool_Load_Out_457;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_457;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_166.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_168.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_379.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_400.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_39.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_166.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_168.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_364.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_375.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_379.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_400.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_10.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_12.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_50.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_95.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_122.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_125.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_137.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_143.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.OnDisable();
		logic_uScript_SetTankTeam_uScript_SetTankTeam_154.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_166.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_168.OnDisable();
		logic_uScript_GetPositionOfTech_uScript_GetPositionOfTech_169.OnDisable();
		logic_uScript_SetTankTeam_uScript_SetTankTeam_179.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_185.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_193.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_198.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_213.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_220.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_231.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_244.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_254.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_264.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_274.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_279.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_293.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_301.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_365.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_379.OnDisable();
		logic_uScript_SetTankTeam_uScript_SetTankTeam_380.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_400.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_410.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_412.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_417.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_420.OnDisable();
		logic_uScript_SetTankTeam_uScript_SetTankTeam_444.OnDisable();
		logic_uScript_IsTechAnchored_uScript_IsTechAnchored_454.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_166.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_168.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_379.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_400.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_166.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_168.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_379.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_400.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Save_Out -= SubGraph_SaveLoadBool_Save_Out_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Load_Out -= SubGraph_SaveLoadBool_Load_Out_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save_Out -= SubGraph_SaveLoadBool_Save_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load_Out -= SubGraph_SaveLoadBool_Load_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Save_Out -= SubGraph_SaveLoadBool_Save_Out_32;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Load_Out -= SubGraph_SaveLoadBool_Load_Out_32;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_32;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Save_Out -= SubGraph_SaveLoadBool_Save_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Load_Out -= SubGraph_SaveLoadBool_Load_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.Save_Out -= SubGraph_SaveLoadBool_Save_Out_34;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.Load_Out -= SubGraph_SaveLoadBool_Load_Out_34;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_34;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Save_Out -= SubGraph_SaveLoadBool_Save_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Load_Out -= SubGraph_SaveLoadBool_Load_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Save_Out -= SubGraph_SaveLoadBool_Save_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Load_Out -= SubGraph_SaveLoadBool_Load_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Save_Out -= SubGraph_SaveLoadBool_Save_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Load_Out -= SubGraph_SaveLoadBool_Load_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Save_Out -= SubGraph_SaveLoadBool_Save_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Load_Out -= SubGraph_SaveLoadBool_Load_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Save_Out -= SubGraph_SaveLoadBool_Save_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Load_Out -= SubGraph_SaveLoadBool_Load_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Save_Out -= SubGraph_SaveLoadBool_Save_Out_149;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Load_Out -= SubGraph_SaveLoadBool_Load_Out_149;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_149;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Save_Out -= SubGraph_SaveLoadBool_Save_Out_150;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Load_Out -= SubGraph_SaveLoadBool_Load_Out_150;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_150;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Save_Out -= SubGraph_SaveLoadBool_Save_Out_151;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Load_Out -= SubGraph_SaveLoadBool_Load_Out_151;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_151;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_166.Out -= SubGraph_CompleteObjectiveStage_Out_166;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.Out -= SubGraph_CompleteObjectiveStage_Out_167;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_168.Out -= SubGraph_CompleteObjectiveStage_Out_168;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.Save_Out -= SubGraph_SaveLoadBool_Save_Out_189;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.Load_Out -= SubGraph_SaveLoadBool_Load_Out_189;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_189;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Save_Out -= SubGraph_SaveLoadBool_Save_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Load_Out -= SubGraph_SaveLoadBool_Load_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Save_Out -= SubGraph_SaveLoadBool_Save_Out_202;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Load_Out -= SubGraph_SaveLoadBool_Load_Out_202;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_202;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Save_Out -= SubGraph_SaveLoadBool_Save_Out_208;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Load_Out -= SubGraph_SaveLoadBool_Load_Out_208;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_208;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.Save_Out -= SubGraph_SaveLoadBool_Save_Out_239;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.Load_Out -= SubGraph_SaveLoadBool_Load_Out_239;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_239;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Save_Out -= SubGraph_SaveLoadBool_Save_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Load_Out -= SubGraph_SaveLoadBool_Load_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_251;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.Save_Out -= SubGraph_SaveLoadBool_Save_Out_262;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.Load_Out -= SubGraph_SaveLoadBool_Load_Out_262;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_262;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.Save_Out -= SubGraph_SaveLoadBool_Save_Out_273;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.Load_Out -= SubGraph_SaveLoadBool_Load_Out_273;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_273;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.Save_Out -= SubGraph_SaveLoadBool_Save_Out_309;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.Load_Out -= SubGraph_SaveLoadBool_Load_Out_309;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_309;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.Save_Out -= SubGraph_SaveLoadBool_Save_Out_311;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.Load_Out -= SubGraph_SaveLoadBool_Load_Out_311;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_311;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.Save_Out -= SubGraph_SaveLoadBool_Save_Out_313;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.Load_Out -= SubGraph_SaveLoadBool_Load_Out_313;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_313;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Save_Out -= SubGraph_SaveLoadBool_Save_Out_315;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Load_Out -= SubGraph_SaveLoadBool_Load_Out_315;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_315;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_379.Out -= SubGraph_CompleteObjectiveStage_Out_379;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_400.Out -= SubGraph_LoadObjectiveStates_Out_400;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406.Save_Out -= SubGraph_SaveLoadInt_Save_Out_406;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406.Load_Out -= SubGraph_SaveLoadInt_Load_Out_406;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_406;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Save_Out -= SubGraph_SaveLoadBool_Save_Out_457;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Load_Out -= SubGraph_SaveLoadBool_Load_Out_457;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_457;
	}

	private void Instance_OnUpdate_0(object o, EventArgs e)
	{
		Relay_OnUpdate_0();
	}

	private void Instance_OnSuspend_0(object o, EventArgs e)
	{
		Relay_OnSuspend_0();
	}

	private void Instance_OnResume_0(object o, EventArgs e)
	{
		Relay_OnResume_0();
	}

	private void Instance_SaveEvent_2(object o, EventArgs e)
	{
		Relay_SaveEvent_2();
	}

	private void Instance_LoadEvent_2(object o, EventArgs e)
	{
		Relay_LoadEvent_2();
	}

	private void Instance_RestartEvent_2(object o, EventArgs e)
	{
		Relay_RestartEvent_2();
	}

	private void SubGraph_SaveLoadBool_Save_Out_8(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_8;
		Relay_Save_Out_8();
	}

	private void SubGraph_SaveLoadBool_Load_Out_8(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_8;
		Relay_Load_Out_8();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_8(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_8;
		Relay_Restart_Out_8();
	}

	private void SubGraph_SaveLoadBool_Save_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Save_Out_18();
	}

	private void SubGraph_SaveLoadBool_Load_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Load_Out_18();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_TechsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Restart_Out_18();
	}

	private void SubGraph_SaveLoadBool_Save_Out_32(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = e.boolean;
		local_TechsSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_32;
		Relay_Save_Out_32();
	}

	private void SubGraph_SaveLoadBool_Load_Out_32(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = e.boolean;
		local_TechsSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_32;
		Relay_Load_Out_32();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_32(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = e.boolean;
		local_TechsSetup_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_32;
		Relay_Restart_Out_32();
	}

	private void SubGraph_SaveLoadBool_Save_Out_33(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = e.boolean;
		local_NPCToBuildSet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_33;
		Relay_Save_Out_33();
	}

	private void SubGraph_SaveLoadBool_Load_Out_33(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = e.boolean;
		local_NPCToBuildSet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_33;
		Relay_Load_Out_33();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_33(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = e.boolean;
		local_NPCToBuildSet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_33;
		Relay_Restart_Out_33();
	}

	private void SubGraph_SaveLoadBool_Save_Out_34(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_34 = e.boolean;
		local_BlockAttachedToNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_34;
		Relay_Save_Out_34();
	}

	private void SubGraph_SaveLoadBool_Load_Out_34(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_34 = e.boolean;
		local_BlockAttachedToNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_34;
		Relay_Load_Out_34();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_34(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_34 = e.boolean;
		local_BlockAttachedToNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_34;
		Relay_Restart_Out_34();
	}

	private void SubGraph_SaveLoadBool_Save_Out_35(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = e.boolean;
		local_GhostBlockSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_35;
		Relay_Save_Out_35();
	}

	private void SubGraph_SaveLoadBool_Load_Out_35(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = e.boolean;
		local_GhostBlockSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_35;
		Relay_Load_Out_35();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_35(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = e.boolean;
		local_GhostBlockSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_35;
		Relay_Restart_Out_35();
	}

	private void SubGraph_SaveLoadBool_Save_Out_92(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = e.boolean;
		local_PlayerHitAMine_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_92;
		Relay_Save_Out_92();
	}

	private void SubGraph_SaveLoadBool_Load_Out_92(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = e.boolean;
		local_PlayerHitAMine_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_92;
		Relay_Load_Out_92();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_92(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = e.boolean;
		local_PlayerHitAMine_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_92;
		Relay_Restart_Out_92();
	}

	private void SubGraph_SaveLoadBool_Save_Out_93(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = e.boolean;
		local_EnemyHitAMine_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_93;
		Relay_Save_Out_93();
	}

	private void SubGraph_SaveLoadBool_Load_Out_93(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = e.boolean;
		local_EnemyHitAMine_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_93;
		Relay_Load_Out_93();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_93(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = e.boolean;
		local_EnemyHitAMine_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_93;
		Relay_Restart_Out_93();
	}

	private void SubGraph_SaveLoadBool_Save_Out_105(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = e.boolean;
		local_ReachedMineIsland_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_105;
		Relay_Save_Out_105();
	}

	private void SubGraph_SaveLoadBool_Load_Out_105(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = e.boolean;
		local_ReachedMineIsland_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_105;
		Relay_Load_Out_105();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_105(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = e.boolean;
		local_ReachedMineIsland_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_105;
		Relay_Restart_Out_105();
	}

	private void SubGraph_SaveLoadBool_Save_Out_106(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = e.boolean;
		local_ArrivedAtMineIsland_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_106;
		Relay_Save_Out_106();
	}

	private void SubGraph_SaveLoadBool_Load_Out_106(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = e.boolean;
		local_ArrivedAtMineIsland_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_106;
		Relay_Load_Out_106();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_106(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = e.boolean;
		local_ArrivedAtMineIsland_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_106;
		Relay_Restart_Out_106();
	}

	private void SubGraph_SaveLoadBool_Save_Out_149(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_149 = e.boolean;
		local_MetBoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_149;
		Relay_Save_Out_149();
	}

	private void SubGraph_SaveLoadBool_Load_Out_149(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_149 = e.boolean;
		local_MetBoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_149;
		Relay_Load_Out_149();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_149(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_149 = e.boolean;
		local_MetBoss_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_149;
		Relay_Restart_Out_149();
	}

	private void SubGraph_SaveLoadBool_Save_Out_150(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_150 = e.boolean;
		local_NPCCallsForHelp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_150;
		Relay_Save_Out_150();
	}

	private void SubGraph_SaveLoadBool_Load_Out_150(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_150 = e.boolean;
		local_NPCCallsForHelp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_150;
		Relay_Load_Out_150();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_150(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_150 = e.boolean;
		local_NPCCallsForHelp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_150;
		Relay_Restart_Out_150();
	}

	private void SubGraph_SaveLoadBool_Save_Out_151(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = e.boolean;
		local_BossResponds_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_151;
		Relay_Save_Out_151();
	}

	private void SubGraph_SaveLoadBool_Load_Out_151(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = e.boolean;
		local_BossResponds_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_151;
		Relay_Load_Out_151();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_151(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = e.boolean;
		local_BossResponds_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_151;
		Relay_Restart_Out_151();
	}

	private void SubGraph_CompleteObjectiveStage_Out_166(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_166 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_166;
		Relay_Out_166();
	}

	private void SubGraph_CompleteObjectiveStage_Out_167(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_167 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_167;
		Relay_Out_167();
	}

	private void SubGraph_CompleteObjectiveStage_Out_168(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_168 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_168;
		Relay_Out_168();
	}

	private void SubGraph_SaveLoadBool_Save_Out_189(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_189 = e.boolean;
		local_msgBossDeadShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_189;
		Relay_Save_Out_189();
	}

	private void SubGraph_SaveLoadBool_Load_Out_189(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_189 = e.boolean;
		local_msgBossDeadShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_189;
		Relay_Load_Out_189();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_189(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_189 = e.boolean;
		local_msgBossDeadShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_189;
		Relay_Restart_Out_189();
	}

	private void SubGraph_SaveLoadBool_Save_Out_197(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = e.boolean;
		local_msgNothingToSeeHereShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_197;
		Relay_Save_Out_197();
	}

	private void SubGraph_SaveLoadBool_Load_Out_197(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = e.boolean;
		local_msgNothingToSeeHereShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_197;
		Relay_Load_Out_197();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_197(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = e.boolean;
		local_msgNothingToSeeHereShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_197;
		Relay_Restart_Out_197();
	}

	private void SubGraph_SaveLoadBool_Save_Out_202(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_202 = e.boolean;
		local_msgHowDareYouShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_202;
		Relay_Save_Out_202();
	}

	private void SubGraph_SaveLoadBool_Load_Out_202(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_202 = e.boolean;
		local_msgHowDareYouShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_202;
		Relay_Load_Out_202();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_202(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_202 = e.boolean;
		local_msgHowDareYouShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_202;
		Relay_Restart_Out_202();
	}

	private void SubGraph_SaveLoadBool_Save_Out_208(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_208 = e.boolean;
		local_AttemptedAlready_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_208;
		Relay_Save_Out_208();
	}

	private void SubGraph_SaveLoadBool_Load_Out_208(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_208 = e.boolean;
		local_AttemptedAlready_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_208;
		Relay_Load_Out_208();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_208(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_208 = e.boolean;
		local_AttemptedAlready_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_208;
		Relay_Restart_Out_208();
	}

	private void SubGraph_SaveLoadBool_Save_Out_239(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_239 = e.boolean;
		local_msgReachedMineIslandShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_239;
		Relay_Save_Out_239();
	}

	private void SubGraph_SaveLoadBool_Load_Out_239(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_239 = e.boolean;
		local_msgReachedMineIslandShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_239;
		Relay_Load_Out_239();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_239(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_239 = e.boolean;
		local_msgReachedMineIslandShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_239;
		Relay_Restart_Out_239();
	}

	private void SubGraph_SaveLoadBool_Save_Out_251(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = e.boolean;
		local_msgIAmRepulsorShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_251;
		Relay_Save_Out_251();
	}

	private void SubGraph_SaveLoadBool_Load_Out_251(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = e.boolean;
		local_msgIAmRepulsorShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_251;
		Relay_Load_Out_251();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_251(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = e.boolean;
		local_msgIAmRepulsorShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_251;
		Relay_Restart_Out_251();
	}

	private void SubGraph_SaveLoadBool_Save_Out_262(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_262 = e.boolean;
		local_msgNearLongJumpShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_262;
		Relay_Save_Out_262();
	}

	private void SubGraph_SaveLoadBool_Load_Out_262(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_262 = e.boolean;
		local_msgNearLongJumpShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_262;
		Relay_Load_Out_262();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_262(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_262 = e.boolean;
		local_msgNearLongJumpShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_262;
		Relay_Restart_Out_262();
	}

	private void SubGraph_SaveLoadBool_Save_Out_273(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_273 = e.boolean;
		local_msgNearBrokenBridgeShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_273;
		Relay_Save_Out_273();
	}

	private void SubGraph_SaveLoadBool_Load_Out_273(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_273 = e.boolean;
		local_msgNearBrokenBridgeShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_273;
		Relay_Load_Out_273();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_273(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_273 = e.boolean;
		local_msgNearBrokenBridgeShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_273;
		Relay_Restart_Out_273();
	}

	private void SubGraph_SaveLoadBool_Save_Out_309(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_309 = e.boolean;
		local_msgArrivedAtMineIslandShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_309;
		Relay_Save_Out_309();
	}

	private void SubGraph_SaveLoadBool_Load_Out_309(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_309 = e.boolean;
		local_msgArrivedAtMineIslandShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_309;
		Relay_Load_Out_309();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_309(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_309 = e.boolean;
		local_msgArrivedAtMineIslandShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_309;
		Relay_Restart_Out_309();
	}

	private void SubGraph_SaveLoadBool_Save_Out_311(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_311 = e.boolean;
		local_BossDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_311;
		Relay_Save_Out_311();
	}

	private void SubGraph_SaveLoadBool_Load_Out_311(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_311 = e.boolean;
		local_BossDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_311;
		Relay_Load_Out_311();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_311(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_311 = e.boolean;
		local_BossDead_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_311;
		Relay_Restart_Out_311();
	}

	private void SubGraph_SaveLoadBool_Save_Out_313(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_313 = e.boolean;
		local_BossTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_313;
		Relay_Save_Out_313();
	}

	private void SubGraph_SaveLoadBool_Load_Out_313(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_313 = e.boolean;
		local_BossTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_313;
		Relay_Load_Out_313();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_313(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_313 = e.boolean;
		local_BossTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_313;
		Relay_Restart_Out_313();
	}

	private void SubGraph_SaveLoadBool_Save_Out_315(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_315 = e.boolean;
		local_MsgBackAgain_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_315;
		Relay_Save_Out_315();
	}

	private void SubGraph_SaveLoadBool_Load_Out_315(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_315 = e.boolean;
		local_MsgBackAgain_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_315;
		Relay_Load_Out_315();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_315(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_315 = e.boolean;
		local_MsgBackAgain_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_315;
		Relay_Restart_Out_315();
	}

	private void SubGraph_CompleteObjectiveStage_Out_379(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_379 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_379;
		Relay_Out_379();
	}

	private void SubGraph_LoadObjectiveStates_Out_400(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_400();
	}

	private void SubGraph_SaveLoadInt_Save_Out_406(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_406 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_406;
		Relay_Save_Out_406();
	}

	private void SubGraph_SaveLoadInt_Load_Out_406(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_406 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_406;
		Relay_Load_Out_406();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_406(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_406 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_406;
		Relay_Restart_Out_406();
	}

	private void SubGraph_SaveLoadBool_Save_Out_457(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_457 = e.boolean;
		local_SpawnedBooster_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_457;
		Relay_Save_Out_457();
	}

	private void SubGraph_SaveLoadBool_Load_Out_457(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_457 = e.boolean;
		local_SpawnedBooster_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_457;
		Relay_Load_Out_457();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_457(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_457 = e.boolean;
		local_SpawnedBooster_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_457;
		Relay_Restart_Out_457();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_95();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
	}

	private void Relay_SaveEvent_2()
	{
		Relay_Save_406();
	}

	private void Relay_LoadEvent_2()
	{
		Relay_Load_406();
	}

	private void Relay_RestartEvent_2()
	{
		Relay_Restart_406();
	}

	private void Relay_In_5()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_5 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_5.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_5, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_5);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_5.Out)
		{
			Relay_In_346();
		}
	}

	private void Relay_In_6()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_6 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6.In(logic_uScript_IsPlayerInRangeOfTech_tech_6, logic_uScript_IsPlayerInRangeOfTech_range_6, logic_uScript_IsPlayerInRangeOfTech_techs_6);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6.OutOfRange;
		if (inRange)
		{
			Relay_In_446();
		}
		if (outOfRange)
		{
			Relay_In_5();
		}
	}

	private void Relay_In_7()
	{
		logic_uScriptCon_CompareBool_Bool_7 = local_msgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.In(logic_uScriptCon_CompareBool_Bool_7);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.False;
		if (num)
		{
			Relay_In_26();
		}
		if (flag)
		{
			Relay_In_10();
		}
	}

	private void Relay_Save_Out_8()
	{
		Relay_Save_34();
	}

	private void Relay_Load_Out_8()
	{
		Relay_Load_34();
	}

	private void Relay_Restart_Out_8()
	{
		Relay_Set_False_34();
	}

	private void Relay_Save_8()
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_8 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Save(ref logic_SubGraph_SaveLoadBool_boolean_8, logic_SubGraph_SaveLoadBool_boolAsVariable_8, logic_SubGraph_SaveLoadBool_uniqueID_8);
	}

	private void Relay_Load_8()
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_8 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Load(ref logic_SubGraph_SaveLoadBool_boolean_8, logic_SubGraph_SaveLoadBool_boolAsVariable_8, logic_SubGraph_SaveLoadBool_uniqueID_8);
	}

	private void Relay_Set_True_8()
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_8 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_8, logic_SubGraph_SaveLoadBool_boolAsVariable_8, logic_SubGraph_SaveLoadBool_uniqueID_8);
	}

	private void Relay_Set_False_8()
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_8 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_8, logic_SubGraph_SaveLoadBool_boolAsVariable_8, logic_SubGraph_SaveLoadBool_uniqueID_8);
	}

	private void Relay_In_10()
	{
		logic_uScript_AddMessage_messageData_10 = msgMeetNPC;
		logic_uScript_AddMessage_speaker_10 = messageNPCSpeaker;
		logic_uScript_AddMessage_Return_10 = logic_uScript_AddMessage_uScript_AddMessage_10.In(logic_uScript_AddMessage_messageData_10, logic_uScript_AddMessage_speaker_10);
		if (logic_uScript_AddMessage_uScript_AddMessage_10.Shown)
		{
			Relay_True_74();
		}
	}

	private void Relay_In_12()
	{
		logic_uScript_AddMessage_messageData_12 = msgAttachBlock;
		logic_uScript_AddMessage_speaker_12 = messageNPCSpeaker;
		logic_uScript_AddMessage_Return_12 = logic_uScript_AddMessage_uScript_AddMessage_12.In(logic_uScript_AddMessage_messageData_12, logic_uScript_AddMessage_speaker_12);
	}

	private void Relay_In_15()
	{
		logic_uScript_DoesTechHaveBlockAtPosition_tech_15 = local_NPCTech_Tank;
		logic_uScript_DoesTechHaveBlockAtPosition_blockType_15 = blockTypeToAttach;
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_15.In(logic_uScript_DoesTechHaveBlockAtPosition_tech_15, logic_uScript_DoesTechHaveBlockAtPosition_blockType_15, logic_uScript_DoesTechHaveBlockAtPosition_localPosition_15);
		bool num = logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_15.True;
		bool flag = logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_15.False;
		if (num)
		{
			Relay_In_20();
		}
		if (flag)
		{
			Relay_In_12();
		}
	}

	private void Relay_Save_Out_18()
	{
		Relay_Save_32();
	}

	private void Relay_Load_Out_18()
	{
		Relay_Load_32();
	}

	private void Relay_Restart_Out_18()
	{
		Relay_Set_False_32();
	}

	private void Relay_Save_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Load_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Set_True_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Set_False_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_TechsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_AtIndex_19()
	{
		int num = 0;
		Array array = local_GhostBlocks_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_19.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_19, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_19, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_19.AtIndex(ref logic_uScript_AccessListBlock_blockList_19, logic_uScript_AccessListBlock_index_19, out logic_uScript_AccessListBlock_value_19);
		local_GhostBlocks_TankBlockArray = logic_uScript_AccessListBlock_blockList_19;
		local_GhostBlock_TankBlock = logic_uScript_AccessListBlock_value_19;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_19.Out)
		{
			Relay_In_342();
		}
	}

	private void Relay_In_20()
	{
		logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_20 = local_NPCTech_Tank;
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_20.In(logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_20);
		if (logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_20.Out)
		{
			Relay_True_29();
		}
	}

	private void Relay_TrySpawnOnTech_23()
	{
		int num = 0;
		Array array = ghostBlock;
		if (logic_uScript_SpawnGhostBlocks_ghostBlockData_23.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_23, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_23, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnGhostBlocks_ownerNode_23 = owner_Connection_21;
		logic_uScript_SpawnGhostBlocks_targetTech_23 = local_NPCTech_Tank;
		logic_uScript_SpawnGhostBlocks_Return_23 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_23.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_23, logic_uScript_SpawnGhostBlocks_ownerNode_23, logic_uScript_SpawnGhostBlocks_targetTech_23);
		local_GhostBlocks_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_23;
		if (logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_23.OnAlreadySpawned)
		{
			Relay_AtIndex_19();
		}
	}

	private void Relay_True_24()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.True(out logic_uScriptAct_SetBool_Target_24);
		local_GhostBlockSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_24;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_24.Out)
		{
			Relay_TrySpawnOnTech_23();
		}
	}

	private void Relay_False_24()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.False(out logic_uScriptAct_SetBool_Target_24);
		local_GhostBlockSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_24;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_24.Out)
		{
			Relay_TrySpawnOnTech_23();
		}
	}

	private void Relay_In_26()
	{
		logic_uScriptCon_CompareBool_Bool_26 = local_GhostBlockSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26.In(logic_uScriptCon_CompareBool_Bool_26);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_26.False;
		if (num)
		{
			Relay_In_342();
		}
		if (flag)
		{
			Relay_True_24();
		}
	}

	private void Relay_True_29()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.True(out logic_uScriptAct_SetBool_Target_29);
		local_BlockAttachedToNPC_System_Boolean = logic_uScriptAct_SetBool_Target_29;
	}

	private void Relay_False_29()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.False(out logic_uScriptAct_SetBool_Target_29);
		local_BlockAttachedToNPC_System_Boolean = logic_uScriptAct_SetBool_Target_29;
	}

	private void Relay_Save_Out_32()
	{
		Relay_Save_33();
	}

	private void Relay_Load_Out_32()
	{
		Relay_Load_33();
	}

	private void Relay_Restart_Out_32()
	{
		Relay_Set_False_33();
	}

	private void Relay_Save_32()
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = local_TechsSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_32 = local_TechsSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Save(ref logic_SubGraph_SaveLoadBool_boolean_32, logic_SubGraph_SaveLoadBool_boolAsVariable_32, logic_SubGraph_SaveLoadBool_uniqueID_32);
	}

	private void Relay_Load_32()
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = local_TechsSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_32 = local_TechsSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Load(ref logic_SubGraph_SaveLoadBool_boolean_32, logic_SubGraph_SaveLoadBool_boolAsVariable_32, logic_SubGraph_SaveLoadBool_uniqueID_32);
	}

	private void Relay_Set_True_32()
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = local_TechsSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_32 = local_TechsSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_32, logic_SubGraph_SaveLoadBool_boolAsVariable_32, logic_SubGraph_SaveLoadBool_uniqueID_32);
	}

	private void Relay_Set_False_32()
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = local_TechsSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_32 = local_TechsSetup_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_32, logic_SubGraph_SaveLoadBool_boolAsVariable_32, logic_SubGraph_SaveLoadBool_uniqueID_32);
	}

	private void Relay_Save_Out_33()
	{
		Relay_Save_8();
	}

	private void Relay_Load_Out_33()
	{
		Relay_Load_8();
	}

	private void Relay_Restart_Out_33()
	{
		Relay_Set_False_8();
	}

	private void Relay_Save_33()
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = local_NPCToBuildSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_33 = local_NPCToBuildSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Save(ref logic_SubGraph_SaveLoadBool_boolean_33, logic_SubGraph_SaveLoadBool_boolAsVariable_33, logic_SubGraph_SaveLoadBool_uniqueID_33);
	}

	private void Relay_Load_33()
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = local_NPCToBuildSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_33 = local_NPCToBuildSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Load(ref logic_SubGraph_SaveLoadBool_boolean_33, logic_SubGraph_SaveLoadBool_boolAsVariable_33, logic_SubGraph_SaveLoadBool_uniqueID_33);
	}

	private void Relay_Set_True_33()
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = local_NPCToBuildSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_33 = local_NPCToBuildSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_33, logic_SubGraph_SaveLoadBool_boolAsVariable_33, logic_SubGraph_SaveLoadBool_uniqueID_33);
	}

	private void Relay_Set_False_33()
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = local_NPCToBuildSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_33 = local_NPCToBuildSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_33, logic_SubGraph_SaveLoadBool_boolAsVariable_33, logic_SubGraph_SaveLoadBool_uniqueID_33);
	}

	private void Relay_Save_Out_34()
	{
		Relay_Save_35();
	}

	private void Relay_Load_Out_34()
	{
		Relay_Load_35();
	}

	private void Relay_Restart_Out_34()
	{
		Relay_Set_False_35();
	}

	private void Relay_Save_34()
	{
		logic_SubGraph_SaveLoadBool_boolean_34 = local_BlockAttachedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_34 = local_BlockAttachedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.Save(ref logic_SubGraph_SaveLoadBool_boolean_34, logic_SubGraph_SaveLoadBool_boolAsVariable_34, logic_SubGraph_SaveLoadBool_uniqueID_34);
	}

	private void Relay_Load_34()
	{
		logic_SubGraph_SaveLoadBool_boolean_34 = local_BlockAttachedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_34 = local_BlockAttachedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.Load(ref logic_SubGraph_SaveLoadBool_boolean_34, logic_SubGraph_SaveLoadBool_boolAsVariable_34, logic_SubGraph_SaveLoadBool_uniqueID_34);
	}

	private void Relay_Set_True_34()
	{
		logic_SubGraph_SaveLoadBool_boolean_34 = local_BlockAttachedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_34 = local_BlockAttachedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_34, logic_SubGraph_SaveLoadBool_boolAsVariable_34, logic_SubGraph_SaveLoadBool_uniqueID_34);
	}

	private void Relay_Set_False_34()
	{
		logic_SubGraph_SaveLoadBool_boolean_34 = local_BlockAttachedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_34 = local_BlockAttachedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_34.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_34, logic_SubGraph_SaveLoadBool_boolAsVariable_34, logic_SubGraph_SaveLoadBool_uniqueID_34);
	}

	private void Relay_Save_Out_35()
	{
		Relay_Save_92();
	}

	private void Relay_Load_Out_35()
	{
		Relay_Load_92();
	}

	private void Relay_Restart_Out_35()
	{
		Relay_Set_False_92();
	}

	private void Relay_Save_35()
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = local_GhostBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_35 = local_GhostBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Save(ref logic_SubGraph_SaveLoadBool_boolean_35, logic_SubGraph_SaveLoadBool_boolAsVariable_35, logic_SubGraph_SaveLoadBool_uniqueID_35);
	}

	private void Relay_Load_35()
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = local_GhostBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_35 = local_GhostBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Load(ref logic_SubGraph_SaveLoadBool_boolean_35, logic_SubGraph_SaveLoadBool_boolAsVariable_35, logic_SubGraph_SaveLoadBool_uniqueID_35);
	}

	private void Relay_Set_True_35()
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = local_GhostBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_35 = local_GhostBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_35, logic_SubGraph_SaveLoadBool_boolAsVariable_35, logic_SubGraph_SaveLoadBool_uniqueID_35);
	}

	private void Relay_Set_False_35()
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = local_GhostBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_35 = local_GhostBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_35, logic_SubGraph_SaveLoadBool_boolAsVariable_35, logic_SubGraph_SaveLoadBool_uniqueID_35);
	}

	private void Relay_In_39()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_39 = owner_Connection_37;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_39.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_39);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_39.Out)
		{
			Relay_InitialSpawn_44();
		}
	}

	private void Relay_True_40()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.True(out logic_uScriptAct_SetBool_Target_40);
		local_NPCToBuildSet_System_Boolean = logic_uScriptAct_SetBool_Target_40;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_40.Out)
		{
			Relay_True_59();
		}
	}

	private void Relay_False_40()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.False(out logic_uScriptAct_SetBool_Target_40);
		local_NPCToBuildSet_System_Boolean = logic_uScriptAct_SetBool_Target_40;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_40.Out)
		{
			Relay_True_59();
		}
	}

	private void Relay_In_41()
	{
		logic_uScriptCon_CompareBool_Bool_41 = local_TechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_41.In(logic_uScriptCon_CompareBool_Bool_41);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_41.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_41.False;
		if (num)
		{
			Relay_In_58();
		}
		if (flag)
		{
			Relay_In_39();
		}
	}

	private void Relay_In_43()
	{
		logic_uScript_SetTutorialTechToBuild_completedTechPreset_43 = CompletedNPCPreset;
		logic_uScript_SetTutorialTechToBuild_tutorialBuildTech_43 = local_NPCTech_Tank;
		logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_43.In(logic_uScript_SetTutorialTechToBuild_completedTechPreset_43, logic_uScript_SetTutorialTechToBuild_tutorialBuildTech_43);
		if (logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_43.Out)
		{
			Relay_True_40();
		}
	}

	private void Relay_InitialSpawn_44()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_44.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_44, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_44, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_44 = owner_Connection_46;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_44.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_44, logic_uScript_SpawnTechsFromData_ownerNode_44, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_44, logic_uScript_SpawnTechsFromData_allowResurrection_44);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_44.Out)
		{
			Relay_InitialSpawn_109();
		}
	}

	private void Relay_True_45()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.True(out logic_uScriptAct_SetBool_Target_45);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_45;
	}

	private void Relay_False_45()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.False(out logic_uScriptAct_SetBool_Target_45);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_45;
	}

	private void Relay_In_47()
	{
		logic_uScriptCon_CompareBool_Bool_47 = local_NPCToBuildSet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.In(logic_uScriptCon_CompareBool_Bool_47);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_47.False;
		if (num)
		{
			Relay_True_59();
		}
		if (flag)
		{
			Relay_In_43();
		}
	}

	private void Relay_In_50()
	{
		logic_uScript_SetTankInvulnerable_tank_50 = local_NPCTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_50.In(logic_uScript_SetTankInvulnerable_invulnerable_50, logic_uScript_SetTankInvulnerable_tank_50);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_50.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_AtIndex_53()
	{
		int num = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_53.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_53, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_53, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_53.AtIndex(ref logic_uScript_AccessListTech_techList_53, logic_uScript_AccessListTech_index_53, out logic_uScript_AccessListTech_value_53);
		local_NPCTechs_TankArray = logic_uScript_AccessListTech_techList_53;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_53;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_53.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_54()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_54.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_54, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_54, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_54 = owner_Connection_51;
		int num2 = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_54.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_54, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_54, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_54 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_54.In(logic_uScript_GetAndCheckTechs_techData_54, logic_uScript_GetAndCheckTechs_ownerNode_54, ref logic_uScript_GetAndCheckTechs_techs_54);
		local_NPCTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_54;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_54.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_54.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_53();
		}
		if (someAlive)
		{
			Relay_AtIndex_53();
		}
	}

	private void Relay_In_58()
	{
		logic_uScriptCon_CompareBool_Bool_58 = local_TechsSetup_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.In(logic_uScriptCon_CompareBool_Bool_58);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.False;
		if (num)
		{
			Relay_In_123();
		}
		if (flag)
		{
			Relay_In_94();
		}
	}

	private void Relay_True_59()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_59.True(out logic_uScriptAct_SetBool_Target_59);
		local_TechsSetup_System_Boolean = logic_uScriptAct_SetBool_Target_59;
	}

	private void Relay_False_59()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_59.False(out logic_uScriptAct_SetBool_Target_59);
		local_TechsSetup_System_Boolean = logic_uScriptAct_SetBool_Target_59;
	}

	private void Relay_In_68()
	{
		logic_uScript_SetTankHideBlockLimit_tech_68 = local_NPCTech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_68.In(logic_uScript_SetTankHideBlockLimit_hidden_68, logic_uScript_SetTankHideBlockLimit_tech_68);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_68.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_In_69()
	{
		logic_uScript_SetCustomRadarTeamID_tech_69 = local_NPCTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_69.In(logic_uScript_SetCustomRadarTeamID_tech_69, logic_uScript_SetCustomRadarTeamID_radarTeamID_69);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_69.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_True_74()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_74.True(out logic_uScriptAct_SetBool_Target_74);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_74;
	}

	private void Relay_False_74()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_74.False(out logic_uScriptAct_SetBool_Target_74);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_74;
	}

	private void Relay_In_76()
	{
		logic_uScript_IsTechInTrigger_triggerAreaName_76 = MinefieldTrigger;
		int num = 0;
		Array array = local_TechsInMinefield_TankArray;
		if (logic_uScript_IsTechInTrigger_techs_76.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_IsTechInTrigger_techs_76, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_IsTechInTrigger_techs_76, num, array.Length);
		num += array.Length;
		logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_76.In(logic_uScript_IsTechInTrigger_triggerAreaName_76, ref logic_uScript_IsTechInTrigger_techs_76);
		local_TechsInMinefield_TankArray = logic_uScript_IsTechInTrigger_techs_76;
		bool inRange = logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_76.InRange;
		bool outOfRange = logic_uScript_IsTechInTrigger_uScript_IsTechInTrigger_76.OutOfRange;
		if (inRange)
		{
			Relay_AtIndex_80();
		}
		if (outOfRange)
		{
			Relay_In_435();
		}
	}

	private void Relay_In_79()
	{
		logic_uScript_IsTechPlayer_tech_79 = local_MinefieldTech_Tank;
		logic_uScript_IsTechPlayer_uScript_IsTechPlayer_79.In(logic_uScript_IsTechPlayer_tech_79);
		bool num = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_79.True;
		bool flag = logic_uScript_IsTechPlayer_uScript_IsTechPlayer_79.False;
		if (num)
		{
			Relay_In_83();
		}
		if (flag)
		{
			Relay_In_82();
		}
	}

	private void Relay_AtIndex_80()
	{
		int num = 0;
		Array array = local_TechsInMinefield_TankArray;
		if (logic_uScript_AccessListTech_techList_80.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_80, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_80, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_80.AtIndex(ref logic_uScript_AccessListTech_techList_80, logic_uScript_AccessListTech_index_80, out logic_uScript_AccessListTech_value_80);
		local_TechsInMinefield_TankArray = logic_uScript_AccessListTech_techList_80;
		local_MinefieldTech_Tank = logic_uScript_AccessListTech_value_80;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_80.Out)
		{
			Relay_In_454();
		}
	}

	private void Relay_In_82()
	{
		int num = 0;
		Array array = local_TechsInMinefield_TankArray;
		if (logic_uScript_DamageTechs_techs_82.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_82, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_82, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_82.In(logic_uScript_DamageTechs_techs_82, logic_uScript_DamageTechs_dmgPercent_82, logic_uScript_DamageTechs_givePlyrCredit_82, logic_uScript_DamageTechs_leaveBlksPercent_82, logic_uScript_DamageTechs_makeVulnerable_82);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_82.Out)
		{
			Relay_True_88();
		}
	}

	private void Relay_In_83()
	{
		int num = 0;
		Array array = local_TechsInMinefield_TankArray;
		if (logic_uScript_DamageTechs_techs_83.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_83, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageTechs_techs_83, num, array.Length);
		num += array.Length;
		logic_uScript_DamageTechs_uScript_DamageTechs_83.In(logic_uScript_DamageTechs_techs_83, logic_uScript_DamageTechs_dmgPercent_83, logic_uScript_DamageTechs_givePlyrCredit_83, logic_uScript_DamageTechs_leaveBlksPercent_83, logic_uScript_DamageTechs_makeVulnerable_83);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_83.Out)
		{
			Relay_True_86();
		}
	}

	private void Relay_True_86()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_86.True(out logic_uScriptAct_SetBool_Target_86);
		local_PlayerHitAMine_System_Boolean = logic_uScriptAct_SetBool_Target_86;
	}

	private void Relay_False_86()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_86.False(out logic_uScriptAct_SetBool_Target_86);
		local_PlayerHitAMine_System_Boolean = logic_uScriptAct_SetBool_Target_86;
	}

	private void Relay_True_88()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_88.True(out logic_uScriptAct_SetBool_Target_88);
		local_EnemyHitAMine_System_Boolean = logic_uScriptAct_SetBool_Target_88;
	}

	private void Relay_False_88()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_88.False(out logic_uScriptAct_SetBool_Target_88);
		local_EnemyHitAMine_System_Boolean = logic_uScriptAct_SetBool_Target_88;
	}

	private void Relay_Save_Out_92()
	{
		Relay_Save_93();
	}

	private void Relay_Load_Out_92()
	{
		Relay_Load_93();
	}

	private void Relay_Restart_Out_92()
	{
		Relay_Set_False_93();
	}

	private void Relay_Save_92()
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = local_PlayerHitAMine_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_92 = local_PlayerHitAMine_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Save(ref logic_SubGraph_SaveLoadBool_boolean_92, logic_SubGraph_SaveLoadBool_boolAsVariable_92, logic_SubGraph_SaveLoadBool_uniqueID_92);
	}

	private void Relay_Load_92()
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = local_PlayerHitAMine_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_92 = local_PlayerHitAMine_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Load(ref logic_SubGraph_SaveLoadBool_boolean_92, logic_SubGraph_SaveLoadBool_boolAsVariable_92, logic_SubGraph_SaveLoadBool_uniqueID_92);
	}

	private void Relay_Set_True_92()
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = local_PlayerHitAMine_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_92 = local_PlayerHitAMine_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_92, logic_SubGraph_SaveLoadBool_boolAsVariable_92, logic_SubGraph_SaveLoadBool_uniqueID_92);
	}

	private void Relay_Set_False_92()
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = local_PlayerHitAMine_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_92 = local_PlayerHitAMine_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_92, logic_SubGraph_SaveLoadBool_boolAsVariable_92, logic_SubGraph_SaveLoadBool_uniqueID_92);
	}

	private void Relay_Save_Out_93()
	{
		Relay_Save_105();
	}

	private void Relay_Load_Out_93()
	{
		Relay_Load_105();
	}

	private void Relay_Restart_Out_93()
	{
		Relay_Set_False_105();
	}

	private void Relay_Save_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_EnemyHitAMine_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_EnemyHitAMine_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Save(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Load_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_EnemyHitAMine_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_EnemyHitAMine_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Load(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Set_True_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_EnemyHitAMine_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_EnemyHitAMine_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Set_False_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_EnemyHitAMine_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_EnemyHitAMine_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_In_94()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_94.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_94.Out)
		{
			Relay_In_121();
		}
	}

	private void Relay_In_95()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_95 = owner_Connection_96;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_95.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_95);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_95.True)
		{
			Relay_In_76();
		}
	}

	private void Relay_True_97()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_97.True(out logic_uScriptAct_SetBool_Target_97);
		local_ArrivedAtMineIsland_System_Boolean = logic_uScriptAct_SetBool_Target_97;
	}

	private void Relay_False_97()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_97.False(out logic_uScriptAct_SetBool_Target_97);
		local_ArrivedAtMineIsland_System_Boolean = logic_uScriptAct_SetBool_Target_97;
	}

	private void Relay_True_99()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_99.True(out logic_uScriptAct_SetBool_Target_99);
		local_ReachedMineIsland_System_Boolean = logic_uScriptAct_SetBool_Target_99;
	}

	private void Relay_False_99()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_99.False(out logic_uScriptAct_SetBool_Target_99);
		local_ReachedMineIsland_System_Boolean = logic_uScriptAct_SetBool_Target_99;
	}

	private void Relay_In_101()
	{
		logic_uScriptCon_CompareBool_Bool_101 = local_ReachedMineIsland_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.In(logic_uScriptCon_CompareBool_Bool_101);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.False;
		if (num)
		{
			Relay_In_237();
		}
		if (flag)
		{
			Relay_In_167();
		}
	}

	private void Relay_Save_Out_105()
	{
		Relay_Save_106();
	}

	private void Relay_Load_Out_105()
	{
		Relay_Load_106();
	}

	private void Relay_Restart_Out_105()
	{
		Relay_Set_False_106();
	}

	private void Relay_Save_105()
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = local_ReachedMineIsland_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_105 = local_ReachedMineIsland_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Save(ref logic_SubGraph_SaveLoadBool_boolean_105, logic_SubGraph_SaveLoadBool_boolAsVariable_105, logic_SubGraph_SaveLoadBool_uniqueID_105);
	}

	private void Relay_Load_105()
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = local_ReachedMineIsland_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_105 = local_ReachedMineIsland_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Load(ref logic_SubGraph_SaveLoadBool_boolean_105, logic_SubGraph_SaveLoadBool_boolAsVariable_105, logic_SubGraph_SaveLoadBool_uniqueID_105);
	}

	private void Relay_Set_True_105()
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = local_ReachedMineIsland_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_105 = local_ReachedMineIsland_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_105, logic_SubGraph_SaveLoadBool_boolAsVariable_105, logic_SubGraph_SaveLoadBool_uniqueID_105);
	}

	private void Relay_Set_False_105()
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = local_ReachedMineIsland_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_105 = local_ReachedMineIsland_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_105, logic_SubGraph_SaveLoadBool_boolAsVariable_105, logic_SubGraph_SaveLoadBool_uniqueID_105);
	}

	private void Relay_Save_Out_106()
	{
		Relay_Save_149();
	}

	private void Relay_Load_Out_106()
	{
		Relay_Load_149();
	}

	private void Relay_Restart_Out_106()
	{
		Relay_Set_False_149();
	}

	private void Relay_Save_106()
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = local_ArrivedAtMineIsland_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_106 = local_ArrivedAtMineIsland_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Save(ref logic_SubGraph_SaveLoadBool_boolean_106, logic_SubGraph_SaveLoadBool_boolAsVariable_106, logic_SubGraph_SaveLoadBool_uniqueID_106);
	}

	private void Relay_Load_106()
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = local_ArrivedAtMineIsland_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_106 = local_ArrivedAtMineIsland_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Load(ref logic_SubGraph_SaveLoadBool_boolean_106, logic_SubGraph_SaveLoadBool_boolAsVariable_106, logic_SubGraph_SaveLoadBool_uniqueID_106);
	}

	private void Relay_Set_True_106()
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = local_ArrivedAtMineIsland_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_106 = local_ArrivedAtMineIsland_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_106, logic_SubGraph_SaveLoadBool_boolAsVariable_106, logic_SubGraph_SaveLoadBool_uniqueID_106);
	}

	private void Relay_Set_False_106()
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = local_ArrivedAtMineIsland_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_106 = local_ArrivedAtMineIsland_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_106, logic_SubGraph_SaveLoadBool_boolAsVariable_106, logic_SubGraph_SaveLoadBool_uniqueID_106);
	}

	private void Relay_InitialSpawn_109()
	{
		int num = 0;
		Array bossSpawnData = BossSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_109.Length != num + bossSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_109, num + bossSpawnData.Length);
		}
		Array.Copy(bossSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_109, num, bossSpawnData.Length);
		num += bossSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_109 = owner_Connection_107;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_109.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_109, logic_uScript_SpawnTechsFromData_ownerNode_109, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_109, logic_uScript_SpawnTechsFromData_allowResurrection_109);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_109.Out)
		{
			Relay_InitialSpawn_240();
		}
	}

	private void Relay_AtIndex_111()
	{
		int num = 0;
		Array array = local_BossTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_111.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_111, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_111, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_111.AtIndex(ref logic_uScript_AccessListTech_techList_111, logic_uScript_AccessListTech_index_111, out logic_uScript_AccessListTech_value_111);
		local_BossTechs_TankArray = logic_uScript_AccessListTech_techList_111;
		local_BossTech_Tank = logic_uScript_AccessListTech_value_111;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_111.Out)
		{
			Relay_In_113();
		}
	}

	private void Relay_In_112()
	{
		logic_uScript_SetCustomRadarTeamID_tech_112 = local_BossTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_112.In(logic_uScript_SetCustomRadarTeamID_tech_112, logic_uScript_SetCustomRadarTeamID_radarTeamID_112);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_112.Out)
		{
			Relay_In_246();
		}
	}

	private void Relay_In_113()
	{
		logic_uScript_SetEncounterTarget_owner_113 = owner_Connection_114;
		logic_uScript_SetEncounterTarget_visibleObject_113 = local_BossTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_113.In(logic_uScript_SetEncounterTarget_owner_113, logic_uScript_SetEncounterTarget_visibleObject_113);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_113.Out)
		{
			Relay_In_112();
		}
	}

	private void Relay_In_116()
	{
		int num = 0;
		Array bossSpawnData = BossSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_116.Length != num + bossSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_116, num + bossSpawnData.Length);
		}
		Array.Copy(bossSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_116, num, bossSpawnData.Length);
		num += bossSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_116 = owner_Connection_115;
		int num2 = 0;
		Array array = local_BossTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_116.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_116, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_116, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_116 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116.In(logic_uScript_GetAndCheckTechs_techData_116, logic_uScript_GetAndCheckTechs_ownerNode_116, ref logic_uScript_GetAndCheckTechs_techs_116);
		local_BossTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_116;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_111();
		}
		if (someAlive)
		{
			Relay_AtIndex_111();
		}
	}

	private void Relay_In_121()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_121.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_121.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_122()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_122 = local_BossTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_122.In(logic_uScript_IsPlayerInRangeOfTech_tech_122, logic_uScript_IsPlayerInRangeOfTech_range_122, logic_uScript_IsPlayerInRangeOfTech_techs_122);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_122.InRange)
		{
			Relay_In_131();
		}
	}

	private void Relay_In_123()
	{
		logic_uScriptCon_CompareBool_Bool_123 = local_ArrivedAtMineIsland_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_123.In(logic_uScriptCon_CompareBool_Bool_123);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_123.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_123.False;
		if (num)
		{
			Relay_In_223();
		}
		if (flag)
		{
			Relay_In_168();
		}
	}

	private void Relay_In_125()
	{
		logic_uScript_AddMessage_messageData_125 = msgMeetBoss;
		logic_uScript_AddMessage_speaker_125 = messageBossSpeaker;
		logic_uScript_AddMessage_Return_125 = logic_uScript_AddMessage_uScript_AddMessage_125.In(logic_uScript_AddMessage_messageData_125, logic_uScript_AddMessage_speaker_125);
		if (logic_uScript_AddMessage_uScript_AddMessage_125.Shown)
		{
			Relay_True_126();
		}
	}

	private void Relay_True_126()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_126.True(out logic_uScriptAct_SetBool_Target_126);
		local_MetBoss_System_Boolean = logic_uScriptAct_SetBool_Target_126;
	}

	private void Relay_False_126()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_126.False(out logic_uScriptAct_SetBool_Target_126);
		local_MetBoss_System_Boolean = logic_uScriptAct_SetBool_Target_126;
	}

	private void Relay_In_131()
	{
		logic_uScriptCon_CompareBool_Bool_131 = local_MetBoss_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.In(logic_uScriptCon_CompareBool_Bool_131);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.False;
		if (num)
		{
			Relay_In_134();
		}
		if (flag)
		{
			Relay_In_175();
		}
	}

	private void Relay_True_132()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.True(out logic_uScriptAct_SetBool_Target_132);
		local_NPCCallsForHelp_System_Boolean = logic_uScriptAct_SetBool_Target_132;
	}

	private void Relay_False_132()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.False(out logic_uScriptAct_SetBool_Target_132);
		local_NPCCallsForHelp_System_Boolean = logic_uScriptAct_SetBool_Target_132;
	}

	private void Relay_In_134()
	{
		logic_uScriptCon_CompareBool_Bool_134 = local_NPCCallsForHelp_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_134.In(logic_uScriptCon_CompareBool_Bool_134);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_134.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_134.False;
		if (num)
		{
			Relay_In_142();
		}
		if (flag)
		{
			Relay_In_137();
		}
	}

	private void Relay_In_137()
	{
		logic_uScript_AddMessage_messageData_137 = msgNPCCallsForHelp;
		logic_uScript_AddMessage_speaker_137 = messageNPCSpeaker;
		logic_uScript_AddMessage_Return_137 = logic_uScript_AddMessage_uScript_AddMessage_137.In(logic_uScript_AddMessage_messageData_137, logic_uScript_AddMessage_speaker_137);
		if (logic_uScript_AddMessage_uScript_AddMessage_137.Shown)
		{
			Relay_True_132();
		}
	}

	private void Relay_True_140()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_140.True(out logic_uScriptAct_SetBool_Target_140);
		local_BossResponds_System_Boolean = logic_uScriptAct_SetBool_Target_140;
	}

	private void Relay_False_140()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_140.False(out logic_uScriptAct_SetBool_Target_140);
		local_BossResponds_System_Boolean = logic_uScriptAct_SetBool_Target_140;
	}

	private void Relay_In_142()
	{
		logic_uScriptCon_CompareBool_Bool_142 = local_BossResponds_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_142.In(logic_uScriptCon_CompareBool_Bool_142);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_142.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_142.False;
		if (num)
		{
			Relay_In_158();
		}
		if (flag)
		{
			Relay_In_143();
		}
	}

	private void Relay_In_143()
	{
		logic_uScript_AddMessage_messageData_143 = msgBossResponds;
		logic_uScript_AddMessage_speaker_143 = messageBossSpeaker;
		logic_uScript_AddMessage_Return_143 = logic_uScript_AddMessage_uScript_AddMessage_143.In(logic_uScript_AddMessage_messageData_143, logic_uScript_AddMessage_speaker_143);
		if (logic_uScript_AddMessage_uScript_AddMessage_143.Shown)
		{
			Relay_True_140();
		}
	}

	private void Relay_Save_Out_149()
	{
		Relay_Save_150();
	}

	private void Relay_Load_Out_149()
	{
		Relay_Load_150();
	}

	private void Relay_Restart_Out_149()
	{
		Relay_Set_False_150();
	}

	private void Relay_Save_149()
	{
		logic_SubGraph_SaveLoadBool_boolean_149 = local_MetBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_149 = local_MetBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Save(ref logic_SubGraph_SaveLoadBool_boolean_149, logic_SubGraph_SaveLoadBool_boolAsVariable_149, logic_SubGraph_SaveLoadBool_uniqueID_149);
	}

	private void Relay_Load_149()
	{
		logic_SubGraph_SaveLoadBool_boolean_149 = local_MetBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_149 = local_MetBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Load(ref logic_SubGraph_SaveLoadBool_boolean_149, logic_SubGraph_SaveLoadBool_boolAsVariable_149, logic_SubGraph_SaveLoadBool_uniqueID_149);
	}

	private void Relay_Set_True_149()
	{
		logic_SubGraph_SaveLoadBool_boolean_149 = local_MetBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_149 = local_MetBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_149, logic_SubGraph_SaveLoadBool_boolAsVariable_149, logic_SubGraph_SaveLoadBool_uniqueID_149);
	}

	private void Relay_Set_False_149()
	{
		logic_SubGraph_SaveLoadBool_boolean_149 = local_MetBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_149 = local_MetBoss_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_149.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_149, logic_SubGraph_SaveLoadBool_boolAsVariable_149, logic_SubGraph_SaveLoadBool_uniqueID_149);
	}

	private void Relay_Save_Out_150()
	{
		Relay_Save_151();
	}

	private void Relay_Load_Out_150()
	{
		Relay_Load_151();
	}

	private void Relay_Restart_Out_150()
	{
		Relay_Set_False_151();
	}

	private void Relay_Save_150()
	{
		logic_SubGraph_SaveLoadBool_boolean_150 = local_NPCCallsForHelp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_150 = local_NPCCallsForHelp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Save(ref logic_SubGraph_SaveLoadBool_boolean_150, logic_SubGraph_SaveLoadBool_boolAsVariable_150, logic_SubGraph_SaveLoadBool_uniqueID_150);
	}

	private void Relay_Load_150()
	{
		logic_SubGraph_SaveLoadBool_boolean_150 = local_NPCCallsForHelp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_150 = local_NPCCallsForHelp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Load(ref logic_SubGraph_SaveLoadBool_boolean_150, logic_SubGraph_SaveLoadBool_boolAsVariable_150, logic_SubGraph_SaveLoadBool_uniqueID_150);
	}

	private void Relay_Set_True_150()
	{
		logic_SubGraph_SaveLoadBool_boolean_150 = local_NPCCallsForHelp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_150 = local_NPCCallsForHelp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_150, logic_SubGraph_SaveLoadBool_boolAsVariable_150, logic_SubGraph_SaveLoadBool_uniqueID_150);
	}

	private void Relay_Set_False_150()
	{
		logic_SubGraph_SaveLoadBool_boolean_150 = local_NPCCallsForHelp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_150 = local_NPCCallsForHelp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_150, logic_SubGraph_SaveLoadBool_boolAsVariable_150, logic_SubGraph_SaveLoadBool_uniqueID_150);
	}

	private void Relay_Save_Out_151()
	{
		Relay_Save_189();
	}

	private void Relay_Load_Out_151()
	{
		Relay_Load_189();
	}

	private void Relay_Restart_Out_151()
	{
		Relay_Set_False_189();
	}

	private void Relay_Save_151()
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = local_BossResponds_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_151 = local_BossResponds_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Save(ref logic_SubGraph_SaveLoadBool_boolean_151, logic_SubGraph_SaveLoadBool_boolAsVariable_151, logic_SubGraph_SaveLoadBool_uniqueID_151);
	}

	private void Relay_Load_151()
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = local_BossResponds_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_151 = local_BossResponds_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Load(ref logic_SubGraph_SaveLoadBool_boolean_151, logic_SubGraph_SaveLoadBool_boolAsVariable_151, logic_SubGraph_SaveLoadBool_uniqueID_151);
	}

	private void Relay_Set_True_151()
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = local_BossResponds_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_151 = local_BossResponds_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_151, logic_SubGraph_SaveLoadBool_boolAsVariable_151, logic_SubGraph_SaveLoadBool_uniqueID_151);
	}

	private void Relay_Set_False_151()
	{
		logic_SubGraph_SaveLoadBool_boolean_151 = local_BossResponds_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_151 = local_BossResponds_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_151.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_151, logic_SubGraph_SaveLoadBool_boolAsVariable_151, logic_SubGraph_SaveLoadBool_uniqueID_151);
	}

	private void Relay_In_154()
	{
		logic_uScript_SetTankTeam_tank_154 = local_BossTech_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_154.In(logic_uScript_SetTankTeam_tank_154, logic_uScript_SetTankTeam_team_154);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_154.Out)
		{
			Relay_True_160();
		}
	}

	private void Relay_In_157()
	{
		int num = 0;
		Array bossSpawnData = BossSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_157.Length != num + bossSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_157, num + bossSpawnData.Length);
		}
		Array.Copy(bossSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_157, num, bossSpawnData.Length);
		num += bossSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_157 = owner_Connection_155;
		int num2 = 0;
		Array array = local_BossTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_157.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_157, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_157, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_157 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_157.In(logic_uScript_GetAndCheckTechs_techData_157, logic_uScript_GetAndCheckTechs_ownerNode_157, ref logic_uScript_GetAndCheckTechs_techs_157);
		local_BossTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_157;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_157.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_157.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_157.AllDead;
		if (allAlive)
		{
			Relay_In_172();
		}
		if (someAlive)
		{
			Relay_In_172();
		}
		if (allDead)
		{
			Relay_In_431();
		}
	}

	private void Relay_In_158()
	{
		logic_uScriptCon_CompareBool_Bool_158 = local_BossTriggered_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_158.In(logic_uScriptCon_CompareBool_Bool_158);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_158.False)
		{
			Relay_In_154();
		}
	}

	private void Relay_True_160()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_160.True(out logic_uScriptAct_SetBool_Target_160);
		local_BossTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_160;
	}

	private void Relay_False_160()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_160.False(out logic_uScriptAct_SetBool_Target_160);
		local_BossTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_160;
	}

	private void Relay_In_162()
	{
		logic_uScriptCon_CompareBool_Bool_162 = local_BossDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162.In(logic_uScriptCon_CompareBool_Bool_162);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162.False;
		if (num)
		{
			Relay_In_372();
		}
		if (flag)
		{
			Relay_In_452();
		}
	}

	private void Relay_True_164()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_164.True(out logic_uScriptAct_SetBool_Target_164);
		local_BossDead_System_Boolean = logic_uScriptAct_SetBool_Target_164;
	}

	private void Relay_False_164()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_164.False(out logic_uScriptAct_SetBool_Target_164);
		local_BossDead_System_Boolean = logic_uScriptAct_SetBool_Target_164;
	}

	private void Relay_Out_166()
	{
		Relay_In_383();
	}

	private void Relay_In_166()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_166 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_166.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_166, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_166);
	}

	private void Relay_Out_167()
	{
		Relay_True_99();
	}

	private void Relay_In_167()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_167 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_167.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_167, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_167);
	}

	private void Relay_Out_168()
	{
		Relay_True_97();
	}

	private void Relay_In_168()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_168 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_168.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_168, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_168);
	}

	private void Relay_In_169()
	{
		logic_uScript_GetPositionOfTech_tech_169 = local_BossTech_Tank;
		logic_uScript_GetPositionOfTech_Return_169 = logic_uScript_GetPositionOfTech_uScript_GetPositionOfTech_169.In(logic_uScript_GetPositionOfTech_tech_169);
		local_BossLastKnownPosition_UnityEngine_Vector3 = logic_uScript_GetPositionOfTech_Return_169;
		if (logic_uScript_GetPositionOfTech_uScript_GetPositionOfTech_169.Out)
		{
			Relay_In_209();
		}
	}

	private void Relay_In_172()
	{
		logic_uScript_IsVisibleInTrigger_visibleObject_172 = local_BossTech_Tank;
		logic_uScript_IsVisibleInTrigger_triggerAreaName_172 = MineIslandTrigger;
		logic_uScript_IsVisibleInTrigger_uScript_IsVisibleInTrigger_172.In(logic_uScript_IsVisibleInTrigger_visibleObject_172, logic_uScript_IsVisibleInTrigger_triggerAreaName_172);
		if (logic_uScript_IsVisibleInTrigger_uScript_IsVisibleInTrigger_172.InRange)
		{
			Relay_In_169();
		}
	}

	private void Relay_In_175()
	{
		logic_uScript_SetBatteryChargeAmount_tech_175 = local_BossTech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_175.In(logic_uScript_SetBatteryChargeAmount_tech_175, logic_uScript_SetBatteryChargeAmount_chargeAmount_175);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_175.Out)
		{
			Relay_In_125();
		}
	}

	private void Relay_In_178()
	{
		logic_uScript_SetCustomRadarTeamID_tech_178 = local_NPCTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_178.In(logic_uScript_SetCustomRadarTeamID_tech_178, logic_uScript_SetCustomRadarTeamID_radarTeamID_178);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_178.Out)
		{
			Relay_In_166();
		}
	}

	private void Relay_In_179()
	{
		logic_uScript_SetTankTeam_tank_179 = local_NPCTech_Tank;
		logic_uScript_SetTankTeam_team_179 = local_PlayerTeam_System_Int32;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_179.In(logic_uScript_SetTankTeam_tank_179, logic_uScript_SetTankTeam_team_179);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_179.Out)
		{
			Relay_In_178();
		}
	}

	private void Relay_True_180()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_180.True(out logic_uScriptAct_SetBool_Target_180);
		local_msgBossDeadShown_System_Boolean = logic_uScriptAct_SetBool_Target_180;
	}

	private void Relay_False_180()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_180.False(out logic_uScriptAct_SetBool_Target_180);
		local_msgBossDeadShown_System_Boolean = logic_uScriptAct_SetBool_Target_180;
	}

	private void Relay_In_183()
	{
		logic_uScriptCon_CompareBool_Bool_183 = local_msgBossDeadShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.In(logic_uScriptCon_CompareBool_Bool_183);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.False;
		if (num)
		{
			Relay_In_184();
		}
		if (flag)
		{
			Relay_In_185();
		}
	}

	private void Relay_In_184()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_184.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_184.Out)
		{
			Relay_In_6();
		}
	}

	private void Relay_In_185()
	{
		logic_uScript_AddMessage_messageData_185 = msgBossDead;
		logic_uScript_AddMessage_speaker_185 = messageNPCSpeaker;
		logic_uScript_AddMessage_Return_185 = logic_uScript_AddMessage_uScript_AddMessage_185.In(logic_uScript_AddMessage_messageData_185, logic_uScript_AddMessage_speaker_185);
		if (logic_uScript_AddMessage_uScript_AddMessage_185.Out)
		{
			Relay_True_180();
		}
	}

	private void Relay_Save_Out_189()
	{
		Relay_Save_197();
	}

	private void Relay_Load_Out_189()
	{
		Relay_Load_197();
	}

	private void Relay_Restart_Out_189()
	{
		Relay_Set_False_197();
	}

	private void Relay_Save_189()
	{
		logic_SubGraph_SaveLoadBool_boolean_189 = local_msgBossDeadShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_189 = local_msgBossDeadShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.Save(ref logic_SubGraph_SaveLoadBool_boolean_189, logic_SubGraph_SaveLoadBool_boolAsVariable_189, logic_SubGraph_SaveLoadBool_uniqueID_189);
	}

	private void Relay_Load_189()
	{
		logic_SubGraph_SaveLoadBool_boolean_189 = local_msgBossDeadShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_189 = local_msgBossDeadShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.Load(ref logic_SubGraph_SaveLoadBool_boolean_189, logic_SubGraph_SaveLoadBool_boolAsVariable_189, logic_SubGraph_SaveLoadBool_uniqueID_189);
	}

	private void Relay_Set_True_189()
	{
		logic_SubGraph_SaveLoadBool_boolean_189 = local_msgBossDeadShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_189 = local_msgBossDeadShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_189, logic_SubGraph_SaveLoadBool_boolAsVariable_189, logic_SubGraph_SaveLoadBool_uniqueID_189);
	}

	private void Relay_Set_False_189()
	{
		logic_SubGraph_SaveLoadBool_boolean_189 = local_msgBossDeadShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_189 = local_msgBossDeadShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_189.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_189, logic_SubGraph_SaveLoadBool_boolAsVariable_189, logic_SubGraph_SaveLoadBool_uniqueID_189);
	}

	private void Relay_In_190()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_190.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_190.Out)
		{
			Relay_In_410();
		}
	}

	private void Relay_In_192()
	{
		logic_uScriptCon_CompareBool_Bool_192 = local_msgNothingToSeeHereShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192.In(logic_uScriptCon_CompareBool_Bool_192);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192.False;
		if (num)
		{
			Relay_In_190();
		}
		if (flag)
		{
			Relay_In_193();
		}
	}

	private void Relay_In_193()
	{
		logic_uScript_AddMessage_messageData_193 = msgNothingToSeeHere;
		logic_uScript_AddMessage_speaker_193 = messageBossSpeaker;
		logic_uScript_AddMessage_Return_193 = logic_uScript_AddMessage_uScript_AddMessage_193.In(logic_uScript_AddMessage_messageData_193, logic_uScript_AddMessage_speaker_193);
		if (logic_uScript_AddMessage_uScript_AddMessage_193.Out)
		{
			Relay_True_194();
		}
	}

	private void Relay_True_194()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_194.True(out logic_uScriptAct_SetBool_Target_194);
		local_msgNothingToSeeHereShown_System_Boolean = logic_uScriptAct_SetBool_Target_194;
	}

	private void Relay_False_194()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_194.False(out logic_uScriptAct_SetBool_Target_194);
		local_msgNothingToSeeHereShown_System_Boolean = logic_uScriptAct_SetBool_Target_194;
	}

	private void Relay_Save_Out_197()
	{
		Relay_Save_202();
	}

	private void Relay_Load_Out_197()
	{
		Relay_Load_202();
	}

	private void Relay_Restart_Out_197()
	{
		Relay_Set_False_202();
	}

	private void Relay_Save_197()
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = local_msgNothingToSeeHereShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_197 = local_msgNothingToSeeHereShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Save(ref logic_SubGraph_SaveLoadBool_boolean_197, logic_SubGraph_SaveLoadBool_boolAsVariable_197, logic_SubGraph_SaveLoadBool_uniqueID_197);
	}

	private void Relay_Load_197()
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = local_msgNothingToSeeHereShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_197 = local_msgNothingToSeeHereShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Load(ref logic_SubGraph_SaveLoadBool_boolean_197, logic_SubGraph_SaveLoadBool_boolAsVariable_197, logic_SubGraph_SaveLoadBool_uniqueID_197);
	}

	private void Relay_Set_True_197()
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = local_msgNothingToSeeHereShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_197 = local_msgNothingToSeeHereShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_197, logic_SubGraph_SaveLoadBool_boolAsVariable_197, logic_SubGraph_SaveLoadBool_uniqueID_197);
	}

	private void Relay_Set_False_197()
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = local_msgNothingToSeeHereShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_197 = local_msgNothingToSeeHereShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_197, logic_SubGraph_SaveLoadBool_boolAsVariable_197, logic_SubGraph_SaveLoadBool_uniqueID_197);
	}

	private void Relay_In_198()
	{
		logic_uScript_AddMessage_messageData_198 = msgReachedMineIsland;
		logic_uScript_AddMessage_speaker_198 = messageNPCSpeaker;
		logic_uScript_AddMessage_Return_198 = logic_uScript_AddMessage_uScript_AddMessage_198.In(logic_uScript_AddMessage_messageData_198, logic_uScript_AddMessage_speaker_198);
		if (logic_uScript_AddMessage_uScript_AddMessage_198.Shown)
		{
			Relay_True_234();
		}
	}

	private void Relay_Save_Out_202()
	{
		Relay_Save_208();
	}

	private void Relay_Load_Out_202()
	{
		Relay_Load_208();
	}

	private void Relay_Restart_Out_202()
	{
		Relay_Set_False_208();
	}

	private void Relay_Save_202()
	{
		logic_SubGraph_SaveLoadBool_boolean_202 = local_msgHowDareYouShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_202 = local_msgHowDareYouShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Save(ref logic_SubGraph_SaveLoadBool_boolean_202, logic_SubGraph_SaveLoadBool_boolAsVariable_202, logic_SubGraph_SaveLoadBool_uniqueID_202);
	}

	private void Relay_Load_202()
	{
		logic_SubGraph_SaveLoadBool_boolean_202 = local_msgHowDareYouShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_202 = local_msgHowDareYouShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Load(ref logic_SubGraph_SaveLoadBool_boolean_202, logic_SubGraph_SaveLoadBool_boolAsVariable_202, logic_SubGraph_SaveLoadBool_uniqueID_202);
	}

	private void Relay_Set_True_202()
	{
		logic_SubGraph_SaveLoadBool_boolean_202 = local_msgHowDareYouShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_202 = local_msgHowDareYouShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_202, logic_SubGraph_SaveLoadBool_boolAsVariable_202, logic_SubGraph_SaveLoadBool_uniqueID_202);
	}

	private void Relay_Set_False_202()
	{
		logic_SubGraph_SaveLoadBool_boolean_202 = local_msgHowDareYouShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_202 = local_msgHowDareYouShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_202, logic_SubGraph_SaveLoadBool_boolAsVariable_202, logic_SubGraph_SaveLoadBool_uniqueID_202);
	}

	private void Relay_True_204()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_204.True(out logic_uScriptAct_SetBool_Target_204);
		local_AttemptedAlready_System_Boolean = logic_uScriptAct_SetBool_Target_204;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_204.Out)
		{
			Relay_In_426();
		}
	}

	private void Relay_False_204()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_204.False(out logic_uScriptAct_SetBool_Target_204);
		local_AttemptedAlready_System_Boolean = logic_uScriptAct_SetBool_Target_204;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_204.Out)
		{
			Relay_In_426();
		}
	}

	private void Relay_In_205()
	{
		logic_uScriptCon_CompareBool_Bool_205 = local_ReachedMineIsland_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.In(logic_uScriptCon_CompareBool_Bool_205);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.False;
		if (num)
		{
			Relay_True_204();
		}
		if (flag)
		{
			Relay_In_428();
		}
	}

	private void Relay_Save_Out_208()
	{
		Relay_Save_239();
	}

	private void Relay_Load_Out_208()
	{
		Relay_Load_239();
	}

	private void Relay_Restart_Out_208()
	{
		Relay_Set_False_239();
	}

	private void Relay_Save_208()
	{
		logic_SubGraph_SaveLoadBool_boolean_208 = local_AttemptedAlready_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_208 = local_AttemptedAlready_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Save(ref logic_SubGraph_SaveLoadBool_boolean_208, logic_SubGraph_SaveLoadBool_boolAsVariable_208, logic_SubGraph_SaveLoadBool_uniqueID_208);
	}

	private void Relay_Load_208()
	{
		logic_SubGraph_SaveLoadBool_boolean_208 = local_AttemptedAlready_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_208 = local_AttemptedAlready_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Load(ref logic_SubGraph_SaveLoadBool_boolean_208, logic_SubGraph_SaveLoadBool_boolAsVariable_208, logic_SubGraph_SaveLoadBool_uniqueID_208);
	}

	private void Relay_Set_True_208()
	{
		logic_SubGraph_SaveLoadBool_boolean_208 = local_AttemptedAlready_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_208 = local_AttemptedAlready_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_208, logic_SubGraph_SaveLoadBool_boolAsVariable_208, logic_SubGraph_SaveLoadBool_uniqueID_208);
	}

	private void Relay_Set_False_208()
	{
		logic_SubGraph_SaveLoadBool_boolean_208 = local_AttemptedAlready_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_208 = local_AttemptedAlready_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_208.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_208, logic_SubGraph_SaveLoadBool_boolAsVariable_208, logic_SubGraph_SaveLoadBool_uniqueID_208);
	}

	private void Relay_In_209()
	{
		logic_uScriptCon_CompareBool_Bool_209 = local_AttemptedAlready_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_209.In(logic_uScriptCon_CompareBool_Bool_209);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_209.True)
		{
			Relay_In_212();
		}
	}

	private void Relay_In_212()
	{
		logic_uScriptCon_CompareBool_Bool_212 = local_MsgBackAgain_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.In(logic_uScriptCon_CompareBool_Bool_212);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.False)
		{
			Relay_In_213();
		}
	}

	private void Relay_In_213()
	{
		logic_uScript_AddMessage_messageData_213 = msgBackAgain;
		logic_uScript_AddMessage_speaker_213 = messageNPCSpeaker;
		logic_uScript_AddMessage_Return_213 = logic_uScript_AddMessage_uScript_AddMessage_213.In(logic_uScript_AddMessage_messageData_213, logic_uScript_AddMessage_speaker_213);
		if (logic_uScript_AddMessage_uScript_AddMessage_213.Out)
		{
			Relay_True_215();
		}
	}

	private void Relay_True_215()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_215.True(out logic_uScriptAct_SetBool_Target_215);
		local_MsgBackAgain_System_Boolean = logic_uScriptAct_SetBool_Target_215;
	}

	private void Relay_False_215()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_215.False(out logic_uScriptAct_SetBool_Target_215);
		local_MsgBackAgain_System_Boolean = logic_uScriptAct_SetBool_Target_215;
	}

	private void Relay_In_220()
	{
		logic_uScript_AddMessage_messageData_220 = msgArrivedAtMineIsland;
		logic_uScript_AddMessage_speaker_220 = messageNPCSpeaker;
		logic_uScript_AddMessage_Return_220 = logic_uScript_AddMessage_uScript_AddMessage_220.In(logic_uScript_AddMessage_messageData_220, logic_uScript_AddMessage_speaker_220);
		if (logic_uScript_AddMessage_uScript_AddMessage_220.Shown)
		{
			Relay_True_224();
		}
	}

	private void Relay_In_223()
	{
		logic_uScriptCon_CompareBool_Bool_223 = local_msgArrivedAtMineIslandShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.In(logic_uScriptCon_CompareBool_Bool_223);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.False;
		if (num)
		{
			Relay_In_192();
		}
		if (flag)
		{
			Relay_In_220();
		}
	}

	private void Relay_True_224()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_224.True(out logic_uScriptAct_SetBool_Target_224);
		local_msgArrivedAtMineIslandShown_System_Boolean = logic_uScriptAct_SetBool_Target_224;
	}

	private void Relay_False_224()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_224.False(out logic_uScriptAct_SetBool_Target_224);
		local_msgArrivedAtMineIslandShown_System_Boolean = logic_uScriptAct_SetBool_Target_224;
	}

	private void Relay_True_228()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_228.True(out logic_uScriptAct_SetBool_Target_228);
		local_msgHowDareYouShown_System_Boolean = logic_uScriptAct_SetBool_Target_228;
	}

	private void Relay_False_228()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_228.False(out logic_uScriptAct_SetBool_Target_228);
		local_msgHowDareYouShown_System_Boolean = logic_uScriptAct_SetBool_Target_228;
	}

	private void Relay_In_231()
	{
		logic_uScript_AddMessage_messageData_231 = msgHowDareYou;
		logic_uScript_AddMessage_speaker_231 = messageBossSpeaker;
		logic_uScript_AddMessage_Return_231 = logic_uScript_AddMessage_uScript_AddMessage_231.In(logic_uScript_AddMessage_messageData_231, logic_uScript_AddMessage_speaker_231);
		if (logic_uScript_AddMessage_uScript_AddMessage_231.Shown)
		{
			Relay_True_228();
		}
	}

	private void Relay_In_232()
	{
		logic_uScriptCon_CompareBool_Bool_232 = local_msgHowDareYouShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_232.In(logic_uScriptCon_CompareBool_Bool_232);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_232.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_232.False;
		if (num)
		{
			Relay_In_336();
		}
		if (flag)
		{
			Relay_In_231();
		}
	}

	private void Relay_True_234()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_234.True(out logic_uScriptAct_SetBool_Target_234);
		local_msgReachedMineIslandShown_System_Boolean = logic_uScriptAct_SetBool_Target_234;
	}

	private void Relay_False_234()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_234.False(out logic_uScriptAct_SetBool_Target_234);
		local_msgReachedMineIslandShown_System_Boolean = logic_uScriptAct_SetBool_Target_234;
	}

	private void Relay_In_237()
	{
		logic_uScriptCon_CompareBool_Bool_237 = local_msgReachedMineIslandShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_237.In(logic_uScriptCon_CompareBool_Bool_237);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_237.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_237.False;
		if (num)
		{
			Relay_In_232();
		}
		if (flag)
		{
			Relay_In_198();
		}
	}

	private void Relay_Save_Out_239()
	{
		Relay_Save_251();
	}

	private void Relay_Load_Out_239()
	{
		Relay_Load_251();
	}

	private void Relay_Restart_Out_239()
	{
		Relay_Set_False_251();
	}

	private void Relay_Save_239()
	{
		logic_SubGraph_SaveLoadBool_boolean_239 = local_msgReachedMineIslandShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_239 = local_msgReachedMineIslandShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.Save(ref logic_SubGraph_SaveLoadBool_boolean_239, logic_SubGraph_SaveLoadBool_boolAsVariable_239, logic_SubGraph_SaveLoadBool_uniqueID_239);
	}

	private void Relay_Load_239()
	{
		logic_SubGraph_SaveLoadBool_boolean_239 = local_msgReachedMineIslandShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_239 = local_msgReachedMineIslandShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.Load(ref logic_SubGraph_SaveLoadBool_boolean_239, logic_SubGraph_SaveLoadBool_boolAsVariable_239, logic_SubGraph_SaveLoadBool_uniqueID_239);
	}

	private void Relay_Set_True_239()
	{
		logic_SubGraph_SaveLoadBool_boolean_239 = local_msgReachedMineIslandShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_239 = local_msgReachedMineIslandShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_239, logic_SubGraph_SaveLoadBool_boolAsVariable_239, logic_SubGraph_SaveLoadBool_uniqueID_239);
	}

	private void Relay_Set_False_239()
	{
		logic_SubGraph_SaveLoadBool_boolean_239 = local_msgReachedMineIslandShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_239 = local_msgReachedMineIslandShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_239.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_239, logic_SubGraph_SaveLoadBool_boolAsVariable_239, logic_SubGraph_SaveLoadBool_uniqueID_239);
	}

	private void Relay_InitialSpawn_240()
	{
		int num = 0;
		Array repulsorSpawnData = RepulsorSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_240.Length != num + repulsorSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_240, num + repulsorSpawnData.Length);
		}
		Array.Copy(repulsorSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_240, num, repulsorSpawnData.Length);
		num += repulsorSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_240 = owner_Connection_241;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_240.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_240, logic_uScript_SpawnTechsFromData_ownerNode_240, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_240, logic_uScript_SpawnTechsFromData_allowResurrection_240);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_240.Out)
		{
			Relay_True_45();
		}
	}

	private void Relay_AtIndex_243()
	{
		int num = 0;
		Array array = local_RepulsorTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_243.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_243, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_243, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_243.AtIndex(ref logic_uScript_AccessListTech_techList_243, logic_uScript_AccessListTech_index_243, out logic_uScript_AccessListTech_value_243);
		local_RepulsorTechs_TankArray = logic_uScript_AccessListTech_techList_243;
		local_RepulsorTech_Tank = logic_uScript_AccessListTech_value_243;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_243.Out)
		{
			Relay_In_244();
		}
	}

	private void Relay_In_244()
	{
		logic_uScript_SetTankInvulnerable_tank_244 = local_RepulsorTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_244.In(logic_uScript_SetTankInvulnerable_invulnerable_244, logic_uScript_SetTankInvulnerable_tank_244);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_244.Out)
		{
			Relay_In_292();
		}
	}

	private void Relay_In_246()
	{
		int num = 0;
		Array repulsorSpawnData = RepulsorSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_246.Length != num + repulsorSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_246, num + repulsorSpawnData.Length);
		}
		Array.Copy(repulsorSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_246, num, repulsorSpawnData.Length);
		num += repulsorSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_246 = owner_Connection_245;
		int num2 = 0;
		Array array = local_RepulsorTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_246.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_246, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_246, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_246 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_246.In(logic_uScript_GetAndCheckTechs_techData_246, logic_uScript_GetAndCheckTechs_ownerNode_246, ref logic_uScript_GetAndCheckTechs_techs_246);
		local_RepulsorTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_246;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_246.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_246.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_243();
		}
		if (someAlive)
		{
			Relay_AtIndex_243();
		}
	}

	private void Relay_Save_Out_251()
	{
		Relay_Save_262();
	}

	private void Relay_Load_Out_251()
	{
		Relay_Load_262();
	}

	private void Relay_Restart_Out_251()
	{
		Relay_Set_False_262();
	}

	private void Relay_Save_251()
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = local_msgIAmRepulsorShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_251 = local_msgIAmRepulsorShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Save(ref logic_SubGraph_SaveLoadBool_boolean_251, logic_SubGraph_SaveLoadBool_boolAsVariable_251, logic_SubGraph_SaveLoadBool_uniqueID_251);
	}

	private void Relay_Load_251()
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = local_msgIAmRepulsorShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_251 = local_msgIAmRepulsorShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Load(ref logic_SubGraph_SaveLoadBool_boolean_251, logic_SubGraph_SaveLoadBool_boolAsVariable_251, logic_SubGraph_SaveLoadBool_uniqueID_251);
	}

	private void Relay_Set_True_251()
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = local_msgIAmRepulsorShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_251 = local_msgIAmRepulsorShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_251, logic_SubGraph_SaveLoadBool_boolAsVariable_251, logic_SubGraph_SaveLoadBool_uniqueID_251);
	}

	private void Relay_Set_False_251()
	{
		logic_SubGraph_SaveLoadBool_boolean_251 = local_msgIAmRepulsorShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_251 = local_msgIAmRepulsorShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_251.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_251, logic_SubGraph_SaveLoadBool_boolAsVariable_251, logic_SubGraph_SaveLoadBool_uniqueID_251);
	}

	private void Relay_In_253()
	{
		logic_uScriptCon_CompareBool_Bool_253 = local_msgNearLongJumpShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_253.In(logic_uScriptCon_CompareBool_Bool_253);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_253.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_253.False;
		if (num)
		{
			Relay_In_285();
		}
		if (flag)
		{
			Relay_In_254();
		}
	}

	private void Relay_In_254()
	{
		logic_uScript_AddMessage_messageData_254 = msgNearLongJump;
		logic_uScript_AddMessage_speaker_254 = messageNPCSpeaker;
		logic_uScript_AddMessage_Return_254 = logic_uScript_AddMessage_uScript_AddMessage_254.In(logic_uScript_AddMessage_messageData_254, logic_uScript_AddMessage_speaker_254);
		if (logic_uScript_AddMessage_uScript_AddMessage_254.Shown)
		{
			Relay_In_279();
		}
	}

	private void Relay_True_257()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_257.True(out logic_uScriptAct_SetBool_Target_257);
		local_msgNearLongJumpShown_System_Boolean = logic_uScriptAct_SetBool_Target_257;
	}

	private void Relay_False_257()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_257.False(out logic_uScriptAct_SetBool_Target_257);
		local_msgNearLongJumpShown_System_Boolean = logic_uScriptAct_SetBool_Target_257;
	}

	private void Relay_In_259()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_259 = NearLongJumpTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_259.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_259);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_259.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_259.OutOfRange;
		if (inRange)
		{
			Relay_In_289();
		}
		if (outOfRange)
		{
			Relay_In_280();
		}
	}

	private void Relay_Save_Out_262()
	{
		Relay_Save_273();
	}

	private void Relay_Load_Out_262()
	{
		Relay_Load_273();
	}

	private void Relay_Restart_Out_262()
	{
		Relay_Set_False_273();
	}

	private void Relay_Save_262()
	{
		logic_SubGraph_SaveLoadBool_boolean_262 = local_msgNearLongJumpShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_262 = local_msgNearLongJumpShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.Save(ref logic_SubGraph_SaveLoadBool_boolean_262, logic_SubGraph_SaveLoadBool_boolAsVariable_262, logic_SubGraph_SaveLoadBool_uniqueID_262);
	}

	private void Relay_Load_262()
	{
		logic_SubGraph_SaveLoadBool_boolean_262 = local_msgNearLongJumpShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_262 = local_msgNearLongJumpShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.Load(ref logic_SubGraph_SaveLoadBool_boolean_262, logic_SubGraph_SaveLoadBool_boolAsVariable_262, logic_SubGraph_SaveLoadBool_uniqueID_262);
	}

	private void Relay_Set_True_262()
	{
		logic_SubGraph_SaveLoadBool_boolean_262 = local_msgNearLongJumpShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_262 = local_msgNearLongJumpShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_262, logic_SubGraph_SaveLoadBool_boolAsVariable_262, logic_SubGraph_SaveLoadBool_uniqueID_262);
	}

	private void Relay_Set_False_262()
	{
		logic_SubGraph_SaveLoadBool_boolean_262 = local_msgNearLongJumpShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_262 = local_msgNearLongJumpShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_262.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_262, logic_SubGraph_SaveLoadBool_boolAsVariable_262, logic_SubGraph_SaveLoadBool_uniqueID_262);
	}

	private void Relay_In_263()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_263 = NearBrokenBridgeTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_263.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_263);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_263.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_263.OutOfRange;
		if (inRange)
		{
			Relay_In_291();
		}
		if (outOfRange)
		{
			Relay_In_360();
		}
	}

	private void Relay_In_264()
	{
		logic_uScript_AddMessage_messageData_264 = msgNearBrokenBridge;
		logic_uScript_AddMessage_speaker_264 = messageNPCSpeaker;
		logic_uScript_AddMessage_Return_264 = logic_uScript_AddMessage_uScript_AddMessage_264.In(logic_uScript_AddMessage_messageData_264, logic_uScript_AddMessage_speaker_264);
		if (logic_uScript_AddMessage_uScript_AddMessage_264.Shown)
		{
			Relay_In_274();
		}
	}

	private void Relay_True_270()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_270.True(out logic_uScriptAct_SetBool_Target_270);
		local_msgNearBrokenBridgeShown_System_Boolean = logic_uScriptAct_SetBool_Target_270;
	}

	private void Relay_False_270()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_270.False(out logic_uScriptAct_SetBool_Target_270);
		local_msgNearBrokenBridgeShown_System_Boolean = logic_uScriptAct_SetBool_Target_270;
	}

	private void Relay_In_271()
	{
		logic_uScriptCon_CompareBool_Bool_271 = local_msgNearBrokenBridgeShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271.In(logic_uScriptCon_CompareBool_Bool_271);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271.False;
		if (num)
		{
			Relay_In_286();
		}
		if (flag)
		{
			Relay_In_264();
		}
	}

	private void Relay_Save_Out_273()
	{
		Relay_Save_309();
	}

	private void Relay_Load_Out_273()
	{
		Relay_Load_309();
	}

	private void Relay_Restart_Out_273()
	{
		Relay_Set_False_309();
	}

	private void Relay_Save_273()
	{
		logic_SubGraph_SaveLoadBool_boolean_273 = local_msgNearBrokenBridgeShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_273 = local_msgNearBrokenBridgeShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.Save(ref logic_SubGraph_SaveLoadBool_boolean_273, logic_SubGraph_SaveLoadBool_boolAsVariable_273, logic_SubGraph_SaveLoadBool_uniqueID_273);
	}

	private void Relay_Load_273()
	{
		logic_SubGraph_SaveLoadBool_boolean_273 = local_msgNearBrokenBridgeShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_273 = local_msgNearBrokenBridgeShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.Load(ref logic_SubGraph_SaveLoadBool_boolean_273, logic_SubGraph_SaveLoadBool_boolAsVariable_273, logic_SubGraph_SaveLoadBool_uniqueID_273);
	}

	private void Relay_Set_True_273()
	{
		logic_SubGraph_SaveLoadBool_boolean_273 = local_msgNearBrokenBridgeShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_273 = local_msgNearBrokenBridgeShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_273, logic_SubGraph_SaveLoadBool_boolAsVariable_273, logic_SubGraph_SaveLoadBool_uniqueID_273);
	}

	private void Relay_Set_False_273()
	{
		logic_SubGraph_SaveLoadBool_boolean_273 = local_msgNearBrokenBridgeShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_273 = local_msgNearBrokenBridgeShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_273.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_273, logic_SubGraph_SaveLoadBool_boolAsVariable_273, logic_SubGraph_SaveLoadBool_uniqueID_273);
	}

	private void Relay_In_274()
	{
		logic_uScript_AddMessage_messageData_274 = msgShutUpWimpy;
		logic_uScript_AddMessage_speaker_274 = messageBossSpeaker;
		logic_uScript_AddMessage_Return_274 = logic_uScript_AddMessage_uScript_AddMessage_274.In(logic_uScript_AddMessage_messageData_274, logic_uScript_AddMessage_speaker_274);
		if (logic_uScript_AddMessage_uScript_AddMessage_274.Out)
		{
			Relay_True_270();
		}
	}

	private void Relay_In_279()
	{
		logic_uScript_AddMessage_messageData_279 = msgShutUpWimpy;
		logic_uScript_AddMessage_speaker_279 = messageBossSpeaker;
		logic_uScript_AddMessage_Return_279 = logic_uScript_AddMessage_uScript_AddMessage_279.In(logic_uScript_AddMessage_messageData_279, logic_uScript_AddMessage_speaker_279);
		if (logic_uScript_AddMessage_uScript_AddMessage_279.Out)
		{
			Relay_True_257();
		}
	}

	private void Relay_In_280()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_280.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_280.Out)
		{
			Relay_In_281();
		}
	}

	private void Relay_In_281()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_281.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_281.Out)
		{
			Relay_In_412();
		}
	}

	private void Relay_In_282()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_282.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_282.Out)
		{
			Relay_In_408();
		}
	}

	private void Relay_In_283()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_283.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_283.Out)
		{
			Relay_In_282();
		}
	}

	private void Relay_In_284()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_284.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_284.Out)
		{
			Relay_In_412();
		}
	}

	private void Relay_In_285()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_285.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_285.Out)
		{
			Relay_In_284();
		}
	}

	private void Relay_In_286()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_286.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_286.Out)
		{
			Relay_In_287();
		}
	}

	private void Relay_In_287()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_287.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_287.Out)
		{
			Relay_In_408();
		}
	}

	private void Relay_In_289()
	{
		logic_uScriptCon_CompareBool_Bool_289 = local_ReachedMineIsland_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_289.In(logic_uScriptCon_CompareBool_Bool_289);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_289.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_289.False;
		if (num)
		{
			Relay_In_280();
		}
		if (flag)
		{
			Relay_In_253();
		}
	}

	private void Relay_In_291()
	{
		logic_uScriptCon_CompareBool_Bool_291 = local_ReachedMineIsland_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_291.In(logic_uScriptCon_CompareBool_Bool_291);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_291.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_291.False;
		if (num)
		{
			Relay_In_283();
		}
		if (flag)
		{
			Relay_In_271();
		}
	}

	private void Relay_In_292()
	{
		logic_uScript_SetCustomRadarTeamID_tech_292 = local_RepulsorTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_292.In(logic_uScript_SetCustomRadarTeamID_tech_292, logic_uScript_SetCustomRadarTeamID_radarTeamID_292);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_292.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_In_293()
	{
		logic_uScript_AddMessage_messageData_293 = msgEnemyHitAMine;
		logic_uScript_AddMessage_speaker_293 = messageNPCSpeaker;
		logic_uScript_AddMessage_Return_293 = logic_uScript_AddMessage_uScript_AddMessage_293.In(logic_uScript_AddMessage_messageData_293, logic_uScript_AddMessage_speaker_293);
		if (logic_uScript_AddMessage_uScript_AddMessage_293.Shown)
		{
			Relay_False_303();
		}
	}

	private void Relay_True_299()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_299.True(out logic_uScriptAct_SetBool_Target_299);
		local_PlayerHitAMine_System_Boolean = logic_uScriptAct_SetBool_Target_299;
	}

	private void Relay_False_299()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_299.False(out logic_uScriptAct_SetBool_Target_299);
		local_PlayerHitAMine_System_Boolean = logic_uScriptAct_SetBool_Target_299;
	}

	private void Relay_In_301()
	{
		logic_uScript_AddMessage_messageData_301 = msgPlayerHitAMine;
		logic_uScript_AddMessage_speaker_301 = messageNPCSpeaker;
		logic_uScript_AddMessage_Return_301 = logic_uScript_AddMessage_uScript_AddMessage_301.In(logic_uScript_AddMessage_messageData_301, logic_uScript_AddMessage_speaker_301);
		if (logic_uScript_AddMessage_uScript_AddMessage_301.Shown)
		{
			Relay_False_299();
		}
	}

	private void Relay_True_303()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_303.True(out logic_uScriptAct_SetBool_Target_303);
		local_EnemyHitAMine_System_Boolean = logic_uScriptAct_SetBool_Target_303;
	}

	private void Relay_False_303()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_303.False(out logic_uScriptAct_SetBool_Target_303);
		local_EnemyHitAMine_System_Boolean = logic_uScriptAct_SetBool_Target_303;
	}

	private void Relay_In_304()
	{
		logic_uScriptCon_CompareBool_Bool_304 = local_PlayerHitAMine_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_304.In(logic_uScriptCon_CompareBool_Bool_304);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_304.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_304.False;
		if (num)
		{
			Relay_In_301();
		}
		if (flag)
		{
			Relay_In_305();
		}
	}

	private void Relay_In_305()
	{
		logic_uScriptCon_CompareBool_Bool_305 = local_EnemyHitAMine_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_305.In(logic_uScriptCon_CompareBool_Bool_305);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_305.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_305.False;
		if (num)
		{
			Relay_In_293();
		}
		if (flag)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_307()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_307.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_307.Out)
		{
			Relay_In_304();
		}
	}

	private void Relay_Save_Out_309()
	{
		Relay_Save_311();
	}

	private void Relay_Load_Out_309()
	{
		Relay_Load_311();
	}

	private void Relay_Restart_Out_309()
	{
		Relay_Set_False_311();
	}

	private void Relay_Save_309()
	{
		logic_SubGraph_SaveLoadBool_boolean_309 = local_msgArrivedAtMineIslandShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_309 = local_msgArrivedAtMineIslandShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.Save(ref logic_SubGraph_SaveLoadBool_boolean_309, logic_SubGraph_SaveLoadBool_boolAsVariable_309, logic_SubGraph_SaveLoadBool_uniqueID_309);
	}

	private void Relay_Load_309()
	{
		logic_SubGraph_SaveLoadBool_boolean_309 = local_msgArrivedAtMineIslandShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_309 = local_msgArrivedAtMineIslandShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.Load(ref logic_SubGraph_SaveLoadBool_boolean_309, logic_SubGraph_SaveLoadBool_boolAsVariable_309, logic_SubGraph_SaveLoadBool_uniqueID_309);
	}

	private void Relay_Set_True_309()
	{
		logic_SubGraph_SaveLoadBool_boolean_309 = local_msgArrivedAtMineIslandShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_309 = local_msgArrivedAtMineIslandShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_309, logic_SubGraph_SaveLoadBool_boolAsVariable_309, logic_SubGraph_SaveLoadBool_uniqueID_309);
	}

	private void Relay_Set_False_309()
	{
		logic_SubGraph_SaveLoadBool_boolean_309 = local_msgArrivedAtMineIslandShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_309 = local_msgArrivedAtMineIslandShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_309.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_309, logic_SubGraph_SaveLoadBool_boolAsVariable_309, logic_SubGraph_SaveLoadBool_uniqueID_309);
	}

	private void Relay_Save_Out_311()
	{
		Relay_Save_313();
	}

	private void Relay_Load_Out_311()
	{
		Relay_Load_313();
	}

	private void Relay_Restart_Out_311()
	{
		Relay_Set_False_313();
	}

	private void Relay_Save_311()
	{
		logic_SubGraph_SaveLoadBool_boolean_311 = local_BossDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_311 = local_BossDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.Save(ref logic_SubGraph_SaveLoadBool_boolean_311, logic_SubGraph_SaveLoadBool_boolAsVariable_311, logic_SubGraph_SaveLoadBool_uniqueID_311);
	}

	private void Relay_Load_311()
	{
		logic_SubGraph_SaveLoadBool_boolean_311 = local_BossDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_311 = local_BossDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.Load(ref logic_SubGraph_SaveLoadBool_boolean_311, logic_SubGraph_SaveLoadBool_boolAsVariable_311, logic_SubGraph_SaveLoadBool_uniqueID_311);
	}

	private void Relay_Set_True_311()
	{
		logic_SubGraph_SaveLoadBool_boolean_311 = local_BossDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_311 = local_BossDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_311, logic_SubGraph_SaveLoadBool_boolAsVariable_311, logic_SubGraph_SaveLoadBool_uniqueID_311);
	}

	private void Relay_Set_False_311()
	{
		logic_SubGraph_SaveLoadBool_boolean_311 = local_BossDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_311 = local_BossDead_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_311.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_311, logic_SubGraph_SaveLoadBool_boolAsVariable_311, logic_SubGraph_SaveLoadBool_uniqueID_311);
	}

	private void Relay_Save_Out_313()
	{
		Relay_Save_315();
	}

	private void Relay_Load_Out_313()
	{
		Relay_Load_315();
	}

	private void Relay_Restart_Out_313()
	{
		Relay_Set_False_315();
	}

	private void Relay_Save_313()
	{
		logic_SubGraph_SaveLoadBool_boolean_313 = local_BossTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_313 = local_BossTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.Save(ref logic_SubGraph_SaveLoadBool_boolean_313, logic_SubGraph_SaveLoadBool_boolAsVariable_313, logic_SubGraph_SaveLoadBool_uniqueID_313);
	}

	private void Relay_Load_313()
	{
		logic_SubGraph_SaveLoadBool_boolean_313 = local_BossTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_313 = local_BossTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.Load(ref logic_SubGraph_SaveLoadBool_boolean_313, logic_SubGraph_SaveLoadBool_boolAsVariable_313, logic_SubGraph_SaveLoadBool_uniqueID_313);
	}

	private void Relay_Set_True_313()
	{
		logic_SubGraph_SaveLoadBool_boolean_313 = local_BossTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_313 = local_BossTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_313, logic_SubGraph_SaveLoadBool_boolAsVariable_313, logic_SubGraph_SaveLoadBool_uniqueID_313);
	}

	private void Relay_Set_False_313()
	{
		logic_SubGraph_SaveLoadBool_boolean_313 = local_BossTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_313 = local_BossTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_313.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_313, logic_SubGraph_SaveLoadBool_boolAsVariable_313, logic_SubGraph_SaveLoadBool_uniqueID_313);
	}

	private void Relay_Save_Out_315()
	{
		Relay_Save_457();
	}

	private void Relay_Load_Out_315()
	{
		Relay_Load_457();
	}

	private void Relay_Restart_Out_315()
	{
		Relay_Set_False_457();
	}

	private void Relay_Save_315()
	{
		logic_SubGraph_SaveLoadBool_boolean_315 = local_MsgBackAgain_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_315 = local_MsgBackAgain_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Save(ref logic_SubGraph_SaveLoadBool_boolean_315, logic_SubGraph_SaveLoadBool_boolAsVariable_315, logic_SubGraph_SaveLoadBool_uniqueID_315);
	}

	private void Relay_Load_315()
	{
		logic_SubGraph_SaveLoadBool_boolean_315 = local_MsgBackAgain_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_315 = local_MsgBackAgain_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Load(ref logic_SubGraph_SaveLoadBool_boolean_315, logic_SubGraph_SaveLoadBool_boolAsVariable_315, logic_SubGraph_SaveLoadBool_uniqueID_315);
	}

	private void Relay_Set_True_315()
	{
		logic_SubGraph_SaveLoadBool_boolean_315 = local_MsgBackAgain_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_315 = local_MsgBackAgain_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_315, logic_SubGraph_SaveLoadBool_boolAsVariable_315, logic_SubGraph_SaveLoadBool_uniqueID_315);
	}

	private void Relay_Set_False_315()
	{
		logic_SubGraph_SaveLoadBool_boolean_315 = local_MsgBackAgain_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_315 = local_MsgBackAgain_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_315.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_315, logic_SubGraph_SaveLoadBool_boolAsVariable_315, logic_SubGraph_SaveLoadBool_uniqueID_315);
	}

	private void Relay_In_316()
	{
		logic_uScript_IsTechWheelGrounded_tech_316 = local_MinefieldTech_Tank;
		logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_316.In(logic_uScript_IsTechWheelGrounded_tech_316);
		bool num = logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_316.True;
		bool flag = logic_uScript_IsTechWheelGrounded_uScript_IsTechWheelGrounded_316.False;
		if (num)
		{
			Relay_In_449();
		}
		if (flag)
		{
			Relay_In_317();
		}
	}

	private void Relay_In_317()
	{
		logic_uScript_IsTechTouchingTerrain_tech_317 = local_MinefieldTech_Tank;
		logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_317.In(logic_uScript_IsTechTouchingTerrain_tech_317);
		if (logic_uScript_IsTechTouchingTerrain_uScript_IsTechTouchingTerrain_317.True)
		{
			Relay_In_79();
		}
	}

	private void Relay_In_318()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_318.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_318.Out)
		{
			Relay_In_79();
		}
	}

	private void Relay_In_320()
	{
		logic_uScriptCon_CompareBool_Bool_320 = local_SpawnedBooster_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_320.In(logic_uScriptCon_CompareBool_Bool_320);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_320.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_320.False;
		if (num)
		{
			Relay_In_350();
		}
		if (flag)
		{
			Relay_In_322();
		}
	}

	private void Relay_In_322()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_322.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_322.Out)
		{
			Relay_In_351();
		}
	}

	private void Relay_True_323()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_323.True(out logic_uScriptAct_SetBool_Target_323);
		local_SpawnedBooster_System_Boolean = logic_uScriptAct_SetBool_Target_323;
	}

	private void Relay_False_323()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_323.False(out logic_uScriptAct_SetBool_Target_323);
		local_SpawnedBooster_System_Boolean = logic_uScriptAct_SetBool_Target_323;
	}

	private void Relay_In_325()
	{
		logic_uScript_LockVisibleStackAccept_targetObject_325 = local_BoosterBlock_TankBlock;
		logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_325.In(logic_uScript_LockVisibleStackAccept_targetObject_325);
		if (logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_325.Out)
		{
			Relay_In_326();
		}
	}

	private void Relay_In_326()
	{
		logic_uScript_KeepBlockInvulnerable_block_326 = local_BoosterBlock_TankBlock;
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_326.In(logic_uScript_KeepBlockInvulnerable_block_326);
		if (logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_326.Out)
		{
			Relay_In_183();
		}
	}

	private void Relay_In_329()
	{
		logic_uScript_KeepVisibleInEncounterArea_ownerNode_329 = owner_Connection_330;
		logic_uScript_KeepVisibleInEncounterArea_objectToEnclose_329 = local_BoosterBlock_TankBlock;
		logic_uScript_KeepVisibleInEncounterArea_resetPosName_329 = MineIslandTrigger;
		logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_329.In(logic_uScript_KeepVisibleInEncounterArea_ownerNode_329, logic_uScript_KeepVisibleInEncounterArea_objectToEnclose_329, logic_uScript_KeepVisibleInEncounterArea_resetPosName_329, out logic_uScript_KeepVisibleInEncounterArea_positionBeforeReset_329);
		if (logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_329.InsideArea)
		{
			Relay_In_325();
		}
	}

	private void Relay_In_336()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_336.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_336, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_336, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_336 = owner_Connection_333;
		int num2 = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_336.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_336, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_336, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_336 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_336.In(logic_uScript_GetAndCheckTechs_techData_336, logic_uScript_GetAndCheckTechs_ownerNode_336, ref logic_uScript_GetAndCheckTechs_techs_336);
		local_NPCTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_336;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_336.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_336.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_338();
		}
		if (someAlive)
		{
			Relay_AtIndex_338();
		}
	}

	private void Relay_AtIndex_338()
	{
		int num = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_338.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_338, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_338, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_338.AtIndex(ref logic_uScript_AccessListTech_techList_338, logic_uScript_AccessListTech_index_338, out logic_uScript_AccessListTech_value_338);
		local_NPCTechs_TankArray = logic_uScript_AccessListTech_techList_338;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_338;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_338.Out)
		{
			Relay_In_162();
		}
	}

	private void Relay_In_340()
	{
		logic_uScript_PointArrowAtBlock_block_340 = local_GhostBlock_TankBlock;
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_340.In(logic_uScript_PointArrowAtBlock_block_340, logic_uScript_PointArrowAtBlock_timeToShowFor_340, logic_uScript_PointArrowAtBlock_offset_340);
		if (logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_340.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_342()
	{
		logic_uScript_EnableGlow_targetObject_342 = local_BoosterBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_342.In(logic_uScript_EnableGlow_targetObject_342, logic_uScript_EnableGlow_enable_342);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_342.Out)
		{
			Relay_In_340();
		}
	}

	private void Relay_True_343()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_343.True(out logic_uScriptAct_SetBool_Target_343);
		local_GhostBlockSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_343;
	}

	private void Relay_False_343()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_343.False(out logic_uScriptAct_SetBool_Target_343);
		local_GhostBlockSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_343;
	}

	private void Relay_In_346()
	{
		logic_uScriptCon_CompareBool_Bool_346 = local_BlockAttachedToNPC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_346.In(logic_uScriptCon_CompareBool_Bool_346);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_346.False)
		{
			Relay_In_347();
		}
	}

	private void Relay_In_347()
	{
		logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_347 = local_NPCTech_Tank;
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_347.In(logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_347);
		if (logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_347.Out)
		{
			Relay_In_349();
		}
	}

	private void Relay_In_349()
	{
		logic_uScript_HideArrow_uScript_HideArrow_349.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_349.Out)
		{
			Relay_In_362();
		}
	}

	private void Relay_In_350()
	{
		int num = 0;
		Array keyBlock = KeyBlock;
		if (logic_uScript_GetAndCheckBlocks_blockData_350.Length != num + keyBlock.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blockData_350, num + keyBlock.Length);
		}
		Array.Copy(keyBlock, 0, logic_uScript_GetAndCheckBlocks_blockData_350, num, keyBlock.Length);
		num += keyBlock.Length;
		logic_uScript_GetAndCheckBlocks_ownerNode_350 = owner_Connection_354;
		int num2 = 0;
		Array array = local_KeyBlocks_TankBlockArray;
		if (logic_uScript_GetAndCheckBlocks_blocks_350.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blocks_350, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckBlocks_blocks_350, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_350.In(logic_uScript_GetAndCheckBlocks_blockData_350, logic_uScript_GetAndCheckBlocks_ownerNode_350, ref logic_uScript_GetAndCheckBlocks_blocks_350);
		local_KeyBlocks_TankBlockArray = logic_uScript_GetAndCheckBlocks_blocks_350;
		bool allAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_350.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_350.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_350.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_352();
		}
		if (someAlive)
		{
			Relay_AtIndex_352();
		}
		if (allDead)
		{
			Relay_In_351();
		}
	}

	private void Relay_In_351()
	{
		int num = 0;
		Array keyBlock = KeyBlock;
		if (logic_uScript_SpawnBlocksFromData_blockData_351.Length != num + keyBlock.Length)
		{
			Array.Resize(ref logic_uScript_SpawnBlocksFromData_blockData_351, num + keyBlock.Length);
		}
		Array.Copy(keyBlock, 0, logic_uScript_SpawnBlocksFromData_blockData_351, num, keyBlock.Length);
		num += keyBlock.Length;
		logic_uScript_SpawnBlocksFromData_ownerNode_351 = owner_Connection_355;
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_351.In(logic_uScript_SpawnBlocksFromData_blockData_351, logic_uScript_SpawnBlocksFromData_ownerNode_351);
		if (logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_351.Out)
		{
			Relay_True_323();
		}
	}

	private void Relay_AtIndex_352()
	{
		int num = 0;
		Array array = local_KeyBlocks_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_352.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_352, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_352, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_352.AtIndex(ref logic_uScript_AccessListBlock_blockList_352, logic_uScript_AccessListBlock_index_352, out logic_uScript_AccessListBlock_value_352);
		local_KeyBlocks_TankBlockArray = logic_uScript_AccessListBlock_blockList_352;
		local_BoosterBlock_TankBlock = logic_uScript_AccessListBlock_value_352;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_352.Out)
		{
			Relay_In_329();
		}
	}

	private void Relay_In_360()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_360.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_360.Out)
		{
			Relay_In_283();
		}
	}

	private void Relay_In_362()
	{
		logic_uScript_EnableGlow_targetObject_362 = local_BoosterBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_362.In(logic_uScript_EnableGlow_targetObject_362, logic_uScript_EnableGlow_enable_362);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_362.Out)
		{
			Relay_False_343();
		}
	}

	private void Relay_In_364()
	{
		logic_uScript_FlyTechUpAndAway_tech_364 = local_RepulsorTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_364 = NPCFlyAwayBehaviour;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_364.In(logic_uScript_FlyTechUpAndAway_tech_364, logic_uScript_FlyTechUpAndAway_maxLifetime_364, logic_uScript_FlyTechUpAndAway_targetHeight_364, logic_uScript_FlyTechUpAndAway_aiTree_364, logic_uScript_FlyTechUpAndAway_removalParticles_364);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_364.Out)
		{
			Relay_In_367();
		}
	}

	private void Relay_In_365()
	{
		logic_uScript_AddMessage_messageData_365 = msgComplete;
		logic_uScript_AddMessage_speaker_365 = messageNPCSpeaker;
		logic_uScript_AddMessage_Return_365 = logic_uScript_AddMessage_uScript_AddMessage_365.In(logic_uScript_AddMessage_messageData_365, logic_uScript_AddMessage_speaker_365);
		if (logic_uScript_AddMessage_uScript_AddMessage_365.Shown)
		{
			Relay_In_375();
		}
	}

	private void Relay_In_367()
	{
		logic_uScript_ClearEncounterTarget_owner_367 = owner_Connection_374;
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_367.In(logic_uScript_ClearEncounterTarget_owner_367);
		if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_367.Out)
		{
			Relay_In_379();
		}
	}

	private void Relay_Succeed_371()
	{
		logic_uScript_FinishEncounter_owner_371 = owner_Connection_368;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_371.Succeed(logic_uScript_FinishEncounter_owner_371);
	}

	private void Relay_Fail_371()
	{
		logic_uScript_FinishEncounter_owner_371 = owner_Connection_368;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_371.Fail(logic_uScript_FinishEncounter_owner_371);
	}

	private void Relay_In_372()
	{
		logic_uScriptCon_CompareBool_Bool_372 = local_BlockAttachedToNPC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_372.In(logic_uScriptCon_CompareBool_Bool_372);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_372.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_372.False;
		if (num)
		{
			Relay_In_381();
		}
		if (flag)
		{
			Relay_In_320();
		}
	}

	private void Relay_In_375()
	{
		logic_uScript_FlyTechUpAndAway_tech_375 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_375 = NPCFlyAwayBehaviour;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_375.In(logic_uScript_FlyTechUpAndAway_tech_375, logic_uScript_FlyTechUpAndAway_maxLifetime_375, logic_uScript_FlyTechUpAndAway_targetHeight_375, logic_uScript_FlyTechUpAndAway_aiTree_375, logic_uScript_FlyTechUpAndAway_removalParticles_375);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_375.Out)
		{
			Relay_In_364();
		}
	}

	private void Relay_Out_379()
	{
		Relay_Succeed_371();
	}

	private void Relay_In_379()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_379 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_379.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_379, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_379);
	}

	private void Relay_In_380()
	{
		logic_uScript_SetTankTeam_tank_380 = local_NPCTech_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_380.In(logic_uScript_SetTankTeam_tank_380, logic_uScript_SetTankTeam_team_380);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_380.Out)
		{
			Relay_In_365();
		}
	}

	private void Relay_In_381()
	{
		logic_uScript_EnableGlow_targetObject_381 = local_BoosterBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_381.In(logic_uScript_EnableGlow_targetObject_381, logic_uScript_EnableGlow_enable_381);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_381.Out)
		{
			Relay_In_380();
		}
	}

	private void Relay_In_383()
	{
		logic_uScript_SetEncounterTarget_owner_383 = owner_Connection_384;
		logic_uScript_SetEncounterTarget_visibleObject_383 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_383.In(logic_uScript_SetEncounterTarget_owner_383, logic_uScript_SetEncounterTarget_visibleObject_383);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_383.Out)
		{
			Relay_True_164();
		}
	}

	private void Relay_AtIndex_387()
	{
		int num = 0;
		Array array = local_RepulsorTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_387.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_387, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_387, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_387.AtIndex(ref logic_uScript_AccessListTech_techList_387, logic_uScript_AccessListTech_index_387, out logic_uScript_AccessListTech_value_387);
		local_RepulsorTechs_TankArray = logic_uScript_AccessListTech_techList_387;
		local_RepulsorTech_Tank = logic_uScript_AccessListTech_value_387;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_387.Out)
		{
			Relay_In_307();
		}
	}

	private void Relay_In_388()
	{
		int num = 0;
		Array repulsorSpawnData = RepulsorSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_388.Length != num + repulsorSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_388, num + repulsorSpawnData.Length);
		}
		Array.Copy(repulsorSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_388, num, repulsorSpawnData.Length);
		num += repulsorSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_388 = owner_Connection_386;
		int num2 = 0;
		Array array = local_RepulsorTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_388.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_388, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_388, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_388 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_388.In(logic_uScript_GetAndCheckTechs_techData_388, logic_uScript_GetAndCheckTechs_ownerNode_388, ref logic_uScript_GetAndCheckTechs_techs_388);
		local_RepulsorTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_388;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_388.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_388.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_388.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_388.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_387();
		}
		if (someAlive)
		{
			Relay_AtIndex_387();
		}
		if (allDead)
		{
			Relay_In_390();
		}
		if (waitingToSpawn)
		{
			Relay_In_390();
		}
	}

	private void Relay_In_390()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_390.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_390.Out)
		{
			Relay_In_307();
		}
	}

	private void Relay_AtIndex_393()
	{
		int num = 0;
		Array array = local_BossTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_393.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_393, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_393, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_393.AtIndex(ref logic_uScript_AccessListTech_techList_393, logic_uScript_AccessListTech_index_393, out logic_uScript_AccessListTech_value_393);
		local_BossTechs_TankArray = logic_uScript_AccessListTech_techList_393;
		local_BossTech_Tank = logic_uScript_AccessListTech_value_393;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_393.Out)
		{
			Relay_In_388();
		}
	}

	private void Relay_In_394()
	{
		int num = 0;
		Array bossSpawnData = BossSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_394.Length != num + bossSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_394, num + bossSpawnData.Length);
		}
		Array.Copy(bossSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_394, num, bossSpawnData.Length);
		num += bossSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_394 = owner_Connection_397;
		int num2 = 0;
		Array array = local_BossTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_394.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_394, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_394, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_394 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_394.In(logic_uScript_GetAndCheckTechs_techData_394, logic_uScript_GetAndCheckTechs_ownerNode_394, ref logic_uScript_GetAndCheckTechs_techs_394);
		local_BossTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_394;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_394.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_394.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_394.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_394.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_393();
		}
		if (someAlive)
		{
			Relay_AtIndex_393();
		}
		if (allDead)
		{
			Relay_In_399();
		}
		if (waitingToSpawn)
		{
			Relay_In_399();
		}
	}

	private void Relay_In_399()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_399.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_399.Out)
		{
			Relay_In_388();
		}
	}

	private void Relay_Out_400()
	{
	}

	private void Relay_In_400()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_400 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_400.In(logic_SubGraph_LoadObjectiveStates_currentObjective_400);
	}

	private void Relay_Save_Out_406()
	{
		Relay_Save_18();
	}

	private void Relay_Load_Out_406()
	{
		Relay_Load_18();
	}

	private void Relay_Restart_Out_406()
	{
		Relay_Set_False_18();
	}

	private void Relay_Save_406()
	{
		logic_SubGraph_SaveLoadInt_integer_406 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_406 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406.Save(logic_SubGraph_SaveLoadInt_restartValue_406, ref logic_SubGraph_SaveLoadInt_integer_406, logic_SubGraph_SaveLoadInt_intAsVariable_406, logic_SubGraph_SaveLoadInt_uniqueID_406);
	}

	private void Relay_Load_406()
	{
		logic_SubGraph_SaveLoadInt_integer_406 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_406 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406.Load(logic_SubGraph_SaveLoadInt_restartValue_406, ref logic_SubGraph_SaveLoadInt_integer_406, logic_SubGraph_SaveLoadInt_intAsVariable_406, logic_SubGraph_SaveLoadInt_uniqueID_406);
	}

	private void Relay_Restart_406()
	{
		logic_SubGraph_SaveLoadInt_integer_406 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_406 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_406.Restart(logic_SubGraph_SaveLoadInt_restartValue_406, ref logic_SubGraph_SaveLoadInt_integer_406, logic_SubGraph_SaveLoadInt_intAsVariable_406, logic_SubGraph_SaveLoadInt_uniqueID_406);
	}

	private void Relay_In_408()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_408 = MineIslandTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_408.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_408);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_408.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_408.OutOfRange;
		if (inRange)
		{
			Relay_In_430();
		}
		if (outOfRange)
		{
			Relay_In_205();
		}
	}

	private void Relay_In_410()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_410.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_410.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_410.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_259();
		}
		if (multiplayer)
		{
			Relay_In_411();
		}
	}

	private void Relay_In_411()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_411.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_411.Out)
		{
			Relay_In_280();
		}
	}

	private void Relay_In_412()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_412.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_412.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_412.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_263();
		}
		if (multiplayer)
		{
			Relay_In_413();
		}
	}

	private void Relay_In_413()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_413.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_413.Out)
		{
			Relay_In_360();
		}
	}

	private void Relay_In_417()
	{
		logic_uScript_AddMessage_messageData_417 = msgRepulsorBaseline;
		logic_uScript_AddMessage_speaker_417 = messageRepulsorSpeaker;
		logic_uScript_AddMessage_Return_417 = logic_uScript_AddMessage_uScript_AddMessage_417.In(logic_uScript_AddMessage_messageData_417, logic_uScript_AddMessage_speaker_417);
	}

	private void Relay_In_418()
	{
		logic_uScriptCon_CompareBool_Bool_418 = local_msgIAmRepulsorShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_418.In(logic_uScriptCon_CompareBool_Bool_418);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_418.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_418.False;
		if (num)
		{
			Relay_In_417();
		}
		if (flag)
		{
			Relay_In_420();
		}
	}

	private void Relay_In_419()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_419 = TagRepulsor;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_419.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_419, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_419);
	}

	private void Relay_In_420()
	{
		logic_uScript_AddMessage_messageData_420 = msgIAmRepulsor;
		logic_uScript_AddMessage_speaker_420 = messageRepulsorSpeaker;
		logic_uScript_AddMessage_Return_420 = logic_uScript_AddMessage_uScript_AddMessage_420.In(logic_uScript_AddMessage_messageData_420, logic_uScript_AddMessage_speaker_420);
		if (logic_uScript_AddMessage_uScript_AddMessage_420.Shown)
		{
			Relay_True_423();
		}
	}

	private void Relay_True_423()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_423.True(out logic_uScriptAct_SetBool_Target_423);
		local_msgIAmRepulsorShown_System_Boolean = logic_uScriptAct_SetBool_Target_423;
	}

	private void Relay_False_423()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_423.False(out logic_uScriptAct_SetBool_Target_423);
		local_msgIAmRepulsorShown_System_Boolean = logic_uScriptAct_SetBool_Target_423;
	}

	private void Relay_In_426()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_426 = NearRepulsorTrigger;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_426.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_426);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_426.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_426.OutOfRange;
		if (inRange)
		{
			Relay_In_418();
		}
		if (outOfRange)
		{
			Relay_In_419();
		}
	}

	private void Relay_In_428()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_428.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_428.Out)
		{
			Relay_In_426();
		}
	}

	private void Relay_In_430()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_430 = TagRepulsor;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_430.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_430, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_430);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_430.Out)
		{
			Relay_In_101();
		}
	}

	private void Relay_In_431()
	{
		logic_uScript_GetPlayerTeam_Return_431 = logic_uScript_GetPlayerTeam_uScript_GetPlayerTeam_431.In();
		local_PlayerTeam_System_Int32 = logic_uScript_GetPlayerTeam_Return_431;
		if (logic_uScript_GetPlayerTeam_uScript_GetPlayerTeam_431.Out)
		{
			Relay_In_179();
		}
	}

	private void Relay_In_433()
	{
		logic_uScript_LockTechSendToSCU_tech_433 = local_NPCTech_Tank;
		logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_433.In(logic_uScript_LockTechSendToSCU_tech_433, logic_uScript_LockTechSendToSCU_lockSendToSCU_433);
		if (logic_uScript_LockTechSendToSCU_uScript_LockTechSendToSCU_433.Out)
		{
			Relay_In_394();
		}
	}

	private void Relay_In_435()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_435.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_435, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_435, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_435 = owner_Connection_440;
		int num2 = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_435.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_435, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_435, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_435 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_435.In(logic_uScript_GetAndCheckTechs_techData_435, logic_uScript_GetAndCheckTechs_ownerNode_435, ref logic_uScript_GetAndCheckTechs_techs_435);
		local_NPCTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_435;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_435.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_435.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_435.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_435.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_439();
		}
		if (someAlive)
		{
			Relay_AtIndex_439();
		}
		if (allDead)
		{
			Relay_In_441();
		}
		if (waitingToSpawn)
		{
			Relay_In_441();
		}
	}

	private void Relay_In_437()
	{
		logic_uScript_LockTech_tech_437 = local_NPCTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_437.In(logic_uScript_LockTech_tech_437, logic_uScript_LockTech_lockType_437);
		if (logic_uScript_LockTech_uScript_LockTech_437.Out)
		{
			Relay_In_433();
		}
	}

	private void Relay_AtIndex_439()
	{
		int num = 0;
		Array array = local_NPCTechs_TankArray;
		if (logic_uScript_AccessListTech_techList_439.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_439, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_439, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_439.AtIndex(ref logic_uScript_AccessListTech_techList_439, logic_uScript_AccessListTech_index_439, out logic_uScript_AccessListTech_value_439);
		local_NPCTechs_TankArray = logic_uScript_AccessListTech_techList_439;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_439;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_439.Out)
		{
			Relay_In_437();
		}
	}

	private void Relay_In_441()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_441.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_441.Out)
		{
			Relay_In_442();
		}
	}

	private void Relay_In_442()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_442.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_442.Out)
		{
			Relay_In_394();
		}
	}

	private void Relay_In_444()
	{
		logic_uScript_SetTankTeam_tank_444 = local_NPCTech_Tank;
		logic_uScript_SetTankTeam_team_444 = local_PlayerTeam_System_Int32;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_444.In(logic_uScript_SetTankTeam_tank_444, logic_uScript_SetTankTeam_team_444);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_444.Out)
		{
			Relay_In_448();
		}
	}

	private void Relay_In_446()
	{
		logic_uScript_GetPlayerTeam_Return_446 = logic_uScript_GetPlayerTeam_uScript_GetPlayerTeam_446.In();
		local_PlayerTeam_System_Int32 = logic_uScript_GetPlayerTeam_Return_446;
		if (logic_uScript_GetPlayerTeam_uScript_GetPlayerTeam_446.Out)
		{
			Relay_In_444();
		}
	}

	private void Relay_In_448()
	{
		logic_uScript_SetCustomRadarTeamID_tech_448 = local_NPCTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_448.In(logic_uScript_SetCustomRadarTeamID_tech_448, logic_uScript_SetCustomRadarTeamID_radarTeamID_448);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_448.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_449()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_449.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_449.Out)
		{
			Relay_In_318();
		}
	}

	private void Relay_In_450()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_450.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_450.Out)
		{
			Relay_In_449();
		}
	}

	private void Relay_In_452()
	{
		logic_uScriptCon_CompareBool_Bool_452 = local_BossTriggered_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_452.In(logic_uScriptCon_CompareBool_Bool_452);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_452.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_452.False;
		if (num)
		{
			Relay_In_157();
		}
		if (flag)
		{
			Relay_In_122();
		}
	}

	private void Relay_In_454()
	{
		logic_uScript_IsTechAnchored_tech_454 = local_MinefieldTech_Tank;
		logic_uScript_IsTechAnchored_uScript_IsTechAnchored_454.In(logic_uScript_IsTechAnchored_tech_454);
		bool num = logic_uScript_IsTechAnchored_uScript_IsTechAnchored_454.True;
		bool flag = logic_uScript_IsTechAnchored_uScript_IsTechAnchored_454.False;
		if (num)
		{
			Relay_In_450();
		}
		if (flag)
		{
			Relay_In_316();
		}
	}

	private void Relay_Save_Out_457()
	{
	}

	private void Relay_Load_Out_457()
	{
		Relay_In_400();
	}

	private void Relay_Restart_Out_457()
	{
	}

	private void Relay_Save_457()
	{
		logic_SubGraph_SaveLoadBool_boolean_457 = local_SpawnedBooster_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_457 = local_SpawnedBooster_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Save(ref logic_SubGraph_SaveLoadBool_boolean_457, logic_SubGraph_SaveLoadBool_boolAsVariable_457, logic_SubGraph_SaveLoadBool_uniqueID_457);
	}

	private void Relay_Load_457()
	{
		logic_SubGraph_SaveLoadBool_boolean_457 = local_SpawnedBooster_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_457 = local_SpawnedBooster_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Load(ref logic_SubGraph_SaveLoadBool_boolean_457, logic_SubGraph_SaveLoadBool_boolAsVariable_457, logic_SubGraph_SaveLoadBool_uniqueID_457);
	}

	private void Relay_Set_True_457()
	{
		logic_SubGraph_SaveLoadBool_boolean_457 = local_SpawnedBooster_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_457 = local_SpawnedBooster_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_457, logic_SubGraph_SaveLoadBool_boolAsVariable_457, logic_SubGraph_SaveLoadBool_uniqueID_457);
	}

	private void Relay_Set_False_457()
	{
		logic_SubGraph_SaveLoadBool_boolean_457 = local_SpawnedBooster_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_457 = local_SpawnedBooster_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_457.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_457, logic_SubGraph_SaveLoadBool_boolAsVariable_457, logic_SubGraph_SaveLoadBool_uniqueID_457);
	}
}
