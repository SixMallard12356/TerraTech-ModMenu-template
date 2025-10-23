using UnityEngine;

public class StatusEffectorFieldProjectile : ZoneProjectile
{
	[SerializeField]
	private StatusEffect.EffectTypes m_EffectType;

	private void FixedUpdate()
	{
		if (!base.Deployed)
		{
			return;
		}
		foreach (Collider item in PhysicsUtils.OverlapSphereAllNonAlloc(base.trans.position, base.ZoneRadius, m_LayersToAffect.value))
		{
			Visible visible = Singleton.Manager<ManVisible>.inst.FindVisible(item);
			if (visible == null)
			{
				break;
			}
			Singleton.Manager<ManStatusEffects>.inst.TryApplyUnnetworkedEffectOnVisible(m_EffectType, visible, null);
		}
	}
}
