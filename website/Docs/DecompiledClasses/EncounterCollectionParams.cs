using UnityEngine;

public struct EncounterCollectionParams
{
	public Vector3 centrePosition;

	public int maxTotalCount;

	public int maxCoreCount;

	public int maxSideCount;

	public bool includeNearbyHiddenEncounters;

	public float maxNearbyHiddenEncounterRange;
}
