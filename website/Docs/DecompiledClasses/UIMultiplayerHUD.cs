using System;
using UnityEngine;
using UnityEngine.UI;

public class UIMultiplayerHUD : UIHUDElement
{
	[Serializable]
	public class Message
	{
		public enum StateTypes
		{
			None,
			Rebuild,
			SelfDestruct,
			Connection
		}

		[SerializeField]
		private Text m_Text;

		[SerializeField]
		private GameObject m_Object;

		public Event<string> SetEvent;

		protected float m_Timeout = float.NegativeInfinity;

		protected Action m_TimedOutCallback;

		public StateTypes State { get; private set; }

		public string Text
		{
			get
			{
				return m_Text.text;
			}
			private set
			{
				bool active = !string.IsNullOrEmpty(value);
				m_Object.SetActive(active);
				m_Text.text = value;
				SetEvent.Send(value);
			}
		}

		public void UpdateText(string newText)
		{
			float duration = (float.IsNegativeInfinity(m_Timeout) ? 0f : (m_Timeout - Time.time));
			SetTextWithTimeout(newText, State, duration, m_TimedOutCallback);
		}

		public void Clear()
		{
			SetText(string.Empty);
		}

		public void SetText(string text)
		{
			SetText(text, StateTypes.None);
		}

		public void SetText(string text, StateTypes state)
		{
			SetText_internal(text, state, 0f, null);
		}

		public void SetTextWithTimeout(string text, StateTypes state, float duration, Action timeOutLapsedCallback = null)
		{
			SetText_internal(text, state, duration, timeOutLapsedCallback);
		}

		private void SetText_internal(string text, StateTypes state, float duration, Action timeOutLapsedCallback)
		{
			Text = text;
			State = state;
			if (duration == 0f)
			{
				ClearTimeout();
			}
			else
			{
				SetTimeout(Time.time + duration);
			}
			m_TimedOutCallback = timeOutLapsedCallback;
		}

		private void ClearTimeout()
		{
			SetTimeout(float.NegativeInfinity);
		}

		private void SetTimeout(float timeout)
		{
			if (!float.IsNegativeInfinity(m_Timeout) && float.IsNegativeInfinity(timeout))
			{
				m_TimedOutCallback?.Invoke();
				m_TimedOutCallback = null;
			}
			m_Timeout = timeout;
		}

		public void Tick()
		{
			if (!float.IsNegativeInfinity(m_Timeout) && m_Timeout < Time.time)
			{
				ClearTimeout();
				Clear();
			}
		}
	}

	[SerializeField]
	private RectTransform m_TimerPanel;

	[SerializeField]
	private UITimer m_Timer;

	[SerializeField]
	protected Message m_Message1;

	[SerializeField]
	protected Message m_Message2;

	public bool TimerPanelVisible
	{
		set
		{
			m_TimerPanel.gameObject.SetActive(value);
		}
	}

	public UITimer Timer => m_Timer;

	public Message Message1 => m_Message1;

	public Message Message2 => m_Message2;

	public override void Show(object context)
	{
		base.Show(context);
		TimerPanelVisible = false;
		Message1.Clear();
		Message2.Clear();
	}

	private void Update()
	{
		Message1.Tick();
		Message2.Tick();
	}
}
