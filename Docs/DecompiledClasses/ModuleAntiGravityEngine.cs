#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(ModuleEnergy))]
public class ModuleAntiGravityEngine : Module, IGravityManipulator, INetworkedModule
{
	[SerializeField]
	private float m_Radius = 2f;

	[SerializeField]
	private Transform m_EffectorCentre;

	[Range(0f, 10f)]
	[SerializeField]
	private float m_StrengthAsGravityScale = 0.5f;

	[SerializeField]
	private bool m_SubtractGravity = true;

	[SerializeField]
	private float m_EnergyUsedPerFilledCellPerSecond;

	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	private bool m_SwitchedOn;

	private bool m_HasPower;

	private ModuleEnergy m_ModuleEnergy;

	private bool m_GravityTargetsDirty;

	private float m_GravityDelta;

	private bool m_GravityManipulationZoneDirty;

	private GravityManipulationZone m_GravityManipulationZone;

	private const float k_RadiusFudgeFactor = 0.05f;

	private List<IGravityAdjustmentTarget> m_CurrentGravityAdjustmentTargets;

	private int m_LayerMask;

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

	private Vector3 ZonePosition => base.transform.position;

	private float ZoneRadius => m_Radius + 0.05f;

	private void OnUpdateConsumeEnergy()
	{
		if (ManNetwork.IsHost)
		{
			bool hasPower = m_HasPower;
			if (m_ModuleEnergy.GetCurrentAmount(TechEnergy.EnergyType.Electric) <= 0f)
			{
				m_HasPower = false;
			}
			else
			{
				m_HasPower = true;
				m_ModuleEnergy.ConsumeUpToMax(TechEnergy.EnergyType.Electric, Mathf.Abs(m_GravityDelta) * m_EnergyUsedPerFilledCellPerSecond * Time.deltaTime);
			}
			if (base.block.tank.netTech.IsNotNull() && Singleton.Manager<ManNetwork>.inst.IsServer && hasPower != m_HasPower)
			{
				base.block.tank.netTech.SetModuleDirty(this);
			}
		}
	}

	private void OnAttached()
	{
		Singleton.Manager<ManGravity>.inst.RegisterGravityManipulator(this);
		base.block.tank.AttachEvent.Subscribe(OnTankConfigChanged);
		base.block.tank.DetachEvent.Subscribe(OnTankConfigChanged);
		m_GravityTargetsDirty = true;
		m_GravityManipulationZoneDirty = true;
	}

	private void OnDetaching()
	{
		base.block.tank.AttachEvent.Unsubscribe(OnTankConfigChanged);
		base.block.tank.DetachEvent.Unsubscribe(OnTankConfigChanged);
		Singleton.Manager<ManGravity>.inst.UnRegisterGravityManipulator(this);
		m_GravityTargetsDirty = true;
		m_GravityManipulationZoneDirty = true;
	}

	private void OnTankConfigChanged(TankBlock block, Tank tank)
	{
		m_GravityTargetsDirty = true;
	}

	public bool SwitchedOn()
	{
		return m_SwitchedOn;
	}

	public void UpdateGravityAdjustmentTargets()
	{
		if (!m_GravityTargetsDirty)
		{
			return;
		}
		m_CurrentGravityAdjustmentTargets.Clear();
		if (m_SwitchedOn)
		{
			int iD = base.block.tank.visible.ID;
			foreach (Collider item in PhysicsUtils.OverlapSphereAllNonAlloc(ZonePosition, ZoneRadius, m_LayerMask))
			{
				Visible visible = Singleton.Manager<ManVisible>.inst.FindVisible(item);
				if (visible.IsNotNull() && visible.type == ObjectTypes.Block)
				{
					TankBlock tankBlock = visible.block;
					if ((bool)tankBlock.tank && tankBlock.tank.visible.ID == iD && !tankBlock.HasAdjustmentBeenTouched())
					{
						tankBlock.SetAdjustmentTouched(touched: true);
						m_CurrentGravityAdjustmentTargets.Add(tankBlock);
					}
				}
			}
			foreach (IGravityAdjustmentTarget currentGravityAdjustmentTarget in m_CurrentGravityAdjustmentTargets)
			{
				currentGravityAdjustmentTarget.SetAdjustmentTouched(touched: false);
			}
		}
		m_GravityTargetsDirty = false;
	}

	public List<IGravityAdjustmentTarget> GetGravityAdjustmentTargets()
	{
		return m_CurrentGravityAdjustmentTargets;
	}

	public GravityManipulationZone GetGravityManipulationZone()
	{
		if (m_GravityManipulationZoneDirty)
		{
			d.Assert(base.block.tank.IsNotNull(), "ModuleAntiGravityEngine.GetGravityManipulationZone - Module does not currently support active gravity while it is not attached to a Tech!");
			m_GravityManipulationZone = new GravityManipulationZone
			{
				m_Position = base.block.tank.trans.InverseTransformPoint(m_EffectorCentre.position),
				m_PositionIsLocal = true,
				m_Radius = ZoneRadius,
				m_ManipulationAmount = (m_SubtractGravity ? (0f - m_StrengthAsGravityScale) : m_StrengthAsGravityScale)
			};
		}
		return m_GravityManipulationZone;
	}

	public void SetGravityDelta(float delta)
	{
		m_GravityDelta = delta;
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleAntiGravityEngine;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		writer.Write(m_HasPower);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		m_HasPower = reader.ReadBoolean();
	}

	private void SwitchOnOff(bool on)
	{
		if (on != m_SwitchedOn)
		{
			m_SwitchedOn = on;
			m_GravityTargetsDirty = true;
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
		m_ControlCategoryType = ModuleControlCategory.AntiGravity;
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		m_ModuleEnergy = GetComponent<ModuleEnergy>();
		m_ModuleEnergy.UpdateConsumeEvent.Subscribe(OnUpdateConsumeEnergy);
		m_CurrentGravityAdjustmentTargets = new List<IGravityAdjustmentTarget>();
		m_LayerMask = Globals.inst.layerTank.mask | Globals.inst.layerTankIgnoreTerrain.mask;
	}

	private void OnUpdate()
	{
		bool flag = m_HasPower && base.block.IsAttached && IsCategoryEnabled() && !base.block.tank.beam.IsActive && (!CircuitControlled || base.block.CircuitReceiver.CurrentChargeData > 0);
		SwitchOnOff(flag);
	}
}
