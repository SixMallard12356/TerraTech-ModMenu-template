using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_RR_License_NPCController", "")]
[NodePath("Graphs")]
public class SubGraph_RR_License_NPCController : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SpawnTechData[] external_11 = new SpawnTechData[0];

	private bool external_25;

	private string external_10 = "";

	private uScript_AddMessage.MessageData external_18;

	private uScript_AddMessage.MessageSpeaker external_38;

	private bool external_43;

	private Transform external_12;

	private bool external_16;

	private SpawnTechData[] external_13 = new SpawnTechData[0];

	private Tank[] local_1_TankArray = new Tank[0];

	private ManOnScreenMessages.OnScreenMessage local_NPCDialogue_ManOnScreenMessages_OnScreenMessage;

	private bool local_NPCFound_System_Boolean;

	private Tank local_NPCTech_Tank;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_7;

	private GameObject owner_Connection_19;

	private GameObject owner_Connection_20;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_3 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_3 = new Tank[0];

	private int logic_uScript_AccessListTech_index_3;

	private Tank logic_uScript_AccessListTech_value_3;

	private bool logic_uScript_AccessListTech_Out_3 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_4 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_4 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_4;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_4 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_4;

	private bool logic_uScript_SpawnTechsFromData_Out_4 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_5 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_5;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_5;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_5;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_5;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_5;

	private bool logic_uScript_FlyTechUpAndAway_Out_5 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_6 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_6;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_6 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_6;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_6 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_6 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_6 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_6 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_8 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_8 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_8 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_8 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_8 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_15;

	private bool logic_uScriptCon_CompareBool_True_15 = true;

	private bool logic_uScriptCon_CompareBool_False_15 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_17 = true;

	private uScript_SetEncounterWaypointVisibility logic_uScript_SetEncounterWaypointVisibility_uScript_SetEncounterWaypointVisibility_21 = new uScript_SetEncounterWaypointVisibility();

	private GameObject logic_uScript_SetEncounterWaypointVisibility_ownerNode_21;

	private bool logic_uScript_SetEncounterWaypointVisibility_Out_21 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_22 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_22;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_22 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_22 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_22 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_24 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_24;

	private bool logic_uScriptCon_CompareBool_True_24 = true;

	private bool logic_uScriptCon_CompareBool_False_24 = true;

	private uScript_SetTechMarkerState logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_26 = new uScript_SetTechMarkerState();

	private Tank logic_uScript_SetTechMarkerState_tech_26;

	private bool logic_uScript_SetTechMarkerState_Out_26 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_28 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_28 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_29 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_29 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_31 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_31;

	private bool logic_uScriptAct_SetBool_Out_31 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_31 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_31 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_33;

	private bool logic_uScriptCon_CompareBool_True_33 = true;

	private bool logic_uScriptCon_CompareBool_False_33 = true;

	private uScript_SetTechAIType logic_uScript_SetTechAIType_uScript_SetTechAIType_35 = new uScript_SetTechAIType();

	private Tank logic_uScript_SetTechAIType_tech_35;

	private AITreeType.AITypes logic_uScript_SetTechAIType_aiType_35 = AITreeType.AITypes.FacePlayer;

	private bool logic_uScript_SetTechAIType_Out_35 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_37 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_37;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_37;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_37;

	private bool logic_uScript_AddMessage_Out_37 = true;

	private bool logic_uScript_AddMessage_Shown_37 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_39 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_39;

	private bool logic_uScriptAct_SetBool_Out_39 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_39 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_39 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_42;

	private bool logic_uScriptCon_CompareBool_True_42 = true;

	private bool logic_uScriptCon_CompareBool_False_42 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_44 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_44;

	private bool logic_uScript_RemoveOnScreenMessage_instant_44;

	private bool logic_uScript_RemoveOnScreenMessage_Out_44 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_46;

	private bool logic_uScriptCon_CompareBool_True_46 = true;

	private bool logic_uScriptCon_CompareBool_False_46 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_49 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
		}
		if (null == owner_Connection_7 || !m_RegisteredForEvents)
		{
			owner_Connection_7 = parentGameObject;
		}
		if (null == owner_Connection_19 || !m_RegisteredForEvents)
		{
			owner_Connection_19 = parentGameObject;
		}
		if (null == owner_Connection_20 || !m_RegisteredForEvents)
		{
			owner_Connection_20 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_AccessListTech_uScript_AccessListTech_3.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_4.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_5.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_8.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17.SetParent(g);
		logic_uScript_SetEncounterWaypointVisibility_uScript_SetEncounterWaypointVisibility_21.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_22.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_24.SetParent(g);
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_26.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_28.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_29.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.SetParent(g);
		logic_uScript_SetTechAIType_uScript_SetTechAIType_35.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_37.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_44.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.SetParent(g);
		owner_Connection_2 = parentGameObject;
		owner_Connection_7 = parentGameObject;
		owner_Connection_19 = parentGameObject;
		owner_Connection_20 = parentGameObject;
	}

	public void Awake()
	{
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_5.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_22.OnDisable();
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_26.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_37.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
	}

	public void OnDestroy()
	{
	}

	[FriendlyName("In", "")]
	public void In([FriendlyName("currentNPCTechData", "")] SpawnTechData[] currentNPCTechData, [FriendlyName("hideNPCMarker?", "")] bool hideNPCMarker_, [FriendlyName("npcTriggerVolume", "")] string npcTriggerVolume, [FriendlyName("npcDialogue", "")] uScript_AddMessage.MessageData npcDialogue, [FriendlyName("npcSpeaker", "")] uScript_AddMessage.MessageSpeaker npcSpeaker, [FriendlyName("waitUnitDialogueFinished?", "")] bool waitUnitDialogueFinished_, [FriendlyName("npcDespawnParticleEffect", "")] Transform npcDespawnParticleEffect, [FriendlyName("spawnNextNPC?", "")] bool spawnNextNPC_, [FriendlyName("nextNPCSpawnData", "")] SpawnTechData[] nextNPCSpawnData)
	{
		external_11 = currentNPCTechData;
		external_25 = hideNPCMarker_;
		external_10 = npcTriggerVolume;
		external_18 = npcDialogue;
		external_38 = npcSpeaker;
		external_43 = waitUnitDialogueFinished_;
		external_12 = npcDespawnParticleEffect;
		external_16 = spawnNextNPC_;
		external_13 = nextNPCSpawnData;
		Relay_In_6();
	}

	private void Relay_AtIndex_3()
	{
		int num = 0;
		Array array = local_1_TankArray;
		if (logic_uScript_AccessListTech_techList_3.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_3, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_3, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_3.AtIndex(ref logic_uScript_AccessListTech_techList_3, logic_uScript_AccessListTech_index_3, out logic_uScript_AccessListTech_value_3);
		local_1_TankArray = logic_uScript_AccessListTech_techList_3;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_3;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_3.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_InitialSpawn_4()
	{
		int num = 0;
		Array array = external_13;
		if (logic_uScript_SpawnTechsFromData_spawnData_4.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_4, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_4, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_4 = owner_Connection_7;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_4.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_4, logic_uScript_SpawnTechsFromData_ownerNode_4, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_4, logic_uScript_SpawnTechsFromData_allowResurrection_4);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_4.Out)
		{
			Relay_Connection_14();
		}
	}

	private void Relay_In_5()
	{
		logic_uScript_FlyTechUpAndAway_tech_5 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_removalParticles_5 = external_12;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_5.In(logic_uScript_FlyTechUpAndAway_tech_5, logic_uScript_FlyTechUpAndAway_maxLifetime_5, logic_uScript_FlyTechUpAndAway_targetHeight_5, logic_uScript_FlyTechUpAndAway_aiTree_5, logic_uScript_FlyTechUpAndAway_removalParticles_5);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_5.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_6()
	{
		int num = 0;
		Array array = external_11;
		if (logic_uScript_GetAndCheckTechs_techData_6.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_6, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_6, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_6 = owner_Connection_2;
		int num2 = 0;
		Array array2 = local_1_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_6.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_6, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_6, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_6 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6.In(logic_uScript_GetAndCheckTechs_techData_6, logic_uScript_GetAndCheckTechs_ownerNode_6, ref logic_uScript_GetAndCheckTechs_techs_6);
		local_1_TankArray = logic_uScript_GetAndCheckTechs_techs_6;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_6.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_3();
		}
		if (someAlive)
		{
			Relay_AtIndex_3();
		}
	}

	private void Relay_In_8()
	{
		logic_uScript_IsPlayerInTrigger_triggerAreaName_8 = external_10;
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_8.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_8);
		bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_8.InRange;
		bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_8.OutOfRange;
		if (inRange)
		{
			Relay_In_35();
		}
		if (outOfRange)
		{
			Relay_In_42();
		}
	}

	private void Relay_Connection_9()
	{
	}

	private void Relay_Connection_10()
	{
	}

	private void Relay_Connection_11()
	{
	}

	private void Relay_Connection_12()
	{
	}

	private void Relay_Connection_13()
	{
	}

	private void Relay_Connection_14()
	{
		if (this.Out != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Out(this, args);
		}
	}

	private void Relay_In_15()
	{
		logic_uScriptCon_CompareBool_Bool_15 = external_16;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.In(logic_uScriptCon_CompareBool_Bool_15);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_15.False;
		if (num)
		{
			Relay_InitialSpawn_4();
		}
		if (flag)
		{
			Relay_In_17();
		}
	}

	private void Relay_Connection_16()
	{
	}

	private void Relay_In_17()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_17.Out)
		{
			Relay_Connection_14();
		}
	}

	private void Relay_Connection_18()
	{
	}

	private void Relay_Show_21()
	{
		logic_uScript_SetEncounterWaypointVisibility_ownerNode_21 = owner_Connection_19;
		logic_uScript_SetEncounterWaypointVisibility_uScript_SetEncounterWaypointVisibility_21.Show(logic_uScript_SetEncounterWaypointVisibility_ownerNode_21);
		if (logic_uScript_SetEncounterWaypointVisibility_uScript_SetEncounterWaypointVisibility_21.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_Hide_21()
	{
		logic_uScript_SetEncounterWaypointVisibility_ownerNode_21 = owner_Connection_19;
		logic_uScript_SetEncounterWaypointVisibility_uScript_SetEncounterWaypointVisibility_21.Hide(logic_uScript_SetEncounterWaypointVisibility_ownerNode_21);
		if (logic_uScript_SetEncounterWaypointVisibility_uScript_SetEncounterWaypointVisibility_21.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_In_22()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_22 = owner_Connection_20;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_22.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_22);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_22.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_22.False;
		if (num)
		{
			Relay_Hide_21();
		}
		if (flag)
		{
			Relay_Show_21();
		}
	}

	private void Relay_In_24()
	{
		logic_uScriptCon_CompareBool_Bool_24 = external_25;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_24.In(logic_uScriptCon_CompareBool_Bool_24);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_24.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_24.False;
		if (num)
		{
			Relay_Hide_26();
		}
		if (flag)
		{
			Relay_In_28();
		}
	}

	private void Relay_Connection_25()
	{
	}

	private void Relay_Show_26()
	{
		logic_uScript_SetTechMarkerState_tech_26 = local_NPCTech_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_26.Show(logic_uScript_SetTechMarkerState_tech_26);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_26.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_Hide_26()
	{
		logic_uScript_SetTechMarkerState_tech_26 = local_NPCTech_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_26.Hide(logic_uScript_SetTechMarkerState_tech_26);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_26.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_In_28()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_28.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_28.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_29()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_29.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_29.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_True_31()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.True(out logic_uScriptAct_SetBool_Target_31);
		local_NPCFound_System_Boolean = logic_uScriptAct_SetBool_Target_31;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_31.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_False_31()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.False(out logic_uScriptAct_SetBool_Target_31);
		local_NPCFound_System_Boolean = logic_uScriptAct_SetBool_Target_31;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_31.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_33()
	{
		logic_uScriptCon_CompareBool_Bool_33 = local_NPCFound_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.In(logic_uScriptCon_CompareBool_Bool_33);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.True)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_35()
	{
		logic_uScript_SetTechAIType_tech_35 = local_NPCTech_Tank;
		logic_uScript_SetTechAIType_uScript_SetTechAIType_35.In(logic_uScript_SetTechAIType_tech_35, logic_uScript_SetTechAIType_aiType_35);
		if (logic_uScript_SetTechAIType_uScript_SetTechAIType_35.Out)
		{
			Relay_True_31();
		}
	}

	private void Relay_In_37()
	{
		logic_uScript_AddMessage_messageData_37 = external_18;
		logic_uScript_AddMessage_speaker_37 = external_38;
		logic_uScript_AddMessage_Return_37 = logic_uScript_AddMessage_uScript_AddMessage_37.In(logic_uScript_AddMessage_messageData_37, logic_uScript_AddMessage_speaker_37);
		local_NPCDialogue_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_37;
		if (logic_uScript_AddMessage_uScript_AddMessage_37.Shown)
		{
			Relay_False_39();
		}
	}

	private void Relay_Connection_38()
	{
	}

	private void Relay_True_39()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.True(out logic_uScriptAct_SetBool_Target_39);
		local_NPCFound_System_Boolean = logic_uScriptAct_SetBool_Target_39;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_39.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_False_39()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.False(out logic_uScriptAct_SetBool_Target_39);
		local_NPCFound_System_Boolean = logic_uScriptAct_SetBool_Target_39;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_39.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_In_42()
	{
		logic_uScriptCon_CompareBool_Bool_42 = external_43;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.In(logic_uScriptCon_CompareBool_Bool_42);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.False;
		if (num)
		{
			Relay_In_46();
		}
		if (flag)
		{
			Relay_In_49();
		}
	}

	private void Relay_Connection_43()
	{
	}

	private void Relay_In_44()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_44 = local_NPCDialogue_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_44.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_44, logic_uScript_RemoveOnScreenMessage_instant_44);
	}

	private void Relay_In_46()
	{
		logic_uScriptCon_CompareBool_Bool_46 = local_NPCFound_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.In(logic_uScriptCon_CompareBool_Bool_46);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.True)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_49()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.Out)
		{
			Relay_In_33();
		}
	}
}
