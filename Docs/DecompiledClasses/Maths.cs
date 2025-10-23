#define UNITY_EDITOR
using System;
using System.Runtime.InteropServices;
using UnityEngine;

[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct Maths
{
	public static int HashInt(int a)
	{
		a = a ^ 0x3D ^ (a >> 16);
		a += a << 3;
		a ^= a >> 4;
		a *= 668265261;
		a ^= a >> 15;
		return a;
	}

	public static float HashToFloat01(int input)
	{
		return (float)(HashInt(input) & 0xFF) / 255f;
	}

	public static Vector2 HashToVec2_01(int input)
	{
		int num = HashInt(input);
		return new Vector2((float)(num & 0xFF) / 255f, (float)((num & 0xFF00) >> 8) / 255f);
	}

	public static float RandomStdDev(float mean, float stdDev)
	{
		float value = UnityEngine.Random.value;
		float value2 = UnityEngine.Random.value;
		float num = Mathf.Sqrt(-2f * Mathf.Log(value)) * Mathf.Sin((float)Math.PI * 2f * value2);
		return mean + stdDev * num;
	}

	public static int CountBits(uint bitfield)
	{
		int num = 0;
		while (bitfield != 0)
		{
			bitfield &= bitfield - 1;
			num++;
		}
		return num;
	}

	public static float QuadEaseOut(float time01)
	{
		return PowerEaseOut(time01, 2);
	}

	public static float QuadEaseIn(float time01)
	{
		return PowerEaseIn(time01, 2);
	}

	public static float QuadEaseInOut(float time01)
	{
		return PowerEaseInOut(time01, 2);
	}

	public static float CubicEaseIn(float time01)
	{
		return PowerEaseIn(time01, 3);
	}

	public static float CubicEaseOut(float time01)
	{
		return PowerEaseOut(time01, 3);
	}

	public static float CubicEaseInOut(float time01)
	{
		return PowerEaseInOut(time01, 3);
	}

	public static float PowerEaseIn(float time01, int power)
	{
		return Mathf.Pow(time01, power);
	}

	public static float PowerEaseOut(float time01, int power)
	{
		return 1f - Mathf.Pow(1f - time01, power);
	}

	public static float PowerEaseInOut(float time01, int power)
	{
		if (time01 <= 0.5f)
		{
			return PowerEaseIn(time01 * 2f, power) / 2f;
		}
		float time2 = (time01 - 0.5f) * 2f;
		return 0.5f + PowerEaseOut(time2, power) / 2f;
	}

	public static float SinEaseIn(float time01)
	{
		float f = time01 * ((float)Math.PI / 2f);
		return 1f - Mathf.Cos(f);
	}

	public static float SinEaseOut(float time01)
	{
		return Mathf.Sin(time01 * ((float)Math.PI / 2f));
	}

	public static float SinEaseInOut(float time01)
	{
		float num = Mathf.Sin((float)Math.PI * time01 / 2f);
		return num * num;
	}

	public static bool CheckLineSectionOverlapXZ(Vector3 a0WithY, Vector3 a1WithY, Vector3 b0WithY, Vector3 b1WithY, out float intersectParam)
	{
		bool result = false;
		intersectParam = 0f;
		Vector3 vector = new Vector3(a0WithY.x, 0f, a0WithY.z);
		Vector3 vector2 = new Vector3(a1WithY.x, 0f, a1WithY.z);
		Vector3 vector3 = new Vector3(b0WithY.x, 0f, b0WithY.z);
		Vector3 vector4 = new Vector3(b1WithY.x, 0f, b1WithY.z);
		Vector3 vector5 = vector2 - vector;
		float magnitude = vector5.magnitude;
		Vector3 vector6 = ((magnitude > 0f) ? (vector5 / magnitude) : Vector3.forward);
		Vector3 rhs = vector6.Cross(Vector3.up);
		float num = Vector3.Dot(vector3 - vector, rhs);
		float f = Vector3.Dot(vector4 - vector, rhs);
		if (Mathf.Sign(num) != Mathf.Sign(f))
		{
			Vector3 vector7 = vector4 - vector3;
			float num2 = Vector3.Dot(vector7, rhs);
			float num3 = (0f - num) / num2;
			float num4 = Vector3.Dot(vector3 + vector7 * num3 - vector, vector6);
			if (num4 >= 0f && num4 <= magnitude)
			{
				result = true;
				intersectParam = num4 / magnitude;
			}
		}
		return result;
	}

	public static bool IntersectRayWithSphere(Vector3 rayStart, Vector3 rayDir, float rayDist, Vector3 sphereCentre, float sphereRadius, out float intersectDist)
	{
		bool result = false;
		Vector3 a = sphereCentre - rayStart;
		float num = a.Dot(rayDir);
		if (num >= 0f - sphereRadius && num <= rayDist + sphereRadius)
		{
			float num2 = num * num - a.sqrMagnitude + sphereRadius * sphereRadius;
			if (num2 >= 0f)
			{
				float num3 = Mathf.Sqrt(num2);
				intersectDist = num - num3;
				if (intersectDist < 0f)
				{
					intersectDist = num + num3;
				}
				result = intersectDist >= 0f && intersectDist <= rayDist;
			}
			else
			{
				intersectDist = 0f;
			}
		}
		else
		{
			intersectDist = 0f;
		}
		return result;
	}

	public static Vector3 VecToXZUnitVec(Vector3 inVec)
	{
		if (Mathf.Abs(inVec.x) > Mathf.Epsilon || Mathf.Abs(inVec.z) > Mathf.Epsilon)
		{
			return inVec.SetY(0f).normalized;
		}
		return Vector3.forward;
	}

	public static Vector3 CalcNormalFromTangent(Vector3 tangent, Vector3 up, Vector3 fallback)
	{
		Vector3 v = tangent.Cross(up);
		if (v.sqrMagnitude < 0.25f || v.IsNaN())
		{
			v = fallback;
		}
		return v;
	}

	public static float DistOfClosestPointOnLine(Vector3 startPos, Vector3 endPos, Vector3 pos)
	{
		Vector3 vector = endPos - startPos;
		return Vector3.Dot(pos - startPos, vector.normalized) / vector.magnitude;
	}

	public static Vector3 ClosestPointOnLine(Vector3 startPos, Vector3 endPos, Vector3 pos, out float distanceAlongLine)
	{
		Vector3 vector = endPos - startPos;
		Vector3 lhs = pos - startPos;
		Vector3 result;
		if (vector.magnitude > 0f)
		{
			distanceAlongLine = Vector3.Dot(lhs, vector.normalized);
			distanceAlongLine /= vector.magnitude;
			result = ((distanceAlongLine < 0f) ? startPos : ((!(distanceAlongLine > 1f)) ? (startPos + vector * distanceAlongLine) : endPos));
		}
		else
		{
			distanceAlongLine = 0f;
			result = startPos;
			d.LogError("Maths.ClosestPointOnLine - Line Length 0 passed in");
		}
		return result;
	}

	public static bool SphereSphereOverlap(Vector3 aPos, float aRadius, Vector3 bPos, float bRadius)
	{
		float sqrMagnitude = (aPos - bPos).sqrMagnitude;
		float num = aRadius + bRadius;
		return sqrMagnitude < num * num;
	}

	public static Quaternion LookRotationUpInvariant(Vector3 forward, Vector3 up)
	{
		Vector3 forward2 = up.Cross(forward).Cross(up);
		d.Assert(forward.sqrMagnitude != 0f && up.sqrMagnitude != 0f && forward2.sqrMagnitude != 0f);
		return Quaternion.LookRotation(forward2, up);
	}
}
