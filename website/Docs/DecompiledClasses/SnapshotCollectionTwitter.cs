#define UNITY_EDITOR
using System;
using System.Collections;
using UnityEngine;

public class SnapshotCollectionTwitter : SnapshotCollection<SnapshotTwitter>
{
	private class WaitForSnapshotConversion
	{
		private bool m_ConversionInProgress;

		private bool m_WasBlockOnConversion;

		private float m_TimeRemaining;

		public bool ConversionSuccess { get; private set; }

		public string SnapshotPath { get; private set; }

		public WaitForSnapshotConversion(Action snapshotLoadFunc, float maxWaitTimeMs = 5000f)
		{
			m_TimeRemaining = maxWaitTimeMs;
			ManScreenshot.SnapshotConversionStartedEvent.Subscribe(OnSnapshotConversionStarted);
			ManScreenshot.SnapshotConversionCompletedEvent.Subscribe(OnSnapshotConversionCompleted);
			m_WasBlockOnConversion = ManScreenshot.s_BlockExecutionDuringConversion;
			ManScreenshot.s_BlockExecutionDuringConversion = false;
			snapshotLoadFunc();
			ManScreenshot.s_BlockExecutionDuringConversion = m_WasBlockOnConversion;
		}

		public IEnumerator WaitForConversion()
		{
			bool wait;
			do
			{
				wait = m_ConversionInProgress;
				if (m_ConversionInProgress && m_TimeRemaining > 0f)
				{
					m_TimeRemaining -= Time.deltaTime;
					if (m_TimeRemaining < 0f)
					{
						wait = false;
						d.LogError("WaitForSnapshotConversion - Timeout expired! Snapshot was not converted, and ManScreenshot.SnapshotConversionCompletedEvent will not be fired!");
					}
				}
				if (wait)
				{
					yield return null;
				}
			}
			while (wait);
			ManScreenshot.SnapshotConversionStartedEvent.Unsubscribe(OnSnapshotConversionStarted);
			ManScreenshot.SnapshotConversionCompletedEvent.Unsubscribe(OnSnapshotConversionCompleted);
		}

		private void OnSnapshotConversionStarted(string path)
		{
			m_ConversionInProgress = true;
		}

		private void OnSnapshotConversionCompleted(string path, bool success)
		{
			m_ConversionInProgress = false;
			SnapshotPath = path;
			ConversionSuccess = success;
		}
	}

	public SnapshotCollectionTwitter(string modeRestriction, string subModeRestriction, string userDataRestriction, TankPreset.UserData userProfileRestriction = null)
		: base(modeRestriction, subModeRestriction, userDataRestriction, userProfileRestriction)
	{
	}

	public IEnumerator TryAddFromImage(TwitterAPI.TweetWithMedia tweetData)
	{
		Texture2D image = new Texture2D(4, 4);
		image.LoadImage(tweetData.ImageArray);
		bool addSuccess = false;
		WaitForSnapshotConversion snapshotConversionWaitObject = new WaitForSnapshotConversion(delegate
		{
			addSuccess = TryAddFromImage(image, tweetData, out var _);
		});
		yield return snapshotConversionWaitObject.WaitForConversion();
		if (!addSuccess && snapshotConversionWaitObject.ConversionSuccess)
		{
			image = FileUtils.LoadTexture(snapshotConversionWaitObject.SnapshotPath);
			TryAddFromImage(image, tweetData, out var _);
		}
		yield return null;
	}

	public bool TryAddFromImage(Texture2D snapshotRender, TwitterAPI.TweetWithMedia tweetData, out SnapshotTwitter snapshot, bool convertLegacy = true)
	{
		snapshot = DecodeSnapshotFromImage(snapshotRender, tweetData, convertLegacy);
		if (snapshot != null)
		{
			AddSnapshot(snapshot);
			return true;
		}
		return false;
	}

	private SnapshotTwitter DecodeSnapshotFromImage(Texture2D snapshotRender, TwitterAPI.TweetWithMedia tweetData, bool convertLegacy = true)
	{
		SnapshotTwitter snapshotTwitter = DecodeSnapshotFromImage(snapshotRender, convertLegacy);
		if (snapshotTwitter != null)
		{
			snapshotTwitter.creator = tweetData.ScreenName;
			snapshotTwitter.profileImageUrl = tweetData.ProfileImageUrl;
			snapshotTwitter.tweetID = tweetData.TweetID;
			snapshotTwitter.m_Name.Value = snapshotTwitter.techData.Name;
			snapshotTwitter.UniqueID = tweetData.TweetID.ToString();
			snapshotTwitter.DateCreated = tweetData.DateCreated;
		}
		else
		{
			d.LogWarning("SnapshotCollectionTwitter.GetSnapshotFromImage tweet data not contain valid tech info. TweetID: " + tweetData.TweetID);
		}
		return snapshotTwitter;
	}
}
