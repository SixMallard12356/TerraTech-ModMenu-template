#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class Projectile : WeaponRound, IWorldTreadmill, IGravityAdjustmentTarget, IGravityApplicationTarget
{
	[SerializeField]
	private float m_LifeTime = 10f;

	[SerializeField]
	private bool m_ExplodeAfterLifetime;

	[SerializeField]
	private bool m_DieAfterDelay = true;

	[SerializeField]
	private float m_DeathDelay;

	[SerializeField]
	private bool m_ExplodeOnTerrain;

	[SerializeField]
	private Transform m_Explosion;

	[SerializeField]
	[Tooltip("Whether this projectile allows multiple damage dealing collisions.")]
	private bool m_SingleImpact = true;

	[FormerlySerializedAs("rotateWithVelocity")]
	[SerializeField]
	private bool m_RotateWithVelocity;

	[SerializeField]
	private bool m_IgnoreCollisionWithBarrel;

	[SerializeField]
	private Transform m_StickImpactEffect;

	[SerializeField]
	private bool m_StickOnContact;

	[SerializeField]
	private bool m_StickOnTerrain;

	[SerializeField]
	private bool m_ExplodeOnStick;

	[SerializeField]
	private bool m_HideProjectileOnStick;

	[SerializeField]
	private GameObject m_ProjectileToHide;

	[SerializeField]
	private ManSFX.WeaponImpactSfxType m_ImpactSFXType;

	[SerializeField]
	private ManSFX.ProjectileFlightType m_FlightSFXType;

	[SerializeField]
	protected Transform m_TetherAnchorPoint;

	public Event<bool, Projectile, Visible> m_StickOnVisibleAttemptedEvent;

	public Event<bool, Projectile, Visible> m_StuckToVisibleEvent;

	public EventNoParams m_FiredEvent;

	public EventNoParams m_RecycledEvent;

	public Event<Damageable> m_CollisionEvent;

	private ModuleWeapon m_Weapon;

	private Transform m_FiringOrigin;

	private Vector3 m_FirePosition;

	private Vector3 m_LastFireDirection;

	private Vector3 m_LastFireSpin;

	private Vector3 m_OrigScale;

	private float m_GravityScale = 1f;

	private bool m_Stuck;

	private Visible m_VisibleStuckTo;

	private bool m_CanHaveGravity = true;

	private bool m_GravityAdjustmentTouched;

	private bool m_GravityApplicationTouched;

	private HashSet<Func<Visible, bool>> m_AdditionalStickConditions = new HashSet<Func<Visible, bool>>();

	private CollisionDetectionMode m_OriginalCollisionDetectionMode;

	[SerializeField]
	[HideInInspector]
	protected MeshRenderer m_MeshRenderer;

	[SerializeField]
	[HideInInspector]
	protected Collider m_Collider;

	[SerializeField]
	[HideInInspector]
	private SeekingProjectile m_SeekingProjectile;

	[HideInInspector]
	[SerializeField]
	private SwitchableUpdater m_RotationUpdater;

	[HideInInspector]
	[SerializeField]
	private TrailRenderer m_Trail;

	[HideInInspector]
	[SerializeField]
	private SmokeTrail m_Smoke;

	private int m_CollisionCount;

	private bool m_NeedsReturningToPool;

	public Tank Shooter { get; private set; }

	public Transform trans { get; private set; }

	public Rigidbody rbody { get; private set; }

	public SeekingProjectile SeekingProjectile => m_SeekingProjectile;

	public ManSFX.ProjectileFlightType FlightSFXType => m_FlightSFXType;

	public ModuleWeapon Weapon => m_Weapon;

	public Transform FiringOrigin => m_FiringOrigin;

	public Vector3 FirePosition => m_FirePosition;

	public bool Stuck => m_Stuck;

	public Visible VisibleStuckTo => m_VisibleStuckTo;

	public Transform TetherAnchorPoint => m_TetherAnchorPoint;

	public override void Fire(Vector3 fireDirection, Transform firingOrigin, FireData fireData, ModuleWeapon weapon, Tank shooter = null, bool seekingRounds = false, bool replayRounds = false)
	{
		m_Weapon = weapon;
		Shooter = shooter;
		m_FiringOrigin = firingOrigin;
		if (m_IgnoreCollisionWithBarrel && (bool)m_Collider)
		{
			m_Weapon.block.IgnoreCollision(m_Collider, ignore: true);
		}
		Vector3 v;
		if (!replayRounds)
		{
			v = fireDirection * fireData.m_MuzzleVelocity;
			if (fireData.m_ForceLegacyVariance)
			{
				v = v.RandomVariancePerAxis(fireData.m_BulletSprayVariance);
			}
			else
			{
				v *= 1f + UnityEngine.Random.Range(0f - fireData.m_MuzzleVelocityVarianceFactor, fireData.m_MuzzleVelocityVarianceFactor);
				v = v.RandomAngleOffset(fireData.m_MuzzleMaxAngleVarianceDegrees);
			}
			m_LastFireDirection = v;
		}
		else
		{
			v = m_LastFireDirection;
		}
		if (shooter != null)
		{
			v += shooter.rbody.velocity;
		}
		rbody.velocity = v;
		Vector3 angularVelocity = Vector3.zero;
		if (fireData.m_BulletSpin != 0f)
		{
			angularVelocity = (replayRounds ? m_LastFireSpin : (m_LastFireSpin = (Vector3.one * fireData.m_BulletSpin).RandomVariancePerAxis(1f)));
		}
		rbody.angularVelocity = angularVelocity;
		m_FirePosition = trans.position;
		if ((bool)m_SeekingProjectile)
		{
			m_SeekingProjectile.enabled = seekingRounds;
		}
		if (m_LifeTime > 0f)
		{
			SetPerishableAfterTimeout(m_LifeTime);
		}
		m_FiredEvent.Send();
	}

	public void RegisterStickCondition(Func<Visible, bool> stickCondition)
	{
		m_AdditionalStickConditions.Add(stickCondition);
	}

	public void UnRegisterStickCondition(Func<Visible, bool> stickCondition)
	{
		m_AdditionalStickConditions.Remove(stickCondition);
	}

	public override void GetVariationParameters(out Vector3 fireDirection, out Vector3 fireSpin)
	{
		fireDirection = m_LastFireDirection;
		fireSpin = m_LastFireSpin;
	}

	public override void SetVariationParameters(Vector3 fireDirection, Vector3 fireSpin)
	{
		m_LastFireDirection = fireDirection;
		m_LastFireSpin = fireSpin;
	}

	public virtual void HandleCollision(Damageable damageable, Vector3 hitPoint, Collider otherCollider, bool ForceDestroy)
	{
		if (!base.gameObject.activeInHierarchy || m_Stuck || (m_SingleImpact && m_CollisionCount > 0))
		{
			return;
		}
		if ((bool)damageable)
		{
			Singleton.Manager<ManDamage>.inst.DealDamage(damageable, m_Damage, m_DamageType, m_Weapon, Shooter, hitPoint, rbody.velocity);
			if (IsProjectileArmed() && !m_StickOnContact)
			{
				SpawnExplosion(hitPoint, damageable);
			}
		}
		else if (otherCollider.IsTerrain() || otherCollider.gameObject.layer == (int)Globals.inst.layerLandmark || (bool)otherCollider.GetComponentInParents<TerrainObject>(thisObjectFirst: true))
		{
			SpawnTerrainHitEffect(hitPoint);
			if (m_ExplodeOnTerrain && IsProjectileArmed())
			{
				SpawnExplosion(hitPoint);
			}
		}
		Singleton.Manager<ManSFX>.inst.PlayImpactSFX(Shooter, m_ImpactSFXType, damageable, hitPoint, otherCollider);
		if (m_SeekingProjectile != null && m_SeekingProjectile.enabled)
		{
			m_SeekingProjectile.enabled = false;
		}
		float deathDelay = GetDeathDelay();
		if ((DieAfterDelay() && deathDelay == 0f) || ForceDestroy)
		{
			this.Recycle(worldPosStays: false);
		}
		else if (DieAfterDelay() && m_CollisionCount == 0)
		{
			SetPerishableAfterTimeout(deathDelay);
			OnDelayedDeathSet();
		}
		TryStickToCollider(otherCollider, withVisuals: true, hitPoint);
		m_CollisionEvent.Send(damageable);
		m_CollisionCount++;
	}

	protected void ModifyProjectileVisibility(bool show)
	{
		if (m_ProjectileToHide != null)
		{
			m_ProjectileToHide.SetActive(show);
			return;
		}
		if ((bool)m_MeshRenderer)
		{
			m_MeshRenderer.enabled = show;
		}
		LineRenderer component = GetComponent<LineRenderer>();
		if ((bool)component)
		{
			component.enabled = show;
		}
	}

	protected virtual bool IsProjectileArmed()
	{
		return true;
	}

	protected virtual bool WillRotateWithVelocity()
	{
		return m_RotateWithVelocity;
	}

	protected bool DieAfterDelay()
	{
		return m_DieAfterDelay;
	}

	protected virtual float GetDeathDelay()
	{
		return m_DeathDelay;
	}

	protected virtual void OnDelayedDeathSet()
	{
	}

	protected void SetRotateWithVelocity(bool enable)
	{
		if ((bool)m_RotationUpdater)
		{
			m_RotationUpdater.enabled = enable;
		}
	}

	private void SpawnTerrainHitEffect(Vector3 hitPoint)
	{
		ManWorld.CachedBiomeBlendWeights biomeWeightsAtScenePosition = Singleton.Manager<ManWorld>.inst.GetBiomeWeightsAtScenePosition(hitPoint);
		Transform transform = (biomeWeightsAtScenePosition.Valid ? biomeWeightsAtScenePosition.Biome(0).GetImpactPrefab(base.name) : null);
		if ((bool)transform)
		{
			Quaternion rotation = Quaternion.identity;
			if (Singleton.Manager<ManWorld>.inst.TryProjectToGround(ref hitPoint))
			{
				rotation = Quaternion.LookRotation(Singleton.Manager<ManWorld>.inst.GetTerrainNormal(hitPoint));
			}
			transform.Spawn(Singleton.dynamicContainer, hitPoint, rotation);
		}
	}

	private void SpawnExplosion(Vector3 explodePos, Damageable directHitTarget = null)
	{
		if ((bool)m_Explosion)
		{
			Explosion component = m_Explosion.Spawn(Singleton.dynamicContainer, explodePos).GetComponent<Explosion>();
			if (component != null)
			{
				component.SetDamageSource(Shooter);
				component.SetDirectHitTarget(directHitTarget);
			}
		}
	}

	public bool TryStickToCollider(Collider collider, bool withVisuals, Vector3 vfxPoint, bool allowRestick = false)
	{
		if (!m_StickOnContact)
		{
			return false;
		}
		Visible visible = Singleton.Manager<ManVisible>.inst.FindVisible(collider);
		bool flag = GetCanStickToCollider(collider, visible);
		if (flag)
		{
			if (withVisuals)
			{
				StickToObjectWithVisuals(collider.transform, visible, vfxPoint, allowRestick);
			}
			else
			{
				StickToObject(collider.transform, visible, allowRestick);
			}
		}
		m_StickOnVisibleAttemptedEvent.Send(flag, this, visible);
		return flag;
		bool GetCanStickToCollider(Collider _collider, Visible _visibleToStickTo)
		{
			if ((_collider.IsTerrain() || _collider.gameObject.layer == (int)Globals.inst.layerLandmark || (bool)_collider.GetComponentInParents<TerrainObject>(thisObjectFirst: true)) && !m_StickOnTerrain)
			{
				return false;
			}
			foreach (Func<Visible, bool> additionalStickCondition in m_AdditionalStickConditions)
			{
				if (!additionalStickCondition(_visibleToStickTo))
				{
					return false;
				}
			}
			return true;
		}
	}

	public void StickToObjectWithVisuals(Transform stickTargetTrans, Visible stickTargetVis, Vector3 vfxPoint, bool allowRestick = false)
	{
		StickToObject(stickTargetTrans, stickTargetVis, allowRestick);
		if (m_HideProjectileOnStick)
		{
			ModifyProjectileVisibility(show: false);
		}
		SetTrailsEnabled(state: false);
		if (m_ExplodeOnStick)
		{
			Damageable directHitTarget = (m_VisibleStuckTo.IsNotNull() ? m_VisibleStuckTo.damageable : null);
			SpawnExplosion(vfxPoint, directHitTarget);
		}
		if (m_StickImpactEffect.IsNotNull())
		{
			m_StickImpactEffect.Spawn(Singleton.dynamicContainer, vfxPoint);
		}
	}

	public void StickToObject(Transform stickTargetTrans, Visible stickTargetVis, bool allowRestick = false)
	{
		SetStuckToObject(stuck: true, stickTargetTrans, stickTargetVis, allowRestick);
	}

	public void UnstickFromObject(bool impartParentVelocityOnUnstick = true)
	{
		SetStuckToObject(stuck: false, null, null, impartParentVelocityOnUnstick);
	}

	private void SetStuckToObject(bool stuck, Transform stickTargetTrans = null, Visible stickTargetVis = null, bool impartParentVelocityOnUnstick = true, bool allowRestick = false)
	{
		if (stuck == m_Stuck && !allowRestick)
		{
			return;
		}
		Transform transform = stickTargetTrans ?? (stickTargetVis ? stickTargetVis.trans : null);
		Transform transform2 = (stuck ? transform : null);
		Rigidbody rigidbody = null;
		if (!stuck && impartParentVelocityOnUnstick)
		{
			rigidbody = ((!(m_VisibleStuckTo != null)) ? null : ((!(m_VisibleStuckTo.block != null) || !(m_VisibleStuckTo.block.tank != null)) ? m_VisibleStuckTo.rbody : m_VisibleStuckTo.block.tank.rbody));
		}
		if (m_Stuck && m_VisibleStuckTo.IsNotNull())
		{
			m_VisibleStuckTo.RecycledEvent.Unsubscribe(OnParentRecycled);
			if (m_VisibleStuckTo.resdisp.IsNotNull())
			{
				m_VisibleStuckTo.resdisp.ResourceGiverDamageStageChangingEvent.Unsubscribe(OnRespDispDamageStageChanging);
			}
			if (m_VisibleStuckTo.damageable.IsNotNull())
			{
				m_VisibleStuckTo.damageable.deathEvent.Unsubscribe(OnParentDestroyed);
			}
		}
		if (stuck)
		{
			m_OrigScale = trans.localScale;
		}
		trans.SetParent(transform2, worldPositionStays: true);
		if (stuck)
		{
			trans.localScale = Vector3.Scale(m_OrigScale, new Vector3(1f / transform2.lossyScale.x, 1f / transform2.lossyScale.y, 1f / transform2.lossyScale.z));
		}
		else
		{
			trans.localScale = m_OrigScale;
		}
		if (stuck)
		{
			if (stickTargetVis.IsNotNull())
			{
				stickTargetVis.RecycledEvent.Subscribe(OnParentRecycled);
				if (stickTargetVis.resdisp.IsNotNull())
				{
					stickTargetVis.resdisp.ResourceGiverDamageStageChangingEvent.Subscribe(OnRespDispDamageStageChanging);
				}
				if (stickTargetVis.damageable.IsNotNull())
				{
					stickTargetVis.damageable.deathEvent.Subscribe(OnParentDestroyed);
				}
			}
			rbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
			rbody.isKinematic = true;
		}
		else
		{
			rbody.isKinematic = false;
			rbody.collisionDetectionMode = m_OriginalCollisionDetectionMode;
			if (impartParentVelocityOnUnstick && rigidbody != null)
			{
				Vector3 pointVelocity = rigidbody.GetPointVelocity(trans.position);
				rbody.velocity = pointVelocity;
			}
		}
		rbody.detectCollisions = !stuck;
		m_Stuck = stuck;
		m_VisibleStuckTo = (stuck ? stickTargetVis : null);
		m_StuckToVisibleEvent.Send(m_Stuck, this, m_VisibleStuckTo);
	}

	private void SetPerishableAfterTimeout(float timeout)
	{
		ManCombat.Projectiles.RegisterPerishableProjectile(this, timeout, OnLifetimeEnd);
	}

	private void SetTrailsEnabled(bool state, bool reset = false)
	{
		if (m_Smoke != null)
		{
			m_Smoke.enabled = state;
		}
		if (m_Trail != null)
		{
			m_Trail.emitting = state;
		}
		if (state)
		{
			Singleton.Manager<ManWorldTreadmill>.inst.AddListener(this);
			return;
		}
		if (m_Smoke != null)
		{
			m_Smoke.Reset();
		}
		Singleton.Manager<ManWorldTreadmill>.inst.RemoveListener(this);
		if (reset && m_Trail != null)
		{
			m_Trail.Clear();
		}
	}

	private void OnParentRecycled(Visible vis)
	{
		UnstickFromObject();
	}

	private void OnRespDispDamageStageChanging(ResourceDispenser respDisp)
	{
		UnstickFromObject();
	}

	private void OnParentDestroyed(Damageable damageable, ManDamage.DamageInfo damageInfo)
	{
		UnstickFromObject();
	}

	private void OnUpdateRotation()
	{
		if (!rbody.velocity.magnitude.Approximately(0f, 0.1f))
		{
			trans.rotation = Quaternion.LookRotation(rbody.velocity);
		}
	}

	protected virtual void OnLifetimeEnd()
	{
		d.Assert(base.gameObject.activeInHierarchy);
		if (m_ExplodeAfterLifetime)
		{
			SpawnExplosion(trans.position);
		}
		Destroy();
	}

	private void Destroy()
	{
		this.Recycle();
	}

	private void PrePool()
	{
		m_Collider = (from go in base.gameObject.EnumerateHierarchy()
			select go.GetComponent<Collider>()).SingleOrDefault((Collider c) => c != null);
		d.Assert(m_Collider != null, "Projectile '" + base.name + "' has no collider");
		m_MeshRenderer = GetComponentInChildren<MeshRenderer>();
		m_SeekingProjectile = GetComponent<SeekingProjectile>();
		m_Trail = GetComponent<TrailRenderer>();
		m_Smoke = GetComponent<SmokeTrail>();
		if (WillRotateWithVelocity())
		{
			m_RotationUpdater = base.gameObject.AddComponent<SwitchableUpdater>();
		}
	}

	private void OnPool()
	{
		if (m_TetherAnchorPoint == null)
		{
			m_TetherAnchorPoint = base.transform;
		}
		m_NeedsReturningToPool = false;
		trans = base.transform;
		rbody = GetComponent<Rigidbody>();
		rbody.mass = 0.00123f;
		if ((bool)m_RotationUpdater)
		{
			m_RotationUpdater.FixedUpdateEvent.Subscribe(OnUpdateRotation);
		}
		m_CanHaveGravity = rbody.useGravity;
		m_OriginalCollisionDetectionMode = rbody.collisionDetectionMode;
	}

	private void OnSpawn()
	{
		m_CollisionCount = 0;
		if ((bool)m_Collider)
		{
			m_Collider.enabled = true;
		}
		SetRotateWithVelocity(m_RotateWithVelocity);
		SetTrailsEnabled(state: true);
		rbody.useGravity = m_CanHaveGravity;
		if ((bool)m_ProjectileToHide)
		{
			m_ProjectileToHide.SetActive(value: true);
		}
		m_NeedsReturningToPool = true;
	}

	private void OnRecycle()
	{
		UnstickFromObject(impartParentVelocityOnUnstick: false);
		m_NeedsReturningToPool = false;
		Shooter = null;
		SetTrailsEnabled(state: false, reset: true);
		m_RecycledEvent.Send();
		m_RecycledEvent.EnsureNoSubscribers();
	}

	private void OnApplicationQuit()
	{
		m_NeedsReturningToPool = false;
	}

	private void OnDisable()
	{
		if (m_NeedsReturningToPool)
		{
			d.LogError("Projectile " + base.gameObject.name + " being disabled before recycle! This shouldn't happen. Cleaning it up safely.");
			this.Recycle();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		ContactPoint[] contacts = collision.contacts;
		if (contacts.Length != 0)
		{
			ContactPoint contactPoint = contacts[0];
			HandleCollision(contactPoint.otherCollider.GetComponentInParents<Damageable>(thisObjectFirst: true), contactPoint.point, collision.collider, ForceDestroy: false);
		}
	}

	public void OnMoveWorldOrigin(IntVector3 amountToMove)
	{
		if ((bool)m_Trail && m_Trail.enabled && m_Trail.positionCount > 0)
		{
			Vector3 vector = new Vector3(amountToMove.x, amountToMove.y, amountToMove.z);
			Vector3[] array = new Vector3[m_Trail.positionCount];
			m_Trail.GetPositions(array);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] += vector;
			}
			m_Trail.SetPositions(array);
		}
	}

	public void ResetGravityAdjustment()
	{
		m_GravityScale = 1f;
	}

	public void PrimeForGravityAdjustment()
	{
		m_GravityScale = 1f;
	}

	public float AdjustGravity(GravityManipulationZone zone)
	{
		float gravityScale = m_GravityScale;
		m_GravityScale += zone.m_ManipulationAmount;
		m_GravityScale = Mathf.Max(m_GravityScale, 0f);
		return gravityScale - m_GravityScale;
	}

	public void FinaliseGravityAdjustment()
	{
	}

	public IGravityApplicationTarget GetGravityApplicationTarget()
	{
		return this;
	}

	public void SetAdjustmentTouched(bool touched)
	{
		m_GravityAdjustmentTouched = touched;
	}

	public bool HasAdjustmentBeenTouched()
	{
		return m_GravityAdjustmentTouched;
	}

	public float GetGravityScale()
	{
		return m_GravityScale;
	}

	public Rigidbody GetApplicationRigidbody()
	{
		return rbody;
	}

	public Vector3 GetWorldCentreOfGravity()
	{
		return rbody.worldCenterOfMass;
	}

	public bool CanApplyGravity()
	{
		return m_CanHaveGravity;
	}

	public void SetApplicationTouched(bool touched)
	{
		m_GravityApplicationTouched = touched;
	}

	public bool HasApplicationBeenTouched()
	{
		return m_GravityApplicationTouched;
	}
}
