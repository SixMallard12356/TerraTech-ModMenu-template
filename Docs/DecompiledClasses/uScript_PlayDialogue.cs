#define UNITY_EDITOR
using System;
using UnityEngine;

public class uScript_PlayDialogue : uScriptLogic
{
	[Serializable]
	public class Dialogue
	{
		public DialogueEntry[] entries;
	}

	[Serializable]
	public class DialogueEntry
	{
		public uScript_AddMessage.MessageData m_Message;

		public uScript_AddMessage.MessageSpeaker m_Speaker;
	}

	private Encounter m_Encounter;

	private bool m_AllShown;

	private bool m_CurrentMsgFinished;

	private bool m_BeginMessage;

	private int m_LastShown;

	private ManOnScreenMessages.OnScreenMessage m_Message;

	private OnScreenMessageNetworkMessage m_NetworkMessage;

	public bool Out => true;

	public bool Shown => m_AllShown;

	public bool BeginMessage => m_BeginMessage;

	public override void SetParent(GameObject parent)
	{
		base.SetParent(parent);
		m_Encounter = parent.GetComponent<Encounter>();
	}

	public void In(Dialogue dialogue, ref int progress)
	{
		if (progress < 0)
		{
			d.LogError("uScript_PlayDialogue is being fed negative progress");
			progress = 0;
		}
		if (m_CurrentMsgFinished)
		{
			progress++;
		}
		m_CurrentMsgFinished = false;
		int num = ((dialogue != null) ? dialogue.entries.Length : 0);
		m_AllShown = progress >= num;
		m_BeginMessage = !m_AllShown && m_LastShown != progress;
		if (m_AllShown)
		{
			return;
		}
		DialogueEntry dialogueEntry = dialogue.entries[progress];
		if (dialogueEntry.m_Message != null && dialogueEntry.m_Speaker != null && dialogueEntry.m_Message.m_LocStrings != null && dialogueEntry.m_Message.m_LocStrings.Length != 0)
		{
			if (m_LastShown != progress)
			{
				if (m_Message != null)
				{
					m_Message.Callback.Unsubscribe(OnMessageShown);
				}
				m_Message = new ManOnScreenMessages.OnScreenMessage(dialogueEntry.m_Message.m_LocStrings, dialogueEntry.m_Message.m_MsgPriority, dialogueEntry.m_Message.m_HoldMsg, dialogueEntry.m_Message.m_Tag, (ManOnScreenMessages.Speaker)dialogueEntry.m_Speaker.m_Speaker.GetValue(), dialogueEntry.m_Speaker.m_Side);
				m_Message.Callback.Subscribe(OnMessageShown);
			}
			if (m_Encounter == null || !Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				Singleton.Manager<ManOnScreenMessages>.inst.AddMessage(m_Message);
			}
			else if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				if (m_LastShown != progress)
				{
					m_NetworkMessage = new OnScreenMessageNetworkMessage(m_Encounter, m_Message);
					m_NetworkMessage.m_ID = Singleton.Manager<ManOnScreenMessagesNetworked>.inst.AllocateNetworkMessageID();
				}
				Singleton.Manager<ManOnScreenMessagesNetworked>.inst.AddMessage(m_NetworkMessage, m_Encounter.GetInstanceID());
			}
			m_LastShown = progress;
		}
		else
		{
			d.LogError($"uScript_PlayDialogue dialogue entry {progress} has null message, null speaker or no localised strings");
		}
	}

	public void OnEnable()
	{
		m_AllShown = false;
		m_CurrentMsgFinished = false;
		m_LastShown = -1;
		m_BeginMessage = false;
	}

	public void OnDisable()
	{
		if (m_Message != null)
		{
			m_Message.Callback.Unsubscribe(OnMessageShown);
		}
		m_Message = null;
		m_NetworkMessage = null;
	}

	private void OnMessageShown()
	{
		m_CurrentMsgFinished = true;
	}
}
