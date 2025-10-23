using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(ModuleCircuitReceiver))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public abstract class ModuleCircuit_Display : Module
{
	[SerializeField]
	protected MinMaxFloat m_ChargeDisplayRange;

	protected abstract void OnFrameChargeChanged(Circuits.BlockChargeData newCharge);

	protected abstract void OnFrameChargeChanged(int newChargeStrength);

	protected abstract void ResetDisplay();

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(ResetDisplay);
		base.block.DetachingEvent.Subscribe(ResetDisplay);
	}

	private void OnDepool()
	{
		base.block.AttachedEvent.Unsubscribe(ResetDisplay);
		base.block.DetachingEvent.Unsubscribe(ResetDisplay);
	}

	private void OnSpawn()
	{
		OnFrameChargeChanged(base.block.CircuitNode.Receiver.CurrentChargeData);
		base.block.CircuitNode.Receiver.SubscribeToChargeData(null, null, null, OnFrameChargeChanged, requireExtensiveChargeData: false);
	}

	private void OnRecycle()
	{
		base.block.CircuitNode.Receiver.UnSubscribeFromChargeData(null, null, null, OnFrameChargeChanged);
	}
}
