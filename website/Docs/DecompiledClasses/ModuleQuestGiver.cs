using UnityEngine;

public class ModuleQuestGiver : Module, ManPointer.OpenMenuEventConsumer
{
	[SerializeField]
	private bool m_CanLaunchByDefault = true;

	[SerializeField]
	private float m_MaxInteractDistance = 50f;

	public Event<Module> MouseInteractEvent;

	private bool m_CanLaunch;

	private ModuleAnimator m_Animator;

	private AnimatorBool m_ReadyBool = new AnimatorBool("Ready");

	private static ModuleQuestGiver s_ActiveQuestGiverBlock;

	public bool CanLaunch
	{
		set
		{
			m_CanLaunch = value;
		}
	}

	public bool CanOpenMenu(bool isRadial)
	{
		bool result = false;
		if (ManNetwork.IsHost && !isRadial)
		{
			result = m_CanLaunch && base.block.tank != null && base.block.tank.IsAnchored && !base.block.tank.IsEnemy(0);
		}
		return result;
	}

	public bool OnOpenMenuEvent(OpenMenuEventData openMenu)
	{
		if (openMenu.m_AllowNonRadialMenu && !base.block.tank.IsEnemy(0))
		{
			MouseInteractEvent.Send(this);
			if (m_CanLaunch && base.block.tank != null && base.block.tank.IsAnchored && (Singleton.playerTank == null || (Singleton.playerPos - base.block.tank.boundsCentreWorld).SetY(0f).sqrMagnitude < m_MaxInteractDistance * m_MaxInteractDistance) && base.block.tank.QuestGiver.ShowMissionSelectUI())
			{
				s_ActiveQuestGiverBlock = this;
				Singleton.Manager<ManHUD>.inst.OnHideHUDElementEvent.Subscribe(OnHudElementHidden);
				return true;
			}
		}
		return false;
	}

	private void HideMissionSelectUI()
	{
		base.block.tank.QuestGiver.HideMissionSelectUI();
		s_ActiveQuestGiverBlock = null;
	}

	private void OnAttached()
	{
	}

	private void OnDetaching()
	{
		if (s_ActiveQuestGiverBlock == this)
		{
			HideMissionSelectUI();
		}
	}

	private void OnHudElementHidden(UIHUDElement hiddenHudElement)
	{
		if (hiddenHudElement.HudElementType == ManHUD.HUDElementType.MissionBoard)
		{
			s_ActiveQuestGiverBlock = null;
			Singleton.Manager<ManHUD>.inst.OnHideHUDElementEvent.Unsubscribe(OnHudElementHidden);
		}
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		m_Animator = GetComponent<ModuleAnimator>();
	}

	private void OnSpawn()
	{
		m_CanLaunch = m_CanLaunchByDefault;
	}

	private void OnRecycle()
	{
		if (s_ActiveQuestGiverBlock == this)
		{
			HideMissionSelectUI();
		}
	}

	private void OnUpdate()
	{
		if ((bool)m_Animator)
		{
			bool value = m_CanLaunch && base.block.tank != null && base.block.tank.IsAnchored;
			m_Animator.Set(m_ReadyBool, value);
		}
		if (s_ActiveQuestGiverBlock == this && (Singleton.playerTank == null || (Singleton.playerPos - base.block.tank.boundsCentreWorld).SetY(0f).sqrMagnitude > m_MaxInteractDistance * m_MaxInteractDistance))
		{
			HideMissionSelectUI();
		}
	}
}
