using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_SetPiece_RR_TeslaBase : uScriptLogic
{
	private delegate void ContinueExecution();

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private ContinueExecution m_ContinueExecution;

	private bool m_Breakpoint;

	private const int MaxRelayCallCount = 1000;

	private int relayCallCount;

	[Multiline(3)]
	public string[] blasterTurretGroup03AlertZones = new string[0];

	public SpawnTechData[] blasterTurretGroup03SpawnData = new SpawnTechData[0];

	[Multiline(3)]
	public string[] blasterTurretGroup04AlertZones = new string[0];

	public SpawnTechData[] blasterTurretGroup04SpawnData = new SpawnTechData[0];

	public BlockTypes blasterTurretShieldBlockType;

	public SpawnTechData[] ForcefieldGatesSpawnData = new SpawnTechData[0];

	public float initialTechSpawnCheck_Distance;

	[Multiline(3)]
	public string initialTechSpawnCheck_Position = "";

	public float[] laserTurretGroup02AnimDurations = new float[0];

	[Multiline(3)]
	public string[] laserTurretGroup02AnimTriggers = new string[0];

	public SpawnTechData[] laserTurretGroup02SpawnData = new SpawnTechData[0];

	public float[] laserTurretGroup03AnimDurations = new float[0];

	[Multiline(3)]
	public string[] laserTurretGroup03AnimTriggers = new string[0];

	public SpawnTechData[] laserTurretGroup03SpawnData = new SpawnTechData[0];

	public float[] laserTurretGroup04AnimDurations = new float[0];

	[Multiline(3)]
	public string[] laserTurretGroup04AnimTriggers = new string[0];

	public SpawnTechData[] laserTurretGroup04SpawnData = new SpawnTechData[0];

	public BlockTypes laserTurretShieldBlockType;

	public BlockTypes laserTurretWeaponBlockType;

	private Tank[] local_127_TankArray = new Tank[0];

	private Tank local_136_Tank;

	private Tank local_138_Tank;

	private Tank local_151_Tank;

	private Tank local_156_Tank;

	private Tank local_162_Tank;

	private Tank[] local_170_TankArray = new Tank[0];

	private Tank local_183_Tank;

	private Tank local_194_Tank;

	private Vector3 local_321_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private bool local_BlasterTurrets03Destroyed_System_Boolean;

	private bool local_BlasterTurrets04Destroyed_System_Boolean;

	private int local_CurrentObjective_System_Int32 = 1;

	private Tank[] local_ForcefieldGates_TankArray = new Tank[0];

	private bool local_Init_EnemyTurrets_System_Boolean;

	private bool local_Init_System_Boolean;

	private bool local_LaserTurrets02Destroyed_System_Boolean;

	private bool local_LaserTurrets03Destroyed_System_Boolean;

	private bool local_LaserTurrets04Destroyed_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_MsgIntro_ManOnScreenMessages_OnScreenMessage;

	private bool local_MsgIntroShown_System_Boolean;

	private bool local_msgTeslaBatteryCharge_System_Boolean;

	private Tank local_NPCTech_Tank;

	private Tank local_NPCTech02_Tank;

	private int local_Objective02SubStage_System_Int32 = 1;

	private Tank[] local_Room04Forcefields_TankArray = new Tank[0];

	private bool local_TeslaTurrets01Destroyed_System_Boolean;

	private bool local_TeslaTurrets02Destroyed_System_Boolean;

	private bool local_TeslaTurrets03Destroyed_System_Boolean;

	private bool local_TeslaTurrets04Destroyed_System_Boolean;

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02Room01Clear;

	public uScript_AddMessage.MessageData msg02TeslaBatteryCharge;

	public uScript_AddMessage.MessageData msg03Room02Clear;

	public uScript_AddMessage.MessageData msg04Room03Clear;

	public uScript_AddMessage.MessageData msg05Room04Clear;

	public uScript_AddMessage.MessageData msg06Outro;

	public Transform NPCDespawnEffect;

	public SpawnTechData[] NPCSpawnData01 = new SpawnTechData[0];

	public SpawnTechData[] NPCSpawnData02 = new SpawnTechData[0];

	public uScript_AddMessage.MessageSpeaker NPCSpeaker;

	[Multiline(3)]
	public string NPCTriggerVolume01 = "";

	[Multiline(3)]
	public string NPCTriggerVolume02 = "";

	[Multiline(3)]
	public string objectiveMarkerPosRoom01 = "";

	[Multiline(3)]
	public string objectiveMarkerPosRoom02 = "";

	[Multiline(3)]
	public string objectiveMarkerPosRoom03 = "";

	[Multiline(3)]
	public string objectiveMarkerPosRoom04 = "";

	[Multiline(3)]
	public string room01TriggerVolume = "";

	public SpawnTechData[] Room04ForcefieldsSpawnData = new SpawnTechData[0];

	public SpawnTechData[] shieldCharger01SpawnData = new SpawnTechData[0];

	public SpawnTechData[] shieldCharger02SpawnData = new SpawnTechData[0];

	public SpawnTechData[] shieldCharger03SpawnData = new SpawnTechData[0];

	public SpawnTechData[] shieldCharger04SpawnData = new SpawnTechData[0];

	public float telsaTurretGroup01SpawnInterval;

	public float telsaTurretGroup02SpawnInterval;

	public float telsaTurretGroup03SpawnInterval;

	public float telsaTurretGroup04SpawnInterval;

	public SpawnTechData[] teslaTurretGroup01SpawnData = new SpawnTechData[0];

	public SpawnTechData[] teslaTurretGroup02SpawnData = new SpawnTechData[0];

	public SpawnTechData[] teslaTurretGroup03SpawnData = new SpawnTechData[0];

	public SpawnTechData[] teslaTurretGroup04SpawnData = new SpawnTechData[0];

	public BlockTypes teslaTurretShieldBlockType;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_7;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_13;

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_20;

	private GameObject owner_Connection_30;

	private GameObject owner_Connection_69;

	private GameObject owner_Connection_88;

	private GameObject owner_Connection_118;

	private GameObject owner_Connection_126;

	private GameObject owner_Connection_129;

	private GameObject owner_Connection_171;

	private GameObject owner_Connection_205;

	private GameObject owner_Connection_214;

	private GameObject owner_Connection_215;

	private GameObject owner_Connection_221;

	private GameObject owner_Connection_224;

	private GameObject owner_Connection_254;

	private GameObject owner_Connection_256;

	private GameObject owner_Connection_267;

	private GameObject owner_Connection_283;

	private GameObject owner_Connection_289;

	private GameObject owner_Connection_292;

	private GameObject owner_Connection_295;

	private GameObject owner_Connection_303;

	private GameObject owner_Connection_315;

	private GameObject owner_Connection_319;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_2;

	private bool logic_uScriptCon_CompareBool_True_2 = true;

	private bool logic_uScriptCon_CompareBool_False_2 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_3 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_3;

	private bool logic_uScriptAct_SetBool_Out_3 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_3 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_3 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_5 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_5 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_5;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_5 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_5;

	private bool logic_uScript_SpawnTechsFromData_Out_5 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_10 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_10 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_10;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_10;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_10;

	private bool logic_uScript_SpawnTechsFromData_Out_10 = true;

	private SubGraph_RR_LaserLab_TurretAndChargerController logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_11 = new SubGraph_RR_LaserLab_TurretAndChargerController();

	private SpawnTechData[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_11 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_11 = new SpawnTechData[0];

	private string[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_11 = new string[0];

	private float[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_11 = new float[0];

	private BlockTypes logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_11;

	private BlockTypes logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_11;

	private bool logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_11;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_14 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_14 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_14;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_14;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_14;

	private bool logic_uScript_SpawnTechsFromData_Out_14 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_19 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_19 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_19;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_19;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_19;

	private bool logic_uScript_SpawnTechsFromData_Out_19 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_21 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_21 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_21;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_21;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_21;

	private bool logic_uScript_SpawnTechsFromData_Out_21 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_29 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_29 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_29;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_29 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_29;

	private bool logic_uScript_SpawnTechsFromData_Out_29 = true;

	private SubGraph_RR_TeslaBase_TeslaTurretController logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_31 = new SubGraph_RR_TeslaBase_TeslaTurretController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_31 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_31 = new SpawnTechData[0];

	private BlockTypes logic_SubGraph_RR_TeslaBase_TeslaTurretController_teslaTurretShieldBlockType_31;

	private bool logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretsDestroyed_31;

	private SubGraph_RR_TeslaBase_TeslaTurretController logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_36 = new SubGraph_RR_TeslaBase_TeslaTurretController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_36 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_36 = new SpawnTechData[0];

	private BlockTypes logic_SubGraph_RR_TeslaBase_TeslaTurretController_teslaTurretShieldBlockType_36;

	private bool logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretsDestroyed_36;

	private SubGraph_RR_TeslaBase_TeslaTurretController logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_43 = new SubGraph_RR_TeslaBase_TeslaTurretController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_43 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_43 = new SpawnTechData[0];

	private BlockTypes logic_SubGraph_RR_TeslaBase_TeslaTurretController_teslaTurretShieldBlockType_43;

	private bool logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretsDestroyed_43;

	private SubGraph_RR_LaserLab_TurretAndChargerController logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_51 = new SubGraph_RR_LaserLab_TurretAndChargerController();

	private SpawnTechData[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_51 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_51 = new SpawnTechData[0];

	private string[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_51 = new string[0];

	private float[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_51 = new float[0];

	private BlockTypes logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_51;

	private BlockTypes logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_51;

	private bool logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_51;

	private SubGraph_RR_LaserLab_TurretAndChargerController logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_54 = new SubGraph_RR_LaserLab_TurretAndChargerController();

	private SpawnTechData[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_54 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_54 = new SpawnTechData[0];

	private string[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_54 = new string[0];

	private float[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_54 = new float[0];

	private BlockTypes logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_54;

	private BlockTypes logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_54;

	private bool logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_54;

	private SubGraph_RR_TeslaBase_TeslaTurretController logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_61 = new SubGraph_RR_TeslaBase_TeslaTurretController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_61 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_61 = new SpawnTechData[0];

	private BlockTypes logic_SubGraph_RR_TeslaBase_TeslaTurretController_teslaTurretShieldBlockType_61;

	private bool logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretsDestroyed_61;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_67 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_67 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_67;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_67 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_67;

	private bool logic_uScript_SpawnTechsFromData_Out_67 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_71 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_71 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_71 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_71 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_71 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_71 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_71 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_73 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_73 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_73 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_73 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_73 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_73 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_73 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_74 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_74 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_74;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_74 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_74;

	private bool logic_uScript_SpawnTechsFromData_Out_74 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_76 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_76;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_76;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_81 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_81;

	private bool logic_uScript_RemoveOnScreenMessage_instant_81;

	private bool logic_uScript_RemoveOnScreenMessage_Out_81 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_82 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_82 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_83 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_83 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_83;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_83 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_83;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_83 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_83 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_83 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_83 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_92 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_92;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_92;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_92;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_92;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_92;

	private bool logic_uScript_FlyTechUpAndAway_Out_92 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_93 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_93 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_93 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_93 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_93 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_93 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_93 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_94 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_94;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_94;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_94;

	private bool logic_uScript_AddMessage_Out_94 = true;

	private bool logic_uScript_AddMessage_Shown_94 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_95 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_95 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_95 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_95 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_95 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_95 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_95 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_98 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_98;

	private bool logic_uScript_FinishEncounter_Out_98 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_100 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_100;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_100;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_100;

	private bool logic_uScript_AddMessage_Out_100 = true;

	private bool logic_uScript_AddMessage_Shown_100 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_106 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_106;

	private bool logic_uScriptCon_CompareBool_True_106 = true;

	private bool logic_uScriptCon_CompareBool_False_106 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_108 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_108;

	private bool logic_uScriptAct_SetBool_Out_108 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_108 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_108 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_109 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_109;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_109;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_109;

	private bool logic_uScript_AddMessage_Out_109 = true;

	private bool logic_uScript_AddMessage_Shown_109 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_111;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_111;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_114 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_114 = new Tank[0];

	private int logic_uScript_AccessListTech_index_114;

	private Tank logic_uScript_AccessListTech_value_114;

	private bool logic_uScript_AccessListTech_Out_114 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_115;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_119 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_119;

	private bool logic_uScriptAct_SetBool_Out_119 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_119 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_119 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_121 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_121;

	private object logic_uScript_SetEncounterTarget_visibleObject_121 = "";

	private bool logic_uScript_SetEncounterTarget_Out_121 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_124 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_124;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_124;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_124;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_124;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_124;

	private bool logic_uScript_FlyTechUpAndAway_Out_124 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_130;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_132 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_132;

	private bool logic_uScriptCon_CompareBool_True_132 = true;

	private bool logic_uScriptCon_CompareBool_False_132 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_134 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_134 = new Tank[0];

	private int logic_uScript_AccessListTech_index_134 = 1;

	private Tank logic_uScript_AccessListTech_value_134;

	private bool logic_uScript_AccessListTech_Out_134 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_137 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_137;

	private bool logic_uScript_RemoveTech_Out_137 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_139 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_139;

	private bool logic_uScript_RemoveTech_Out_139 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_140 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_140 = new Tank[0];

	private int logic_uScript_AccessListTech_index_140 = 2;

	private Tank logic_uScript_AccessListTech_value_140;

	private bool logic_uScript_AccessListTech_Out_140 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_143;

	private bool logic_uScriptCon_CompareBool_True_143 = true;

	private bool logic_uScriptCon_CompareBool_False_143 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_144 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_144;

	private bool logic_uScriptCon_CompareBool_True_144 = true;

	private bool logic_uScriptCon_CompareBool_False_144 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_146 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_146 = new Tank[0];

	private int logic_uScript_AccessListTech_index_146 = 3;

	private Tank logic_uScript_AccessListTech_value_146;

	private bool logic_uScript_AccessListTech_Out_146 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_147 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_147;

	private bool logic_uScriptCon_CompareBool_True_147 = true;

	private bool logic_uScriptCon_CompareBool_False_147 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_150 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_150;

	private bool logic_uScript_RemoveTech_Out_150 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_152 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_152;

	private bool logic_uScriptCon_CompareBool_True_152 = true;

	private bool logic_uScriptCon_CompareBool_False_152 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_154 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_154;

	private bool logic_uScript_RemoveTech_Out_154 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_155 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_155 = new Tank[0];

	private int logic_uScript_AccessListTech_index_155 = 4;

	private Tank logic_uScript_AccessListTech_value_155;

	private bool logic_uScript_AccessListTech_Out_155 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_158 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_158;

	private bool logic_uScript_RemoveTech_Out_158 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_159;

	private bool logic_uScriptCon_CompareBool_True_159 = true;

	private bool logic_uScriptCon_CompareBool_False_159 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_160 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_160 = new Tank[0];

	private int logic_uScript_AccessListTech_index_160 = 5;

	private Tank logic_uScript_AccessListTech_value_160;

	private bool logic_uScript_AccessListTech_Out_160 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_163 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_163;

	private bool logic_uScriptCon_CompareBool_True_163 = true;

	private bool logic_uScriptCon_CompareBool_False_163 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_173 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_173 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_175 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_175 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_175;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_175 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_175;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_175 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_175 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_175 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_175 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_176 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_176 = new Tank[0];

	private int logic_uScript_AccessListTech_index_176;

	private Tank logic_uScript_AccessListTech_value_176;

	private bool logic_uScript_AccessListTech_Out_176 = true;

	private SubGraph_RR_TeslaBase_ForcefieldController logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_177 = new SubGraph_RR_TeslaBase_ForcefieldController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_177 = new SpawnTechData[0];

	private Tank[] logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_177 = new Tank[0];

	private SubGraph_RR_TeslaBase_ForcefieldController logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_180 = new SubGraph_RR_TeslaBase_ForcefieldController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_180 = new SpawnTechData[0];

	private Tank[] logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_180 = new Tank[0];

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_184 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_184;

	private bool logic_uScript_RemoveTech_Out_184 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_185 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_185 = new Tank[0];

	private int logic_uScript_AccessListTech_index_185;

	private Tank logic_uScript_AccessListTech_value_185;

	private bool logic_uScript_AccessListTech_Out_185 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_188 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_188;

	private int logic_uScriptAct_AddInt_v2_B_188 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_188;

	private float logic_uScriptAct_AddInt_v2_FloatResult_188;

	private bool logic_uScriptAct_AddInt_v2_Out_188 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_190 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_190;

	private int logic_uScriptAct_AddInt_v2_B_190 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_190;

	private float logic_uScriptAct_AddInt_v2_FloatResult_190;

	private bool logic_uScriptAct_AddInt_v2_Out_190 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_192 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_192;

	private int logic_uScriptAct_AddInt_v2_B_192 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_192;

	private float logic_uScriptAct_AddInt_v2_FloatResult_192;

	private bool logic_uScriptAct_AddInt_v2_Out_192 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_195 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_195 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_196 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_196 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_197 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_197 = true;

	private uScript_ForEachListTech logic_uScript_ForEachListTech_uScript_ForEachListTech_198 = new uScript_ForEachListTech();

	private Tank[] logic_uScript_ForEachListTech_List_198 = new Tank[0];

	private Tank logic_uScript_ForEachListTech_Value_198;

	private int logic_uScript_ForEachListTech_currentIndex_198;

	private bool logic_uScript_ForEachListTech_Immediate_198 = true;

	private bool logic_uScript_ForEachListTech_Done_198 = true;

	private bool logic_uScript_ForEachListTech_Iteration_198 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_199 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_199 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_201 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_201;

	private bool logic_uScript_RemoveTech_Out_201 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_204;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_204 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_204 = "Init";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_206;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_207 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_207;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_207 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_207 = "CurrentObjective";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_210 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_210;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_210 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_210 = "Objective02SubStage";

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_217 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_217 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_217;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_217;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_217;

	private bool logic_uScript_SpawnTechsFromData_Out_217 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_218 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_218 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_218;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_218;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_218;

	private bool logic_uScript_SpawnTechsFromData_Out_218 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_219 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_219 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_219;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_219;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_219;

	private bool logic_uScript_SpawnTechsFromData_Out_219 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_220 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_220 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_220;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_220;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_220;

	private bool logic_uScript_SpawnTechsFromData_Out_220 = true;

	private SubGraph_RR_TeslaBase_BlasterTurretController logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_225 = new SubGraph_RR_TeslaBase_BlasterTurretController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_BlasterTurretController_chargerSpawnData_225 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretSpawnData_225 = new SpawnTechData[0];

	private BlockTypes logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretShieldBlockType_225;

	private string[] logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretAlertZones_225 = new string[0];

	private bool logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretsDestroyed_225;

	private SubGraph_RR_TeslaBase_BlasterTurretController logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_231 = new SubGraph_RR_TeslaBase_BlasterTurretController();

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_BlasterTurretController_chargerSpawnData_231 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretSpawnData_231 = new SpawnTechData[0];

	private BlockTypes logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretShieldBlockType_231;

	private string[] logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretAlertZones_231 = new string[0];

	private bool logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretsDestroyed_231;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_243 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_243;

	private bool logic_uScriptCon_CompareBool_True_243 = true;

	private bool logic_uScriptCon_CompareBool_False_243 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_245 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_245;

	private bool logic_uScriptCon_CompareBool_True_245 = true;

	private bool logic_uScriptCon_CompareBool_False_245 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_253 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_253 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_253;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_253;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_253;

	private bool logic_uScript_SpawnTechsFromData_Out_253 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_257 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_257 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_257;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_257;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_257;

	private bool logic_uScript_SpawnTechsFromData_Out_257 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_260 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_260 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_260 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_262 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_262 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_262 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_264 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_264 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_264 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_266 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_266 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_266 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_269 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_269;

	private object logic_uScript_SetEncounterTarget_visibleObject_269 = "";

	private bool logic_uScript_SetEncounterTarget_Out_269 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_274 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_274;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_274;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_274;

	private bool logic_uScript_AddMessage_Out_274 = true;

	private bool logic_uScript_AddMessage_Shown_274 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_278 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_278;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_278;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_278;

	private bool logic_uScript_AddMessage_Out_278 = true;

	private bool logic_uScript_AddMessage_Shown_278 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_281 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_281;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_281;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_281;

	private bool logic_uScript_AddMessage_Out_281 = true;

	private bool logic_uScript_AddMessage_Shown_281 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_282 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_282 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_282;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_282;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_282;

	private bool logic_uScript_SpawnTechsFromData_Out_282 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_288 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_288 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_288;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_288;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_288;

	private bool logic_uScript_SpawnTechsFromData_Out_288 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_290 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_290 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_290;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_290;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_290;

	private bool logic_uScript_SpawnTechsFromData_Out_290 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_293 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_293 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_294 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_294;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_294 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_294 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_294 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_296 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_296;

	private int logic_uScriptAct_AddInt_v2_B_296 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_296;

	private float logic_uScriptAct_AddInt_v2_FloatResult_296;

	private bool logic_uScriptAct_AddInt_v2_Out_296 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_298 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_298;

	private int logic_uScriptAct_AddInt_v2_B_298 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_298;

	private float logic_uScriptAct_AddInt_v2_FloatResult_298;

	private bool logic_uScriptAct_AddInt_v2_Out_298 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_300 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_300;

	private int logic_uScriptAct_AddInt_v2_B_300 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_300;

	private float logic_uScriptAct_AddInt_v2_FloatResult_300;

	private bool logic_uScriptAct_AddInt_v2_Out_300 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_302 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_302 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_302;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_302 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_302;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_302 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_302 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_302 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_302 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_306 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_306;

	private bool logic_uScriptCon_CompareBool_True_306 = true;

	private bool logic_uScriptCon_CompareBool_False_306 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_309 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_309;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_309;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_309;

	private bool logic_uScript_AddMessage_Out_309 = true;

	private bool logic_uScript_AddMessage_Shown_309 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_310 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_310;

	private bool logic_uScriptAct_SetBool_Out_310 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_310 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_310 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_312;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_312 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_312 = "msgTeslaBatteryCharge";

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_314 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_314;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_314 = true;

	private uScript_PlayerInRange logic_uScript_PlayerInRange_uScript_PlayerInRange_317 = new uScript_PlayerInRange();

	private Vector3 logic_uScript_PlayerInRange_position_317;

	private float logic_uScript_PlayerInRange_range_317;

	private bool logic_uScript_PlayerInRange_True_317 = true;

	private bool logic_uScript_PlayerInRange_False_317 = true;

	private bool logic_uScript_PlayerInRange_Out_317 = true;

	private uScript_GetPositionInEncounter logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_318 = new uScript_GetPositionInEncounter();

	private GameObject logic_uScript_GetPositionInEncounter_ownerNode_318;

	private string logic_uScript_GetPositionInEncounter_posName_318 = "";

	private Vector3 logic_uScript_GetPositionInEncounter_Return_318;

	private bool logic_uScript_GetPositionInEncounter_Out_318 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_324 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_324;

	private bool logic_uScriptCon_CompareBool_True_324 = true;

	private bool logic_uScriptCon_CompareBool_False_324 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_326 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_326;

	private bool logic_uScriptAct_SetBool_Out_326 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_326 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_326 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_327;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_327 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_327 = "Init_EnemyTurrets";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_329 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_329 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_331 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_331 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_332 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_332;

	private float logic_uScript_IsPlayerInRangeOfTech_range_332 = 350f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_332 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_332 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_332 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_332 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_335;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_335 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_335 = "MsgIntroShown";

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
		if (null == owner_Connection_7 || !m_RegisteredForEvents)
		{
			owner_Connection_7 = parentGameObject;
		}
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
		}
		if (null == owner_Connection_13 || !m_RegisteredForEvents)
		{
			owner_Connection_13 = parentGameObject;
		}
		if (null == owner_Connection_17 || !m_RegisteredForEvents)
		{
			owner_Connection_17 = parentGameObject;
		}
		if (null == owner_Connection_20 || !m_RegisteredForEvents)
		{
			owner_Connection_20 = parentGameObject;
		}
		if (null == owner_Connection_30 || !m_RegisteredForEvents)
		{
			owner_Connection_30 = parentGameObject;
		}
		if (null == owner_Connection_69 || !m_RegisteredForEvents)
		{
			owner_Connection_69 = parentGameObject;
		}
		if (null == owner_Connection_88 || !m_RegisteredForEvents)
		{
			owner_Connection_88 = parentGameObject;
		}
		if (null == owner_Connection_118 || !m_RegisteredForEvents)
		{
			owner_Connection_118 = parentGameObject;
		}
		if (null == owner_Connection_126 || !m_RegisteredForEvents)
		{
			owner_Connection_126 = parentGameObject;
		}
		if (null == owner_Connection_129 || !m_RegisteredForEvents)
		{
			owner_Connection_129 = parentGameObject;
		}
		if (null == owner_Connection_171 || !m_RegisteredForEvents)
		{
			owner_Connection_171 = parentGameObject;
		}
		if (null == owner_Connection_205 || !m_RegisteredForEvents)
		{
			owner_Connection_205 = parentGameObject;
			if (null != owner_Connection_205)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_205.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_205.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_211;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_211;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_211;
				}
			}
		}
		if (null == owner_Connection_214 || !m_RegisteredForEvents)
		{
			owner_Connection_214 = parentGameObject;
		}
		if (null == owner_Connection_215 || !m_RegisteredForEvents)
		{
			owner_Connection_215 = parentGameObject;
		}
		if (null == owner_Connection_221 || !m_RegisteredForEvents)
		{
			owner_Connection_221 = parentGameObject;
		}
		if (null == owner_Connection_224 || !m_RegisteredForEvents)
		{
			owner_Connection_224 = parentGameObject;
		}
		if (null == owner_Connection_254 || !m_RegisteredForEvents)
		{
			owner_Connection_254 = parentGameObject;
		}
		if (null == owner_Connection_256 || !m_RegisteredForEvents)
		{
			owner_Connection_256 = parentGameObject;
		}
		if (null == owner_Connection_267 || !m_RegisteredForEvents)
		{
			owner_Connection_267 = parentGameObject;
		}
		if (null == owner_Connection_283 || !m_RegisteredForEvents)
		{
			owner_Connection_283 = parentGameObject;
		}
		if (null == owner_Connection_289 || !m_RegisteredForEvents)
		{
			owner_Connection_289 = parentGameObject;
		}
		if (null == owner_Connection_292 || !m_RegisteredForEvents)
		{
			owner_Connection_292 = parentGameObject;
		}
		if (null == owner_Connection_295 || !m_RegisteredForEvents)
		{
			owner_Connection_295 = parentGameObject;
		}
		if (null == owner_Connection_303 || !m_RegisteredForEvents)
		{
			owner_Connection_303 = parentGameObject;
		}
		if (null == owner_Connection_315 || !m_RegisteredForEvents)
		{
			owner_Connection_315 = parentGameObject;
		}
		if (null == owner_Connection_319 || !m_RegisteredForEvents)
		{
			owner_Connection_319 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_205)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_205.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_205.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_211;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_211;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_211;
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
		if (null != owner_Connection_205)
		{
			uScript_SaveLoad component2 = owner_Connection_205.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_211;
				component2.LoadEvent -= Instance_LoadEvent_211;
				component2.RestartEvent -= Instance_RestartEvent_211;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_3.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_5.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_10.SetParent(g);
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_11.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_14.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_19.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_21.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_29.SetParent(g);
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_31.SetParent(g);
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_36.SetParent(g);
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_43.SetParent(g);
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_51.SetParent(g);
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_54.SetParent(g);
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_61.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_67.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_71.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_73.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_74.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_76.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_81.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_82.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_83.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_92.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_93.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_94.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_95.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_98.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_100.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_106.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_109.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_114.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_119.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_121.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_124.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_132.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_134.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_137.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_139.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_140.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_144.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_146.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_147.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_150.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_152.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_154.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_155.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_158.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_160.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_163.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_173.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_175.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_176.SetParent(g);
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_177.SetParent(g);
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_180.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_184.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_185.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_188.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_190.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_192.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_195.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_196.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_197.SetParent(g);
		logic_uScript_ForEachListTech_uScript_ForEachListTech_198.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_199.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_201.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_217.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_218.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_219.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_220.SetParent(g);
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_225.SetParent(g);
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_231.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_243.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_245.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_253.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_257.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_260.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_262.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_264.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_266.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_269.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_274.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_278.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_281.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_282.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_288.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_290.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_293.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_294.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_296.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_298.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_300.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_302.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_306.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_309.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_310.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_314.SetParent(g);
		logic_uScript_PlayerInRange_uScript_PlayerInRange_317.SetParent(g);
		logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_318.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_324.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_326.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_329.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_331.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_332.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_7 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_13 = parentGameObject;
		owner_Connection_17 = parentGameObject;
		owner_Connection_20 = parentGameObject;
		owner_Connection_30 = parentGameObject;
		owner_Connection_69 = parentGameObject;
		owner_Connection_88 = parentGameObject;
		owner_Connection_118 = parentGameObject;
		owner_Connection_126 = parentGameObject;
		owner_Connection_129 = parentGameObject;
		owner_Connection_171 = parentGameObject;
		owner_Connection_205 = parentGameObject;
		owner_Connection_214 = parentGameObject;
		owner_Connection_215 = parentGameObject;
		owner_Connection_221 = parentGameObject;
		owner_Connection_224 = parentGameObject;
		owner_Connection_254 = parentGameObject;
		owner_Connection_256 = parentGameObject;
		owner_Connection_267 = parentGameObject;
		owner_Connection_283 = parentGameObject;
		owner_Connection_289 = parentGameObject;
		owner_Connection_292 = parentGameObject;
		owner_Connection_295 = parentGameObject;
		owner_Connection_303 = parentGameObject;
		owner_Connection_315 = parentGameObject;
		owner_Connection_319 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_11.Awake();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_31.Awake();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_36.Awake();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_43.Awake();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_51.Awake();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_54.Awake();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_61.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_76.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.Awake();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_177.Awake();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_180.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Awake();
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_225.Awake();
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_231.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.Awake();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_11.Out += SubGraph_RR_LaserLab_TurretAndChargerController_Out_11;
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_31.Out += SubGraph_RR_TeslaBase_TeslaTurretController_Out_31;
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_36.Out += SubGraph_RR_TeslaBase_TeslaTurretController_Out_36;
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_43.Out += SubGraph_RR_TeslaBase_TeslaTurretController_Out_43;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_51.Out += SubGraph_RR_LaserLab_TurretAndChargerController_Out_51;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_54.Out += SubGraph_RR_LaserLab_TurretAndChargerController_Out_54;
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_61.Out += SubGraph_RR_TeslaBase_TeslaTurretController_Out_61;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_76.Out += SubGraph_CompleteObjectiveStage_Out_76;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.Out += SubGraph_CompleteObjectiveStage_Out_111;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output1 += uScriptCon_ManualSwitch_Output1_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output2 += uScriptCon_ManualSwitch_Output2_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output3 += uScriptCon_ManualSwitch_Output3_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output4 += uScriptCon_ManualSwitch_Output4_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output5 += uScriptCon_ManualSwitch_Output5_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output6 += uScriptCon_ManualSwitch_Output6_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output7 += uScriptCon_ManualSwitch_Output7_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output8 += uScriptCon_ManualSwitch_Output8_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.Output1 += uScriptCon_ManualSwitch_Output1_130;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.Output2 += uScriptCon_ManualSwitch_Output2_130;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.Output3 += uScriptCon_ManualSwitch_Output3_130;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.Output4 += uScriptCon_ManualSwitch_Output4_130;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.Output5 += uScriptCon_ManualSwitch_Output5_130;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.Output6 += uScriptCon_ManualSwitch_Output6_130;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.Output7 += uScriptCon_ManualSwitch_Output7_130;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.Output8 += uScriptCon_ManualSwitch_Output8_130;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_177.Out += SubGraph_RR_TeslaBase_ForcefieldController_Out_177;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_180.Out += SubGraph_RR_TeslaBase_ForcefieldController_Out_180;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Save_Out += SubGraph_SaveLoadBool_Save_Out_204;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Load_Out += SubGraph_SaveLoadBool_Load_Out_204;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_204;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.Out += SubGraph_LoadObjectiveStates_Out_206;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207.Save_Out += SubGraph_SaveLoadInt_Save_Out_207;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207.Load_Out += SubGraph_SaveLoadInt_Load_Out_207;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_207;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Save_Out += SubGraph_SaveLoadInt_Save_Out_210;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Load_Out += SubGraph_SaveLoadInt_Load_Out_210;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_210;
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_225.Out += SubGraph_RR_TeslaBase_BlasterTurretController_Out_225;
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_231.Out += SubGraph_RR_TeslaBase_BlasterTurretController_Out_231;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.Save_Out += SubGraph_SaveLoadBool_Save_Out_312;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.Load_Out += SubGraph_SaveLoadBool_Load_Out_312;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_312;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Save_Out += SubGraph_SaveLoadBool_Save_Out_327;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Load_Out += SubGraph_SaveLoadBool_Load_Out_327;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_327;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.Save_Out += SubGraph_SaveLoadBool_Save_Out_335;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.Load_Out += SubGraph_SaveLoadBool_Load_Out_335;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_335;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_11.Start();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_31.Start();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_36.Start();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_43.Start();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_51.Start();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_54.Start();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_61.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_76.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.Start();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_177.Start();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_180.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Start();
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_225.Start();
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_231.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_11.OnEnable();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_31.OnEnable();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_36.OnEnable();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_43.OnEnable();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_51.OnEnable();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_54.OnEnable();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_61.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_76.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_92.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_124.OnEnable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_177.OnEnable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_180.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.OnEnable();
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_225.OnEnable();
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_231.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_314.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_11.OnDisable();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_31.OnDisable();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_36.OnDisable();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_43.OnDisable();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_51.OnDisable();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_54.OnDisable();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_61.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_76.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_94.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_100.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_109.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.OnDisable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_177.OnDisable();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_180.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.OnDisable();
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_225.OnDisable();
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_231.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_274.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_278.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_281.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_294.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_309.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_332.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		if (m_ContinueExecution != null)
		{
			ContinueExecution continueExecution = m_ContinueExecution;
			m_ContinueExecution = null;
			m_Breakpoint = false;
			continueExecution();
			return;
		}
		UpdateEditorValues();
		SyncEventListeners();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_11.Update();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_31.Update();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_36.Update();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_43.Update();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_51.Update();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_54.Update();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_61.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_76.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.Update();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_177.Update();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_180.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Update();
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_225.Update();
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_231.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_11.OnDestroy();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_31.OnDestroy();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_36.OnDestroy();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_43.OnDestroy();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_51.OnDestroy();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_54.OnDestroy();
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_61.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_76.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.OnDestroy();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_177.OnDestroy();
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_180.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.OnDestroy();
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_225.OnDestroy();
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_231.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.OnDestroy();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_11.Out -= SubGraph_RR_LaserLab_TurretAndChargerController_Out_11;
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_31.Out -= SubGraph_RR_TeslaBase_TeslaTurretController_Out_31;
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_36.Out -= SubGraph_RR_TeslaBase_TeslaTurretController_Out_36;
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_43.Out -= SubGraph_RR_TeslaBase_TeslaTurretController_Out_43;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_51.Out -= SubGraph_RR_LaserLab_TurretAndChargerController_Out_51;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_54.Out -= SubGraph_RR_LaserLab_TurretAndChargerController_Out_54;
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_61.Out -= SubGraph_RR_TeslaBase_TeslaTurretController_Out_61;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_76.Out -= SubGraph_CompleteObjectiveStage_Out_76;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.Out -= SubGraph_CompleteObjectiveStage_Out_111;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output1 -= uScriptCon_ManualSwitch_Output1_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output2 -= uScriptCon_ManualSwitch_Output2_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output3 -= uScriptCon_ManualSwitch_Output3_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output4 -= uScriptCon_ManualSwitch_Output4_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output5 -= uScriptCon_ManualSwitch_Output5_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output6 -= uScriptCon_ManualSwitch_Output6_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output7 -= uScriptCon_ManualSwitch_Output7_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output8 -= uScriptCon_ManualSwitch_Output8_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.Output1 -= uScriptCon_ManualSwitch_Output1_130;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.Output2 -= uScriptCon_ManualSwitch_Output2_130;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.Output3 -= uScriptCon_ManualSwitch_Output3_130;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.Output4 -= uScriptCon_ManualSwitch_Output4_130;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.Output5 -= uScriptCon_ManualSwitch_Output5_130;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.Output6 -= uScriptCon_ManualSwitch_Output6_130;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.Output7 -= uScriptCon_ManualSwitch_Output7_130;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.Output8 -= uScriptCon_ManualSwitch_Output8_130;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_177.Out -= SubGraph_RR_TeslaBase_ForcefieldController_Out_177;
		logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_180.Out -= SubGraph_RR_TeslaBase_ForcefieldController_Out_180;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Save_Out -= SubGraph_SaveLoadBool_Save_Out_204;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Load_Out -= SubGraph_SaveLoadBool_Load_Out_204;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_204;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.Out -= SubGraph_LoadObjectiveStates_Out_206;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207.Save_Out -= SubGraph_SaveLoadInt_Save_Out_207;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207.Load_Out -= SubGraph_SaveLoadInt_Load_Out_207;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_207;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Save_Out -= SubGraph_SaveLoadInt_Save_Out_210;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Load_Out -= SubGraph_SaveLoadInt_Load_Out_210;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_210;
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_225.Out -= SubGraph_RR_TeslaBase_BlasterTurretController_Out_225;
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_231.Out -= SubGraph_RR_TeslaBase_BlasterTurretController_Out_231;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.Save_Out -= SubGraph_SaveLoadBool_Save_Out_312;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.Load_Out -= SubGraph_SaveLoadBool_Load_Out_312;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_312;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Save_Out -= SubGraph_SaveLoadBool_Save_Out_327;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Load_Out -= SubGraph_SaveLoadBool_Load_Out_327;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_327;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.Save_Out -= SubGraph_SaveLoadBool_Save_Out_335;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.Load_Out -= SubGraph_SaveLoadBool_Load_Out_335;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_335;
	}

	private void Instance_OnUpdate_0(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_OnUpdate_0();
	}

	private void Instance_OnSuspend_0(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_OnSuspend_0();
	}

	private void Instance_OnResume_0(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_OnResume_0();
	}

	private void Instance_SaveEvent_211(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_SaveEvent_211();
	}

	private void Instance_LoadEvent_211(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_LoadEvent_211();
	}

	private void Instance_RestartEvent_211(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_RestartEvent_211();
	}

	private void SubGraph_RR_LaserLab_TurretAndChargerController_Out_11(object o, SubGraph_RR_LaserLab_TurretAndChargerController.LogicEventArgs e)
	{
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_11 = e.turretsDestroyed;
		local_LaserTurrets02Destroyed_System_Boolean = logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_11;
		Relay_Out_11();
	}

	private void SubGraph_RR_TeslaBase_TeslaTurretController_Out_31(object o, SubGraph_RR_TeslaBase_TeslaTurretController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretsDestroyed_31 = e.turretsDestroyed;
		local_TeslaTurrets01Destroyed_System_Boolean = logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretsDestroyed_31;
		Relay_Out_31();
	}

	private void SubGraph_RR_TeslaBase_TeslaTurretController_Out_36(object o, SubGraph_RR_TeslaBase_TeslaTurretController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretsDestroyed_36 = e.turretsDestroyed;
		local_TeslaTurrets02Destroyed_System_Boolean = logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretsDestroyed_36;
		Relay_Out_36();
	}

	private void SubGraph_RR_TeslaBase_TeslaTurretController_Out_43(object o, SubGraph_RR_TeslaBase_TeslaTurretController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretsDestroyed_43 = e.turretsDestroyed;
		local_TeslaTurrets03Destroyed_System_Boolean = logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretsDestroyed_43;
		Relay_Out_43();
	}

	private void SubGraph_RR_LaserLab_TurretAndChargerController_Out_51(object o, SubGraph_RR_LaserLab_TurretAndChargerController.LogicEventArgs e)
	{
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_51 = e.turretsDestroyed;
		local_LaserTurrets03Destroyed_System_Boolean = logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_51;
		Relay_Out_51();
	}

	private void SubGraph_RR_LaserLab_TurretAndChargerController_Out_54(object o, SubGraph_RR_LaserLab_TurretAndChargerController.LogicEventArgs e)
	{
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_54 = e.turretsDestroyed;
		local_LaserTurrets04Destroyed_System_Boolean = logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_54;
		Relay_Out_54();
	}

	private void SubGraph_RR_TeslaBase_TeslaTurretController_Out_61(object o, SubGraph_RR_TeslaBase_TeslaTurretController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretsDestroyed_61 = e.turretsDestroyed;
		local_TeslaTurrets04Destroyed_System_Boolean = logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretsDestroyed_61;
		Relay_Out_61();
	}

	private void SubGraph_CompleteObjectiveStage_Out_76(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_76 = e.objectiveStage;
		local_CurrentObjective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_76;
		Relay_Out_76();
	}

	private void SubGraph_CompleteObjectiveStage_Out_111(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_111 = e.objectiveStage;
		local_CurrentObjective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_111;
		Relay_Out_111();
	}

	private void uScriptCon_ManualSwitch_Output1_115(object o, EventArgs e)
	{
		Relay_Output1_115();
	}

	private void uScriptCon_ManualSwitch_Output2_115(object o, EventArgs e)
	{
		Relay_Output2_115();
	}

	private void uScriptCon_ManualSwitch_Output3_115(object o, EventArgs e)
	{
		Relay_Output3_115();
	}

	private void uScriptCon_ManualSwitch_Output4_115(object o, EventArgs e)
	{
		Relay_Output4_115();
	}

	private void uScriptCon_ManualSwitch_Output5_115(object o, EventArgs e)
	{
		Relay_Output5_115();
	}

	private void uScriptCon_ManualSwitch_Output6_115(object o, EventArgs e)
	{
		Relay_Output6_115();
	}

	private void uScriptCon_ManualSwitch_Output7_115(object o, EventArgs e)
	{
		Relay_Output7_115();
	}

	private void uScriptCon_ManualSwitch_Output8_115(object o, EventArgs e)
	{
		Relay_Output8_115();
	}

	private void uScriptCon_ManualSwitch_Output1_130(object o, EventArgs e)
	{
		Relay_Output1_130();
	}

	private void uScriptCon_ManualSwitch_Output2_130(object o, EventArgs e)
	{
		Relay_Output2_130();
	}

	private void uScriptCon_ManualSwitch_Output3_130(object o, EventArgs e)
	{
		Relay_Output3_130();
	}

	private void uScriptCon_ManualSwitch_Output4_130(object o, EventArgs e)
	{
		Relay_Output4_130();
	}

	private void uScriptCon_ManualSwitch_Output5_130(object o, EventArgs e)
	{
		Relay_Output5_130();
	}

	private void uScriptCon_ManualSwitch_Output6_130(object o, EventArgs e)
	{
		Relay_Output6_130();
	}

	private void uScriptCon_ManualSwitch_Output7_130(object o, EventArgs e)
	{
		Relay_Output7_130();
	}

	private void uScriptCon_ManualSwitch_Output8_130(object o, EventArgs e)
	{
		Relay_Output8_130();
	}

	private void SubGraph_RR_TeslaBase_ForcefieldController_Out_177(object o, SubGraph_RR_TeslaBase_ForcefieldController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_177 = e.Forcefields;
		local_ForcefieldGates_TankArray = logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_177;
		Relay_Out_177();
	}

	private void SubGraph_RR_TeslaBase_ForcefieldController_Out_180(object o, SubGraph_RR_TeslaBase_ForcefieldController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_180 = e.Forcefields;
		local_Room04Forcefields_TankArray = logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_180;
		Relay_Out_180();
	}

	private void SubGraph_SaveLoadBool_Save_Out_204(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_204;
		Relay_Save_Out_204();
	}

	private void SubGraph_SaveLoadBool_Load_Out_204(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_204;
		Relay_Load_Out_204();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_204(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_204 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_204;
		Relay_Restart_Out_204();
	}

	private void SubGraph_LoadObjectiveStates_Out_206(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_206();
	}

	private void SubGraph_SaveLoadInt_Save_Out_207(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_207 = e.integer;
		local_CurrentObjective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_207;
		Relay_Save_Out_207();
	}

	private void SubGraph_SaveLoadInt_Load_Out_207(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_207 = e.integer;
		local_CurrentObjective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_207;
		Relay_Load_Out_207();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_207(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_207 = e.integer;
		local_CurrentObjective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_207;
		Relay_Restart_Out_207();
	}

	private void SubGraph_SaveLoadInt_Save_Out_210(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_210 = e.integer;
		local_Objective02SubStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_210;
		Relay_Save_Out_210();
	}

	private void SubGraph_SaveLoadInt_Load_Out_210(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_210 = e.integer;
		local_Objective02SubStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_210;
		Relay_Load_Out_210();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_210(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_210 = e.integer;
		local_Objective02SubStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_210;
		Relay_Restart_Out_210();
	}

	private void SubGraph_RR_TeslaBase_BlasterTurretController_Out_225(object o, SubGraph_RR_TeslaBase_BlasterTurretController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretsDestroyed_225 = e.turretsDestroyed;
		local_BlasterTurrets03Destroyed_System_Boolean = logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretsDestroyed_225;
		Relay_Out_225();
	}

	private void SubGraph_RR_TeslaBase_BlasterTurretController_Out_231(object o, SubGraph_RR_TeslaBase_BlasterTurretController.LogicEventArgs e)
	{
		logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretsDestroyed_231 = e.turretsDestroyed;
		local_BlasterTurrets04Destroyed_System_Boolean = logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretsDestroyed_231;
		Relay_Out_231();
	}

	private void SubGraph_SaveLoadBool_Save_Out_312(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_312 = e.boolean;
		local_msgTeslaBatteryCharge_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_312;
		Relay_Save_Out_312();
	}

	private void SubGraph_SaveLoadBool_Load_Out_312(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_312 = e.boolean;
		local_msgTeslaBatteryCharge_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_312;
		Relay_Load_Out_312();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_312(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_312 = e.boolean;
		local_msgTeslaBatteryCharge_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_312;
		Relay_Restart_Out_312();
	}

	private void SubGraph_SaveLoadBool_Save_Out_327(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = e.boolean;
		local_Init_EnemyTurrets_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_327;
		Relay_Save_Out_327();
	}

	private void SubGraph_SaveLoadBool_Load_Out_327(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = e.boolean;
		local_Init_EnemyTurrets_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_327;
		Relay_Load_Out_327();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_327(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_327 = e.boolean;
		local_Init_EnemyTurrets_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_327;
		Relay_Restart_Out_327();
	}

	private void SubGraph_SaveLoadBool_Save_Out_335(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_335 = e.boolean;
		local_MsgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_335;
		Relay_Save_Out_335();
	}

	private void SubGraph_SaveLoadBool_Load_Out_335(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_335 = e.boolean;
		local_MsgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_335;
		Relay_Load_Out_335();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_335(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_335 = e.boolean;
		local_MsgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_335;
		Relay_Restart_Out_335();
	}

	private void Relay_OnUpdate_0()
	{
		if (!CheckDebugBreak("f8859a6d-1c87-4e87-b3ba-e44280fde163", "Encounter_Update", Relay_OnUpdate_0))
		{
			Relay_In_2();
			Relay_In_324();
		}
	}

	private void Relay_OnSuspend_0()
	{
		CheckDebugBreak("f8859a6d-1c87-4e87-b3ba-e44280fde163", "Encounter_Update", Relay_OnSuspend_0);
	}

	private void Relay_OnResume_0()
	{
		CheckDebugBreak("f8859a6d-1c87-4e87-b3ba-e44280fde163", "Encounter_Update", Relay_OnResume_0);
	}

	private void Relay_In_2()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("74f92039-1d9e-423c-b764-e062e1f81b1e", "Compare_Bool", Relay_In_2))
			{
				logic_uScriptCon_CompareBool_Bool_2 = local_Init_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.In(logic_uScriptCon_CompareBool_Bool_2);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_2.False;
				if (num)
				{
					Relay_In_314();
				}
				if (flag)
				{
					Relay_InitialSpawn_5();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_3()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e9f3b84d-dc2e-4143-837a-49742ae89086", "Set_Bool", Relay_True_3))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_3.True(out logic_uScriptAct_SetBool_Target_3);
				local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_3;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_3.Out)
				{
					Relay_In_314();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_3()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e9f3b84d-dc2e-4143-837a-49742ae89086", "Set_Bool", Relay_False_3))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_3.False(out logic_uScriptAct_SetBool_Target_3);
				local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_3;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_3.Out)
				{
					Relay_In_314();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_5()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("3ef0c7e1-56fe-410b-b449-c7a5d21820d6", "uScript_SpawnTechsFromData", Relay_InitialSpawn_5))
			{
				int num = 0;
				Array nPCSpawnData = NPCSpawnData01;
				if (logic_uScript_SpawnTechsFromData_spawnData_5.Length != num + nPCSpawnData.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_5, num + nPCSpawnData.Length);
				}
				Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_5, num, nPCSpawnData.Length);
				num += nPCSpawnData.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_5 = owner_Connection_7;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_5.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_5, logic_uScript_SpawnTechsFromData_ownerNode_5, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_5, logic_uScript_SpawnTechsFromData_allowResurrection_5);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_5.Out)
				{
					Relay_InitialSpawn_67();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_10()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bda0819b-10b6-4d13-b0be-e428b0667e96", "uScript_SpawnTechsFromData", Relay_InitialSpawn_10))
			{
				int num = 0;
				Array array = teslaTurretGroup01SpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_10.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_10, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_10, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_10 = owner_Connection_8;
				logic_uScript_SpawnTechsFromData_delayBetweenSpawns_10 = telsaTurretGroup01SpawnInterval;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_10.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_10, logic_uScript_SpawnTechsFromData_ownerNode_10, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_10, logic_uScript_SpawnTechsFromData_allowResurrection_10);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_10.Out)
				{
					Relay_InitialSpawn_14();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_11()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c1b7a20d-4bea-4a0c-80d4-793e25aec6b5", "SubGraph_RR_LaserLab_TurretAndChargerController", Relay_Out_11))
			{
				Relay_In_36();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_LaserLab_TurretAndChargerController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_11()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c1b7a20d-4bea-4a0c-80d4-793e25aec6b5", "SubGraph_RR_LaserLab_TurretAndChargerController", Relay_In_11))
			{
				int num = 0;
				Array array = shieldCharger02SpawnData;
				if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_11.Length != num + array.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_11, num + array.Length);
				}
				Array.Copy(array, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_11, num, array.Length);
				num += array.Length;
				int num2 = 0;
				Array array2 = laserTurretGroup02SpawnData;
				if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_11.Length != num2 + array2.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_11, num2 + array2.Length);
				}
				Array.Copy(array2, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_11, num2, array2.Length);
				num2 += array2.Length;
				int num3 = 0;
				Array array3 = laserTurretGroup02AnimTriggers;
				if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_11.Length != num3 + array3.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_11, num3 + array3.Length);
				}
				Array.Copy(array3, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_11, num3, array3.Length);
				num3 += array3.Length;
				int num4 = 0;
				Array array4 = laserTurretGroup02AnimDurations;
				if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_11.Length != num4 + array4.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_11, num4 + array4.Length);
				}
				Array.Copy(array4, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_11, num4, array4.Length);
				num4 += array4.Length;
				logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_11 = laserTurretWeaponBlockType;
				logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_11 = laserTurretShieldBlockType;
				logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_11.In(logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_11, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_11, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_11, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_11, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_11, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_11);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_LaserLab_TurretAndChargerController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_14()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6fc36dee-9986-48ec-bafb-8d14d603de7b", "uScript_SpawnTechsFromData", Relay_InitialSpawn_14))
			{
				int num = 0;
				Array array = shieldCharger01SpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_14.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_14, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_14, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_14 = owner_Connection_13;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_14.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_14, logic_uScript_SpawnTechsFromData_ownerNode_14, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_14, logic_uScript_SpawnTechsFromData_allowResurrection_14);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_14.Out)
				{
					Relay_True_326();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_19()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("06b64144-44ee-4b54-ae9a-ff759559f3a4", "uScript_SpawnTechsFromData", Relay_InitialSpawn_19))
			{
				int num = 0;
				Array array = shieldCharger04SpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_19.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_19, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_19, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_19 = owner_Connection_17;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_19.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_19, logic_uScript_SpawnTechsFromData_ownerNode_19, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_19, logic_uScript_SpawnTechsFromData_allowResurrection_19);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_19.Out)
				{
					Relay_In_266();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_21()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("58313651-2f71-4df0-ba90-0b9173bc2039", "uScript_SpawnTechsFromData", Relay_InitialSpawn_21))
			{
				int num = 0;
				Array array = shieldCharger03SpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_21.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_21, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_21, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_21 = owner_Connection_20;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_21.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_21, logic_uScript_SpawnTechsFromData_ownerNode_21, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_21, logic_uScript_SpawnTechsFromData_allowResurrection_21);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_21.Out)
				{
					Relay_In_264();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_29()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b5aee1da-7831-4fe7-a533-f15456ddfe05", "uScript_SpawnTechsFromData", Relay_InitialSpawn_29))
			{
				int num = 0;
				Array room04ForcefieldsSpawnData = Room04ForcefieldsSpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_29.Length != num + room04ForcefieldsSpawnData.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_29, num + room04ForcefieldsSpawnData.Length);
				}
				Array.Copy(room04ForcefieldsSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_29, num, room04ForcefieldsSpawnData.Length);
				num += room04ForcefieldsSpawnData.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_29 = owner_Connection_30;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_29.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_29, logic_uScript_SpawnTechsFromData_ownerNode_29, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_29, logic_uScript_SpawnTechsFromData_allowResurrection_29);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_29.Out)
				{
					Relay_True_3();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_31()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("04fd5c19-0c47-4d46-913b-7fb1b9f4258b", "SubGraph_RR_TeslaBase_TeslaTurretController", Relay_Out_31))
			{
				Relay_In_11();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_TeslaBase_TeslaTurretController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_31()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("04fd5c19-0c47-4d46-913b-7fb1b9f4258b", "SubGraph_RR_TeslaBase_TeslaTurretController", Relay_In_31))
			{
				int num = 0;
				Array array = shieldCharger01SpawnData;
				if (logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_31.Length != num + array.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_31, num + array.Length);
				}
				Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_31, num, array.Length);
				num += array.Length;
				int num2 = 0;
				Array array2 = teslaTurretGroup01SpawnData;
				if (logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_31.Length != num2 + array2.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_31, num2 + array2.Length);
				}
				Array.Copy(array2, 0, logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_31, num2, array2.Length);
				num2 += array2.Length;
				logic_SubGraph_RR_TeslaBase_TeslaTurretController_teslaTurretShieldBlockType_31 = teslaTurretShieldBlockType;
				logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_31.In(logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_31, logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_31, logic_SubGraph_RR_TeslaBase_TeslaTurretController_teslaTurretShieldBlockType_31);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_TeslaBase_TeslaTurretController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_36()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("15a5ae3c-2f52-4e03-9712-4b49009f84f5", "SubGraph_RR_TeslaBase_TeslaTurretController", Relay_Out_36))
			{
				Relay_In_51();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_TeslaBase_TeslaTurretController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_36()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("15a5ae3c-2f52-4e03-9712-4b49009f84f5", "SubGraph_RR_TeslaBase_TeslaTurretController", Relay_In_36))
			{
				int num = 0;
				Array array = shieldCharger02SpawnData;
				if (logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_36.Length != num + array.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_36, num + array.Length);
				}
				Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_36, num, array.Length);
				num += array.Length;
				int num2 = 0;
				Array array2 = teslaTurretGroup02SpawnData;
				if (logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_36.Length != num2 + array2.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_36, num2 + array2.Length);
				}
				Array.Copy(array2, 0, logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_36, num2, array2.Length);
				num2 += array2.Length;
				logic_SubGraph_RR_TeslaBase_TeslaTurretController_teslaTurretShieldBlockType_36 = teslaTurretShieldBlockType;
				logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_36.In(logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_36, logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_36, logic_SubGraph_RR_TeslaBase_TeslaTurretController_teslaTurretShieldBlockType_36);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_TeslaBase_TeslaTurretController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_43()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("82830d8f-60a4-4d80-9933-ed6b80df1a03", "SubGraph_RR_TeslaBase_TeslaTurretController", Relay_Out_43))
			{
				Relay_In_225();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_TeslaBase_TeslaTurretController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_43()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("82830d8f-60a4-4d80-9933-ed6b80df1a03", "SubGraph_RR_TeslaBase_TeslaTurretController", Relay_In_43))
			{
				int num = 0;
				Array array = shieldCharger03SpawnData;
				if (logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_43.Length != num + array.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_43, num + array.Length);
				}
				Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_43, num, array.Length);
				num += array.Length;
				int num2 = 0;
				Array array2 = teslaTurretGroup03SpawnData;
				if (logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_43.Length != num2 + array2.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_43, num2 + array2.Length);
				}
				Array.Copy(array2, 0, logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_43, num2, array2.Length);
				num2 += array2.Length;
				logic_SubGraph_RR_TeslaBase_TeslaTurretController_teslaTurretShieldBlockType_43 = teslaTurretShieldBlockType;
				logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_43.In(logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_43, logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_43, logic_SubGraph_RR_TeslaBase_TeslaTurretController_teslaTurretShieldBlockType_43);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_TeslaBase_TeslaTurretController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_51()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8b6bc1bb-98df-486b-92a4-9e1245914196", "SubGraph_RR_LaserLab_TurretAndChargerController", Relay_Out_51))
			{
				Relay_In_43();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_LaserLab_TurretAndChargerController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_51()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8b6bc1bb-98df-486b-92a4-9e1245914196", "SubGraph_RR_LaserLab_TurretAndChargerController", Relay_In_51))
			{
				int num = 0;
				Array array = shieldCharger03SpawnData;
				if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_51.Length != num + array.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_51, num + array.Length);
				}
				Array.Copy(array, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_51, num, array.Length);
				num += array.Length;
				int num2 = 0;
				Array array2 = laserTurretGroup03SpawnData;
				if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_51.Length != num2 + array2.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_51, num2 + array2.Length);
				}
				Array.Copy(array2, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_51, num2, array2.Length);
				num2 += array2.Length;
				int num3 = 0;
				Array array3 = laserTurretGroup03AnimTriggers;
				if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_51.Length != num3 + array3.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_51, num3 + array3.Length);
				}
				Array.Copy(array3, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_51, num3, array3.Length);
				num3 += array3.Length;
				int num4 = 0;
				Array array4 = laserTurretGroup03AnimDurations;
				if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_51.Length != num4 + array4.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_51, num4 + array4.Length);
				}
				Array.Copy(array4, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_51, num4, array4.Length);
				num4 += array4.Length;
				logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_51 = laserTurretWeaponBlockType;
				logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_51 = laserTurretShieldBlockType;
				logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_51.In(logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_51, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_51, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_51, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_51, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_51, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_51);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_LaserLab_TurretAndChargerController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_54()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("470e9e81-db91-45ab-ba64-5cf4ee2af7d9", "SubGraph_RR_LaserLab_TurretAndChargerController", Relay_Out_54))
			{
				Relay_In_61();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_LaserLab_TurretAndChargerController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_54()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("470e9e81-db91-45ab-ba64-5cf4ee2af7d9", "SubGraph_RR_LaserLab_TurretAndChargerController", Relay_In_54))
			{
				int num = 0;
				Array array = shieldCharger04SpawnData;
				if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_54.Length != num + array.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_54, num + array.Length);
				}
				Array.Copy(array, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_54, num, array.Length);
				num += array.Length;
				int num2 = 0;
				Array array2 = laserTurretGroup04SpawnData;
				if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_54.Length != num2 + array2.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_54, num2 + array2.Length);
				}
				Array.Copy(array2, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_54, num2, array2.Length);
				num2 += array2.Length;
				int num3 = 0;
				Array array3 = laserTurretGroup04AnimTriggers;
				if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_54.Length != num3 + array3.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_54, num3 + array3.Length);
				}
				Array.Copy(array3, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_54, num3, array3.Length);
				num3 += array3.Length;
				int num4 = 0;
				Array array4 = laserTurretGroup04AnimDurations;
				if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_54.Length != num4 + array4.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_54, num4 + array4.Length);
				}
				Array.Copy(array4, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_54, num4, array4.Length);
				num4 += array4.Length;
				logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_54 = laserTurretWeaponBlockType;
				logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_54 = laserTurretShieldBlockType;
				logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_54.In(logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_54, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_54, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_54, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_54, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_54, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_54);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_LaserLab_TurretAndChargerController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_61()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("1e0e33fc-1a08-4335-8cc7-51a9c81ad267", "SubGraph_RR_TeslaBase_TeslaTurretController", Relay_Out_61))
			{
				Relay_In_231();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_TeslaBase_TeslaTurretController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_61()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("1e0e33fc-1a08-4335-8cc7-51a9c81ad267", "SubGraph_RR_TeslaBase_TeslaTurretController", Relay_In_61))
			{
				int num = 0;
				Array array = shieldCharger04SpawnData;
				if (logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_61.Length != num + array.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_61, num + array.Length);
				}
				Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_61, num, array.Length);
				num += array.Length;
				int num2 = 0;
				Array array2 = teslaTurretGroup04SpawnData;
				if (logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_61.Length != num2 + array2.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_61, num2 + array2.Length);
				}
				Array.Copy(array2, 0, logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_61, num2, array2.Length);
				num2 += array2.Length;
				logic_SubGraph_RR_TeslaBase_TeslaTurretController_teslaTurretShieldBlockType_61 = teslaTurretShieldBlockType;
				logic_SubGraph_RR_TeslaBase_TeslaTurretController_SubGraph_RR_TeslaBase_TeslaTurretController_61.In(logic_SubGraph_RR_TeslaBase_TeslaTurretController_chargerSpawnData_61, logic_SubGraph_RR_TeslaBase_TeslaTurretController_turretSpawnData_61, logic_SubGraph_RR_TeslaBase_TeslaTurretController_teslaTurretShieldBlockType_61);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_TeslaBase_TeslaTurretController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_67()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2bbe1866-d14a-4299-8d33-7d84f8f71b48", "uScript_SpawnTechsFromData", Relay_InitialSpawn_67))
			{
				int num = 0;
				Array forcefieldGatesSpawnData = ForcefieldGatesSpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_67.Length != num + forcefieldGatesSpawnData.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_67, num + forcefieldGatesSpawnData.Length);
				}
				Array.Copy(forcefieldGatesSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_67, num, forcefieldGatesSpawnData.Length);
				num += forcefieldGatesSpawnData.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_67 = owner_Connection_69;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_67.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_67, logic_uScript_SpawnTechsFromData_ownerNode_67, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_67, logic_uScript_SpawnTechsFromData_allowResurrection_67);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_67.Out)
				{
					Relay_InitialSpawn_29();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_71()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ba013382-9c71-4883-a642-405a69e59d68", "Distance_Is_Player_in_Trigger_Area", Relay_In_71))
			{
				logic_uScript_IsPlayerInTrigger_triggerAreaName_71 = room01TriggerVolume;
				logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_71.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_71);
				if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_71.InRange)
				{
					Relay_In_73();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Distance/Is Player in Trigger Area.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_73()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a1c34f3a-9e69-4ea6-9c18-57ca5920dd9b", "Distance_Is_Player_in_Trigger_Area", Relay_In_73))
			{
				logic_uScript_IsPlayerInTrigger_triggerAreaName_73 = NPCTriggerVolume01;
				logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_73.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_73);
				if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_73.OutOfRange)
				{
					Relay_In_81();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Distance/Is Player in Trigger Area.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_74()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("203939d2-c470-4657-a0ea-2fb91cb056ce", "uScript_SpawnTechsFromData", Relay_InitialSpawn_74))
			{
				int num = 0;
				Array nPCSpawnData = NPCSpawnData02;
				if (logic_uScript_SpawnTechsFromData_spawnData_74.Length != num + nPCSpawnData.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_74, num + nPCSpawnData.Length);
				}
				Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_74, num, nPCSpawnData.Length);
				num += nPCSpawnData.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_74 = owner_Connection_129;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_74.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_74, logic_uScript_SpawnTechsFromData_ownerNode_74, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_74, logic_uScript_SpawnTechsFromData_allowResurrection_74);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_74.Out)
				{
					Relay_In_111();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_76()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9b31106e-fc63-4241-b21d-dca248a40034", "SubGraph_CompleteObjectiveStage", Relay_Out_76))
			{
				Relay_In_260();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_76()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9b31106e-fc63-4241-b21d-dca248a40034", "SubGraph_CompleteObjectiveStage", Relay_In_76))
			{
				logic_SubGraph_CompleteObjectiveStage_objectiveStage_76 = local_CurrentObjective_System_Int32;
				logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_76.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_76, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_76);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_81()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("1fc7964c-2126-4386-a33e-5ac9a9a0fe2f", "uScript_RemoveOnScreenMessage", Relay_In_81))
			{
				logic_uScript_RemoveOnScreenMessage_onScreenMessage_81 = local_MsgIntro_ManOnScreenMessages_OnScreenMessage;
				logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_81.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_81, logic_uScript_RemoveOnScreenMessage_instant_81);
				if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_81.Out)
				{
					Relay_In_124();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_RemoveOnScreenMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_82()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9824ca8e-3f70-478b-b12a-5444ca323ac5", "Pass", Relay_In_82))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_82.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_82.Out)
				{
					Relay_In_331();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_83()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("645022fa-c0fb-4ce9-8203-06809bbbc3e9", "uScript_GetAndCheckTechs", Relay_In_83))
			{
				int num = 0;
				Array nPCSpawnData = NPCSpawnData02;
				if (logic_uScript_GetAndCheckTechs_techData_83.Length != num + nPCSpawnData.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_83, num + nPCSpawnData.Length);
				}
				Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_83, num, nPCSpawnData.Length);
				num += nPCSpawnData.Length;
				logic_uScript_GetAndCheckTechs_ownerNode_83 = owner_Connection_118;
				int num2 = 0;
				Array array = local_127_TankArray;
				if (logic_uScript_GetAndCheckTechs_techs_83.Length != num2 + array.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_83, num2 + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_83, num2, array.Length);
				num2 += array.Length;
				logic_uScript_GetAndCheckTechs_Return_83 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_83.In(logic_uScript_GetAndCheckTechs_techData_83, logic_uScript_GetAndCheckTechs_ownerNode_83, ref logic_uScript_GetAndCheckTechs_techs_83);
				local_127_TankArray = logic_uScript_GetAndCheckTechs_techs_83;
				bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_83.AllAlive;
				bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_83.SomeAlive;
				if (allAlive)
				{
					Relay_AtIndex_114();
				}
				if (someAlive)
				{
					Relay_AtIndex_114();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_GetAndCheckTechs.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_92()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("432be785-3cd3-4d05-9d79-73e778e77f8a", "uScript_FlyTechUpAndAway", Relay_In_92))
			{
				logic_uScript_FlyTechUpAndAway_tech_92 = local_NPCTech02_Tank;
				logic_uScript_FlyTechUpAndAway_removalParticles_92 = NPCDespawnEffect;
				logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_92.In(logic_uScript_FlyTechUpAndAway_tech_92, logic_uScript_FlyTechUpAndAway_maxLifetime_92, logic_uScript_FlyTechUpAndAway_targetHeight_92, logic_uScript_FlyTechUpAndAway_aiTree_92, logic_uScript_FlyTechUpAndAway_removalParticles_92);
				if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_92.Out)
				{
					Relay_Succeed_98();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_FlyTechUpAndAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_93()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("25431d17-11f9-43dc-84c6-5d5244df5726", "Distance_Is_Player_in_Trigger_Area", Relay_In_93))
			{
				logic_uScript_IsPlayerInTrigger_triggerAreaName_93 = NPCTriggerVolume02;
				logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_93.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_93);
				if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_93.InRange)
				{
					Relay_In_94();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Distance/Is Player in Trigger Area.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_94()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ad69ad7c-a446-487a-aa69-73c0fbdd641d", "uScript_AddMessage", Relay_In_94))
			{
				logic_uScript_AddMessage_messageData_94 = msg06Outro;
				logic_uScript_AddMessage_speaker_94 = NPCSpeaker;
				logic_uScript_AddMessage_Return_94 = logic_uScript_AddMessage_uScript_AddMessage_94.In(logic_uScript_AddMessage_messageData_94, logic_uScript_AddMessage_speaker_94);
				if (logic_uScript_AddMessage_uScript_AddMessage_94.Shown)
				{
					Relay_In_92();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_95()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b7c6757a-6d1c-4e2a-a190-0f33f1ba2ea9", "Distance_Is_Player_in_Trigger_Area", Relay_In_95))
			{
				logic_uScript_IsPlayerInTrigger_triggerAreaName_95 = NPCTriggerVolume01;
				logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_95.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_95);
				bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_95.InRange;
				bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_95.OutOfRange;
				if (inRange)
				{
					Relay_True_119();
				}
				if (outOfRange)
				{
					Relay_In_106();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Distance/Is Player in Trigger Area.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Succeed_98()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9c1165ef-a2ef-47ab-b94c-fd8eef852195", "uScript_FinishEncounter", Relay_Succeed_98))
			{
				logic_uScript_FinishEncounter_owner_98 = owner_Connection_126;
				logic_uScript_FinishEncounter_uScript_FinishEncounter_98.Succeed(logic_uScript_FinishEncounter_owner_98);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_FinishEncounter.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Fail_98()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9c1165ef-a2ef-47ab-b94c-fd8eef852195", "uScript_FinishEncounter", Relay_Fail_98))
			{
				logic_uScript_FinishEncounter_owner_98 = owner_Connection_126;
				logic_uScript_FinishEncounter_uScript_FinishEncounter_98.Fail(logic_uScript_FinishEncounter_owner_98);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_FinishEncounter.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_100()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("57a678bd-f94d-4d61-ba7b-716ecb067475", "uScript_AddMessage", Relay_In_100))
			{
				logic_uScript_AddMessage_messageData_100 = msg05Room04Clear;
				logic_uScript_AddMessage_speaker_100 = NPCSpeaker;
				logic_uScript_AddMessage_Return_100 = logic_uScript_AddMessage_uScript_AddMessage_100.In(logic_uScript_AddMessage_messageData_100, logic_uScript_AddMessage_speaker_100);
				if (logic_uScript_AddMessage_uScript_AddMessage_100.Out)
				{
					Relay_InitialSpawn_74();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_106()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d3ea1998-c92b-4e05-9d45-8a7626b2a7dc", "Compare_Bool", Relay_In_106))
			{
				logic_uScriptCon_CompareBool_Bool_106 = local_MsgIntroShown_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_106.In(logic_uScriptCon_CompareBool_Bool_106);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_106.True)
				{
					Relay_In_109();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_108()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("433ef802-b442-4e9b-bc2c-e8d1f6f78c48", "Set_Bool", Relay_True_108))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_108.True(out logic_uScriptAct_SetBool_Target_108);
				local_MsgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_108;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_108.Out)
				{
					Relay_In_76();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_108()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("433ef802-b442-4e9b-bc2c-e8d1f6f78c48", "Set_Bool", Relay_False_108))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_108.False(out logic_uScriptAct_SetBool_Target_108);
				local_MsgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_108;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_108.Out)
				{
					Relay_In_76();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_109()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9e92e4a3-b8a1-412d-9cda-de82983e66ee", "uScript_AddMessage", Relay_In_109))
			{
				logic_uScript_AddMessage_messageData_109 = msg01Intro;
				logic_uScript_AddMessage_speaker_109 = NPCSpeaker;
				logic_uScript_AddMessage_Return_109 = logic_uScript_AddMessage_uScript_AddMessage_109.In(logic_uScript_AddMessage_messageData_109, logic_uScript_AddMessage_speaker_109);
				local_MsgIntro_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_109;
				bool num = logic_uScript_AddMessage_uScript_AddMessage_109.Out;
				bool shown = logic_uScript_AddMessage_uScript_AddMessage_109.Shown;
				if (num)
				{
					Relay_In_332();
				}
				if (shown)
				{
					Relay_In_82();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_111()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("86f6aac1-f0ee-4f90-a79b-492f496f8a2f", "SubGraph_CompleteObjectiveStage", Relay_Out_111);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_111()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("86f6aac1-f0ee-4f90-a79b-492f496f8a2f", "SubGraph_CompleteObjectiveStage", Relay_In_111))
			{
				logic_SubGraph_CompleteObjectiveStage_objectiveStage_111 = local_CurrentObjective_System_Int32;
				logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_111, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_111);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_AtIndex_114()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("eb3e0ccd-427b-4609-b2c7-3bb4cd1fcbde", "uScript_AccessListTech", Relay_AtIndex_114))
			{
				int num = 0;
				Array array = local_127_TankArray;
				if (logic_uScript_AccessListTech_techList_114.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AccessListTech_techList_114, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AccessListTech_techList_114, num, array.Length);
				num += array.Length;
				logic_uScript_AccessListTech_uScript_AccessListTech_114.AtIndex(ref logic_uScript_AccessListTech_techList_114, logic_uScript_AccessListTech_index_114, out logic_uScript_AccessListTech_value_114);
				local_127_TankArray = logic_uScript_AccessListTech_techList_114;
				local_NPCTech02_Tank = logic_uScript_AccessListTech_value_114;
				if (logic_uScript_AccessListTech_uScript_AccessListTech_114.Out)
				{
					Relay_In_121();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_AccessListTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output1_115()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("84acc130-1eb9-4b08-a1b4-6b39747e50fd", "Manual_Switch", Relay_Output1_115))
			{
				Relay_In_175();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output2_115()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("84acc130-1eb9-4b08-a1b4-6b39747e50fd", "Manual_Switch", Relay_Output2_115))
			{
				Relay_In_130();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output3_115()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("84acc130-1eb9-4b08-a1b4-6b39747e50fd", "Manual_Switch", Relay_Output3_115))
			{
				Relay_In_83();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output4_115()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("84acc130-1eb9-4b08-a1b4-6b39747e50fd", "Manual_Switch", Relay_Output4_115);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output5_115()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("84acc130-1eb9-4b08-a1b4-6b39747e50fd", "Manual_Switch", Relay_Output5_115);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output6_115()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("84acc130-1eb9-4b08-a1b4-6b39747e50fd", "Manual_Switch", Relay_Output6_115);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output7_115()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("84acc130-1eb9-4b08-a1b4-6b39747e50fd", "Manual_Switch", Relay_Output7_115);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output8_115()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("84acc130-1eb9-4b08-a1b4-6b39747e50fd", "Manual_Switch", Relay_Output8_115);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_115()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("84acc130-1eb9-4b08-a1b4-6b39747e50fd", "Manual_Switch", Relay_In_115))
			{
				logic_uScriptCon_ManualSwitch_CurrentOutput_115 = local_CurrentObjective_System_Int32;
				logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.In(logic_uScriptCon_ManualSwitch_CurrentOutput_115);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_119()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("77287004-ea1b-438f-8608-d4a583dfc213", "Set_Bool", Relay_True_119))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_119.True(out logic_uScriptAct_SetBool_Target_119);
				local_MsgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_119;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_119.Out)
				{
					Relay_In_109();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_119()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("77287004-ea1b-438f-8608-d4a583dfc213", "Set_Bool", Relay_False_119))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_119.False(out logic_uScriptAct_SetBool_Target_119);
				local_MsgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_119;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_119.Out)
				{
					Relay_In_109();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_121()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("419c5754-5e9e-4f62-9b98-74b6036ff5ea", "uScript_SetEncounterTarget", Relay_In_121))
			{
				logic_uScript_SetEncounterTarget_owner_121 = owner_Connection_88;
				logic_uScript_SetEncounterTarget_visibleObject_121 = local_NPCTech02_Tank;
				logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_121.In(logic_uScript_SetEncounterTarget_owner_121, logic_uScript_SetEncounterTarget_visibleObject_121);
				if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_121.Out)
				{
					Relay_In_93();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SetEncounterTarget.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_124()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6c596162-2c21-436b-9a5d-d35b72626e70", "uScript_FlyTechUpAndAway", Relay_In_124))
			{
				logic_uScript_FlyTechUpAndAway_tech_124 = local_NPCTech_Tank;
				logic_uScript_FlyTechUpAndAway_removalParticles_124 = NPCDespawnEffect;
				logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_124.In(logic_uScript_FlyTechUpAndAway_tech_124, logic_uScript_FlyTechUpAndAway_maxLifetime_124, logic_uScript_FlyTechUpAndAway_targetHeight_124, logic_uScript_FlyTechUpAndAway_aiTree_124, logic_uScript_FlyTechUpAndAway_removalParticles_124);
				if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_124.Out)
				{
					Relay_AtIndex_185();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_FlyTechUpAndAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output1_130()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ba27c87d-b7e8-4c1e-bb95-bd247ea1c0eb", "Manual_Switch", Relay_Output1_130))
			{
				Relay_In_132();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output2_130()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ba27c87d-b7e8-4c1e-bb95-bd247ea1c0eb", "Manual_Switch", Relay_Output2_130))
			{
				Relay_In_274();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output3_130()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ba27c87d-b7e8-4c1e-bb95-bd247ea1c0eb", "Manual_Switch", Relay_Output3_130))
			{
				Relay_In_143();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output4_130()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ba27c87d-b7e8-4c1e-bb95-bd247ea1c0eb", "Manual_Switch", Relay_Output4_130))
			{
				Relay_In_278();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output5_130()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ba27c87d-b7e8-4c1e-bb95-bd247ea1c0eb", "Manual_Switch", Relay_Output5_130))
			{
				Relay_In_152();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output6_130()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ba27c87d-b7e8-4c1e-bb95-bd247ea1c0eb", "Manual_Switch", Relay_Output6_130))
			{
				Relay_In_281();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output7_130()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ba27c87d-b7e8-4c1e-bb95-bd247ea1c0eb", "Manual_Switch", Relay_Output7_130))
			{
				Relay_In_163();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output8_130()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("ba27c87d-b7e8-4c1e-bb95-bd247ea1c0eb", "Manual_Switch", Relay_Output8_130);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_130()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ba27c87d-b7e8-4c1e-bb95-bd247ea1c0eb", "Manual_Switch", Relay_In_130))
			{
				logic_uScriptCon_ManualSwitch_CurrentOutput_130 = local_Objective02SubStage_System_Int32;
				logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_130.In(logic_uScriptCon_ManualSwitch_CurrentOutput_130);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_132()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("11b8c1a1-d76f-48e1-87fb-5affd4b9ca4a", "Compare_Bool", Relay_In_132))
			{
				logic_uScriptCon_CompareBool_Bool_132 = local_TeslaTurrets01Destroyed_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_132.In(logic_uScriptCon_CompareBool_Bool_132);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_132.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_132.False;
				if (num)
				{
					Relay_InitialSpawn_288();
				}
				if (flag)
				{
					Relay_In_302();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_AtIndex_134()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e76e46e7-7bac-42b2-8185-4a8e84cb70c9", "uScript_AccessListTech", Relay_AtIndex_134))
			{
				int num = 0;
				Array array = local_ForcefieldGates_TankArray;
				if (logic_uScript_AccessListTech_techList_134.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AccessListTech_techList_134, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AccessListTech_techList_134, num, array.Length);
				num += array.Length;
				logic_uScript_AccessListTech_uScript_AccessListTech_134.AtIndex(ref logic_uScript_AccessListTech_techList_134, logic_uScript_AccessListTech_index_134, out logic_uScript_AccessListTech_value_134);
				local_ForcefieldGates_TankArray = logic_uScript_AccessListTech_techList_134;
				local_136_Tank = logic_uScript_AccessListTech_value_134;
				if (logic_uScript_AccessListTech_uScript_AccessListTech_134.Out)
				{
					Relay_In_137();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_AccessListTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_137()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("80e3e7f3-2f79-4558-b747-4bb888c73c1f", "uScript_RemoveTech", Relay_In_137))
			{
				logic_uScript_RemoveTech_tech_137 = local_136_Tank;
				logic_uScript_RemoveTech_uScript_RemoveTech_137.In(logic_uScript_RemoveTech_tech_137);
				if (logic_uScript_RemoveTech_uScript_RemoveTech_137.Out)
				{
					Relay_In_296();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_RemoveTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_139()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("906d78ba-c79e-483c-997e-cea804d15d82", "uScript_RemoveTech", Relay_In_139))
			{
				logic_uScript_RemoveTech_tech_139 = local_138_Tank;
				logic_uScript_RemoveTech_uScript_RemoveTech_139.In(logic_uScript_RemoveTech_tech_139);
				if (logic_uScript_RemoveTech_uScript_RemoveTech_139.Out)
				{
					Relay_In_298();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_RemoveTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_AtIndex_140()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("243aaa52-aee0-4abc-81cf-49ce1dc4dbe0", "uScript_AccessListTech", Relay_AtIndex_140))
			{
				int num = 0;
				Array array = local_ForcefieldGates_TankArray;
				if (logic_uScript_AccessListTech_techList_140.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AccessListTech_techList_140, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AccessListTech_techList_140, num, array.Length);
				num += array.Length;
				logic_uScript_AccessListTech_uScript_AccessListTech_140.AtIndex(ref logic_uScript_AccessListTech_techList_140, logic_uScript_AccessListTech_index_140, out logic_uScript_AccessListTech_value_140);
				local_ForcefieldGates_TankArray = logic_uScript_AccessListTech_techList_140;
				local_138_Tank = logic_uScript_AccessListTech_value_140;
				if (logic_uScript_AccessListTech_uScript_AccessListTech_140.Out)
				{
					Relay_In_139();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_AccessListTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_143()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ad9a562e-30bf-49ae-aa82-e79641adb157", "Compare_Bool", Relay_In_143))
			{
				logic_uScriptCon_CompareBool_Bool_143 = local_LaserTurrets02Destroyed_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.In(logic_uScriptCon_CompareBool_Bool_143);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.True)
				{
					Relay_In_144();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_144()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b5b4a169-b70e-4d38-a385-3da3434293c7", "Compare_Bool", Relay_In_144))
			{
				logic_uScriptCon_CompareBool_Bool_144 = local_TeslaTurrets02Destroyed_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_144.In(logic_uScriptCon_CompareBool_Bool_144);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_144.True)
				{
					Relay_InitialSpawn_217();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_AtIndex_146()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("11b0e8af-870e-46a1-8431-65e64f2b6859", "uScript_AccessListTech", Relay_AtIndex_146))
			{
				int num = 0;
				Array array = local_ForcefieldGates_TankArray;
				if (logic_uScript_AccessListTech_techList_146.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AccessListTech_techList_146, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AccessListTech_techList_146, num, array.Length);
				num += array.Length;
				logic_uScript_AccessListTech_uScript_AccessListTech_146.AtIndex(ref logic_uScript_AccessListTech_techList_146, logic_uScript_AccessListTech_index_146, out logic_uScript_AccessListTech_value_146);
				local_ForcefieldGates_TankArray = logic_uScript_AccessListTech_techList_146;
				local_151_Tank = logic_uScript_AccessListTech_value_146;
				if (logic_uScript_AccessListTech_uScript_AccessListTech_146.Out)
				{
					Relay_In_150();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_AccessListTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_147()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("96cc7736-7b60-417a-b7de-2707c56e6840", "Compare_Bool", Relay_In_147))
			{
				logic_uScriptCon_CompareBool_Bool_147 = local_TeslaTurrets03Destroyed_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_147.In(logic_uScriptCon_CompareBool_Bool_147);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_147.True)
				{
					Relay_In_243();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_150()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b7a34c1e-25c1-4e76-9fd9-84c6473a2e01", "uScript_RemoveTech", Relay_In_150))
			{
				logic_uScript_RemoveTech_tech_150 = local_151_Tank;
				logic_uScript_RemoveTech_uScript_RemoveTech_150.In(logic_uScript_RemoveTech_tech_150);
				if (logic_uScript_RemoveTech_uScript_RemoveTech_150.Out)
				{
					Relay_AtIndex_155();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_RemoveTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_152()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b5714336-f771-48a4-b46d-c422adf8d865", "Compare_Bool", Relay_In_152))
			{
				logic_uScriptCon_CompareBool_Bool_152 = local_LaserTurrets03Destroyed_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_152.In(logic_uScriptCon_CompareBool_Bool_152);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_152.True)
				{
					Relay_In_147();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_154()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c192a534-2fd4-4457-bdb1-ef564f068679", "uScript_RemoveTech", Relay_In_154))
			{
				logic_uScript_RemoveTech_tech_154 = local_156_Tank;
				logic_uScript_RemoveTech_uScript_RemoveTech_154.In(logic_uScript_RemoveTech_tech_154);
				if (logic_uScript_RemoveTech_uScript_RemoveTech_154.Out)
				{
					Relay_In_300();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_RemoveTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_AtIndex_155()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ac197c6c-a845-40b4-90f0-4c504d50e1a3", "uScript_AccessListTech", Relay_AtIndex_155))
			{
				int num = 0;
				Array array = local_ForcefieldGates_TankArray;
				if (logic_uScript_AccessListTech_techList_155.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AccessListTech_techList_155, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AccessListTech_techList_155, num, array.Length);
				num += array.Length;
				logic_uScript_AccessListTech_uScript_AccessListTech_155.AtIndex(ref logic_uScript_AccessListTech_techList_155, logic_uScript_AccessListTech_index_155, out logic_uScript_AccessListTech_value_155);
				local_ForcefieldGates_TankArray = logic_uScript_AccessListTech_techList_155;
				local_156_Tank = logic_uScript_AccessListTech_value_155;
				if (logic_uScript_AccessListTech_uScript_AccessListTech_155.Out)
				{
					Relay_In_154();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_AccessListTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_158()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("173e1179-9c67-47b2-a9fa-bbab82ced9c7", "uScript_RemoveTech", Relay_In_158))
			{
				logic_uScript_RemoveTech_tech_158 = local_162_Tank;
				logic_uScript_RemoveTech_uScript_RemoveTech_158.In(logic_uScript_RemoveTech_tech_158);
				if (logic_uScript_RemoveTech_uScript_RemoveTech_158.Out)
				{
					Relay_In_198();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_RemoveTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_159()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("25274838-5165-41cb-b4f6-ae6c62bd7d42", "Compare_Bool", Relay_In_159))
			{
				logic_uScriptCon_CompareBool_Bool_159 = local_TeslaTurrets04Destroyed_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.In(logic_uScriptCon_CompareBool_Bool_159);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.True)
				{
					Relay_In_245();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_AtIndex_160()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("1ec2fd3f-0a9d-4363-829c-a5348c50bf33", "uScript_AccessListTech", Relay_AtIndex_160))
			{
				int num = 0;
				Array array = local_ForcefieldGates_TankArray;
				if (logic_uScript_AccessListTech_techList_160.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AccessListTech_techList_160, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AccessListTech_techList_160, num, array.Length);
				num += array.Length;
				logic_uScript_AccessListTech_uScript_AccessListTech_160.AtIndex(ref logic_uScript_AccessListTech_techList_160, logic_uScript_AccessListTech_index_160, out logic_uScript_AccessListTech_value_160);
				local_ForcefieldGates_TankArray = logic_uScript_AccessListTech_techList_160;
				local_162_Tank = logic_uScript_AccessListTech_value_160;
				if (logic_uScript_AccessListTech_uScript_AccessListTech_160.Out)
				{
					Relay_In_158();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_AccessListTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_163()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d3a7c1cc-a111-43f8-8191-9c73a70521ec", "Compare_Bool", Relay_In_163))
			{
				logic_uScriptCon_CompareBool_Bool_163 = local_LaserTurrets04Destroyed_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_163.In(logic_uScriptCon_CompareBool_Bool_163);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_163.True)
				{
					Relay_In_159();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_173()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("616b5fd7-41d0-4a1e-bb7a-5b5948553b07", "Pass", Relay_In_173))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_173.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_173.Out)
				{
					Relay_In_95();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_175()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("1b0f4416-23e5-4f61-b914-385992c535f1", "uScript_GetAndCheckTechs", Relay_In_175))
			{
				int num = 0;
				Array nPCSpawnData = NPCSpawnData01;
				if (logic_uScript_GetAndCheckTechs_techData_175.Length != num + nPCSpawnData.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_175, num + nPCSpawnData.Length);
				}
				Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_175, num, nPCSpawnData.Length);
				num += nPCSpawnData.Length;
				logic_uScript_GetAndCheckTechs_ownerNode_175 = owner_Connection_171;
				int num2 = 0;
				Array array = local_170_TankArray;
				if (logic_uScript_GetAndCheckTechs_techs_175.Length != num2 + array.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_175, num2 + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_175, num2, array.Length);
				num2 += array.Length;
				logic_uScript_GetAndCheckTechs_Return_175 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_175.In(logic_uScript_GetAndCheckTechs_techData_175, logic_uScript_GetAndCheckTechs_ownerNode_175, ref logic_uScript_GetAndCheckTechs_techs_175);
				local_170_TankArray = logic_uScript_GetAndCheckTechs_techs_175;
				bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_175.AllAlive;
				bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_175.SomeAlive;
				bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_175.AllDead;
				bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_175.WaitingToSpawn;
				if (allAlive)
				{
					Relay_AtIndex_176();
				}
				if (someAlive)
				{
					Relay_AtIndex_176();
				}
				if (allDead)
				{
					Relay_In_173();
				}
				if (waitingToSpawn)
				{
					Relay_In_173();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_GetAndCheckTechs.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_AtIndex_176()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e47ede8c-396a-409a-8b32-e76bac182a35", "uScript_AccessListTech", Relay_AtIndex_176))
			{
				int num = 0;
				Array array = local_170_TankArray;
				if (logic_uScript_AccessListTech_techList_176.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AccessListTech_techList_176, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AccessListTech_techList_176, num, array.Length);
				num += array.Length;
				logic_uScript_AccessListTech_uScript_AccessListTech_176.AtIndex(ref logic_uScript_AccessListTech_techList_176, logic_uScript_AccessListTech_index_176, out logic_uScript_AccessListTech_value_176);
				local_170_TankArray = logic_uScript_AccessListTech_techList_176;
				local_NPCTech_Tank = logic_uScript_AccessListTech_value_176;
				if (logic_uScript_AccessListTech_uScript_AccessListTech_176.Out)
				{
					Relay_In_269();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_AccessListTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_177()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("55226bff-8663-4027-984f-c8b6fcd78e01", "SubGraph_RR_TeslaBase_ForcefieldController", Relay_Out_177))
			{
				Relay_In_180();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_TeslaBase_ForcefieldController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_177()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("55226bff-8663-4027-984f-c8b6fcd78e01", "SubGraph_RR_TeslaBase_ForcefieldController", Relay_In_177))
			{
				int num = 0;
				Array forcefieldGatesSpawnData = ForcefieldGatesSpawnData;
				if (logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_177.Length != num + forcefieldGatesSpawnData.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_177, num + forcefieldGatesSpawnData.Length);
				}
				Array.Copy(forcefieldGatesSpawnData, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_177, num, forcefieldGatesSpawnData.Length);
				num += forcefieldGatesSpawnData.Length;
				int num2 = 0;
				Array array = local_ForcefieldGates_TankArray;
				if (logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_177.Length != num2 + array.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_177, num2 + array.Length);
				}
				Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_177, num2, array.Length);
				num2 += array.Length;
				logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_177.In(logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_177, ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_177);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_TeslaBase_ForcefieldController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_180()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("284a3379-1f29-46ec-9158-c6ca879c4d87", "SubGraph_RR_TeslaBase_ForcefieldController", Relay_Out_180))
			{
				Relay_In_31();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_TeslaBase_ForcefieldController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_180()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("284a3379-1f29-46ec-9158-c6ca879c4d87", "SubGraph_RR_TeslaBase_ForcefieldController", Relay_In_180))
			{
				int num = 0;
				Array room04ForcefieldsSpawnData = Room04ForcefieldsSpawnData;
				if (logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_180.Length != num + room04ForcefieldsSpawnData.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_180, num + room04ForcefieldsSpawnData.Length);
				}
				Array.Copy(room04ForcefieldsSpawnData, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_180, num, room04ForcefieldsSpawnData.Length);
				num += room04ForcefieldsSpawnData.Length;
				int num2 = 0;
				Array array = local_Room04Forcefields_TankArray;
				if (logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_180.Length != num2 + array.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_180, num2 + array.Length);
				}
				Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_180, num2, array.Length);
				num2 += array.Length;
				logic_SubGraph_RR_TeslaBase_ForcefieldController_SubGraph_RR_TeslaBase_ForcefieldController_180.In(logic_SubGraph_RR_TeslaBase_ForcefieldController_forcefieldSpawnData_180, ref logic_SubGraph_RR_TeslaBase_ForcefieldController_Forcefields_180);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_TeslaBase_ForcefieldController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_184()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ec4dfce6-7a03-4b66-8dec-ebe1bbccda45", "uScript_RemoveTech", Relay_In_184))
			{
				logic_uScript_RemoveTech_tech_184 = local_183_Tank;
				logic_uScript_RemoveTech_uScript_RemoveTech_184.In(logic_uScript_RemoveTech_tech_184);
				if (logic_uScript_RemoveTech_uScript_RemoveTech_184.Out)
				{
					Relay_False_108();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_RemoveTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_AtIndex_185()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f788fa48-79ea-4d52-b65f-5bb92185759f", "uScript_AccessListTech", Relay_AtIndex_185))
			{
				int num = 0;
				Array array = local_ForcefieldGates_TankArray;
				if (logic_uScript_AccessListTech_techList_185.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AccessListTech_techList_185, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AccessListTech_techList_185, num, array.Length);
				num += array.Length;
				logic_uScript_AccessListTech_uScript_AccessListTech_185.AtIndex(ref logic_uScript_AccessListTech_techList_185, logic_uScript_AccessListTech_index_185, out logic_uScript_AccessListTech_value_185);
				local_ForcefieldGates_TankArray = logic_uScript_AccessListTech_techList_185;
				local_183_Tank = logic_uScript_AccessListTech_value_185;
				if (logic_uScript_AccessListTech_uScript_AccessListTech_185.Out)
				{
					Relay_In_184();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_AccessListTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_188()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("67b491b6-1669-4910-9e04-bf14ff4fda1b", "Add_Int", Relay_In_188))
			{
				logic_uScriptAct_AddInt_v2_A_188 = local_Objective02SubStage_System_Int32;
				logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_188.In(logic_uScriptAct_AddInt_v2_A_188, logic_uScriptAct_AddInt_v2_B_188, out logic_uScriptAct_AddInt_v2_IntResult_188, out logic_uScriptAct_AddInt_v2_FloatResult_188);
				local_Objective02SubStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_188;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Add Int.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_190()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("af093a2d-becb-4518-a6d4-8e8b21e1d152", "Add_Int", Relay_In_190))
			{
				logic_uScriptAct_AddInt_v2_A_190 = local_Objective02SubStage_System_Int32;
				logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_190.In(logic_uScriptAct_AddInt_v2_A_190, logic_uScriptAct_AddInt_v2_B_190, out logic_uScriptAct_AddInt_v2_IntResult_190, out logic_uScriptAct_AddInt_v2_FloatResult_190);
				local_Objective02SubStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_190;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Add Int.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_192()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("99fcbfb1-2573-4868-8fe6-ee8905377053", "Add_Int", Relay_In_192))
			{
				logic_uScriptAct_AddInt_v2_A_192 = local_Objective02SubStage_System_Int32;
				logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_192.In(logic_uScriptAct_AddInt_v2_A_192, logic_uScriptAct_AddInt_v2_B_192, out logic_uScriptAct_AddInt_v2_IntResult_192, out logic_uScriptAct_AddInt_v2_FloatResult_192);
				local_Objective02SubStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_192;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Add Int.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_195()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8c368636-5307-4d1b-a017-b69983d681de", "Pass", Relay_In_195))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_195.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_195.Out)
				{
					Relay_In_198();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_196()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("abc753ef-25e6-47c9-b78e-29f55bc7f1bd", "Pass", Relay_In_196))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_196.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_196.Out)
				{
					Relay_In_195();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_197()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("825a2336-8f8d-41f0-96b0-71a6c18b6e5c", "Pass", Relay_In_197))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_197.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_197.Out)
				{
					Relay_In_196();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Reset_198()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("fef9acc4-ed88-4f37-ae30-66f485e8236c", "For_Each_In_List__Tech_", Relay_Reset_198))
			{
				int num = 0;
				Array array = local_Room04Forcefields_TankArray;
				if (logic_uScript_ForEachListTech_List_198.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_ForEachListTech_List_198, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_ForEachListTech_List_198, num, array.Length);
				num += array.Length;
				logic_uScript_ForEachListTech_uScript_ForEachListTech_198.Reset(logic_uScript_ForEachListTech_List_198, out logic_uScript_ForEachListTech_Value_198, out logic_uScript_ForEachListTech_currentIndex_198);
				local_194_Tank = logic_uScript_ForEachListTech_Value_198;
				bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_198.Done;
				bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_198.Iteration;
				if (done)
				{
					Relay_In_199();
				}
				if (iteration)
				{
					Relay_In_201();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at For Each In List (Tech).  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_198()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("fef9acc4-ed88-4f37-ae30-66f485e8236c", "For_Each_In_List__Tech_", Relay_In_198))
			{
				int num = 0;
				Array array = local_Room04Forcefields_TankArray;
				if (logic_uScript_ForEachListTech_List_198.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_ForEachListTech_List_198, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_ForEachListTech_List_198, num, array.Length);
				num += array.Length;
				logic_uScript_ForEachListTech_uScript_ForEachListTech_198.In(logic_uScript_ForEachListTech_List_198, out logic_uScript_ForEachListTech_Value_198, out logic_uScript_ForEachListTech_currentIndex_198);
				local_194_Tank = logic_uScript_ForEachListTech_Value_198;
				bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_198.Done;
				bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_198.Iteration;
				if (done)
				{
					Relay_In_199();
				}
				if (iteration)
				{
					Relay_In_201();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at For Each In List (Tech).  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_199()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ac58655b-0126-4f90-aff2-49f01b22bd61", "Pass", Relay_In_199))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_199.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_199.Out)
				{
					Relay_In_100();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_201()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("607e91f3-0169-48f6-88cf-60330934b9f5", "uScript_RemoveTech", Relay_In_201))
			{
				logic_uScript_RemoveTech_tech_201 = local_194_Tank;
				logic_uScript_RemoveTech_uScript_RemoveTech_201.In(logic_uScript_RemoveTech_tech_201);
				if (logic_uScript_RemoveTech_uScript_RemoveTech_201.Out)
				{
					Relay_In_197();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_RemoveTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_204()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("43b1a793-609f-4efb-ba90-e7043010de96", "", Relay_Save_Out_204))
			{
				Relay_Save_327();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_204()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("43b1a793-609f-4efb-ba90-e7043010de96", "", Relay_Load_Out_204))
			{
				Relay_Load_327();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_204()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("43b1a793-609f-4efb-ba90-e7043010de96", "", Relay_Restart_Out_204))
			{
				Relay_Set_False_327();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_204()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("43b1a793-609f-4efb-ba90-e7043010de96", "", Relay_Save_204))
			{
				logic_SubGraph_SaveLoadBool_boolean_204 = local_Init_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_204 = local_Init_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Save(ref logic_SubGraph_SaveLoadBool_boolean_204, logic_SubGraph_SaveLoadBool_boolAsVariable_204, logic_SubGraph_SaveLoadBool_uniqueID_204);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_204()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("43b1a793-609f-4efb-ba90-e7043010de96", "", Relay_Load_204))
			{
				logic_SubGraph_SaveLoadBool_boolean_204 = local_Init_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_204 = local_Init_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Load(ref logic_SubGraph_SaveLoadBool_boolean_204, logic_SubGraph_SaveLoadBool_boolAsVariable_204, logic_SubGraph_SaveLoadBool_uniqueID_204);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_204()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("43b1a793-609f-4efb-ba90-e7043010de96", "", Relay_Set_True_204))
			{
				logic_SubGraph_SaveLoadBool_boolean_204 = local_Init_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_204 = local_Init_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_204, logic_SubGraph_SaveLoadBool_boolAsVariable_204, logic_SubGraph_SaveLoadBool_uniqueID_204);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_204()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("43b1a793-609f-4efb-ba90-e7043010de96", "", Relay_Set_False_204))
			{
				logic_SubGraph_SaveLoadBool_boolean_204 = local_Init_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_204 = local_Init_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_204.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_204, logic_SubGraph_SaveLoadBool_boolAsVariable_204, logic_SubGraph_SaveLoadBool_uniqueID_204);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_206()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("de34ae9b-3fc9-44f9-9f4b-a6d8c5ee3b3b", "SubGraph_LoadObjectiveStates", Relay_Out_206);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_LoadObjectiveStates.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_206()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("de34ae9b-3fc9-44f9-9f4b-a6d8c5ee3b3b", "SubGraph_LoadObjectiveStates", Relay_In_206))
			{
				logic_SubGraph_LoadObjectiveStates_currentObjective_206 = local_CurrentObjective_System_Int32;
				logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.In(logic_SubGraph_LoadObjectiveStates_currentObjective_206);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_LoadObjectiveStates.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_207()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2245086f-b9a0-4de7-a366-b875eb4aa82e", "", Relay_Save_Out_207))
			{
				Relay_Save_210();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_207()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2245086f-b9a0-4de7-a366-b875eb4aa82e", "", Relay_Load_Out_207))
			{
				Relay_Load_210();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_207()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2245086f-b9a0-4de7-a366-b875eb4aa82e", "", Relay_Restart_Out_207))
			{
				Relay_Restart_210();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_207()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2245086f-b9a0-4de7-a366-b875eb4aa82e", "", Relay_Save_207))
			{
				logic_SubGraph_SaveLoadInt_integer_207 = local_CurrentObjective_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_207 = local_CurrentObjective_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207.Save(logic_SubGraph_SaveLoadInt_restartValue_207, ref logic_SubGraph_SaveLoadInt_integer_207, logic_SubGraph_SaveLoadInt_intAsVariable_207, logic_SubGraph_SaveLoadInt_uniqueID_207);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_207()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2245086f-b9a0-4de7-a366-b875eb4aa82e", "", Relay_Load_207))
			{
				logic_SubGraph_SaveLoadInt_integer_207 = local_CurrentObjective_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_207 = local_CurrentObjective_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207.Load(logic_SubGraph_SaveLoadInt_restartValue_207, ref logic_SubGraph_SaveLoadInt_integer_207, logic_SubGraph_SaveLoadInt_intAsVariable_207, logic_SubGraph_SaveLoadInt_uniqueID_207);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_207()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2245086f-b9a0-4de7-a366-b875eb4aa82e", "", Relay_Restart_207))
			{
				logic_SubGraph_SaveLoadInt_integer_207 = local_CurrentObjective_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_207 = local_CurrentObjective_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_207.Restart(logic_SubGraph_SaveLoadInt_restartValue_207, ref logic_SubGraph_SaveLoadInt_integer_207, logic_SubGraph_SaveLoadInt_intAsVariable_207, logic_SubGraph_SaveLoadInt_uniqueID_207);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_210()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("f29ab2d4-ee73-4577-86cd-e80e0419fec8", "", Relay_Save_Out_210);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_210()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f29ab2d4-ee73-4577-86cd-e80e0419fec8", "", Relay_Load_Out_210))
			{
				Relay_In_206();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_210()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("f29ab2d4-ee73-4577-86cd-e80e0419fec8", "", Relay_Restart_Out_210);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_210()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f29ab2d4-ee73-4577-86cd-e80e0419fec8", "", Relay_Save_210))
			{
				logic_SubGraph_SaveLoadInt_integer_210 = local_Objective02SubStage_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_210 = local_Objective02SubStage_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Save(logic_SubGraph_SaveLoadInt_restartValue_210, ref logic_SubGraph_SaveLoadInt_integer_210, logic_SubGraph_SaveLoadInt_intAsVariable_210, logic_SubGraph_SaveLoadInt_uniqueID_210);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_210()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f29ab2d4-ee73-4577-86cd-e80e0419fec8", "", Relay_Load_210))
			{
				logic_SubGraph_SaveLoadInt_integer_210 = local_Objective02SubStage_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_210 = local_Objective02SubStage_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Load(logic_SubGraph_SaveLoadInt_restartValue_210, ref logic_SubGraph_SaveLoadInt_integer_210, logic_SubGraph_SaveLoadInt_intAsVariable_210, logic_SubGraph_SaveLoadInt_uniqueID_210);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_210()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f29ab2d4-ee73-4577-86cd-e80e0419fec8", "", Relay_Restart_210))
			{
				logic_SubGraph_SaveLoadInt_integer_210 = local_Objective02SubStage_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_210 = local_Objective02SubStage_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Restart(logic_SubGraph_SaveLoadInt_restartValue_210, ref logic_SubGraph_SaveLoadInt_integer_210, logic_SubGraph_SaveLoadInt_intAsVariable_210, logic_SubGraph_SaveLoadInt_uniqueID_210);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_SaveEvent_211()
	{
		if (!CheckDebugBreak("eeaa2fdf-732b-47e3-88fd-2c01467c1a08", "uScript_SaveLoad", Relay_SaveEvent_211))
		{
			Relay_Save_204();
		}
	}

	private void Relay_LoadEvent_211()
	{
		if (!CheckDebugBreak("eeaa2fdf-732b-47e3-88fd-2c01467c1a08", "uScript_SaveLoad", Relay_LoadEvent_211))
		{
			Relay_Load_204();
		}
	}

	private void Relay_RestartEvent_211()
	{
		if (!CheckDebugBreak("eeaa2fdf-732b-47e3-88fd-2c01467c1a08", "uScript_SaveLoad", Relay_RestartEvent_211))
		{
			Relay_Set_False_204();
		}
	}

	private void Relay_InitialSpawn_217()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("be51bef9-ab10-4cc4-abfa-87d92401cb70", "uScript_SpawnTechsFromData", Relay_InitialSpawn_217))
			{
				int num = 0;
				Array array = teslaTurretGroup03SpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_217.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_217, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_217, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_217 = owner_Connection_214;
				logic_uScript_SpawnTechsFromData_delayBetweenSpawns_217 = telsaTurretGroup03SpawnInterval;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_217.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_217, logic_uScript_SpawnTechsFromData_ownerNode_217, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_217, logic_uScript_SpawnTechsFromData_allowResurrection_217);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_217.Out)
				{
					Relay_InitialSpawn_218();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_218()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("1fb997f1-b67d-4258-9937-c52e9fe59fe2", "uScript_SpawnTechsFromData", Relay_InitialSpawn_218))
			{
				int num = 0;
				Array array = laserTurretGroup03SpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_218.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_218, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_218, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_218 = owner_Connection_221;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_218.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_218, logic_uScript_SpawnTechsFromData_ownerNode_218, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_218, logic_uScript_SpawnTechsFromData_allowResurrection_218);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_218.Out)
				{
					Relay_InitialSpawn_253();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_219()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f7f6626c-590b-4ca8-b681-be08624dc038", "uScript_SpawnTechsFromData", Relay_InitialSpawn_219))
			{
				int num = 0;
				Array array = laserTurretGroup04SpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_219.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_219, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_219, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_219 = owner_Connection_224;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_219.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_219, logic_uScript_SpawnTechsFromData_ownerNode_219, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_219, logic_uScript_SpawnTechsFromData_allowResurrection_219);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_219.Out)
				{
					Relay_InitialSpawn_257();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_220()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ebfa037d-76ca-4d87-8701-c4caef34b0e7", "uScript_SpawnTechsFromData", Relay_InitialSpawn_220))
			{
				int num = 0;
				Array array = teslaTurretGroup04SpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_220.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_220, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_220, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_220 = owner_Connection_215;
				logic_uScript_SpawnTechsFromData_delayBetweenSpawns_220 = telsaTurretGroup04SpawnInterval;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_220.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_220, logic_uScript_SpawnTechsFromData_ownerNode_220, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_220, logic_uScript_SpawnTechsFromData_allowResurrection_220);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_220.Out)
				{
					Relay_InitialSpawn_219();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_225()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("074c063f-19fd-4a14-a3ce-1347c09310b8", "SubGraph_RR_TeslaBase_BlasterTurretController", Relay_Out_225))
			{
				Relay_In_54();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_TeslaBase_BlasterTurretController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_225()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("074c063f-19fd-4a14-a3ce-1347c09310b8", "SubGraph_RR_TeslaBase_BlasterTurretController", Relay_In_225))
			{
				int num = 0;
				Array array = shieldCharger03SpawnData;
				if (logic_SubGraph_RR_TeslaBase_BlasterTurretController_chargerSpawnData_225.Length != num + array.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_BlasterTurretController_chargerSpawnData_225, num + array.Length);
				}
				Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_BlasterTurretController_chargerSpawnData_225, num, array.Length);
				num += array.Length;
				int num2 = 0;
				Array array2 = blasterTurretGroup03SpawnData;
				if (logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretSpawnData_225.Length != num2 + array2.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretSpawnData_225, num2 + array2.Length);
				}
				Array.Copy(array2, 0, logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretSpawnData_225, num2, array2.Length);
				num2 += array2.Length;
				logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretShieldBlockType_225 = blasterTurretShieldBlockType;
				int num3 = 0;
				Array array3 = blasterTurretGroup03AlertZones;
				if (logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretAlertZones_225.Length != num3 + array3.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretAlertZones_225, num3 + array3.Length);
				}
				Array.Copy(array3, 0, logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretAlertZones_225, num3, array3.Length);
				num3 += array3.Length;
				logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_225.In(logic_SubGraph_RR_TeslaBase_BlasterTurretController_chargerSpawnData_225, logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretSpawnData_225, logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretShieldBlockType_225, logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretAlertZones_225);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_TeslaBase_BlasterTurretController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_231()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bff747fe-04b8-451d-9c6c-370aa2fa744f", "SubGraph_RR_TeslaBase_BlasterTurretController", Relay_Out_231))
			{
				Relay_In_115();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_TeslaBase_BlasterTurretController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_231()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bff747fe-04b8-451d-9c6c-370aa2fa744f", "SubGraph_RR_TeslaBase_BlasterTurretController", Relay_In_231))
			{
				int num = 0;
				Array array = shieldCharger04SpawnData;
				if (logic_SubGraph_RR_TeslaBase_BlasterTurretController_chargerSpawnData_231.Length != num + array.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_BlasterTurretController_chargerSpawnData_231, num + array.Length);
				}
				Array.Copy(array, 0, logic_SubGraph_RR_TeslaBase_BlasterTurretController_chargerSpawnData_231, num, array.Length);
				num += array.Length;
				int num2 = 0;
				Array array2 = blasterTurretGroup04SpawnData;
				if (logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretSpawnData_231.Length != num2 + array2.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretSpawnData_231, num2 + array2.Length);
				}
				Array.Copy(array2, 0, logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretSpawnData_231, num2, array2.Length);
				num2 += array2.Length;
				logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretShieldBlockType_231 = blasterTurretShieldBlockType;
				int num3 = 0;
				Array array3 = blasterTurretGroup04AlertZones;
				if (logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretAlertZones_231.Length != num3 + array3.Length)
				{
					Array.Resize(ref logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretAlertZones_231, num3 + array3.Length);
				}
				Array.Copy(array3, 0, logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretAlertZones_231, num3, array3.Length);
				num3 += array3.Length;
				logic_SubGraph_RR_TeslaBase_BlasterTurretController_SubGraph_RR_TeslaBase_BlasterTurretController_231.In(logic_SubGraph_RR_TeslaBase_BlasterTurretController_chargerSpawnData_231, logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretSpawnData_231, logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretShieldBlockType_231, logic_SubGraph_RR_TeslaBase_BlasterTurretController_turretAlertZones_231);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_RR_TeslaBase_BlasterTurretController.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_243()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2bd184b0-5853-4511-add0-c3fdbfbc71b6", "Compare_Bool", Relay_In_243))
			{
				logic_uScriptCon_CompareBool_Bool_243 = local_BlasterTurrets03Destroyed_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_243.In(logic_uScriptCon_CompareBool_Bool_243);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_243.True)
				{
					Relay_InitialSpawn_220();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_245()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("68cea585-c073-40b2-b84e-953269feebb0", "Compare_Bool", Relay_In_245))
			{
				logic_uScriptCon_CompareBool_Bool_245 = local_BlasterTurrets04Destroyed_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_245.In(logic_uScriptCon_CompareBool_Bool_245);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_245.True)
				{
					Relay_AtIndex_160();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_253()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b07d4a87-1700-492e-b558-924a43a6212e", "uScript_SpawnTechsFromData", Relay_InitialSpawn_253))
			{
				int num = 0;
				Array array = blasterTurretGroup03SpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_253.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_253, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_253, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_253 = owner_Connection_254;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_253.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_253, logic_uScript_SpawnTechsFromData_ownerNode_253, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_253, logic_uScript_SpawnTechsFromData_allowResurrection_253);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_253.Out)
				{
					Relay_InitialSpawn_21();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_257()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("37f53de4-d0be-4a0f-bac9-ed887f71b0b1", "uScript_SpawnTechsFromData", Relay_InitialSpawn_257))
			{
				int num = 0;
				Array array = blasterTurretGroup04SpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_257.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_257, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_257, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_257 = owner_Connection_256;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_257.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_257, logic_uScript_SpawnTechsFromData_ownerNode_257, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_257, logic_uScript_SpawnTechsFromData_allowResurrection_257);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_257.Out)
				{
					Relay_InitialSpawn_19();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_260()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("dcd14144-070b-4381-a55c-f18330018d80", "uScript_SetEncounterTargetPosition", Relay_In_260))
			{
				logic_uScript_SetEncounterTargetPosition_positionName_260 = objectiveMarkerPosRoom01;
				logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_260.In(logic_uScript_SetEncounterTargetPosition_positionName_260);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SetEncounterTargetPosition.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_262()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0de5e5c1-82f5-4d5d-94a3-64837215b183", "uScript_SetEncounterTargetPosition", Relay_In_262))
			{
				logic_uScript_SetEncounterTargetPosition_positionName_262 = objectiveMarkerPosRoom02;
				logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_262.In(logic_uScript_SetEncounterTargetPosition_positionName_262);
				if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_262.Out)
				{
					Relay_In_188();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SetEncounterTargetPosition.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_264()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("01d577b5-600c-48a4-a363-17f80dfb07d3", "uScript_SetEncounterTargetPosition", Relay_In_264))
			{
				logic_uScript_SetEncounterTargetPosition_positionName_264 = objectiveMarkerPosRoom03;
				logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_264.In(logic_uScript_SetEncounterTargetPosition_positionName_264);
				if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_264.Out)
				{
					Relay_In_190();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SetEncounterTargetPosition.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_266()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("70f10b79-a636-4877-89e1-41a3899f0fe5", "uScript_SetEncounterTargetPosition", Relay_In_266))
			{
				logic_uScript_SetEncounterTargetPosition_positionName_266 = objectiveMarkerPosRoom04;
				logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_266.In(logic_uScript_SetEncounterTargetPosition_positionName_266);
				if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_266.Out)
				{
					Relay_In_192();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SetEncounterTargetPosition.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_269()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9ce2e416-ff67-4849-a33a-fa07a0e1f395", "uScript_SetEncounterTarget", Relay_In_269))
			{
				logic_uScript_SetEncounterTarget_owner_269 = owner_Connection_267;
				logic_uScript_SetEncounterTarget_visibleObject_269 = local_NPCTech_Tank;
				logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_269.In(logic_uScript_SetEncounterTarget_owner_269, logic_uScript_SetEncounterTarget_visibleObject_269);
				if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_269.Out)
				{
					Relay_In_95();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SetEncounterTarget.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_274()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("67e16ef8-f49f-41de-b3c3-67a80037c903", "uScript_AddMessage", Relay_In_274))
			{
				logic_uScript_AddMessage_messageData_274 = msg02Room01Clear;
				logic_uScript_AddMessage_speaker_274 = NPCSpeaker;
				logic_uScript_AddMessage_Return_274 = logic_uScript_AddMessage_uScript_AddMessage_274.In(logic_uScript_AddMessage_messageData_274, logic_uScript_AddMessage_speaker_274);
				if (logic_uScript_AddMessage_uScript_AddMessage_274.Shown)
				{
					Relay_AtIndex_134();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_278()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f1c8a887-aac0-4322-bda8-a306d1ad3a04", "uScript_AddMessage", Relay_In_278))
			{
				logic_uScript_AddMessage_messageData_278 = msg03Room02Clear;
				logic_uScript_AddMessage_speaker_278 = NPCSpeaker;
				logic_uScript_AddMessage_Return_278 = logic_uScript_AddMessage_uScript_AddMessage_278.In(logic_uScript_AddMessage_messageData_278, logic_uScript_AddMessage_speaker_278);
				if (logic_uScript_AddMessage_uScript_AddMessage_278.Shown)
				{
					Relay_AtIndex_140();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_281()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b33b61bb-b996-4767-954b-8475ae6f9508", "uScript_AddMessage", Relay_In_281))
			{
				logic_uScript_AddMessage_messageData_281 = msg04Room03Clear;
				logic_uScript_AddMessage_speaker_281 = NPCSpeaker;
				logic_uScript_AddMessage_Return_281 = logic_uScript_AddMessage_uScript_AddMessage_281.In(logic_uScript_AddMessage_messageData_281, logic_uScript_AddMessage_speaker_281);
				if (logic_uScript_AddMessage_uScript_AddMessage_281.Shown)
				{
					Relay_AtIndex_146();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_282()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("de602663-caa6-4581-a3d4-342043a72225", "uScript_SpawnTechsFromData", Relay_InitialSpawn_282))
			{
				int num = 0;
				Array array = laserTurretGroup02SpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_282.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_282, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_282, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_282 = owner_Connection_289;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_282.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_282, logic_uScript_SpawnTechsFromData_ownerNode_282, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_282, logic_uScript_SpawnTechsFromData_allowResurrection_282);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_282.Out)
				{
					Relay_InitialSpawn_290();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_288()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9b8d211c-a509-4bb7-9d9c-b38a89338758", "uScript_SpawnTechsFromData", Relay_InitialSpawn_288))
			{
				int num = 0;
				Array array = teslaTurretGroup02SpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_288.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_288, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_288, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_288 = owner_Connection_283;
				logic_uScript_SpawnTechsFromData_delayBetweenSpawns_288 = telsaTurretGroup02SpawnInterval;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_288.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_288, logic_uScript_SpawnTechsFromData_ownerNode_288, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_288, logic_uScript_SpawnTechsFromData_allowResurrection_288);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_288.Out)
				{
					Relay_InitialSpawn_282();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_290()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ea439f86-3073-4c5e-8ddb-ae5bb6a47261", "uScript_SpawnTechsFromData", Relay_InitialSpawn_290))
			{
				int num = 0;
				Array array = shieldCharger02SpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_290.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_290, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_290, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_290 = owner_Connection_292;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_290.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_290, logic_uScript_SpawnTechsFromData_ownerNode_290, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_290, logic_uScript_SpawnTechsFromData_allowResurrection_290);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_290.Out)
				{
					Relay_In_262();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Pause_293()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f3d194e1-6a84-47c6-a3f7-a18e20c962af", "uScript_PausePopulation", Relay_Pause_293))
			{
				logic_uScript_PausePopulation_uScript_PausePopulation_293.Pause();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_PausePopulation.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_UnPause_293()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f3d194e1-6a84-47c6-a3f7-a18e20c962af", "uScript_PausePopulation", Relay_UnPause_293))
			{
				logic_uScript_PausePopulation_uScript_PausePopulation_293.UnPause();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_PausePopulation.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_294()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8f5d08b5-868c-4647-8b14-37db08d0e894", "uScript_PlayerInRangeOfCurrentEncounter", Relay_In_294))
			{
				logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_294 = owner_Connection_295;
				logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_294.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_294);
				bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_294.Out;
				bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_294.True;
				bool flag2 = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_294.False;
				if (num)
				{
					Relay_In_177();
				}
				if (flag)
				{
					Relay_Pause_293();
				}
				if (flag2)
				{
					Relay_UnPause_293();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_PlayerInRangeOfCurrentEncounter.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_296()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("38e19cbc-26a3-4838-a0c0-950093be01b6", "Add_Int", Relay_In_296))
			{
				logic_uScriptAct_AddInt_v2_A_296 = local_Objective02SubStage_System_Int32;
				logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_296.In(logic_uScriptAct_AddInt_v2_A_296, logic_uScriptAct_AddInt_v2_B_296, out logic_uScriptAct_AddInt_v2_IntResult_296, out logic_uScriptAct_AddInt_v2_FloatResult_296);
				local_Objective02SubStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_296;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Add Int.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_298()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6a854840-dd86-414b-b92d-394c52319a53", "Add_Int", Relay_In_298))
			{
				logic_uScriptAct_AddInt_v2_A_298 = local_Objective02SubStage_System_Int32;
				logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_298.In(logic_uScriptAct_AddInt_v2_A_298, logic_uScriptAct_AddInt_v2_B_298, out logic_uScriptAct_AddInt_v2_IntResult_298, out logic_uScriptAct_AddInt_v2_FloatResult_298);
				local_Objective02SubStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_298;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Add Int.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_300()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cc15586c-dd97-43a4-9b7f-2836620f29ef", "Add_Int", Relay_In_300))
			{
				logic_uScriptAct_AddInt_v2_A_300 = local_Objective02SubStage_System_Int32;
				logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_300.In(logic_uScriptAct_AddInt_v2_A_300, logic_uScriptAct_AddInt_v2_B_300, out logic_uScriptAct_AddInt_v2_IntResult_300, out logic_uScriptAct_AddInt_v2_FloatResult_300);
				local_Objective02SubStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_300;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Add Int.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_302()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7caee585-5243-4ced-92cf-ade57f2b7ead", "uScript_GetAndCheckTechs", Relay_In_302))
			{
				int num = 0;
				Array array = shieldCharger01SpawnData;
				if (logic_uScript_GetAndCheckTechs_techData_302.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_302, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_302, num, array.Length);
				num += array.Length;
				logic_uScript_GetAndCheckTechs_ownerNode_302 = owner_Connection_303;
				logic_uScript_GetAndCheckTechs_Return_302 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_302.In(logic_uScript_GetAndCheckTechs_techData_302, logic_uScript_GetAndCheckTechs_ownerNode_302, ref logic_uScript_GetAndCheckTechs_techs_302);
				if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_302.AllDead)
				{
					Relay_In_306();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_GetAndCheckTechs.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_306()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("aeaa1023-82a8-4a14-a898-0e67cc2a121e", "Compare_Bool", Relay_In_306))
			{
				logic_uScriptCon_CompareBool_Bool_306 = local_msgTeslaBatteryCharge_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_306.In(logic_uScriptCon_CompareBool_Bool_306);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_306.False)
				{
					Relay_True_310();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_309()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e4b1a82a-e58c-43fb-9df7-a4481f9c7f23", "uScript_AddMessage", Relay_In_309))
			{
				logic_uScript_AddMessage_messageData_309 = msg02TeslaBatteryCharge;
				logic_uScript_AddMessage_speaker_309 = NPCSpeaker;
				logic_uScript_AddMessage_Return_309 = logic_uScript_AddMessage_uScript_AddMessage_309.In(logic_uScript_AddMessage_messageData_309, logic_uScript_AddMessage_speaker_309);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_310()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e22b82a8-522b-485b-b1a5-c7f2cee871ab", "Set_Bool", Relay_True_310))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_310.True(out logic_uScriptAct_SetBool_Target_310);
				local_msgTeslaBatteryCharge_System_Boolean = logic_uScriptAct_SetBool_Target_310;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_310.Out)
				{
					Relay_In_309();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_310()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e22b82a8-522b-485b-b1a5-c7f2cee871ab", "Set_Bool", Relay_False_310))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_310.False(out logic_uScriptAct_SetBool_Target_310);
				local_msgTeslaBatteryCharge_System_Boolean = logic_uScriptAct_SetBool_Target_310;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_310.Out)
				{
					Relay_In_309();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_312()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ff46a23d-a3e5-43f6-aab1-ab946ff77192", "", Relay_Save_Out_312))
			{
				Relay_Save_335();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_312()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ff46a23d-a3e5-43f6-aab1-ab946ff77192", "", Relay_Load_Out_312))
			{
				Relay_Load_335();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_312()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ff46a23d-a3e5-43f6-aab1-ab946ff77192", "", Relay_Restart_Out_312))
			{
				Relay_Set_False_335();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_312()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ff46a23d-a3e5-43f6-aab1-ab946ff77192", "", Relay_Save_312))
			{
				logic_SubGraph_SaveLoadBool_boolean_312 = local_msgTeslaBatteryCharge_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_312 = local_msgTeslaBatteryCharge_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.Save(ref logic_SubGraph_SaveLoadBool_boolean_312, logic_SubGraph_SaveLoadBool_boolAsVariable_312, logic_SubGraph_SaveLoadBool_uniqueID_312);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_312()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ff46a23d-a3e5-43f6-aab1-ab946ff77192", "", Relay_Load_312))
			{
				logic_SubGraph_SaveLoadBool_boolean_312 = local_msgTeslaBatteryCharge_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_312 = local_msgTeslaBatteryCharge_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.Load(ref logic_SubGraph_SaveLoadBool_boolean_312, logic_SubGraph_SaveLoadBool_boolAsVariable_312, logic_SubGraph_SaveLoadBool_uniqueID_312);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_312()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ff46a23d-a3e5-43f6-aab1-ab946ff77192", "", Relay_Set_True_312))
			{
				logic_SubGraph_SaveLoadBool_boolean_312 = local_msgTeslaBatteryCharge_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_312 = local_msgTeslaBatteryCharge_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_312, logic_SubGraph_SaveLoadBool_boolAsVariable_312, logic_SubGraph_SaveLoadBool_uniqueID_312);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_312()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ff46a23d-a3e5-43f6-aab1-ab946ff77192", "", Relay_Set_False_312))
			{
				logic_SubGraph_SaveLoadBool_boolean_312 = local_msgTeslaBatteryCharge_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_312 = local_msgTeslaBatteryCharge_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_312.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_312, logic_SubGraph_SaveLoadBool_boolAsVariable_312, logic_SubGraph_SaveLoadBool_uniqueID_312);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_314()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2d7dde65-7feb-419d-991a-917d5a272ff4", "uScript_DirectEnemiesOutOfEncounter", Relay_In_314))
			{
				logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_314 = owner_Connection_315;
				logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_314.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_314);
				if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_314.Out)
				{
					Relay_In_294();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_DirectEnemiesOutOfEncounter.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_317()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("89bac105-0b55-4147-9211-83481c8e647e", "Distance_Player_In_Range", Relay_In_317))
			{
				logic_uScript_PlayerInRange_position_317 = local_321_UnityEngine_Vector3;
				logic_uScript_PlayerInRange_range_317 = initialTechSpawnCheck_Distance;
				logic_uScript_PlayerInRange_uScript_PlayerInRange_317.In(logic_uScript_PlayerInRange_position_317, logic_uScript_PlayerInRange_range_317);
				if (logic_uScript_PlayerInRange_uScript_PlayerInRange_317.True)
				{
					Relay_InitialSpawn_10();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Distance/Player In Range.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_318()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("504a965d-b078-4b54-b2db-523ab071ba3a", "uScript_GetPositionInEncounter", Relay_In_318))
			{
				logic_uScript_GetPositionInEncounter_ownerNode_318 = owner_Connection_319;
				logic_uScript_GetPositionInEncounter_posName_318 = initialTechSpawnCheck_Position;
				logic_uScript_GetPositionInEncounter_Return_318 = logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_318.In(logic_uScript_GetPositionInEncounter_ownerNode_318, logic_uScript_GetPositionInEncounter_posName_318);
				local_321_UnityEngine_Vector3 = logic_uScript_GetPositionInEncounter_Return_318;
				if (logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_318.Out)
				{
					Relay_In_317();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at uScript_GetPositionInEncounter.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_324()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("81a211b1-3cbb-4835-a264-e3e147f4ab81", "Compare_Bool", Relay_In_324))
			{
				logic_uScriptCon_CompareBool_Bool_324 = local_Init_EnemyTurrets_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_324.In(logic_uScriptCon_CompareBool_Bool_324);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_324.False)
				{
					Relay_In_318();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_326()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4e454204-fa67-4c91-a946-a8e160dce34e", "Set_Bool", Relay_True_326))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_326.True(out logic_uScriptAct_SetBool_Target_326);
				local_Init_EnemyTurrets_System_Boolean = logic_uScriptAct_SetBool_Target_326;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_326()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4e454204-fa67-4c91-a946-a8e160dce34e", "Set_Bool", Relay_False_326))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_326.False(out logic_uScriptAct_SetBool_Target_326);
				local_Init_EnemyTurrets_System_Boolean = logic_uScriptAct_SetBool_Target_326;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_327()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d1ca3ef6-9eaf-4362-9101-59301ea3a891", "", Relay_Save_Out_327))
			{
				Relay_Save_312();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_327()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d1ca3ef6-9eaf-4362-9101-59301ea3a891", "", Relay_Load_Out_327))
			{
				Relay_Load_312();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_327()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d1ca3ef6-9eaf-4362-9101-59301ea3a891", "", Relay_Restart_Out_327))
			{
				Relay_Set_False_312();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_327()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d1ca3ef6-9eaf-4362-9101-59301ea3a891", "", Relay_Save_327))
			{
				logic_SubGraph_SaveLoadBool_boolean_327 = local_Init_EnemyTurrets_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_327 = local_Init_EnemyTurrets_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Save(ref logic_SubGraph_SaveLoadBool_boolean_327, logic_SubGraph_SaveLoadBool_boolAsVariable_327, logic_SubGraph_SaveLoadBool_uniqueID_327);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_327()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d1ca3ef6-9eaf-4362-9101-59301ea3a891", "", Relay_Load_327))
			{
				logic_SubGraph_SaveLoadBool_boolean_327 = local_Init_EnemyTurrets_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_327 = local_Init_EnemyTurrets_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Load(ref logic_SubGraph_SaveLoadBool_boolean_327, logic_SubGraph_SaveLoadBool_boolAsVariable_327, logic_SubGraph_SaveLoadBool_uniqueID_327);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_327()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d1ca3ef6-9eaf-4362-9101-59301ea3a891", "", Relay_Set_True_327))
			{
				logic_SubGraph_SaveLoadBool_boolean_327 = local_Init_EnemyTurrets_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_327 = local_Init_EnemyTurrets_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_327, logic_SubGraph_SaveLoadBool_boolAsVariable_327, logic_SubGraph_SaveLoadBool_uniqueID_327);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_327()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d1ca3ef6-9eaf-4362-9101-59301ea3a891", "", Relay_Set_False_327))
			{
				logic_SubGraph_SaveLoadBool_boolean_327 = local_Init_EnemyTurrets_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_327 = local_Init_EnemyTurrets_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_327.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_327, logic_SubGraph_SaveLoadBool_boolAsVariable_327, logic_SubGraph_SaveLoadBool_uniqueID_327);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_329()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("241432db-21cb-49bc-9369-e0f95c724752", "Pass", Relay_In_329))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_329.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_329.Out)
				{
					Relay_In_81();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_331()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("39e6c6c1-d1c9-461c-9ab7-1253ab039880", "Pass", Relay_In_331))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_331.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_331.Out)
				{
					Relay_In_124();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_332()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e102a93a-319e-45ce-b319-bf6138c7e86f", "Distance_Is_player_in_range_of_tech", Relay_In_332))
			{
				logic_uScript_IsPlayerInRangeOfTech_tech_332 = local_NPCTech_Tank;
				logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_332.In(logic_uScript_IsPlayerInRangeOfTech_tech_332, logic_uScript_IsPlayerInRangeOfTech_range_332, logic_uScript_IsPlayerInRangeOfTech_techs_332);
				bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_332.InRange;
				bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_332.OutOfRange;
				if (inRange)
				{
					Relay_In_71();
				}
				if (outOfRange)
				{
					Relay_In_329();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at Distance/Is player in range of tech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_335()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("60f8d1d9-f9c9-4559-9441-3b38091a6f04", "", Relay_Save_Out_335))
			{
				Relay_Save_207();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_335()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("60f8d1d9-f9c9-4559-9441-3b38091a6f04", "", Relay_Load_Out_335))
			{
				Relay_Load_207();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_335()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("60f8d1d9-f9c9-4559-9441-3b38091a6f04", "", Relay_Restart_Out_335))
			{
				Relay_Restart_207();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_335()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("60f8d1d9-f9c9-4559-9441-3b38091a6f04", "", Relay_Save_335))
			{
				logic_SubGraph_SaveLoadBool_boolean_335 = local_MsgIntroShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_335 = local_MsgIntroShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.Save(ref logic_SubGraph_SaveLoadBool_boolean_335, logic_SubGraph_SaveLoadBool_boolAsVariable_335, logic_SubGraph_SaveLoadBool_uniqueID_335);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_335()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("60f8d1d9-f9c9-4559-9441-3b38091a6f04", "", Relay_Load_335))
			{
				logic_SubGraph_SaveLoadBool_boolean_335 = local_MsgIntroShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_335 = local_MsgIntroShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.Load(ref logic_SubGraph_SaveLoadBool_boolean_335, logic_SubGraph_SaveLoadBool_boolAsVariable_335, logic_SubGraph_SaveLoadBool_uniqueID_335);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_335()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("60f8d1d9-f9c9-4559-9441-3b38091a6f04", "", Relay_Set_True_335))
			{
				logic_SubGraph_SaveLoadBool_boolean_335 = local_MsgIntroShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_335 = local_MsgIntroShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_335, logic_SubGraph_SaveLoadBool_boolAsVariable_335, logic_SubGraph_SaveLoadBool_uniqueID_335);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_335()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("60f8d1d9-f9c9-4559-9441-3b38091a6f04", "", Relay_Set_False_335))
			{
				logic_SubGraph_SaveLoadBool_boolean_335 = local_MsgIntroShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_335 = local_MsgIntroShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_335.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_335, logic_SubGraph_SaveLoadBool_boolAsVariable_335, logic_SubGraph_SaveLoadBool_uniqueID_335);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_RR_TeslaBase.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void UpdateEditorValues()
	{
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:laserTurretGroup02AnimTriggers", laserTurretGroup02AnimTriggers);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("b89df39b-f204-4e1b-89f4-1c9f474b2e32", laserTurretGroup02AnimTriggers);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:laserTurretGroup02AnimDurations", laserTurretGroup02AnimDurations);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("ac1a8e63-f625-4e11-b3fe-b74cb87f742e", laserTurretGroup02AnimDurations);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:teslaTurretGroup01SpawnData", teslaTurretGroup01SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("c6070898-0ad8-4405-be99-83d32139e36f", teslaTurretGroup01SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:laserTurretGroup03AnimTriggers", laserTurretGroup03AnimTriggers);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("826b95dc-bc80-4fcb-b443-2d2ec523679c", laserTurretGroup03AnimTriggers);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:laserTurretGroup03AnimDurations", laserTurretGroup03AnimDurations);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("6b1d6db9-7471-4614-a45c-0a2b7645dc3e", laserTurretGroup03AnimDurations);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:laserTurretGroup04AnimTriggers", laserTurretGroup04AnimTriggers);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("7d90762d-d93b-4f6e-9d36-759b3de2d7b2", laserTurretGroup04AnimTriggers);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:teslaTurretShieldBlockType", teslaTurretShieldBlockType);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("a561f196-a397-4be5-9029-e53e2fc45dcc", teslaTurretShieldBlockType);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:laserTurretShieldBlockType", laserTurretShieldBlockType);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("fc491b24-e6b7-44b5-b4eb-3bd57cd97a0d", laserTurretShieldBlockType);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:laserTurretWeaponBlockType", laserTurretWeaponBlockType);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("1a866ac0-f061-4364-b9a2-a5d05d8b5728", laserTurretWeaponBlockType);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:laserTurretGroup04AnimDurations", laserTurretGroup04AnimDurations);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("cbde40c6-22ea-495e-b009-8cc9e3234735", laserTurretGroup04AnimDurations);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:NPCSpawnData02", NPCSpawnData02);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("eb9bbf77-04a5-49eb-a954-14b7fb5417ed", NPCSpawnData02);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:NPCTriggerVolume02", NPCTriggerVolume02);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("6056ef72-d42b-45c8-be71-ee5e37114b94", NPCTriggerVolume02);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:msg06Outro", msg06Outro);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("55d535df-5a36-46ac-a935-696b1dbdbc48", msg06Outro);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:NPCDespawnEffect", NPCDespawnEffect);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("4b9da5c5-1311-4fd6-83f6-bacd867f0b86", NPCDespawnEffect);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:room01TriggerVolume", room01TriggerVolume);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("9365add1-1507-483e-b623-7971f1500450", room01TriggerVolume);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:msg05Room04Clear", msg05Room04Clear);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("212ad44e-b5e8-442e-95c9-95daaffaa06f", msg05Room04Clear);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:NPCTriggerVolume01", NPCTriggerVolume01);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("b1c8d9b5-e774-4be8-a0e5-a55c51cafb2f", NPCTriggerVolume01);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:MsgIntro", local_MsgIntro_ManOnScreenMessages_OnScreenMessage);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("2172f9ea-4a20-43a0-9dc4-51c56d2932e4", local_MsgIntro_ManOnScreenMessages_OnScreenMessage);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:msg01Intro", msg01Intro);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("1175f2df-5275-44b8-a563-fc86c38ad3ad", msg01Intro);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:NPCTech02", local_NPCTech02_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("b4ec1fe7-6dc4-44c0-a8f7-6f6ba475d56c", local_NPCTech02_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:127", local_127_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("8b4e3c53-ee58-417a-bfc9-c20b275816fc", local_127_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:TeslaTurrets01Destroyed", local_TeslaTurrets01Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("9c6bec70-a161-4cb3-87a3-192b5be80827", local_TeslaTurrets01Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:136", local_136_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("fe6276f9-cec4-45d3-8b9a-032dddd8b54b", local_136_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:138", local_138_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("e76ced5b-84e8-46fb-8cbd-93be78b4dc1e", local_138_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:LaserTurrets02Destroyed", local_LaserTurrets02Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("8518eaef-57d7-4613-b104-d26f551636a9", local_LaserTurrets02Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:TeslaTurrets02Destroyed", local_TeslaTurrets02Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("ebec2208-e4ba-48cc-88ed-d4eecb8411d0", local_TeslaTurrets02Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:LaserTurrets03Destroyed", local_LaserTurrets03Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("aa351eee-c0cc-4ea3-b0f3-782d69afcdf7", local_LaserTurrets03Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:151", local_151_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("cbdb5666-1a47-4a98-968e-6ba44383243c", local_151_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:TeslaTurrets03Destroyed", local_TeslaTurrets03Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("057403c7-33d1-4d97-9985-ed83319e7e80", local_TeslaTurrets03Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:156", local_156_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("1087ac6d-81dd-46db-ade4-94901a7c6054", local_156_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:LaserTurrets04Destroyed", local_LaserTurrets04Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("e8c6f739-62f8-485c-ace5-784beb36498e", local_LaserTurrets04Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:162", local_162_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("99c907b3-f327-4949-80db-073bdedd18a3", local_162_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:TeslaTurrets04Destroyed", local_TeslaTurrets04Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("f2e84611-a0fc-46fb-a782-490973f1eee4", local_TeslaTurrets04Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:170", local_170_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("c6538f9c-a78d-4617-974b-b95d6d12eb15", local_170_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:NPCSpawnData01", NPCSpawnData01);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("1d201c95-f401-4ae0-a5d0-7bbe7be09157", NPCSpawnData01);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:ForcefieldGatesSpawnData", ForcefieldGatesSpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("6ce9b04e-2743-4eca-8eeb-e15790394777", ForcefieldGatesSpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:Room04ForcefieldsSpawnData", Room04ForcefieldsSpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("c3875b34-b1ec-46ba-8377-8d96d88ce1fe", Room04ForcefieldsSpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:183", local_183_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("befc7d81-7213-46d6-81ab-8ab99e461a03", local_183_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:ForcefieldGates", local_ForcefieldGates_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("ac111818-ba42-4e94-9cc0-432ebfd6619f", local_ForcefieldGates_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:194", local_194_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("45e9893d-41b4-4e1c-acf5-45be724946c1", local_194_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:Room04Forcefields", local_Room04Forcefields_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("31441d6f-7d88-4248-8996-4a8cd273932b", local_Room04Forcefields_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:CurrentObjective", local_CurrentObjective_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("4486a89b-b4c6-404b-a1c9-2aeadadbc0e7", local_CurrentObjective_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:laserTurretGroup03SpawnData", laserTurretGroup03SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("1f0633ab-cbb7-4f0d-8e42-6743e8b3128b", laserTurretGroup03SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:teslaTurretGroup03SpawnData", teslaTurretGroup03SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("53d1b66d-aedf-4750-8955-22d3cfb7035e", teslaTurretGroup03SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:teslaTurretGroup04SpawnData", teslaTurretGroup04SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("7faa4900-acbb-4b53-9a3c-a81184b198c3", teslaTurretGroup04SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:laserTurretGroup04SpawnData", laserTurretGroup04SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("90b3f35c-d7d2-419a-a97a-537f7985c2a5", laserTurretGroup04SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:shieldCharger03SpawnData", shieldCharger03SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("3b64de7e-1bd7-4fd0-a2c6-e84e8ae6d6e8", shieldCharger03SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:blasterTurretGroup03AlertZones", blasterTurretGroup03AlertZones);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("19389766-cef4-4f17-8232-214916b1bff2", blasterTurretGroup03AlertZones);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:blasterTurretShieldBlockType", blasterTurretShieldBlockType);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("82704c82-2c5b-4a90-a1ad-ed33dee06373", blasterTurretShieldBlockType);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:blasterTurretGroup04AlertZones", blasterTurretGroup04AlertZones);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("c93e1d4c-0f9d-4cea-b018-c1cf9ad7b46d", blasterTurretGroup04AlertZones);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:shieldCharger04SpawnData", shieldCharger04SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("23dea63c-dd26-4a5e-a7fd-ff5361c90789", shieldCharger04SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:BlasterTurrets03Destroyed", local_BlasterTurrets03Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("2fb87305-cff5-4d3e-a415-3d28c9e03bda", local_BlasterTurrets03Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:BlasterTurrets04Destroyed", local_BlasterTurrets04Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("f0d6a298-bffb-48cc-8874-5d42c926011d", local_BlasterTurrets04Destroyed_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:blasterTurretGroup03SpawnData", blasterTurretGroup03SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("0868ab15-1d68-4510-8d27-7879ea1fe09a", blasterTurretGroup03SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:blasterTurretGroup04SpawnData", blasterTurretGroup04SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("41192e5a-d2d9-4c92-8feb-4c0047391262", blasterTurretGroup04SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:Init", local_Init_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("00b3c90e-ce23-4a29-82fc-0024ad228b43", local_Init_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:objectiveMarkerPosRoom01", objectiveMarkerPosRoom01);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("2fba1ea8-6445-4eed-98ae-f1ab92d33735", objectiveMarkerPosRoom01);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:objectiveMarkerPosRoom02", objectiveMarkerPosRoom02);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("68b35281-be7d-4d2d-9d84-3357367ef49c", objectiveMarkerPosRoom02);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:objectiveMarkerPosRoom03", objectiveMarkerPosRoom03);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("00f81540-2d45-42aa-a45a-491d6ece17b2", objectiveMarkerPosRoom03);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:objectiveMarkerPosRoom04", objectiveMarkerPosRoom04);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("9e2ef547-389c-4b25-a983-16510dc7badd", objectiveMarkerPosRoom04);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:telsaTurretGroup01SpawnInterval", telsaTurretGroup01SpawnInterval);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("44673d3a-0502-48ec-b303-996027ffd4d3", telsaTurretGroup01SpawnInterval);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:telsaTurretGroup03SpawnInterval", telsaTurretGroup03SpawnInterval);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("d1b42b27-6541-4855-86e0-837e1f158b8a", telsaTurretGroup03SpawnInterval);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:telsaTurretGroup04SpawnInterval", telsaTurretGroup04SpawnInterval);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("2a1858ef-32fb-4be5-b74e-969f82d16846", telsaTurretGroup04SpawnInterval);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:msg02Room01Clear", msg02Room01Clear);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("27d725fa-6da8-45e2-ae4f-8e3c39f77c0e", msg02Room01Clear);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:msg03Room02Clear", msg03Room02Clear);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("17a575c0-bf5e-4301-ac9f-ed8019bf5057", msg03Room02Clear);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:msg04Room03Clear", msg04Room03Clear);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("4688ced3-7907-437b-bcfe-0455c270bdc2", msg04Room03Clear);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:telsaTurretGroup02SpawnInterval", telsaTurretGroup02SpawnInterval);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("97658f3d-218e-475e-845c-89c8550ae6e8", telsaTurretGroup02SpawnInterval);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:laserTurretGroup02SpawnData", laserTurretGroup02SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("0e96ee03-29ea-4dea-9117-e33d4b0989fc", laserTurretGroup02SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:teslaTurretGroup02SpawnData", teslaTurretGroup02SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("940079c2-2854-46a7-a3be-4dc4e4dea54b", teslaTurretGroup02SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:shieldCharger02SpawnData", shieldCharger02SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("82daef7b-f929-444f-abb1-fdd473acda80", shieldCharger02SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:Objective02SubStage", local_Objective02SubStage_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("cc0e1799-0eb7-412a-a980-92087156a36b", local_Objective02SubStage_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:shieldCharger01SpawnData", shieldCharger01SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("f376bc28-41a3-48b6-99eb-37aecfb77e56", shieldCharger01SpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:msg02TeslaBatteryCharge", msg02TeslaBatteryCharge);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("d555eaee-2068-4c14-8c51-e37e8a249ae2", msg02TeslaBatteryCharge);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:NPCSpeaker", NPCSpeaker);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("7d6ad242-1ecc-439b-88e8-d1063ce43886", NPCSpeaker);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:msgTeslaBatteryCharge", local_msgTeslaBatteryCharge_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("f6fd00de-c082-4e39-a6bb-c8517d86ef55", local_msgTeslaBatteryCharge_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:initialTechSpawnCheck_Position", initialTechSpawnCheck_Position);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("04a0212f-9ad8-4d2d-ba4c-45813bace2cd", initialTechSpawnCheck_Position);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:321", local_321_UnityEngine_Vector3);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("67aaf12f-a3db-4aba-9dee-091c5a36f36a", local_321_UnityEngine_Vector3);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:initialTechSpawnCheck_Distance", initialTechSpawnCheck_Distance);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("9f335e28-0730-41b1-80dc-9d2f555fe480", initialTechSpawnCheck_Distance);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:Init_EnemyTurrets", local_Init_EnemyTurrets_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("e71bbaac-da4d-4c90-a735-a79def210160", local_Init_EnemyTurrets_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:NPCTech", local_NPCTech_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("79874605-1dbf-4a64-bda5-4a84cfd84309", local_NPCTech_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_RR_TeslaBase.uscript:MsgIntroShown", local_MsgIntroShown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("95d77df2-b062-4c14-92bc-997dee98313e", local_MsgIntroShown_System_Boolean);
	}

	private bool CheckDebugBreak(string guid, string name, ContinueExecution method)
	{
		if (m_Breakpoint)
		{
			return true;
		}
		if (uScript_MasterComponent.FindBreakpoint(guid))
		{
			if (!(uScript_MasterComponent.LatestMasterComponent.CurrentBreakpoint == guid))
			{
				uScript_MasterComponent.LatestMasterComponent.CurrentBreakpoint = guid;
				UpdateEditorValues();
				Debug.Log(("uScript BREAK Node:" + name + " ((Time: " + Time.time) ?? "");
				Debug.Break();
				m_ContinueExecution = method.Invoke;
				m_Breakpoint = true;
				return true;
			}
			uScript_MasterComponent.LatestMasterComponent.CurrentBreakpoint = "";
		}
		return false;
	}
}
