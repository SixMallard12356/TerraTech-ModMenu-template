using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class SwitchableUpdater : MonoBehaviour
{
	public EventNoParams UpdateEvent;

	public EventNoParams FixedUpdateEvent;

	private void Update()
	{
		UpdateEvent.Send();
	}

	private void FixedUpdate()
	{
		FixedUpdateEvent.Send();
	}
}
