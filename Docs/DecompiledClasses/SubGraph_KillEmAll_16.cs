using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_KillEmAll_16", "")]
[NodePath("Graphs")]
public class SubGraph_KillEmAll_16 : uScriptLogic
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

	private SpawnTechData[] external_39 = new SpawnTechData[0];

	private SpawnTechData[] external_48 = new SpawnTechData[0];

	private SpawnTechData[] external_38 = new SpawnTechData[0];

	private SpawnTechData[] external_32 = new SpawnTechData[0];

	private SpawnTechData[] external_36 = new SpawnTechData[0];

	private SpawnTechData[] external_55 = new SpawnTechData[0];

	private SpawnTechData[] external_51 = new SpawnTechData[0];

	private SpawnTechData[] external_53 = new SpawnTechData[0];

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

	private Tank local_ghostTech12_Tank;

	private Tank local_ghostTech13_Tank;

	private Tank local_ghostTech14_Tank;

	private Tank local_ghostTech15_Tank;

	private Tank local_ghostTech16_Tank;

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

	private Tank[] local_ghostTechs12_TankArray = new Tank[0];

	private Tank[] local_ghostTechs13_TankArray = new Tank[0];

	private Tank[] local_ghostTechs14_TankArray = new Tank[0];

	private Tank[] local_ghostTechs15_TankArray = new Tank[0];

	private Tank[] local_ghostTechs16_TankArray = new Tank[0];

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_12;

	private GameObject owner_Connection_14;

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_21;

	private GameObject owner_Connection_25;

	private GameObject owner_Connection_27;

	private GameObject owner_Connection_30;

	private GameObject owner_Connection_33;

	private GameObject owner_Connection_37;

	private GameObject owner_Connection_40;

	private GameObject owner_Connection_43;

	private GameObject owner_Connection_45;

	private GameObject owner_Connection_47;

	private GameObject owner_Connection_49;

	private GameObject owner_Connection_52;

	private GameObject owner_Connection_57;

	private GameObject owner_Connection_66;

	private GameObject owner_Connection_73;

	private GameObject owner_Connection_80;

	private GameObject owner_Connection_88;

	private GameObject owner_Connection_96;

	private GameObject owner_Connection_105;

	private GameObject owner_Connection_107;

	private GameObject owner_Connection_117;

	private GameObject owner_Connection_121;

	private GameObject owner_Connection_128;

	private GameObject owner_Connection_135;

	private GameObject owner_Connection_143;

	private GameObject owner_Connection_151;

	private GameObject owner_Connection_161;

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

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_34 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_34 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_34 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_34;

	private bool logic_uScript_DestroyTechsFromData_Out_34 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_35 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_35 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_35 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_35;

	private bool logic_uScript_DestroyTechsFromData_Out_35 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_41 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_41 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_41 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_41;

	private bool logic_uScript_DestroyTechsFromData_Out_41 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_42 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_42 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_42 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_42;

	private bool logic_uScript_DestroyTechsFromData_Out_42 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_44 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_44 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_44 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_44;

	private bool logic_uScript_DestroyTechsFromData_Out_44 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_46 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_46 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_46 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_46;

	private bool logic_uScript_DestroyTechsFromData_Out_46 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_50 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_50 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_50 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_50;

	private bool logic_uScript_DestroyTechsFromData_Out_50 = true;

	private uScript_DestroyTechsFromData logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_54 = new uScript_DestroyTechsFromData();

	private SpawnTechData[] logic_uScript_DestroyTechsFromData_techData_54 = new SpawnTechData[0];

	private bool logic_uScript_DestroyTechsFromData_shouldExplode_54 = true;

	private GameObject logic_uScript_DestroyTechsFromData_ownerNode_54;

	private bool logic_uScript_DestroyTechsFromData_Out_54 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_56 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_56 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_58 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_58;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_58 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_58;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_58 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_58 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_58 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_58 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_59 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_59;

	private Tank logic_uScript_SetTankInvulnerable_tank_59;

	private bool logic_uScript_SetTankInvulnerable_Out_59 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_60 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_60 = new Tank[0];

	private int logic_uScript_AccessListTech_index_60;

	private Tank logic_uScript_AccessListTech_value_60;

	private bool logic_uScript_AccessListTech_Out_60 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_61 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_61 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_64 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_64 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_65 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_65 = new Tank[0];

	private int logic_uScript_AccessListTech_index_65;

	private Tank logic_uScript_AccessListTech_value_65;

	private bool logic_uScript_AccessListTech_Out_65 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_68 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_68;

	private Tank logic_uScript_SetTankInvulnerable_tank_68;

	private bool logic_uScript_SetTankInvulnerable_Out_68 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_69 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_69;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_69 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_69;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_69 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_69 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_69 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_69 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_71 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_71 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_72 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_72 = new Tank[0];

	private int logic_uScript_AccessListTech_index_72;

	private Tank logic_uScript_AccessListTech_value_72;

	private bool logic_uScript_AccessListTech_Out_72 = true;

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

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_78 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_78 = new Tank[0];

	private int logic_uScript_AccessListTech_index_78;

	private Tank logic_uScript_AccessListTech_value_78;

	private bool logic_uScript_AccessListTech_Out_78 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_82 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_82;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_82 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_82;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_82 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_82 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_82 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_82 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_83 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_83 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_84 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_84;

	private Tank logic_uScript_SetTankInvulnerable_tank_84;

	private bool logic_uScript_SetTankInvulnerable_Out_84 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_87 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_89 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_89;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_89 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_89;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_89 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_89 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_89 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_89 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_90 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_90;

	private Tank logic_uScript_SetTankInvulnerable_tank_90;

	private bool logic_uScript_SetTankInvulnerable_Out_90 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_91 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_91 = new Tank[0];

	private int logic_uScript_AccessListTech_index_91;

	private Tank logic_uScript_AccessListTech_value_91;

	private bool logic_uScript_AccessListTech_Out_91 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_92 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_92 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_92;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_92 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_92;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_92 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_92 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_92 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_92 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_93 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_93 = new Tank[0];

	private int logic_uScript_AccessListTech_index_93;

	private Tank logic_uScript_AccessListTech_value_93;

	private bool logic_uScript_AccessListTech_Out_93 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_94 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_94 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_97 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_97;

	private Tank logic_uScript_SetTankInvulnerable_tank_97;

	private bool logic_uScript_SetTankInvulnerable_Out_97 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_99 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_99 = new Tank[0];

	private int logic_uScript_AccessListTech_index_99;

	private Tank logic_uScript_AccessListTech_value_99;

	private bool logic_uScript_AccessListTech_Out_99 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_100 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_100 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_103 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_103;

	private Tank logic_uScript_SetTankInvulnerable_tank_103;

	private bool logic_uScript_SetTankInvulnerable_Out_103 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_104 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_104 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_104;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_104 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_104;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_104 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_104 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_104 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_104 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_106 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_106 = new Tank[0];

	private int logic_uScript_AccessListTech_index_106;

	private Tank logic_uScript_AccessListTech_value_106;

	private bool logic_uScript_AccessListTech_Out_106 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_108 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_108 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_108;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_108 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_108;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_108 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_108 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_108 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_108 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_110 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_111 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_111;

	private Tank logic_uScript_SetTankInvulnerable_tank_111;

	private bool logic_uScript_SetTankInvulnerable_Out_111 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_114 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_114 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_115 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_115 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_115;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_115 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_115;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_115 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_115 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_115 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_115 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_116 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_116 = new Tank[0];

	private int logic_uScript_AccessListTech_index_116;

	private Tank logic_uScript_AccessListTech_value_116;

	private bool logic_uScript_AccessListTech_Out_116 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_118 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_118;

	private Tank logic_uScript_SetTankInvulnerable_tank_118;

	private bool logic_uScript_SetTankInvulnerable_Out_118 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_120 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_120 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_120;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_120 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_120;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_120 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_120 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_120 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_120 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_122 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_122 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_125 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_125 = new Tank[0];

	private int logic_uScript_AccessListTech_index_125;

	private Tank logic_uScript_AccessListTech_value_125;

	private bool logic_uScript_AccessListTech_Out_125 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_126 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_126;

	private Tank logic_uScript_SetTankInvulnerable_tank_126;

	private bool logic_uScript_SetTankInvulnerable_Out_126 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_127 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_127 = new Tank[0];

	private int logic_uScript_AccessListTech_index_127;

	private Tank logic_uScript_AccessListTech_value_127;

	private bool logic_uScript_AccessListTech_Out_127 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_129 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_129;

	private Tank logic_uScript_SetTankInvulnerable_tank_129;

	private bool logic_uScript_SetTankInvulnerable_Out_129 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_131 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_131 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_131;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_131 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_131;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_131 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_131 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_131 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_131 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_132 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_132 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_136 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_136;

	private Tank logic_uScript_SetTankInvulnerable_tank_136;

	private bool logic_uScript_SetTankInvulnerable_Out_136 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_137 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_137 = new Tank[0];

	private int logic_uScript_AccessListTech_index_137;

	private Tank logic_uScript_AccessListTech_value_137;

	private bool logic_uScript_AccessListTech_Out_137 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_138 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_138 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_138;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_138 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_138;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_138 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_138 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_138 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_138 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_140 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_140 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_141 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_141 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_144 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_144 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_144;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_144 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_144;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_144 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_144 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_144 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_144 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_146 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_146 = new Tank[0];

	private int logic_uScript_AccessListTech_index_146;

	private Tank logic_uScript_AccessListTech_value_146;

	private bool logic_uScript_AccessListTech_Out_146 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_147 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_147;

	private Tank logic_uScript_SetTankInvulnerable_tank_147;

	private bool logic_uScript_SetTankInvulnerable_Out_147 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_148 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_148 = new Tank[0];

	private int logic_uScript_AccessListTech_index_148;

	private Tank logic_uScript_AccessListTech_value_148;

	private bool logic_uScript_AccessListTech_Out_148 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_150 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_150;

	private Tank logic_uScript_SetTankInvulnerable_tank_150;

	private bool logic_uScript_SetTankInvulnerable_Out_150 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_152 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_152;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_152 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_152;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_152 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_152 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_152 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_152 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_153 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_153 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_155 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_155;

	private Tank logic_uScript_SetTankInvulnerable_tank_155;

	private bool logic_uScript_SetTankInvulnerable_Out_155 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_156 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_156 = new Tank[0];

	private int logic_uScript_AccessListTech_index_156;

	private Tank logic_uScript_AccessListTech_value_156;

	private bool logic_uScript_AccessListTech_Out_156 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_158 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_158 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_159 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_159 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_159;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_159 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_159;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_159 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_159 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_159 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_159 = true;

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
		if (null == owner_Connection_33 || !m_RegisteredForEvents)
		{
			owner_Connection_33 = parentGameObject;
		}
		if (null == owner_Connection_37 || !m_RegisteredForEvents)
		{
			owner_Connection_37 = parentGameObject;
		}
		if (null == owner_Connection_40 || !m_RegisteredForEvents)
		{
			owner_Connection_40 = parentGameObject;
		}
		if (null == owner_Connection_43 || !m_RegisteredForEvents)
		{
			owner_Connection_43 = parentGameObject;
		}
		if (null == owner_Connection_45 || !m_RegisteredForEvents)
		{
			owner_Connection_45 = parentGameObject;
		}
		if (null == owner_Connection_47 || !m_RegisteredForEvents)
		{
			owner_Connection_47 = parentGameObject;
		}
		if (null == owner_Connection_49 || !m_RegisteredForEvents)
		{
			owner_Connection_49 = parentGameObject;
		}
		if (null == owner_Connection_52 || !m_RegisteredForEvents)
		{
			owner_Connection_52 = parentGameObject;
		}
		if (null == owner_Connection_57 || !m_RegisteredForEvents)
		{
			owner_Connection_57 = parentGameObject;
		}
		if (null == owner_Connection_66 || !m_RegisteredForEvents)
		{
			owner_Connection_66 = parentGameObject;
		}
		if (null == owner_Connection_73 || !m_RegisteredForEvents)
		{
			owner_Connection_73 = parentGameObject;
		}
		if (null == owner_Connection_80 || !m_RegisteredForEvents)
		{
			owner_Connection_80 = parentGameObject;
		}
		if (null == owner_Connection_88 || !m_RegisteredForEvents)
		{
			owner_Connection_88 = parentGameObject;
		}
		if (null == owner_Connection_96 || !m_RegisteredForEvents)
		{
			owner_Connection_96 = parentGameObject;
		}
		if (null == owner_Connection_105 || !m_RegisteredForEvents)
		{
			owner_Connection_105 = parentGameObject;
		}
		if (null == owner_Connection_107 || !m_RegisteredForEvents)
		{
			owner_Connection_107 = parentGameObject;
		}
		if (null == owner_Connection_117 || !m_RegisteredForEvents)
		{
			owner_Connection_117 = parentGameObject;
		}
		if (null == owner_Connection_121 || !m_RegisteredForEvents)
		{
			owner_Connection_121 = parentGameObject;
		}
		if (null == owner_Connection_128 || !m_RegisteredForEvents)
		{
			owner_Connection_128 = parentGameObject;
		}
		if (null == owner_Connection_135 || !m_RegisteredForEvents)
		{
			owner_Connection_135 = parentGameObject;
		}
		if (null == owner_Connection_143 || !m_RegisteredForEvents)
		{
			owner_Connection_143 = parentGameObject;
		}
		if (null == owner_Connection_151 || !m_RegisteredForEvents)
		{
			owner_Connection_151 = parentGameObject;
		}
		if (null == owner_Connection_161 || !m_RegisteredForEvents)
		{
			owner_Connection_161 = parentGameObject;
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
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_34.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_35.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_41.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_42.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_44.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_46.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_50.SetParent(g);
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_54.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_56.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_59.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_60.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_61.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_64.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_65.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_68.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_71.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_72.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_74.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_75.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_78.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_83.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_84.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_90.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_91.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_92.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_93.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_94.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_97.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_99.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_100.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_103.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_104.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_106.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_108.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_111.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_114.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_115.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_116.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_118.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_120.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_122.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_125.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_126.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_127.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_129.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_131.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_132.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_136.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_137.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_138.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_140.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_141.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_144.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_146.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_147.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_148.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_150.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_153.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_155.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_156.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_158.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_159.SetParent(g);
		owner_Connection_5 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_12 = parentGameObject;
		owner_Connection_14 = parentGameObject;
		owner_Connection_17 = parentGameObject;
		owner_Connection_21 = parentGameObject;
		owner_Connection_25 = parentGameObject;
		owner_Connection_27 = parentGameObject;
		owner_Connection_30 = parentGameObject;
		owner_Connection_33 = parentGameObject;
		owner_Connection_37 = parentGameObject;
		owner_Connection_40 = parentGameObject;
		owner_Connection_43 = parentGameObject;
		owner_Connection_45 = parentGameObject;
		owner_Connection_47 = parentGameObject;
		owner_Connection_49 = parentGameObject;
		owner_Connection_52 = parentGameObject;
		owner_Connection_57 = parentGameObject;
		owner_Connection_66 = parentGameObject;
		owner_Connection_73 = parentGameObject;
		owner_Connection_80 = parentGameObject;
		owner_Connection_88 = parentGameObject;
		owner_Connection_96 = parentGameObject;
		owner_Connection_105 = parentGameObject;
		owner_Connection_107 = parentGameObject;
		owner_Connection_117 = parentGameObject;
		owner_Connection_121 = parentGameObject;
		owner_Connection_128 = parentGameObject;
		owner_Connection_135 = parentGameObject;
		owner_Connection_143 = parentGameObject;
		owner_Connection_151 = parentGameObject;
		owner_Connection_161 = parentGameObject;
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
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_59.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_68.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_75.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_84.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_90.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_97.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_103.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_111.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_118.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_126.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_129.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_136.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_147.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_150.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_155.OnDisable();
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
	public void In([FriendlyName("ghostTechSpawnData01", "")] SpawnTechData[] ghostTechSpawnData01, [FriendlyName("ghostTechSpawnData02", "")] SpawnTechData[] ghostTechSpawnData02, [FriendlyName("ghostTechSpawnData03", "")] SpawnTechData[] ghostTechSpawnData03, [FriendlyName("ghostTechSpawnData04", "")] SpawnTechData[] ghostTechSpawnData04, [FriendlyName("ghostTechSpawnData05", "")] SpawnTechData[] ghostTechSpawnData05, [FriendlyName("ghostTechSpawnData06", "")] SpawnTechData[] ghostTechSpawnData06, [FriendlyName("ghostTechSpawnData07", "")] SpawnTechData[] ghostTechSpawnData07, [FriendlyName("ghostTechSpawnData08", "")] SpawnTechData[] ghostTechSpawnData08, [FriendlyName("ghostTechSpawnData09", "")] SpawnTechData[] ghostTechSpawnData09, [FriendlyName("ghostTechSpawnData10", "")] SpawnTechData[] ghostTechSpawnData10, [FriendlyName("ghostTechSpawnData11", "")] SpawnTechData[] ghostTechSpawnData11, [FriendlyName("ghostTechSpawnData12", "")] SpawnTechData[] ghostTechSpawnData12, [FriendlyName("ghostTechSpawnData13", "")] SpawnTechData[] ghostTechSpawnData13, [FriendlyName("ghostTechSpawnData14", "")] SpawnTechData[] ghostTechSpawnData14, [FriendlyName("ghostTechSpawnData15", "")] SpawnTechData[] ghostTechSpawnData15, [FriendlyName("ghostTechSpawnData16", "")] SpawnTechData[] ghostTechSpawnData16)
	{
		external_4 = ghostTechSpawnData01;
		external_11 = ghostTechSpawnData02;
		external_15 = ghostTechSpawnData03;
		external_18 = ghostTechSpawnData04;
		external_24 = ghostTechSpawnData05;
		external_28 = ghostTechSpawnData06;
		external_23 = ghostTechSpawnData07;
		external_20 = ghostTechSpawnData08;
		external_39 = ghostTechSpawnData09;
		external_48 = ghostTechSpawnData10;
		external_38 = ghostTechSpawnData11;
		external_32 = ghostTechSpawnData12;
		external_36 = ghostTechSpawnData13;
		external_55 = ghostTechSpawnData14;
		external_51 = ghostTechSpawnData15;
		external_53 = ghostTechSpawnData16;
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
			Relay_In_56();
		}
		if (waitingToSpawn)
		{
			Relay_In_56();
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
			Relay_In_58();
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
			Relay_In_69();
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
			Relay_In_82();
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
			Relay_In_74();
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
			Relay_In_104();
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
			Relay_In_92();
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
			Relay_In_108();
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
			Relay_In_89();
		}
	}

	private void Relay_Connection_32()
	{
	}

	private void Relay_In_34()
	{
		int num = 0;
		Array array = external_36;
		if (logic_uScript_DestroyTechsFromData_techData_34.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_34, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_34, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_34 = owner_Connection_43;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_34.In(logic_uScript_DestroyTechsFromData_techData_34, logic_uScript_DestroyTechsFromData_shouldExplode_34, logic_uScript_DestroyTechsFromData_ownerNode_34);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_34.Out)
		{
			Relay_In_144();
		}
	}

	private void Relay_In_35()
	{
		int num = 0;
		Array array = external_38;
		if (logic_uScript_DestroyTechsFromData_techData_35.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_35, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_35, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_35 = owner_Connection_40;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_35.In(logic_uScript_DestroyTechsFromData_techData_35, logic_uScript_DestroyTechsFromData_shouldExplode_35, logic_uScript_DestroyTechsFromData_ownerNode_35);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_35.Out)
		{
			Relay_In_131();
		}
	}

	private void Relay_Connection_36()
	{
	}

	private void Relay_Connection_38()
	{
	}

	private void Relay_Connection_39()
	{
	}

	private void Relay_In_41()
	{
		int num = 0;
		Array array = external_55;
		if (logic_uScript_DestroyTechsFromData_techData_41.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_41, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_41, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_41 = owner_Connection_45;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_41.In(logic_uScript_DestroyTechsFromData_techData_41, logic_uScript_DestroyTechsFromData_shouldExplode_41, logic_uScript_DestroyTechsFromData_ownerNode_41);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_41.Out)
		{
			Relay_In_152();
		}
	}

	private void Relay_In_42()
	{
		int num = 0;
		Array array = external_53;
		if (logic_uScript_DestroyTechsFromData_techData_42.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_42, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_42, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_42 = owner_Connection_49;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_42.In(logic_uScript_DestroyTechsFromData_techData_42, logic_uScript_DestroyTechsFromData_shouldExplode_42, logic_uScript_DestroyTechsFromData_ownerNode_42);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_42.Out)
		{
			Relay_Connection_2();
		}
	}

	private void Relay_In_44()
	{
		int num = 0;
		Array array = external_48;
		if (logic_uScript_DestroyTechsFromData_techData_44.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_44, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_44, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_44 = owner_Connection_33;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_44.In(logic_uScript_DestroyTechsFromData_techData_44, logic_uScript_DestroyTechsFromData_shouldExplode_44, logic_uScript_DestroyTechsFromData_ownerNode_44);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_44.Out)
		{
			Relay_In_120();
		}
	}

	private void Relay_In_46()
	{
		int num = 0;
		Array array = external_51;
		if (logic_uScript_DestroyTechsFromData_techData_46.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_46, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_46, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_46 = owner_Connection_37;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_46.In(logic_uScript_DestroyTechsFromData_techData_46, logic_uScript_DestroyTechsFromData_shouldExplode_46, logic_uScript_DestroyTechsFromData_ownerNode_46);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_46.Out)
		{
			Relay_In_159();
		}
	}

	private void Relay_Connection_48()
	{
	}

	private void Relay_In_50()
	{
		int num = 0;
		Array array = external_32;
		if (logic_uScript_DestroyTechsFromData_techData_50.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_50, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_50, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_50 = owner_Connection_52;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_50.In(logic_uScript_DestroyTechsFromData_techData_50, logic_uScript_DestroyTechsFromData_shouldExplode_50, logic_uScript_DestroyTechsFromData_ownerNode_50);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_50.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_Connection_51()
	{
	}

	private void Relay_Connection_53()
	{
	}

	private void Relay_In_54()
	{
		int num = 0;
		Array array = external_39;
		if (logic_uScript_DestroyTechsFromData_techData_54.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_DestroyTechsFromData_techData_54, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_DestroyTechsFromData_techData_54, num, array.Length);
		num += array.Length;
		logic_uScript_DestroyTechsFromData_ownerNode_54 = owner_Connection_47;
		logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_54.In(logic_uScript_DestroyTechsFromData_techData_54, logic_uScript_DestroyTechsFromData_shouldExplode_54, logic_uScript_DestroyTechsFromData_ownerNode_54);
		if (logic_uScript_DestroyTechsFromData_uScript_DestroyTechsFromData_54.Out)
		{
			Relay_In_115();
		}
	}

	private void Relay_Connection_55()
	{
	}

	private void Relay_In_56()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_56.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_56.Out)
		{
			Relay_In_6();
		}
	}

	private void Relay_In_58()
	{
		int num = 0;
		Array array = external_11;
		if (logic_uScript_GetAndCheckTechs_techData_58.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_58, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_58, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_58 = owner_Connection_57;
		int num2 = 0;
		Array array2 = local_ghostTechs02_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_58.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_58, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_58, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_58 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.In(logic_uScript_GetAndCheckTechs_techData_58, logic_uScript_GetAndCheckTechs_ownerNode_58, ref logic_uScript_GetAndCheckTechs_techs_58);
		local_ghostTechs02_TankArray = logic_uScript_GetAndCheckTechs_techs_58;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_60();
		}
		if (someAlive)
		{
			Relay_AtIndex_60();
		}
		if (allDead)
		{
			Relay_In_61();
		}
		if (waitingToSpawn)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_59()
	{
		logic_uScript_SetTankInvulnerable_tank_59 = local_ghostTech02_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_59.In(logic_uScript_SetTankInvulnerable_invulnerable_59, logic_uScript_SetTankInvulnerable_tank_59);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_59.Out)
		{
			Relay_In_13();
		}
	}

	private void Relay_AtIndex_60()
	{
		int num = 0;
		Array array = local_ghostTechs02_TankArray;
		if (logic_uScript_AccessListTech_techList_60.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_60, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_60, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_60.AtIndex(ref logic_uScript_AccessListTech_techList_60, logic_uScript_AccessListTech_index_60, out logic_uScript_AccessListTech_value_60);
		local_ghostTechs02_TankArray = logic_uScript_AccessListTech_techList_60;
		local_ghostTech02_Tank = logic_uScript_AccessListTech_value_60;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_60.Out)
		{
			Relay_In_59();
		}
	}

	private void Relay_In_61()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_61.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_61.Out)
		{
			Relay_In_13();
		}
	}

	private void Relay_In_64()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_64.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_64.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_AtIndex_65()
	{
		int num = 0;
		Array array = local_ghostTechs03_TankArray;
		if (logic_uScript_AccessListTech_techList_65.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_65, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_65, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_65.AtIndex(ref logic_uScript_AccessListTech_techList_65, logic_uScript_AccessListTech_index_65, out logic_uScript_AccessListTech_value_65);
		local_ghostTechs03_TankArray = logic_uScript_AccessListTech_techList_65;
		local_ghostTech03_Tank = logic_uScript_AccessListTech_value_65;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_65.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_In_68()
	{
		logic_uScript_SetTankInvulnerable_tank_68 = local_ghostTech03_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_68.In(logic_uScript_SetTankInvulnerable_invulnerable_68, logic_uScript_SetTankInvulnerable_tank_68);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_68.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_In_69()
	{
		int num = 0;
		Array array = external_15;
		if (logic_uScript_GetAndCheckTechs_techData_69.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_69, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_69, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_69 = owner_Connection_66;
		int num2 = 0;
		Array array2 = local_ghostTechs03_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_69.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_69, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_69, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_69 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69.In(logic_uScript_GetAndCheckTechs_techData_69, logic_uScript_GetAndCheckTechs_ownerNode_69, ref logic_uScript_GetAndCheckTechs_techs_69);
		local_ghostTechs03_TankArray = logic_uScript_GetAndCheckTechs_techs_69;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_69.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_65();
		}
		if (someAlive)
		{
			Relay_AtIndex_65();
		}
		if (allDead)
		{
			Relay_In_64();
		}
		if (waitingToSpawn)
		{
			Relay_In_64();
		}
	}

	private void Relay_In_71()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_71.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_71.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_AtIndex_72()
	{
		int num = 0;
		Array array = local_ghostTechs04_TankArray;
		if (logic_uScript_AccessListTech_techList_72.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_72, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_72, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_72.AtIndex(ref logic_uScript_AccessListTech_techList_72, logic_uScript_AccessListTech_index_72, out logic_uScript_AccessListTech_value_72);
		local_ghostTechs04_TankArray = logic_uScript_AccessListTech_techList_72;
		local_ghostTech04_Tank = logic_uScript_AccessListTech_value_72;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_72.Out)
		{
			Relay_In_75();
		}
	}

	private void Relay_In_74()
	{
		int num = 0;
		Array array = external_18;
		if (logic_uScript_GetAndCheckTechs_techData_74.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_74, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_74, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_74 = owner_Connection_73;
		int num2 = 0;
		Array array2 = local_ghostTechs04_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_74.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_74, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_74, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_74 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_74.In(logic_uScript_GetAndCheckTechs_techData_74, logic_uScript_GetAndCheckTechs_ownerNode_74, ref logic_uScript_GetAndCheckTechs_techs_74);
		local_ghostTechs04_TankArray = logic_uScript_GetAndCheckTechs_techs_74;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_74.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_74.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_74.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_74.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_72();
		}
		if (someAlive)
		{
			Relay_AtIndex_72();
		}
		if (allDead)
		{
			Relay_In_71();
		}
		if (waitingToSpawn)
		{
			Relay_In_71();
		}
	}

	private void Relay_In_75()
	{
		logic_uScript_SetTankInvulnerable_tank_75 = local_ghostTech04_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_75.In(logic_uScript_SetTankInvulnerable_invulnerable_75, logic_uScript_SetTankInvulnerable_tank_75);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_75.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_AtIndex_78()
	{
		int num = 0;
		Array array = local_ghostTechs05_TankArray;
		if (logic_uScript_AccessListTech_techList_78.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_78, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_78, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_78.AtIndex(ref logic_uScript_AccessListTech_techList_78, logic_uScript_AccessListTech_index_78, out logic_uScript_AccessListTech_value_78);
		local_ghostTechs05_TankArray = logic_uScript_AccessListTech_techList_78;
		local_ghostTech05_Tank = logic_uScript_AccessListTech_value_78;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_78.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_In_82()
	{
		int num = 0;
		Array array = external_24;
		if (logic_uScript_GetAndCheckTechs_techData_82.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_82, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_82, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_82 = owner_Connection_80;
		int num2 = 0;
		Array array2 = local_ghostTechs05_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_82.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_82, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_82, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_82 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.In(logic_uScript_GetAndCheckTechs_techData_82, logic_uScript_GetAndCheckTechs_ownerNode_82, ref logic_uScript_GetAndCheckTechs_techs_82);
		local_ghostTechs05_TankArray = logic_uScript_GetAndCheckTechs_techs_82;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.WaitingToSpawn;
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
			Relay_In_83();
		}
		if (waitingToSpawn)
		{
			Relay_In_83();
		}
	}

	private void Relay_In_83()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_83.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_83.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_84()
	{
		logic_uScript_SetTankInvulnerable_tank_84 = local_ghostTech05_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_84.In(logic_uScript_SetTankInvulnerable_invulnerable_84, logic_uScript_SetTankInvulnerable_tank_84);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_84.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_87()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_In_89()
	{
		int num = 0;
		Array array = external_28;
		if (logic_uScript_GetAndCheckTechs_techData_89.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_89, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_89, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_89 = owner_Connection_88;
		int num2 = 0;
		Array array2 = local_ghostTechs06_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_89.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_89, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_89, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_89 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.In(logic_uScript_GetAndCheckTechs_techData_89, logic_uScript_GetAndCheckTechs_ownerNode_89, ref logic_uScript_GetAndCheckTechs_techs_89);
		local_ghostTechs06_TankArray = logic_uScript_GetAndCheckTechs_techs_89;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.WaitingToSpawn;
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
			Relay_In_87();
		}
		if (waitingToSpawn)
		{
			Relay_In_87();
		}
	}

	private void Relay_In_90()
	{
		logic_uScript_SetTankInvulnerable_tank_90 = local_ghostTech06_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_90.In(logic_uScript_SetTankInvulnerable_invulnerable_90, logic_uScript_SetTankInvulnerable_tank_90);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_90.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_AtIndex_91()
	{
		int num = 0;
		Array array = local_ghostTechs06_TankArray;
		if (logic_uScript_AccessListTech_techList_91.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_91, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_91, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_91.AtIndex(ref logic_uScript_AccessListTech_techList_91, logic_uScript_AccessListTech_index_91, out logic_uScript_AccessListTech_value_91);
		local_ghostTechs06_TankArray = logic_uScript_AccessListTech_techList_91;
		local_ghostTech06_Tank = logic_uScript_AccessListTech_value_91;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_91.Out)
		{
			Relay_In_90();
		}
	}

	private void Relay_In_92()
	{
		int num = 0;
		Array array = external_23;
		if (logic_uScript_GetAndCheckTechs_techData_92.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_92, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_92, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_92 = owner_Connection_96;
		int num2 = 0;
		Array array2 = local_ghostTechs07_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_92.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_92, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_92, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_92 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_92.In(logic_uScript_GetAndCheckTechs_techData_92, logic_uScript_GetAndCheckTechs_ownerNode_92, ref logic_uScript_GetAndCheckTechs_techs_92);
		local_ghostTechs07_TankArray = logic_uScript_GetAndCheckTechs_techs_92;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_92.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_92.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_92.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_92.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_93();
		}
		if (someAlive)
		{
			Relay_AtIndex_93();
		}
		if (allDead)
		{
			Relay_In_94();
		}
		if (waitingToSpawn)
		{
			Relay_In_94();
		}
	}

	private void Relay_AtIndex_93()
	{
		int num = 0;
		Array array = local_ghostTechs07_TankArray;
		if (logic_uScript_AccessListTech_techList_93.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_93, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_93, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_93.AtIndex(ref logic_uScript_AccessListTech_techList_93, logic_uScript_AccessListTech_index_93, out logic_uScript_AccessListTech_value_93);
		local_ghostTechs07_TankArray = logic_uScript_AccessListTech_techList_93;
		local_ghostTech07_Tank = logic_uScript_AccessListTech_value_93;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_93.Out)
		{
			Relay_In_97();
		}
	}

	private void Relay_In_94()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_94.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_94.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_In_97()
	{
		logic_uScript_SetTankInvulnerable_tank_97 = local_ghostTech07_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_97.In(logic_uScript_SetTankInvulnerable_invulnerable_97, logic_uScript_SetTankInvulnerable_tank_97);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_97.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_AtIndex_99()
	{
		int num = 0;
		Array array = local_ghostTechs08_TankArray;
		if (logic_uScript_AccessListTech_techList_99.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_99, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_99, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_99.AtIndex(ref logic_uScript_AccessListTech_techList_99, logic_uScript_AccessListTech_index_99, out logic_uScript_AccessListTech_value_99);
		local_ghostTechs08_TankArray = logic_uScript_AccessListTech_techList_99;
		local_ghostTech08_Tank = logic_uScript_AccessListTech_value_99;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_99.Out)
		{
			Relay_In_103();
		}
	}

	private void Relay_In_100()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_100.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_100.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_103()
	{
		logic_uScript_SetTankInvulnerable_tank_103 = local_ghostTech08_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_103.In(logic_uScript_SetTankInvulnerable_invulnerable_103, logic_uScript_SetTankInvulnerable_tank_103);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_103.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_104()
	{
		int num = 0;
		Array array = external_20;
		if (logic_uScript_GetAndCheckTechs_techData_104.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_104, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_104, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_104 = owner_Connection_105;
		int num2 = 0;
		Array array2 = local_ghostTechs08_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_104.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_104, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_104, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_104 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_104.In(logic_uScript_GetAndCheckTechs_techData_104, logic_uScript_GetAndCheckTechs_ownerNode_104, ref logic_uScript_GetAndCheckTechs_techs_104);
		local_ghostTechs08_TankArray = logic_uScript_GetAndCheckTechs_techs_104;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_104.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_104.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_104.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_104.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_99();
		}
		if (someAlive)
		{
			Relay_AtIndex_99();
		}
		if (allDead)
		{
			Relay_In_100();
		}
		if (waitingToSpawn)
		{
			Relay_In_100();
		}
	}

	private void Relay_AtIndex_106()
	{
		int num = 0;
		Array array = local_ghostTechs09_TankArray;
		if (logic_uScript_AccessListTech_techList_106.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_106, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_106, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_106.AtIndex(ref logic_uScript_AccessListTech_techList_106, logic_uScript_AccessListTech_index_106, out logic_uScript_AccessListTech_value_106);
		local_ghostTechs09_TankArray = logic_uScript_AccessListTech_techList_106;
		local_ghostTech09_Tank = logic_uScript_AccessListTech_value_106;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_106.Out)
		{
			Relay_In_111();
		}
	}

	private void Relay_In_108()
	{
		int num = 0;
		Array array = external_39;
		if (logic_uScript_GetAndCheckTechs_techData_108.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_108, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_108, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_108 = owner_Connection_107;
		int num2 = 0;
		Array array2 = local_ghostTechs09_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_108.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_108, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_108, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_108 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_108.In(logic_uScript_GetAndCheckTechs_techData_108, logic_uScript_GetAndCheckTechs_ownerNode_108, ref logic_uScript_GetAndCheckTechs_techs_108);
		local_ghostTechs09_TankArray = logic_uScript_GetAndCheckTechs_techs_108;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_108.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_108.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_108.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_108.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_106();
		}
		if (someAlive)
		{
			Relay_AtIndex_106();
		}
		if (allDead)
		{
			Relay_In_110();
		}
		if (waitingToSpawn)
		{
			Relay_In_110();
		}
	}

	private void Relay_In_110()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_111()
	{
		logic_uScript_SetTankInvulnerable_tank_111 = local_ghostTech09_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_111.In(logic_uScript_SetTankInvulnerable_invulnerable_111, logic_uScript_SetTankInvulnerable_tank_111);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_111.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_114()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_114.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_114.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_115()
	{
		int num = 0;
		Array array = external_48;
		if (logic_uScript_GetAndCheckTechs_techData_115.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_115, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_115, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_115 = owner_Connection_117;
		int num2 = 0;
		Array array2 = local_ghostTechs10_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_115.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_115, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_115, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_115 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_115.In(logic_uScript_GetAndCheckTechs_techData_115, logic_uScript_GetAndCheckTechs_ownerNode_115, ref logic_uScript_GetAndCheckTechs_techs_115);
		local_ghostTechs10_TankArray = logic_uScript_GetAndCheckTechs_techs_115;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_115.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_115.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_115.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_115.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_116();
		}
		if (someAlive)
		{
			Relay_AtIndex_116();
		}
		if (allDead)
		{
			Relay_In_114();
		}
		if (waitingToSpawn)
		{
			Relay_In_114();
		}
	}

	private void Relay_AtIndex_116()
	{
		int num = 0;
		Array array = local_ghostTechs10_TankArray;
		if (logic_uScript_AccessListTech_techList_116.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_116, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_116, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_116.AtIndex(ref logic_uScript_AccessListTech_techList_116, logic_uScript_AccessListTech_index_116, out logic_uScript_AccessListTech_value_116);
		local_ghostTechs10_TankArray = logic_uScript_AccessListTech_techList_116;
		local_ghostTech10_Tank = logic_uScript_AccessListTech_value_116;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_116.Out)
		{
			Relay_In_118();
		}
	}

	private void Relay_In_118()
	{
		logic_uScript_SetTankInvulnerable_tank_118 = local_ghostTech10_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_118.In(logic_uScript_SetTankInvulnerable_invulnerable_118, logic_uScript_SetTankInvulnerable_tank_118);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_118.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_120()
	{
		int num = 0;
		Array array = external_38;
		if (logic_uScript_GetAndCheckTechs_techData_120.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_120, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_120, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_120 = owner_Connection_121;
		int num2 = 0;
		Array array2 = local_ghostTechs11_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_120.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_120, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_120, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_120 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_120.In(logic_uScript_GetAndCheckTechs_techData_120, logic_uScript_GetAndCheckTechs_ownerNode_120, ref logic_uScript_GetAndCheckTechs_techs_120);
		local_ghostTechs11_TankArray = logic_uScript_GetAndCheckTechs_techs_120;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_120.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_120.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_120.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_120.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_125();
		}
		if (someAlive)
		{
			Relay_AtIndex_125();
		}
		if (allDead)
		{
			Relay_In_122();
		}
		if (waitingToSpawn)
		{
			Relay_In_122();
		}
	}

	private void Relay_In_122()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_122.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_122.Out)
		{
			Relay_In_35();
		}
	}

	private void Relay_AtIndex_125()
	{
		int num = 0;
		Array array = local_ghostTechs11_TankArray;
		if (logic_uScript_AccessListTech_techList_125.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_125, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_125, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_125.AtIndex(ref logic_uScript_AccessListTech_techList_125, logic_uScript_AccessListTech_index_125, out logic_uScript_AccessListTech_value_125);
		local_ghostTechs11_TankArray = logic_uScript_AccessListTech_techList_125;
		local_ghostTech11_Tank = logic_uScript_AccessListTech_value_125;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_125.Out)
		{
			Relay_In_126();
		}
	}

	private void Relay_In_126()
	{
		logic_uScript_SetTankInvulnerable_tank_126 = local_ghostTech11_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_126.In(logic_uScript_SetTankInvulnerable_invulnerable_126, logic_uScript_SetTankInvulnerable_tank_126);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_126.Out)
		{
			Relay_In_35();
		}
	}

	private void Relay_AtIndex_127()
	{
		int num = 0;
		Array array = local_ghostTechs12_TankArray;
		if (logic_uScript_AccessListTech_techList_127.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_127, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_127, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_127.AtIndex(ref logic_uScript_AccessListTech_techList_127, logic_uScript_AccessListTech_index_127, out logic_uScript_AccessListTech_value_127);
		local_ghostTechs12_TankArray = logic_uScript_AccessListTech_techList_127;
		local_ghostTech12_Tank = logic_uScript_AccessListTech_value_127;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_127.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_In_129()
	{
		logic_uScript_SetTankInvulnerable_tank_129 = local_ghostTech12_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_129.In(logic_uScript_SetTankInvulnerable_invulnerable_129, logic_uScript_SetTankInvulnerable_tank_129);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_129.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_131()
	{
		int num = 0;
		Array array = external_32;
		if (logic_uScript_GetAndCheckTechs_techData_131.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_131, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_131, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_131 = owner_Connection_128;
		int num2 = 0;
		Array array2 = local_ghostTechs12_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_131.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_131, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_131, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_131 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_131.In(logic_uScript_GetAndCheckTechs_techData_131, logic_uScript_GetAndCheckTechs_ownerNode_131, ref logic_uScript_GetAndCheckTechs_techs_131);
		local_ghostTechs12_TankArray = logic_uScript_GetAndCheckTechs_techs_131;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_131.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_131.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_131.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_131.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_127();
		}
		if (someAlive)
		{
			Relay_AtIndex_127();
		}
		if (allDead)
		{
			Relay_In_132();
		}
		if (waitingToSpawn)
		{
			Relay_In_132();
		}
	}

	private void Relay_In_132()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_132.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_132.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_136()
	{
		logic_uScript_SetTankInvulnerable_tank_136 = local_ghostTech13_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_136.In(logic_uScript_SetTankInvulnerable_invulnerable_136, logic_uScript_SetTankInvulnerable_tank_136);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_136.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_AtIndex_137()
	{
		int num = 0;
		Array array = local_ghostTechs13_TankArray;
		if (logic_uScript_AccessListTech_techList_137.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_137, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_137, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_137.AtIndex(ref logic_uScript_AccessListTech_techList_137, logic_uScript_AccessListTech_index_137, out logic_uScript_AccessListTech_value_137);
		local_ghostTechs13_TankArray = logic_uScript_AccessListTech_techList_137;
		local_ghostTech13_Tank = logic_uScript_AccessListTech_value_137;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_137.Out)
		{
			Relay_In_136();
		}
	}

	private void Relay_In_138()
	{
		int num = 0;
		Array array = external_36;
		if (logic_uScript_GetAndCheckTechs_techData_138.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_138, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_138, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_138 = owner_Connection_135;
		int num2 = 0;
		Array array2 = local_ghostTechs13_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_138.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_138, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_138, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_138 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_138.In(logic_uScript_GetAndCheckTechs_techData_138, logic_uScript_GetAndCheckTechs_ownerNode_138, ref logic_uScript_GetAndCheckTechs_techs_138);
		local_ghostTechs13_TankArray = logic_uScript_GetAndCheckTechs_techs_138;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_138.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_138.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_138.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_138.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_137();
		}
		if (someAlive)
		{
			Relay_AtIndex_137();
		}
		if (allDead)
		{
			Relay_In_140();
		}
		if (waitingToSpawn)
		{
			Relay_In_140();
		}
	}

	private void Relay_In_140()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_140.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_140.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_In_141()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_141.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_141.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_144()
	{
		int num = 0;
		Array array = external_55;
		if (logic_uScript_GetAndCheckTechs_techData_144.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_144, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_144, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_144 = owner_Connection_143;
		int num2 = 0;
		Array array2 = local_ghostTechs14_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_144.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_144, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_144, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_144 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_144.In(logic_uScript_GetAndCheckTechs_techData_144, logic_uScript_GetAndCheckTechs_ownerNode_144, ref logic_uScript_GetAndCheckTechs_techs_144);
		local_ghostTechs14_TankArray = logic_uScript_GetAndCheckTechs_techs_144;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_144.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_144.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_144.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_144.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_146();
		}
		if (someAlive)
		{
			Relay_AtIndex_146();
		}
		if (allDead)
		{
			Relay_In_141();
		}
		if (waitingToSpawn)
		{
			Relay_In_141();
		}
	}

	private void Relay_AtIndex_146()
	{
		int num = 0;
		Array array = local_ghostTechs14_TankArray;
		if (logic_uScript_AccessListTech_techList_146.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_146, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_146, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_146.AtIndex(ref logic_uScript_AccessListTech_techList_146, logic_uScript_AccessListTech_index_146, out logic_uScript_AccessListTech_value_146);
		local_ghostTechs14_TankArray = logic_uScript_AccessListTech_techList_146;
		local_ghostTech14_Tank = logic_uScript_AccessListTech_value_146;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_146.Out)
		{
			Relay_In_147();
		}
	}

	private void Relay_In_147()
	{
		logic_uScript_SetTankInvulnerable_tank_147 = local_ghostTech14_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_147.In(logic_uScript_SetTankInvulnerable_invulnerable_147, logic_uScript_SetTankInvulnerable_tank_147);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_147.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_AtIndex_148()
	{
		int num = 0;
		Array array = local_ghostTechs15_TankArray;
		if (logic_uScript_AccessListTech_techList_148.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_148, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_148, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_148.AtIndex(ref logic_uScript_AccessListTech_techList_148, logic_uScript_AccessListTech_index_148, out logic_uScript_AccessListTech_value_148);
		local_ghostTechs15_TankArray = logic_uScript_AccessListTech_techList_148;
		local_ghostTech15_Tank = logic_uScript_AccessListTech_value_148;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_148.Out)
		{
			Relay_In_150();
		}
	}

	private void Relay_In_150()
	{
		logic_uScript_SetTankInvulnerable_tank_150 = local_ghostTech15_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_150.In(logic_uScript_SetTankInvulnerable_invulnerable_150, logic_uScript_SetTankInvulnerable_tank_150);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_150.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_152()
	{
		int num = 0;
		Array array = external_51;
		if (logic_uScript_GetAndCheckTechs_techData_152.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_152, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_152, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_152 = owner_Connection_151;
		int num2 = 0;
		Array array2 = local_ghostTechs15_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_152.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_152, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_152, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_152 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152.In(logic_uScript_GetAndCheckTechs_techData_152, logic_uScript_GetAndCheckTechs_ownerNode_152, ref logic_uScript_GetAndCheckTechs_techs_152);
		local_ghostTechs15_TankArray = logic_uScript_GetAndCheckTechs_techs_152;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_152.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_148();
		}
		if (someAlive)
		{
			Relay_AtIndex_148();
		}
		if (allDead)
		{
			Relay_In_153();
		}
		if (waitingToSpawn)
		{
			Relay_In_153();
		}
	}

	private void Relay_In_153()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_153.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_153.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_155()
	{
		logic_uScript_SetTankInvulnerable_tank_155 = local_ghostTech16_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_155.In(logic_uScript_SetTankInvulnerable_invulnerable_155, logic_uScript_SetTankInvulnerable_tank_155);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_155.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_AtIndex_156()
	{
		int num = 0;
		Array array = local_ghostTechs16_TankArray;
		if (logic_uScript_AccessListTech_techList_156.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_156, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_156, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_156.AtIndex(ref logic_uScript_AccessListTech_techList_156, logic_uScript_AccessListTech_index_156, out logic_uScript_AccessListTech_value_156);
		local_ghostTechs16_TankArray = logic_uScript_AccessListTech_techList_156;
		local_ghostTech16_Tank = logic_uScript_AccessListTech_value_156;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_156.Out)
		{
			Relay_In_155();
		}
	}

	private void Relay_In_158()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_158.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_158.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_159()
	{
		int num = 0;
		Array array = external_53;
		if (logic_uScript_GetAndCheckTechs_techData_159.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_159, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_159, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_159 = owner_Connection_161;
		int num2 = 0;
		Array array2 = local_ghostTechs16_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_159.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_159, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_159, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_159 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_159.In(logic_uScript_GetAndCheckTechs_techData_159, logic_uScript_GetAndCheckTechs_ownerNode_159, ref logic_uScript_GetAndCheckTechs_techs_159);
		local_ghostTechs16_TankArray = logic_uScript_GetAndCheckTechs_techs_159;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_159.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_159.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_159.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_159.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_156();
		}
		if (someAlive)
		{
			Relay_AtIndex_156();
		}
		if (allDead)
		{
			Relay_In_158();
		}
		if (waitingToSpawn)
		{
			Relay_In_158();
		}
	}
}
