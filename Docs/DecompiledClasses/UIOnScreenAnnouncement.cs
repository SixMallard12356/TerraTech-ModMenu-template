using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOnScreenAnnouncement : UIHUDElement
{
	public class MessageParams
	{
		public State m_MessageState;

		public string m_MessageText;

		public float m_FadeInTime;

		public float m_ShowTime;

		public float m_FadeOutTime;

		public MessageParams()
		{
			m_MessageState = State.Inactive;
			m_MessageText = null;
			m_FadeInTime = 0f;
			m_ShowTime = 0f;
			m_FadeOutTime = 0f;
		}
	}

	public enum State
	{
		Inactive,
		FadeIn,
		Showing,
		FadeOut
	}

	public Text m_AnnouncementText;

	private Queue<MessageParams> m_MessageQueue = new Queue<MessageParams>();

	private MessageParams m_CurrentMessage;

	private float m_Timer;

	public override void Show(object context)
	{
		base.Show(context);
		MessageParams item = context as MessageParams;
		m_MessageQueue.Enqueue(item);
	}

	private void SetScale(float scale)
	{
		m_AnnouncementText.transform.localScale = new Vector3(scale, scale, 1f);
	}

	private void UpdateMessage()
	{
		switch (m_CurrentMessage.m_MessageState)
		{
		case State.Inactive:
			m_AnnouncementText.text = m_CurrentMessage.m_MessageText;
			m_CurrentMessage.m_MessageState = State.FadeIn;
			SetScale(0f);
			m_Timer = 0f;
			break;
		case State.FadeIn:
			m_Timer += Time.deltaTime;
			m_Timer = Mathf.Min(m_Timer, m_CurrentMessage.m_FadeInTime);
			SetScale(m_Timer / m_CurrentMessage.m_FadeInTime);
			if (m_Timer >= m_CurrentMessage.m_FadeInTime)
			{
				m_Timer = 0f;
				m_CurrentMessage.m_MessageState = State.Showing;
			}
			break;
		case State.Showing:
			if (m_Timer >= m_CurrentMessage.m_ShowTime)
			{
				m_Timer = 0f;
				m_CurrentMessage.m_MessageState = State.FadeOut;
			}
			else
			{
				m_Timer += Time.deltaTime;
			}
			break;
		case State.FadeOut:
			if (m_Timer >= m_CurrentMessage.m_FadeOutTime)
			{
				m_Timer = 0f;
				SetScale(0f);
				m_CurrentMessage.m_MessageState = State.FadeOut;
				m_CurrentMessage = null;
			}
			else
			{
				m_Timer = Mathf.Min(m_Timer, m_CurrentMessage.m_FadeOutTime);
				SetScale(1f - m_Timer / m_CurrentMessage.m_FadeOutTime);
				m_Timer += Time.deltaTime;
			}
			break;
		}
	}

	private void Awake()
	{
		m_AnnouncementText.text = null;
	}

	private void Update()
	{
		if (m_CurrentMessage == null && m_MessageQueue.Count > 0)
		{
			m_CurrentMessage = m_MessageQueue.Dequeue();
		}
		if (m_CurrentMessage != null)
		{
			UpdateMessage();
		}
	}

	private void OnRecycle()
	{
		m_AnnouncementText.text = null;
		m_MessageQueue.Clear();
		m_CurrentMessage = null;
	}
}
