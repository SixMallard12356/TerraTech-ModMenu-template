#define UNITY_EDITOR
using UnityEngine;

public class uScript_AddOnScreenMessage : uScriptLogic
{
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

	public ManOnScreenMessages.OnScreenMessage In(LocalisedString[] locString, ManOnScreenMessages.MessagePriority msgPriority, bool holdMsg = false, string tag = null, ManOnScreenMessages.Speaker speaker = ManOnScreenMessages.Speaker.GSOGeneric, ManOnScreenMessages.Side side = ManOnScreenMessages.Side.Left)
	{
		if (speaker == ManOnScreenMessages.Speaker.None)
		{
			speaker = ManOnScreenMessages.Speaker.GSOGeneric;
		}
		if (!m_Shown || Time.frameCount > m_ShownFrameNumber + 2)
		{
			if (locString != null && locString.Length != 0)
			{
				if (m_Message == null)
				{
					m_Message = new ManOnScreenMessages.OnScreenMessage(locString, msgPriority, holdMsg, tag, speaker, side);
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
				d.LogWarning("uScript_AddOnScreenMessage - LocalisedString not set");
				m_Shown = true;
			}
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
