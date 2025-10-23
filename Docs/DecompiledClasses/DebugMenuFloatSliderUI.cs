using UnityEngine;
using UnityEngine.UI;

public class DebugMenuFloatSliderUI : DebugMenuUI
{
	[SerializeField]
	private Text m_TextField;

	[SerializeField]
	private InputField m_InputField;

	[SerializeField]
	private Slider m_Slider;

	public override void SetMenuObject(DebugMenuObject menuObject)
	{
		m_MenuData = menuObject;
		if (m_MenuData is DebugMenuFloatSlider debugMenuFloatSlider)
		{
			m_TextField.text = m_MenuData.Name;
			m_Slider.minValue = debugMenuFloatSlider.MinVal;
			m_Slider.maxValue = debugMenuFloatSlider.MaxVal;
			m_Slider.wholeNumbers = debugMenuFloatSlider.WholeNumbers;
			float sliderValue = debugMenuFloatSlider.GetSliderValue();
			m_Slider.value = sliderValue;
			m_InputField.text = sliderValue.ToString();
			m_InputField.contentType = (debugMenuFloatSlider.WholeNumbers ? InputField.ContentType.IntegerNumber : InputField.ContentType.DecimalNumber);
		}
	}

	public override void Show()
	{
		m_Slider.onValueChanged.RemoveAllListeners();
		m_Slider.onValueChanged.AddListener(UpdateSliderState);
		if (m_MenuData is DebugMenuFloatSlider debugMenuFloatSlider)
		{
			m_InputField.text = debugMenuFloatSlider.GetSliderValue().ToString();
		}
	}

	private void UpdateSliderState(float value)
	{
		if (m_MenuData is DebugMenuFloatSlider debugMenuFloatSlider)
		{
			debugMenuFloatSlider.SetSliderVal(value);
			debugMenuFloatSlider.TriggerMenuOption();
			m_InputField.text = value.ToString();
		}
	}

	public void OnValueEdited()
	{
		if (m_MenuData is DebugMenuFloatSlider debugMenuFloatSlider)
		{
			if (float.TryParse(m_InputField.text, out var result))
			{
				debugMenuFloatSlider.SetSliderVal(result);
				debugMenuFloatSlider.TriggerMenuOption();
				m_Slider.value = result;
			}
			else
			{
				m_InputField.text = debugMenuFloatSlider.GetSliderValue().ToString();
			}
		}
	}
}
