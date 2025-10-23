using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[RequireComponent(typeof(ModuleCircuitReceiver))]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleCircuit_Actuator_Ramp : Module, TechAudio.IModuleAudioProvider, INetworkedModule
{
	[SerializeField]
	private Animation m_Anim;

	[SerializeField]
	private VisiblePhysicsWakerVolume m_VisiblePhysicsWakerVolume;

	[SerializeField]
	private float m_FullExtensionDuration = 1f;

	[SerializeField]
	[FormerlySerializedAs("m_SFX_Actuation_Up")]
	private TechAudio.SFXType m_ActuationSFXType;

	[SerializeField]
	[FormerlySerializedAs("m_ValueSlider")]
	private ModuleHUDSliderControl m_RampAngleValueSlider;

	private bool m_GoingUp;

	private float m_EndAnimPBT01;

	private float m_StartAnimPBT01;

	private float m_DurationToNewAnimPos;

	private float m_DurationRemainingToNewAnimPos;

	private AnimationState m_AnimState;

	private NetworkedProperty<FloatParamBlockMessage> net_Anim01;

	private bool _blockUpdate_Actuating;

	public TechAudio.SFXType SFXType => m_ActuationSFXType;

	public event Action<TechAudio.AudioTickData, FMODEvent.FMODParams> OnAudioTickUpdate;

	private void RefreshRampState(bool changeInstantly)
	{
		m_StartAnimPBT01 = m_AnimState.normalizedTime;
		m_EndAnimPBT01 = Mathf.Clamp01(m_RampAngleValueSlider.AdjustableValueRange.InverseLerp((m_RampAngleValueSlider.Value == 0f) ? ((float)base.block.CircuitNode.Receiver.CurrentChargeData.ChargeStrength) : ((base.block.CircuitNode.Receiver.CurrentChargeData.ChargeStrength > 0) ? m_RampAngleValueSlider.Value : 0f)));
		m_GoingUp = m_StartAnimPBT01 < m_EndAnimPBT01;
		m_DurationToNewAnimPos = m_FullExtensionDuration * Mathf.Abs(m_EndAnimPBT01 - m_StartAnimPBT01);
		m_DurationRemainingToNewAnimPos = m_DurationToNewAnimPos;
		if (changeInstantly)
		{
			m_DurationToNewAnimPos = 0f;
			m_DurationRemainingToNewAnimPos = 0f;
			m_AnimState.normalizedTime = m_EndAnimPBT01;
		}
		if (m_DurationRemainingToNewAnimPos > 0f)
		{
			m_VisiblePhysicsWakerVolume.IsActive = true;
		}
	}

	protected void OnChargeChanged(Circuits.BlockChargeData charge)
	{
		RefreshRampState(changeInstantly: false);
	}

	protected void OnConnectedToCircuitNetwork(bool state)
	{
		RefreshRampState(changeInstantly: false);
	}

	protected void OnInstantRefresh()
	{
		RefreshRampState(changeInstantly: true);
	}

	protected void OnRampAngleSet()
	{
		RefreshRampState(changeInstantly: false);
	}

	protected void OnAttached()
	{
		base.block.tank.TechAudio.AddModule(this);
	}

	protected void OnDetaching()
	{
		base.block.tank.TechAudio.RemoveModule(this);
	}

	protected void OnPostSlowUpdate()
	{
		float num = (m_Anim.isPlaying ? m_AnimState.normalizedTime : m_EndAnimPBT01);
		bool num2 = !num.Approximately(net_Anim01.Data.value, 0.01f);
		net_Anim01.Data.value = num;
		if (num2)
		{
			net_Anim01.Sync();
		}
	}

	protected void OnMPSynced(FloatParamBlockMessage msg)
	{
		if (!ManNetwork.IsHost)
		{
			m_AnimState.normalizedTime = msg.value;
		}
	}

	private void OnSpawn()
	{
		base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
		base.block.CircuitNode.ConnectedToOtherNodesEvent.Subscribe(OnConnectedToCircuitNetwork);
		base.block.BlockUpdate.Subscribe(OnBlockUpdate);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		Circuits.PostSlowUpdate.Subscribe(OnPostSlowUpdate);
		m_Anim.Play();
		m_AnimState.speed = 0f;
	}

	private void OnRecycle()
	{
		base.block.CircuitNode.Receiver.UnSubscribeFromChargeData(null, OnChargeChanged, null, null);
		base.block.CircuitNode.ConnectedToOtherNodesEvent.Unsubscribe(OnConnectedToCircuitNetwork);
		base.block.BlockUpdate.Unsubscribe(OnBlockUpdate);
		base.block.AttachedEvent.Unsubscribe(OnAttached);
		base.block.DetachingEvent.Unsubscribe(OnDetaching);
		Circuits.PostSlowUpdate.Unsubscribe(OnPostSlowUpdate);
	}

	private void OnPool()
	{
		net_Anim01 = new NetworkedProperty<FloatParamBlockMessage>(this, TTMsgType.SyncCircuitActuatorRamp, OnMPSynced);
		m_RampAngleValueSlider.InstantRefreshEvent.Subscribe(OnInstantRefresh);
		m_RampAngleValueSlider.OptionSetEvent.Subscribe(OnRampAngleSet);
		foreach (AnimationState item in m_Anim)
		{
			m_AnimState = item;
		}
	}

	private void Depool()
	{
		m_RampAngleValueSlider.InstantRefreshEvent.Unsubscribe(OnInstantRefresh);
		m_RampAngleValueSlider.OptionSetEvent.Unsubscribe(OnRampAngleSet);
	}

	private void OnBlockUpdate()
	{
		if (this.OnAudioTickUpdate != null)
		{
			TechAudio.AudioTickData value = TechAudio.AudioTickData.ConfigureLoopedADSR(this, SFXType, _blockUpdate_Actuating, _blockUpdate_Actuating ? 1f : 0f, 0.5f);
			this.OnAudioTickUpdate.Send(value, new FMODEvent.FMODParams("Ramp", m_GoingUp ? 1f : 0f));
		}
		_blockUpdate_Actuating = m_DurationRemainingToNewAnimPos != 0f;
		if (_blockUpdate_Actuating)
		{
			m_DurationRemainingToNewAnimPos = Mathf.Max(0f, m_DurationRemainingToNewAnimPos - Time.deltaTime);
			m_AnimState.normalizedTime = Mathf.Lerp(m_StartAnimPBT01, m_EndAnimPBT01, 1f - m_DurationRemainingToNewAnimPos / m_DurationToNewAnimPos);
			if (m_DurationRemainingToNewAnimPos == 0f)
			{
				m_VisiblePhysicsWakerVolume.IsActive = false;
			}
		}
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleCircuit_Actuator_Ramp;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		net_Anim01.Serialise(writer);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		net_Anim01.Deserialise(reader);
	}
}
