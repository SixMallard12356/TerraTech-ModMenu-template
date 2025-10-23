using System;
using Rewired;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISchemaMenuBindingPair : QueryableSelectable
{
	[Serializable]
	public struct KeyBindDisplay
	{
		public Image m_BackingImage;

		public TextMeshProUGUI m_Text;
	}

	[SerializeField]
	private MovementAxis m_DesiredAxis;

	[SerializeField]
	private CustomExplicitNavigationButton m_ButtonShared;

	[SerializeField]
	private KeyBindDisplay m_KeyBind1Key1;

	[SerializeField]
	private KeyBindDisplay m_KeyBind1Key2;

	[SerializeField]
	private KeyBindDisplay m_KeyBind2Key1;

	[SerializeField]
	private KeyBindDisplay m_KeyBind2Key2;

	[SerializeField]
	private KeyBindDisplay m_KeyBindFullHeightKey1;

	[SerializeField]
	private KeyBindDisplay m_KeyBindFullHeightKey2;

	[SerializeField]
	private RectTransform m_KeyBind1Container;

	[SerializeField]
	private RectTransform m_KeyBind2Container;

	[SerializeField]
	private RectTransform m_KeyBindFullHeightContainer;

	[SerializeField]
	private LocalisedString m_AssigningKeyString;

	[SerializeField]
	private bool m_NegativeDirectionIsPrimary;

	[SerializeField]
	private float m_IconFontSize;

	private Action<MovementAxis, bool> OnHoverEvent;

	private Action<MovementAxis, bool, int> OnClickEvent;

	private ControlScheme m_ControlScheme;

	private float m_KeyFontSize;

	public MovementAxis m_MovementAxis => m_DesiredAxis;

	public LocalisedString AssigningKeyString => m_AssigningKeyString;

	public void Init(ControlScheme controlScheme, Action<MovementAxis, bool, int> clickCallback, Action<MovementAxis, bool> hoverCallback)
	{
		m_ControlScheme = controlScheme;
		UpdateText();
		if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad() && m_ButtonShared != null)
		{
			m_ButtonShared.OnSelected.Unsubscribe(OnButtonSelected);
			m_ButtonShared.OnSelected.Subscribe(OnButtonSelected);
		}
		OnClickEvent = clickCallback;
		OnHoverEvent = hoverCallback;
	}

	public void UpdateText()
	{
		AxisMapping axisMapping = m_ControlScheme.GetAxisMapping(m_DesiredAxis);
		axisMapping.GetRewiredActionElements(out var pos, out var neg, out var pos2, out var neg2);
		if (m_NegativeDirectionIsPrimary)
		{
			ActionElementMap actionElementMap = pos;
			pos = neg;
			neg = actionElementMap;
			ActionElementMap actionElementMap2 = pos2;
			pos2 = neg2;
			neg2 = actionElementMap2;
		}
		bool flag = SKU.ConsoleUI && axisMapping.m_InputAxis != InputAxisMapping.Unmapped && axisMapping.m_InputAxis2 == InputAxisMapping.Unmapped;
		m_KeyBind2Container.gameObject.SetActive(axisMapping.m_InputAxis2 != InputAxisMapping.Unmapped && (pos2 != null || neg2 != null) && !flag);
		m_KeyBind1Container.gameObject.SetActive(axisMapping.m_InputAxis != InputAxisMapping.Unmapped && (pos != null || neg != null) && !flag);
		m_KeyBindFullHeightContainer.gameObject.SetActive(axisMapping.m_InputAxis != InputAxisMapping.Unmapped && (pos != null || neg != null) && flag);
		if (flag)
		{
			UpdateKeyDisplay(ref m_KeyBindFullHeightKey1, pos, pos == null || pos.actionDescriptiveName == null);
			UpdateKeyDisplay(ref m_KeyBindFullHeightKey2, neg, neg == null || neg.actionDescriptiveName == null);
			return;
		}
		UpdateKeyDisplay(ref m_KeyBind1Key1, pos, pos == null || pos.actionDescriptiveName == null);
		UpdateKeyDisplay(ref m_KeyBind1Key2, neg, neg == null || neg.actionDescriptiveName == null);
		UpdateKeyDisplay(ref m_KeyBind2Key1, pos2, pos2 == null || pos2.actionDescriptiveName == null);
		UpdateKeyDisplay(ref m_KeyBind2Key2, neg2, neg2 == null || neg2.actionDescriptiveName == null);
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		OnHoverEvent.Send(m_MovementAxis, value2: true);
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		OnHoverEvent.Send(m_MovementAxis, value2: false);
	}

	private void OnClick1()
	{
		OnClickEvent.Send(m_MovementAxis, m_NegativeDirectionIsPrimary, 0);
	}

	private void OnClick2()
	{
		OnClickEvent.Send(m_MovementAxis, m_NegativeDirectionIsPrimary, 1);
	}

	private void OnClickShared()
	{
		OnClickEvent.Send(m_MovementAxis, m_NegativeDirectionIsPrimary, -1);
	}

	private void OnButtonSelected(bool selected)
	{
		OnHoverEvent(m_MovementAxis, selected);
	}

	private void UpdateKeyDisplay(ref KeyBindDisplay display, ActionElementMap aem, bool forceClear)
	{
		if (forceClear)
		{
			if ((bool)display.m_BackingImage)
			{
				display.m_BackingImage.enabled = false;
			}
			if ((bool)display.m_Text)
			{
				display.m_Text.text = string.Empty;
			}
		}
		else
		{
			bool flag = aem != null && aem.keyboardKeyCode != KeyboardKeyCode.None;
			if ((bool)display.m_BackingImage)
			{
				display.m_BackingImage.enabled = flag;
			}
			if ((bool)display.m_Text)
			{
				display.m_Text.text = Singleton.Manager<Localisation>.inst.ActionElementMapToString(aem);
				display.m_Text.fontSize = (flag ? m_KeyFontSize : m_IconFontSize);
			}
		}
		if ((bool)display.m_Text)
		{
			display.m_Text.transform.parent.gameObject.SetActive(!forceClear && aem != null);
		}
	}

	private void OnSpawn()
	{
		if (m_KeyFontSize == 0f)
		{
			m_KeyFontSize = m_KeyBind1Key1.m_Text.fontSize;
		}
		if (m_IconFontSize == 0f)
		{
			m_IconFontSize = m_KeyFontSize;
		}
		m_ButtonShared.onClick.AddListener(OnClickShared);
	}

	private void OnRecycle()
	{
		m_ButtonShared.onClick.RemoveListener(OnClickShared);
	}
}
