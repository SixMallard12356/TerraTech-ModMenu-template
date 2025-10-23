#define UNITY_EDITOR
using System;
using UnityEngine;

[Serializable]
[ExecuteInEditMode]
public class Spline : MonoBehaviour
{
	[SerializeField]
	private Transform[] m_Points = new Transform[0];

	[SerializeField]
	private bool m_Loops;

	public int PointCount => m_Points.Length;

	public bool Loops => m_Loops;

	public Vector3 GetControlPointPos(int index)
	{
		if (index < PointCount)
		{
			return m_Points[index].transform.position;
		}
		d.Assert(condition: false, "Invalid control point index " + index);
		return Vector3.zero;
	}

	public Vector3 CalcCurvePositionSlow(int pointIndex, float paramToNext)
	{
		return CatmullRom.CalcCurvePositionSlow(GetControlPoints(), m_Loops, pointIndex, paramToNext);
	}

	public Vector3 CalcCurveTangentSlow(int pointIndex, float paramToNext)
	{
		return CatmullRom.CalcCurveTangentSlow(GetControlPoints(), m_Loops, pointIndex, paramToNext);
	}

	public Vector3[] GenerateCurve(int resolutionBetweenPoints)
	{
		return CatmullRom.GenerateCurve(GetControlPoints(), m_Loops, resolutionBetweenPoints);
	}

	public T GetControlPointComponent<T>(int pointInd)
	{
		bool num = pointInd >= 0 && pointInd < PointCount;
		d.Assert(num, "Spline:GetControlPointComponent index out of range");
		if (num)
		{
			return m_Points[pointInd].GetComponent<T>();
		}
		return default(T);
	}

	private Vector3[] GetControlPoints()
	{
		Vector3[] array = new Vector3[PointCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = m_Points[i].position;
		}
		return array;
	}
}
