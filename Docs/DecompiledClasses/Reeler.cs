#define UNITY_EDITOR
using System;
using DevCommands;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Animator))]
public class Reeler : MonoBehaviour, ModuleWeaponGun.INetComponent
{
	public struct StateTrigger
	{
		private float m_TriggerTimestamp;

		private Action<bool> m_OnSetResetHandler;

		public float TimeSinceSet
		{
			get
			{
				if (!HasBeenSet)
				{
					return 0f;
				}
				return Time.time - m_TriggerTimestamp;
			}
		}

		public bool HasBeenSet => m_TriggerTimestamp != -1f;

		public StateTrigger(Action<bool> onSetResetHandler)
		{
			m_TriggerTimestamp = -1f;
			m_OnSetResetHandler = onSetResetHandler;
		}

		public void Reset()
		{
			Set(state: false);
		}

		public void Set(bool state = true)
		{
			if (state != HasBeenSet)
			{
				m_TriggerTimestamp = (state ? Time.time : (-1f));
				m_OnSetResetHandler?.Invoke(state);
			}
		}

		public static implicit operator bool(StateTrigger trig)
		{
			return trig.HasBeenSet;
		}
	}

	[Serializable]
	private struct TetherStyle
	{
		[Tooltip("Level of detail: How many vertices the line renderer has per in game unit of tether distance. Lower is more performant, but looks worse")]
		[SerializeField]
		private int SegmentsPerUnit;

		[SerializeField]
		[Tooltip("How soon the tether 'ripples' should dramatically fade out as it nears either end. In game units of distance. We ease out the ripples so either end can connect with the tips.")]
		private float EndHeavyDampingDistance;

		[Tooltip("How soon the tether 'ripples' should calmly ease out as it nears either end. As a percentage of the half length of the tether.")]
		[Range(0f, 1f)]
		[SerializeField]
		private float EndLightDampingThreshold;

		[SerializeField]
		[Tooltip("How strong the light damping is. Higher value = strong damping")]
		private float EndLightDampingStrength;

		[SerializeField]
		[Tooltip("How many ripples are there over time while the line is expanding out. Higher value = more frequent ripples")]
		private AnimationCurve WobbleFrequencyOverTime;

		[SerializeField]
		[Tooltip("How big the ripples can get while the line is expanding out. Higher value = bigger ripples")]
		private float WobbleMagnitude;

		[SerializeField]
		[Tooltip("How quickly the rope tightens and loses its ripples once a connection is made. Lower value = quicker tightening")]
		private float TightenTime;

		[SerializeField]
		[Tooltip("How quickly the rope should ripple out once fired. Lower value = quicker rippling time and the sooner you get bigger ripples")]
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
	public ManSFX.TransformLoopingSFXTypes m_TetherSFXType;

	[SerializeField]
	protected Spinner m_ReelSpinner;

	[SerializeField]
	protected GameObject m_DummyProjectile;

	[SerializeField]
	protected float m_MaxTetherDistance = 20f;

	[SerializeField]
	[Tooltip("The maximum time the projectile will stick to a block before popping off")]
	protected float m_MaxStickTime = 1.8f;

	[Tooltip("Multiplier that determines the strength of the force removing blocks the projectile is stuck to. Higher is stronger")]
	[SerializeField]
	protected float m_PluckingFactor = 2f;

	[SerializeField]
	protected float m_PostPluckStickDuration = 0.5f;

	[Tooltip("Duration in seconds before the projectile should be automatically reeled back in")]
	[SerializeField]
	protected float m_AutoReelTime = 3f;

	[SerializeField]
	[Tooltip("The maximum time the projectile will stay alive for once returning")]
	protected float m_MaxReturnTime = 1f;

	[SerializeField]
	[Tooltip("The minimum distance the projectile can be from the barrel before it gets reset while returning")]
	protected float m_MinReturnDistance = 3f;

	[SerializeField]
	protected float m_Slack = 5f;

	[Tooltip("When the harpoon sticks, how much extra (or less) chain distance the spool is locked to. Negative distance gives user feedback in the form of a small jolt towards the target once stuck")]
	[SerializeField]
	protected float m_TetherLockDistance;

	[SerializeField]
	protected float m_TetherStrainForce = 10f;

	[Tooltip("Number of continuous frames detach or Rip out force must be exceeded before action is acted on.")]
	[SerializeField]
	protected float m_TimeBeforeEffectAction;

