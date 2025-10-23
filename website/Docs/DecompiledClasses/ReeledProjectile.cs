using System;
using UnityEngine;

[Obsolete("No longer using this projectile-based component, should use a Reeler on the barrel instead!")]
[RequireComponent(typeof(Projectile))]
public class ReeledProjectile : MonoBehaviour
{
	[Serializable]
	private struct TetherStyle
	{
		[SerializeField]
		[Tooltip("Level of detail: How many vertices the line renderer has per in game unit of tether distance. Lower is more performant, but looks worse")]
		private int SegmentsPerUnit;

		[SerializeField]
		[Tooltip("How soon the tether 'ripples' should dramatically fade out as it nears either end. In game units of distance. We ease out the ripples so either end can connect with the tips.")]
		private float EndHeavyDampingDistance;

		[Range(0f, 1f)]
		[SerializeField]
		[Tooltip("How soon the tether 'ripples' should calmly ease out as it nears either end. As a percentage of the half length of the tether.")]
		private float EndLightDampingThreshold;

		[SerializeField]
		[Tooltip("How strong the light damping is. Higher value = strong damping")]
		private float EndLightDampingStrength;

		[Tooltip("How many ripples are there over time while the line is expanding out. Higher value = more frequent ripples")]
		[SerializeField]
		private AnimationCurve WobbleFrequencyOverTime;

		[Tooltip("How big the ripples can get while the line is expanding out. Higher value = bigger ripples")]
		[SerializeField]
		private float WobbleMagnitude;

		[SerializeField]
		[Tooltip("How quickly the rope tightens and loses its ripples once a connection is made. Lower value = quicker tightening")]
		private float TightenTime;

		[Tooltip("How quickly the rope should ripple out once fired. Lower value = quicker rippling time and the sooner you get bigger ripples")]
		[SerializeField]
		private float ExpansionTime;

		public void EvaluateKeyFactors(float distance, float firedDuration, float stickDuration, float reelingDuration, out int pointCount, out float wobbleFactor01, out float tighteningFactor01, out float wobblePeriod)
		{
			pointCount = Mathf.Max(2, Mathf.CeilToInt(distance * (float)SegmentsPerUnit));
			wobbleFactor01 = Mathf.Min(1f, firedDuration / ExpansionTime);
			wobblePeriod = WobbleFrequencyOverTime.Evaluate(firedDuration);
			tighteningFactor01 = Mathf.Min(1f, Mathf.Max(reelingDuration / TightenTime, stickDuration / TightenTime));
		}

		public Vector2 EvaluateFlatLinePosition(float distance, float t, float wobblePeriod, float wobbleFactor01, float tighteningFactor01)
		{
			Vector2 result = default(Vector2);
			result.x = distance * t;
			float num = ((t > 0.5f) ? (distance - result.x) : result.x);
			float num2 = Mathf.Min(num / (distance / 2f), 1f);
			result.y = wobbleFactor01 * WobbleMagnitude * Mathf.Sin(wobblePeriod * result.x);
			float num3 = Mathf.Min(num2 / EndLightDampingThreshold, 1f);
			float num4 = Mathf.Min(num / EndHeavyDampingDistance, 1f);
			result.y *= 1f - Mathf.Pow(1f - num4, 2f);
			result.y *= Mathf.Min(1f, num3 / EndLightDampingStrength);
			result.y *= 1f - tighteningFactor01;
			return result;
		}
	}

	[SerializeField]
	protected LineRenderer m_TetherRenderer;

	[SerializeField]
	protected float m_MaxTetherDistance = 20f;

	[SerializeField]
	[Tooltip("Duration in seconds before the projectile should be automatically reeled back in")]
	protected float m_AutoReelTime = 3f;

	[SerializeField]
	protected float m_Slack = 5f;

	[SerializeField]
	protected float m_TetherStrainForce = 55f;

	[SerializeField]
	protected float m_ReelForce = 55f;

	[SerializeField]
	private TetherStyle m_TetherStyle;

	private Projectile m_Projectile;

	private MissileProjectile m_MissileProjectile;

	private TankBlock m_CurrentBlock;

	private float m_StickTime = float.PositiveInfinity;

