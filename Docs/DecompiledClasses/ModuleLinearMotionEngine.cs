#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ModuleEnergy))]
public class ModuleLinearMotionEngine : Module
{
	[Serializable]
	public class Effector
	{
		[SerializeField]
		private Transform m_Effector;

		private Vector3 m_TankLocalBoostDirection;

		public Vector3 WorldBoostDirection
		{
			get
			{
				if (!(m_Effector != null))
				{
					return Vector3.zero;
				}
				return -m_Effector.forward;
			}
		}

		public Vector3 TankLocalBoostDirection => m_TankLocalBoostDirection;

		public float CurrentPower { get; set; }

		public Transform EffectorTransform => m_Effector;

		public void ApplyForce(Rigidbody rbody, Vector3 rbodyOffset, float force)
		{
			if (m_Effector != null && CurrentPower > 0f)
			{
				Vector3 position = m_Effector.position + rbodyOffset;
				rbody.AddForceAtPosition(WorldBoostDirection * (CurrentPower * force), position);
			}
		}

		public void ResetTechPhysics(Tank tank)
		{
			if (m_Effector != null)
			{
				TankControl.GetInputEffect(tank, m_Effector.position, WorldBoostDirection, out var _, out m_TankLocalBoostDirection);
			}
		}
	}

	[SerializeField]
	private float m_ForcePerEffector;

	[SerializeField]
	private float m_PowerUseAtMaxThrottlePerSecond;

	[SerializeField]
	private List<Effector> m_Effectors;

	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	private ModuleEnergy m_ModuleEnergy;

	private bool m_HasPower;

	private float m_CurrentPower;

	private WarningHolder m_Warning;

	private bool m_WantsThrottleControl;

	public float CurrentPower => m_CurrentPower;

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

	private void OnUpdateConsumeEnergy()
	{
		float currentAmount = m_ModuleEnergy.GetCurrentAmount(TechEnergy.EnergyType.Electric);
		float num = m_PowerUseAtMaxThrottlePerSecond * m_CurrentPower * Time.deltaTime;
		if (currentAmount < num)
		{
			m_HasPower = false;
			return;
		}
		m_HasPower = true;
		m_ModuleEnergy.ConsumeUpToMax(TechEnergy.EnergyType.Electric, num);
	}

	private void OnAttached()
	{
		base.block.tank.control.driveControlEvent.Subscribe(OnDriveControl);
		base.block.tank.control.axesWarningEvent.Subscribe(OnAxesWarning);
		base.block.tank.ResetPhysicsEvent.Subscribe(OnResetTechPhysics);
		TankBeam.OnBeamEnabled.Subscribe(OnTankBeamEnabled);
		m_WantsThrottleControl = true;
		UpdateThrottleControlEnabled();
		base.block.tank.TechAudio.AddLinearMotionEngine(this);
	}

	private void OnDetaching()
	{
		base.block.tank.TechAudio.RemoveLinearMotionEngine(this);
		m_WantsThrottleControl = false;
		UpdateThrottleControlEnabled();
		base.block.tank.control.driveControlEvent.Unsubscribe(OnDriveControl);
		base.block.tank.control.axesWarningEvent.Unsubscribe(OnAxesWarning);
		base.block.tank.ResetPhysicsEvent.Unsubscribe(OnResetTechPhysics);
		TankBeam.OnBeamEnabled.Unsubscribe(OnTankBeamEnabled);
		m_Warning.Remove();
	}

	private void OnDriveControl(TankControl.ControlState driveData)
	{
		if (CircuitControlled)
		{
			return;
		}
		m_CurrentPower = 0f;
		foreach (Effector effector in m_Effectors)
		{
			float num = Mathf.Clamp01(Vector3.Dot(driveData.Throttle, effector.TankLocalBoostDirection));
			m_CurrentPower = Mathf.Max(m_CurrentPower, num);
			effector.CurrentPower = num;
		}
	}

	private void OnCircuitControlChanged()
	{
		if (!CircuitControlled)
		{
			return;
		}
		float currentPower = (m_CurrentPower = (base.block.CircuitReceiver.ShouldProcessInput ? Mathf.Clamp01(base.block.CircuitReceiver.CurrentChargeData.ChargeStrength) : 0f));
		foreach (Effector effector in m_Effectors)
		{
			effector.CurrentPower = currentPower;
		}
	}

