#define UNITY_EDITOR
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItemDisplay : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IInteractionCursorEnterHandler, IInteractionCursorExitHandler
{
	[SerializeField]
	private Image m_Image;

	[SerializeField]
	private Image m_ImageBackground;

	[SerializeField]
	private Text m_Title;

	[SerializeField]
	private TMP_Text m_TitleTMP;

	[SerializeField]
	private Text m_QuantityText;

	[SerializeField]
	private GameObject[] m_ComponentTierMarkers;

	[SerializeField]
	private GameObject m_WarningIcon;

	[SerializeField]
	private Image m_ModdedItem;

	public Event<UIItemDisplay> PointerEntered;

	public Event<UIItemDisplay> PointerExited;

	private TooltipComponent m_TooltipComponent;

	private Outline[] m_Outlines;

	public ItemTypeInfo ItemType { get; private set; }

	public RectTransform RectTrans { get; private set; }

	public void Setup(ItemTypeInfo itemType)
	{
		Setup(itemType, Color.white, Color.white, string.Empty, Color.white, showComponentTier: false, showWarningIcon: false);
	}

	public void Setup(ItemTypeInfo itemType, string quantityText)
	{
		Setup(itemType, Color.white, Color.white, quantityText, Color.white, showComponentTier: false, showWarningIcon: false);
	}

	public void Setup(ItemTypeInfo itemType, Color itemColor, Color itemBackgroundColor, string quantityText)
	{
		Setup(itemType, itemColor, itemBackgroundColor, quantityText, Color.white, showComponentTier: false, showWarningIcon: false);
	}

	public void Setup(ItemTypeInfo itemType, Color itemColor, Color itemBackgroundColor, string quantityText, Color quantityTextColor, bool showComponentTier, bool showWarningIcon)
	{
		ItemType = itemType;
		m_Image.sprite = Singleton.Manager<ManUI>.inst.GetSprite(itemType.ObjectType, itemType.ItemType);
		m_Image.color = itemColor;
		if (m_ImageBackground != null)
		{
			m_ImageBackground.color = itemBackgroundColor;
		}
		if ((bool)m_Title || (bool)m_TitleTMP)
		{
			string itemName = StringLookup.GetItemName(itemType.ObjectType, itemType.ItemType);
			if (m_Title != null)
			{
				m_Title.text = itemName;
			}
			if (m_TitleTMP != null)
			{
				m_TitleTMP.text = itemName;
			}
		}
		if (m_TooltipComponent != null)
		{
			m_TooltipComponent.SetText(StringLookup.GetItemName(itemType.ObjectType, itemType.ItemType));
		}
		if (m_QuantityText != null)
		{
			m_QuantityText.text = quantityText;
			m_QuantityText.color = quantityTextColor;
		}
		int num = (showComponentTier ? GetNumComponentTierMarkers(itemType) : 0);
		for (int i = 0; i < m_ComponentTierMarkers.Length; i++)
		{
			m_ComponentTierMarkers[i].SetActive(num > i);
		}
		if (m_WarningIcon != null)
		{
			m_WarningIcon.SetActive(showWarningIcon);
		}
		if (m_ModdedItem != null)
		{
			m_ModdedItem.gameObject.SetActive(itemType.ObjectType == ObjectTypes.Block && Singleton.Manager<ManMods>.inst.IsModdedBlock((BlockTypes)itemType.ItemType));
		}
	}

	public void SetOutlinesEnabled(bool outlinesEnabled)
	{
		if (m_Outlines != null)
		{
			for (int i = 0; i < m_Outlines.Length; i++)
			{
				m_Outlines[i].enabled = outlinesEnabled;
			}
		}
	}

	public void SetDisplayItemName(bool displayName)
	{
		if ((bool)m_Title || (bool)m_TitleTMP)
		{
			string text = (displayName ? StringLookup.GetItemName(ItemType.ObjectType, ItemType.ItemType) : string.Empty);
			if (m_Title != null)
			{
				m_Title.text = text;
			}
			if (m_TitleTMP != null)
			{
				m_TitleTMP.text = text;
			}
		}
	}

	private int GetNumComponentTierMarkers(ItemTypeInfo itemType)
	{
		switch ((ComponentTier)((itemType.ObjectType == ObjectTypes.Chunk) ? Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.GetDescriptorFlags<ComponentTier>(itemType.GetHashCode()) : 0))
		{
		case ComponentTier.Null:
			return 0;
		case ComponentTier.Simple:
			return 0;
		case ComponentTier.Advanced:
			return 1;
		case ComponentTier.Complex:
			return 2;
		case ComponentTier.Exotic:
			return 3;
		default:
			d.AssertFormat(false, "UIItemDisplay.GetNumTierMarkers unhandled tier {0}", itemType);
			return 0;
		}
	}

	public void OnPointerEnter(PointerEventData dataName)
	{
		PointerEntered.Send(this);
	}

	public void OnPointerExit(PointerEventData dataName)
	{
		PointerExited.Send(this);
	}

	public void OnInteractionCursorEnter(PointerEventData dataName)
	{
		PointerEntered.Send(this);
	}

	public void OnInteractionCursorExit(PointerEventData dataName)
	{
		PointerExited.Send(this);
	}

	private void Awake()
	{
		RectTrans = base.transform as RectTransform;
		m_TooltipComponent = GetComponent<TooltipComponent>();
		m_Outlines = GetComponentsInChildren<Outline>();
	}
}
