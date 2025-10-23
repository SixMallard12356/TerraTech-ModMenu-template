using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleLight : Module
{
	[SerializeField]
	private Light[] m_Lights;

	[SerializeField]
	private float m_RotateSpeed;

	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	private TargetAimer m_TargetAimer;

	private ModuleAnimator m_AnimatorController;

	private AnimatorBool m_LightsOnBool = new AnimatorBool("LightsOn");

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

	private void RefreshLightsActive()
	{
		bool enable = false;
		if (base.block.IsAttached)
		{
			enable = (CircuitControlled ? (base.block.CircuitNode.Receiver.CurrentChargeData > 0) : Singleton.Manager<ManTimeOfDay>.inst.NightTime);
		}
		EnableLights(enable);
	}

	private void EnableLights(bool enable)
	{
		for (int i = 0; i < m_Lights.Length; i++)
		{
			m_Lights[i].enabled = enable;
		}
		if (m_AnimatorController != null && m_AnimatorController.Inited)
		{
			m_AnimatorController.Set(m_LightsOnBool, enable);
		}
		base.block.SetNightTimeVisualsActive(enable);
	}

	private void OnAttached()
	{
		m_TargetAimer?.Reset();
		RefreshLightsActive();
	}

	private void OnDetached()
	{
		RefreshLightsActive();
	}

	private void OnDayNightChanged(bool ended)
	{
		RefreshLightsActive();
	}

	protected void OnChargeChanged(Circuits.BlockChargeData charge)
	{
		RefreshLightsActive();
	}

	protected void OnConnectedToCircuitNetwork(bool state)
	{
		RefreshLightsActive();
	}

	private void PrePool()
	{
		if (m_IsUsedOnCircuit)
		{
			this.SetupForCircuitInput();
		}
		LightShadows blockLightShadowType = QualitySettingsExtended.BlockLightShadowType;
		for (int i = 0; i < m_Lights.Length; i++)
		{
			if (m_Lights[i].shadows > blockLightShadowType)
			{
				m_Lights[i].shadows = blockLightShadowType;
			}
		}
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachedEvent.Subscribe(OnDetached);
		if (m_IsUsedOnCircuit)
		{
			base.block.CircuitNode.ConnectedToOtherNodesEvent.Subscribe(OnConnectedToCircuitNetwork);
			base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
		}
		m_AnimatorController = GetComponent<ModuleAnimator>();
		m_TargetAimer = GetComponent<TargetAimer>();
		if ((bool)m_TargetAimer)
		{
			base.block.BlockUpdate.Subscribe(OnUpdate);
			m_TargetAimer.Init(base.block, 0.5f, null);
		}
	}

	private void OnSpawn()
	{
		RefreshLightsActive();
		Singleton.Manager<ManTimeOfDay>.inst.DayNightChangedEvent.Subscribe(OnDayNightChanged);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManTimeOfDay>.inst.DayNightChangedEvent.Unsubscribe(OnDayNightChanged);
	}

	private void OnUpdate()
	{
		if (base.block.IsAttached)
		{
			m_TargetAimer.UpdateAndAimAtTarget(m_RotateSpeed);
		}
	}
}
