using UnityEngine;

public class FireData : MonoBehaviour
{
	public WeaponRound m_BulletPrefab;

	public BulletCasing m_BulletCasingPrefab;

	public float m_MuzzleVelocity;

	public float m_MuzzleVelocityVarianceFactor;

	public float m_MuzzleMaxAngleVarianceDegrees;

	public float m_BulletSpin = 50f;

	[Tooltip("Deprecated. This fails to apply random variance when aiming close to cardinal directions")]
	[ReadOnly(ReadOnlyAttribute.EnabledState.Always)]
	public bool m_ForceLegacyVariance;

	[ReadOnly(ReadOnlyAttribute.EnabledState.Always)]
	[Tooltip("Deprecated, replace with m_MuzzleVelocityVarianceFactor and m_MuzzleMaxAngleVarianceDegrees")]
	public float m_BulletSprayVariance;

	public float m_CasingVelocity = 5f;

	public float m_CasingEjectVariance = 0.3f;

	public float m_CasingEjectSpin = 50f;

	public float m_KickbackStrength = 1f;

	private void PrePool()
	{
		if (!m_ForceLegacyVariance && m_MuzzleVelocityVarianceFactor == 0f && m_MuzzleMaxAngleVarianceDegrees == 0f)
		{
			m_MuzzleVelocityVarianceFactor = m_BulletSprayVariance;
			m_MuzzleMaxAngleVarianceDegrees = m_BulletSprayVariance * 35f;
		}
	}
}
