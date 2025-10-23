#define UNITY_EDITOR
using UnityEngine;

public static class IntVectorExtensions
{
	public static IntVector3 LocalToAP(this Vector3 v)
	{
		return new IntVector3
		{
			x = Mathf.RoundToInt(v.x + v.x),
			y = Mathf.RoundToInt(v.y + v.y),
			z = Mathf.RoundToInt(v.z + v.z)
		};
	}

	public static IntVector3 WorldToAP(this Vector3 v, Transform t)
	{
		d.Assert(condition: false, "Vector3.WorldToAP is SLOW, don't use it please!");
		return t.InverseTransformPoint(v).LocalToAP();
	}
}
