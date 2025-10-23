#define UNITY_EDITOR
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ScrewTrack : MonoBehaviour
{
	[SerializeField]
	private float screwSpeed = 0.5f;

	[SerializeField]
	private ModuleWheels wheelsModule;

	private float wheelRotation;

	private float screwOffset;

	private void Start()
	{
		d.AssertFormat(wheelsModule, "ScrewTrack failed to find ModuleWheels: {0}", base.transform.GetTopParent().name);
	}

	private void Update()
	{
		wheelRotation = wheelsModule.FirstWheelTireRotation * 57.29578f;
		screwOffset = wheelRotation * screwSpeed;
		base.transform.localRotation = Quaternion.Euler(0f, 0f, screwOffset);
	}
}
