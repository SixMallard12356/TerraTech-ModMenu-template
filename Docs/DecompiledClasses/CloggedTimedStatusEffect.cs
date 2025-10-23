using UnityEngine;

[CreateAssetMenu(fileName = "Clogged Status Effect", menuName = "TerraTech/Status Effects/Clogged")]
public class CloggedTimedStatusEffect : TimedStatusEffect
{
	protected class CloggedState : TimedState
	{
		public TankBlock m_TankBlock;

		public TechAudio.SFXType m_EffectStartSFX;

		public float m_AnchorAdditionalFrictionTorque;

		public float m_WeaponTurnSlowDownIntensity;

		public float m_Clogged_Drive_Counterforce_Flat;

		public float m_Clogged_Drive_Counterforce_Scaled;

		public float m_Clogged_FrictionTorque_Additional_Flat;

		public float m_Clogged_FrictionTorque_Additional_Scaled;

		public CloggedState(Visible visible, Visible source, EffectTypes type)
			: base(visible, source, type)
		{
		}

		private void EndEffects()
		{
			m_TankBlock.DetachingEvent.Unsubscribe(EndEffects);
			m_TankBlock.SetCloggedVisualsActive(cloggedActive: false);
			if (m_TankBlock.visible.block.wheelsModule != null)
			{
				m_TankBlock.visible.block.wheelsModule.TrySetWheelClogged(0f, 0f, 0f, 0f);
			}
			if (m_TankBlock.visible.block.weapon != null)
			{
				m_TankBlock.visible.block.weapon.SetRotationSpeedCoefficient(1f);
			}
			if (m_TankBlock.visible.block.Anchor != null)
			{
				m_TankBlock.visible.block.Anchor.SetCloggedTorque(0f);
			}
		}

		public override void OnStart()
		{
			base.OnStart();
			m_TankBlock.DetachingEvent.Subscribe(EndEffects);
			m_TankBlock.SetCloggedVisualsActive(cloggedActive: true);
			m_TankBlock.tank.TechAudio.PlayOneshot(m_TankBlock, m_EffectStartSFX, 1f);
			if (m_TankBlock.visible.block.wheelsModule != null)
			{
				m_TankBlock.visible.block.wheelsModule.TrySetWheelClogged(m_Clogged_Drive_Counterforce_Flat, m_Clogged_Drive_Counterforce_Scaled, m_Clogged_FrictionTorque_Additional_Flat, m_Clogged_FrictionTorque_Additional_Scaled);
			}
			if (m_TankBlock.visible.block.weapon != null)
			{
				m_TankBlock.visible.block.weapon.SetRotationSpeedCoefficient(1f - m_WeaponTurnSlowDownIntensity);
			}
			if (m_TankBlock.visible.block.Anchor != null)
			{
				m_TankBlock.visible.block.Anchor.SetCloggedTorque(m_AnchorAdditionalFrictionTorque);
			}
		}

		public override void OnTick()
		{
			base.OnTick();
		}

		public override void OnEnd()
		{
			base.OnEnd();
			EndEffects();
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();
		}
	}

	[Header("General")]
	[SerializeField]
	protected TechAudio.SFXType m_EffectStartSFX = TechAudio.SFXType.SJCogsJamming;

	[Header("Wheel Slowdown")]
	[SerializeField]
	[Tooltip("A flat counter torque that slows down the driving force of affected wheels [FLAT forces are particularly punishing on weaker wheels!]")]
	protected float m_Clogged_Drive_Counterforce_Flat = 10f;

	[Tooltip("A scaled counter torque that slows down the driving force of affected wheels [SCALED forces are equally punishing on weaker and stronger wheels]")]
	[Range(0f, 1f)]
	[SerializeField]
	protected float m_Clogged_Drive_Counterforce_Scaled = 0.2f;

	[Tooltip("A flat added torque that increases passive braking of affected wheels [FLAT forces are particularly punishing on weaker wheels!]")]
	[SerializeField]
	protected float m_Clogged_FrictionTorque_Additional_Flat = 10f;

	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("A scaled added torque that increases passive braking of affected wheels [SCALED forces are equally punishing on weaker and stronger wheels]")]
	protected float m_Clogged_FrictionTorque_Additional_Scaled = 0.2f;

	[SerializeField]
	[Header("Other Slowdown Effects")]
	[Tooltip("How much we actively brake rotating anchors when it has the effect applied, as a uniform number across all anchors.")]
	protected float m_AnchorAdditionalFrictionTorque = 20000f;

	[Range(0f, 1f)]
	[Tooltip("How much we reduce the turning speed of effected weapons. 1 = They wont be able to turn at all, 0 = They wont be slowed at all")]
	[SerializeField]
	protected float m_WeaponTurnSlowDownIntensity = 0.5f;

	public override bool CanApplyEffectOnVisible(Visible visible, Visible source)
	{
		if (base.CanApplyEffectOnVisible(visible, source) && visible.block != null && visible.block.tank != null)
		{
			if (!(visible.block.wheelsModule != null) && !(visible.block.weapon != null))
			{
				return visible.block.Anchor != null;
			}
			return true;
		}
		return false;
	}

	protected override State ConfigureNew(Visible visible, Visible source)
	{
		CloggedState cloggedState = new CloggedState(visible, source, base.EffectType);
		Configure(cloggedState);
		return cloggedState;
	}

	protected override void Configure(State existingEffect)
	{
		base.Configure(existingEffect);
		CloggedState obj = existingEffect as CloggedState;
		obj.m_TankBlock = obj.Visible.block;
		obj.m_EffectStartSFX = m_EffectStartSFX;
		obj.m_WeaponTurnSlowDownIntensity = m_WeaponTurnSlowDownIntensity;
		obj.m_AnchorAdditionalFrictionTorque = m_AnchorAdditionalFrictionTorque;
		obj.m_Clogged_FrictionTorque_Additional_Scaled = m_Clogged_FrictionTorque_Additional_Scaled;
		obj.m_Clogged_Drive_Counterforce_Flat = m_Clogged_Drive_Counterforce_Flat;
		obj.m_Clogged_Drive_Counterforce_Scaled = m_Clogged_Drive_Counterforce_Scaled;
		obj.m_Clogged_FrictionTorque_Additional_Flat = m_Clogged_FrictionTorque_Additional_Flat;
	}

	protected override void StackEffect(State existingEffect)
	{
	}
}
