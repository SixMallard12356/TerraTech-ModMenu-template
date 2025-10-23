#define UNITY_EDITOR
using System;
using UnityEngine;

public class UIRadialTechAndBlockActionsMenu : UIHUDElement
{
	public enum PlayerCommands
	{
		Rename,
		BlockActions,
		Schemas,
		ToggleAnchor,
		Snapshot,
		TechActions
	}

	[SerializeField]
	private RadialMenu m_RadialMenu;

	[SerializeField]
	private UIAnchorPlayerTechButton m_AnchorButton;

	[SerializeField]
	private QueryableSelectableWithTooltip m_LightsSelectable;

	[SerializeField]
	private QueryableSelectableWithTooltip m_DetonateSelectable;

	[Header("Control schemes")]
	[SerializeField]
	private SchemaSubmenuListItem m_SchemaListElementPrefab;

	private Bitfield<PlayerCommands> m_EnabledCommands = new Bitfield<PlayerCommands>();

	private Vector3 m_PlayerLocationAtMenuOpen;

	private IRadialInputController m_Controller;

	private UIRadialSubMenuControlSchemes m_SchemaSubMenu;

	private const int kBlockSubMenuIndex = 0;

	private const int kSchemaSubMenuIndex = 1;

	public override void Show(object context)
	{
		OpenMenuEventData openMenuEventData = (OpenMenuEventData)context;
		m_Controller = Singleton.Manager<ManInput>.inst.GetRadialInputController(openMenuEventData.m_RadialInputController);
		if (!m_Controller.IsGamePad())
		{
			m_RadialMenu.SetDisableInputOnSubmenuOpen(disable: false);
		}
		if (!openMenuEventData.m_AllowRadialMenu || !(Singleton.playerTank != null))
		{
			return;
		}
		m_EnabledCommands.Clear();
		m_EnabledCommands.Add(0);
		m_EnabledCommands.Add(2);
		if (!Singleton.Manager<ManBlockLimiter>.inst.LimiterActive || Singleton.Manager<ManBlockLimiter>.inst.GetTrackedTechCost(Singleton.playerTank, includeHeldItems: false) <= Singleton.Manager<ManBlockLimiter>.inst.MaximumUsage)
		{
			m_EnabledCommands.Add(4);
		}
		if (Singleton.playerTank.Anchors.NumPossibleAnchors > 0)
		{
			m_EnabledCommands.Add(3);
		}
		m_DetonateSelectable.interactable = Singleton.playerTank.control.HasAnyExplosiveBolts;
		m_LightsSelectable.interactable = false;
		if (m_DetonateSelectable.interactable || m_LightsSelectable.interactable)
		{
			m_EnabledCommands.Add(1);
		}
		if (Singleton.Manager<ManGameMode>.inst.CanPlayerSwapOrPlaceTech())
		{
			m_EnabledCommands.Add(5);
		}
		if (m_EnabledCommands.AnySet)
		{
			m_SchemaSubMenu.SetUpSchemaMenu();
			base.Show(context);
			m_RadialMenu.Show(openMenuEventData.m_RadialInputController, freezeCamera: false);
			for (int i = 0; i < m_RadialMenu.GetOptionsCount(); i++)
			{
				UIRadialMenuOptionWithWarning obj = (UIRadialMenuOptionWithWarning)m_RadialMenu.GetOption(i);
				bool isAllowed = m_EnabledCommands.Contains(i);
				obj.SetIsAllowed(isAllowed);
			}
			if (m_AnchorButton != null)
			{
				m_AnchorButton.UpdateButton(forceUpdate: true);
			}
			ManUI inst = Singleton.Manager<ManUI>.inst;
			inst.OnScreenChangeEvent = (Action<bool, ManUI.ScreenType>)Delegate.Combine(inst.OnScreenChangeEvent, new Action<bool, ManUI.ScreenType>(OnScreenChanged));
		}
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		m_RadialMenu.Hide();
		m_SchemaSubMenu.CleanSchemaMenu();
		ManUI inst = Singleton.Manager<ManUI>.inst;
		inst.OnScreenChangeEvent = (Action<bool, ManUI.ScreenType>)Delegate.Remove(inst.OnScreenChangeEvent, new Action<bool, ManUI.ScreenType>(OnScreenChanged));
	}

