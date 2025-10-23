using UnityEngine;
using UnityEngine.UI;

public class UIScreenSumoChampion : UIScreen
{
	[SerializeField]
	private Text m_ResultText;

	[SerializeField]
	private Image m_PlayerImage;

	[SerializeField]
	private Text m_AchievedRankText;

	private SnapshotTwitter m_Player;

	public void SetScreenInfo(SnapshotTwitter player)
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 11);
		m_Player = player;
		player.ResolveThumbnail(delegate(Sprite s)
		{
			m_PlayerImage.sprite = s;
		});
		if (m_AchievedRankText != null)
		{
			int currentRank = Mode<ModeSumo>.inst.CurrentRank;
			m_AchievedRankText.text = currentRank.ToString();
		}
		m_ResultText.text = string.Format(localisedString, m_Player.techData.Name, Mode<ModeSumo>.inst.CurrentRank);
	}

	public void OnNext()
	{
		TweetWin();
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
		Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
	}

	private void TweetWin()
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 12);
		string rankedTweetLevel = Mode<ModeSumo>.inst.RankedTweetLevel;
		m_Player.techData.m_CreationData.userData = rankedTweetLevel;
		ManScreenshot.EncodeSnapshotRender(m_Player.techData, m_Player.image);
		Texture2D image = m_Player.image;
		string text = string.Format(localisedString, m_Player.techData.Name, rankedTweetLevel);
		Singleton.Manager<TwitterAPI>.inst.PostTweetAsync(text, image.EncodeToPNG());
	}
}
