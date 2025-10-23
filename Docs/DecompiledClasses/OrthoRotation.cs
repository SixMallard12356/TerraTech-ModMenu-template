#define UNITY_EDITOR
using UnityEngine;

public struct OrthoRotation
{
	public enum r
	{
		u000,
		u010,
		u020,
		u030,
		d002,
		d012,
		d022,
		d032,
		l001,
		l101,
		l201,
		l301,
		r003,
		r103,
		r203,
		r303,
		f100,
		f011,
		f320,
		f033,
		b300,
		b013,
		b120,
		b031,
		_max
	}

	public const int NumDistinctRotations = 24;

	public static readonly OrthoRotation identity;

	public static readonly OrthoRotation invalid;

	private static int[] componentTable;

	private static Quaternion[] quaternionCache;

	public static readonly r[] AllRotations;

	public r rot { get; private set; }

	private static float GetComponent(r rot, int shift)
	{
		return 90f * (float)((componentTable[(int)rot] & (3 << shift)) >> shift);
	}

	static OrthoRotation()
	{
		identity = default(OrthoRotation);
		invalid = new OrthoRotation(r._max);
		componentTable = new int[24]
		{
			0, 4, 8, 12, 2, 6, 10, 14, 1, 17,
			33, 49, 3, 19, 35, 51, 16, 5, 56, 15,
			48, 7, 24, 13
		};
		quaternionCache = new Quaternion[24];
		AllRotations = new r[24]
		{
			r.u000,
			r.u010,
			r.u020,
			r.u030,
			r.l001,
			r.l101,
			r.l201,
			r.l301,
			r.r003,
			r.r103,
			r.r203,
			r.r303,
			r.d002,
			r.d012,
			r.d022,
			r.d032,
			r.b300,
			r.b013,
			r.b120,
			r.b031,
			r.f100,
			r.f011,
			r.f320,
			r.f033
		};
		for (int i = 0; i < 24; i++)
		{
			quaternionCache[i] = Quaternion.Euler(new Vector3(GetComponent((r)i, 4), GetComponent((r)i, 2), GetComponent((r)i, 0)));
			d.Assert(quaternionCache[i] * new Vector3(1f, 2f, 3f) == new OrthoRotation((r)i) * new Vector3(1f, 2f, 3f));
			d.Assert((IntVector3)(quaternionCache[i] * new IntVector3(1, 2, 3)) == new OrthoRotation((r)i) * new IntVector3(1, 2, 3));
		}
	}

