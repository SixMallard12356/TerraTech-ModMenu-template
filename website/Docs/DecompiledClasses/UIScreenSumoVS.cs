using Spring.Social.Twitter.Api;

public class UIScreenSumoVS : UIScreen
{
	public UISumoPlayerInfo m_Player1;

	public UISumoPlayerInfo m_Player2;

	public void SetScreenInfo(ModeSumo.Contestants contestants)
	{
		if (contestants == null)
		{
			return;
		}
		if ((bool)m_Player1)
		{
			ModeSumo.Contestants.ContestantData contestant = contestants.GetContestant(0);
			string profileImageUrl = contestant.snapshot.profileImageUrl;
			string twitterHandle = contestant.snapshot.creator;
			TwitterProfile twitterProfile = Singleton.Manager<TwitterAPI>.inst.GetTwitterProfile();
			if (twitterProfile != null)
			{
				profileImageUrl = twitterProfile.ProfileImageUrl;
				twitterHandle = twitterProfile.ScreenName;
			}
			m_Player1.SetPlayerInfo(profileImageUrl, twitterHandle, contestant.snapshot.techData.Name, contestant.snapshot.image, Mode<ModeSumo>.inst.CurrentRank);
		}
		if ((bool)m_Player2)
		{
			ModeSumo.Contestants.ContestantData contestant2 = contestants.GetContestant(1);
			m_Player2.SetPlayerInfo(contestant2.snapshot.profileImageUrl, contestant2.snapshot.creator, contestant2.snapshot.techData.Name, contestant2.snapshot.image, Mode<ModeSumo>.inst.EnemyRank);
		}
	}
}
