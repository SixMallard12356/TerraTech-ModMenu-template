using System;
using UnityEngine;
using UnityEngine.UI;

public class UIIconSelectionField_Option : MonoBehaviour
{
	[Serializable]
	public struct IconConfig
	{
		public Sprite Icon_FrontSprite;

		public Color Icon_FrontColor;

		public Sprite Icon_BackSprite;

		public Color Icon_BackColor;

		public bool UseTooltip;

		public LocalisedString TooltipText;
	}

	[SerializeField]
	protected Image m_IconFrontImage;

	[SerializeField]
	protected Image m_IconBackImage;

	[SerializeField]
	protected Image SelectedFrame;

	[SerializeField]
	protected Image HilightedFrame;

	[SerializeField]
	private TooltipComponent m_Tooltip;

	public Event<byte> OptionSelectedEvent;

	protected Color m_DefaultSelectedFrameColor = Color.white;

	public byte OptionIndex { get; private set; }

	public Button Button { get; private set; }

	public void SetOption(byte optionIndex, IconConfig iconConfig)
	{
		OptionIndex = optionIndex;
		m_IconFrontImage.sprite = iconConfig.Icon_FrontSprite;
		m_IconFrontImage.color = iconConfig.Icon_FrontColor;
		m_IconBackImage.sprite = iconConfig.Icon_BackSprite;
		m_IconBackImage.color = iconConfig.Icon_BackColor;
		m_Tooltip.enabled = iconConfig.UseTooltip;
		m_Tooltip?.SetText(iconConfig.UseTooltip ? iconConfig.TooltipText.Value : "");
	}

	public void UI_SelectOption()
	{
		OptionSelectedEvent.Send(OptionIndex);
	}

	public void SetOptionSelected(bool state)
	{
		SelectedFrame.gameObject.SetActive(state);
	}

	public void SetHilighted(bool state)
	{
		HilightedFrame.gameObject.SetActive(state);
		SelectedFrame.color = (state ? HilightedFrame.color : m_DefaultSelectedFrameColor);
	}

	public void Init()
	{
		if (!(Button != null))
		{
			Button = GetComponent<Button>();
			m_DefaultSelectedFrameColor = SelectedFrame.color;
		}
	}
}
