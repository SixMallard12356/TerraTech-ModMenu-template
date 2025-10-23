using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class UITechSelectorTabHelper : MonoBehaviour
{
	[SerializeField]
	private UITechSelector m_TechSelector;

	[SerializeField]
	private UITechSelector.TechLocations m_TargetTab;

	private Toggle m_ToggleComponent;

	private bool m_WasInitiallyToggled;

	public UITechSelector.TechLocations TargetTab => m_TargetTab;

	public void ShowTab()
	{
		if (m_TechSelector.IsVisible && m_TechSelector.CurrentTab != m_TargetTab)
		{
			m_TechSelector.ShowTab(m_TargetTab);
		}
	}

	private void OnPool()
	{
		m_ToggleComponent = GetComponent<Toggle>();
		m_WasInitiallyToggled = m_ToggleComponent.isOn;
	}

	private void OnSpawn()
	{
		m_ToggleComponent.isOn = m_WasInitiallyToggled;
	}
}
