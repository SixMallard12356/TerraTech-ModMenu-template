using UnityEngine;
using UnityEngine.Serialization;

public class LaserProjectile : Projectile
{
	[SerializeField]
	[FormerlySerializedAs("velocityScaleFactor")]
	private float m_VelocityScaleFactor = 1f;

	public override void Fire(Vector3 fireDirection, Transform firingOrigin, FireData fireData, ModuleWeapon weapon, Tank shooter = null, bool seekingRounds = false, bool replayRounds = false)
	{
		base.Fire(fireDirection, firingOrigin, fireData, weapon, shooter, seekingRounds, replayRounds);
		base.trans.localScale = base.trans.localScale.SetZ(base.rbody.velocity.magnitude * m_VelocityScaleFactor);
	}
}
