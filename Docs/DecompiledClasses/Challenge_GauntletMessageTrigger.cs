using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Challenge_GauntletMessageTrigger : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool holdMsg;

	private ManOnScreenMessages.OnScreenMessage local_5_ManOnScreenMessages_OnScreenMessage;

	public LocalisedString[] msgString = new LocalisedString[0];

	private GameObject owner_Connection_1;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_2 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_2 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_2 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_2;

	private string logic_uScript_AddOnScreenMessage_tag_2 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_2;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_2;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_2;

	private bool logic_uScript_AddOnScreenMessage_Out_2 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_2 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_4 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_4;

	private bool logic_uScript_RemoveOnScreenMessage_instant_4;

	private bool logic_uScript_RemoveOnScreenMessage_Out_4 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (!(null == owner_Connection_1) && m_RegisteredForEvents)
		{
			return;
		}
		owner_Connection_1 = parentGameObject;
		if (null != owner_Connection_1)
		{
			uScript_PlayerTrigger uScript_PlayerTrigger2 = owner_Connection_1.GetComponent<uScript_PlayerTrigger>();
			if (null == uScript_PlayerTrigger2)
			{
				uScript_PlayerTrigger2 = owner_Connection_1.AddComponent<uScript_PlayerTrigger>();
			}
			if (null != uScript_PlayerTrigger2)
			{
				uScript_PlayerTrigger2.OnEnterTrigger += Instance_OnEnterTrigger_0;
				uScript_PlayerTrigger2.OnExitTrigger += Instance_OnExitTrigger_0;
			}
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_1)
		{
			uScript_PlayerTrigger uScript_PlayerTrigger2 = owner_Connection_1.GetComponent<uScript_PlayerTrigger>();
			if (null == uScript_PlayerTrigger2)
			{
				uScript_PlayerTrigger2 = owner_Connection_1.AddComponent<uScript_PlayerTrigger>();
			}
			if (null != uScript_PlayerTrigger2)
			{
				uScript_PlayerTrigger2.OnEnterTrigger += Instance_OnEnterTrigger_0;
				uScript_PlayerTrigger2.OnExitTrigger += Instance_OnExitTrigger_0;
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
			uScript_PlayerTrigger component = owner_Connection_1.GetComponent<uScript_PlayerTrigger>();
			if (null != component)
			{
				component.OnEnterTrigger -= Instance_OnEnterTrigger_0;
				component.OnExitTrigger -= Instance_OnExitTrigger_0;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_2.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_4.SetParent(g);
		owner_Connection_1 = parentGameObject;
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
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_2.OnDisable();
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

	private void Instance_OnEnterTrigger_0(object o, EventArgs e)
	{
		Relay_OnEnterTrigger_0();
	}

	private void Instance_OnExitTrigger_0(object o, EventArgs e)
	{
		Relay_OnExitTrigger_0();
	}

	private void Relay_OnEnterTrigger_0()
	{
		Relay_In_2();
	}

	private void Relay_OnExitTrigger_0()
	{
		Relay_In_4();
	}

	private void Relay_In_2()
	{
		int num = 0;
		Array array = msgString;
		if (logic_uScript_AddOnScreenMessage_locString_2.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_2, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_2, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_holdMsg_2 = holdMsg;
		logic_uScript_AddOnScreenMessage_Return_2 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_2.In(logic_uScript_AddOnScreenMessage_locString_2, logic_uScript_AddOnScreenMessage_msgPriority_2, logic_uScript_AddOnScreenMessage_holdMsg_2, logic_uScript_AddOnScreenMessage_tag_2, logic_uScript_AddOnScreenMessage_speaker_2, logic_uScript_AddOnScreenMessage_side_2);
		local_5_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddOnScreenMessage_Return_2;
	}

	private void Relay_In_4()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_4 = local_5_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_4.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_4, logic_uScript_RemoveOnScreenMessage_instant_4);
	}
}
