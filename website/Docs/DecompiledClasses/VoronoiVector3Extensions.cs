using UnityEngine;

public static class VoronoiVector3Extensions
{
	public static float GetManhattan(this Vector3 v)
	{
		return Mathf.Abs(v.x) + Mathf.Abs(v.y) + Mathf.Abs(v.z);
	}

	public static float GetChebychev(this Vector3 v)
	{
		return Mathf.Max(Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y)), Mathf.Abs(v.z));
	}

	public static float GetMinkowski4(this Vector3 v)
	{
		return Mathf.Pow(v.x * v.x * v.x * v.x + v.y * v.y * v.y * v.y + v.z * v.z * v.z * v.z, 0.25f);
	}

	public static float GetMinkowski5(this Vector3 v)
	{
		return Mathf.Pow(Mathf.Sqrt(v.x) + Mathf.Sqrt(v.y) + Mathf.Sqrt(v.z), 2f);
	}
}
