#define UNITY_EDITOR
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(FireData))]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
[RequireComponent(typeof(ModuleWeapon))]
public class ModuleWeaponGun : Module, IModuleWeapon, IModuleDamager, TechAudio.IModuleAudioProvider, INetworkedModule
{
	public enum FireControlMode
	{
		Sequenced,
		AllAtOnce
	}

	public interface INetComponent
	{
		void Serialize(NetworkWriter writer);

		void Deserialize(NetworkReader reader);
	}

	[SerializeField]
	public float m_ShotCooldown = 1f;

	[SerializeField]
	public float m_CooldownVariancePct = 0.05f;

	[SerializeField]
	public FireControlMode m_FireControlMode;

	[SerializeField]
	public int m_BurstShotCount;

	[SerializeField]
	public float m_BurstCooldown;

	[SerializeField]
	public bool m_ResetBurstOnInterrupt = true;

	[SerializeField]
	public bool m_SeekingRounds;

	[SerializeField]
	public float m_RegisterWarningAfter = 1f;

	[SerializeField]
	public float m_ResetFiringTAfterNotFiredFor = 1f;

	[SerializeField]
	public bool m_HasSpinUpDownAnim;

	[SerializeField]
	public bool m_HasCooldownAnim;

	[SerializeField]
	public bool m_CanInterruptSpinUpAnim;

	[SerializeField]
	public bool m_CanInterruptSpinDownAnim;

	[SerializeField]
	public int m_SpinUpAnimLayerIndex;

	[SerializeField]
	public TechAudio.SFXType m_DeploySFXType;

	[SerializeField]
	public float m_OverheatTime;

	[SerializeField]
	public float m_OverheatPauseWindow;

	[SerializeField]
	public bool m_DisableMainAudioLoop;

	[SerializeField]
	public float m_AudioLoopDelay;

	private FireData m_FiringData;

	private INetComponent[] m_NetComponents;

	private NetworkedProperty<ByteArrayBlockMessage> net_NetComponentsData;

	private CannonBarrel[] m_CannonBarrels;

	private int m_NumCannonBarrels;

	private Transform m_BarrelTransform;

	private ModuleWeapon m_WeaponModule;

	private Animator m_Animator;

	private static readonly int m_AnimatorCoolingDownHash = Animator.StringToHash("CoolingDown");

	private static readonly int m_AnimatorCooldownRemainingHash = Animator.StringToHash("CooldownRemaining");

	private static readonly int m_AnimatorOverheatedHash = Animator.StringToHash("Overheated");

	private static readonly int m_AnimatorOverheatHash = Animator.StringToHash("Overheat");

	private float m_ShotTimer;

	private int m_NextBarrelToFire;

	private float m_IdleTime;

	private float m_FailedToFireTime;

	private int m_BurstShotsRemaining;

	private bool m_DeployingState;

	private float m_Overheat;

	private float m_OverheatPause;

	private float m_DetachTimeStamp;

	private bool _m_NetComponentsDirty;

	public TechAudio.SFXType SFXType => m_DeploySFXType;

	ManDamage.DamageType IModuleDamager.DamageType
	{
		get
		{
			if (!(FiringData != null) || !(FiringData.m_BulletPrefab != null))
			{
				return GetBeamWeaponDamageTypeOrDefault();
			}
			return FiringData.m_BulletPrefab.DamageType;
		}
	}

	private FireData FiringData
	{
		get
		{
			if (m_FiringData == null)
			{
				m_FiringData = GetComponent<FireData>();
			}
			return m_FiringData;
		}
	}

	public event Action<TechAudio.AudioTickData, FMODEvent.FMODParams> OnAudioTickUpdate;

	public int GetNumCannonBarrels()
	{
		return m_NumCannonBarrels;
	}

	public int GetCannonBarrelIndex(CannonBarrel cannonBarrel)
	{
		for (int i = 0; i < m_NumCannonBarrels; i++)
		{
			if (m_CannonBarrels[i] == cannonBarrel)
			{
				return i;
			}
		}
		return -1;
	}

