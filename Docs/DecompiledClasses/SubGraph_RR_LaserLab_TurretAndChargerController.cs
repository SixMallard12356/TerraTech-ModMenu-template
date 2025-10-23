using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_RR_LaserLab_TurretAndChargerController", "")]
[NodePath("Graphs")]
public class SubGraph_RR_LaserLab_TurretAndChargerController : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public bool turretsDestroyed;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SpawnTechData[] external_108 = new SpawnTechData[0];

	private SpawnTechData[] external_37 = new SpawnTechData[0];

	private string[] external_38 = new string[0];

	private float[] external_49 = new float[0];

	private BlockTypes external_39;

	private BlockTypes external_40;

	private bool external_43;

	private int local_55_System_Int32;

	private Tank[] local_ChargerTechData_TankArray = new Tank[0];

	private Tank local_CurrentCharger_Tank;

	private int local_CurrentChargerIndex_System_Int32;

	private Tank local_CurrentTurret_Tank;

	private int local_CurrentTurretIndex_System_Int32;

	private Tank[] local_EnemyTurrets_TankArray = new Tank[0];

	private float local_TurretAnimDuration_System_Single;

	private string local_TurretAnimTrigger_System_String = "";

	private float local_TurretBatteryChargeAmount_System_Single;

	private TankBlock local_TurretShieldBlock_TankBlock;

	private bool local_TurretShieldsEnabled_System_Boolean;

	private TankBlock local_TurretWeaponBlock_TankBlock;

	private GameObject owner_Connection_13;

	private GameObject owner_Connection_103;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_2 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_2 = true;

	private uScript_SetTechMarkerState logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_3 = new uScript_SetTechMarkerState();

	private Tank logic_uScript_SetTechMarkerState_tech_3;

	private bool logic_uScript_SetTechMarkerState_Out_3 = true;

	private uScript_SetShieldEnabled logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_4 = new uScript_SetShieldEnabled();

	private object logic_uScript_SetShieldEnabled_targetObject_4 = "";

	private bool logic_uScript_SetShieldEnabled_enable_4;

	private bool logic_uScript_SetShieldEnabled_Out_4 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_7 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_7;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_7;

	private bool logic_uScript_SetBatteryChargeAmount_Out_7 = true;

	private uScriptAct_AccessListString logic_uScriptAct_AccessListString_uScriptAct_AccessListString_8 = new uScriptAct_AccessListString();

	private string[] logic_uScriptAct_AccessListString_StringList_8 = new string[0];

	private int logic_uScriptAct_AccessListString_Index_8;

	private string logic_uScriptAct_AccessListString_Value_8;

	private bool logic_uScriptAct_AccessListString_Out_8 = true;

	private uScript_ForEachListTech logic_uScript_ForEachListTech_uScript_ForEachListTech_16 = new uScript_ForEachListTech();

	private Tank[] logic_uScript_ForEachListTech_List_16 = new Tank[0];

	private Tank logic_uScript_ForEachListTech_Value_16;

	private int logic_uScript_ForEachListTech_currentIndex_16;

	private bool logic_uScript_ForEachListTech_Immediate_16 = true;

	private bool logic_uScript_ForEachListTech_Done_16 = true;

	private bool logic_uScript_ForEachListTech_Iteration_16 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_20 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_20 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_22 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_22;

	private BlockTypes logic_uScript_GetTankBlock_blockType_22;

	private TankBlock logic_uScript_GetTankBlock_Return_22;

	private bool logic_uScript_GetTankBlock_Out_22 = true;

	private bool logic_uScript_GetTankBlock_Returned_22 = true;

	private bool logic_uScript_GetTankBlock_NotFound_22 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_23 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_23 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_23;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_23 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_23;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_23 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_23 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_23 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_23 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_24 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_24 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_27 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_27;

	private bool logic_uScript_IsTechAlive_Alive_27 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_27 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_28 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_28;

	private BlockTypes logic_uScript_GetTankBlock_blockType_28;

	private TankBlock logic_uScript_GetTankBlock_Return_28;

	private bool logic_uScript_GetTankBlock_Out_28 = true;

	private bool logic_uScript_GetTankBlock_Returned_28 = true;

	private bool logic_uScript_GetTankBlock_NotFound_28 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_29 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_29 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_30 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_30 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_31;

	private bool logic_uScriptCon_CompareBool_True_31 = true;

	private bool logic_uScriptCon_CompareBool_False_31 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_32 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_32;

	private bool logic_uScriptAct_SetBool_Out_32 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_32 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_32 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_34 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_34 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_42 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_42 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_44 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_44 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_45 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_45 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_46 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_46 = true;

	private uScriptAct_AccessListFloat logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_48 = new uScriptAct_AccessListFloat();

	private float[] logic_uScriptAct_AccessListFloat_FloatList_48 = new float[0];

	private int logic_uScriptAct_AccessListFloat_Index_48;

	private float logic_uScriptAct_AccessListFloat_Value_48;

	private bool logic_uScriptAct_AccessListFloat_Out_48 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_52;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_53 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_53;

	private int logic_uScriptAct_AddInt_v2_B_53 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_53;

	private float logic_uScriptAct_AddInt_v2_FloatResult_53;

	private bool logic_uScriptAct_AddInt_v2_Out_53 = true;

	private uScript_SetBlockAnimationTrigger logic_uScript_SetBlockAnimationTrigger_uScript_SetBlockAnimationTrigger_61 = new uScript_SetBlockAnimationTrigger();

	private TankBlock logic_uScript_SetBlockAnimationTrigger_block_61;

	private string logic_uScript_SetBlockAnimationTrigger_name_61 = "";

	private bool logic_uScript_SetBlockAnimationTrigger_Out_61 = true;

	private uScriptCon_TimedGate logic_uScriptCon_TimedGate_uScriptCon_TimedGate_64 = new uScriptCon_TimedGate();

	private float logic_uScriptCon_TimedGate_Duration_64;

	private bool logic_uScriptCon_TimedGate_StartOpen_64 = true;

	private bool logic_uScriptCon_TimedGate_TooSoon_64 = true;

	private uScriptCon_TimedGate logic_uScriptCon_TimedGate_uScriptCon_TimedGate_65 = new uScriptCon_TimedGate();

	private float logic_uScriptCon_TimedGate_Duration_65;

	private bool logic_uScriptCon_TimedGate_StartOpen_65 = true;

	private bool logic_uScriptCon_TimedGate_TooSoon_65 = true;

	private uScriptCon_TimedGate logic_uScriptCon_TimedGate_uScriptCon_TimedGate_66 = new uScriptCon_TimedGate();

	private float logic_uScriptCon_TimedGate_Duration_66;

	private bool logic_uScriptCon_TimedGate_StartOpen_66 = true;

	private bool logic_uScriptCon_TimedGate_TooSoon_66 = true;

	private uScriptCon_TimedGate logic_uScriptCon_TimedGate_uScriptCon_TimedGate_67 = new uScriptCon_TimedGate();

	private float logic_uScriptCon_TimedGate_Duration_67;

	private bool logic_uScriptCon_TimedGate_StartOpen_67 = true;

	private bool logic_uScriptCon_TimedGate_TooSoon_67 = true;

	private uScriptCon_TimedGate logic_uScriptCon_TimedGate_uScriptCon_TimedGate_68 = new uScriptCon_TimedGate();

	private float logic_uScriptCon_TimedGate_Duration_68;

	private bool logic_uScriptCon_TimedGate_StartOpen_68 = true;

	private bool logic_uScriptCon_TimedGate_TooSoon_68 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_69 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_69 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_72 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_72;

	private bool logic_uScriptAct_SetBool_Out_72 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_72 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_72 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_75 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_75;

	private bool logic_uScriptCon_CompareBool_True_75 = true;

	private bool logic_uScriptCon_CompareBool_False_75 = true;

	private uScript_SetTechsInvulnerable logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_76 = new uScript_SetTechsInvulnerable();

	private Tank[] logic_uScript_SetTechsInvulnerable_techs_76 = new Tank[0];

	private bool logic_uScript_SetTechsInvulnerable_Out_76 = true;

	private uScriptCon_TimedGate logic_uScriptCon_TimedGate_uScriptCon_TimedGate_78 = new uScriptCon_TimedGate();

	private float logic_uScriptCon_TimedGate_Duration_78;

	private bool logic_uScriptCon_TimedGate_StartOpen_78 = true;

	private bool logic_uScriptCon_TimedGate_TooSoon_78 = true;

	private uScriptCon_TimedGate logic_uScriptCon_TimedGate_uScriptCon_TimedGate_80 = new uScriptCon_TimedGate();

	private float logic_uScriptCon_TimedGate_Duration_80;

	private bool logic_uScriptCon_TimedGate_StartOpen_80 = true;

	private bool logic_uScriptCon_TimedGate_TooSoon_80 = true;

	private uScript_ForEachListTech logic_uScript_ForEachListTech_uScript_ForEachListTech_83 = new uScript_ForEachListTech();

	private Tank[] logic_uScript_ForEachListTech_List_83 = new Tank[0];

	private Tank logic_uScript_ForEachListTech_Value_83;

	private int logic_uScript_ForEachListTech_currentIndex_83;

	private bool logic_uScript_ForEachListTech_Immediate_83 = true;

	private bool logic_uScript_ForEachListTech_Done_83 = true;

	private bool logic_uScript_ForEachListTech_Iteration_83 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_84 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_84 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_85 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_85 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_86 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_86;

	private bool logic_uScriptAct_SetBool_Out_86 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_86 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_86 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_87 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_91 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_91 = true;

	private uScript_IsTechAlive logic_uScript_IsTechAlive_uScript_IsTechAlive_92 = new uScript_IsTechAlive();

	private Tank logic_uScript_IsTechAlive_tech_92;

	private bool logic_uScript_IsTechAlive_Alive_92 = true;

	private bool logic_uScript_IsTechAlive_Destroyed_92 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_94 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_94 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_94;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_94 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_94;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_94 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_94 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_94 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_94 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_98 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_98 = true;

	private uScriptAct_SetFloat logic_uScriptAct_SetFloat_uScriptAct_SetFloat_100 = new uScriptAct_SetFloat();

	private float logic_uScriptAct_SetFloat_Value_100 = 100f;

	private float logic_uScriptAct_SetFloat_Target_100;

	private bool logic_uScriptAct_SetFloat_Out_100 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_101 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_101 = true;

	private uScriptAct_SetFloat logic_uScriptAct_SetFloat_uScriptAct_SetFloat_102 = new uScriptAct_SetFloat();

	private float logic_uScriptAct_SetFloat_Value_102;

	private float logic_uScriptAct_SetFloat_Target_102;

	private bool logic_uScriptAct_SetFloat_Out_102 = true;

	private uScript_SetTechMarkerState logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_104 = new uScript_SetTechMarkerState();

	private Tank logic_uScript_SetTechMarkerState_tech_104;

	private bool logic_uScript_SetTechMarkerState_Out_104 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_105 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_105 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_13 || !m_RegisteredForEvents)
		{
			owner_Connection_13 = parentGameObject;
		}
		if (null == owner_Connection_103 || !m_RegisteredForEvents)
		{
			owner_Connection_103 = parentGameObject;
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
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_2.SetParent(g);
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_3.SetParent(g);
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_4.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_7.SetParent(g);
		logic_uScriptAct_AccessListString_uScriptAct_AccessListString_8.SetParent(g);
		logic_uScript_ForEachListTech_uScript_ForEachListTech_16.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_20.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_22.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_23.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_24.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_27.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_28.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_29.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_30.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_34.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_42.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_44.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_45.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_46.SetParent(g);
		logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_48.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_53.SetParent(g);
		logic_uScript_SetBlockAnimationTrigger_uScript_SetBlockAnimationTrigger_61.SetParent(g);
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_64.SetParent(g);
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_65.SetParent(g);
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_66.SetParent(g);
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_67.SetParent(g);
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_68.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_69.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_72.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_75.SetParent(g);
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_76.SetParent(g);
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_78.SetParent(g);
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_80.SetParent(g);
		logic_uScript_ForEachListTech_uScript_ForEachListTech_83.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_84.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_85.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_86.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_91.SetParent(g);
		logic_uScript_IsTechAlive_uScript_IsTechAlive_92.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_94.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_98.SetParent(g);
		logic_uScriptAct_SetFloat_uScriptAct_SetFloat_100.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_101.SetParent(g);
		logic_uScriptAct_SetFloat_uScriptAct_SetFloat_102.SetParent(g);
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_104.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_105.SetParent(g);
		owner_Connection_13 = parentGameObject;
		owner_Connection_103 = parentGameObject;
	}

	public void Awake()
	{
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.Output1 += uScriptCon_ManualSwitch_Output1_52;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.Output2 += uScriptCon_ManualSwitch_Output2_52;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.Output3 += uScriptCon_ManualSwitch_Output3_52;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.Output4 += uScriptCon_ManualSwitch_Output4_52;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.Output5 += uScriptCon_ManualSwitch_Output5_52;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.Output6 += uScriptCon_ManualSwitch_Output6_52;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.Output7 += uScriptCon_ManualSwitch_Output7_52;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.Output8 += uScriptCon_ManualSwitch_Output8_52;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_64.Out += uScriptCon_TimedGate_Out_64;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_65.Out += uScriptCon_TimedGate_Out_65;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_66.Out += uScriptCon_TimedGate_Out_66;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_67.Out += uScriptCon_TimedGate_Out_67;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_68.Out += uScriptCon_TimedGate_Out_68;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_78.Out += uScriptCon_TimedGate_Out_78;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_80.Out += uScriptCon_TimedGate_Out_80;
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
	}

	public void OnDisable()
	{
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_3.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_22.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_27.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_28.OnDisable();
		logic_uScript_IsTechAlive_uScript_IsTechAlive_92.OnDisable();
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_104.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_64.Update();
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_65.Update();
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_66.Update();
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_67.Update();
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_68.Update();
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_78.Update();
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_80.Update();
	}

	public void OnDestroy()
	{
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.Output1 -= uScriptCon_ManualSwitch_Output1_52;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.Output2 -= uScriptCon_ManualSwitch_Output2_52;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.Output3 -= uScriptCon_ManualSwitch_Output3_52;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.Output4 -= uScriptCon_ManualSwitch_Output4_52;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.Output5 -= uScriptCon_ManualSwitch_Output5_52;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.Output6 -= uScriptCon_ManualSwitch_Output6_52;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.Output7 -= uScriptCon_ManualSwitch_Output7_52;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.Output8 -= uScriptCon_ManualSwitch_Output8_52;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_64.Out -= uScriptCon_TimedGate_Out_64;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_65.Out -= uScriptCon_TimedGate_Out_65;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_66.Out -= uScriptCon_TimedGate_Out_66;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_67.Out -= uScriptCon_TimedGate_Out_67;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_68.Out -= uScriptCon_TimedGate_Out_68;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_78.Out -= uScriptCon_TimedGate_Out_78;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_80.Out -= uScriptCon_TimedGate_Out_80;
	}

	private void uScriptCon_ManualSwitch_Output1_52(object o, EventArgs e)
	{
		Relay_Output1_52();
	}

	private void uScriptCon_ManualSwitch_Output2_52(object o, EventArgs e)
	{
		Relay_Output2_52();
	}

	private void uScriptCon_ManualSwitch_Output3_52(object o, EventArgs e)
	{
		Relay_Output3_52();
	}

	private void uScriptCon_ManualSwitch_Output4_52(object o, EventArgs e)
	{
		Relay_Output4_52();
	}

	private void uScriptCon_ManualSwitch_Output5_52(object o, EventArgs e)
	{
		Relay_Output5_52();
	}

	private void uScriptCon_ManualSwitch_Output6_52(object o, EventArgs e)
	{
		Relay_Output6_52();
	}

	private void uScriptCon_ManualSwitch_Output7_52(object o, EventArgs e)
	{
		Relay_Output7_52();
	}

	private void uScriptCon_ManualSwitch_Output8_52(object o, EventArgs e)
	{
		Relay_Output8_52();
	}

	private void uScriptCon_TimedGate_Out_64(object o, EventArgs e)
	{
		Relay_Out_64();
	}

	private void uScriptCon_TimedGate_Out_65(object o, EventArgs e)
	{
		Relay_Out_65();
	}

	private void uScriptCon_TimedGate_Out_66(object o, EventArgs e)
	{
		Relay_Out_66();
	}

	private void uScriptCon_TimedGate_Out_67(object o, EventArgs e)
	{
		Relay_Out_67();
	}

	private void uScriptCon_TimedGate_Out_68(object o, EventArgs e)
	{
		Relay_Out_68();
	}

	private void uScriptCon_TimedGate_Out_78(object o, EventArgs e)
	{
		Relay_Out_78();
	}

	private void uScriptCon_TimedGate_Out_80(object o, EventArgs e)
	{
		Relay_Out_80();
	}

	[FriendlyName("In", "")]
	public void In([FriendlyName("chargerSpawnData", "")] SpawnTechData[] chargerSpawnData, [FriendlyName("turretSpawnData", "")] SpawnTechData[] turretSpawnData, [FriendlyName("turretAnimTriggers", "")] string[] turretAnimTriggers, [FriendlyName("turretAnimDurations", "")] float[] turretAnimDurations, [FriendlyName("turretWeaponBlockType", "")] BlockTypes turretWeaponBlockType, [FriendlyName("turretShieldBlockType", "")] BlockTypes turretShieldBlockType)
	{
		external_108 = chargerSpawnData;
		external_37 = turretSpawnData;
		external_38 = turretAnimTriggers;
		external_49 = turretAnimDurations;
		external_39 = turretWeaponBlockType;
		external_40 = turretShieldBlockType;
		Relay_In_94();
	}

	private void Relay_In_2()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_2.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_2.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_Show_3()
	{
		logic_uScript_SetTechMarkerState_tech_3 = local_CurrentTurret_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_3.Show(logic_uScript_SetTechMarkerState_tech_3);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_3.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_Hide_3()
	{
		logic_uScript_SetTechMarkerState_tech_3 = local_CurrentTurret_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_3.Hide(logic_uScript_SetTechMarkerState_tech_3);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_3.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_In_4()
	{
		logic_uScript_SetShieldEnabled_targetObject_4 = local_TurretShieldBlock_TankBlock;
		logic_uScript_SetShieldEnabled_enable_4 = local_TurretShieldsEnabled_System_Boolean;
		logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_4.In(logic_uScript_SetShieldEnabled_targetObject_4, logic_uScript_SetShieldEnabled_enable_4);
		if (logic_uScript_SetShieldEnabled_uScript_SetShieldEnabled_4.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_7()
	{
		logic_uScript_SetBatteryChargeAmount_tech_7 = local_CurrentTurret_Tank;
		logic_uScript_SetBatteryChargeAmount_chargeAmount_7 = local_TurretBatteryChargeAmount_System_Single;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_7.In(logic_uScript_SetBatteryChargeAmount_tech_7, logic_uScript_SetBatteryChargeAmount_chargeAmount_7);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_7.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_First_8()
	{
		int num = 0;
		Array array = external_38;
		if (logic_uScriptAct_AccessListString_StringList_8.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListString_StringList_8, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListString_StringList_8, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListString_Index_8 = local_CurrentTurretIndex_System_Int32;
		logic_uScriptAct_AccessListString_uScriptAct_AccessListString_8.First(logic_uScriptAct_AccessListString_StringList_8, logic_uScriptAct_AccessListString_Index_8, out logic_uScriptAct_AccessListString_Value_8);
		local_TurretAnimTrigger_System_String = logic_uScriptAct_AccessListString_Value_8;
		if (logic_uScriptAct_AccessListString_uScriptAct_AccessListString_8.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_Last_8()
	{
		int num = 0;
		Array array = external_38;
		if (logic_uScriptAct_AccessListString_StringList_8.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListString_StringList_8, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListString_StringList_8, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListString_Index_8 = local_CurrentTurretIndex_System_Int32;
		logic_uScriptAct_AccessListString_uScriptAct_AccessListString_8.Last(logic_uScriptAct_AccessListString_StringList_8, logic_uScriptAct_AccessListString_Index_8, out logic_uScriptAct_AccessListString_Value_8);
		local_TurretAnimTrigger_System_String = logic_uScriptAct_AccessListString_Value_8;
		if (logic_uScriptAct_AccessListString_uScriptAct_AccessListString_8.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_Random_8()
	{
		int num = 0;
		Array array = external_38;
		if (logic_uScriptAct_AccessListString_StringList_8.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListString_StringList_8, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListString_StringList_8, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListString_Index_8 = local_CurrentTurretIndex_System_Int32;
		logic_uScriptAct_AccessListString_uScriptAct_AccessListString_8.Random(logic_uScriptAct_AccessListString_StringList_8, logic_uScriptAct_AccessListString_Index_8, out logic_uScriptAct_AccessListString_Value_8);
		local_TurretAnimTrigger_System_String = logic_uScriptAct_AccessListString_Value_8;
		if (logic_uScriptAct_AccessListString_uScriptAct_AccessListString_8.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_AtIndex_8()
	{
		int num = 0;
		Array array = external_38;
		if (logic_uScriptAct_AccessListString_StringList_8.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListString_StringList_8, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListString_StringList_8, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListString_Index_8 = local_CurrentTurretIndex_System_Int32;
		logic_uScriptAct_AccessListString_uScriptAct_AccessListString_8.AtIndex(logic_uScriptAct_AccessListString_StringList_8, logic_uScriptAct_AccessListString_Index_8, out logic_uScriptAct_AccessListString_Value_8);
		local_TurretAnimTrigger_System_String = logic_uScriptAct_AccessListString_Value_8;
		if (logic_uScriptAct_AccessListString_uScriptAct_AccessListString_8.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_Reset_16()
	{
		int num = 0;
		Array array = local_EnemyTurrets_TankArray;
		if (logic_uScript_ForEachListTech_List_16.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_16, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_16, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_16.Reset(logic_uScript_ForEachListTech_List_16, out logic_uScript_ForEachListTech_Value_16, out logic_uScript_ForEachListTech_currentIndex_16);
		local_CurrentTurret_Tank = logic_uScript_ForEachListTech_Value_16;
		local_CurrentTurretIndex_System_Int32 = logic_uScript_ForEachListTech_currentIndex_16;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_16.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_16.Iteration;
		if (done)
		{
			Relay_In_45();
		}
		if (iteration)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_16()
	{
		int num = 0;
		Array array = local_EnemyTurrets_TankArray;
		if (logic_uScript_ForEachListTech_List_16.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_16, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_16, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_16.In(logic_uScript_ForEachListTech_List_16, out logic_uScript_ForEachListTech_Value_16, out logic_uScript_ForEachListTech_currentIndex_16);
		local_CurrentTurret_Tank = logic_uScript_ForEachListTech_Value_16;
		local_CurrentTurretIndex_System_Int32 = logic_uScript_ForEachListTech_currentIndex_16;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_16.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_16.Iteration;
		if (done)
		{
			Relay_In_45();
		}
		if (iteration)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_20()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_20.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_20.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_22()
	{
		logic_uScript_GetTankBlock_tank_22 = local_CurrentTurret_Tank;
		logic_uScript_GetTankBlock_blockType_22 = external_40;
		logic_uScript_GetTankBlock_Return_22 = logic_uScript_GetTankBlock_uScript_GetTankBlock_22.In(logic_uScript_GetTankBlock_tank_22, logic_uScript_GetTankBlock_blockType_22);
		local_TurretShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_22;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_22.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_22.NotFound;
		if (returned)
		{
			Relay_In_4();
		}
		if (notFound)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_23()
	{
		int num = 0;
		Array array = external_37;
		if (logic_uScript_GetAndCheckTechs_techData_23.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_23, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_23, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_23 = owner_Connection_13;
		int num2 = 0;
		Array array2 = local_EnemyTurrets_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_23.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_23, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_23, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_23 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_23.In(logic_uScript_GetAndCheckTechs_techData_23, logic_uScript_GetAndCheckTechs_ownerNode_23, ref logic_uScript_GetAndCheckTechs_techs_23);
		local_EnemyTurrets_TankArray = logic_uScript_GetAndCheckTechs_techs_23;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_23.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_23.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_23.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_23.WaitingToSpawn;
		if (allAlive)
		{
			Relay_False_72();
		}
		if (someAlive)
		{
			Relay_False_72();
		}
		if (allDead)
		{
			Relay_True_32();
		}
		if (waitingToSpawn)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_24()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_24.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_24.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_In_27()
	{
		logic_uScript_IsTechAlive_tech_27 = local_CurrentTurret_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_27.In(logic_uScript_IsTechAlive_tech_27);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_27.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_27.Destroyed;
		if (alive)
		{
			Relay_AtIndex_8();
		}
		if (destroyed)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_28()
	{
		logic_uScript_GetTankBlock_tank_28 = local_CurrentTurret_Tank;
		logic_uScript_GetTankBlock_blockType_28 = external_39;
		logic_uScript_GetTankBlock_Return_28 = logic_uScript_GetTankBlock_uScript_GetTankBlock_28.In(logic_uScript_GetTankBlock_tank_28, logic_uScript_GetTankBlock_blockType_28);
		local_TurretWeaponBlock_TankBlock = logic_uScript_GetTankBlock_Return_28;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_28.Returned)
		{
			Relay_AtIndex_48();
		}
	}

	private void Relay_In_29()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_29.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_29.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_30()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_30.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_30.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_31()
	{
		logic_uScriptCon_CompareBool_Bool_31 = local_TurretShieldsEnabled_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.In(logic_uScriptCon_CompareBool_Bool_31);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.False;
		if (num)
		{
			Relay_Hide_3();
		}
		if (flag)
		{
			Relay_Show_3();
		}
	}

	private void Relay_True_32()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.True(out logic_uScriptAct_SetBool_Target_32);
		external_43 = logic_uScriptAct_SetBool_Target_32;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_32.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_False_32()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.False(out logic_uScriptAct_SetBool_Target_32);
		external_43 = logic_uScriptAct_SetBool_Target_32;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_32.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_34()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_34.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_34.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_Connection_37()
	{
	}

	private void Relay_Connection_38()
	{
	}

	private void Relay_Connection_39()
	{
	}

	private void Relay_Connection_40()
	{
	}

	private void Relay_Connection_41()
	{
		if (this.Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.turretsDestroyed = external_43;
			this.Out(this, e);
		}
	}

	private void Relay_In_42()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_42.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_42.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_Connection_43()
	{
	}

	private void Relay_In_44()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_44.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_44.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_45()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_45.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_45.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_46()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_46.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_46.Out)
		{
			Relay_Connection_41();
		}
	}

	private void Relay_First_48()
	{
		int num = 0;
		Array array = external_49;
		if (logic_uScriptAct_AccessListFloat_FloatList_48.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListFloat_FloatList_48, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListFloat_FloatList_48, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListFloat_Index_48 = local_CurrentTurretIndex_System_Int32;
		logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_48.First(logic_uScriptAct_AccessListFloat_FloatList_48, logic_uScriptAct_AccessListFloat_Index_48, out logic_uScriptAct_AccessListFloat_Value_48);
		local_TurretAnimDuration_System_Single = logic_uScriptAct_AccessListFloat_Value_48;
		if (logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_48.Out)
		{
			Relay_In_53();
		}
	}

	private void Relay_Last_48()
	{
		int num = 0;
		Array array = external_49;
		if (logic_uScriptAct_AccessListFloat_FloatList_48.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListFloat_FloatList_48, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListFloat_FloatList_48, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListFloat_Index_48 = local_CurrentTurretIndex_System_Int32;
		logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_48.Last(logic_uScriptAct_AccessListFloat_FloatList_48, logic_uScriptAct_AccessListFloat_Index_48, out logic_uScriptAct_AccessListFloat_Value_48);
		local_TurretAnimDuration_System_Single = logic_uScriptAct_AccessListFloat_Value_48;
		if (logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_48.Out)
		{
			Relay_In_53();
		}
	}

	private void Relay_Random_48()
	{
		int num = 0;
		Array array = external_49;
		if (logic_uScriptAct_AccessListFloat_FloatList_48.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListFloat_FloatList_48, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListFloat_FloatList_48, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListFloat_Index_48 = local_CurrentTurretIndex_System_Int32;
		logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_48.Random(logic_uScriptAct_AccessListFloat_FloatList_48, logic_uScriptAct_AccessListFloat_Index_48, out logic_uScriptAct_AccessListFloat_Value_48);
		local_TurretAnimDuration_System_Single = logic_uScriptAct_AccessListFloat_Value_48;
		if (logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_48.Out)
		{
			Relay_In_53();
		}
	}

	private void Relay_AtIndex_48()
	{
		int num = 0;
		Array array = external_49;
		if (logic_uScriptAct_AccessListFloat_FloatList_48.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScriptAct_AccessListFloat_FloatList_48, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScriptAct_AccessListFloat_FloatList_48, num, array.Length);
		num += array.Length;
		logic_uScriptAct_AccessListFloat_Index_48 = local_CurrentTurretIndex_System_Int32;
		logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_48.AtIndex(logic_uScriptAct_AccessListFloat_FloatList_48, logic_uScriptAct_AccessListFloat_Index_48, out logic_uScriptAct_AccessListFloat_Value_48);
		local_TurretAnimDuration_System_Single = logic_uScriptAct_AccessListFloat_Value_48;
		if (logic_uScriptAct_AccessListFloat_uScriptAct_AccessListFloat_48.Out)
		{
			Relay_In_53();
		}
	}

	private void Relay_Connection_49()
	{
	}

	private void Relay_Output1_52()
	{
		Relay_In_64();
	}

	private void Relay_Output2_52()
	{
		Relay_In_65();
	}

	private void Relay_Output3_52()
	{
		Relay_In_66();
	}

	private void Relay_Output4_52()
	{
		Relay_In_67();
	}

	private void Relay_Output5_52()
	{
		Relay_In_68();
	}

	private void Relay_Output6_52()
	{
		Relay_In_78();
	}

	private void Relay_Output7_52()
	{
		Relay_In_80();
	}

	private void Relay_Output8_52()
	{
	}

	private void Relay_In_52()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_52 = local_55_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_52.In(logic_uScriptCon_ManualSwitch_CurrentOutput_52);
	}

	private void Relay_In_53()
	{
		logic_uScriptAct_AddInt_v2_A_53 = local_CurrentTurretIndex_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_53.In(logic_uScriptAct_AddInt_v2_A_53, logic_uScriptAct_AddInt_v2_B_53, out logic_uScriptAct_AddInt_v2_IntResult_53, out logic_uScriptAct_AddInt_v2_FloatResult_53);
		local_55_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_53;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_53.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_In_61()
	{
		logic_uScript_SetBlockAnimationTrigger_block_61 = local_TurretWeaponBlock_TankBlock;
		logic_uScript_SetBlockAnimationTrigger_name_61 = local_TurretAnimTrigger_System_String;
		logic_uScript_SetBlockAnimationTrigger_uScript_SetBlockAnimationTrigger_61.In(logic_uScript_SetBlockAnimationTrigger_block_61, logic_uScript_SetBlockAnimationTrigger_name_61);
		if (logic_uScript_SetBlockAnimationTrigger_uScript_SetBlockAnimationTrigger_61.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_Out_64()
	{
		Relay_In_61();
	}

	private void Relay_In_64()
	{
		logic_uScriptCon_TimedGate_Duration_64 = local_TurretAnimDuration_System_Single;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_64.In(logic_uScriptCon_TimedGate_Duration_64, logic_uScriptCon_TimedGate_StartOpen_64);
		if (logic_uScriptCon_TimedGate_uScriptCon_TimedGate_64.TooSoon)
		{
			Relay_In_69();
		}
	}

	private void Relay_Out_65()
	{
		Relay_In_61();
	}

	private void Relay_In_65()
	{
		logic_uScriptCon_TimedGate_Duration_65 = local_TurretAnimDuration_System_Single;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_65.In(logic_uScriptCon_TimedGate_Duration_65, logic_uScriptCon_TimedGate_StartOpen_65);
		if (logic_uScriptCon_TimedGate_uScriptCon_TimedGate_65.TooSoon)
		{
			Relay_In_69();
		}
	}

	private void Relay_Out_66()
	{
		Relay_In_61();
	}

	private void Relay_In_66()
	{
		logic_uScriptCon_TimedGate_Duration_66 = local_TurretAnimDuration_System_Single;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_66.In(logic_uScriptCon_TimedGate_Duration_66, logic_uScriptCon_TimedGate_StartOpen_66);
		if (logic_uScriptCon_TimedGate_uScriptCon_TimedGate_66.TooSoon)
		{
			Relay_In_69();
		}
	}

	private void Relay_Out_67()
	{
		Relay_In_61();
	}

	private void Relay_In_67()
	{
		logic_uScriptCon_TimedGate_Duration_67 = local_TurretAnimDuration_System_Single;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_67.In(logic_uScriptCon_TimedGate_Duration_67, logic_uScriptCon_TimedGate_StartOpen_67);
		if (logic_uScriptCon_TimedGate_uScriptCon_TimedGate_67.TooSoon)
		{
			Relay_In_69();
		}
	}

	private void Relay_Out_68()
	{
		Relay_In_61();
	}

	private void Relay_In_68()
	{
		logic_uScriptCon_TimedGate_Duration_68 = local_TurretAnimDuration_System_Single;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_68.In(logic_uScriptCon_TimedGate_Duration_68, logic_uScriptCon_TimedGate_StartOpen_68);
		if (logic_uScriptCon_TimedGate_uScriptCon_TimedGate_68.TooSoon)
		{
			Relay_In_69();
		}
	}

	private void Relay_In_69()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_69.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_69.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_True_72()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_72.True(out logic_uScriptAct_SetBool_Target_72);
		external_43 = logic_uScriptAct_SetBool_Target_72;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_72.Out)
		{
			Relay_In_75();
		}
	}

	private void Relay_False_72()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_72.False(out logic_uScriptAct_SetBool_Target_72);
		external_43 = logic_uScriptAct_SetBool_Target_72;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_72.Out)
		{
			Relay_In_75();
		}
	}

	private void Relay_In_75()
	{
		logic_uScriptCon_CompareBool_Bool_75 = local_TurretShieldsEnabled_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_75.In(logic_uScriptCon_CompareBool_Bool_75);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_75.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_75.False;
		if (num)
		{
			Relay_SetInvulnerable_76();
		}
		if (flag)
		{
			Relay_SetVulnerable_76();
		}
	}

	private void Relay_SetInvulnerable_76()
	{
		int num = 0;
		Array array = local_EnemyTurrets_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_76.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_76, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_76, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_76.SetInvulnerable(logic_uScript_SetTechsInvulnerable_techs_76);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_76.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_SetVulnerable_76()
	{
		int num = 0;
		Array array = local_EnemyTurrets_TankArray;
		if (logic_uScript_SetTechsInvulnerable_techs_76.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetTechsInvulnerable_techs_76, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetTechsInvulnerable_techs_76, num, array.Length);
		num += array.Length;
		logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_76.SetVulnerable(logic_uScript_SetTechsInvulnerable_techs_76);
		if (logic_uScript_SetTechsInvulnerable_uScript_SetTechsInvulnerable_76.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_Out_78()
	{
		Relay_In_61();
	}

	private void Relay_In_78()
	{
		logic_uScriptCon_TimedGate_Duration_78 = local_TurretAnimDuration_System_Single;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_78.In(logic_uScriptCon_TimedGate_Duration_78, logic_uScriptCon_TimedGate_StartOpen_78);
		if (logic_uScriptCon_TimedGate_uScriptCon_TimedGate_78.TooSoon)
		{
			Relay_In_69();
		}
	}

	private void Relay_Out_80()
	{
		Relay_In_61();
	}

	private void Relay_In_80()
	{
		logic_uScriptCon_TimedGate_Duration_80 = local_TurretAnimDuration_System_Single;
		logic_uScriptCon_TimedGate_uScriptCon_TimedGate_80.In(logic_uScriptCon_TimedGate_Duration_80, logic_uScriptCon_TimedGate_StartOpen_80);
		if (logic_uScriptCon_TimedGate_uScriptCon_TimedGate_80.TooSoon)
		{
			Relay_In_69();
		}
	}

	private void Relay_Reset_83()
	{
		int num = 0;
		Array array = local_ChargerTechData_TankArray;
		if (logic_uScript_ForEachListTech_List_83.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_83, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_83, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_83.Reset(logic_uScript_ForEachListTech_List_83, out logic_uScript_ForEachListTech_Value_83, out logic_uScript_ForEachListTech_currentIndex_83);
		local_CurrentCharger_Tank = logic_uScript_ForEachListTech_Value_83;
		local_CurrentChargerIndex_System_Int32 = logic_uScript_ForEachListTech_currentIndex_83;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_83.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_83.Iteration;
		if (done)
		{
			Relay_In_105();
		}
		if (iteration)
		{
			Relay_In_92();
		}
	}

	private void Relay_In_83()
	{
		int num = 0;
		Array array = local_ChargerTechData_TankArray;
		if (logic_uScript_ForEachListTech_List_83.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_ForEachListTech_List_83, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_ForEachListTech_List_83, num, array.Length);
		num += array.Length;
		logic_uScript_ForEachListTech_uScript_ForEachListTech_83.In(logic_uScript_ForEachListTech_List_83, out logic_uScript_ForEachListTech_Value_83, out logic_uScript_ForEachListTech_currentIndex_83);
		local_CurrentCharger_Tank = logic_uScript_ForEachListTech_Value_83;
		local_CurrentChargerIndex_System_Int32 = logic_uScript_ForEachListTech_currentIndex_83;
		bool done = logic_uScript_ForEachListTech_uScript_ForEachListTech_83.Done;
		bool iteration = logic_uScript_ForEachListTech_uScript_ForEachListTech_83.Iteration;
		if (done)
		{
			Relay_In_105();
		}
		if (iteration)
		{
			Relay_In_92();
		}
	}

	private void Relay_In_84()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_84.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_84.Out)
		{
			Relay_In_98();
		}
	}

	private void Relay_In_85()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_85.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_85.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_True_86()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_86.True(out logic_uScriptAct_SetBool_Target_86);
		local_TurretShieldsEnabled_System_Boolean = logic_uScriptAct_SetBool_Target_86;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_86.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_86.SetFalse;
		if (setTrue)
		{
			Relay_In_100();
		}
		if (setFalse)
		{
			Relay_In_102();
		}
	}

	private void Relay_False_86()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_86.False(out logic_uScriptAct_SetBool_Target_86);
		local_TurretShieldsEnabled_System_Boolean = logic_uScriptAct_SetBool_Target_86;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_86.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_86.SetFalse;
		if (setTrue)
		{
			Relay_In_100();
		}
		if (setFalse)
		{
			Relay_In_102();
		}
	}

	private void Relay_In_87()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87.Out)
		{
			Relay_In_85();
		}
	}

	private void Relay_In_91()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_91.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_91.Out)
		{
			Relay_In_83();
		}
	}

	private void Relay_In_92()
	{
		logic_uScript_IsTechAlive_tech_92 = local_CurrentCharger_Tank;
		logic_uScript_IsTechAlive_uScript_IsTechAlive_92.In(logic_uScript_IsTechAlive_tech_92);
		bool alive = logic_uScript_IsTechAlive_uScript_IsTechAlive_92.Alive;
		bool destroyed = logic_uScript_IsTechAlive_uScript_IsTechAlive_92.Destroyed;
		if (alive)
		{
			Relay_Hide_104();
		}
		if (destroyed)
		{
			Relay_In_101();
		}
	}

	private void Relay_In_94()
	{
		int num = 0;
		Array array = external_108;
		if (logic_uScript_GetAndCheckTechs_techData_94.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_94, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_94, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_94 = owner_Connection_103;
		int num2 = 0;
		Array array2 = local_ChargerTechData_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_94.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_94, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_94, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_94 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_94.In(logic_uScript_GetAndCheckTechs_techData_94, logic_uScript_GetAndCheckTechs_ownerNode_94, ref logic_uScript_GetAndCheckTechs_techs_94);
		local_ChargerTechData_TankArray = logic_uScript_GetAndCheckTechs_techs_94;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_94.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_94.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_94.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_94.WaitingToSpawn;
		if (allAlive)
		{
			Relay_True_86();
		}
		if (someAlive)
		{
			Relay_True_86();
		}
		if (allDead)
		{
			Relay_False_86();
		}
		if (waitingToSpawn)
		{
			Relay_In_84();
		}
	}

	private void Relay_In_98()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_98.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_98.Out)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_100()
	{
		logic_uScriptAct_SetFloat_uScriptAct_SetFloat_100.In(logic_uScriptAct_SetFloat_Value_100, out logic_uScriptAct_SetFloat_Target_100);
		local_TurretBatteryChargeAmount_System_Single = logic_uScriptAct_SetFloat_Target_100;
		if (logic_uScriptAct_SetFloat_uScriptAct_SetFloat_100.Out)
		{
			Relay_In_83();
		}
	}

	private void Relay_In_101()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_101.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_101.Out)
		{
			Relay_In_85();
		}
	}

	private void Relay_In_102()
	{
		logic_uScriptAct_SetFloat_uScriptAct_SetFloat_102.In(logic_uScriptAct_SetFloat_Value_102, out logic_uScriptAct_SetFloat_Target_102);
		local_TurretBatteryChargeAmount_System_Single = logic_uScriptAct_SetFloat_Target_102;
		if (logic_uScriptAct_SetFloat_uScriptAct_SetFloat_102.Out)
		{
			Relay_In_98();
		}
	}

	private void Relay_Show_104()
	{
		logic_uScript_SetTechMarkerState_tech_104 = local_CurrentCharger_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_104.Show(logic_uScript_SetTechMarkerState_tech_104);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_104.Out)
		{
			Relay_In_87();
		}
	}

	private void Relay_Hide_104()
	{
		logic_uScript_SetTechMarkerState_tech_104 = local_CurrentCharger_Tank;
		logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_104.Hide(logic_uScript_SetTechMarkerState_tech_104);
		if (logic_uScript_SetTechMarkerState_uScript_SetTechMarkerState_104.Out)
		{
			Relay_In_87();
		}
	}

	private void Relay_In_105()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_105.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_105.Out)
		{
			Relay_In_23();
		}
	}

	private void Relay_Connection_107()
	{
	}

	private void Relay_Connection_108()
	{
	}
}
