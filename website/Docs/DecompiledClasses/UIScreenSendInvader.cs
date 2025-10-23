using UnityEngine;

public class UIScreenSendInvader : UIScreen
{
	[SerializeField]
	private UITweet m_Tweet;

	[SerializeField]
	private string m_DefaultTags;

	[SerializeField]
	private string m_DefaultMessage;

	private Tank m_Invader;

	private Texture2D m_Snapshot;

	private TechData m_TechData;

	public bool SendingInvader { get; private set; }

	public void GoBackButton()
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	public void SendInvaderButton()
	{
		UIScreenTwitterAuth uIScreenTwitterAuth = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.TwitterAuth) as UIScreenTwitterAuth;
		uIScreenTwitterAuth.SetFailAction(delegate
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
		});
		uIScreenTwitterAuth.SetSuccessAction(delegate
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
			Singleton.Manager<TwitterAPI>.inst.UserEnabled = true;
			SendTweet();
		});
		uIScreenTwitterAuth.SetUseLegacyMode();
		Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenTwitterAuth);
	}

	private void SendTweet()
	{
		SendingInvader = true;
		Singleton.Manager<ManInvasion>.inst.StartSendingInvader(m_Invader, m_TechData.Name);
		string tweetMessage = m_Tweet.GetTweetMessage();
		Singleton.Manager<TwitterAPI>.inst.PostTweetAsync(tweetMessage, m_Snapshot.EncodeToPNG(), TweetSent, TweetFailed);
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
	}

	private void TweetSent(long tweetID)
	{
		Singleton.Manager<ManInvasion>.inst.SendInvaderSuccess(m_Invader, tweetID);
		SendingInvader = false;
	}

	private void TweetFailed()
	{
		Singleton.Manager<ManInvasion>.inst.SendInvaderFail(m_Invader);
		SendingInvader = false;
	}

	private void OnTechRendered(TechData techData, Texture2D techImage)
	{
		m_TechData = techData;
		m_Snapshot = techImage;
		if (techData != null)
		{
			string messageText = string.Format(m_DefaultMessage, m_TechData.Name);
			m_Tweet.SetupTweet(m_Snapshot, messageText, m_DefaultTags);
		}
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		m_Invader = Singleton.playerTank;
		Singleton.Manager<ManScreenshot>.inst.RenderTechImage((TrackedVisible)null, Singleton.Manager<ManScreenshot>.inst.DefaultSnapshotSize, encodeTechData: true, (ManScreenshot.OnTechRendered)OnTechRendered);
	}

	public override void Hide()
	{
		base.Hide();
	}
}
