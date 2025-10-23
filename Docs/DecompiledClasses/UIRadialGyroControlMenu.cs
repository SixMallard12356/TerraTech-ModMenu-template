#define UNITY_EDITOR
using UnityEngine;

public class UIRadialGyroControlMenu : UIHUDElement
{
	[SerializeField]
	private RadialMenu m_RadialMenu;

	private ModuleGyro m_TargetGyro;

	public override void Show(object context)
	{
		OpenMenuEventData openMenuEventData = (OpenMenuEventData)context;
		TankBlock targetTankBlock = openMenuEventData.m_TargetTankBlock;
		if (IsMenuAvailableForTech(targetTankBlock.tank) && openMenuEventData.m_AllowRadialMenu)
		{
			m_TargetGyro = targetTankBlock.GetComponent<ModuleGyro>();
			base.Show(context);
			m_RadialMenu.Show(openMenuEventData.m_RadialInputController, targetTankBlock != Singleton.playerTank);
			for (int i = 0; i < m_RadialMenu.GetOptionsCount(); i++)
			{
				(m_RadialMenu.GetOption(i) as UIRadialMenuOptionWithWarning).SetIsAllowed(isAllowed: true);
			}
		}
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		m_RadialMenu.Hide();
		m_TargetGyro = null;
	}

	private void OnOptionSelected(int option)
	{
		m_TargetGyro.EnableResistive(option == 0);
		HideSelf();
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
		m_RadialMenu.OnOptionSelected.Subscribe(OnOptionSelected);
		m_RadialMenu.OnClose.Subscribe(base.HideSelf);
	}
}
