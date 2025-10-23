using UnityEngine;
using UnityEngine.UI;

public class UIScreenSumoResults : UIScreen
{
	public Text m_ResultText;

	public Text m_OpponentText;

	public Text m_AchievedRankText;

	public Text m_DescriptionText;

	public Text m_ButtonDescriptionText;

	public Text m_OpponentLoserText;

	public Image m_PlayerImage;

	public Image m_OpponentImage;

	public Image m_OpponentResultImage;

	public Sprite m_WonSprite;

	public Sprite m_LostSprite;

	private bool m_Won;

	private ModeSumo.Contestants m_Contestants;

	public void SetScreenInfo(ModeSumo.Contestants contestants, bool won)
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 17);
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 21);
		string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 18);
		string localisedString4 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 22);
		m_Won = won;
		m_Contestants = contestants;
		ModeSumo.Contestants.ContestantData contestant = contestants.GetContestant(0);
		Texture2D image = contestant.snapshot.image;
		if (m_PlayerImage != null && contestant.snapshot.image != null)
		{
			m_PlayerImage.sprite = Sprite.Create(image, new Rect(0f, 0f, image.width, image.height), new Vector2(0.5f, 0.5f));
		}
		ModeSumo.Contestants.ContestantData contestant2 = contestants.GetContestant(1);
		Texture2D image2 = contestant2.snapshot.image;
		if (m_OpponentImage != null && image2 != null)
		{
			m_OpponentImage.sprite = Sprite.Create(image2, new Rect(0f, 0f, image2.width, image2.height), new Vector2(0.5f, 0.5f));
		}
		if (m_AchievedRankText != null)
		{
			int currentRank = Mode<ModeSumo>.inst.CurrentRank;
			m_AchievedRankText.text = currentRank.ToString();
		}
		if (m_Won)
		{
			m_ResultText.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 16);
			m_OpponentLoserText.enabled = true;
			m_OpponentText.text = string.Format(localisedString, contestant2.snapshot.techData.Name, contestant2.snapshot.creator);
			m_DescriptionText.text = string.Format(localisedString3, contestant.snapshot.techData.Name, Mode<ModeSumo>.inst.CurrentRank);
			m_ButtonDescriptionText.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 19);
			m_OpponentResultImage.sprite = m_LostSprite;
		}
		else
		{
			m_ResultText.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 20);
			m_OpponentLoserText.enabled = false;
			m_OpponentText.text = string.Format(localisedString2, contestant2.snapshot.techData.Name, contestant2.snapshot.creator);
			m_DescriptionText.text = string.Format(localisedString4, contestant.snapshot.techData.Name, Mode<ModeSumo>.inst.CurrentRank, Mode<ModeSumo>.inst.RankedGameCount);
			m_ButtonDescriptionText.text = string.Empty;
			m_OpponentResultImage.sprite = m_WonSprite;
		}
	}

	public void OnNext()
	{
		if (m_Won)
		{
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.SumoRankedEnemies);
			return;
		}
		TweetLoss();
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
		Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
	}

	private void TweetLoss()
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 9);
		string rankedTweetLevel = Mode<ModeSumo>.inst.RankedTweetLevel;
		ModeSumo.Contestants.ContestantData contestant = m_Contestants.GetContestant(0);
		contestant.snapshot.techData.m_CreationData.userData = rankedTweetLevel;
		Texture2D image = contestant.snapshot.image;
		ManScreenshot.EncodeSnapshotRender(contestant.snapshot.techData, image);
		string text = string.Format(localisedString, contestant.snapshot.techData.Name, rankedTweetLevel);
		Singleton.Manager<TwitterAPI>.inst.PostTweetAsync(text, image.EncodeToPNG());
	}
}
