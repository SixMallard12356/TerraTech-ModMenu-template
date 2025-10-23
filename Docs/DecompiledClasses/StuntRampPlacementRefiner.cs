using System;
using UnityEngine;

public class StuntRampPlacementRefiner : PlacementRefiner
{
	[SerializeField]
	private float m_FlatTerrainRadius = 15f;

	[SerializeField]
	[Tooltip("Maximum average slope over the entire area.")]
	private float m_MaxAverageSlope = 30f;

	[SerializeField]
	[Tooltip("Maximum slope from one sample point to the next")]
	private float m_MaxIndividualSlope = 30f;

	[SerializeField]
	[Tooltip("If set, it will place the ramp in the direction following the natural slope (height increase) in the terrain. This usually creates nice natural looking ramps.")]
	private bool m_PlaceInSlopeDirection = true;

	[SerializeField]
	[Range(0f, 1f)]
	private float m_DistanceAwayFromCentre = 0.75f;

	[Tooltip("Wether to match the normal of the terrain, or place it pointing straight up")]
	[SerializeField]
	private bool m_MatchTerrainNormal = true;

	[Tooltip("How much the normal in the exact spawn location will affect the placement. Interpolates between the average normal of the area, and the normal at the placement position. If left at 0 the average normal is used. Only used if MatchTerrainNormal is set")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_ExactLocationNormalBias = 0.5f;

	[SerializeField]
	private PositionValidator[] m_PositionValidators;

	public override bool TryFindExactSpawnTransform(Vector3 scenePos, out Vector3 outPos, out Quaternion outRotation)
	{
		bool result = false;
		outPos = scenePos;
		outRotation = Quaternion.identity;
		if (TryFindDesiredSpawnTransformInFlatArea(scenePos, out var outPos2, out var outRotation2) && Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in outPos2) != null && PassesPositionValidators(outPos2, outRotation2))
		{
			result = true;
			outPos = outPos2;
			outRotation = outRotation2;
		}
		return result;
	}

	private bool TryFindDesiredSpawnTransformInFlatArea(Vector3 scenePos, out Vector3 outPos, out Quaternion outRotation)
	{
		bool result = false;
		outPos = scenePos;
		outRotation = Quaternion.identity;
		Vector3 toDirection = Vector3.up;
		if (Singleton.Manager<ManWorld>.inst.QueryCurvature(scenePos, m_FlatTerrainRadius, out Vector3 averageNormal, out float _, out float largestHeightVarianceBetweenPoints))
		{
			bool flag = false;
			if (Mathf.Acos(Vector3.Dot(averageNormal, Vector3.up)) * 57.29578f < m_MaxAverageSlope)
			{
				float flatTerrainRadius = m_FlatTerrainRadius;
				float num = Mathf.Tan(m_MaxIndividualSlope * ((float)Math.PI / 180f)) * flatTerrainRadius;
				flag = largestHeightVarianceBetweenPoints <= num;
			}
			if (flag)
			{
				result = true;
				toDirection = averageNormal;
				if (m_PlaceInSlopeDirection)
				{
					Vector3 normalized = Vector3.Cross(Vector3.up, averageNormal).normalized;
					Vector3 normalized2 = Vector3.Cross(Vector3.up, normalized).normalized;
					float num2 = m_FlatTerrainRadius * m_DistanceAwayFromCentre;
					Vector3 vector = scenePos + normalized2 * num2;
					if (Singleton.Manager<ManWorld>.inst.GetTerrainHeight(vector, out var outHeight))
					{
						outPos = vector.SetY(outHeight);
						outRotation = Quaternion.LookRotation(normalized2.SetY(0f), Vector3.up);
					}
					else
					{
						result = false;
					}
				}
				else
				{
					outPos = scenePos;
					outRotation = Quaternion.Euler(0f, UnityEngine.Random.value * 360f, 0f);
				}
			}
		}
		if (m_MatchTerrainNormal)
		{
			Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, toDirection);
			if (m_ExactLocationNormalBias > 0f)
			{
				Singleton.Manager<ManWorld>.inst.GetTerrainNormal(outPos, out var _);
				quaternion = Quaternion.Slerp(quaternion, Quaternion.FromToRotation(Vector3.up, toDirection), m_ExactLocationNormalBias);
			}
			outRotation *= quaternion;
		}
		return result;
	}

	public bool PassesPositionValidators(Vector3 worldPos, Quaternion orientation)
	{
		bool result = true;
		if (m_PositionValidators != null)
		{
			Transform prefabRoot = base.transform;
			for (int i = 0; i < m_PositionValidators.Length; i++)
			{
				if (!m_PositionValidators[i].Validate(prefabRoot, worldPos, orientation))
				{
					result = false;
					break;
				}
			}
		}
		return result;
	}
}
