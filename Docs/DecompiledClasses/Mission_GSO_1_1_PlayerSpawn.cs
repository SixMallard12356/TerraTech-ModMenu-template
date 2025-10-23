using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("buildinitialtank", "")]
public class Mission_GSO_1_1_PlayerSpawn : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public TankPreset BuiltTechPreset;

	public TankPreset CabPreset;

	public Transform FallEffect;

	private PositionWithFacing local_10_PositionWithFacing;

	private PositionWithFacing local_8_PositionWithFacing;

	private GameObject owner_Connection_15;

	private GameObject owner_Connection_16;

	private GameObject owner_Connection_18;

	private uScript_SpawnFallingCab logic_uScript_SpawnFallingCab_uScript_SpawnFallingCab_0 = new uScript_SpawnFallingCab();

	private Transform logic_uScript_SpawnFallingCab_particleEffect_0;

	private TankPreset logic_uScript_SpawnFallingCab_preset_0;

	private GameObject logic_uScript_SpawnFallingCab_owner_0;

	private string logic_uScript_SpawnFallingCab_uniqueName_0 = "cabo";

	private bool logic_uScript_SpawnFallingCab_damage_0 = true;

	private Transform logic_uScript_SpawnFallingCab_Return_0;

	private bool logic_uScript_SpawnFallingCab_Finished_0 = true;

	private bool logic_uScript_SpawnFallingCab_Out_0 = true;

	private uScript_DisablePlayerInput logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_3 = new uScript_DisablePlayerInput();

	private bool logic_uScript_DisablePlayerInput_disableInput_3 = true;

	private bool logic_uScript_DisablePlayerInput_Out_3 = true;

	private uScript_SKU logic_uScript_SKU_uScript_SKU_4 = new uScript_SKU();

	private bool logic_uScript_SKU_Show_4 = true;

	private bool logic_uScript_SKU_Demo_4 = true;

	private bool logic_uScript_SKU_Normal_4 = true;

	private uScript_SpawnPresetAsPlayer logic_uScript_SpawnPresetAsPlayer_uScript_SpawnPresetAsPlayer_5 = new uScript_SpawnPresetAsPlayer();

	private TankPreset logic_uScript_SpawnPresetAsPlayer_preset_5;

	private PositionWithFacing logic_uScript_SpawnPresetAsPlayer_playerPosition_5;

	private bool logic_uScript_SpawnPresetAsPlayer_Done_5 = true;

	private bool logic_uScript_SpawnPresetAsPlayer_NotDone_5 = true;

	private uScript_GetStartPosition logic_uScript_GetStartPosition_uScript_GetStartPosition_6 = new uScript_GetStartPosition();

	private PositionWithFacing logic_uScript_GetStartPosition_Return_6;

	private bool logic_uScript_GetStartPosition_Out_6 = true;

	private uScript_IsDebugSkip logic_uScript_IsDebugSkip_uScript_IsDebugSkip_9 = new uScript_IsDebugSkip();

	private bool logic_uScript_IsDebugSkip_Out_9 = true;

	private bool logic_uScript_IsDebugSkip_True_9 = true;

	private bool logic_uScript_IsDebugSkip_False_9 = true;

	private uScript_SpawnPresetAsPlayer logic_uScript_SpawnPresetAsPlayer_uScript_SpawnPresetAsPlayer_12 = new uScript_SpawnPresetAsPlayer();

	private TankPreset logic_uScript_SpawnPresetAsPlayer_preset_12;

	private PositionWithFacing logic_uScript_SpawnPresetAsPlayer_playerPosition_12;

	private bool logic_uScript_SpawnPresetAsPlayer_Done_12 = true;

	private bool logic_uScript_SpawnPresetAsPlayer_NotDone_12 = true;

	private uScript_GetStartPosition logic_uScript_GetStartPosition_uScript_GetStartPosition_13 = new uScript_GetStartPosition();

	private PositionWithFacing logic_uScript_GetStartPosition_Return_13;

	private bool logic_uScript_GetStartPosition_Out_13 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_19 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_19;

	private bool logic_uScript_FinishEncounter_Out_19 = true;

	private uScript_LockInteractionMode logic_uScript_LockInteractionMode_uScript_LockInteractionMode_20 = new uScript_LockInteractionMode();

	private bool logic_uScript_LockInteractionMode_Out_20 = true;

	private uScript_GetJoypadControlMode logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_21 = new uScript_GetJoypadControlMode();

	private bool logic_uScript_GetJoypadControlMode_Joypad_21 = true;

	private bool logic_uScript_GetJoypadControlMode_MouseAndKeyboard_21 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_22 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_22 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_15 || !m_RegisteredForEvents)
		{
			owner_Connection_15 = parentGameObject;
		}
		if (null == owner_Connection_16 || !m_RegisteredForEvents)
		{
			owner_Connection_16 = parentGameObject;
			if (null != owner_Connection_16)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_16.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_16.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_17;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_17;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_17;
				}
			}
		}
		if (null == owner_Connection_18 || !m_RegisteredForEvents)
		{
			owner_Connection_18 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_16)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_16.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_16.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_17;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_17;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_17;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_16)
		{
			uScript_EncounterUpdate component = owner_Connection_16.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_17;
				component.OnSuspend -= Instance_OnSuspend_17;
				component.OnResume -= Instance_OnResume_17;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SpawnFallingCab_uScript_SpawnFallingCab_0.SetParent(g);
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_3.SetParent(g);
		logic_uScript_SKU_uScript_SKU_4.SetParent(g);
		logic_uScript_SpawnPresetAsPlayer_uScript_SpawnPresetAsPlayer_5.SetParent(g);
		logic_uScript_GetStartPosition_uScript_GetStartPosition_6.SetParent(g);
		logic_uScript_IsDebugSkip_uScript_IsDebugSkip_9.SetParent(g);
		logic_uScript_SpawnPresetAsPlayer_uScript_SpawnPresetAsPlayer_12.SetParent(g);
		logic_uScript_GetStartPosition_uScript_GetStartPosition_13.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_19.SetParent(g);
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_20.SetParent(g);
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_21.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_22.SetParent(g);
		owner_Connection_15 = parentGameObject;
		owner_Connection_16 = parentGameObject;
		owner_Connection_18 = parentGameObject;
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
		logic_uScript_SpawnFallingCab_uScript_SpawnFallingCab_0.OnDisable();
		logic_uScript_SpawnPresetAsPlayer_uScript_SpawnPresetAsPlayer_5.OnDisable();
		logic_uScript_SpawnPresetAsPlayer_uScript_SpawnPresetAsPlayer_12.OnDisable();
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_21.OnDisable();
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

	private void Instance_OnUpdate_17(object o, EventArgs e)
	{
		Relay_OnUpdate_17();
	}

	private void Instance_OnSuspend_17(object o, EventArgs e)
	{
		Relay_OnSuspend_17();
	}

	private void Instance_OnResume_17(object o, EventArgs e)
	{
		Relay_OnResume_17();
	}

	private void Relay_In_0()
	{
		logic_uScript_SpawnFallingCab_particleEffect_0 = FallEffect;
		logic_uScript_SpawnFallingCab_preset_0 = CabPreset;
		logic_uScript_SpawnFallingCab_owner_0 = owner_Connection_15;
		logic_uScript_SpawnFallingCab_Return_0 = logic_uScript_SpawnFallingCab_uScript_SpawnFallingCab_0.In(logic_uScript_SpawnFallingCab_particleEffect_0, logic_uScript_SpawnFallingCab_preset_0, logic_uScript_SpawnFallingCab_owner_0, logic_uScript_SpawnFallingCab_uniqueName_0, logic_uScript_SpawnFallingCab_damage_0);
		if (logic_uScript_SpawnFallingCab_uScript_SpawnFallingCab_0.Finished)
		{
			Relay_In_3();
		}
	}

	private void Relay_In_3()
	{
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_3.In(logic_uScript_DisablePlayerInput_disableInput_3);
		if (logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_3.Out)
		{
			Relay_Succeed_19();
		}
	}

	private void Relay_In_4()
	{
		logic_uScript_SKU_uScript_SKU_4.In();
		bool show = logic_uScript_SKU_uScript_SKU_4.Show;
		bool demo = logic_uScript_SKU_uScript_SKU_4.Demo;
		bool normal = logic_uScript_SKU_uScript_SKU_4.Normal;
		if (show)
		{
			Relay_In_21();
		}
		if (demo)
		{
			Relay_In_6();
		}
		if (normal)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_5()
	{
		logic_uScript_SpawnPresetAsPlayer_preset_5 = CabPreset;
		logic_uScript_SpawnPresetAsPlayer_playerPosition_5 = local_8_PositionWithFacing;
		logic_uScript_SpawnPresetAsPlayer_uScript_SpawnPresetAsPlayer_5.In(logic_uScript_SpawnPresetAsPlayer_preset_5, logic_uScript_SpawnPresetAsPlayer_playerPosition_5);
		if (logic_uScript_SpawnPresetAsPlayer_uScript_SpawnPresetAsPlayer_5.Done)
		{
			Relay_In_3();
		}
	}

	private void Relay_In_6()
	{
		logic_uScript_GetStartPosition_Return_6 = logic_uScript_GetStartPosition_uScript_GetStartPosition_6.In();
		local_8_PositionWithFacing = logic_uScript_GetStartPosition_Return_6;
		if (logic_uScript_GetStartPosition_uScript_GetStartPosition_6.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_In_9()
	{
		logic_uScript_IsDebugSkip_uScript_IsDebugSkip_9.In();
		bool num = logic_uScript_IsDebugSkip_uScript_IsDebugSkip_9.True;
		bool flag = logic_uScript_IsDebugSkip_uScript_IsDebugSkip_9.False;
		if (num)
		{
			Relay_In_13();
		}
		if (flag)
		{
			Relay_In_4();
		}
	}

	private void Relay_In_12()
	{
		logic_uScript_SpawnPresetAsPlayer_preset_12 = BuiltTechPreset;
		logic_uScript_SpawnPresetAsPlayer_playerPosition_12 = local_10_PositionWithFacing;
		logic_uScript_SpawnPresetAsPlayer_uScript_SpawnPresetAsPlayer_12.In(logic_uScript_SpawnPresetAsPlayer_preset_12, logic_uScript_SpawnPresetAsPlayer_playerPosition_12);
		if (logic_uScript_SpawnPresetAsPlayer_uScript_SpawnPresetAsPlayer_12.Done)
		{
			Relay_In_3();
		}
	}

	private void Relay_In_13()
	{
		logic_uScript_GetStartPosition_Return_13 = logic_uScript_GetStartPosition_uScript_GetStartPosition_13.In();
		local_10_PositionWithFacing = logic_uScript_GetStartPosition_Return_13;
		if (logic_uScript_GetStartPosition_uScript_GetStartPosition_13.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_OnUpdate_17()
	{
		Relay_In_9();
	}

	private void Relay_OnSuspend_17()
	{
	}

	private void Relay_OnResume_17()
	{
	}

	private void Relay_Succeed_19()
	{
		logic_uScript_FinishEncounter_owner_19 = owner_Connection_18;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_19.Succeed(logic_uScript_FinishEncounter_owner_19);
	}

	private void Relay_Fail_19()
	{
		logic_uScript_FinishEncounter_owner_19 = owner_Connection_18;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_19.Fail(logic_uScript_FinishEncounter_owner_19);
	}

	private void Relay_Lock_20()
	{
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_20.Lock();
		if (logic_uScript_LockInteractionMode_uScript_LockInteractionMode_20.Out)
		{
			Relay_In_0();
		}
	}

	private void Relay_Unlock_20()
	{
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_20.Unlock();
		if (logic_uScript_LockInteractionMode_uScript_LockInteractionMode_20.Out)
		{
			Relay_In_0();
		}
	}

	private void Relay_In_21()
	{
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_21.In();
		bool joypad = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_21.Joypad;
		bool mouseAndKeyboard = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_21.MouseAndKeyboard;
		if (joypad)
		{
			Relay_Lock_20();
		}
		if (mouseAndKeyboard)
		{
			Relay_In_22();
		}
	}

	private void Relay_In_22()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_22.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_22.Out)
		{
			Relay_In_0();
		}
	}
}
