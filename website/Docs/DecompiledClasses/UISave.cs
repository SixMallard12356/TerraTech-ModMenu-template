#define UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISave : MonoBehaviour
{
	private struct CorpIconData
	{
		public GameObject gameObject;

		public Text levelLabel;
	}

	[SerializeField]
	private Toggle m_SelectableComponent;

	[SerializeField]
	private GameObject m_Content;

	[SerializeField]
	private Text m_SaveName;

	[SerializeField]
	private Text m_TimePlayed;

	[SerializeField]
	private LocalisedString m_TimePlayedFormat;

	[SerializeField]
	private Text m_LastPlayed;

	[SerializeField]
	private LocalisedString m_LastPlayedFormat;

	[SerializeField]
	private Text m_Seed;

	[SerializeField]
	private LocalisedString m_SeedFormat;

	[SerializeField]
	private Text m_SaveVersionNr;

	[SerializeField]
	private LocalisedString m_SaveVersionFormat;

	[SerializeField]
	private RectTransform m_CorpIconPrefab;

	[SerializeField]
	private RectTransform m_CorpIconContainer;

	[SerializeField]
	private RawImage m_Snapshot;

	[SerializeField]
	private Texture2D m_MissingImage;

	[SerializeField]
	private RectTransform m_MoneyPanel;

	[SerializeField]
	private Text m_Money;

	[SerializeField]
	private Text m_VisibilityText;

	[SerializeField]
	private Sprite m_BackgroundDefault;

	[SerializeField]
	private Sprite m_BackgroundAutosave;

	[SerializeField]
	private TooltipComponent m_TooltipComponent;

	[SerializeField]
	private LocalisedString m_UnableToLoadNoLongerSupportedText;

	[SerializeField]
	private LocalisedString m_UnableToLoadNewerVersionFormatText;

	[SerializeField]
	private Image m_SteamIcon;

	private string m_SaveFileName;

	private ManSaveGame.SaveInfo m_SaveInfo;

	private ManGameMode.GameType m_SaveGameType;

	private Texture m_origSnapshotTexture;

	private RectTransform m_RectTransform;

	private List<RectTransform> m_CorpImages;

	public ManSaveGame.SaveInfo SaveInfo => m_SaveInfo;

	public Toggle ToggleComponent => m_SelectableComponent;

	public void SetActiveIfInViewportRect(Rect viewportRect)
	{
		if (!(m_Content == null))
		{
			m_Content.SetActive(m_RectTransform.WorldRect().Overlaps(viewportRect));
		}
	}

	public void Setup(string saveName, ManSaveGame.SaveInfo saveInfo, bool useAsSlot)
	{
		if (m_SaveName != null && saveName != null && saveInfo != null)
		{
			string text = saveName.Substring(0, (saveName.Length > 25) ? 25 : saveName.Length);
			int result;
			if (saveInfo.IsAutoSave && !string.IsNullOrEmpty(saveInfo.m_SaveNameAncestor))
			{
				text = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, 72), " (" + saveInfo.m_SaveNameAncestor + ")");
			}
			else if (ManSaveGame.UseFixedSlots && saveName.StartsWith("Slot ") && int.TryParse(saveName.Substring("Slot ".Length), out result))
			{
				text = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.Save.saveSlotName), result);
			}
			m_SaveName.text = text;
		}
		m_SaveFileName = saveName;
		if (m_SaveInfo != null)
		{
			m_SaveInfo.DestroyTexture();
		}
		m_SaveInfo = saveInfo;
		if (m_SaveInfo != null)
		{
			m_SaveGameType = saveInfo.m_GameType;
			SetPropertyText(m_TimePlayed, m_TimePlayedFormat, $"{(int)saveInfo.m_TimePlayed.TotalHours:00}:{saveInfo.m_TimePlayed.Minutes:00}:{saveInfo.m_TimePlayed.Seconds:00}");
			SetPropertyText(m_LastPlayed, m_LastPlayedFormat, Singleton.Manager<Localisation>.inst.GetDateString(saveInfo.m_LastPlayed));
			DestroySnapshotTexture();
			m_Snapshot.texture = (saveInfo.LastScreenshot ? saveInfo.LastScreenshot : m_MissingImage);
			m_Snapshot.enabled = true;
			bool flag = m_SaveGameType != ManGameMode.GameType.RaD;
			if (m_Seed != null)
			{
				m_Seed.gameObject.SetActive(flag);
				if (flag)
				{
					SetPropertyText(m_Seed, m_SeedFormat, saveInfo.m_WorldSeed);
				}
			}
			m_SelectableComponent.image.sprite = (saveInfo.IsAutoSave ? m_BackgroundAutosave : m_BackgroundDefault);
			bool flag2 = saveInfo.CanLoadSave();
			m_SelectableComponent.interactable = flag2;
			if (m_TooltipComponent != null)
			{
				m_TooltipComponent.enabled = !flag2;
				if (!flag2)
				{
					if (saveInfo.WasSavedInNewerVersion())
					{
						string text2 = string.Format(m_UnableToLoadNewerVersionFormatText.Value, saveInfo.m_DisplayVersionNr);
						m_TooltipComponent.SetText(text2);
					}
					else
					{
						m_TooltipComponent.SetText(m_UnableToLoadNoLongerSupportedText.Value);
					}
				}
			}
			if (m_SaveVersionNr != null)
			{
				string text3 = "";
				SetPropertyText(m_SaveVersionNr, m_SaveVersionFormat, text3 + saveInfo.m_DisplayVersionNr);
				bool active = !SKU.ConsoleUI;
				m_SaveVersionNr.gameObject.SetActive(active);
			}
			bool flag3 = m_SaveGameType != ManGameMode.GameType.RaD;
			if (m_MoneyPanel != null)
			{
				m_MoneyPanel.gameObject.SetActive(flag3);
			}
			if (m_Money != null)
			{
				m_Money.text = (flag3 ? Singleton.Manager<Localisation>.inst.GetMoneyString(saveInfo.m_Money) : string.Empty);
			}
			if (m_CorpIconContainer != null && m_CorpIconPrefab != null)
			{
				bool flag4 = m_SaveGameType.IsCampaign();
				m_CorpIconContainer.gameObject.SetActive(flag4);
				if (flag4)
				{
					int count = Singleton.Manager<ManPurchases>.inst.AvailableCorporations.Count;
					for (int i = 0; i < count; i++)
					{
						FactionSubTypes factionSubTypes = Singleton.Manager<ManPurchases>.inst.AvailableCorporations[i];
						int num = (int)factionSubTypes;
						if (num >= saveInfo.m_CorpLicenceLevels.Length)
						{
							continue;
						}
						int num2 = saveInfo.m_CorpLicenceLevels[num];
						if (num2 >= 0)
						{
							if (m_CorpImages == null)
							{
								m_CorpImages = new List<RectTransform>();
							}
							RectTransform rectTransform = m_CorpIconPrefab.Spawn();
							rectTransform.SetParent(m_CorpIconContainer, worldPositionStays: false);
							Image componentInChildren = rectTransform.GetComponentInChildren<Image>();
							Text componentInChildren2 = rectTransform.GetComponentInChildren<Text>();
							componentInChildren.sprite = Singleton.Manager<ManUI>.inst.GetSelectedCorpIcon(factionSubTypes);
							componentInChildren2.text = (num2 + 1).ToString();
							m_CorpImages.Add(rectTransform);
						}
					}
				}
			}
			bool flag5 = m_SaveGameType.IsCoOp();
			if (m_VisibilityText != null)
			{
				m_VisibilityText.gameObject.SetActive(flag5);
				if (flag5)
				{
					m_VisibilityText.text = Singleton.Manager<Localisation>.inst.GetLobbyVisibilityString(saveInfo.m_LobbyVisibility);
				}
			}
			if ((bool)m_SteamIcon)
			{
				m_SteamIcon.enabled = m_SaveInfo.IsWorkshopSave;
			}
		}
		else
		{
			if (useAsSlot)
			{
				m_SaveName.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.Save.saveEmptySlot);
				m_Snapshot.enabled = false;
			}
			SetPropertyText(m_TimePlayed, m_TimePlayedFormat, "--:--:--");
			SetPropertyText(m_LastPlayed, m_LastPlayedFormat, "--/--/--");
			m_SelectableComponent.interactable = true;
			m_SelectableComponent.image.sprite = m_BackgroundDefault;
			if (m_Seed != null)
			{
				m_Seed.gameObject.SetActive(value: false);
			}
			if (m_MoneyPanel != null)
			{
				m_MoneyPanel.gameObject.SetActive(value: false);
			}
			if (m_CorpIconContainer != null)
			{
				m_CorpIconContainer.gameObject.SetActive(value: false);
			}
			if (m_SaveVersionNr != null)
			{
				m_SaveVersionNr.gameObject.SetActive(value: false);
			}
			if (m_TooltipComponent != null)
			{
				m_TooltipComponent.enabled = false;
			}
			if (m_VisibilityText != null)
			{
				m_VisibilityText.gameObject.SetActive(value: false);
			}
			if (m_SteamIcon != null)
			{
				m_SteamIcon.enabled = false;
			}
		}
	}

	public void SetupSaveGameToLoad()
	{
		bool flag = m_SaveInfo != null && m_SaveInfo.IsWorkshopSave;
		Singleton.Manager<ManGameMode>.inst.SetupSaveGameToLoad(m_SaveGameType, m_SaveFileName, m_SaveInfo.WorldGenVersion, flag ? m_SaveInfo.FullFilePath : null);
	}

	public string GetSaveFileName()
	{
		return m_SaveFileName;
	}

	public void AskDelete()
	{
		(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 94), Delete, delegate
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
		}, Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29), Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30));
		Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
	}

	public void SetSelected()
	{
		m_SelectableComponent.isOn = true;
		UIAutoScrollItem component = GetComponent<UIAutoScrollItem>();
		if ((bool)component)
		{
			component.OnSelect(null);
		}
	}

	public void SetSelectable(bool selectable)
	{
		m_SelectableComponent.interactable = selectable;
		m_SelectableComponent.SetValue(value: false);
	}

	public ManGameMode.GameType GetSaveGameType()
	{
		return m_SaveGameType;
	}

	private void Delete()
	{
		string path = ManSaveGame.CreateGameSaveFilePath(m_SaveGameType, m_SaveFileName);
		if (File.Exists(path))
		{
			File.Delete(path);
		}
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	private void SetPropertyText(Text textObject, LocalisedString localisedStringWithFormat, string propertyValueString)
	{
		if (localisedStringWithFormat != null && !localisedStringWithFormat.Value.NullOrEmpty())
		{
			textObject.text = string.Format(localisedStringWithFormat.Value, propertyValueString);
		}
		else
		{
			textObject.text = propertyValueString;
		}
	}

	private IUIHandlers GetUIHandlers()
	{
		IUIHandlers iUIHandlers = null;
		Transform parent = base.gameObject.transform.parent;
		parent = (parent ? parent.gameObject.transform.parent : null);
		while (iUIHandlers == null && (bool)parent)
		{
			iUIHandlers = parent.GetComponent<IUIHandlers>();
			parent = parent.transform.parent;
		}
		d.AssertFormat(iUIHandlers != null, "Item {0} must have a IUIHandlers above it in the heirachy", base.name);
		return iUIHandlers;
	}

	public void OnSubmit(BaseEventData eventData)
	{
		GetUIHandlers()?.OnSubmit(eventData);
	}

	public void OnCancel(BaseEventData eventData)
	{
		GetUIHandlers().OnCancel(eventData);
	}

	public void OnUIExtraButton1(BaseEventData eventData)
	{
		GetUIHandlers().OnUIExtraButton1(eventData);
	}

	public void OnUIExtraButton2(BaseEventData eventData)
	{
		GetUIHandlers().OnUIExtraButton2(eventData);
	}

	private void DestroySnapshotTexture()
	{
		if (m_Snapshot.texture != null && m_Snapshot.texture != m_MissingImage && m_Snapshot.texture != m_origSnapshotTexture)
		{
			Object.Destroy(m_Snapshot.texture);
			m_Snapshot.texture = null;
		}
		d.Assert(m_Snapshot.texture == null || m_Snapshot.texture == m_MissingImage || m_Snapshot.texture == m_origSnapshotTexture, "ASSERT - Unexpected snapshot texture!");
	}

	private void OnPool()
	{
		m_origSnapshotTexture = m_Snapshot.texture;
		m_RectTransform = base.transform as RectTransform;
		if (m_Content != null)
		{
			m_Content.SetActive(value: false);
		}
	}

	private void OnRecycle()
	{
		if (m_CorpImages != null)
		{
			for (int i = 0; i < m_CorpImages.Count; i++)
			{
				m_CorpImages[i].Recycle();
			}
			m_CorpImages.Clear();
		}
		if (m_SaveInfo != null)
		{
			m_SaveInfo.DestroyTexture();
		}
		DestroySnapshotTexture();
		m_Snapshot.texture = m_origSnapshotTexture;
	}
}
