using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UICheckpointChallengeHUD : UIHUDElement
{
	[SerializeField]
	[FormerlySerializedAs("m_Timer")]
	private UITimer m_PrimaryUITimer;

	[SerializeField]
	private UITimer[] m_SecondaryUITimers;

	[SerializeField]
	private GameObject m_SecondaryTimersEllipsis;

	[SerializeField]
	private Text m_Speed;

	[SerializeField]
	private Text m_TopSpeed;

	[SerializeField]
	private Button m_TutorialButton;

	[SerializeField]
	private Transform m_GoingWrongWayMessage;

	[SerializeField]
	private Transform m_PassedCheckPointMessage;

	[SerializeField]
	[FormerlySerializedAs("m_BestTime")]
	private Text m_BestTimeTextDisplay;

	[SerializeField]
	private GameObject m_BestTimeContainer;

	[SerializeField]
	private const string DefaultBestTimeText = "--:--:--";

	protected Timer m_ChallengeTimer;

	protected string m_BestTimeText = "";

	protected ModuleCircuit_Time_Stopwatch[] m_CircuitStopwatches = new ModuleCircuit_Time_Stopwatch[0];

	public bool GoingWrongWayMessageVisible
	{
		set
		{
			if ((bool)m_GoingWrongWayMessage)
			{
				m_GoingWrongWayMessage.gameObject.SetActive(value);
			}
		}
	}

	public bool PassedCheckPointMessageVisible
	{
		set
		{
			if ((bool)m_PassedCheckPointMessage)
			{
				m_PassedCheckPointMessage.gameObject.SetActive(value);
			}
		}
	}

	public bool HasBestTimeText
	{
		get
		{
			if (m_BestTimeText != string.Empty)
			{
				return m_BestTimeText != "--:--:--";
			}
			return false;
		}
	}

	public Timer ActiveChallengeTimer => m_ChallengeTimer;

	public void Init(string bestTime = "--:--:--")
	{
		SetBestTimeText(bestTime);
	}

	public void SetSpeed(float speed)
	{
		if ((bool)m_Speed)
		{
			m_Speed.text = speed.ToString("000") + " mph";
		}
	}

	public void SetTopSpeed(float speed)
	{
		if ((bool)m_TopSpeed)
		{
			m_TopSpeed.text = speed.ToString("000") + " mph";
		}
	}

	public void SetChallengeTimer(ChallengeTimer timer)
	{
		m_ChallengeTimer = timer;
		RefreshUITimerDisplays();
	}

	public void SetBestTimeText(string timeText)
	{
		m_BestTimeText = ((timeText == "") ? "" : (Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HUD, 10) + ": " + timeText));
		RefreshUITimerDisplays();
	}

	public void SetCircuitTimers(ModuleCircuit_Time_Stopwatch[] circuitTimers)
	{
		m_CircuitStopwatches = circuitTimers;
		RefreshUITimerDisplays();
	}

	private void RefreshUITimerDisplays()
	{
		List<UITimer.DisplayOptions> list = new List<UITimer.DisplayOptions>();
		bool flag = m_ChallengeTimer != null;
		if (flag)
		{
			list.Add(new UITimer.DisplayOptions(m_ChallengeTimer));
		}
		for (int i = 0; i < m_CircuitStopwatches.Length; i++)
		{
			list.Add(new UITimer.DisplayOptions(m_CircuitStopwatches[i].Timer, m_CircuitStopwatches[i].TagColor));
		}
		for (int j = -1; j < m_SecondaryUITimers.Length; j++)
		{
			UITimer.DisplayOptions displayOptions = ((j + 1 < list.Count) ? list[j + 1] : null);
			UITimer obj = ((j == -1) ? m_PrimaryUITimer : m_SecondaryUITimers[j]);
			obj.gameObject.SetActive(displayOptions != null);
			obj.SetTimer(displayOptions);
			if (j + 1 == m_SecondaryUITimers.Length)
			{
				m_SecondaryTimersEllipsis.SetActive(j + 2 < list.Count);
			}
		}
		if (m_BestTimeTextDisplay != null)
		{
			bool active = flag && m_BestTimeText != "";
			if (m_BestTimeContainer != null)
			{
				m_BestTimeContainer.SetActive(active);
			}
			m_BestTimeTextDisplay.gameObject.SetActive(active);
			if (m_BestTimeTextDisplay.gameObject.activeSelf)
			{
				m_BestTimeTextDisplay.text = m_BestTimeText;
			}
		}
		if (list.Count > 0 && !base.IsShowing)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.CheckpointChallenge);
		}
		else if (list.Count == 0 && base.IsShowing)
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.CheckpointChallenge);
		}
	}

	public void SetTutorialButtonAction(UnityAction buttonAction)
	{
		if ((bool)m_TutorialButton)
		{
			m_TutorialButton.onClick.AddListener(buttonAction);
		}
	}

	public override void Show(object context)
	{
		base.Show(context);
		UIBlockLimit.SetHiddenByOverlappingUI(hide: true);
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		UIBlockLimit.SetHiddenByOverlappingUI(hide: false);
	}

	private void OnSpawn()
	{
		GoingWrongWayMessageVisible = false;
		PassedCheckPointMessageVisible = false;
	}

	private void OnRecycle()
	{
		if ((bool)m_TutorialButton)
		{
			m_TutorialButton.onClick.RemoveAllListeners();
		}
	}
}
