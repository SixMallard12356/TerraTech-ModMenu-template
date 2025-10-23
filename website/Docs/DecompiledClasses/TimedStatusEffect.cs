using UnityEngine;

public abstract class TimedStatusEffect : StatusEffect
{
	protected class TimedState : State
	{
		public float m_Duration;

		private float m_DurationRemaining;

		public TimedState(Visible visible, Visible source, EffectTypes type)
			: base(visible, source, type)
		{
		}

		public override void Reapply()
		{
			base.Reapply();
			m_DurationRemaining = m_Duration;
		}

		public override void OnStart()
		{
			base.OnStart();
			m_DurationRemaining = m_Duration;
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();
			m_DurationRemaining -= Time.fixedDeltaTime;
			if (m_DurationRemaining <= 0f)
			{
				End();
			}
		}
	}

	[SerializeField]
	protected float m_Duration;

	protected override State ConfigureNew(Visible visible, Visible source)
	{
		TimedState timedState = new TimedState(visible, source, base.EffectType);
		Configure(timedState);
		return timedState;
	}

	protected override void Configure(State existingEffect)
	{
		base.Configure(existingEffect);
		(existingEffect as TimedState).m_Duration = m_Duration;
	}
}
