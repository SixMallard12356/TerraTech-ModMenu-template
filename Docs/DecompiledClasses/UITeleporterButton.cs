using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class UITeleporterButton : UISelectableHoverableButton
{
	[SerializeField]
	private Text m_Text;

	[SerializeField]
	private Color m_TextHighlightedColor;

	private Color TextDefaultColor;

	public override void Initialise()
	{
		base.Initialise();
		if (m_Text != null)
		{
			TextDefaultColor = m_Text.color;
		}
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);
		UpdateTextHighlightColor(highlighted: true);
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
		UpdateTextHighlightColor(highlighted: false);
	}

	public override void OnSelect(BaseEventData eventData)
	{
		base.OnSelect(eventData);
		UpdateTextHighlightColor(highlighted: true);
	}

	public override void OnDeselect(BaseEventData eventData)
	{
		base.OnDeselect(eventData);
		UpdateTextHighlightColor(highlighted: false);
	}

	private void UpdateTextHighlightColor(bool highlighted)
	{
		if (m_Text != null && IsInteractable())
		{
			m_Text.color = (highlighted ? m_TextHighlightedColor : TextDefaultColor);
		}
	}

	private void OnRecycle()
	{
		m_Text.color = TextDefaultColor;
	}
}
