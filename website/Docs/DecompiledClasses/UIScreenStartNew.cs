using UnityEngine;
using UnityEngine.UI;

public class UIScreenStartNew : UIScreen
{
	[SerializeField]
	private RectTransform m_StartRandD;

	[SerializeField]
	private RectTransform m_UpsellRandD;

	[SerializeField]
	private UISeedGenerator m_SeedGenerator;

	[SerializeField]
	private Toggle m_TwitterToggle;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		bool active = false;
		bool active2 = false;
		if (Singleton.Manager<ManDLC>.inst.HasAnyDLCOfType(ManDLC.DLCType.RandD))
		{
			active = true;
		}
		else
		{
			active2 = true;
		}
		m_StartRandD.gameObject.SetActive(active);
		m_UpsellRandD.gameObject.SetActive(active2);
		m_TwitterToggle.isOn = Singleton.Manager<TwitterAPI>.inst.UserEnabled;
	}

	public void OnClickStartNewGame()
	{
		Singleton.Manager<ManGameMode>.inst.NextModeSetting = new ManGameMode.ModeSettings();
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_SwitchAction = Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeMain>;
		m_SeedGenerator.AddSeed();
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.SwitchToMode();
	}

	public void OnClickToggleTwitter()
	{
		if (m_TwitterToggle.isOn)
		{
			UIScreenTwitterAuth uIScreenTwitterAuth = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.TwitterAuth) as UIScreenTwitterAuth;
			uIScreenTwitterAuth.SetFailAction(delegate
			{
				m_TwitterToggle.isOn = false;
				Singleton.Manager<ManUI>.inst.PopScreen();
			});
			uIScreenTwitterAuth.SetSuccessAction(delegate
			{
				Singleton.Manager<TwitterAPI>.inst.UserEnabled = true;
				Singleton.Manager<ManUI>.inst.PopScreen();
			});
			uIScreenTwitterAuth.SetUseLegacyMode();
			Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenTwitterAuth);
		}
		else
		{
			Singleton.Manager<TwitterAPI>.inst.UserEnabled = false;
		}
	}
}
