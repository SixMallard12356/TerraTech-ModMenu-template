using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_Crafting_Tutorial_Finish", "")]
public class SubGraph_Crafting_Tutorial_Finish : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private Tank external_7;

	private Tank external_9;

	private ExternalBehaviorTree external_10;

	private Transform external_11;

	private GameObject owner_Connection_3;

	private uScript_ClearCustomRadarTeamID logic_uScript_ClearCustomRadarTeamID_uScript_ClearCustomRadarTeamID_0 = new uScript_ClearCustomRadarTeamID();

	private Tank logic_uScript_ClearCustomRadarTeamID_tech_0;

	private bool logic_uScript_ClearCustomRadarTeamID_Out_0 = true;

	private uScript_ClearTutorialTechToBuild logic_uScript_ClearTutorialTechToBuild_uScript_ClearTutorialTechToBuild_1 = new uScript_ClearTutorialTechToBuild();

	private bool logic_uScript_ClearTutorialTechToBuild_Out_1 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_2 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_2;

	private Tank logic_uScript_SetTankInvulnerable_tank_2;

	private bool logic_uScript_SetTankInvulnerable_Out_2 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_4 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_4;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_4 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_4 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_4;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_4;

	private bool logic_uScript_FlyTechUpAndAway_Out_4 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_5 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_5;

	private bool logic_uScript_FinishEncounter_Out_5 = true;

	private uScript_ClearItemPickup logic_uScript_ClearItemPickup_uScript_ClearItemPickup_13 = new uScript_ClearItemPickup();

	private Tank logic_uScript_ClearItemPickup_tech_13;

	private bool logic_uScript_ClearItemPickup_Out_13 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_15 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_15;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_15;

	private bool logic_uScript_SetTankHideBlockLimit_Out_15 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
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
		logic_uScript_ClearCustomRadarTeamID_uScript_ClearCustomRadarTeamID_0.SetParent(g);
		logic_uScript_ClearTutorialTechToBuild_uScript_ClearTutorialTechToBuild_1.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_2.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_4.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_5.SetParent(g);
		logic_uScript_ClearItemPickup_uScript_ClearItemPickup_13.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_15.SetParent(g);
		owner_Connection_3 = parentGameObject;
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
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_2.OnDisable();
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
	public void In([FriendlyName("craftingBaseTech", "")] Tank craftingBaseTech, [FriendlyName("NPCTech", "")] Tank NPCTech, [FriendlyName("NPCFlyAwayAI", "")] ExternalBehaviorTree NPCFlyAwayAI, [FriendlyName("NPCDespawnParticleEffect", "")] Transform NPCDespawnParticleEffect)
	{
		external_7 = craftingBaseTech;
		external_9 = NPCTech;
		external_10 = NPCFlyAwayAI;
		external_11 = NPCDespawnParticleEffect;
		Relay_In_1();
	}

	private void Relay_In_0()
	{
		logic_uScript_ClearCustomRadarTeamID_tech_0 = external_7;
		logic_uScript_ClearCustomRadarTeamID_uScript_ClearCustomRadarTeamID_0.In(logic_uScript_ClearCustomRadarTeamID_tech_0);
		if (logic_uScript_ClearCustomRadarTeamID_uScript_ClearCustomRadarTeamID_0.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_1()
	{
		logic_uScript_ClearTutorialTechToBuild_uScript_ClearTutorialTechToBuild_1.In();
		if (logic_uScript_ClearTutorialTechToBuild_uScript_ClearTutorialTechToBuild_1.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_2()
	{
		logic_uScript_SetTankInvulnerable_tank_2 = external_7;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_2.In(logic_uScript_SetTankInvulnerable_invulnerable_2, logic_uScript_SetTankInvulnerable_tank_2);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_2.Out)
		{
			Relay_In_0();
		}
	}

	private void Relay_In_4()
	{
		logic_uScript_FlyTechUpAndAway_tech_4 = external_9;
		logic_uScript_FlyTechUpAndAway_aiTree_4 = external_10;
		logic_uScript_FlyTechUpAndAway_removalParticles_4 = external_11;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_4.In(logic_uScript_FlyTechUpAndAway_tech_4, logic_uScript_FlyTechUpAndAway_maxLifetime_4, logic_uScript_FlyTechUpAndAway_targetHeight_4, logic_uScript_FlyTechUpAndAway_aiTree_4, logic_uScript_FlyTechUpAndAway_removalParticles_4);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_4.Out)
		{
			Relay_Succeed_5();
		}
	}

	private void Relay_Succeed_5()
	{
		logic_uScript_FinishEncounter_owner_5 = owner_Connection_3;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_5.Succeed(logic_uScript_FinishEncounter_owner_5);
		if (logic_uScript_FinishEncounter_uScript_FinishEncounter_5.Out)
		{
			Relay_Connection_12();
		}
	}

	private void Relay_Fail_5()
	{
		logic_uScript_FinishEncounter_owner_5 = owner_Connection_3;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_5.Fail(logic_uScript_FinishEncounter_owner_5);
		if (logic_uScript_FinishEncounter_uScript_FinishEncounter_5.Out)
		{
			Relay_Connection_12();
		}
	}

	private void Relay_Connection_6()
	{
	}

	private void Relay_Connection_7()
	{
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
		if (this.Out != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Out(this, args);
		}
	}

	private void Relay_In_13()
	{
		logic_uScript_ClearItemPickup_tech_13 = external_7;
		logic_uScript_ClearItemPickup_uScript_ClearItemPickup_13.In(logic_uScript_ClearItemPickup_tech_13);
		if (logic_uScript_ClearItemPickup_uScript_ClearItemPickup_13.Out)
		{
			Relay_In_4();
		}
	}

	private void Relay_In_15()
	{
		logic_uScript_SetTankHideBlockLimit_tech_15 = external_7;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_15.In(logic_uScript_SetTankHideBlockLimit_hidden_15, logic_uScript_SetTankHideBlockLimit_tech_15);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_15.Out)
		{
			Relay_In_13();
		}
	}
}
