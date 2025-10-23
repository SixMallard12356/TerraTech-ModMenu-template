#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class FilterInfoPanel : OverlayPanel
{
	[SerializeField]
	private Image m_FilterModeImage;

	[EnumArray(typeof(ModuleItemFilter.AcceptMode))]
	[SerializeField]
	private Sprite[] m_FilterModeImages = new Sprite[0];

	[SerializeField]
	private UIItemDisplay m_SpecificItemDisplay;

	private ModuleItemFilter m_Filter;

	private ModuleItemFilter.AcceptMode m_DisplayedAcceptMode;

	private ItemTypeInfo m_DisplayedItemTypeInfo;

	public override void SetContext(object context)
	{
		m_Filter = context as ModuleItemFilter;
		d.Assert(context == null || m_Filter != null, "FilterInfoPanel - Failed to cast context to ModuleItemFilter");
		SetupPanel();
	}

	private void SetupPanel()
	{
		if (m_Filter != null)
		{
			switch (m_Filter.FilterMode)
			{
			case ModuleItemFilter.AcceptMode.None:
			case ModuleItemFilter.AcceptMode.Any:
			case ModuleItemFilter.AcceptMode.RawResource:
			case ModuleItemFilter.AcceptMode.RefinedResource:
			case ModuleItemFilter.AcceptMode.Component:
			case ModuleItemFilter.AcceptMode.Fuel:
				m_FilterModeImage.gameObject.SetActive(value: true);
				m_SpecificItemDisplay.gameObject.SetActive(value: false);
				m_FilterModeImage.sprite = m_FilterModeImages[(int)m_Filter.FilterMode];
				break;
			case ModuleItemFilter.AcceptMode.SpecificItem:
				m_FilterModeImage.gameObject.SetActive(value: false);
				m_SpecificItemDisplay.gameObject.SetActive(value: true);
				m_SpecificItemDisplay.Setup(m_Filter.SpecificAcceptedItem);
				break;
			default:
				d.LogError("FilterInfoPanel - Filter mode '" + m_Filter.FilterMode.ToString() + "' is not supported!");
				m_FilterModeImage.gameObject.SetActive(value: false);
				m_SpecificItemDisplay.gameObject.SetActive(value: false);
				break;
			}
			m_DisplayedAcceptMode = m_Filter.FilterMode;
			m_DisplayedItemTypeInfo = m_Filter.SpecificAcceptedItem;
		}
	}

	private void Update()
	{
		if ((bool)m_Filter && (m_DisplayedAcceptMode != m_Filter.FilterMode || m_DisplayedItemTypeInfo != m_Filter.SpecificAcceptedItem))
		{
			SetupPanel();
		}
	}
}
