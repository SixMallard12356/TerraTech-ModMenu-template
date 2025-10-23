using UnityEngine;

public class TerrainIntersectionValidator : PositionValidator
{
	[SerializeField]
	private float m_TestWidth = 5f;

	[SerializeField]
	private bool m_FailIfIntersection = true;

	protected override bool Validate(Vector3 worldPosition, Quaternion worldRotation)
	{
		bool flag = false;
		Vector3 vector = worldRotation * Vector3.right;
		Vector3 vector2 = vector * m_TestWidth * 0.5f;
		if (Physics.Raycast(worldPosition - vector2, vector, out var hitInfo, m_TestWidth, Globals.inst.layerTerrain.mask, QueryTriggerInteraction.Ignore))
		{
			flag = true;
		}
		if (!flag && Physics.Raycast(worldPosition + vector2, -vector, out hitInfo, m_TestWidth, Globals.inst.layerTerrain.mask, QueryTriggerInteraction.Ignore))
		{
			flag = true;
		}
		return flag != m_FailIfIntersection;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(base.transform.position, -base.transform.right * m_TestWidth * 0.5f);
		Gizmos.DrawRay(base.transform.position, base.transform.right * m_TestWidth * 0.5f);
	}
}
