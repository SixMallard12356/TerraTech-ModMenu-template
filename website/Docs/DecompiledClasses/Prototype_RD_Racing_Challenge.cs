using System;
using UnityEngine;

[Serializable]
[FriendlyName("", "")]
[NodePath("Graphs")]
public class Prototype_RD_Racing_Challenge : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public ChallengeData challengeData;

	private uScript_ShowPrompt.Context local_Prompt_uScript_ShowPrompt_Context;

	private bool local_WaitingOnPrompt_System_Boolean = true;

	public LocalisedString msgCancel;

	public LocalisedString msgContinue;

	public LocalisedString[] msgWin = new LocalisedString[0];

	public GameObject startingTape;

	private GameObject startingTape_previous;

	public LocalisedString startPromptText;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_4;

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_21;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_0 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_0 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_0 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_0;

	private string logic_uScript_AddOnScreenMessage_tag_0 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_0;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_0;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_0;

	private bool logic_uScript_AddOnScreenMessage_Out_0 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_0 = true;

	private uScript_StartChallenge logic_uScript_StartChallenge_uScript_StartChallenge_5 = new uScript_StartChallenge();

	private GameObject logic_uScript_StartChallenge_owner_5;

	private ChallengeData logic_uScript_StartChallenge_data_5;

	private bool logic_uScript_StartChallenge_Out_5 = true;

	private uScript_ClearAsActiveRandDScript logic_uScript_ClearAsActiveRandDScript_uScript_ClearAsActiveRandDScript_8 = new uScript_ClearAsActiveRandDScript();

	private GameObject logic_uScript_ClearAsActiveRandDScript_owner_8;

	private bool logic_uScript_ClearAsActiveRandDScript_Out_8 = true;

	private uScriptAct_Toggle logic_uScriptAct_Toggle_uScriptAct_Toggle_11 = new uScriptAct_Toggle();

	private GameObject[] logic_uScriptAct_Toggle_Target_11 = new GameObject[0];

	private bool logic_uScriptAct_Toggle_IgnoreChildren_11;

	private bool logic_uScriptAct_Toggle_Out_11 = true;

	private uScript_DisablePlayerInput logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_13 = new uScript_DisablePlayerInput();

	private bool logic_uScript_DisablePlayerInput_disableInput_13 = true;

	private bool logic_uScript_DisablePlayerInput_Out_13 = true;

	private uScript_ShowPrompt logic_uScript_ShowPrompt_uScript_ShowPrompt_14 = new uScript_ShowPrompt();

	private LocalisedString logic_uScript_ShowPrompt_bodyText_14;

	private LocalisedString logic_uScript_ShowPrompt_acceptButtonText_14;

	private LocalisedString logic_uScript_ShowPrompt_rejectButtonText_14;

	private uScript_ShowPrompt.Context logic_uScript_ShowPrompt_Return_14;

	private bool logic_uScript_ShowPrompt_Out_14 = true;

	private uScript_GetChallengeState logic_uScript_GetChallengeState_uScript_GetChallengeState_15 = new uScript_GetChallengeState();

	private bool logic_uScript_GetChallengeState_Running_15 = true;

	private bool logic_uScript_GetChallengeState_NotRunning_15 = true;

	private bool logic_uScript_GetChallengeState_Success_15 = true;

	private bool logic_uScript_GetChallengeState_Failure_15 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_16;

	private bool logic_uScriptCon_CompareBool_True_16 = true;

	private bool logic_uScriptCon_CompareBool_False_16 = true;

	private uScript_DisablePlayerInput logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_18 = new uScript_DisablePlayerInput();

	private bool logic_uScript_DisablePlayerInput_disableInput_18;

	private bool logic_uScript_DisablePlayerInput_Out_18 = true;

	private uScript_DisablePlayerInput logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_19 = new uScript_DisablePlayerInput();

	private bool logic_uScript_DisablePlayerInput_disableInput_19;

	private bool logic_uScript_DisablePlayerInput_Out_19 = true;

	private uScript_SetAsActiveRandDScript logic_uScript_SetAsActiveRandDScript_uScript_SetAsActiveRandDScript_20 = new uScript_SetAsActiveRandDScript();

	private GameObject logic_uScript_SetAsActiveRandDScript_owner_20;

	private bool logic_uScript_SetAsActiveRandDScript_Out_20 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_22 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_22;

	private bool logic_uScriptAct_SetBool_Out_22 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_22 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_22 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_30 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_30;

	private bool logic_uScriptAct_SetBool_Out_30 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_30 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_30 = true;

	private uScriptAct_Toggle logic_uScriptAct_Toggle_uScriptAct_Toggle_31 = new uScriptAct_Toggle();

	private GameObject[] logic_uScriptAct_Toggle_Target_31 = new GameObject[0];

	private bool logic_uScriptAct_Toggle_IgnoreChildren_31;

	private bool logic_uScriptAct_Toggle_Out_31 = true;

	private uScript_GetPromptResult logic_uScript_GetPromptResult_uScript_GetPromptResult_33 = new uScript_GetPromptResult();

	private uScript_ShowPrompt.Context logic_uScript_GetPromptResult_context_33;

	private bool logic_uScript_GetPromptResult_Out_33 = true;

	private bool logic_uScript_GetPromptResult_Accepted_33 = true;

	private bool logic_uScript_GetPromptResult_Declined_33 = true;

	private bool logic_uScript_GetPromptResult_Showing_33 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (startingTape_previous != startingTape || !m_RegisteredForEvents)
		{
			startingTape_previous = startingTape;
		}
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
			if (null != owner_Connection_3)
			{
				uScript_PlayerTrigger uScript_PlayerTrigger2 = owner_Connection_3.GetComponent<uScript_PlayerTrigger>();
				if (null == uScript_PlayerTrigger2)
				{
					uScript_PlayerTrigger2 = owner_Connection_3.AddComponent<uScript_PlayerTrigger>();
				}
				if (null != uScript_PlayerTrigger2)
				{
					uScript_PlayerTrigger2.OnEnterTrigger += Instance_OnEnterTrigger_24;
					uScript_PlayerTrigger2.OnExitTrigger += Instance_OnExitTrigger_24;
				}
			}
		}
		if (null == owner_Connection_4 || !m_RegisteredForEvents)
		{
			owner_Connection_4 = parentGameObject;
			if (null != owner_Connection_4)
			{
				uScript_RandDScriptEvent uScript_RandDScriptEvent2 = owner_Connection_4.GetComponent<uScript_RandDScriptEvent>();
				if (null == uScript_RandDScriptEvent2)
				{
					uScript_RandDScriptEvent2 = owner_Connection_4.AddComponent<uScript_RandDScriptEvent>();
				}
				if (null != uScript_RandDScriptEvent2)
				{
					uScript_RandDScriptEvent2.OnUpdate += Instance_OnUpdate_2;
					uScript_RandDScriptEvent2.OnActivate += Instance_OnActivate_2;
					uScript_RandDScriptEvent2.OnDeactivate += Instance_OnDeactivate_2;
				}
			}
		}
		if (null == owner_Connection_6 || !m_RegisteredForEvents)
		{
			owner_Connection_6 = parentGameObject;
		}
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
		}
		if (null == owner_Connection_21 || !m_RegisteredForEvents)
		{
			owner_Connection_21 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (startingTape_previous != startingTape || !m_RegisteredForEvents)
		{
			startingTape_previous = startingTape;
		}
		if (!m_RegisteredForEvents && null != owner_Connection_3)
		{
			uScript_PlayerTrigger uScript_PlayerTrigger2 = owner_Connection_3.GetComponent<uScript_PlayerTrigger>();
			if (null == uScript_PlayerTrigger2)
			{
				uScript_PlayerTrigger2 = owner_Connection_3.AddComponent<uScript_PlayerTrigger>();
			}
			if (null != uScript_PlayerTrigger2)
			{
				uScript_PlayerTrigger2.OnEnterTrigger += Instance_OnEnterTrigger_24;
				uScript_PlayerTrigger2.OnExitTrigger += Instance_OnExitTrigger_24;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_4)
		{
			uScript_RandDScriptEvent uScript_RandDScriptEvent2 = owner_Connection_4.GetComponent<uScript_RandDScriptEvent>();
			if (null == uScript_RandDScriptEvent2)
			{
				uScript_RandDScriptEvent2 = owner_Connection_4.AddComponent<uScript_RandDScriptEvent>();
			}
			if (null != uScript_RandDScriptEvent2)
			{
				uScript_RandDScriptEvent2.OnUpdate += Instance_OnUpdate_2;
				uScript_RandDScriptEvent2.OnActivate += Instance_OnActivate_2;
				uScript_RandDScriptEvent2.OnDeactivate += Instance_OnDeactivate_2;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_3)
		{
			uScript_PlayerTrigger component = owner_Connection_3.GetComponent<uScript_PlayerTrigger>();
			if (null != component)
			{
				component.OnEnterTrigger -= Instance_OnEnterTrigger_24;
				component.OnExitTrigger -= Instance_OnExitTrigger_24;
			}
		}
		if (null != owner_Connection_4)
		{
			uScript_RandDScriptEvent component2 = owner_Connection_4.GetComponent<uScript_RandDScriptEvent>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_2;
				component2.OnActivate -= Instance_OnActivate_2;
				component2.OnDeactivate -= Instance_OnDeactivate_2;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_0.SetParent(g);
		logic_uScript_StartChallenge_uScript_StartChallenge_5.SetParent(g);
		logic_uScript_ClearAsActiveRandDScript_uScript_ClearAsActiveRandDScript_8.SetParent(g);
		logic_uScriptAct_Toggle_uScriptAct_Toggle_11.SetParent(g);
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_13.SetParent(g);
		logic_uScript_ShowPrompt_uScript_ShowPrompt_14.SetParent(g);
		logic_uScript_GetChallengeState_uScript_GetChallengeState_15.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.SetParent(g);
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_18.SetParent(g);
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_19.SetParent(g);
		logic_uScript_SetAsActiveRandDScript_uScript_SetAsActiveRandDScript_20.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_30.SetParent(g);
		logic_uScriptAct_Toggle_uScriptAct_Toggle_31.SetParent(g);
		logic_uScript_GetPromptResult_uScript_GetPromptResult_33.SetParent(g);
		owner_Connection_3 = parentGameObject;
		owner_Connection_4 = parentGameObject;
		owner_Connection_6 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_21 = parentGameObject;
	}

	public void Awake()
	{
		logic_uScriptAct_Toggle_uScriptAct_Toggle_11.OnOut += uScriptAct_Toggle_OnOut_11;
		logic_uScriptAct_Toggle_uScriptAct_Toggle_11.OffOut += uScriptAct_Toggle_OffOut_11;
		logic_uScriptAct_Toggle_uScriptAct_Toggle_11.ToggleOut += uScriptAct_Toggle_ToggleOut_11;
		logic_uScriptAct_Toggle_uScriptAct_Toggle_31.OnOut += uScriptAct_Toggle_OnOut_31;
		logic_uScriptAct_Toggle_uScriptAct_Toggle_31.OffOut += uScriptAct_Toggle_OffOut_31;
		logic_uScriptAct_Toggle_uScriptAct_Toggle_31.ToggleOut += uScriptAct_Toggle_ToggleOut_31;
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
		logic_uScript_ShowPrompt_uScript_ShowPrompt_14.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_0.OnDisable();
		logic_uScript_ShowPrompt_uScript_ShowPrompt_14.OnDisable();
		logic_uScript_GetPromptResult_uScript_GetPromptResult_33.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
	}

	public void OnDestroy()
	{
		logic_uScriptAct_Toggle_uScriptAct_Toggle_11.OnOut -= uScriptAct_Toggle_OnOut_11;
		logic_uScriptAct_Toggle_uScriptAct_Toggle_11.OffOut -= uScriptAct_Toggle_OffOut_11;
		logic_uScriptAct_Toggle_uScriptAct_Toggle_11.ToggleOut -= uScriptAct_Toggle_ToggleOut_11;
		logic_uScriptAct_Toggle_uScriptAct_Toggle_31.OnOut -= uScriptAct_Toggle_OnOut_31;
		logic_uScriptAct_Toggle_uScriptAct_Toggle_31.OffOut -= uScriptAct_Toggle_OffOut_31;
		logic_uScriptAct_Toggle_uScriptAct_Toggle_31.ToggleOut -= uScriptAct_Toggle_ToggleOut_31;
	}

	private void Instance_OnUpdate_2(object o, EventArgs e)
	{
		Relay_OnUpdate_2();
	}

	private void Instance_OnActivate_2(object o, EventArgs e)
	{
		Relay_OnActivate_2();
	}

	private void Instance_OnDeactivate_2(object o, EventArgs e)
	{
		Relay_OnDeactivate_2();
	}

	private void Instance_OnEnterTrigger_24(object o, EventArgs e)
	{
		Relay_OnEnterTrigger_24();
	}

	private void Instance_OnExitTrigger_24(object o, EventArgs e)
	{
		Relay_OnExitTrigger_24();
	}

	private void uScriptAct_Toggle_OnOut_11(object o, EventArgs e)
	{
		Relay_OnOut_11();
	}

	private void uScriptAct_Toggle_OffOut_11(object o, EventArgs e)
	{
		Relay_OffOut_11();
	}

	private void uScriptAct_Toggle_ToggleOut_11(object o, EventArgs e)
	{
		Relay_ToggleOut_11();
	}

	private void uScriptAct_Toggle_OnOut_31(object o, EventArgs e)
	{
		Relay_OnOut_31();
	}

	private void uScriptAct_Toggle_OffOut_31(object o, EventArgs e)
	{
		Relay_OffOut_31();
	}

	private void uScriptAct_Toggle_ToggleOut_31(object o, EventArgs e)
	{
		Relay_ToggleOut_31();
	}

	private void Relay_In_0()
	{
		int num = 0;
		Array array = msgWin;
		if (logic_uScript_AddOnScreenMessage_locString_0.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_0, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_0, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_0 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_0.In(logic_uScript_AddOnScreenMessage_locString_0, logic_uScript_AddOnScreenMessage_msgPriority_0, logic_uScript_AddOnScreenMessage_holdMsg_0, logic_uScript_AddOnScreenMessage_tag_0, logic_uScript_AddOnScreenMessage_speaker_0, logic_uScript_AddOnScreenMessage_side_0);
	}

	private void Relay_OnUpdate_2()
	{
		Relay_In_15();
	}

	private void Relay_OnActivate_2()
	{
		Relay_In_13();
	}

	private void Relay_OnDeactivate_2()
	{
		Relay_TurnOn_11();
	}

	private void Relay_In_5()
	{
		logic_uScript_StartChallenge_owner_5 = owner_Connection_6;
		logic_uScript_StartChallenge_data_5 = challengeData;
		logic_uScript_StartChallenge_uScript_StartChallenge_5.In(logic_uScript_StartChallenge_owner_5, logic_uScript_StartChallenge_data_5);
		if (logic_uScript_StartChallenge_uScript_StartChallenge_5.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_In_8()
	{
		logic_uScript_ClearAsActiveRandDScript_owner_8 = owner_Connection_9;
		logic_uScript_ClearAsActiveRandDScript_uScript_ClearAsActiveRandDScript_8.In(logic_uScript_ClearAsActiveRandDScript_owner_8);
		if (logic_uScript_ClearAsActiveRandDScript_uScript_ClearAsActiveRandDScript_8.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_OnOut_11()
	{
	}

	private void Relay_OffOut_11()
	{
	}

	private void Relay_ToggleOut_11()
	{
	}

	private void Relay_TurnOn_11()
	{
		int num = 0;
		if (startingTape_previous != startingTape || !m_RegisteredForEvents)
		{
			startingTape_previous = startingTape;
		}
		if (logic_uScriptAct_Toggle_Target_11.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Toggle_Target_11, num + 1);
		}
		logic_uScriptAct_Toggle_Target_11[num++] = startingTape;
		logic_uScriptAct_Toggle_uScriptAct_Toggle_11.TurnOn(logic_uScriptAct_Toggle_Target_11, logic_uScriptAct_Toggle_IgnoreChildren_11);
		if (logic_uScriptAct_Toggle_uScriptAct_Toggle_11.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_TurnOff_11()
	{
		int num = 0;
		if (startingTape_previous != startingTape || !m_RegisteredForEvents)
		{
			startingTape_previous = startingTape;
		}
		if (logic_uScriptAct_Toggle_Target_11.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Toggle_Target_11, num + 1);
		}
		logic_uScriptAct_Toggle_Target_11[num++] = startingTape;
		logic_uScriptAct_Toggle_uScriptAct_Toggle_11.TurnOff(logic_uScriptAct_Toggle_Target_11, logic_uScriptAct_Toggle_IgnoreChildren_11);
		if (logic_uScriptAct_Toggle_uScriptAct_Toggle_11.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_Toggle_11()
	{
		int num = 0;
		if (startingTape_previous != startingTape || !m_RegisteredForEvents)
		{
			startingTape_previous = startingTape;
		}
		if (logic_uScriptAct_Toggle_Target_11.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Toggle_Target_11, num + 1);
		}
		logic_uScriptAct_Toggle_Target_11[num++] = startingTape;
		logic_uScriptAct_Toggle_uScriptAct_Toggle_11.Toggle(logic_uScriptAct_Toggle_Target_11, logic_uScriptAct_Toggle_IgnoreChildren_11);
		if (logic_uScriptAct_Toggle_uScriptAct_Toggle_11.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_In_13()
	{
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_13.In(logic_uScript_DisablePlayerInput_disableInput_13);
		if (logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_13.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_In_14()
	{
		logic_uScript_ShowPrompt_bodyText_14 = startPromptText;
		logic_uScript_ShowPrompt_acceptButtonText_14 = msgContinue;
		logic_uScript_ShowPrompt_rejectButtonText_14 = msgCancel;
		logic_uScript_ShowPrompt_Return_14 = logic_uScript_ShowPrompt_uScript_ShowPrompt_14.In(logic_uScript_ShowPrompt_bodyText_14, logic_uScript_ShowPrompt_acceptButtonText_14, logic_uScript_ShowPrompt_rejectButtonText_14);
		local_Prompt_uScript_ShowPrompt_Context = logic_uScript_ShowPrompt_Return_14;
		if (logic_uScript_ShowPrompt_uScript_ShowPrompt_14.Out)
		{
			Relay_True_30();
		}
	}

	private void Relay_In_15()
	{
		logic_uScript_GetChallengeState_uScript_GetChallengeState_15.In();
		if (logic_uScript_GetChallengeState_uScript_GetChallengeState_15.NotRunning)
		{
			Relay_In_16();
		}
	}

	private void Relay_In_16()
	{
		logic_uScriptCon_CompareBool_Bool_16 = local_WaitingOnPrompt_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.In(logic_uScriptCon_CompareBool_Bool_16);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.False;
		if (num)
		{
			Relay_In_33();
		}
		if (flag)
		{
			Relay_In_8();
		}
	}

	private void Relay_In_18()
	{
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_18.In(logic_uScript_DisablePlayerInput_disableInput_18);
	}

	private void Relay_In_19()
	{
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_19.In(logic_uScript_DisablePlayerInput_disableInput_19);
		if (logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_19.Out)
		{
			Relay_False_22();
		}
	}

	private void Relay_In_20()
	{
		logic_uScript_SetAsActiveRandDScript_owner_20 = owner_Connection_21;
		logic_uScript_SetAsActiveRandDScript_uScript_SetAsActiveRandDScript_20.In(logic_uScript_SetAsActiveRandDScript_owner_20);
	}

	private void Relay_True_22()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.True(out logic_uScriptAct_SetBool_Target_22);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_22;
	}

	private void Relay_False_22()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.False(out logic_uScriptAct_SetBool_Target_22);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_22;
	}

	private void Relay_OnEnterTrigger_24()
	{
		Relay_In_20();
	}

	private void Relay_OnExitTrigger_24()
	{
	}

	private void Relay_True_30()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_30.True(out logic_uScriptAct_SetBool_Target_30);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_30;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_30.Out)
		{
			Relay_TurnOff_31();
		}
	}

	private void Relay_False_30()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_30.False(out logic_uScriptAct_SetBool_Target_30);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_30;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_30.Out)
		{
			Relay_TurnOff_31();
		}
	}

	private void Relay_OnOut_31()
	{
	}

	private void Relay_OffOut_31()
	{
	}

	private void Relay_ToggleOut_31()
	{
	}

	private void Relay_TurnOn_31()
	{
		int num = 0;
		if (startingTape_previous != startingTape || !m_RegisteredForEvents)
		{
			startingTape_previous = startingTape;
		}
		if (logic_uScriptAct_Toggle_Target_31.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Toggle_Target_31, num + 1);
		}
		logic_uScriptAct_Toggle_Target_31[num++] = startingTape;
		logic_uScriptAct_Toggle_uScriptAct_Toggle_31.TurnOn(logic_uScriptAct_Toggle_Target_31, logic_uScriptAct_Toggle_IgnoreChildren_31);
	}

	private void Relay_TurnOff_31()
	{
		int num = 0;
		if (startingTape_previous != startingTape || !m_RegisteredForEvents)
		{
			startingTape_previous = startingTape;
		}
		if (logic_uScriptAct_Toggle_Target_31.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Toggle_Target_31, num + 1);
		}
		logic_uScriptAct_Toggle_Target_31[num++] = startingTape;
		logic_uScriptAct_Toggle_uScriptAct_Toggle_31.TurnOff(logic_uScriptAct_Toggle_Target_31, logic_uScriptAct_Toggle_IgnoreChildren_31);
	}

	private void Relay_Toggle_31()
	{
		int num = 0;
		if (startingTape_previous != startingTape || !m_RegisteredForEvents)
		{
			startingTape_previous = startingTape;
		}
		if (logic_uScriptAct_Toggle_Target_31.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Toggle_Target_31, num + 1);
		}
		logic_uScriptAct_Toggle_Target_31[num++] = startingTape;
		logic_uScriptAct_Toggle_uScriptAct_Toggle_31.Toggle(logic_uScriptAct_Toggle_Target_31, logic_uScriptAct_Toggle_IgnoreChildren_31);
	}

	private void Relay_In_33()
	{
		logic_uScript_GetPromptResult_context_33 = local_Prompt_uScript_ShowPrompt_Context;
		logic_uScript_GetPromptResult_uScript_GetPromptResult_33.In(logic_uScript_GetPromptResult_context_33);
		bool accepted = logic_uScript_GetPromptResult_uScript_GetPromptResult_33.Accepted;
		bool declined = logic_uScript_GetPromptResult_uScript_GetPromptResult_33.Declined;
		if (accepted)
		{
			Relay_In_5();
		}
		if (declined)
		{
			Relay_In_8();
		}
	}
}
