#define UNITY_EDITOR
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleTechController : Module
{
	[SerializeField]
	public bool m_PlayerInput;

	[SerializeField]
	public bool m_IsAdditive;

	private ICustomTechController m_CustomController;

	public bool HandlesPlayerInput => m_PlayerInput;

	public bool IsAdditive => m_IsAdditive;

	public int GetPriority()
	{
		if (IsAdditive)
		{
			return 0;
		}
		if (m_CustomController != null)
		{
			return m_CustomController.DefaultPriority;
		}
		if (!m_PlayerInput)
		{
			return 1;
		}
		return 2;
	}

	public bool ExecuteControl(bool additive)
	{
		if (base.block.tank.IsNull())
		{
			return false;
		}
		if (additive != IsAdditive)
		{
			return false;
		}
		if (m_CustomController != null)
		{
			return m_CustomController.ExecuteControl(additive);
		}
		d.Assert(!additive, "Player control and AI input are not setup to allow for additive control at this time!");
		if ((object)base.block.tank == Singleton.playerTank || base.block.tank.control.HandlesPlayerInput)
		{
			if (m_PlayerInput)
			{
				return PlayerInputControl();
			}
			return AIControl();
		}
		if (!m_PlayerInput || !base.block.tank.ControllableByAnyPlayer)
		{
			if (Singleton.Manager<DebugUtil>.inst.EnemiesPaused && !ManSpawn.IsPlayerTeam(base.block.tank.Team))
			{
				return false;
			}
			return AIControl();
		}
		return false;
	}

	public bool DoesAIControl()
	{
		if (base.block.tank.IsNull())
		{
			return false;
		}
		if (base.block.tank == Singleton.playerTank || base.block.tank.control.HandlesPlayerInput)
		{
			if (m_PlayerInput)
			{
				return false;
			}
			return true;
		}
		if (!m_PlayerInput || !base.block.tank.ControllableByAnyPlayer)
		{
			return true;
		}
		return false;
	}

	private bool PlayerInputControl()
	{
		if (!base.block.tank.PlayerFocused)
		{
			return false;
		}
		if (!Singleton.Manager<ManGameMode>.inst.LockPlayerControls && !base.block.damage.AboutToDie)
		{
			base.block.tank.control.PlayerInput();
		}
		return true;
	}

	private bool AIControl()
	{
		return base.block.tank.AI.ControlTech();
	}

	private void OnAttached()
	{
		if ((bool)base.block.tank && (bool)base.block.tank.control)
		{
			base.block.tank.control.AddController(this);
		}
	}

	private void OnDetaching()
	{
		if ((bool)base.block.tank && (bool)base.block.tank.control)
		{
			base.block.tank.control.RemoveController(this);
		}
	}

	private void OnPool()
	{
		m_CustomController = GetComponent<ICustomTechController>();
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
	}

	private void OnValidate()
	{
		d.AssertFormat(!m_IsAdditive || GetComponent<ICustomTechController>() != null, base.gameObject, "Player control and AI input are not setup to allow for additive control at this time! On '{0}'", base.name);
	}
}
