using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[RequireComponent(typeof(ModuleCircuitReceiver), typeof(ModuleHUDSliderControl))]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleCircuit_Actuator_Repulsor : Module, TechAudio.IModuleAudioProvider
{
	[SerializeField]
	protected Vector3 m_RepulseDirectionalForce = Vector3.up;

	[SerializeField]
	protected RepulsionPlane m_RepulsionPlane;

	[SerializeField]
	protected ModuleHUDSliderControl m_AdjustableForceSlider;

	[SerializeField]
	protected TechAudio.SFXType m_ActuatingSFXType = TechAudio.SFXType.EXP_Circuits_Actuator_Repulsor_Loop;

	[SerializeField]
	private float m_ChargeToRepulsionForceMultiplier = 0.1f;

	public TechAudio.SFXType SFXType => m_ActuatingSFXType;

	public event Action<TechAudio.AudioTickData, FMODEvent.FMODParams> OnAudioTickUpdate;

	private void Reset()
	{
		m_RepulsionPlane.SetRepulsing(state: false);
	}

	private void RefreshRepulsing()
	{
		bool flag = base.block.IsAttached && base.block.CircuitReceiver.CurrentChargeData > 0;
		bool flag2 = m_AdjustableForceSlider.Value == 0f;
		if (flag && flag2)
		{
			float value = (float)base.block.CircuitNode.Receiver.CurrentChargeData.ChargeStrength * m_ChargeToRepulsionForceMultiplier;
			value = Mathf.Clamp(value, m_AdjustableForceSlider.AdjustableValueRange.x, m_AdjustableForceSlider.AdjustableValueRange.y);
			m_RepulsionPlane.SetRepulsionForce(value);
		}
		m_RepulsionPlane.SetRepulsing(flag);
	}

	protected void OnChargeChanged(Circuits.BlockChargeData newCharge)
	{
		RefreshRepulsing();
	}

	protected void OnValueSet()
	{
		if (m_AdjustableForceSlider.Value != 0f)
		{
			m_RepulsionPlane.SetRepulsionForce(m_AdjustableForceSlider.Value);
		}
		RefreshRepulsing();
	}

	private void OnSFXUpdate()
	{
		if (this.OnAudioTickUpdate != null)
		{
			TechAudio.AudioTickData value = TechAudio.AudioTickData.ConfigureLoopedADSR(this, SFXType, m_RepulsionPlane.IsRepulsing, m_RepulsionPlane.IsRepulsing ? 1f : 0f, 0.5f);
			this.OnAudioTickUpdate.Send(value, new FMODEvent.FMODParams("RepulsorDistance", m_RepulsionPlane.CurrentRepulsionPenetration01));
		}
	}

	protected void OnAttached()
	{
		base.block.tank.TechAudio.AddModule(this);
		base.block.BlockUpdate.Subscribe(OnSFXUpdate);
	}

	protected void OnDetaching()
	{
		base.block.tank.TechAudio.RemoveModule(this);
		base.block.BlockUpdate.Unsubscribe(OnSFXUpdate);
		Reset();
	}

	private void OnPool()
	{
		m_AdjustableForceSlider.OptionSetEvent.Subscribe(OnValueSet);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		m_RepulsionPlane.Init(base.block);
	}

	private void OnDepool()
	{
		base.block.AttachedEvent.Unsubscribe(OnAttached);
		base.block.DetachingEvent.Unsubscribe(OnDetaching);
	}

	private void OnSpawn()
	{
		Reset();
		RefreshRepulsing();
		base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
	}

	private void OnRecycle()
	{
		Reset();
		base.block.CircuitNode.Receiver.UnSubscribeFromChargeData(null, OnChargeChanged, null, null);
	}
}
