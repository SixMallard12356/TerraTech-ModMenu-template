#define UNITY_EDITOR
using Spring.Social.Twitter.Api;
using UnityEngine;

public class SnapshotTwitter : Snapshot
{
	public string profileImageUrl = "";

	public long tweetID;

	public static SnapshotTwitter ConvertFromDisk(SnapshotDisk snapshotDisk)
	{
		string text = "";
		string text2 = "";
		TwitterProfile twitterProfile = Singleton.Manager<TwitterAPI>.inst.GetTwitterProfile();
		if (twitterProfile != null)
		{
			text = twitterProfile.ProfileImageUrl;
			text2 = twitterProfile.ScreenName;
		}
		else
		{
			d.LogError("SnapshotTwitter.ConvertFromDisk - could not retrieve player twitter profile. Are they authenticated?");
		}
		SnapshotTwitter snapshotTwitter = new SnapshotTwitter
		{
			image = snapshotDisk.image,
			techData = snapshotDisk.techData,
			creator = text2,
			profileImageUrl = text,
			UniqueID = snapshotDisk.UniqueID,
			DateCreated = snapshotDisk.DateCreated
		};
		if (snapshotTwitter.image == null)
		{
			Texture2D texture2D = FileUtils.LoadTexture(snapshotDisk.snapName);
			d.Assert(texture2D != null, "SnapshotTwitter - source snapshotDisk has and invalid image on disk. May have become corrupted. Path: " + snapshotDisk.snapName);
			snapshotTwitter.image = texture2D;
		}
		return snapshotTwitter;
	}
}
