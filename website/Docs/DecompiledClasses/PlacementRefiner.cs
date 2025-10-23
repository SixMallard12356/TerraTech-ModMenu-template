using UnityEngine;

public abstract class PlacementRefiner : MonoBehaviour
{
	public abstract bool TryFindExactSpawnTransform(Vector3 scenePos, out Vector3 outPos, out Quaternion outRotation);
}
