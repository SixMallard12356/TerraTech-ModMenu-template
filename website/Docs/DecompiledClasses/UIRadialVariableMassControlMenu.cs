using System;
using UnityEngine;

[Obsolete("Migrated to block context menu workflow", true)]
public class UIRadialVariableMassControlMenu : UIHUDElement
{
	[SerializeField]
	private RadialMenu m_RadialMenu;

	[SerializeField]
	private float m_SliderGamepadSensitivity = 0.05f;

	[SerializeField]
	private float m_SliderGamepadDelay = 0.2f;

	[SerializeField]
	private GameObject m_RightPanel;

	private UIRadialMenuSlider m_Slider;

	private Tank m_TargetTank;

	private ModuleVariableMass m_VariableMassBlock;

	private IRadialInputController m_RadialController;

	private float m_SliderLastGamepadUpdate;

	public override void Show(object context)
	{
	}

	public override void Hide(object context)
	{
	}

	private void OnOptionSelected(int option)
	{
	}

	private void OnOptionHovered(int option)
	{
	}

	private bool IsMenuAvailableForTech(Tank targetTank)
	{
		return false;
	}

	private void Awake()
	{
	}

	private void Update()
	{
	}
}
