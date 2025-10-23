#define UNITY_EDITOR
using UnityEngine;

public class UIRadialTechControlMenu : UIHUDElement
{
	public enum PlayerCommands
	{
		RenameTech,
		SwitchToTech,
		AIIdle,
		AIGuard,
		SendToSCU,
		AIHarvest
	}

	[SerializeField]
	private RadialMenu m_RadialMenu;

	[SerializeField]
	private bool m_AllowTargetRefinement;

	[SerializeField]
	[EnumFlag]
	private PlayerCommands m_AllowedCommands = (PlayerCommands)(-1);

	private Tank m_TargetTank;

	private Bitfield<PlayerCommands> m_EnabledCommands = new Bitfield<PlayerCommands>();

	private UIRadialMenuOptionDelayTimer m_SendToSCUOption;

	public override void Show(object context)
	{
		OpenMenuEventData openMenuEventData = (OpenMenuEventData)context;
		TankBlock targetTankBlock = openMenuEventData.m_TargetTankBlock;
		if (targetTankBlock.tank.IsNotNull())
		{
			if (!IsMenuAvailableForTech(targetTankBlock.tank) || !openMenuEventData.m_AllowRadialMenu)
			{
				return;
			}
			m_TargetTank = targetTankBlock.tank;
			m_EnabledCommands.Clear();
			if (IsControlAllowed(PlayerCommands.SwitchToTech) && m_TargetTank.ControllableByLocalPlayer)
			{
				m_EnabledCommands.Add(1);
			}
			if (IsControlAllowed(PlayerCommands.RenameTech))
			{
				m_EnabledCommands.Add(0);
			}
			if ((bool)m_TargetTank.AI && m_TargetTank.AI.HasAIModules)
			{
				bool flag = false;
				if (IsControlAllowed(PlayerCommands.AIHarvest) && m_TargetTank.AI.AvailableAITypes.Contains(TechAI.AITypes.Harvest))
				{
					m_EnabledCommands.Add(5);
					flag = true;
				}
				if (IsControlAllowed(PlayerCommands.AIGuard) && m_TargetTank.AI.AvailableAITypes.Contains(TechAI.AITypes.Escort))
				{
					m_EnabledCommands.Add(3);
					flag = true;
				}
				if (IsControlAllowed(PlayerCommands.AIIdle) && flag)
				{
					d.Assert(m_TargetTank.AI.AvailableAITypes.Contains(TechAI.AITypes.Idle), m_TargetTank.name + " AI does not provide Idle behaviour!?");
					m_EnabledCommands.Add(2);
				}
			}
			if (IsControlAllowed(PlayerCommands.SendToSCU) && Singleton.Manager<ManPlayer>.inst.PaletteUnlocked)
			{
				m_EnabledCommands.Add(4);
			}
			if (!m_EnabledCommands.AnySet)
			{
				return;
			}
			base.Show(context);
			m_RadialMenu.Show(openMenuEventData.m_RadialInputController, freezeCamera: true);
			for (int i = 0; i < m_RadialMenu.GetOptionsCount(); i++)
			{
				UIRadialMenuOptionWithWarning uIRadialMenuOptionWithWarning = m_RadialMenu.GetOption(i) as UIRadialMenuOptionWithWarning;
				bool isAllowed = m_EnabledCommands.Contains(i);
				uIRadialMenuOptionWithWarning.SetIsAllowed(isAllowed);
				if (i == 4)
				{
					m_SendToSCUOption = uIRadialMenuOptionWithWarning as UIRadialMenuOptionDelayTimer;
				}
			}
		}
		else
		{
			d.LogError("Tried to open UIRadialTechControlMenu on a block that was not part of a tech");
		}
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		m_RadialMenu.Hide();
		m_TargetTank = null;
	}