	public UIRadialMenuOption GetRadialMenuButton(PlayerCommands option)
	{
		return m_RadialMenu.GetOption((int)option);
	}

	private void OnScreenChanged(bool pushed, ManUI.ScreenType screenType)
	{
		if (screenType == ManUI.ScreenType.Pause && pushed)
		{
			HideSelf();
			ManUI inst = Singleton.Manager<ManUI>.inst;
			inst.OnScreenChangeEvent = (Action<bool, ManUI.ScreenType>)Delegate.Remove(inst.OnScreenChangeEvent, new Action<bool, ManUI.ScreenType>(OnScreenChanged));
		}
	}

	private void OnOptionSelected(int option)
	{
		if (m_EnabledCommands.Contains(option))
		{
			switch ((PlayerCommands)option)
			{
			case PlayerCommands.Rename:
			{
				UIScreenRenameTech uIScreenRenameTech = Singleton.Manager<ManUI>.inst.GetScreen((Singleton.playerTank.IsNotNull() && Singleton.playerTank.RadarMarker.RadarMarkerConfig.IsUsed) ? ManUI.ScreenType.RenameTech_MarkerBlock : ManUI.ScreenType.RenameTech) as UIScreenRenameTech;
				d.Assert(uIScreenRenameTech != null, "Cannot find Renme Tech screen");
				if ((bool)uIScreenRenameTech && Singleton.playerTank.IsNotNull())
				{
					TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(Singleton.playerTank.visible.ID);
					if (trackedVisible != null)
					{
						uIScreenRenameTech.SetSelectedTech(trackedVisible);
						Singleton.Manager<ManUI>.inst.PushScreen(uIScreenRenameTech);
						HideSelf();
					}
				}
				break;
			}
			case PlayerCommands.BlockActions:
				m_RadialMenu.OpenSubmenu(0, OnBlockActionsSubmenu);
				break;
			case PlayerCommands.ToggleAnchor:
				if (m_AnchorButton != null && m_AnchorButton.CanAnchor)
				{
					m_AnchorButton.TryToggleTechAnchor();
				}
				HideSelf();
				break;
			case PlayerCommands.Schemas:
				m_RadialMenu.OpenSubmenu(1, OnSchemasSubmenu);
				break;
			case PlayerCommands.Snapshot:
				Singleton.Manager<ManScreenshot>.inst.TakeSnapshotAndShowUI();
				HideSelf();
				break;
			case PlayerCommands.TechActions:
				if (Singleton.Manager<ManGameMode>.inst.CanPlayerSwapOrPlaceTech())
				{
					Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechLoader);
					HideSelf();
				}
				break;
			}
		}
		else
		{
			HideSelf();
		}
	}

	private void OnOptionHovered(int option)
	{
		if (!m_Controller.IsGamePad())
		{
			if (option == 1 && m_EnabledCommands.Contains(option))
			{
				m_RadialMenu.OpenSubmenu(0, OnBlockActionsSubmenu);
			}
			else
			{
				m_RadialMenu.CloseSubmenu(0);
			}
			if (option == 2 && m_EnabledCommands.Contains(option))
			{
				m_RadialMenu.OpenSubmenu(1, OnSchemasSubmenu);
			}
			else
			{
				m_RadialMenu.CloseSubmenu(1);
			}
		}
	}

	private void OnBlockActionsSubmenu(int index)
	{
		if (index == 0)
		{
			Singleton.playerTank.control.ServerDetonateExplosiveBolt();
		}
		else
		{
			_ = 1;
		}
		HideSelf();
	}

	private void OnSchemasSubmenu(int index)
	{
		m_SchemaSubMenu.OnSubmenuItemSelected(index);
		HideSelf();
	}

	private void OnPool()
	{
		m_SchemaSubMenu = m_RadialMenu.GetSubmenu(1).GetComponent<UIRadialSubMenuControlSchemes>();
		d.Assert(m_SchemaSubMenu != null, "Failed to find UIRadialSubMenuControlSchemes component on control scheme sub menu");
	}

	private void OnSpawn()
	{
		AddElementToGroup(ManHUD.HUDGroup.GamepadCursorHidingElements);
		AddElementToGroup(ManHUD.HUDGroup.ContextMenuBlocking);
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
