#define UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleBooster : Module
{
	private enum Position
	{
		positive,
		neutral,
		negative
	}

	[SerializeField]
	[FormerlySerializedAs("useBoostControls")]
	private bool m_UseBoostControls = true;

	[FormerlySerializedAs("useDriveControls")]
	[SerializeField]
	private bool m_UseDriveControls;

	[SerializeField]
	private bool m_IsRocketBooster;

	[SerializeField]
	private bool m_EnablesThrottleControl;

	[SerializeField]
	private int m_BoosterAudioType;

	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	[SerializeField]
	[Tooltip("A RnD ready animator driven by fireRate")]
	private Animator m_FlappyWingAnim;

	private int s_FireRate_AnimProperty_Id = Animator.StringToHash("_FireRate");

	[SerializeField]
	[HideInInspector]
	private List<BoosterJet> jets;

	[HideInInspector]
	[SerializeField]
	private List<FanJet> fans;

	[SerializeField]
	[HideInInspector]
	private List<ThrustPivoter> thrustPivoters;

	[SerializeField]
	[HideInInspector]
	private bool m_ConsumesFuel;

	private bool m_IsFiringBoost;

	private bool m_IsFiringSteer;

	private WarningHolder m_Warning;

	private bool m_IsEnabled;

	public bool UsesDriveControls => m_UseDriveControls;

	public bool IsRocketBooster => m_IsRocketBooster;

	public bool IsRotor
	{
		get
		{
			if (fans.Count > 0)
			{
				return m_EnablesThrottleControl;
			}
			return false;
		}
	}

	public Vector3 RotorDirection
	{
		get
		{
			if (!IsRotor)
			{
				return Vector3.zero;
			}
			return fans[0].EffectorForward;
		}
	}

	public Vector3 RotorDefaultDirection
	{
		get
		{
			if (!IsRotor)
			{
				return Vector3.zero;
			}
			return fans[0].EffectorDefaultForward;
		}
	}

	public TechBooster.BoosterType RocketBoosterType
	{
		get
		{
			d.Assert(m_IsRocketBooster, "ModuleBooster.RocketBoosterType - Booster is not of type Rocket booster!");
			return (TechBooster.BoosterType)m_BoosterAudioType;
		}
	}

	public TechAudio.BoosterEngineType BoosterEngineType
	{
		get
		{
			d.Assert(!m_IsRocketBooster, "ModuleBooster.BoosterEngineType - Booster is not of type Booster Engine!");
			return (TechAudio.BoosterEngineType)m_BoosterAudioType;
		}
	}

	public bool IsFiring
	{
		get
		{
			if (!m_IsFiringBoost)
			{
				return m_IsFiringSteer;
			}
			return true;
		}
	}

	public bool IsFiringBooster => m_IsFiringBoost;

	public bool IsFiringSteer => m_IsFiringSteer;

	public float FireRate { get; private set; }

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

	public float FuelBurnPerSecond()
	{
		float num = 0f;
		foreach (BoosterJet jet in jets)
		{
			num += jet.BurnRate;
		}
		return num;
	}

	public void CopyAudioType(ModuleBooster src)
	{
		if (m_BoosterAudioType != src.m_BoosterAudioType)
		{
			if (base.block.tank != null)
			{
				base.block.tank.TechAudio.RemoveBooster(this);
			}
			m_BoosterAudioType = src.m_BoosterAudioType;
			if (base.block.tank != null)
			{
				base.block.tank.TechAudio.AddBooster(this);
			}
		}
	}

	public Vector3 QueryBoosterThrustVectorApproximation(out Vector3 centerOfThrustLocal)
	{
		return QueryThrustVectorApproximation(driveControls: false, out centerOfThrustLocal);
	}

	public Vector3 QueryPropellerThrustVectorApproximation(out Vector3 centerOfThrustLocal)
	{
		return QueryThrustVectorApproximation(driveControls: true, out centerOfThrustLocal);
	}

	private Vector3 QueryThrustVectorApproximation(bool driveControls, out Vector3 centerOfThrustLocal)
	{
		Vector3 zero = Vector3.zero;
		centerOfThrustLocal = Vector3.zero;
		if (m_UseDriveControls != driveControls)
		{
			return zero;
		}
		float num = 0f;
		for (int i = 0; i < fans.Count; i++)
		{
			Vector3 centerOfBoosterThrustWorld;
			Vector3 vector = fans[i].QueryBoostThrustVector(1f, out centerOfBoosterThrustWorld);
			if (vector.magnitude != 0f)
			{
				zero += vector;
				num += vector.magnitude;
				centerOfThrustLocal += base.block.trans.InverseTransformPoint(centerOfBoosterThrustWorld) * vector.magnitude;
			}
		}
		for (int j = 0; j < jets.Count; j++)
		{
			Vector3 centerOfBoosterThrustWorld2;
			Vector3 vector2 = jets[j].QueryBoostThrustVector(1f, out centerOfBoosterThrustWorld2);
			if (vector2.magnitude != 0f)
			{
				zero += vector2;
				num += vector2.magnitude;
				centerOfThrustLocal += base.block.trans.InverseTransformPoint(centerOfBoosterThrustWorld2) * vector2.magnitude;
			}
		}
		centerOfThrustLocal /= ((num == 0f) ? 1f : num);
		centerOfThrustLocal = base.block.trans.localPosition + base.block.trans.localRotation * centerOfThrustLocal;
		return zero;
	}

	public bool OnTechUpdate()
	{
		if (!m_IsEnabled || CircuitControlled)
		{
			float num = 0f;
			float actuationT = 0f;
			if (m_IsEnabled && base.block.CircuitReceiver.ShouldProcessInput)
			{
				float num2 = Mathf.Clamp01(base.block.CircuitNode.Receiver.CurrentChargeData.ChargeStrength);
				num = ((m_ConsumesFuel && base.block.tank.Boosters.FuelBurnedOut) ? 0f : num2);
				actuationT = num2;
			}
			m_IsFiringBoost = num > 0f;
			m_IsFiringSteer = false;
			foreach (BoosterJet jet in jets)
			{
				jet.SetThrustRate(num);
			}
			foreach (FanJet fan in fans)
			{
				fan.SetThrustRate(actuationT);
			}
		}
		if (!IsFiringBooster)
		{
			if (IsRocketBooster)
			{
				return IsFiring;
			}
			return false;
		}
		return true;
	}

	public void SetEnabled(bool enable)
	{
		m_IsEnabled = enable;
	}

	private void DriveControlInput(TankControl.ControlState driveData)
	{
		if (!base.enabled || !m_IsEnabled || CircuitControlled)
		{
			return;
		}
		m_IsFiringSteer = false;
		m_IsFiringBoost = false;
		bool flag = base.block.tank.ShouldAutoStabilise && m_UseDriveControls && !m_EnablesThrottleControl && !driveData.AnyMovementOrBoostControl;
		foreach (ThrustPivoter thrustPivoter in thrustPivoters)
		{
			Vector3 inputMovement = driveData.InputMovement;
			Vector3 vector = driveData.InputRotation;
			if (!vector.sqrMagnitude.Approximately(0f))
			{
				vector = TankControl.CalculateDirectionalContributionForRotation(base.block.tank, thrustPivoter.transform.position, Quaternion.Euler(driveData.InputRotation * 90f));
			}
			Vector3 techAlignmentDirection = Vector3.ClampMagnitude(inputMovement + vector, 1f);
			thrustPivoter.SetTechAlignmentDirection(techAlignmentDirection);
		}
		foreach (FanJet fan in fans)
		{
			float num = 0f;
			if (m_UseDriveControls)
			{
				float num2 = Vector3.Dot(driveData.InputRotation, fan.RotationContribution);
				float num3 = Vector3.Dot(driveData.InputMovement + driveData.Throttle, fan.LocalThrustDirection);
				num = Mathf.Clamp(num2 + num3, -1f, 1f);
				if (num != 0f)
				{
					m_IsFiringSteer = true;
				}
				if (driveData.BoostProps && num3 >= 0f && m_UseBoostControls)
				{
					m_IsFiringBoost = true;
					num = 1f;
				}
			}
			else if (m_UseBoostControls)
			{
				num = (driveData.BoostProps ? 1f : 0f);
				m_IsFiringBoost = m_IsFiringBoost || driveData.BoostProps;
			}
			fan.SetThrustRate(num);
			if (flag)
			{
				fan.AutoStabiliseTank();
			}
		}
		foreach (BoosterJet jet in jets)
		{
			float num4 = 0f;
			if (!m_ConsumesFuel || !base.block.tank.Boosters.FuelBurnedOut)
			{
				if (m_UseDriveControls)
				{
					float num5 = Vector3.Dot(driveData.InputMovement + driveData.Throttle, jet.LocalThrustDirection);
					if (m_UseBoostControls && driveData.BoostJets)
					{
						num4 = ((num5 >= 0f) ? 1f : 0f);
					}
					else
					{
						num4 = Mathf.Clamp01(Vector3.Dot(driveData.InputRotation, jet.RotationContribution) + num5);
						if (num4 < 0.01f)
						{
							num4 = 0f;
						}
					}
				}
				else if (m_UseBoostControls)
				{
					num4 = (driveData.BoostJets ? 1f : 0f);
					m_IsFiringBoost = m_IsFiringBoost || driveData.BoostJets;
				}
			}
			jet.SetThrustRate(num4);
			if (flag)
			{
				jet.AutoStabiliseTank();
			}
		}
	}

	private void OnAxesWarning(bool show, int inputAxesBitfield)
	{
		bool flag = false;
		if (m_UseBoostControls)
		{
			flag = (fans.Count > 0 && (inputAxesBitfield & 0x40) != 0) | (jets.Count > 0 && (inputAxesBitfield & 0x80) != 0);
		}
		bool flag2 = false;
		if (m_UseDriveControls)
		{
			int num = 0;
			foreach (FanJet fan in fans)
			{
				num |= TankControl.GetInputAxisBitfieldForRotation(fan.RotationContribution) | TankControl.GetInputAxisBitfieldForMovement(fan.LocalThrustDirection);
			}
			foreach (BoosterJet jet in jets)
			{
				num |= TankControl.GetInputAxisBitfieldForRotation(jet.RotationContribution) | TankControl.GetInputAxisBitfieldForMovement(jet.LocalThrustDirection);
			}
			flag2 = (inputAxesBitfield & num) != 0;
		}
		if (show && !flag && !flag2)
		{
			m_Warning.TryRegisterWarning(LocalisationEnums.Warnings.warningTitleNoControls, LocalisationEnums.Warnings.warningMsgNoControls, 12);
		}
		else
		{
			m_Warning.Remove();
		}
	}

	private void RecalculateBoostDirection()
	{
		foreach (BoosterJet jet in jets)
		{
			jet.RecalculateThrustDirection();
		}
		foreach (FanJet fan in fans)
		{
			fan.RecalculateThrustDirection();
		}
	}

	private void OnAttached()
	{
		base.block.tank.control.driveControlEvent.Subscribe(DriveControlInput);
		base.block.tank.control.axesWarningEvent.Subscribe(OnAxesWarning);
		base.block.tank.ResetEvent.Subscribe(OnResetTank);
		base.block.tank.ResetPhysicsEvent.Subscribe(OnResetTechPhysics);
		base.block.tank.Boosters.AddBooster(this);
		base.block.tank.TechAudio.AddBooster(this);
		if (!m_EnablesThrottleControl)
		{
			return;
		}
		foreach (FanJet fan in fans)
		{
			fan.RecalculateThrustDirection();
			base.block.tank.control.AddThrottleControlEnabler(fan.transform, fan.LocalThrustDirection);
			fan.ThrustDirectionRecalculatedEvent.Subscribe(OnFanJetThrustDirectionRecalculated);
		}
	}

	private void OnDetaching()
	{
		if (m_EnablesThrottleControl)
		{
			foreach (FanJet fan in fans)
			{
				base.block.tank.control.RemoveThrottleControlEnabler(fan.transform, fan.LocalThrustDirection);
				fan.ThrustDirectionRecalculatedEvent.Unsubscribe(OnFanJetThrustDirectionRecalculated);
			}
		}
		base.block.tank.control.driveControlEvent.Unsubscribe(DriveControlInput);
		base.block.tank.control.axesWarningEvent.Unsubscribe(OnAxesWarning);
		base.block.tank.ResetEvent.Unsubscribe(OnResetTank);
		base.block.tank.ResetPhysicsEvent.Unsubscribe(OnResetTechPhysics);
		base.block.tank.Boosters.RemoveBooster(this);
		base.block.tank.TechAudio.RemoveBooster(this);
		m_Warning.Remove();
		OnResetTank();
	}

	private void OnDisable()
	{
		OnResetTank();
	}

	private void OnResetTank(int unused = 0)
	{
		foreach (BoosterJet jet in jets)
		{
			jet.SetThrustRate(0f);
		}
		foreach (FanJet fan in fans)
		{
			fan.ResetThrustRate();
		}
		FireRate = 0f;
	}

	private void OnResetTechPhysics()
	{
		RecalculateBoostDirection();
	}

	private void OnFanJetThrustDirectionRecalculated(Thruster thruster, Vector3 oldLocalThrustDirection, Vector3 newLocalThrustDirection)
	{
		if (m_EnablesThrottleControl && !(oldLocalThrustDirection == newLocalThrustDirection))
		{
			FanJet fanJet = (FanJet)thruster;
			base.block.tank.control.RemoveThrottleControlEnabler(fanJet.transform, oldLocalThrustDirection);
			base.block.tank.control.AddThrottleControlEnabler(fanJet.transform, fanJet.LocalThrustDirection);
		}
	}

	private void PrePool()
	{
		if (m_IsUsedOnCircuit)
		{
			this.SetupForCircuitInput();
		}
		jets = new List<BoosterJet>(GetComponentsInChildren<BoosterJet>(includeInactive: true));
		fans = new List<FanJet>(GetComponentsInChildren<FanJet>(includeInactive: true));
		thrustPivoters = new List<ThrustPivoter>(GetComponentsInChildren<ThrustPivoter>(includeInactive: true));
		m_ConsumesFuel = jets.Any((BoosterJet jet) => jet.ConsumesFuel);
		d.Assert(jets.Count != 0 || fans.Count != 0, "ModuleBooster " + base.name + " needs at least one BoosterJet or FanJet in hiearcarchy");
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		m_Warning = new WarningHolder(base.block.visible, WarningHolder.WarningType.NoControlsMapped);
	}

	private void OnSpawn()
	{
		m_IsEnabled = true;
		OnResetTank();
	}

	private void OnUpdate()
	{
		float num = 0f;
		foreach (BoosterJet jet in jets)
		{
			if (jet.ThrustRateCurrent > num)
			{
				num = jet.ThrustRateCurrent;
			}
		}
		foreach (FanJet fan in fans)
		{
			float thrustRateCurrent_Abs = fan.ThrustRateCurrent_Abs;
			if (thrustRateCurrent_Abs > num)
			{
				num = thrustRateCurrent_Abs;
			}
		}
		FireRate = num;
		if (m_FlappyWingAnim != null)
		{
			m_FlappyWingAnim.SetFloat(s_FireRate_AnimProperty_Id, FireRate);
		}
	}
}
