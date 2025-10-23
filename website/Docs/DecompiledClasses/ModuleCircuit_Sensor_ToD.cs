using UnityEngine;

[RequireComponent(typeof(ModuleCircuitNode), typeof(ModuleCircuitDispensor))]
public class ModuleCircuit_Sensor_ToD : Module, ICircuitDispensor
{
	[SerializeField]
	protected Transform m_Dial;

	private static float m_DefaultRotation;

	int ICircuitDispensor.GetDispensableCharge(Vector3 outputAP)
	{
		return Singleton.Manager<ManTimeOfDay>.inst.TimeOfDay;
	}

	private void UpdateDial()
	{
		Vector3 eulerAngles = m_Dial.localRotation.eulerAngles;
		eulerAngles.y = m_DefaultRotation - Singleton.Manager<ManTimeOfDay>.inst.TimeOfDayPrecise / 24f * 360f;
		m_Dial.localRotation = Quaternion.Euler(eulerAngles);
	}

	protected void OnAttached()
	{
	}

	protected void OnDetaching()
	{
	}

	private void PrePool()
	{
		m_DefaultRotation = m_Dial.localRotation.eulerAngles.y;
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockFixedUpdate.Subscribe(OnFixedUpdate);
	}

	private void OnFixedUpdate()
	{
		UpdateDial();
	}
}