	public CannonBarrel FindCannonBarrelFromIndex(int index)
	{
		d.Assert(index < m_NumCannonBarrels);
		return m_CannonBarrels[index];
	}

	public bool UpdateDeployment(bool shouldDeploy)
	{
		bool flag = true;
		float adsrTime = 1f;
		if (m_HasSpinUpDownAnim && m_Animator != null)
		{
			AnimatorStateInfo currentAnimatorStateInfo = m_Animator.GetCurrentAnimatorStateInfo(m_SpinUpAnimLayerIndex);
			bool flag2 = m_OverheatTime > 0f && m_Overheat == m_OverheatTime;
			if (shouldDeploy && !flag2 && (m_DeployingState || m_CanInterruptSpinDownAnim || currentAnimatorStateInfo.shortNameHash == ModuleWeapon.k_UniversalAnimator_UnDeployedState_Hash))
			{
				m_DeployingState = true;
				m_Animator.SetBool(ModuleWeapon.k_UniversalAnimator_DeployingState_Hash, value: true);
				if (!currentAnimatorStateInfo.IsName("Up"))
				{
					flag = false;
					adsrTime = 0f;
				}
				else
				{
					m_Overheat = Mathf.Min(m_OverheatTime, m_Overheat + Time.deltaTime);
				}
				m_OverheatPause = 0f;
			}
			else if (!m_DeployingState || flag2 || m_CanInterruptSpinUpAnim || currentAnimatorStateInfo.shortNameHash == ModuleWeapon.k_UniversalAnimator_DeployedState_Hash)
			{
				m_OverheatPause = Mathf.Min(m_OverheatPauseWindow, m_OverheatPause + Time.deltaTime);
				if (!flag2 && m_OverheatPause < m_OverheatPauseWindow)
				{
					m_Overheat = Mathf.Max(0f, m_Overheat - Time.deltaTime);
				}
				else
				{
					m_DeployingState = false;
					m_Animator.SetBool(ModuleWeapon.k_UniversalAnimator_DeployingState_Hash, value: false);
					flag = false;
					if (currentAnimatorStateInfo.shortNameHash == ModuleWeapon.k_UniversalAnimator_UnDeployedState_Hash)
					{
						adsrTime = 0f;
						m_Overheat = 0f;
						flag2 = false;
					}
					else
					{
						adsrTime = 0.99f;
					}
				}
			}
			if (m_OverheatTime > 0f)
			{
				if (m_Overheat == m_OverheatTime)
				{
					flag = false;
					flag2 = true;
				}
				m_Animator.SetBool(m_AnimatorOverheatedHash, flag2);
				m_Animator.SetFloat(m_AnimatorOverheatHash, m_Overheat / m_OverheatTime);
			}
		}
		if ((bool)m_WeaponModule)
		{
			m_WeaponModule.DisableTargetAimer = !flag;
		}
		if (this.OnAudioTickUpdate != null)
		{
			bool flag3 = m_DeployingState;
			if (flag)
			{
				flag3 &= !m_DisableMainAudioLoop;
			}
			TechAudio.AudioTickData value = TechAudio.AudioTickData.ConfigureLoopedADSR(this, m_DeploySFXType, flag3, adsrTime, 0f, 0);
			this.OnAudioTickUpdate.Send(value, FMODEvent.FMODParams.empty);
		}
		return flag;
	}

	public bool PrepareFiring(bool prepareFiring)
	{
		bool result = true;
		for (int i = 0; i < m_NumCannonBarrels; i++)
		{
			if (!m_CannonBarrels[i].PrepareFiring(prepareFiring))
			{
				result = false;
			}
		}
		return result;
	}

