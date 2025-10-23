using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_KillEmAll_11", "")]
[NodePath("Graphs")]
public class SubGraph_KillEmAll_11 : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SpawnTechData[] external_4 = new SpawnTechData[0];

	private SpawnTechData[] external_11 = new SpawnTechData[0];

	private SpawnTechData[] external_15 = new SpawnTechData[0];

	private SpawnTechData[] external_18 = new SpawnTechData[0];

	private SpawnTechData[] external_24 = new SpawnTechData[0];

	private SpawnTechData[] external_28 = new SpawnTechData[0];

	private SpawnTechData[] external_23 = new SpawnTechData[0];

	private SpawnTechData[] external_20 = new SpawnTechData[0];

	private SpawnTechData[] external_35 = new SpawnTechData[0];

	private SpawnTechData[] external_39 = new SpawnTechData[0];

	private SpawnTechData[] external_34 = new SpawnTechData[0];

	private Tank local_ghostTech01_Tank;

	private Tank local_ghostTech02_Tank;

	private Tank local_ghostTech03_Tank;

	private Tank local_ghostTech04_Tank;

	private Tank local_ghostTech05_Tank;

	private Tank local_ghostTech06_Tank;

	private Tank local_ghostTech07_Tank;

	private Tank local_ghostTech08_Tank;

	private Tank local_ghostTech09_Tank;

	private Tank local_ghostTech10_Tank;

	private Tank local_ghostTech11_Tank;

	private Tank[] local_ghostTechs01_TankArray = new Tank[0];

	private Tank[] local_ghostTechs02_TankArray = new Tank[0];

	private Tank[] local_ghostTechs03_TankArray = new Tank[0];

	private Tank[] local_ghostTechs04_TankArray = new Tank[0];

	private Tank[] local_ghostTechs05_TankArray = new Tank[0];

	private Tank[] local_ghostTechs06_TankArray = new Tank[0];

	private Tank[] local_ghostTechs07_TankArray = new Tank[0];

	private Tank[] local_ghostTechs08_TankArray = new Tank[0];

	private Tank[] local_ghostTechs09_TankArray = new Tank[0];

	private Tank[] local_ghostTechs10_TankArray = new Tank[0];

	private Tank[] local_ghostTechs11_TankArray = new Tank[0];

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_12;

	private GameObject owner_Connection_14;

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_21;

	private GameObject owner_Connection_25;

	private GameObject owner_Connection_27;

	private GameObject owner_Connection_30;

	private GameObject owner_Connection_32;

	private GameObject owner_Connection_36;

	private GameObject owner_Connection_38;

	private GameObject owner_Connection_42;

	private GameObject owner_Connection_51;

	private GameObject owner_Connection_58;

	private GameObject owner_Connection_65;

	private GameObject owner_Connection_73;

	private GameObject owner_Connection_81;

	private GameObject owner_Connection_90;

	private GameObject owner_Connection_92;

	private GameObject owner_Connection_102;

	private GameObject owner_Connection_106;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_0 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_0;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_0 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_0;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_0 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_0 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_0 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_0 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_6 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_6 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_6 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_6;

	private bool logic_uScript_DestroyTechsFromData_Out_6 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_7 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_7 = new Tank[0];

	private int logic_uScript_AccessListTech_index_7;

	private Tank logic_uScript_AccessListTech_value_7;

	private bool logic_uScript_AccessListTech_Out_7 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_9 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_9;

	private Tank logic_uScript_SetTankInvulnerable_tank_9;

	private bool logic_uScript_SetTankInvulnerable_Out_9 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_13 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_13 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_13 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_13;

	private bool logic_uScript_DestroyTechsFromData_Out_13 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_16 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_16 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_16 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_16;

	private bool logic_uScript_DestroyTechsFromData_Out_16 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_19 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_19 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_19 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_19;

	private bool logic_uScript_DestroyTechsFromData_Out_19 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_22 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_22 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_22 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_22;

	private bool logic_uScript_DestroyTechsFromData_Out_22 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_26 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_26 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_26 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_26;

	private bool logic_uScript_DestroyTechsFromData_Out_26 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_29 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_29 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_29 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_29;

	private bool logic_uScript_DestroyTechsFromData_Out_29 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_31 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_31 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_31 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_31;

	private bool logic_uScript_DestroyTechsFromData_Out_31 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_33 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_33 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_33 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_33;

	private bool logic_uScript_DestroyTechsFromData_Out_33 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_37 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_37 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_37 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_37;

	private bool logic_uScript_DestroyTechsFromData_Out_37 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_40 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_40 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_40 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_40;

	private bool logic_uScript_DestroyTechsFromData_Out_40 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_41 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_43 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_43 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_43;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_43 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_43;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_43 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_43 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_43 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_43 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_44 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_44;

	private Tank logic_uScript_SetTankInvulnerable_tank_44;

	private bool logic_uScript_SetTankInvulnerable_Out_44 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_45 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_45 = new Tank[0];

	private int logic_uScript_AccessListTech_index_45;

	private Tank logic_uScript_AccessListTech_value_45;

	private bool logic_uScript_AccessListTech_Out_45 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_46 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_46 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_49 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_50 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_50 = new Tank[0];

	private int logic_uScript_AccessListTech_index_50;

	private Tank logic_uScript_AccessListTech_value_50;

	private bool logic_uScript_AccessListTech_Out_50 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_53 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_53;

	private Tank logic_uScript_SetTankInvulnerable_tank_53;

	private bool logic_uScript_SetTankInvulnerable_Out_53 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_54 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_54 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_54;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_54 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_54;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_54 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_54 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_54 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_54 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_56 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_56 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_57 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_57 = new Tank[0];

	private int logic_uScript_AccessListTech_index_57;

	private Tank logic_uScript_AccessListTech_value_57;

	private bool logic_uScript_AccessListTech_Out_57 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_59 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_59 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_59;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_59 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_59;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_59 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_59 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_59 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_59 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_60 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_60;

	private Tank logic_uScript_SetTankInvulnerable_tank_60;

	private bool logic_uScript_SetTankInvulnerable_Out_60 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_63 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_63 = new Tank[0];

	private int logic_uScript_AccessListTech_index_63;

	private Tank logic_uScript_AccessListTech_value_63;

	private bool logic_uScript_AccessListTech_Out_63 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_67 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_67 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_67;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_67 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_67;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_67 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_67 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_67 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_67 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_68 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_68 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_69 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_69;

	private Tank logic_uScript_SetTankInvulnerable_tank_69;

	private bool logic_uScript_SetTankInvulnerable_Out_69 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_72 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_74 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_74 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_74;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_74 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_74;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_74 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_74 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_74 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_74 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_75 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_75;

	private Tank logic_uScript_SetTankInvulnerable_tank_75;

	private bool logic_uScript_SetTankInvulnerable_Out_75 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_76 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_76 = new Tank[0];

	private int logic_uScript_AccessListTech_index_76;

	private Tank logic_uScript_AccessListTech_value_76;

	private bool logic_uScript_AccessListTech_Out_76 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_77 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_77 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_77;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_77 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_77;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_77 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_77 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_77 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_77 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_78 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_78 = new Tank[0];

	private int logic_uScript_AccessListTech_index_78;

	private Tank logic_uScript_AccessListTech_value_78;

	private bool logic_uScript_AccessListTech_Out_78 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_79 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_79 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_82 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_82;

	private Tank logic_uScript_SetTankInvulnerable_tank_82;

	private bool logic_uScript_SetTankInvulnerable_Out_82 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_84 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_84 = new Tank[0];

	private int logic_uScript_AccessListTech_index_84;

	private Tank logic_uScript_AccessListTech_value_84;

	private bool logic_uScript_AccessListTech_Out_84 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_85 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_85 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_88 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_88;

	private Tank logic_uScript_SetTankInvulnerable_tank_88;

	private bool logic_uScript_SetTankInvulnerable_Out_88 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_89 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_89;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_89 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_89;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_89 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_89 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_89 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_89 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_91 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_91 = new Tank[0];

	private int logic_uScript_AccessListTech_index_91;

	private Tank logic_uScript_AccessListTech_value_91;

	private bool logic_uScript_AccessListTech_Out_91 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_93 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_93 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_93;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_93 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_93;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_93 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_93 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_93 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_93 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_95 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_95 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_96 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_96;

	private Tank logic_uScript_SetTankInvulnerable_tank_96;

	private bool logic_uScript_SetTankInvulnerable_Out_96 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_99 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_99 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_100 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_100 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_100;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_100 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_100;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_100 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_100 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_100 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_100 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_101 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_101 = new Tank[0];

	private int logic_uScript_AccessListTech_index_101;

	private Tank logic_uScript_AccessListTech_value_101;

	private bool logic_uScript_AccessListTech_Out_101 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_103 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_103;

	private Tank logic_uScript_SetTankInvulnerable_tank_103;

	private bool logic_uScript_SetTankInvulnerable_Out_103 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_105 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_105;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_105 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_105;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_105 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_105 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_105 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_105 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_107 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_110 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_110 = new Tank[0];

	private int logic_uScript_AccessListTech_index_110;

	private Tank logic_uScript_AccessListTech_value_110;

	private bool logic_uScript_AccessListTech_Out_110 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_111 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_111;

	private Tank logic_uScript_SetTankInvulnerable_tank_111;

	private bool logic_uScript_SetTankInvulnerable_Out_111 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_5 || !m_RegisteredForEvents)
		{
			owner_Connection_5 = parentGameObject;
		}
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
		}
		if (null == owner_Connection_12 || !m_RegisteredForEvents)
		{
			owner_Connection_12 = parentGameObject;
		}
		if (null == owner_Connection_14 || !m_RegisteredForEvents)
		{
			owner_Connection_14 = parentGameObject;
		}
		if (null == owner_Connection_17 || !m_RegisteredForEvents)
		{
			owner_Connection_17 = parentGameObject;
		}
		if (null == owner_Connection_21 || !m_RegisteredForEvents)
		{
			owner_Connection_21 = parentGameObject;
		}
		if (null == owner_Connection_25 || !m_RegisteredForEvents)
		{
			owner_Connection_25 = parentGameObject;
		}
		if (null == owner_Connection_27 || !m_RegisteredForEvents)
		{
			owner_Connection_27 = parentGameObject;
		}
		if (null == owner_Connection_30 || !m_RegisteredForEvents)
		{
			owner_Connection_30 = parentGameObject;
		}
		if (null == owner_Connection_32 || !m_RegisteredForEvents)
		{
			owner_Connection_32 = parentGameObject;
		}
		if (null == owner_Connection_36 || !m_RegisteredForEvents)
		{
			owner_Connection_36 = parentGameObject;
		}
		if (null == owner_Connection_38 || !m_RegisteredForEvents)
		{
			owner_Connection_38 = parentGameObject;
		}
		if (null == owner_Connection_42 || !m_RegisteredForEvents)
		{
			owner_Connection_42 = parentGameObject;
		}
		if (null == owner_Connection_51 || !m_RegisteredForEvents)
		{
			owner_Connection_51 = parentGameObject;
		}
		if (null == owner_Connection_58 || !m_RegisteredForEvents)
		{
			owner_Connection_58 = parentGameObject;
		}
		if (null == owner_Connection_65 || !m_RegisteredForEvents)
		{
			owner_Connection_65 = parentGameObject;
		}
		if (null == owner_Connection_73 || !m_RegisteredForEvents)
		{
			owner_Connection_73 = parentGameObject;
		}
		if (null == owner_Connection_81 || !m_RegisteredForEvents)
		{
			owner_Connection_81 = parentGameObject;
		}
		if (null == owner_Connection_90 || !m_RegisteredForEvents)
		{
			owner_Connection_90 = parentGameObject;
		}
		if (null == owner_Connection_92 || !m_RegisteredForEvents)
		{
			owner_Connection_92 = parentGameObject;
		}
		if (null == owner_Connection_102 || !m_RegisteredForEvents)
		{
			owner_Connection_102 = parentGameObject;
		}
		if (null == owner_Connection_106 || !m_RegisteredForEvents)
		{
			owner_Connection_106 = parentGameObject;
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
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_6.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_7.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_9.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_13.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_16.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_19.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_22.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_26.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_29.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_31.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_33.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_37.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_40.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_43.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_44.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_45.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_46.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_50.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_53.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_54.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_56.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_57.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_59.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_60.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_63.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_67.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_68.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_69.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_74.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_75.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_76.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_77.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_78.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_79.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_82.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_84.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_85.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_88.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_91.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_93.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_95.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_96.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_99.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_100.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_101.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_103.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_110.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_111.SetParent(g);
		owner_Connection_5 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_12 = parentGameObject;
		owner_Connection_14 = parentGameObject;
		owner_Connection_17 = parentGameObject;
		owner_Connection_21 = parentGameObject;
		owner_Connection_25 = parentGameObject;
		owner_Connection_27 = parentGameObject;
		owner_Connection_30 = parentGameObject;
		owner_Connection_32 = parentGameObject;
		owner_Connection_36 = parentGameObject;
		owner_Connection_38 = parentGameObject;
		owner_Connection_42 = parentGameObject;
		owner_Connection_51 = parentGameObject;
		owner_Connection_58 = parentGameObject;
		owner_Connection_65 = parentGameObject;
		owner_Connection_73 = parentGameObject;
		owner_Connection_81 = parentGameObject;
		owner_Connection_90 = parentGameObject;
		owner_Connection_92 = parentGameObject;
		owner_Connection_102 = parentGameObject;
		owner_Connection_106 = parentGameObject;
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
	}

	public void OnDisable()
	{
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_9.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_44.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_53.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_60.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_69.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_75.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_82.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_88.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_96.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_103.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_111.OnDisable();
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
	public void In([FriendlyName("ghostTechSpawnData01", "")] SpawnTechData[] ghostTechSpawnData01, [FriendlyName("ghostTechSpawnData02", "")] SpawnTechData[] ghostTechSpawnData02, [FriendlyName("ghostTechSpawnData03", "")] SpawnTechData[] ghostTechSpawnData03, [FriendlyName("ghostTechSpawnData04", "")] SpawnTechData[] ghostTechSpawnData04, [FriendlyName("ghostTechSpawnData05", "")] SpawnTechData[] ghostTechSpawnData05, [FriendlyName("ghostTechSpawnData06", "")] SpawnTechData[] ghostTechSpawnData06, [FriendlyName("ghostTechSpawnData07", "")] SpawnTechData[] ghostTechSpawnData07, [FriendlyName("ghostTechSpawnData08", "")] SpawnTechData[] ghostTechSpawnData08, [FriendlyName("ghostTechSpawnData09", "")] SpawnTechData[] ghostTechSpawnData09, [FriendlyName("ghostTechSpawnData10", "")] SpawnTechData[] ghostTechSpawnData10, [FriendlyName("ghostTechSpawnData11", "")] SpawnTechData[] ghostTechSpawnData11)
	{
		external_4 = ghostTechSpawnData01;
		external_11 = ghostTechSpawnData02;
		external_15 = ghostTechSpawnData03;
		external_18 = ghostTechSpawnData04;
		external_24 = ghostTechSpawnData05;
		external_28 = ghostTechSpawnData06;
		external_23 = ghostTechSpawnData07;
		external_20 = ghostTechSpawnData08;
		external_35 = ghostTechSpawnData09;
		external_39 = ghostTechSpawnData10;
		external_34 = ghostTechSpawnData11;
		Relay_In_0();
	}

	private void Relay_In_0()
	{
		int num = 0;
		Array array = external_4;
		if (logic_uScript_GetAndCheckTechs_techData_0.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_0, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_0, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_0 = owner_Connection_5;
		int num2 = 0;
		Array array2 = local_ghostTechs01_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_0.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_0, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_0, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_0 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.In(logic_uScript_GetAndCheckTechs_techData_0, logic_uScript_GetAndCheckTechs_ownerNode_0, ref logic_uScript_GetAndCheckTechs_techs_0);
		local_ghostTechs01_TankArray = logic_uScript_GetAndCheckTechs_techs_0;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_0.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_7();
		}
		if (someAlive)
		{
			Relay_AtIndex_7();
		}
		if (allDead)
		{
			Relay_In_41();
		}
		if (waitingToSpawn)
		{
			Relay_In_41();
		}
	}

	private void Relay_Connection_2()
	{
		if (this.Out != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Out(this, args);
		}
	}

	private void Relay_Connection_4()
	{
	}

	private void Relay_In_6()
	{
		int num = 0;
		Array array = external_4;
		if (logic_uScript_DestroyTechsFromData_techData_6.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_6, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_6, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_6 = owner_Connection_8;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_6.In(logic_uScript_DestroyTechsFromData_techData_6, logic_uScript_DestroyTechsFromData_shouldExplode_6, logic_uScript_DestroyTechsFromData_ownerNode_6);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_6.Out)
		{
			Relay_In_43();
		}
	}

	private void Relay_AtIndex_7()
	{
		int num = 0;
		Array array = local_ghostTechs01_TankArray;
		if (logic_uScript_AccessListTech_techList_7.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_7, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_7, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_7.AtIndex(ref logic_uScript_AccessListTech_techList_7, logic_uScript_AccessListTech_index_7, out logic_uScript_AccessListTech_value_7);
		local_ghostTechs01_TankArray = logic_uScript_AccessListTech_techList_7;
		local_ghostTech01_Tank = logic_uScript_AccessListTech_value_7;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_7.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_In_9()
	{
		logic_uScript_SetTankInvulnerable_tank_9 = local_ghostTech01_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_9.In(logic_uScript_SetTankInvulnerable_invulnerable_9, logic_uScript_SetTankInvulnerable_tank_9);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_9.Out)
		{
			Relay_In_6();
		}
	}

	private void Relay_Connection_10()
	{
	}

	private void Relay_Connection_11()
	{
	}

	private void Relay_In_13()
	{
		int num = 0;
		Array array = external_11;
		if (logic_uScript_DestroyTechsFromData_techData_13.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_13, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_13, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_13 = owner_Connection_12;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_13.In(logic_uScript_DestroyTechsFromData_techData_13, logic_uScript_DestroyTechsFromData_shouldExplode_13, logic_uScript_DestroyTechsFromData_ownerNode_13);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_13.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_Connection_15()
	{
	}

	private void Relay_In_16()
	{
		int num = 0;
		Array array = external_18;
		if (logic_uScript_DestroyTechsFromData_techData_16.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_16, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_16, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_16 = owner_Connection_14;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_16.In(logic_uScript_DestroyTechsFromData_techData_16, logic_uScript_DestroyTechsFromData_shouldExplode_16, logic_uScript_DestroyTechsFromData_ownerNode_16);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_16.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_Connection_18()
	{
	}

	private void Relay_In_19()
	{
		int num = 0;
		Array array = external_15;
		if (logic_uScript_DestroyTechsFromData_techData_19.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_19, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_19, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_19 = owner_Connection_17;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_19.In(logic_uScript_DestroyTechsFromData_techData_19, logic_uScript_DestroyTechsFromData_shouldExplode_19, logic_uScript_DestroyTechsFromData_ownerNode_19);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_19.Out)
		{
			Relay_In_59();
		}
	}

	private void Relay_Connection_20()
	{
	}

	private void Relay_In_22()
	{
		int num = 0;
		Array array = external_23;
		if (logic_uScript_DestroyTechsFromData_techData_22.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_22, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_22, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_22 = owner_Connection_25;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_22.In(logic_uScript_DestroyTechsFromData_techData_22, logic_uScript_DestroyTechsFromData_shouldExplode_22, logic_uScript_DestroyTechsFromData_ownerNode_22);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_22.Out)
		{
			Relay_In_89();
		}
	}

	private void Relay_Connection_23()
	{
	}

	private void Relay_Connection_24()
	{
	}

	private void Relay_In_26()
	{
		int num = 0;
		Array array = external_28;
		if (logic_uScript_DestroyTechsFromData_techData_26.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_26, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_26, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_26 = owner_Connection_21;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_26.In(logic_uScript_DestroyTechsFromData_techData_26, logic_uScript_DestroyTechsFromData_shouldExplode_26, logic_uScript_DestroyTechsFromData_ownerNode_26);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_26.Out)
		{
			Relay_In_77();
		}
	}

	private void Relay_Connection_28()
	{
	}

	private void Relay_In_29()
	{
		int num = 0;
		Array array = external_20;
		if (logic_uScript_DestroyTechsFromData_techData_29.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_29, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_29, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_29 = owner_Connection_30;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_29.In(logic_uScript_DestroyTechsFromData_techData_29, logic_uScript_DestroyTechsFromData_shouldExplode_29, logic_uScript_DestroyTechsFromData_ownerNode_29);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_29.Out)
		{
			Relay_In_93();
		}
	}

	private void Relay_In_31()
	{
		int num = 0;
		Array array = external_24;
		if (logic_uScript_DestroyTechsFromData_techData_31.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_31, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_31, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_31 = owner_Connection_27;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_31.In(logic_uScript_DestroyTechsFromData_techData_31, logic_uScript_DestroyTechsFromData_shouldExplode_31, logic_uScript_DestroyTechsFromData_ownerNode_31);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_31.Out)
		{
			Relay_In_74();
		}
	}

	private void Relay_In_33()
	{
		int num = 0;
		Array array = external_34;
		if (logic_uScript_DestroyTechsFromData_techData_33.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_33, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_33, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_33 = owner_Connection_36;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_33.In(logic_uScript_DestroyTechsFromData_techData_33, logic_uScript_DestroyTechsFromData_shouldExplode_33, logic_uScript_DestroyTechsFromData_ownerNode_33);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_33.Out)
		{
			Relay_Connection_2();
		}
	}

	private void Relay_Connection_34()
	{
	}

	private void Relay_Connection_35()
	{
	}

	private void Relay_In_37()
	{
		int num = 0;
		Array array = external_39;
		if (logic_uScript_DestroyTechsFromData_techData_37.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_37, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_37, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_37 = owner_Connection_32;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_37.In(logic_uScript_DestroyTechsFromData_techData_37, logic_uScript_DestroyTechsFromData_shouldExplode_37, logic_uScript_DestroyTechsFromData_ownerNode_37);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_37.Out)
		{
			Relay_In_105();
		}
	}

	private void Relay_Connection_39()
	{
	}

	private void Relay_In_40()
	{
		int num = 0;
		Array array = external_35;
		if (logic_uScript_DestroyTechsFromData_techData_40.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_40, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_40, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_40 = owner_Connection_38;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_40.In(logic_uScript_DestroyTechsFromData_techData_40, logic_uScript_DestroyTechsFromData_shouldExplode_40, logic_uScript_DestroyTechsFromData_ownerNode_40);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_40.Out)
		{
			Relay_In_100();
		}
	}

	private void Relay_In_41()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_41.Out)
		{
			Relay_In_6();
		}
	}

	private void Relay_In_43()
	{
		int num = 0;
		Array array = external_11;
		if (logic_uScript_GetAndCheckTechs_techData_43.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_43, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_43, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_43 = owner_Connection_42;
		int num2 = 0;
		Array array2 = local_ghostTechs02_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_43.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_43, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_43, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_43 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_43.In(logic_uScript_GetAndCheckTechs_techData_43, logic_uScript_GetAndCheckTechs_ownerNode_43, ref logic_uScript_GetAndCheckTechs_techs_43);
		local_ghostTechs02_TankArray = logic_uScript_GetAndCheckTechs_techs_43;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_43.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_43.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_43.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_43.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_45();
		}
		if (someAlive)
		{
			Relay_AtIndex_45();
		}
		if (allDead)
		{
			Relay_In_46();
		}
		if (waitingToSpawn)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_44()
	{
		logic_uScript_SetTankInvulnerable_tank_44 = local_ghostTech02_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_44.In(logic_uScript_SetTankInvulnerable_invulnerable_44, logic_uScript_SetTankInvulnerable_tank_44);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_44.Out)
		{
			Relay_In_13();
		}
	}

	private void Relay_AtIndex_45()
	{
		int num = 0;
		Array array = local_ghostTechs02_TankArray;
		if (logic_uScript_AccessListTech_techList_45.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_45, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_45, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_45.AtIndex(ref logic_uScript_AccessListTech_techList_45, logic_uScript_AccessListTech_index_45, out logic_uScript_AccessListTech_value_45);
		local_ghostTechs02_TankArray = logic_uScript_AccessListTech_techList_45;
		local_ghostTech02_Tank = logic_uScript_AccessListTech_value_45;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_45.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_46()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_46.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_46.Out)
		{
			Relay_In_13();
		}
	}

	private void Relay_In_49()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_49.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_AtIndex_50()
	{
		int num = 0;
		Array array = local_ghostTechs03_TankArray;
		if (logic_uScript_AccessListTech_techList_50.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_50, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_50, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_50.AtIndex(ref logic_uScript_AccessListTech_techList_50, logic_uScript_AccessListTech_index_50, out logic_uScript_AccessListTech_value_50);
		local_ghostTechs03_TankArray = logic_uScript_AccessListTech_techList_50;
		local_ghostTech03_Tank = logic_uScript_AccessListTech_value_50;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_50.Out)
		{
			Relay_In_53();
		}
	}

	private void Relay_In_53()
	{
		logic_uScript_SetTankInvulnerable_tank_53 = local_ghostTech03_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_53.In(logic_uScript_SetTankInvulnerable_invulnerable_53, logic_uScript_SetTankInvulnerable_tank_53);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_53.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_In_54()
	{
		int num = 0;
		Array array = external_15;
		if (logic_uScript_GetAndCheckTechs_techData_54.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_54, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_54, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_54 = owner_Connection_51;
		int num2 = 0;
		Array array2 = local_ghostTechs03_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_54.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_54, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_54, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_54 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_54.In(logic_uScript_GetAndCheckTechs_techData_54, logic_uScript_GetAndCheckTechs_ownerNode_54, ref logic_uScript_GetAndCheckTechs_techs_54);
		local_ghostTechs03_TankArray = logic_uScript_GetAndCheckTechs_techs_54;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_54.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_54.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_54.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_54.WaitingToSpawn;
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
			Relay_In_49();
		}
		if (waitingToSpawn)
		{
			Relay_In_49();
		}
	}

	private void Relay_In_56()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_56.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_56.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_AtIndex_57()
	{
		int num = 0;
		Array array = local_ghostTechs04_TankArray;
		if (logic_uScript_AccessListTech_techList_57.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_57, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_57, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_57.AtIndex(ref logic_uScript_AccessListTech_techList_57, logic_uScript_AccessListTech_index_57, out logic_uScript_AccessListTech_value_57);
		local_ghostTechs04_TankArray = logic_uScript_AccessListTech_techList_57;
		local_ghostTech04_Tank = logic_uScript_AccessListTech_value_57;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_57.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_59()
	{
		int num = 0;
		Array array = external_18;
		if (logic_uScript_GetAndCheckTechs_techData_59.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_59, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_59, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_59 = owner_Connection_58;
		int num2 = 0;
		Array array2 = local_ghostTechs04_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_59.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_59, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_59, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_59 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_59.In(logic_uScript_GetAndCheckTechs_techData_59, logic_uScript_GetAndCheckTechs_ownerNode_59, ref logic_uScript_GetAndCheckTechs_techs_59);
		local_ghostTechs04_TankArray = logic_uScript_GetAndCheckTechs_techs_59;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_59.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_59.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_59.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_59.WaitingToSpawn;
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
			Relay_In_56();
		}
		if (waitingToSpawn)
		{
			Relay_In_56();
		}
	}

	private void Relay_In_60()
	{
		logic_uScript_SetTankInvulnerable_tank_60 = local_ghostTech04_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_60.In(logic_uScript_SetTankInvulnerable_invulnerable_60, logic_uScript_SetTankInvulnerable_tank_60);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_60.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_AtIndex_63()
	{
		int num = 0;
		Array array = local_ghostTechs05_TankArray;
		if (logic_uScript_AccessListTech_techList_63.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_63, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_63, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_63.AtIndex(ref logic_uScript_AccessListTech_techList_63, logic_uScript_AccessListTech_index_63, out logic_uScript_AccessListTech_value_63);
		local_ghostTechs05_TankArray = logic_uScript_AccessListTech_techList_63;
		local_ghostTech05_Tank = logic_uScript_AccessListTech_value_63;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_63.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_In_67()
	{
		int num = 0;
		Array array = external_24;
		if (logic_uScript_GetAndCheckTechs_techData_67.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_67, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_67, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_67 = owner_Connection_65;
		int num2 = 0;
		Array array2 = local_ghostTechs05_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_67.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_67, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_67, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_67 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_67.In(logic_uScript_GetAndCheckTechs_techData_67, logic_uScript_GetAndCheckTechs_ownerNode_67, ref logic_uScript_GetAndCheckTechs_techs_67);
		local_ghostTechs05_TankArray = logic_uScript_GetAndCheckTechs_techs_67;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_67.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_67.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_67.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_67.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_63();
		}
		if (someAlive)
		{
			Relay_AtIndex_63();
		}
		if (allDead)
		{
			Relay_In_68();
		}
		if (waitingToSpawn)
		{
			Relay_In_68();
		}
	}

	private void Relay_In_68()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_68.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_68.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_69()
	{
		logic_uScript_SetTankInvulnerable_tank_69 = local_ghostTech05_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_69.In(logic_uScript_SetTankInvulnerable_invulnerable_69, logic_uScript_SetTankInvulnerable_tank_69);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_69.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_72()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_In_74()
	{
		int num = 0;
		Array array = external_28;
		if (logic_uScript_GetAndCheckTechs_techData_74.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_74, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_74, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_74 = owner_Connection_73;
		int num2 = 0;
		Array array2 = local_ghostTechs06_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_74.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_74, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_74, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_74 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_74.In(logic_uScript_GetAndCheckTechs_techData_74, logic_uScript_GetAndCheckTechs_ownerNode_74, ref logic_uScript_GetAndCheckTechs_techs_74);
		local_ghostTechs06_TankArray = logic_uScript_GetAndCheckTechs_techs_74;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_74.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_74.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_74.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_74.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_76();
		}
		if (someAlive)
		{
			Relay_AtIndex_76();
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

	private void Relay_In_75()
	{
		logic_uScript_SetTankInvulnerable_tank_75 = local_ghostTech06_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_75.In(logic_uScript_SetTankInvulnerable_invulnerable_75, logic_uScript_SetTankInvulnerable_tank_75);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_75.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_AtIndex_76()
	{
		int num = 0;
		Array array = local_ghostTechs06_TankArray;
		if (logic_uScript_AccessListTech_techList_76.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_76, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_76, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_76.AtIndex(ref logic_uScript_AccessListTech_techList_76, logic_uScript_AccessListTech_index_76, out logic_uScript_AccessListTech_value_76);
		local_ghostTechs06_TankArray = logic_uScript_AccessListTech_techList_76;
		local_ghostTech06_Tank = logic_uScript_AccessListTech_value_76;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_76.Out)
		{
			Relay_In_75();
		}
	}

	private void Relay_In_77()
	{
		int num = 0;
		Array array = external_23;
		if (logic_uScript_GetAndCheckTechs_techData_77.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_77, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_77, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_77 = owner_Connection_81;
		int num2 = 0;
		Array array2 = local_ghostTechs07_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_77.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_77, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_77, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_77 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_77.In(logic_uScript_GetAndCheckTechs_techData_77, logic_uScript_GetAndCheckTechs_ownerNode_77, ref logic_uScript_GetAndCheckTechs_techs_77);
		local_ghostTechs07_TankArray = logic_uScript_GetAndCheckTechs_techs_77;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_77.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_77.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_77.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_77.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_78();
		}
		if (someAlive)
		{
			Relay_AtIndex_78();
		}
		if (allDead)
		{
			Relay_In_79();
		}
		if (waitingToSpawn)
		{
			Relay_In_79();
		}
	}

	private void Relay_AtIndex_78()
	{
		int num = 0;
		Array array = local_ghostTechs07_TankArray;
		if (logic_uScript_AccessListTech_techList_78.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_78, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_78, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_78.AtIndex(ref logic_uScript_AccessListTech_techList_78, logic_uScript_AccessListTech_index_78, out logic_uScript_AccessListTech_value_78);
		local_ghostTechs07_TankArray = logic_uScript_AccessListTech_techList_78;
		local_ghostTech07_Tank = logic_uScript_AccessListTech_value_78;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_78.Out)
		{
			Relay_In_82();
		}
	}

	private void Relay_In_79()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_79.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_79.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_In_82()
	{
		logic_uScript_SetTankInvulnerable_tank_82 = local_ghostTech07_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_82.In(logic_uScript_SetTankInvulnerable_invulnerable_82, logic_uScript_SetTankInvulnerable_tank_82);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_82.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_AtIndex_84()
	{
		int num = 0;
		Array array = local_ghostTechs08_TankArray;
		if (logic_uScript_AccessListTech_techList_84.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_84, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_84, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_84.AtIndex(ref logic_uScript_AccessListTech_techList_84, logic_uScript_AccessListTech_index_84, out logic_uScript_AccessListTech_value_84);
		local_ghostTechs08_TankArray = logic_uScript_AccessListTech_techList_84;
		local_ghostTech08_Tank = logic_uScript_AccessListTech_value_84;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_84.Out)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_85()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_85.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_85.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_88()
	{
		logic_uScript_SetTankInvulnerable_tank_88 = local_ghostTech08_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_88.In(logic_uScript_SetTankInvulnerable_invulnerable_88, logic_uScript_SetTankInvulnerable_tank_88);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_88.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_89()
	{
		int num = 0;
		Array array = external_20;
		if (logic_uScript_GetAndCheckTechs_techData_89.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_89, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_89, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_89 = owner_Connection_90;
		int num2 = 0;
		Array array2 = local_ghostTechs08_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_89.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_89, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_89, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_89 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.In(logic_uScript_GetAndCheckTechs_techData_89, logic_uScript_GetAndCheckTechs_ownerNode_89, ref logic_uScript_GetAndCheckTechs_techs_89);
		local_ghostTechs08_TankArray = logic_uScript_GetAndCheckTechs_techs_89;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_84();
		}
		if (someAlive)
		{
			Relay_AtIndex_84();
		}
		if (allDead)
		{
			Relay_In_85();
		}
		if (waitingToSpawn)
		{
			Relay_In_85();
		}
	}

	private void Relay_AtIndex_91()
	{
		int num = 0;
		Array array = local_ghostTechs09_TankArray;
		if (logic_uScript_AccessListTech_techList_91.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_91, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_91, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_91.AtIndex(ref logic_uScript_AccessListTech_techList_91, logic_uScript_AccessListTech_index_91, out logic_uScript_AccessListTech_value_91);
		local_ghostTechs09_TankArray = logic_uScript_AccessListTech_techList_91;
		local_ghostTech09_Tank = logic_uScript_AccessListTech_value_91;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_91.Out)
		{
			Relay_In_96();
		}
	}

	private void Relay_In_93()
	{
		int num = 0;
		Array array = external_35;
		if (logic_uScript_GetAndCheckTechs_techData_93.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_93, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_93, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_93 = owner_Connection_92;
		int num2 = 0;
		Array array2 = local_ghostTechs09_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_93.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_93, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_93, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_93 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_93.In(logic_uScript_GetAndCheckTechs_techData_93, logic_uScript_GetAndCheckTechs_ownerNode_93, ref logic_uScript_GetAndCheckTechs_techs_93);
		local_ghostTechs09_TankArray = logic_uScript_GetAndCheckTechs_techs_93;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_93.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_93.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_93.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_93.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_91();
		}
		if (someAlive)
		{
			Relay_AtIndex_91();
		}
		if (allDead)
		{
			Relay_In_95();
		}
		if (waitingToSpawn)
		{
			Relay_In_95();
		}
	}

	private void Relay_In_95()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_95.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_95.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_In_96()
	{
		logic_uScript_SetTankInvulnerable_tank_96 = local_ghostTech09_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_96.In(logic_uScript_SetTankInvulnerable_invulnerable_96, logic_uScript_SetTankInvulnerable_tank_96);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_96.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_In_99()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_99.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_99.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_100()
	{
		int num = 0;
		Array array = external_39;
		if (logic_uScript_GetAndCheckTechs_techData_100.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_100, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_100, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_100 = owner_Connection_102;
		int num2 = 0;
		Array array2 = local_ghostTechs10_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_100.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_100, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_100, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_100 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_100.In(logic_uScript_GetAndCheckTechs_techData_100, logic_uScript_GetAndCheckTechs_ownerNode_100, ref logic_uScript_GetAndCheckTechs_techs_100);
		local_ghostTechs10_TankArray = logic_uScript_GetAndCheckTechs_techs_100;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_100.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_100.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_100.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_100.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_101();
		}
		if (someAlive)
		{
			Relay_AtIndex_101();
		}
		if (allDead)
		{
			Relay_In_99();
		}
		if (waitingToSpawn)
		{
			Relay_In_99();
		}
	}

	private void Relay_AtIndex_101()
	{
		int num = 0;
		Array array = local_ghostTechs10_TankArray;
		if (logic_uScript_AccessListTech_techList_101.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_101, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_101, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_101.AtIndex(ref logic_uScript_AccessListTech_techList_101, logic_uScript_AccessListTech_index_101, out logic_uScript_AccessListTech_value_101);
		local_ghostTechs10_TankArray = logic_uScript_AccessListTech_techList_101;
		local_ghostTech10_Tank = logic_uScript_AccessListTech_value_101;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_101.Out)
		{
			Relay_In_103();
		}
	}

	private void Relay_In_103()
	{
		logic_uScript_SetTankInvulnerable_tank_103 = local_ghostTech10_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_103.In(logic_uScript_SetTankInvulnerable_invulnerable_103, logic_uScript_SetTankInvulnerable_tank_103);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_103.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_105()
	{
		int num = 0;
		Array array = external_34;
		if (logic_uScript_GetAndCheckTechs_techData_105.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_105, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_105, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_105 = owner_Connection_106;
		int num2 = 0;
		Array array2 = local_ghostTechs11_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_105.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_105, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_105, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_105 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105.In(logic_uScript_GetAndCheckTechs_techData_105, logic_uScript_GetAndCheckTechs_ownerNode_105, ref logic_uScript_GetAndCheckTechs_techs_105);
		local_ghostTechs11_TankArray = logic_uScript_GetAndCheckTechs_techs_105;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_110();
		}
		if (someAlive)
		{
			Relay_AtIndex_110();
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

	private void Relay_In_107()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.Out)
		{
			Relay_In_33();
		}
	}

	private void Relay_AtIndex_110()
	{
		int num = 0;
		Array array = local_ghostTechs11_TankArray;
		if (logic_uScript_AccessListTech_techList_110.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_110, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_110, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_110.AtIndex(ref logic_uScript_AccessListTech_techList_110, logic_uScript_AccessListTech_index_110, out logic_uScript_AccessListTech_value_110);
		local_ghostTechs11_TankArray = logic_uScript_AccessListTech_techList_110;
		local_ghostTech11_Tank = logic_uScript_AccessListTech_value_110;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_110.Out)
		{
			Relay_In_111();
		}
	}

	private void Relay_In_111()
	{
		logic_uScript_SetTankInvulnerable_tank_111 = local_ghostTech11_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_111.In(logic_uScript_SetTankInvulnerable_invulnerable_111, logic_uScript_SetTankInvulnerable_tank_111);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_111.Out)
		{
			Relay_In_33();
		}
	}
}
