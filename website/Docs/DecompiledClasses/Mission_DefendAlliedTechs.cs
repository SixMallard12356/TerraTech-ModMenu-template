using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_DefendAlliedTechs : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool autoTriggerNextWave;

	public float autoTriggerNextWaveTime = 30f;

	private int local_62_System_Int32;

	private Color local_65_UnityEngine_Color = new Color(0f, 0f, 0f, 1f);

	private Color local_72_UnityEngine_Color = new Color(0f, 0f, 0f, 1f);

	private bool local_AlliesSpawned_System_Boolean;

	private Color[] local_base_list_UnityEngine_ColorArray = new Color[0];

	private Color[] local_main_list_UnityEngine_ColorArray = new Color[0];

	private bool local_StartWaves_System_Boolean;

	private Tank[] local_TechsToDefend_TankArray = new Tank[0];

	private int local_WaveCounter_System_Int32 = 1;

	public LocalisedString[] msgLose = new LocalisedString[0];

	public LocalisedString[] msgWin = new LocalisedString[0];

	public int numWaves;

	public SpawnTechData[] techsToDefend = new SpawnTechData[0];

	public float timeBeforeNextWave = 3f;

	public SpawnTechData[] wave01Techs = new SpawnTechData[0];

	public SpawnTechData[] wave02Techs = new SpawnTechData[0];

	public SpawnTechData[] wave03Techs = new SpawnTechData[0];

	public SpawnTechData[] wave04Techs = new SpawnTechData[0];

	public SpawnTechData[] wave05Techs = new SpawnTechData[0];

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_76;

	private GameObject owner_Connection_80;

	private GameObject owner_Connection_177;

	private GameObject owner_Connection_181;

	private uScript_InRangeOfAtLeastOneTech logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_2 = new uScript_InRangeOfAtLeastOneTech();

	private Tank[] logic_uScript_InRangeOfAtLeastOneTech_techs_2 = new Tank[0];

	private float logic_uScript_InRangeOfAtLeastOneTech_range_2 = 15f;

	private bool logic_uScript_InRangeOfAtLeastOneTech_Out_2 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_InRange_2 = true;

	private bool logic_uScript_InRangeOfAtLeastOneTech_OutOfRange_2 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_4 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_4;

	private bool logic_uScriptAct_SetBool_Out_4 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_4 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_4 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_6 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_6;

	private bool logic_uScriptCon_CompareBool_True_6 = true;

	private bool logic_uScriptCon_CompareBool_False_6 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_8 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_8 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_8 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_8;

	private string logic_uScript_AddOnScreenMessage_tag_8 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_8;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_8;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_8;

	private bool logic_uScript_AddOnScreenMessage_Out_8 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_8 = true;

	private SubGraph_SpawnEnemyWave logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_11 = new SubGraph_SpawnEnemyWave();

	private SpawnTechData[] logic_SubGraph_SpawnEnemyWave_techsInWave_11 = new SpawnTechData[0];

	private float logic_SubGraph_SpawnEnemyWave_timeBeforeNextWave_11;

	private bool logic_SubGraph_SpawnEnemyWave_autoTriggerNextWave__11;

	private float logic_SubGraph_SpawnEnemyWave_timeBeforeAutoTrigger_11;

	private SubGraph_SpawnEnemyWave logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_13 = new SubGraph_SpawnEnemyWave();

	private SpawnTechData[] logic_SubGraph_SpawnEnemyWave_techsInWave_13 = new SpawnTechData[0];

	private float logic_SubGraph_SpawnEnemyWave_timeBeforeNextWave_13;

	private bool logic_SubGraph_SpawnEnemyWave_autoTriggerNextWave__13;

	private float logic_SubGraph_SpawnEnemyWave_timeBeforeAutoTrigger_13;

	private SubGraph_SpawnEnemyWave logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_14 = new SubGraph_SpawnEnemyWave();

	private SpawnTechData[] logic_SubGraph_SpawnEnemyWave_techsInWave_14 = new SpawnTechData[0];

	private float logic_SubGraph_SpawnEnemyWave_timeBeforeNextWave_14;

	private bool logic_SubGraph_SpawnEnemyWave_autoTriggerNextWave__14;

	private float logic_SubGraph_SpawnEnemyWave_timeBeforeAutoTrigger_14;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_17 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_17 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_17 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_17;

	private string logic_uScript_AddOnScreenMessage_tag_17 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_17;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_17;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_17;

	private bool logic_uScript_AddOnScreenMessage_Out_17 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_17 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_19;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_21 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_21;

	private int logic_uScriptCon_CompareInt_B_21;

	private bool logic_uScriptCon_CompareInt_GreaterThan_21 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_21 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_21 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_21 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_21 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_21 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_25 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_25;

	private int logic_uScriptAct_AddInt_v2_B_25 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_25;

	private float logic_uScriptAct_AddInt_v2_FloatResult_25;

	private bool logic_uScriptAct_AddInt_v2_Out_25 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_26 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_26;

	private int logic_uScriptAct_AddInt_v2_B_26 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_26;

	private float logic_uScriptAct_AddInt_v2_FloatResult_26;

	private bool logic_uScriptAct_AddInt_v2_Out_26 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_28 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_28;

	private int logic_uScriptAct_AddInt_v2_B_28 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_28;

	private float logic_uScriptAct_AddInt_v2_FloatResult_28;

	private bool logic_uScriptAct_AddInt_v2_Out_28 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_30 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_30;

	private int logic_uScriptAct_AddInt_v2_B_30 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_30;

	private float logic_uScriptAct_AddInt_v2_FloatResult_30;

	private bool logic_uScriptAct_AddInt_v2_Out_30 = true;

	private SubGraph_SpawnEnemyWave logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_32 = new SubGraph_SpawnEnemyWave();

	private SpawnTechData[] logic_SubGraph_SpawnEnemyWave_techsInWave_32 = new SpawnTechData[0];

	private float logic_SubGraph_SpawnEnemyWave_timeBeforeNextWave_32;

	private bool logic_SubGraph_SpawnEnemyWave_autoTriggerNextWave__32;

	private float logic_SubGraph_SpawnEnemyWave_timeBeforeAutoTrigger_32;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_34 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_34;

	private int logic_uScriptAct_AddInt_v2_B_34 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_34;

	private float logic_uScriptAct_AddInt_v2_FloatResult_34;

	private bool logic_uScriptAct_AddInt_v2_Out_34 = true;

	private SubGraph_SpawnEnemyWave logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_36 = new SubGraph_SpawnEnemyWave();

	private SpawnTechData[] logic_SubGraph_SpawnEnemyWave_techsInWave_36 = new SpawnTechData[0];

	private float logic_SubGraph_SpawnEnemyWave_timeBeforeNextWave_36;

	private bool logic_SubGraph_SpawnEnemyWave_autoTriggerNextWave__36;

	private float logic_SubGraph_SpawnEnemyWave_timeBeforeAutoTrigger_36;

	private uScriptAct_SetFloat logic_uScriptAct_SetFloat_uScriptAct_SetFloat_54 = new uScriptAct_SetFloat();

	private float logic_uScriptAct_SetFloat_Value_54;

	private float logic_uScriptAct_SetFloat_Target_54;

	private bool logic_uScriptAct_SetFloat_Out_54 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_55 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_55;

	private bool logic_uScriptAct_SetBool_Out_55 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_55 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_55 = true;

	private uScriptAct_ForEachListColor logic_uScriptAct_ForEachListColor_uScriptAct_ForEachListColor_57 = new uScriptAct_ForEachListColor();

	private Color[] logic_uScriptAct_ForEachListColor_List_57 = new Color[0];

	private Color logic_uScriptAct_ForEachListColor_Value_57;

	private int logic_uScriptAct_ForEachListColor_currentIndex_57;

	private bool logic_uScriptAct_ForEachListColor_Immediate_57 = true;

	private bool logic_uScriptAct_ForEachListColor_Done_57 = true;

	private bool logic_uScriptAct_ForEachListColor_Iteration_57 = true;

	private uScriptAct_GetListSizeColor logic_uScriptAct_GetListSizeColor_uScriptAct_GetListSizeColor_58 = new uScriptAct_GetListSizeColor();

	private Color[] logic_uScriptAct_GetListSizeColor_List_58 = new Color[0];

	private int logic_uScriptAct_GetListSizeColor_ListSize_58;

	private bool logic_uScriptAct_GetListSizeColor_Out_58 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_60 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_60;

	private int logic_uScriptCon_CompareInt_B_60;

	private bool logic_uScriptCon_CompareInt_GreaterThan_60 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_60 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_60 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_60 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_60 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_60 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_67 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_67;

	private int logic_uScriptAct_AddInt_v2_B_67;

	private int logic_uScriptAct_AddInt_v2_IntResult_67;

	private float logic_uScriptAct_AddInt_v2_FloatResult_67;

	private bool logic_uScriptAct_AddInt_v2_Out_67 = true;

	private uScriptAct_SetColor logic_uScriptAct_SetColor_uScriptAct_SetColor_68 = new uScriptAct_SetColor();

	private Color logic_uScriptAct_SetColor_Value_68 = Color.black;

	private Color logic_uScriptAct_SetColor_TargetColor_68;

	private bool logic_uScriptAct_SetColor_Out_68 = true;

	private uScriptAct_AccessListColor logic_uScriptAct_AccessListColor_uScriptAct_AccessListColor_69 = new uScriptAct_AccessListColor();

	private Color[] logic_uScriptAct_AccessListColor_List_69 = new Color[0];

	private int logic_uScriptAct_AccessListColor_Index_69;

	private Color logic_uScriptAct_AccessListColor_Value_69;

	private bool logic_uScriptAct_AccessListColor_Out_69 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_78 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_78 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_78;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_78 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_78 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_79 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_79 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_79;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_79 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_79;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_79 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_79 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_79 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_79 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_82 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_82;

	private bool logic_uScriptAct_SetBool_Out_82 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_82 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_82 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_83;

	private bool logic_uScriptCon_CompareBool_True_83 = true;

	private bool logic_uScriptCon_CompareBool_False_83 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_176 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_176;

	private bool logic_uScript_FinishEncounter_Out_176 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_180 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_180;

	private bool logic_uScript_FinishEncounter_Out_180 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
		}
		if (null == owner_Connection_76 || !m_RegisteredForEvents)
		{
			owner_Connection_76 = parentGameObject;
			if (null != owner_Connection_76)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_76.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_76.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_77;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_77;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_77;
				}
			}
		}
		if (null == owner_Connection_80 || !m_RegisteredForEvents)
		{
			owner_Connection_80 = parentGameObject;
		}
		if (null == owner_Connection_177 || !m_RegisteredForEvents)
		{
			owner_Connection_177 = parentGameObject;
		}
		if (null == owner_Connection_181 || !m_RegisteredForEvents)
		{
			owner_Connection_181 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_76)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_76.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_76.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_77;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_77;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_77;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_76)
		{
			uScript_EncounterUpdate component = owner_Connection_76.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_77;
				component.OnSuspend -= Instance_OnSuspend_77;
				component.OnResume -= Instance_OnResume_77;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_2.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_6.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_8.SetParent(g);
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_11.SetParent(g);
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_13.SetParent(g);
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_14.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_17.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_21.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_25.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_26.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_28.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_30.SetParent(g);
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_32.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_34.SetParent(g);
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_36.SetParent(g);
		logic_uScriptAct_SetFloat_uScriptAct_SetFloat_54.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_55.SetParent(g);
		logic_uScriptAct_ForEachListColor_uScriptAct_ForEachListColor_57.SetParent(g);
		logic_uScriptAct_GetListSizeColor_uScriptAct_GetListSizeColor_58.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_60.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_67.SetParent(g);
		logic_uScriptAct_SetColor_uScriptAct_SetColor_68.SetParent(g);
		logic_uScriptAct_AccessListColor_uScriptAct_AccessListColor_69.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_78.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_79.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_176.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_180.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_76 = parentGameObject;
		owner_Connection_80 = parentGameObject;
		owner_Connection_177 = parentGameObject;
		owner_Connection_181 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_11.Awake();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_13.Awake();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_14.Awake();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_32.Awake();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_36.Awake();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_11.Out += SubGraph_SpawnEnemyWave_Out_11;
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_13.Out += SubGraph_SpawnEnemyWave_Out_13;
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_14.Out += SubGraph_SpawnEnemyWave_Out_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.Output1 += uScriptCon_ManualSwitch_Output1_19;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.Output2 += uScriptCon_ManualSwitch_Output2_19;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.Output3 += uScriptCon_ManualSwitch_Output3_19;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.Output4 += uScriptCon_ManualSwitch_Output4_19;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.Output5 += uScriptCon_ManualSwitch_Output5_19;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.Output6 += uScriptCon_ManualSwitch_Output6_19;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.Output7 += uScriptCon_ManualSwitch_Output7_19;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.Output8 += uScriptCon_ManualSwitch_Output8_19;
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_32.Out += SubGraph_SpawnEnemyWave_Out_32;
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_36.Out += SubGraph_SpawnEnemyWave_Out_36;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_11.Start();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_13.Start();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_14.Start();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_32.Start();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_36.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_11.OnEnable();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_13.OnEnable();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_14.OnEnable();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_32.OnEnable();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_36.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_2.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_8.OnDisable();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_11.OnDisable();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_13.OnDisable();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_14.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_17.OnDisable();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_32.OnDisable();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_36.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_11.Update();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_13.Update();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_14.Update();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_32.Update();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_36.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_11.OnDestroy();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_13.OnDestroy();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_14.OnDestroy();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_32.OnDestroy();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_36.OnDestroy();
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_11.Out -= SubGraph_SpawnEnemyWave_Out_11;
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_13.Out -= SubGraph_SpawnEnemyWave_Out_13;
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_14.Out -= SubGraph_SpawnEnemyWave_Out_14;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.Output1 -= uScriptCon_ManualSwitch_Output1_19;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.Output2 -= uScriptCon_ManualSwitch_Output2_19;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.Output3 -= uScriptCon_ManualSwitch_Output3_19;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.Output4 -= uScriptCon_ManualSwitch_Output4_19;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.Output5 -= uScriptCon_ManualSwitch_Output5_19;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.Output6 -= uScriptCon_ManualSwitch_Output6_19;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.Output7 -= uScriptCon_ManualSwitch_Output7_19;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.Output8 -= uScriptCon_ManualSwitch_Output8_19;
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_32.Out -= SubGraph_SpawnEnemyWave_Out_32;
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_36.Out -= SubGraph_SpawnEnemyWave_Out_36;
	}

	private void Instance_OnUpdate_77(object o, EventArgs e)
	{
		Relay_OnUpdate_77();
	}

	private void Instance_OnSuspend_77(object o, EventArgs e)
	{
		Relay_OnSuspend_77();
	}

	private void Instance_OnResume_77(object o, EventArgs e)
	{
		Relay_OnResume_77();
	}

	private void SubGraph_SpawnEnemyWave_Out_11(object o, SubGraph_SpawnEnemyWave.LogicEventArgs e)
	{
		Relay_Out_11();
	}

	private void SubGraph_SpawnEnemyWave_Out_13(object o, SubGraph_SpawnEnemyWave.LogicEventArgs e)
	{
		Relay_Out_13();
	}

	private void SubGraph_SpawnEnemyWave_Out_14(object o, SubGraph_SpawnEnemyWave.LogicEventArgs e)
	{
		Relay_Out_14();
	}

	private void uScriptCon_ManualSwitch_Output1_19(object o, EventArgs e)
	{
		Relay_Output1_19();
	}

	private void uScriptCon_ManualSwitch_Output2_19(object o, EventArgs e)
	{
		Relay_Output2_19();
	}

	private void uScriptCon_ManualSwitch_Output3_19(object o, EventArgs e)
	{
		Relay_Output3_19();
	}

	private void uScriptCon_ManualSwitch_Output4_19(object o, EventArgs e)
	{
		Relay_Output4_19();
	}

	private void uScriptCon_ManualSwitch_Output5_19(object o, EventArgs e)
	{
		Relay_Output5_19();
	}

	private void uScriptCon_ManualSwitch_Output6_19(object o, EventArgs e)
	{
		Relay_Output6_19();
	}

	private void uScriptCon_ManualSwitch_Output7_19(object o, EventArgs e)
	{
		Relay_Output7_19();
	}

	private void uScriptCon_ManualSwitch_Output8_19(object o, EventArgs e)
	{
		Relay_Output8_19();
	}

	private void SubGraph_SpawnEnemyWave_Out_32(object o, SubGraph_SpawnEnemyWave.LogicEventArgs e)
	{
		Relay_Out_32();
	}

	private void SubGraph_SpawnEnemyWave_Out_36(object o, SubGraph_SpawnEnemyWave.LogicEventArgs e)
	{
		Relay_Out_36();
	}

	private void Relay_In_2()
	{
		int num = 0;
		Array array = local_TechsToDefend_TankArray;
		if (logic_uScript_InRangeOfAtLeastOneTech_techs_2.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_InRangeOfAtLeastOneTech_techs_2, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_InRangeOfAtLeastOneTech_techs_2, num, array.Length);
		num += array.Length;
		logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_2.In(logic_uScript_InRangeOfAtLeastOneTech_techs_2, logic_uScript_InRangeOfAtLeastOneTech_range_2);
		if (logic_uScript_InRangeOfAtLeastOneTech_uScript_InRangeOfAtLeastOneTech_2.InRange)
		{
			Relay_True_4();
		}
	}

	private void Relay_True_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.True(out logic_uScriptAct_SetBool_Target_4);
		local_StartWaves_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_False_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.False(out logic_uScriptAct_SetBool_Target_4);
		local_StartWaves_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_6()
	{
		logic_uScriptCon_CompareBool_Bool_6 = local_StartWaves_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_6.In(logic_uScriptCon_CompareBool_Bool_6);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_6.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_6.False;
		if (num)
		{
			Relay_In_21();
		}
		if (flag)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_8()
	{
		int num = 0;
		Array array = msgLose;
		if (logic_uScript_AddOnScreenMessage_locString_8.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_8, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_8, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_8 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_8.In(logic_uScript_AddOnScreenMessage_locString_8, logic_uScript_AddOnScreenMessage_msgPriority_8, logic_uScript_AddOnScreenMessage_holdMsg_8, logic_uScript_AddOnScreenMessage_tag_8, logic_uScript_AddOnScreenMessage_speaker_8, logic_uScript_AddOnScreenMessage_side_8);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_8.Out)
		{
			Relay_Fail_180();
		}
	}

	private void Relay_Out_11()
	{
		Relay_In_25();
	}

	private void Relay_In_11()
	{
		int num = 0;
		Array array = wave01Techs;
		if (logic_SubGraph_SpawnEnemyWave_techsInWave_11.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_SpawnEnemyWave_techsInWave_11, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_SpawnEnemyWave_techsInWave_11, num, array.Length);
		num += array.Length;
		logic_SubGraph_SpawnEnemyWave_timeBeforeNextWave_11 = timeBeforeNextWave;
		logic_SubGraph_SpawnEnemyWave_autoTriggerNextWave__11 = autoTriggerNextWave;
		logic_SubGraph_SpawnEnemyWave_timeBeforeAutoTrigger_11 = autoTriggerNextWaveTime;
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_11.In(logic_SubGraph_SpawnEnemyWave_techsInWave_11, logic_SubGraph_SpawnEnemyWave_timeBeforeNextWave_11, logic_SubGraph_SpawnEnemyWave_autoTriggerNextWave__11, logic_SubGraph_SpawnEnemyWave_timeBeforeAutoTrigger_11);
	}

	private void Relay_Out_13()
	{
		Relay_In_28();
	}

	private void Relay_In_13()
	{
		int num = 0;
		Array array = wave03Techs;
		if (logic_SubGraph_SpawnEnemyWave_techsInWave_13.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_SpawnEnemyWave_techsInWave_13, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_SpawnEnemyWave_techsInWave_13, num, array.Length);
		num += array.Length;
		logic_SubGraph_SpawnEnemyWave_timeBeforeNextWave_13 = timeBeforeNextWave;
		logic_SubGraph_SpawnEnemyWave_autoTriggerNextWave__13 = autoTriggerNextWave;
		logic_SubGraph_SpawnEnemyWave_timeBeforeAutoTrigger_13 = autoTriggerNextWaveTime;
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_13.In(logic_SubGraph_SpawnEnemyWave_techsInWave_13, logic_SubGraph_SpawnEnemyWave_timeBeforeNextWave_13, logic_SubGraph_SpawnEnemyWave_autoTriggerNextWave__13, logic_SubGraph_SpawnEnemyWave_timeBeforeAutoTrigger_13);
	}

	private void Relay_Out_14()
	{
		Relay_In_26();
	}

	private void Relay_In_14()
	{
		int num = 0;
		Array array = wave02Techs;
		if (logic_SubGraph_SpawnEnemyWave_techsInWave_14.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_SpawnEnemyWave_techsInWave_14, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_SpawnEnemyWave_techsInWave_14, num, array.Length);
		num += array.Length;
		logic_SubGraph_SpawnEnemyWave_timeBeforeNextWave_14 = timeBeforeNextWave;
		logic_SubGraph_SpawnEnemyWave_autoTriggerNextWave__14 = autoTriggerNextWave;
		logic_SubGraph_SpawnEnemyWave_timeBeforeAutoTrigger_14 = autoTriggerNextWaveTime;
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_14.In(logic_SubGraph_SpawnEnemyWave_techsInWave_14, logic_SubGraph_SpawnEnemyWave_timeBeforeNextWave_14, logic_SubGraph_SpawnEnemyWave_autoTriggerNextWave__14, logic_SubGraph_SpawnEnemyWave_timeBeforeAutoTrigger_14);
	}

	private void Relay_In_17()
	{
		int num = 0;
		Array array = msgWin;
		if (logic_uScript_AddOnScreenMessage_locString_17.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_17, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_17, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_17 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_17.In(logic_uScript_AddOnScreenMessage_locString_17, logic_uScript_AddOnScreenMessage_msgPriority_17, logic_uScript_AddOnScreenMessage_holdMsg_17, logic_uScript_AddOnScreenMessage_tag_17, logic_uScript_AddOnScreenMessage_speaker_17, logic_uScript_AddOnScreenMessage_side_17);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_17.Out)
		{
			Relay_Succeed_176();
		}
	}

	private void Relay_Output1_19()
	{
		Relay_In_11();
	}

	private void Relay_Output2_19()
	{
	}

	private void Relay_Output3_19()
	{
	}

	private void Relay_Output4_19()
	{
	}

	private void Relay_Output5_19()
	{
	}

	private void Relay_Output6_19()
	{
	}

	private void Relay_Output7_19()
	{
	}

	private void Relay_Output8_19()
	{
	}

	private void Relay_In_19()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_19 = local_WaveCounter_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_19.In(logic_uScriptCon_ManualSwitch_CurrentOutput_19);
	}

	private void Relay_In_21()
	{
		logic_uScriptCon_CompareInt_A_21 = local_WaveCounter_System_Int32;
		logic_uScriptCon_CompareInt_B_21 = numWaves;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_21.In(logic_uScriptCon_CompareInt_A_21, logic_uScriptCon_CompareInt_B_21);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_21.GreaterThan;
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_21.EqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_21.LessThan;
		if (greaterThan)
		{
			Relay_In_17();
		}
		if (equalTo)
		{
			Relay_In_54();
		}
		if (lessThan)
		{
			Relay_In_19();
		}
	}

	private void Relay_In_25()
	{
		logic_uScriptAct_AddInt_v2_A_25 = local_WaveCounter_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_25.In(logic_uScriptAct_AddInt_v2_A_25, logic_uScriptAct_AddInt_v2_B_25, out logic_uScriptAct_AddInt_v2_IntResult_25, out logic_uScriptAct_AddInt_v2_FloatResult_25);
		local_WaveCounter_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_25;
	}

	private void Relay_In_26()
	{
		logic_uScriptAct_AddInt_v2_A_26 = local_WaveCounter_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_26.In(logic_uScriptAct_AddInt_v2_A_26, logic_uScriptAct_AddInt_v2_B_26, out logic_uScriptAct_AddInt_v2_IntResult_26, out logic_uScriptAct_AddInt_v2_FloatResult_26);
		local_WaveCounter_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_26;
	}

	private void Relay_In_28()
	{
		logic_uScriptAct_AddInt_v2_A_28 = local_WaveCounter_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_28.In(logic_uScriptAct_AddInt_v2_A_28, logic_uScriptAct_AddInt_v2_B_28, out logic_uScriptAct_AddInt_v2_IntResult_28, out logic_uScriptAct_AddInt_v2_FloatResult_28);
		local_WaveCounter_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_28;
	}

	private void Relay_In_30()
	{
		logic_uScriptAct_AddInt_v2_A_30 = local_WaveCounter_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_30.In(logic_uScriptAct_AddInt_v2_A_30, logic_uScriptAct_AddInt_v2_B_30, out logic_uScriptAct_AddInt_v2_IntResult_30, out logic_uScriptAct_AddInt_v2_FloatResult_30);
		local_WaveCounter_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_30;
	}

	private void Relay_Out_32()
	{
		Relay_In_30();
	}

	private void Relay_In_32()
	{
		int num = 0;
		Array array = wave04Techs;
		if (logic_SubGraph_SpawnEnemyWave_techsInWave_32.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_SpawnEnemyWave_techsInWave_32, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_SpawnEnemyWave_techsInWave_32, num, array.Length);
		num += array.Length;
		logic_SubGraph_SpawnEnemyWave_timeBeforeNextWave_32 = timeBeforeNextWave;
		logic_SubGraph_SpawnEnemyWave_autoTriggerNextWave__32 = autoTriggerNextWave;
		logic_SubGraph_SpawnEnemyWave_timeBeforeAutoTrigger_32 = autoTriggerNextWaveTime;
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_32.In(logic_SubGraph_SpawnEnemyWave_techsInWave_32, logic_SubGraph_SpawnEnemyWave_timeBeforeNextWave_32, logic_SubGraph_SpawnEnemyWave_autoTriggerNextWave__32, logic_SubGraph_SpawnEnemyWave_timeBeforeAutoTrigger_32);
	}

	private void Relay_In_34()
	{
		logic_uScriptAct_AddInt_v2_A_34 = local_WaveCounter_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_34.In(logic_uScriptAct_AddInt_v2_A_34, logic_uScriptAct_AddInt_v2_B_34, out logic_uScriptAct_AddInt_v2_IntResult_34, out logic_uScriptAct_AddInt_v2_FloatResult_34);
		local_WaveCounter_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_34;
	}

	private void Relay_Out_36()
	{
		Relay_In_34();
	}

	private void Relay_In_36()
	{
		int num = 0;
		Array array = wave05Techs;
		if (logic_SubGraph_SpawnEnemyWave_techsInWave_36.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_SpawnEnemyWave_techsInWave_36, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_SpawnEnemyWave_techsInWave_36, num, array.Length);
		num += array.Length;
		logic_SubGraph_SpawnEnemyWave_timeBeforeNextWave_36 = timeBeforeNextWave;
		logic_SubGraph_SpawnEnemyWave_autoTriggerNextWave__36 = autoTriggerNextWave;
		logic_SubGraph_SpawnEnemyWave_timeBeforeAutoTrigger_36 = autoTriggerNextWaveTime;
		logic_SubGraph_SpawnEnemyWave_SubGraph_SpawnEnemyWave_36.In(logic_SubGraph_SpawnEnemyWave_techsInWave_36, logic_SubGraph_SpawnEnemyWave_timeBeforeNextWave_36, logic_SubGraph_SpawnEnemyWave_autoTriggerNextWave__36, logic_SubGraph_SpawnEnemyWave_timeBeforeAutoTrigger_36);
	}

	private void Relay_In_54()
	{
		logic_uScriptAct_SetFloat_uScriptAct_SetFloat_54.In(logic_uScriptAct_SetFloat_Value_54, out logic_uScriptAct_SetFloat_Target_54);
		timeBeforeNextWave = logic_uScriptAct_SetFloat_Target_54;
		if (logic_uScriptAct_SetFloat_uScriptAct_SetFloat_54.Out)
		{
			Relay_False_55();
		}
	}

	private void Relay_True_55()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_55.True(out logic_uScriptAct_SetBool_Target_55);
		autoTriggerNextWave = logic_uScriptAct_SetBool_Target_55;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_55.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_False_55()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_55.False(out logic_uScriptAct_SetBool_Target_55);
		autoTriggerNextWave = logic_uScriptAct_SetBool_Target_55;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_55.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_Reset_57()
	{
		int num = 0;
		Array array = local_main_list_UnityEngine_ColorArray;
		if (logic_uScriptAct_ForEachListColor_List_57.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_ForEachListColor_List_57, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_ForEachListColor_List_57, num, array.Length);
		num += array.Length;
		logic_uScriptAct_ForEachListColor_uScriptAct_ForEachListColor_57.Reset(logic_uScriptAct_ForEachListColor_List_57, out logic_uScriptAct_ForEachListColor_Value_57, out logic_uScriptAct_ForEachListColor_currentIndex_57);
		local_65_UnityEngine_Color = logic_uScriptAct_ForEachListColor_Value_57;
		if (logic_uScriptAct_ForEachListColor_uScriptAct_ForEachListColor_57.Iteration)
		{
			Relay_In_67();
			Relay_AtIndex_69();
		}
	}

	private void Relay_In_57()
	{
		int num = 0;
		Array array = local_main_list_UnityEngine_ColorArray;
		if (logic_uScriptAct_ForEachListColor_List_57.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_ForEachListColor_List_57, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_ForEachListColor_List_57, num, array.Length);
		num += array.Length;
		logic_uScriptAct_ForEachListColor_uScriptAct_ForEachListColor_57.In(logic_uScriptAct_ForEachListColor_List_57, out logic_uScriptAct_ForEachListColor_Value_57, out logic_uScriptAct_ForEachListColor_currentIndex_57);
		local_65_UnityEngine_Color = logic_uScriptAct_ForEachListColor_Value_57;
		if (logic_uScriptAct_ForEachListColor_uScriptAct_ForEachListColor_57.Iteration)
		{
			Relay_In_67();
			Relay_AtIndex_69();
		}
	}

	private void Relay_In_58()
	{
		int num = 0;
		Array array = local_main_list_UnityEngine_ColorArray;
		if (logic_uScriptAct_GetListSizeColor_List_58.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_GetListSizeColor_List_58, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_GetListSizeColor_List_58, num, array.Length);
		num += array.Length;
		logic_uScriptAct_GetListSizeColor_uScriptAct_GetListSizeColor_58.In(logic_uScriptAct_GetListSizeColor_List_58, out logic_uScriptAct_GetListSizeColor_ListSize_58);
		local_62_System_Int32 = logic_uScriptAct_GetListSizeColor_ListSize_58;
		if (logic_uScriptAct_GetListSizeColor_uScriptAct_GetListSizeColor_58.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_60()
	{
		logic_uScriptCon_CompareInt_A_60 = local_WaveCounter_System_Int32;
		logic_uScriptCon_CompareInt_B_60 = numWaves;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_60.In(logic_uScriptCon_CompareInt_A_60, logic_uScriptCon_CompareInt_B_60);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_60.LessThan)
		{
			Relay_In_57();
		}
	}

	private void Relay_In_67()
	{
		logic_uScriptAct_AddInt_v2_A_67 = local_WaveCounter_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_67.In(logic_uScriptAct_AddInt_v2_A_67, logic_uScriptAct_AddInt_v2_B_67, out logic_uScriptAct_AddInt_v2_IntResult_67, out logic_uScriptAct_AddInt_v2_FloatResult_67);
		local_WaveCounter_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_67;
	}

	private void Relay_In_68()
	{
		logic_uScriptAct_SetColor_Value_68 = local_72_UnityEngine_Color;
		logic_uScriptAct_SetColor_uScriptAct_SetColor_68.In(logic_uScriptAct_SetColor_Value_68, out logic_uScriptAct_SetColor_TargetColor_68);
		local_65_UnityEngine_Color = logic_uScriptAct_SetColor_TargetColor_68;
	}

	private void Relay_First_69()
	{
		int num = 0;
		Array array = local_base_list_UnityEngine_ColorArray;
		if (logic_uScriptAct_AccessListColor_List_69.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListColor_List_69, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListColor_List_69, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListColor_Index_69 = local_WaveCounter_System_Int32;
		logic_uScriptAct_AccessListColor_uScriptAct_AccessListColor_69.First(logic_uScriptAct_AccessListColor_List_69, logic_uScriptAct_AccessListColor_Index_69, out logic_uScriptAct_AccessListColor_Value_69);
		local_72_UnityEngine_Color = logic_uScriptAct_AccessListColor_Value_69;
		if (logic_uScriptAct_AccessListColor_uScriptAct_AccessListColor_69.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_Last_69()
	{
		int num = 0;
		Array array = local_base_list_UnityEngine_ColorArray;
		if (logic_uScriptAct_AccessListColor_List_69.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListColor_List_69, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListColor_List_69, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListColor_Index_69 = local_WaveCounter_System_Int32;
		logic_uScriptAct_AccessListColor_uScriptAct_AccessListColor_69.Last(logic_uScriptAct_AccessListColor_List_69, logic_uScriptAct_AccessListColor_Index_69, out logic_uScriptAct_AccessListColor_Value_69);
		local_72_UnityEngine_Color = logic_uScriptAct_AccessListColor_Value_69;
		if (logic_uScriptAct_AccessListColor_uScriptAct_AccessListColor_69.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_Random_69()
	{
		int num = 0;
		Array array = local_base_list_UnityEngine_ColorArray;
		if (logic_uScriptAct_AccessListColor_List_69.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListColor_List_69, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListColor_List_69, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListColor_Index_69 = local_WaveCounter_System_Int32;
		logic_uScriptAct_AccessListColor_uScriptAct_AccessListColor_69.Random(logic_uScriptAct_AccessListColor_List_69, logic_uScriptAct_AccessListColor_Index_69, out logic_uScriptAct_AccessListColor_Value_69);
		local_72_UnityEngine_Color = logic_uScriptAct_AccessListColor_Value_69;
		if (logic_uScriptAct_AccessListColor_uScriptAct_AccessListColor_69.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_AtIndex_69()
	{
		int num = 0;
		Array array = local_base_list_UnityEngine_ColorArray;
		if (logic_uScriptAct_AccessListColor_List_69.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListColor_List_69, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListColor_List_69, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListColor_Index_69 = local_WaveCounter_System_Int32;
		logic_uScriptAct_AccessListColor_uScriptAct_AccessListColor_69.AtIndex(logic_uScriptAct_AccessListColor_List_69, logic_uScriptAct_AccessListColor_Index_69, out logic_uScriptAct_AccessListColor_Value_69);
		local_72_UnityEngine_Color = logic_uScriptAct_AccessListColor_Value_69;
		if (logic_uScriptAct_AccessListColor_uScriptAct_AccessListColor_69.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_OnUpdate_77()
	{
		Relay_In_83();
	}

	private void Relay_OnSuspend_77()
	{
	}

	private void Relay_OnResume_77()
	{
	}

	private void Relay_InitialSpawn_78()
	{
		int num = 0;
		Array array = techsToDefend;
		if (logic_uScript_SpawnTechsFromData_spawnData_78.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_78, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_78, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_78 = owner_Connection_1;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_78.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_78, logic_uScript_SpawnTechsFromData_ownerNode_78, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_78);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_78.Out)
		{
			Relay_In_79();
		}
	}

	private void Relay_In_79()
	{
		int num = 0;
		Array array = techsToDefend;
		if (logic_uScript_GetAndCheckTechs_techData_79.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_79, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_79, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_79 = owner_Connection_80;
		int num2 = 0;
		Array array2 = local_TechsToDefend_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_79.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_79, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_79, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_79 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_79.In(logic_uScript_GetAndCheckTechs_techData_79, logic_uScript_GetAndCheckTechs_ownerNode_79, ref logic_uScript_GetAndCheckTechs_techs_79);
		local_TechsToDefend_TankArray = logic_uScript_GetAndCheckTechs_techs_79;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_79.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_79.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_79.AllDead;
		if (allAlive)
		{
			Relay_In_6();
		}
		if (someAlive)
		{
			Relay_In_6();
		}
		if (allDead)
		{
			Relay_In_8();
		}
	}

	private void Relay_True_82()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.True(out logic_uScriptAct_SetBool_Target_82);
		local_AlliesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_82;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_82.Out)
		{
			Relay_InitialSpawn_78();
		}
	}

	private void Relay_False_82()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.False(out logic_uScriptAct_SetBool_Target_82);
		local_AlliesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_82;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_82.Out)
		{
			Relay_InitialSpawn_78();
		}
	}

	private void Relay_In_83()
	{
		logic_uScriptCon_CompareBool_Bool_83 = local_AlliesSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83.In(logic_uScriptCon_CompareBool_Bool_83);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83.False;
		if (num)
		{
			Relay_In_79();
		}
		if (flag)
		{
			Relay_True_82();
		}
	}

	private void Relay_Succeed_176()
	{
		logic_uScript_FinishEncounter_owner_176 = owner_Connection_177;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_176.Succeed(logic_uScript_FinishEncounter_owner_176);
	}

	private void Relay_Fail_176()
	{
		logic_uScript_FinishEncounter_owner_176 = owner_Connection_177;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_176.Fail(logic_uScript_FinishEncounter_owner_176);
	}

	private void Relay_Succeed_180()
	{
		logic_uScript_FinishEncounter_owner_180 = owner_Connection_181;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_180.Succeed(logic_uScript_FinishEncounter_owner_180);
	}

	private void Relay_Fail_180()
	{
		logic_uScript_FinishEncounter_owner_180 = owner_Connection_181;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_180.Fail(logic_uScript_FinishEncounter_owner_180);
	}
}
