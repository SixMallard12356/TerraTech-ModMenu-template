#define UNITY_EDITOR
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UISliderControlRadialMenu : UIHUDElement
{
	public enum LeftOptionIconType
	{
		Reset,
		Power
	}

	[Serializable]
	private class LeftOptionIconGroup
	{
		public LeftOptionIconType m_IconType;

		public Sprite m_IconSprite;
	}

	[SerializeField]
	private RadialMenu m_RadialMenu;

	[SerializeField]
	private AnimationCurve m_SliderGamepadSensitivityCurve = AnimationCurve.Linear(0f, 0f, 1f, 0.05f);

	[SerializeField]
	private float m_SliderGamepadDelay = 0.2f;

	[SerializeField]
	private GameObject m_RightPanel;

	[SerializeField]
	private Text m_SettingValueTextTitle;

	[SerializeField]
	private Text m_SettingValueTextDisplay;

	[SerializeField]
	private Image m_LeftOptionIconDisplay;

	[SerializeField]
	private LeftOptionIconGroup[] m_LeftOptionIconGroups;

	[SerializeField]
	private UIRadialMenuOptionWithWarning m_ResetOptionWithWarning;

	[SerializeField]
	private UIRadialMenuOptionWithWarning m_SliderOptionWithWarning;

	private UIRadialMenuSlider m_Slider;

	private TankBlock m_TargetBlock;

	private IHUDRadialSliderControlledModule m_TargetModule;

	private IRadialInputController m_RadialController;

	private float m_SliderLastGamepadUpdate;

	private const int CHOICE_SLIDER = 0;

	private const int CHOICE_TOGGLE = 1;

	private Tank TargetTank
	{
		get
		{
			if (!(m_TargetBlock == null))
			{
				return m_TargetBlock.tank;
			}
			return null;
		}
	}

	private bool IsMenuAvailableForTech
	{
		get
		{
			bool num = !Singleton.Manager<ManGameMode>.inst.LockPlayerControls && Singleton.playerTank != null;
			bool flag = TargetTank != null && (!m_TargetBlock.ContextMenuForPlayerTechOnly || TargetTank == Singleton.playerTank) && TargetTank.Team == Singleton.Manager<ManPlayer>.inst.PlayerTeam && TargetTank.visible.IsInteractible;
			bool flag2 = Singleton.Manager<ManPointer>.inst.DraggingItem != null;
			bool isInteractionBlocked = Singleton.Manager<ManPointer>.inst.IsInteractionBlocked;
			if (num && flag && !isInteractionBlocked)
			{
				return !flag2;
			}
			return false;
		}
	}

	private float SliderValue
	{
		get
		{
			return m_Slider.value;
		}
		set
		{
			bool num = m_Slider.value != Mathf.Clamp01(value);
			m_Slider.value = Mathf.Clamp01(value);
			if (num)
			{
				OnSliderValueChanged(m_Slider.value);
			}
		}
	}

	public override void Show(object context)
	{
		OpenMenuEventData openMenuEventData = (OpenMenuEventData)context;
		m_TargetBlock = openMenuEventData.m_TargetTankBlock;
		if (!IsMenuAvailableForTech || !openMenuEventData.m_AllowRadialMenu)
		{
			return;
		}
		m_TargetModule = m_TargetBlock.GetComponent<IHUDRadialSliderControlledModule>();
		LeftOptionIconGroup leftOptionIconGroup = m_LeftOptionIconGroups.FirstOrDefault((LeftOptionIconGroup r) => r.m_IconType == m_TargetModule.LeftOptionIconType);
		d.Assert(leftOptionIconGroup != null, "No icon group setup for icon type '" + m_TargetModule.LeftOptionIconType.ToString() + "' on " + base.gameObject.name + "! This needs setting up!");
		m_LeftOptionIconDisplay.sprite = leftOptionIconGroup.m_IconSprite;
		m_SettingValueTextTitle.text = m_TargetModule.SliderTitle.Value;
		m_ResetOptionWithWarning.TooltipString = m_TargetModule.ResetTooltip;
		m_ResetOptionWithWarning.TooltipWarningString = m_TargetModule.ResetWarningTooltip;
		m_SliderOptionWithWarning.TooltipString = m_TargetModule.SliderTooltip;
		m_SliderOptionWithWarning.TooltipWarningString = m_TargetModule.SliderWarningTooltip;
		if (m_TargetModule == null)
		{
			d.LogError("Attempted to access target module but module was not found... <b>FIX THIS!</b>\nDoes the relevant module not have an implementation of the required interface?");
		}
		m_RadialController = Singleton.Manager<ManInput>.inst.GetRadialInputController(openMenuEventData.m_RadialInputController);
		m_Slider.onValueChanged.AddListener(OnSliderValueChanged);
		SliderValue = m_TargetModule.AdjustableValueFulfillment01;
		m_SettingValueTextDisplay.text = m_TargetModule.GetAdjustableValueDisplayText(m_TargetModule.GetValueFromRangeFulfillment(SliderValue), includeUnit: true, out var _);
		SetSliderPanelActive(state: false);
		base.Show(context);
		m_RadialMenu.Show(openMenuEventData.m_RadialInputController, TargetTank != Singleton.playerTank);
		for (int num = 0; num < m_RadialMenu.GetOptionsCount(); num++)
		{
			UIRadialMenuOptionWithWarning uIRadialMenuOptionWithWarning = m_RadialMenu.GetOption(num) as UIRadialMenuOptionWithWarning;
			if (num == 1 && m_TargetModule.BlockControlCategory != ModuleControlCategory.NotImplemented)
			{
				bool flag = m_TargetBlock.tank.BlockStateController.IsCategoryCircuitControlled(m_TargetModule.BlockControlCategory);
				uIRadialMenuOptionWithWarning.SetIsAllowed(!flag);
			}
			else
			{
				uIRadialMenuOptionWithWarning.SetIsAllowed(isAllowed: true);
			}
		}
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		m_TargetModule.AdjustableValueFulfillment01 = SliderValue;
		m_Slider.onValueChanged.RemoveListener(OnSliderValueChanged);
		m_Slider.SetIsHighlighted(isHighlighted: false);
		m_RadialMenu.Hide();
		m_RadialController = null;
	}

	private void OnOptionSelected(int option)
	{
		bool flag = false;
		switch (option)
		{
		case 0:
			if (!m_Slider.gameObject.activeInHierarchy)
			{
				SetSliderPanelActive(state: true);
				m_Slider.SetIsHighlighted(isHighlighted: true);
				flag = true;
			}
			else
			{
				HideSelf();
			}
			break;
		case 1:
			if (m_Slider.gameObject.activeInHierarchy)
			{
				m_Slider.SetIsHighlighted(isHighlighted: false);
				SetSliderPanelActive(state: false);
				flag = true;
			}
			else
			{
				m_TargetModule.SetAdjustableValueDefault();
				SliderValue = m_TargetModule.AdjustableValueFulfillment01;
				HideSelf();
			}
			break;
		}
		if (flag)
		{
			if (m_RadialMenu.IsModal())
			{
				m_RadialMenu.SetModal(modal: false);
			}
			m_RadialMenu.SetModal(modal: true, option, option == 1);
		}
	}

	private void SetSliderPanelActive(bool state)
	{
		m_Slider.gameObject.SetActive(state);
		m_RightPanel?.SetActive(state);
	}

	private void OnOptionHovered(int option)
	{
	}

	private void HandleGamepadSliderInput()
	{
		if (!(Time.time < m_SliderLastGamepadUpdate + m_SliderGamepadDelay))
		{
			float axis = Singleton.Manager<ManInput>.inst.GetAxis(19);
			if (!Mathf.Approximately(axis, 0f))
			{
				SliderValue += m_SliderGamepadSensitivityCurve.Evaluate(Mathf.Abs(axis)) * Mathf.Sign(axis);
				m_SliderLastGamepadUpdate = Time.time;
			}
		}
	}

	private void OnSliderValueChanged(float value)
	{
		if (m_TargetModule.IsAdjustableValueUpdatedRealtime)
		{
			m_TargetModule.AdjustableValueFulfillment01 = value;
		}
		m_SettingValueTextDisplay.text = m_TargetModule.GetAdjustableValueDisplayText(m_TargetModule.GetValueFromRangeFulfillment(value), includeUnit: true, out var _);
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
		if (m_TargetModule != null && m_RadialMenu.IsModal())
		{
			if (m_RadialController.DidCancel() || (m_RadialController.DidSelect() && m_RadialController.IsGamePad()))
			{
				m_Slider.SetIsHighlighted(isHighlighted: false);
				m_RadialMenu.SetModal(modal: false);
				SetSliderPanelActive(state: false);
			}
			else
			{
				HandleGamepadSliderInput();
			}
		}
	}
}
