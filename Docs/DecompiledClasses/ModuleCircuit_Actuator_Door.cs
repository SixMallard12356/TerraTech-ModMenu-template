using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.NullChecks, false)]
[RequireComponent(typeof(ModuleCircuitReceiver))]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleCircuit_Actuator_Door : Module, TechAudio.IModuleAudioProvider, INetworkedModule
{
	[SerializeField]
	private Animation m_Anim;

	[SerializeField]
	private VisiblePhysicsWakerVolume m_VisiblePhysicsWakerVolume;

	[SerializeField]
	[Header("Audio")]
	[FormerlySerializedAs("m_LoopingSFXType_Up")]
	private TechAudio.SFXType m_ActuatingSFXType = TechAudio.SFXType.BFLaserFlyingSaucer;

	private AnimationState m_AnimState;

	private bool m_DoorState;

	private NetworkedProperty<FloatParamBlockMessage> net_Anim01;

	public TechAudio.SFXType SFXType => m_ActuatingSFXType;

	public event Action<TechAudio.AudioTickData, FMODEvent.FMODParams> OnAudioTickUpdate;

	private void RefreshDoorState(bool instantChange = false)
	{
		bool flag = base.block.IsAttached && base.block.CircuitNode.Receiver.CurrentChargeData > 0;
		if (flag != m_DoorState || instantChange)
		{
			m_DoorState = flag;
			float num = (m_Anim.isPlaying ? m_AnimState.normalizedTime : (m_DoorState ? 0f : 1f));
			float num2 = (m_DoorState ? 1f : 0f);
			if (m_Anim.isPlaying)
			{
				m_Anim.Stop();
			}
			m_Anim.Play();
			m_VisiblePhysicsWakerVolume.IsActive = true;
			m_AnimState.speed = (m_DoorState ? 1f : (-1f));
			m_AnimState.normalizedTime = (instantChange ? num2 : num);
		}
	}

	private void OnSFXUpdate()
	{
		if (this.OnAudioTickUpdate != null)
		{
			TechAudio.AudioTickData value = TechAudio.AudioTickData.ConfigureLoopedADSR(this, SFXType, m_Anim.isPlaying, m_Anim.isPlaying ? 1f : 0f, 0.5f);
			this.OnAudioTickUpdate.Send(value, new FMODEvent.FMODParams("Extension", m_AnimState.normalizedTime));
		}
	}

	protected void OnChargeChanged(Circuits.BlockChargeData charge)
	{
		RefreshDoorState();
	}

	protected void OnConnectedToCircuitNetwork(bool state)
	{
		RefreshDoorState();
	}

	protected void OnChargeValueInstantlyRefreshed()
	{
		RefreshDoorState(instantChange: true);
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
	}

	protected void OnDetached()
	{
		RefreshDoorState(instantChange: true);
	}

	protected void OnPostSlowUpdate()
	{
		float num = (m_Anim.isPlaying ? m_AnimState.normalizedTime : (m_DoorState ? 1f : 0f));
		bool num2 = !num.Approximately(net_Anim01.Data.value, 0.01f);
		net_Anim01.Data.value = num;
		if (num2)
		{
			net_Anim01.Data.value = num;
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
		base.block.BlockFixedUpdate.Subscribe(OnFixedUpdate);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.DetachedEvent.Subscribe(OnDetached);
		Circuits.PostSlowUpdate.Subscribe(OnPostSlowUpdate);
		RefreshDoorState(instantChange: true);
	}

	private void OnRecycle()
	{
		base.block.BlockFixedUpdate.Unsubscribe(OnFixedUpdate);
		base.block.AttachedEvent.Unsubscribe(OnAttached);
		base.block.DetachingEvent.Unsubscribe(OnDetaching);
		base.block.DetachedEvent.Unsubscribe(OnDetached);
		Circuits.PostSlowUpdate.Unsubscribe(OnPostSlowUpdate);
	}

	private void OnPool()
	{
		net_Anim01 = new NetworkedProperty<FloatParamBlockMessage>(this, TTMsgType.SyncCircuitActuatorDoor, OnMPSynced);
		base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
		base.block.CircuitNode.ConnectedToOtherNodesEvent.Subscribe(OnConnectedToCircuitNetwork);
		base.block.CircuitNode.Receiver.InstantRefreshEvent.Subscribe(OnChargeValueInstantlyRefreshed);
		foreach (AnimationState item in m_Anim)
		{
			m_AnimState = item;
		}
	}

	private void OnDepool()
	{
		base.block.CircuitNode.Receiver.UnSubscribeFromChargeData(null, OnChargeChanged, null, null);
		base.block.CircuitNode.ConnectedToOtherNodesEvent.Unsubscribe(OnConnectedToCircuitNetwork);
		base.block.CircuitNode.Receiver.InstantRefreshEvent.Unsubscribe(OnChargeValueInstantlyRefreshed);
	}

	private void OnFixedUpdate()
	{
		if (m_VisiblePhysicsWakerVolume.IsActive && !m_Anim.isPlaying)
		{
			m_VisiblePhysicsWakerVolume.IsActive = false;
		}
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleCircuit_Actuator_Door;
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
