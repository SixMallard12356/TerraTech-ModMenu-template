using System;
using UnityEngine;

[Serializable]
public struct QuatSerial
{
	public float w;

	public float x;

	public float y;

	public float z;

	public QuatSerial(Quaternion q)
	{
		w = q.w;
		x = q.x;
		y = q.y;
		z = q.z;
	}

	public static implicit operator Quaternion(QuatSerial qs)
	{
		return new Quaternion(qs.x, qs.y, qs.z, qs.w);
	}

	public static implicit operator QuatSerial(Quaternion q)
	{
		return new QuatSerial(q);
	}
}
