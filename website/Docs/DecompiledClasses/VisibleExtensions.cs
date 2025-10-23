#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public static class VisibleExtensions
{
	public static IEnumerable<Visible.ConeFiltered> ConeFilter(this IEnumerable<Visible> visibles, float range, float angle, Vector3 centre, Vector3 forward, bool ignoreY, Func<Visible, bool> ignorePredicate)
	{
		float angleDotThreshold = Mathf.Cos((float)Math.PI / 180f * angle / 2f);
		float distSqThreshold = range * range;
		foreach (Visible visible in visibles)
		{
			d.Assert(visible.type != ObjectTypes.Null);
			if (ignorePredicate(visible))
			{
				continue;
			}
			Vector3 vector = visible.centrePosition - centre;
			if (ignoreY)
			{
				vector.y = 0f;
			}
			float sqrMagnitude = vector.sqrMagnitude;
			if (!((visible.type == ObjectTypes.Vehicle) ? (!visible.tank.BoundsIntersectSphere(centre, range)) : (sqrMagnitude > distSqThreshold)))
			{
				float num;
				if (ignoreY)
				{
					Vector3 normalized = new Vector3(forward.x, 0f, forward.z).normalized;
					Vector3 normalized2 = new Vector3(vector.x, 0f, vector.z).normalized;
					num = Vector3.Dot(normalized, normalized2);
				}
				else
				{
					num = Vector3.Dot(forward.normalized, vector.normalized);
				}
				if (!(num < angleDotThreshold))
				{
					yield return new Visible.ConeFiltered
					{
						visible = visible,
						distSq = sqrMagnitude,
						angleDot = num
					};
				}
			}
		}
	}
}
