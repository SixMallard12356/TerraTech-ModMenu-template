#define UNITY_EDITOR
using UnityEngine;

public class UIRadialHoverControlMenu : UIHUDElement
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

	private IRadialInputController m_RadialController;

	private float m_SliderLastGamepadUpdate;

	public override void Show(object context)
	{
		OpenMenuEventData openMenuEventData = (OpenMenuEventData)context;
		TankBlock targetTankBlock = openMenuEventData.m_TargetTankBlock;
		if (IsMenuAvailableForTech(targetTankBlock.tank) && openMenuEventData.m_AllowRadialMenu)
		{
			m_TargetTank = targetTankBlock.tank;
			m_RadialController = Singleton.Manager<ManInput>.inst.GetRadialInputController(openMenuEventData.m_RadialInputController);
			m_Slider.value = m_TargetTank.BlockStateController.HoverPower;
			m_Slider.gameObject.SetActive(value: false);
			if ((bool)m_RightPanel)
			{
				m_RightPanel.SetActive(value: false);
			}
			base.Show(context);
			m_RadialMenu.Show(openMenuEventData.m_RadialInputController, m_TargetTank != Singleton.playerTank);
			for (int i = 0; i < m_RadialMenu.GetOptionsCount(); i++)
			{
				(m_RadialMenu.GetOption(i) as UIRadialMenuOptionWithWarning).SetIsAllowed(isAllowed: true);
			}
		}
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		m_Slider.SetIsHighlighted(isHighlighted: false);
		m_RadialMenu.Hide();
		m_TargetTank.BlockStateController.HoverPower = m_Slider.value;
		m_TargetTank.BlockStateController.SendHoverAndGyroToServer();
		m_TargetTank = null;
		m_RadialController = null;
	}

	private void OnOptionSelected(int option)
	{
		if (option == 0)
		{
			if (!m_Slider.gameObject.activeInHierarchy)
			{
				m_Slider.gameObject.SetActive(value: true);
				m_Slider.SetIsHighlighted(isHighlighted: true);
				m_RadialMenu.SetModal(modal: true, option);
				if ((bool)m_RightPanel)
				{
					m_RightPanel.SetActive(value: true);
				}
			}
			else
			{
				HideSelf();
			}
		}
		else
		{
			bool flag = m_TargetTank.BlockStateController.IsCategoryActive(ModuleControlCategory.Hover);
			m_TargetTank.BlockStateController.RequestSetCategoryActive(ModuleControlCategory.Hover, !flag);
			HideSelf();
		}
	}

	private void OnOptionHovered(int option)
	{
	}

	private bool IsMenuAvailableForTech(Tank targetTank)
	{
		bool num = !Singleton.Manager<ManGameMode>.inst.LockPlayerControls && Singleton.playerTank != null;
		bool flag = targetTank != null && targetTank == Singleton.playerTank && targetTank.Team == Singleton.Manager<ManPlayer>.inst.PlayerTeam && targetTank.visible.IsInteractible;
		bool flag2 = false;
		bool flag3 = Singleton.Manager<ManPointer>.inst.DraggingItem != null;
		bool isInteractionBlocked = Singleton.Manager<ManPointer>.inst.IsInteractionBlocked;
		if (num && flag && !flag2 && !isInteractionBlocked)
		{
			return !flag3;
		}
		return false;
	}

	private void Awake()
	{
		d.Assert(m_RadialMenu != null);
		m_Slider = GetComponentInChildren<UIRadialMenuSlider>();
		m_RadialMenu.OnOptionHovered.Subscribe(OnOptionHovered);
		m_RadialMenu.OnOptionSelected.Subscribe(OnOptionSelected);
		m_RadialMenu.OnClose.Subscribe(base.HideSelf);
	}

	private void Update()
	{
		if (!m_TargetTank)
		{
			return;
		}
		if (m_RadialMenu.IsModal())
		{
			if (m_RadialController.DidCancel() || (m_RadialController.IsGamePad() && m_RadialController.DidSelect()))
			{
				m_Slider.SetIsHighlighted(isHighlighted: false);
				m_RadialMenu.SetModal(modal: false);
				m_Slider.gameObject.SetActive(value: false);
				if ((bool)m_RightPanel)
				{
					m_RightPanel.SetActive(value: false);
				}
			}
			else if (Time.time >= m_SliderLastGamepadUpdate + m_SliderGamepadDelay)
			{
				float axis = Singleton.Manager<ManInput>.inst.GetAxis(19);
				if (!Mathf.Approximately(axis, 0f))
				{
					m_Slider.value += m_SliderGamepadSensitivity * Mathf.Sign(axis);
					m_SliderLastGamepadUpdate = Time.time;
				}
			}
		}
		m_TargetTank.BlockStateController.HoverPower = m_Slider.value;
	}
}
