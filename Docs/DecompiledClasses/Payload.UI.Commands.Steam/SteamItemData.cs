using System.Collections.Generic;
using Steamworks;

namespace Payload.UI.Commands.Steam;

public struct SteamItemData
{
	public PublishedFileId_t m_PublishedFileID;

	public string m_Name;

	public string m_Description;

	public bool m_Favourite;

	public List<string> m_Tags;

	public ERemoteStoragePublishedFileVisibility m_Visibility;

	public bool HasAnyTags
	{
		get
		{
			if (m_Tags != null)
			{
				return m_Tags.Count > 0;
			}
			return false;
		}
	}

	public void AddTag(string steamTagBackingValue)
	{
		if (m_Tags == null)
		{
			m_Tags = new List<string>();
		}
		if (!m_Tags.Contains(steamTagBackingValue))
		{
			m_Tags.Add(steamTagBackingValue);
		}
	}

	public void RemoveTag(string steamTagBackingValue)
	{
		if (m_Tags != null)
		{
			m_Tags.Remove(steamTagBackingValue);
		}
	}
}
