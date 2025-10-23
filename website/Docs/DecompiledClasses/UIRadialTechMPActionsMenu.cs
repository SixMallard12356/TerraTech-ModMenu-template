#define UNITY_EDITOR
using System;
using UnityEngine;

public class UIRadialTechMPActionsMenu : UIHUDElement
{
	[Serializable]
	public class ControlOption
	{
		public PlayerCommands command;

		public UIRadialMenuOptionWithWarning UIComponent;
	}

	public enum PlayerCommands
	{
		SelfDestruct,
		ControlSchemes,
		Killstreak
	}

	[SerializeField]
	private RadialMenu m_RadialMenu;

	private Bitfield<PlayerCommands> m_EnabledCommands = new Bitfield<PlayerCommands>();

	private UIMPKillStreakClaimRewardHUD m_KillstreakUI;

	private UIRadialSubMenuControlSchemes m_SchemaSubMenu;

	private const int kSchemaSubMenuIndex = 0;

	public override void Show(object context)
	{
		OpenMenuEventData openMenuEventData = (OpenMenuEventData)context;
		if (!Singleton.Manager<ManInput>.inst.GetRadialInputController(openMenuEventData.m_RadialInputController).IsGamePad())
		{
			m_RadialMenu.SetDisableInputOnSubmenuOpen(disable: false);
		}
		if (!openMenuEventData.m_AllowRadialMenu || !(Singleton.Manager<ManNetwork>.inst.MyPlayer != null))
		{
			return;
		}
		m_KillstreakUI = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MPKillStreakClaimReward) as UIMPKillStreakClaimRewardHUD;
		m_EnabledCommands.Clear();
		if (m_KillstreakUI != null && m_KillstreakUI.RewardAvailable)
		{
			m_EnabledCommands.Add(2);
		}
		m_EnabledCommands.Add(1);
		m_EnabledCommands.Add(0);
		if (!m_EnabledCommands.AnySet)
		{
			return;
		}
		m_SchemaSubMenu.SetUpSchemaMenu();
		base.Show(context);
		m_RadialMenu.Show(openMenuEventData.m_RadialInputController, freezeCamera: false);
		for (int i = 0; i < m_RadialMenu.GetOptionsCount(); i++)
		{
			UIRadialMenuOptionWithWarning uIRadialMenuOptionWithWarning = (UIRadialMenuOptionWithWarning)m_RadialMenu.GetOption(i);
			bool flag = m_EnabledCommands.Contains(i);
			uIRadialMenuOptionWithWarning.SetIsAllowed(flag);
			if (i == 2 && m_KillstreakUI != null)
			{
				string text = ((!flag && !m_KillstreakUI.IsVisible) ? Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 84) : m_KillstreakUI.TooltipText);
				uIRadialMenuOptionWithWarning.TooltipComponent.SetText(text);
			}
		}
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		m_RadialMenu.Hide();
		m_SchemaSubMenu.CleanSchemaMenu();
	}

	private void OnOptionSelected(int option)
	{
		switch (option)
		{
		case 2:
			if (m_KillstreakUI != null)
			{
				m_KillstreakUI.ClaimReward();
				HideSelf();
			}
			break;
		case 1:
			Singleton.Manager<ManOverlay>.inst.RemoveToolTip();
			m_RadialMenu.OpenSubmenu(0, OnSchemasSubmenu);
			break;
		case 0:
			if (Singleton.Manager<ManNetwork>.inst.MyPlayer != null)
			{
				Singleton.Manager<ManNetwork>.inst.MyPlayer.SelfDestructFromUI();
				HideSelf();
			}
			break;
		default:
			d.LogError("UIRadialTechControlMenu.OnOptionSelected: No action found for command: " + (PlayerCommands)option);
			break;
		}
	}

	private void OnSchemasSubmenu(int index)
	{
		m_SchemaSubMenu.OnSubmenuItemSelected(index);
		HideSelf();
	}

	private void OnOptionHovered(int option)
	{
		if (!m_RadialMenu.IsUsingGamePad)
		{
			if (option == 1 && m_EnabledCommands.Contains(option))
			{
				m_RadialMenu.OpenSubmenu(0, OnSchemasSubmenu);
			}
			else
			{
				m_RadialMenu.CloseSubmenu(0);
			}
		}
	}

	private bool IsMenuAvailable()
	{
		bool num = Singleton.Manager<ManNetwork>.inst.IsMultiplayer();
		bool flag = !Singleton.Manager<ManGameMode>.inst.LockPlayerControls && Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech != null;
		bool isInteractionBlocked = Singleton.Manager<ManPointer>.inst.IsInteractionBlocked;
		bool flag2 = Singleton.Manager<ManPointer>.inst.DraggingItem != null;
		if (num && flag && !isInteractionBlocked)
		{
			return !flag2;
		}
		return false;
	}

	private void OnPool()
	{
		m_SchemaSubMenu = m_RadialMenu.GetSubmenu(0).GetComponent<UIRadialSubMenuControlSchemes>();
		d.Assert(m_SchemaSubMenu != null, "Failed to find UIRadialSubMenuControlSchemes component on control scheme sub menu");
	}

	private void Awake()
	{
		m_RadialMenu.OnOptionSelected.Subscribe(OnOptionSelected);
		m_RadialMenu.OnOptionHovered.Subscribe(OnOptionHovered);
		m_RadialMenu.OnClose.Subscribe(base.HideSelf);
	}

	private void Update()
	{
		if (Singleton.playerTank == null)
		{
			HideSelf();
		}
	}
}
