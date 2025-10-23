using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
[RequireComponent(typeof(ModuleCircuitDispensor))]
public class ModuleCircuit_Input_Button : Module, ICircuitDispensor, IHUDPowerToggleControlledModule, INetworkedModule
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public bool m_PowerControlSetting;
	}

	[SerializeField]
	[Tooltip("The number of circuit ticks to output for once triggered")]
	private int m_NumOutputTicks = 10;

	[SerializeField]
	protected Animation m_Anim;

	private int m_OutputTicksRemaining;

	private NetworkedProperty<BoolParamBlockMessage> net_PowerControlSetting;

	public bool PowerControlSetting
	{
		get
		{
			return m_OutputTicksRemaining > 0;
		}
		set
		{
			bool num = net_PowerControlSetting.Data.value != value;
			SetOutputTicking(value);
			if (num)
			{
				net_PowerControlSetting.Sync();
			}
		}
	}

	public Gradient ToggleGradientOverride => null;

	public bool AutoCloseMenuOnComplete => true;

	public bool CanOpenMenuOnBlock => !PowerControlSetting;

	int ICircuitDispensor.GetDispensableCharge(Vector3 outputAP)
	{
		if (!PowerControlSetting)
		{
			return 0;
		}
		m_OutputTicksRemaining--;
		if (!PowerControlSetting)
		{
			PowerControlSetting = false;
		}
		return base.block.CircuitNode.Dispensor.DefaultChargeStrength;
	}

	private void SetOutputTicking(bool state)
	{
		m_OutputTicksRemaining = (state ? Mathf.Max(1, m_NumOutputTicks) : 0);
		net_PowerControlSetting.Data.value = state;
		if (!PowerControlSetting)
		{
			foreach (AnimationState item in m_Anim)
			{
				item.speed = 1f;
			}
			return;
		}
		if (!(m_Anim != null) || !(m_Anim.clip != null))
		{
			return;
		}
		if (m_Anim.isPlaying)
		{
			m_Anim.Stop();
		}
		m_Anim.Play();
		foreach (AnimationState item2 in m_Anim)
		{
			item2.speed = 0f;
		}
	}

	private void OnMPPowerControlSynced(BoolParamBlockMessage msg)
	{
		SetOutputTicking(msg.value);
	}

	private void OnAttachDetach()
	{
		SetOutputTicking(state: false);
	}

	private void OnPool()
	{
		net_PowerControlSetting = new NetworkedProperty<BoolParamBlockMessage>(this, TTMsgType.SyncCircuitInputButton, OnMPPowerControlSynced);
		base.block.AttachedEvent.Subscribe(OnAttachDetach);
		base.block.DetachingEvent.Subscribe(OnAttachDetach);
	}

	private void OnDepool()
	{
		base.block.AttachedEvent.Unsubscribe(OnAttachDetach);
		base.block.DetachingEvent.Unsubscribe(OnAttachDetach);
	}

	private void OnSpawn()
	{
		m_Anim.Play();
		foreach (AnimationState item in m_Anim)
		{
			item.normalizedTime = 1f;
		}
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleCircuit_Input_Button;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		net_PowerControlSetting.Serialise(writer);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		net_PowerControlSetting.Deserialise(reader);
	}
}