	public int ProcessFiring(bool firing)
	{
		float num = Time.deltaTime;
		if (m_ShotTimer > 0f)
		{
			if (num > 0.04f)
			{
				if (num > 1f / 12f)
				{
					num = 0f;
				}
				else
				{
					float num2 = Mathf.InverseLerp(1f / 12f, 0.04f, num);
					num *= num2;
				}
			}
			m_ShotTimer -= num;
			if (m_HasCooldownAnim && m_Animator.IsNotNull())
			{
				m_Animator.SetFloat(m_AnimatorCooldownRemainingHash, m_ShotTimer);
			}
		}
		else if (m_HasCooldownAnim && m_Animator.IsNotNull())
		{
			m_Animator.SetBool(m_AnimatorCoolingDownHash, value: false);
		}
		int num3 = 0;
		if (firing)
		{
			m_IdleTime = 0f;
			bool flag = true;
			if (m_FireControlMode == FireControlMode.AllAtOnce)
			{
				for (int i = 0; i < m_NumCannonBarrels; i++)
				{
					if (m_CannonBarrels[i].HasClearLineOfFire())
					{
						if (m_CannonBarrels[i].Fire(m_SeekingRounds))
						{
							num3++;
						}
					}
					else
					{
						flag = false;
					}
				}
			}
			else
			{
				if (m_CannonBarrels[m_NextBarrelToFire].HasClearLineOfFire())
				{
					if (m_CannonBarrels[m_NextBarrelToFire].Fire(m_SeekingRounds))
					{
						num3++;
					}
				}
				else
				{
					flag = false;
				}
				if (num3 > 0)
				{
					m_NextBarrelToFire = ((m_NextBarrelToFire != m_NumCannonBarrels - 1) ? (m_NextBarrelToFire + 1) : 0);
				}
			}
			if (num3 > 0)
			{
				if (m_BurstShotCount > 0)
				{
					m_BurstShotsRemaining -= num3;
				}
				bool flag2 = m_BurstShotCount > 0 && m_BurstShotsRemaining <= 0;
				m_ShotTimer = (flag2 ? m_BurstCooldown : ((float)(num3 - 1) * m_ShotCooldown + m_ShotCooldown.RandomVariance(m_CooldownVariancePct)));
				if (m_HasCooldownAnim && m_Animator.IsNotNull())
				{
					m_Animator.SetBool(m_AnimatorCoolingDownHash, value: true);
				}
				if (flag2)
				{
					m_BurstShotsRemaining = m_BurstShotCount;
				}
				if (flag)
				{
					m_FailedToFireTime = 0f;
				}
			}
			if (m_FailedToFireTime > m_RegisterWarningAfter)
			{
				m_FailedToFireTime = 0f;
			}
			if (!flag)
			{
				m_FailedToFireTime += num;
			}
		}
		else
		{
			if (m_ResetBurstOnInterrupt && !m_WeaponModule.FireRequested && m_BurstShotsRemaining != m_BurstShotCount)
			{
				m_BurstShotsRemaining = m_BurstShotCount;
				m_ShotTimer = m_BurstCooldown;
				if (m_HasCooldownAnim && m_Animator.IsNotNull())
				{
					m_Animator.SetBool(m_AnimatorCoolingDownHash, value: true);
				}
			}
			m_IdleTime += num;
			if (m_IdleTime >= m_ResetFiringTAfterNotFiredFor)
			{
				m_FailedToFireTime = 0f;
			}
		}
		return num3;
	}

	public bool ReadyToFire()
	{
		if (m_ShotTimer <= Mathf.Epsilon)
		{
			return m_NumCannonBarrels > 0;
		}
		return false;
	}

