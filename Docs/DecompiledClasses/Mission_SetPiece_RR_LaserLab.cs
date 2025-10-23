using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_SetPiece_RR_LaserLab : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public SpawnTechData[] charger01SpawnData = new SpawnTechData[0];

	public SpawnTechData[] charger02SpawnData = new SpawnTechData[0];

	public SpawnTechData[] charger03SpawnData = new SpawnTechData[0];

	[Multiline(1)]
	public string chargerMsgTriggerVolume = "";

	public SpawnTechData[] forcefieldSpawnData = new SpawnTechData[0];

	private Tank[] local_139_TankArray = new Tank[0];

	private Tank[] local_152_TankArray = new Tank[0];

	private Tank[] local_159_TankArray = new Tank[0];

	private Tank[] local_16_TankArray = new Tank[0];

	private Tank[] local_209_TankArray = new Tank[0];

	private Tank[] local_285_TankArray = new Tank[0];

	private int local_CurrentObjective_System_Int32 = 1;

	private string local_CurrentSwitch_System_String = "";

	private int local_CurrentSwitchIndex_System_Int32;

	private Tank local_ForcefieldTech_Tank;

	private bool local_Init_System_Boolean;

	private bool local_MsgChargerShown_System_Boolean;

	private bool local_MsgFirstRoomClearShown_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_MsgIntro_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgMissionComplete_ManOnScreenMessages_OnScreenMessage;

	private Tank local_NPCTech_Tank;

	private Tank local_NPCTech02_Tank;

	private int local_NumChargerMessagesShown_System_Int32;

	private int local_NumChargersDestroyed_System_Int32;

	private int local_NumGatesOpened_System_Int32;

	private bool local_SetShutterStates_System_Boolean;

	private bool local_Turrets01Destroyed_System_Boolean;

	private bool local_Turrets02Destroyed_System_Boolean;

	private bool local_Turrets03Destroyed_System_Boolean;

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg01IntroMP;

	public uScript_AddMessage.MessageData msg02aShieldsDown;

	public uScript_AddMessage.MessageData msg02bShieldsDown;

	public uScript_AddMessage.MessageData msg02cShieldsDown;

	public uScript_AddMessage.MessageData msg02FirstRoomClear;

	public uScript_AddMessage.MessageData msg02ShieldChargerInfo;

	public uScript_AddMessage.MessageData msg03ReturnToNPC;

	public uScript_AddMessage.MessageData msg04MissionComplete;

	public Transform NPCDespawnEffect;

	public SpawnTechData[] npcSpawnData = new SpawnTechData[0];

	public SpawnTechData[] npcSpawnData02 = new SpawnTechData[0];

	public uScript_AddMessage.MessageSpeaker npcSpeaker;

	[Multiline(1)]
	public string NPCTriggerVolume01 = "";

	[Multiline(1)]
	public string NPCTriggerVolume02 = "";

	[Multiline(1)]
	public string objectiveMarkerPosition = "";

	[Multiline(1)]
	public string room01TriggerVolume = "";

	[Multiline(1)]
	public string room02TriggerVolume = "";

	[Multiline(1)]
	public string switch01TriggerVolume = "";

	[Multiline(1)]
	public string switch02TriggerVolume = "";

	[Multiline(1)]
	public string switch03TriggerVolume = "";

	[Multiline(1)]
	public string[] switchObjectNames = new string[0];

	public float[] turretGroup01AnimDurations = new float[0];

	[Multiline(1)]
	public string[] turretGroup01AnimTriggers = new string[0];

	public SpawnTechData[] turretGroup01SpawnData = new SpawnTechData[0];

	public float[] turretGroup02AnimDurations = new float[0];

	[Multiline(1)]
	public string[] turretGroup02AnimTriggers = new string[0];

	public SpawnTechData[] turretGroup02SpawnData = new SpawnTechData[0];

	public float[] turretGroup03AnimDurations = new float[0];

	[Multiline(1)]
	public string[] turretGroup03AnimTriggers = new string[0];

	public SpawnTechData[] turretGroup03SpawnData = new SpawnTechData[0];

	public BlockTypes turretShieldBlockType;

	public BlockTypes turretWeaponBlockType;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_10;

	private GameObject owner_Connection_13;

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_28;

	private GameObject owner_Connection_31;

	private GameObject owner_Connection_36;

	private GameObject owner_Connection_69;

	private GameObject owner_Connection_74;

	private GameObject owner_Connection_76;

	private GameObject owner_Connection_78;

	private GameObject owner_Connection_90;

	private GameObject owner_Connection_92;

	private GameObject owner_Connection_140;

	private GameObject owner_Connection_150;

	private GameObject owner_Connection_156;

	private GameObject owner_Connection_167;

	private GameObject owner_Connection_205;

	private GameObject owner_Connection_207;

	private GameObject owner_Connection_210;

	private GameObject owner_Connection_230;

	private GameObject owner_Connection_280;

	private GameObject owner_Connection_286;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_3;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_3 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_3 = "Init";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_4;

	private bool logic_uScriptCon_CompareBool_True_4 = true;

	private bool logic_uScriptCon_CompareBool_False_4 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_7 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_7;

	private bool logic_uScriptAct_SetBool_Out_7 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_7 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_7 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_9 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_9 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_9;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_9 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_9;

	private bool logic_uScript_SpawnTechsFromData_Out_9 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_12 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_12 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_12;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_12 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_12;

	private bool logic_uScript_SpawnTechsFromData_Out_12 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_15 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_15 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_15;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_15 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_15;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_15 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_15 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_15 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_15 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_18 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_18 = new Tank[0];

	private int logic_uScript_AccessListTech_index_18;

	private Tank logic_uScript_AccessListTech_value_18;

	private bool logic_uScript_AccessListTech_Out_18 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_20;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_22 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_22;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_22 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_22 = "CurrentObjective";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_24;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_26;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_26;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_29 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_29 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_29;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_29 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_29;

	private bool logic_uScript_SpawnTechsFromData_Out_29 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_32 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_32 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_32;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_32 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_32;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_32 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_32 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_32 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_32 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_34 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_34 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_35 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_35;

	private bool logic_uScript_FinishEncounter_Out_35 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_39 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_39;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_39;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_39;

	private bool logic_uScript_AddMessage_Out_39 = true;

	private bool logic_uScript_AddMessage_Shown_39 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_43;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_43;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_46 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_46;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_46;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_46;

	private bool logic_uScript_AddMessage_Out_46 = true;

	private bool logic_uScript_AddMessage_Shown_46 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_50 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_50;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_50;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_50;

	private bool logic_uScript_AddMessage_Out_50 = true;

	private bool logic_uScript_AddMessage_Shown_50 = true;

	private SubGraph_RR_LaserLab_TurretAndChargerController logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_52 = new SubGraph_RR_LaserLab_TurretAndChargerController();

	private SpawnTechData[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_52 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_52 = new SpawnTechData[0];

	private string[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_52 = new string[0];

	private float[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_52 = new float[0];

	private BlockTypes logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_52;

	private BlockTypes logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_52;

	private bool logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_52;

	private SubGraph_RR_LaserLab_TurretAndChargerController logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_59 = new SubGraph_RR_LaserLab_TurretAndChargerController();

	private SpawnTechData[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_59 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_59 = new SpawnTechData[0];

	private string[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_59 = new string[0];

	private float[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_59 = new float[0];

	private BlockTypes logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_59;

	private BlockTypes logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_59;

	private bool logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_59;

	private SubGraph_RR_LaserLab_TurretAndChargerController logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_66 = new SubGraph_RR_LaserLab_TurretAndChargerController();

	private SpawnTechData[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_66 = new SpawnTechData[0];

	private SpawnTechData[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_66 = new SpawnTechData[0];

	private string[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_66 = new string[0];

	private float[] logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_66 = new float[0];

	private BlockTypes logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_66;

	private BlockTypes logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_66;

	private bool logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_66;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_72 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_72 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_72;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_72 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_72;

	private bool logic_uScript_SpawnTechsFromData_Out_72 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_73 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_73 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_73;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_73 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_73;

	private bool logic_uScript_SpawnTechsFromData_Out_73 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_75 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_75 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_75;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_75 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_75;

	private bool logic_uScript_SpawnTechsFromData_Out_75 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_79 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_79 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_79;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_79 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_79;

	private bool logic_uScript_SpawnTechsFromData_Out_79 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_81;

	private bool logic_uScriptCon_CompareBool_True_81 = true;

	private bool logic_uScriptCon_CompareBool_False_81 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_83;

	private bool logic_uScriptCon_CompareBool_True_83 = true;

	private bool logic_uScriptCon_CompareBool_False_83 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_86;

	private bool logic_uScriptCon_CompareBool_True_86 = true;

	private bool logic_uScriptCon_CompareBool_False_86 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_87 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_88 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_88 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_89 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_89 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_91 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_91 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_91;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_91 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_91;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_91 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_91 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_91 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_91 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_94 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_94 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_94;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_94 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_94;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_94 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_94 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_94 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_94 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_103;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_105 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_105;

	private int logic_uScriptAct_AddInt_v2_B_105 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_105;

	private float logic_uScriptAct_AddInt_v2_FloatResult_105;

	private bool logic_uScriptAct_AddInt_v2_Out_105 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_107 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_107;

	private int logic_uScriptAct_SetInt_Target_107;

	private bool logic_uScriptAct_SetInt_Out_107 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_109 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_109;

	private int logic_uScriptAct_AddInt_v2_B_109 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_109;

	private float logic_uScriptAct_AddInt_v2_FloatResult_109;

	private bool logic_uScriptAct_AddInt_v2_Out_109 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_112 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_112;

	private int logic_uScriptAct_AddInt_v2_B_112 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_112;

	private float logic_uScriptAct_AddInt_v2_FloatResult_112;

	private bool logic_uScriptAct_AddInt_v2_Out_112 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_114 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_114;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_114;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_114;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_114;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_114;

	private bool logic_uScript_FlyTechUpAndAway_Out_114 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_116 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_116;

	private int logic_uScriptCon_CompareInt_B_116;

	private bool logic_uScriptCon_CompareInt_GreaterThan_116 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_116 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_116 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_116 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_116 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_116 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_118 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_118;

	private int logic_uScriptAct_AddInt_v2_B_118 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_118;

	private float logic_uScriptAct_AddInt_v2_FloatResult_118;

	private bool logic_uScriptAct_AddInt_v2_Out_118 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_122 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_122;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_122;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_122;

	private bool logic_uScript_AddMessage_Out_122 = true;

	private bool logic_uScript_AddMessage_Shown_122 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_126 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_126;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_126;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_126;

	private bool logic_uScript_AddMessage_Out_126 = true;

	private bool logic_uScript_AddMessage_Shown_126 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_127 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_127;

	private int logic_uScriptCon_CompareInt_B_127 = 1;

	private bool logic_uScriptCon_CompareInt_GreaterThan_127 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_127 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_127 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_127 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_127 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_127 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_129 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_129;

	private int logic_uScriptAct_AddInt_v2_B_129 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_129;

	private float logic_uScriptAct_AddInt_v2_FloatResult_129;

	private bool logic_uScriptAct_AddInt_v2_Out_129 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_133 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_133;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_133;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_133;

	private bool logic_uScript_AddMessage_Out_133 = true;

	private bool logic_uScript_AddMessage_Shown_133 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_134 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_134;

	private int logic_uScriptCon_CompareInt_B_134 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_134 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_134 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_134 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_134 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_134 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_134 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_136 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_136;

	private int logic_uScriptAct_AddInt_v2_B_136 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_136;

	private float logic_uScriptAct_AddInt_v2_FloatResult_136;

	private bool logic_uScriptAct_AddInt_v2_Out_136 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_137;

	private int logic_SubGraph_SaveLoadInt_integer_137;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_137 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_137 = "NumChargerMessagesShown";

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_142 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_142 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_142;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_142 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_142;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_142 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_142 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_142 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_142 = true;

	private uScript_SetTechsInvulnerable logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_143 = new uScript_SetTechsInvulnerable();

	private Tank[] logic_uScript_SetTechsInvulnerable_techs_143 = new Tank[0];

	private bool logic_uScript_SetTechsInvulnerable_Out_143 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_144 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_144;

	private int logic_uScriptCon_CompareInt_B_144 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_144 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_144 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_144 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_144 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_144 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_144 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_146 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_146;

	private int logic_uScriptCon_CompareInt_B_146 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_146 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_146 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_146 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_146 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_146 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_146 = true;

	private uScript_SetTechsInvulnerable logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_147 = new uScript_SetTechsInvulnerable();

	private Tank[] logic_uScript_SetTechsInvulnerable_techs_147 = new Tank[0];

	private bool logic_uScript_SetTechsInvulnerable_Out_147 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_149 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_149 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_149;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_149 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_149;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_149 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_149 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_149 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_149 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_153 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_153 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_153;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_153 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_153;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_153 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_153 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_153 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_153 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_154 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_154;

	private int logic_uScriptCon_CompareInt_B_154 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_154 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_154 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_154 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_154 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_154 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_154 = true;

	private uScript_SetTechsInvulnerable logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_158 = new uScript_SetTechsInvulnerable();

	private Tank[] logic_uScript_SetTechsInvulnerable_techs_158 = new Tank[0];

	private bool logic_uScript_SetTechsInvulnerable_Out_158 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_160 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_160 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_161 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_161 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_162 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_162 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_168 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_168 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_168;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_168 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_168;

	private bool logic_uScript_SpawnTechsFromData_Out_168 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_170 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_170;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_170;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_170;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_170;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_170;

	private bool logic_uScript_FlyTechUpAndAway_Out_170 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_173 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_173 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_173 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_173 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_173 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_173 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_173 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_176 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_176 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_176 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_176 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_176 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_176 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_176 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_177 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_177 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_177 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_179 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_179;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_179;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_179;

	private bool logic_uScript_AddMessage_Out_179 = true;

	private bool logic_uScript_AddMessage_Shown_179 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_183 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_183;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_183;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_183;

	private bool logic_uScript_AddMessage_Out_183 = true;

	private bool logic_uScript_AddMessage_Shown_183 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_185 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_185;

	private bool logic_uScriptCon_CompareBool_True_185 = true;

	private bool logic_uScriptCon_CompareBool_False_185 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_187 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_187;

	private bool logic_uScriptAct_SetBool_Out_187 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_187 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_187 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_189 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_189 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_189 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_189;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_189 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_189 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_189 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_189 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_189 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_189 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_189 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_192 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_192;

	private bool logic_uScriptAct_SetBool_Out_192 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_192 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_192 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_193;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_193 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_193 = "MsgFirstRoomClearShown";

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_196 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_196 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_196 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_196 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_196 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_196 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_196 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_197 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_197 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_197 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_197 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_197 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_197 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_197 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_199 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_199 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_201 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_201;

	private bool logic_uScript_RemoveOnScreenMessage_instant_201;

	private bool logic_uScript_RemoveOnScreenMessage_Out_201 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_204 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_204 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_206 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_206;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_206 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_206 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_206 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_208 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_208;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_208 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_212 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_212 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_212;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_212 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_212;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_212 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_212 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_212 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_212 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_213 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_213 = new Tank[0];

	private int logic_uScript_AccessListTech_index_213;

	private Tank logic_uScript_AccessListTech_value_213;

	private bool logic_uScript_AccessListTech_Out_213 = true;

	private uScript_SetEncounterTargetPosition logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_215 = new uScript_SetEncounterTargetPosition();

	private string logic_uScript_SetEncounterTargetPosition_positionName_215 = "";

	private bool logic_uScript_SetEncounterTargetPosition_Out_215 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_217 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_217 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_217 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_217 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_217 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_217 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_217 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_219 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_219;

	private bool logic_uScriptCon_CompareBool_True_219 = true;

	private bool logic_uScriptCon_CompareBool_False_219 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_221 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_221;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_221;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_221;

	private bool logic_uScript_AddMessage_Out_221 = true;

	private bool logic_uScript_AddMessage_Shown_221 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_224 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_224;

	private bool logic_uScriptAct_SetBool_Out_224 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_224 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_224 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_226;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_226 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_226 = "MsgChargerShown";

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_228 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_228;

	private object logic_uScript_SetEncounterTarget_visibleObject_228 = "";

	private bool logic_uScript_SetEncounterTarget_Out_228 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_235 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_235;

	private bool logic_uScriptCon_CompareBool_True_235 = true;

	private bool logic_uScriptCon_CompareBool_False_235 = true;

	private uScriptAct_ForEachListString logic_uScriptAct_ForEachListString_uScriptAct_ForEachListString_236 = new uScriptAct_ForEachListString();

	private string[] logic_uScriptAct_ForEachListString_List_236 = new string[0];

	private string logic_uScriptAct_ForEachListString_Value_236;

	private int logic_uScriptAct_ForEachListString_currentIndex_236;

	private bool logic_uScriptAct_ForEachListString_Immediate_236 = true;

	private bool logic_uScriptAct_ForEachListString_Done_236 = true;

	private bool logic_uScriptAct_ForEachListString_Iteration_236 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_239 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_239;

	private int logic_uScriptCon_CompareInt_B_239;

	private bool logic_uScriptCon_CompareInt_GreaterThan_239 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_239 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_239 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_239 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_239 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_239 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_241 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_241;

	private int logic_uScriptAct_AddInt_v2_B_241 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_241;

	private float logic_uScriptAct_AddInt_v2_FloatResult_241;

	private bool logic_uScriptAct_AddInt_v2_Out_241 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_243 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_243 = 1;

	private int logic_uScriptAct_SetInt_Target_243;

	private bool logic_uScriptAct_SetInt_Out_243 = true;

	private uScript_SetLabShutterSwitch logic_uScript_SetLabShutterSwitch_uScript_SetLabShutterSwitch_246 = new uScript_SetLabShutterSwitch();

	private string logic_uScript_SetLabShutterSwitch_switchToOpen_246 = "";

	private string logic_uScript_SetLabShutterSwitch_switchToClose_246 = "";

	private string[] logic_uScript_SetLabShutterSwitch_batchSwitchesToOpen_246 = new string[0];

	private string[] logic_uScript_SetLabShutterSwitch_batchSwitchesToClose_246 = new string[0];

	private bool logic_uScript_SetLabShutterSwitch_Out_246 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_248 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_248 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_250 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_250 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_250 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_250;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_250 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_250 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_250 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_250 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_250 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_250 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_250 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_252 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_252 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_252 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_252;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_252 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_252 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_252 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_252 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_252 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_252 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_252 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_253 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_253 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_253 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_253;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_253 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_253 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_253 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_253 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_253 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_253 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_253 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_255 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_255;

	private int logic_uScriptCon_CompareInt_B_255 = 1;

	private bool logic_uScriptCon_CompareInt_GreaterThan_255 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_255 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_255 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_255 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_255 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_255 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_257 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_257 = 1;

	private int logic_uScriptAct_SetInt_Target_257;

	private bool logic_uScriptAct_SetInt_Out_257 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_259 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_259;

	private int logic_uScriptCon_CompareInt_B_259 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_259 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_259 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_259 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_259 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_259 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_259 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_262 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_262 = 2;

	private int logic_uScriptAct_SetInt_Target_262;

	private bool logic_uScriptAct_SetInt_Out_262 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_263 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_263;

	private int logic_uScriptCon_CompareInt_B_263 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_263 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_263 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_263 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_263 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_263 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_263 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_265 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_265 = 3;

	private int logic_uScriptAct_SetInt_Target_265;

	private bool logic_uScriptAct_SetInt_Out_265 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_267;

	private int logic_SubGraph_SaveLoadInt_integer_267;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_267 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_267 = "NumGatesOpened";

	private uScript_SetLabShutterSwitch logic_uScript_SetLabShutterSwitch_uScript_SetLabShutterSwitch_269 = new uScript_SetLabShutterSwitch();

	private string logic_uScript_SetLabShutterSwitch_switchToOpen_269 = "";

	private string logic_uScript_SetLabShutterSwitch_switchToClose_269 = "";

	private string[] logic_uScript_SetLabShutterSwitch_batchSwitchesToOpen_269 = new string[0];

	private string[] logic_uScript_SetLabShutterSwitch_batchSwitchesToClose_269 = new string[0];

	private bool logic_uScript_SetLabShutterSwitch_Out_269 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_271 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_271 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_272 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_272 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_276 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_276 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_276 = true;

	private uScript_IsMultiplayerMode logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_277 = new uScript_IsMultiplayerMode();

	private bool logic_uScript_IsMultiplayerMode_SinglePlayer_277 = true;

	private bool logic_uScript_IsMultiplayerMode_Multiplayer_277 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_278 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_278 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_279 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_279 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_281 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_281 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_281;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_281 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_281;

	private bool logic_uScript_SpawnTechsFromData_Out_281 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_283 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_283;

	private bool logic_uScript_RemoveTech_Out_283 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_289 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_289 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_291 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_291 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_291;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_291 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_291;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_291 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_291 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_291 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_291 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_292 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_292 = new Tank[0];

	private int logic_uScript_AccessListTech_index_292;

	private Tank logic_uScript_AccessListTech_value_292;

	private bool logic_uScript_AccessListTech_Out_292 = true;

	private uScript_SetTechMarkerState logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_293 = new uScript_SetTechMarkerState();

	private Tank logic_uScript_SetTechMarkerState_tech_293;

	private bool logic_uScript_SetTechMarkerState_Out_293 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_294 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_294 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_294;

	private bool logic_uScript_SetTankInvulnerable_Out_294 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_296 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_296 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_297 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_297;

	private bool logic_uScriptAct_SetBool_Out_297 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_297 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_297 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_300 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_300;

	private bool logic_uScriptAct_SetBool_Out_300 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_300 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_300 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_301 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_301;

	private bool logic_uScript_RemoveOnScreenMessage_instant_301;

	private bool logic_uScript_RemoveOnScreenMessage_Out_301 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_304 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_304;

	private bool logic_uScript_RemoveOnScreenMessage_instant_304;

	private bool logic_uScript_RemoveOnScreenMessage_Out_304 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_305 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_305 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
			if (null != owner_Connection_2)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_2.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_2.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_1;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_1;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_1;
				}
			}
		}
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
			if (null != owner_Connection_8)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_8.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_8.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_6;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_6;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_6;
				}
			}
		}
		if (null == owner_Connection_10 || !m_RegisteredForEvents)
		{
			owner_Connection_10 = parentGameObject;
		}
		if (null == owner_Connection_13 || !m_RegisteredForEvents)
		{
			owner_Connection_13 = parentGameObject;
		}
		if (null == owner_Connection_17 || !m_RegisteredForEvents)
		{
			owner_Connection_17 = parentGameObject;
		}
		if (null == owner_Connection_28 || !m_RegisteredForEvents)
		{
			owner_Connection_28 = parentGameObject;
		}
		if (null == owner_Connection_31 || !m_RegisteredForEvents)
		{
			owner_Connection_31 = parentGameObject;
		}
		if (null == owner_Connection_36 || !m_RegisteredForEvents)
		{
			owner_Connection_36 = parentGameObject;
		}
		if (null == owner_Connection_69 || !m_RegisteredForEvents)
		{
			owner_Connection_69 = parentGameObject;
		}
		if (null == owner_Connection_74 || !m_RegisteredForEvents)
		{
			owner_Connection_74 = parentGameObject;
		}
		if (null == owner_Connection_76 || !m_RegisteredForEvents)
		{
			owner_Connection_76 = parentGameObject;
		}
		if (null == owner_Connection_78 || !m_RegisteredForEvents)
		{
			owner_Connection_78 = parentGameObject;
		}
		if (null == owner_Connection_90 || !m_RegisteredForEvents)
		{
			owner_Connection_90 = parentGameObject;
		}
		if (null == owner_Connection_92 || !m_RegisteredForEvents)
		{
			owner_Connection_92 = parentGameObject;
		}
		if (null == owner_Connection_140 || !m_RegisteredForEvents)
		{
			owner_Connection_140 = parentGameObject;
		}
		if (null == owner_Connection_150 || !m_RegisteredForEvents)
		{
			owner_Connection_150 = parentGameObject;
		}
		if (null == owner_Connection_156 || !m_RegisteredForEvents)
		{
			owner_Connection_156 = parentGameObject;
		}
		if (null == owner_Connection_167 || !m_RegisteredForEvents)
		{
			owner_Connection_167 = parentGameObject;
		}
		if (null == owner_Connection_205 || !m_RegisteredForEvents)
		{
			owner_Connection_205 = parentGameObject;
		}
		if (null == owner_Connection_207 || !m_RegisteredForEvents)
		{
			owner_Connection_207 = parentGameObject;
		}
		if (null == owner_Connection_210 || !m_RegisteredForEvents)
		{
			owner_Connection_210 = parentGameObject;
		}
		if (null == owner_Connection_230 || !m_RegisteredForEvents)
		{
			owner_Connection_230 = parentGameObject;
		}
		if (null == owner_Connection_280 || !m_RegisteredForEvents)
		{
			owner_Connection_280 = parentGameObject;
		}
		if (null == owner_Connection_286 || !m_RegisteredForEvents)
		{
			owner_Connection_286 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_2)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_2.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_2.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_1;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_1;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_1;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_8)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_8.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_8.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_6;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_6;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_6;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_2)
		{
			uScript_EncounterUpdate component = owner_Connection_2.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_1;
				component.OnSuspend -= Instance_OnSuspend_1;
				component.OnResume -= Instance_OnResume_1;
			}
		}
		if (null != owner_Connection_8)
		{
			uScript_SaveLoad component2 = owner_Connection_8.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_6;
				component2.LoadEvent -= Instance_LoadEvent_6;
				component2.RestartEvent -= Instance_RestartEvent_6;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_9.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_12.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_15.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_18.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_29.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_32.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_34.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_35.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_39.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_46.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_50.SetParent(g);
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_52.SetParent(g);
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_59.SetParent(g);
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_66.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_72.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_73.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_75.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_79.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_88.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_89.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_91.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_94.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_105.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_107.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_109.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_112.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_114.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_116.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_118.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_122.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_126.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_127.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_129.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_133.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_134.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_136.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_142.SetParent(g);
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_143.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_144.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_146.SetParent(g);
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_147.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_149.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_153.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_154.SetParent(g);
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_158.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_160.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_161.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_162.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_168.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_170.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_173.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_176.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_177.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_179.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_183.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_185.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_187.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_189.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_192.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_196.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_197.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_199.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_201.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_204.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_206.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_208.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_212.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_213.SetParent(g);
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_215.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_217.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_219.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_221.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_224.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_228.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_235.SetParent(g);
		logic_uScriptAct_ForEachListString_uScriptAct_ForEachListString_236.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_239.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_241.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_243.SetParent(g);
		logic_uScript_SetLabShutterSwitch_uScript_SetLabShutterSwitch_246.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_248.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_250.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_252.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_253.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_255.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_257.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_259.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_262.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_263.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_265.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267.SetParent(g);
		logic_uScript_SetLabShutterSwitch_uScript_SetLabShutterSwitch_269.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_271.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_272.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_276.SetParent(g);
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_277.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_278.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_279.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_281.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_283.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_289.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_291.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_292.SetParent(g);
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_293.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_294.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_296.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_297.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_300.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_301.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_304.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_305.SetParent(g);
		owner_Connection_2 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_10 = parentGameObject;
		owner_Connection_13 = parentGameObject;
		owner_Connection_17 = parentGameObject;
		owner_Connection_28 = parentGameObject;
		owner_Connection_31 = parentGameObject;
		owner_Connection_36 = parentGameObject;
		owner_Connection_69 = parentGameObject;
		owner_Connection_74 = parentGameObject;
		owner_Connection_76 = parentGameObject;
		owner_Connection_78 = parentGameObject;
		owner_Connection_90 = parentGameObject;
		owner_Connection_92 = parentGameObject;
		owner_Connection_140 = parentGameObject;
		owner_Connection_150 = parentGameObject;
		owner_Connection_156 = parentGameObject;
		owner_Connection_167 = parentGameObject;
		owner_Connection_205 = parentGameObject;
		owner_Connection_207 = parentGameObject;
		owner_Connection_210 = parentGameObject;
		owner_Connection_230 = parentGameObject;
		owner_Connection_280 = parentGameObject;
		owner_Connection_286 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.Awake();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_52.Awake();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_59.Awake();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_66.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Save_Out += SubGraph_SaveLoadBool_Save_Out_3;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Load_Out += SubGraph_SaveLoadBool_Load_Out_3;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output1 += uScriptCon_ManualSwitch_Output1_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output2 += uScriptCon_ManualSwitch_Output2_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output3 += uScriptCon_ManualSwitch_Output3_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output4 += uScriptCon_ManualSwitch_Output4_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output5 += uScriptCon_ManualSwitch_Output5_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output6 += uScriptCon_ManualSwitch_Output6_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output7 += uScriptCon_ManualSwitch_Output7_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output8 += uScriptCon_ManualSwitch_Output8_20;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Save_Out += SubGraph_SaveLoadInt_Save_Out_22;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Load_Out += SubGraph_SaveLoadInt_Load_Out_22;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_22;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.Out += SubGraph_LoadObjectiveStates_Out_24;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.Out += SubGraph_CompleteObjectiveStage_Out_26;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.Out += SubGraph_CompleteObjectiveStage_Out_43;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_52.Out += SubGraph_RR_LaserLab_TurretAndChargerController_Out_52;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_59.Out += SubGraph_RR_LaserLab_TurretAndChargerController_Out_59;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_66.Out += SubGraph_RR_LaserLab_TurretAndChargerController_Out_66;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.Output1 += uScriptCon_ManualSwitch_Output1_103;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.Output2 += uScriptCon_ManualSwitch_Output2_103;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.Output3 += uScriptCon_ManualSwitch_Output3_103;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.Output4 += uScriptCon_ManualSwitch_Output4_103;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.Output5 += uScriptCon_ManualSwitch_Output5_103;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.Output6 += uScriptCon_ManualSwitch_Output6_103;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.Output7 += uScriptCon_ManualSwitch_Output7_103;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.Output8 += uScriptCon_ManualSwitch_Output8_103;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137.Save_Out += SubGraph_SaveLoadInt_Save_Out_137;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137.Load_Out += SubGraph_SaveLoadInt_Load_Out_137;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Save_Out += SubGraph_SaveLoadBool_Save_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Load_Out += SubGraph_SaveLoadBool_Load_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.Save_Out += SubGraph_SaveLoadBool_Save_Out_226;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.Load_Out += SubGraph_SaveLoadBool_Load_Out_226;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_226;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267.Save_Out += SubGraph_SaveLoadInt_Save_Out_267;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267.Load_Out += SubGraph_SaveLoadInt_Load_Out_267;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_267;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.Start();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_52.Start();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_59.Start();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_66.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.OnEnable();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_52.OnEnable();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_59.OnEnable();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_66.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_114.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_170.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_208.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_39.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_46.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_50.OnDisable();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_52.OnDisable();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_59.OnDisable();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_66.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_122.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_126.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_133.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_177.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_179.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_183.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_206.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_221.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_276.OnDisable();
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_277.OnDisable();
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_293.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_294.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.Update();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_52.Update();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_59.Update();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_66.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.OnDestroy();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_52.OnDestroy();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_59.OnDestroy();
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_66.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Save_Out -= SubGraph_SaveLoadBool_Save_Out_3;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Load_Out -= SubGraph_SaveLoadBool_Load_Out_3;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output1 -= uScriptCon_ManualSwitch_Output1_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output2 -= uScriptCon_ManualSwitch_Output2_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output3 -= uScriptCon_ManualSwitch_Output3_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output4 -= uScriptCon_ManualSwitch_Output4_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output5 -= uScriptCon_ManualSwitch_Output5_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output6 -= uScriptCon_ManualSwitch_Output6_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output7 -= uScriptCon_ManualSwitch_Output7_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output8 -= uScriptCon_ManualSwitch_Output8_20;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Save_Out -= SubGraph_SaveLoadInt_Save_Out_22;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Load_Out -= SubGraph_SaveLoadInt_Load_Out_22;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_22;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.Out -= SubGraph_LoadObjectiveStates_Out_24;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.Out -= SubGraph_CompleteObjectiveStage_Out_26;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.Out -= SubGraph_CompleteObjectiveStage_Out_43;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_52.Out -= SubGraph_RR_LaserLab_TurretAndChargerController_Out_52;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_59.Out -= SubGraph_RR_LaserLab_TurretAndChargerController_Out_59;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_66.Out -= SubGraph_RR_LaserLab_TurretAndChargerController_Out_66;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.Output1 -= uScriptCon_ManualSwitch_Output1_103;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.Output2 -= uScriptCon_ManualSwitch_Output2_103;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.Output3 -= uScriptCon_ManualSwitch_Output3_103;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.Output4 -= uScriptCon_ManualSwitch_Output4_103;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.Output5 -= uScriptCon_ManualSwitch_Output5_103;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.Output6 -= uScriptCon_ManualSwitch_Output6_103;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.Output7 -= uScriptCon_ManualSwitch_Output7_103;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.Output8 -= uScriptCon_ManualSwitch_Output8_103;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137.Save_Out -= SubGraph_SaveLoadInt_Save_Out_137;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137.Load_Out -= SubGraph_SaveLoadInt_Load_Out_137;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Save_Out -= SubGraph_SaveLoadBool_Save_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Load_Out -= SubGraph_SaveLoadBool_Load_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.Save_Out -= SubGraph_SaveLoadBool_Save_Out_226;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.Load_Out -= SubGraph_SaveLoadBool_Load_Out_226;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_226;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267.Save_Out -= SubGraph_SaveLoadInt_Save_Out_267;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267.Load_Out -= SubGraph_SaveLoadInt_Load_Out_267;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_267;
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

	private void Instance_SaveEvent_6(object o, EventArgs e)
	{
		Relay_SaveEvent_6();
	}

	private void Instance_LoadEvent_6(object o, EventArgs e)
	{
		Relay_LoadEvent_6();
	}

	private void Instance_RestartEvent_6(object o, EventArgs e)
	{
		Relay_RestartEvent_6();
	}

	private void SubGraph_SaveLoadBool_Save_Out_3(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_3;
		Relay_Save_Out_3();
	}

	private void SubGraph_SaveLoadBool_Load_Out_3(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_3;
		Relay_Load_Out_3();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_3(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_3;
		Relay_Restart_Out_3();
	}

	private void uScriptCon_ManualSwitch_Output1_20(object o, EventArgs e)
	{
		Relay_Output1_20();
	}

	private void uScriptCon_ManualSwitch_Output2_20(object o, EventArgs e)
	{
		Relay_Output2_20();
	}

	private void uScriptCon_ManualSwitch_Output3_20(object o, EventArgs e)
	{
		Relay_Output3_20();
	}

	private void uScriptCon_ManualSwitch_Output4_20(object o, EventArgs e)
	{
		Relay_Output4_20();
	}

	private void uScriptCon_ManualSwitch_Output5_20(object o, EventArgs e)
	{
		Relay_Output5_20();
	}

	private void uScriptCon_ManualSwitch_Output6_20(object o, EventArgs e)
	{
		Relay_Output6_20();
	}

	private void uScriptCon_ManualSwitch_Output7_20(object o, EventArgs e)
	{
		Relay_Output7_20();
	}

	private void uScriptCon_ManualSwitch_Output8_20(object o, EventArgs e)
	{
		Relay_Output8_20();
	}

	private void SubGraph_SaveLoadInt_Save_Out_22(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_22 = e.integer;
		local_CurrentObjective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_22;
		Relay_Save_Out_22();
	}

	private void SubGraph_SaveLoadInt_Load_Out_22(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_22 = e.integer;
		local_CurrentObjective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_22;
		Relay_Load_Out_22();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_22(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_22 = e.integer;
		local_CurrentObjective_System_Int32 = logic_SubGraph_SaveLoadInt_integer_22;
		Relay_Restart_Out_22();
	}

	private void SubGraph_LoadObjectiveStates_Out_24(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_24();
	}

	private void SubGraph_CompleteObjectiveStage_Out_26(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_26 = e.objectiveStage;
		local_CurrentObjective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_26;
		Relay_Out_26();
	}

	private void SubGraph_CompleteObjectiveStage_Out_43(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_43 = e.objectiveStage;
		local_CurrentObjective_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_43;
		Relay_Out_43();
	}

	private void SubGraph_RR_LaserLab_TurretAndChargerController_Out_52(object o, SubGraph_RR_LaserLab_TurretAndChargerController.LogicEventArgs e)
	{
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_52 = e.turretsDestroyed;
		local_Turrets01Destroyed_System_Boolean = logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_52;
		Relay_Out_52();
	}

	private void SubGraph_RR_LaserLab_TurretAndChargerController_Out_59(object o, SubGraph_RR_LaserLab_TurretAndChargerController.LogicEventArgs e)
	{
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_59 = e.turretsDestroyed;
		local_Turrets02Destroyed_System_Boolean = logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_59;
		Relay_Out_59();
	}

	private void SubGraph_RR_LaserLab_TurretAndChargerController_Out_66(object o, SubGraph_RR_LaserLab_TurretAndChargerController.LogicEventArgs e)
	{
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_66 = e.turretsDestroyed;
		local_Turrets03Destroyed_System_Boolean = logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretsDestroyed_66;
		Relay_Out_66();
	}

	private void uScriptCon_ManualSwitch_Output1_103(object o, EventArgs e)
	{
		Relay_Output1_103();
	}

	private void uScriptCon_ManualSwitch_Output2_103(object o, EventArgs e)
	{
		Relay_Output2_103();
	}

	private void uScriptCon_ManualSwitch_Output3_103(object o, EventArgs e)
	{
		Relay_Output3_103();
	}

	private void uScriptCon_ManualSwitch_Output4_103(object o, EventArgs e)
	{
		Relay_Output4_103();
	}

	private void uScriptCon_ManualSwitch_Output5_103(object o, EventArgs e)
	{
		Relay_Output5_103();
	}

	private void uScriptCon_ManualSwitch_Output6_103(object o, EventArgs e)
	{
		Relay_Output6_103();
	}

	private void uScriptCon_ManualSwitch_Output7_103(object o, EventArgs e)
	{
		Relay_Output7_103();
	}

	private void uScriptCon_ManualSwitch_Output8_103(object o, EventArgs e)
	{
		Relay_Output8_103();
	}

	private void SubGraph_SaveLoadInt_Save_Out_137(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_137 = e.integer;
		local_NumChargerMessagesShown_System_Int32 = logic_SubGraph_SaveLoadInt_integer_137;
		Relay_Save_Out_137();
	}

	private void SubGraph_SaveLoadInt_Load_Out_137(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_137 = e.integer;
		local_NumChargerMessagesShown_System_Int32 = logic_SubGraph_SaveLoadInt_integer_137;
		Relay_Load_Out_137();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_137(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_137 = e.integer;
		local_NumChargerMessagesShown_System_Int32 = logic_SubGraph_SaveLoadInt_integer_137;
		Relay_Restart_Out_137();
	}

	private void SubGraph_SaveLoadBool_Save_Out_193(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = e.boolean;
		local_MsgFirstRoomClearShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_193;
		Relay_Save_Out_193();
	}

	private void SubGraph_SaveLoadBool_Load_Out_193(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = e.boolean;
		local_MsgFirstRoomClearShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_193;
		Relay_Load_Out_193();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_193(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = e.boolean;
		local_MsgFirstRoomClearShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_193;
		Relay_Restart_Out_193();
	}

	private void SubGraph_SaveLoadBool_Save_Out_226(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_226 = e.boolean;
		local_MsgChargerShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_226;
		Relay_Save_Out_226();
	}

	private void SubGraph_SaveLoadBool_Load_Out_226(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_226 = e.boolean;
		local_MsgChargerShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_226;
		Relay_Load_Out_226();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_226(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_226 = e.boolean;
		local_MsgChargerShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_226;
		Relay_Restart_Out_226();
	}

	private void SubGraph_SaveLoadInt_Save_Out_267(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_267 = e.integer;
		local_NumGatesOpened_System_Int32 = logic_SubGraph_SaveLoadInt_integer_267;
		Relay_Save_Out_267();
	}

	private void SubGraph_SaveLoadInt_Load_Out_267(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_267 = e.integer;
		local_NumGatesOpened_System_Int32 = logic_SubGraph_SaveLoadInt_integer_267;
		Relay_Load_Out_267();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_267(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_267 = e.integer;
		local_NumGatesOpened_System_Int32 = logic_SubGraph_SaveLoadInt_integer_267;
		Relay_Restart_Out_267();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_4();
		Relay_In_235();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_Save_Out_3()
	{
		Relay_Save_22();
	}

	private void Relay_Load_Out_3()
	{
		Relay_Load_22();
	}

	private void Relay_Restart_Out_3()
	{
		Relay_Restart_22();
	}

	private void Relay_Save_3()
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_3 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Save(ref logic_SubGraph_SaveLoadBool_boolean_3, logic_SubGraph_SaveLoadBool_boolAsVariable_3, logic_SubGraph_SaveLoadBool_uniqueID_3);
	}

	private void Relay_Load_3()
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_3 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Load(ref logic_SubGraph_SaveLoadBool_boolean_3, logic_SubGraph_SaveLoadBool_boolAsVariable_3, logic_SubGraph_SaveLoadBool_uniqueID_3);
	}

	private void Relay_Set_True_3()
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_3 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_3, logic_SubGraph_SaveLoadBool_boolAsVariable_3, logic_SubGraph_SaveLoadBool_uniqueID_3);
	}

	private void Relay_Set_False_3()
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_3 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_3, logic_SubGraph_SaveLoadBool_boolAsVariable_3, logic_SubGraph_SaveLoadBool_uniqueID_3);
	}

	private void Relay_In_4()
	{
		logic_uScriptCon_CompareBool_Bool_4 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.In(logic_uScriptCon_CompareBool_Bool_4);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.False;
		if (num)
		{
			Relay_In_15();
		}
		if (flag)
		{
			Relay_True_7();
		}
	}

	private void Relay_SaveEvent_6()
	{
		Relay_Save_3();
	}

	private void Relay_LoadEvent_6()
	{
		Relay_Load_3();
	}

	private void Relay_RestartEvent_6()
	{
		Relay_Set_False_3();
	}

	private void Relay_True_7()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.True(out logic_uScriptAct_SetBool_Target_7);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_7;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_7.Out)
		{
			Relay_InitialSpawn_12();
		}
	}

	private void Relay_False_7()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.False(out logic_uScriptAct_SetBool_Target_7);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_7;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_7.Out)
		{
			Relay_InitialSpawn_12();
		}
	}

	private void Relay_InitialSpawn_9()
	{
		int num = 0;
		Array array = turretGroup01SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_9.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_9, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_9, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_9 = owner_Connection_10;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_9.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_9, logic_uScript_SpawnTechsFromData_ownerNode_9, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_9, logic_uScript_SpawnTechsFromData_allowResurrection_9);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_9.Out)
		{
			Relay_InitialSpawn_29();
		}
	}

	private void Relay_InitialSpawn_12()
	{
		int num = 0;
		Array array = npcSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_12.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_12, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_12, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_12 = owner_Connection_13;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_12.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_12, logic_uScript_SpawnTechsFromData_ownerNode_12, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_12, logic_uScript_SpawnTechsFromData_allowResurrection_12);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_12.Out)
		{
			Relay_InitialSpawn_9();
		}
	}

	private void Relay_In_15()
	{
		int num = 0;
		Array array = npcSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_15.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_15, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_15, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_15 = owner_Connection_17;
		int num2 = 0;
		Array array2 = local_16_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_15.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_15, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_15, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_15 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_15.In(logic_uScript_GetAndCheckTechs_techData_15, logic_uScript_GetAndCheckTechs_ownerNode_15, ref logic_uScript_GetAndCheckTechs_techs_15);
		local_16_TankArray = logic_uScript_GetAndCheckTechs_techs_15;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_15.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_15.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_15.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_15.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_18();
		}
		if (someAlive)
		{
			Relay_AtIndex_18();
		}
		if (allDead)
		{
			Relay_In_34();
		}
		if (waitingToSpawn)
		{
			Relay_In_34();
		}
	}

	private void Relay_AtIndex_18()
	{
		int num = 0;
		Array array = local_16_TankArray;
		if (logic_uScript_AccessListTech_techList_18.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_18, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_18, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_18.AtIndex(ref logic_uScript_AccessListTech_techList_18, logic_uScript_AccessListTech_index_18, out logic_uScript_AccessListTech_value_18);
		local_16_TankArray = logic_uScript_AccessListTech_techList_18;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_18;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_18.Out)
		{
			Relay_In_291();
		}
	}

	private void Relay_Output1_20()
	{
		Relay_In_173();
	}

	private void Relay_Output2_20()
	{
		Relay_In_189();
	}

	private void Relay_Output3_20()
	{
		Relay_In_212();
	}

	private void Relay_Output4_20()
	{
	}

	private void Relay_Output5_20()
	{
	}

	private void Relay_Output6_20()
	{
	}

	private void Relay_Output7_20()
	{
	}

	private void Relay_Output8_20()
	{
	}

	private void Relay_In_20()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_20 = local_CurrentObjective_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.In(logic_uScriptCon_ManualSwitch_CurrentOutput_20);
	}

	private void Relay_Save_Out_22()
	{
		Relay_Save_193();
	}

	private void Relay_Load_Out_22()
	{
		Relay_Load_193();
	}

	private void Relay_Restart_Out_22()
	{
		Relay_Set_False_193();
	}

	private void Relay_Save_22()
	{
		logic_SubGraph_SaveLoadInt_integer_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Save(logic_SubGraph_SaveLoadInt_restartValue_22, ref logic_SubGraph_SaveLoadInt_integer_22, logic_SubGraph_SaveLoadInt_intAsVariable_22, logic_SubGraph_SaveLoadInt_uniqueID_22);
	}

	private void Relay_Load_22()
	{
		logic_SubGraph_SaveLoadInt_integer_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Load(logic_SubGraph_SaveLoadInt_restartValue_22, ref logic_SubGraph_SaveLoadInt_integer_22, logic_SubGraph_SaveLoadInt_intAsVariable_22, logic_SubGraph_SaveLoadInt_uniqueID_22);
	}

	private void Relay_Restart_22()
	{
		logic_SubGraph_SaveLoadInt_integer_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_22 = local_CurrentObjective_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_22.Restart(logic_SubGraph_SaveLoadInt_restartValue_22, ref logic_SubGraph_SaveLoadInt_integer_22, logic_SubGraph_SaveLoadInt_intAsVariable_22, logic_SubGraph_SaveLoadInt_uniqueID_22);
	}

	private void Relay_Out_24()
	{
	}

	private void Relay_In_24()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_24 = local_CurrentObjective_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_24.In(logic_SubGraph_LoadObjectiveStates_currentObjective_24);
	}

	private void Relay_Out_26()
	{
	}

	private void Relay_In_26()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_26 = local_CurrentObjective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_26.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_26, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_26);
	}

	private void Relay_InitialSpawn_29()
	{
		int num = 0;
		Array array = charger01SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_29.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_29, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_29, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_29 = owner_Connection_28;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_29.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_29, logic_uScript_SpawnTechsFromData_ownerNode_29, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_29, logic_uScript_SpawnTechsFromData_allowResurrection_29);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_29.Out)
		{
			Relay_InitialSpawn_73();
		}
	}

	private void Relay_In_32()
	{
		int num = 0;
		Array array = charger03SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_32.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_32, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_32, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_32 = owner_Connection_31;
		logic_uScript_GetAndCheckTechs_Return_32 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_32.In(logic_uScript_GetAndCheckTechs_techData_32, logic_uScript_GetAndCheckTechs_ownerNode_32, ref logic_uScript_GetAndCheckTechs_techs_32);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_32.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_32.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_32.AllDead;
		if (allAlive)
		{
			Relay_In_103();
		}
		if (someAlive)
		{
			Relay_In_103();
		}
		if (allDead)
		{
			Relay_In_112();
		}
	}

	private void Relay_In_34()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_34.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_34.Out)
		{
			Relay_In_291();
		}
	}

	private void Relay_Succeed_35()
	{
		logic_uScript_FinishEncounter_owner_35 = owner_Connection_36;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_35.Succeed(logic_uScript_FinishEncounter_owner_35);
	}

	private void Relay_Fail_35()
	{
		logic_uScript_FinishEncounter_owner_35 = owner_Connection_36;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_35.Fail(logic_uScript_FinishEncounter_owner_35);
	}

	private void Relay_In_39()
	{
		logic_uScript_AddMessage_messageData_39 = msg01Intro;
		logic_uScript_AddMessage_speaker_39 = npcSpeaker;
		logic_uScript_AddMessage_Return_39 = logic_uScript_AddMessage_uScript_AddMessage_39.In(logic_uScript_AddMessage_messageData_39, logic_uScript_AddMessage_speaker_39);
		local_MsgIntro_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_39;
		bool num = logic_uScript_AddMessage_uScript_AddMessage_39.Out;
		bool shown = logic_uScript_AddMessage_uScript_AddMessage_39.Shown;
		if (num)
		{
			Relay_In_196();
		}
		if (shown)
		{
			Relay_In_199();
		}
	}

	private void Relay_Out_43()
	{
	}

	private void Relay_In_43()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_43 = local_CurrentObjective_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_43.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_43, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_43);
	}

	private void Relay_In_46()
	{
		logic_uScript_AddMessage_messageData_46 = msg04MissionComplete;
		logic_uScript_AddMessage_speaker_46 = npcSpeaker;
		logic_uScript_AddMessage_Return_46 = logic_uScript_AddMessage_uScript_AddMessage_46.In(logic_uScript_AddMessage_messageData_46, logic_uScript_AddMessage_speaker_46);
		local_MsgMissionComplete_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_46;
		if (logic_uScript_AddMessage_uScript_AddMessage_46.Shown)
		{
			Relay_In_114();
		}
	}

	private void Relay_In_50()
	{
		logic_uScript_AddMessage_messageData_50 = msg03ReturnToNPC;
		logic_uScript_AddMessage_speaker_50 = npcSpeaker;
		logic_uScript_AddMessage_Return_50 = logic_uScript_AddMessage_uScript_AddMessage_50.In(logic_uScript_AddMessage_messageData_50, logic_uScript_AddMessage_speaker_50);
		if (logic_uScript_AddMessage_uScript_AddMessage_50.Out)
		{
			Relay_In_283();
		}
	}

	private void Relay_Out_52()
	{
		Relay_In_59();
	}

	private void Relay_In_52()
	{
		int num = 0;
		Array array = charger01SpawnData;
		if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_52.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_52, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_52, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array array2 = turretGroup01SpawnData;
		if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_52.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_52, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_52, num2, array2.Length);
		num2 += array2.Length;
		int num3 = 0;
		Array array3 = turretGroup01AnimTriggers;
		if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_52.Length != num3 + array3.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_52, num3 + array3.Length);
		}
		Array.Copy(array3, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_52, num3, array3.Length);
		num3 += array3.Length;
		int num4 = 0;
		Array array4 = turretGroup01AnimDurations;
		if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_52.Length != num4 + array4.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_52, num4 + array4.Length);
		}
		Array.Copy(array4, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_52, num4, array4.Length);
		num4 += array4.Length;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_52 = turretWeaponBlockType;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_52 = turretShieldBlockType;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_52.In(logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_52, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_52, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_52, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_52, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_52, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_52);
	}

	private void Relay_Out_59()
	{
		Relay_In_66();
	}

	private void Relay_In_59()
	{
		int num = 0;
		Array array = charger02SpawnData;
		if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_59.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_59, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_59, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array array2 = turretGroup02SpawnData;
		if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_59.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_59, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_59, num2, array2.Length);
		num2 += array2.Length;
		int num3 = 0;
		Array array3 = turretGroup02AnimTriggers;
		if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_59.Length != num3 + array3.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_59, num3 + array3.Length);
		}
		Array.Copy(array3, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_59, num3, array3.Length);
		num3 += array3.Length;
		int num4 = 0;
		Array array4 = turretGroup02AnimDurations;
		if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_59.Length != num4 + array4.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_59, num4 + array4.Length);
		}
		Array.Copy(array4, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_59, num4, array4.Length);
		num4 += array4.Length;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_59 = turretWeaponBlockType;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_59 = turretShieldBlockType;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_59.In(logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_59, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_59, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_59, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_59, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_59, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_59);
	}

	private void Relay_Out_66()
	{
		Relay_In_142();
	}

	private void Relay_In_66()
	{
		int num = 0;
		Array array = charger03SpawnData;
		if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_66.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_66, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_66, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array array2 = turretGroup03SpawnData;
		if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_66.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_66, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_66, num2, array2.Length);
		num2 += array2.Length;
		int num3 = 0;
		Array array3 = turretGroup03AnimTriggers;
		if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_66.Length != num3 + array3.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_66, num3 + array3.Length);
		}
		Array.Copy(array3, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_66, num3, array3.Length);
		num3 += array3.Length;
		int num4 = 0;
		Array array4 = turretGroup03AnimDurations;
		if (logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_66.Length != num4 + array4.Length)
		{
			Array.Resize(ref logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_66, num4 + array4.Length);
		}
		Array.Copy(array4, 0, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_66, num4, array4.Length);
		num4 += array4.Length;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_66 = turretWeaponBlockType;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_66 = turretShieldBlockType;
		logic_SubGraph_RR_LaserLab_TurretAndChargerController_SubGraph_RR_LaserLab_TurretAndChargerController_66.In(logic_SubGraph_RR_LaserLab_TurretAndChargerController_chargerSpawnData_66, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretSpawnData_66, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimTriggers_66, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretAnimDurations_66, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretWeaponBlockType_66, logic_SubGraph_RR_LaserLab_TurretAndChargerController_turretShieldBlockType_66);
	}

	private void Relay_InitialSpawn_72()
	{
		int num = 0;
		Array array = charger02SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_72.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_72, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_72, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_72 = owner_Connection_74;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_72.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_72, logic_uScript_SpawnTechsFromData_ownerNode_72, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_72, logic_uScript_SpawnTechsFromData_allowResurrection_72);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_72.Out)
		{
			Relay_InitialSpawn_79();
		}
	}

	private void Relay_InitialSpawn_73()
	{
		int num = 0;
		Array array = turretGroup02SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_73.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_73, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_73, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_73 = owner_Connection_69;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_73.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_73, logic_uScript_SpawnTechsFromData_ownerNode_73, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_73, logic_uScript_SpawnTechsFromData_allowResurrection_73);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_73.Out)
		{
			Relay_InitialSpawn_72();
		}
	}

	private void Relay_InitialSpawn_75()
	{
		int num = 0;
		Array array = charger03SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_75.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_75, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_75, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_75 = owner_Connection_78;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_75.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_75, logic_uScript_SpawnTechsFromData_ownerNode_75, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_75, logic_uScript_SpawnTechsFromData_allowResurrection_75);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_75.Out)
		{
			Relay_InitialSpawn_281();
		}
	}

	private void Relay_InitialSpawn_79()
	{
		int num = 0;
		Array array = turretGroup03SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_79.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_79, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_79, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_79 = owner_Connection_76;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_79.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_79, logic_uScript_SpawnTechsFromData_ownerNode_79, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_79, logic_uScript_SpawnTechsFromData_allowResurrection_79);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_79.Out)
		{
			Relay_InitialSpawn_75();
		}
	}

	private void Relay_In_81()
	{
		logic_uScriptCon_CompareBool_Bool_81 = local_Turrets01Destroyed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.In(logic_uScriptCon_CompareBool_Bool_81);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.False;
		if (num)
		{
			Relay_In_83();
			Relay_In_185();
		}
		if (flag)
		{
			Relay_In_87();
		}
	}

	private void Relay_In_83()
	{
		logic_uScriptCon_CompareBool_Bool_83 = local_Turrets02Destroyed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83.In(logic_uScriptCon_CompareBool_Bool_83);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83.False;
		if (num)
		{
			Relay_In_86();
		}
		if (flag)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_86()
	{
		logic_uScriptCon_CompareBool_Bool_86 = local_Turrets03Destroyed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86.In(logic_uScriptCon_CompareBool_Bool_86);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86.False;
		if (num)
		{
			Relay_In_50();
		}
		if (flag)
		{
			Relay_In_89();
		}
	}

	private void Relay_In_87()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87.Out)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_88()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_88.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_88.Out)
		{
			Relay_In_89();
		}
	}

	private void Relay_In_89()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_89.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_89.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_In_91()
	{
		int num = 0;
		Array array = charger01SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_91.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_91, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_91, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_91 = owner_Connection_90;
		logic_uScript_GetAndCheckTechs_Return_91 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_91.In(logic_uScript_GetAndCheckTechs_techData_91, logic_uScript_GetAndCheckTechs_ownerNode_91, ref logic_uScript_GetAndCheckTechs_techs_91);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_91.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_91.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_91.AllDead;
		if (allAlive)
		{
			Relay_In_94();
		}
		if (someAlive)
		{
			Relay_In_94();
		}
		if (allDead)
		{
			Relay_In_105();
		}
	}

	private void Relay_In_94()
	{
		int num = 0;
		Array array = charger02SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_94.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_94, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_94, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_94 = owner_Connection_92;
		logic_uScript_GetAndCheckTechs_Return_94 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_94.In(logic_uScript_GetAndCheckTechs_techData_94, logic_uScript_GetAndCheckTechs_ownerNode_94, ref logic_uScript_GetAndCheckTechs_techs_94);
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_94.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_94.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_94.AllDead;
		if (allAlive)
		{
			Relay_In_32();
		}
		if (someAlive)
		{
			Relay_In_32();
		}
		if (allDead)
		{
			Relay_In_109();
		}
	}

	private void Relay_Output1_103()
	{
		Relay_In_116();
	}

	private void Relay_Output2_103()
	{
		Relay_In_127();
	}

	private void Relay_Output3_103()
	{
		Relay_In_134();
	}

	private void Relay_Output4_103()
	{
	}

	private void Relay_Output5_103()
	{
	}

	private void Relay_Output6_103()
	{
	}

	private void Relay_Output7_103()
	{
	}

	private void Relay_Output8_103()
	{
	}

	private void Relay_In_103()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_103 = local_NumChargersDestroyed_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_103.In(logic_uScriptCon_ManualSwitch_CurrentOutput_103);
	}

	private void Relay_In_105()
	{
		logic_uScriptAct_AddInt_v2_A_105 = local_NumChargersDestroyed_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_105.In(logic_uScriptAct_AddInt_v2_A_105, logic_uScriptAct_AddInt_v2_B_105, out logic_uScriptAct_AddInt_v2_IntResult_105, out logic_uScriptAct_AddInt_v2_FloatResult_105);
		local_NumChargersDestroyed_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_105;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_105.Out)
		{
			Relay_In_94();
		}
	}

	private void Relay_In_107()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_107.In(logic_uScriptAct_SetInt_Value_107, out logic_uScriptAct_SetInt_Target_107);
		local_NumChargersDestroyed_System_Int32 = logic_uScriptAct_SetInt_Target_107;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_107.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_In_109()
	{
		logic_uScriptAct_AddInt_v2_A_109 = local_NumChargersDestroyed_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_109.In(logic_uScriptAct_AddInt_v2_A_109, logic_uScriptAct_AddInt_v2_B_109, out logic_uScriptAct_AddInt_v2_IntResult_109, out logic_uScriptAct_AddInt_v2_FloatResult_109);
		local_NumChargersDestroyed_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_109;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_109.Out)
		{
			Relay_In_32();
		}
	}

	private void Relay_In_112()
	{
		logic_uScriptAct_AddInt_v2_A_112 = local_NumChargersDestroyed_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_112.In(logic_uScriptAct_AddInt_v2_A_112, logic_uScriptAct_AddInt_v2_B_112, out logic_uScriptAct_AddInt_v2_IntResult_112, out logic_uScriptAct_AddInt_v2_FloatResult_112);
		local_NumChargersDestroyed_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_112;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_112.Out)
		{
			Relay_In_103();
		}
	}

	private void Relay_In_114()
	{
		logic_uScript_FlyTechUpAndAway_tech_114 = local_NPCTech02_Tank;
		logic_uScript_FlyTechUpAndAway_removalParticles_114 = NPCDespawnEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_114.In(logic_uScript_FlyTechUpAndAway_tech_114, logic_uScript_FlyTechUpAndAway_maxLifetime_114, logic_uScript_FlyTechUpAndAway_targetHeight_114, logic_uScript_FlyTechUpAndAway_aiTree_114, logic_uScript_FlyTechUpAndAway_removalParticles_114);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_114.Out)
		{
			Relay_Succeed_35();
		}
	}

	private void Relay_In_116()
	{
		logic_uScriptCon_CompareInt_A_116 = local_NumChargerMessagesShown_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_116.In(logic_uScriptCon_CompareInt_A_116, logic_uScriptCon_CompareInt_B_116);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_116.EqualTo)
		{
			Relay_In_122();
		}
	}

	private void Relay_In_118()
	{
		logic_uScriptAct_AddInt_v2_A_118 = local_NumChargerMessagesShown_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_118.In(logic_uScriptAct_AddInt_v2_A_118, logic_uScriptAct_AddInt_v2_B_118, out logic_uScriptAct_AddInt_v2_IntResult_118, out logic_uScriptAct_AddInt_v2_FloatResult_118);
		local_NumChargerMessagesShown_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_118;
	}

	private void Relay_In_122()
	{
		logic_uScript_AddMessage_messageData_122 = msg02aShieldsDown;
		logic_uScript_AddMessage_speaker_122 = npcSpeaker;
		logic_uScript_AddMessage_Return_122 = logic_uScript_AddMessage_uScript_AddMessage_122.In(logic_uScript_AddMessage_messageData_122, logic_uScript_AddMessage_speaker_122);
		if (logic_uScript_AddMessage_uScript_AddMessage_122.Out)
		{
			Relay_In_118();
		}
	}

	private void Relay_In_126()
	{
		logic_uScript_AddMessage_messageData_126 = msg02bShieldsDown;
		logic_uScript_AddMessage_speaker_126 = npcSpeaker;
		logic_uScript_AddMessage_Return_126 = logic_uScript_AddMessage_uScript_AddMessage_126.In(logic_uScript_AddMessage_messageData_126, logic_uScript_AddMessage_speaker_126);
		if (logic_uScript_AddMessage_uScript_AddMessage_126.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_In_127()
	{
		logic_uScriptCon_CompareInt_A_127 = local_NumChargerMessagesShown_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_127.In(logic_uScriptCon_CompareInt_A_127, logic_uScriptCon_CompareInt_B_127);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_127.EqualTo)
		{
			Relay_In_126();
		}
	}

	private void Relay_In_129()
	{
		logic_uScriptAct_AddInt_v2_A_129 = local_NumChargerMessagesShown_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_129.In(logic_uScriptAct_AddInt_v2_A_129, logic_uScriptAct_AddInt_v2_B_129, out logic_uScriptAct_AddInt_v2_IntResult_129, out logic_uScriptAct_AddInt_v2_FloatResult_129);
		local_NumChargerMessagesShown_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_129;
	}

	private void Relay_In_133()
	{
		logic_uScript_AddMessage_messageData_133 = msg02cShieldsDown;
		logic_uScript_AddMessage_speaker_133 = npcSpeaker;
		logic_uScript_AddMessage_Return_133 = logic_uScript_AddMessage_uScript_AddMessage_133.In(logic_uScript_AddMessage_messageData_133, logic_uScript_AddMessage_speaker_133);
		if (logic_uScript_AddMessage_uScript_AddMessage_133.Out)
		{
			Relay_In_136();
		}
	}

	private void Relay_In_134()
	{
		logic_uScriptCon_CompareInt_A_134 = local_NumChargerMessagesShown_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_134.In(logic_uScriptCon_CompareInt_A_134, logic_uScriptCon_CompareInt_B_134);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_134.EqualTo)
		{
			Relay_In_133();
		}
	}

	private void Relay_In_136()
	{
		logic_uScriptAct_AddInt_v2_A_136 = local_NumChargerMessagesShown_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_136.In(logic_uScriptAct_AddInt_v2_A_136, logic_uScriptAct_AddInt_v2_B_136, out logic_uScriptAct_AddInt_v2_IntResult_136, out logic_uScriptAct_AddInt_v2_FloatResult_136);
		local_NumChargerMessagesShown_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_136;
	}

	private void Relay_Save_Out_137()
	{
		Relay_Save_267();
	}

	private void Relay_Load_Out_137()
	{
		Relay_Load_267();
	}

	private void Relay_Restart_Out_137()
	{
		Relay_Restart_267();
	}

	private void Relay_Save_137()
	{
		logic_SubGraph_SaveLoadInt_integer_137 = local_NumChargerMessagesShown_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_137 = local_NumChargerMessagesShown_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137.Save(logic_SubGraph_SaveLoadInt_restartValue_137, ref logic_SubGraph_SaveLoadInt_integer_137, logic_SubGraph_SaveLoadInt_intAsVariable_137, logic_SubGraph_SaveLoadInt_uniqueID_137);
	}

	private void Relay_Load_137()
	{
		logic_SubGraph_SaveLoadInt_integer_137 = local_NumChargerMessagesShown_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_137 = local_NumChargerMessagesShown_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137.Load(logic_SubGraph_SaveLoadInt_restartValue_137, ref logic_SubGraph_SaveLoadInt_integer_137, logic_SubGraph_SaveLoadInt_intAsVariable_137, logic_SubGraph_SaveLoadInt_uniqueID_137);
	}

	private void Relay_Restart_137()
	{
		logic_SubGraph_SaveLoadInt_integer_137 = local_NumChargerMessagesShown_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_137 = local_NumChargerMessagesShown_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_137.Restart(logic_SubGraph_SaveLoadInt_restartValue_137, ref logic_SubGraph_SaveLoadInt_integer_137, logic_SubGraph_SaveLoadInt_intAsVariable_137, logic_SubGraph_SaveLoadInt_uniqueID_137);
	}

	private void Relay_In_142()
	{
		int num = 0;
		Array array = charger01SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_142.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_142, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_142, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_142 = owner_Connection_140;
		int num2 = 0;
		Array array2 = local_139_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_142.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_142, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_142, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_142 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_142.In(logic_uScript_GetAndCheckTechs_techData_142, logic_uScript_GetAndCheckTechs_ownerNode_142, ref logic_uScript_GetAndCheckTechs_techs_142);
		local_139_TankArray = logic_uScript_GetAndCheckTechs_techs_142;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_142.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_142.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_142.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_142.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_144();
		}
		if (someAlive)
		{
			Relay_In_144();
		}
		if (allDead)
		{
			Relay_In_160();
		}
		if (waitingToSpawn)
		{
			Relay_In_160();
		}
	}

	private void Relay_SetInvulnerable_143()
	{
		int num = 0;
		Array array = local_139_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_143.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_143, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_143, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_143.SetInvulnerable(logic_uScript_SetTechsInvulnerable_techs_143);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_143.Out)
		{
			Relay_In_149();
		}
	}

	private void Relay_SetVulnerable_143()
	{
		int num = 0;
		Array array = local_139_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_143.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_143, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_143, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_143.SetVulnerable(logic_uScript_SetTechsInvulnerable_techs_143);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_143.Out)
		{
			Relay_In_149();
		}
	}

	private void Relay_In_144()
	{
		logic_uScriptCon_CompareInt_A_144 = local_CurrentObjective_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_144.In(logic_uScriptCon_CompareInt_A_144, logic_uScriptCon_CompareInt_B_144);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_144.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_144.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_SetVulnerable_143();
		}
		if (lessThan)
		{
			Relay_SetInvulnerable_143();
		}
	}

	private void Relay_In_146()
	{
		logic_uScriptCon_CompareInt_A_146 = local_CurrentObjective_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_146.In(logic_uScriptCon_CompareInt_A_146, logic_uScriptCon_CompareInt_B_146);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_146.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_146.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_SetVulnerable_147();
		}
		if (lessThan)
		{
			Relay_SetInvulnerable_147();
		}
	}

	private void Relay_SetInvulnerable_147()
	{
		int num = 0;
		Array array = local_152_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_147.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_147, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_147, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_147.SetInvulnerable(logic_uScript_SetTechsInvulnerable_techs_147);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_147.Out)
		{
			Relay_In_153();
		}
	}

	private void Relay_SetVulnerable_147()
	{
		int num = 0;
		Array array = local_152_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_147.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_147, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_147, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_147.SetVulnerable(logic_uScript_SetTechsInvulnerable_techs_147);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_147.Out)
		{
			Relay_In_153();
		}
	}

	private void Relay_In_149()
	{
		int num = 0;
		Array array = charger02SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_149.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_149, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_149, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_149 = owner_Connection_150;
		int num2 = 0;
		Array array2 = local_152_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_149.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_149, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_149, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_149 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_149.In(logic_uScript_GetAndCheckTechs_techData_149, logic_uScript_GetAndCheckTechs_ownerNode_149, ref logic_uScript_GetAndCheckTechs_techs_149);
		local_152_TankArray = logic_uScript_GetAndCheckTechs_techs_149;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_149.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_149.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_149.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_149.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_146();
		}
		if (someAlive)
		{
			Relay_In_146();
		}
		if (allDead)
		{
			Relay_In_161();
		}
		if (waitingToSpawn)
		{
			Relay_In_161();
		}
	}

	private void Relay_In_153()
	{
		int num = 0;
		Array array = charger03SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_153.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_153, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_153, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_153 = owner_Connection_156;
		int num2 = 0;
		Array array2 = local_159_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_153.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_153, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_153, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_153 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_153.In(logic_uScript_GetAndCheckTechs_techData_153, logic_uScript_GetAndCheckTechs_ownerNode_153, ref logic_uScript_GetAndCheckTechs_techs_153);
		local_159_TankArray = logic_uScript_GetAndCheckTechs_techs_153;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_153.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_153.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_153.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_153.WaitingToSpawn;
		if (allAlive)
		{
			Relay_In_154();
		}
		if (someAlive)
		{
			Relay_In_154();
		}
		if (allDead)
		{
			Relay_In_162();
		}
		if (waitingToSpawn)
		{
			Relay_In_162();
		}
	}

	private void Relay_In_154()
	{
		logic_uScriptCon_CompareInt_A_154 = local_CurrentObjective_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_154.In(logic_uScriptCon_CompareInt_A_154, logic_uScriptCon_CompareInt_B_154);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_154.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_154.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_SetVulnerable_158();
		}
		if (lessThan)
		{
			Relay_SetInvulnerable_158();
		}
	}

	private void Relay_SetInvulnerable_158()
	{
		int num = 0;
		Array array = local_159_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_158.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_158, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_158, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_158.SetInvulnerable(logic_uScript_SetTechsInvulnerable_techs_158);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_158.Out)
		{
			Relay_In_208();
		}
	}

	private void Relay_SetVulnerable_158()
	{
		int num = 0;
		Array array = local_159_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_158.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_158, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_158, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_158.SetVulnerable(logic_uScript_SetTechsInvulnerable_techs_158);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_158.Out)
		{
			Relay_In_208();
		}
	}

	private void Relay_In_160()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_160.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_160.Out)
		{
			Relay_In_161();
		}
	}

	private void Relay_In_161()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_161.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_161.Out)
		{
			Relay_In_162();
		}
	}

	private void Relay_In_162()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_162.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_162.Out)
		{
			Relay_In_208();
		}
	}

	private void Relay_InitialSpawn_168()
	{
		int num = 0;
		Array array = npcSpawnData02;
		if (logic_uScript_SpawnTechsFromData_spawnData_168.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_168, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_168, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_168 = owner_Connection_167;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_168.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_168, logic_uScript_SpawnTechsFromData_ownerNode_168, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_168, logic_uScript_SpawnTechsFromData_allowResurrection_168);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_168.Out)
		{
			Relay_In_43();
		}
	}

	private void Relay_In_170()
	{
		logic_uScript_FlyTechUpAndAway_tech_170 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_removalParticles_170 = NPCDespawnEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_170.In(logic_uScript_FlyTechUpAndAway_tech_170, logic_uScript_FlyTechUpAndAway_maxLifetime_170, logic_uScript_FlyTechUpAndAway_targetHeight_170, logic_uScript_FlyTechUpAndAway_aiTree_170, logic_uScript_FlyTechUpAndAway_removalParticles_170);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_170.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_In_173()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_173 = NPCTriggerVolume01;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_173.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_173);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_173.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_173.OutOfRange;
		if (inRange)
		{
			Relay_In_177();
		}
		if (outOfRange)
		{
			Relay_In_301();
		}
	}

	private void Relay_In_176()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_176 = NPCTriggerVolume02;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_176.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_176);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_176.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_176.OutOfRange;
		if (inRange)
		{
			Relay_In_46();
		}
		if (outOfRange)
		{
			Relay_In_304();
		}
	}

	private void Relay_In_177()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_177.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_177.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_177.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_39();
		}
		if (multiplayer)
		{
			Relay_In_179();
		}
	}

	private void Relay_In_179()
	{
		logic_uScript_AddMessage_messageData_179 = msg01IntroMP;
		logic_uScript_AddMessage_speaker_179 = npcSpeaker;
		logic_uScript_AddMessage_Return_179 = logic_uScript_AddMessage_uScript_AddMessage_179.In(logic_uScript_AddMessage_messageData_179, logic_uScript_AddMessage_speaker_179);
		local_MsgIntro_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_179;
		bool num = logic_uScript_AddMessage_uScript_AddMessage_179.Out;
		bool shown = logic_uScript_AddMessage_uScript_AddMessage_179.Shown;
		if (num)
		{
			Relay_In_196();
		}
		if (shown)
		{
			Relay_In_199();
		}
	}

	private void Relay_In_183()
	{
		logic_uScript_AddMessage_messageData_183 = msg02FirstRoomClear;
		logic_uScript_AddMessage_speaker_183 = npcSpeaker;
		logic_uScript_AddMessage_Return_183 = logic_uScript_AddMessage_uScript_AddMessage_183.In(logic_uScript_AddMessage_messageData_183, logic_uScript_AddMessage_speaker_183);
	}

	private void Relay_In_185()
	{
		logic_uScriptCon_CompareBool_Bool_185 = local_MsgFirstRoomClearShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_185.In(logic_uScriptCon_CompareBool_Bool_185);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_185.False)
		{
			Relay_True_187();
		}
	}

	private void Relay_True_187()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_187.True(out logic_uScriptAct_SetBool_Target_187);
		local_MsgFirstRoomClearShown_System_Boolean = logic_uScriptAct_SetBool_Target_187;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_187.Out)
		{
			Relay_In_183();
		}
	}

	private void Relay_False_187()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_187.False(out logic_uScriptAct_SetBool_Target_187);
		local_MsgFirstRoomClearShown_System_Boolean = logic_uScriptAct_SetBool_Target_187;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_187.Out)
		{
			Relay_In_183();
		}
	}

	private void Relay_In_189()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_189 = room02TriggerVolume;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_189 = room02TriggerVolume;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_189.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_189, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_189, ref logic_uScript_IsPlayerInTriggerSmart_inside_189);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_189.Out;
		bool firstEntered = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_189.FirstEntered;
		if (num)
		{
			Relay_In_217();
		}
		if (firstEntered)
		{
			Relay_True_192();
		}
	}

	private void Relay_True_192()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_192.True(out logic_uScriptAct_SetBool_Target_192);
		local_MsgFirstRoomClearShown_System_Boolean = logic_uScriptAct_SetBool_Target_192;
	}

	private void Relay_False_192()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_192.False(out logic_uScriptAct_SetBool_Target_192);
		local_MsgFirstRoomClearShown_System_Boolean = logic_uScriptAct_SetBool_Target_192;
	}

	private void Relay_Save_Out_193()
	{
		Relay_Save_226();
	}

	private void Relay_Load_Out_193()
	{
		Relay_Load_226();
	}

	private void Relay_Restart_Out_193()
	{
		Relay_Set_False_226();
	}

	private void Relay_Save_193()
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = local_MsgFirstRoomClearShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_193 = local_MsgFirstRoomClearShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Save(ref logic_SubGraph_SaveLoadBool_boolean_193, logic_SubGraph_SaveLoadBool_boolAsVariable_193, logic_SubGraph_SaveLoadBool_uniqueID_193);
	}

	private void Relay_Load_193()
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = local_MsgFirstRoomClearShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_193 = local_MsgFirstRoomClearShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Load(ref logic_SubGraph_SaveLoadBool_boolean_193, logic_SubGraph_SaveLoadBool_boolAsVariable_193, logic_SubGraph_SaveLoadBool_uniqueID_193);
	}

	private void Relay_Set_True_193()
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = local_MsgFirstRoomClearShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_193 = local_MsgFirstRoomClearShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_193, logic_SubGraph_SaveLoadBool_boolAsVariable_193, logic_SubGraph_SaveLoadBool_uniqueID_193);
	}

	private void Relay_Set_False_193()
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = local_MsgFirstRoomClearShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_193 = local_MsgFirstRoomClearShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_193, logic_SubGraph_SaveLoadBool_boolAsVariable_193, logic_SubGraph_SaveLoadBool_uniqueID_193);
	}

	private void Relay_In_196()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_196 = room01TriggerVolume;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_196.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_196);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_196.InRange)
		{
			Relay_In_197();
		}
	}

	private void Relay_In_197()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_197 = NPCTriggerVolume01;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_197.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_197);
		if (logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_197.OutOfRange)
		{
			Relay_In_201();
		}
	}

	private void Relay_In_199()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_199.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_199.Out)
		{
			Relay_In_170();
		}
	}

	private void Relay_In_201()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_201 = local_MsgIntro_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_201.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_201, logic_uScript_RemoveOnScreenMessage_instant_201);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_201.Out)
		{
			Relay_In_170();
		}
	}

	private void Relay_Pause_204()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_204.Pause();
	}

	private void Relay_UnPause_204()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_204.UnPause();
	}

	private void Relay_In_206()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_206 = owner_Connection_207;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_206.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_206);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_206.Out;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_206.True;
		bool flag2 = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_206.False;
		if (num)
		{
			Relay_In_277();
		}
		if (flag)
		{
			Relay_Pause_204();
		}
		if (flag2)
		{
			Relay_UnPause_204();
		}
	}

	private void Relay_In_208()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_208 = owner_Connection_205;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_208.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_208);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_208.Out)
		{
			Relay_In_206();
		}
	}

	private void Relay_In_212()
	{
		int num = 0;
		Array array = npcSpawnData02;
		if (logic_uScript_GetAndCheckTechs_techData_212.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_212, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_212, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_212 = owner_Connection_210;
		int num2 = 0;
		Array array2 = local_209_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_212.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_212, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_212, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_212 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_212.In(logic_uScript_GetAndCheckTechs_techData_212, logic_uScript_GetAndCheckTechs_ownerNode_212, ref logic_uScript_GetAndCheckTechs_techs_212);
		local_209_TankArray = logic_uScript_GetAndCheckTechs_techs_212;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_212.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_212.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_213();
		}
		if (someAlive)
		{
			Relay_AtIndex_213();
		}
	}

	private void Relay_AtIndex_213()
	{
		int num = 0;
		Array array = local_209_TankArray;
		if (logic_uScript_AccessListTech_techList_213.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_213, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_213, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_213.AtIndex(ref logic_uScript_AccessListTech_techList_213, logic_uScript_AccessListTech_index_213, out logic_uScript_AccessListTech_value_213);
		local_209_TankArray = logic_uScript_AccessListTech_techList_213;
		local_NPCTech02_Tank = logic_uScript_AccessListTech_value_213;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_213.Out)
		{
			Relay_In_228();
		}
	}

	private void Relay_In_215()
	{
		logic_uScript_SetEncounterTargetPosition_positionName_215 = objectiveMarkerPosition;
		logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_215.In(logic_uScript_SetEncounterTargetPosition_positionName_215);
		if (logic_uScript_SetEncounterTargetPosition_uScript_SetEncounterTargetPosition_215.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_217()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_217 = chargerMsgTriggerVolume;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_217.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_217);
		bool num = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_217.Out;
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_217.InRange;
		if (num)
		{
			Relay_In_81();
		}
		if (inRange)
		{
			Relay_In_219();
		}
	}

	private void Relay_In_219()
	{
		logic_uScriptCon_CompareBool_Bool_219 = local_MsgChargerShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_219.In(logic_uScriptCon_CompareBool_Bool_219);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_219.False)
		{
			Relay_True_224();
		}
	}

	private void Relay_In_221()
	{
		logic_uScript_AddMessage_messageData_221 = msg02ShieldChargerInfo;
		logic_uScript_AddMessage_speaker_221 = npcSpeaker;
		logic_uScript_AddMessage_Return_221 = logic_uScript_AddMessage_uScript_AddMessage_221.In(logic_uScript_AddMessage_messageData_221, logic_uScript_AddMessage_speaker_221);
	}

	private void Relay_True_224()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_224.True(out logic_uScriptAct_SetBool_Target_224);
		local_MsgChargerShown_System_Boolean = logic_uScriptAct_SetBool_Target_224;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_224.Out)
		{
			Relay_In_221();
		}
	}

	private void Relay_False_224()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_224.False(out logic_uScriptAct_SetBool_Target_224);
		local_MsgChargerShown_System_Boolean = logic_uScriptAct_SetBool_Target_224;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_224.Out)
		{
			Relay_In_221();
		}
	}

	private void Relay_Save_Out_226()
	{
		Relay_Save_137();
	}

	private void Relay_Load_Out_226()
	{
		Relay_Load_137();
	}

	private void Relay_Restart_Out_226()
	{
		Relay_Restart_137();
	}

	private void Relay_Save_226()
	{
		logic_SubGraph_SaveLoadBool_boolean_226 = local_MsgChargerShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_226 = local_MsgChargerShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.Save(ref logic_SubGraph_SaveLoadBool_boolean_226, logic_SubGraph_SaveLoadBool_boolAsVariable_226, logic_SubGraph_SaveLoadBool_uniqueID_226);
	}

	private void Relay_Load_226()
	{
		logic_SubGraph_SaveLoadBool_boolean_226 = local_MsgChargerShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_226 = local_MsgChargerShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.Load(ref logic_SubGraph_SaveLoadBool_boolean_226, logic_SubGraph_SaveLoadBool_boolAsVariable_226, logic_SubGraph_SaveLoadBool_uniqueID_226);
	}

	private void Relay_Set_True_226()
	{
		logic_SubGraph_SaveLoadBool_boolean_226 = local_MsgChargerShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_226 = local_MsgChargerShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_226, logic_SubGraph_SaveLoadBool_boolAsVariable_226, logic_SubGraph_SaveLoadBool_uniqueID_226);
	}

	private void Relay_Set_False_226()
	{
		logic_SubGraph_SaveLoadBool_boolean_226 = local_MsgChargerShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_226 = local_MsgChargerShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_226.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_226, logic_SubGraph_SaveLoadBool_boolAsVariable_226, logic_SubGraph_SaveLoadBool_uniqueID_226);
	}

	private void Relay_In_228()
	{
		logic_uScript_SetEncounterTarget_owner_228 = owner_Connection_230;
		logic_uScript_SetEncounterTarget_visibleObject_228 = local_NPCTech02_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_228.In(logic_uScript_SetEncounterTarget_owner_228, logic_uScript_SetEncounterTarget_visibleObject_228);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_228.Out)
		{
			Relay_In_176();
		}
	}

	private void Relay_In_235()
	{
		logic_uScriptCon_CompareBool_Bool_235 = local_SetShutterStates_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_235.In(logic_uScriptCon_CompareBool_Bool_235);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_235.False)
		{
			Relay_True_297();
		}
	}

	private void Relay_Reset_236()
	{
		int num = 0;
		Array array = switchObjectNames;
		if (logic_uScriptAct_ForEachListString_List_236.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_ForEachListString_List_236, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_ForEachListString_List_236, num, array.Length);
		num += array.Length;
		logic_uScriptAct_ForEachListString_uScriptAct_ForEachListString_236.Reset(logic_uScriptAct_ForEachListString_List_236, out logic_uScriptAct_ForEachListString_Value_236, out logic_uScriptAct_ForEachListString_currentIndex_236);
		local_CurrentSwitch_System_String = logic_uScriptAct_ForEachListString_Value_236;
		if (logic_uScriptAct_ForEachListString_uScriptAct_ForEachListString_236.Iteration)
		{
			Relay_In_239();
		}
	}

	private void Relay_In_236()
	{
		int num = 0;
		Array array = switchObjectNames;
		if (logic_uScriptAct_ForEachListString_List_236.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_ForEachListString_List_236, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_ForEachListString_List_236, num, array.Length);
		num += array.Length;
		logic_uScriptAct_ForEachListString_uScriptAct_ForEachListString_236.In(logic_uScriptAct_ForEachListString_List_236, out logic_uScriptAct_ForEachListString_Value_236, out logic_uScriptAct_ForEachListString_currentIndex_236);
		local_CurrentSwitch_System_String = logic_uScriptAct_ForEachListString_Value_236;
		if (logic_uScriptAct_ForEachListString_uScriptAct_ForEachListString_236.Iteration)
		{
			Relay_In_239();
		}
	}

	private void Relay_In_239()
	{
		logic_uScriptCon_CompareInt_A_239 = local_NumGatesOpened_System_Int32;
		logic_uScriptCon_CompareInt_B_239 = local_CurrentSwitchIndex_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_239.In(logic_uScriptCon_CompareInt_A_239, logic_uScriptCon_CompareInt_B_239);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_239.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_239.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_246();
		}
		if (lessThan)
		{
			Relay_In_269();
		}
	}

	private void Relay_In_241()
	{
		logic_uScriptAct_AddInt_v2_A_241 = local_CurrentSwitchIndex_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_241.In(logic_uScriptAct_AddInt_v2_A_241, logic_uScriptAct_AddInt_v2_B_241, out logic_uScriptAct_AddInt_v2_IntResult_241, out logic_uScriptAct_AddInt_v2_FloatResult_241);
		local_CurrentSwitchIndex_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_241;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_241.Out)
		{
			Relay_In_271();
		}
	}

	private void Relay_In_243()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_243.In(logic_uScriptAct_SetInt_Value_243, out logic_uScriptAct_SetInt_Target_243);
		local_CurrentSwitchIndex_System_Int32 = logic_uScriptAct_SetInt_Target_243;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_243.Out)
		{
			Relay_In_236();
		}
	}

	private void Relay_In_246()
	{
		logic_uScript_SetLabShutterSwitch_switchToOpen_246 = local_CurrentSwitch_System_String;
		logic_uScript_SetLabShutterSwitch_uScript_SetLabShutterSwitch_246.In(logic_uScript_SetLabShutterSwitch_switchToOpen_246, logic_uScript_SetLabShutterSwitch_switchToClose_246, logic_uScript_SetLabShutterSwitch_batchSwitchesToOpen_246, logic_uScript_SetLabShutterSwitch_batchSwitchesToClose_246);
		if (logic_uScript_SetLabShutterSwitch_uScript_SetLabShutterSwitch_246.Out)
		{
			Relay_In_241();
		}
	}

	private void Relay_In_248()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_248.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_248.Out)
		{
			Relay_In_236();
		}
	}

	private void Relay_In_250()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_250 = switch01TriggerVolume;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_250 = switch01TriggerVolume;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_250.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_250, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_250, ref logic_uScript_IsPlayerInTriggerSmart_inside_250);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_250.Out;
		bool firstEntered = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_250.FirstEntered;
		if (num)
		{
			Relay_In_252();
		}
		if (firstEntered)
		{
			Relay_In_255();
		}
	}

	private void Relay_In_252()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_252 = switch02TriggerVolume;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_252 = switch02TriggerVolume;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_252.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_252, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_252, ref logic_uScript_IsPlayerInTriggerSmart_inside_252);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_252.Out;
		bool firstEntered = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_252.FirstEntered;
		if (num)
		{
			Relay_In_253();
		}
		if (firstEntered)
		{
			Relay_In_259();
		}
	}

	private void Relay_In_253()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_253 = switch03TriggerVolume;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_253 = switch03TriggerVolume;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_253.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_253, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_253, ref logic_uScript_IsPlayerInTriggerSmart_inside_253);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_253.Out;
		bool firstEntered = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_253.FirstEntered;
		if (num)
		{
			Relay_In_20();
		}
		if (firstEntered)
		{
			Relay_In_263();
		}
	}

	private void Relay_In_255()
	{
		logic_uScriptCon_CompareInt_A_255 = local_NumGatesOpened_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_255.In(logic_uScriptCon_CompareInt_A_255, logic_uScriptCon_CompareInt_B_255);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_255.LessThan)
		{
			Relay_In_257();
		}
	}

	private void Relay_In_257()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_257.In(logic_uScriptAct_SetInt_Value_257, out logic_uScriptAct_SetInt_Target_257);
		local_NumGatesOpened_System_Int32 = logic_uScriptAct_SetInt_Target_257;
	}

	private void Relay_In_259()
	{
		logic_uScriptCon_CompareInt_A_259 = local_NumGatesOpened_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_259.In(logic_uScriptCon_CompareInt_A_259, logic_uScriptCon_CompareInt_B_259);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_259.LessThan)
		{
			Relay_In_262();
		}
	}

	private void Relay_In_262()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_262.In(logic_uScriptAct_SetInt_Value_262, out logic_uScriptAct_SetInt_Target_262);
		local_NumGatesOpened_System_Int32 = logic_uScriptAct_SetInt_Target_262;
	}

	private void Relay_In_263()
	{
		logic_uScriptCon_CompareInt_A_263 = local_NumGatesOpened_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_263.In(logic_uScriptCon_CompareInt_A_263, logic_uScriptCon_CompareInt_B_263);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_263.LessThan)
		{
			Relay_In_265();
		}
	}

	private void Relay_In_265()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_265.In(logic_uScriptAct_SetInt_Value_265, out logic_uScriptAct_SetInt_Target_265);
		local_NumGatesOpened_System_Int32 = logic_uScriptAct_SetInt_Target_265;
	}

	private void Relay_Save_Out_267()
	{
	}

	private void Relay_Load_Out_267()
	{
		Relay_In_24();
	}

	private void Relay_Restart_Out_267()
	{
		Relay_False_300();
	}

	private void Relay_Save_267()
	{
		logic_SubGraph_SaveLoadInt_integer_267 = local_NumGatesOpened_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_267 = local_NumGatesOpened_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267.Save(logic_SubGraph_SaveLoadInt_restartValue_267, ref logic_SubGraph_SaveLoadInt_integer_267, logic_SubGraph_SaveLoadInt_intAsVariable_267, logic_SubGraph_SaveLoadInt_uniqueID_267);
	}

	private void Relay_Load_267()
	{
		logic_SubGraph_SaveLoadInt_integer_267 = local_NumGatesOpened_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_267 = local_NumGatesOpened_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267.Load(logic_SubGraph_SaveLoadInt_restartValue_267, ref logic_SubGraph_SaveLoadInt_integer_267, logic_SubGraph_SaveLoadInt_intAsVariable_267, logic_SubGraph_SaveLoadInt_uniqueID_267);
	}

	private void Relay_Restart_267()
	{
		logic_SubGraph_SaveLoadInt_integer_267 = local_NumGatesOpened_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_267 = local_NumGatesOpened_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_267.Restart(logic_SubGraph_SaveLoadInt_restartValue_267, ref logic_SubGraph_SaveLoadInt_integer_267, logic_SubGraph_SaveLoadInt_intAsVariable_267, logic_SubGraph_SaveLoadInt_uniqueID_267);
	}

	private void Relay_In_269()
	{
		logic_uScript_SetLabShutterSwitch_switchToClose_269 = local_CurrentSwitch_System_String;
		logic_uScript_SetLabShutterSwitch_uScript_SetLabShutterSwitch_269.In(logic_uScript_SetLabShutterSwitch_switchToOpen_269, logic_uScript_SetLabShutterSwitch_switchToClose_269, logic_uScript_SetLabShutterSwitch_batchSwitchesToOpen_269, logic_uScript_SetLabShutterSwitch_batchSwitchesToClose_269);
		if (logic_uScript_SetLabShutterSwitch_uScript_SetLabShutterSwitch_269.Out)
		{
			Relay_In_241();
		}
	}

	private void Relay_In_271()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_271.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_271.Out)
		{
			Relay_In_272();
		}
	}

	private void Relay_In_272()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_272.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_272.Out)
		{
			Relay_In_248();
		}
	}

	private void Relay_In_276()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_276.In();
		if (logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_276.SinglePlayer)
		{
			Relay_In_243();
		}
	}

	private void Relay_In_277()
	{
		logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_277.In();
		bool singlePlayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_277.SinglePlayer;
		bool multiplayer = logic_uScript_IsMultiplayerMode_uScript_IsMultiplayerMode_277.Multiplayer;
		if (singlePlayer)
		{
			Relay_In_250();
		}
		if (multiplayer)
		{
			Relay_In_278();
		}
	}

	private void Relay_In_278()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_278.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_278.Out)
		{
			Relay_In_279();
		}
	}

	private void Relay_In_279()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_279.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_279.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_InitialSpawn_281()
	{
		int num = 0;
		Array array = forcefieldSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_281.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_281, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_281, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_281 = owner_Connection_280;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_281.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_281, logic_uScript_SpawnTechsFromData_ownerNode_281, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_281, logic_uScript_SpawnTechsFromData_allowResurrection_281);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_281.Out)
		{
			Relay_In_215();
		}
	}

	private void Relay_In_283()
	{
		logic_uScript_RemoveTech_tech_283 = local_ForcefieldTech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_283.In(logic_uScript_RemoveTech_tech_283);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_283.Out)
		{
			Relay_InitialSpawn_168();
		}
	}

	private void Relay_In_289()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_289.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_289.Out)
		{
			Relay_In_296();
		}
	}

	private void Relay_In_291()
	{
		int num = 0;
		Array array = forcefieldSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_291.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_291, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_291, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_291 = owner_Connection_286;
		int num2 = 0;
		Array array2 = local_285_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_291.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_291, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_291, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_291 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_291.In(logic_uScript_GetAndCheckTechs_techData_291, logic_uScript_GetAndCheckTechs_ownerNode_291, ref logic_uScript_GetAndCheckTechs_techs_291);
		local_285_TankArray = logic_uScript_GetAndCheckTechs_techs_291;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_291.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_291.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_291.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_291.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_292();
		}
		if (someAlive)
		{
			Relay_AtIndex_292();
		}
		if (allDead)
		{
			Relay_In_289();
		}
		if (waitingToSpawn)
		{
			Relay_In_289();
		}
	}

	private void Relay_AtIndex_292()
	{
		int num = 0;
		Array array = local_285_TankArray;
		if (logic_uScript_AccessListTech_techList_292.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_292, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_292, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_292.AtIndex(ref logic_uScript_AccessListTech_techList_292, logic_uScript_AccessListTech_index_292, out logic_uScript_AccessListTech_value_292);
		local_285_TankArray = logic_uScript_AccessListTech_techList_292;
		local_ForcefieldTech_Tank = logic_uScript_AccessListTech_value_292;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_292.Out)
		{
			Relay_Hide_293();
		}
	}

	private void Relay_Show_293()
	{
		logic_uScript_SetTechMarkerState_tech_293 = local_ForcefieldTech_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_293.Show(logic_uScript_SetTechMarkerState_tech_293);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_293.Out)
		{
			Relay_In_294();
		}
	}

	private void Relay_Hide_293()
	{
		logic_uScript_SetTechMarkerState_tech_293 = local_ForcefieldTech_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_293.Hide(logic_uScript_SetTechMarkerState_tech_293);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_293.Out)
		{
			Relay_In_294();
		}
	}

	private void Relay_In_294()
	{
		logic_uScript_SetTankInvulnerable_tank_294 = local_ForcefieldTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_294.In(logic_uScript_SetTankInvulnerable_invulnerable_294, logic_uScript_SetTankInvulnerable_tank_294);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_294.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_In_296()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_296.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_296.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_True_297()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_297.True(out logic_uScriptAct_SetBool_Target_297);
		local_SetShutterStates_System_Boolean = logic_uScriptAct_SetBool_Target_297;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_297.Out)
		{
			Relay_In_276();
		}
	}

	private void Relay_False_297()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_297.False(out logic_uScriptAct_SetBool_Target_297);
		local_SetShutterStates_System_Boolean = logic_uScriptAct_SetBool_Target_297;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_297.Out)
		{
			Relay_In_276();
		}
	}

	private void Relay_True_300()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_300.True(out logic_uScriptAct_SetBool_Target_300);
		local_SetShutterStates_System_Boolean = logic_uScriptAct_SetBool_Target_300;
	}

	private void Relay_False_300()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_300.False(out logic_uScriptAct_SetBool_Target_300);
		local_SetShutterStates_System_Boolean = logic_uScriptAct_SetBool_Target_300;
	}

	private void Relay_In_301()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_301 = local_MsgIntro_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_301.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_301, logic_uScript_RemoveOnScreenMessage_instant_301);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_301.Out)
		{
			Relay_In_305();
		}
	}

	private void Relay_In_304()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_304 = local_MsgMissionComplete_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_304.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_304, logic_uScript_RemoveOnScreenMessage_instant_304);
	}

	private void Relay_In_305()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_305.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_305.Out)
		{
			Relay_In_196();
		}
	}
}
