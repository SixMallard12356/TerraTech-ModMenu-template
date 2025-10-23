using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIBlockInfoDisplay : MonoBehaviour
{
	public enum CorpIconStyle
	{
		Regular,
		Selected
	}

	[Header("Generic")]
	[SerializeField]
	private TextMeshProUGUI m_NameText;

	[SerializeField]
	private TextMeshProUGUI m_DescriptionText;

	[SerializeField]
	private UITimedAutoScrollView m_DescriptionScroller;

	[SerializeField]
	private bool m_AutoScrollEnabled;

	[SerializeField]
	private Color m_BlockDescriptionWarningColour;

	[SerializeField]
	private bool m_DoBlockAllowedChecksDefault = true;

	[SerializeField]
	private TextMeshProUGUI m_BlockCategoryText;

	[SerializeField]
	private Image m_BlockCategoryIcon;

	[SerializeField]
	private TooltipComponent m_BlockCategoryTooltip;

	[SerializeField]
	private Image m_CorpIcon;

	[SerializeField]
	private CorpIconStyle m_CorpIconStyle = CorpIconStyle.Selected;

	[SerializeField]
	[FormerlySerializedAs("m_GradeText")]
	private TextMeshProUGUI m_GradeValueText;

	[SerializeField]
	private TextMeshProUGUI m_GradeStringText;

	[SerializeField]
	private TooltipComponent m_CorpAndGradeTooltip;

	[SerializeField]
	private TextMeshProUGUI m_RarityText;

	[SerializeField]
	private Image m_RarityIcon;

	[SerializeField]
	private TooltipComponent m_RarityTooltip;

	[SerializeField]
	private GameObject m_LimiterCostElement;

	[SerializeField]
	private TextMeshProUGUI m_LimiterCostText;

	[SerializeField]
	private UIDamageTypeDisplay m_DamageTypeDisplay;

	[SerializeField]
	private UIDamageTypeDisplay m_DamageableTypeDisplay;

	[Header("Attribute things")]
	[SerializeField]
	private InfoPanelItemAttribute m_ItemBlockAttrPrefab;

	[SerializeField]
	private RectTransform m_BlockAttrContainer;

	[SerializeField]
	private bool m_DisableBlockAttrContainerOnEmpty;

	[Header("Shop")]
	[SerializeField]
	private Button m_PurchaseBlockButton;

	[SerializeField]
	private Image m_PurchaseButtonImage;

	[SerializeField]
	private Color m_PurchaseOkColour;

	[SerializeField]
	private Color m_PurchaseInvalidColour;

	[SerializeField]
	private TextMeshProUGUI m_PurchaseBlockPriceText;

	private Color m_BlockDescriptionDefaultColour = Color.magenta;

	private List<InfoPanelItemAttribute> m_BlockAttributeUIConfig = new List<InfoPanelItemAttribute>(0);

	private UILayoutElementFromChildren m_ChildLayourFitter;

	public void UpdateBlockFromGridSelection(UIBlockSelectGrid grid)
	{
		if (grid.TryGetSelection(out var type))
		{
			UpdateBlock(type);
		}
		else
		{
			ClearBlock();
		}
	}

	public void UpdateBlockFromGridSelection(UIBlockSelectGrid grid, Func<BlockTypes, bool> canPurchaseFunc)
	{
		string costText = string.Empty;
		bool flag = false;
		if (grid.TryGetSelection(out var type))
		{
			UpdateBlock(type);
			m_DescriptionText.color = m_BlockDescriptionDefaultColour;
			int num = (Singleton.Manager<RecipeManager>.inst ? Singleton.Manager<RecipeManager>.inst.GetBlockBuyPrice(type, silentFail: true) : 0);
			int blockTier = Singleton.Manager<ManLicenses>.inst.GetBlockTier(type);
			flag = canPurchaseFunc(type);
			if (flag)
			{
				flag = Singleton.Manager<ManPlayer>.inst.CanAfford(num);
				costText = Singleton.Manager<Localisation>.inst.GetMoneyString(num);
				m_DescriptionText.text = StringLookup.GetItemDescription(ObjectTypes.Block, (int)type);
			}
			else if (Singleton.Manager<ManLicenses>.inst.GetBlockState(type) == ManLicenses.BlockState.Unknown)
			{
				costText = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Purchasing, 4);
				m_DescriptionText.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Purchasing, 5);
			}
			else if (blockTier == int.MaxValue)
			{
				costText = "???";
				m_DescriptionText.text = "???";
			}
			else
			{
				costText = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Purchasing, 6), blockTier + 1);
				m_DescriptionText.text = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Purchasing, 7), Singleton.Manager<ManSpawn>.inst.GetCorporation(type).ToString(), blockTier + 1);
			}
		}
		else
		{
			ClearBlock();
		}
		SetPurchaseButtonState(costText, flag);
	}

	public void ClearBlock()
	{
		m_NameText?.SetText("");
		m_DescriptionText?.SetText("");
		m_BlockCategoryText?.SetText("");
		m_BlockCategoryTooltip?.SetText(string.Empty);
		m_DamageTypeDisplay?.Hide();
		m_DamageableTypeDisplay?.Hide();
		m_GradeValueText?.SetText("");
		m_GradeStringText?.SetText("");
		m_RarityText?.SetText("");
		ClearBlockAttrList();
		m_RarityTooltip?.SetText(string.Empty);
		if (m_RarityIcon != null)
		{
			m_RarityIcon.sprite = null;
		}
		m_LimiterCostElement?.SetActive(value: false);
		m_LimiterCostText?.SetText("");
		if (m_CorpIcon != null)
		{
			m_CorpIcon.sprite = null;
		}
		m_CorpAndGradeTooltip?.SetText(string.Empty);
		if (m_BlockCategoryIcon != null)
		{
			m_BlockCategoryIcon.sprite = null;
		}
		if (m_DescriptionText != null)
		{
			m_DescriptionText.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Purchasing, 11);
			m_DescriptionText.color = m_BlockDescriptionDefaultColour;
			if (m_DescriptionScroller != null)
			{
				m_DescriptionScroller.Reset();
				m_DescriptionScroller.enabled = m_AutoScrollEnabled || Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled();
			}
		}
		base.gameObject.SetActive(value: false);
	}

	public void UpdateBlock(BlockTypes blockType, bool purchasing = false)
	{
		base.gameObject.SetActive(value: true);
		m_NameText?.SetText(StringLookup.GetItemName(ObjectTypes.Block, (int)blockType));
		if (m_DescriptionText != null)
		{
			if (!purchasing)
			{
				string text = StringLookup.GetItemDescription(ObjectTypes.Block, (int)blockType);
				Color color = m_BlockDescriptionDefaultColour;
				if (m_DoBlockAllowedChecksDefault && Singleton.Manager<ManSpawn>.inst.IsBlockUsageRestrictedInGameMode(blockType))
				{
					text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.Purchasing.NotAvailable);
					color = m_BlockDescriptionWarningColour;
				}
				else if (Singleton.Manager<ManMods>.inst.IsModdedBlock(blockType))
				{
					text = "<b>" + Singleton.Manager<ManMods>.inst.GetModNameForBlockID(blockType) + " |</b> " + text;
				}
				m_DescriptionText.SetText(text);
				m_DescriptionText.color = color;
			}
			if (m_DescriptionScroller != null)
			{
				m_DescriptionScroller.Reset();
				m_DescriptionScroller.enabled = m_AutoScrollEnabled || Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled();
			}
		}
		if (m_BlockCategoryIcon != null)
		{
			m_BlockCategoryIcon.sprite = Singleton.Manager<ManUI>.inst.GetBlockCatIcon(Singleton.Manager<ManSpawn>.inst.GetCategory(blockType));
		}
		string blockCategoryName = StringLookup.GetBlockCategoryName(Singleton.Manager<ManSpawn>.inst.GetCategory(blockType));
		m_BlockCategoryText?.SetText(blockCategoryName);
		m_BlockCategoryTooltip?.SetText(blockCategoryName);
		m_DamageTypeDisplay?.SetBlock(blockType);
		m_DamageableTypeDisplay?.SetBlock(blockType);
		if (m_LimiterCostElement != null)
		{
			m_LimiterCostElement.SetActive(Singleton.Manager<ManBlockLimiter>.inst.IsNotNull() && Singleton.Manager<ManBlockLimiter>.inst.RequestedOn);
			m_LimiterCostText?.SetText(Singleton.Manager<ManBlockLimiter>.inst.GetBlockCost(blockType).ToString("F0"));
		}
		FactionSubTypes corporation = Singleton.Manager<ManSpawn>.inst.GetCorporation(blockType);
		if (m_CorpIcon != null)
		{
			m_CorpIcon.sprite = ((m_CorpIconStyle == CorpIconStyle.Selected) ? Singleton.Manager<ManUI>.inst.GetSelectedCorpIcon(corporation) : Singleton.Manager<ManUI>.inst.GetCorpIcon(corporation));
		}
		m_GradeValueText?.SetText(StringLookup.GetBlockTierName(blockType, usePrefixText: false));
		string blockTierName = StringLookup.GetBlockTierName(blockType);
		m_GradeStringText?.SetText(blockTierName);
		m_CorpAndGradeTooltip?.SetText(StringLookup.GetCorporationName(corporation) + " " + blockTierName);
		BlockRarity rarity = Singleton.Manager<ManSpawn>.inst.GetRarity(blockType);
		string blockRarityName = StringLookup.GetBlockRarityName(rarity);
		m_RarityText?.SetText(blockRarityName);
		m_RarityTooltip?.SetText(blockRarityName);
		if (m_RarityIcon != null)
		{
			m_RarityIcon.sprite = Singleton.Manager<ManUI>.inst.m_SpriteFetcher.GetBlockRarity(rarity);
		}
		m_ChildLayourFitter?.ForceRefresh();
		if (m_BlockAttrContainer != null)
		{
			ClearBlockAttrList();
			BlockAttributes[] blockAttributes = Singleton.Manager<ManSpawn>.inst.GetBlockAttributes(blockType);
			for (int i = 0; i < blockAttributes.Length; i++)
			{
				AddBlockAttribute(blockAttributes[i]);
			}
		}
	}

	private void SetPurchaseButtonState(string costText, bool interactable)
	{
		if ((bool)m_PurchaseBlockButton)
		{
			m_PurchaseButtonImage.color = (interactable ? m_PurchaseOkColour : m_PurchaseInvalidColour);
			m_PurchaseBlockButton.interactable = interactable;
			if ((bool)m_PurchaseBlockPriceText)
			{
				m_PurchaseBlockPriceText.text = costText;
			}
		}
	}

	private void AddBlockAttribute(BlockAttributes blockAttribute)
	{
		if (!(m_BlockAttrContainer == null))
		{
			InfoPanelItemAttribute infoPanelItemAttribute = m_ItemBlockAttrPrefab.Spawn();
			infoPanelItemAttribute.transform.SetParent(m_BlockAttrContainer, worldPositionStays: false);
			InfoOverlayDataValues.ItemAttribute data = new InfoOverlayDataValues.ItemAttribute(StringLookup.GetBlockAttribute(blockAttribute), Singleton.Manager<ManUI>.inst.GetBlockAttributeIcon(blockAttribute));
			infoPanelItemAttribute.Setup(data);
			m_BlockAttributeUIConfig.Add(infoPanelItemAttribute);
			if (m_DisableBlockAttrContainerOnEmpty && !m_BlockAttrContainer.gameObject.activeSelf)
			{
				m_BlockAttrContainer.gameObject.SetActive(value: true);
			}
		}
	}

	private void ClearBlockAttrList()
	{
		if (m_BlockAttrContainer == null)
		{
			return;
		}
		foreach (InfoPanelItemAttribute item in m_BlockAttributeUIConfig)
		{
			item.transform.SetParent(null, worldPositionStays: false);
			item.Recycle();
		}
		m_BlockAttributeUIConfig.Clear();
		if (m_DisableBlockAttrContainerOnEmpty)
		{
			m_BlockAttrContainer.gameObject.SetActive(value: false);
		}
	}

	private void OnRecycle()
	{
		ClearBlockAttrList();
	}

	private void Awake()
	{
		m_BlockAttributeUIConfig = GetComponentsInChildren<InfoPanelItemAttribute>().ToList();
		if (m_DescriptionText != null)
		{
			m_BlockDescriptionDefaultColour = m_DescriptionText.color;
		}
		m_ChildLayourFitter = GetComponent<UILayoutElementFromChildren>();
	}
}
