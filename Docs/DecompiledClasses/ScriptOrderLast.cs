using UnityEngine;

public class ScriptOrderLast : MonoBehaviour
{
	public Event<ManUpdate.Type> UpdateHandler;

	private void Update()
	{
		UpdateHandler.Send(ManUpdate.Type.Update);
	}

	private void LateUpdate()
	{
		UpdateHandler.Send(ManUpdate.Type.LateUpdate);
	}

	private void FixedUpdate()
	{
		UpdateHandler.Send(ManUpdate.Type.FixedUpdate);
	}
}