	[SerializeField]
	protected float m_ReelForce = 50f;

	[SerializeField]
	protected float m_ReelSpeed = 3f;

	[Tooltip("Maximum force the hook can sustain before it is effectively ripped loose from whatever it is attached to")]
	[SerializeField]
	protected float m_HookMaxGripForce = 100f;

	[SerializeField]
	private float m_RipOutDamage;

	[SerializeField]
	private ManDamage.DamageType m_RipOutDamageType;

	[SerializeField]
	private Transform m_RipOutEffectPrefab;

	[SerializeField]
	private TetherStyle m_TetherStyle;

	private StateTrigger m_FiredTrigger;

	private StateTrigger m_StuckTrigger;

	private StateTrigger m_ReelingTrigger;

	private StateTrigger m_ReleaseProjectileTrigger;

	private StateTrigger m_ReturningTrigger;

	private StateTrigger m_UntetheredTrigger;

	protected ModuleWeapon m_Weapon;

	protected ModuleWeaponGun m_Gun;

	protected CannonBarrel m_Barrel;

	protected Animator m_Anim;

	private static readonly int s_Anim_FireTrigger_ID = Animator.StringToHash("Fire");

	private static readonly int s_Anim_ReloadTrigger_ID = Animator.StringToHash("Reload");

	protected Projectile m_Projectile;

	protected MissileProjectile m_MissileProjectile;

	private bool m_HasProjectile;

	private ManSFX.LoopingSFXKey m_AudioLoopKey;

	private float m_MaxTetherDistanceStuck;

	private float m_AdditionalStickTime;

	private Vector3 m_LastFrameTotalLineForce = Vector3.zero;

	private float m_lastFrameTetherLength;

	private Vector2 m_WobbleAngle = Vector2.up;

	private float m_TimeInEffect;

	private const bool k_AllowStickAnywhere = true;

	private const float k_SpinnerSpeedFactor = 50f;

	private const float k_TetherSFXMaxDeltaPerSecond = 120f;

	private static bool s_EnableDetachFriendly = false;

	public bool HasFired => m_FiredTrigger.HasBeenSet;

	private float CurrentTetherDistance => Vector3.Distance(m_Projectile.TetherAnchorPoint.position, m_Projectile.FiringOrigin.position);

	private float CurrentMaxTetherDistance
	{
		get
		{
			if (!m_StuckTrigger)
			{
				return m_MaxTetherDistance;
			}
			return m_MaxTetherDistanceStuck;
		}
	}

	private float CurrentStrainMagnitude => CurrentVectorToFiredOrigin.magnitude - CurrentMaxTetherDistance;

	private float CurrentSlackSaturation
	{
		get
		{
			if (m_Slack == 0f)
			{
				return 1f;
			}
			return Mathf.Min(1f, CurrentStrainMagnitude / m_Slack);
		}
	}

	private Vector3 CurrentVectorToFiredOrigin => m_Projectile.FiringOrigin.position - m_Projectile.TetherAnchorPoint.position;

	public void InitOnGunBarrel(ModuleWeaponGun gun, CannonBarrel barrel, ModuleWeapon weapon)
	{
		m_Gun = gun;
		m_Barrel = barrel;
		m_Weapon = weapon;
	}

	public void TetherToProjectile(Projectile projectile)
	{
		UntetherFromCurrentProjectile();
		m_Projectile = projectile;
		m_MissileProjectile = projectile as MissileProjectile;
		m_HasProjectile = m_Projectile != null;
		m_TetherRenderer.enabled = m_HasProjectile;
		if (m_HasProjectile)
		{
			Singleton.Manager<ManSFX>.inst.TryStartTransformLoopingSFX(m_TetherSFXType, base.transform, out m_AudioLoopKey);
			m_Projectile.m_RecycledEvent.Subscribe(OnProjectileRecycled);
			m_Projectile.m_CollisionEvent.Subscribe(OnProjectileCollision);
			m_Projectile.m_StuckToVisibleEvent.Subscribe(OnStuck);
			m_Projectile.m_FiredEvent.Subscribe(OnFired);
			m_Projectile.RegisterStickCondition(CanStickToTarget);
		}
	}

	private bool CanStickToTarget(Visible stickTarget)
	{
		if ((bool)m_ReelingTrigger)
		{
			return false;
		}
		return true;
	}

