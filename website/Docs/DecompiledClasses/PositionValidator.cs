using UnityEngine;

public abstract class PositionValidator : MonoBehaviour
{
	public bool Validate(Transform prefabRoot, Vector3 prefabSpawnPosition, Quaternion prefabSpawnRotation)
	{
		Transform transform = base.transform;
		Vector3 worldPosition;
		Quaternion worldRotation;
		if (transform == prefabRoot)
		{
			worldPosition = prefabSpawnPosition;
			worldRotation = prefabSpawnRotation;
		}
		else
		{
			Vector3 vector = transform.position - prefabRoot.position;
			Quaternion quaternion = transform.rotation * Quaternion.Inverse(prefabRoot.rotation);
			worldPosition = prefabSpawnPosition + prefabSpawnRotation * vector;
			worldRotation = prefabSpawnRotation * quaternion;
		}
		return Validate(worldPosition, worldRotation);
	}

	protected abstract bool Validate(Vector3 worldPosition, Quaternion worldRotation);
}
