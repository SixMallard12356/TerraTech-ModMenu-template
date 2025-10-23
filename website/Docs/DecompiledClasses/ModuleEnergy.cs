#define UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ModuleEnergy : Module
{
	[Flags]
	public enum OutputConditionFlags
	{
		Anchored = 1,
		DayTime = 2,
		Thermal = 4
	}

	private new class SerialData : SerialData<SerialData>
	{
		public bool poweredUp;
	}

	[SerializeField]
	private int m_Priority = 10;

	[SerializeField]
	[FormerlySerializedAs("m_OutputPerSecondWhileAnchored")]
	private float m_OutputPerSecond;

	[SerializeField]
	[FormerlySerializedAs("m_OutputAnchoredEnergyType")]
	private TechEnergy.EnergyType m_OutputEnergyType;

	[SerializeField]
	[EnumFlag]
	private OutputConditionFlags m_OutputConditions = OutputConditionFlags.Anchored | OutputConditionFlags.DayTime;

	[SerializeField]
	private bool m_SequentialPowerUp;

	[SerializeField]
	private float m_PowerUpDelay;

	[SerializeField]
	private TechAudio.SFXType m_GeneratingSFXType;

	public EventNoParams UpdateSupplyEvent;

	public EventNoParams UpdateConsumeEvent;

	private ModuleEnergyStore m_EnergyStore;

	private ModuleAnimator m_AnimatorController;

	private ModuleAudioProvider m_AudioProvider;

	private AnimatorBool m_HasSupplyBool = new AnimatorBool("HasSupply");

	private AnimatorBool m_GeneratingEnergyBool = new AnimatorBool("IsGenerating");

	[HideInInspector]
	[SerializeField]
	private float m_ThermalGroundRadius;

	private ThermalPowerSource m_ThermalSourceInRange;

	private static readonly Bitfield<ObjectTypes> k_MaskScenery = new Bitfield<ObjectTypes>(new ObjectTypes[1] { ObjectTypes.Scenery });

	private float m_PowerUpRemaining;

	private TechSequencer.SequenceNode m_SequenceNode;

	public int Priority => m_Priority;

	public ModuleEnergyStore Store => m_EnergyStore;

	public bool IsGenerating { get; private set; }

	public TechEnergy.EnergyType OutputEnergyType => m_OutputEnergyType;

	public void Supply(TechEnergy.EnergyType type, float energyAmount)
	{
		base.block.tank.EnergyRegulator.Supply(type, this, energyAmount);
	}

	public bool ConsumeIfEnough(TechEnergy.EnergyType type, float amount)
	{
		return base.block.tank.EnergyRegulator.ConsumeIfEnough(type, amount);
	}

	public float ConsumeUpToMax(TechEnergy.EnergyType type, float amount)
	{
		return base.block.tank.EnergyRegulator.ConsumeUpToMax(type, amount);
	}

	public float GetSpareStorageCapacity(TechEnergy.EnergyType type)
	{
		if (m_EnergyStore.IsNotNull() && m_EnergyStore.m_EnergyType == type)
		{
			return m_EnergyStore.m_Capacity - m_EnergyStore.CurrentAmount;
		}
		return 0f;
	}

	public float GetCurrentAmount(TechEnergy.EnergyType type)
	{
		return base.block.tank.EnergyRegulator.GetCurrentAmount(type);
	}

	public float GetTotalCapacity(TechEnergy.EnergyType type)
	{
		return base.block.tank.EnergyRegulator.Energy(type).storageTotal;
	}

	public void UpdateSfx()
	{
		if (m_AudioProvider != null)
		{
			m_AudioProvider.SetNoteOn(m_GeneratingSFXType, IsGenerating);
		}
	}

	private bool CheckOutputConditions()
	{
		d.Assert(base.block.tank.IsNotNull(), "ModuleEnergy.CheckOutputConditions - Called while not attached to a tech");
		if ((m_OutputConditions & OutputConditionFlags.Anchored) != 0 && !base.block.tank.IsAnchored)
		{
			return false;
		}
		if ((m_OutputConditions & OutputConditionFlags.DayTime) != 0 && Singleton.Manager<ManTimeOfDay>.inst.NightTime)
		{
			return false;
		}
		if ((m_OutputConditions & OutputConditionFlags.Thermal) != 0 && !m_ThermalSourceInRange)
		{
			return false;
		}
		return true;
	}

	private void OnAttached()
	{
		base.block.tank.EnergyRegulator.Register(this);
		if (m_SequenceNode != null)
		{
			m_SequenceNode.SetComplete(complete: false);
			base.block.tank.Sequencer.RegisterNode(m_SequenceNode, TechSequencer.ChainType.EnergyModule);
		}
	}

	private void OnDetaching()
	{
		base.block.tank.EnergyRegulator.Unregister(this);
		IsGenerating = false;
		m_PowerUpRemaining = m_PowerUpDelay;
		if (m_AnimatorController != null)
		{
			m_AnimatorController.Set(m_HasSupplyBool, value: false);
			m_AnimatorController.Set(m_GeneratingEnergyBool, value: false);
		}
		if (m_SequenceNode != null)
		{
			base.block.tank.Sequencer.UnregisterNode(m_SequenceNode);
		}
	}

	private void OnUpdateSupplyEnergy()
	{
		bool flag = (m_SequenceNode == null || m_SequenceNode.CheckIsReady()) && CheckOutputConditions();
		if (flag)
		{
			m_PowerUpRemaining = Mathf.MoveTowards(m_PowerUpRemaining, 0f, Time.deltaTime);
		}
		else
		{
			m_PowerUpRemaining = m_PowerUpDelay;
		}
		bool flag2 = flag && m_PowerUpRemaining <= 0f;
		IsGenerating = flag2 && m_OutputPerSecond != 0f;
		if (m_SequenceNode != null)
		{
			m_SequenceNode.SetComplete(flag2 || Singleton.Manager<ManPlayer>.inst.SkipPowerupSequencing);
		}
		if (IsGenerating)
		{
			float num = (((m_OutputConditions & OutputConditionFlags.Thermal) != 0) ? m_ThermalSourceInRange.PowerMultiplier : 1f);
			Supply(m_OutputEnergyType, m_OutputPerSecond * num * Time.deltaTime);
		}
		if (m_AnimatorController != null)
		{
			m_AnimatorController.Set(m_HasSupplyBool, flag);
			m_AnimatorController.Set(m_GeneratingEnergyBool, IsGenerating);
		}
	}

	private void OnAnchorStatusChanged(ModuleAnchor anchor)
	{
		if (anchor.IsAnchored)
		{
			Vector3 groundPoint = anchor.GroundPoint;
			if ((m_OutputConditions & OutputConditionFlags.Thermal) == 0)
			{
				return;
			}
			float num = m_ThermalGroundRadius * m_ThermalGroundRadius;
			int pickerMask = Singleton.Manager<ManVisible>.inst.VisiblePickerMask | Globals.inst.layerTerrain.mask;
			{
				foreach (Visible item in Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(groundPoint, m_ThermalGroundRadius, k_MaskScenery, includeTriggers: true, pickerMask))
				{
					if ((bool)item.resdisp.ThermalSource && (item.centrePosition - groundPoint).SetY(0f).sqrMagnitude <= num)
					{
						m_ThermalSourceInRange = item.resdisp.ThermalSource;
						m_ThermalSourceInRange.Cap(capped: true);
						break;
					}
				}
				return;
			}
		}
		if ((bool)m_ThermalSourceInRange)
		{
			m_ThermalSourceInRange.Cap(capped: false);
			m_ThermalSourceInRange = null;
		}
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.poweredUp = m_PowerUpRemaining <= 0f;
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null && serialData2.poweredUp)
		{
			m_PowerUpRemaining = 0f;
		}
	}

	private void PrePool()
	{
		m_ThermalGroundRadius = 0f;
		if ((m_OutputConditions & OutputConditionFlags.Thermal) == 0)
		{
			return;
		}
		Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>(includeInactive: true);
		if (componentsInChildren.Length != 0)
		{
			Bounds bounds = componentsInChildren[0].bounds;
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				bounds.Encapsulate(renderer.bounds);
			}
			m_ThermalGroundRadius = Mathf.Max(bounds.extents.x, bounds.extents.z);
		}
	}

	private void OnPool()
	{
		m_EnergyStore = GetComponent<ModuleEnergyStore>();
		m_AnimatorController = GetComponent<ModuleAnimator>();
		m_AudioProvider = GetComponent<ModuleAudioProvider>();
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		if (m_PowerUpDelay > 0f)
		{
			base.block.serializeEvent.Subscribe(OnSerialize);
		}
		UpdateSupplyEvent.Subscribe(OnUpdateSupplyEnergy);
		ModuleAnchor component = GetComponent<ModuleAnchor>();
		if ((bool)component)
		{
			component.AnchorEvent.Subscribe(OnAnchorStatusChanged);
		}
		if (m_SequentialPowerUp)
		{
			m_SequenceNode = new TechSequencer.SequenceNode(base.block);
		}
	}

	private void OnSpawn()
	{
		m_ThermalSourceInRange = null;
		m_PowerUpRemaining = m_PowerUpDelay;
	}
}
