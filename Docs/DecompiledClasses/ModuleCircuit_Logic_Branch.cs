using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(ModuleCircuitReceiver), typeof(ModuleCircuitDispensor))]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleCircuit_Logic_Branch : Module, ICircuitDispensor
{
	public enum APUse
	{
		IN_Selector,
		IN_Signal,
		OUT_Selector_Off,
		OUT_Selector_On
	}

	[EnumArray(typeof(APUse))]
	[SerializeField]
	private Vector3[] m_ChargeAPs = new Vector3[0];

	[SerializeField]
	private Visualiser_Anim m_Visualiser;

	private int m_SelectorValue;

	private int m_SignalValue;

	int ICircuitDispensor.GetDispensableCharge(Vector3 outputAP)
	{
		Vector3 other = ((m_SelectorValue > 0) ? m_ChargeAPs[3] : m_ChargeAPs[2]);
		if (!base.block.CircuitNode.Receiver.InitialisedOnNetwork || !outputAP.Approximately(in other, 0.01f))
		{
			return 0;
		}
		return m_SignalValue;
	}

	private void EvaluateBranchState()
	{
		TryGetInputChargeValue(m_ChargeAPs[0], out m_SelectorValue);
		TryGetInputChargeValue(m_ChargeAPs[1], out m_SignalValue);
		if (m_Visualiser != null)
		{
			m_Visualiser.SetOn(m_SelectorValue != 0);
		}
	}

	private bool TryGetInputChargeValue(Vector3 localAP, out int chargeValue)
	{
		if (base.block.CircuitNode.HasConnectionOnAP(localAP, ModuleCircuitNode.ConnexionTypes.Input))
		{
			if (!base.block.CircuitNode.Receiver.InitialisedOnNetwork || !base.block.CircuitNode.Receiver.CurrentChargeData.AllChargeAPsAndCharges.TryGetValue(localAP, out chargeValue))
			{
				chargeValue = 0;
			}
			return true;
		}
		chargeValue = 0;
		return false;
	}

	private void OnChargeChanged(Circuits.BlockChargeData chargeInfo)
	{
		EvaluateBranchState();
	}

	private void OnConnexionsChanged()
	{
		EvaluateBranchState();
	}

	private void OnPool()
	{
		base.block.CircuitNode.ConnexionsUpdatedEvent.Subscribe(OnConnexionsChanged);
	}

	private void OnSpawn()
	{
		base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: true);
		if (m_Visualiser != null)
		{
			m_Visualiser.Reset();
		}
	}

	private void OnRecycle()
	{
		base.block.CircuitNode.Receiver.UnSubscribeFromChargeData(null, OnChargeChanged, null, null);
	}
}
