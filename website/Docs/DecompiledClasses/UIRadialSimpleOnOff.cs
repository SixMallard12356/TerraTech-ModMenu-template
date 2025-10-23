#define UNITY_EDITOR
using UnityEngine;

public class UIRadialSimpleOnOff : UIHUDElement
{
	public interface Implementer
	{
		bool CanOpenMenuOnBlock { get; }

		void OnHUDStateChosen(bool state);
	}

	[SerializeField]
	private RadialMenu m_RadialMenu;

	private TankBlock m_TargetBlock;

	private Implementer m_Implementor;

	public override void Show(object context)
	{
		OpenMenuEventData openMenuEventData = (OpenMenuEventData)context;
		m_TargetBlock = openMenuEventData.m_TargetTankBlock;
		m_Implementor = m_TargetBlock.GetComponent<Implementer>();
		if (IsMenuAvailableForTech(m_TargetBlock.tank) && openMenuEventData.m_AllowRadialMenu && m_Implementor != null)
		{
			base.Show(context);
			m_RadialMenu.Show(openMenuEventData.m_RadialInputController, m_TargetBlock.tank != Singleton.playerTank);
		}
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		m_RadialMenu.Hide();
		m_TargetBlock = null;
	}

	private void OnOptionSelected(int option)
	{
		m_Implementor.OnHUDStateChosen(option > 0);
		HideSelf();
	}

	private bool IsMenuAvailableForTech(Tank targetTank)
	{
		bool num = !Singleton.Manager<ManGameMode>.inst.LockPlayerControls && Singleton.playerTank != null;
		bool flag = m_Implementor != null && m_TargetBlock != null && m_TargetBlock.tank != null && ManSpawn.IsPlayerTeam(m_TargetBlock.tank.Team) && m_TargetBlock.IsInteractible;
		bool flag2 = !base.IsVisible;
		bool flag3 = Singleton.Manager<ManPointer>.inst.DraggingItem != null;
		bool flag4 = Singleton.Manager<ManPointer>.inst.IsInteractionBlocked || Singleton.Manager<ManHUD>.inst.IsAnyElementInGroupVisible(ManHUD.HUDGroup.ContextMenuBlocking);
		if (num && flag && flag2 && !flag4 && !flag3)
		{
			return m_Implementor.CanOpenMenuOnBlock;
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
