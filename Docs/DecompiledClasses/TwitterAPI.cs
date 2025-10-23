#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Spring.IO;
using Spring.Social.Twitter.Api;
using UnityEngine;

public class TwitterAPI : Singleton.Manager<TwitterAPI>
{
	public class TwitterSettings
	{
		private enum EnabledState
		{
			IfValid,
			Disabled,
			Enabled
		}

		public string m_AccessToken = "";

		public string m_AccessTokenSecret = "";

		public string m_UserId = "";

		public string m_ScreenName = "";

		private EnabledState m_UserEnabled;

		public bool UserEnabled
		{
			get
			{
				if (m_UserEnabled != EnabledState.IfValid)
				{
					return m_UserEnabled == EnabledState.Enabled;
				}
				return Valid;
			}
			set
			{
				m_UserEnabled = ((!value) ? EnabledState.Disabled : EnabledState.Enabled);
			}
		}

		public bool Valid
		{
			get
			{
				if (!string.IsNullOrEmpty(m_UserId) && !string.IsNullOrEmpty(m_ScreenName) && !string.IsNullOrEmpty(m_AccessToken))
				{
					return !string.IsNullOrEmpty(m_AccessTokenSecret);
				}
				return false;
			}
		}

		public void Clear()
		{
			m_AccessToken = "";
			m_AccessTokenSecret = "";
			m_UserId = "";
			m_ScreenName = "";
		}
	}

	public struct TweetWithMedia
	{
		public long TweetID;

		public string link;

		public string tag;

		public string ScreenName;

		public string Name;

		public string ProfileImageUrl;

		public byte[] ImageArray;

		public int Favourites;

		public DateTime DateCreated;
	}

	public class TweetWithMediaDataThreaded : ThreadWorker.ThreadedData
	{
		public List<TweetWithMedia> m_Links = new List<TweetWithMedia>();
	}

	[SerializeField]
	private TwitterAuthenticator m_Authenticator;

	private List<TweetWithMedia> m_CachedTweets;

	private TwitterSettings m_Settings = new TwitterSettings();

	private ThreadWorker m_ThreadWorker;

	public string ScreenName => m_Settings.m_ScreenName;

	public bool UserEnabled
	{
		get
		{
			if (m_Settings == null)
			{
				return false;
			}
			return m_Settings.UserEnabled;
		}
		set
		{
			if (m_Settings != null)
			{
				m_Settings.UserEnabled = value;
			}
		}
	}

	public void TryAuthenticate(Action<bool> loggedInCallback)
	{
		bool ignoreUserEnabled = false;
		DoTryAuthenticateOnThreadWorker(loggedInCallback, ignoreUserEnabled);
	}

	public void TryAuthenticateIgnoreUserEnabled(Action<bool> loggedInCallback)
	{
		bool ignoreUserEnabled = true;
		DoTryAuthenticateOnThreadWorker(loggedInCallback, ignoreUserEnabled);
	}

	private void LoadUserInfo()
	{
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (currentUser != null && currentUser.m_TwitterSettings != null)
		{
			m_Settings = currentUser.m_TwitterSettings;
		}
		else
		{
			m_Settings.Clear();
		}
		if (m_Settings.Valid)
		{
			d.Log(string.Concat("LoadTwitterUserInfo - succeeded" + "\n    UserId : " + m_Settings.m_UserId, "\n    ScreenName : ", m_Settings.m_ScreenName));
		}
	}

	public void Authenticate(Action successAction, Action failAction, TwitterAuthenticator.PinEntryForm pinEntry)
	{
		Action<string, string> successAction2 = delegate(string accessToken, string accessSecret)
		{
			m_Settings.m_AccessToken = accessToken;
			m_Settings.m_AccessTokenSecret = accessSecret;
			try
			{
				TwitterProfile userProfile = m_Authenticator.ServiceProvider.GetApi(accessToken, accessSecret).UserOperations.GetUserProfile();
				m_Settings.m_UserId = userProfile.ID.ToString();
				m_Settings.m_ScreenName = userProfile.ScreenName;
			}
			catch (Exception ex)
			{
				d.Log("Authenticate failed: " + ex);
				m_Settings.m_UserId = "";
				m_Settings.m_ScreenName = "";
			}
			successAction();
		};
		m_Authenticator.BeginAuthentication(successAction2, failAction, pinEntry);
	}

