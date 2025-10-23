using System;
using UnityEngine;

[Serializable]
public class Axis
{
	public enum AxisType
	{
		X,
		Y,
		Z
	}

	public AxisType axis;

	private static Vector3[] axisVectors = new Vector3[3]
	{
		Vector3.right,
		Vector3.up,
		Vector3.forward
	};

	public Axis(AxisType a)
	{
		axis = a;
	}

	public static implicit operator Vector3(Axis ra)
	{
		return axisVectors[(int)ra.axis];
	}
}
