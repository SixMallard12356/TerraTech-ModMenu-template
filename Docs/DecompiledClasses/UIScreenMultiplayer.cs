using System;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenMultiplayer : UIScreen
{
	[SerializeField]
	private GameObject m_Popup;

	[SerializeField]
	private GameObject m_PopupCloseButton;

	[SerializeField]
	private Text m_PopupHostLabel;

	public void CreateServer()
	{
		m_Popup.SetActive(value: true);
		m_PopupCloseButton.SetActive(value: false);
		m_PopupHostLabel.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuJoinMultiplayer, 3);
	}

	public void ClosePopup()
	{
		m_Popup.SetActive(value: false);
	}

	private void OnServerModeReady(string gameMode)
	{
		ClosePopup();
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
		Singleton.Manager<ManGameMode>.inst.NextModeSetting = new ManGameMode.ModeSettings();
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_SwitchAction = delegate
		{
			Singleton.Manager<ManGameMode>.inst.TriggerSwitch(System.Type.GetType(gameMode));
		};
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.SwitchToMode();
	}
}
