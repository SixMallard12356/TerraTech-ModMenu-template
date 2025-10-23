using UnityEngine;
using UnityEngine.UI;

public class UIDeleteProfile : MonoBehaviour
{
	public void OnButtonClicked(Text profileNameLabel)
	{
		string profileName = profileNameLabel.text;
		int index = Singleton.Manager<ManProfile>.inst.GetProfiles().FindIndex((ManProfile.Profile x) => x.m_Name == profileName);
		if (index == -1)
		{
			return;
		}
		UIScreenNotifications uIScreenNotifications = (UIScreenNotifications)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen);
		string notification = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 10), profileName);
		uIScreenNotifications.Set(notification, delegate
		{
			Singleton.Manager<ManProfile>.inst.DeleteProfile(index);
			if (Singleton.Manager<ManProfile>.inst.GetProfiles().Count == 0)
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
				Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NewUser);
			}
			else
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			}
		}, delegate
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
		}, Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29), Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30));
		Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenNotifications);
	}
}
