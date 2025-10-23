using UnityEngine;

public class UIRadialTechHeartbeatMenu : UIHUDElement
{
	[SerializeField]
	private RadialMenu m_RadialMenu;

	private Tank m_TargetTank;

	private static readonly TechHolders.HeartbeatSpeed[] UIOptionToSpeed = new TechHolders.HeartbeatSpeed[4]
	{
		TechHolders.HeartbeatSpeed.Paused,
		TechHolders.HeartbeatSpeed.Fast,
		TechHolders.HeartbeatSpeed.Normal,
		TechHolders.HeartbeatSpeed.Slow
	};

	public override void Show(object context)
	{
		OpenMenuEventData openMenuEventData = (OpenMenuEventData)context;
		m_TargetTank = openMenuEventData.m_TargetTankBlock.tank;
		base.Show(context);
		m_RadialMenu.Show(openMenuEventData.m_RadialInputController, m_TargetTank != Singleton.playerTank);
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		m_RadialMenu.Hide();
		m_TargetTank = null;
	}

	private void OnOptionSelected(int nOption)
	{
		if (nOption >= 0 && nOption < UIOptionToSpeed.Length)
		{
			TechHolders.HeartbeatSpeed speed = UIOptionToSpeed[nOption];
			m_TargetTank.Holders.RequestSetHeartbeatSpeed(speed);
		}
		HideSelf();
	}

	private void OnSpawn()
	{
		AddElementToGroup(ManHUD.HUDGroup.GamepadCursorHidingElements);
		AddElementToGroup(ManHUD.HUDGroup.ContextMenuBlocking);
	}

	private void Awake()
	{
		m_RadialMenu.OnOptionSelected.Subscribe(OnOptionSelected);
		m_RadialMenu.OnClose.Subscribe(base.HideSelf);
	}
}
