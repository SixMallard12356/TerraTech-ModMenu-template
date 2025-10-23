using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_UnlockHawkeyeLicense : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public GameObject animPrefabToInstantiate;

	private GameObject animPrefabToInstantiate_previous;

	[Multiline(3)]
	public string animSpawnPos = "";

	[Multiline(3)]
	public string animStateToCheck = "";

	public SpawnBlockData[] crashedBlocksData = new SpawnBlockData[0];

	public SpawnTechData[] crashedTechData = new SpawnTechData[0];

	public Transform craterPrefab;

	public float distCrashAnimTriggered;

	public float distCrashedTechBlowUp;

	public float distCrashedTechSpotted;

	public float enemySpawnDelay;

	public SpawnTechData[] enemyTechData = new SpawnTechData[0];

	public Transform explosionPrefab;

	private Vector3 local_100_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private Vector3 local_29_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private Tank[] local_70_TankArray = new Tank[0];

	private TankBlock[] local_75_TankBlockArray = new TankBlock[0];

	private Tank[] local_83_TankArray = new Tank[0];

	private Vector3 local_86_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private Animator local_Animator_UnityEngine_Animator;

	private bool local_CrashAnimStarted_System_Boolean;

	private bool local_CrashAnimTriggered_System_Boolean;

	private Tank local_CrashedTech_Tank;

	private bool local_CrashedTechBlownUp_System_Boolean;

	private bool local_CrashedTechFound_System_Boolean;

	private bool local_CrashedTechSpawned_System_Boolean;

	private bool local_MsgMissionStartShown_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private bool local_TechCrashed_System_Boolean;

	public float looseBlockDamagePercent;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	public uScript_AddMessage.MessageData msg01MissionStart;

	public uScript_AddMessage.MessageData msg02TechCrashing;

	public uScript_AddMessage.MessageData msg03CrashedTechFound;

	public uScript_AddMessage.MessageData msg04CrashedTechBlownUp;

	public uScript_AddMessage.MessageData msg05EnemiesIncoming;

	public float sceneryRemovalRadius;

	public Transform smokestackPrefab;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_4;

	private GameObject owner_Connection_28;

	private GameObject owner_Connection_38;

	private GameObject owner_Connection_39;

	private GameObject owner_Connection_48;

	private GameObject owner_Connection_55;

	private GameObject owner_Connection_59;

	private GameObject owner_Connection_64;

	private GameObject owner_Connection_65;

	private GameObject owner_Connection_79;

	private GameObject owner_Connection_80;

	private GameObject owner_Connection_81;

	private GameObject owner_Connection_85;

	private GameObject owner_Connection_90;

	private GameObject owner_Connection_91;

	private GameObject owner_Connection_119;

	private GameObject owner_Connection_134;

	private GameObject owner_Connection_162;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_3;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_3 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_3 = "CrashedTechSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_7;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_7 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_7 = "CrashedTechFound";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_8;

	private bool logic_uScriptCon_CompareBool_True_8 = true;

	private bool logic_uScriptCon_CompareBool_False_8 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_11;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_11 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_11 = "CrashAnimStarted";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_14;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_14 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_14 = "CrashAnimTriggered";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_15;

	private bool logic_uScriptCon_CompareBool_True_15 = true;

	private bool logic_uScriptCon_CompareBool_False_15 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_17 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_17;

	private bool logic_uScriptAct_SetBool_Out_17 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_17 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_17 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_18;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_18 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_18 = "MsgMissionStartShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_20;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_20 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_20 = "CrashedTechBlownUp";

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_22;

	private uScript_SpawnEncounterObject logic_uScript_SpawnEncounterObject_uScript_SpawnEncounterObject_24 = new uScript_SpawnEncounterObject();

	private GameObject logic_uScript_SpawnEncounterObject_ownerNode_24;

	private Transform logic_uScript_SpawnEncounterObject_encounterObjectToSpawn_24;

	private string logic_uScript_SpawnEncounterObject_nameWithinEncounter_24 = "Crater";

	private string logic_uScript_SpawnEncounterObject_positionName_24 = "";

	private bool logic_uScript_SpawnEncounterObject_Out_24 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_27 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_27;

	private bool logic_uScriptCon_CompareBool_True_27 = true;

	private bool logic_uScriptCon_CompareBool_False_27 = true;

	private uScript_PlayAnim logic_uScript_PlayAnim_uScript_PlayAnim_31 = new uScript_PlayAnim();

	private GameObject logic_uScript_PlayAnim_ownerNode_31;

	private GameObject logic_uScript_PlayAnim_animPrefab_31;

	private string logic_uScript_PlayAnim_spawnPosName_31 = "";

	private Animator logic_uScript_PlayAnim_Return_31;

	private bool logic_uScript_PlayAnim_Out_31 = true;

	private uScript_IsAnimPlaying logic_uScript_IsAnimPlaying_uScript_IsAnimPlaying_35 = new uScript_IsAnimPlaying();

	private Animator logic_uScript_IsAnimPlaying_animator_35;

	private string logic_uScript_IsAnimPlaying_animStateToCheck_35 = "";

	private bool logic_uScript_IsAnimPlaying_removeOnFinsh_35 = true;

	private bool logic_uScript_IsAnimPlaying_True_35 = true;

	private bool logic_uScript_IsAnimPlaying_False_35 = true;

	private uScript_PlayerInRange logic_uScript_PlayerInRange_uScript_PlayerInRange_36 = new uScript_PlayerInRange();

	private Vector3 logic_uScript_PlayerInRange_position_36;

	private float logic_uScript_PlayerInRange_range_36;

	private bool logic_uScript_PlayerInRange_True_36 = true;

	private bool logic_uScript_PlayerInRange_False_36 = true;

	private bool logic_uScript_PlayerInRange_Out_36 = true;

	private uScript_SpawnVFX logic_uScript_SpawnVFX_uScript_SpawnVFX_37 = new uScript_SpawnVFX();

	private GameObject logic_uScript_SpawnVFX_ownerNode_37;

	private Transform logic_uScript_SpawnVFX_vfxToSpawn_37;

	private string logic_uScript_SpawnVFX_spawnPosName_37 = "";

	private bool logic_uScript_SpawnVFX_Out_37 = true;

	private uScript_SpawnEncounterObject logic_uScript_SpawnEncounterObject_uScript_SpawnEncounterObject_44 = new uScript_SpawnEncounterObject();

	private GameObject logic_uScript_SpawnEncounterObject_ownerNode_44;

	private Transform logic_uScript_SpawnEncounterObject_encounterObjectToSpawn_44;

	private string logic_uScript_SpawnEncounterObject_nameWithinEncounter_44 = "SmokeStack";

	private string logic_uScript_SpawnEncounterObject_positionName_44 = "";

	private bool logic_uScript_SpawnEncounterObject_Out_44 = true;

	private uScript_GetPositionInEncounter logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_50 = new uScript_GetPositionInEncounter();

	private GameObject logic_uScript_GetPositionInEncounter_ownerNode_50;

	private string logic_uScript_GetPositionInEncounter_posName_50 = "";

	private Vector3 logic_uScript_GetPositionInEncounter_Return_50;

	private bool logic_uScript_GetPositionInEncounter_Out_50 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_52 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_52;

	private bool logic_uScriptAct_SetBool_Out_52 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_52 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_52 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_54 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_54;

	private bool logic_uScriptAct_SetBool_Out_54 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_54 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_54 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_57 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_57;

	private string logic_uScript_RemoveScenery_positionName_57 = "";

	private float logic_uScript_RemoveScenery_radius_57;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_57 = true;

	private bool logic_uScript_RemoveScenery_Out_57 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_58;

	private bool logic_uScriptCon_CompareBool_True_58 = true;

	private bool logic_uScriptCon_CompareBool_False_58 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_62 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_62;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_62;

	private uScript_GetPositionInEncounter logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_66 = new uScript_GetPositionInEncounter();

	private GameObject logic_uScript_GetPositionInEncounter_ownerNode_66;

	private string logic_uScript_GetPositionInEncounter_posName_66 = "";

	private Vector3 logic_uScript_GetPositionInEncounter_Return_66;

	private bool logic_uScript_GetPositionInEncounter_Out_66 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_67 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_67 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_67;

	private bool logic_uScript_SetTankInvulnerable_Out_67 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_68;

	private bool logic_uScriptCon_CompareBool_True_68 = true;

	private bool logic_uScriptCon_CompareBool_False_68 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_71 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_71;

	private float logic_uScript_IsPlayerInRangeOfTech_range_71;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_71 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_71 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_71 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_71 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_72 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_73 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_73;

	private bool logic_uScriptAct_SetBool_Out_73 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_73 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_73 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_74 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_74;

	private bool logic_uScriptAct_SetBool_Out_74 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_74 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_74 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_76 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_76;

	private Tank logic_uScript_SetTankInvulnerable_tank_76;

	private bool logic_uScript_SetTankInvulnerable_Out_76 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_84;

	private bool logic_uScriptCon_CompareBool_True_84 = true;

	private bool logic_uScriptCon_CompareBool_False_84 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_88 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_88 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_88;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_88 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_88;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_88 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_88 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_88 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_88 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_94 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_94 = new Tank[0];

	private int logic_uScript_AccessListTech_index_94;

	private Tank logic_uScript_AccessListTech_value_94;

	private bool logic_uScript_AccessListTech_Out_94 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_96 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_96 = new Tank[0];

	private float logic_uScript_DamageTechs_damagePercentage_96 = 1000f;

	private bool logic_uScript_DamageTechs_givePlayerCredit_96;

	private float logic_uScript_DamageTechs_leaveBlocksPercentage_96;

	private bool logic_uScript_DamageTechs_Out_96 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_97 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_97;

	private object logic_uScript_SetEncounterTarget_visibleObject_97 = "";

	private bool logic_uScript_SetEncounterTarget_Out_97 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_102 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_102 = new Tank[0];

	private int logic_uScript_AccessListTech_index_102;

	private Tank logic_uScript_AccessListTech_value_102;

	private bool logic_uScript_AccessListTech_Out_102 = true;

	private uScript_DamageBlocks logic_uScript_DamageBlocks_uScript_DamageBlocks_105 = new uScript_DamageBlocks();

	private TankBlock[] logic_uScript_DamageBlocks_blocks_105 = new TankBlock[0];

	private float logic_uScript_DamageBlocks_damagePercentage_105;

	private bool logic_uScript_DamageBlocks_Out_105 = true;

	private uScript_GetAndCheckBlocks logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_106 = new uScript_GetAndCheckBlocks();

	private SpawnBlockData[] logic_uScript_GetAndCheckBlocks_blockData_106 = new SpawnBlockData[0];

	private GameObject logic_uScript_GetAndCheckBlocks_ownerNode_106;

	private TankBlock[] logic_uScript_GetAndCheckBlocks_blocks_106 = new TankBlock[0];

	private bool logic_uScript_GetAndCheckBlocks_AllAlive_106 = true;

	private bool logic_uScript_GetAndCheckBlocks_SomeAlive_106 = true;

	private bool logic_uScript_GetAndCheckBlocks_AllDead_106 = true;

	private bool logic_uScript_GetAndCheckBlocks_WaitingToSpawn_106 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_107 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_108 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_108 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_108;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_108 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_108 = true;

	private uScript_GetPositionInEncounter logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_111 = new uScript_GetPositionInEncounter();

	private GameObject logic_uScript_GetPositionInEncounter_ownerNode_111;

	private string logic_uScript_GetPositionInEncounter_posName_111 = "";

	private Vector3 logic_uScript_GetPositionInEncounter_Return_111;

	private bool logic_uScript_GetPositionInEncounter_Out_111 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_112 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_112 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_112;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_112 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_112;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_112 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_112 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_112 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_112 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_115;

	private bool logic_uScriptCon_CompareBool_True_115 = true;

	private bool logic_uScriptCon_CompareBool_False_115 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_116 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_116;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_116 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_116 = true;

	private uScript_SpawnBlocksFromData logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_120 = new uScript_SpawnBlocksFromData();

	private SpawnBlockData[] logic_uScript_SpawnBlocksFromData_blockData_120 = new SpawnBlockData[0];

	private GameObject logic_uScript_SpawnBlocksFromData_ownerNode_120;

	private bool logic_uScript_SpawnBlocksFromData_Out_120 = true;

	private uScript_PlayerInRange logic_uScript_PlayerInRange_uScript_PlayerInRange_121 = new uScript_PlayerInRange();

	private Vector3 logic_uScript_PlayerInRange_position_121;

	private float logic_uScript_PlayerInRange_range_121;

	private bool logic_uScript_PlayerInRange_True_121 = true;

	private bool logic_uScript_PlayerInRange_False_121 = true;

	private bool logic_uScript_PlayerInRange_Out_121 = true;

	private uScript_PlayerInRange logic_uScript_PlayerInRange_uScript_PlayerInRange_123 = new uScript_PlayerInRange();

	private Vector3 logic_uScript_PlayerInRange_position_123;

	private float logic_uScript_PlayerInRange_range_123;

	private bool logic_uScript_PlayerInRange_True_123 = true;

	private bool logic_uScript_PlayerInRange_False_123 = true;

	private bool logic_uScript_PlayerInRange_Out_123 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_125 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_125;

	private bool logic_uScriptAct_SetBool_Out_125 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_125 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_125 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_129 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_129;

	private bool logic_uScript_Wait_repeat_129;

	private bool logic_uScript_Wait_Waited_129 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_131;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_131;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_135 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_135;

	private bool logic_uScript_FinishEncounter_Out_135 = true;

	private SubGraph_DefeatEnemyTechs logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_136 = new SubGraph_DefeatEnemyTechs();

	private SpawnTechData[] logic_SubGraph_DefeatEnemyTechs_enemyTechData_136 = new SpawnTechData[0];

	private float logic_SubGraph_DefeatEnemyTechs_distEnemiesSpotted_136 = -1f;

	private string logic_SubGraph_DefeatEnemyTechs_clearSceneryPos_136 = "";

	private float logic_SubGraph_DefeatEnemyTechs_clearSceneryRadius_136;

	private LocalisedString[] logic_SubGraph_DefeatEnemyTechs_msgEnemySpotted_136 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_DefeatEnemyTechs_msgComplete_136 = new LocalisedString[0];

	private ManOnScreenMessages.Speaker logic_SubGraph_DefeatEnemyTechs_messageSpeaker_136;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_140 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_140;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_140 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_140 = "Stage";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_142 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_142;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_142;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_142;

	private bool logic_uScript_AddMessage_Out_142 = true;

	private bool logic_uScript_AddMessage_Shown_142 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_147 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_147;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_147;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_147;

	private bool logic_uScript_AddMessage_Out_147 = true;

	private bool logic_uScript_AddMessage_Shown_147 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_150 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_150;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_150;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_150;

	private bool logic_uScript_AddMessage_Out_150 = true;

	private bool logic_uScript_AddMessage_Shown_150 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_153 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_153;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_153;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_153;

	private bool logic_uScript_AddMessage_Out_153 = true;

	private bool logic_uScript_AddMessage_Shown_153 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_154 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_154;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_154;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_154;

	private bool logic_uScript_AddMessage_Out_154 = true;

	private bool logic_uScript_AddMessage_Shown_154 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_157 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_157;

	private bool logic_uScriptCon_CompareBool_True_157 = true;

	private bool logic_uScriptCon_CompareBool_False_157 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_159 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_159;

	private bool logic_uScriptAct_SetBool_Out_159 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_159 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_159 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_161;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_161 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_161 = "TechCrashed";

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_163 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_163;

	private string logic_uScript_RemoveScenery_positionName_163 = "";

	private float logic_uScript_RemoveScenery_radius_163 = 50f;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_163 = true;

	private bool logic_uScript_RemoveScenery_Out_163 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (animPrefabToInstantiate_previous != animPrefabToInstantiate || !m_RegisteredForEvents)
		{
			animPrefabToInstantiate_previous = animPrefabToInstantiate;
		}
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
		if (null == owner_Connection_4 || !m_RegisteredForEvents)
		{
			owner_Connection_4 = parentGameObject;
			if (null != owner_Connection_4)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_4.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_4.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_2;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_2;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_2;
				}
			}
		}
		if (null == owner_Connection_28 || !m_RegisteredForEvents)
		{
			owner_Connection_28 = parentGameObject;
		}
		if (null == owner_Connection_38 || !m_RegisteredForEvents)
		{
			owner_Connection_38 = parentGameObject;
		}
		if (null == owner_Connection_39 || !m_RegisteredForEvents)
		{
			owner_Connection_39 = parentGameObject;
		}
		if (null == owner_Connection_48 || !m_RegisteredForEvents)
		{
			owner_Connection_48 = parentGameObject;
		}
		if (null == owner_Connection_55 || !m_RegisteredForEvents)
		{
			owner_Connection_55 = parentGameObject;
		}
		if (null == owner_Connection_59 || !m_RegisteredForEvents)
		{
			owner_Connection_59 = parentGameObject;
		}
		if (null == owner_Connection_64 || !m_RegisteredForEvents)
		{
			owner_Connection_64 = parentGameObject;
		}
		if (null == owner_Connection_65 || !m_RegisteredForEvents)
		{
			owner_Connection_65 = parentGameObject;
		}
		if (null == owner_Connection_79 || !m_RegisteredForEvents)
		{
			owner_Connection_79 = parentGameObject;
		}
		if (null == owner_Connection_80 || !m_RegisteredForEvents)
		{
			owner_Connection_80 = parentGameObject;
		}
		if (null == owner_Connection_81 || !m_RegisteredForEvents)
		{
			owner_Connection_81 = parentGameObject;
		}
		if (null == owner_Connection_85 || !m_RegisteredForEvents)
		{
			owner_Connection_85 = parentGameObject;
		}
		if (null == owner_Connection_90 || !m_RegisteredForEvents)
		{
			owner_Connection_90 = parentGameObject;
		}
		if (null == owner_Connection_91 || !m_RegisteredForEvents)
		{
			owner_Connection_91 = parentGameObject;
		}
		if (null == owner_Connection_119 || !m_RegisteredForEvents)
		{
			owner_Connection_119 = parentGameObject;
		}
		if (null == owner_Connection_134 || !m_RegisteredForEvents)
		{
			owner_Connection_134 = parentGameObject;
		}
		if (null == owner_Connection_162 || !m_RegisteredForEvents)
		{
			owner_Connection_162 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (animPrefabToInstantiate_previous != animPrefabToInstantiate || !m_RegisteredForEvents)
		{
			animPrefabToInstantiate_previous = animPrefabToInstantiate;
		}
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
		if (!m_RegisteredForEvents && null != owner_Connection_4)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_4.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_4.AddComponent<uScript_SaveLoad>();
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
		if (null != owner_Connection_4)
		{
			uScript_SaveLoad component2 = owner_Connection_4.GetComponent<uScript_SaveLoad>();
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
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.SetParent(g);
		logic_uScript_SpawnEncounterObject_uScript_SpawnEncounterObject_24.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_27.SetParent(g);
		logic_uScript_PlayAnim_uScript_PlayAnim_31.SetParent(g);
		logic_uScript_IsAnimPlaying_uScript_IsAnimPlaying_35.SetParent(g);
		logic_uScript_PlayerInRange_uScript_PlayerInRange_36.SetParent(g);
		logic_uScript_SpawnVFX_uScript_SpawnVFX_37.SetParent(g);
		logic_uScript_SpawnEncounterObject_uScript_SpawnEncounterObject_44.SetParent(g);
		logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_50.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_52.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_54.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_57.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_62.SetParent(g);
		logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_66.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_67.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_71.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_74.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_76.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_88.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_94.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_96.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_97.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_102.SetParent(g);
		logic_uScript_DamageBlocks_uScript_DamageBlocks_105.SetParent(g);
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_106.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_108.SetParent(g);
		logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_111.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_112.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_116.SetParent(g);
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_120.SetParent(g);
		logic_uScript_PlayerInRange_uScript_PlayerInRange_121.SetParent(g);
		logic_uScript_PlayerInRange_uScript_PlayerInRange_123.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.SetParent(g);
		logic_uScript_Wait_uScript_Wait_129.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_135.SetParent(g);
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_136.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_142.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_147.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_150.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_153.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_154.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_157.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_163.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_4 = parentGameObject;
		owner_Connection_28 = parentGameObject;
		owner_Connection_38 = parentGameObject;
		owner_Connection_39 = parentGameObject;
		owner_Connection_48 = parentGameObject;
		owner_Connection_55 = parentGameObject;
		owner_Connection_59 = parentGameObject;
		owner_Connection_64 = parentGameObject;
		owner_Connection_65 = parentGameObject;
		owner_Connection_79 = parentGameObject;
		owner_Connection_80 = parentGameObject;
		owner_Connection_81 = parentGameObject;
		owner_Connection_85 = parentGameObject;
		owner_Connection_90 = parentGameObject;
		owner_Connection_91 = parentGameObject;
		owner_Connection_119 = parentGameObject;
		owner_Connection_134 = parentGameObject;
		owner_Connection_162 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_62.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.Awake();
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_136.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Save_Out += SubGraph_SaveLoadBool_Save_Out_3;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Load_Out += SubGraph_SaveLoadBool_Load_Out_3;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_3;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out += SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out += SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Save_Out += SubGraph_SaveLoadBool_Save_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Load_Out += SubGraph_SaveLoadBool_Load_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Save_Out += SubGraph_SaveLoadBool_Save_Out_14;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Load_Out += SubGraph_SaveLoadBool_Load_Out_14;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_14;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save_Out += SubGraph_SaveLoadBool_Save_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load_Out += SubGraph_SaveLoadBool_Load_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Save_Out += SubGraph_SaveLoadBool_Save_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Load_Out += SubGraph_SaveLoadBool_Load_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.Output1 += uScriptCon_ManualSwitch_Output1_22;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.Output2 += uScriptCon_ManualSwitch_Output2_22;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.Output3 += uScriptCon_ManualSwitch_Output3_22;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.Output4 += uScriptCon_ManualSwitch_Output4_22;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.Output5 += uScriptCon_ManualSwitch_Output5_22;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.Output6 += uScriptCon_ManualSwitch_Output6_22;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.Output7 += uScriptCon_ManualSwitch_Output7_22;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.Output8 += uScriptCon_ManualSwitch_Output8_22;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_62.Out += SubGraph_CompleteObjectiveStage_Out_62;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.Out += SubGraph_CompleteObjectiveStage_Out_131;
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_136.Complete += SubGraph_DefeatEnemyTechs_Complete_136;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140.Save_Out += SubGraph_SaveLoadInt_Save_Out_140;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140.Load_Out += SubGraph_SaveLoadInt_Load_Out_140;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_140;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.Save_Out += SubGraph_SaveLoadBool_Save_Out_161;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.Load_Out += SubGraph_SaveLoadBool_Load_Out_161;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_161;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_62.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.Start();
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_136.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_62.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.OnEnable();
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_136.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_62.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_67.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_71.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_76.OnDisable();
		logic_uScript_Wait_uScript_Wait_129.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.OnDisable();
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_136.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_142.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_147.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_150.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_153.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_154.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_62.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.Update();
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_136.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_62.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.OnDestroy();
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_136.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Save_Out -= SubGraph_SaveLoadBool_Save_Out_3;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Load_Out -= SubGraph_SaveLoadBool_Load_Out_3;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_3;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out -= SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out -= SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Save_Out -= SubGraph_SaveLoadBool_Save_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Load_Out -= SubGraph_SaveLoadBool_Load_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Save_Out -= SubGraph_SaveLoadBool_Save_Out_14;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Load_Out -= SubGraph_SaveLoadBool_Load_Out_14;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_14;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save_Out -= SubGraph_SaveLoadBool_Save_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load_Out -= SubGraph_SaveLoadBool_Load_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Save_Out -= SubGraph_SaveLoadBool_Save_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Load_Out -= SubGraph_SaveLoadBool_Load_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.Output1 -= uScriptCon_ManualSwitch_Output1_22;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.Output2 -= uScriptCon_ManualSwitch_Output2_22;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.Output3 -= uScriptCon_ManualSwitch_Output3_22;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.Output4 -= uScriptCon_ManualSwitch_Output4_22;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.Output5 -= uScriptCon_ManualSwitch_Output5_22;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.Output6 -= uScriptCon_ManualSwitch_Output6_22;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.Output7 -= uScriptCon_ManualSwitch_Output7_22;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.Output8 -= uScriptCon_ManualSwitch_Output8_22;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_62.Out -= SubGraph_CompleteObjectiveStage_Out_62;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.Out -= SubGraph_CompleteObjectiveStage_Out_131;
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_136.Complete -= SubGraph_DefeatEnemyTechs_Complete_136;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140.Save_Out -= SubGraph_SaveLoadInt_Save_Out_140;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140.Load_Out -= SubGraph_SaveLoadInt_Load_Out_140;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_140;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.Save_Out -= SubGraph_SaveLoadBool_Save_Out_161;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.Load_Out -= SubGraph_SaveLoadBool_Load_Out_161;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_161;
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

	private void SubGraph_SaveLoadBool_Save_Out_3(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = e.boolean;
		local_CrashedTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_3;
		Relay_Save_Out_3();
	}

	private void SubGraph_SaveLoadBool_Load_Out_3(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = e.boolean;
		local_CrashedTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_3;
		Relay_Load_Out_3();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_3(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = e.boolean;
		local_CrashedTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_3;
		Relay_Restart_Out_3();
	}

	private void SubGraph_SaveLoadBool_Save_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_CrashedTechFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Save_Out_7();
	}

	private void SubGraph_SaveLoadBool_Load_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_CrashedTechFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Load_Out_7();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_CrashedTechFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Restart_Out_7();
	}

	private void SubGraph_SaveLoadBool_Save_Out_11(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = e.boolean;
		local_CrashAnimStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_11;
		Relay_Save_Out_11();
	}

	private void SubGraph_SaveLoadBool_Load_Out_11(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = e.boolean;
		local_CrashAnimStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_11;
		Relay_Load_Out_11();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_11(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = e.boolean;
		local_CrashAnimStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_11;
		Relay_Restart_Out_11();
	}

	private void SubGraph_SaveLoadBool_Save_Out_14(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = e.boolean;
		local_CrashAnimTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_14;
		Relay_Save_Out_14();
	}

	private void SubGraph_SaveLoadBool_Load_Out_14(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = e.boolean;
		local_CrashAnimTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_14;
		Relay_Load_Out_14();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_14(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = e.boolean;
		local_CrashAnimTriggered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_14;
		Relay_Restart_Out_14();
	}

	private void SubGraph_SaveLoadBool_Save_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_MsgMissionStartShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Save_Out_18();
	}

	private void SubGraph_SaveLoadBool_Load_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_MsgMissionStartShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Load_Out_18();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_MsgMissionStartShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Restart_Out_18();
	}

	private void SubGraph_SaveLoadBool_Save_Out_20(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = e.boolean;
		local_CrashedTechBlownUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_20;
		Relay_Save_Out_20();
	}

	private void SubGraph_SaveLoadBool_Load_Out_20(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = e.boolean;
		local_CrashedTechBlownUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_20;
		Relay_Load_Out_20();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_20(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = e.boolean;
		local_CrashedTechBlownUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_20;
		Relay_Restart_Out_20();
	}

	private void uScriptCon_ManualSwitch_Output1_22(object o, EventArgs e)
	{
		Relay_Output1_22();
	}

	private void uScriptCon_ManualSwitch_Output2_22(object o, EventArgs e)
	{
		Relay_Output2_22();
	}

	private void uScriptCon_ManualSwitch_Output3_22(object o, EventArgs e)
	{
		Relay_Output3_22();
	}

	private void uScriptCon_ManualSwitch_Output4_22(object o, EventArgs e)
	{
		Relay_Output4_22();
	}

	private void uScriptCon_ManualSwitch_Output5_22(object o, EventArgs e)
	{
		Relay_Output5_22();
	}

	private void uScriptCon_ManualSwitch_Output6_22(object o, EventArgs e)
	{
		Relay_Output6_22();
	}

	private void uScriptCon_ManualSwitch_Output7_22(object o, EventArgs e)
	{
		Relay_Output7_22();
	}

	private void uScriptCon_ManualSwitch_Output8_22(object o, EventArgs e)
	{
		Relay_Output8_22();
	}

	private void SubGraph_CompleteObjectiveStage_Out_62(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_62 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_62;
		Relay_Out_62();
	}

	private void SubGraph_CompleteObjectiveStage_Out_131(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_131 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_131;
		Relay_Out_131();
	}

	private void SubGraph_DefeatEnemyTechs_Complete_136(object o, SubGraph_DefeatEnemyTechs.LogicEventArgs e)
	{
		Relay_Complete_136();
	}

	private void SubGraph_SaveLoadInt_Save_Out_140(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_140 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_140;
		Relay_Save_Out_140();
	}

	private void SubGraph_SaveLoadInt_Load_Out_140(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_140 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_140;
		Relay_Load_Out_140();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_140(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_140 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_140;
		Relay_Restart_Out_140();
	}

	private void SubGraph_SaveLoadBool_Save_Out_161(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_161 = e.boolean;
		local_TechCrashed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_161;
		Relay_Save_Out_161();
	}

	private void SubGraph_SaveLoadBool_Load_Out_161(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_161 = e.boolean;
		local_TechCrashed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_161;
		Relay_Load_Out_161();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_161(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_161 = e.boolean;
		local_TechCrashed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_161;
		Relay_Restart_Out_161();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_15();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_SaveEvent_2()
	{
		Relay_Save_140();
	}

	private void Relay_LoadEvent_2()
	{
		Relay_Load_140();
	}

	private void Relay_RestartEvent_2()
	{
		Relay_Restart_140();
	}

	private void Relay_Save_Out_3()
	{
		Relay_Save_161();
	}

	private void Relay_Load_Out_3()
	{
		Relay_Load_161();
	}

	private void Relay_Restart_Out_3()
	{
		Relay_Set_False_161();
	}

	private void Relay_Save_3()
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = local_CrashedTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_3 = local_CrashedTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Save(ref logic_SubGraph_SaveLoadBool_boolean_3, logic_SubGraph_SaveLoadBool_boolAsVariable_3, logic_SubGraph_SaveLoadBool_uniqueID_3);
	}

	private void Relay_Load_3()
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = local_CrashedTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_3 = local_CrashedTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Load(ref logic_SubGraph_SaveLoadBool_boolean_3, logic_SubGraph_SaveLoadBool_boolAsVariable_3, logic_SubGraph_SaveLoadBool_uniqueID_3);
	}

	private void Relay_Set_True_3()
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = local_CrashedTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_3 = local_CrashedTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_3, logic_SubGraph_SaveLoadBool_boolAsVariable_3, logic_SubGraph_SaveLoadBool_uniqueID_3);
	}

	private void Relay_Set_False_3()
	{
		logic_SubGraph_SaveLoadBool_boolean_3 = local_CrashedTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_3 = local_CrashedTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_3.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_3, logic_SubGraph_SaveLoadBool_boolAsVariable_3, logic_SubGraph_SaveLoadBool_uniqueID_3);
	}

	private void Relay_Save_Out_7()
	{
		Relay_Save_20();
	}

	private void Relay_Load_Out_7()
	{
		Relay_Load_20();
	}

	private void Relay_Restart_Out_7()
	{
		Relay_Set_False_20();
	}

	private void Relay_Save_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_CrashedTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_CrashedTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Load_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_CrashedTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_CrashedTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Set_True_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_CrashedTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_CrashedTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Set_False_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_CrashedTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_CrashedTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_In_8()
	{
		logic_uScriptCon_CompareBool_Bool_8 = local_CrashAnimStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_8.In(logic_uScriptCon_CompareBool_Bool_8);
	}

	private void Relay_Save_Out_11()
	{
	}

	private void Relay_Load_Out_11()
	{
		Relay_In_8();
	}

	private void Relay_Restart_Out_11()
	{
	}

	private void Relay_Save_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_CrashAnimStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_CrashAnimStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Save(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Load_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_CrashAnimStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_CrashAnimStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Load(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Set_True_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_CrashAnimStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_CrashAnimStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Set_False_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_CrashAnimStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_CrashAnimStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Save_Out_14()
	{
		Relay_Save_11();
	}

	private void Relay_Load_Out_14()
	{
		Relay_Load_11();
	}

	private void Relay_Restart_Out_14()
	{
		Relay_Set_False_11();
	}

	private void Relay_Save_14()
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = local_CrashAnimTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_14 = local_CrashAnimTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Save(ref logic_SubGraph_SaveLoadBool_boolean_14, logic_SubGraph_SaveLoadBool_boolAsVariable_14, logic_SubGraph_SaveLoadBool_uniqueID_14);
	}

	private void Relay_Load_14()
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = local_CrashAnimTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_14 = local_CrashAnimTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Load(ref logic_SubGraph_SaveLoadBool_boolean_14, logic_SubGraph_SaveLoadBool_boolAsVariable_14, logic_SubGraph_SaveLoadBool_uniqueID_14);
	}

	private void Relay_Set_True_14()
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = local_CrashAnimTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_14 = local_CrashAnimTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_14, logic_SubGraph_SaveLoadBool_boolAsVariable_14, logic_SubGraph_SaveLoadBool_uniqueID_14);
	}

	private void Relay_Set_False_14()
	{
		logic_SubGraph_SaveLoadBool_boolean_14 = local_CrashAnimTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_14 = local_CrashAnimTriggered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_14.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_14, logic_SubGraph_SaveLoadBool_boolAsVariable_14, logic_SubGraph_SaveLoadBool_uniqueID_14);
	}

	private void Relay_In_15()
	{
		logic_uScriptCon_CompareBool_Bool_15 = local_MsgMissionStartShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.In(logic_uScriptCon_CompareBool_Bool_15);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.False;
		if (num)
		{
			Relay_In_22();
		}
		if (flag)
		{
			Relay_True_17();
		}
	}

	private void Relay_True_17()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.True(out logic_uScriptAct_SetBool_Target_17);
		local_MsgMissionStartShown_System_Boolean = logic_uScriptAct_SetBool_Target_17;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_17.Out)
		{
			Relay_In_142();
		}
	}

	private void Relay_False_17()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.False(out logic_uScriptAct_SetBool_Target_17);
		local_MsgMissionStartShown_System_Boolean = logic_uScriptAct_SetBool_Target_17;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_17.Out)
		{
			Relay_In_142();
		}
	}

	private void Relay_Save_Out_18()
	{
		Relay_Save_3();
	}

	private void Relay_Load_Out_18()
	{
		Relay_Load_3();
	}

	private void Relay_Restart_Out_18()
	{
		Relay_Set_False_3();
	}

	private void Relay_Save_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_MsgMissionStartShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_MsgMissionStartShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Load_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_MsgMissionStartShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_MsgMissionStartShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Set_True_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_MsgMissionStartShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_MsgMissionStartShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Set_False_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_MsgMissionStartShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_MsgMissionStartShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Save_Out_20()
	{
		Relay_Save_14();
	}

	private void Relay_Load_Out_20()
	{
		Relay_Load_14();
	}

	private void Relay_Restart_Out_20()
	{
		Relay_Set_False_14();
	}

	private void Relay_Save_20()
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = local_CrashedTechBlownUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_20 = local_CrashedTechBlownUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Save(ref logic_SubGraph_SaveLoadBool_boolean_20, logic_SubGraph_SaveLoadBool_boolAsVariable_20, logic_SubGraph_SaveLoadBool_uniqueID_20);
	}

	private void Relay_Load_20()
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = local_CrashedTechBlownUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_20 = local_CrashedTechBlownUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Load(ref logic_SubGraph_SaveLoadBool_boolean_20, logic_SubGraph_SaveLoadBool_boolAsVariable_20, logic_SubGraph_SaveLoadBool_uniqueID_20);
	}

	private void Relay_Set_True_20()
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = local_CrashedTechBlownUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_20 = local_CrashedTechBlownUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_20, logic_SubGraph_SaveLoadBool_boolAsVariable_20, logic_SubGraph_SaveLoadBool_uniqueID_20);
	}

	private void Relay_Set_False_20()
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = local_CrashedTechBlownUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_20 = local_CrashedTechBlownUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_20, logic_SubGraph_SaveLoadBool_boolAsVariable_20, logic_SubGraph_SaveLoadBool_uniqueID_20);
	}

	private void Relay_Output1_22()
	{
		Relay_In_27();
	}

	private void Relay_Output2_22()
	{
		Relay_In_84();
	}

	private void Relay_Output3_22()
	{
		Relay_In_136();
	}

	private void Relay_Output4_22()
	{
	}

	private void Relay_Output5_22()
	{
	}

	private void Relay_Output6_22()
	{
	}

	private void Relay_Output7_22()
	{
	}

	private void Relay_Output8_22()
	{
	}

	private void Relay_In_22()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_22 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_22.In(logic_uScriptCon_ManualSwitch_CurrentOutput_22);
	}

	private void Relay_In_24()
	{
		logic_uScript_SpawnEncounterObject_ownerNode_24 = owner_Connection_38;
		logic_uScript_SpawnEncounterObject_encounterObjectToSpawn_24 = craterPrefab;
		logic_uScript_SpawnEncounterObject_positionName_24 = animSpawnPos;
		logic_uScript_SpawnEncounterObject_uScript_SpawnEncounterObject_24.In(logic_uScript_SpawnEncounterObject_ownerNode_24, logic_uScript_SpawnEncounterObject_encounterObjectToSpawn_24, logic_uScript_SpawnEncounterObject_nameWithinEncounter_24, logic_uScript_SpawnEncounterObject_positionName_24);
		if (logic_uScript_SpawnEncounterObject_uScript_SpawnEncounterObject_24.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_27()
	{
		logic_uScriptCon_CompareBool_Bool_27 = local_CrashAnimTriggered_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_27.In(logic_uScriptCon_CompareBool_Bool_27);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_27.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_27.False;
		if (num)
		{
			Relay_In_58();
		}
		if (flag)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_31()
	{
		logic_uScript_PlayAnim_ownerNode_31 = owner_Connection_55;
		if (animPrefabToInstantiate_previous != animPrefabToInstantiate || !m_RegisteredForEvents)
		{
			animPrefabToInstantiate_previous = animPrefabToInstantiate;
		}
		logic_uScript_PlayAnim_animPrefab_31 = animPrefabToInstantiate;
		logic_uScript_PlayAnim_spawnPosName_31 = animSpawnPos;
		logic_uScript_PlayAnim_Return_31 = logic_uScript_PlayAnim_uScript_PlayAnim_31.In(logic_uScript_PlayAnim_ownerNode_31, logic_uScript_PlayAnim_animPrefab_31, logic_uScript_PlayAnim_spawnPosName_31);
		local_Animator_UnityEngine_Animator = logic_uScript_PlayAnim_Return_31;
		if (logic_uScript_PlayAnim_uScript_PlayAnim_31.Out)
		{
			Relay_In_147();
		}
	}

	private void Relay_In_35()
	{
		logic_uScript_IsAnimPlaying_animator_35 = local_Animator_UnityEngine_Animator;
		logic_uScript_IsAnimPlaying_animStateToCheck_35 = animStateToCheck;
		logic_uScript_IsAnimPlaying_uScript_IsAnimPlaying_35.In(logic_uScript_IsAnimPlaying_animator_35, logic_uScript_IsAnimPlaying_animStateToCheck_35, logic_uScript_IsAnimPlaying_removeOnFinsh_35);
		if (logic_uScript_IsAnimPlaying_uScript_IsAnimPlaying_35.False)
		{
			Relay_In_62();
		}
	}

	private void Relay_In_36()
	{
		logic_uScript_PlayerInRange_position_36 = local_29_UnityEngine_Vector3;
		logic_uScript_PlayerInRange_range_36 = distCrashAnimTriggered;
		logic_uScript_PlayerInRange_uScript_PlayerInRange_36.In(logic_uScript_PlayerInRange_position_36, logic_uScript_PlayerInRange_range_36);
		if (logic_uScript_PlayerInRange_uScript_PlayerInRange_36.True)
		{
			Relay_True_54();
		}
	}

	private void Relay_In_37()
	{
		logic_uScript_SpawnVFX_ownerNode_37 = owner_Connection_39;
		logic_uScript_SpawnVFX_vfxToSpawn_37 = explosionPrefab;
		logic_uScript_SpawnVFX_spawnPosName_37 = animSpawnPos;
		logic_uScript_SpawnVFX_uScript_SpawnVFX_37.In(logic_uScript_SpawnVFX_ownerNode_37, logic_uScript_SpawnVFX_vfxToSpawn_37, logic_uScript_SpawnVFX_spawnPosName_37);
		if (logic_uScript_SpawnVFX_uScript_SpawnVFX_37.Out)
		{
			Relay_In_57();
		}
	}

	private void Relay_In_44()
	{
		logic_uScript_SpawnEncounterObject_ownerNode_44 = owner_Connection_48;
		logic_uScript_SpawnEncounterObject_encounterObjectToSpawn_44 = smokestackPrefab;
		logic_uScript_SpawnEncounterObject_positionName_44 = animSpawnPos;
		logic_uScript_SpawnEncounterObject_uScript_SpawnEncounterObject_44.In(logic_uScript_SpawnEncounterObject_ownerNode_44, logic_uScript_SpawnEncounterObject_encounterObjectToSpawn_44, logic_uScript_SpawnEncounterObject_nameWithinEncounter_44, logic_uScript_SpawnEncounterObject_positionName_44);
		if (logic_uScript_SpawnEncounterObject_uScript_SpawnEncounterObject_44.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_50()
	{
		logic_uScript_GetPositionInEncounter_ownerNode_50 = owner_Connection_59;
		logic_uScript_GetPositionInEncounter_posName_50 = animSpawnPos;
		logic_uScript_GetPositionInEncounter_Return_50 = logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_50.In(logic_uScript_GetPositionInEncounter_ownerNode_50, logic_uScript_GetPositionInEncounter_posName_50);
		local_29_UnityEngine_Vector3 = logic_uScript_GetPositionInEncounter_Return_50;
		if (logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_50.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_True_52()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_52.True(out logic_uScriptAct_SetBool_Target_52);
		local_CrashAnimStarted_System_Boolean = logic_uScriptAct_SetBool_Target_52;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_52.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_False_52()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_52.False(out logic_uScriptAct_SetBool_Target_52);
		local_CrashAnimStarted_System_Boolean = logic_uScriptAct_SetBool_Target_52;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_52.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_True_54()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_54.True(out logic_uScriptAct_SetBool_Target_54);
		local_CrashAnimTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_54;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_54.Out)
		{
			Relay_In_58();
		}
	}

	private void Relay_False_54()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_54.False(out logic_uScriptAct_SetBool_Target_54);
		local_CrashAnimTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_54;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_54.Out)
		{
			Relay_In_58();
		}
	}

	private void Relay_In_57()
	{
		logic_uScript_RemoveScenery_ownerNode_57 = owner_Connection_28;
		logic_uScript_RemoveScenery_positionName_57 = animSpawnPos;
		logic_uScript_RemoveScenery_radius_57 = sceneryRemovalRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_57.In(logic_uScript_RemoveScenery_ownerNode_57, logic_uScript_RemoveScenery_positionName_57, logic_uScript_RemoveScenery_radius_57, logic_uScript_RemoveScenery_preventChunksSpawning_57);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_57.Out)
		{
			Relay_True_125();
		}
	}

	private void Relay_In_58()
	{
		logic_uScriptCon_CompareBool_Bool_58 = local_CrashAnimStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.In(logic_uScriptCon_CompareBool_Bool_58);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.False;
		if (num)
		{
			Relay_In_35();
		}
		if (flag)
		{
			Relay_True_52();
		}
	}

	private void Relay_Out_62()
	{
	}

	private void Relay_In_62()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_62 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_62.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_62, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_62);
	}

	private void Relay_In_66()
	{
		logic_uScript_GetPositionInEncounter_ownerNode_66 = owner_Connection_79;
		logic_uScript_GetPositionInEncounter_posName_66 = animSpawnPos;
		logic_uScript_GetPositionInEncounter_Return_66 = logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_66.In(logic_uScript_GetPositionInEncounter_ownerNode_66, logic_uScript_GetPositionInEncounter_posName_66);
		local_86_UnityEngine_Vector3 = logic_uScript_GetPositionInEncounter_Return_66;
		if (logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_66.Out)
		{
			Relay_In_121();
		}
	}

	private void Relay_In_67()
	{
		logic_uScript_SetTankInvulnerable_tank_67 = local_CrashedTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_67.In(logic_uScript_SetTankInvulnerable_invulnerable_67, logic_uScript_SetTankInvulnerable_tank_67);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_67.Out)
		{
			Relay_In_97();
		}
	}

	private void Relay_In_68()
	{
		logic_uScriptCon_CompareBool_Bool_68 = local_CrashedTechBlownUp_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.In(logic_uScriptCon_CompareBool_Bool_68);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.False;
		if (num)
		{
			Relay_In_129();
		}
		if (flag)
		{
			Relay_In_66();
		}
	}

	private void Relay_In_71()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_71 = local_CrashedTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_71 = distCrashedTechSpotted;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_71.In(logic_uScript_IsPlayerInRangeOfTech_tech_71, logic_uScript_IsPlayerInRangeOfTech_range_71, logic_uScript_IsPlayerInRangeOfTech_techs_71);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_71.InRange)
		{
			Relay_In_150();
		}
	}

	private void Relay_In_72()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72.Out)
		{
			Relay_In_111();
		}
	}

	private void Relay_True_73()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.True(out logic_uScriptAct_SetBool_Target_73);
		local_CrashedTechBlownUp_System_Boolean = logic_uScriptAct_SetBool_Target_73;
	}

	private void Relay_False_73()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.False(out logic_uScriptAct_SetBool_Target_73);
		local_CrashedTechBlownUp_System_Boolean = logic_uScriptAct_SetBool_Target_73;
	}

	private void Relay_True_74()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_74.True(out logic_uScriptAct_SetBool_Target_74);
		local_CrashedTechFound_System_Boolean = logic_uScriptAct_SetBool_Target_74;
	}

	private void Relay_False_74()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_74.False(out logic_uScriptAct_SetBool_Target_74);
		local_CrashedTechFound_System_Boolean = logic_uScriptAct_SetBool_Target_74;
	}

	private void Relay_In_76()
	{
		logic_uScript_SetTankInvulnerable_tank_76 = local_CrashedTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_76.In(logic_uScript_SetTankInvulnerable_invulnerable_76, logic_uScript_SetTankInvulnerable_tank_76);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_76.Out)
		{
			Relay_In_96();
		}
	}

	private void Relay_In_84()
	{
		logic_uScriptCon_CompareBool_Bool_84 = local_TechCrashed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.In(logic_uScriptCon_CompareBool_Bool_84);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.False;
		if (num)
		{
			Relay_In_115();
		}
		if (flag)
		{
			Relay_In_157();
		}
	}

	private void Relay_In_88()
	{
		int num = 0;
		Array array = crashedTechData;
		if (logic_uScript_GetAndCheckTechs_techData_88.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_88, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_88, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_88 = owner_Connection_119;
		int num2 = 0;
		Array array2 = local_70_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_88.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_88, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_88, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_88 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_88.In(logic_uScript_GetAndCheckTechs_techData_88, logic_uScript_GetAndCheckTechs_ownerNode_88, ref logic_uScript_GetAndCheckTechs_techs_88);
		local_70_TankArray = logic_uScript_GetAndCheckTechs_techs_88;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_88.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_88.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_88.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_88.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_94();
		}
		if (someAlive)
		{
			Relay_AtIndex_94();
		}
		if (allDead)
		{
			Relay_In_72();
		}
		if (waitingToSpawn)
		{
			Relay_In_72();
		}
	}

	private void Relay_AtIndex_94()
	{
		int num = 0;
		Array array = local_70_TankArray;
		if (logic_uScript_AccessListTech_techList_94.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_94, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_94, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_94.AtIndex(ref logic_uScript_AccessListTech_techList_94, logic_uScript_AccessListTech_index_94, out logic_uScript_AccessListTech_value_94);
		local_70_TankArray = logic_uScript_AccessListTech_techList_94;
		local_CrashedTech_Tank = logic_uScript_AccessListTech_value_94;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_94.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_In_96()
	{
		int num = 0;
		if (logic_uScript_DamageTechs_techs_96.Length <= num)
		{
			Array.Resize(ref logic_uScript_DamageTechs_techs_96, num + 1);
		}
		logic_uScript_DamageTechs_techs_96[num++] = local_CrashedTech_Tank;
		logic_uScript_DamageTechs_uScript_DamageTechs_96.In(logic_uScript_DamageTechs_techs_96, logic_uScript_DamageTechs_damagePercentage_96, logic_uScript_DamageTechs_givePlayerCredit_96, logic_uScript_DamageTechs_leaveBlocksPercentage_96);
		if (logic_uScript_DamageTechs_uScript_DamageTechs_96.Out)
		{
			Relay_In_153();
		}
	}

	private void Relay_In_97()
	{
		logic_uScript_SetEncounterTarget_owner_97 = owner_Connection_85;
		logic_uScript_SetEncounterTarget_visibleObject_97 = local_CrashedTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_97.In(logic_uScript_SetEncounterTarget_owner_97, logic_uScript_SetEncounterTarget_visibleObject_97);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_97.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_AtIndex_102()
	{
		int num = 0;
		Array array = local_83_TankArray;
		if (logic_uScript_AccessListTech_techList_102.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_102, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_102, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_102.AtIndex(ref logic_uScript_AccessListTech_techList_102, logic_uScript_AccessListTech_index_102, out logic_uScript_AccessListTech_value_102);
		local_83_TankArray = logic_uScript_AccessListTech_techList_102;
		local_CrashedTech_Tank = logic_uScript_AccessListTech_value_102;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_102.Out)
		{
			Relay_In_76();
		}
	}

	private void Relay_In_105()
	{
		int num = 0;
		Array array = local_75_TankBlockArray;
		if (logic_uScript_DamageBlocks_blocks_105.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DamageBlocks_blocks_105, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DamageBlocks_blocks_105, num, array.Length);
		num += array.Length;
		logic_uScript_DamageBlocks_damagePercentage_105 = looseBlockDamagePercent;
		logic_uScript_DamageBlocks_uScript_DamageBlocks_105.In(logic_uScript_DamageBlocks_blocks_105, logic_uScript_DamageBlocks_damagePercentage_105);
		if (logic_uScript_DamageBlocks_uScript_DamageBlocks_105.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_106()
	{
		int num = 0;
		Array array = crashedBlocksData;
		if (logic_uScript_GetAndCheckBlocks_blockData_106.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blockData_106, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckBlocks_blockData_106, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckBlocks_ownerNode_106 = owner_Connection_90;
		int num2 = 0;
		Array array2 = local_75_TankBlockArray;
		if (logic_uScript_GetAndCheckBlocks_blocks_106.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blocks_106, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckBlocks_blocks_106, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_106.In(logic_uScript_GetAndCheckBlocks_blockData_106, logic_uScript_GetAndCheckBlocks_ownerNode_106, ref logic_uScript_GetAndCheckBlocks_blocks_106);
		local_75_TankBlockArray = logic_uScript_GetAndCheckBlocks_blocks_106;
		bool allAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_106.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_106.SomeAlive;
		if (allAlive)
		{
			Relay_In_105();
		}
		if (someAlive)
		{
			Relay_In_105();
		}
	}

	private void Relay_In_107()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.Out)
		{
			Relay_True_73();
		}
	}

	private void Relay_InitialSpawn_108()
	{
		int num = 0;
		Array array = crashedTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_108.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_108, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_108, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_108 = owner_Connection_64;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_108.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_108, logic_uScript_SpawnTechsFromData_ownerNode_108, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_108);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_108.Out)
		{
			Relay_In_120();
		}
	}

	private void Relay_In_111()
	{
		logic_uScript_GetPositionInEncounter_ownerNode_111 = owner_Connection_80;
		logic_uScript_GetPositionInEncounter_posName_111 = animSpawnPos;
		logic_uScript_GetPositionInEncounter_Return_111 = logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_111.In(logic_uScript_GetPositionInEncounter_ownerNode_111, logic_uScript_GetPositionInEncounter_posName_111);
		local_100_UnityEngine_Vector3 = logic_uScript_GetPositionInEncounter_Return_111;
		if (logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_111.Out)
		{
			Relay_In_123();
		}
	}

	private void Relay_In_112()
	{
		int num = 0;
		Array array = crashedTechData;
		if (logic_uScript_GetAndCheckTechs_techData_112.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_112, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_112, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_112 = owner_Connection_81;
		int num2 = 0;
		Array array2 = local_83_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_112.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_112, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_112, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_112 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_112.In(logic_uScript_GetAndCheckTechs_techData_112, logic_uScript_GetAndCheckTechs_ownerNode_112, ref logic_uScript_GetAndCheckTechs_techs_112);
		local_83_TankArray = logic_uScript_GetAndCheckTechs_techs_112;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_112.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_112.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_112.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_112.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_102();
		}
		if (someAlive)
		{
			Relay_AtIndex_102();
		}
		if (allDead)
		{
			Relay_In_107();
		}
		if (waitingToSpawn)
		{
			Relay_In_107();
		}
	}

	private void Relay_In_115()
	{
		logic_uScriptCon_CompareBool_Bool_115 = local_CrashedTechFound_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.In(logic_uScriptCon_CompareBool_Bool_115);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_115.False;
		if (num)
		{
			Relay_In_68();
		}
		if (flag)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_116()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_116 = owner_Connection_65;
		logic_uScript_MoveEncounterWithVisible_visibleObject_116 = local_CrashedTech_Tank;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_116.In(logic_uScript_MoveEncounterWithVisible_ownerNode_116, logic_uScript_MoveEncounterWithVisible_visibleObject_116);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_116.Out)
		{
			Relay_In_71();
		}
	}

	private void Relay_In_120()
	{
		int num = 0;
		Array array = crashedBlocksData;
		if (logic_uScript_SpawnBlocksFromData_blockData_120.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnBlocksFromData_blockData_120, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnBlocksFromData_blockData_120, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnBlocksFromData_ownerNode_120 = owner_Connection_91;
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_120.In(logic_uScript_SpawnBlocksFromData_blockData_120, logic_uScript_SpawnBlocksFromData_ownerNode_120);
		if (logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_120.Out)
		{
			Relay_In_106();
		}
	}

	private void Relay_In_121()
	{
		logic_uScript_PlayerInRange_position_121 = local_86_UnityEngine_Vector3;
		logic_uScript_PlayerInRange_range_121 = distCrashedTechBlowUp;
		logic_uScript_PlayerInRange_uScript_PlayerInRange_121.In(logic_uScript_PlayerInRange_position_121, logic_uScript_PlayerInRange_range_121);
		if (logic_uScript_PlayerInRange_uScript_PlayerInRange_121.True)
		{
			Relay_In_112();
		}
	}

	private void Relay_In_123()
	{
		logic_uScript_PlayerInRange_position_123 = local_100_UnityEngine_Vector3;
		logic_uScript_PlayerInRange_range_123 = distCrashedTechSpotted;
		logic_uScript_PlayerInRange_uScript_PlayerInRange_123.In(logic_uScript_PlayerInRange_position_123, logic_uScript_PlayerInRange_range_123);
		if (logic_uScript_PlayerInRange_uScript_PlayerInRange_123.True)
		{
			Relay_True_74();
		}
	}

	private void Relay_True_125()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.True(out logic_uScriptAct_SetBool_Target_125);
		local_TechCrashed_System_Boolean = logic_uScriptAct_SetBool_Target_125;
	}

	private void Relay_False_125()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.False(out logic_uScriptAct_SetBool_Target_125);
		local_TechCrashed_System_Boolean = logic_uScriptAct_SetBool_Target_125;
	}

	private void Relay_In_129()
	{
		logic_uScript_Wait_seconds_129 = enemySpawnDelay;
		logic_uScript_Wait_uScript_Wait_129.In(logic_uScript_Wait_seconds_129, logic_uScript_Wait_repeat_129);
		if (logic_uScript_Wait_uScript_Wait_129.Waited)
		{
			Relay_In_154();
		}
	}

	private void Relay_Out_131()
	{
	}

	private void Relay_In_131()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_131 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_131.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_131, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_131);
	}

	private void Relay_Succeed_135()
	{
		logic_uScript_FinishEncounter_owner_135 = owner_Connection_134;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_135.Succeed(logic_uScript_FinishEncounter_owner_135);
	}

	private void Relay_Fail_135()
	{
		logic_uScript_FinishEncounter_owner_135 = owner_Connection_134;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_135.Fail(logic_uScript_FinishEncounter_owner_135);
	}

	private void Relay_Complete_136()
	{
		Relay_Succeed_135();
	}

	private void Relay_In_136()
	{
		int num = 0;
		Array array = enemyTechData;
		if (logic_SubGraph_DefeatEnemyTechs_enemyTechData_136.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_DefeatEnemyTechs_enemyTechData_136, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_DefeatEnemyTechs_enemyTechData_136, num, array.Length);
		num += array.Length;
		logic_SubGraph_DefeatEnemyTechs_SubGraph_DefeatEnemyTechs_136.In(logic_SubGraph_DefeatEnemyTechs_enemyTechData_136, logic_SubGraph_DefeatEnemyTechs_distEnemiesSpotted_136, logic_SubGraph_DefeatEnemyTechs_clearSceneryPos_136, logic_SubGraph_DefeatEnemyTechs_clearSceneryRadius_136, logic_SubGraph_DefeatEnemyTechs_msgEnemySpotted_136, logic_SubGraph_DefeatEnemyTechs_msgComplete_136, logic_SubGraph_DefeatEnemyTechs_messageSpeaker_136);
	}

	private void Relay_Save_Out_140()
	{
		Relay_Save_18();
	}

	private void Relay_Load_Out_140()
	{
		Relay_Load_18();
	}

	private void Relay_Restart_Out_140()
	{
		Relay_Set_False_18();
	}

	private void Relay_Save_140()
	{
		logic_SubGraph_SaveLoadInt_integer_140 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_140 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140.Save(logic_SubGraph_SaveLoadInt_restartValue_140, ref logic_SubGraph_SaveLoadInt_integer_140, logic_SubGraph_SaveLoadInt_intAsVariable_140, logic_SubGraph_SaveLoadInt_uniqueID_140);
	}

	private void Relay_Load_140()
	{
		logic_SubGraph_SaveLoadInt_integer_140 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_140 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140.Load(logic_SubGraph_SaveLoadInt_restartValue_140, ref logic_SubGraph_SaveLoadInt_integer_140, logic_SubGraph_SaveLoadInt_intAsVariable_140, logic_SubGraph_SaveLoadInt_uniqueID_140);
	}

	private void Relay_Restart_140()
	{
		logic_SubGraph_SaveLoadInt_integer_140 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_140 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_140.Restart(logic_SubGraph_SaveLoadInt_restartValue_140, ref logic_SubGraph_SaveLoadInt_integer_140, logic_SubGraph_SaveLoadInt_intAsVariable_140, logic_SubGraph_SaveLoadInt_uniqueID_140);
	}

	private void Relay_In_142()
	{
		logic_uScript_AddMessage_messageData_142 = msg01MissionStart;
		logic_uScript_AddMessage_speaker_142 = messageSpeaker;
		logic_uScript_AddMessage_Return_142 = logic_uScript_AddMessage_uScript_AddMessage_142.In(logic_uScript_AddMessage_messageData_142, logic_uScript_AddMessage_speaker_142);
		if (logic_uScript_AddMessage_uScript_AddMessage_142.Out)
		{
			Relay_In_163();
		}
	}

	private void Relay_In_147()
	{
		logic_uScript_AddMessage_messageData_147 = msg02TechCrashing;
		logic_uScript_AddMessage_speaker_147 = messageSpeaker;
		logic_uScript_AddMessage_Return_147 = logic_uScript_AddMessage_uScript_AddMessage_147.In(logic_uScript_AddMessage_messageData_147, logic_uScript_AddMessage_speaker_147);
	}

	private void Relay_In_150()
	{
		logic_uScript_AddMessage_messageData_150 = msg03CrashedTechFound;
		logic_uScript_AddMessage_speaker_150 = messageSpeaker;
		logic_uScript_AddMessage_Return_150 = logic_uScript_AddMessage_uScript_AddMessage_150.In(logic_uScript_AddMessage_messageData_150, logic_uScript_AddMessage_speaker_150);
		if (logic_uScript_AddMessage_uScript_AddMessage_150.Out)
		{
			Relay_True_74();
		}
	}

	private void Relay_In_153()
	{
		logic_uScript_AddMessage_messageData_153 = msg04CrashedTechBlownUp;
		logic_uScript_AddMessage_speaker_153 = messageSpeaker;
		logic_uScript_AddMessage_Return_153 = logic_uScript_AddMessage_uScript_AddMessage_153.In(logic_uScript_AddMessage_messageData_153, logic_uScript_AddMessage_speaker_153);
		if (logic_uScript_AddMessage_uScript_AddMessage_153.Out)
		{
			Relay_True_73();
		}
	}

	private void Relay_In_154()
	{
		logic_uScript_AddMessage_messageData_154 = msg05EnemiesIncoming;
		logic_uScript_AddMessage_speaker_154 = messageSpeaker;
		logic_uScript_AddMessage_Return_154 = logic_uScript_AddMessage_uScript_AddMessage_154.In(logic_uScript_AddMessage_messageData_154, logic_uScript_AddMessage_speaker_154);
		if (logic_uScript_AddMessage_uScript_AddMessage_154.Out)
		{
			Relay_In_131();
		}
	}

	private void Relay_In_157()
	{
		logic_uScriptCon_CompareBool_Bool_157 = local_CrashedTechSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_157.In(logic_uScriptCon_CompareBool_Bool_157);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_157.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_157.False;
		if (num)
		{
			Relay_In_106();
		}
		if (flag)
		{
			Relay_True_159();
		}
	}

	private void Relay_True_159()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.True(out logic_uScriptAct_SetBool_Target_159);
		local_CrashedTechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_159;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_159.Out)
		{
			Relay_InitialSpawn_108();
		}
	}

	private void Relay_False_159()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.False(out logic_uScriptAct_SetBool_Target_159);
		local_CrashedTechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_159;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_159.Out)
		{
			Relay_InitialSpawn_108();
		}
	}

	private void Relay_Save_Out_161()
	{
		Relay_Save_7();
	}

	private void Relay_Load_Out_161()
	{
		Relay_Load_7();
	}

	private void Relay_Restart_Out_161()
	{
		Relay_Set_False_7();
	}

	private void Relay_Save_161()
	{
		logic_SubGraph_SaveLoadBool_boolean_161 = local_TechCrashed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_161 = local_TechCrashed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.Save(ref logic_SubGraph_SaveLoadBool_boolean_161, logic_SubGraph_SaveLoadBool_boolAsVariable_161, logic_SubGraph_SaveLoadBool_uniqueID_161);
	}

	private void Relay_Load_161()
	{
		logic_SubGraph_SaveLoadBool_boolean_161 = local_TechCrashed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_161 = local_TechCrashed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.Load(ref logic_SubGraph_SaveLoadBool_boolean_161, logic_SubGraph_SaveLoadBool_boolAsVariable_161, logic_SubGraph_SaveLoadBool_uniqueID_161);
	}

	private void Relay_Set_True_161()
	{
		logic_SubGraph_SaveLoadBool_boolean_161 = local_TechCrashed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_161 = local_TechCrashed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_161, logic_SubGraph_SaveLoadBool_boolAsVariable_161, logic_SubGraph_SaveLoadBool_uniqueID_161);
	}

	private void Relay_Set_False_161()
	{
		logic_SubGraph_SaveLoadBool_boolean_161 = local_TechCrashed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_161 = local_TechCrashed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_161.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_161, logic_SubGraph_SaveLoadBool_boolAsVariable_161, logic_SubGraph_SaveLoadBool_uniqueID_161);
	}

	private void Relay_In_163()
	{
		logic_uScript_RemoveScenery_ownerNode_163 = owner_Connection_162;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_163.In(logic_uScript_RemoveScenery_ownerNode_163, logic_uScript_RemoveScenery_positionName_163, logic_uScript_RemoveScenery_radius_163, logic_uScript_RemoveScenery_preventChunksSpawning_163);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_163.Out)
		{
			Relay_In_22();
		}
	}
}
