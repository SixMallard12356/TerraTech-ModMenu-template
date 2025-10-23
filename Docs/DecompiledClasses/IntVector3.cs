#define UNITY_EDITOR
using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public struct IntVector3 : IEquatable<IntVector3>
{
	public int x;

	public int y;

	public int z;

	public static IntVector3 zero;

	public static IntVector3 one;

	public static IntVector3 forward;

	public static IntVector3 right;

	public static IntVector3 up;

	public static IntVector3 invalid;

	public int this[int i]
	{
		get
		{
			return i switch
			{
				0 => x, 
				1 => y, 
				2 => z, 
				_ => int.MinValue, 
			};
		}
		set
		{
			switch (i)
			{
			case 0:
				x = value;
				break;
			case 1:
				y = value;
				break;
			case 2:
				z = value;
				break;
			}
		}
	}

	[JsonIgnore]
	public int sqrMagnitude => x * x + y * y + z * z;

	[JsonIgnore]
	public float magnitude => Mathf.Sqrt(sqrMagnitude);

	public IntVector3(int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public IntVector3(Vector3 v)
	{
		x = Mathf.RoundToInt(v.x);
		y = Mathf.RoundToInt(v.y);
		z = Mathf.RoundToInt(v.z);
	}

	public static implicit operator Vector3(IntVector3 c)
	{
		return new Vector3(c.x, c.y, c.z);
	}

	public static implicit operator IntVector3(Vector3 v)
	{
		return new IntVector3(v);
	}

	public static implicit operator Vector3Int(IntVector3 c)
	{
		return new Vector3Int(c.x, c.y, c.z);
	}

	public static implicit operator IntVector3(Vector3Int v)
	{
		return new IntVector3(v.x, v.y, v.z);
	}

	public static bool operator ==(IntVector3 a, IntVector3 b)
	{
		if (a.x == b.x && a.y == b.y)
		{
			return a.z == b.z;
		}
		return false;
	}

	public static bool operator !=(IntVector3 a, IntVector3 b)
	{
		if (a.x == b.x && a.y == b.y)
		{
			return a.z != b.z;
		}
		return true;
	}

	public static IntVector3 operator +(IntVector3 c, IntVector3 d)
	{
		return new IntVector3
		{
			x = c.x + d.x,
			y = c.y + d.y,
			z = c.z + d.z
		};
	}

	public static IntVector3 operator -(IntVector3 c, IntVector3 d)
	{
		return new IntVector3
		{
			x = c.x - d.x,
			y = c.y - d.y,
			z = c.z - d.z
		};
	}

	public static IntVector3 operator -(IntVector3 a)
	{
		return new IntVector3
		{
			x = -a.x,
			y = -a.y,
			z = -a.z
		};
	}

	public static Vector3 operator +(IntVector3 iv, Vector3 fv)
	{
		return new Vector3
		{
			x = (float)iv.x + fv.x,
			y = (float)iv.y + fv.y,
			z = (float)iv.z + fv.z
		};
	}

	public static Vector3 operator +(Vector3 fv, IntVector3 iv)
	{
		return new Vector3
		{
			x = (float)iv.x + fv.x,
			y = (float)iv.y + fv.y,
			z = (float)iv.z + fv.z
		};
	}

	public static Vector3 operator -(IntVector3 iv, Vector3 fv)
	{
		return new Vector3
		{
			x = (float)iv.x - fv.x,
			y = (float)iv.y - fv.y,
			z = (float)iv.z - fv.z
		};
	}

	public static Vector3 operator -(Vector3 fv, IntVector3 iv)
	{
		return new Vector3
		{
			x = fv.x - (float)iv.x,
			y = fv.y - (float)iv.y,
			z = fv.z - (float)iv.z
		};
	}

	public static IntVector3 operator *(IntVector3 a, int b)
	{
		return new IntVector3
		{
			x = a.x * b,
			y = a.y * b,
			z = a.z * b
		};
	}

	public static IntVector3 operator /(IntVector3 a, int b)
	{
		return new IntVector3
		{
			x = a.x / b,
			y = a.y / b,
			z = a.z / b
		};
	}

	public static IntVector3 Max(IntVector3 a, IntVector3 b)
	{
		return new IntVector3
		{
			x = ((a.x > b.x) ? a.x : b.x),
			y = ((a.y > b.y) ? a.y : b.y),
			z = ((a.z > b.z) ? a.z : b.z)
		};
	}

	public static IntVector3 Min(IntVector3 a, IntVector3 b)
	{
		return new IntVector3
		{
			x = ((a.x < b.x) ? a.x : b.x),
			y = ((a.y < b.y) ? a.y : b.y),
			z = ((a.z < b.z) ? a.z : b.z)
		};
	}

	public static IntVector3 Clamp(IntVector3 v, IntVector3 lower, IntVector3 upper)
	{
		return new IntVector3
		{
			x = ((v.x < lower.x) ? lower.x : Mathf.Min(v.x, upper.x)),
			y = ((v.y < lower.y) ? lower.y : Mathf.Min(v.y, upper.y)),
			z = ((v.z < lower.z) ? lower.z : Mathf.Min(v.z, upper.z))
		};
	}

	public static IntVector3 Parse(string s)
	{
		if (s != null)
		{
			string[] array = s.Split(',');
			if (array.Length == 3)
			{
				return new IntVector3
				{
					x = int.Parse(array[0]),
					y = int.Parse(array[1]),
					z = int.Parse(array[2])
				};
			}
			throw new ArgumentException($"IntVector3.Parse - Input string not in expected format 'x,y,z' (got '{s}'");
		}
		return invalid;
	}

	public Vector3 APtoLocal()
	{
		return new Vector3((float)x * 0.5f, (float)y * 0.5f, (float)z * 0.5f);
	}

	public Vector3 APtoWorld(Transform t)
	{
		return t.TransformPoint(APtoLocal());
	}

	public bool APFaceX()
	{
		return (x & 1) == 1;
	}

	public bool APFaceY()
	{
		return (y & 1) == 1;
	}

	public bool APFaceZ()
	{
		return (z & 1) == 1;
	}

	public IntVector3 PadHalf()
	{
		return new IntVector3(x + (1 | (x >> 31)) * (x & 1) >> 1, y + (1 | (y >> 31)) * (y & 1) >> 1, z + (1 | (z >> 31)) * (z & 1) >> 1);
	}

	public IntVector3 PadHalfDown()
	{
		return new IntVector3(x - (1 | (x >> 31)) * (x & 1) >> 1, y - (1 | (y >> 31)) * (y & 1) >> 1, z - (1 | (z >> 31)) * (z & 1) >> 1);
	}

	public IntVector3 AxisUnit()
	{
		return new IntVector3((1 | (x >> 31)) * (x & 1), (1 | (y >> 31)) * (y & 1), (1 | (z >> 31)) * (z & 1));
	}

	public byte APHalfBits()
	{
		return (byte)(((x & 1) << (1 & (x >> 31)) << 4) | ((y & 1) << (1 & (y >> 31)) << 2) | ((z & 1) << (1 & (z >> 31))));
	}

	public override bool Equals(object obj)
	{
		if (!(obj is IntVector3))
		{
			return false;
		}
		return this == (IntVector3)obj;
	}

	public override int GetHashCode()
	{
		return ((x & 0x3FF) << 20) | ((y & 0x3FF) << 10) | (z & 0x3FF);
	}

	public bool Equals(IntVector3 other)
	{
		if (x == other.x && y == other.y)
		{
			return z == other.z;
		}
		return false;
	}

	public override string ToString()
	{
		if (this == invalid)
		{
			return "[invalid]";
		}
		return $"[{x}, {y}, {z}]";
	}

	public static IntVector3 ConvertFromString(string _string)
	{
		if (_string[0] != '[' || _string[_string.Length - 1] != ']')
		{
			throw new ArgumentException($"IntVector3.ConvertFromString - Input string not in expected format '[x, y, z]' (got '{_string}'");
		}
		if (_string.Equals("[invalid]"))
		{
			return invalid;
		}
		string text = _string.Trim('[', ' ', ']');
		text = text.Replace(" ", "");
		d.Assert(_string.Length == text.Length + 4, "IntVector3.ConvertFromString - Trimmed more characters from string than expected: \"" + _string + "\" became \"" + text + "\"");
		return Parse(text);
	}

	static IntVector3()
	{
		zero = new IntVector3
		{
			x = 0,
			y = 0,
			z = 0
		};
		one = new IntVector3
		{
			x = 1,
			y = 1,
			z = 1
		};
		forward = new IntVector3
		{
			x = 0,
			y = 0,
			z = 1
		};
		right = new IntVector3
		{
			x = 1,
			y = 0,
			z = 0
		};
		up = new IntVector3
		{
			x = 0,
			y = 1,
			z = 0
		};
		invalid = new IntVector3
		{
			x = int.MaxValue,
			y = int.MaxValue,
			z = int.MaxValue
		};
	}
}
