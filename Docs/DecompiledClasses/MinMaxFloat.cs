using System;
using UnityEngine;

[Serializable]
public struct MinMaxFloat
{
	public float Min;

	public float Max;

	public float Mean => (Min + Max) / 2f;

	public float Random => UnityEngine.Random.Range(Min, Max);

	public static MinMaxFloat ZeroOne => new MinMaxFloat(0f, 1f);

	public static MinMaxFloat One => new MinMaxFloat(1f, 1f);

	public static MinMaxFloat Infinity => new MinMaxFloat(float.PositiveInfinity, float.PositiveInfinity);

	public MinMaxFloat(float min, float max)
	{
		Min = min;
		Max = max;
	}

	public float Lerp(float t)
	{
		return Min + (Max - Min) * t;
	}

	public float InverseLerp(float value)
	{
		return (value - Min) / (Max - Min);
	}

	public bool Contains(float value)
	{
		if (value >= Min)
		{
			return value <= Max;
		}
		return false;
	}

	public float Clamp(float value)
	{
		return Mathf.Clamp(value, Min, Max);
	}

	public Vector2 ToVector2()
	{
		return new Vector2(Min, Max);
	}

	public static MinMaxFloat operator -(MinMaxFloat mMF, float value)
	{
		mMF.Min -= value;
		mMF.Max -= value;
		return mMF;
	}

	public static MinMaxFloat operator +(MinMaxFloat mMF, float value)
	{
		mMF.Min += value;
		mMF.Max += value;
		return mMF;
	}

	public static MinMaxFloat operator /(MinMaxFloat mMF, float value)
	{
		mMF.Min /= value;
		mMF.Max /= value;
		return mMF;
	}

	public static MinMaxFloat operator *(MinMaxFloat mMF, float value)
	{
		mMF.Min *= value;
		mMF.Max *= value;
		return mMF;
	}
}
