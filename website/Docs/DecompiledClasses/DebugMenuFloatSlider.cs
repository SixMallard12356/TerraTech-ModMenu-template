using System;

public class DebugMenuFloatSlider : DebugMenuObject
{
	private Func<float> m_GetSliderValFunction;

	private Action<float> m_UpdateSliderAction;

	private float m_SliderVal;

	public float MinVal { get; private set; }

	public float MaxVal { get; private set; }

	public bool WholeNumbers { get; private set; }

	public DebugMenuFloatSlider(string name, float min, float max, bool wholeNumbers, Func<float> getSliderValFunction, Action<float> updateSliderAction)
	{
		base.Name = name;
		MinVal = min;
		MaxVal = max;
		WholeNumbers = wholeNumbers;
		m_GetSliderValFunction = getSliderValFunction;
		m_UpdateSliderAction = updateSliderAction;
	}

	public void SetSliderVal(float sliderVal)
	{
		m_SliderVal = sliderVal;
	}

	public float GetSliderValue()
	{
		m_SliderVal = m_GetSliderValFunction();
		return m_SliderVal;
	}

	public override void TriggerMenuOption()
	{
		m_UpdateSliderAction(m_SliderVal);
	}

	public override ManDebugMenu.DebugMenuType MenuType()
	{
		return ManDebugMenu.DebugMenuType.FloatSlider;
	}
}
