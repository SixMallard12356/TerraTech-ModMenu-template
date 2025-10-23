#define UNITY_EDITOR
using System;
using System.IO;
using UnityEngine;

public class SnapshotDisk : Snapshot
{
	public string snapName;

	public override void ResolveThumbnail(Action<Sprite> callback)
	{
		if (m_Thumbnail == null)
		{
			if ((bool)Singleton.Manager<ManSnapshots>.inst.ServiceDisk_Desktop)
			{
				FileInfo fileInfo = new FileInfo(snapName);
				Texture2D texture = Singleton.Manager<ManSnapshots>.inst.ServiceDisk_Desktop.LoadThumbnail(fileInfo);
				m_Thumbnail = CreateThumbnailFromTexture(texture);
				AssignThumbnailToImage(m_Thumbnail, ref image);
				callback(m_Thumbnail);
			}
			else
			{
				FileUtils.LoadSnapshotTextureAsync(snapName, delegate(Texture2D texture2)
				{
					m_Thumbnail = CreateThumbnailFromTexture(texture2);
					AssignThumbnailToImage(m_Thumbnail, ref image);
					callback(m_Thumbnail);
				});
			}
		}
		else
		{
			AssignThumbnailToImage(m_Thumbnail, ref image);
			callback(m_Thumbnail);
		}
	}

	public string GetFileName()
	{
		string fileNameWithoutExtension = snapName;
		if (!SKU.ConsoleUI)
		{
			fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileNameWithoutExtension);
		}
		return fileNameWithoutExtension;
	}

	private Sprite CreateThumbnailFromTexture(Texture2D texture)
	{
		Sprite sprite = null;
		if (texture != null)
		{
			try
			{
				sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			}
			catch (Exception ex)
			{
				d.Log(ex.Message);
			}
		}
		d.Assert(sprite != null, "Snapshot.ResolveThumbnail - could not resolve thumbnail. Image on disk might be corrupt. Path: " + snapName);
		return sprite;
	}

	private void AssignThumbnailToImage(Sprite thumbSprite, ref Texture2D targetImage)
	{
		if (thumbSprite != null)
		{
			targetImage = thumbSprite.texture;
		}
	}
}
