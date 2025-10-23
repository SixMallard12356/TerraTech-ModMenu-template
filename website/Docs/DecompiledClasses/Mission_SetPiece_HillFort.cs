using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_DefeatEnemyTechs", "")]
public class Mission_SetPiece_HillFort : uScriptLogic
{
	private delegate void ContinueExecution();

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private ContinueExecution m_ContinueExecution;

	private bool m_Breakpoint;

	private const int MaxRelayCallCount = 1000;

	private int relayCallCount;

	public bool AllowEnemyGroupToRespawn = true;

	public SpawnTechData[] chargerTechData = new SpawnTechData[0];

	public float DelayBetweenEnemyGroupSpaws;

	public float DelayBetweenRespawnArivals;

	public float distAtWhichNPCTechFound = 75f;

	public float distAtWhichTargetTechFound = 75f;

	public float distAtWhichTargetTechLeft = 100f;

	public SpawnTechData enemyGroupData;

	public WaveSizeSpecification EnemyGroupSize;

	public SpawnTechData[] FirstFly01TechData = new SpawnTechData[0];

	public SpawnTechData[] FirstFly02TechData = new SpawnTechData[0];

	public SpawnTechData[] FirstFly03TechData = new SpawnTechData[0];

	public SpawnTechData[] guardFliesTechData = new SpawnTechData[0];

	private string local_169_System_String = "Found";

	private string local_171_System_String = "Found";

	private string local_188_System_String = "Found";

	private string local_191_System_String = "Found";

	private string local_61_System_String = "msgMeeting";

	private string local_77_System_String = "msgMeeting";

	private string local_96_System_String = "msgMeeting";

	private Tank local_chargerTech_Tank;

	private Tank[] local_chargerTechs_TankArray = new Tank[0];

	private bool local_EnemyDeadEarly_System_Boolean;

	private bool local_FirstFliesSpawned_System_Boolean;

	private Tank[] local_MobTechs_TankArray = new Tank[0];

	private string local_Msg_System_String = "Msg";

	private bool local_MsgFound1and2Shown_System_Boolean;

	private bool local_MsgFound3and4Shown_System_Boolean;

	private bool local_NPCIgnored_System_Boolean;

	private bool local_NPCMet_System_Boolean;

	private bool local_NPCSeen_System_Boolean;

	private Tank local_NPCTank_Tank;

	private Tank[] local_NPCTanks_TankArray = new Tank[0];

	private bool local_ObjectiveComplete_System_Boolean;

	private bool local_ShownMsgEnemiesGivingUp_System_Boolean;

	private bool local_ShownMsgEnemySpotted_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private int local_StartValue_System_Int32;

	private Tank local_TargetTech_Tank;

	private bool local_TargetTechFound_System_Boolean;

	private Tank[] local_targetTechs_TankArray = new Tank[0];

	private bool local_TargetTechSpawned_System_Boolean;

	private int local_TotalGroupEnemiesSpawned_System_Int32;

	public ManOnScreenMessages.Speaker messageSpeaker;

	public ManOnScreenMessages.Speaker messageSpeakerSpider;

	public LocalisedString[] msgMissionComplete = new LocalisedString[0];

	public LocalisedString[] msgNPCGreeting = new LocalisedString[0];

	public LocalisedString[] msgNPCGreetingEnemyDead = new LocalisedString[0];

	public LocalisedString[] msgNPCGreetingInturrupt = new LocalisedString[0];

	public LocalisedString[] msgTargetTechFound1to2 = new LocalisedString[0];

	public LocalisedString[] msgTargetTechFound3to4 = new LocalisedString[0];

	public LocalisedString[] msgTargetTechFound5 = new LocalisedString[0];

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCTech = new SpawnTechData[0];

	public int ReinforcementSubGroupSize;

	public SpawnTechData[] targetTechData = new SpawnTechData[0];

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_7;

	private GameObject owner_Connection_10;

	private GameObject owner_Connection_25;

	private GameObject owner_Connection_28;

	private GameObject owner_Connection_35;

	private GameObject owner_Connection_58;

	private GameObject owner_Connection_71;

	private GameObject owner_Connection_98;

	private GameObject owner_Connection_103;

	private GameObject owner_Connection_113;

	private GameObject owner_Connection_128;

	private GameObject owner_Connection_154;

	private GameObject owner_Connection_159;

	private GameObject owner_Connection_162;

	private GameObject owner_Connection_175;

	private GameObject owner_Connection_203;

	private GameObject owner_Connection_206;

	private GameObject owner_Connection_211;

	private GameObject owner_Connection_217;

	private GameObject owner_Connection_229;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_2 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_2 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_2;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_2 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_2;

	private bool logic_uScript_SpawnTechsFromData_Out_2 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_4 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_4;

	private bool logic_uScriptAct_SetBool_Out_4 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_4 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_4 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_5;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_5 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_5 = "TargetTechSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_6;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_6 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_6 = "MsgFound1and2Shown";

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_12 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_12;

	private float logic_uScript_IsPlayerInRangeOfTech_range_12;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_12 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_12 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_12 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_12 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_15;

	private bool logic_uScriptCon_CompareBool_True_15 = true;

	private bool logic_uScriptCon_CompareBool_False_15 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_16;

	private bool logic_uScriptCon_CompareBool_True_16 = true;

	private bool logic_uScriptCon_CompareBool_False_16 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_17;

	private bool logic_uScriptCon_CompareBool_True_17 = true;

	private bool logic_uScriptCon_CompareBool_False_17 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_19 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_19;

	private bool logic_uScriptAct_SetBool_Out_19 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_19 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_19 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_20;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_20 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_20 = "TargetTechFound";

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_29 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_29 = new Tank[0];

	private int logic_uScript_AccessListTech_index_29;

	private Tank logic_uScript_AccessListTech_value_29;

	private bool logic_uScript_AccessListTech_Out_29 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_32 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_32;

	private object logic_uScript_SetEncounterTarget_visibleObject_32 = "";

	private bool logic_uScript_SetEncounterTarget_Out_32 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_33 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_33;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_33 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_33;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_33 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_33 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_33 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_33 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_37 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_37;

	private bool logic_uScript_FinishEncounter_Out_37 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_38;

	private int logic_SubGraph_SaveLoadInt_integer_38;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_38 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_38 = "EnemyGroup";

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_40 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_40 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_40 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_40;

	private string logic_uScript_AddOnScreenMessage_tag_40 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_40;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_40;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_40;

	private bool logic_uScript_AddOnScreenMessage_Out_40 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_40 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_42 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_42 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_42 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_42 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_47;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_47 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_47 = "ShownMsgEnemySpotted";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_48;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_48 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_48 = "ShownMsgEnemiesGivingUp";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_49;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_49 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_49 = "AllowEnemyGroupToRespawn";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_53 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_53;

	private bool logic_uScriptCon_CompareBool_True_53 = true;

	private bool logic_uScriptCon_CompareBool_False_53 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_54;

	private bool logic_uScriptCon_CompareBool_True_54 = true;

	private bool logic_uScriptCon_CompareBool_False_54 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_56 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_56 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_56;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_56 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_56;

	private bool logic_uScript_SpawnTechsFromData_Out_56 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_59 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_59;

	private bool logic_uScriptAct_SetBool_Out_59 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_59 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_59 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_62 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_62 = new Tank[0];

	private int logic_uScript_AccessListTech_index_62;

	private Tank logic_uScript_AccessListTech_value_62;

	private bool logic_uScript_AccessListTech_Out_62 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_63;

	private bool logic_uScriptCon_CompareBool_True_63 = true;

	private bool logic_uScriptCon_CompareBool_False_63 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_65 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_65 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_65 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_65;

	private string logic_uScript_AddOnScreenMessage_tag_65 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_65;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_65;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_65;

	private bool logic_uScript_AddOnScreenMessage_Out_65 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_65 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_66 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_66 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_66;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_66 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_73 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_73 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_73 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_73;

	private string logic_uScript_AddOnScreenMessage_tag_73 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_73;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_73;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_73;

	private bool logic_uScript_AddOnScreenMessage_Out_73 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_73 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_74 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_74;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_74 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_74 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_74;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_74;

	private bool logic_uScript_FlyTechUpAndAway_Out_74 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_78;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_78;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_82 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_82;

	private bool logic_uScriptAct_SetBool_Out_82 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_82 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_82 = true;

	private uScript_InRangeOfTech logic_uScript_InRangeOfTech_uScript_InRangeOfTech_84 = new uScript_InRangeOfTech();

	private Tank logic_uScript_InRangeOfTech_tank_84;

	private float logic_uScript_InRangeOfTech_range_84;

	private bool logic_uScript_InRangeOfTech_Out_84 = true;

	private bool logic_uScript_InRangeOfTech_InRange_84 = true;

	private bool logic_uScript_InRangeOfTech_OutOfRange_84 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_86 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_86;

	private bool logic_uScriptAct_SetBool_Out_86 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_86 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_86 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_89 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_89 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_89;

	private bool logic_uScript_SetTankInvulnerable_Out_89 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_92 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_92;

	private bool logic_uScriptAct_SetBool_Out_92 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_92 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_92 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_93 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_93 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_93 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_93;

	private string logic_uScript_AddOnScreenMessage_tag_93 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_93;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_93;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_93;

	private bool logic_uScript_AddOnScreenMessage_Out_93 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_93 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_97 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_97 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_97;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_97 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_97;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_97 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_97 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_97 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_97 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_99 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_99 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_99;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_99 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_99;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_99 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_99 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_99 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_99 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_100 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_100;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_100 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_100;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_100 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_101;

	private bool logic_uScriptCon_CompareBool_True_101 = true;

	private bool logic_uScriptCon_CompareBool_False_101 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_106;

	private uScript_SpawnTechWaveFromData logic_uScript_SpawnTechWaveFromData_uScript_SpawnTechWaveFromData_108 = new uScript_SpawnTechWaveFromData();

	private GameObject logic_uScript_SpawnTechWaveFromData_ownerNode_108;

	private SpawnTechData logic_uScript_SpawnTechWaveFromData_spawnData_108;

	private WaveSizeSpecification logic_uScript_SpawnTechWaveFromData_waveSize_108;

	private float logic_uScript_SpawnTechWaveFromData_delayBetweenSpawns_108;

	private int logic_uScript_SpawnTechWaveFromData_numSpawnedSoFar_108;

	private bool logic_uScript_SpawnTechWaveFromData_allowRespawn_108;

	private int logic_uScript_SpawnTechWaveFromData_respawnGroupSize_108;

	private float logic_uScript_SpawnTechWaveFromData_delayBetweenRespawnGroups_108;

	private int logic_uScript_SpawnTechWaveFromData_Return_108;

	private bool logic_uScript_SpawnTechWaveFromData_Out_108 = true;

	private bool logic_uScript_SpawnTechWaveFromData_RespawnedWaveGroup_108 = true;

	private bool logic_uScript_SpawnTechWaveFromData_AllTechKilled_108 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_119;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_119 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_119 = "EnemyDeadEarly";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_120;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_120 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_120 = "NPCSeen";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_121;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_121 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_121 = "NPCMet";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_123;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_123 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_123 = "ObjectiveComplete";

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_126 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_126 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_126;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_126 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_126;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_126 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_126 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_126 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_126 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_131 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_131;

	private bool logic_uScriptAct_SetBool_Out_131 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_131 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_131 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_132 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_132;

	private bool logic_uScriptAct_SetBool_Out_132 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_132 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_132 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_134 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_134;

	private bool logic_uScriptCon_CompareBool_True_134 = true;

	private bool logic_uScriptCon_CompareBool_False_134 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_137;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_137 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_137 = "NPCIgnored";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_139 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_139;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_139 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_139 = "Stage";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_140 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_140 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_143 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_143 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_143 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_143;

	private string logic_uScript_AddOnScreenMessage_tag_143 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_143;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_143;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_143;

	private bool logic_uScript_AddOnScreenMessage_Out_143 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_143 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_144 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_144 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_145 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_145;

	private int logic_uScript_SetTankTeam_team_145 = 1;

	private bool logic_uScript_SetTankTeam_Out_145 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_148 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_148;

	private bool logic_uScriptAct_SetBool_Out_148 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_148 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_148 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_150 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_150;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_150 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_150 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_150;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_150;

	private bool logic_uScript_FlyTechUpAndAway_Out_150 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_153 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_153;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_153;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_155 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_155 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_155;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_155 = 0.1f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_155;

	private bool logic_uScript_SpawnTechsFromData_Out_155 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_157 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_157 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_157;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_157 = 0.1f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_157;

	private bool logic_uScript_SpawnTechsFromData_Out_157 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_161 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_161 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_161;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_161 = 0.1f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_161;

	private bool logic_uScript_SpawnTechsFromData_Out_161 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_164 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_164;

	private bool logic_uScriptAct_SetBool_Out_164 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_164 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_164 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_165 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_165;

	private bool logic_uScriptCon_CompareBool_True_165 = true;

	private bool logic_uScriptCon_CompareBool_False_165 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_168;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_168 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_168 = "FirstFliesSpawned";

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_170 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_170 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_170;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_170 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_172 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_172 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_173 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_173 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_173 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_173;

	private float logic_uScript_DamageTechs_leaveBlksPercent_173;

	private bool logic_uScript_DamageTechs_makeVulnerable_173;

	private bool logic_uScript_DamageTechs_Out_173 = true;

	private uScript_GetAndCheckWaveTechs logic_uScript_GetAndCheckWaveTechs_uScript_GetAndCheckWaveTechs_177 = new uScript_GetAndCheckWaveTechs();

	private SpawnTechData logic_uScript_GetAndCheckWaveTechs_spawnData_177;

	private GameObject logic_uScript_GetAndCheckWaveTechs_ownerNode_177;

	private Tank[] logic_uScript_GetAndCheckWaveTechs_techs_177 = new Tank[0];

	private int logic_uScript_GetAndCheckWaveTechs_Return_177;

	private bool logic_uScript_GetAndCheckWaveTechs_AllAlive_177 = true;

	private bool logic_uScript_GetAndCheckWaveTechs_SomeAlive_177 = true;

	private bool logic_uScript_GetAndCheckWaveTechs_AllDead_177 = true;

	private bool logic_uScript_GetAndCheckWaveTechs_WaitingToSpawn_177 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_179 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_179;

	private float logic_uScript_IsPlayerInRangeOfTech_range_179;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_179 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_179 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_179 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_179 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_182 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_182;

	private bool logic_uScriptCon_CompareBool_True_182 = true;

	private bool logic_uScriptCon_CompareBool_False_182 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_184 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_184;

	private bool logic_uScriptAct_SetBool_Out_184 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_184 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_184 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_185 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_185 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_185 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_185;

	private string logic_uScript_AddOnScreenMessage_tag_185 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_185;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_185;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_185;

	private bool logic_uScript_AddOnScreenMessage_Out_185 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_185 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_194 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_194 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_194 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_194;

	private string logic_uScript_AddOnScreenMessage_tag_194 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_194;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_194;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_194;

	private bool logic_uScript_AddOnScreenMessage_Out_194 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_194 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_195 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_195;

	private bool logic_uScriptAct_SetBool_Out_195 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_195 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_195 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_198;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_198 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_198 = "MsgFound3and4Shown";

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_199 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_199 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_199;

	private bool logic_uScript_SetTankInvulnerable_Out_199 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_201 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_201;

	private Tank logic_uScript_SetTankInvulnerable_tank_201;

	private bool logic_uScript_SetTankInvulnerable_Out_201 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_204 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_204 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_204;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_204 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_204;

	private bool logic_uScript_SpawnTechsFromData_Out_204 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_207 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_207 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_207;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_207 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_207;

	private bool logic_uScript_SpawnTechsFromData_Out_207 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_210 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_210 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_210;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_210 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_210;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_210 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_210 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_210 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_210 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_213 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_213 = new Tank[0];

	private int logic_uScript_AccessListTech_index_213;

	private Tank logic_uScript_AccessListTech_value_213;

	private bool logic_uScript_AccessListTech_Out_213 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_215 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_215 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_219 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_219 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_219;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_219 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_219;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_219 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_219 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_219 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_219 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_220 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_220 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_220 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_220;

	private float logic_uScript_DamageTechs_leaveBlksPercent_220;

	private bool logic_uScript_DamageTechs_makeVulnerable_220;

	private bool logic_uScript_DamageTechs_Out_220 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_221 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_221 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_223 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_223 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_223;

	private bool logic_uScript_SetTankInvulnerable_Out_223 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_225 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_225 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_226 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_226;

