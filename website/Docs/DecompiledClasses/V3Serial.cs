using System;
using UnityEngine;

[Serializable]
public struct V3Serial
{
	public float x;

	public float y;

	public float z;

	public V3Serial(Vector3 v)
	{
		x = v.x;
		y = v.y;
		z = v.z;
	}

	public static implicit operator Vector3(V3Serial v3s)
	{
		return new Vector3(v3s.x, v3s.y, v3s.z);
	}

	public static implicit operator V3Serial(Vector3 v)
	{
		return new V3Serial(v);
	}

	public override string ToString()
	{
		return "(" + x + ", " + y + ", " + z + ")";
	}
}
