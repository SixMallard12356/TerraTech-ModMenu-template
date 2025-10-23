using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_Crafting_Tutorial_Init", "")]
[NodePath("Graphs")]
public class SubGraph_Crafting_Tutorial_Init : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public Tank CraftingBaseTech;

		public Tank NPCTech;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SpawnTechData[] external_19 = new SpawnTechData[0];

	private SpawnBlockData[] external_20 = new SpawnBlockData[0];

	private SpawnTechData[] external_49 = new SpawnTechData[0];

	private TankPreset external_24;

	private bool external_56;

	private bool external_67;

	private string external_21 = "";

	private float external_22;

	private Tank external_32;

	private Tank external_58;

	private Tank[] local_45_TankArray = new Tank[0];

	private Tank[] local_9_TankArray = new Tank[0];

	private bool local_BaseSpawned_System_Boolean;

	private bool local_BaseToBuildSet_System_Boolean;

	private GameObject owner_Connection_4;

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_15;

	private GameObject owner_Connection_33;

	private GameObject owner_Connection_44;

	private GameObject owner_Connection_47;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_0 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_0 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_0;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_0 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_0 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_1;

	private bool logic_uScriptCon_CompareBool_True_1 = true;

	private bool logic_uScriptCon_CompareBool_False_1 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_2 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_2;

	private bool logic_uScriptAct_SetBool_Out_2 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_2 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_2 = true;

	private uScript_SpawnBlocksFromData logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_5 = new uScript_SpawnBlocksFromData();

	private SpawnBlockData[] logic_uScript_SpawnBlocksFromData_blockData_5 = new SpawnBlockData[0];

	private GameObject logic_uScript_SpawnBlocksFromData_ownerNode_5;

	private bool logic_uScript_SpawnBlocksFromData_Out_5 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_7 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_7 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_7;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_7 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_7;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_7 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_7 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_7 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_7 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_10 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_10;

	private string logic_uScript_RemoveScenery_positionName_10 = "";

	private float logic_uScript_RemoveScenery_radius_10;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_10 = true;

	private bool logic_uScript_RemoveScenery_Out_10 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_12 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_12 = new Tank[0];

	private int logic_uScript_AccessListTech_index_12;

	private Tank logic_uScript_AccessListTech_value_12;

	private bool logic_uScript_AccessListTech_Out_12 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_13;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_13 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_13 = "BaseSpawned";

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_17 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_17;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_17;

	private bool logic_uScript_LockTech_Out_17 = true;

	private uScript_SetTutorialTechToBuild logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_18 = new uScript_SetTutorialTechToBuild();

	private TankPreset logic_uScript_SetTutorialTechToBuild_completedTechPreset_18;

	private Tank logic_uScript_SetTutorialTechToBuild_tutorialBuildTech_18;

	private bool logic_uScript_SetTutorialTechToBuild_Out_18 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_29;

	private bool logic_uScriptCon_CompareBool_True_29 = true;

	private bool logic_uScriptCon_CompareBool_False_29 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_31 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_31;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_31 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_31 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_34 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_34;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_34 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_36 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_36;

	private bool logic_uScriptAct_SetBool_Out_36 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_36 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_36 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_39 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_39;

	private bool logic_uScriptAct_SetBool_Out_39 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_39 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_39 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_40 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_41 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_42 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_42 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_42;

	private bool logic_uScript_SetTankInvulnerable_Out_42 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_46 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_46 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_46;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_46 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_46;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_46 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_46 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_46 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_46 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_50 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_50 = new Tank[0];

	private int logic_uScript_AccessListTech_index_50;

	private Tank logic_uScript_AccessListTech_value_50;

	private bool logic_uScript_AccessListTech_Out_50 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_51 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_51 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_51;

	private bool logic_uScript_SetTankInvulnerable_Out_51 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_53 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_53 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_53;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_53 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_53 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_54 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_54 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_55;

	private bool logic_uScriptCon_CompareBool_True_55 = true;

	private bool logic_uScriptCon_CompareBool_False_55 = true;

	private uScript_SetTutorialTechToBuild logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_59 = new uScript_SetTutorialTechToBuild();

	private TankPreset logic_uScript_SetTutorialTechToBuild_completedTechPreset_59;

	private Tank logic_uScript_SetTutorialTechToBuild_tutorialBuildTech_59;

	private bool logic_uScript_SetTutorialTechToBuild_Out_59 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_64;

	private bool logic_uScriptCon_CompareBool_True_64 = true;

	private bool logic_uScriptCon_CompareBool_False_64 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_66 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_66;

	private bool logic_uScriptCon_CompareBool_True_66 = true;

	private bool logic_uScriptCon_CompareBool_False_66 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_68;

	private bool logic_uScriptCon_CompareBool_True_68 = true;

	private bool logic_uScriptCon_CompareBool_False_68 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_70 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_70 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_71 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_71 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_72 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_73 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_73 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_73;

	private bool logic_uScript_SetTankHideBlockLimit_Out_73 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_4 || !m_RegisteredForEvents)
		{
			owner_Connection_4 = parentGameObject;
		}
		if (null == owner_Connection_6 || !m_RegisteredForEvents)
		{
			owner_Connection_6 = parentGameObject;
		}
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
		}
		if (null == owner_Connection_15 || !m_RegisteredForEvents)
		{
			owner_Connection_15 = parentGameObject;
			if (null != owner_Connection_15)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_15.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_15.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_14;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_14;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_14;
				}
			}
		}
		if (null == owner_Connection_33 || !m_RegisteredForEvents)
		{
			owner_Connection_33 = parentGameObject;
		}
		if (null == owner_Connection_44 || !m_RegisteredForEvents)
		{
			owner_Connection_44 = parentGameObject;
		}
		if (null == owner_Connection_47 || !m_RegisteredForEvents)
		{
			owner_Connection_47 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_15)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_15.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_15.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_14;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_14;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_14;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_15)
		{
			uScript_SaveLoad component = owner_Connection_15.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_14;
				component.LoadEvent -= Instance_LoadEvent_14;
				component.RestartEvent -= Instance_RestartEvent_14;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_0.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.SetParent(g);
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_5.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_7.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_10.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_12.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_17.SetParent(g);
		logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_18.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_31.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_34.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_36.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_42.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_46.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_50.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_51.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_53.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_54.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.SetParent(g);
		logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_59.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_66.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_70.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_71.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_73.SetParent(g);
		owner_Connection_4 = parentGameObject;
		owner_Connection_6 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_15 = parentGameObject;
		owner_Connection_33 = parentGameObject;
		owner_Connection_44 = parentGameObject;
		owner_Connection_47 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Save_Out += SubGraph_SaveLoadBool_Save_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Load_Out += SubGraph_SaveLoadBool_Load_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_13;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_34.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_42.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_51.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Save_Out -= SubGraph_SaveLoadBool_Save_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Load_Out -= SubGraph_SaveLoadBool_Load_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_13;
	}

	private void Instance_SaveEvent_14(object o, EventArgs e)
	{
		Relay_SaveEvent_14();
	}

	private void Instance_LoadEvent_14(object o, EventArgs e)
	{
		Relay_LoadEvent_14();
	}

	private void Instance_RestartEvent_14(object o, EventArgs e)
	{
		Relay_RestartEvent_14();
	}

	private void SubGraph_SaveLoadBool_Save_Out_13(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = e.boolean;
		local_BaseSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_13;
		Relay_Save_Out_13();
	}

	private void SubGraph_SaveLoadBool_Load_Out_13(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = e.boolean;
		local_BaseSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_13;
		Relay_Load_Out_13();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_13(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = e.boolean;
		local_BaseSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_13;
		Relay_Restart_Out_13();
	}

	[FriendlyName("In", "")]
	public void In([FriendlyName("baseSpawnData", "")] SpawnTechData[] baseSpawnData, [FriendlyName("blockSpawnData", "")] SpawnBlockData[] blockSpawnData, [FriendlyName("NPCSpawnData", "")] SpawnTechData[] NPCSpawnData, [FriendlyName("completedBasePreset", "")] TankPreset completedBasePreset, [FriendlyName("useNPCAsTutorialTech", "")] bool useNPCAsTutorialTech, [FriendlyName("spawnBase", "")] bool spawnBase, [FriendlyName("basePosition", "")] string basePosition, [FriendlyName("clearSceneryRadius", "")] float clearSceneryRadius, [FriendlyName("CraftingBaseTech", "")] ref Tank CraftingBaseTech, [FriendlyName("NPCTech", "")] ref Tank NPCTech)
	{
		external_19 = baseSpawnData;
		external_20 = blockSpawnData;
		external_49 = NPCSpawnData;
		external_24 = completedBasePreset;
		external_56 = useNPCAsTutorialTech;
		external_67 = spawnBase;
		external_21 = basePosition;
		external_22 = clearSceneryRadius;
		external_32 = CraftingBaseTech;
		external_58 = NPCTech;
		Relay_In_1();
	}

	private void Relay_InitialSpawn_0()
	{
		int num = 0;
		Array array = external_19;
		if (logic_uScript_SpawnTechsFromData_spawnData_0.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_0, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_0, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_0 = owner_Connection_4;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_0.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_0, logic_uScript_SpawnTechsFromData_ownerNode_0, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_0);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_0.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_In_1()
	{
		logic_uScriptCon_CompareBool_Bool_1 = local_BaseSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1.In(logic_uScriptCon_CompareBool_Bool_1);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1.False;
		if (num)
		{
			Relay_In_64();
		}
		if (flag)
		{
			Relay_True_2();
		}
	}

	private void Relay_True_2()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.True(out logic_uScriptAct_SetBool_Target_2);
		local_BaseSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_2;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_2.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_False_2()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.False(out logic_uScriptAct_SetBool_Target_2);
		local_BaseSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_2;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_2.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_In_5()
	{
		int num = 0;
		Array array = external_20;
		if (logic_uScript_SpawnBlocksFromData_blockData_5.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnBlocksFromData_blockData_5, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnBlocksFromData_blockData_5, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnBlocksFromData_ownerNode_5 = owner_Connection_6;
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_5.In(logic_uScript_SpawnBlocksFromData_blockData_5, logic_uScript_SpawnBlocksFromData_ownerNode_5);
		if (logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_5.Out)
		{
			Relay_InitialSpawn_53();
		}
	}

	private void Relay_In_7()
	{
		int num = 0;
		Array array = external_19;
		if (logic_uScript_GetAndCheckTechs_techData_7.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_7, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_7, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_7 = owner_Connection_8;
		int num2 = 0;
		Array array2 = local_9_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_7.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_7, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_7, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_7 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_7.In(logic_uScript_GetAndCheckTechs_techData_7, logic_uScript_GetAndCheckTechs_ownerNode_7, ref logic_uScript_GetAndCheckTechs_techs_7);
		local_9_TankArray = logic_uScript_GetAndCheckTechs_techs_7;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_7.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_7.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_7.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_12();
		}
		if (someAlive)
		{
			Relay_AtIndex_12();
		}
		if (allDead)
		{
			Relay_In_40();
		}
	}

	private void Relay_In_10()
	{
		logic_uScript_RemoveScenery_ownerNode_10 = owner_Connection_11;
		logic_uScript_RemoveScenery_positionName_10 = external_21;
		logic_uScript_RemoveScenery_radius_10 = external_22;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_10.In(logic_uScript_RemoveScenery_ownerNode_10, logic_uScript_RemoveScenery_positionName_10, logic_uScript_RemoveScenery_radius_10, logic_uScript_RemoveScenery_preventChunksSpawning_10);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_10.Out)
		{
			Relay_In_64();
		}
	}

	private void Relay_AtIndex_12()
	{
		int num = 0;
		Array array = local_9_TankArray;
		if (logic_uScript_AccessListTech_techList_12.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_12, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_12, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_12.AtIndex(ref logic_uScript_AccessListTech_techList_12, logic_uScript_AccessListTech_index_12, out logic_uScript_AccessListTech_value_12);
		local_9_TankArray = logic_uScript_AccessListTech_techList_12;
		external_32 = logic_uScript_AccessListTech_value_12;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_12.Out)
		{
			Relay_In_17();
		}
	}

	private void Relay_Save_Out_13()
	{
	}

	private void Relay_Load_Out_13()
	{
	}

	private void Relay_Restart_Out_13()
	{
		Relay_False_39();
	}

	private void Relay_Save_13()
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = local_BaseSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_13 = local_BaseSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Save(ref logic_SubGraph_SaveLoadBool_boolean_13, logic_SubGraph_SaveLoadBool_boolAsVariable_13, logic_SubGraph_SaveLoadBool_uniqueID_13);
	}

	private void Relay_Load_13()
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = local_BaseSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_13 = local_BaseSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Load(ref logic_SubGraph_SaveLoadBool_boolean_13, logic_SubGraph_SaveLoadBool_boolAsVariable_13, logic_SubGraph_SaveLoadBool_uniqueID_13);
	}

	private void Relay_Set_True_13()
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = local_BaseSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_13 = local_BaseSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_13, logic_SubGraph_SaveLoadBool_boolAsVariable_13, logic_SubGraph_SaveLoadBool_uniqueID_13);
	}

	private void Relay_Set_False_13()
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = local_BaseSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_13 = local_BaseSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_13, logic_SubGraph_SaveLoadBool_boolAsVariable_13, logic_SubGraph_SaveLoadBool_uniqueID_13);
	}

	private void Relay_SaveEvent_14()
	{
		Relay_Save_13();
	}

	private void Relay_LoadEvent_14()
	{
		Relay_Load_13();
	}

	private void Relay_RestartEvent_14()
	{
		Relay_Set_False_13();
	}

	private void Relay_In_17()
	{
		logic_uScript_LockTech_tech_17 = external_32;
		logic_uScript_LockTech_uScript_LockTech_17.In(logic_uScript_LockTech_tech_17, logic_uScript_LockTech_lockType_17);
		if (logic_uScript_LockTech_uScript_LockTech_17.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_18()
	{
		logic_uScript_SetTutorialTechToBuild_completedTechPreset_18 = external_24;
		logic_uScript_SetTutorialTechToBuild_tutorialBuildTech_18 = external_32;
		logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_18.In(logic_uScript_SetTutorialTechToBuild_completedTechPreset_18, logic_uScript_SetTutorialTechToBuild_tutorialBuildTech_18);
		if (logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_18.Out)
		{
			Relay_True_36();
		}
	}

	private void Relay_Connection_19()
	{
	}

	private void Relay_Connection_20()
	{
	}

	private void Relay_Connection_21()
	{
	}

	private void Relay_Connection_22()
	{
	}

	private void Relay_Connection_24()
	{
	}

	private void Relay_Connection_25()
	{
		if (this.Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.CraftingBaseTech = external_32;
			e.NPCTech = external_58;
			this.Out(this, e);
		}
	}

	private void Relay_Connection_26()
	{
	}

	private void Relay_In_29()
	{
		logic_uScriptCon_CompareBool_Bool_29 = local_BaseToBuildSet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.In(logic_uScriptCon_CompareBool_Bool_29);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.False;
		if (num)
		{
			Relay_In_34();
		}
		if (flag)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_31()
	{
		logic_uScript_SetCustomRadarTeamID_tech_31 = external_32;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_31.In(logic_uScript_SetCustomRadarTeamID_tech_31, logic_uScript_SetCustomRadarTeamID_radarTeamID_31);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_31.Out)
		{
			Relay_In_73();
		}
	}

	private void Relay_Connection_32()
	{
	}

	private void Relay_In_34()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_34 = owner_Connection_33;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_34.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_34);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_34.Out)
		{
			Relay_Connection_25();
		}
	}

	private void Relay_True_36()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_36.True(out logic_uScriptAct_SetBool_Target_36);
		local_BaseToBuildSet_System_Boolean = logic_uScriptAct_SetBool_Target_36;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_36.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_False_36()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_36.False(out logic_uScriptAct_SetBool_Target_36);
		local_BaseToBuildSet_System_Boolean = logic_uScriptAct_SetBool_Target_36;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_36.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_True_39()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.True(out logic_uScriptAct_SetBool_Target_39);
		local_BaseToBuildSet_System_Boolean = logic_uScriptAct_SetBool_Target_39;
	}

	private void Relay_False_39()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.False(out logic_uScriptAct_SetBool_Target_39);
		local_BaseToBuildSet_System_Boolean = logic_uScriptAct_SetBool_Target_39;
	}

	private void Relay_In_40()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_40.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_41()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_42()
	{
		logic_uScript_SetTankInvulnerable_tank_42 = external_32;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_42.In(logic_uScript_SetTankInvulnerable_invulnerable_42, logic_uScript_SetTankInvulnerable_tank_42);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_42.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_46()
	{
		int num = 0;
		Array array = external_49;
		if (logic_uScript_GetAndCheckTechs_techData_46.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_46, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_46, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_46 = owner_Connection_44;
		int num2 = 0;
		Array array2 = local_45_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_46.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_46, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_46, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_46 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_46.In(logic_uScript_GetAndCheckTechs_techData_46, logic_uScript_GetAndCheckTechs_ownerNode_46, ref logic_uScript_GetAndCheckTechs_techs_46);
		local_45_TankArray = logic_uScript_GetAndCheckTechs_techs_46;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_46.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_46.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_46.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_50();
		}
		if (someAlive)
		{
			Relay_AtIndex_50();
		}
		if (allDead)
		{
			Relay_In_54();
		}
	}

	private void Relay_Connection_49()
	{
	}

	private void Relay_AtIndex_50()
	{
		int num = 0;
		Array array = local_45_TankArray;
		if (logic_uScript_AccessListTech_techList_50.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_50, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_50, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_50.AtIndex(ref logic_uScript_AccessListTech_techList_50, logic_uScript_AccessListTech_index_50, out logic_uScript_AccessListTech_value_50);
		local_45_TankArray = logic_uScript_AccessListTech_techList_50;
		external_58 = logic_uScript_AccessListTech_value_50;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_50.Out)
		{
			Relay_In_51();
		}
	}

	private void Relay_In_51()
	{
		logic_uScript_SetTankInvulnerable_tank_51 = external_58;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_51.In(logic_uScript_SetTankInvulnerable_invulnerable_51, logic_uScript_SetTankInvulnerable_tank_51);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_51.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_InitialSpawn_53()
	{
		int num = 0;
		Array array = external_49;
		if (logic_uScript_SpawnTechsFromData_spawnData_53.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_53, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_53, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_53 = owner_Connection_47;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_53.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_53, logic_uScript_SpawnTechsFromData_ownerNode_53, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_53);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_53.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_54()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_54.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_54.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_55()
	{
		logic_uScriptCon_CompareBool_Bool_55 = external_56;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.In(logic_uScriptCon_CompareBool_Bool_55);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.False;
		if (num)
		{
			Relay_In_59();
		}
		if (flag)
		{
			Relay_In_66();
		}
	}

	private void Relay_Connection_56()
	{
	}

	private void Relay_Connection_58()
	{
	}

	private void Relay_In_59()
	{
		logic_uScript_SetTutorialTechToBuild_completedTechPreset_59 = external_24;
		logic_uScript_SetTutorialTechToBuild_tutorialBuildTech_59 = external_58;
		logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_59.In(logic_uScript_SetTutorialTechToBuild_completedTechPreset_59, logic_uScript_SetTutorialTechToBuild_tutorialBuildTech_59);
		if (logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_59.Out)
		{
			Relay_True_36();
		}
	}

	private void Relay_In_64()
	{
		logic_uScriptCon_CompareBool_Bool_64 = external_67;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64.In(logic_uScriptCon_CompareBool_Bool_64);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64.False;
		if (num)
		{
			Relay_In_7();
		}
		if (flag)
		{
			Relay_In_71();
		}
	}

	private void Relay_In_66()
	{
		logic_uScriptCon_CompareBool_Bool_66 = external_67;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_66.In(logic_uScriptCon_CompareBool_Bool_66);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_66.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_66.False;
		if (num)
		{
			Relay_In_18();
		}
		if (flag)
		{
			Relay_In_72();
		}
	}

	private void Relay_Connection_67()
	{
	}

	private void Relay_In_68()
	{
		logic_uScriptCon_CompareBool_Bool_68 = external_67;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.In(logic_uScriptCon_CompareBool_Bool_68);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.False;
		if (num)
		{
			Relay_InitialSpawn_0();
		}
		if (flag)
		{
			Relay_In_70();
		}
	}

	private void Relay_In_70()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_70.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_70.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_In_71()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_71.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_71.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_In_72()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72.Out)
		{
			Relay_True_36();
		}
	}

	private void Relay_In_73()
	{
		logic_uScript_SetTankHideBlockLimit_tech_73 = external_32;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_73.In(logic_uScript_SetTankHideBlockLimit_hidden_73, logic_uScript_SetTankHideBlockLimit_tech_73);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_73.Out)
		{
			Relay_In_46();
		}
	}
}
