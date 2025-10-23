using UnityEngine;

public class HeightAboveTerrainValidator : PositionValidator
{
	[SerializeField]
	private float m_MinHeightAboveTerrain = 1f;

	protected override bool Validate(Vector3 worldPosition, Quaternion worldRotation)
	{
		bool result = false;
		if (Singleton.Manager<ManWorld>.inst.GetTerrainHeight(worldPosition, out var outHeight))
		{
			result = worldPosition.y - outHeight >= m_MinHeightAboveTerrain;
		}
		return result;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(base.transform.position, Vector3.down * m_MinHeightAboveTerrain);
	}
}
