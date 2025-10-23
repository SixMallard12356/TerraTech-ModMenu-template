using UnityEngine;

[CreateAssetMenu(fileName = "Burn Timed Status Effect", menuName = "TerraTech/Status Effects/Burn")]
public class BurnTimedStatusEffect : TimedStatusEffect
{
	protected class BurnState : TimedState
	{
		public Damageable m_Damageable;

		public float m_DamagePerTick;

		public MeshRendererParticleSystem m_MeshParticleSystemPrefab;

		public ManDamage.DamageType m_BurnDamageType;

		private MeshRendererParticleSystem[] m_ActiveParticleSystems;

		public BurnState(Visible visible, Visible source, EffectTypes type)
			: base(visible, source, type)
		{
		}

		private void ClearVFX()
		{
			if (m_ActiveParticleSystems != null)
			{
				MeshRendererParticleSystem[] activeParticleSystems = m_ActiveParticleSystems;
				for (int i = 0; i < activeParticleSystems.Length; i++)
				{
					activeParticleSystems[i].Recycle();
				}
				m_ActiveParticleSystems = null;
			}
		}

		private void ApplyVFX()
		{
			ClearVFX();
			m_ActiveParticleSystems = new MeshRendererParticleSystem[m_AffecteeVisible.meshRenderers.Length];
			for (int i = 0; i < m_AffecteeVisible.meshRenderers.Length; i++)
			{
				m_ActiveParticleSystems[i] = m_MeshParticleSystemPrefab.Spawn(m_AffecteeVisible.meshRenderers[i].transform);
			}
		}

		private void EndEffects(Visible vis)
		{
			m_AffecteeVisible.RecycledEvent.Unsubscribe(EndEffects);
			m_AffecteeVisible.MesheRenderersUpdatedEvent.Unsubscribe(ApplyVFX);
			ClearVFX();
		}

		public override void OnStart()
		{
			base.OnStart();
			m_AffecteeVisible.RecycledEvent.Subscribe(EndEffects);
			m_AffecteeVisible.MesheRenderersUpdatedEvent.Subscribe(ApplyVFX);
			ApplyVFX();
		}

		public override void OnTick()
		{
			base.OnTick();
			Singleton.Manager<ManDamage>.inst.DealDamage(m_Damageable, m_DamagePerTick, m_BurnDamageType, m_AffectorVisible);
		}

		public override void OnEnd()
		{
			base.OnEnd();
			EndEffects(m_AffecteeVisible);
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();
		}
	}

	[SerializeField]
	private MeshRendererParticleSystem m_MeshParticleSystemPrefab;

	[SerializeField]
	private ManDamage.DamageType m_BurnDamageType = ManDamage.DamageType.Fire;

	[SerializeField]
	private float m_DamagePerTick = 1f;

	public override bool CanApplyEffectOnVisible(Visible visible, Visible source)
	{
		if (base.CanApplyEffectOnVisible(visible, source) && visible.damageable != null)
		{
			return Singleton.Manager<ManDamage>.inst.GetDamageMultiplier(m_BurnDamageType, visible.damageable.DamageableType) > 0f;
		}
		return false;
	}

	protected override State ConfigureNew(Visible visible, Visible source)
	{
		BurnState burnState = new BurnState(visible, source, base.EffectType);
		Configure(burnState);
		return burnState;
	}

	protected override void Configure(State existingEffect)
	{
		base.Configure(existingEffect);
		BurnState obj = existingEffect as BurnState;
		obj.m_Damageable = obj.Visible.damageable;
		obj.m_DamagePerTick = m_DamagePerTick;
		obj.m_MeshParticleSystemPrefab = m_MeshParticleSystemPrefab;
		obj.m_BurnDamageType = m_BurnDamageType;
	}

	protected override void StackEffect(State existingEffect)
	{
	}
}
