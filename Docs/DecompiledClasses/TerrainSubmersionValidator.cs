using UnityEngine;

public class TerrainSubmersionValidator : PositionValidator
{
	[SerializeField]
	private float m_MinDistBelowTerrainSurface;

	protected override bool Validate(Vector3 worldPosition, Quaternion worldRotation)
	{
		bool result = false;
		if (Singleton.Manager<ManWorld>.inst.GetTerrainHeight(worldPosition, out var outHeight))
		{
			result = outHeight - worldPosition.y >= m_MinDistBelowTerrainSurface;
		}
		return result;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(base.transform.position, Vector3.up * m_MinDistBelowTerrainSurface);
	}
}
