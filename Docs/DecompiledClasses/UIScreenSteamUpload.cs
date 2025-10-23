#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Payload.UI.Commands.Steam;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenSteamUpload : UIScreen
{
	[SerializeField]
	private InputField m_NameInput;

	[SerializeField]
	private InputField m_DescriptionInput;

	[EnumArray(typeof(ERemoteStoragePublishedFileVisibility))]
	[SerializeField]
	private UISteamVisibilityToggle[] m_VisibilityToggles;

	[EnumArray(typeof(SteamItemTechTags))]
	[SerializeField]
	private UISteamTechTagToggle[] m_TechTagToggles;

	[EnumArray(typeof(SteamItemSavesAutoTags))]
	[SerializeField]
	private UISteamSaveTagToggle[] m_SaveTagToggles;

	[SerializeField]
	private Transform m_TechTagsParent;

	[SerializeField]
	private Transform m_SaveTagsParent;

	[SerializeField]
	private Image m_PreviewImage;

	private Action<SteamItemData> m_CompleteCallback;

	private Action m_CancelCallback;

	private SteamItemCategory m_ItemCategory;

	private SteamItemData m_ItemData;

	private Texture2D m_Preview;

	private HashSet<string> m_TagsCache = new HashSet<string>();

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		BlockScreenExit(exitBlocked: true);
		m_NameInput.interactable = m_ItemCategory != SteamItemCategory.Saves;
		m_NameInput.text = m_ItemData.m_Name;
		m_DescriptionInput.text = m_ItemData.m_Description;
		SetVisibility(m_ItemData.m_Visibility);
		m_TagsCache.Clear();
		if (m_ItemData.HasAnyTags)
		{
			for (int i = 0; i < m_ItemData.m_Tags.Count; i++)
			{
				string item = m_ItemData.m_Tags[i];
				m_TagsCache.Add(item);
			}
		}
		for (int j = 0; j < m_TechTagToggles.Length; j++)
		{
			SteamItemTechTags steamTag = m_TechTagToggles[j].SteamTag;
			string tagBackingValueTechs = Singleton.Manager<ManSteamworks>.inst.Workshop.GetTagBackingValueTechs(steamTag);
			bool selected = m_TagsCache.Contains(tagBackingValueTechs);
			m_TechTagToggles[j].SetSelected(selected);
		}
		for (int k = 0; k < m_SaveTagToggles.Length; k++)
		{
			SteamItemSavesAutoTags steamTag2 = m_SaveTagToggles[k].SteamTag;
			string tagBackingValueAutoSaves = Singleton.Manager<ManSteamworks>.inst.Workshop.GetTagBackingValueAutoSaves(steamTag2);
			bool selected2 = m_TagsCache.Contains(tagBackingValueAutoSaves);
			m_SaveTagToggles[k].SetSelected(selected2);
		}
		m_TechTagsParent.gameObject.SetActive(m_ItemCategory == SteamItemCategory.Techs);
		m_SaveTagsParent.gameObject.SetActive(m_ItemCategory == SteamItemCategory.Saves);
		if (m_Preview != null)
		{
			m_PreviewImage.sprite = Sprite.Create(m_Preview, new Rect(0f, 0f, m_Preview.width, m_Preview.height), new Vector2(0.5f, 0.5f));
			m_PreviewImage.preserveAspect = true;
		}
	}

	public override void Hide()
	{
		base.Hide();
		BlockScreenExit(exitBlocked: false);
	}

	public void Set(SteamItemCategory category, SteamItemData itemData, Action<SteamItemData> completeCallback, Action cancelCallback, Texture2D preview)
	{
		m_CompleteCallback = completeCallback;
		m_CancelCallback = cancelCallback;
		m_ItemCategory = category;
		m_ItemData = itemData;
		m_Preview = preview;
	}

	public void Upload()
	{
		if (m_CompleteCallback != null)
		{
			m_CompleteCallback(m_ItemData);
		}
	}

	public void UpdataName()
	{
		if (!string.IsNullOrEmpty(m_NameInput.text))
		{
			m_ItemData.m_Name = m_NameInput.text;
		}
	}

	public void Cancel()
	{
		if (m_CancelCallback != null)
		{
			m_CancelCallback();
		}
	}

	public void UpdateDescription()
	{
		m_ItemData.m_Description = m_DescriptionInput.text;
	}

	private void SetVisibility(ERemoteStoragePublishedFileVisibility visibility)
	{
		m_ItemData.m_Visibility = visibility;
		for (int i = 0; i < m_VisibilityToggles.Length; i++)
		{
			UISteamVisibilityToggle uISteamVisibilityToggle = m_VisibilityToggles[i];
			if (uISteamVisibilityToggle != null)
			{
				bool selected = uISteamVisibilityToggle.VisibilityOption == visibility;
				uISteamVisibilityToggle.SetSelected(selected);
			}
		}
	}

	public void OnVisibilityToggleSelected(ERemoteStoragePublishedFileVisibility visibility)
	{
		SetVisibility(visibility);
	}

	public void OnTechTagSelected(SteamItemTechTags tag, bool isSelected)
	{
		string tagBackingValueTechs = Singleton.Manager<ManSteamworks>.inst.Workshop.GetTagBackingValueTechs(tag);
		if (isSelected)
		{
			m_ItemData.AddTag(tagBackingValueTechs);
		}
		else
		{
			m_ItemData.RemoveTag(tagBackingValueTechs);
		}
	}

	public void OnSaveTagSelected(SteamItemSavesAutoTags tag, bool isSelected)
	{
	}

	private void OnPool()
	{
		Array values = Enum.GetValues(typeof(ERemoteStoragePublishedFileVisibility));
		for (int i = 0; i < m_VisibilityToggles.Length; i++)
		{
			UISteamVisibilityToggle uISteamVisibilityToggle = m_VisibilityToggles[i];
			ERemoteStoragePublishedFileVisibility eRemoteStoragePublishedFileVisibility = (ERemoteStoragePublishedFileVisibility)values.GetValue(i);
			if (uISteamVisibilityToggle != null)
			{
				uISteamVisibilityToggle.VisibilityOption = eRemoteStoragePublishedFileVisibility;
				uISteamVisibilityToggle.OnSelected.Subscribe(OnVisibilityToggleSelected);
			}
			else
			{
				d.AssertFormat(eRemoteStoragePublishedFileVisibility == ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityUnlisted, "Unexpected missing toggle in Steam Visibility options for enum '{0}'. Only Unlisted is expected to be null!", eRemoteStoragePublishedFileVisibility);
			}
		}
		Array values2 = Enum.GetValues(typeof(SteamItemTechTags));
		Enum.GetValues(typeof(SteamItemSavesAutoTags));
		for (int j = 0; j < m_TechTagToggles.Length; j++)
		{
			UISteamTechTagToggle obj = m_TechTagToggles[j];
			obj.SteamTag = (SteamItemTechTags)values2.GetValue(j);
			obj.OnSelected.Subscribe(OnTechTagSelected);
		}
		for (int k = 0; k < m_SaveTagToggles.Length; k++)
		{
			UISteamSaveTagToggle obj2 = m_SaveTagToggles[k];
			obj2.SteamTag = (SteamItemSavesAutoTags)values2.GetValue(k);
			obj2.OnSelected.Subscribe(OnSaveTagSelected);
		}
	}
}