	private float m_FiredTime = float.PositiveInfinity;

	private float m_ReelTime = float.PositiveInfinity;

	private Vector2 m_WobbleAngle = Vector2.up;

	private float m_MaxTetherDistanceStuck;

	private bool m_MaxSlackReached;

	private bool m_Returning;

	private float DurationStuck
	{
		get
		{
			if (m_StickTime != float.PositiveInfinity)
			{
				return Time.time - m_StickTime;
			}
			return 0f;
		}
	}

	private float DurationFired
	{
		get
		{
			if (m_FiredTime != float.PositiveInfinity)
			{
				return Time.time - m_FiredTime;
			}
			return 0f;
		}
	}

	private float DurationReeled
	{
		get
		{
			if (m_ReelTime != float.PositiveInfinity)
			{
				return Time.time - m_ReelTime;
			}
			return 0f;
		}
	}

	private float TetherDistance => Vector3.Distance(m_TetherRenderer.transform.position, m_Projectile.FiringOrigin.position);

	private float MaxTetherDistance
	{
		get
		{
			if (!m_Projectile.Stuck)
			{
				return m_MaxTetherDistance;
			}
			return m_MaxTetherDistanceStuck;
		}
	}

	private Vector3 VectorToFiredOrigin => m_Projectile.FiringOrigin.position - m_TetherRenderer.transform.position;

	private void UpdateTether()
	{
		Vector3 position = m_TetherRenderer.transform.position;
		Vector3 position2 = m_Projectile.FiringOrigin.position;
		float tetherDistance = TetherDistance;
		m_TetherStyle.EvaluateKeyFactors(tetherDistance, DurationFired, DurationStuck, DurationReeled, out var pointCount, out var wobbleFactor, out var tighteningFactor, out var wobblePeriod);
		Quaternion quaternion = Quaternion.FromToRotation(Vector3.forward, position2 - position);
		Vector3[] array = new Vector3[pointCount];
		for (int i = 0; i < pointCount; i++)
		{
			float t = (float)i / (float)(pointCount - 1);
			Vector2 vector = m_TetherStyle.EvaluateFlatLinePosition(tetherDistance, t, wobblePeriod, wobbleFactor, tighteningFactor);
			Vector2 vector2 = m_WobbleAngle * vector.y;
			Vector3 vector3 = new Vector3(vector2.x, vector2.y, vector.x);
			array[i] = quaternion * vector3;
			array[i] += position;
		}
		m_TetherRenderer.positionCount = pointCount;
		m_TetherRenderer.SetPositions(array);
	}

	private void UpdateAutoRetract()
	{
		if (DurationReeled == 0f && DurationStuck == 0f && DurationFired > m_AutoReelTime)
		{
			Retract();
		}
	}

	private void Retract()
	{
		m_ReelTime = Time.time;
	}

	private void ResetStuck()
	{
		if (m_CurrentBlock != null)
		{
			m_CurrentBlock.visible.RecycledEvent.Unsubscribe(OnStuckVisibleRecycled);
			m_CurrentBlock.DetachedEvent.Unsubscribe(OnStuckBlockDetached);
			m_CurrentBlock = null;
		}
		m_StickTime = float.PositiveInfinity;
		m_MaxTetherDistanceStuck = 0f;
	}

	private void StartFiring()
	{
		m_FiredTime = Time.time;
		m_WobbleAngle = UnityEngine.Random.insideUnitCircle.normalized;
		m_Projectile.Weapon.block.visible.RecycledEvent.Subscribe(OnWeaponRecycled);
	}

	private void ResetFired()
	{
		m_Returning = false;
		m_MaxSlackReached = false;
		m_ReelTime = float.PositiveInfinity;
		m_FiredTime = float.PositiveInfinity;
		m_Projectile.Weapon.block.visible.RecycledEvent.Unsubscribe(OnWeaponRecycled);
	}

	private void AlignProjectileWithTether()
	{
		if (DurationReeled != 0f)
		{
			m_Projectile.rbody.MoveRotation(Quaternion.LookRotation(-VectorToFiredOrigin));
		}
	}