	public void Cancel()
	{
		if (m_Authenticator != null)
		{
			m_Authenticator.Cancel();
		}
		if (m_ThreadWorker != null)
		{
			m_ThreadWorker.CancelAllActions();
		}
	}

	public void PostTweetAsync(string text, byte[] i, Action<long> returnTweetID = null, Action tweetFailed = null)
	{
		m_ThreadWorker.AddAction(PostTweet(text, i, returnTweetID, tweetFailed), tweetFailed);
	}

	public void FlagTweetAsFavouriteAsync(long tweetID, Action<bool> completedCallback = null)
	{
		m_ThreadWorker.AddAction(FlagTweetAsFavourite(tweetID, completedCallback), delegate
		{
			if (completedCallback != null)
			{
				completedCallback(obj: false);
			}
		});
	}

	public void RetrieveTweetAsync(long tweetID, Action<Tweet> fetchedTweet)
	{
		m_ThreadWorker.AddAction(RetrieveTweet(tweetID, fetchedTweet), delegate
		{
			if (fetchedTweet != null)
			{
				fetchedTweet(null);
			}
		});
	}

	public void RetrieveTaggedTweetsSinceAsync(string tag, bool myTweets, long sinceID, TweetWithMediaDataThreaded loadedTweets, Action callback = null)
	{
		m_ThreadWorker.AddAction(RetrieveTaggedTweetsSince(tag, myTweets, sinceID, loadedTweets, callback), delegate
		{
			if (callback != null)
			{
				callback();
			}
		});
	}

	public void RetrieveTaggedTweetsAsync(string tag, bool myTweets, TweetWithMediaDataThreaded loadedTweets, Action callback = null)
	{
		m_ThreadWorker.AddAction(RetrieveTaggedTweetsSince(tag, myTweets, -1L, loadedTweets, callback), delegate
		{
			if (callback != null)
			{
				callback();
			}
		});
	}

	public TwitterProfile GetTwitterProfile()
	{
		TwitterProfile result = null;
		try
		{
			result = m_Authenticator.ServiceProvider.GetApi(m_Settings.m_AccessToken, m_Settings.m_AccessTokenSecret).UserOperations.GetUserProfile();
		}
		catch (Exception ex)
		{
			d.Log("GetTwitterProfile failed: " + ex);
		}
		return result;
	}

	private void DoTryAuthenticateOnThreadWorker(Action<bool> loggedInCallback, bool ignoreUserEnabled)
	{
		if (m_Settings.Valid)
		{
			m_ThreadWorker.AddAction(CheckAuthorization(loggedInCallback, ignoreUserEnabled), delegate
			{
				loggedInCallback(obj: false);
			});
		}
		else
		{
			loggedInCallback(obj: false);
		}
	}

	private IEnumerator CheckAuthorization(Action<bool> loggedInCallback, bool ignoreUserEnabled)
	{
		bool obj = false;
		if ((ignoreUserEnabled || m_Settings.UserEnabled) && m_Settings.Valid)
		{
			try
			{
				m_Authenticator.ServiceProvider.GetApi(m_Settings.m_AccessToken, m_Settings.m_AccessTokenSecret).TimelineOperations.GetUserTimeline(1);
				obj = true;
			}
			catch (Exception ex)
			{
				d.Log("Twitter Auth failed: " + ex);
				m_Settings.Clear();
			}
		}
		loggedInCallback?.Invoke(obj);
		yield return null;
	}

