using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleAltimeter : Module, ICircuitDispensor
{
	[SerializeField]
	private int m_Priority;

	[SerializeField]
	private bool m_ShowOnHUD = true;

	private int m_RecordedAltitude;

	public int GetPriority()
	{
		return m_Priority;
	}

	int ICircuitDispensor.GetDispensableCharge(Vector3 outputAP)
	{
		return m_RecordedAltitude;
	}

	private void OnAttached()
	{
		base.block.tank.HUDControl.AddHudElement(this, ManHUD.HUDElementType.Altimeter);
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
		base.block.BlockUpdate.Subscribe(OnUpdate);
	}

	private void OnUpdate()
	{
		m_RecordedAltitude = (base.block.IsAttached ? ((int)GameUnits.GetAltitude(base.block.centreOfMassWorld.y)) : 0);
	}
}