	private void ApplyLineForces()
	{
		Rigidbody rigidbody = ((!m_Projectile.Stuck) ? m_Projectile.rbody : (m_CurrentBlock.IsAttached ? m_CurrentBlock.tank.rbody : m_CurrentBlock.rbody));
		Rigidbody obj = (m_Projectile.Weapon.block.IsAttached ? m_Projectile.Weapon.block.tank.rbody : m_Projectile.Weapon.block.rbody);
		Vector3 tetherStrainForce = GetTetherStrainForce(rigidbody);
		Vector3 tetherReelForce = GetTetherReelForce(rigidbody);
		Vector3 vector = tetherStrainForce + tetherReelForce;
		rigidbody.AddForce(vector);
		obj.AddForce(-vector);
	}

	private Vector3 GetTetherReelForce(Rigidbody projectileRb)
	{
		if (DurationReeled == 0f)
		{
			return Vector3.zero;
		}
		Vector3 vectorToFiredOrigin = VectorToFiredOrigin;
		if (vectorToFiredOrigin.magnitude < 0.4f)
		{
			m_Projectile.Recycle();
			return Vector3.zero;
		}
		float num = Mathf.Max(0f, projectileRb.GetSpeedInDirection(-vectorToFiredOrigin));
		m_Returning = m_Returning || num <= 0f;
		return vectorToFiredOrigin.normalized * (projectileRb.mass * m_ReelForce + projectileRb.mass * m_ReelForce * num);
	}

	private Vector3 GetTetherStrainForce(Rigidbody projectileRb)
	{
		if (TetherDistance < MaxTetherDistance)
		{
			if (m_Projectile.Stuck)
			{
				m_MaxTetherDistanceStuck = TetherDistance;
			}
			return Vector3.zero;
		}
		Vector3 vectorToFiredOrigin = VectorToFiredOrigin;
		float num = vectorToFiredOrigin.magnitude - MaxTetherDistance;
		vectorToFiredOrigin = vectorToFiredOrigin.normalized;
		float num2 = ((m_Slack != 0f) ? Mathf.Min(1f, num / m_Slack) : 1f);
		if (!m_MaxSlackReached && num2 == 1f)
		{
			m_MaxSlackReached = true;
			if (!m_Projectile.Stuck)
			{
				if (m_MissileProjectile != null)
				{
					m_MissileProjectile.KillBoosters();
				}
				Retract();
			}
		}
		float num3 = Mathf.Max(0f, projectileRb.GetSpeedInDirection(-vectorToFiredOrigin));
		float num4 = num2 * num * (projectileRb.mass * m_TetherStrainForce + projectileRb.mass * m_TetherStrainForce * num3) * 0.05f;
		return vectorToFiredOrigin * num4;
	}

	public bool GetCanStick(Visible visible)
	{
		if (visible != null && visible.block != null && visible.block.tank != null)
		{
			return !m_Returning;
		}
		return false;
	}

	public void OnWeaponRecycled(Visible vis)
	{
		m_Projectile.Recycle();
	}

	public void OnStuck(Visible visible)
	{
		m_CurrentBlock = visible.block;
		m_StickTime = Time.time;
		visible.RecycledEvent.Subscribe(OnStuckVisibleRecycled);
		visible.block.DetachedEvent.Subscribe(OnStuckBlockDetached);
		m_MaxTetherDistanceStuck = TetherDistance - m_Slack / 4f;
	}

	private void OnStuckVisibleRecycled(Visible vis)
	{
		ResetStuck();
	}

	private void OnStuckBlockDetached()
	{
		Retract();
	}

	private void PrePool()
	{
	}

	private void OnPool()
	{
		m_Projectile = GetComponent<Projectile>();
		m_Projectile.m_FiredEvent.Subscribe(StartFiring);
		m_MissileProjectile = m_Projectile as MissileProjectile;
	}

	private void OnDepool()
	{
	}

	private void OnSpawn()
	{
	}

	private void OnRecycle()
	{
		ResetStuck();
		ResetFired();
	}

	private void Update()
	{
		UpdateTether();
		_ = m_CurrentBlock == null;
	}

	private void FixedUpdate()
	{
		UpdateAutoRetract();
		ApplyLineForces();
		AlignProjectileWithTether();
	}
}
