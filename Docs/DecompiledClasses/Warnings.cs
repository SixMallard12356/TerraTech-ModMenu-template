using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Warnings : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	public LocalisedString[] msgBaseUnderAttack = new LocalisedString[0];

	public LocalisedString[] msgHeartBlockAttacked = new LocalisedString[0];

	public uScript_AddMessage.MessageData msgTippedOverFallen;

	public uScript_AddMessage.MessageData msgTippedOverFallen_Pad;

	public uScript_AddMessage.MessageData msgTippedOverRecovered;

	public uScript_AddMessage.MessageData msgTippedOverRecovered_Pad;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_5;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_3 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_3 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_3;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_3;

	private string logic_uScript_AddOnScreenMessage_tag_3 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_3;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_3;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_3;

	private bool logic_uScript_AddOnScreenMessage_Out_3 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_3 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_6 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_6 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_6;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_6;

	private string logic_uScript_AddOnScreenMessage_tag_6 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_6;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_6;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_6;

	private bool logic_uScript_AddOnScreenMessage_Out_6 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_6 = true;

	private uScript_IsCoreEncounterCompleted logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_8 = new uScript_IsCoreEncounterCompleted();

	private FactionSubTypes logic_uScript_IsCoreEncounterCompleted_corp_8 = FactionSubTypes.GSO;

	private int logic_uScript_IsCoreEncounterCompleted_grade_8 = 1;

	private string logic_uScript_IsCoreEncounterCompleted_encounterName_8 = "1-2 Build Tech";

	private bool logic_uScript_IsCoreEncounterCompleted_True_8 = true;

	private bool logic_uScript_IsCoreEncounterCompleted_False_8 = true;

	private uScript_IsCurrentGameType logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_16 = new uScript_IsCurrentGameType();

	private ManGameMode.GameType logic_uScript_IsCurrentGameType_gameType_16 = ManGameMode.GameType.MainGame;

	private bool logic_uScript_IsCurrentGameType_True_16 = true;

	private bool logic_uScript_IsCurrentGameType_False_16 = true;

	private uScript_IsCurrentGameType logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_17 = new uScript_IsCurrentGameType();

	private ManGameMode.GameType logic_uScript_IsCurrentGameType_gameType_17 = ManGameMode.GameType.SumoShowdown;

	private bool logic_uScript_IsCurrentGameType_True_17 = true;

	private bool logic_uScript_IsCurrentGameType_False_17 = true;

	private uScript_IsCurrentGameType logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_18 = new uScript_IsCurrentGameType();

	private ManGameMode.GameType logic_uScript_IsCurrentGameType_gameType_18 = ManGameMode.GameType.RaD;

	private bool logic_uScript_IsCurrentGameType_True_18 = true;

	private bool logic_uScript_IsCurrentGameType_False_18 = true;

	private uScript_IsCurrentGameType logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_19 = new uScript_IsCurrentGameType();

	private ManGameMode.GameType logic_uScript_IsCurrentGameType_gameType_19 = ManGameMode.GameType.RaD;

	private bool logic_uScript_IsCurrentGameType_True_19 = true;

	private bool logic_uScript_IsCurrentGameType_False_19 = true;

	private SubGraph_ShowHintWithPadSupport logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_20 = new SubGraph_ShowHintWithPadSupport();

	private UIHintFloating.HintFloatTypes logic_SubGraph_ShowHintWithPadSupport_hintControlPad_20 = UIHintFloating.HintFloatTypes.Buildbeam_Pad;

	private UIHintFloating.HintFloatTypes logic_SubGraph_ShowHintWithPadSupport_hintMouseKeyboard_20 = UIHintFloating.HintFloatTypes.Buildbeam_Keyboard;

	private uScript_HideHintFloating logic_uScript_HideHintFloating_uScript_HideHintFloating_21 = new uScript_HideHintFloating();

	private bool logic_uScript_HideHintFloating_Out_21 = true;

	private uScript_CheckIfPlayerTippedOver logic_uScript_CheckIfPlayerTippedOver_uScript_CheckIfPlayerTippedOver_22 = new uScript_CheckIfPlayerTippedOver();

	private bool logic_uScript_CheckIfPlayerTippedOver_TippedOver_22 = true;

	private bool logic_uScript_CheckIfPlayerTippedOver_NotTippedOver_22 = true;

	private bool logic_uScript_CheckIfPlayerTippedOver_Recovered_22 = true;

	private uScript_GetPlayerTank logic_uScript_GetPlayerTank_uScript_GetPlayerTank_23 = new uScript_GetPlayerTank();

	private Tank logic_uScript_GetPlayerTank_Return_23;

	private bool logic_uScript_GetPlayerTank_Returned_23 = true;

	private bool logic_uScript_GetPlayerTank_NotReturned_23 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
			if (null != owner_Connection_2)
			{
				uScript_IsPlayerBaseBeingDamaged uScript_IsPlayerBaseBeingDamaged2 = owner_Connection_2.GetComponent<uScript_IsPlayerBaseBeingDamaged>();
				if (null == uScript_IsPlayerBaseBeingDamaged2)
				{
					uScript_IsPlayerBaseBeingDamaged2 = owner_Connection_2.AddComponent<uScript_IsPlayerBaseBeingDamaged>();
				}
				if (null != uScript_IsPlayerBaseBeingDamaged2)
				{
					uScript_IsPlayerBaseBeingDamaged2.OnPlayerBaseDamaged += Instance_OnPlayerBaseDamaged_7;
					uScript_IsPlayerBaseBeingDamaged2.OnPlayerHeartBaseDamaged += Instance_OnPlayerHeartBaseDamaged_7;
				}
			}
		}
		if (!(null == owner_Connection_5) && m_RegisteredForEvents)
		{
			return;
		}
		owner_Connection_5 = parentGameObject;
		if (null != owner_Connection_5)
		{
			uScript_Update uScript_Update2 = owner_Connection_5.GetComponent<uScript_Update>();
			if (null == uScript_Update2)
			{
				uScript_Update2 = owner_Connection_5.AddComponent<uScript_Update>();
			}
			if (null != uScript_Update2)
			{
				uScript_Update2.OnUpdate += Instance_OnUpdate_0;
				uScript_Update2.OnLateUpdate += Instance_OnLateUpdate_0;
				uScript_Update2.OnFixedUpdate += Instance_OnFixedUpdate_0;
			}
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_2)
		{
			uScript_IsPlayerBaseBeingDamaged uScript_IsPlayerBaseBeingDamaged2 = owner_Connection_2.GetComponent<uScript_IsPlayerBaseBeingDamaged>();
			if (null == uScript_IsPlayerBaseBeingDamaged2)
			{
				uScript_IsPlayerBaseBeingDamaged2 = owner_Connection_2.AddComponent<uScript_IsPlayerBaseBeingDamaged>();
			}
			if (null != uScript_IsPlayerBaseBeingDamaged2)
			{
				uScript_IsPlayerBaseBeingDamaged2.OnPlayerBaseDamaged += Instance_OnPlayerBaseDamaged_7;
				uScript_IsPlayerBaseBeingDamaged2.OnPlayerHeartBaseDamaged += Instance_OnPlayerHeartBaseDamaged_7;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_5)
		{
			uScript_Update uScript_Update2 = owner_Connection_5.GetComponent<uScript_Update>();
			if (null == uScript_Update2)
			{
				uScript_Update2 = owner_Connection_5.AddComponent<uScript_Update>();
			}
			if (null != uScript_Update2)
			{
				uScript_Update2.OnUpdate += Instance_OnUpdate_0;
				uScript_Update2.OnLateUpdate += Instance_OnLateUpdate_0;
				uScript_Update2.OnFixedUpdate += Instance_OnFixedUpdate_0;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_2)
		{
			uScript_IsPlayerBaseBeingDamaged component = owner_Connection_2.GetComponent<uScript_IsPlayerBaseBeingDamaged>();
			if (null != component)
			{
				component.OnPlayerBaseDamaged -= Instance_OnPlayerBaseDamaged_7;
				component.OnPlayerHeartBaseDamaged -= Instance_OnPlayerHeartBaseDamaged_7;
			}
		}
		if (null != owner_Connection_5)
		{
			uScript_Update component2 = owner_Connection_5.GetComponent<uScript_Update>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_0;
				component2.OnLateUpdate -= Instance_OnLateUpdate_0;
				component2.OnFixedUpdate -= Instance_OnFixedUpdate_0;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_3.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_6.SetParent(g);
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_8.SetParent(g);
		logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_16.SetParent(g);
		logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_17.SetParent(g);
		logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_18.SetParent(g);
		logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_19.SetParent(g);
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_20.SetParent(g);
		logic_uScript_HideHintFloating_uScript_HideHintFloating_21.SetParent(g);
		logic_uScript_CheckIfPlayerTippedOver_uScript_CheckIfPlayerTippedOver_22.SetParent(g);
		logic_uScript_GetPlayerTank_uScript_GetPlayerTank_23.SetParent(g);
		owner_Connection_2 = parentGameObject;
		owner_Connection_5 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_20.Awake();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_20.Out += SubGraph_ShowHintWithPadSupport_Out_20;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_20.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_8.OnEnable();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_20.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_3.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_6.OnDisable();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_20.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_20.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_20.OnDestroy();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_20.Out -= SubGraph_ShowHintWithPadSupport_Out_20;
	}

	private void Instance_OnUpdate_0(object o, EventArgs e)
	{
		Relay_OnUpdate_0();
	}

	private void Instance_OnLateUpdate_0(object o, EventArgs e)
	{
		Relay_OnLateUpdate_0();
	}

	private void Instance_OnFixedUpdate_0(object o, EventArgs e)
	{
		Relay_OnFixedUpdate_0();
	}

	private void Instance_OnPlayerBaseDamaged_7(object o, EventArgs e)
	{
		Relay_OnPlayerBaseDamaged_7();
	}

	private void Instance_OnPlayerHeartBaseDamaged_7(object o, EventArgs e)
	{
		Relay_OnPlayerHeartBaseDamaged_7();
	}

	private void SubGraph_ShowHintWithPadSupport_Out_20(object o, SubGraph_ShowHintWithPadSupport.LogicEventArgs e)
	{
		Relay_Out_20();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_16();
	}

	private void Relay_OnLateUpdate_0()
	{
	}

	private void Relay_OnFixedUpdate_0()
	{
	}

	private void Relay_In_3()
	{
		int num = 0;
		Array array = msgBaseUnderAttack;
		if (logic_uScript_AddOnScreenMessage_locString_3.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_3, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_3, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_3 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_3.In(logic_uScript_AddOnScreenMessage_locString_3, logic_uScript_AddOnScreenMessage_msgPriority_3, logic_uScript_AddOnScreenMessage_holdMsg_3, logic_uScript_AddOnScreenMessage_tag_3, logic_uScript_AddOnScreenMessage_speaker_3, logic_uScript_AddOnScreenMessage_side_3);
	}

	private void Relay_In_6()
	{
		int num = 0;
		Array array = msgHeartBlockAttacked;
		if (logic_uScript_AddOnScreenMessage_locString_6.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_6, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_6, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_6 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_6.In(logic_uScript_AddOnScreenMessage_locString_6, logic_uScript_AddOnScreenMessage_msgPriority_6, logic_uScript_AddOnScreenMessage_holdMsg_6, logic_uScript_AddOnScreenMessage_tag_6, logic_uScript_AddOnScreenMessage_speaker_6, logic_uScript_AddOnScreenMessage_side_6);
	}

	private void Relay_OnPlayerBaseDamaged_7()
	{
		Relay_In_18();
	}

	private void Relay_OnPlayerHeartBaseDamaged_7()
	{
		Relay_In_19();
	}

	private void Relay_In_8()
	{
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_8.In(logic_uScript_IsCoreEncounterCompleted_corp_8, logic_uScript_IsCoreEncounterCompleted_grade_8, logic_uScript_IsCoreEncounterCompleted_encounterName_8);
		if (logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_8.True)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_16()
	{
		logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_16.In(logic_uScript_IsCurrentGameType_gameType_16);
		bool num = logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_16.True;
		bool flag = logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_16.False;
		if (num)
		{
			Relay_In_8();
		}
		if (flag)
		{
			Relay_In_17();
		}
	}

	private void Relay_In_17()
	{
		logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_17.In(logic_uScript_IsCurrentGameType_gameType_17);
		if (logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_17.False)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_18()
	{
		logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_18.In(logic_uScript_IsCurrentGameType_gameType_18);
		if (logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_18.False)
		{
			Relay_In_3();
		}
	}

	private void Relay_In_19()
	{
		logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_19.In(logic_uScript_IsCurrentGameType_gameType_19);
		if (logic_uScript_IsCurrentGameType_uScript_IsCurrentGameType_19.False)
		{
			Relay_In_6();
		}
	}

	private void Relay_Out_20()
	{
	}

	private void Relay_In_20()
	{
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_20.In(logic_SubGraph_ShowHintWithPadSupport_hintControlPad_20, logic_SubGraph_ShowHintWithPadSupport_hintMouseKeyboard_20);
	}

	private void Relay_In_21()
	{
		logic_uScript_HideHintFloating_uScript_HideHintFloating_21.In();
	}

	private void Relay_In_22()
	{
		logic_uScript_CheckIfPlayerTippedOver_uScript_CheckIfPlayerTippedOver_22.In();
		bool tippedOver = logic_uScript_CheckIfPlayerTippedOver_uScript_CheckIfPlayerTippedOver_22.TippedOver;
		bool recovered = logic_uScript_CheckIfPlayerTippedOver_uScript_CheckIfPlayerTippedOver_22.Recovered;
		if (tippedOver)
		{
			Relay_In_20();
		}
		if (recovered)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_23()
	{
		logic_uScript_GetPlayerTank_Return_23 = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_23.In();
		if (logic_uScript_GetPlayerTank_uScript_GetPlayerTank_23.Returned)
		{
			Relay_In_22();
		}
	}
}
