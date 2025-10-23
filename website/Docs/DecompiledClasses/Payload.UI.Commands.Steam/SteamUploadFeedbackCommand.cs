namespace Payload.UI.Commands.Steam;

public class SteamUploadFeedbackCommand : Command<SteamUploadData>
{
	private SteamUploadData m_Data;

	public override void Execute(SteamUploadData data)
	{
		m_Data = data;
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		if (data.m_NeedsToAcceptAgreement)
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 41);
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 4);
			uIScreenNotifications.Set(localisedString, OnGotoSteam, localisedString2);
		}
		else
		{
			string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 40);
			string localisedString4 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 4);
			string localisedString5 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 10);
			uIScreenNotifications.Set(localisedString3, OnGotoSteam, OnContinue, localisedString4, localisedString5);
		}
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications, ManUI.PauseType.Pause);
	}

	private void OnGotoSteam()
	{
		m_Data.m_GotoItemOnSteam = true;
		Singleton.Manager<ManUI>.inst.PopScreen();
		SetComplete(m_Data);
	}

	private void OnContinue()
	{
		m_Data.m_GotoItemOnSteam = false;
		Singleton.Manager<ManUI>.inst.PopScreen();
		SetComplete(m_Data);
	}
}