	private void OnOptionSelected(int option)
	{
		if (m_EnabledCommands.Contains(option))
		{
			switch (option)
			{
			case 1:
				Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.SwitchTech);
				Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(m_TargetTank);
				m_TargetTank.control.targetType = ObjectTypes.Vehicle;
				break;
			case 4:
				if (m_TargetTank != null && ((UIRadialMenuOptionDelayTimer)m_RadialMenu.GetOption(4)).IsSelected)
				{
					TrackedVisible trackedVisible2 = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(m_TargetTank.visible.ID);
					Singleton.Manager<ManPurchases>.inst.StoreTechToInventory(trackedVisible2, supportUndo: true);
				}
				break;
			case 0:
			{
				if (!(m_TargetTank != null))
				{
					break;
				}
				UIScreenRenameTech uIScreenRenameTech = Singleton.Manager<ManUI>.inst.GetScreen(m_TargetTank.RadarMarker.RadarMarkerConfig.IsUsed ? ManUI.ScreenType.RenameTech_MarkerBlock : ManUI.ScreenType.RenameTech) as UIScreenRenameTech;
				d.Assert(uIScreenRenameTech != null, "Cannot find Rename Tech screen");
				if ((bool)uIScreenRenameTech && m_TargetTank.IsNotNull())
				{
					TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(m_TargetTank.visible.ID);
					if (trackedVisible != null)
					{
						uIScreenRenameTech.SetSelectedTech(trackedVisible);
						Singleton.Manager<ManUI>.inst.PushScreen(uIScreenRenameTech, ManUI.PauseType.Pause);
					}
				}
				break;
			}
			case 2:
			case 3:
			case 5:
				OnAIOptionSelected((PlayerCommands)option);
				break;
			default:
				d.LogError("UIRadialTechControlMenu.OnOptionSelected: No action found for command: " + (PlayerCommands)option);
				break;
			}
			HideSelf();
		}
		else
		{
			HideSelf();
		}
	}

	private void OnAIOptionSelected(PlayerCommands command)
	{
		AITreeType.AITypes aITypes = AITreeType.AITypes.Idle;
		ManSFX.UISfxType sfxType = ManSFX.UISfxType.AIIdle;
		switch (command)
		{
		case PlayerCommands.AIIdle:
			aITypes = AITreeType.AITypes.Idle;
			sfxType = ManSFX.UISfxType.AIIdle;
			break;
		case PlayerCommands.AIGuard:
			if (m_TargetTank.IsAnchored)
			{
				aITypes = AITreeType.AITypes.Guard;
				sfxType = ManSFX.UISfxType.AIGuard;
			}
			else
			{
				aITypes = (m_AllowTargetRefinement ? AITreeType.AITypes.Escort : AITreeType.AITypes.Escort);
				sfxType = ManSFX.UISfxType.AIFollow;
			}
			break;
		case PlayerCommands.AIHarvest:
			aITypes = (m_AllowTargetRefinement ? AITreeType.AITypes.Escort : AITreeType.AITypes.Harvest);
			sfxType = ManSFX.UISfxType.AIFollow;
			break;
		default:
			d.LogError("Unsupported AI type being set from UIRadialTechControlMenu!");
			break;
		}
		Singleton.Manager<ManSFX>.inst.PlayUISFX(sfxType);
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && m_TargetTank.netTech.IsNotNull())
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.SetAIMode, new SetAIModeMessage
			{
				m_AIAction = aITypes
			}, m_TargetTank.netTech.netId);
		}
		else
		{
			m_TargetTank.AI.SetBehaviorType(aITypes);
		}
		if (m_AllowTargetRefinement && (command == PlayerCommands.AIGuard || command == PlayerCommands.AIHarvest))
		{
			UITechAITargetSelect.AITargetSelectContext aITargetSelectContext = new UITechAITargetSelect.AITargetSelectContext
			{
				targetTank = m_TargetTank,
				selectedCommand = command
			};
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechControlChoiceSetAITarget, aITargetSelectContext);
		}
	}

	private bool IsMenuAvailableForTech(Tank targetTank)
	{
		bool num = !Singleton.Manager<ManGameMode>.inst.LockPlayerControls && Singleton.playerTank != null;
		bool flag = Singleton.Manager<ManNetTechs>.inst.CanSwitchToTech(targetTank);
		bool flag2 = m_AllowTargetRefinement && Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.TechControlChoiceSetAITarget);
		bool flag3 = Singleton.Manager<ManPointer>.inst.DraggingItem != null;
		bool isInteractionBlocked = Singleton.Manager<ManPointer>.inst.IsInteractionBlocked;
		if (num && flag && !flag2 && !isInteractionBlocked)
		{
			return !flag3;
		}
		return false;
	}

	private bool IsControlAllowed(PlayerCommands command)
	{
		bool result = ((uint)m_AllowedCommands & (uint)(1 << (int)command)) != 0;
		if (!SKU.AllowTextInput && command == PlayerCommands.RenameTech)
		{
			result = false;
		}
		if (command == PlayerCommands.RenameTech && Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && SKU.OverrideMpTechNames)
		{
			result = m_TargetTank.RadarMarker.RadarMarkerConfig.IsUsed;
		}
		return result;
	}

	private void OnSpawn()
	{
		AddElementToGroup(ManHUD.HUDGroup.GamepadCursorHidingElements);
		AddElementToGroup(ManHUD.HUDGroup.ContextMenuBlocking);
	}

	private void Awake()
	{
		d.Assert(m_RadialMenu != null);
		m_RadialMenu.OnOptionSelected.Subscribe(OnOptionSelected);
		m_RadialMenu.OnClose.Subscribe(base.HideSelf);
	}

	private void Update()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (m_TargetTank.IsNull() || !m_TargetTank.gameObject.activeSelf)
		{
			HideSelf();
		}
		else
		{
			if (!(m_SendToSCUOption != null))
			{
				return;
			}
			bool flag = m_EnabledCommands.Contains(4);
			bool flag2 = true;
			bool flag3 = !Singleton.Manager<ManUI>.inst.IsUILocked(ManUI.LockTimerTypes.SendToSCU) && m_TargetTank.visible.CanBeSentToSCU;
			UIRadialMenuOptionDelayTimer.TooltipReason tooltip = UIRadialMenuOptionDelayTimer.TooltipReason.Default;
			if (flag)
			{
				flag2 = Singleton.Manager<ManTechs>.inst.CanEnemyProximitySensitiveActionBeExecuted(m_TargetTank.boundsCentreWorld, Globals.inst.m_TechStoreThreatDistance);
				if (!flag2)
				{
					tooltip = UIRadialMenuOptionDelayTimer.TooltipReason.EnemiesNearby;
				}
			}
			else
			{
				tooltip = UIRadialMenuOptionDelayTimer.TooltipReason.NoInventory;
			}
			UIRadialMenuOptionDelayTimer sendToSCUOption = m_SendToSCUOption;
			sendToSCUOption.SetTooltip(tooltip);
			sendToSCUOption.SetIsAllowed(flag && flag2 && flag3);
		}
	}
}
