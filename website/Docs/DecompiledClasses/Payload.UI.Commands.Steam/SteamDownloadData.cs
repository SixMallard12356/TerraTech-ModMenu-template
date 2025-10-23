#define UNITY_EDITOR
using System.Collections.Generic;
using Steamworks;

namespace Payload.UI.Commands.Steam;

public struct SteamDownloadData
{
	public uint m_Page;

	public UGCQueryHandle_t m_QueryHadle;

	public SteamItemCategory m_Category;

	public uint m_NumItems;

	public uint m_TotalItems;

	public List<SteamDownloadItemData> m_Items;

	public const int kAllPages = 0;

	public bool HasAnyItems
	{
		get
		{
			if (m_Items != null)
			{
				return m_Items.Count > 0;
			}
			return false;
		}
	}

	public void ClearItems()
	{
		if (m_Items != null)
		{
			m_Items.Clear();
		}
	}

	public void AddItem(SteamDownloadItemData item)
	{
		if (m_Items == null)
		{
			m_Items = new List<SteamDownloadItemData>();
		}
		m_Items.Add(item);
	}

	public static SteamDownloadData Create(SteamItemCategory category, uint page = 0u)
	{
		SteamDownloadData result = new SteamDownloadData
		{
			m_Category = category
		};
		d.AssertFormat(page == 0 || page >= 1, "Pages are 1 indexed. Invalid value '{0}' specified.", page);
		result.m_Page = page;
		return result;
	}
}
