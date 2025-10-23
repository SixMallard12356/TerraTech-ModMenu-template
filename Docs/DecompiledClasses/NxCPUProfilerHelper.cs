using UnityEngine;

public class NxCPUProfilerHelper : MonoBehaviour
{
	private const string kUpdateLoopIdentifier = "Unity_UpdateLoop";

	private const string kFixedUpdateLoopIdentifier = "Unity_FixedUpdateLoop";

	private void DummyDoNothing()
	{
		if (Singleton.Manager<ManUpdate>.inst != null)
		{
			Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.Update, ManUpdate.Order.First, DoRecordHeartbeat, -1000000);
			Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.Update, ManUpdate.Order.First, StartOfUpdate, -1000000);
			Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.Update, ManUpdate.Order.Last, EndOfUpdate, 1000000);
			Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.FixedUpdate, ManUpdate.Order.First, StartOfFixedUpdate, -1000000);
			Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.FixedUpdate, ManUpdate.Order.Last, EndOfFixedUpdate, 1000000);
			base.enabled = false;
		}
	}

	private void DoRecordHeartbeat()
	{
	}

	private void StartOfUpdate()
	{
	}

	private void EndOfUpdate()
	{
	}

	private void StartOfFixedUpdate()
	{
	}

	private void EndOfFixedUpdate()
	{
	}
}
