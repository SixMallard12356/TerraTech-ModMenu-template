#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UIPowerToggleBlockMenu : UIHUDElement
{
	[SerializeField]
	private float m_DelayTime = 1f;

	[SerializeField]
	private Image m_TimeoutBar;

	[SerializeField]
	private Image m_BackgroundImage;

	[SerializeField]
	private Gradient m_DefaultToggleGradient;

	private RadialMenu m_RadialMenu;

	private float m_Timer;

	private bool m_DesiredPowerState;

	private Module m_TargetModule;

	private IHUDPowerToggleControlledModule m_Target;

	private Gradient ToggleGradient
	{
		get
		{
			if (m_Target.ToggleGradientOverride != null)
			{
				return m_Target.ToggleGradientOverride;
			}
			return m_DefaultToggleGradient;
		}
	}

	private float Timer
	{
		get
		{
			return m_Timer;
		}
		set
		{
			m_Timer = Mathf.Clamp(value, 0f, m_DelayTime);
			float num = ((m_DelayTime == 0f) ? 1f : (1f - m_Timer / m_DelayTime));
			m_TimeoutBar.fillAmount = 1f - num;
			m_BackgroundImage.color = ToggleGradient.Evaluate(m_DesiredPowerState ? num : (1f - num));
		}
	}

	public override void Show(object context)
	{
		OpenMenuEventData openMenuEventData = (OpenMenuEventData)context;
		IHUDPowerToggleControlledModule component = openMenuEventData.m_TargetTankBlock.GetComponent<IHUDPowerToggleControlledModule>();
		d.Assert(component != null, "UIPowerToggleBlockMenu.Show being called, but target object data is invalid (block does not contain a module that implements IHUDPowerToggleControlledModule!");
		d.Assert(component is Module, "UIPowerToggleBlockMenu.Show being called, but target object that implements IHUDPowerToggleControlledModule is not a valid Module!");
		Module targetModule = component as Module;
		if (IsMenuAvailableForModule(targetModule, component))
		{
			m_Target = component;
			m_TargetModule = targetModule;
			m_DesiredPowerState = !m_Target.PowerControlSetting;
			Timer = m_DelayTime;
			base.Show(context);
			m_RadialMenu.Show(openMenuEventData.m_RadialInputController, m_TargetModule.block.tank != Singleton.playerTank);
		}
	}

	public override void Hide(object context)
	{
		if (base.IsVisible)
		{
			m_RadialMenu.Hide();
			m_TargetModule = null;
		}
		base.Hide(context);
	}

	private void OnOptionSelected(int option)
	{
		if (Timer == 0f)
		{
			m_Target.PowerControlSetting = m_DesiredPowerState;
			HideSelf();
		}
		else if (!m_RadialMenu.IsUsingGamePad)
		{
			HideSelf();
		}
	}

	private bool IsMenuAvailableForModule(Module targetModule, IHUDPowerToggleControlledModule target)
	{
		bool num = !Singleton.Manager<ManGameMode>.inst.LockPlayerControls && Singleton.playerTank != null;
		bool flag = targetModule != null && targetModule.block != null && targetModule.block.tank != null && ManSpawn.IsPlayerTeam(targetModule.block.tank.Team) && targetModule.block.IsInteractible;
		bool flag2 = !base.IsVisible;
		bool flag3 = Singleton.Manager<ManPointer>.inst.DraggingItem != null;
		bool flag4 = Singleton.Manager<ManPointer>.inst.IsInteractionBlocked || Singleton.Manager<ManHUD>.inst.IsAnyElementInGroupVisible(ManHUD.HUDGroup.ContextMenuBlocking);
		if (num && flag && flag2 && !flag4 && !flag3)
		{
			return target.CanOpenMenuOnBlock;
		}
		return false;
	}

	private void Update()
	{
		if (!base.IsShowing)
		{
			return;
		}
		if (Timer == 0f)
		{
			if (m_Target.AutoCloseMenuOnComplete && m_Target.PowerControlSetting == m_DesiredPowerState)
			{
				HideSelf();
			}
		}
		else
		{
			Timer -= Time.deltaTime;
		}
	}

	private void OnPool()
	{
		m_RadialMenu = GetComponent<RadialMenu>();
		m_RadialMenu.OnOptionSelected.Subscribe(OnOptionSelected);
		m_RadialMenu.OnClose.Subscribe(base.HideSelf);
	}

	private void OnSpawn()
	{
		AddElementToGroup(ManHUD.HUDGroup.GamepadCursorHidingElements);
		AddElementToGroup(ManHUD.HUDGroup.ContextMenuBlocking);
	}
}