	private int logic_uScript_SetTankTeam_team_226 = 1;

	private bool logic_uScript_SetTankTeam_Out_226 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_227 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_227;

	private Tank logic_uScript_SetTankInvulnerable_tank_227;

	private bool logic_uScript_SetTankInvulnerable_Out_227 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_228 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_228;

	private bool logic_uScript_ClearEncounterTarget_Out_228 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_231 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_231;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_231 = 100f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_231 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
		}
		if (null == owner_Connection_7 || !m_RegisteredForEvents)
		{
			owner_Connection_7 = parentGameObject;
			if (null != owner_Connection_7)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_7.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_7.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_8;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_8;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_8;
				}
			}
		}
		if (null == owner_Connection_10 || !m_RegisteredForEvents)
		{
			owner_Connection_10 = parentGameObject;
			if (null != owner_Connection_10)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_10.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_10.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_9;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_9;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_9;
				}
			}
		}
		if (null == owner_Connection_25 || !m_RegisteredForEvents)
		{
			owner_Connection_25 = parentGameObject;
		}
		if (null == owner_Connection_28 || !m_RegisteredForEvents)
		{
			owner_Connection_28 = parentGameObject;
		}
		if (null == owner_Connection_35 || !m_RegisteredForEvents)
		{
			owner_Connection_35 = parentGameObject;
		}
		if (null == owner_Connection_58 || !m_RegisteredForEvents)
		{
			owner_Connection_58 = parentGameObject;
		}
		if (null == owner_Connection_71 || !m_RegisteredForEvents)
		{
			owner_Connection_71 = parentGameObject;
		}
		if (null == owner_Connection_98 || !m_RegisteredForEvents)
		{
			owner_Connection_98 = parentGameObject;
		}
		if (null == owner_Connection_103 || !m_RegisteredForEvents)
		{
			owner_Connection_103 = parentGameObject;
		}
		if (null == owner_Connection_113 || !m_RegisteredForEvents)
		{
			owner_Connection_113 = parentGameObject;
		}
		if (null == owner_Connection_128 || !m_RegisteredForEvents)
		{
			owner_Connection_128 = parentGameObject;
		}
		if (null == owner_Connection_154 || !m_RegisteredForEvents)
		{
			owner_Connection_154 = parentGameObject;
		}
		if (null == owner_Connection_159 || !m_RegisteredForEvents)
		{
			owner_Connection_159 = parentGameObject;
		}
		if (null == owner_Connection_162 || !m_RegisteredForEvents)
		{
			owner_Connection_162 = parentGameObject;
		}
		if (null == owner_Connection_175 || !m_RegisteredForEvents)
		{
			owner_Connection_175 = parentGameObject;
		}
		if (null == owner_Connection_203 || !m_RegisteredForEvents)
		{
			owner_Connection_203 = parentGameObject;
		}
		if (null == owner_Connection_206 || !m_RegisteredForEvents)
		{
			owner_Connection_206 = parentGameObject;
		}
		if (null == owner_Connection_211 || !m_RegisteredForEvents)
		{
			owner_Connection_211 = parentGameObject;
		}
		if (null == owner_Connection_217 || !m_RegisteredForEvents)
		{
			owner_Connection_217 = parentGameObject;
		}
		if (null == owner_Connection_229 || !m_RegisteredForEvents)
		{
			owner_Connection_229 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_7)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_7.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_7.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_8;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_8;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_8;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_10)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_10.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_10.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_9;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_9;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_9;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_7)
		{
			uScript_SaveLoad component = owner_Connection_7.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_8;
				component.LoadEvent -= Instance_LoadEvent_8;
				component.RestartEvent -= Instance_RestartEvent_8;
			}
		}
		if (null != owner_Connection_10)
		{
			uScript_EncounterUpdate component2 = owner_Connection_10.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_9;
				component2.OnSuspend -= Instance_OnSuspend_9;
				component2.OnResume -= Instance_OnResume_9;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_2.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_12.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_19.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_29.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_32.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_37.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_40.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_42.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_53.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_56.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_59.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_62.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_65.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_66.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_73.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_74.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.SetParent(g);
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_84.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_86.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_89.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_93.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_97.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_99.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_100.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.SetParent(g);
		logic_uScript_SpawnTechWaveFromData_uScript_SpawnTechWaveFromData_108.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_126.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_131.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_134.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_140.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_143.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_144.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_145.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_148.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_150.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_153.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_155.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_157.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_161.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_164.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_165.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_170.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_172.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_173.SetParent(g);
		logic_uScript_GetAndCheckWaveTechs_uScript_GetAndCheckWaveTechs_177.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_179.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_182.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_184.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_185.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_194.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_195.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_199.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_201.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_204.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_207.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_210.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_213.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_215.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_219.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_220.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_221.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_223.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_225.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_226.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_227.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_228.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_231.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_7 = parentGameObject;
		owner_Connection_10 = parentGameObject;
		owner_Connection_25 = parentGameObject;
		owner_Connection_28 = parentGameObject;
		owner_Connection_35 = parentGameObject;
		owner_Connection_58 = parentGameObject;
		owner_Connection_71 = parentGameObject;
		owner_Connection_98 = parentGameObject;
		owner_Connection_103 = parentGameObject;
		owner_Connection_113 = parentGameObject;
		owner_Connection_128 = parentGameObject;
		owner_Connection_154 = parentGameObject;
		owner_Connection_159 = parentGameObject;
		owner_Connection_162 = parentGameObject;
		owner_Connection_175 = parentGameObject;
		owner_Connection_203 = parentGameObject;
		owner_Connection_206 = parentGameObject;
		owner_Connection_211 = parentGameObject;
		owner_Connection_217 = parentGameObject;
		owner_Connection_229 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_153.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Save_Out += SubGraph_SaveLoadBool_Save_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Load_Out += SubGraph_SaveLoadBool_Load_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Save_Out += SubGraph_SaveLoadBool_Save_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Load_Out += SubGraph_SaveLoadBool_Load_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Save_Out += SubGraph_SaveLoadBool_Save_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Load_Out += SubGraph_SaveLoadBool_Load_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_20;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Save_Out += SubGraph_SaveLoadInt_Save_Out_38;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Load_Out += SubGraph_SaveLoadInt_Load_Out_38;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Save_Out += SubGraph_SaveLoadBool_Save_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Load_Out += SubGraph_SaveLoadBool_Load_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Save_Out += SubGraph_SaveLoadBool_Save_Out_48;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Load_Out += SubGraph_SaveLoadBool_Load_Out_48;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_48;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save_Out += SubGraph_SaveLoadBool_Save_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load_Out += SubGraph_SaveLoadBool_Load_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_49;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.Out += SubGraph_CompleteObjectiveStage_Out_78;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.Output1 += uScriptCon_ManualSwitch_Output1_106;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.Output2 += uScriptCon_ManualSwitch_Output2_106;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.Output3 += uScriptCon_ManualSwitch_Output3_106;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.Output4 += uScriptCon_ManualSwitch_Output4_106;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.Output5 += uScriptCon_ManualSwitch_Output5_106;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.Output6 += uScriptCon_ManualSwitch_Output6_106;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.Output7 += uScriptCon_ManualSwitch_Output7_106;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.Output8 += uScriptCon_ManualSwitch_Output8_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Save_Out += SubGraph_SaveLoadBool_Save_Out_119;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Load_Out += SubGraph_SaveLoadBool_Load_Out_119;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_119;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.Save_Out += SubGraph_SaveLoadBool_Save_Out_120;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.Load_Out += SubGraph_SaveLoadBool_Load_Out_120;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_120;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.Save_Out += SubGraph_SaveLoadBool_Save_Out_121;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.Load_Out += SubGraph_SaveLoadBool_Load_Out_121;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_121;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Save_Out += SubGraph_SaveLoadBool_Save_Out_123;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Load_Out += SubGraph_SaveLoadBool_Load_Out_123;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_123;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Save_Out += SubGraph_SaveLoadBool_Save_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Load_Out += SubGraph_SaveLoadBool_Load_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_137;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Save_Out += SubGraph_SaveLoadInt_Save_Out_139;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Load_Out += SubGraph_SaveLoadInt_Load_Out_139;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_139;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_153.Out += SubGraph_CompleteObjectiveStage_Out_153;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Save_Out += SubGraph_SaveLoadBool_Save_Out_168;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Load_Out += SubGraph_SaveLoadBool_Load_Out_168;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_168;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Save_Out += SubGraph_SaveLoadBool_Save_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Load_Out += SubGraph_SaveLoadBool_Load_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_198;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_153.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_74.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_150.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_153.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_12.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_40.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_65.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_73.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.OnDisable();
		logic_uScript_InRangeOfTech_uScript_InRangeOfTech_84.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_89.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_93.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_100.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_143.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_153.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_179.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_185.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_194.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_199.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_201.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_223.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_227.OnDisable();
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
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_153.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_153.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Save_Out -= SubGraph_SaveLoadBool_Save_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Load_Out -= SubGraph_SaveLoadBool_Load_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Save_Out -= SubGraph_SaveLoadBool_Save_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Load_Out -= SubGraph_SaveLoadBool_Load_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Save_Out -= SubGraph_SaveLoadBool_Save_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Load_Out -= SubGraph_SaveLoadBool_Load_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_20;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Save_Out -= SubGraph_SaveLoadInt_Save_Out_38;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Load_Out -= SubGraph_SaveLoadInt_Load_Out_38;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Save_Out -= SubGraph_SaveLoadBool_Save_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Load_Out -= SubGraph_SaveLoadBool_Load_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Save_Out -= SubGraph_SaveLoadBool_Save_Out_48;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Load_Out -= SubGraph_SaveLoadBool_Load_Out_48;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_48;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save_Out -= SubGraph_SaveLoadBool_Save_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load_Out -= SubGraph_SaveLoadBool_Load_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_49;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.Out -= SubGraph_CompleteObjectiveStage_Out_78;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.Output1 -= uScriptCon_ManualSwitch_Output1_106;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.Output2 -= uScriptCon_ManualSwitch_Output2_106;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.Output3 -= uScriptCon_ManualSwitch_Output3_106;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.Output4 -= uScriptCon_ManualSwitch_Output4_106;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.Output5 -= uScriptCon_ManualSwitch_Output5_106;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.Output6 -= uScriptCon_ManualSwitch_Output6_106;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.Output7 -= uScriptCon_ManualSwitch_Output7_106;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.Output8 -= uScriptCon_ManualSwitch_Output8_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Save_Out -= SubGraph_SaveLoadBool_Save_Out_119;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Load_Out -= SubGraph_SaveLoadBool_Load_Out_119;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_119;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.Save_Out -= SubGraph_SaveLoadBool_Save_Out_120;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.Load_Out -= SubGraph_SaveLoadBool_Load_Out_120;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_120;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.Save_Out -= SubGraph_SaveLoadBool_Save_Out_121;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.Load_Out -= SubGraph_SaveLoadBool_Load_Out_121;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_121;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Save_Out -= SubGraph_SaveLoadBool_Save_Out_123;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Load_Out -= SubGraph_SaveLoadBool_Load_Out_123;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_123;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Save_Out -= SubGraph_SaveLoadBool_Save_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Load_Out -= SubGraph_SaveLoadBool_Load_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_137;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Save_Out -= SubGraph_SaveLoadInt_Save_Out_139;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Load_Out -= SubGraph_SaveLoadInt_Load_Out_139;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_139;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_153.Out -= SubGraph_CompleteObjectiveStage_Out_153;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Save_Out -= SubGraph_SaveLoadBool_Save_Out_168;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Load_Out -= SubGraph_SaveLoadBool_Load_Out_168;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_168;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Save_Out -= SubGraph_SaveLoadBool_Save_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Load_Out -= SubGraph_SaveLoadBool_Load_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_198;
	}

	private void Instance_SaveEvent_8(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_SaveEvent_8();
	}

	private void Instance_LoadEvent_8(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_LoadEvent_8();
	}

	private void Instance_RestartEvent_8(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_RestartEvent_8();
	}

	private void Instance_OnUpdate_9(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_OnUpdate_9();
	}

	private void Instance_OnSuspend_9(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_OnSuspend_9();
	}

	private void Instance_OnResume_9(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_OnResume_9();
	}

	private void SubGraph_SaveLoadBool_Save_Out_5(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = e.boolean;
		local_TargetTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_5;
		Relay_Save_Out_5();
	}

	private void SubGraph_SaveLoadBool_Load_Out_5(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = e.boolean;
		local_TargetTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_5;
		Relay_Load_Out_5();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_5(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = e.boolean;
		local_TargetTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_5;
		Relay_Restart_Out_5();
	}

	private void SubGraph_SaveLoadBool_Save_Out_6(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = e.boolean;
		local_MsgFound1and2Shown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_6;
		Relay_Save_Out_6();
	}

	private void SubGraph_SaveLoadBool_Load_Out_6(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = e.boolean;
		local_MsgFound1and2Shown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_6;
		Relay_Load_Out_6();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_6(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = e.boolean;
		local_MsgFound1and2Shown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_6;
		Relay_Restart_Out_6();
	}

	private void SubGraph_SaveLoadBool_Save_Out_20(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = e.boolean;
		local_TargetTechFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_20;
		Relay_Save_Out_20();
	}

	private void SubGraph_SaveLoadBool_Load_Out_20(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = e.boolean;
		local_TargetTechFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_20;
		Relay_Load_Out_20();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_20(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = e.boolean;
		local_TargetTechFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_20;
		Relay_Restart_Out_20();
	}

	private void SubGraph_SaveLoadInt_Save_Out_38(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_38 = e.integer;
		local_TotalGroupEnemiesSpawned_System_Int32 = logic_SubGraph_SaveLoadInt_integer_38;
		Relay_Save_Out_38();
	}

	private void SubGraph_SaveLoadInt_Load_Out_38(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_38 = e.integer;
		local_TotalGroupEnemiesSpawned_System_Int32 = logic_SubGraph_SaveLoadInt_integer_38;
		Relay_Load_Out_38();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_38(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_38 = e.integer;
		local_TotalGroupEnemiesSpawned_System_Int32 = logic_SubGraph_SaveLoadInt_integer_38;
		Relay_Restart_Out_38();
	}

	private void SubGraph_SaveLoadBool_Save_Out_47(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = e.boolean;
		local_ShownMsgEnemySpotted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_47;
		Relay_Save_Out_47();
	}

	private void SubGraph_SaveLoadBool_Load_Out_47(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = e.boolean;
		local_ShownMsgEnemySpotted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_47;
		Relay_Load_Out_47();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_47(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = e.boolean;
		local_ShownMsgEnemySpotted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_47;
		Relay_Restart_Out_47();
	}

	private void SubGraph_SaveLoadBool_Save_Out_48(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_48 = e.boolean;
		local_ShownMsgEnemiesGivingUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_48;
		Relay_Save_Out_48();
	}

	private void SubGraph_SaveLoadBool_Load_Out_48(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_48 = e.boolean;
		local_ShownMsgEnemiesGivingUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_48;
		Relay_Load_Out_48();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_48(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_48 = e.boolean;
		local_ShownMsgEnemiesGivingUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_48;
		Relay_Restart_Out_48();
	}

	private void SubGraph_SaveLoadBool_Save_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		AllowEnemyGroupToRespawn = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Save_Out_49();
	}

	private void SubGraph_SaveLoadBool_Load_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		AllowEnemyGroupToRespawn = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Load_Out_49();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		AllowEnemyGroupToRespawn = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Restart_Out_49();
	}

	private void SubGraph_CompleteObjectiveStage_Out_78(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_78 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_78;
		Relay_Out_78();
	}

	private void uScriptCon_ManualSwitch_Output1_106(object o, EventArgs e)
	{
		Relay_Output1_106();
	}

	private void uScriptCon_ManualSwitch_Output2_106(object o, EventArgs e)
	{
		Relay_Output2_106();
	}

	private void uScriptCon_ManualSwitch_Output3_106(object o, EventArgs e)
	{
		Relay_Output3_106();
	}

	private void uScriptCon_ManualSwitch_Output4_106(object o, EventArgs e)
	{
		Relay_Output4_106();
	}

	private void uScriptCon_ManualSwitch_Output5_106(object o, EventArgs e)
	{
		Relay_Output5_106();
	}

	private void uScriptCon_ManualSwitch_Output6_106(object o, EventArgs e)
	{
		Relay_Output6_106();
	}

	private void uScriptCon_ManualSwitch_Output7_106(object o, EventArgs e)
	{
		Relay_Output7_106();
	}

	private void uScriptCon_ManualSwitch_Output8_106(object o, EventArgs e)
	{
		Relay_Output8_106();
	}

	private void SubGraph_SaveLoadBool_Save_Out_119(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_119 = e.boolean;
		local_EnemyDeadEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_119;
		Relay_Save_Out_119();
	}

	private void SubGraph_SaveLoadBool_Load_Out_119(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_119 = e.boolean;
		local_EnemyDeadEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_119;
		Relay_Load_Out_119();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_119(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_119 = e.boolean;
		local_EnemyDeadEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_119;
		Relay_Restart_Out_119();
	}

	private void SubGraph_SaveLoadBool_Save_Out_120(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_120 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_120;
		Relay_Save_Out_120();
	}

	private void SubGraph_SaveLoadBool_Load_Out_120(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_120 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_120;
		Relay_Load_Out_120();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_120(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_120 = e.boolean;
		local_NPCSeen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_120;
		Relay_Restart_Out_120();
	}

	private void SubGraph_SaveLoadBool_Save_Out_121(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_121 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_121;
		Relay_Save_Out_121();
	}

	private void SubGraph_SaveLoadBool_Load_Out_121(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_121 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_121;
		Relay_Load_Out_121();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_121(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_121 = e.boolean;
		local_NPCMet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_121;
		Relay_Restart_Out_121();
	}

	private void SubGraph_SaveLoadBool_Save_Out_123(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_123 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_123;
		Relay_Save_Out_123();
	}

	private void SubGraph_SaveLoadBool_Load_Out_123(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_123 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_123;
		Relay_Load_Out_123();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_123(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_123 = e.boolean;
		local_ObjectiveComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_123;
		Relay_Restart_Out_123();
	}

	private void SubGraph_SaveLoadBool_Save_Out_137(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_137;
		Relay_Save_Out_137();
	}

	private void SubGraph_SaveLoadBool_Load_Out_137(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_137;
		Relay_Load_Out_137();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_137(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = e.boolean;
		local_NPCIgnored_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_137;
		Relay_Restart_Out_137();
	}

	private void SubGraph_SaveLoadInt_Save_Out_139(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_139 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_139;
		Relay_Save_Out_139();
	}

	private void SubGraph_SaveLoadInt_Load_Out_139(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_139 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_139;
		Relay_Load_Out_139();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_139(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_139 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_139;
		Relay_Restart_Out_139();
	}

	private void SubGraph_CompleteObjectiveStage_Out_153(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_153 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_153;
		Relay_Out_153();
	}

	private void SubGraph_SaveLoadBool_Save_Out_168(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_168 = e.boolean;
		local_FirstFliesSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_168;
		Relay_Save_Out_168();
	}

	private void SubGraph_SaveLoadBool_Load_Out_168(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_168 = e.boolean;
		local_FirstFliesSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_168;
		Relay_Load_Out_168();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_168(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_168 = e.boolean;
		local_FirstFliesSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_168;
		Relay_Restart_Out_168();
	}

	private void SubGraph_SaveLoadBool_Save_Out_198(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = e.boolean;
		local_MsgFound3and4Shown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_198;
		Relay_Save_Out_198();
	}

	private void SubGraph_SaveLoadBool_Load_Out_198(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = e.boolean;
		local_MsgFound3and4Shown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_198;
		Relay_Load_Out_198();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_198(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = e.boolean;
		local_MsgFound3and4Shown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_198;
		Relay_Restart_Out_198();
	}

	private void Relay_InitialSpawn_2()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e5ef55c2-3475-4f40-9ea0-3ee68b1aea53", "uScript_SpawnTechsFromData", Relay_InitialSpawn_2))
			{
				int num = 0;
				Array array = targetTechData;
				if (logic_uScript_SpawnTechsFromData_spawnData_2.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_2, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_2, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_2 = owner_Connection_0;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_2.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_2, logic_uScript_SpawnTechsFromData_ownerNode_2, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_2, logic_uScript_SpawnTechsFromData_allowResurrection_2);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_2.Out)
				{
					Relay_InitialSpawn_56();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_4()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ff1c992e-d9f2-4af4-a2f9-630630ba9d04", "Set_Bool", Relay_True_4))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_4.True(out logic_uScriptAct_SetBool_Target_4);
				local_TargetTechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_4;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
				{
					Relay_In_63();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_4()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ff1c992e-d9f2-4af4-a2f9-630630ba9d04", "Set_Bool", Relay_False_4))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_4.False(out logic_uScriptAct_SetBool_Target_4);
				local_TargetTechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_4;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
				{
					Relay_In_63();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_5()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("14a6909b-8e71-422d-95f4-41084668a9d2", "", Relay_Save_Out_5))
			{
				Relay_Save_20();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_5()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("14a6909b-8e71-422d-95f4-41084668a9d2", "", Relay_Load_Out_5))
			{
				Relay_Load_20();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_5()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("14a6909b-8e71-422d-95f4-41084668a9d2", "", Relay_Restart_Out_5))
			{
				Relay_Set_False_20();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_5()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("14a6909b-8e71-422d-95f4-41084668a9d2", "", Relay_Save_5))
			{
				logic_SubGraph_SaveLoadBool_boolean_5 = local_TargetTechSpawned_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_5 = local_TargetTechSpawned_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Save(ref logic_SubGraph_SaveLoadBool_boolean_5, logic_SubGraph_SaveLoadBool_boolAsVariable_5, logic_SubGraph_SaveLoadBool_uniqueID_5);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_5()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("14a6909b-8e71-422d-95f4-41084668a9d2", "", Relay_Load_5))
			{
				logic_SubGraph_SaveLoadBool_boolean_5 = local_TargetTechSpawned_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_5 = local_TargetTechSpawned_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Load(ref logic_SubGraph_SaveLoadBool_boolean_5, logic_SubGraph_SaveLoadBool_boolAsVariable_5, logic_SubGraph_SaveLoadBool_uniqueID_5);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_5()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("14a6909b-8e71-422d-95f4-41084668a9d2", "", Relay_Set_True_5))
			{
				logic_SubGraph_SaveLoadBool_boolean_5 = local_TargetTechSpawned_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_5 = local_TargetTechSpawned_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_5, logic_SubGraph_SaveLoadBool_boolAsVariable_5, logic_SubGraph_SaveLoadBool_uniqueID_5);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_5()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("14a6909b-8e71-422d-95f4-41084668a9d2", "", Relay_Set_False_5))
			{
				logic_SubGraph_SaveLoadBool_boolean_5 = local_TargetTechSpawned_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_5 = local_TargetTechSpawned_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_5, logic_SubGraph_SaveLoadBool_boolAsVariable_5, logic_SubGraph_SaveLoadBool_uniqueID_5);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_6()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d5e67ad8-5523-426e-8f56-9e29a26ce754", "", Relay_Save_Out_6))
			{
				Relay_Save_198();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_6()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d5e67ad8-5523-426e-8f56-9e29a26ce754", "", Relay_Load_Out_6))
			{
				Relay_Load_198();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_6()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d5e67ad8-5523-426e-8f56-9e29a26ce754", "", Relay_Restart_Out_6))
			{
				Relay_Set_False_198();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_6()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d5e67ad8-5523-426e-8f56-9e29a26ce754", "", Relay_Save_6))
			{
				logic_SubGraph_SaveLoadBool_boolean_6 = local_MsgFound1and2Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_MsgFound1and2Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Save(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_6()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d5e67ad8-5523-426e-8f56-9e29a26ce754", "", Relay_Load_6))
			{
				logic_SubGraph_SaveLoadBool_boolean_6 = local_MsgFound1and2Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_MsgFound1and2Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Load(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_6()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d5e67ad8-5523-426e-8f56-9e29a26ce754", "", Relay_Set_True_6))
			{
				logic_SubGraph_SaveLoadBool_boolean_6 = local_MsgFound1and2Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_MsgFound1and2Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_6()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d5e67ad8-5523-426e-8f56-9e29a26ce754", "", Relay_Set_False_6))
			{
				logic_SubGraph_SaveLoadBool_boolean_6 = local_MsgFound1and2Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_MsgFound1and2Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_SaveEvent_8()
	{
		if (!CheckDebugBreak("52a052c1-0b30-48ae-8736-8da7e10d59a3", "uScript_SaveLoad", Relay_SaveEvent_8))
		{
			Relay_Save_5();
		}
	}

	private void Relay_LoadEvent_8()
	{
		if (!CheckDebugBreak("52a052c1-0b30-48ae-8736-8da7e10d59a3", "uScript_SaveLoad", Relay_LoadEvent_8))
		{
			Relay_Load_5();
		}
	}

	private void Relay_RestartEvent_8()
	{
		if (!CheckDebugBreak("52a052c1-0b30-48ae-8736-8da7e10d59a3", "uScript_SaveLoad", Relay_RestartEvent_8))
		{
			Relay_Set_False_5();
		}
	}

	private void Relay_OnUpdate_9()
	{
		if (!CheckDebugBreak("15570992-3d9e-4ffd-9a3d-da98855b22a1", "Encounter_Update", Relay_OnUpdate_9))
		{
			Relay_In_106();
		}
	}

	private void Relay_OnSuspend_9()
	{
		CheckDebugBreak("15570992-3d9e-4ffd-9a3d-da98855b22a1", "Encounter_Update", Relay_OnSuspend_9);
	}

	private void Relay_OnResume_9()
	{
		CheckDebugBreak("15570992-3d9e-4ffd-9a3d-da98855b22a1", "Encounter_Update", Relay_OnResume_9);
	}

	private void Relay_In_12()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("40b64171-c57b-4a4a-8ef8-8c08383fc5ee", "Distance_Is_player_in_range_of_tech", Relay_In_12))
			{
				logic_uScript_IsPlayerInRangeOfTech_tech_12 = local_TargetTech_Tank;
				logic_uScript_IsPlayerInRangeOfTech_range_12 = distAtWhichTargetTechFound;
				logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_12.In(logic_uScript_IsPlayerInRangeOfTech_tech_12, logic_uScript_IsPlayerInRangeOfTech_range_12, logic_uScript_IsPlayerInRangeOfTech_techs_12);
				bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_12.InRange;
				bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_12.OutOfRange;
				if (inRange)
				{
					Relay_InitialSpawn_204();
				}
				if (outOfRange)
				{
					Relay_In_179();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Distance/Is player in range of tech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_15()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("76bd94fe-d77b-4ef3-ba6c-26a4e89f705d", "Compare_Bool", Relay_In_15))
			{
				logic_uScriptCon_CompareBool_Bool_15 = local_MsgFound1and2Shown_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.In(logic_uScriptCon_CompareBool_Bool_15);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.False;
				if (num)
				{
					Relay_In_182();
				}
				if (flag)
				{
					Relay_In_185();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_16()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("20a2e963-6a9e-4c46-8d78-1397634c5c8b", "Compare_Bool", Relay_In_16))
			{
				logic_uScriptCon_CompareBool_Bool_16 = local_TargetTechSpawned_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.In(logic_uScriptCon_CompareBool_Bool_16);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.False;
				if (num)
				{
					Relay_In_126();
				}
				if (flag)
				{
					Relay_In_144();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_17()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b3ce26b8-4fa9-4829-a5b3-d099dde8a65a", "Compare_Bool", Relay_In_17))
			{
				logic_uScriptCon_CompareBool_Bool_17 = local_TargetTechFound_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.In(logic_uScriptCon_CompareBool_Bool_17);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.False;
				if (num)
				{
					Relay_TickWave_108();
				}
				if (flag)
				{
					Relay_In_12();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_19()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bc5a9f7a-5b95-4c9d-b2c9-68bfda15978b", "Set_Bool", Relay_True_19))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_19.True(out logic_uScriptAct_SetBool_Target_19);
				local_TargetTechFound_System_Boolean = logic_uScriptAct_SetBool_Target_19;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_19()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bc5a9f7a-5b95-4c9d-b2c9-68bfda15978b", "Set_Bool", Relay_False_19))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_19.False(out logic_uScriptAct_SetBool_Target_19);
				local_TargetTechFound_System_Boolean = logic_uScriptAct_SetBool_Target_19;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_20()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8abbf5ba-13ce-4861-9267-a24d7ae65ce1", "", Relay_Save_Out_20))
			{
				Relay_Save_6();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_20()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8abbf5ba-13ce-4861-9267-a24d7ae65ce1", "", Relay_Load_Out_20))
			{
				Relay_Load_6();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_20()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8abbf5ba-13ce-4861-9267-a24d7ae65ce1", "", Relay_Restart_Out_20))
			{
				Relay_Set_False_6();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_20()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8abbf5ba-13ce-4861-9267-a24d7ae65ce1", "", Relay_Save_20))
			{
				logic_SubGraph_SaveLoadBool_boolean_20 = local_TargetTechFound_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_20 = local_TargetTechFound_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Save(ref logic_SubGraph_SaveLoadBool_boolean_20, logic_SubGraph_SaveLoadBool_boolAsVariable_20, logic_SubGraph_SaveLoadBool_uniqueID_20);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_20()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8abbf5ba-13ce-4861-9267-a24d7ae65ce1", "", Relay_Load_20))
			{
				logic_SubGraph_SaveLoadBool_boolean_20 = local_TargetTechFound_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_20 = local_TargetTechFound_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Load(ref logic_SubGraph_SaveLoadBool_boolean_20, logic_SubGraph_SaveLoadBool_boolAsVariable_20, logic_SubGraph_SaveLoadBool_uniqueID_20);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_20()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8abbf5ba-13ce-4861-9267-a24d7ae65ce1", "", Relay_Set_True_20))
			{
				logic_SubGraph_SaveLoadBool_boolean_20 = local_TargetTechFound_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_20 = local_TargetTechFound_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_20, logic_SubGraph_SaveLoadBool_boolAsVariable_20, logic_SubGraph_SaveLoadBool_uniqueID_20);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_20()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8abbf5ba-13ce-4861-9267-a24d7ae65ce1", "", Relay_Set_False_20))
			{
				logic_SubGraph_SaveLoadBool_boolean_20 = local_TargetTechFound_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_20 = local_TargetTechFound_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_20, logic_SubGraph_SaveLoadBool_boolAsVariable_20, logic_SubGraph_SaveLoadBool_uniqueID_20);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_AtIndex_29()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4b9076e5-4ac5-4c3a-807e-04d407ed255c", "uScript_AccessListTech", Relay_AtIndex_29))
			{
				int num = 0;
				Array array = local_targetTechs_TankArray;
				if (logic_uScript_AccessListTech_techList_29.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AccessListTech_techList_29, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AccessListTech_techList_29, num, array.Length);
				num += array.Length;
				logic_uScript_AccessListTech_uScript_AccessListTech_29.AtIndex(ref logic_uScript_AccessListTech_techList_29, logic_uScript_AccessListTech_index_29, out logic_uScript_AccessListTech_value_29);
				local_targetTechs_TankArray = logic_uScript_AccessListTech_techList_29;
				local_TargetTech_Tank = logic_uScript_AccessListTech_value_29;
				if (logic_uScript_AccessListTech_uScript_AccessListTech_29.Out)
				{
					Relay_In_231();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_AccessListTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_32()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b1ca08ac-5c24-4417-9118-0a0488b5db2d", "uScript_SetEncounterTarget", Relay_In_32))
			{
				logic_uScript_SetEncounterTarget_owner_32 = owner_Connection_25;
				logic_uScript_SetEncounterTarget_visibleObject_32 = local_TargetTech_Tank;
				logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_32.In(logic_uScript_SetEncounterTarget_owner_32, logic_uScript_SetEncounterTarget_visibleObject_32);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_SetEncounterTarget.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_33()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d7b116ff-f46e-4775-8268-0d2de637afbe", "uScript_GetAndCheckTechs", Relay_In_33))
			{
				int num = 0;
				Array array = targetTechData;
				if (logic_uScript_GetAndCheckTechs_techData_33.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_33, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_33, num, array.Length);
				num += array.Length;
				logic_uScript_GetAndCheckTechs_ownerNode_33 = owner_Connection_28;
				int num2 = 0;
				Array array2 = local_targetTechs_TankArray;
				if (logic_uScript_GetAndCheckTechs_techs_33.Length != num2 + array2.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_33, num2 + array2.Length);
				}
				Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_33, num2, array2.Length);
				num2 += array2.Length;
				logic_uScript_GetAndCheckTechs_Return_33 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33.In(logic_uScript_GetAndCheckTechs_techData_33, logic_uScript_GetAndCheckTechs_ownerNode_33, ref logic_uScript_GetAndCheckTechs_techs_33);
				local_targetTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_33;
				bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33.AllAlive;
				bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33.SomeAlive;
				bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_33.AllDead;
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
					Relay_True_59();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_GetAndCheckTechs.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Succeed_37()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("61125eb5-47f7-4cd7-9dd2-e5b3f43f5e90", "uScript_FinishEncounter", Relay_Succeed_37))
			{
				logic_uScript_FinishEncounter_owner_37 = owner_Connection_35;
				logic_uScript_FinishEncounter_uScript_FinishEncounter_37.Succeed(logic_uScript_FinishEncounter_owner_37);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_FinishEncounter.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Fail_37()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("61125eb5-47f7-4cd7-9dd2-e5b3f43f5e90", "uScript_FinishEncounter", Relay_Fail_37))
			{
				logic_uScript_FinishEncounter_owner_37 = owner_Connection_35;
				logic_uScript_FinishEncounter_uScript_FinishEncounter_37.Fail(logic_uScript_FinishEncounter_owner_37);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_FinishEncounter.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0c63828f-8f1f-4595-9064-d0e6e5ba3887", "", Relay_Save_Out_38))
			{
				Relay_Save_47();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0c63828f-8f1f-4595-9064-d0e6e5ba3887", "", Relay_Load_Out_38))
			{
				Relay_Load_47();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0c63828f-8f1f-4595-9064-d0e6e5ba3887", "", Relay_Restart_Out_38))
			{
				Relay_Set_False_47();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0c63828f-8f1f-4595-9064-d0e6e5ba3887", "", Relay_Save_38))
			{
				logic_SubGraph_SaveLoadInt_restartValue_38 = local_StartValue_System_Int32;
				logic_SubGraph_SaveLoadInt_integer_38 = local_TotalGroupEnemiesSpawned_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_38 = local_TotalGroupEnemiesSpawned_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Save(logic_SubGraph_SaveLoadInt_restartValue_38, ref logic_SubGraph_SaveLoadInt_integer_38, logic_SubGraph_SaveLoadInt_intAsVariable_38, logic_SubGraph_SaveLoadInt_uniqueID_38);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0c63828f-8f1f-4595-9064-d0e6e5ba3887", "", Relay_Load_38))
			{
				logic_SubGraph_SaveLoadInt_restartValue_38 = local_StartValue_System_Int32;
				logic_SubGraph_SaveLoadInt_integer_38 = local_TotalGroupEnemiesSpawned_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_38 = local_TotalGroupEnemiesSpawned_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Load(logic_SubGraph_SaveLoadInt_restartValue_38, ref logic_SubGraph_SaveLoadInt_integer_38, logic_SubGraph_SaveLoadInt_intAsVariable_38, logic_SubGraph_SaveLoadInt_uniqueID_38);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0c63828f-8f1f-4595-9064-d0e6e5ba3887", "", Relay_Restart_38))
			{
				logic_SubGraph_SaveLoadInt_restartValue_38 = local_StartValue_System_Int32;
				logic_SubGraph_SaveLoadInt_integer_38 = local_TotalGroupEnemiesSpawned_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_38 = local_TotalGroupEnemiesSpawned_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Restart(logic_SubGraph_SaveLoadInt_restartValue_38, ref logic_SubGraph_SaveLoadInt_integer_38, logic_SubGraph_SaveLoadInt_intAsVariable_38, logic_SubGraph_SaveLoadInt_uniqueID_38);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_40()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("66c25cf7-d8cf-4c46-9f64-76629097fb5a", "uScript_AddOnScreenMessage", Relay_In_40))
			{
				int num = 0;
				Array array = msgMissionComplete;
				if (logic_uScript_AddOnScreenMessage_locString_40.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_40, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_40, num, array.Length);
				num += array.Length;
				logic_uScript_AddOnScreenMessage_speaker_40 = messageSpeaker;
				logic_uScript_AddOnScreenMessage_Return_40 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_40.In(logic_uScript_AddOnScreenMessage_locString_40, logic_uScript_AddOnScreenMessage_msgPriority_40, logic_uScript_AddOnScreenMessage_holdMsg_40, logic_uScript_AddOnScreenMessage_tag_40, logic_uScript_AddOnScreenMessage_speaker_40, logic_uScript_AddOnScreenMessage_side_40);
				if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_40.Out)
				{
					Relay_In_228();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_AddOnScreenMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_42()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6153ec2d-346f-4176-80df-ef573d910d05", "uScript_ClearOnScreenMessagesWithTag", Relay_In_42))
			{
				logic_uScript_ClearOnScreenMessagesWithTag_tag_42 = local_Msg_System_String;
				logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_42.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_42, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_42);
				if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_42.Out)
				{
					Relay_In_40();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_ClearOnScreenMessagesWithTag.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_47()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("77f0b26f-3abd-4b3e-b29c-3b666ccfd01d", "", Relay_Save_Out_47))
			{
				Relay_Save_48();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_47()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("77f0b26f-3abd-4b3e-b29c-3b666ccfd01d", "", Relay_Load_Out_47))
			{
				Relay_Load_48();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_47()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("77f0b26f-3abd-4b3e-b29c-3b666ccfd01d", "", Relay_Restart_Out_47))
			{
				Relay_Set_False_48();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_47()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("77f0b26f-3abd-4b3e-b29c-3b666ccfd01d", "", Relay_Save_47))
			{
				logic_SubGraph_SaveLoadBool_boolean_47 = local_ShownMsgEnemySpotted_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_47 = local_ShownMsgEnemySpotted_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Save(ref logic_SubGraph_SaveLoadBool_boolean_47, logic_SubGraph_SaveLoadBool_boolAsVariable_47, logic_SubGraph_SaveLoadBool_uniqueID_47);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_47()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("77f0b26f-3abd-4b3e-b29c-3b666ccfd01d", "", Relay_Load_47))
			{
				logic_SubGraph_SaveLoadBool_boolean_47 = local_ShownMsgEnemySpotted_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_47 = local_ShownMsgEnemySpotted_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Load(ref logic_SubGraph_SaveLoadBool_boolean_47, logic_SubGraph_SaveLoadBool_boolAsVariable_47, logic_SubGraph_SaveLoadBool_uniqueID_47);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_47()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("77f0b26f-3abd-4b3e-b29c-3b666ccfd01d", "", Relay_Set_True_47))
			{
				logic_SubGraph_SaveLoadBool_boolean_47 = local_ShownMsgEnemySpotted_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_47 = local_ShownMsgEnemySpotted_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_47, logic_SubGraph_SaveLoadBool_boolAsVariable_47, logic_SubGraph_SaveLoadBool_uniqueID_47);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_47()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("77f0b26f-3abd-4b3e-b29c-3b666ccfd01d", "", Relay_Set_False_47))
			{
				logic_SubGraph_SaveLoadBool_boolean_47 = local_ShownMsgEnemySpotted_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_47 = local_ShownMsgEnemySpotted_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_47, logic_SubGraph_SaveLoadBool_boolAsVariable_47, logic_SubGraph_SaveLoadBool_uniqueID_47);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_48()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d01f472b-93fd-4dd9-b095-b1b71e3ae6aa", "", Relay_Save_Out_48))
			{
				Relay_Save_49();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_48()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d01f472b-93fd-4dd9-b095-b1b71e3ae6aa", "", Relay_Load_Out_48))
			{
				Relay_Load_49();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_48()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d01f472b-93fd-4dd9-b095-b1b71e3ae6aa", "", Relay_Restart_Out_48))
			{
				Relay_Set_True_49();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_48()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d01f472b-93fd-4dd9-b095-b1b71e3ae6aa", "", Relay_Save_48))
			{
				logic_SubGraph_SaveLoadBool_boolean_48 = local_ShownMsgEnemiesGivingUp_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_48 = local_ShownMsgEnemiesGivingUp_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Save(ref logic_SubGraph_SaveLoadBool_boolean_48, logic_SubGraph_SaveLoadBool_boolAsVariable_48, logic_SubGraph_SaveLoadBool_uniqueID_48);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_48()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d01f472b-93fd-4dd9-b095-b1b71e3ae6aa", "", Relay_Load_48))
			{
				logic_SubGraph_SaveLoadBool_boolean_48 = local_ShownMsgEnemiesGivingUp_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_48 = local_ShownMsgEnemiesGivingUp_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Load(ref logic_SubGraph_SaveLoadBool_boolean_48, logic_SubGraph_SaveLoadBool_boolAsVariable_48, logic_SubGraph_SaveLoadBool_uniqueID_48);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_48()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d01f472b-93fd-4dd9-b095-b1b71e3ae6aa", "", Relay_Set_True_48))
			{
				logic_SubGraph_SaveLoadBool_boolean_48 = local_ShownMsgEnemiesGivingUp_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_48 = local_ShownMsgEnemiesGivingUp_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_48, logic_SubGraph_SaveLoadBool_boolAsVariable_48, logic_SubGraph_SaveLoadBool_uniqueID_48);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_48()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d01f472b-93fd-4dd9-b095-b1b71e3ae6aa", "", Relay_Set_False_48))
			{
				logic_SubGraph_SaveLoadBool_boolean_48 = local_ShownMsgEnemiesGivingUp_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_48 = local_ShownMsgEnemiesGivingUp_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_48, logic_SubGraph_SaveLoadBool_boolAsVariable_48, logic_SubGraph_SaveLoadBool_uniqueID_48);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_49()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ca2626a1-9c53-4397-8ffc-aecc7720e66a", "", Relay_Save_Out_49))
			{
				Relay_Save_119();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_49()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ca2626a1-9c53-4397-8ffc-aecc7720e66a", "", Relay_Load_Out_49))
			{
				Relay_Load_119();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_49()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ca2626a1-9c53-4397-8ffc-aecc7720e66a", "", Relay_Restart_Out_49))
			{
				Relay_Set_False_119();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_49()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ca2626a1-9c53-4397-8ffc-aecc7720e66a", "", Relay_Save_49))
			{
				logic_SubGraph_SaveLoadBool_boolean_49 = AllowEnemyGroupToRespawn;
				logic_SubGraph_SaveLoadBool_boolAsVariable_49 = AllowEnemyGroupToRespawn;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_49()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ca2626a1-9c53-4397-8ffc-aecc7720e66a", "", Relay_Load_49))
			{
				logic_SubGraph_SaveLoadBool_boolean_49 = AllowEnemyGroupToRespawn;
				logic_SubGraph_SaveLoadBool_boolAsVariable_49 = AllowEnemyGroupToRespawn;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_49()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ca2626a1-9c53-4397-8ffc-aecc7720e66a", "", Relay_Set_True_49))
			{
				logic_SubGraph_SaveLoadBool_boolean_49 = AllowEnemyGroupToRespawn;
				logic_SubGraph_SaveLoadBool_boolAsVariable_49 = AllowEnemyGroupToRespawn;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_49()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ca2626a1-9c53-4397-8ffc-aecc7720e66a", "", Relay_Set_False_49))
			{
				logic_SubGraph_SaveLoadBool_boolean_49 = AllowEnemyGroupToRespawn;
				logic_SubGraph_SaveLoadBool_boolAsVariable_49 = AllowEnemyGroupToRespawn;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_53()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("56a74bd8-81c6-458c-b770-67fee22fee13", "Compare_Bool", Relay_In_53))
			{
				logic_uScriptCon_CompareBool_Bool_53 = local_ObjectiveComplete_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_53.In(logic_uScriptCon_CompareBool_Bool_53);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_53.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_53.False;
				if (num)
				{
					Relay_In_54();
				}
				if (flag)
				{
					Relay_In_16();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_54()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("368b72a3-9a87-4cf8-bd01-1a21abf8c1e6", "Compare_Bool", Relay_In_54))
			{
				logic_uScriptCon_CompareBool_Bool_54 = local_EnemyDeadEarly_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.In(logic_uScriptCon_CompareBool_Bool_54);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.False;
				if (num)
				{
					Relay_Succeed_37();
				}
				if (flag)
				{
					Relay_In_42();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_56()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("611f90f7-9d9c-4726-a193-49dc6080a2f7", "uScript_SpawnTechsFromData", Relay_InitialSpawn_56))
			{
				int num = 0;
				Array nPCTech = NPCTech;
				if (logic_uScript_SpawnTechsFromData_spawnData_56.Length != num + nPCTech.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_56, num + nPCTech.Length);
				}
				Array.Copy(nPCTech, 0, logic_uScript_SpawnTechsFromData_spawnData_56, num, nPCTech.Length);
				num += nPCTech.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_56 = owner_Connection_58;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_56.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_56, logic_uScript_SpawnTechsFromData_ownerNode_56, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_56, logic_uScript_SpawnTechsFromData_allowResurrection_56);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_56.Out)
				{
					Relay_True_4();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_59()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2ceaeb66-3f60-4a81-8839-684c454ce53a", "Set_Bool", Relay_True_59))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_59.True(out logic_uScriptAct_SetBool_Target_59);
				local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_59;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_59()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2ceaeb66-3f60-4a81-8839-684c454ce53a", "Set_Bool", Relay_False_59))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_59.False(out logic_uScriptAct_SetBool_Target_59);
				local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_59;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_AtIndex_62()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2483c017-b250-4cc6-a385-5de5dddd28dc", "uScript_AccessListTech", Relay_AtIndex_62))
			{
				int num = 0;
				Array array = local_NPCTanks_TankArray;
				if (logic_uScript_AccessListTech_techList_62.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AccessListTech_techList_62, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AccessListTech_techList_62, num, array.Length);
				num += array.Length;
				logic_uScript_AccessListTech_uScript_AccessListTech_62.AtIndex(ref logic_uScript_AccessListTech_techList_62, logic_uScript_AccessListTech_index_62, out logic_uScript_AccessListTech_value_62);
				local_NPCTanks_TankArray = logic_uScript_AccessListTech_techList_62;
				local_NPCTank_Tank = logic_uScript_AccessListTech_value_62;
				if (logic_uScript_AccessListTech_uScript_AccessListTech_62.Out)
				{
					Relay_In_89();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_AccessListTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_63()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a83afc0e-3f02-486e-a511-d55d28949d92", "Compare_Bool", Relay_In_63))
			{
				logic_uScriptCon_CompareBool_Bool_63 = local_NPCMet_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.In(logic_uScriptCon_CompareBool_Bool_63);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.False;
				if (num)
				{
					Relay_In_33();
				}
				if (flag)
				{
					Relay_In_97();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_65()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("342fc50e-2ad3-4b28-9609-c78a1221e55d", "uScript_AddOnScreenMessage", Relay_In_65))
			{
				int num = 0;
				Array array = msgNPCGreetingEnemyDead;
				if (logic_uScript_AddOnScreenMessage_locString_65.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_65, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_65, num, array.Length);
				num += array.Length;
				logic_uScript_AddOnScreenMessage_tag_65 = local_77_System_String;
				logic_uScript_AddOnScreenMessage_speaker_65 = messageSpeaker;
				logic_uScript_AddOnScreenMessage_Return_65 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_65.In(logic_uScript_AddOnScreenMessage_locString_65, logic_uScript_AddOnScreenMessage_msgPriority_65, logic_uScript_AddOnScreenMessage_holdMsg_65, logic_uScript_AddOnScreenMessage_tag_65, logic_uScript_AddOnScreenMessage_speaker_65, logic_uScript_AddOnScreenMessage_side_65);
				if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_65.Shown)
				{
					Relay_True_86();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_AddOnScreenMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_66()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c958dc4c-a3d7-43ac-b479-48a47a34ae1e", "uScript_ClearOnScreenMessagesWithTag", Relay_In_66))
			{
				logic_uScript_ClearOnScreenMessagesWithTag_tag_66 = local_61_System_String;
				logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_66.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_66, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_66);
				if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_66.Out)
				{
					Relay_In_93();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_ClearOnScreenMessagesWithTag.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_73()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("93a30fef-c582-4a08-a3f0-b4f2c8d2ca39", "uScript_AddOnScreenMessage", Relay_In_73))
			{
				int num = 0;
				Array array = msgNPCGreeting;
				if (logic_uScript_AddOnScreenMessage_locString_73.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_73, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_73, num, array.Length);
				num += array.Length;
				logic_uScript_AddOnScreenMessage_tag_73 = local_96_System_String;
				logic_uScript_AddOnScreenMessage_speaker_73 = messageSpeaker;
				logic_uScript_AddOnScreenMessage_Return_73 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_73.In(logic_uScript_AddOnScreenMessage_locString_73, logic_uScript_AddOnScreenMessage_msgPriority_73, logic_uScript_AddOnScreenMessage_holdMsg_73, logic_uScript_AddOnScreenMessage_tag_73, logic_uScript_AddOnScreenMessage_speaker_73, logic_uScript_AddOnScreenMessage_side_73);
				if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_73.Shown)
				{
					Relay_In_165();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_AddOnScreenMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_74()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a7164f7d-7af0-4233-9ce5-a5f4593cbd13", "uScript_FlyTechUpAndAway", Relay_In_74))
			{
				logic_uScript_FlyTechUpAndAway_tech_74 = local_NPCTank_Tank;
				logic_uScript_FlyTechUpAndAway_aiTree_74 = NPCFlyAwayAI;
				logic_uScript_FlyTechUpAndAway_removalParticles_74 = NPCDespawnParticleEffect;
				logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_74.In(logic_uScript_FlyTechUpAndAway_tech_74, logic_uScript_FlyTechUpAndAway_maxLifetime_74, logic_uScript_FlyTechUpAndAway_targetHeight_74, logic_uScript_FlyTechUpAndAway_aiTree_74, logic_uScript_FlyTechUpAndAway_removalParticles_74);
				if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_74.Out)
				{
					Relay_True_82();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_FlyTechUpAndAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_78()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("aa164711-6886-45b4-80c1-b5b9a5017667", "SubGraph_CompleteObjectiveStage", Relay_Out_78);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_78()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("aa164711-6886-45b4-80c1-b5b9a5017667", "SubGraph_CompleteObjectiveStage", Relay_In_78))
			{
				logic_SubGraph_CompleteObjectiveStage_objectiveStage_78 = local_Stage_System_Int32;
				logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_78.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_78, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_78);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_82()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cc6ee73b-8eff-4603-b835-04413be05352", "Set_Bool", Relay_True_82))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_82.True(out logic_uScriptAct_SetBool_Target_82);
				local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_82;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_82.Out)
				{
					Relay_In_78();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_82()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cc6ee73b-8eff-4603-b835-04413be05352", "Set_Bool", Relay_False_82))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_82.False(out logic_uScriptAct_SetBool_Target_82);
				local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_82;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_82.Out)
				{
					Relay_In_78();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_84()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("fa5df2de-1f4f-443f-a879-a52ee27b75e1", "uScript_InRangeOfTech", Relay_In_84))
			{
				logic_uScript_InRangeOfTech_tank_84 = local_NPCTank_Tank;
				logic_uScript_InRangeOfTech_range_84 = distAtWhichNPCTechFound;
				logic_uScript_InRangeOfTech_uScript_InRangeOfTech_84.In(logic_uScript_InRangeOfTech_tank_84, logic_uScript_InRangeOfTech_range_84);
				bool inRange = logic_uScript_InRangeOfTech_uScript_InRangeOfTech_84.InRange;
				bool outOfRange = logic_uScript_InRangeOfTech_uScript_InRangeOfTech_84.OutOfRange;
				if (inRange)
				{
					Relay_True_92();
				}
				if (outOfRange)
				{
					Relay_In_101();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_InRangeOfTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_86()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("89818463-1dce-400a-9764-02a4c1a35ffb", "Set_Bool", Relay_True_86))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_86.True(out logic_uScriptAct_SetBool_Target_86);
				local_EnemyDeadEarly_System_Boolean = logic_uScriptAct_SetBool_Target_86;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_86.Out)
				{
					Relay_In_150();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_86()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("89818463-1dce-400a-9764-02a4c1a35ffb", "Set_Bool", Relay_False_86))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_86.False(out logic_uScriptAct_SetBool_Target_86);
				local_EnemyDeadEarly_System_Boolean = logic_uScriptAct_SetBool_Target_86;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_86.Out)
				{
					Relay_In_150();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_89()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0229e836-69ba-4ac8-9b23-485b91f4bc77", "Set_tank_invulnerable", Relay_In_89))
			{
				logic_uScript_SetTankInvulnerable_tank_89 = local_NPCTank_Tank;
				logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_89.In(logic_uScript_SetTankInvulnerable_invulnerable_89, logic_uScript_SetTankInvulnerable_tank_89);
				if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_89.Out)
				{
					Relay_In_100();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set tank invulnerable.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_92()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e76e09c8-c22b-4b15-8b9d-375b004fc89e", "Set_Bool", Relay_True_92))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_92.True(out logic_uScriptAct_SetBool_Target_92);
				local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_92;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_92.Out)
				{
					Relay_In_99();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_92()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e76e09c8-c22b-4b15-8b9d-375b004fc89e", "Set_Bool", Relay_False_92))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_92.False(out logic_uScriptAct_SetBool_Target_92);
				local_NPCSeen_System_Boolean = logic_uScriptAct_SetBool_Target_92;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_92.Out)
				{
					Relay_In_99();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_93()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ddadc7eb-4a6f-4e8d-a295-7eaa74a15c18", "uScript_AddOnScreenMessage", Relay_In_93))
			{
				int num = 0;
				Array array = msgNPCGreetingInturrupt;
				if (logic_uScript_AddOnScreenMessage_locString_93.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_93, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_93, num, array.Length);
				num += array.Length;
				logic_uScript_AddOnScreenMessage_speaker_93 = messageSpeaker;
				logic_uScript_AddOnScreenMessage_Return_93 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_93.In(logic_uScript_AddOnScreenMessage_locString_93, logic_uScript_AddOnScreenMessage_msgPriority_93, logic_uScript_AddOnScreenMessage_holdMsg_93, logic_uScript_AddOnScreenMessage_tag_93, logic_uScript_AddOnScreenMessage_speaker_93, logic_uScript_AddOnScreenMessage_side_93);
				if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_93.Shown)
				{
					Relay_In_165();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_AddOnScreenMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_97()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4b75ae97-23b1-4836-8a6f-66d170ab5e74", "uScript_GetAndCheckTechs", Relay_In_97))
			{
				int num = 0;
				Array nPCTech = NPCTech;
				if (logic_uScript_GetAndCheckTechs_techData_97.Length != num + nPCTech.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_97, num + nPCTech.Length);
				}
				Array.Copy(nPCTech, 0, logic_uScript_GetAndCheckTechs_techData_97, num, nPCTech.Length);
				num += nPCTech.Length;
				logic_uScript_GetAndCheckTechs_ownerNode_97 = owner_Connection_98;
				int num2 = 0;
				Array array = local_NPCTanks_TankArray;
				if (logic_uScript_GetAndCheckTechs_techs_97.Length != num2 + array.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_97, num2 + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_97, num2, array.Length);
				num2 += array.Length;
				logic_uScript_GetAndCheckTechs_Return_97 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_97.In(logic_uScript_GetAndCheckTechs_techData_97, logic_uScript_GetAndCheckTechs_ownerNode_97, ref logic_uScript_GetAndCheckTechs_techs_97);
				local_NPCTanks_TankArray = logic_uScript_GetAndCheckTechs_techs_97;
				bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_97.AllAlive;
				bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_97.SomeAlive;
				if (allAlive)
				{
					Relay_AtIndex_62();
				}
				if (someAlive)
				{
					Relay_AtIndex_62();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_GetAndCheckTechs.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_99()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("52546f3c-6041-488f-be3d-b928d40b0b7e", "uScript_GetAndCheckTechs", Relay_In_99))
			{
				int num = 0;
				Array array = targetTechData;
				if (logic_uScript_GetAndCheckTechs_techData_99.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_99, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_99, num, array.Length);
				num += array.Length;
				logic_uScript_GetAndCheckTechs_ownerNode_99 = owner_Connection_71;
				int num2 = 0;
				Array array2 = local_targetTechs_TankArray;
				if (logic_uScript_GetAndCheckTechs_techs_99.Length != num2 + array2.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_99, num2 + array2.Length);
				}
				Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_99, num2, array2.Length);
				num2 += array2.Length;
				logic_uScript_GetAndCheckTechs_Return_99 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_99.In(logic_uScript_GetAndCheckTechs_techData_99, logic_uScript_GetAndCheckTechs_ownerNode_99, ref logic_uScript_GetAndCheckTechs_techs_99);
				local_targetTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_99;
				bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_99.AllAlive;
				bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_99.SomeAlive;
				bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_99.AllDead;
				if (allAlive)
				{
					Relay_In_73();
				}
				if (someAlive)
				{
					Relay_In_73();
				}
				if (allDead)
				{
					Relay_In_65();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_GetAndCheckTechs.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_100()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("76702451-728f-481c-822a-ea6926872751", "uScript_SetOneTechAsEncounterTarget", Relay_In_100))
			{
				logic_uScript_SetOneTechAsEncounterTarget_owner_100 = owner_Connection_103;
				int num = 0;
				Array array = local_NPCTanks_TankArray;
				if (logic_uScript_SetOneTechAsEncounterTarget_techs_100.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_100, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_100, num, array.Length);
				num += array.Length;
				logic_uScript_SetOneTechAsEncounterTarget_Return_100 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_100.In(logic_uScript_SetOneTechAsEncounterTarget_owner_100, logic_uScript_SetOneTechAsEncounterTarget_techs_100);
				local_NPCTank_Tank = logic_uScript_SetOneTechAsEncounterTarget_Return_100;
				if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_100.Out)
				{
					Relay_In_134();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_SetOneTechAsEncounterTarget.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_101()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("94cc0499-e287-49e4-81f2-395b69023b44", "Compare_Bool", Relay_In_101))
			{
				logic_uScriptCon_CompareBool_Bool_101 = local_NPCSeen_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.In(logic_uScriptCon_CompareBool_Bool_101);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.True)
				{
					Relay_True_132();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output1_106()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c9722d9d-a712-4df0-9199-cf4b91639d80", "Manual_Switch", Relay_Output1_106))
			{
				Relay_In_53();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output2_106()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c9722d9d-a712-4df0-9199-cf4b91639d80", "Manual_Switch", Relay_Output2_106))
			{
				Relay_In_53();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output3_106()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("c9722d9d-a712-4df0-9199-cf4b91639d80", "Manual_Switch", Relay_Output3_106);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output4_106()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("c9722d9d-a712-4df0-9199-cf4b91639d80", "Manual_Switch", Relay_Output4_106);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output5_106()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("c9722d9d-a712-4df0-9199-cf4b91639d80", "Manual_Switch", Relay_Output5_106);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output6_106()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("c9722d9d-a712-4df0-9199-cf4b91639d80", "Manual_Switch", Relay_Output6_106);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output7_106()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("c9722d9d-a712-4df0-9199-cf4b91639d80", "Manual_Switch", Relay_Output7_106);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output8_106()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("c9722d9d-a712-4df0-9199-cf4b91639d80", "Manual_Switch", Relay_Output8_106);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_106()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c9722d9d-a712-4df0-9199-cf4b91639d80", "Manual_Switch", Relay_In_106))
			{
				logic_uScriptCon_ManualSwitch_CurrentOutput_106 = local_Stage_System_Int32;
				logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_106.In(logic_uScriptCon_ManualSwitch_CurrentOutput_106);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_TickWave_108()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("937302ce-41f5-41b7-bb21-0059a6898b6f", "uScript_SpawnTechWaveFromData", Relay_TickWave_108))
			{
				logic_uScript_SpawnTechWaveFromData_ownerNode_108 = owner_Connection_113;
				logic_uScript_SpawnTechWaveFromData_spawnData_108 = enemyGroupData;
				logic_uScript_SpawnTechWaveFromData_waveSize_108 = EnemyGroupSize;
				logic_uScript_SpawnTechWaveFromData_delayBetweenSpawns_108 = DelayBetweenEnemyGroupSpaws;
				logic_uScript_SpawnTechWaveFromData_numSpawnedSoFar_108 = local_TotalGroupEnemiesSpawned_System_Int32;
				logic_uScript_SpawnTechWaveFromData_allowRespawn_108 = AllowEnemyGroupToRespawn;
				logic_uScript_SpawnTechWaveFromData_respawnGroupSize_108 = ReinforcementSubGroupSize;
				logic_uScript_SpawnTechWaveFromData_delayBetweenRespawnGroups_108 = DelayBetweenRespawnArivals;
				logic_uScript_SpawnTechWaveFromData_Return_108 = logic_uScript_SpawnTechWaveFromData_uScript_SpawnTechWaveFromData_108.TickWave(logic_uScript_SpawnTechWaveFromData_ownerNode_108, logic_uScript_SpawnTechWaveFromData_spawnData_108, logic_uScript_SpawnTechWaveFromData_waveSize_108, logic_uScript_SpawnTechWaveFromData_delayBetweenSpawns_108, logic_uScript_SpawnTechWaveFromData_numSpawnedSoFar_108, logic_uScript_SpawnTechWaveFromData_allowRespawn_108, logic_uScript_SpawnTechWaveFromData_respawnGroupSize_108, logic_uScript_SpawnTechWaveFromData_delayBetweenRespawnGroups_108);
				local_TotalGroupEnemiesSpawned_System_Int32 = logic_uScript_SpawnTechWaveFromData_Return_108;
				if (logic_uScript_SpawnTechWaveFromData_uScript_SpawnTechWaveFromData_108.Out)
				{
					Relay_In_225();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_SpawnTechWaveFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_119()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6bbe37aa-e922-4daa-bc45-e06ccfa85d4a", "", Relay_Save_Out_119))
			{
				Relay_Save_120();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_119()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6bbe37aa-e922-4daa-bc45-e06ccfa85d4a", "", Relay_Load_Out_119))
			{
				Relay_Load_120();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_119()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6bbe37aa-e922-4daa-bc45-e06ccfa85d4a", "", Relay_Restart_Out_119))
			{
				Relay_Set_False_120();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_119()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6bbe37aa-e922-4daa-bc45-e06ccfa85d4a", "", Relay_Save_119))
			{
				logic_SubGraph_SaveLoadBool_boolean_119 = local_EnemyDeadEarly_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_119 = local_EnemyDeadEarly_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Save(ref logic_SubGraph_SaveLoadBool_boolean_119, logic_SubGraph_SaveLoadBool_boolAsVariable_119, logic_SubGraph_SaveLoadBool_uniqueID_119);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_119()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6bbe37aa-e922-4daa-bc45-e06ccfa85d4a", "", Relay_Load_119))
			{
				logic_SubGraph_SaveLoadBool_boolean_119 = local_EnemyDeadEarly_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_119 = local_EnemyDeadEarly_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Load(ref logic_SubGraph_SaveLoadBool_boolean_119, logic_SubGraph_SaveLoadBool_boolAsVariable_119, logic_SubGraph_SaveLoadBool_uniqueID_119);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_119()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6bbe37aa-e922-4daa-bc45-e06ccfa85d4a", "", Relay_Set_True_119))
			{
				logic_SubGraph_SaveLoadBool_boolean_119 = local_EnemyDeadEarly_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_119 = local_EnemyDeadEarly_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_119, logic_SubGraph_SaveLoadBool_boolAsVariable_119, logic_SubGraph_SaveLoadBool_uniqueID_119);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_119()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6bbe37aa-e922-4daa-bc45-e06ccfa85d4a", "", Relay_Set_False_119))
			{
				logic_SubGraph_SaveLoadBool_boolean_119 = local_EnemyDeadEarly_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_119 = local_EnemyDeadEarly_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_119, logic_SubGraph_SaveLoadBool_boolAsVariable_119, logic_SubGraph_SaveLoadBool_uniqueID_119);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_120()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6fc15d90-779d-418d-8aea-ef0520f9d709", "", Relay_Save_Out_120))
			{
				Relay_Save_121();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_120()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6fc15d90-779d-418d-8aea-ef0520f9d709", "", Relay_Load_Out_120))
			{
				Relay_Load_121();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_120()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6fc15d90-779d-418d-8aea-ef0520f9d709", "", Relay_Restart_Out_120))
			{
				Relay_Set_False_121();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_120()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6fc15d90-779d-418d-8aea-ef0520f9d709", "", Relay_Save_120))
			{
				logic_SubGraph_SaveLoadBool_boolean_120 = local_NPCSeen_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_120 = local_NPCSeen_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.Save(ref logic_SubGraph_SaveLoadBool_boolean_120, logic_SubGraph_SaveLoadBool_boolAsVariable_120, logic_SubGraph_SaveLoadBool_uniqueID_120);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_120()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6fc15d90-779d-418d-8aea-ef0520f9d709", "", Relay_Load_120))
			{
				logic_SubGraph_SaveLoadBool_boolean_120 = local_NPCSeen_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_120 = local_NPCSeen_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.Load(ref logic_SubGraph_SaveLoadBool_boolean_120, logic_SubGraph_SaveLoadBool_boolAsVariable_120, logic_SubGraph_SaveLoadBool_uniqueID_120);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_120()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6fc15d90-779d-418d-8aea-ef0520f9d709", "", Relay_Set_True_120))
			{
				logic_SubGraph_SaveLoadBool_boolean_120 = local_NPCSeen_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_120 = local_NPCSeen_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_120, logic_SubGraph_SaveLoadBool_boolAsVariable_120, logic_SubGraph_SaveLoadBool_uniqueID_120);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_120()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6fc15d90-779d-418d-8aea-ef0520f9d709", "", Relay_Set_False_120))
			{
				logic_SubGraph_SaveLoadBool_boolean_120 = local_NPCSeen_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_120 = local_NPCSeen_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_120.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_120, logic_SubGraph_SaveLoadBool_boolAsVariable_120, logic_SubGraph_SaveLoadBool_uniqueID_120);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_121()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bd9cafe5-33b3-4023-a7d8-4d06d5bb9f7c", "", Relay_Save_Out_121))
			{
				Relay_Save_123();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_121()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bd9cafe5-33b3-4023-a7d8-4d06d5bb9f7c", "", Relay_Load_Out_121))
			{
				Relay_Load_123();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_121()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bd9cafe5-33b3-4023-a7d8-4d06d5bb9f7c", "", Relay_Restart_Out_121))
			{
				Relay_Set_False_123();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_121()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bd9cafe5-33b3-4023-a7d8-4d06d5bb9f7c", "", Relay_Save_121))
			{
				logic_SubGraph_SaveLoadBool_boolean_121 = local_NPCMet_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_121 = local_NPCMet_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.Save(ref logic_SubGraph_SaveLoadBool_boolean_121, logic_SubGraph_SaveLoadBool_boolAsVariable_121, logic_SubGraph_SaveLoadBool_uniqueID_121);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_121()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bd9cafe5-33b3-4023-a7d8-4d06d5bb9f7c", "", Relay_Load_121))
			{
				logic_SubGraph_SaveLoadBool_boolean_121 = local_NPCMet_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_121 = local_NPCMet_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.Load(ref logic_SubGraph_SaveLoadBool_boolean_121, logic_SubGraph_SaveLoadBool_boolAsVariable_121, logic_SubGraph_SaveLoadBool_uniqueID_121);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_121()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bd9cafe5-33b3-4023-a7d8-4d06d5bb9f7c", "", Relay_Set_True_121))
			{
				logic_SubGraph_SaveLoadBool_boolean_121 = local_NPCMet_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_121 = local_NPCMet_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_121, logic_SubGraph_SaveLoadBool_boolAsVariable_121, logic_SubGraph_SaveLoadBool_uniqueID_121);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_121()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bd9cafe5-33b3-4023-a7d8-4d06d5bb9f7c", "", Relay_Set_False_121))
			{
				logic_SubGraph_SaveLoadBool_boolean_121 = local_NPCMet_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_121 = local_NPCMet_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_121.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_121, logic_SubGraph_SaveLoadBool_boolAsVariable_121, logic_SubGraph_SaveLoadBool_uniqueID_121);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_123()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("36faf30b-bed2-43d2-a1f1-d427c70b08cf", "", Relay_Save_Out_123))
			{
				Relay_Save_137();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_123()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("36faf30b-bed2-43d2-a1f1-d427c70b08cf", "", Relay_Load_Out_123))
			{
				Relay_Load_137();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_123()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("36faf30b-bed2-43d2-a1f1-d427c70b08cf", "", Relay_Restart_Out_123))
			{
				Relay_Set_False_137();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_123()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("36faf30b-bed2-43d2-a1f1-d427c70b08cf", "", Relay_Save_123))
			{
				logic_SubGraph_SaveLoadBool_boolean_123 = local_ObjectiveComplete_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_123 = local_ObjectiveComplete_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Save(ref logic_SubGraph_SaveLoadBool_boolean_123, logic_SubGraph_SaveLoadBool_boolAsVariable_123, logic_SubGraph_SaveLoadBool_uniqueID_123);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_123()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("36faf30b-bed2-43d2-a1f1-d427c70b08cf", "", Relay_Load_123))
			{
				logic_SubGraph_SaveLoadBool_boolean_123 = local_ObjectiveComplete_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_123 = local_ObjectiveComplete_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Load(ref logic_SubGraph_SaveLoadBool_boolean_123, logic_SubGraph_SaveLoadBool_boolAsVariable_123, logic_SubGraph_SaveLoadBool_uniqueID_123);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_123()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("36faf30b-bed2-43d2-a1f1-d427c70b08cf", "", Relay_Set_True_123))
			{
				logic_SubGraph_SaveLoadBool_boolean_123 = local_ObjectiveComplete_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_123 = local_ObjectiveComplete_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_123, logic_SubGraph_SaveLoadBool_boolAsVariable_123, logic_SubGraph_SaveLoadBool_uniqueID_123);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_123()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("36faf30b-bed2-43d2-a1f1-d427c70b08cf", "", Relay_Set_False_123))
			{
				logic_SubGraph_SaveLoadBool_boolean_123 = local_ObjectiveComplete_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_123 = local_ObjectiveComplete_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_123.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_123, logic_SubGraph_SaveLoadBool_boolAsVariable_123, logic_SubGraph_SaveLoadBool_uniqueID_123);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_126()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("83b083c4-bd82-4304-8ef9-6d960fee7b0f", "uScript_GetAndCheckTechs", Relay_In_126))
			{
				int num = 0;
				Array array = targetTechData;
				if (logic_uScript_GetAndCheckTechs_techData_126.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_126, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_126, num, array.Length);
				num += array.Length;
				logic_uScript_GetAndCheckTechs_ownerNode_126 = owner_Connection_128;
				int num2 = 0;
				Array array2 = local_targetTechs_TankArray;
				if (logic_uScript_GetAndCheckTechs_techs_126.Length != num2 + array2.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_126, num2 + array2.Length);
				}
				Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_126, num2, array2.Length);
				num2 += array2.Length;
				logic_uScript_GetAndCheckTechs_Return_126 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_126.In(logic_uScript_GetAndCheckTechs_techData_126, logic_uScript_GetAndCheckTechs_ownerNode_126, ref logic_uScript_GetAndCheckTechs_techs_126);
				local_targetTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_126;
				bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_126.AllAlive;
				bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_126.SomeAlive;
				bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_126.AllDead;
				if (allAlive)
				{
					Relay_In_210();
				}
				if (someAlive)
				{
					Relay_In_210();
				}
				if (allDead)
				{
					Relay_In_215();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_GetAndCheckTechs.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_131()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("786b7a42-e1a9-4528-8728-f6e19bdd3ba6", "Set_Bool", Relay_True_131))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_131.True(out logic_uScriptAct_SetBool_Target_131);
				AllowEnemyGroupToRespawn = logic_uScriptAct_SetBool_Target_131;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_131.Out)
				{
					Relay_In_140();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_131()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("786b7a42-e1a9-4528-8728-f6e19bdd3ba6", "Set_Bool", Relay_False_131))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_131.False(out logic_uScriptAct_SetBool_Target_131);
				AllowEnemyGroupToRespawn = logic_uScriptAct_SetBool_Target_131;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_131.Out)
				{
					Relay_In_140();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_132()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("58f9efcd-f066-4250-a2be-8ffe0514fca1", "Set_Bool", Relay_True_132))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_132.True(out logic_uScriptAct_SetBool_Target_132);
				local_NPCIgnored_System_Boolean = logic_uScriptAct_SetBool_Target_132;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_132()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("58f9efcd-f066-4250-a2be-8ffe0514fca1", "Set_Bool", Relay_False_132))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_132.False(out logic_uScriptAct_SetBool_Target_132);
				local_NPCIgnored_System_Boolean = logic_uScriptAct_SetBool_Target_132;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_134()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2240c6f2-dcad-4955-87ff-5745abeae111", "Compare_Bool", Relay_In_134))
			{
				logic_uScriptCon_CompareBool_Bool_134 = local_NPCIgnored_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_134.In(logic_uScriptCon_CompareBool_Bool_134);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_134.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_134.False;
				if (num)
				{
					Relay_In_66();
				}
				if (flag)
				{
					Relay_In_84();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_137()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2df0acde-5e3a-4cc7-9f4a-cb96960e1ab2", "", Relay_Save_Out_137))
			{
				Relay_Save_168();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_137()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2df0acde-5e3a-4cc7-9f4a-cb96960e1ab2", "", Relay_Load_Out_137))
			{
				Relay_Load_168();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_137()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2df0acde-5e3a-4cc7-9f4a-cb96960e1ab2", "", Relay_Restart_Out_137))
			{
				Relay_Set_False_168();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_137()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2df0acde-5e3a-4cc7-9f4a-cb96960e1ab2", "", Relay_Save_137))
			{
				logic_SubGraph_SaveLoadBool_boolean_137 = local_NPCIgnored_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_137 = local_NPCIgnored_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Save(ref logic_SubGraph_SaveLoadBool_boolean_137, logic_SubGraph_SaveLoadBool_boolAsVariable_137, logic_SubGraph_SaveLoadBool_uniqueID_137);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_137()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2df0acde-5e3a-4cc7-9f4a-cb96960e1ab2", "", Relay_Load_137))
			{
				logic_SubGraph_SaveLoadBool_boolean_137 = local_NPCIgnored_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_137 = local_NPCIgnored_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Load(ref logic_SubGraph_SaveLoadBool_boolean_137, logic_SubGraph_SaveLoadBool_boolAsVariable_137, logic_SubGraph_SaveLoadBool_uniqueID_137);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_137()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2df0acde-5e3a-4cc7-9f4a-cb96960e1ab2", "", Relay_Set_True_137))
			{
				logic_SubGraph_SaveLoadBool_boolean_137 = local_NPCIgnored_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_137 = local_NPCIgnored_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_137, logic_SubGraph_SaveLoadBool_boolAsVariable_137, logic_SubGraph_SaveLoadBool_uniqueID_137);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_137()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2df0acde-5e3a-4cc7-9f4a-cb96960e1ab2", "", Relay_Set_False_137))
			{
				logic_SubGraph_SaveLoadBool_boolean_137 = local_NPCIgnored_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_137 = local_NPCIgnored_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_137, logic_SubGraph_SaveLoadBool_boolAsVariable_137, logic_SubGraph_SaveLoadBool_uniqueID_137);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_139()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("ae305844-ba55-4019-9bc6-32bf9dd61194", "", Relay_Save_Out_139);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_139()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("ae305844-ba55-4019-9bc6-32bf9dd61194", "", Relay_Load_Out_139);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_139()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("ae305844-ba55-4019-9bc6-32bf9dd61194", "", Relay_Restart_Out_139);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_139()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ae305844-ba55-4019-9bc6-32bf9dd61194", "", Relay_Save_139))
			{
				logic_SubGraph_SaveLoadInt_integer_139 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_139 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Save(logic_SubGraph_SaveLoadInt_restartValue_139, ref logic_SubGraph_SaveLoadInt_integer_139, logic_SubGraph_SaveLoadInt_intAsVariable_139, logic_SubGraph_SaveLoadInt_uniqueID_139);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_139()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ae305844-ba55-4019-9bc6-32bf9dd61194", "", Relay_Load_139))
			{
				logic_SubGraph_SaveLoadInt_integer_139 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_139 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Load(logic_SubGraph_SaveLoadInt_restartValue_139, ref logic_SubGraph_SaveLoadInt_integer_139, logic_SubGraph_SaveLoadInt_intAsVariable_139, logic_SubGraph_SaveLoadInt_uniqueID_139);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_139()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ae305844-ba55-4019-9bc6-32bf9dd61194", "", Relay_Restart_139))
			{
				logic_SubGraph_SaveLoadInt_integer_139 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_139 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Restart(logic_SubGraph_SaveLoadInt_restartValue_139, ref logic_SubGraph_SaveLoadInt_integer_139, logic_SubGraph_SaveLoadInt_intAsVariable_139, logic_SubGraph_SaveLoadInt_uniqueID_139);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_140()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("88da08a6-73e1-4872-995c-1ee82279ffbd", "Pass", Relay_In_140))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_140.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_140.Out)
				{
					Relay_In_63();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_143()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9a739ba1-4954-4f0b-a9c5-0e24b063e666", "uScript_AddOnScreenMessage", Relay_In_143))
			{
				int num = 0;
				Array array = msgTargetTechFound5;
				if (logic_uScript_AddOnScreenMessage_locString_143.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_143, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_143, num, array.Length);
				num += array.Length;
				logic_uScript_AddOnScreenMessage_tag_143 = local_169_System_String;
				logic_uScript_AddOnScreenMessage_speaker_143 = messageSpeakerSpider;
				logic_uScript_AddOnScreenMessage_Return_143 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_143.In(logic_uScript_AddOnScreenMessage_locString_143, logic_uScript_AddOnScreenMessage_msgPriority_143, logic_uScript_AddOnScreenMessage_holdMsg_143, logic_uScript_AddOnScreenMessage_tag_143, logic_uScript_AddOnScreenMessage_speaker_143, logic_uScript_AddOnScreenMessage_side_143);
				if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_143.Out)
				{
					Relay_In_145();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_AddOnScreenMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_144()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("13d9b1cd-0151-4d7d-bfc6-d4b5c9b8ebde", "Pass", Relay_In_144))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_144.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_144.Out)
				{
					Relay_InitialSpawn_207();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_145()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f3f5cf11-7fbb-4eed-8035-9850f61dda2f", "AI_Set_team", Relay_In_145))
			{
				logic_uScript_SetTankTeam_tank_145 = local_TargetTech_Tank;
				logic_uScript_SetTankTeam_uScript_SetTankTeam_145.In(logic_uScript_SetTankTeam_tank_145, logic_uScript_SetTankTeam_team_145);
				if (logic_uScript_SetTankTeam_uScript_SetTankTeam_145.Out)
				{
					Relay_In_201();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at AI/Set team.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_148()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("67b34e25-f4a3-4544-ac4f-2950da9d3160", "Set_Bool", Relay_True_148))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_148.True(out logic_uScriptAct_SetBool_Target_148);
				local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_148;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_148.Out)
				{
					Relay_In_153();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_148()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("67b34e25-f4a3-4544-ac4f-2950da9d3160", "Set_Bool", Relay_False_148))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_148.False(out logic_uScriptAct_SetBool_Target_148);
				local_NPCMet_System_Boolean = logic_uScriptAct_SetBool_Target_148;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_148.Out)
				{
					Relay_In_153();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_150()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("498b4015-7978-4b2c-8cd5-7a9b61893cf5", "uScript_FlyTechUpAndAway", Relay_In_150))
			{
				logic_uScript_FlyTechUpAndAway_tech_150 = local_NPCTank_Tank;
				logic_uScript_FlyTechUpAndAway_aiTree_150 = NPCFlyAwayAI;
				logic_uScript_FlyTechUpAndAway_removalParticles_150 = NPCDespawnParticleEffect;
				logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_150.In(logic_uScript_FlyTechUpAndAway_tech_150, logic_uScript_FlyTechUpAndAway_maxLifetime_150, logic_uScript_FlyTechUpAndAway_targetHeight_150, logic_uScript_FlyTechUpAndAway_aiTree_150, logic_uScript_FlyTechUpAndAway_removalParticles_150);
				if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_150.Out)
				{
					Relay_True_148();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_FlyTechUpAndAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_153()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("762bff45-f71c-4b3c-8af2-1e38688d1f67", "SubGraph_CompleteObjectiveStage", Relay_Out_153);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_153()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("762bff45-f71c-4b3c-8af2-1e38688d1f67", "SubGraph_CompleteObjectiveStage", Relay_In_153))
			{
				logic_SubGraph_CompleteObjectiveStage_objectiveStage_153 = local_Stage_System_Int32;
				logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_153.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_153, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_153);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_155()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("decc136b-0c09-4beb-b372-8c221ce8246e", "uScript_SpawnTechsFromData", Relay_InitialSpawn_155))
			{
				int num = 0;
				Array firstFly01TechData = FirstFly01TechData;
				if (logic_uScript_SpawnTechsFromData_spawnData_155.Length != num + firstFly01TechData.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_155, num + firstFly01TechData.Length);
				}
				Array.Copy(firstFly01TechData, 0, logic_uScript_SpawnTechsFromData_spawnData_155, num, firstFly01TechData.Length);
				num += firstFly01TechData.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_155 = owner_Connection_154;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_155.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_155, logic_uScript_SpawnTechsFromData_ownerNode_155, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_155, logic_uScript_SpawnTechsFromData_allowResurrection_155);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_155.Out)
				{
					Relay_InitialSpawn_157();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_157()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ec839836-e094-43a2-b533-93c4c0a9af0d", "uScript_SpawnTechsFromData", Relay_InitialSpawn_157))
			{
				int num = 0;
				Array firstFly02TechData = FirstFly02TechData;
				if (logic_uScript_SpawnTechsFromData_spawnData_157.Length != num + firstFly02TechData.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_157, num + firstFly02TechData.Length);
				}
				Array.Copy(firstFly02TechData, 0, logic_uScript_SpawnTechsFromData_spawnData_157, num, firstFly02TechData.Length);
				num += firstFly02TechData.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_157 = owner_Connection_159;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_157.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_157, logic_uScript_SpawnTechsFromData_ownerNode_157, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_157, logic_uScript_SpawnTechsFromData_allowResurrection_157);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_157.Out)
				{
					Relay_InitialSpawn_161();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_161()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5b3f468d-a4c2-4618-b4a9-7fb51616c4fa", "uScript_SpawnTechsFromData", Relay_InitialSpawn_161))
			{
				int num = 0;
				Array firstFly03TechData = FirstFly03TechData;
				if (logic_uScript_SpawnTechsFromData_spawnData_161.Length != num + firstFly03TechData.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_161, num + firstFly03TechData.Length);
				}
				Array.Copy(firstFly03TechData, 0, logic_uScript_SpawnTechsFromData_spawnData_161, num, firstFly03TechData.Length);
				num += firstFly03TechData.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_161 = owner_Connection_162;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_161.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_161, logic_uScript_SpawnTechsFromData_ownerNode_161, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_161, logic_uScript_SpawnTechsFromData_allowResurrection_161);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_161.Out)
				{
					Relay_True_164();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_164()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e716789b-98ce-48f1-b439-45f97eac9759", "Set_Bool", Relay_True_164))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_164.True(out logic_uScriptAct_SetBool_Target_164);
				local_FirstFliesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_164;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_164()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e716789b-98ce-48f1-b439-45f97eac9759", "Set_Bool", Relay_False_164))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_164.False(out logic_uScriptAct_SetBool_Target_164);
				local_FirstFliesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_164;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_165()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("43f1f803-63fc-434c-b463-17a3c95f4465", "Compare_Bool", Relay_In_165))
			{
				logic_uScriptCon_CompareBool_Bool_165 = local_FirstFliesSpawned_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_165.In(logic_uScriptCon_CompareBool_Bool_165);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_165.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_165.False;
				if (num)
				{
					Relay_In_74();
				}
				if (flag)
				{
					Relay_InitialSpawn_155();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_168()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7bd76ff8-2de7-44e2-8e2b-936a7e30d2ca", "", Relay_Save_Out_168))
			{
				Relay_Save_139();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_168()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7bd76ff8-2de7-44e2-8e2b-936a7e30d2ca", "", Relay_Load_Out_168))
			{
				Relay_Load_139();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_168()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7bd76ff8-2de7-44e2-8e2b-936a7e30d2ca", "", Relay_Restart_Out_168))
			{
				Relay_Restart_139();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_168()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7bd76ff8-2de7-44e2-8e2b-936a7e30d2ca", "", Relay_Save_168))
			{
				logic_SubGraph_SaveLoadBool_boolean_168 = local_FirstFliesSpawned_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_168 = local_FirstFliesSpawned_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Save(ref logic_SubGraph_SaveLoadBool_boolean_168, logic_SubGraph_SaveLoadBool_boolAsVariable_168, logic_SubGraph_SaveLoadBool_uniqueID_168);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_168()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7bd76ff8-2de7-44e2-8e2b-936a7e30d2ca", "", Relay_Load_168))
			{
				logic_SubGraph_SaveLoadBool_boolean_168 = local_FirstFliesSpawned_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_168 = local_FirstFliesSpawned_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Load(ref logic_SubGraph_SaveLoadBool_boolean_168, logic_SubGraph_SaveLoadBool_boolAsVariable_168, logic_SubGraph_SaveLoadBool_uniqueID_168);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_168()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7bd76ff8-2de7-44e2-8e2b-936a7e30d2ca", "", Relay_Set_True_168))
			{
				logic_SubGraph_SaveLoadBool_boolean_168 = local_FirstFliesSpawned_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_168 = local_FirstFliesSpawned_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_168, logic_SubGraph_SaveLoadBool_boolAsVariable_168, logic_SubGraph_SaveLoadBool_uniqueID_168);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_168()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7bd76ff8-2de7-44e2-8e2b-936a7e30d2ca", "", Relay_Set_False_168))
			{
				logic_SubGraph_SaveLoadBool_boolean_168 = local_FirstFliesSpawned_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_168 = local_FirstFliesSpawned_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_168.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_168, logic_SubGraph_SaveLoadBool_boolAsVariable_168, logic_SubGraph_SaveLoadBool_uniqueID_168);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_170()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c02c3d62-fdd3-4723-92c0-d706f544aa68", "uScript_ClearOnScreenMessagesWithTag", Relay_In_170))
			{
				logic_uScript_ClearOnScreenMessagesWithTag_tag_170 = local_171_System_String;
				logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_170.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_170, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_170);
				if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_170.Out)
				{
					Relay_In_140();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_ClearOnScreenMessagesWithTag.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_172()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8ee6c7a6-ef6b-4584-a882-5ec3fa219649", "Pass", Relay_In_172))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_172.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_172.Out)
				{
					Relay_False_131();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_173()
	{
		if (relayCallCount++ < 1000)
		{
			uScriptDebug.Log("[Detox.ScriptEditor.LogicNode] ********MOB TECHS CLEAN-UP*******", uScriptDebug.Type.Message);
			if (!CheckDebugBreak("f00b4c18-adb6-4773-84b6-ef2235e32f8b", "uScript_DamageTechs", Relay_In_173))
			{
				int num = 0;
				Array array = local_MobTechs_TankArray;
				if (logic_uScript_DamageTechs_techs_173.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_DamageTechs_techs_173, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_DamageTechs_techs_173, num, array.Length);
				num += array.Length;
				logic_uScript_DamageTechs_uScript_DamageTechs_173.In(logic_uScript_DamageTechs_techs_173, logic_uScript_DamageTechs_dmgPercent_173, logic_uScript_DamageTechs_givePlyrCredit_173, logic_uScript_DamageTechs_leaveBlksPercent_173, logic_uScript_DamageTechs_makeVulnerable_173);
				if (logic_uScript_DamageTechs_uScript_DamageTechs_173.Out)
				{
					Relay_False_131();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_DamageTechs.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_177()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d2b6790f-d770-45bf-9c71-a460cdc099b6", "uScript_GetAndCheckWaveTechs", Relay_In_177))
			{
				logic_uScript_GetAndCheckWaveTechs_spawnData_177 = enemyGroupData;
				logic_uScript_GetAndCheckWaveTechs_ownerNode_177 = owner_Connection_175;
				int num = 0;
				Array array = local_MobTechs_TankArray;
				if (logic_uScript_GetAndCheckWaveTechs_techs_177.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckWaveTechs_techs_177, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_GetAndCheckWaveTechs_techs_177, num, array.Length);
				num += array.Length;
				logic_uScript_GetAndCheckWaveTechs_Return_177 = logic_uScript_GetAndCheckWaveTechs_uScript_GetAndCheckWaveTechs_177.In(logic_uScript_GetAndCheckWaveTechs_spawnData_177, logic_uScript_GetAndCheckWaveTechs_ownerNode_177, ref logic_uScript_GetAndCheckWaveTechs_techs_177);
				local_MobTechs_TankArray = logic_uScript_GetAndCheckWaveTechs_techs_177;
				bool allAlive = logic_uScript_GetAndCheckWaveTechs_uScript_GetAndCheckWaveTechs_177.AllAlive;
				bool someAlive = logic_uScript_GetAndCheckWaveTechs_uScript_GetAndCheckWaveTechs_177.SomeAlive;
				bool allDead = logic_uScript_GetAndCheckWaveTechs_uScript_GetAndCheckWaveTechs_177.AllDead;
				bool waitingToSpawn = logic_uScript_GetAndCheckWaveTechs_uScript_GetAndCheckWaveTechs_177.WaitingToSpawn;
				if (allAlive)
				{
					Relay_In_173();
				}
				if (someAlive)
				{
					Relay_In_173();
				}
				if (allDead)
				{
					Relay_In_172();
				}
				if (waitingToSpawn)
				{
					Relay_In_172();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_GetAndCheckWaveTechs.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_179()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c744d973-8c65-413f-b841-79c860ac3bb3", "Distance_Is_player_in_range_of_tech", Relay_In_179))
			{
				logic_uScript_IsPlayerInRangeOfTech_tech_179 = local_TargetTech_Tank;
				logic_uScript_IsPlayerInRangeOfTech_range_179 = distAtWhichTargetTechLeft;
				logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_179.In(logic_uScript_IsPlayerInRangeOfTech_tech_179, logic_uScript_IsPlayerInRangeOfTech_range_179, logic_uScript_IsPlayerInRangeOfTech_techs_179);
				bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_179.InRange;
				bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_179.OutOfRange;
				if (inRange)
				{
					Relay_In_15();
				}
				if (outOfRange)
				{
					Relay_In_223();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Distance/Is player in range of tech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_182()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("19bbae6d-498b-4b5d-8bbb-cac6e4a5c340", "Compare_Bool", Relay_In_182))
			{
				logic_uScriptCon_CompareBool_Bool_182 = local_MsgFound3and4Shown_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_182.In(logic_uScriptCon_CompareBool_Bool_182);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_182.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_182.False;
				if (num)
				{
					Relay_In_143();
				}
				if (flag)
				{
					Relay_In_194();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_184()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f9a3cbd1-d3a3-4155-b455-cdd26e81558b", "Set_Bool", Relay_True_184))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_184.True(out logic_uScriptAct_SetBool_Target_184);
				local_MsgFound1and2Shown_System_Boolean = logic_uScriptAct_SetBool_Target_184;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_184()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f9a3cbd1-d3a3-4155-b455-cdd26e81558b", "Set_Bool", Relay_False_184))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_184.False(out logic_uScriptAct_SetBool_Target_184);
				local_MsgFound1and2Shown_System_Boolean = logic_uScriptAct_SetBool_Target_184;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_185()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d6dabaa4-bc06-4ec0-95b5-6ea05a9a4d49", "uScript_AddOnScreenMessage", Relay_In_185))
			{
				int num = 0;
				Array array = msgTargetTechFound1to2;
				if (logic_uScript_AddOnScreenMessage_locString_185.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_185, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_185, num, array.Length);
				num += array.Length;
				logic_uScript_AddOnScreenMessage_tag_185 = local_188_System_String;
				logic_uScript_AddOnScreenMessage_speaker_185 = messageSpeakerSpider;
				logic_uScript_AddOnScreenMessage_Return_185 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_185.In(logic_uScript_AddOnScreenMessage_locString_185, logic_uScript_AddOnScreenMessage_msgPriority_185, logic_uScript_AddOnScreenMessage_holdMsg_185, logic_uScript_AddOnScreenMessage_tag_185, logic_uScript_AddOnScreenMessage_speaker_185, logic_uScript_AddOnScreenMessage_side_185);
				if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_185.Shown)
				{
					Relay_True_184();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_AddOnScreenMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_194()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a388f823-6bd9-43ce-bb48-87a1fcf1f3f0", "uScript_AddOnScreenMessage", Relay_In_194))
			{
				int num = 0;
				Array array = msgTargetTechFound3to4;
				if (logic_uScript_AddOnScreenMessage_locString_194.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_194, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_194, num, array.Length);
				num += array.Length;
				logic_uScript_AddOnScreenMessage_tag_194 = local_191_System_String;
				logic_uScript_AddOnScreenMessage_speaker_194 = messageSpeakerSpider;
				logic_uScript_AddOnScreenMessage_Return_194 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_194.In(logic_uScript_AddOnScreenMessage_locString_194, logic_uScript_AddOnScreenMessage_msgPriority_194, logic_uScript_AddOnScreenMessage_holdMsg_194, logic_uScript_AddOnScreenMessage_tag_194, logic_uScript_AddOnScreenMessage_speaker_194, logic_uScript_AddOnScreenMessage_side_194);
				if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_194.Shown)
				{
					Relay_True_195();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_AddOnScreenMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_195()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("82066988-799b-455d-b83a-a141bb077d7c", "Set_Bool", Relay_True_195))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_195.True(out logic_uScriptAct_SetBool_Target_195);
				local_MsgFound3and4Shown_System_Boolean = logic_uScriptAct_SetBool_Target_195;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_195()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("82066988-799b-455d-b83a-a141bb077d7c", "Set_Bool", Relay_False_195))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_195.False(out logic_uScriptAct_SetBool_Target_195);
				local_MsgFound3and4Shown_System_Boolean = logic_uScriptAct_SetBool_Target_195;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_198()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bbd8cbed-1d4a-44c8-8353-92c23e7d53af", "", Relay_Save_Out_198))
			{
				Relay_Save_38();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_198()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bbd8cbed-1d4a-44c8-8353-92c23e7d53af", "", Relay_Load_Out_198))
			{
				Relay_Load_38();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_198()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bbd8cbed-1d4a-44c8-8353-92c23e7d53af", "", Relay_Restart_Out_198))
			{
				Relay_Restart_38();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_198()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bbd8cbed-1d4a-44c8-8353-92c23e7d53af", "", Relay_Save_198))
			{
				logic_SubGraph_SaveLoadBool_boolean_198 = local_MsgFound3and4Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_198 = local_MsgFound3and4Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Save(ref logic_SubGraph_SaveLoadBool_boolean_198, logic_SubGraph_SaveLoadBool_boolAsVariable_198, logic_SubGraph_SaveLoadBool_uniqueID_198);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_198()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bbd8cbed-1d4a-44c8-8353-92c23e7d53af", "", Relay_Load_198))
			{
				logic_SubGraph_SaveLoadBool_boolean_198 = local_MsgFound3and4Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_198 = local_MsgFound3and4Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Load(ref logic_SubGraph_SaveLoadBool_boolean_198, logic_SubGraph_SaveLoadBool_boolAsVariable_198, logic_SubGraph_SaveLoadBool_uniqueID_198);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_198()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bbd8cbed-1d4a-44c8-8353-92c23e7d53af", "", Relay_Set_True_198))
			{
				logic_SubGraph_SaveLoadBool_boolean_198 = local_MsgFound3and4Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_198 = local_MsgFound3and4Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_198, logic_SubGraph_SaveLoadBool_boolAsVariable_198, logic_SubGraph_SaveLoadBool_uniqueID_198);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_198()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bbd8cbed-1d4a-44c8-8353-92c23e7d53af", "", Relay_Set_False_198))
			{
				logic_SubGraph_SaveLoadBool_boolean_198 = local_MsgFound3and4Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_198 = local_MsgFound3and4Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_198, logic_SubGraph_SaveLoadBool_boolAsVariable_198, logic_SubGraph_SaveLoadBool_uniqueID_198);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_199()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c574aa34-ee35-416d-8d49-2f4bccb065a7", "Set_tank_invulnerable", Relay_In_199))
			{
				logic_uScript_SetTankInvulnerable_tank_199 = local_TargetTech_Tank;
				logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_199.In(logic_uScript_SetTankInvulnerable_invulnerable_199, logic_uScript_SetTankInvulnerable_tank_199);
				if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_199.Out)
				{
					Relay_In_170();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set tank invulnerable.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_201()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("1f48c336-abef-4f69-8896-4c55fa70570b", "Set_tank_invulnerable", Relay_In_201))
			{
				logic_uScript_SetTankInvulnerable_tank_201 = local_TargetTech_Tank;
				logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_201.In(logic_uScript_SetTankInvulnerable_invulnerable_201, logic_uScript_SetTankInvulnerable_tank_201);
				if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_201.Out)
				{
					Relay_In_226();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set tank invulnerable.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_204()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("338b4a56-eea5-4bfa-9f0b-d8f57799e546", "uScript_SpawnTechsFromData", Relay_InitialSpawn_204))
			{
				int num = 0;
				Array array = guardFliesTechData;
				if (logic_uScript_SpawnTechsFromData_spawnData_204.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_204, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_204, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_204 = owner_Connection_203;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_204.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_204, logic_uScript_SpawnTechsFromData_ownerNode_204, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_204, logic_uScript_SpawnTechsFromData_allowResurrection_204);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_204.Out)
				{
					Relay_In_15();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_207()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("1623270c-46fc-4abf-932c-dd39d8094463", "uScript_SpawnTechsFromData", Relay_InitialSpawn_207))
			{
				int num = 0;
				Array array = chargerTechData;
				if (logic_uScript_SpawnTechsFromData_spawnData_207.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_207, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_207, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_207 = owner_Connection_206;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_207.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_207, logic_uScript_SpawnTechsFromData_ownerNode_207, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_207, logic_uScript_SpawnTechsFromData_allowResurrection_207);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_207.Out)
				{
					Relay_InitialSpawn_2();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_210()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("891ce3fb-7477-4e0c-ae0f-15633170938b", "uScript_GetAndCheckTechs", Relay_In_210))
			{
				int num = 0;
				Array array = chargerTechData;
				if (logic_uScript_GetAndCheckTechs_techData_210.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_210, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_210, num, array.Length);
				num += array.Length;
				logic_uScript_GetAndCheckTechs_ownerNode_210 = owner_Connection_211;
				int num2 = 0;
				Array array2 = local_chargerTechs_TankArray;
				if (logic_uScript_GetAndCheckTechs_techs_210.Length != num2 + array2.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_210, num2 + array2.Length);
				}
				Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_210, num2, array2.Length);
				num2 += array2.Length;
				logic_uScript_GetAndCheckTechs_Return_210 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_210.In(logic_uScript_GetAndCheckTechs_techData_210, logic_uScript_GetAndCheckTechs_ownerNode_210, ref logic_uScript_GetAndCheckTechs_techs_210);
				local_chargerTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_210;
				bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_210.AllAlive;
				bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_210.SomeAlive;
				bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_210.AllDead;
				if (allAlive)
				{
					Relay_AtIndex_213();
				}
				if (someAlive)
				{
					Relay_AtIndex_213();
				}
				if (allDead)
				{
					Relay_AtIndex_29();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_GetAndCheckTechs.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_AtIndex_213()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("36b6da67-a7f0-419f-b934-08adcc380845", "uScript_AccessListTech", Relay_AtIndex_213))
			{
				int num = 0;
				Array array = local_chargerTechs_TankArray;
				if (logic_uScript_AccessListTech_techList_213.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AccessListTech_techList_213, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AccessListTech_techList_213, num, array.Length);
				num += array.Length;
				logic_uScript_AccessListTech_uScript_AccessListTech_213.AtIndex(ref logic_uScript_AccessListTech_techList_213, logic_uScript_AccessListTech_index_213, out logic_uScript_AccessListTech_value_213);
				local_chargerTechs_TankArray = logic_uScript_AccessListTech_techList_213;
				local_chargerTech_Tank = logic_uScript_AccessListTech_value_213;
				if (logic_uScript_AccessListTech_uScript_AccessListTech_213.Out)
				{
					Relay_AtIndex_29();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_AccessListTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_215()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("38f31bfc-ae06-4fd9-a4f3-93d5b84c0901", "Pass", Relay_In_215))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_215.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_215.Out)
				{
					Relay_In_219();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_219()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("75ed3c29-4f6f-48de-ab89-b673db2c483c", "uScript_GetAndCheckTechs", Relay_In_219))
			{
				int num = 0;
				Array array = chargerTechData;
				if (logic_uScript_GetAndCheckTechs_techData_219.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_219, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_219, num, array.Length);
				num += array.Length;
				logic_uScript_GetAndCheckTechs_ownerNode_219 = owner_Connection_217;
				int num2 = 0;
				Array array2 = local_chargerTechs_TankArray;
				if (logic_uScript_GetAndCheckTechs_techs_219.Length != num2 + array2.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_219, num2 + array2.Length);
				}
				Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_219, num2, array2.Length);
				num2 += array2.Length;
				logic_uScript_GetAndCheckTechs_Return_219 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_219.In(logic_uScript_GetAndCheckTechs_techData_219, logic_uScript_GetAndCheckTechs_ownerNode_219, ref logic_uScript_GetAndCheckTechs_techs_219);
				local_chargerTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_219;
				bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_219.AllAlive;
				bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_219.SomeAlive;
				bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_219.AllDead;
				bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_219.WaitingToSpawn;
				if (allAlive)
				{
					Relay_In_220();
				}
				if (someAlive)
				{
					Relay_In_220();
				}
				if (allDead)
				{
					Relay_In_221();
				}
				if (waitingToSpawn)
				{
					Relay_In_221();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_GetAndCheckTechs.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_220()
	{
		if (relayCallCount++ < 1000)
		{
			uScriptDebug.Log("[Detox.ScriptEditor.LogicNode] ********MOB TECHS CLEAN-UP*******", uScriptDebug.Type.Message);
			if (!CheckDebugBreak("a6b238b2-857b-453c-a5b9-a088e88784f2", "uScript_DamageTechs", Relay_In_220))
			{
				int num = 0;
				Array array = local_chargerTechs_TankArray;
				if (logic_uScript_DamageTechs_techs_220.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_DamageTechs_techs_220, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_DamageTechs_techs_220, num, array.Length);
				num += array.Length;
				logic_uScript_DamageTechs_uScript_DamageTechs_220.In(logic_uScript_DamageTechs_techs_220, logic_uScript_DamageTechs_dmgPercent_220, logic_uScript_DamageTechs_givePlyrCredit_220, logic_uScript_DamageTechs_leaveBlksPercent_220, logic_uScript_DamageTechs_makeVulnerable_220);
				if (logic_uScript_DamageTechs_uScript_DamageTechs_220.Out)
				{
					Relay_In_177();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_DamageTechs.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_221()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("1ce370d6-8e18-4b96-9a2d-53d876b98511", "Pass", Relay_In_221))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_221.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_221.Out)
				{
					Relay_In_177();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_223()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("44eb6f11-06e2-41a2-96ce-13c2e8ac7a01", "Set_tank_invulnerable", Relay_In_223))
			{
				logic_uScript_SetTankInvulnerable_tank_223 = local_chargerTech_Tank;
				logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_223.In(logic_uScript_SetTankInvulnerable_invulnerable_223, logic_uScript_SetTankInvulnerable_tank_223);
				if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_223.Out)
				{
					Relay_In_199();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set tank invulnerable.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_225()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b25dc384-13e9-474a-be4b-dcc42253e963", "Pass", Relay_In_225))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_225.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_225.Out)
				{
					Relay_In_63();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_226()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f6ef90b0-982f-43b4-bf5a-a847ac670b63", "AI_Set_team", Relay_In_226))
			{
				logic_uScript_SetTankTeam_tank_226 = local_chargerTech_Tank;
				logic_uScript_SetTankTeam_uScript_SetTankTeam_226.In(logic_uScript_SetTankTeam_tank_226, logic_uScript_SetTankTeam_team_226);
				if (logic_uScript_SetTankTeam_uScript_SetTankTeam_226.Out)
				{
					Relay_In_227();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at AI/Set team.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_227()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c294b230-54f9-4332-a0ac-a23f54781e7e", "Set_tank_invulnerable", Relay_In_227))
			{
				logic_uScript_SetTankInvulnerable_tank_227 = local_chargerTech_Tank;
				logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_227.In(logic_uScript_SetTankInvulnerable_invulnerable_227, logic_uScript_SetTankInvulnerable_tank_227);
				if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_227.Out)
				{
					Relay_True_19();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at Set tank invulnerable.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_228()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c45af128-f016-4b69-84cb-477ac85fb624", "uScript_ClearEncounterTarget", Relay_In_228))
			{
				logic_uScript_ClearEncounterTarget_owner_228 = owner_Connection_229;
				logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_228.In(logic_uScript_ClearEncounterTarget_owner_228);
				if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_228.Out)
				{
					Relay_Succeed_37();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_ClearEncounterTarget.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_231()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("45cd5bf3-6d12-4d96-95cc-d89fd8d6d910", "uScript_SetBatteryChargeAmount", Relay_In_231))
			{
				logic_uScript_SetBatteryChargeAmount_tech_231 = local_TargetTech_Tank;
				logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_231.In(logic_uScript_SetBatteryChargeAmount_tech_231, logic_uScript_SetBatteryChargeAmount_chargeAmount_231);
				if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_231.Out)
				{
					Relay_In_17();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_HillFort.uscript at uScript_SetBatteryChargeAmount.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void UpdateEditorValues()
	{
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:distAtWhichTargetTechFound", distAtWhichTargetTechFound);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("64ef9488-4712-4481-be10-3a8c75d0a29b", distAtWhichTargetTechFound);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:TargetTechSpawned", local_TargetTechSpawned_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("937d2304-385b-45aa-92b9-38aee0f50e56", local_TargetTechSpawned_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:TargetTechFound", local_TargetTechFound_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("566222e5-65e1-4716-8280-ac17b8ab4b0b", local_TargetTechFound_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:Msg", local_Msg_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("fc868012-e051-42da-9db6-e2556d32b0d0", local_Msg_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:msgMissionComplete", msgMissionComplete);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("5a8fd109-5625-4226-a63a-9d1ba22c3fc8", msgMissionComplete);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:StartValue", local_StartValue_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("ea3a3e51-b6b0-43e1-94bd-f620956cea20", local_StartValue_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:ShownMsgEnemiesGivingUp", local_ShownMsgEnemiesGivingUp_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("00dadb50-9775-4a9c-afa7-d28ea664033d", local_ShownMsgEnemiesGivingUp_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:61", local_61_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("d04a9055-632c-4e08-886f-01f598a51549", local_61_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:NPCTanks", local_NPCTanks_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("b8899805-e1c3-49b5-8946-1b9ee57b6672", local_NPCTanks_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:77", local_77_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("e8c10597-fb5d-41ec-baf0-cc0e56a15789", local_77_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:distAtWhichNPCTechFound", distAtWhichNPCTechFound);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("308bb313-ab96-4aaa-a0b0-906c794c63be", distAtWhichNPCTechFound);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:msgNPCGreetingEnemyDead", msgNPCGreetingEnemyDead);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("d97ad896-dfd2-4ead-ad8f-8cbccfa1e3d8", msgNPCGreetingEnemyDead);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:msgNPCGreeting", msgNPCGreeting);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("88688911-440e-454e-afcf-f5844b5f54b1", msgNPCGreeting);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:NPCTech", NPCTech);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("1950be86-f08e-47e3-8b2d-78c448600a08", NPCTech);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:96", local_96_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("0437fd4a-ba93-4a5e-ab8a-a0aa4d332837", local_96_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:messageSpeaker", messageSpeaker);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("bd1cacc8-eaf3-4ed8-b5b6-687d51a378fa", messageSpeaker);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:msgNPCGreetingInturrupt", msgNPCGreetingInturrupt);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("38ddb1ed-f7fb-411c-9a1d-3428b85f52db", msgNPCGreetingInturrupt);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:DelayBetweenEnemyGroupSpaws", DelayBetweenEnemyGroupSpaws);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("4100d286-4654-456d-9e94-e5912729d8f7", DelayBetweenEnemyGroupSpaws);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:ReinforcementSubGroupSize", ReinforcementSubGroupSize);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("94efe22e-0559-4799-9bd2-6cb5b48ad841", ReinforcementSubGroupSize);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:TotalGroupEnemiesSpawned", local_TotalGroupEnemiesSpawned_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("27a56519-b969-495b-a59d-0fdc43a54997", local_TotalGroupEnemiesSpawned_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:DelayBetweenRespawnArivals", DelayBetweenRespawnArivals);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("562cc6f5-680a-48f6-87c1-2e929cd4476b", DelayBetweenRespawnArivals);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:ShownMsgEnemySpotted", local_ShownMsgEnemySpotted_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("fa48eb79-ed9a-4315-9019-5d28a5cb60f5", local_ShownMsgEnemySpotted_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:NPCSeen", local_NPCSeen_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("6ecca938-4fee-4a8c-b564-d089163cba4b", local_NPCSeen_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:EnemyDeadEarly", local_EnemyDeadEarly_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("478638ec-bf4b-474a-bb02-255e501307e1", local_EnemyDeadEarly_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:ObjectiveComplete", local_ObjectiveComplete_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("d7c317c5-b8b4-4838-a354-1c55de9c42b5", local_ObjectiveComplete_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:targetTechs", local_targetTechs_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("62c65f5b-9ccd-40c8-be8f-7e302945ff33", local_targetTechs_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:targetTechData", targetTechData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("7c70bae5-bac4-41d5-845a-0cdd10d0e262", targetTechData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:NPCIgnored", local_NPCIgnored_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("00d41cc4-7703-4d00-a3bd-925517eba35b", local_NPCIgnored_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:msgTargetTechFound5", msgTargetTechFound5);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("e6fd261e-7919-4693-ab38-fa5923841eed", msgTargetTechFound5);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:NPCMet", local_NPCMet_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("6c5fbf67-a639-4e72-84a5-e7ca4b169a94", local_NPCMet_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:NPCDespawnParticleEffect", NPCDespawnParticleEffect);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("21e98417-99e5-4b63-a3e0-9c5c3bdb11b0", NPCDespawnParticleEffect);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:NPCFlyAwayAI", NPCFlyAwayAI);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("a6366dea-6d6c-4936-a993-e438e89bb2a3", NPCFlyAwayAI);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:NPCTank", local_NPCTank_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("56c8dfb3-bed3-4801-9e62-814aed4badbd", local_NPCTank_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:Stage", local_Stage_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("449e6637-f212-4448-9abd-24395961fbbd", local_Stage_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:FirstFly01TechData", FirstFly01TechData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("bc92fe6d-94ed-4e74-995a-336f8329aa76", FirstFly01TechData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:FirstFly02TechData", FirstFly02TechData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("a35d1cdc-9baa-4dfd-9a37-256dd50af68e", FirstFly02TechData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:FirstFly03TechData", FirstFly03TechData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("09ddce33-fc52-4555-8458-85072276a776", FirstFly03TechData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:FirstFliesSpawned", local_FirstFliesSpawned_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("916a7038-ed34-4f9c-b6f7-399ea926b153", local_FirstFliesSpawned_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:169", local_169_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("e0c618c2-8019-4e30-9473-1430e60adc94", local_169_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:171", local_171_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("6457b098-b45c-46b1-8918-1caa50cacd53", local_171_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:MobTechs", local_MobTechs_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("b53b9e0e-b60b-44e9-9046-76130358175b", local_MobTechs_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:AllowEnemyGroupToRespawn", AllowEnemyGroupToRespawn);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("20e79ceb-253d-4535-8083-9c498427afc7", AllowEnemyGroupToRespawn);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:enemyGroupData", enemyGroupData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("02c375dc-3afe-4b53-b574-fc8664a6f94c", enemyGroupData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:distAtWhichTargetTechLeft", distAtWhichTargetTechLeft);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("f32038f3-64f8-4cce-97c2-1d389373566f", distAtWhichTargetTechLeft);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:msgTargetTechFound1to2", msgTargetTechFound1to2);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("276b2a28-a7a9-42f0-a8b7-678eaec2e828", msgTargetTechFound1to2);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:188", local_188_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("9c962d86-7254-4c4a-8a85-f708c6fdbdd0", local_188_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:191", local_191_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("b987f6ee-dd53-4852-8195-82c8b29e750c", local_191_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:messageSpeakerSpider", messageSpeakerSpider);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("c1a931b5-f05c-41ac-9f33-93f0decdbaf0", messageSpeakerSpider);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:msgTargetTechFound3to4", msgTargetTechFound3to4);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("962dc348-c708-48e9-bf56-1a07dedb5ab1", msgTargetTechFound3to4);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:MsgFound1and2Shown", local_MsgFound1and2Shown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("4170bcba-0dc3-41ed-9036-a72823ad905a", local_MsgFound1and2Shown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:MsgFound3and4Shown", local_MsgFound3and4Shown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("bf37d27f-cbbb-4a99-b145-773f2449e474", local_MsgFound3and4Shown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:TargetTech", local_TargetTech_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("d2f44795-f69c-42e3-9bfc-6cb516b0e92d", local_TargetTech_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:guardFliesTechData", guardFliesTechData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("01d1a62b-18d2-488e-9522-09bf87081ec9", guardFliesTechData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:chargerTechData", chargerTechData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("b956b2bb-26e5-43c2-8aba-cf0eeaf5da4e", chargerTechData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:chargerTechs", local_chargerTechs_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("26286a1c-1c02-4da4-8004-74f8225962cd", local_chargerTechs_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:chargerTech", local_chargerTech_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("2f9287cc-7927-4fe7-8546-4ec4f66fd441", local_chargerTech_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_HillFort.uscript:EnemyGroupSize", EnemyGroupSize);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("42bd3abf-6871-4fd5-891b-eed9aec83c24", EnemyGroupSize);
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
