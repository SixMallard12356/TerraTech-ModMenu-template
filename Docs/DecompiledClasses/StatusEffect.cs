using System;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
	[Flags]
	public enum TargetTeamTypes
	{
		Enemies = 1,
		Neutral = 2,
		Friendlies = 4
	}

	public enum EffectTypes
	{
		Unassigned,
		Clogged_Reapplicable,
		Burn_Reapplicable
	}

	public enum StackingTypes
	{
		Unassigned,
		ReApply,
		StackEffect
	}

	public class State
	{
		public float m_TickInterval;

		protected Visible m_AffecteeVisible;

		protected Visible m_AffectorVisible;

		protected EffectTypes m_EffectType;

		protected float m_StartTime;

		private float m_NextTickCountdown;

		private bool m_HasEnded;

		public Visible Visible => m_AffecteeVisible;

		public bool HasEnded => m_HasEnded;

		public State(Visible affecteeVisible, Visible affectorVisible, EffectTypes type)
		{
			m_AffecteeVisible = affecteeVisible;
			m_AffectorVisible = affectorVisible;
			m_EffectType = type;
		}

		public virtual void Reapply()
		{
		}

		public virtual void OnStart()
		{
		}

		public void Start()
		{
			m_StartTime = Time.time;
			m_NextTickCountdown = 0f;
			m_HasEnded = false;
			m_AffecteeVisible.RecycledEvent.Subscribe(OnAffectedVisibleRecycled);
			OnStart();
		}

		public virtual void OnTick()
		{
		}

		public void Tick()
		{
			m_NextTickCountdown += m_TickInterval;
			OnTick();
		}

		public virtual void OnEnd()
		{
		}

		public void End(bool justClear = false)
		{
			m_HasEnded = true;
			if (!justClear)
			{
				OnEnd();
			}
		}

		private void OnAffectedVisibleRecycled(Visible _)
		{
			End(justClear: true);
			m_AffecteeVisible.RecycledEvent.Unsubscribe(OnAffectedVisibleRecycled);
		}

		public virtual void FixedUpdate()
		{
			if (m_HasEnded)
			{
				return;
			}
			m_NextTickCountdown -= Time.fixedDeltaTime;
			while (m_NextTickCountdown <= 0f)
			{
				Tick();
				if (m_TickInterval == 0f)
				{
					break;
				}
			}
		}

		public int GetID()
		{
			return GetID(m_AffecteeVisible, m_EffectType);
		}

		public static int GetID(Visible visible, EffectTypes effectType)
		{
			return (17 * 31 + visible.GetHashCode()) * 31 + effectType.GetHashCode();
		}
	}

	[SerializeField]
	protected EffectTypes m_EffectType;

	[SerializeField]
	protected StackingTypes m_StackingType;

	[SerializeField]
	protected float m_TickInterval;

	[EnumFlag]
	[SerializeField]
	protected TargetTeamTypes m_TargetTeamFlags = TargetTeamTypes.Enemies;

	public EffectTypes EffectType => m_EffectType;

	public State GetNewEffect(Visible visible, Visible source)
	{
		return ConfigureNew(visible, source);
	}

	public void StackExistingEffect(State existingEffect)
	{
		switch (m_StackingType)
		{
		case StackingTypes.ReApply:
			existingEffect.Reapply();
			break;
		case StackingTypes.StackEffect:
			existingEffect.Reapply();
			StackEffect(existingEffect);
			break;
		case StackingTypes.Unassigned:
			break;
		}
	}

	protected virtual State ConfigureNew(Visible visible, Visible sourceVisible)
	{
		State state = new State(visible, sourceVisible, EffectType);
		Configure(state);
		return state;
	}

	protected virtual void Configure(State existingEffect)
	{
		existingEffect.m_TickInterval = m_TickInterval;
	}

	protected virtual void StackEffect(State existingEffect)
	{
	}

	public virtual bool CanApplyEffectOnVisible(Visible visibleEffectee, Visible visibleEffector)
	{
		if (visibleEffectee == null)
		{
			return false;
		}
		if (visibleEffector == null || visibleEffectee.type != ObjectTypes.Block || visibleEffector.type != ObjectTypes.Block || visibleEffectee.block.tank == null || visibleEffector.block.tank == null)
		{
			return true;
		}
		if ((m_TargetTeamFlags & TargetTeamTypes.Enemies) != 0 && visibleEffectee.block.tank.IsEnemy(visibleEffector.block.tank.Team))
		{
			return true;
		}
		if ((m_TargetTeamFlags & TargetTeamTypes.Neutral) != 0 && visibleEffectee.block.tank.IsNeutral())
		{
			return true;
		}
		if ((m_TargetTeamFlags & TargetTeamTypes.Friendlies) != 0 && visibleEffectee.block.tank.IsFriendly(visibleEffector.block.tank.Team))
		{
			return true;
		}
		return false;
	}
}
