using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
	public class DisplayOptions
	{
		public Timer Timer;

		public Color TagColor;

		public DisplayOptions(Timer Timer, Color TagColor = default(Color))
		{
			this.Timer = Timer;
			this.TagColor = TagColor;
		}
	}

	[SerializeField]
	protected Text m_Text;

	[SerializeField]
	protected TextMeshProUGUI m_TextMP;

	[SerializeField]
	protected Color m_RunningColour = Color.yellow;

	[SerializeField]
	protected Color m_StoppedColour = Color.white;

	[SerializeField]
	protected Image m_ColorTagImage;

	[SerializeField]
	protected int m_MaxMinutes = 999;

	[SerializeField]
	private bool m_ShowCentiSeconds = true;

	private Timer m_Timer;

	public void Reset(float startingTime = 0f)
	{
		m_Timer?.ResetTimeElapsed(startingTime);
	}

	public void SetTimeText(float displayTime)
	{
		string text = GetFormattedTimeString(displayTime);
		if (m_Text != null)
		{
			m_Text.text = text;
		}
		m_TextMP?.SetText(text);
		string GetFormattedTimeString(float time)
		{
			time = Mathf.Min(time, (float)(m_MaxMinutes * 60) + 59.999f);
			if (m_ShowCentiSeconds)
			{
				return $"{(int)time / 60:00}:{(int)time % 60:00}:{(int)(time * 100f) % 100:00}";
			}
			int num = (int)Mathf.Ceil(time);
			return $"{num / 60:00}:{num % 60:00}";
		}
	}

	public void SetTimer(DisplayOptions displayOptions)
	{
		if (m_Timer != null)
		{
			m_Timer.TickingStateChangedEvent.Unsubscribe(OnTimerTickingStateChanged);
			m_Timer.TimeElapsedChangedEvent.Unsubscribe(OnTimerElapsedTimeChanged);
		}
		m_Timer = displayOptions?.Timer;
		SetTimeText((m_Timer != null) ? m_Timer.DisplayTime : 0f);
		SetTextStateColor(m_Timer != null && m_Timer.IsTicking);
		SetTagColor(displayOptions?.TagColor ?? default(Color));
		if (m_Timer != null)
		{
			m_Timer.TickingStateChangedEvent.Subscribe(OnTimerTickingStateChanged);
			m_Timer.TimeElapsedChangedEvent.Subscribe(OnTimerElapsedTimeChanged);
		}
	}

	private void SetTextStateColor(bool ticking)
	{
		Color color = (ticking ? m_RunningColour : m_StoppedColour);
		if (m_Text != null)
		{
			m_Text.color = color;
		}
		if (m_TextMP != null)
		{
			m_TextMP.color = color;
		}
	}

	private void SetTagColor(Color color = default(Color))
	{
		if (!(m_ColorTagImage == null))
		{
			m_ColorTagImage.gameObject.SetActive(color != default(Color));
			m_ColorTagImage.color = color;
		}
	}

	private void OnTimerTickingStateChanged(bool state)
	{
		SetTextStateColor(state);
	}

	private void OnTimerElapsedTimeChanged(float timeElapsed)
	{
		SetTimeText(m_Timer.DisplayTime);
	}
}
