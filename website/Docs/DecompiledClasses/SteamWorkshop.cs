#define UNITY_EDITOR
using System;
using Payload.UI.Commands.Steam;
using UnityEngine;

[Serializable]
public class SteamWorkshop
{
	[SerializeField]
	private bool m_Enabled;

	[EnumArray(typeof(SteamItemCategory))]
	[SerializeField]
	private string[] m_SteamWorkshopCategoryTags;

	[EnumArray(typeof(SteamItemTechTags))]
	[SerializeField]
	private string[] m_SteamWorkshopTechTags;

	[SerializeField]
	[EnumArray(typeof(SteamItemSavesTags))]
	private string[] m_SteamWorkshopSaveTags;

	[EnumArray(typeof(SteamItemSavesAutoTags))]
	[SerializeField]
	private string[] m_SteamWorkshopAutoSaveTags;

	[EnumArray(typeof(SteamItemModsTags))]
	[SerializeField]
	private string[] m_SteamWorkshopModsTags;

	public bool Enabled
	{
		get
		{
			if (m_Enabled && SKU.IsSteam)
			{
				return Singleton.Manager<ManSteamworks>.inst.Inited;
			}
			return false;
		}
	}

	public string GetTagBackingValueCategory(SteamItemCategory category)
	{
		string text = m_SteamWorkshopCategoryTags[(int)category];
		d.AssertFormat(!string.IsNullOrEmpty(text), "No backing value set for {0} in inspector", category);
		return text;
	}

	public string GetTagBackingValueTechs(SteamItemTechTags steamTag)
	{
		string text = m_SteamWorkshopTechTags[(int)steamTag];
		d.AssertFormat(!string.IsNullOrEmpty(text), "No backing value set for {0} in inspector", steamTag);
		return text;
	}

	public string GetTagBackingValueSaves(SteamItemSavesTags saveTag)
	{
		string text = m_SteamWorkshopSaveTags[(int)saveTag];
		d.AssertFormat(!string.IsNullOrEmpty(text), "No backing value set for {0} in inspector", saveTag);
		return text;
	}

	public string GetTagBackingValueAutoSaves(SteamItemSavesAutoTags saveTag)
	{
		string text = m_SteamWorkshopAutoSaveTags[(int)saveTag];
		d.AssertFormat(!string.IsNullOrEmpty(text), "No backing value set for {0} in inspector", saveTag);
		return text;
	}
}
