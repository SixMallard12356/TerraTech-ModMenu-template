#define UNITY_EDITOR
using System;
using UnityEngine;

public class UIButtonSaveMode : MonoBehaviour
{
	[SerializeField]
	private bool m_ReportSaveSuccess;

	[SerializeField]
	private LocalisedString m_SaveSuccessString;

	[SerializeField]
	private LocalisedString m_SaveFailedString;

	public void OnButtonClicked()
	{
		bool success = false;
		if (Singleton.Manager<ManGameMode>.inst.CurrentModeCanSave())
		{
			success = Singleton.Manager<ManGameMode>.inst.SaveCurrentMode();
		}
		else
		{
			d.Log(string.Concat("UIButtonSaveMode - Trying to Save Game - But current gameType (", Singleton.Manager<ManGameMode>.inst.GetCurrentGameType(), ") does not allow saving!"));
		}
		if (m_ReportSaveSuccess)
		{
			ReportSaveSuccess(success);
		}
	}

	private void ReportSaveSuccess(bool success)
	{
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		string notification = (success ? m_SaveSuccessString.Value : m_SaveFailedString.Value);
		Action accept = ((!success) ? new Action(PopNotification) : new Action(PopNotificationAndResumeGame));
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
		uIScreenNotifications.Set(notification, accept, localisedString);
		Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenNotifications, ManUI.PauseType.Pause);
	}

	private void PopNotificationAndResumeGame()
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	private void PopNotification()
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
	}
}