	private void UntetherFromAndRecycleCurrentProjectile()
	{
		UntetherFromCurrentProjectile(andRecycleProjectile: true);
	}

	private void UntetherFromCurrentProjectile()
	{
		UntetherFromCurrentProjectile(andRecycleProjectile: false);
	}

	private void UntetherFromCurrentProjectile(bool andRecycleProjectile)
	{
		if (m_HasProjectile)
		{
			Singleton.Manager<ManSFX>.inst.TryStopTransformLoopingSFX(m_TetherSFXType, base.transform);
			Projectile projectile = m_Projectile;
			m_Projectile.m_RecycledEvent.Unsubscribe(OnProjectileRecycled);
			m_Projectile.m_CollisionEvent.Unsubscribe(OnProjectileCollision);
			m_Projectile.m_StuckToVisibleEvent.Unsubscribe(OnStuck);
			m_Projectile.m_FiredEvent.Unsubscribe(OnFired);
			m_Projectile.UnRegisterStickCondition(CanStickToTarget);
			m_Projectile.rbody.isKinematic = false;
			m_Projectile = null;
			m_MissileProjectile = null;
			m_HasProjectile = false;
			m_TetherRenderer.enabled = false;
			if (andRecycleProjectile && projectile != null)
			{
				projectile.Recycle();
			}
			m_Anim.SetTrigger(s_Anim_ReloadTrigger_ID);
			m_FiredTrigger.Reset();
			m_StuckTrigger.Reset();
			m_ReelingTrigger.Reset();
			m_ReleaseProjectileTrigger.Reset();
			m_ReturningTrigger.Reset();
		}
	}

	private void ApplyLineForces()
	{
		if (!m_HasProjectile)
		{
			return;
		}
		Rigidbody rigidbody = (m_StuckTrigger ? null : m_Projectile.rbody);
		if (m_Projectile.VisibleStuckTo.IsNotNull())
		{
			rigidbody = ((m_Projectile.VisibleStuckTo.block.IsNotNull() && m_Projectile.VisibleStuckTo.block.IsAttached) ? m_Projectile.VisibleStuckTo.block.tank.rbody : m_Projectile.VisibleStuckTo.rbody);
		}
		Rigidbody rigidbody2 = (m_Projectile.Weapon.block.IsAttached ? m_Projectile.Weapon.block.tank.rbody : m_Projectile.Weapon.block.rbody);
		Vector3 tetherStrainForce = GetTetherStrainForce(rigidbody);
		Vector3 tetherReelForce = GetTetherReelForce();
		if ((bool)m_ReturningTrigger)
		{
			Vector3 vector = CurrentVectorToFiredOrigin.normalized * m_ReelForce * Time.fixedDeltaTime * m_ReelSpeed;
			m_Projectile.rbody.position += vector + rigidbody2.velocity * Time.fixedDeltaTime;
			return;
		}
		Vector3 vector2 = tetherStrainForce + tetherReelForce;
		if (rigidbody.IsNotNull())
		{
			rigidbody.AddForceAtPosition(vector2, m_Projectile.TetherAnchorPoint.position);
		}
		else
		{
			vector2 *= 2f;
		}
		rigidbody2.AddForceAtPosition(-vector2, m_Projectile.FiringOrigin.position);
		m_LastFrameTotalLineForce = vector2;
	}

	private Vector3 GetTetherReelForce()
	{
		if (!m_ReelingTrigger)
		{
			return Vector3.zero;
		}
		Vector3 currentVectorToFiredOrigin = CurrentVectorToFiredOrigin;
		float num = Mathf.Max(0f, m_Projectile.rbody.GetSpeedInDirection(-currentVectorToFiredOrigin));
		if (!m_Projectile.Stuck && num <= 0f && !m_ReturningTrigger)
		{
			m_ReturningTrigger.Set();
		}
		Vector3 dampingForce = m_Projectile.rbody.GetDampingForce(currentVectorToFiredOrigin, m_Projectile.rbody.GetPointVelocity(m_Projectile.TetherAnchorPoint.position), 0.85f);
		Vector3 vector = currentVectorToFiredOrigin.normalized * m_ReelForce * m_Projectile.rbody.mass / Time.fixedDeltaTime;
		return dampingForce + vector;
	}

