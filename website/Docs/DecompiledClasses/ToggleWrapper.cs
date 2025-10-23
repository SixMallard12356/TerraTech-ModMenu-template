using UnityEngine;
using UnityEngine.UI;

public class ToggleWrapper
{
	public Event<bool> OnToggled;

	private Toggle m_Toggle;

	private bool m_SurpressEvent;

	public bool isOn
	{
		get
		{
			return m_Toggle.isOn;
		}
		set
		{
			m_Toggle.isOn = value;
		}
	}

	public bool interactable
	{
		get
		{
			return m_Toggle.interactable;
		}
		set
		{
			m_Toggle.interactable = value;
		}
	}

	public Transform transform => m_Toggle.transform;

	public bool AllowMultiFilter { get; private set; }

	public ToggleWrapper(Toggle toggle)
	{
		OnToggled = default(Event<bool>);
		m_Toggle = toggle;
		m_SurpressEvent = false;
		AllowMultiFilter = true;
		m_Toggle.onValueChanged.AddListener(OnToggle);
	}

	public void SetOnWithoutCallback(bool enabled)
	{
		bool surpressEvent = m_SurpressEvent;
		m_SurpressEvent = true;
		isOn = enabled;
		m_SurpressEvent = surpressEvent;
	}

	public void InvokeToggleHandler(bool enable, bool allowMultiFilter = true)
	{
		if (isOn == enable)
		{
			SetOnWithoutCallback(!enable);
		}
		AllowMultiFilter = allowMultiFilter;
		isOn = enable;
		AllowMultiFilter = true;
	}

	private void OnToggle(bool enabled)
	{
		if (!m_SurpressEvent)
		{
			OnToggled.Send(enabled);
		}
	}
}
