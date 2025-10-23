using System;
using UnityEngine;

[Serializable]
public struct RangeFloat
{
	[SerializeField]
	private float _min;

	[SerializeField]
	private float _max;

	public static RangeFloat zero;

	public static RangeFloat one;

	public static RangeFloat invalid;

	public float min => _min;

	public float max => _max;

	public float extent => _max - _min;

	public RangeFloat(float min, float max)
	{
		_min = min;
		_max = max;
	}

	public static bool operator ==(RangeFloat a, RangeFloat b)
	{
		if (a._min == b._min)
		{
			return a._max == b._max;
		}
		return false;
	}

	public static bool operator !=(RangeFloat a, RangeFloat b)
	{
		if (a._min == b._min)
		{
			return a._max != b._max;
		}
		return true;
	}

	public RangeFloat Encapsulate(float value)
	{
		return new RangeFloat(Mathf.Min(_min, value), Mathf.Max(_max, value));
	}

	public RangeFloat Encapsulate(RangeFloat r)
	{
		return new RangeFloat(Mathf.Min(_min, r._min), Mathf.Max(_max, r._max));
	}

	public override bool Equals(object obj)
	{
		if (!(obj is RangeFloat))
		{
			return false;
		}
		return this == (RangeFloat)obj;
	}

	public override int GetHashCode()
	{
		return (_min.ToIntBitwise() << 16) ^ _max.ToIntBitwise();
	}

	public override string ToString()
	{
		return $"[{_min}->{_max}]";
	}

	static RangeFloat()
	{
		zero = new RangeFloat(0f, 0f);
		one = new RangeFloat(0f, 1f);
		invalid = new RangeFloat(float.MaxValue, float.MinValue);
	}
}