	private Vector3 GetTetherStrainForce(Rigidbody projectileRb)
	{
		if (!m_Projectile.Stuck)
		{
			return Vector3.zero;
		}
		if (CurrentTetherDistance < CurrentMaxTetherDistance)
		{
			return Vector3.zero;
		}
		float currentStrainMagnitude = CurrentStrainMagnitude;
		Vector3 normalized = CurrentVectorToFiredOrigin.normalized;
		float num = 0f;
		float num2 = 100f;
		if (projectileRb != null)
		{
			num = Mathf.Max(0f, projectileRb.GetSpeedInDirection(-normalized));
			num2 = projectileRb.mass;
		}
		float num3 = CurrentSlackSaturation * currentStrainMagnitude * currentStrainMagnitude * (num2 * m_TetherStrainForce + num2 * m_TetherStrainForce * num) * 0.005f;
		return normalized * num3;
	}

	private void UpdateTetherVisuals()
	{
		if (m_HasProjectile)
		{
			if (!m_Projectile.Stuck && (bool)m_ReturningTrigger)
			{
				m_Projectile.rbody.MoveRotation(Quaternion.LookRotation(-CurrentVectorToFiredOrigin));
			}
			Vector3 position = m_Projectile.TetherAnchorPoint.position;
			Vector3 position2 = m_Projectile.FiringOrigin.position;
			float currentTetherDistance = CurrentTetherDistance;
			float num = currentTetherDistance - m_lastFrameTetherLength;
			UpdateSpinner(num);
			UpdateTetherSFX(num);
			m_lastFrameTetherLength = currentTetherDistance;
			m_TetherStyle.EvaluateKeyFactors(currentTetherDistance, m_FiredTrigger.TimeSinceSet, m_StuckTrigger.TimeSinceSet, m_ReelingTrigger.TimeSinceSet, out var pointCount, out var wobbleFactor, out var tighteningFactor, out var wobblePeriod);
			Quaternion quaternion = Quaternion.FromToRotation(Vector3.forward, position2 - position);
			Vector3[] array = new Vector3[pointCount];
			for (int i = 0; i < pointCount; i++)
			{
				float t = (float)i / (float)(pointCount - 1);
				Vector2 vector = m_TetherStyle.EvaluateFlatLinePosition(currentTetherDistance, t, wobblePeriod, wobbleFactor, tighteningFactor);
				Vector2 vector2 = m_WobbleAngle * vector.y;
				Vector3 vector3 = new Vector3(vector2.x, vector2.y, vector.x);
				array[i] = quaternion * vector3;
				array[i] += position;
			}
			m_TetherRenderer.positionCount = pointCount;
			m_TetherRenderer.SetPositions(array);
		}
	}

	private void UpdateSpinner(float tetherDistanceDelta)
	{
		if (!(m_ReelSpinner == null) && m_ReelSpinner != null)
		{
			m_ReelSpinner.UpdateSpin(tetherDistanceDelta * 50f);
		}
	}

	private void UpdateTetherSFX(float tetherDistanceFrameDelta)
	{
		if (m_TetherSFXType != ManSFX.TransformLoopingSFXTypes.None)
		{
			float paramValue = Mathf.Clamp01(Mathf.Abs(tetherDistanceFrameDelta) / (120f * Time.deltaTime)) * 100f;
			Singleton.Manager<ManSFX>.inst.SetTransformLoopingSFXParam(m_AudioLoopKey, "Chain", paramValue);
		}
	}

	private void UpdateReturning()
	{
		if (m_HasProjectile && (bool)m_ReturningTrigger && (m_ReturningTrigger.TimeSinceSet > m_MaxReturnTime || CurrentVectorToFiredOrigin.magnitude < m_MinReturnDistance))
		{
			m_UntetheredTrigger.Set();
		}
	}

