using System;
using Rewired;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISchemaMenuAxisButtonPair : MonoBehaviour
{
	[SerializeField]
	private InputAxisMapping m_AssignedMapping;

	[SerializeField]
	private Button m_MainButton;

	[SerializeField]
	private TextMeshProUGUI m_MainText;

	[SerializeField]
	private Button m_ReverseButton;

	[SerializeField]
	private TextMeshProUGUI m_ReverseText;

	[SerializeField]
	private float m_IconFontSize;

	private Event<InputAxisMapping, bool> OnMappingSelected;

	private float m_KeyFontSize;

	public void Init(Action<InputAxisMapping, bool> selectedCallback)
	{
		OnMappingSelected.Subscribe(selectedCallback);
	}

	public void Setup(ControlScheme controlScheme)
	{
	}

	public void OnClicked()
	{
		OnMappingSelected.Send(m_AssignedMapping, paramB: false);
	}

	public void OnReverseClicked()
	{
		OnMappingSelected.Send(m_AssignedMapping, paramB: true);
	}

	public void UpdateText()
	{
		if (m_AssignedMapping != InputAxisMapping.Unmapped)
		{
			AxisMapping.GetRewiredActionElements(m_AssignedMapping, invert: false, out var pos, out var neg);
			bool num = pos != null;
			bool flag = neg != null && m_ReverseText != null;
			if (num)
			{
				m_MainText.text = Singleton.Manager<Localisation>.inst.ActionElementMapToString(pos);
				m_MainText.fontSize = ((pos.keyboardKeyCode != KeyboardKeyCode.None) ? m_KeyFontSize : m_IconFontSize);
			}
			if (flag)
			{
				m_ReverseText.text = Singleton.Manager<Localisation>.inst.ActionElementMapToString(neg);
				m_ReverseText.fontSize = ((neg.keyboardKeyCode != KeyboardKeyCode.None) ? m_KeyFontSize : m_IconFontSize);
			}
		}
	}

	public InputAxisMapping GetAssignedAxisMapping()
	{
		return m_AssignedMapping;
	}

	private void Awake()
	{
		m_KeyFontSize = m_MainText.fontSize;
		if (m_IconFontSize == 0f)
		{
			m_IconFontSize = m_KeyFontSize;
		}
		m_MainButton.onClick.AddListener(OnClicked);
		if (m_ReverseButton != null)
		{
			m_ReverseButton.onClick.AddListener(OnReverseClicked);
		}
	}
}
