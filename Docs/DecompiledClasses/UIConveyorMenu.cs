#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UIConveyorMenu : UIHUDElement
{
	[SerializeField]
	private float m_DelayTime = 1f;

	[SerializeField]
	private Image m_TimeoutBar;

	[SerializeField]
	private Image m_BackgroundImage;

	[SerializeField]
	private Sprite m_WaitingSprite;

	[SerializeField]
	private Sprite m_PrimedSprite;

	private RadialMenu m_RadialMenu;

	private ModuleItemConveyor m_TargetModule;

	private float m_Timer;

	public override void Show(object context)
	{
		OpenMenuEventData openMenuEventData = (OpenMenuEventData)context;
		ModuleItemConveyor component = openMenuEventData.m_TargetTankBlock.GetComponent<ModuleItemConveyor>();
		d.Assert(component != null, "UIConveyorMenu.Show being called, but target object data is invalid (block does not contain a ModuleConveyor!");
		if (IsMenuAvailableForModule(component))
		{
			m_TargetModule = component;
			m_Timer = 0f;
			m_TimeoutBar.fillAmount = 1f;
			m_BackgroundImage.sprite = m_WaitingSprite;
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
		if (m_Timer >= m_DelayTime)
		{
			m_TargetModule.RequestFlipLoopDirection();
			HideSelf();
		}
		else if (!m_RadialMenu.IsUsingGamePad)
		{
			HideSelf();
		}
	}

	private bool IsMenuAvailableForModule(ModuleItemConveyor targetModule)
	{
		bool num = !Singleton.Manager<ManGameMode>.inst.LockPlayerControls && Singleton.playerTank != null;
		bool flag = targetModule != null && targetModule.block != null && targetModule.block.tank != null && ManSpawn.IsPlayerTeam(targetModule.block.tank.Team);
		bool flag2 = !base.IsVisible;
		bool flag3 = Singleton.Manager<ManPointer>.inst.DraggingItem != null;
		bool flag4 = Singleton.Manager<ManPointer>.inst.IsInteractionBlocked || Singleton.Manager<ManHUD>.inst.IsAnyElementInGroupVisible(ManHUD.HUDGroup.ContextMenuBlocking);
		if (num && flag && flag2 && !flag4)
		{
			return !flag3;
		}
		return false;
	}

	private void Update()
	{
		if (m_Timer < m_DelayTime)
		{
			m_Timer += Time.deltaTime;
			m_Timer = Mathf.Clamp(m_Timer, 0f, m_DelayTime);
			float num = ((m_DelayTime > 0f) ? (m_Timer / m_DelayTime) : 1f);
			m_TimeoutBar.fillAmount = Mathf.Clamp01(1f - num);
			m_BackgroundImage.sprite = ((num < 1f) ? m_WaitingSprite : m_PrimedSprite);
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
