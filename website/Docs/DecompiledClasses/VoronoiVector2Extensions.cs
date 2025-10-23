using UnityEngine;

public static class VoronoiVector2Extensions
{
	public static float GetManhattan(this Vector2 v)
	{
		return ((v.x < 0f) ? (0f - v.x) : v.x) + ((v.y < 0f) ? (0f - v.y) : v.y);
	}

	public static float GetChebychev(this Vector2 v)
	{
		float num = ((v.x < 0f) ? (0f - v.x) : v.x);
		float num2 = ((v.y < 0f) ? (0f - v.y) : v.y);
		if (!(num > num2))
		{
			return num2;
		}
		return num;
	}

	public static float GetMinkowski4(this Vector2 v)
	{
		return Mathf.Pow(v.x * v.x * v.x * v.x + v.y * v.y * v.y * v.y, 0.25f);
	}

	public static float GetMinkowski5(this Vector2 v)
	{
		return Mathf.Pow(Mathf.Sqrt(v.x) + Mathf.Sqrt(v.y), 2f);
	}
}
