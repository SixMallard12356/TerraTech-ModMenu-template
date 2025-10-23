#define UNITY_EDITOR
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(TargetAimer))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleWeapon : Module, TechAudio.IModuleAudioProvider
{
	public enum AimType
	{
		AutoAim,
		Default
	}

	private new class SerialData : SerialData<SerialData>
	{
		public WarningHolder warning;

		public float m_EnergyStoredForFiring;
	}

	[SerializeField]
	public AimType m_AimType;

	[SerializeField]
	public float m_RotateSpeed = 90f;

	[SerializeField]
	public float m_ChangeTargetInteval = 0.5f;

	[SerializeField]
	public bool m_AutoFire;

	[SerializeField]
	[Tooltip("How many units of electrical energy is required to fire this weapon one time")]
	protected float m_FiringEnergyRequired;

	[Header("Prevent shooting at floor")]
	[SerializeField]
	public bool m_PreventShootingTowardsFloor;

	[SerializeField]
	public bool m_DeployOnHasTarget;

	[SerializeField]
	public float m_LimitedShootAngle = 90f;

	[SerializeField]
	public bool m_DontFireIfNotAimingAtTarget;

	[SerializeField]
	public bool m_IsUsedOnCircuit;

	[Header("SFX")]
	[SerializeField]
	public float m_ShotCooldown = 1f;

	[SerializeField]
	public TechAudio.SFXType m_FireSFXType;

	public static readonly int k_UniversalAnimator_DeployedState_Hash = Animator.StringToHash("Up");

	public static readonly int k_UniversalAnimator_UnDeployedState_Hash = Animator.StringToHash("Down");

	public static readonly int k_UniversalAnimator_DeployingState_Hash = Animator.StringToHash("SpinUp");

	private TargetAimer m_TargetAimer;

	private IModuleWeapon m_WeaponComponent;

	private IModuleDamager m_DamagerComponent;

	private Vector3 m_TargetAimDirectionLocal;

	private Vector3 m_TargetPosition;

	private float m_RotationSpeedCoefficient = 1f;

	private WarningHolder m_Warning;

	private ManTimedEvents.ManagedEvent m_CheckDismissWarningEvent = new ManTimedEvents.ManagedEvent();

	private int m_RemoteShotFiredPending;

	private bool m_HasTargetInFiringCone;

	private float m_EnergyStoredForFiring;

	private EnergyGauge[] m_EnergyGauges;

	public const float kNumHitsPerSecForDisplayCalc = 30f;

	public bool FireControl { get; set; }

	public bool FireRequested
	{
		get
		{
			if (!CircuitControlled)
			{
				return FireControl;
			}
			if (base.block.CircuitNode.Receiver.CurrentChargeData > 0)
			{
				return base.block.CircuitReceiver.ShouldProcessInput;
			}
			return false;
		}
	}

	private bool CircuitControlled
	{
		get
		{
			if (m_IsUsedOnCircuit)
			{
				return base.block.CircuitNode.Receiver.IsConnectedToOtherNodes;
			}
			return false;
		}
	}

	private bool HasEnergyForShot
	{
		get
		{
			if (UsesEnergyForFiring)
			{
				return m_EnergyStoredForFiring >= m_FiringEnergyRequired;
			}
			return true;
		}
	}

	private bool UsesEnergyForFiring
	{
		get
		{
			if (base.block.energyModule != null)
			{
				return m_FiringEnergyRequired > 0f;
			}
			return false;
		}
	}

	public int AimControl { get; set; }

	public float RotateSpeed => m_RotateSpeed * m_RotationSpeedCoefficient;

	public TechAudio.SFXType SFXType => m_FireSFXType;

	public float ShotCooldown => m_ShotCooldown;

	public bool DisableTargetAimer { get; set; }

	public event Action<TechAudio.AudioTickData, FMODEvent.FMODParams> OnAudioTickUpdate;

	public void AddRemoteShotFired()
	{
		m_RemoteShotFiredPending++;
	}

	public int Process()
	{
		int num = 0;
		UpdateAim();
		bool flag = m_WeaponComponent.UpdateDeployment(FireRequested || (m_HasTargetInFiringCone && base.block.tank.IsAIControlled()) || (m_DeployOnHasTarget && m_TargetAimer.HasTarget));
		bool flag2 = FireRequested && flag && !m_WeaponComponent.FiringObstructed();
		bool flag3 = m_WeaponComponent.PrepareFiring(flag2) && FireRequested && CanShoot();
		num = m_WeaponComponent.ProcessFiring(flag3);
		if (flag3 && Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetwork>.inst.IsServer && Singleton.Manager<ManNetwork>.inst.ServerSpawnBank != null && Singleton.Manager<ManNetwork>.inst.NetController.GameModeType == MultiplayerModeType.Deathmatch && base.block.tank.netTech != null && base.block.tank.netTech.SpawnShieldCount > 0)
		{
			Singleton.Manager<ManNetwork>.inst.ServerSpawnBank.DisableShieldAfterDelay(base.block.tank.netTech.InitialSpawnShieldID);
		}
		if (m_RemoteShotFiredPending > 0)
		{
			num += m_RemoteShotFiredPending;
			m_RemoteShotFiredPending = 0;
		}
		if (ManNetwork.IsHost && num > 0 && m_FiringEnergyRequired > 0f)
		{
			float num2 = (float)num * m_FiringEnergyRequired;
			SetEnergyStoredForFiring(m_EnergyStoredForFiring - num2);
		}
		FireControl = false;
		if (this.OnAudioTickUpdate != null)
		{
			float fireRateFraction = m_WeaponComponent.GetFireRateFraction();
			TechAudio.AudioTickData value = TechAudio.AudioTickData.ConfigureLoopedADSR(this, m_FireSFXType, flag2, fireRateFraction, m_ShotCooldown, num);
			this.OnAudioTickUpdate.Send(value, FMODEvent.FMODParams.empty);
		}
		return num;
	}

	public void SetRotationSpeedCoefficient(float rotationSpeedCoef01)
	{
		m_RotationSpeedCoefficient = rotationSpeedCoef01;
	}

	private void UpdateAim()
	{
		m_HasTargetInFiringCone = false;
		switch (m_AimType)
		{
		case AimType.AutoAim:
			UpdateAutoAimBehaviour();
			break;
		case AimType.Default:
			UpdateDefaultBehaviour();
			break;
		default:
			d.LogError("ModuleWeapon.UpdateAim - Unsupported Aim Type");
			break;
		}
	}

	private void ResetAim()
	{
		m_TargetAimDirectionLocal = Vector3.forward;
		m_TargetPosition = Vector3.zero;
		m_TargetAimer.Reset();
	}

	private void SetAimTarget(Vector3 targetWorld)
	{
		m_TargetAimDirectionLocal = base.block.trans.InverseTransformPoint(targetWorld).normalized;
		m_TargetPosition = targetWorld;
	}

	private void UpdateAutoAimBehaviour()
	{
		Transform fireTransform = m_WeaponComponent.GetFireTransform();
		Vector3 position = fireTransform.position;
		if (base.block.tank.IsNull())
		{
			if (Singleton.Manager<ManTechBuilder>.inst.IsPlacingBlock(base.block))
			{
				m_TargetAimer.AimAtWorldPos(position + base.block.trans.forward, RotateSpeed);
			}
			return;
		}
		if (DisableTargetAimer)
		{
			m_HasTargetInFiringCone = m_TargetAimer.UpdateAndCanAimAtTarget() && m_TargetAimer.HasTarget;
			m_TargetAimer.AimAtWorldPos(position + 10f * base.block.trans.forward, RotateSpeed);
			return;
		}
		m_HasTargetInFiringCone = m_TargetAimer.UpdateAndAimAtTarget(RotateSpeed) && m_TargetAimer.HasTarget;
		if (!m_TargetAimer.HasTarget)
		{
			return;
		}
		Vector3 position2 = base.block.trans.position;
		m_TargetPosition = m_TargetAimer.Target.GetAimPoint(position2);
		if (!m_AutoFire)
		{
			return;
		}
		float range = m_WeaponComponent.GetRange();
		if (!((m_TargetPosition - position).sqrMagnitude < range * range))
		{
			return;
		}
		Vector3 lhs = m_TargetPosition - position2;
		lhs.y = 0f;
		Vector3 up = fireTransform.up;
		if (Vector3.Dot(lhs, up) > 0f)
		{
			Vector3 normalized = new Vector3(up.z, 0f, 0f - up.x).normalized;
			float num = Mathf.Max(m_TargetAimer.Target.Radius, 0.5f);
			if (Mathf.Abs(Vector3.Dot(lhs, normalized)) < num)
			{
				FireControl = true;
			}
		}
	}

	private void UpdateDefaultBehaviour()
	{
		if (AimControl != 0)
		{
			Vector3 axis = base.block.trans.InverseTransformDirection(base.block.tank.rootBlockTrans.up);
			Quaternion quaternion = Quaternion.AngleAxis((float)AimControl * RotateSpeed * Time.deltaTime, axis);
			m_TargetAimDirectionLocal = quaternion * m_TargetAimDirectionLocal;
		}
		Transform fireTransform = m_WeaponComponent.GetFireTransform();
		m_TargetAimer.AimAtWorldPos(fireTransform.position + base.block.trans.TransformDirection(m_TargetAimDirectionLocal), RotateSpeed);
	}

	private void SetTargetPosition(Vector3 targetPosition)
	{
		m_TargetPosition = targetPosition;
	}

	private bool CanShoot()
	{
		bool result = false;
		if (base.block.tank != null)
		{
			result = m_WeaponComponent.ReadyToFire();
			if (!HasEnergyForShot)
			{
				result = false;
			}
		}
		if (m_PreventShootingTowardsFloor && m_WeaponComponent.IsAimingAtFloor(m_LimitedShootAngle))
		{
			result = false;
		}
		return result;
	}

	private Vector3 AimPointWithTrajectory(Vector3 aimPoint)
	{
		float velocity = m_WeaponComponent.GetVelocity();
		Vector3 input = aimPoint - m_WeaponComponent.GetFireTransform().position;
		float magnitude = Physics.gravity.magnitude;
		float sqrMagnitude = input.SetY(0f).sqrMagnitude;
		float num = velocity * velocity;
		float num2 = num * num - magnitude * (magnitude * sqrMagnitude + (input.y + input.y) * num);
		num2 = ((num2 < 0f) ? 0f : Mathf.Sqrt(num2));
		Vector3 result = aimPoint;
		result.y += (num - num2) / magnitude - input.y;
		return result;
	}

	private void ControlInputTargeted(Vector3 targetPositionWorld, float targetRadiusWorld)
	{
		SetAimTarget(targetPositionWorld);
		Vector3 lhs = targetPositionWorld - base.block.trans.position;
		lhs.y = 0f;
		Vector3 forward = m_WeaponComponent.GetFireTransform().forward;
		Vector3 normalized = new Vector3(forward.z, 0f, 0f - forward.x).normalized;
		if (Mathf.Abs(Vector3.Dot(lhs, normalized)) < targetRadiusWorld && Vector3.Dot(lhs, forward) > 0f)
		{
			FireControl = true;
		}
		else if (m_DontFireIfNotAimingAtTarget)
		{
			FireControl = false;
		}
	}

	private void ControlInputManual(int aim, bool fire)
	{
		AimControl = aim;
		FireControl = fire && Time.timeScale != 0f;
		m_TargetPosition = Vector3.zero;
	}

	private void SetEnergyStoredForFiring(float energyTotal)
	{
		if (energyTotal != m_EnergyStoredForFiring)
		{
			if (energyTotal.Approximately(m_FiringEnergyRequired))
			{
				energyTotal = m_FiringEnergyRequired;
			}
			m_EnergyStoredForFiring = Mathf.Clamp(energyTotal, 0f, m_FiringEnergyRequired);
			UpdateGauges();
		}
	}

	private void UpdateGauges()
	{
		if (UsesEnergyForFiring)
		{
			float fullness = Mathf.Clamp01(m_EnergyStoredForFiring / m_FiringEnergyRequired);
			EnergyGauge[] energyGauges = m_EnergyGauges;
			for (int i = 0; i < energyGauges.Length; i++)
			{
				energyGauges[i].UpdateGaugeLevel(null, fullness);
			}
		}
	}

	private void OnUpdateConsumeEnergy()
	{
		float num = m_FiringEnergyRequired - m_EnergyStoredForFiring;
		if (ManNetwork.IsHost && num > 0f)
		{
			float num2 = base.block.energyModule.ConsumeUpToMax(TechEnergy.EnergyType.Electric, num);
			SetEnergyStoredForFiring(m_EnergyStoredForFiring + num2);
		}
	}

	private void OnAttached()
	{
		ResetAim();
		base.block.tank.Weapons.AddWeapon(this);
		base.block.tank.TechAudio.AddModule(this);
		UpdateGauges();
		base.block.tank.control.manualAimFireEvent.Subscribe(ControlInputManual);
		base.block.tank.control.targetedAimFireEvent.Subscribe(ControlInputTargeted);
	}

	private void OnDetaching()
	{
		m_TargetAimer.ResetGimbalAngles();
		m_Warning.Reset();
		base.block.tank.Weapons.RemoveWeapon(this);
		base.block.tank.TechAudio.RemoveModule(this);
		base.block.tank.control.manualAimFireEvent.Unsubscribe(ControlInputManual);
		base.block.tank.control.targetedAimFireEvent.Unsubscribe(ControlInputTargeted);
		SetEnergyStoredForFiring(0f);
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.warning = m_Warning;
			serialData.m_EnergyStoredForFiring = m_EnergyStoredForFiring;
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			m_Warning.Restore(serialData2.warning);
			SetEnergyStoredForFiring(serialData2.m_EnergyStoredForFiring);
		}
	}

	private void CheckIfCanDismissWarning()
	{
		if (m_WeaponComponent.FiringObstructed())
		{
			m_CheckDismissWarningEvent.Reset(Singleton.Manager<ManOverlay>.inst.m_GunFireCheckTime);
		}
		else
		{
			m_Warning.Clear();
		}
	}

	private void PrePool()
	{
		if (m_IsUsedOnCircuit)
		{
			this.SetupForCircuitInput();
		}
	}

	private void OnPool()
	{
		m_WeaponComponent = GetComponent<IModuleWeapon>();
		m_DamagerComponent = GetComponent<IModuleDamager>();
		if (base.block.energyModule != null && m_FiringEnergyRequired > 0f)
		{
			base.block.energyModule.UpdateConsumeEvent.Subscribe(OnUpdateConsumeEnergy);
			m_EnergyGauges = GetComponentsInChildren<EnergyGauge>(includeInactive: true);
		}
		d.Assert(m_WeaponComponent != null, "ModuleWeapon needs an IModuleWeaponComponent.");
		m_TargetAimer = GetComponent<TargetAimer>();
		if ((bool)m_TargetAimer)
		{
			Func<Vector3, Vector3> aimDelegate = null;
			if (m_WeaponComponent != null && m_WeaponComponent.AimWithTrajectory())
			{
				aimDelegate = AimPointWithTrajectory;
			}
			m_TargetAimer.Init(base.block, m_ChangeTargetInteval, aimDelegate);
		}
		else
		{
			d.LogWarning("ModuleWeapon.OnPool: No Target Aimer on block" + base.block.name);
		}
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		m_Warning = new WarningHolder(base.block.visible, WarningHolder.WarningType.GunLineOfSight);
		base.block.serializeEvent.Subscribe(OnSerialize);
	}

	private void OnSpawn()
	{
		ResetAim();
		m_RotationSpeedCoefficient = 1f;
		AimControl = 0;
		FireControl = false;
		DisableTargetAimer = false;
		m_HasTargetInFiringCone = false;
		m_TargetPosition = Vector3.zero;
		SetEnergyStoredForFiring(0f);
		m_RemoteShotFiredPending = 0;
	}

	private void OnRecycle()
	{
		if ((bool)base.block.tank)
		{
			base.block.tank.Weapons.RemoveWeapon(this);
		}
		m_CheckDismissWarningEvent.Clear();
	}
}
