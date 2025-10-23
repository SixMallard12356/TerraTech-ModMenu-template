#define UNITY_EDITOR
using System;
using UnityEngine;

public class uScript_AddMessage : uScriptLogic
{
	[Serializable]
	public class MessageData
	{
		public LocalisedString[] m_LocStrings;

		public ManOnScreenMessages.MessagePriority m_MsgPriority = ManOnScreenMessages.MessagePriority.Medium;

		public bool m_HoldMsg;

		public string m_Tag;
	}

	[Serializable]
	public class MessageSpeaker
	{
		[EnumString(typeof(ManOnScreenMessages.Speaker))]
		public EnumString m_Speaker = new EnumString(typeof(ManOnScreenMessages.Speaker), 1);

		public ManOnScreenMessages.Side m_Side;
	}

	private bool m_Shown;

	private ManOnScreenMessages.OnScreenMessage m_Message;

	private OnScreenMessageNetworkMessage m_NetworkMessage;

	private int m_ShownFrameNumber;

	private Encounter m_Encounter;

	public bool Out => true;

	public bool Shown => m_Shown;

	public override void SetParent(GameObject parent)
	{
		base.SetParent(parent);
		m_Encounter = parent.GetComponent<Encounter>();
	}

	public ManOnScreenMessages.OnScreenMessage In(MessageData messageData, MessageSpeaker speaker)
	{
		if (messageData != null)
		{
			if (speaker != null)
			{
				if (!m_Shown || Time.frameCount > m_ShownFrameNumber + 2)
				{
					if (messageData.m_LocStrings != null && messageData.m_LocStrings.Length != 0)
					{
						if (m_Message == null)
						{
							m_Message = new ManOnScreenMessages.OnScreenMessage(messageData.m_LocStrings, messageData.m_MsgPriority, messageData.m_HoldMsg, messageData.m_Tag, (ManOnScreenMessages.Speaker)speaker.m_Speaker.GetValue(), speaker.m_Side);
							m_Message.Callback.Subscribe(OnMessageShown);
						}
						if (m_Encounter == null || !Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
						{
							Singleton.Manager<ManOnScreenMessages>.inst.AddMessage(m_Message);
						}
						else if (Singleton.Manager<ManNetwork>.inst.IsServer)
						{
							if (m_NetworkMessage == null)
							{
								m_NetworkMessage = new OnScreenMessageNetworkMessage(m_Encounter, m_Message);
								m_NetworkMessage.m_ID = Singleton.Manager<ManOnScreenMessagesNetworked>.inst.AllocateNetworkMessageID();
							}
							Singleton.Manager<ManOnScreenMessagesNetworked>.inst.AddMessage(m_NetworkMessage, m_Encounter.GetInstanceID());
						}
					}
					else
					{
						d.LogWarning("uScript_AddMessage - LocalisedString not set");
						m_Shown = true;
					}
				}
			}
			else
			{
				d.LogWarning("uScript_AddMessage - No Speaker");
			}
		}
		else
		{
			d.LogWarning("uScript_AddMessage - No Message Data");
		}
		return m_Message;
	}

	public void OnDisable()
	{
		if (m_Message != null)
		{
			m_Message.Callback.Unsubscribe(OnMessageShown);
		}
		m_Shown = false;
		m_Message = null;
		m_NetworkMessage = null;
	}

	private void OnMessageShown()
	{
		m_Shown = true;
		m_ShownFrameNumber = Time.frameCount;
	}
}
