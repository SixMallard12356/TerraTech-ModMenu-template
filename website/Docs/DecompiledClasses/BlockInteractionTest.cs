using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class BlockInteractionTest : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public BlockTypes interactableBlockType;

	private Tank[] local_21_TankArray = new Tank[0];

	private Tank local_23_Tank;

	private BlockTypes local_24_BlockTypes = BlockTypes.GCTerminal_212;

	private TankBlock local_25_TankBlock;

	private BlockTypes local_29_BlockTypes;

	private bool local_Init_System_Boolean;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	public uScript_AddMessage.MessageData msgBlockClicked;

	public SpawnTechData[] techData = new SpawnTechData[0];

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_12;

	private GameObject owner_Connection_16;

	private GameObject owner_Connection_22;

	private GameObject owner_Connection_26;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_2 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_2;

	private bool logic_uScriptAct_SetBool_Out_2 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_2 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_2 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_3 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_3;

	private bool logic_uScriptCon_CompareBool_True_3 = true;

	private bool logic_uScriptCon_CompareBool_False_3 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_4 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_4 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_4;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_4 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_4 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_8 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_8;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_8;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_8;

	private bool logic_uScript_AddMessage_Out_8 = true;

	private bool logic_uScript_AddMessage_Shown_8 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_11;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_11 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_11 = "Init";

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_15 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_15;

	private string logic_uScript_RemoveScenery_positionName_15 = "NPCPos";

	private float logic_uScript_RemoveScenery_radius_15 = 25f;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_15 = true;

	private bool logic_uScript_RemoveScenery_Out_15 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_18 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_18 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_18;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_18 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_18;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_18 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_18 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_18 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_18 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_19 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_19;

	private BlockTypes logic_uScript_GetTankBlock_blockType_19;

	private TankBlock logic_uScript_GetTankBlock_Return_19;

	private bool logic_uScript_GetTankBlock_Out_19 = true;

	private bool logic_uScript_GetTankBlock_Returned_19 = true;

	private bool logic_uScript_GetTankBlock_NotFound_19 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_20 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_20 = new Tank[0];

	private int logic_uScript_AccessListTech_index_20;

	private Tank logic_uScript_AccessListTech_value_20;

	private bool logic_uScript_AccessListTech_Out_20 = true;

	private uScript_CompareBlockTypes logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_28 = new uScript_CompareBlockTypes();

	private BlockTypes logic_uScript_CompareBlockTypes_A_28;

	private BlockTypes logic_uScript_CompareBlockTypes_B_28;

	private bool logic_uScript_CompareBlockTypes_EqualTo_28 = true;

	private bool logic_uScript_CompareBlockTypes_NotEqualTo_28 = true;

	private BlockTypes event_UnityEngine_GameObject_BlockID_17;

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
		if (null == owner_Connection_6 || !m_RegisteredForEvents)
		{
			owner_Connection_6 = parentGameObject;
		}
		if (null == owner_Connection_12 || !m_RegisteredForEvents)
		{
			owner_Connection_12 = parentGameObject;
			if (null != owner_Connection_12)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_12.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_12.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_13;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_13;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_13;
				}
			}
		}
		if (null == owner_Connection_16 || !m_RegisteredForEvents)
		{
			owner_Connection_16 = parentGameObject;
		}
		if (null == owner_Connection_22 || !m_RegisteredForEvents)
		{
			owner_Connection_22 = parentGameObject;
		}
		if (!(null == owner_Connection_26) && m_RegisteredForEvents)
		{
			return;
		}
		owner_Connection_26 = parentGameObject;
		if (null != owner_Connection_26)
		{
			uScript_GetBlockMouseDown uScript_GetBlockMouseDown2 = owner_Connection_26.GetComponent<uScript_GetBlockMouseDown>();
			if (null == uScript_GetBlockMouseDown2)
			{
				uScript_GetBlockMouseDown2 = owner_Connection_26.AddComponent<uScript_GetBlockMouseDown>();
			}
			if (null != uScript_GetBlockMouseDown2)
			{
				uScript_GetBlockMouseDown2.OnBlockMouseDown += Instance_OnBlockMouseDown_17;
			}
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
		if (!m_RegisteredForEvents && null != owner_Connection_12)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_12.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_12.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_13;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_13;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_13;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_26)
		{
			uScript_GetBlockMouseDown uScript_GetBlockMouseDown2 = owner_Connection_26.GetComponent<uScript_GetBlockMouseDown>();
			if (null == uScript_GetBlockMouseDown2)
			{
				uScript_GetBlockMouseDown2 = owner_Connection_26.AddComponent<uScript_GetBlockMouseDown>();
			}
			if (null != uScript_GetBlockMouseDown2)
			{
				uScript_GetBlockMouseDown2.OnBlockMouseDown += Instance_OnBlockMouseDown_17;
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
		if (null != owner_Connection_12)
		{
			uScript_SaveLoad component2 = owner_Connection_12.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_13;
				component2.LoadEvent -= Instance_LoadEvent_13;
				component2.RestartEvent -= Instance_RestartEvent_13;
			}
		}
		if (null != owner_Connection_26)
		{
			uScript_GetBlockMouseDown component3 = owner_Connection_26.GetComponent<uScript_GetBlockMouseDown>();
			if (null != component3)
			{
				component3.OnBlockMouseDown -= Instance_OnBlockMouseDown_17;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_3.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_4.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_8.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_15.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_18.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_19.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_20.SetParent(g);
		logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_28.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_6 = parentGameObject;
		owner_Connection_12 = parentGameObject;
		owner_Connection_16 = parentGameObject;
		owner_Connection_22 = parentGameObject;
		owner_Connection_26 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Save_Out += SubGraph_SaveLoadBool_Save_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Load_Out += SubGraph_SaveLoadBool_Load_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_11;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_AddMessage_uScript_AddMessage_8.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Save_Out -= SubGraph_SaveLoadBool_Save_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Load_Out -= SubGraph_SaveLoadBool_Load_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_11;
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

	private void Instance_SaveEvent_13(object o, EventArgs e)
	{
		Relay_SaveEvent_13();
	}

	private void Instance_LoadEvent_13(object o, EventArgs e)
	{
		Relay_LoadEvent_13();
	}

	private void Instance_RestartEvent_13(object o, EventArgs e)
	{
		Relay_RestartEvent_13();
	}

	private void Instance_OnBlockMouseDown_17(object o, uScript_GetBlockMouseDown.BlockMouseDownEventArgs e)
	{
		event_UnityEngine_GameObject_BlockID_17 = e.BlockID;
		Relay_OnBlockMouseDown_17();
	}

	private void SubGraph_SaveLoadBool_Save_Out_11(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_11;
		Relay_Save_Out_11();
	}

	private void SubGraph_SaveLoadBool_Load_Out_11(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_11;
		Relay_Load_Out_11();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_11(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_11;
		Relay_Restart_Out_11();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_3();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
	}

	private void Relay_True_2()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.True(out logic_uScriptAct_SetBool_Target_2);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_2;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_2.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_False_2()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_2.False(out logic_uScriptAct_SetBool_Target_2);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_2;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_2.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_3()
	{
		logic_uScriptCon_CompareBool_Bool_3 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_3.In(logic_uScriptCon_CompareBool_Bool_3);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_3.False)
		{
			Relay_True_2();
		}
	}

	private void Relay_InitialSpawn_4()
	{
		int num = 0;
		Array array = techData;
		if (logic_uScript_SpawnTechsFromData_spawnData_4.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_4, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_4, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_4 = owner_Connection_6;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_4.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_4, logic_uScript_SpawnTechsFromData_ownerNode_4, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_4);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_4.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_In_8()
	{
		logic_uScript_AddMessage_messageData_8 = msgBlockClicked;
		logic_uScript_AddMessage_speaker_8 = messageSpeaker;
		logic_uScript_AddMessage_Return_8 = logic_uScript_AddMessage_uScript_AddMessage_8.In(logic_uScript_AddMessage_messageData_8, logic_uScript_AddMessage_speaker_8);
	}

	private void Relay_Save_Out_11()
	{
	}

	private void Relay_Load_Out_11()
	{
	}

	private void Relay_Restart_Out_11()
	{
	}

	private void Relay_Save_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Save(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Load_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Load(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Set_True_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Set_False_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_SaveEvent_13()
	{
		Relay_Save_11();
	}

	private void Relay_LoadEvent_13()
	{
		Relay_Load_11();
	}

	private void Relay_RestartEvent_13()
	{
		Relay_Set_False_11();
	}

	private void Relay_In_15()
	{
		logic_uScript_RemoveScenery_ownerNode_15 = owner_Connection_16;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_15.In(logic_uScript_RemoveScenery_ownerNode_15, logic_uScript_RemoveScenery_positionName_15, logic_uScript_RemoveScenery_radius_15, logic_uScript_RemoveScenery_preventChunksSpawning_15);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_15.Out)
		{
			Relay_InitialSpawn_4();
		}
	}

	private void Relay_OnBlockMouseDown_17()
	{
		local_29_BlockTypes = event_UnityEngine_GameObject_BlockID_17;
		Relay_In_28();
	}

	private void Relay_In_18()
	{
		int num = 0;
		Array array = techData;
		if (logic_uScript_GetAndCheckTechs_techData_18.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_18, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_18, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_18 = owner_Connection_22;
		int num2 = 0;
		Array array2 = local_21_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_18.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_18, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_18, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_18 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_18.In(logic_uScript_GetAndCheckTechs_techData_18, logic_uScript_GetAndCheckTechs_ownerNode_18, ref logic_uScript_GetAndCheckTechs_techs_18);
		local_21_TankArray = logic_uScript_GetAndCheckTechs_techs_18;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_18.SomeAlive)
		{
			Relay_AtIndex_20();
		}
	}

	private void Relay_In_19()
	{
		logic_uScript_GetTankBlock_tank_19 = local_23_Tank;
		logic_uScript_GetTankBlock_blockType_19 = local_24_BlockTypes;
		logic_uScript_GetTankBlock_Return_19 = logic_uScript_GetTankBlock_uScript_GetTankBlock_19.In(logic_uScript_GetTankBlock_tank_19, logic_uScript_GetTankBlock_blockType_19);
		local_25_TankBlock = logic_uScript_GetTankBlock_Return_19;
	}

	private void Relay_AtIndex_20()
	{
		int num = 0;
		Array array = local_21_TankArray;
		if (logic_uScript_AccessListTech_techList_20.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_20, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_20, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_20.AtIndex(ref logic_uScript_AccessListTech_techList_20, logic_uScript_AccessListTech_index_20, out logic_uScript_AccessListTech_value_20);
		local_21_TankArray = logic_uScript_AccessListTech_techList_20;
		local_23_Tank = logic_uScript_AccessListTech_value_20;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_20.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_In_28()
	{
		logic_uScript_CompareBlockTypes_A_28 = local_29_BlockTypes;
		logic_uScript_CompareBlockTypes_B_28 = interactableBlockType;
		logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_28.In(logic_uScript_CompareBlockTypes_A_28, logic_uScript_CompareBlockTypes_B_28);
		if (logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_28.EqualTo)
		{
			Relay_In_8();
		}
	}
}