	private static r FindPacked(int packed)
	{
		int num = 0;
		int[] array = componentTable;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == packed)
			{
				return (r)num;
			}
			num++;
		}
		return r.u000;
	}

	public OrthoRotation(r rotation)
	{
		rot = rotation;
	}

	public OrthoRotation(int packed)
	{
		rot = FindPacked(packed);
	}

	public OrthoRotation(Quaternion rotation)
	{
		rot = r.u000;
		for (int i = 0; i < 24; i++)
		{
			if (quaternionCache[i] == rotation)
			{
				rot = (r)i;
				return;
			}
		}
		Vector3 other = rotation.eulerAngles;
		for (int j = 0; j < 24; j++)
		{
			if (quaternionCache[j].eulerAngles.Approximately(in other, 1f))
			{
				rot = (r)j;
				break;
			}
		}
	}

	public OrthoRotation(Vector3 eulers)
	{
		rot = new OrthoRotation(Quaternion.Euler(eulers)).rot;
	}

	public Vector3 ToEulers()
	{
		if (rot == r._max)
		{
			d.LogError("Invalid OrthoRotation");
			return Vector3.zero;
		}
		return new Vector3(GetComponent(rot, 4), GetComponent(rot, 2), GetComponent(rot, 0));
	}

	public static implicit operator Quaternion(OrthoRotation or)
	{
		return quaternionCache[(int)or.rot];
	}

	public static implicit operator int(OrthoRotation or)
	{
		d.AssertFormat(or.rot >= r.u000 && or.rot <= r.b031, "rot {0} out of range", (int)or.rot);
		return componentTable[(int)or.rot];
	}

	public static Vector3 operator *(OrthoRotation or, Vector3 v)
	{
		switch (or.rot)
		{
		case r.u000:
			return new Vector3(v.x, v.y, v.z);
		case r.u010:
			return new Vector3(v.z, v.y, 0f - v.x);
		case r.u020:
			return new Vector3(0f - v.x, v.y, 0f - v.z);
		case r.u030:
			return new Vector3(0f - v.z, v.y, v.x);
		case r.d002:
			return new Vector3(0f - v.x, 0f - v.y, v.z);
		case r.d012:
			return new Vector3(v.z, 0f - v.y, v.x);
		case r.d022:
			return new Vector3(v.x, 0f - v.y, 0f - v.z);
		case r.d032:
			return new Vector3(0f - v.z, 0f - v.y, 0f - v.x);
		case r.l001:
			return new Vector3(0f - v.y, v.x, v.z);
		case r.l101:
			return new Vector3(0f - v.y, 0f - v.z, v.x);
		case r.l201:
			return new Vector3(0f - v.y, 0f - v.x, 0f - v.z);
		case r.l301:
			return new Vector3(0f - v.y, v.z, 0f - v.x);
		case r.r003:
			return new Vector3(v.y, 0f - v.x, v.z);
		case r.r103:
			return new Vector3(v.y, 0f - v.z, 0f - v.x);
		case r.r203:
			return new Vector3(v.y, v.x, 0f - v.z);
		case r.r303:
			return new Vector3(v.y, v.z, v.x);
		case r.f100:
			return new Vector3(v.x, 0f - v.z, v.y);
		case r.f011:
			return new Vector3(v.z, v.x, v.y);
		case r.f320:
			return new Vector3(0f - v.x, v.z, v.y);
		case r.f033:
			return new Vector3(0f - v.z, 0f - v.x, v.y);
		case r.b300:
			return new Vector3(v.x, v.z, 0f - v.y);
		case r.b013:
			return new Vector3(v.z, 0f - v.x, 0f - v.y);
		case r.b120:
			return new Vector3(0f - v.x, 0f - v.z, 0f - v.y);
		case r.b031:
			return new Vector3(0f - v.z, v.x, 0f - v.y);
		default:
			d.LogErrorFormat("unknown rotation: {0}", or.rot);
			return Vector3.zero;
		}
	}

	public static IntVector3 operator *(OrthoRotation or, IntVector3 v)
	{
		switch (or.rot)
		{
		case r.u000:
			return new Vector3(v.x, v.y, v.z);
		case r.u010:
			return new Vector3(v.z, v.y, -v.x);
		case r.u020:
			return new Vector3(-v.x, v.y, -v.z);
		case r.u030:
			return new Vector3(-v.z, v.y, v.x);
		case r.d002:
			return new Vector3(-v.x, -v.y, v.z);
		case r.d012:
			return new Vector3(v.z, -v.y, v.x);
		case r.d022:
			return new Vector3(v.x, -v.y, -v.z);
		case r.d032:
			return new Vector3(-v.z, -v.y, -v.x);
		case r.l001:
			return new Vector3(-v.y, v.x, v.z);
		case r.l101:
			return new Vector3(-v.y, -v.z, v.x);
		case r.l201:
			return new Vector3(-v.y, -v.x, -v.z);
		case r.l301:
			return new Vector3(-v.y, v.z, -v.x);
		case r.r003:
			return new Vector3(v.y, -v.x, v.z);
		case r.r103:
			return new Vector3(v.y, -v.z, -v.x);
		case r.r203:
			return new Vector3(v.y, v.x, -v.z);
		case r.r303:
			return new Vector3(v.y, v.z, v.x);
		case r.f100:
			return new Vector3(v.x, -v.z, v.y);
		case r.f011:
			return new Vector3(v.z, v.x, v.y);
		case r.f320:
			return new Vector3(-v.x, v.z, v.y);
		case r.f033:
			return new Vector3(-v.z, -v.x, v.y);
		case r.b300:
			return new Vector3(v.x, v.z, -v.y);
		case r.b013:
			return new Vector3(v.z, -v.x, -v.y);
		case r.b120:
			return new Vector3(-v.x, -v.z, -v.y);
		case r.b031:
			return new Vector3(-v.z, v.x, -v.y);
		default:
			d.LogErrorFormat("unknown rotation: {0}", or.rot);
			return IntVector3.zero;
		}
	}

	public static Bounds operator *(OrthoRotation or, Bounds b)
	{
		Bounds result = default(Bounds);
		switch (or.rot)
		{
		case r.u000:
			result.center = new Vector3(b.center.x, b.center.y, b.center.z);
			result.extents = new Vector3(b.extents.x, b.extents.y, b.extents.z);
			break;
		case r.u010:
			result.center = new Vector3(b.center.z, b.center.y, 0f - b.center.x);
			result.extents = new Vector3(b.extents.z, b.extents.y, b.extents.x);
			break;
		case r.u020:
			result.center = new Vector3(0f - b.center.x, b.center.y, 0f - b.center.z);
			result.extents = new Vector3(b.extents.x, b.extents.y, b.extents.z);
			break;
		case r.u030:
			result.center = new Vector3(0f - b.center.z, b.center.y, b.center.x);
			result.extents = new Vector3(b.extents.z, b.extents.y, b.extents.x);
			break;
		case r.d002:
			result.center = new Vector3(0f - b.center.x, 0f - b.center.y, b.center.z);
			result.extents = new Vector3(b.extents.x, b.extents.y, b.extents.z);
			break;
		case r.d012:
			result.center = new Vector3(b.center.z, 0f - b.center.y, b.center.x);
			result.extents = new Vector3(b.extents.z, b.extents.y, b.extents.x);
			break;
		case r.d022:
			result.center = new Vector3(b.center.x, 0f - b.center.y, 0f - b.center.z);
			result.extents = new Vector3(b.extents.x, b.extents.y, b.extents.z);
			break;
		case r.d032:
			result.center = new Vector3(0f - b.center.z, 0f - b.center.y, 0f - b.center.x);
			result.extents = new Vector3(b.extents.z, b.extents.y, b.extents.x);
			break;
		case r.l001:
			result.center = new Vector3(0f - b.center.y, b.center.x, b.center.z);
			result.extents = new Vector3(b.extents.y, b.extents.x, b.extents.z);
			break;
		case r.l101:
			result.center = new Vector3(0f - b.center.y, 0f - b.center.z, b.center.x);
			result.extents = new Vector3(b.extents.y, b.extents.z, b.extents.x);
			break;
		case r.l201:
			result.center = new Vector3(0f - b.center.y, 0f - b.center.x, 0f - b.center.z);
			result.extents = new Vector3(b.extents.y, b.extents.x, b.extents.z);
			break;
		case r.l301:
			result.center = new Vector3(0f - b.center.y, b.center.z, 0f - b.center.x);
			result.extents = new Vector3(b.extents.y, b.extents.z, b.extents.x);
			break;
		case r.r003:
			result.center = new Vector3(b.center.y, 0f - b.center.x, b.center.z);
			result.extents = new Vector3(b.extents.y, b.extents.x, b.extents.z);
			break;
		case r.r103:
			result.center = new Vector3(b.center.y, 0f - b.center.z, 0f - b.center.x);
			result.extents = new Vector3(b.extents.y, b.extents.z, b.extents.x);
			break;
		case r.r203:
			result.center = new Vector3(b.center.y, b.center.x, 0f - b.center.z);
			result.extents = new Vector3(b.extents.y, b.extents.x, b.extents.z);
			break;
		case r.r303:
			result.center = new Vector3(b.center.y, b.center.z, b.center.x);
			result.extents = new Vector3(b.extents.y, b.extents.z, b.extents.x);
			break;
		case r.f100:
			result.center = new Vector3(b.center.x, 0f - b.center.z, b.center.y);
			result.extents = new Vector3(b.extents.x, b.extents.z, b.extents.y);
			break;
		case r.f011:
			result.center = new Vector3(b.center.z, b.center.x, b.center.y);
			result.extents = new Vector3(b.extents.z, b.extents.x, b.extents.y);
			break;
		case r.f320:
			result.center = new Vector3(0f - b.center.x, b.center.z, b.center.y);
			result.extents = new Vector3(b.extents.x, b.extents.z, b.extents.y);
			break;
		case r.f033:
			result.center = new Vector3(0f - b.center.z, 0f - b.center.x, b.center.y);
			result.extents = new Vector3(b.extents.z, b.extents.x, b.extents.y);
			break;
		case r.b300:
			result.center = new Vector3(b.center.x, b.center.z, 0f - b.center.y);
			result.extents = new Vector3(b.extents.x, b.extents.z, b.extents.y);
			break;
		case r.b013:
			result.center = new Vector3(b.center.z, 0f - b.center.x, 0f - b.center.y);
			result.extents = new Vector3(b.extents.z, b.extents.x, b.extents.y);
			break;
		case r.b120:
			result.center = new Vector3(0f - b.center.x, 0f - b.center.z, 0f - b.center.y);
			result.extents = new Vector3(b.extents.x, b.extents.z, b.extents.y);
			break;
		case r.b031:
			result.center = new Vector3(0f - b.center.z, b.center.x, 0f - b.center.y);
			result.extents = new Vector3(b.extents.z, b.extents.x, b.extents.y);
			break;
		default:
			result.center = Vector3.zero;
			result.extents = Vector3.zero;
			d.LogErrorFormat("unknown rotation: {0}", or.rot);
			break;
		}
		return result;
	}

	public static OrthoRotation operator ++(OrthoRotation or)
	{
		or.rot = ((or.rot != r.b031) ? (or.rot + 1) : r.u000);
		return or;
	}

	public static OrthoRotation operator --(OrthoRotation or)
	{
		or.rot = ((or.rot == r.u000) ? r.b031 : (or.rot - 1));
		return or;
	}

	public static bool operator ==(OrthoRotation a, OrthoRotation b)
	{
		return a.rot == b.rot;
	}

	public static bool operator !=(OrthoRotation a, OrthoRotation b)
	{
		return a.rot != b.rot;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is OrthoRotation))
		{
			return false;
		}
		return rot == ((OrthoRotation)obj).rot;
	}

	public override int GetHashCode()
	{
		return (int)rot;
	}

	public override string ToString()
	{
		if (rot == r._max)
		{
			return "Invalid";
		}
		return $"({rot.ToString()}: {(int)GetComponent(rot, 4)}, {(int)GetComponent(rot, 2)}, {(int)GetComponent(rot, 0)})";
	}
}