	private void UpdateStuck()
	{
		if (!m_HasProjectile || !m_Projectile.Stuck)
		{
			return;
		}
		bool flag = m_Projectile.VisibleStuckTo != null;
		if (m_StuckTrigger.TimeSinceSet > m_MaxStickTime + m_AdditionalStickTime)
		{
			if (flag && m_Projectile.VisibleStuckTo.rbody.IsNotNull() && !m_Projectile.VisibleStuckTo.rbody.isKinematic)
			{
				m_Projectile.VisibleStuckTo.rbody.velocity = (CurrentVectorToFiredOrigin.normalized + Vector3.up).normalized * m_ReelForce;
			}
			m_ReleaseProjectileTrigger.Set();
			return;
		}
		bool flag2 = false;
		float num = m_LastFrameTotalLineForce.magnitude;
		if (flag)
		{
			TankBlock block = m_Projectile.VisibleStuckTo.block;
			if (block.IsNotNull() && block.IsAttached && !block.IsController && !block.visible.damageable.Invulnerable && (s_EnableDetachFriendly || block.tank.IsEnemy()) && !block.visible.IsLocked(Visible.LockTimerTypes.Grabbable) && !block.tank.visible.IsLocked(Visible.LockTimerTypes.Grabbable) && block.damage.CouldDetachFromAdditionalDamage(num * m_PluckingFactor))
			{
				flag2 = true;
				if (m_TimeInEffect + Time.deltaTime > m_TimeBeforeEffectAction)
				{
					Singleton.Manager<ManLooseBlocks>.inst.HostDetachBlock(block, allowHeadlessTech: false, propagate: true);
					num = 0f;
					if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
					{
						m_Projectile.StickToObject(block.trans, block.visible);
						m_AdditionalStickTime += m_PostPluckStickDuration;
						m_MaxTetherDistanceStuck = Mathf.Max(CurrentTetherDistance - m_Slack * 0.25f, 0f);
					}
					else
					{
						m_ReleaseProjectileTrigger.Set();
					}
					block.rbody.AddForce((CurrentVectorToFiredOrigin.normalized + Vector3.up).normalized * m_ReelForce, ForceMode.VelocityChange);
				}
			}
		}
		if ((bool)m_StuckTrigger && num > m_HookMaxGripForce)
		{
			flag2 = true;
			if (m_TimeInEffect + Time.deltaTime > m_TimeBeforeEffectAction)
			{
				if (flag && m_Projectile.VisibleStuckTo.damageable != null)
				{
					float kickbackStrength = num * 0.1f;
					Singleton.Manager<ManDamage>.inst.DealDamage(m_Projectile.VisibleStuckTo.damageable, m_RipOutDamage, m_RipOutDamageType, m_Weapon, m_Projectile.Shooter, m_Projectile.TetherAnchorPoint.position, CurrentVectorToFiredOrigin, kickbackStrength);
				}
				m_ReleaseProjectileTrigger.Set();
				num = 0f;
				if (m_RipOutEffectPrefab != null)
				{
					m_RipOutEffectPrefab.Spawn(Singleton.dynamicContainer, m_Projectile.TetherAnchorPoint.position);
				}
			}
		}
		if ((bool)m_StuckTrigger && flag2)
		{
			m_TimeInEffect += Time.deltaTime;
		}
		else
		{
			m_TimeInEffect = 0f;
		}
	}

	[DevCommand(Name = "Reeler.SetDetachFriendly")]
	private static void EnableDetachFriendly(bool enabled)
	{
		s_EnableDetachFriendly = enabled;
	}

	private void UpdateAutoReelIn()
	{
		if (!m_ReelingTrigger && ((bool)m_StuckTrigger || m_FiredTrigger.TimeSinceSet > m_AutoReelTime || CurrentSlackSaturation == 1f))
		{
			m_ReelingTrigger.Set();
		}
	}

	private void OnFiredTriggerChanged(bool set)
	{
		if (set)
		{
			m_Anim.SetTrigger(s_Anim_FireTrigger_ID);
			m_WobbleAngle = UnityEngine.Random.insideUnitCircle.normalized;
		}
		else
		{
			m_lastFrameTetherLength = 0f;
			m_LastFrameTotalLineForce = Vector3.zero;
		}
		m_DummyProjectile.SetActive(!set);
	}

	private void OnStuckTriggerChanged(bool set)
	{
		m_MaxTetherDistanceStuck = (set ? Mathf.Max(CurrentTetherDistance + m_TetherLockDistance, 0f) : 0f);
		m_AdditionalStickTime = 0f;
	}

	private void OnReelingTriggerChanged(bool set)
	{
		if (set)
		{
			if (m_MissileProjectile != null)
			{
				m_MissileProjectile.KillBoosters();
			}
			m_Projectile.rbody.isKinematic = true;
		}
		if (ManNetwork.IsHost)
		{
			m_Gun.SetNetComponentsDirty();
		}
	}

