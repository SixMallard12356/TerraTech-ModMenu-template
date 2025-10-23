using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ScriptOrderFirst : MonoBehaviour
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
