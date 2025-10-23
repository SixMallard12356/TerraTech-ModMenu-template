#define UNITY_EDITOR
using System;
using UnityEngine;

[Serializable]
public struct IntBounds
{
	private IntVector3 _min;

	private IntVector3 _max;

	public IntVector3 min
	{
		get
		{
			return _min;
		}
		set
		{
			_min = value;
		}
	}

	public IntVector3 max
	{
		get
		{
			return _max;
		}
		set
		{
			_max = value;
		}
	}

	public IntVector3 size => _max - _min;

	public IntBounds(IntVector3 min, IntVector3 max)
	{
		_min = min;
		_max = max;
	}

	public IntBounds(Bounds bounds)
	{
		_min = bounds.min;
		_max = bounds.max;
	}

	public static implicit operator Bounds(IntBounds b)
	{
		Bounds result = default(Bounds);
		result.SetMinMax(b._min, b._max);
		return result;
	}

	public void Set(IntVector3 min, IntVector3 max)
	{
		_min = min;
		_max = max;
	}

	public IntBounds Translate(IntVector3 offset)
	{
		return new IntBounds(_min + offset, _max + offset);
	}

	public IntBounds Union(IntBounds other)
	{
		return new IntBounds(IntVector3.Min(_min, other._min), IntVector3.Max(_max, other._max));
	}

	public IntBounds Intersection(IntBounds other)
	{
		return new IntBounds(IntVector3.Max(_min, other._min), IntVector3.Min(_max, other._max));
	}

	public IntBounds Clamp(int lower, int upper)
	{
		return new IntBounds(IntVector3.Max(_min, new IntVector3(lower, lower, lower)), IntVector3.Min(_max, new IntVector3(upper, upper, upper)));
	}

	public static bool operator ==(IntBounds a, IntBounds b)
	{
		if (a._min == b._min)
		{
			return a._max == b._max;
		}
		return false;
	}

	public static bool operator !=(IntBounds a, IntBounds b)
	{
		if (!(a._min != b._min))
		{
			return a._max != b._max;
		}
		return true;
	}

	public override bool Equals(object other)
	{
		return (IntBounds)other == this;
	}

	public override int GetHashCode()
	{
		d.Assert(condition: false, "please replace this implementation, if GetHashCode() is actually needed to work");
		return (_min.GetHashCode() << 8) | _max.GetHashCode();
	}

	public override string ToString()
	{
		return $"{{{_min} - {_max}}}";
	}
}