	private void OnReleaseProjectileTriggerChanged(bool set)
	{
		if (set)
		{
			if (m_Projectile.Stuck)
			{
				m_Projectile.UnstickFromObject();
			}
			m_Projectile.rbody.AddForce(Vector3.up * (m_ReelForce * 0.5f), ForceMode.VelocityChange);
		}
		if (ManNetwork.IsHost)
		{
			m_Gun.SetNetComponentsDirty();
		}
	}

	private void OnReturningTriggerChanged(bool set)
	{
		if (ManNetwork.IsHost)
		{
			m_Gun.SetNetComponentsDirty();
		}
	}

	private void OnUntetheredTriggerChanged(bool set)
	{
		if (set)
		{
			UntetherFromAndRecycleCurrentProjectile();
		}
		if (ManNetwork.IsHost)
		{
			m_Gun.SetNetComponentsDirty();
		}
	}

	private void OnProjectileRecycled()
	{
		UntetherFromCurrentProjectile();
	}

	private void OnProjectileCollision(Damageable damageable)
	{
		if ((bool)m_ReturningTrigger && !(damageable == null) && !damageable.Block.IsNull() && !damageable.Block.IsAttached && (object)damageable.Block.tank == m_Weapon.block.tank)
		{
			UntetherFromAndRecycleCurrentProjectile();
		}
	}

	private void OnFired()
	{
		m_UntetheredTrigger.Reset();
		m_FiredTrigger.Set();
	}

	private void OnStuck(bool state, Projectile _, Visible __)
	{
		if (state)
		{
			m_StuckTrigger.Set();
		}
	}

	private void PrePool()
	{
		m_TetherRenderer.useWorldSpace = true;
	}

	private void OnPool()
	{
		m_Anim = GetComponent<Animator>();
		TankBlock block = m_Weapon.block;
		block.BlockUpdate.Subscribe(OnUpdate);
		block.BlockFixedUpdate.Subscribe(OnFixedUpdate);
		m_FiredTrigger = new StateTrigger(OnFiredTriggerChanged);
		m_StuckTrigger = new StateTrigger(OnStuckTriggerChanged);
		m_ReelingTrigger = new StateTrigger(OnReelingTriggerChanged);
		m_ReleaseProjectileTrigger = new StateTrigger(OnReleaseProjectileTriggerChanged);
		m_ReturningTrigger = new StateTrigger(OnReturningTriggerChanged);
		m_UntetheredTrigger = new StateTrigger(OnUntetheredTriggerChanged);
	}

	private void OnSpawn()
	{
		m_TetherRenderer.enabled = false;
	}

	private void OnRecycle()
	{
		UntetherFromAndRecycleCurrentProjectile();
	}

	private void OnUpdate()
	{
		UpdateTetherVisuals();
	}

	private void OnFixedUpdate()
	{
		if (m_HasProjectile)
		{
			ApplyLineForces();
			if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer() || ManNetwork.IsHost)
			{
				UpdateAutoReelIn();
				UpdateStuck();
				UpdateReturning();
			}
		}
	}

	private void OnValidate()
	{
		d.AssertFormat(m_RipOutEffectPrefab == null || m_RipOutEffectPrefab.GetComponent<ParticleRecycler>() != null, "Rip out effect {0} on Reeler {1} does not have the required ParticleRecycler to clean up after itself!", m_RipOutEffectPrefab, this);
	}

	void ModuleWeaponGun.INetComponent.Serialize(NetworkWriter writer)
	{
		byte value = (byte)(((m_ReelingTrigger.HasBeenSet ? 1 : 0) << 2) | ((m_ReturningTrigger.HasBeenSet ? 1 : 0) << 3) | ((m_ReleaseProjectileTrigger.HasBeenSet ? 1 : 0) << 4) | ((m_UntetheredTrigger.HasBeenSet ? 1 : 0) << 5));
		writer.Write(value);
	}

	void ModuleWeaponGun.INetComponent.Deserialize(NetworkReader reader)
	{
		byte b = reader.ReadByte();
		if (!ManNetwork.IsHost)
		{
			m_ReelingTrigger.Set((b & 4) != 0);
			m_ReturningTrigger.Set((b & 8) != 0);
			m_ReleaseProjectileTrigger.Set((b & 0x10) != 0);
			m_UntetheredTrigger.Set((b & 0x20) != 0);
		}
	}
}
