#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeLevel : UIHUDElement
{
	[Serializable]
	private struct UpgradeMessageOverride
	{
		public FactionSubTypes corp;

		public int level;

		public LocalisedString message;
	}

	[SerializeField]
	private Image m_FactionIcon;

	[SerializeField]
	private Text m_Title;

	[SerializeField]
	private Text m_BodyText;

	[SerializeField]
	private Transform m_UnlockedBlocks;

	[SerializeField]
	private Image m_BlocksItemPrefab;

	[SerializeField]
	private Button m_DismissButton;

	[SerializeField]
	private Transform m_GamepadDismissPrompt;

	[SerializeField]
	private int m_MaxNumberDisplayed = 6;

	[SerializeField]
	private GameObject m_PlusMoreIcon;

	[SerializeField]
	private Text m_PlusMoreCount;

	[SerializeField]
	private UpgradeMessageOverride[] m_MessageOverrides;

	private CustomCorpUnlockData m_CustomCorpUnlocks;

	public override void Show(object context)
	{
		if (!base.IsVisible)
		{
			base.Show(context);
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.FullscreenUI);
		}
		if (context is FactionLicense)
		{
			FactionLicense factionLicense = context as FactionLicense;
			Setup(factionLicense.Corporation, factionLicense.CurrentLevel);
			m_CustomCorpUnlocks = CustomCorpUnlockData.CreateDataOrNull(factionLicense.Corporation);
		}
		else if (context is CustomCorpUnlockData)
		{
			Setup(context as CustomCorpUnlockData);
		}
		else
		{
			d.LogError("Incorrect context for UIUpgradeLevel");
		}
		bool flag = Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad();
		m_DismissButton.gameObject.SetActive(!flag);
		m_GamepadDismissPrompt.gameObject.SetActive(flag);
		Singleton.Manager<ManInput>.inst.RegisterInputEventDelegate(22, InputActionEventType.ButtonJustPressed, OnCancelPressed);
	}

	public override void Hide(object context)
	{
		Singleton.Manager<ManInput>.inst.UnregisterInputEventDelegate(22, InputActionEventType.ButtonJustPressed, OnCancelPressed);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.FullscreenUI);
		base.Hide(context);
		for (int num = m_UnlockedBlocks.childCount - 1; num >= 0; num--)
		{
			Transform child = m_UnlockedBlocks.GetChild(num);
			child.SetParent(null, worldPositionStays: false);
			child.Recycle();
		}
	}

	public void Close()
	{
		if (m_CustomCorpUnlocks != null)
		{
			Show(m_CustomCorpUnlocks);
			m_CustomCorpUnlocks = null;
		}
		else
		{
			HideSelf();
		}
	}

	private void Setup(CustomCorpUnlockData customData)
	{
		Setup(customData.m_VanillaCorp, 0);
		m_Title.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Licensing, 19);
		m_BodyText.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Licensing, 20);
		for (int num = m_UnlockedBlocks.childCount - 1; num >= 0; num--)
		{
			Transform child = m_UnlockedBlocks.GetChild(num);
			child.SetParent(null, worldPositionStays: false);
			child.Recycle();
		}
		for (int i = 0; i < Mathf.Min(customData.m_CustomCorpIDs.Count, m_MaxNumberDisplayed); i++)
		{
			Transform obj = m_BlocksItemPrefab.transform.Spawn();
			obj.SetParent(m_UnlockedBlocks, worldPositionStays: false);
			obj.GetComponent<Image>().sprite = Singleton.Manager<ManUI>.inst.m_SpriteFetcher.GetCorpIcon((FactionSubTypes)customData.m_CustomCorpIDs[i]);
		}
		if (m_PlusMoreIcon != null)
		{
			m_PlusMoreIcon.SetActive(customData.m_CustomCorpIDs.Count > m_MaxNumberDisplayed);
			if (m_PlusMoreCount != null)
			{
				m_PlusMoreCount.text = $"+{customData.m_CustomCorpIDs.Count - m_MaxNumberDisplayed}";
			}
		}
	}

	private void Setup(FactionSubTypes corporation, int level)
	{
		m_FactionIcon.sprite = Singleton.Manager<ManUI>.inst.GetCorpIcon(corporation);
		string corporationName = StringLookup.GetCorporationName(corporation);
		string arg = (level + 1).ToString();
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Licensing, 11);
		m_Title.text = string.Format(localisedString, corporationName, arg);
		string text = null;
		if (m_MessageOverrides != null)
		{
			UpgradeMessageOverride[] messageOverrides = m_MessageOverrides;
			for (int i = 0; i < messageOverrides.Length; i++)
			{
				UpgradeMessageOverride upgradeMessageOverride = messageOverrides[i];
				if (upgradeMessageOverride.corp == corporation && (upgradeMessageOverride.level < 0 || upgradeMessageOverride.level == level))
				{
					text = upgradeMessageOverride.message.Value;
				}
			}
		}
		if (text == null)
		{
			text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Licensing, 12);
		}
		m_BodyText.text = string.Format(text, corporationName, arg);
		List<BlockTypes> levelUpScreenBlocksInTier = Singleton.Manager<ManLicenses>.inst.GetLevelUpScreenBlocksInTier(corporation, level);
		for (int j = 0; j < Mathf.Min(levelUpScreenBlocksInTier.Count, m_MaxNumberDisplayed); j++)
		{
			Transform obj = m_BlocksItemPrefab.transform.Spawn();
			obj.SetParent(m_UnlockedBlocks, worldPositionStays: false);
			obj.GetComponent<Image>().sprite = Singleton.Manager<ManUI>.inst.GetSprite(ObjectTypes.Block, (int)levelUpScreenBlocksInTier[j]);
		}
		if (m_PlusMoreIcon != null)
		{
			m_PlusMoreIcon.SetActive(levelUpScreenBlocksInTier.Count > m_MaxNumberDisplayed);
			if (m_PlusMoreCount != null)
			{
				m_PlusMoreCount.text = $"+{levelUpScreenBlocksInTier.Count - m_MaxNumberDisplayed}";
			}
		}
	}

	private void OnCancelPressed(InputActionEventData eventData)
	{
		if (eventData.IsCurrentInputSource(ControllerType.Joystick) && Singleton.Manager<ManUI>.inst.IsStackEmpty())
		{
			m_DismissButton.onClick.Invoke();
		}
	}

	private void OnSpawn()
	{
		AddElementToGroup(ManHUD.HUDGroup.Main);
		AddElementToGroup(ManHUD.HUDGroup.GamepadCursorHidingElements);
	}
}
