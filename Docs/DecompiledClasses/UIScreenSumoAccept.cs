using UnityEngine;
using UnityEngine.UI;

public class UIScreenSumoAccept : UIScreen
{
	public Text m_TweetMessage;

	public Image m_VehicleImage;

	private SnapshotTwitter m_PlayerCapture;

	public void SetScreenData(SnapshotTwitter snapshotTwitter)
	{
		m_PlayerCapture = snapshotTwitter;
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 9);
		m_TweetMessage.text = string.Format(localisedString, snapshotTwitter.techData.Name, Mode<ModeSumo>.inst.RankedHashtag);
		snapshotTwitter.ResolveThumbnail(delegate(Sprite s)
		{
			m_VehicleImage.sprite = s;
		});
	}

	public void OnAgree()
	{
		Mode<ModeSumo>.inst.ClearRankedSettings();
		Singleton.Manager<ManUI>.inst.PopScreen(showPrev: false);
		UIScreenSumoLoadContestants uIScreenSumoLoadContestants = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.SumoRankedEnemies) as UIScreenSumoLoadContestants;
		if ((bool)uIScreenSumoLoadContestants)
		{
			uIScreenSumoLoadContestants.SetPlayer(m_PlayerCapture);
			Singleton.Manager<ManUI>.inst.PushScreen(uIScreenSumoLoadContestants);
		}
	}

	public void OnDecline()
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
	}
}
