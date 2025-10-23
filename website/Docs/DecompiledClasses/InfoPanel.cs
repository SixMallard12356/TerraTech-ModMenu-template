using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ManHUD.Focussable
{
	[Serializable]
	private class InfoPanelBlockItems : InfoPanelItems
	{
		[SerializeField]
		public UIBlockInfoDisplay m_BlockInfoDisplay;
	}

	[Serializable]
	private class InfoPanelChunkItems : InfoPanelItems
	{
		[SerializeField]
		public TextMeshProUGUI m_CategoryText;

		[SerializeField]
		public Image m_CategoryIcon;

		[SerializeField]
		public TextMeshProUGUI m_RarityText;

		[SerializeField]
		public Image m_RarityIcon;

		[SerializeField]
		public TextMeshProUGUI m_PriceText;

		[SerializeField]
		public Image m_PriceIcon;
	}

	[Serializable]
	public class InfoPanelSceneryItems : InfoPanelItems
	{
		[SerializeField]
		public UIDamageTypeDisplay m_DamageableTypeDisplay;
	}

	[Serializable]
	public class InfoPanelItems
	{
		[SerializeField]
		protected GameObject[] m_PanelSpecificUncollapsedItems = new GameObject[0];

		[SerializeField]
		[FormerlySerializedAs("m_Container")]
		public RectTransform m_CollapsableContainer;

		public void SetPanelSpecificUncollapsedItemsActive(bool state)
		{
			for (int i = 0; i < m_PanelSpecificUncollapsedItems.Length; i++)
			{
				m_PanelSpecificUncollapsedItems[i].SetActive(state);
			}
		}
	}

	[Header("General Content")]
	[SerializeField]
	private TextMeshProUGUI m_MainTitle;

	[SerializeField]
	private TextMeshProUGUI m_SubTitle;

	[SerializeField]
	private TextMeshProUGUI m_Description;

	[SerializeField]
	private UITimedAutoScrollView m_DescriptionScroller;

	[SerializeField]
	private Image m_Icon;

	[SerializeField]
	private RectTransform m_MoreInfoPanel;

	[SerializeField]
	private Transform m_LineStart;

	[Header("Panel Specific Content")]
	[FormerlySerializedAs("m_Block_ExpandedItems")]
	[SerializeField]
	private InfoPanelBlockItems m_BlockItems;

	[FormerlySerializedAs("m_Chunk_ExpandedItems")]
	[SerializeField]
	private InfoPanelChunkItems m_ChunkItems;

	[FormerlySerializedAs("m_Scenery_ExpandedItems")]
	[SerializeField]
	private InfoPanelSceneryItems m_SceneryItems;

	[SerializeField]
	[Header("Attribute things")]
	[FormerlySerializedAs("m_ItemAttributePrefab")]
	private InfoPanelItemAttribute m_ItemBlockAttrPrefab;

	[SerializeField]
	[FormerlySerializedAs("m_AttributeContainer")]
	private RectTransform m_BlockAttrContainer;

	[SerializeField]
	private InfoPanelControlAttribute m_ItemControlAttrPrefab;

	[SerializeField]
	private RectTransform m_ControlAttrContainer;

	private InfoPanelItems[] m_AllInfoPanelItems;

	private RectTransform m_Rect;

	private string m_FoldedText = string.Empty;

	private string m_ExpandedText = string.Empty;

	private List<InfoPanelItemAttribute> m_BlockAttributes = new List<InfoPanelItemAttribute>(0);

	private List<InfoPanelControlAttribute> m_ControlAttributes = new List<InfoPanelControlAttribute>(0);

	public Vector3 LineStartPos => m_LineStart.position;

	public bool PointerInside { get; private set; }

	public RectTransform Rect => m_Rect;

	public bool IsFolded { get; private set; }

	public bool ShouldHide { get; private set; }

	public UITimedAutoScrollView DescriptionScroller => m_DescriptionScroller;

	protected InfoPanelItems[] AllInfoPanelItems
	{
		get
		{
			if (m_AllInfoPanelItems == null)
			{
				m_AllInfoPanelItems = new InfoPanelItems[3] { m_BlockItems, m_ChunkItems, m_SceneryItems };
			}
			return m_AllInfoPanelItems;
		}
	}

	public void Setup(InfoOverlayDataValues data)
	{
		if (m_MainTitle.text != data.m_MainTitle)
		{
			m_MainTitle.text = data.m_MainTitle;
		}
		SetupSubtitle(data.m_Subtitle, data.m_SubtitleExpanded);
		if (m_Description.text != data.m_Description)
		{
			m_Description.text = data.m_Description;
		}
		SetupIcon(data.IconSprite);
		for (int i = 0; i < AllInfoPanelItems.Length; i++)
		{
			AllInfoPanelItems[i].m_CollapsableContainer.gameObject.SetActive(value: false);
			AllInfoPanelItems[i].SetPanelSpecificUncollapsedItemsActive(state: false);
		}
		switch (data.m_SubjectType)
		{
		case ObjectTypes.Block:
			m_BlockItems.m_BlockInfoDisplay?.UpdateBlock(data.m_BlockType);
			m_BlockItems.m_CollapsableContainer.gameObject.SetActive(value: true);
			m_BlockItems.SetPanelSpecificUncollapsedItemsActive(state: true);
			break;
		case ObjectTypes.Chunk:
			m_ChunkItems.m_CategoryText.text = data.m_Category;
			m_ChunkItems.m_PriceText.text = data.m_Price;
			m_ChunkItems.m_RarityText.text = data.m_Rarity;
			m_ChunkItems.m_RarityIcon.sprite = data.m_RarityIcon;
			m_ChunkItems.m_CollapsableContainer.gameObject.SetActive(value: true);
			m_ChunkItems.SetPanelSpecificUncollapsedItemsActive(state: true);
			break;
		case ObjectTypes.Scenery:
			m_SceneryItems.m_DamageableTypeDisplay?.Set(data.m_DamageableType);
			m_SceneryItems.m_CollapsableContainer.gameObject.SetActive(value: true);
			m_SceneryItems.SetPanelSpecificUncollapsedItemsActive(state: true);
			break;
		}
		ClearBlockAttrList();
		for (int j = 0; j < data.m_BlockAttributes.Count; j++)
		{
			InfoPanelItemAttribute infoPanelItemAttribute = m_ItemBlockAttrPrefab.Spawn();
			infoPanelItemAttribute.transform.SetParent(m_BlockAttrContainer, worldPositionStays: false);
			infoPanelItemAttribute.Setup(data.m_BlockAttributes[j]);
			m_BlockAttributes.Add(infoPanelItemAttribute);
		}
		ClearControlAttrList();
		for (int k = 0; k < data.m_ControlAttributes.Count; k++)
		{
			InfoPanelControlAttribute infoPanelControlAttribute = m_ItemControlAttrPrefab.Spawn();
			infoPanelControlAttribute.transform.SetParent(m_ControlAttrContainer, worldPositionStays: false);
			infoPanelControlAttribute.Setup(data.m_ControlAttributes[k]);
			m_ControlAttributes.Add(infoPanelControlAttribute);
		}
		m_BlockAttrContainer.gameObject.SetActive(data.m_BlockAttributes.Count > 0);
		m_ControlAttrContainer.gameObject.SetActive(data.m_ControlAttributes.Count > 0);
	}

	public void SetupSubtitle(string foldedText, string expandedText)
	{
		m_FoldedText = foldedText;
		m_ExpandedText = expandedText;
		string text = (IsFolded ? m_FoldedText : m_ExpandedText);
		if (m_SubTitle.text != text)
		{
			m_SubTitle.text = text;
		}
	}

	public void SetFolded(bool folded)
	{
		IsFolded = folded;
		m_MoreInfoPanel.gameObject.SetActive(!IsFolded);
		m_SubTitle.text = (IsFolded ? m_FoldedText : m_ExpandedText);
		m_Icon.gameObject.SetActive(IsFolded);
	}

	private void SetupIcon(Sprite sprite)
	{
		if ((bool)m_Icon)
		{
			if (m_Icon.sprite != sprite)
			{
				m_Icon.sprite = sprite;
			}
			if (m_ChunkItems.m_CategoryIcon.sprite != sprite)
			{
				m_ChunkItems.m_CategoryIcon.sprite = sprite;
			}
		}
	}

	private void ClearBlockAttrList()
	{
		for (int num = m_BlockAttributes.Count - 1; num >= 0; num--)
		{
			m_BlockAttributes[num].transform.SetParent(null, worldPositionStays: false);
			m_BlockAttributes[num].Recycle();
		}
		m_BlockAttributes.Clear();
	}

	private void ClearControlAttrList()
	{
		for (int num = m_ControlAttributes.Count - 1; num >= 0; num--)
		{
			m_ControlAttributes[num].transform.SetParent(null, worldPositionStays: false);
			m_ControlAttributes[num].Recycle();
		}
		m_ControlAttributes.Clear();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		PointerInside = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		PointerInside = false;
	}

	private void OnPool()
	{
		m_Rect = GetComponent<RectTransform>();
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManHUD>.inst.AddOverlay(this);
	}

	private void OnRecycle()
	{
		ClearBlockAttrList();
		ClearControlAttrList();
		Singleton.Manager<ManHUD>.inst.RemoveOverlay(this);
	}

	public void SetFocusLevel(ManHUD.FocusLevel level)
	{
		ShouldHide = level == ManHUD.FocusLevel.Dimmed;
	}

	public Transform GetTransform()
	{
		return m_Rect;
	}
}