	public bool FiringObstructed()
	{
		bool result = false;
		for (int i = 0; i < m_NumCannonBarrels; i++)
		{
			if (!m_CannonBarrels[i].HasClearLineOfFire())
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public float GetVelocity()
	{
		return FiringData.m_MuzzleVelocity;
	}

	public float GetRange()
	{
		return m_CannonBarrels[0].Range;
	}

	public bool IsAimingAtFloor(float limitedAngle)
	{
		bool result = false;
		for (int i = 0; i < m_CannonBarrels.Length; i++)
		{
			if (Vector3.Angle(m_CannonBarrels[i].trans.forward, -Vector3.up) < limitedAngle)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public bool AimWithTrajectory()
	{
		bool result = false;
		if ((bool)FiringData.m_BulletPrefab)
		{
			Rigidbody component = FiringData.m_BulletPrefab.GetComponent<Rigidbody>();
			result = component != null && component.useGravity;
		}
		return result;
	}

	public Transform GetFireTransform()
	{
		return m_BarrelTransform;
	}

	public float GetFireRateFraction()
	{
		float num = 0f;
		if (m_NumCannonBarrels > 0)
		{
			for (int i = 0; i < m_NumCannonBarrels; i++)
			{
				num += m_CannonBarrels[i].GetFireRateFraction();
			}
			num /= (float)m_NumCannonBarrels;
		}
		return num;
	}

	private ManDamage.DamageType GetBeamWeaponDamageTypeOrDefault()
	{
		if (m_CannonBarrels == null || m_CannonBarrels.Length == 0)
		{
			m_CannonBarrels = GetComponentsInChildren<CannonBarrel>(includeInactive: true);
		}
		int num = 0;
		while (m_CannonBarrels != null && num < m_CannonBarrels.Length)
		{
			if (m_CannonBarrels[num].beamWeapon != null)
			{
				return m_CannonBarrels[num].beamWeapon.DamageType;
			}
			num++;
		}
		return ManDamage.DamageType.Standard;
	}

	float IModuleDamager.GetHitDamage()
	{
		if (FiringData != null && FiringData.m_BulletPrefab != null)
		{
			return FiringData.m_BulletPrefab.Damage;
		}
		if (m_CannonBarrels == null || m_CannonBarrels.Length == 0)
		{
			m_CannonBarrels = GetComponentsInChildren<CannonBarrel>(includeInactive: true);
		}
		float num = 0f;
		int num2 = 0;
		while (m_CannonBarrels != null && num2 < m_CannonBarrels.Length)
		{
			if (m_CannonBarrels[num2].beamWeapon != null)
			{
				num += m_CannonBarrels[num2].beamWeapon.DamagePerSecond;
			}
			num2++;
		}
		return num / 30f;
	}

	float IModuleDamager.GetHitsPerSec()
	{
		if (m_CannonBarrels == null || m_CannonBarrels.Length == 0)
		{
			m_CannonBarrels = GetComponentsInChildren<CannonBarrel>(includeInactive: true);
		}
		int num = 0;
		float b = 0f;
		int num2 = 0;
		while (m_CannonBarrels != null && num2 < m_CannonBarrels.Length)
		{
			if (m_CannonBarrels[num2].beamWeapon != null)
			{
				num++;
			}
			else if (m_CannonBarrels[num2].recoiler != null)
			{
				Animation componentInChildren = m_CannonBarrels[num2].recoiler.GetComponentInChildren<Animation>(includeInactive: true);
				if (componentInChildren != null && componentInChildren.clip != null)
				{
					b = Mathf.Max(componentInChildren.clip.length);
				}
			}
			num2++;
		}
		if (num > 0)
		{
			return 30f * (float)num;
		}
		if (FiringData != null && FiringData.m_BulletPrefab != null)
		{
			float num3 = m_ShotCooldown;
			if (Application.isPlaying)
			{
				num3 *= (float)m_CannonBarrels.Length;
			}
			float num4 = Mathf.Max(num3, b);
			if (m_BurstShotCount > 1)
			{
				num4 = ((float)(m_BurstShotCount - 1) * num3 + m_BurstCooldown) / (float)m_BurstShotCount;
			}
			return (float)((m_FireControlMode != FireControlMode.AllAtOnce) ? 1 : m_CannonBarrels.Length) / num4;
		}
		return 0f;
	}

	private void OnAttached()
	{
		float num = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() - m_DetachTimeStamp;
		m_ShotTimer = Math.Max(Mathf.Epsilon, m_ShotTimer - num);
		m_DetachTimeStamp = 0f;
		m_BurstShotsRemaining = m_BurstShotCount;
		if (m_HasSpinUpDownAnim && m_DeploySFXType != TechAudio.SFXType.Default)
		{
			base.block.tank.TechAudio.AddModule(this);
		}
	}

	private void OnDetaching()
	{
		if (m_HasSpinUpDownAnim && m_DeploySFXType != TechAudio.SFXType.Default)
		{
			base.block.tank.TechAudio.RemoveModule(this);
		}
		m_DetachTimeStamp = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
		if (m_HasSpinUpDownAnim && m_Animator != null)
		{
			m_Animator.SetBool(ModuleWeapon.k_UniversalAnimator_DeployingState_Hash, value: false);
		}
		m_DeployingState = false;
	}

	private void OnPool()
	{
		m_CannonBarrels = GetComponentsInChildren<CannonBarrel>(includeInactive: true);
		m_NumCannonBarrels = m_CannonBarrels.Length;
		d.Assert(m_NumCannonBarrels != 0, "ModuleWeaponGun needs at least one CannonBarrel in hierarchy");
		m_NetComponents = GetComponentsInChildren<INetComponent>(includeInactive: true);
		net_NetComponentsData = new NetworkedProperty<ByteArrayBlockMessage>(this, TTMsgType.ModuleWeaponGunComponentsSync, OnMPNetComponentsDataSync);
		m_WeaponModule = GetComponent<ModuleWeapon>();
		m_Animator = GetComponentInChildren<Animator>();
		if (m_NumCannonBarrels != 0)
		{
			for (int i = 0; i < m_CannonBarrels.Length; i++)
			{
				if (m_BarrelTransform == null)
				{
					m_BarrelTransform = m_CannonBarrels[i].transform;
				}
				m_CannonBarrels[i].InitOnGun(this, FiringData, m_WeaponModule);
				m_CannonBarrels[i].CapRecoilDuration(m_ShotCooldown);
			}
		}
		m_ShotCooldown /= m_NumCannonBarrels;
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockLateUpdate.Subscribe(OnLateUpdate);
	}

	private void OnSpawn()
	{
		m_FailedToFireTime = 0f;
		m_IdleTime = 0f;
		m_DetachTimeStamp = 0f;
	}

	private void OnRecycle()
	{
	}

	private void OnLateUpdate()
	{
		UpdateNetComponents();
	}

	private void OnValidate()
	{
		d.AssertFormat(m_BurstShotCount != 1, this, "Weapon {0} has a burst count of 1 - this means it will use burst cooldown ({1}) instead of regular shot cooldown ({2}), even through there is no actual burst!", base.name, m_BurstCooldown, m_ShotCooldown);
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleWeaponGun;
	}

	public void SetNetComponentsDirty()
	{
		_m_NetComponentsDirty = true;
	}

	private void UpdateNetComponents()
	{
		TrySyncNetComponents();
	}

	public void TrySyncNetComponents()
	{
		if (_m_NetComponentsDirty)
		{
			_m_NetComponentsDirty = false;
			if (!ManNetwork.IsNetworked || ManNetwork.IsHost)
			{
				net_NetComponentsData.Data.value = SerializeNetComponents();
				net_NetComponentsData.Sync();
			}
		}
		byte[] SerializeNetComponents()
		{
			NetworkWriter networkWriter = new NetworkWriter();
			INetComponent[] netComponents = m_NetComponents;
			for (int i = 0; i < netComponents.Length; i++)
			{
				netComponents[i].Serialize(networkWriter);
			}
			return networkWriter.ToArray();
		}
	}

	private void OnMPNetComponentsDataSync(ByteArrayBlockMessage msg)
	{
		net_NetComponentsData.Data.value = msg.value;
		DeserializeNetComponents(net_NetComponentsData.Data.value);
		void DeserializeNetComponents(byte[] data)
		{
			if (data != null && data.Length != 0)
			{
				NetworkReader reader = new NetworkReader(data);
				INetComponent[] netComponents = m_NetComponents;
				for (int i = 0; i < netComponents.Length; i++)
				{
					netComponents[i].Deserialize(reader);
				}
			}
		}
	}

	public void OnSerialize(NetworkWriter writer)
	{
		net_NetComponentsData.Serialise(writer);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		net_NetComponentsData.Deserialise(reader);
	}
}
