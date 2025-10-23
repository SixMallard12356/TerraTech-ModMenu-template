#define UNITY_EDITOR
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class TankTrack : MonoBehaviour
{
	[SerializeField]
	private float trackSpeed = 0.005f;

	private Material trackMaterial;

	private ModuleWheels wheelsModule;

	private float wheelRotation;

	private float trackOffset;

	private void Start()
	{
		trackMaterial = GetComponent<Renderer>().material;
		wheelsModule = this.GetComponentInParents<ModuleWheels>();
		d.Assert(wheelsModule, "TankTrack failed to find ModuleWheels: " + base.transform.GetTopParent().name);
	}

	private void Update()
	{
		wheelRotation = wheelsModule.FirstWheelTireRotation * 57.29578f;
		trackOffset = wheelRotation * trackSpeed;
		trackMaterial.SetTextureOffset("_MainTex", new Vector2(0f, trackOffset));
	}
}
