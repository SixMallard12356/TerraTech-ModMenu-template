#define UNITY_EDITOR
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;

public class ManOnScreenMessagesNetworked : Singleton.Manager<ManOnScreenMessagesNetworked>
{
	private struct HostData
	{
		public int m_ID;

		public ManOnScreenMessages.OnScreenMessage m_Message;

		public int m_AddMessageFrameCount;

		public EventNoParams m_HostCallback;

		public int m_EncounterId;

		public bool m_SentToClients;
	}

	[SerializeField]
	private int m_TimeoutClientMessageFrameCount = 3;

	[SerializeField]
	private int m_TimeoutHostMessageFrameCount = 5000;

	private List<HostData> m_HostMessages = new List<HostData>();

	private List<OnScreenMessageNetworkMessage> m_ClientMessages = new List<OnScreenMessageNetworkMessage>();

	private int m_NextNetworkMessageID = 1;

	private const string kHostControlledTag = "From_Host_Tag";

	public int AllocateNetworkMessageID()
	{
		return m_NextNetworkMessageID++;
	}

	public void AddMessage(OnScreenMessageNetworkMessage networkMsg, int encounterId)
	{
		if (!Singleton.Manager<ManOnScreenMessages>.inst.IsValidMessage(networkMsg.m_Message))
		{
			return;
		}
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			Singleton.Manager<ManOnScreenMessages>.inst.AddMessage(networkMsg.m_Message);
		}
		else if (ManNetwork.IsHost)
		{
			bool flag = false;
			int num = FindHostNetworkMessage(networkMsg.m_ID);
			if (num != -1)
			{
				HostData value = m_HostMessages[num];
				value.m_AddMessageFrameCount = Time.frameCount;
				if (!value.m_SentToClients)
				{
					value.m_SentToClients = true;
					flag = true;
				}
				m_HostMessages[num] = value;
			}
			else
			{
				HostData item = new HostData
				{
					m_ID = networkMsg.m_ID,
					m_Message = networkMsg.m_Message,
					m_AddMessageFrameCount = Time.frameCount,
					m_HostCallback = networkMsg.m_Message.Callback,
					m_EncounterId = encounterId,
					m_SentToClients = true
				};
				m_HostMessages.Add(item);
				flag = true;
			}
			if (flag)
			{
				Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.OnScreenMessageAdd, networkMsg);
			}
			if (networkMsg.m_Message.m_Priority != ManOnScreenMessages.MessagePriority.Medium)
			{
				return;
			}
			for (int num2 = m_HostMessages.Count - 1; num2 >= 0; num2--)
			{
				if (m_HostMessages[num2].m_EncounterId == encounterId)
				{
					ManOnScreenMessages.OnScreenMessage message = m_HostMessages[num2].m_Message;
					if (message != networkMsg.m_Message && message.m_Priority == ManOnScreenMessages.MessagePriority.Medium)
					{
						if (m_HostMessages[num2].m_SentToClients)
						{
							Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.OnScreenMessageRemove, new OnScreenMessageRemoveMessage
							{
								m_ID = m_HostMessages[num2].m_ID
							});
						}
						m_HostMessages.RemoveAt(num2);
					}
				}
			}
		}
		else
		{
			d.LogWarning("ManOnScreenMessagesNetworked: Ignoring network version of AddMessage called on non-host");
		}
	}

	public void ClearMessagesWithTag(string tag, bool clearCurrent, bool instant = false)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			for (int num = m_HostMessages.Count - 1; num >= 0; num--)
			{
				if (m_HostMessages[num].m_Message.m_Tag == tag)
				{
					RemoveHostMessageByIndex(num, clearCurrent, instant);
				}
			}
		}
		Singleton.Manager<ManOnScreenMessages>.inst.ClearMessagesWithTag(tag, clearCurrent, instant);
	}

	public void RemoveMessage(ManOnScreenMessages.OnScreenMessage message, bool instant = false)
	{
		bool flag = false;
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			for (int i = 0; i < m_HostMessages.Count; i++)
			{
				if (m_HostMessages[i].m_Message == message)
				{
					RemoveHostMessageByIndex(i, clearCurrent: true, instant);
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			for (int j = 0; j < m_ClientMessages.Count; j++)
			{
				if (m_ClientMessages[j].m_Message == message)
				{
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			Singleton.Manager<ManOnScreenMessages>.inst.RemoveMessage(message, instant);
		}
	}

	[Conditional("UNITY_EDITOR")]
	private void DebugLogInternal(string msg)
	{
		d.Log(msg);
	}

	private void RemoveHostMessageByIndex(int i, bool clearCurrent, bool instant)
	{
		int iD = m_HostMessages[i].m_ID;
		bool sentToClients = m_HostMessages[i].m_SentToClients;
		m_HostMessages.RemoveAt(i);
		if (sentToClients)
		{
			OnScreenMessageRemoveMessage message = new OnScreenMessageRemoveMessage
			{
				m_ID = iD,
				m_Explicit = clearCurrent,
				m_Instant = instant
			};
			Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.OnScreenMessageRemove, message);
		}
	}

	private int FindHostNetworkMessage(int netID)
	{
		for (int i = 0; i < m_HostMessages.Count; i++)
		{
			if (m_HostMessages[i].m_ID == netID)
			{
				return i;
			}
		}
		return -1;
	}

	private int FindClientNetworkMessage(int netID)
	{
		for (int i = 0; i < m_ClientMessages.Count; i++)
		{
			if (m_ClientMessages[i].m_ID == netID)
			{
				return i;
			}
		}
		return -1;
	}

	private bool CanSeeNetworkMessage(OnScreenMessageNetworkMessage msg)
	{
		return (msg.m_Position.ScenePosition - Singleton.playerPos).ToVector2XZ().magnitude <= msg.m_Radius;
	}

	private void OnPostModeExit(Mode exitedMode)
	{
		m_HostMessages.Clear();
		m_ClientMessages.Clear();
	}

	private void OnClientMessageFinishShowing(OnScreenMessageNetworkMessage msg)
	{
		int num = FindClientNetworkMessage(msg.m_ID);
		if (num >= 0)
		{
			m_ClientMessages.RemoveAt(num);
		}
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.OnScreenMessageFinished, new OnScreenMessageFinishedMessage
			{
				m_ID = msg.m_ID
			});
		}
		else
		{
			d.Log("Networked on screen message callback was fired after network shut down.");
		}
	}

	private void HandleMessageAddMessage(NetworkMessage netMsg)
	{
		OnScreenMessageNetworkMessage msg = netMsg.ReadMessage<OnScreenMessageNetworkMessage>();
		msg.m_Message.m_Tag = "From_Host_Tag";
		msg.m_Message.Callback.Subscribe(delegate
		{
			OnClientMessageFinishShowing(msg);
		});
		int num = FindClientNetworkMessage(msg.m_ID);
		if (num == -1)
		{
			m_ClientMessages.Add(msg);
		}
		else
		{
			m_ClientMessages[num] = msg;
		}
		if (CanSeeNetworkMessage(msg))
		{
			Singleton.Manager<ManOnScreenMessages>.inst.AddMessage(msg.m_Message);
		}
	}

	private void HandleMessageRemoveMessage(NetworkMessage netMsg)
	{
		OnScreenMessageRemoveMessage onScreenMessageRemoveMessage = netMsg.ReadMessage<OnScreenMessageRemoveMessage>();
		int num = FindClientNetworkMessage(onScreenMessageRemoveMessage.m_ID);
		if (num >= 0)
		{
			OnScreenMessageNetworkMessage onScreenMessageNetworkMessage = m_ClientMessages[num];
			m_ClientMessages.RemoveAt(num);
			if (onScreenMessageRemoveMessage.m_Explicit)
			{
				Singleton.Manager<ManOnScreenMessages>.inst.RemoveMessage(onScreenMessageNetworkMessage.m_Message, onScreenMessageRemoveMessage.m_Instant);
			}
		}
	}

	private void HandleClientFinishedDisplay(NetworkMessage netMsg)
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer);
		OnScreenMessageFinishedMessage onScreenMessageFinishedMessage = netMsg.ReadMessage<OnScreenMessageFinishedMessage>();
		int num = FindHostNetworkMessage(onScreenMessageFinishedMessage.m_ID);
		if (num >= 0)
		{
			HostData hostData = m_HostMessages[num];
			m_HostMessages.RemoveAt(num);
			if (hostData.m_SentToClients)
			{
				Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.OnScreenMessageRemove, new OnScreenMessageRemoveMessage
				{
					m_ID = onScreenMessageFinishedMessage.m_ID
				});
			}
			hostData.m_HostCallback.Send();
		}
	}

	private void Start()
	{
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(OnPostModeExit);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.OnScreenMessageAdd, HandleMessageAddMessage);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.OnScreenMessageRemove, HandleMessageRemoveMessage);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.OnScreenMessageFinished, HandleClientFinishedDisplay);
	}

	private void Update()
	{
		int frameCount = Time.frameCount;
		for (int num = m_HostMessages.Count - 1; num >= 0; num--)
		{
			if (frameCount > m_HostMessages[num].m_AddMessageFrameCount + m_TimeoutClientMessageFrameCount)
			{
				if (m_HostMessages[num].m_SentToClients)
				{
					HostData value = m_HostMessages[num];
					Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.OnScreenMessageRemove, new OnScreenMessageRemoveMessage
					{
						m_ID = value.m_ID
					});
					value.m_SentToClients = false;
					m_HostMessages[num] = value;
				}
				if (frameCount > m_HostMessages[num].m_AddMessageFrameCount + m_TimeoutHostMessageFrameCount)
				{
					_ = m_HostMessages[num];
					m_HostMessages.RemoveAt(num);
				}
			}
		}
		if (m_ClientMessages.Count <= 0)
		{
			return;
		}
		int highestUsedPriority = Singleton.Manager<ManOnScreenMessages>.inst.GetHighestUsedPriority();
		for (int num2 = m_ClientMessages.Count - 1; num2 >= 0; num2--)
		{
			OnScreenMessageNetworkMessage onScreenMessageNetworkMessage = m_ClientMessages[num2];
			if ((int)onScreenMessageNetworkMessage.m_Message.m_Priority > highestUsedPriority && CanSeeNetworkMessage(onScreenMessageNetworkMessage))
			{
				Singleton.Manager<ManOnScreenMessages>.inst.AddMessage(onScreenMessageNetworkMessage.m_Message);
				break;
			}
		}
	}
}
