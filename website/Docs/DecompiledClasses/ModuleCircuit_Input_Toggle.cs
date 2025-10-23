using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(ModuleCircuitDispensor))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleCircuit_Input_Toggle : Module, ICircuitDispensor, UIRadialSimpleOnOff.Implementer, INetworkedModule
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public bool m_PowerControlSetting;
	}

	[SerializeField]
	protected Transform m_SwitchTransform;

	[SerializeField]
	protected Vector3 m_ActiveSwitchRotation = Vector3.zero;

	[SerializeField]
	protected Vector3 m_InactiveSwitchRotation = Vector3.zero;

	[SerializeField]
	protected Vector3 m_ActiveSwitchPosition = Vector3.zero;

	[SerializeField]
	protected Vector3 m_InactiveSwitchPosition = Vector3.zero;

	private bool m_PowerControlSetting;

	private NetworkedProperty<BoolParamBlockMessage> net_PowerControlSetting;

	bool UIRadialSimpleOnOff.Implementer.CanOpenMenuOnBlock => true;

	int ICircuitDispensor.GetDispensableCharge(Vector3 outputAP)
	{
		if (!m_PowerControlSetting)
		{
			return 0;
		}
		return base.block.CircuitNode.Dispensor.DefaultChargeStrength;
	}

	private void SetPowerControlState(bool state)
	{
		m_PowerControlSetting = state;
		net_PowerControlSetting.Data.value = state;
		m_SwitchTransform.localRotation = Quaternion.Euler(m_PowerControlSetting ? m_ActiveSwitchRotation : m_InactiveSwitchRotation);
		m_SwitchTransform.localPosition = (m_PowerControlSetting ? m_ActiveSwitchPosition : m_InactiveSwitchPosition);
	}

	void UIRadialSimpleOnOff.Implementer.OnHUDStateChosen(bool state)
	{
		SetPowerControlState(state);
		net_PowerControlSetting.Sync();
	}

	private void OnSerialze(bool saving, TankPreset.BlockSpec blockSpec)
	{
		SerialData serialData;
		if (saving)
		{
			serialData = new SerialData
			{
				m_PowerControlSetting = m_PowerControlSetting
			};
			serialData.Store(blockSpec.saveState);
			return;
		}
		serialData = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData != null)
		{
			SetPowerControlState(serialData.m_PowerControlSetting);
		}
	}

	private void OnSerializeText(bool saving, TankPreset.BlockSpec context, bool onTech)
	{
		if (saving)
		{
			context.Store(this, "toggleState", m_PowerControlSetting);
			return;
		}
		context.TryRetrieve(this, "toggleState", out var boolVal, defaultValue: false);
		SetPowerControlState(boolVal);
	}

	private void OnMPPowerControlSynced(BoolParamBlockMessage msg)
	{
		SetPowerControlState(msg.value);
	}

	private void OnPool()
	{
		net_PowerControlSetting = new NetworkedProperty<BoolParamBlockMessage>(this, TTMsgType.SyncCircuitInputToggle, OnMPPowerControlSynced);
		base.block.serializeEvent.Subscribe(OnSerialze);
		base.block.serializeTextEvent.Subscribe(OnSerializeText);
	}

	private void OnSpawn()
	{
		SetPowerControlState(state: false);
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleCircuit_Input_Toggle;
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
