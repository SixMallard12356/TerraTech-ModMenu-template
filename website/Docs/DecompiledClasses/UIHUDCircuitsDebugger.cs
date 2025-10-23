using Rewired;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHUDCircuitsDebugger : UIHUDElement
{
	[SerializeField]
	private Toggle m_TogglePauseButton;

	[SerializeField]
	private Image m_PausedIcon;

	[SerializeField]
	private Button m_StepFrameButton;

	[SerializeField]
	private Button m_SlowPlayButton;

	[SerializeField]
	private TextMeshProUGUI m_SlowPlayLabel;

	[SerializeField]
	private int[] m_PlaybackSpeedDivisors;

	[SerializeField]
	private Button m_CloseButton;

	private int m_CurrentPlaybackSpeedIdx;

	private bool m_IsPaused;

	public override void Show(object context)
	{
		m_CurrentPlaybackSpeedIdx = -1;
		m_SlowPlayLabel.text = "1";
		base.Show(context);
	}

	public override void Hide(object context)
	{
		SetPaused(paused: false);
		Circuits.Time.SetExecutionSpeed(1f);
		base.Hide(context);
	}

	private void SetPaused(bool paused)
	{
		if (paused != m_IsPaused)
		{
			Circuits.Time.SetExecutionPaused(paused);
			m_TogglePauseButton.SetValue(paused);
			m_PausedIcon.enabled = paused;
			m_IsPaused = paused;
		}
	}

	private void StepFrame()
	{
		if (!m_IsPaused)
		{
			SetPaused(paused: true);
		}
		Circuits.Time.PauseAndAdvanceFrame();
	}

	private void CyclePlaybackSpeed()
	{
		m_CurrentPlaybackSpeedIdx++;
		if (m_CurrentPlaybackSpeedIdx >= m_PlaybackSpeedDivisors.Length)
		{
			m_CurrentPlaybackSpeedIdx = -1;
		}
		float executionSpeed = ((m_CurrentPlaybackSpeedIdx == -1) ? 1f : (1f / (float)m_PlaybackSpeedDivisors[m_CurrentPlaybackSpeedIdx]));
		m_SlowPlayLabel.text = ((m_CurrentPlaybackSpeedIdx == -1) ? "1" : $"1/{m_PlaybackSpeedDivisors[m_CurrentPlaybackSpeedIdx]}");
		Circuits.Time.SetExecutionSpeed(executionSpeed);
	}

	private void OnTogglePauseClicked(bool isOn)
	{
		SetPaused(isOn);
	}

	private void OnStepFrameClicked()
	{
		StepFrame();
	}

	private void OnSlowPlayClicked()
	{
		CyclePlaybackSpeed();
	}

	private void OnManuallyAdvanceFrame(InputActionEventData evtData)
	{
		StepFrame();
	}

	private void OnCloseClicked()
	{
		HideSelf();
	}

	private void OnSpawn()
	{
		m_TogglePauseButton.onValueChanged.AddListener(OnTogglePauseClicked);
		m_StepFrameButton.onClick.AddListener(OnStepFrameClicked);
		m_SlowPlayButton.onClick.AddListener(OnSlowPlayClicked);
		m_CloseButton.onClick.AddListener(OnCloseClicked);
	}

	private void OnRecycle()
	{
		m_TogglePauseButton.onValueChanged.RemoveAllListeners();
		m_StepFrameButton.onClick.RemoveAllListeners();
		m_SlowPlayButton.onClick.RemoveAllListeners();
		m_CloseButton.onClick.RemoveAllListeners();
	}
}
