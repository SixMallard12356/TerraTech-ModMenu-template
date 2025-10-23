#define UNITY_EDITOR
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class CannonBarrel : MonoBehaviour
{
	[FormerlySerializedAs("m_Particles")]
	public ParticleSystem[] particles;

	[FormerlySerializedAs("m_MuzzleFlash")]
	public MuzzleFlash muzzleFlash;

	[FormerlySerializedAs("m_CasingEjectTransform")]
	public Transform casingEjectTransform;

	[FormerlySerializedAs("m_ProjectileSpawnPoint")]
	public Transform projectileSpawnPoint;

	public Transform recoiler;

	[FormerlySerializedAs("m_Reeler")]
	[SerializeField]
	protected Reeler reeler;

	[SerializeField]
	public Spinner m_FireSpinner;

	[SerializeField]
	public bool m_ShowParticlesOnAllQualitySettings;

	public BeamWeapon beamWeapon;

	private TankBlock parentBlock;

	private bool recoiling;

	private AnimationState animState;

	private Animation recoilAnim;

	private FireData m_FiringData;

	private ModuleWeapon m_Weapon;

	private ModuleWeaponGun m_Gun;

	private bool m_CachedHasClearLineOfFire;

	private int m_CachedClearLineOfFireFrameIndex = -1;

	private const float kCasingMaxZ = 50f;

	private const int kMaxCasingPerFrame = 2;

	private static int s_LastCasingFrame;

	private static int s_CasingCount;

	public float Range
	{
		get
		{
			if (!beamWeapon)
			{
				return float.MaxValue;
			}
			return beamWeapon.Range;
		}
	}

	public Transform trans { get; private set; }

	public void InitOnGun(ModuleWeaponGun gun, FireData firingData, ModuleWeapon weapon)
	{
		m_Gun = gun;
		m_FiringData = firingData;
		m_Weapon = weapon;
		reeler?.InitOnGunBarrel(m_Gun, this, weapon);
	}

	public bool HasClearLineOfFire()
	{
		int frameCount = Time.frameCount;
		if (m_CachedClearLineOfFireFrameIndex == frameCount)
		{
			return m_CachedHasClearLineOfFire;
		}
		if ((object)parentBlock.tank == null)
		{
			return true;
		}
		bool flag = true;
		float num = Mathf.Max(parentBlock.tank.blockBounds.size.magnitude, 1f);
		Vector3 position = projectileSpawnPoint.position;
		if (Physics.Raycast(position, projectileSpawnPoint.forward, out var hitInfo, num, Globals.inst.layerTank.mask, QueryTriggerInteraction.Ignore) && hitInfo.rigidbody == parentBlock.tank.rbody)
		{
			flag = false;
		}
		if (flag)
		{
			Vector3 vector = parentBlock.tank.trans.InverseTransformPoint(position);
			TankBlock blockAtPosition = parentBlock.tank.blockman.GetBlockAtPosition(vector);
			if ((bool)blockAtPosition && blockAtPosition != parentBlock)
			{
				ColliderSwapper.ColliderSwapperEntry[] allColliders = blockAtPosition.visible.ColliderSwapper.AllColliders;
				for (int i = 0; i < allColliders.Length; i++)
				{
					ColliderSwapper.ColliderSwapperEntry colliderSwapperEntry = allColliders[i];
					if (!colliderSwapperEntry.collisionWhenAttached)
					{
						continue;
					}
					Collider collider = colliderSwapperEntry.collider;
					if (collider.bounds.Contains(position))
					{
						Ray ray = new Ray(position - trans.up * num, trans.up);
						if (collider.Raycast(ray, out hitInfo, num))
						{
							flag = false;
							break;
						}
					}
				}
			}
		}
		m_CachedClearLineOfFireFrameIndex = frameCount;
		m_CachedHasClearLineOfFire = flag;
		return flag;
	}

	public bool OnClientFire(Vector3 projectileSpawnPoint_forward, Vector3 spin, bool seeking, int projectileUID)
	{
		if ((bool)m_FiringData.m_BulletPrefab)
		{
			WeaponRound weaponRound = m_FiringData.m_BulletPrefab.Spawn(Singleton.dynamicContainer, projectileSpawnPoint.position, trans.rotation);
			weaponRound.SetVariationParameters(projectileSpawnPoint_forward, spin);
			if (reeler != null && weaponRound is Projectile projectile)
			{
				reeler.TetherToProjectile(projectile);
			}
			weaponRound.Fire(Vector3.zero, projectileSpawnPoint, m_FiringData, m_Weapon, parentBlock.tank, seeking, replayRounds: true);
			ManCombat.Projectiles.RegisterWeaponRound(weaponRound, projectileUID);
		}
		return ProcessFire();
	}

	public bool PrepareFiring(bool prepareFiring)
	{
		bool flag = true;
		if (m_FireSpinner != null)
		{
			m_FireSpinner.SetAutoSpin(prepareFiring);
			return m_FireSpinner.AtFullSpeed;
		}
		return prepareFiring;
	}

	public bool Fire(bool seeking)
	{
		bool flag = false;
		d.Assert(parentBlock.tank != null);
		NetTech netTech = parentBlock.tank.netTech;
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && netTech.IsNotNull())
		{
			if (netTech.NetPlayer.IsNotNull())
			{
				flag = netTech.NetPlayer.IsActuallyLocalPlayer;
			}
			else if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				flag = true;
			}
			if (!flag && !beamWeapon)
			{
				return false;
			}
		}
		if (recoiling && !recoilAnim.isPlaying)
		{
			d.Log(parentBlock.name + " barrel recoil anim completed without sending recoil-return event");
			recoiling = false;
		}
		if (recoiling || !parentBlock.tank)
		{
			return false;
		}
		if (reeler != null && reeler.HasFired)
		{
			return false;
		}
		if ((bool)m_FiringData.m_BulletPrefab)
		{
			Vector3 position = projectileSpawnPoint.position;
			Vector3 forward = projectileSpawnPoint.forward;
			WeaponRound weaponRound = m_FiringData.m_BulletPrefab.Spawn(Singleton.dynamicContainer, position, trans.rotation);
			if (reeler != null && weaponRound is Projectile projectile)
			{
				reeler.TetherToProjectile(projectile);
			}
			weaponRound.Fire(forward, projectileSpawnPoint, m_FiringData, m_Weapon, parentBlock.tank, seeking);
			ManCombat.Projectiles.RegisterWeaponRound(weaponRound);
			Vector3 force = -forward * m_FiringData.m_KickbackStrength;
			parentBlock.tank.rbody.AddForceAtPosition(force, position, ForceMode.Impulse);
			if (flag)
			{
				weaponRound.GetVariationParameters(out var fireDirection, out var fireSpin);
				parentBlock.tank.Weapons.QueueProjectileLaunch(this, weaponRound.ShortlivedUID, fireDirection, fireSpin, m_Weapon, seeking);
			}
		}
		return ProcessFire();
	}

	private bool ProcessFire()
	{
		if ((bool)beamWeapon)
		{
			beamWeapon.SetActive(muzzleFlash);
		}
		else if ((bool)muzzleFlash)
		{
			muzzleFlash.Fire();
		}
		if ((!QualitySettingsExtended.DisableWeaponFireParticles || m_ShowParticlesOnAllQualitySettings) && particles != null)
		{
			for (int i = 0; i < particles.Length; i++)
			{
				particles[i].Play();
			}
		}
		if ((bool)recoilAnim)
		{
			recoiling = true;
			if (recoilAnim.isPlaying)
			{
				recoilAnim.Rewind();
			}
			else
			{
				recoilAnim.Play();
			}
		}
		else
		{
			EjectCasing();
		}
		return true;
	}

	public float GetFireRateFraction()
	{
		float result = 1f;
		if (m_FireSpinner != null)
		{
			result = m_FireSpinner.SpeedFraction;
		}
		return result;
	}

	private void OnRecoilMax()
	{
		if (recoiling)
		{
			EjectCasing();
		}
	}

	private void EjectCasing()
	{
		QualitySettingsExtended.CasingSpawnMode shellCasingSpawnMode = QualitySettingsExtended.ShellCasingSpawnMode;
		if (shellCasingSpawnMode == QualitySettingsExtended.CasingSpawnMode.None || !m_FiringData.m_BulletCasingPrefab || !(parentBlock.tank != null))
		{
			return;
		}
		bool flag = shellCasingSpawnMode == QualitySettingsExtended.CasingSpawnMode.Throttled;
		bool flag2 = true;
		if (flag)
		{
			int frameCount = Time.frameCount;
			if (s_LastCasingFrame == frameCount)
			{
				if (s_CasingCount >= 2)
				{
					flag2 = false;
				}
			}
			else
			{
				s_LastCasingFrame = frameCount;
				s_CasingCount = 0;
			}
		}
		if (flag2)
		{
			Vector3 position = casingEjectTransform.position;
			if (flag)
			{
				Vector3 vector = Singleton.camera.WorldToViewportPoint(position);
				flag2 = vector.x >= 0f && vector.y >= 0f && vector.x <= 1f && vector.y <= 1f && vector.z > 0f && vector.z < 50f;
			}
			if (flag2)
			{
				m_FiringData.m_BulletCasingPrefab.Spawn(Singleton.dynamicContainer, position).Eject(casingEjectTransform.forward, m_FiringData, parentBlock.tank);
				s_CasingCount++;
			}
		}
	}

	private void OnRecoilReturn()
	{
		recoiling = false;
	}

	public void CapRecoilDuration(float shotCooldown)
	{
		if ((bool)animState && animState.length > shotCooldown)
		{
			animState.speed = animState.length / shotCooldown;
		}
	}

	private void OnDetaching()
	{
		if (m_FireSpinner != null)
		{
			m_FireSpinner.SetAutoSpin(enableAutoSpin: false);
		}
	}

	private void PrePool()
	{
	}

	private void OnPool()
	{
		recoiling = false;
		if ((bool)recoiler)
		{
			recoilAnim = recoiler.GetComponentInChildren<Animation>(includeInactive: true);
			if ((bool)recoilAnim)
			{
				foreach (AnimationState item in recoilAnim)
				{
					if (animState != null)
					{
						d.LogError(string.Format("{0} (base anim {1}) contains additional animation {2}", animState.name, item.name));
					}
					else
					{
						animState = item;
					}
				}
			}
			AnimEvent componentInChildren = recoiler.GetComponentInChildren<AnimEvent>(includeInactive: true);
			if ((bool)componentInChildren)
			{
				componentInChildren.HandleEvent.Subscribe(delegate(int i)
				{
					if (i == 1)
					{
						OnRecoilMax();
					}
					else
					{
						OnRecoilReturn();
					}
				});
			}
		}
		parentBlock = this.GetComponentInParents<TankBlock>();
		trans = base.transform;
		parentBlock.DetachingEvent.Subscribe(OnDetaching);
	}

	private void OnSpawn()
	{
		if ((bool)muzzleFlash)
		{
			muzzleFlash.gameObject.SetActive(value: false);
		}
	}

	private void OnRecycle()
	{
		if (recoilAnim != null && animState != null && recoilAnim.isPlaying)
		{
			animState.enabled = true;
			animState.normalizedTime = 1f;
			recoilAnim.Sample();
			animState.enabled = false;
			recoilAnim.Stop();
			recoiling = false;
		}
	}
}
