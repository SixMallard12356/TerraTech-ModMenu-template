#define UNITY_EDITOR
using System;
using Binding;
using Newtonsoft.Json;
using UnityEngine;

public class Snapshot
{
	public struct MetaData
	{
		public string Snapshot_UID;

		public bool IsFavourite;

		public string FolderName;

		[JsonIgnore]
		public static readonly MetaData Empty = new MetaData
		{
			Snapshot_UID = string.Empty,
			IsFavourite = false,
			FolderName = string.Empty
		};

		[JsonIgnore]
		public static readonly MetaData Invalid = default(MetaData);

		[JsonIgnore]
		public ulong Snapshot_UID_ULONG
		{
			get
			{
				if (!ulong.TryParse(Snapshot_UID, out var result))
				{
					return 0uL;
				}
				return result;
			}
			set
			{
				Snapshot_UID = value.ToString();
			}
		}

		public MetaData(string snapshotUID)
		{
			Snapshot_UID = snapshotUID;
			IsFavourite = false;
			FolderName = string.Empty;
		}

		public MetaData(ulong snapshotUID)
		{
			Snapshot_UID = snapshotUID.ToString();
			IsFavourite = false;
			FolderName = string.Empty;
		}

		public MetaData(MetaData copy)
		{
			Snapshot_UID = copy.Snapshot_UID;
			IsFavourite = copy.IsFavourite;
			FolderName = copy.FolderName;
		}

		public bool IsGeneric()
		{
			MetaData metaData = new MetaData(this);
			metaData.Snapshot_UID = string.Empty;
			return metaData == Empty;
		}

		public static bool operator ==(MetaData metaData, MetaData otherMetaData)
		{
			return metaData.Equals(otherMetaData);
		}

		public static bool operator !=(MetaData metaData, MetaData otherMetaData)
		{
			return !metaData.Equals(otherMetaData);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is MetaData metaData))
			{
				return false;
			}
			if (Snapshot_UID == metaData.Snapshot_UID && IsFavourite == metaData.IsFavourite)
			{
				return FolderName == metaData.FolderName;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCodeUtility.CombineHashCodes((!Snapshot_UID.NullOrEmpty()) ? Snapshot_UID.GetHashCode() : 0, IsFavourite ? 1 : 0, (!FolderName.NullOrEmpty()) ? FolderName.GetHashCode() : 0);
		}
	}

	public Texture2D image;

	public TechData techData;

	public string creator = "";

	[Obsolete("The old way of setting favourites. Use Snapshot.m_Meta.IsFavourite Instead!", false)]
	public Bindable<bool> m_IsFavourite = new Bindable<bool>(value: false);

	public Bindable<string> m_Name = new Bindable<string>(string.Empty);

	public Bindable<MetaData> m_Meta = new Bindable<MetaData>(MetaData.Empty);

	protected Sprite m_Thumbnail;

	public string UniqueID { get; set; }

	public DateTime DateCreated { get; set; }

	public virtual void ResolveThumbnail(Action<Sprite> callback)
	{
		if (m_Thumbnail == null && image != null)
		{
			m_Thumbnail = Sprite.Create(image, new Rect(0f, 0f, image.width, image.height), new Vector2(0.5f, 0.5f));
		}
		d.Assert(m_Thumbnail != null, "Snapshot.ResolveThumbnail - could not resolve thumbnail. Image: " + image);
		callback(m_Thumbnail);
	}

	public void DestroyTexture()
	{
		if (image != null)
		{
			UnityEngine.Object.Destroy(image);
			image = null;
		}
	}
}