	private IEnumerator RetrieveTaggedTweetsSince(string tag, bool myTweets, long sinceID, TweetWithMediaDataThreaded loadedTweets, Action completionCallback)
	{
		for (int i = 0; i < m_CachedTweets.Count; i++)
		{
			if (tag == m_CachedTweets[i].tag && m_CachedTweets[i].TweetID > sinceID)
			{
				lock (loadedTweets.m_Lock)
				{
					loadedTweets.m_Links.Add(m_CachedTweets[i]);
				}
			}
		}
		List<Tweet> tweets;
		try
		{
			ITwitter api = m_Authenticator.ServiceProvider.GetApi(m_Settings.m_AccessToken, m_Settings.m_AccessTokenSecret);
			if (myTweets)
			{
				List<Tweet> list = ((sinceID == -1) ? (api.TimelineOperations.GetUserTimeline(api.UserOperations.GetUserProfile().ScreenName, 100) as List<Tweet>) : (api.TimelineOperations.GetUserTimeline(api.UserOperations.GetUserProfile().ScreenName, 100, sinceID, long.MaxValue) as List<Tweet>));
				tweets = list.FindAll((Tweet tweet) => tweet.Text.Contains(tag));
			}
			else
			{
				SearchResults searchResults = ((sinceID == -1) ? api.SearchOperations.Search(tag, 500) : api.SearchOperations.Search(tag, 500, sinceID, long.MaxValue));
				tweets = searchResults.Tweets as List<Tweet>;
			}
		}
		catch (Exception ex)
		{
			d.Log("RetrieveTaggedTweetsSince GetUserTimeline failed: " + ex);
			tweets = new List<Tweet>();
		}
		yield return null;
		foreach (Tweet t in tweets)
		{
			try
			{
				if (t.Entities.Media.Count > 0)
				{
					if (m_CachedTweets.Any((TweetWithMedia x) => x.TweetID == t.ID))
					{
						continue;
					}
					foreach (MediaEntity medium in t.Entities.Media)
					{
						if ((t.RetweetCount == 0 || t.RetweetedStatus == null) && medium.MediaUrl != null)
						{
							TweetWithMedia item = new TweetWithMedia
							{
								link = medium.MediaUrl,
								TweetID = t.ID,
								tag = tag,
								ScreenName = t.User.ScreenName,
								Name = t.User.Name,
								ProfileImageUrl = t.User.ProfileImageUrl,
								Favourites = t.FavoriteCount,
								DateCreated = (t.CreatedAt ?? DateTime.MinValue)
							};
							WebClient webClient = new WebClient();
							item.ImageArray = webClient.DownloadData(medium.MediaUrl + ":large");
							lock (loadedTweets.m_Lock)
							{
								loadedTweets.m_Links.Add(item);
							}
							m_CachedTweets.Add(item);
						}
					}
				}
			}
			catch (Exception ex2)
			{
				d.Log("RetrieveTaggedTweetsSince DownloadData failed: " + ex2);
			}
			yield return null;
		}
		completionCallback?.Invoke();
		yield return null;
	}

	private IEnumerator PostTweet(string text, byte[] image, Action<long> returnTweetID, Action tweetFailed)
	{
		try
		{
			IResource photo = new ByteArrayResource(image);
			Tweet tweet = m_Authenticator.ServiceProvider.GetApi(m_Settings.m_AccessToken, m_Settings.m_AccessTokenSecret).TimelineOperations.UpdateStatus(text, photo);
			returnTweetID?.Invoke(tweet.ID);
		}
		catch (Exception ex)
		{
			d.Log("PostTweet failed: " + ex);
			tweetFailed?.Invoke();
		}
		yield return null;
	}

	private IEnumerator FlagTweetAsFavourite(long tweetID, Action<bool> completionCallback)
	{
		try
		{
			m_Authenticator.ServiceProvider.GetApi(m_Settings.m_AccessToken, m_Settings.m_AccessTokenSecret).TimelineOperations.AddToFavorites(tweetID);
			completionCallback?.Invoke(obj: true);
		}
		catch (Exception ex)
		{
			d.Log("FlagTweetAsFavourite failed: " + ex);
			completionCallback?.Invoke(obj: false);
		}
		yield return null;
	}

	private IEnumerator RetrieveTweet(long tweetID, Action<Tweet> fetchedTweet)
	{
		try
		{
			Tweet status = m_Authenticator.ServiceProvider.GetApi(m_Settings.m_AccessToken, m_Settings.m_AccessTokenSecret).TimelineOperations.GetStatus(tweetID);
			fetchedTweet?.Invoke(status);
		}
		catch (Exception ex)
		{
			d.Log("RetrieveTweet failed: " + ex);
			fetchedTweet(null);
		}
		yield return null;
	}

	private void Init()
	{
		ServicePointManager.ServerCertificateValidationCallback = (object p1, X509Certificate p2, X509Chain p3, SslPolicyErrors p4) => true;
		LoadUserInfo();
		InitializeThreadWorker();
		m_CachedTweets = new List<TweetWithMedia>();
	}

	private void InitializeThreadWorker()
	{
		m_ThreadWorker = new ThreadWorker();
		Thread thread = new Thread(m_ThreadWorker.DoWork);
		thread.Name = "TwitterAPI";
		thread.Start();
	}

	private void Awake()
	{
	}
}
