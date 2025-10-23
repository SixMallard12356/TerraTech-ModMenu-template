#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHUDToggleButton : UIHUDElement
{
	[SerializeField]
	private Toggle m_ToggleButton;

	[SerializeField]
	private bool m_ToggleDefaultsToOn;

	private UnityAction<bool> m_ToggleAction;

	private bool m_IgnoreToggleCallback;

	public override void Show(object context)
	{
		m_ToggleButton.isOn = m_ToggleDefaultsToOn;
		if (context != null)
		{
			m_ToggleAction = context as UnityAction<bool>;
		}
		base.Show(context);
	}

	public override void Hide(object context)
	{
		m_ToggleAction = null;
		base.Hide(context);
	}

	public void SetToggledState(bool toggled)
	{
		m_IgnoreToggleCallback = true;
		m_ToggleButton.isOn = toggled;
		m_IgnoreToggleCallback = false;
	}

	private void OnButtonToggled(bool toggledOn)
	{
		if (m_ToggleAction != null && !m_IgnoreToggleCallback)
		{
			m_ToggleAction(toggledOn);
		}
	}

	private void OnSpawn()
	{
		d.Assert(m_ToggleButton != null, "UIHUDToggleButton - Could not find Toggle button component!");
		m_ToggleButton.onValueChanged.AddListener(OnButtonToggled);
	}

	private void OnRecycle()
	{
		m_ToggleButton.onValueChanged.RemoveListener(OnButtonToggled);
	}
}
