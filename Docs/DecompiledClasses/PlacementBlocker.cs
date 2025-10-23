using UnityEngine;

public interface PlacementBlocker
{
	bool GetNearestBlocker(Vector3 worldPos, out Vector3 blockerPos);
}
