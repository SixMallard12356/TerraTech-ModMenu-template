#define UNITY_EDITOR
using System;
using UnityEngine;

public class CatmullRom
{
	private static Vector3[] s_FallbackControlPoints = new Vector3[4];

	public static Vector3[] GenerateCurve(Vector3[] controlPoints, bool loops, int resolutionBetweenPoints)
	{
		controlPoints = EnsureAtLeast4ControlPoints(controlPoints);
		int num = controlPoints.Length;
		int num2 = (loops ? num : (num - 1)) * resolutionBetweenPoints + 1;
		Vector3[] array = new Vector3[num2];
		int num3 = 0;
		if (loops)
		{
			GenerateCurveSection(controlPoints[num - 1], controlPoints[0], controlPoints[1], controlPoints[2], resolutionBetweenPoints, array, num3);
		}
		else
		{
			GenerateCurveSection(controlPoints[0] * 2f - controlPoints[1], controlPoints[0], controlPoints[1], controlPoints[2], resolutionBetweenPoints, array, num3);
		}
		num3 += resolutionBetweenPoints;
		for (int i = 1; i <= num - 3; i++)
		{
			GenerateCurveSection(controlPoints[i - 1], controlPoints[i], controlPoints[i + 1], controlPoints[i + 2], resolutionBetweenPoints, array, num3);
			num3 += resolutionBetweenPoints;
		}
		if (loops)
		{
			GenerateCurveSection(controlPoints[num - 3], controlPoints[num - 2], controlPoints[num - 1], controlPoints[0], resolutionBetweenPoints, array, num3);
			num3 += resolutionBetweenPoints;
			GenerateCurveSection(controlPoints[num - 2], controlPoints[num - 1], controlPoints[0], controlPoints[1], resolutionBetweenPoints, array, num3);
			num3 += resolutionBetweenPoints;
		}
		else
		{
			GenerateCurveSection(controlPoints[num - 3], controlPoints[num - 2], controlPoints[num - 1], controlPoints[num - 1] * 2f - controlPoints[num - 2], resolutionBetweenPoints, array, num3);
			num3 += resolutionBetweenPoints;
		}
		array[num3] = (loops ? controlPoints[0] : controlPoints[num - 1]);
		num3++;
		d.Assert(num3 == num2, "Did not add as many control points as expected");
		return array;
	}

	public static Vector3 CalcCurvePositionSlow(Vector3[] controlPoints, bool loops, int pointIndex, float paramToNext)
	{
		controlPoints = EnsureAtLeast4ControlPoints(controlPoints);
		if (pointIndex >= 0 && pointIndex < controlPoints.Length && paramToNext >= 0f && paramToNext <= 1f && (loops || pointIndex < controlPoints.Length - 1 || paramToNext == 0f))
		{
			Vector3 vector = ((pointIndex > 0) ? controlPoints[pointIndex - 1] : ((!loops) ? (controlPoints[0] * 2f - controlPoints[1]) : controlPoints[controlPoints.Length - 1]));
			Vector3 vector2 = controlPoints[pointIndex];
			Vector3 vector3 = ((pointIndex + 1 < controlPoints.Length) ? controlPoints[pointIndex + 1] : ((!loops) ? (vector2 * 2f - vector) : controlPoints[0]));
			Vector3 p = ((pointIndex + 2 < controlPoints.Length) ? controlPoints[pointIndex + 2] : ((!loops) ? (vector3 * 2f - vector2) : controlPoints[(pointIndex + 2) % controlPoints.Length]));
			return CalcCatmullRomPos(vector, vector2, vector3, p, paramToNext);
		}
		d.LogWarning("Unable to get position on spline, as index was invalid (" + pointIndex + ", " + paramToNext + ")");
		return Vector3.zero;
	}

	public static Vector3 CalcCurveTangentSlow(Vector3[] controlPoints, bool loops, int pointIndex, float paramToNext)
	{
		int pointIndex2;
		float paramToNext2;
		float paramToNext3;
		if ((loops || pointIndex < controlPoints.Length - 1) && paramToNext < 0.999f)
		{
			pointIndex2 = pointIndex;
			paramToNext2 = paramToNext;
			paramToNext3 = paramToNext + 0.001f;
		}
		else if (paramToNext > 0.001f)
		{
			pointIndex2 = pointIndex;
			paramToNext2 = paramToNext - 0.001f;
			paramToNext3 = paramToNext;
		}
		else
		{
			pointIndex2 = pointIndex - 1;
			paramToNext2 = 0.999f;
			paramToNext3 = 1f;
		}
		Vector3 vector = CalcCurvePositionSlow(controlPoints, loops, pointIndex2, paramToNext2);
		Vector3 v = (CalcCurvePositionSlow(controlPoints, loops, pointIndex2, paramToNext3) - vector).normalized;
		if (v.IsZeroEpsilon())
		{
			return Vector3.forward;
		}
		return v;
	}

	private static void GenerateCurveSection(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, int resolution, Vector3[] outArray, int outPos)
	{
		if (outPos + resolution <= outArray.Length)
		{
			float num = 1f / Mathf.Max(resolution, 1f);
			float num2 = 0f;
			for (int i = 0; i < resolution; i++)
			{
				outArray[outPos + i] = CalcCatmullRomPos(p0, p1, p2, p3, num2);
				num2 += num;
			}
		}
		else
		{
			d.LogWarning("GenerateCurveSection ran out of array to write to");
		}
	}

	public static Vector3 CalcCatmullRomPos(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float rawParam)
	{
		float num = 0f;
		float num2 = CalcTVal(num, p0, p1);
		float num3 = CalcTVal(num2, p1, p2);
		float num4 = CalcTVal(num3, p2, p3);
		float num5 = rawParam * (num3 - num2) + num2;
		Vector3 vector = (num2 - num5) / (num2 - num) * p0 + (num5 - num) / (num2 - num) * p1;
		Vector3 vector2 = (num3 - num5) / (num3 - num2) * p1 + (num5 - num2) / (num3 - num2) * p2;
		Vector3 vector3 = (num4 - num5) / (num4 - num3) * p2 + (num5 - num3) / (num4 - num3) * p3;
		Vector3 vector4 = (num3 - num5) / (num3 - num) * vector + (num5 - num) / (num3 - num) * vector2;
		Vector3 vector5 = (num4 - num5) / (num4 - num2) * vector2 + (num5 - num2) / (num4 - num2) * vector3;
		return (num3 - num5) / (num3 - num2) * vector4 + (num5 - num2) / (num3 - num2) * vector5;
	}

	private static float CalcTVal(float prevT, Vector3 cA, Vector3 cB)
	{
		Vector3 vector = cB - cA;
		return Mathf.Pow(Mathf.Sqrt(Vector3.Dot(vector, vector)), 0.5f) + prevT;
	}

	private static Vector3[] EnsureAtLeast4ControlPoints(Vector3[] controlPoints)
	{
		int num = controlPoints.Length;
		if (num < s_FallbackControlPoints.Length)
		{
			Array.Copy(controlPoints, 0, s_FallbackControlPoints, 0, num);
			Vector3 vector = ((num > 0) ? controlPoints[controlPoints.Length - 1] : Vector3.zero);
			for (int i = num; i < s_FallbackControlPoints.Length; i++)
			{
				s_FallbackControlPoints[i] = vector;
			}
			return s_FallbackControlPoints;
		}
		return controlPoints;
	}
}
