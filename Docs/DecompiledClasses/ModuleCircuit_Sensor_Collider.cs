using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class ModuleCircuit_Sensor_Collider : Module, ICircuitDispensor, TechAudio.IModuleAudioProvider
{
	[Serializable]
	protected class DetectionOptionConfig
	{
		[Flags]
		public enum ConfigFlagTypes
		{
			DetectTechs = 1,
			DetectLooseBlocks = 2,
			OnlySameTeamTechs = 4,
			OnlyEnemyTechs = 8
		}

		[SerializeField]
		[HideInInspector]
		public string Name;

		[SerializeField]
		public Globals.ObjectLayer.Group.Type layerGroup;

		[EnumFlag]
		[SerializeField]
		public ConfigFlagTypes m_AdditionalDetectionFlags;

		public int LayerMask => Globals.inst.layerGroups[layerGroup];

		public void OnValidate()
		{
			Name = layerGroup.ToString().ToUpper() + ((m_AdditionalDetectionFlags == (ConfigFlagTypes)0) ? "" : " + additional flags");
		}
	}

	[SerializeField]
	private TechAudio.SFXType m_SensingSFXType = TechAudio.SFXType.EXP_Circuits_Sensor_Tech_Loop;

	[SerializeField]
	private DetectionOptionConfig[] m_DetectionOptions = new DetectionOptionConfig[0];

	[SerializeField]
	[FormerlySerializedAs("m_BeamLayerControl")]
	protected ModuleHUDContextControl_ColorPickerField m_LayerControl;

	private bool m_IsActive;

	private bool m_IsTriggered;

	private int m_TriggeredFixedFrameCount;

	private int m_LastUpdateFixedFrameCount;

	protected int m_DetectionLayerMask;

	private DetectionOptionConfig.ConfigFlagTypes m_AdditionalDetectionFlags;

	private static Collider[] _s_CachedSensedColliders = new Collider[4];

	public TechAudio.SFXType SFXType => m_SensingSFXType;

	public bool IsActiveAndTriggered
	{
		get
		{
			if (m_IsActive)
			{
				return m_IsTriggered;
			}
			return false;
		}
	}

	public event Action<TechAudio.AudioTickData, FMODEvent.FMODParams> OnAudioTickUpdate;

	protected abstract int GetSensedColliders(ref Collider[] sensedCollidersCache);

	protected abstract void OnSetActive(bool state);

	private void SetLayers(UIBlockContext_IconSelectionField.Option[] layerOptions)
	{
		m_DetectionLayerMask = 0;
		m_AdditionalDetectionFlags = (DetectionOptionConfig.ConfigFlagTypes)0;
		for (int i = 0; i < layerOptions.Length; i++)
		{
			m_DetectionLayerMask |= m_DetectionOptions[layerOptions[i].m_Params.m_Return_Int].LayerMask;
			m_AdditionalDetectionFlags |= m_DetectionOptions[layerOptions[i].m_Params.m_Return_Int].m_AdditionalDetectionFlags;
		}
		ResetTriggeredData();
	}

	private void SetActive(bool state)
	{
		m_IsActive = state;
		OnSetActive(m_IsActive);
		ResetTriggeredData();
	}

	private void ResetTriggeredData()
	{
		m_TriggeredFixedFrameCount = -1;
		m_LastUpdateFixedFrameCount = 0;
		m_IsTriggered = false;
	}

	private void Sense()
	{
		if (!m_IsActive)
		{
			return;
		}
		int sensedColliders = GetSensedColliders(ref _s_CachedSensedColliders);
		for (int i = 0; i < sensedColliders; i++)
		{
			if (_s_CachedSensedColliders[i].isTrigger)
			{
				continue;
			}
			if (((1 << _s_CachedSensedColliders[i].gameObject.layer) & (int)Globals.inst.layerGroups[Globals.ObjectLayer.Group.Type.PhysicalTech]) != 0)
			{
				Visible visible = Singleton.Manager<ManVisible>.inst.FindVisible(_s_CachedSensedColliders[i]);
				if (visible != null && visible.type == ObjectTypes.Block)
				{
					if (visible.block.tank.IsNull())
					{
						if ((m_AdditionalDetectionFlags & DetectionOptionConfig.ConfigFlagTypes.DetectLooseBlocks) == 0)
						{
							continue;
						}
					}
					else
					{
						if (visible.block.tank == base.block.tank || (m_AdditionalDetectionFlags & DetectionOptionConfig.ConfigFlagTypes.DetectTechs) == 0)
						{
							continue;
						}
						if ((m_AdditionalDetectionFlags & (DetectionOptionConfig.ConfigFlagTypes.OnlySameTeamTechs | DetectionOptionConfig.ConfigFlagTypes.OnlyEnemyTechs)) != 0)
						{
							bool num = (m_AdditionalDetectionFlags & DetectionOptionConfig.ConfigFlagTypes.OnlySameTeamTechs) != 0 && Tank.IsFriendly(base.block.tank.Team, visible.block.tank.Team);
							bool flag = (m_AdditionalDetectionFlags & DetectionOptionConfig.ConfigFlagTypes.OnlyEnemyTechs) != 0 && Tank.IsEnemy(base.block.tank.Team, visible.block.tank.Team);
							if (!(num || flag))
							{
								continue;
							}
						}
					}
				}
			}
			m_TriggeredFixedFrameCount = Singleton.instance.FixedFrameCount;
			break;
		}
	}

	int ICircuitDispensor.GetDispensableCharge(Vector3 outputAP)
	{
		m_IsTriggered = m_TriggeredFixedFrameCount >= m_LastUpdateFixedFrameCount;
		m_LastUpdateFixedFrameCount = Singleton.instance.FixedFrameCount;
		if (!m_IsTriggered)
		{
			return 0;
		}
		return base.block.CircuitNode.Dispensor.DefaultChargeStrength;
	}

	private void OnLayersSet()
	{
		SetLayers(m_LayerControl.CurrentOptions);
	}

	private void OnSFXUpdate()
	{
		if (this.OnAudioTickUpdate != null)
		{
			TechAudio.AudioTickData value = TechAudio.AudioTickData.ConfigureLoopedADSR(this, SFXType, IsActiveAndTriggered, IsActiveAndTriggered ? 1f : 0f, 0.5f);
			this.OnAudioTickUpdate.Send(value, FMODEvent.FMODParams.empty);
		}
	}

	private void OnAttached()
	{
		base.block.tank.TechAudio.AddModule(this);
		SetActive(state: true);
	}

	private void OnDetaching()
	{
		base.block.tank.TechAudio.RemoveModule(this);
		SetActive(state: false);
	}

	private void OnPool()
	{
		m_LayerControl.OptionSetEvent.Subscribe(OnLayersSet);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockFixedUpdate.Subscribe(OnFixedUpdate);
		base.block.BlockUpdate.Subscribe(OnSFXUpdate);
		SetActive(state: false);
	}

	private void OnSpawn()
	{
		SetActive(state: false);
	}

	private void OnFixedUpdate()
	{
		Sense();
	}

	private void OnValidate()
	{
		DetectionOptionConfig[] detectionOptions = m_DetectionOptions;
		for (int i = 0; i < detectionOptions.Length; i++)
		{
			detectionOptions[i].OnValidate();
		}
	}
}
