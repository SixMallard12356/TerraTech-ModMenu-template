using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_SetPiece_RR_CombatLab : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool _DEBUG_SKIPS_ACTIVE;

	public float BossTurretHostileBatteryCharge;

	public BlockTypes bossTurretShieldBlockType;

	public SpawnTechData[] bossTurretSpawnData = new SpawnTechData[0];

	public SpawnTechData[] ForcefieldInnerGroupSpawnData = new SpawnTechData[0];

	public SpawnTechData[] ForcefieldOuterGroupSpawnData = new SpawnTechData[0];

	public float initialTechSpawnCheck_Distance;

	[Multiline(1)]
	public string initialTechSpawnCheck_Position = "";

	public float[] laserTurretGroupAnimDurations = new float[0];

	[Multiline(1)]
	public string[] laserTurretGroupAnimTriggers = new string[0];

	public SpawnTechData[] laserTurretGroupSpawnData = new SpawnTechData[0];

	public BlockTypes laserTurretShieldBlockType;

	public BlockTypes laserTurretWeaponBlockType;

	private Tank[] local_154_TankArray = new Tank[0];

	private Tank[] local_163_TankArray = new Tank[0];

	private KeyCode local_172_UnityEngine_KeyCode;

	private Vector3 local_198_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private Tank[] local_31_TankArray = new Tank[0];

	private Tank[] local_99_TankArray = new Tank[0];

	private Tank[] local_ArenaForcefields_TankArray = new Tank[0];

	private bool local_BossTurretDestroyed_System_Boolean;

	private Tank local_BossTurretTech_Tank;

	private int local_CurrentObjective_System_Int32 = 1;

	private Tank[] local_EntranceForcefields_TankArray = new Tank[0];

	private bool local_Init_System_Boolean;

	private bool local_Init_TeslaTurrets_System_Boolean;

	private bool local_LaserTurretsDestroyed_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_MsgIntro_ManOnScreenMessages_OnScreenMessage;

	private bool local_MsgIntroShown_System_Boolean;

	private Tank local_NPCTech_Tank;

	private Tank local_NPCTech02_Tank;

	private int local_numChargersAlive_System_Int32;

	private int local_Objective02MessageCount_System_Int32 = 1;

	private int local_Objective04SubStage_System_Int32 = 1;

	private bool local_TeslaTurretsDestroyed_System_Boolean;

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02TurretExplanation;

	public uScript_AddMessage.MessageData msg03ChargersDestroyedHalfway;

	public uScript_AddMessage.MessageData msg04ChargersDestroyedOneLeft;

	public uScript_AddMessage.MessageData msg05ChargersDestroyedAll;

	public uScript_AddMessage.MessageData msg06BossDefeated;

	public uScript_AddMessage.MessageData msg07Outro;

	public Transform NPCDespawnEffect;

	public SpawnTechData[] NPCSpawnData01 = new SpawnTechData[0];

	public SpawnTechData[] NPCSpawnData02 = new SpawnTechData[0];

	public uScript_AddMessage.MessageSpeaker NPCSpeaker;

	[Multiline(1)]
	public string NPCTriggerVolume01 = "";

	[Multiline(1)]
	public string NPCTriggerVolume02 = "";

	[Multiline(1)]
	public string room01TriggerVolume = "";

	public SpawnTechData[] shieldChargerGroupSpawnData = new SpawnTechData[0];

	public float telsaTurretGroupSpawnInterval;

	public SpawnTechData[] teslaTurretGroupSpawnData = new SpawnTechData[0];

	public BlockTypes teslaTurretShieldBlockType;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_12;

	private GameObject owner_Connection_24;

	private GameObject owner_Connection_35;

	private GameObject owner_Connection_53;

	private GameObject owner_Connection_67;

	private GameObject owner_Connection_69;

	private GameObject owner_Connection_71;

	private GameObject owner_Connection_82;

	private GameObject owner_Connection_85;

	private GameObject owner_Connection_87;

	private GameObject owner_Connection_101;

	private GameObject owner_Connection_102;

	private GameObject owner_Connection_104;

	private GameObject owner_Connection_114;

	private GameObject owner_Connection_137;

	private GameObject owner_Connection_153;

	private GameObject owner_Connection_159;

	private GameObject owner_Connection_168;

	private GameObject owner_Connection_177;

	private GameObject owner_Connection_179;

	private GameObject owner_Connection_182;

	private GameObject owner_Connection_185;

	private GameObject owner_Connection_187;

	private GameObject owner_Connection_190;

	private GameObject owner_Connection_193;

	private GameObject owner_Connection_206;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_4 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_4 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_4;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_4;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_4;

	private bool logic_uScript_SpawnTechsFromData_Out_4 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_5;

	private bool logic_uScriptCon_CompareBool_True_5 = true;

	private bool logic_uScriptCon_CompareBool_False_5 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_6 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_6;

	private bool logic_uScriptAct_SetBool_Out_6 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_6 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_6 = true;

	private SubGraph_RR_LaserLab_TurretAndChargerController logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_10 = new SubGraph_RR_LaserLab_TurretAndChargerController();

	private SpawnTechData[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_10 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_10 = new SpawnTechData[0];

	private string[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_10 = new string[0];

	private float[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_10 = new float[0];

	private BlockTypes logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_10;

	private BlockTypes logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_10;

	private bool logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_10;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_11 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_11 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_11;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_11;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_11;

	private bool logic_uScript_SpawnTechsFromData_Out_11 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_14;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_14 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_14 = "Init";

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_23 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_23 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_23;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_23 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_23;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_23 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_23 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_23 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_23 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_26;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_29 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_29 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_29 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_29 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_29 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_29 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_29 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_34 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_34;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_34;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_34;

	private bool logic_uScript_AddMessage_Out_34 = true;

	private bool logic_uScript_AddMessage_Shown_34 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_36;

	private bool logic_uScriptCon_CompareBool_True_36 = true;

	private bool logic_uScriptCon_CompareBool_False_36 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_39 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_39 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_42 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_42;

	private bool logic_uScript_RemoveOnScreenMessage_instant_42;

	private bool logic_uScript_RemoveOnScreenMessage_Out_42 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_43 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_43 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_43;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_43 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_43;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_43 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_43 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_43 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_43 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_47 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_47;

	private bool logic_uScriptAct_SetBool_Out_47 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_47 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_47 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_49;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_49;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_51 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_51;

	private object logic_uScript_SetEncounterTarget_visibleObject_51 = "";

	private bool logic_uScript_SetEncounterTarget_Out_51 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_54 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_54 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_54 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_54 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_54 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_54 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_54 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_57 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_57 = new Tank[0];

	private int logic_uScript_AccessListTech_index_57;

	private Tank logic_uScript_AccessListTech_value_57;

	private bool logic_uScript_AccessListTech_Out_57 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_60 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_60;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_60;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_60;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_60;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_60;

	private bool logic_uScript_FlyTechUpAndAway_Out_60 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_62 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_62 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_63 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_63;

	private bool logic_uScriptAct_SetBool_Out_63 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_63 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_63 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_64 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_64 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_64 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_64 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_64 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_64 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_64 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_66 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_66 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_66;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_66;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_66;

	private bool logic_uScript_SpawnTechsFromData_Out_66 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_68 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_68 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_68;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_68;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_68;

	private bool logic_uScript_SpawnTechsFromData_Out_68 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_72 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_72 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_72;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_72;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_72;

	private bool logic_uScript_SpawnTechsFromData_Out_72 = true;

	private SubGraph_RR_TeslaBase_TeslaTurretController logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_74 = new SubGraph_RR_TeslaBase_TeslaTurretController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_74 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_74 = new SpawnTechData[0];

	private BlockTypes logic_SubGraph_RR_TeslaBase_TeslaTurretController_teslaTurretShieldBlockType_74;

	private bool logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretsDestroyed_74;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_78 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_78;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_78 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_78 = "CurrentObjective";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_80;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_83 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_83;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_83 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_83 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_83 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_84 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_84;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_84 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_86 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_86 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_88 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_88 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_88;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_88;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_88;

	private bool logic_uScript_SpawnTechsFromData_Out_88 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_92 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_92;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_92;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_94 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_94;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_94;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_97 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_97;

	private object logic_uScript_SetEncounterTarget_visibleObject_97 = "";

	private bool logic_uScript_SetEncounterTarget_Out_97 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_106 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_106;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_106;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_106;

	private bool logic_uScript_AddMessage_Out_106 = true;

	private bool logic_uScript_AddMessage_Shown_106 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_109 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_109 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_109;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_109 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_109;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_109 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_109 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_109 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_109 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_110 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_110 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_110;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_110 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_110;

	private bool logic_uScript_SpawnTechsFromData_Out_110 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_111 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_111;

	private bool logic_uScript_FinishEncounter_Out_111 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_113 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_113 = new Tank[0];

	private int logic_uScript_AccessListTech_index_113;

	private Tank logic_uScript_AccessListTech_value_113;

	private bool logic_uScript_AccessListTech_Out_113 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_115 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_115;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_115;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_115;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_115;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_115;

	private bool logic_uScript_FlyTechUpAndAway_Out_115 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_117 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_117 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_117 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_117 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_117 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_117 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_117 = true;

	private SubGraph_RR_TeslaBase_ForcefieldController logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_121 = new SubGraph_RR_TeslaBase_ForcefieldController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_121 = new SpawnTechData[0];

	private Tank[] logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_121 = new Tank[0];

	private SubGraph_RR_TeslaBase_ForcefieldController logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_124 = new SubGraph_RR_TeslaBase_ForcefieldController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_124 = new SpawnTechData[0];

	private Tank[] logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_124 = new Tank[0];

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_129 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_129;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_129;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_129;

	private bool logic_uScript_AddMessage_Out_129 = true;

	private bool logic_uScript_AddMessage_Shown_129 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_133 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_133;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_133;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_133;

	private bool logic_uScript_AddMessage_Out_133 = true;

	private bool logic_uScript_AddMessage_Shown_133 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_138 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_138;

	private object logic_uScript_SetEncounterTarget_visibleObject_138 = "";

	private bool logic_uScript_SetEncounterTarget_Out_138 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_139 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_139;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_139 = AITreeType.AITypes.Guard;

	private bool logic_uScript_SetTechAIType_Out_139 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_146;

	private bool logic_uScriptCon_CompareBool_True_146 = true;

	private bool logic_uScriptCon_CompareBool_False_146 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_148 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_148;

	private int logic_uScript_SetTankTeam_team_148 = 1;

	private bool logic_uScript_SetTankTeam_Out_148 = true;

	private SubGraph_RR_CombatLab_BossTurretController logic_SubGraph_RR_CombatLab_BossTurretController_SubGraph_RR_CombatLab_BossTurretController_150 = new SubGraph_RR_CombatLab_BossTurretController();

	private SpawnTechData[] logic_SubGraph_RR_CombatLab_BossTurretController_chargerSpawnData_150 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_RR_CombatLab_BossTurretController_turretSpawnData_150 = new SpawnTechData[0];

	private BlockTypes logic_SubGraph_RR_CombatLab_BossTurretController_teslaTurretShieldBlockType_150;

	private bool logic_SubGraph_RR_CombatLab_BossTurretController_turretDestroyed_150;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_151 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_151;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_151 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_151;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_151 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_151 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_151 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_151 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_155 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_155 = new Tank[0];

	private int logic_uScript_AccessListTech_index_155;

	private Tank logic_uScript_AccessListTech_value_155;

	private bool logic_uScript_AccessListTech_Out_155 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_160 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_160 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_160;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_160 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_160;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_160 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_160 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_160 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_160 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_162 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_162 = new Tank[0];

	private int logic_uScript_AccessListTech_index_162;

	private Tank logic_uScript_AccessListTech_value_162;

	private bool logic_uScript_AccessListTech_Out_162 = true;

	private uScriptAct_GetKeyCode logic_uScriptAct_GetKeyCode_uScriptAct_GetKeyCode_167 = new uScriptAct_GetKeyCode();

	private KeyCode logic_uScriptAct_GetKeyCode_result_167;

	private string logic_uScriptAct_GetKeyCode_resultString_167;

	private bool logic_uScriptAct_GetKeyCode_Out_167 = true;

	private uScriptCon_CompareKeyCodes logic_uScriptCon_CompareKeyCodes_uScriptCon_CompareKeyCodes_170 = new uScriptCon_CompareKeyCodes();

	private KeyCode logic_uScriptCon_CompareKeyCodes_A_170;

	private KeyCode logic_uScriptCon_CompareKeyCodes_B_170 = KeyCode.G;

	private bool logic_uScriptCon_CompareKeyCodes_EqualTo_170 = true;

	private bool logic_uScriptCon_CompareKeyCodes_NotEqualTo_170 = true;

	private uScriptCon_CompareKeyCodes logic_uScriptCon_CompareKeyCodes_uScriptCon_CompareKeyCodes_171 = new uScriptCon_CompareKeyCodes();

	private KeyCode logic_uScriptCon_CompareKeyCodes_A_171;

	private KeyCode logic_uScriptCon_CompareKeyCodes_B_171 = KeyCode.F;

	private bool logic_uScriptCon_CompareKeyCodes_EqualTo_171 = true;

	private bool logic_uScriptCon_CompareKeyCodes_NotEqualTo_171 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_174 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_174;

	private bool logic_uScriptCon_CompareBool_True_174 = true;

	private bool logic_uScriptCon_CompareBool_False_174 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_175 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_175 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_175;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_175;

	private bool logic_uScript_DestroyTechsFromData_Out_175 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_178 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_178 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_178 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_178;

	private bool logic_uScript_DestroyTechsFromData_Out_178 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_180 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_180 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_180 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_180;

	private bool logic_uScript_DestroyTechsFromData_Out_180 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_184 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_184 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_184 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_184;

	private bool logic_uScript_DestroyTechsFromData_Out_184 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_188 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_188 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_188 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_188;

	private bool logic_uScript_DestroyTechsFromData_Out_188 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_191 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_191 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_191;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_191;

	private bool logic_uScript_DestroyTechsFromData_Out_191 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_194 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_194 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_194;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_194;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_194;

	private bool logic_uScript_SpawnTechsFromData_Out_194 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_197;

	private bool logic_uScriptCon_CompareBool_True_197 = true;

	private bool logic_uScriptCon_CompareBool_False_197 = true;

	private uScript_GetPositionInEncounter logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_199 = new uScript_GetPositionInEncounter();

	private GameObject logic_uScript_GetPositionInEncounter_ownerNode_199;

	private string logic_uScript_GetPositionInEncounter_posName_199 = "";

	private Vector3 logic_uScript_GetPositionInEncounter_Return_199;

	private bool logic_uScript_GetPositionInEncounter_Out_199 = true;

	private uScript_PlayerInRange logic_uScript_PlayerInRange_uScript_PlayerInRange_202 = new uScript_PlayerInRange();

	private Vector3 logic_uScript_PlayerInRange_position_202;

	private float logic_uScript_PlayerInRange_range_202;

	private bool logic_uScript_PlayerInRange_True_202 = true;

	private bool logic_uScript_PlayerInRange_False_202 = true;

	private bool logic_uScript_PlayerInRange_Out_202 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_204 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_204;

	private bool logic_uScriptAct_SetBool_Out_204 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_204 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_204 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_207;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_207 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_207 = "Init_TeslaTurrets";

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_209;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_211 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_211;

	private int logic_uScriptCon_CompareInt_B_211 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_211 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_211 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_211 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_211 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_211 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_211 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_214 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_214;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_214;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_214;

	private bool logic_uScript_AddMessage_Out_214 = true;

	private bool logic_uScript_AddMessage_Shown_214 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_217 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_217;

	private int logic_uScriptCon_CompareInt_B_217 = 1;

	private bool logic_uScriptCon_CompareInt_GreaterThan_217 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_217 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_217 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_217 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_217 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_217 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_220 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_220;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_220;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_220;

	private bool logic_uScript_AddMessage_Out_220 = true;

	private bool logic_uScript_AddMessage_Shown_220 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_221 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_221;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_221 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_221 = "Objective02MessageCount";

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_223 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_223;

	private int logic_uScriptAct_AddInt_v2_B_223 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_223;

	private float logic_uScriptAct_AddInt_v2_FloatResult_223;

	private bool logic_uScriptAct_AddInt_v2_Out_223 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_225 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_225;

	private int logic_uScriptAct_AddInt_v2_B_225 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_225;

	private float logic_uScriptAct_AddInt_v2_FloatResult_225;

	private bool logic_uScriptAct_AddInt_v2_Out_225 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_228 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_228 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_228 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_228 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_228 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_228 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_228 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_229 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_229;

	private int logic_uScriptAct_AddInt_v2_B_229 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_229;

	private float logic_uScriptAct_AddInt_v2_FloatResult_229;

	private bool logic_uScriptAct_AddInt_v2_Out_229 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_233 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_233;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_233;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_233;

	private bool logic_uScript_AddMessage_Out_233 = true;

	private bool logic_uScript_AddMessage_Shown_233 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_235 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_235 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_236 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_236 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_241;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_244 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_244;

	private int logic_uScriptAct_AddInt_v2_B_244 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_244;

	private float logic_uScriptAct_AddInt_v2_FloatResult_244;

	private bool logic_uScriptAct_AddInt_v2_Out_244 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_246 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_246;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_246 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_246 = "Objective04SubStage";

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_247 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_247;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_247;

	private bool logic_uScript_SetBatteryChargeAmount_Out_247 = true;

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
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
		}
		if (null == owner_Connection_12 || !m_RegisteredForEvents)
		{
			owner_Connection_12 = parentGameObject;
		}
		if (null == owner_Connection_24 || !m_RegisteredForEvents)
		{
			owner_Connection_24 = parentGameObject;
		}
		if (null == owner_Connection_35 || !m_RegisteredForEvents)
		{
			owner_Connection_35 = parentGameObject;
		}
		if (null == owner_Connection_53 || !m_RegisteredForEvents)
		{
			owner_Connection_53 = parentGameObject;
		}
		if (null == owner_Connection_67 || !m_RegisteredForEvents)
		{
			owner_Connection_67 = parentGameObject;
		}
		if (null == owner_Connection_69 || !m_RegisteredForEvents)
		{
			owner_Connection_69 = parentGameObject;
		}
		if (null == owner_Connection_71 || !m_RegisteredForEvents)
		{
			owner_Connection_71 = parentGameObject;
		}
		if (null == owner_Connection_82 || !m_RegisteredForEvents)
		{
			owner_Connection_82 = parentGameObject;
		}
		if (null == owner_Connection_85 || !m_RegisteredForEvents)
		{
			owner_Connection_85 = parentGameObject;
		}
		if (null == owner_Connection_87 || !m_RegisteredForEvents)
		{
			owner_Connection_87 = parentGameObject;
		}
		if (null == owner_Connection_101 || !m_RegisteredForEvents)
		{
			owner_Connection_101 = parentGameObject;
		}
		if (null == owner_Connection_102 || !m_RegisteredForEvents)
		{
			owner_Connection_102 = parentGameObject;
		}
		if (null == owner_Connection_104 || !m_RegisteredForEvents)
		{
			owner_Connection_104 = parentGameObject;
		}
		if (null == owner_Connection_114 || !m_RegisteredForEvents)
		{
			owner_Connection_114 = parentGameObject;
		}
		if (null == owner_Connection_137 || !m_RegisteredForEvents)
		{
			owner_Connection_137 = parentGameObject;
		}
		if (null == owner_Connection_153 || !m_RegisteredForEvents)
		{
			owner_Connection_153 = parentGameObject;
		}
		if (null == owner_Connection_159 || !m_RegisteredForEvents)
		{
			owner_Connection_159 = parentGameObject;
		}
		if (null == owner_Connection_168 || !m_RegisteredForEvents)
		{
			owner_Connection_168 = parentGameObject;
			if (null != owner_Connection_168)
			{
				uScript_Input uScript_Input2 = owner_Connection_168.GetComponent<uScript_Input>();
				if (null == uScript_Input2)
				{
					uScript_Input2 = owner_Connection_168.AddComponent<uScript_Input>();
				}
				if (null != uScript_Input2)
				{
					uScript_Input2.KeyEvent += Instance_KeyEvent_166;
				}
			}
		}
		if (null == owner_Connection_177 || !m_RegisteredForEvents)
		{
			owner_Connection_177 = parentGameObject;
		}
		if (null == owner_Connection_179 || !m_RegisteredForEvents)
		{
			owner_Connection_179 = parentGameObject;
		}
		if (null == owner_Connection_182 || !m_RegisteredForEvents)
		{
			owner_Connection_182 = parentGameObject;
		}
		if (null == owner_Connection_185 || !m_RegisteredForEvents)
		{
			owner_Connection_185 = parentGameObject;
		}
		if (null == owner_Connection_187 || !m_RegisteredForEvents)
		{
			owner_Connection_187 = parentGameObject;
		}
		if (null == owner_Connection_190 || !m_RegisteredForEvents)
		{
			owner_Connection_190 = parentGameObject;
		}
		if (null == owner_Connection_193 || !m_RegisteredForEvents)
		{
			owner_Connection_193 = parentGameObject;
		}
		if (null == owner_Connection_206 || !m_RegisteredForEvents)
		{
			owner_Connection_206 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_168)
		{
			uScript_Input uScript_Input2 = owner_Connection_168.GetComponent<uScript_Input>();
			if (null == uScript_Input2)
			{
				uScript_Input2 = owner_Connection_168.AddComponent<uScript_Input>();
			}
			if (null != uScript_Input2)
			{
				uScript_Input2.KeyEvent += Instance_KeyEvent_166;
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
		if (null != owner_Connection_168)
		{
			uScript_Input component3 = owner_Connection_168.GetComponent<uScript_Input>();
			if (null != component3)
			{
				component3.KeyEvent -= Instance_KeyEvent_166;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_4.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_6.SetParent(g);
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_10.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_11.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_23.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_29.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_34.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_39.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_42.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_43.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_47.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_51.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_54.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_57.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_60.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_62.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_64.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_66.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_68.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_72.SetParent(g);
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_74.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_83.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_84.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_86.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_88.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_92.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_94.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_97.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_106.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_109.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_110.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_111.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_113.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_115.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_117.SetParent(g);
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_121.SetParent(g);
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_124.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_129.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_133.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_138.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_139.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_148.SetParent(g);
		logic_SubGraph_RR_CombatLab_BossTurretController_SubGraph_RR_CombatLab_BossTurretController_150.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_155.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_160.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_162.SetParent(g);
		logic_uScriptAct_GetKeyCode_uScriptAct_GetKeyCode_167.SetParent(g);
		logic_uScriptCon_CompareKeyCodes_uScriptCon_CompareKeyCodes_170.SetParent(g);
		logic_uScriptCon_CompareKeyCodes_uScriptCon_CompareKeyCodes_171.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_174.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_175.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_178.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_180.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_184.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_188.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_191.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_194.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.SetParent(g);
		logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_199.SetParent(g);
		logic_uScript_PlayerInRange_uScript_PlayerInRange_202.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_204.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_211.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_214.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_217.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_220.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_223.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_225.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_228.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_229.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_233.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_235.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_236.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_244.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_247.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_12 = parentGameObject;
		owner_Connection_24 = parentGameObject;
		owner_Connection_35 = parentGameObject;
		owner_Connection_53 = parentGameObject;
		owner_Connection_67 = parentGameObject;
		owner_Connection_69 = parentGameObject;
		owner_Connection_71 = parentGameObject;
		owner_Connection_82 = parentGameObject;
		owner_Connection_85 = parentGameObject;
		owner_Connection_87 = parentGameObject;
		owner_Connection_101 = parentGameObject;
		owner_Connection_102 = parentGameObject;
		owner_Connection_104 = parentGameObject;
		owner_Connection_114 = parentGameObject;
		owner_Connection_137 = parentGameObject;
		owner_Connection_153 = parentGameObject;
		owner_Connection_159 = parentGameObject;
		owner_Connection_168 = parentGameObject;
		owner_Connection_177 = parentGameObject;
		owner_Connection_179 = parentGameObject;
		owner_Connection_182 = parentGameObject;
		owner_Connection_185 = parentGameObject;
		owner_Connection_187 = parentGameObject;
		owner_Connection_190 = parentGameObject;
		owner_Connection_193 = parentGameObject;
		owner_Connection_206 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_10.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.Awake();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_74.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_92.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_94.Awake();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_121.Awake();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_124.Awake();
		logic_SubGraph_RR_CombatLab_BossTurretController_SubGraph_RR_CombatLab_BossTurretController_150.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Awake();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_10.Out += SubGraph_RR_LaserLab_TurretAndChargerController_Out_10;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Save_Out += SubGraph_SaveLoadBool_Save_Out_14;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Load_Out += SubGraph_SaveLoadBool_Load_Out_14;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.Output1 += uScriptCon_ManualSwitch_Output1_26;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.Output2 += uScriptCon_ManualSwitch_Output2_26;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.Output3 += uScriptCon_ManualSwitch_Output3_26;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.Output4 += uScriptCon_ManualSwitch_Output4_26;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.Output5 += uScriptCon_ManualSwitch_Output5_26;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.Output6 += uScriptCon_ManualSwitch_Output6_26;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.Output7 += uScriptCon_ManualSwitch_Output7_26;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.Output8 += uScriptCon_ManualSwitch_Output8_26;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.Out += SubGraph_CompleteObjectiveStage_Out_49;
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_74.Out += SubGraph_RR_TeslaBase_TeslaTurretController_Out_74;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Save_Out += SubGraph_SaveLoadInt_Save_Out_78;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Load_Out += SubGraph_SaveLoadInt_Load_Out_78;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_78;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.Out += SubGraph_LoadObjectiveStates_Out_80;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_92.Out += SubGraph_CompleteObjectiveStage_Out_92;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_94.Out += SubGraph_CompleteObjectiveStage_Out_94;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_121.Out += SubGraph_RR_TeslaBase_ForcefieldController_Out_121;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_124.Out += SubGraph_RR_TeslaBase_ForcefieldController_Out_124;
		logic_SubGraph_RR_CombatLab_BossTurretController_SubGraph_RR_CombatLab_BossTurretController_150.Out += SubGraph_RR_CombatLab_BossTurretController_Out_150;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Save_Out += SubGraph_SaveLoadBool_Save_Out_207;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Load_Out += SubGraph_SaveLoadBool_Load_Out_207;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_207;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.Output1 += uScriptCon_ManualSwitch_Output1_209;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.Output2 += uScriptCon_ManualSwitch_Output2_209;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.Output3 += uScriptCon_ManualSwitch_Output3_209;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.Output4 += uScriptCon_ManualSwitch_Output4_209;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.Output5 += uScriptCon_ManualSwitch_Output5_209;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.Output6 += uScriptCon_ManualSwitch_Output6_209;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.Output7 += uScriptCon_ManualSwitch_Output7_209;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.Output8 += uScriptCon_ManualSwitch_Output8_209;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221.Save_Out += SubGraph_SaveLoadInt_Save_Out_221;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221.Load_Out += SubGraph_SaveLoadInt_Load_Out_221;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_221;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.Output1 += uScriptCon_ManualSwitch_Output1_241;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.Output2 += uScriptCon_ManualSwitch_Output2_241;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.Output3 += uScriptCon_ManualSwitch_Output3_241;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.Output4 += uScriptCon_ManualSwitch_Output4_241;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.Output5 += uScriptCon_ManualSwitch_Output5_241;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.Output6 += uScriptCon_ManualSwitch_Output6_241;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.Output7 += uScriptCon_ManualSwitch_Output7_241;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.Output8 += uScriptCon_ManualSwitch_Output8_241;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Save_Out += SubGraph_SaveLoadInt_Save_Out_246;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Load_Out += SubGraph_SaveLoadInt_Load_Out_246;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_246;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_10.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.Start();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_74.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_92.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_94.Start();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_121.Start();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_124.Start();
		logic_SubGraph_RR_CombatLab_BossTurretController_SubGraph_RR_CombatLab_BossTurretController_150.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_10.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_60.OnEnable();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_74.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_84.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_92.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_94.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_115.OnEnable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_121.OnEnable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_124.OnEnable();
		logic_SubGraph_RR_CombatLab_BossTurretController_SubGraph_RR_CombatLab_BossTurretController_150.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_10.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_34.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.OnDisable();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_74.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_83.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_92.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_94.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_106.OnDisable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_121.OnDisable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_124.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_129.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_133.OnDisable();
		logic_SubGraph_RR_CombatLab_BossTurretController_SubGraph_RR_CombatLab_BossTurretController_150.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_214.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_220.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_233.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_10.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.Update();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_74.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_92.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_94.Update();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_121.Update();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_124.Update();
		logic_SubGraph_RR_CombatLab_BossTurretController_SubGraph_RR_CombatLab_BossTurretController_150.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_10.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.OnDestroy();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_74.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_92.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_94.OnDestroy();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_121.OnDestroy();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_124.OnDestroy();
		logic_SubGraph_RR_CombatLab_BossTurretController_SubGraph_RR_CombatLab_BossTurretController_150.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.OnDestroy();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_10.Out -= SubGraph_RR_LaserLab_TurretAndChargerController_Out_10;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Save_Out -= SubGraph_SaveLoadBool_Save_Out_14;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Load_Out -= SubGraph_SaveLoadBool_Load_Out_14;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.Output1 -= uScriptCon_ManualSwitch_Output1_26;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.Output2 -= uScriptCon_ManualSwitch_Output2_26;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.Output3 -= uScriptCon_ManualSwitch_Output3_26;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.Output4 -= uScriptCon_ManualSwitch_Output4_26;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.Output5 -= uScriptCon_ManualSwitch_Output5_26;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.Output6 -= uScriptCon_ManualSwitch_Output6_26;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.Output7 -= uScriptCon_ManualSwitch_Output7_26;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.Output8 -= uScriptCon_ManualSwitch_Output8_26;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.Out -= SubGraph_CompleteObjectiveStage_Out_49;
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_74.Out -= SubGraph_RR_TeslaBase_TeslaTurretController_Out_74;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Save_Out -= SubGraph_SaveLoadInt_Save_Out_78;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Load_Out -= SubGraph_SaveLoadInt_Load_Out_78;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_78;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.Out -= SubGraph_LoadObjectiveStates_Out_80;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_92.Out -= SubGraph_CompleteObjectiveStage_Out_92;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_94.Out -= SubGraph_CompleteObjectiveStage_Out_94;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_121.Out -= SubGraph_RR_TeslaBase_ForcefieldController_Out_121;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_124.Out -= SubGraph_RR_TeslaBase_ForcefieldController_Out_124;
		logic_SubGraph_RR_CombatLab_BossTurretController_SubGraph_RR_CombatLab_BossTurretController_150.Out -= SubGraph_RR_CombatLab_BossTurretController_Out_150;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Save_Out -= SubGraph_SaveLoadBool_Save_Out_207;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Load_Out -= SubGraph_SaveLoadBool_Load_Out_207;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_207;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.Output1 -= uScriptCon_ManualSwitch_Output1_209;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.Output2 -= uScriptCon_ManualSwitch_Output2_209;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.Output3 -= uScriptCon_ManualSwitch_Output3_209;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.Output4 -= uScriptCon_ManualSwitch_Output4_209;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.Output5 -= uScriptCon_ManualSwitch_Output5_209;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.Output6 -= uScriptCon_ManualSwitch_Output6_209;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.Output7 -= uScriptCon_ManualSwitch_Output7_209;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.Output8 -= uScriptCon_ManualSwitch_Output8_209;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221.Save_Out -= SubGraph_SaveLoadInt_Save_Out_221;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221.Load_Out -= SubGraph_SaveLoadInt_Load_Out_221;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_221;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.Output1 -= uScriptCon_ManualSwitch_Output1_241;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.Output2 -= uScriptCon_ManualSwitch_Output2_241;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.Output3 -= uScriptCon_ManualSwitch_Output3_241;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.Output4 -= uScriptCon_ManualSwitch_Output4_241;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.Output5 -= uScriptCon_ManualSwitch_Output5_241;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.Output6 -= uScriptCon_ManualSwitch_Output6_241;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.Output7 -= uScriptCon_ManualSwitch_Output7_241;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.Output8 -= uScriptCon_ManualSwitch_Output8_241;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Save_Out -= SubGraph_SaveLoadInt_Save_Out_246;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Load_Out -= SubGraph_SaveLoadInt_Load_Out_246;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_246;
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

	private void Instance_KeyEvent_166(object o, EventArgs e)
	{
		Relay_KeyEvent_166();
	}

	private void SubGraph_RR_LaserLab_TurretAndChargerController_Out_10(object o, SubGraph_RR_LaserLab_TurretAndChargerController.LogicEventArgs e)
	{
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_10 = e.turretsDestroyed;
		local_LaserTurretsDestroyed_System_Boolean = logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_10;
		Relay_Out_10();
	}

	private void SubGraph_SaveLoadBool_Save_Out_14(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_14;
		Relay_Save_Out_14();
	}

	private void SubGraph_SaveLoadBool_Load_Out_14(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_14;
		Relay_Load_Out_14();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_14(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_14;
		Relay_Restart_Out_14();
	}

	private void uScriptCon_ManualSwitch_Output1_26(object o, EventArgs e)
	{
		Relay_Output1_26();
	}

	private void uScriptCon_ManualSwitch_Output2_26(object o, EventArgs e)
	{
		Relay_Output2_26();
	}

	private void uScriptCon_ManualSwitch_Output3_26(object o, EventArgs e)
	{
		Relay_Output3_26();
	}

	private void uScriptCon_ManualSwitch_Output4_26(object o, EventArgs e)
	{
		Relay_Output4_26();
	}

	private void uScriptCon_ManualSwitch_Output5_26(object o, EventArgs e)
	{
		Relay_Output5_26();
	}

	private void uScriptCon_ManualSwitch_Output6_26(object o, EventArgs e)
	{
		Relay_Output6_26();
	}

	private void uScriptCon_ManualSwitch_Output7_26(object o, EventArgs e)
	{
		Relay_Output7_26();
	}

	private void uScriptCon_ManualSwitch_Output8_26(object o, EventArgs e)
	{
		Relay_Output8_26();
	}

	private void SubGraph_CompleteObjectiveStage_Out_49(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_49 = e.objectiveStage;
		local_CurrentObjective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_49;
		Relay_Out_49();
	}

	private void SubGraph_RR_TeslaBase_TeslaTurretController_Out_74(object o, SubGraph_RR_TeslaBase_TeslaTurretController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretsDestroyed_74 = e.turretsDestroyed;
		local_TeslaTurretsDestroyed_System_Boolean = logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretsDestroyed_74;
		Relay_Out_74();
	}

	private void SubGraph_SaveLoadInt_Save_Out_78(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_78 = e.integer;
		local_CurrentObjective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_78;
		Relay_Save_Out_78();
	}

	private void SubGraph_SaveLoadInt_Load_Out_78(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_78 = e.integer;
		local_CurrentObjective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_78;
		Relay_Load_Out_78();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_78(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_78 = e.integer;
		local_CurrentObjective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_78;
		Relay_Restart_Out_78();
	}

	private void SubGraph_LoadObjectiveStates_Out_80(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_80();
	}

	private void SubGraph_CompleteObjectiveStage_Out_92(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_92 = e.objectiveStage;
		local_CurrentObjective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_92;
		Relay_Out_92();
	}

	private void SubGraph_CompleteObjectiveStage_Out_94(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_94 = e.objectiveStage;
		local_CurrentObjective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_94;
		Relay_Out_94();
	}

	private void SubGraph_RR_TeslaBase_ForcefieldController_Out_121(object o, SubGraph_RR_TeslaBase_ForcefieldController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_121 = e.Forcefields;
		local_ArenaForcefields_TankArray = logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_121;
		Relay_Out_121();
	}

	private void SubGraph_RR_TeslaBase_ForcefieldController_Out_124(object o, SubGraph_RR_TeslaBase_ForcefieldController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_124 = e.Forcefields;
		local_EntranceForcefields_TankArray = logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_124;
		Relay_Out_124();
	}

	private void SubGraph_RR_CombatLab_BossTurretController_Out_150(object o, SubGraph_RR_CombatLab_BossTurretController.LogicEventArgs e)
	{
		logic_SubGraph_RR_CombatLab_BossTurretController_turretDestroyed_150 = e.turretDestroyed;
		local_BossTurretDestroyed_System_Boolean = logic_SubGraph_RR_CombatLab_BossTurretController_turretDestroyed_150;
		Relay_Out_150();
	}

	private void SubGraph_SaveLoadBool_Save_Out_207(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = e.boolean;
		local_Init_TeslaTurrets_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_207;
		Relay_Save_Out_207();
	}

	private void SubGraph_SaveLoadBool_Load_Out_207(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = e.boolean;
		local_Init_TeslaTurrets_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_207;
		Relay_Load_Out_207();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_207(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = e.boolean;
		local_Init_TeslaTurrets_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_207;
		Relay_Restart_Out_207();
	}

	private void uScriptCon_ManualSwitch_Output1_209(object o, EventArgs e)
	{
		Relay_Output1_209();
	}

	private void uScriptCon_ManualSwitch_Output2_209(object o, EventArgs e)
	{
		Relay_Output2_209();
	}

	private void uScriptCon_ManualSwitch_Output3_209(object o, EventArgs e)
	{
		Relay_Output3_209();
	}

	private void uScriptCon_ManualSwitch_Output4_209(object o, EventArgs e)
	{
		Relay_Output4_209();
	}

	private void uScriptCon_ManualSwitch_Output5_209(object o, EventArgs e)
	{
		Relay_Output5_209();
	}

	private void uScriptCon_ManualSwitch_Output6_209(object o, EventArgs e)
	{
		Relay_Output6_209();
	}

	private void uScriptCon_ManualSwitch_Output7_209(object o, EventArgs e)
	{
		Relay_Output7_209();
	}

	private void uScriptCon_ManualSwitch_Output8_209(object o, EventArgs e)
	{
		Relay_Output8_209();
	}

	private void SubGraph_SaveLoadInt_Save_Out_221(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_221 = e.integer;
		local_Objective02MessageCount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_221;
		Relay_Save_Out_221();
	}

	private void SubGraph_SaveLoadInt_Load_Out_221(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_221 = e.integer;
		local_Objective02MessageCount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_221;
		Relay_Load_Out_221();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_221(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_221 = e.integer;
		local_Objective02MessageCount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_221;
		Relay_Restart_Out_221();
	}

	private void uScriptCon_ManualSwitch_Output1_241(object o, EventArgs e)
	{
		Relay_Output1_241();
	}

	private void uScriptCon_ManualSwitch_Output2_241(object o, EventArgs e)
	{
		Relay_Output2_241();
	}

	private void uScriptCon_ManualSwitch_Output3_241(object o, EventArgs e)
	{
		Relay_Output3_241();
	}

	private void uScriptCon_ManualSwitch_Output4_241(object o, EventArgs e)
	{
		Relay_Output4_241();
	}

	private void uScriptCon_ManualSwitch_Output5_241(object o, EventArgs e)
	{
		Relay_Output5_241();
	}

	private void uScriptCon_ManualSwitch_Output6_241(object o, EventArgs e)
	{
		Relay_Output6_241();
	}

	private void uScriptCon_ManualSwitch_Output7_241(object o, EventArgs e)
	{
		Relay_Output7_241();
	}

	private void uScriptCon_ManualSwitch_Output8_241(object o, EventArgs e)
	{
		Relay_Output8_241();
	}

	private void SubGraph_SaveLoadInt_Save_Out_246(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_246 = e.integer;
		local_Objective04SubStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_246;
		Relay_Save_Out_246();
	}

	private void SubGraph_SaveLoadInt_Load_Out_246(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_246 = e.integer;
		local_Objective04SubStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_246;
		Relay_Load_Out_246();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_246(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_246 = e.integer;
		local_Objective04SubStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_246;
		Relay_Restart_Out_246();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_5();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
	}

	private void Relay_SaveEvent_2()
	{
		Relay_Save_14();
	}

	private void Relay_LoadEvent_2()
	{
		Relay_Load_14();
	}

	private void Relay_RestartEvent_2()
	{
		Relay_Set_False_14();
	}

	private void Relay_InitialSpawn_4()
	{
		int num = 0;
		Array array = laserTurretGroupSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_4.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_4, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_4, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_4 = owner_Connection_8;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_4.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_4, logic_uScript_SpawnTechsFromData_ownerNode_4, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_4, logic_uScript_SpawnTechsFromData_allowResurrection_4);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_4.Out)
		{
			Relay_InitialSpawn_66();
		}
	}

	private void Relay_In_5()
	{
		logic_uScriptCon_CompareBool_Bool_5 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.In(logic_uScriptCon_CompareBool_Bool_5);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.False;
		if (num)
		{
			Relay_In_197();
		}
		if (flag)
		{
			Relay_True_6();
		}
	}

	private void Relay_True_6()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_6.True(out logic_uScriptAct_SetBool_Target_6);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_6;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_6.Out)
		{
			Relay_InitialSpawn_88();
		}
	}

	private void Relay_False_6()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_6.False(out logic_uScriptAct_SetBool_Target_6);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_6;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_6.Out)
		{
			Relay_InitialSpawn_88();
		}
	}

	private void Relay_Out_10()
	{
		Relay_In_74();
	}

	private void Relay_In_10()
	{
		int num = 0;
		Array array = shieldChargerGroupSpawnData;
		if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_10.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_10, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_10, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array array2 = laserTurretGroupSpawnData;
		if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_10.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_10, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_10, num2, array2.Length);
		num2 += array2.Length;
		int num3 = 0;
		Array array3 = laserTurretGroupAnimTriggers;
		if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_10.Length != num3 + array3.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_10, num3 + array3.Length);
		}
		Array.Copy(array3, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_10, num3, array3.Length);
		num3 += array3.Length;
		int num4 = 0;
		Array array4 = laserTurretGroupAnimDurations;
		if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_10.Length != num4 + array4.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_10, num4 + array4.Length);
		}
		Array.Copy(array4, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_10, num4, array4.Length);
		num4 += array4.Length;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_10 = laserTurretWeaponBlockType;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_10 = laserTurretShieldBlockType;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_10.In(logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_10, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_10, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_10, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_10, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_10, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_10);
	}

	private void Relay_InitialSpawn_11()
	{
		int num = 0;
		Array array = bossTurretSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_11.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_11, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_11, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_11 = owner_Connection_12;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_11.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_11, logic_uScript_SpawnTechsFromData_ownerNode_11, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_11, logic_uScript_SpawnTechsFromData_allowResurrection_11);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_11.Out)
		{
			Relay_InitialSpawn_4();
		}
	}

	private void Relay_Save_Out_14()
	{
		Relay_Save_207();
	}

	private void Relay_Load_Out_14()
	{
		Relay_Load_207();
	}

	private void Relay_Restart_Out_14()
	{
		Relay_Set_False_207();
	}

	private void Relay_Save_14()
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_14 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Save(ref logic_SubGraph_SaveLoadBool_boolean_14, logic_SubGraph_SaveLoadBool_boolAsVariable_14, logic_SubGraph_SaveLoadBool_uniqueID_14);
	}

	private void Relay_Load_14()
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_14 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Load(ref logic_SubGraph_SaveLoadBool_boolean_14, logic_SubGraph_SaveLoadBool_boolAsVariable_14, logic_SubGraph_SaveLoadBool_uniqueID_14);
	}

	private void Relay_Set_True_14()
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_14 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_14, logic_SubGraph_SaveLoadBool_boolAsVariable_14, logic_SubGraph_SaveLoadBool_uniqueID_14);
	}

	private void Relay_Set_False_14()
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_14 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_14, logic_SubGraph_SaveLoadBool_boolAsVariable_14, logic_SubGraph_SaveLoadBool_uniqueID_14);
	}

	private void Relay_In_23()
	{
		int num = 0;
		Array array = shieldChargerGroupSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_23.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_23, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_23, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_23 = owner_Connection_24;
		logic_uScript_GetAndCheckTechs_Return_23 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_23.In(logic_uScript_GetAndCheckTechs_techData_23, logic_uScript_GetAndCheckTechs_ownerNode_23, ref logic_uScript_GetAndCheckTechs_techs_23);
		local_numChargersAlive_System_Int32 = logic_uScript_GetAndCheckTechs_Return_23;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_23.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_23.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_23.AllDead;
		if (allAlive)
		{
			Relay_In_209();
		}
		if (someAlive)
		{
			Relay_In_209();
		}
		if (allDead)
		{
			Relay_In_133();
		}
	}

	private void Relay_Output1_26()
	{
		Relay_In_43();
	}

	private void Relay_Output2_26()
	{
		Relay_In_23();
	}

	private void Relay_Output3_26()
	{
		Relay_In_146();
	}

	private void Relay_Output4_26()
	{
		Relay_In_109();
	}

	private void Relay_Output5_26()
	{
	}

	private void Relay_Output6_26()
	{
	}

	private void Relay_Output7_26()
	{
	}

	private void Relay_Output8_26()
	{
	}

	private void Relay_In_26()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_26 = local_CurrentObjective_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_26.In(logic_uScriptCon_ManualSwitch_CurrentOutput_26);
	}

	private void Relay_In_29()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_29 = NPCTriggerVolume01;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_29.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_29);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_29.OutOfRange)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_34()
	{
		logic_uScript_AddMessage_messageData_34 = msg01Intro;
		logic_uScript_AddMessage_speaker_34 = NPCSpeaker;
		logic_uScript_AddMessage_Return_34 = logic_uScript_AddMessage_uScript_AddMessage_34.In(logic_uScript_AddMessage_messageData_34, logic_uScript_AddMessage_speaker_34);
		local_MsgIntro_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_34;
		bool num = logic_uScript_AddMessage_uScript_AddMessage_34.Out;
		bool shown = logic_uScript_AddMessage_uScript_AddMessage_34.Shown;
		if (num)
		{
			Relay_In_64();
		}
		if (shown)
		{
			Relay_In_39();
		}
	}

	private void Relay_In_36()
	{
		logic_uScriptCon_CompareBool_Bool_36 = local_MsgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.In(logic_uScriptCon_CompareBool_Bool_36);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.True)
		{
			Relay_In_34();
		}
	}

	private void Relay_In_39()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_39.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_39.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_42()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_42 = local_MsgIntro_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_42.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_42, logic_uScript_RemoveOnScreenMessage_instant_42);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_42.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_43()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData01;
		if (logic_uScript_GetAndCheckTechs_techData_43.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_43, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_43, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_43 = owner_Connection_35;
		int num2 = 0;
		Array array = local_31_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_43.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_43, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_43, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_43 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_43.In(logic_uScript_GetAndCheckTechs_techData_43, logic_uScript_GetAndCheckTechs_ownerNode_43, ref logic_uScript_GetAndCheckTechs_techs_43);
		local_31_TankArray = logic_uScript_GetAndCheckTechs_techs_43;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_43.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_43.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_43.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_43.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_57();
		}
		if (someAlive)
		{
			Relay_AtIndex_57();
		}
		if (allDead)
		{
			Relay_In_62();
		}
		if (waitingToSpawn)
		{
			Relay_In_62();
		}
	}

	private void Relay_True_47()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_47.True(out logic_uScriptAct_SetBool_Target_47);
		local_MsgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_47;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_47.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_False_47()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_47.False(out logic_uScriptAct_SetBool_Target_47);
		local_MsgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_47;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_47.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_Out_49()
	{
	}

	private void Relay_In_49()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_49 = local_CurrentObjective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_49.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_49, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_49);
	}

	private void Relay_In_51()
	{
		logic_uScript_SetEncounterTarget_owner_51 = owner_Connection_53;
		logic_uScript_SetEncounterTarget_visibleObject_51 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_51.In(logic_uScript_SetEncounterTarget_owner_51, logic_uScript_SetEncounterTarget_visibleObject_51);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_51.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_54()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_54 = NPCTriggerVolume01;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_54.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_54);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_54.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_54.OutOfRange;
		if (inRange)
		{
			Relay_True_47();
		}
		if (outOfRange)
		{
			Relay_In_36();
		}
	}

	private void Relay_AtIndex_57()
	{
		int num = 0;
		Array array = local_31_TankArray;
		if (logic_uScript_AccessListTech_techList_57.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_57, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_57, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_57.AtIndex(ref logic_uScript_AccessListTech_techList_57, logic_uScript_AccessListTech_index_57, out logic_uScript_AccessListTech_value_57);
		local_31_TankArray = logic_uScript_AccessListTech_techList_57;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_57;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_57.Out)
		{
			Relay_In_51();
		}
	}

	private void Relay_In_60()
	{
		logic_uScript_FlyTechUpAndAway_tech_60 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_removalParticles_60 = NPCDespawnEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_60.In(logic_uScript_FlyTechUpAndAway_tech_60, logic_uScript_FlyTechUpAndAway_maxLifetime_60, logic_uScript_FlyTechUpAndAway_targetHeight_60, logic_uScript_FlyTechUpAndAway_aiTree_60, logic_uScript_FlyTechUpAndAway_removalParticles_60);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_60.Out)
		{
			Relay_In_191();
		}
	}

	private void Relay_In_62()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_62.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_62.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_True_63()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.True(out logic_uScriptAct_SetBool_Target_63);
		local_MsgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_63;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_63.Out)
		{
			Relay_In_160();
		}
	}

	private void Relay_False_63()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.False(out logic_uScriptAct_SetBool_Target_63);
		local_MsgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_63;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_63.Out)
		{
			Relay_In_160();
		}
	}

	private void Relay_In_64()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_64 = room01TriggerVolume;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_64.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_64);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_64.InRange)
		{
			Relay_In_29();
		}
	}

	private void Relay_InitialSpawn_66()
	{
		int num = 0;
		Array array = shieldChargerGroupSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_66.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_66, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_66, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_66 = owner_Connection_67;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_66.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_66, logic_uScript_SpawnTechsFromData_ownerNode_66, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_66, logic_uScript_SpawnTechsFromData_allowResurrection_66);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_66.Out)
		{
			Relay_InitialSpawn_72();
		}
	}

	private void Relay_InitialSpawn_68()
	{
		int num = 0;
		Array forcefieldInnerGroupSpawnData = ForcefieldInnerGroupSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_68.Length != num + forcefieldInnerGroupSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_68, num + forcefieldInnerGroupSpawnData.Length);
		}
		Array.Copy(forcefieldInnerGroupSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_68, num, forcefieldInnerGroupSpawnData.Length);
		num += forcefieldInnerGroupSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_68 = owner_Connection_69;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_68.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_68, logic_uScript_SpawnTechsFromData_ownerNode_68, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_68, logic_uScript_SpawnTechsFromData_allowResurrection_68);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_68.Out)
		{
			Relay_In_197();
		}
	}

	private void Relay_InitialSpawn_72()
	{
		int num = 0;
		Array forcefieldOuterGroupSpawnData = ForcefieldOuterGroupSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_72.Length != num + forcefieldOuterGroupSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_72, num + forcefieldOuterGroupSpawnData.Length);
		}
		Array.Copy(forcefieldOuterGroupSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_72, num, forcefieldOuterGroupSpawnData.Length);
		num += forcefieldOuterGroupSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_72 = owner_Connection_71;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_72.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_72, logic_uScript_SpawnTechsFromData_ownerNode_72, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_72, logic_uScript_SpawnTechsFromData_allowResurrection_72);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_72.Out)
		{
			Relay_InitialSpawn_68();
		}
	}

	private void Relay_Out_74()
	{
		Relay_In_150();
	}

	private void Relay_In_74()
	{
		int num = 0;
		Array array = shieldChargerGroupSpawnData;
		if (logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_74.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_74, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_74, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array array2 = teslaTurretGroupSpawnData;
		if (logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_74.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_74, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_74, num2, array2.Length);
		num2 += array2.Length;
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_teslaTurretShieldBlockType_74 = teslaTurretShieldBlockType;
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_74.In(logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_74, logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_74, logic_SubGraph_RR_TeslaBase_TeslaTurretController_teslaTurretShieldBlockType_74);
	}

	private void Relay_Save_Out_78()
	{
		Relay_Save_221();
	}

	private void Relay_Load_Out_78()
	{
		Relay_Load_221();
	}

	private void Relay_Restart_Out_78()
	{
		Relay_Restart_221();
	}

	private void Relay_Save_78()
	{
		logic_SubGraph_SaveLoadInt_integer_78 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_78 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Save(logic_SubGraph_SaveLoadInt_restartValue_78, ref logic_SubGraph_SaveLoadInt_integer_78, logic_SubGraph_SaveLoadInt_intAsVariable_78, logic_SubGraph_SaveLoadInt_uniqueID_78);
	}

	private void Relay_Load_78()
	{
		logic_SubGraph_SaveLoadInt_integer_78 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_78 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Load(logic_SubGraph_SaveLoadInt_restartValue_78, ref logic_SubGraph_SaveLoadInt_integer_78, logic_SubGraph_SaveLoadInt_intAsVariable_78, logic_SubGraph_SaveLoadInt_uniqueID_78);
	}

	private void Relay_Restart_78()
	{
		logic_SubGraph_SaveLoadInt_integer_78 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_78 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_78.Restart(logic_SubGraph_SaveLoadInt_restartValue_78, ref logic_SubGraph_SaveLoadInt_integer_78, logic_SubGraph_SaveLoadInt_intAsVariable_78, logic_SubGraph_SaveLoadInt_uniqueID_78);
	}

	private void Relay_Out_80()
	{
	}

	private void Relay_In_80()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_80 = local_CurrentObjective_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.In(logic_SubGraph_LoadObjectiveStates_currentObjective_80);
	}

	private void Relay_In_83()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_83 = owner_Connection_82;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_83.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_83);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_83.Out;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_83.True;
		bool flag2 = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_83.False;
		if (num)
		{
			Relay_In_124();
		}
		if (flag)
		{
			Relay_Pause_86();
		}
		if (flag2)
		{
			Relay_UnPause_86();
		}
	}

	private void Relay_In_84()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_84 = owner_Connection_85;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_84.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_84);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_84.Out)
		{
			Relay_In_83();
		}
	}

	private void Relay_Pause_86()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_86.Pause();
	}

	private void Relay_UnPause_86()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_86.UnPause();
	}

	private void Relay_InitialSpawn_88()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData01;
		if (logic_uScript_SpawnTechsFromData_spawnData_88.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_88, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_88, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_88 = owner_Connection_87;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_88.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_88, logic_uScript_SpawnTechsFromData_ownerNode_88, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_88, logic_uScript_SpawnTechsFromData_allowResurrection_88);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_88.Out)
		{
			Relay_InitialSpawn_11();
		}
	}

	private void Relay_Out_92()
	{
	}

	private void Relay_In_92()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_92 = local_CurrentObjective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_92.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_92, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_92);
	}

	private void Relay_Out_94()
	{
	}

	private void Relay_In_94()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_94 = local_CurrentObjective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_94.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_94, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_94);
	}

	private void Relay_In_97()
	{
		logic_uScript_SetEncounterTarget_owner_97 = owner_Connection_104;
		logic_uScript_SetEncounterTarget_visibleObject_97 = local_NPCTech02_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_97.In(logic_uScript_SetEncounterTarget_owner_97, logic_uScript_SetEncounterTarget_visibleObject_97);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_97.Out)
		{
			Relay_In_241();
		}
	}

	private void Relay_In_106()
	{
		logic_uScript_AddMessage_messageData_106 = msg07Outro;
		logic_uScript_AddMessage_speaker_106 = NPCSpeaker;
		logic_uScript_AddMessage_Return_106 = logic_uScript_AddMessage_uScript_AddMessage_106.In(logic_uScript_AddMessage_messageData_106, logic_uScript_AddMessage_speaker_106);
		if (logic_uScript_AddMessage_uScript_AddMessage_106.Shown)
		{
			Relay_In_115();
		}
	}

	private void Relay_In_109()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData02;
		if (logic_uScript_GetAndCheckTechs_techData_109.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_109, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_109, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_109 = owner_Connection_101;
		int num2 = 0;
		Array array = local_99_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_109.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_109, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_109, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_109 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_109.In(logic_uScript_GetAndCheckTechs_techData_109, logic_uScript_GetAndCheckTechs_ownerNode_109, ref logic_uScript_GetAndCheckTechs_techs_109);
		local_99_TankArray = logic_uScript_GetAndCheckTechs_techs_109;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_109.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_109.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_113();
		}
		if (someAlive)
		{
			Relay_AtIndex_113();
		}
	}

	private void Relay_InitialSpawn_110()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData02;
		if (logic_uScript_SpawnTechsFromData_spawnData_110.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_110, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_110, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_110 = owner_Connection_114;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_110.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_110, logic_uScript_SpawnTechsFromData_ownerNode_110, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_110, logic_uScript_SpawnTechsFromData_allowResurrection_110);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_110.Out)
		{
			Relay_In_94();
		}
	}

	private void Relay_Succeed_111()
	{
		logic_uScript_FinishEncounter_owner_111 = owner_Connection_102;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_111.Succeed(logic_uScript_FinishEncounter_owner_111);
	}

	private void Relay_Fail_111()
	{
		logic_uScript_FinishEncounter_owner_111 = owner_Connection_102;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_111.Fail(logic_uScript_FinishEncounter_owner_111);
	}

	private void Relay_AtIndex_113()
	{
		int num = 0;
		Array array = local_99_TankArray;
		if (logic_uScript_AccessListTech_techList_113.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_113, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_113, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_113.AtIndex(ref logic_uScript_AccessListTech_techList_113, logic_uScript_AccessListTech_index_113, out logic_uScript_AccessListTech_value_113);
		local_99_TankArray = logic_uScript_AccessListTech_techList_113;
		local_NPCTech02_Tank = logic_uScript_AccessListTech_value_113;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_113.Out)
		{
			Relay_In_97();
		}
	}

	private void Relay_In_115()
	{
		logic_uScript_FlyTechUpAndAway_tech_115 = local_NPCTech02_Tank;
		logic_uScript_FlyTechUpAndAway_removalParticles_115 = NPCDespawnEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_115.In(logic_uScript_FlyTechUpAndAway_tech_115, logic_uScript_FlyTechUpAndAway_maxLifetime_115, logic_uScript_FlyTechUpAndAway_targetHeight_115, logic_uScript_FlyTechUpAndAway_aiTree_115, logic_uScript_FlyTechUpAndAway_removalParticles_115);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_115.Out)
		{
			Relay_Succeed_111();
		}
	}

	private void Relay_In_117()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_117 = NPCTriggerVolume02;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_117.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_117);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_117.InRange)
		{
			Relay_In_244();
		}
	}

	private void Relay_Out_121()
	{
		Relay_In_10();
	}

	private void Relay_In_121()
	{
		int num = 0;
		Array forcefieldInnerGroupSpawnData = ForcefieldInnerGroupSpawnData;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_121.Length != num + forcefieldInnerGroupSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_121, num + forcefieldInnerGroupSpawnData.Length);
		}
		Array.Copy(forcefieldInnerGroupSpawnData, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_121, num, forcefieldInnerGroupSpawnData.Length);
		num += forcefieldInnerGroupSpawnData.Length;
		int num2 = 0;
		Array array = local_ArenaForcefields_TankArray;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_121.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_121, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_121, num2, array.Length);
		num2 += array.Length;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_121.In(logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_121, ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_121);
	}

	private void Relay_Out_124()
	{
		Relay_In_121();
	}

	private void Relay_In_124()
	{
		int num = 0;
		Array forcefieldOuterGroupSpawnData = ForcefieldOuterGroupSpawnData;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_124.Length != num + forcefieldOuterGroupSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_124, num + forcefieldOuterGroupSpawnData.Length);
		}
		Array.Copy(forcefieldOuterGroupSpawnData, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_124, num, forcefieldOuterGroupSpawnData.Length);
		num += forcefieldOuterGroupSpawnData.Length;
		int num2 = 0;
		Array array = local_EntranceForcefields_TankArray;
		if (logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_124.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_124, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_124, num2, array.Length);
		num2 += array.Length;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_124.In(logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_124, ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_124);
	}

	private void Relay_In_129()
	{
		logic_uScript_AddMessage_messageData_129 = msg06BossDefeated;
		logic_uScript_AddMessage_speaker_129 = NPCSpeaker;
		logic_uScript_AddMessage_Return_129 = logic_uScript_AddMessage_uScript_AddMessage_129.In(logic_uScript_AddMessage_messageData_129, logic_uScript_AddMessage_speaker_129);
		if (logic_uScript_AddMessage_uScript_AddMessage_129.Out)
		{
			Relay_InitialSpawn_110();
		}
	}

	private void Relay_In_133()
	{
		logic_uScript_AddMessage_messageData_133 = msg05ChargersDestroyedAll;
		logic_uScript_AddMessage_speaker_133 = NPCSpeaker;
		logic_uScript_AddMessage_Return_133 = logic_uScript_AddMessage_uScript_AddMessage_133.In(logic_uScript_AddMessage_messageData_133, logic_uScript_AddMessage_speaker_133);
		if (logic_uScript_AddMessage_uScript_AddMessage_133.Out)
		{
			Relay_In_151();
		}
	}

	private void Relay_In_138()
	{
		logic_uScript_SetEncounterTarget_owner_138 = owner_Connection_137;
		logic_uScript_SetEncounterTarget_visibleObject_138 = local_BossTurretTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_138.In(logic_uScript_SetEncounterTarget_owner_138, logic_uScript_SetEncounterTarget_visibleObject_138);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_138.Out)
		{
			Relay_In_49();
		}
	}

	private void Relay_In_139()
	{
		logic_uScript_SetTechAIType_tech_139 = local_BossTurretTech_Tank;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_139.In(logic_uScript_SetTechAIType_tech_139, logic_uScript_SetTechAIType_aiType_139);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_139.Out)
		{
			Relay_In_247();
		}
	}

	private void Relay_In_146()
	{
		logic_uScriptCon_CompareBool_Bool_146 = local_BossTurretDestroyed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146.In(logic_uScriptCon_CompareBool_Bool_146);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146.True)
		{
			Relay_In_175();
		}
	}

	private void Relay_In_148()
	{
		logic_uScript_SetTankTeam_tank_148 = local_BossTurretTech_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_148.In(logic_uScript_SetTankTeam_tank_148, logic_uScript_SetTankTeam_team_148);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_148.Out)
		{
			Relay_In_139();
		}
	}

	private void Relay_Out_150()
	{
		Relay_In_26();
	}

	private void Relay_In_150()
	{
		int num = 0;
		Array array = shieldChargerGroupSpawnData;
		if (logic_SubGraph_RR_CombatLab_BossTurretController_chargerSpawnData_150.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_CombatLab_BossTurretController_chargerSpawnData_150, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_CombatLab_BossTurretController_chargerSpawnData_150, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array array2 = bossTurretSpawnData;
		if (logic_SubGraph_RR_CombatLab_BossTurretController_turretSpawnData_150.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_CombatLab_BossTurretController_turretSpawnData_150, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_RR_CombatLab_BossTurretController_turretSpawnData_150, num2, array2.Length);
		num2 += array2.Length;
		logic_SubGraph_RR_CombatLab_BossTurretController_teslaTurretShieldBlockType_150 = bossTurretShieldBlockType;
		logic_SubGraph_RR_CombatLab_BossTurretController_SubGraph_RR_CombatLab_BossTurretController_150.In(logic_SubGraph_RR_CombatLab_BossTurretController_chargerSpawnData_150, logic_SubGraph_RR_CombatLab_BossTurretController_turretSpawnData_150, logic_SubGraph_RR_CombatLab_BossTurretController_teslaTurretShieldBlockType_150);
	}

	private void Relay_In_151()
	{
		int num = 0;
		Array array = bossTurretSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_151.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_151, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_151, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_151 = owner_Connection_153;
		int num2 = 0;
		Array array2 = local_154_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_151.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_151, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_151, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_151 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151.In(logic_uScript_GetAndCheckTechs_techData_151, logic_uScript_GetAndCheckTechs_ownerNode_151, ref logic_uScript_GetAndCheckTechs_techs_151);
		local_154_TankArray = logic_uScript_GetAndCheckTechs_techs_151;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_155();
		}
		if (someAlive)
		{
			Relay_AtIndex_155();
		}
	}

	private void Relay_AtIndex_155()
	{
		int num = 0;
		Array array = local_154_TankArray;
		if (logic_uScript_AccessListTech_techList_155.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_155, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_155, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_155.AtIndex(ref logic_uScript_AccessListTech_techList_155, logic_uScript_AccessListTech_index_155, out logic_uScript_AccessListTech_value_155);
		local_154_TankArray = logic_uScript_AccessListTech_techList_155;
		local_BossTurretTech_Tank = logic_uScript_AccessListTech_value_155;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_155.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_In_160()
	{
		int num = 0;
		Array array = bossTurretSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_160.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_160, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_160, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_160 = owner_Connection_159;
		int num2 = 0;
		Array array2 = local_163_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_160.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_160, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_160, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_160 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_160.In(logic_uScript_GetAndCheckTechs_techData_160, logic_uScript_GetAndCheckTechs_ownerNode_160, ref logic_uScript_GetAndCheckTechs_techs_160);
		local_163_TankArray = logic_uScript_GetAndCheckTechs_techs_160;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_160.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_160.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_162();
		}
		if (someAlive)
		{
			Relay_AtIndex_162();
		}
	}

	private void Relay_AtIndex_162()
	{
		int num = 0;
		Array array = local_163_TankArray;
		if (logic_uScript_AccessListTech_techList_162.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_162, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_162, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_162.AtIndex(ref logic_uScript_AccessListTech_techList_162, logic_uScript_AccessListTech_index_162, out logic_uScript_AccessListTech_value_162);
		local_163_TankArray = logic_uScript_AccessListTech_techList_162;
		local_BossTurretTech_Tank = logic_uScript_AccessListTech_value_162;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_162.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_KeyEvent_166()
	{
		Relay_In_174();
	}

	private void Relay_In_167()
	{
		logic_uScriptAct_GetKeyCode_uScriptAct_GetKeyCode_167.In(out logic_uScriptAct_GetKeyCode_result_167, out logic_uScriptAct_GetKeyCode_resultString_167);
		local_172_UnityEngine_KeyCode = logic_uScriptAct_GetKeyCode_result_167;
		if (logic_uScriptAct_GetKeyCode_uScriptAct_GetKeyCode_167.Out)
		{
			Relay_In_171();
		}
	}

	private void Relay_In_170()
	{
		logic_uScriptCon_CompareKeyCodes_A_170 = local_172_UnityEngine_KeyCode;
		logic_uScriptCon_CompareKeyCodes_uScriptCon_CompareKeyCodes_170.In(logic_uScriptCon_CompareKeyCodes_A_170, logic_uScriptCon_CompareKeyCodes_B_170);
		if (logic_uScriptCon_CompareKeyCodes_uScriptCon_CompareKeyCodes_170.EqualTo)
		{
			Relay_In_184();
		}
	}

	private void Relay_In_171()
	{
		logic_uScriptCon_CompareKeyCodes_A_171 = local_172_UnityEngine_KeyCode;
		logic_uScriptCon_CompareKeyCodes_uScriptCon_CompareKeyCodes_171.In(logic_uScriptCon_CompareKeyCodes_A_171, logic_uScriptCon_CompareKeyCodes_B_171);
		bool equalTo = logic_uScriptCon_CompareKeyCodes_uScriptCon_CompareKeyCodes_171.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareKeyCodes_uScriptCon_CompareKeyCodes_171.NotEqualTo;
		if (equalTo)
		{
			Relay_In_188();
		}
		if (notEqualTo)
		{
			Relay_In_170();
		}
	}

	private void Relay_In_174()
	{
		logic_uScriptCon_CompareBool_Bool_174 = _DEBUG_SKIPS_ACTIVE;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_174.In(logic_uScriptCon_CompareBool_Bool_174);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_174.True)
		{
			Relay_In_167();
		}
	}

	private void Relay_In_175()
	{
		int num = 0;
		Array forcefieldInnerGroupSpawnData = ForcefieldInnerGroupSpawnData;
		if (logic_uScript_DestroyTechsFromData_techData_175.Length != num + forcefieldInnerGroupSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_175, num + forcefieldInnerGroupSpawnData.Length);
		}
		Array.Copy(forcefieldInnerGroupSpawnData, 0, logic_uScript_DestroyTechsFromData_techData_175, num, forcefieldInnerGroupSpawnData.Length);
		num += forcefieldInnerGroupSpawnData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_175 = owner_Connection_177;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_175.In(logic_uScript_DestroyTechsFromData_techData_175, logic_uScript_DestroyTechsFromData_shouldExplode_175, logic_uScript_DestroyTechsFromData_ownerNode_175);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_175.Out)
		{
			Relay_In_178();
		}
	}

	private void Relay_In_178()
	{
		int num = 0;
		Array array = laserTurretGroupSpawnData;
		if (logic_uScript_DestroyTechsFromData_techData_178.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_178, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_178, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_178 = owner_Connection_179;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_178.In(logic_uScript_DestroyTechsFromData_techData_178, logic_uScript_DestroyTechsFromData_shouldExplode_178, logic_uScript_DestroyTechsFromData_ownerNode_178);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_178.Out)
		{
			Relay_In_180();
		}
	}

	private void Relay_In_180()
	{
		int num = 0;
		Array array = teslaTurretGroupSpawnData;
		if (logic_uScript_DestroyTechsFromData_techData_180.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_180, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_180, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_180 = owner_Connection_182;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_180.In(logic_uScript_DestroyTechsFromData_techData_180, logic_uScript_DestroyTechsFromData_shouldExplode_180, logic_uScript_DestroyTechsFromData_ownerNode_180);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_180.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_In_184()
	{
		int num = 0;
		Array array = bossTurretSpawnData;
		if (logic_uScript_DestroyTechsFromData_techData_184.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_184, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_184, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_184 = owner_Connection_185;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_184.In(logic_uScript_DestroyTechsFromData_techData_184, logic_uScript_DestroyTechsFromData_shouldExplode_184, logic_uScript_DestroyTechsFromData_ownerNode_184);
	}

	private void Relay_In_188()
	{
		int num = 0;
		Array array = shieldChargerGroupSpawnData;
		if (logic_uScript_DestroyTechsFromData_techData_188.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_188, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_188, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_188 = owner_Connection_187;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_188.In(logic_uScript_DestroyTechsFromData_techData_188, logic_uScript_DestroyTechsFromData_shouldExplode_188, logic_uScript_DestroyTechsFromData_ownerNode_188);
	}

	private void Relay_In_191()
	{
		int num = 0;
		Array forcefieldOuterGroupSpawnData = ForcefieldOuterGroupSpawnData;
		if (logic_uScript_DestroyTechsFromData_techData_191.Length != num + forcefieldOuterGroupSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_191, num + forcefieldOuterGroupSpawnData.Length);
		}
		Array.Copy(forcefieldOuterGroupSpawnData, 0, logic_uScript_DestroyTechsFromData_techData_191, num, forcefieldOuterGroupSpawnData.Length);
		num += forcefieldOuterGroupSpawnData.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_191 = owner_Connection_190;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_191.In(logic_uScript_DestroyTechsFromData_techData_191, logic_uScript_DestroyTechsFromData_shouldExplode_191, logic_uScript_DestroyTechsFromData_ownerNode_191);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_191.Out)
		{
			Relay_False_63();
		}
	}

	private void Relay_InitialSpawn_194()
	{
		int num = 0;
		Array array = teslaTurretGroupSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_194.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_194, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_194, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_194 = owner_Connection_206;
		logic_uScript_SpawnTechsFromData_delayBetweenSpawns_194 = telsaTurretGroupSpawnInterval;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_194.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_194, logic_uScript_SpawnTechsFromData_ownerNode_194, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_194, logic_uScript_SpawnTechsFromData_allowResurrection_194);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_194.Out)
		{
			Relay_True_204();
		}
	}

	private void Relay_In_197()
	{
		logic_uScriptCon_CompareBool_Bool_197 = local_Init_TeslaTurrets_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.In(logic_uScriptCon_CompareBool_Bool_197);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.False;
		if (num)
		{
			Relay_In_84();
		}
		if (flag)
		{
			Relay_In_199();
		}
	}

	private void Relay_In_199()
	{
		logic_uScript_GetPositionInEncounter_ownerNode_199 = owner_Connection_193;
		logic_uScript_GetPositionInEncounter_posName_199 = initialTechSpawnCheck_Position;
		logic_uScript_GetPositionInEncounter_Return_199 = logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_199.In(logic_uScript_GetPositionInEncounter_ownerNode_199, logic_uScript_GetPositionInEncounter_posName_199);
		local_198_UnityEngine_Vector3 = logic_uScript_GetPositionInEncounter_Return_199;
		if (logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_199.Out)
		{
			Relay_In_202();
		}
	}

	private void Relay_In_202()
	{
		logic_uScript_PlayerInRange_position_202 = local_198_UnityEngine_Vector3;
		logic_uScript_PlayerInRange_range_202 = initialTechSpawnCheck_Distance;
		logic_uScript_PlayerInRange_uScript_PlayerInRange_202.In(logic_uScript_PlayerInRange_position_202, logic_uScript_PlayerInRange_range_202);
		bool num = logic_uScript_PlayerInRange_uScript_PlayerInRange_202.True;
		bool flag = logic_uScript_PlayerInRange_uScript_PlayerInRange_202.False;
		if (num)
		{
			Relay_InitialSpawn_194();
		}
		if (flag)
		{
			Relay_In_235();
		}
	}

	private void Relay_True_204()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_204.True(out logic_uScriptAct_SetBool_Target_204);
		local_Init_TeslaTurrets_System_Boolean = logic_uScriptAct_SetBool_Target_204;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_204.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_False_204()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_204.False(out logic_uScriptAct_SetBool_Target_204);
		local_Init_TeslaTurrets_System_Boolean = logic_uScriptAct_SetBool_Target_204;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_204.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_Save_Out_207()
	{
		Relay_Save_78();
	}

	private void Relay_Load_Out_207()
	{
		Relay_Load_78();
	}

	private void Relay_Restart_Out_207()
	{
		Relay_Restart_78();
	}

	private void Relay_Save_207()
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = local_Init_TeslaTurrets_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_207 = local_Init_TeslaTurrets_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Save(ref logic_SubGraph_SaveLoadBool_boolean_207, logic_SubGraph_SaveLoadBool_boolAsVariable_207, logic_SubGraph_SaveLoadBool_uniqueID_207);
	}

	private void Relay_Load_207()
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = local_Init_TeslaTurrets_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_207 = local_Init_TeslaTurrets_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Load(ref logic_SubGraph_SaveLoadBool_boolean_207, logic_SubGraph_SaveLoadBool_boolAsVariable_207, logic_SubGraph_SaveLoadBool_uniqueID_207);
	}

	private void Relay_Set_True_207()
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = local_Init_TeslaTurrets_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_207 = local_Init_TeslaTurrets_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_207, logic_SubGraph_SaveLoadBool_boolAsVariable_207, logic_SubGraph_SaveLoadBool_uniqueID_207);
	}

	private void Relay_Set_False_207()
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = local_Init_TeslaTurrets_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_207 = local_Init_TeslaTurrets_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_207, logic_SubGraph_SaveLoadBool_boolAsVariable_207, logic_SubGraph_SaveLoadBool_uniqueID_207);
	}

	private void Relay_Output1_209()
	{
		Relay_In_228();
	}

	private void Relay_Output2_209()
	{
		Relay_In_211();
	}

	private void Relay_Output3_209()
	{
		Relay_In_217();
	}

	private void Relay_Output4_209()
	{
	}

	private void Relay_Output5_209()
	{
	}

	private void Relay_Output6_209()
	{
	}

	private void Relay_Output7_209()
	{
	}

	private void Relay_Output8_209()
	{
	}

	private void Relay_In_209()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_209 = local_Objective02MessageCount_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_209.In(logic_uScriptCon_ManualSwitch_CurrentOutput_209);
	}

	private void Relay_In_211()
	{
		logic_uScriptCon_CompareInt_A_211 = local_numChargersAlive_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_211.In(logic_uScriptCon_CompareInt_A_211, logic_uScriptCon_CompareInt_B_211);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_211.LessThanOrEqualTo)
		{
			Relay_In_214();
		}
	}

	private void Relay_In_214()
	{
		logic_uScript_AddMessage_messageData_214 = msg03ChargersDestroyedHalfway;
		logic_uScript_AddMessage_speaker_214 = NPCSpeaker;
		logic_uScript_AddMessage_Return_214 = logic_uScript_AddMessage_uScript_AddMessage_214.In(logic_uScript_AddMessage_messageData_214, logic_uScript_AddMessage_speaker_214);
		if (logic_uScript_AddMessage_uScript_AddMessage_214.Out)
		{
			Relay_In_223();
		}
	}

	private void Relay_In_217()
	{
		logic_uScriptCon_CompareInt_A_217 = local_numChargersAlive_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_217.In(logic_uScriptCon_CompareInt_A_217, logic_uScriptCon_CompareInt_B_217);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_217.EqualTo)
		{
			Relay_In_220();
		}
	}

	private void Relay_In_220()
	{
		logic_uScript_AddMessage_messageData_220 = msg04ChargersDestroyedOneLeft;
		logic_uScript_AddMessage_speaker_220 = NPCSpeaker;
		logic_uScript_AddMessage_Return_220 = logic_uScript_AddMessage_uScript_AddMessage_220.In(logic_uScript_AddMessage_messageData_220, logic_uScript_AddMessage_speaker_220);
		if (logic_uScript_AddMessage_uScript_AddMessage_220.Out)
		{
			Relay_In_225();
		}
	}

	private void Relay_Save_Out_221()
	{
		Relay_Save_246();
	}

	private void Relay_Load_Out_221()
	{
		Relay_Load_246();
	}

	private void Relay_Restart_Out_221()
	{
		Relay_Restart_246();
	}

	private void Relay_Save_221()
	{
		logic_SubGraph_SaveLoadInt_integer_221 = local_Objective02MessageCount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_221 = local_Objective02MessageCount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221.Save(logic_SubGraph_SaveLoadInt_restartValue_221, ref logic_SubGraph_SaveLoadInt_integer_221, logic_SubGraph_SaveLoadInt_intAsVariable_221, logic_SubGraph_SaveLoadInt_uniqueID_221);
	}

	private void Relay_Load_221()
	{
		logic_SubGraph_SaveLoadInt_integer_221 = local_Objective02MessageCount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_221 = local_Objective02MessageCount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221.Load(logic_SubGraph_SaveLoadInt_restartValue_221, ref logic_SubGraph_SaveLoadInt_integer_221, logic_SubGraph_SaveLoadInt_intAsVariable_221, logic_SubGraph_SaveLoadInt_uniqueID_221);
	}

	private void Relay_Restart_221()
	{
		logic_SubGraph_SaveLoadInt_integer_221 = local_Objective02MessageCount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_221 = local_Objective02MessageCount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_221.Restart(logic_SubGraph_SaveLoadInt_restartValue_221, ref logic_SubGraph_SaveLoadInt_integer_221, logic_SubGraph_SaveLoadInt_intAsVariable_221, logic_SubGraph_SaveLoadInt_uniqueID_221);
	}

	private void Relay_In_223()
	{
		logic_uScriptAct_AddInt_v2_A_223 = local_Objective02MessageCount_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_223.In(logic_uScriptAct_AddInt_v2_A_223, logic_uScriptAct_AddInt_v2_B_223, out logic_uScriptAct_AddInt_v2_IntResult_223, out logic_uScriptAct_AddInt_v2_FloatResult_223);
		local_Objective02MessageCount_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_223;
	}

	private void Relay_In_225()
	{
		logic_uScriptAct_AddInt_v2_A_225 = local_Objective02MessageCount_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_225.In(logic_uScriptAct_AddInt_v2_A_225, logic_uScriptAct_AddInt_v2_B_225, out logic_uScriptAct_AddInt_v2_IntResult_225, out logic_uScriptAct_AddInt_v2_FloatResult_225);
		local_Objective02MessageCount_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_225;
	}

	private void Relay_In_228()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_228 = room01TriggerVolume;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_228.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_228);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_228.AllInRange)
		{
			Relay_In_233();
		}
	}

	private void Relay_In_229()
	{
		logic_uScriptAct_AddInt_v2_A_229 = local_Objective02MessageCount_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_229.In(logic_uScriptAct_AddInt_v2_A_229, logic_uScriptAct_AddInt_v2_B_229, out logic_uScriptAct_AddInt_v2_IntResult_229, out logic_uScriptAct_AddInt_v2_FloatResult_229);
		local_Objective02MessageCount_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_229;
	}

	private void Relay_In_233()
	{
		logic_uScript_AddMessage_messageData_233 = msg02TurretExplanation;
		logic_uScript_AddMessage_speaker_233 = NPCSpeaker;
		logic_uScript_AddMessage_Return_233 = logic_uScript_AddMessage_uScript_AddMessage_233.In(logic_uScript_AddMessage_messageData_233, logic_uScript_AddMessage_speaker_233);
		if (logic_uScript_AddMessage_uScript_AddMessage_233.Out)
		{
			Relay_In_229();
		}
	}

	private void Relay_In_235()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_235.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_235.Out)
		{
			Relay_In_236();
		}
	}

	private void Relay_In_236()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_236.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_236.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_Output1_241()
	{
		Relay_In_117();
	}

	private void Relay_Output2_241()
	{
		Relay_In_106();
	}

	private void Relay_Output3_241()
	{
	}

	private void Relay_Output4_241()
	{
	}

	private void Relay_Output5_241()
	{
	}

	private void Relay_Output6_241()
	{
	}

	private void Relay_Output7_241()
	{
	}

	private void Relay_Output8_241()
	{
	}

	private void Relay_In_241()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_241 = local_Objective04SubStage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_241.In(logic_uScriptCon_ManualSwitch_CurrentOutput_241);
	}

	private void Relay_In_244()
	{
		logic_uScriptAct_AddInt_v2_A_244 = local_Objective04SubStage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_244.In(logic_uScriptAct_AddInt_v2_A_244, logic_uScriptAct_AddInt_v2_B_244, out logic_uScriptAct_AddInt_v2_IntResult_244, out logic_uScriptAct_AddInt_v2_FloatResult_244);
		local_Objective04SubStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_244;
	}

	private void Relay_Save_Out_246()
	{
	}

	private void Relay_Load_Out_246()
	{
		Relay_In_80();
	}

	private void Relay_Restart_Out_246()
	{
	}

	private void Relay_Save_246()
	{
		logic_SubGraph_SaveLoadInt_integer_246 = local_Objective04SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_246 = local_Objective04SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Save(logic_SubGraph_SaveLoadInt_restartValue_246, ref logic_SubGraph_SaveLoadInt_integer_246, logic_SubGraph_SaveLoadInt_intAsVariable_246, logic_SubGraph_SaveLoadInt_uniqueID_246);
	}

	private void Relay_Load_246()
	{
		logic_SubGraph_SaveLoadInt_integer_246 = local_Objective04SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_246 = local_Objective04SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Load(logic_SubGraph_SaveLoadInt_restartValue_246, ref logic_SubGraph_SaveLoadInt_integer_246, logic_SubGraph_SaveLoadInt_intAsVariable_246, logic_SubGraph_SaveLoadInt_uniqueID_246);
	}

	private void Relay_Restart_246()
	{
		logic_SubGraph_SaveLoadInt_integer_246 = local_Objective04SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_246 = local_Objective04SubStage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Restart(logic_SubGraph_SaveLoadInt_restartValue_246, ref logic_SubGraph_SaveLoadInt_integer_246, logic_SubGraph_SaveLoadInt_intAsVariable_246, logic_SubGraph_SaveLoadInt_uniqueID_246);
	}

	private void Relay_In_247()
	{
		logic_uScript_SetBatteryChargeAmount_tech_247 = local_BossTurretTech_Tank;
		logic_uScript_SetBatteryChargeAmount_chargeAmount_247 = BossTurretHostileBatteryCharge;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_247.In(logic_uScript_SetBatteryChargeAmount_tech_247, logic_uScript_SetBatteryChargeAmount_chargeAmount_247);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_247.Out)
		{
			Relay_In_92();
		}
	}
}
