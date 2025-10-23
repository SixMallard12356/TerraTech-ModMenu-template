using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleFuelGauge : Module, ICircuitDispensor
{
	[SerializeField]
	private bool m_ShowOnHUD = true;

	int ICircuitDispensor.GetDispensableCharge(Vector3 ap)
	{
		int result = 0;
		if (base.block.IsAttached)
		{
			result = Mathf.RoundToInt(base.block.tank.Boosters.FuelLevel * 100f);
		}
		return result;
	}

	private void OnAttached()
	{
		base.block.tank.HUDControl.AddHudElement(this, ManHUD.HUDElementType.FuelGauge);
	}

	private void OnDetaching()
	{
		base.block.tank.HUDControl.RemoveHudElement(this);
	}

	private void OnPool()
	{
		if (m_ShowOnHUD)
		{
			base.block.AttachedEvent.Subscribe(OnAttached);
			base.block.DetachingEvent.Subscribe(OnDetaching);
		}
	}
}