	private void OnAxesWarning(bool show, int inputAxesBitfield)
	{
		int num = 0;
		foreach (Effector effector in m_Effectors)
		{
			num |= TankControl.GetInputAxisBitfieldForMovement(effector.TankLocalBoostDirection);
		}
		if (show && num != 0 && (inputAxesBitfield & num) == 0)
		{
			m_Warning.TryRegisterWarning(LocalisationEnums.Warnings.warningTitleNoControls, LocalisationEnums.Warnings.warningMsgNoControls, 12);
		}
		else
		{
			m_Warning.Remove();
		}
	}

	private void OnResetTechPhysics()
	{
		if (!m_WantsThrottleControl || CircuitControlled)
		{
			return;
		}
		foreach (Effector effector in m_Effectors)
		{
			Vector3 tankLocalBoostDirection = effector.TankLocalBoostDirection;
			effector.ResetTechPhysics(base.block.tank);
			if (tankLocalBoostDirection != effector.TankLocalBoostDirection)
			{
				base.block.tank.control.RemoveThrottleControlEnabler(effector.EffectorTransform, tankLocalBoostDirection);
				base.block.tank.control.AddThrottleControlEnabler(effector.EffectorTransform, effector.TankLocalBoostDirection);
			}
		}
	}

	private void UpdateThrottleControlEnabled()
	{
		if (m_WantsThrottleControl && !CircuitControlled)
		{
			foreach (Effector effector in m_Effectors)
			{
				base.block.tank.control.AddThrottleControlEnabler(effector.EffectorTransform, effector.TankLocalBoostDirection);
			}
			return;
		}
		foreach (Effector effector2 in m_Effectors)
		{
			base.block.tank.control.RemoveThrottleControlEnabler(effector2.EffectorTransform, effector2.TankLocalBoostDirection);
		}
	}

	private void OnChargeChanged(Circuits.BlockChargeData charge)
	{
		OnCircuitControlChanged();
	}

	private void OnConnectedToCircuitNetworkChanged(bool isNowConnectedToCircuit)
	{
		if (base.block.IsAttached)
		{
			UpdateThrottleControlEnabled();
		}
		OnCircuitControlChanged();
	}

	private void OnTankBeamEnabled(Tank tech, bool enabled)
	{
		if (base.block.IsAttached && base.block.tank == tech)
		{
			OnCircuitControlChanged();
		}
	}

	private void PrePool()
	{
		d.AssertFormat(m_Effectors != null && m_Effectors.Count > 0, "LinearMotionEngine must have at least one effector transform!");
		foreach (Effector effector in m_Effectors)
		{
			if (effector.EffectorTransform.IsNull())
			{
				d.LogError("LinearMotionEngine effector with no transform reference", this);
			}
		}
		if (m_IsUsedOnCircuit)
		{
			this.SetupForCircuitInput();
		}
	}

	private void OnPool()
	{
		m_Warning = new WarningHolder(base.block.visible, WarningHolder.WarningType.NoControlsMapped);
		m_ModuleEnergy = GetComponent<ModuleEnergy>();
		m_ModuleEnergy.UpdateConsumeEvent.Subscribe(OnUpdateConsumeEnergy);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockFixedUpdate.Subscribe(OnFixedUpdate);
		if (m_IsUsedOnCircuit)
		{
			base.block.CircuitNode.ConnectedToOtherNodesEvent.Subscribe(OnConnectedToCircuitNetworkChanged);
			base.block.CircuitNode.Receiver.SubscribeToChargeData(null, null, null, OnChargeChanged, requireExtensiveChargeData: false);
		}
	}

	private void OnSpawn()
	{
		foreach (Effector effector in m_Effectors)
		{
			effector.CurrentPower = 0f;
		}
	}

	private void OnFixedUpdate()
	{
		if (!base.block.IsAttached || !m_HasPower || base.block.tank.beam.IsActive)
		{
			return;
		}
		Rigidbody rbody = base.block.tank.rbody;
		Vector3 rbodyOffset = rbody.position - rbody.transform.position;
		foreach (Effector effector in m_Effectors)
		{
			effector.ApplyForce(rbody, rbodyOffset, m_ForcePerEffector);
		}
	}
}
