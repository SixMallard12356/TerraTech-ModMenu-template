using System;
using System.Collections;
using UnityEngine;

public class UIRestartCurrentMode : MonoBehaviour
{
	[SerializeField]
	private bool m_ExitAllScreens = true;

	[SerializeField]
	private bool m_RestartWithoutSaveGame = true;

	[SerializeField]
	private bool m_ConfirmBeforeRestart;

	[SerializeField]
	private LocalisedString m_ConfirmMessage;

	[SerializeField]
	private LocalisedString m_AcceptButtonString;

	[SerializeField]
	private LocalisedString m_DeclineButtonString;

	private bool m_IsSwitchingToMode;

	public void RestartMode()
	{
		if (!m_IsSwitchingToMode)
		{
			m_IsSwitchingToMode = true;
			if (m_ConfirmBeforeRestart)
			{
				ShowRestartConfirm();
			}
			else
			{
				DoRestartMode();
			}
		}
	}

	private void DoRestartMode()
	{
		if (m_ExitAllScreens)
		{
			Singleton.Manager<ManUI>.inst.ExitAllScreens();
		}
		Singleton.instance.StartCoroutine(WaitForEndOfFade());
	}

	private IEnumerator WaitForEndOfFade()
	{
		while (!Singleton.Manager<ManUI>.inst.FadeFinished())
		{
			yield return null;
		}
		bool reloadSave = !m_RestartWithoutSaveGame;
		Singleton.Manager<ManGameMode>.inst.RestartCurrentMode(reloadSave);
		m_IsSwitchingToMode = false;
	}

	private void ShowRestartConfirm()
	{
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		Action accept = delegate
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
			DoRestartMode();
		};
		Action decline = delegate
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
			m_IsSwitchingToMode = false;
		};
		uIScreenNotifications.Set(m_ConfirmMessage.Value, accept, decline, m_AcceptButtonString.Value, m_DeclineButtonString.Value);
		Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenNotifications, ManUI.PauseType.Pause);
	}
}
