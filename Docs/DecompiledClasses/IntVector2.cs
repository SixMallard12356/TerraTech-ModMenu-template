#define UNITY_EDITOR
using System;
using UnityEngine;

[Serializable]
public struct IntVector2 : IEquatable<IntVector2>
{
	public int x;

	public int y;

	public static IntVector2 zero;

	public static IntVector2 one;

	public static IntVector2 invalid;

	public int this[int i]
	{
		get
		{
			return i switch
			{
				0 => x, 
				1 => y, 
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
			}
		}
	}

	public IntVector2(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public IntVector2(Vector2 v)
	{
		x = Mathf.RoundToInt(v.x);
		y = Mathf.RoundToInt(v.y);
	}

	public static implicit operator Vector2(IntVector2 c)
	{
		return new Vector2(c.x, c.y);
	}

	public static implicit operator IntVector2(Vector2 v)
	{
		return new IntVector2(v);
	}

	public static implicit operator Vector2Int(IntVector2 c)
	{
		return new Vector2Int(c.x, c.y);
	}

	public static implicit operator IntVector2(Vector2Int v)
	{
		return new IntVector2(v.x, v.y);
	}

	public static bool operator ==(IntVector2 a, IntVector2 b)
	{
		if (a.x == b.x)
		{
			return a.y == b.y;
		}
		return false;
	}

	public static bool operator !=(IntVector2 a, IntVector2 b)
	{
		if (a.x == b.x)
		{
			return a.y != b.y;
		}
		return true;
	}

	public static IntVector2 operator +(IntVector2 c, IntVector2 d)
	{
		return new IntVector2
		{
			x = c.x + d.x,
			y = c.y + d.y
		};
	}

	public static IntVector2 operator -(IntVector2 c, IntVector2 d)
	{
		return new IntVector2
		{
			x = c.x - d.x,
			y = c.y - d.y
		};
	}

	public static IntVector2 operator -(IntVector2 a)
	{
		return new IntVector2
		{
			x = -a.x,
			y = -a.y
		};
	}

	public static IntVector2 operator *(IntVector2 a, int b)
	{
		return new IntVector2
		{
			x = a.x * b,
			y = a.y * b
		};
	}

	public static IntVector2 operator /(IntVector2 a, int b)
	{
		return new IntVector2
		{
			x = a.x / b,
			y = a.y / b
		};
	}

	public static IntVector2 Max(IntVector2 a, IntVector2 b)
	{
		return new IntVector2
		{
			x = ((a.x > b.x) ? a.x : b.x),
			y = ((a.y > b.y) ? a.y : b.y)
		};
	}

	public static IntVector2 Min(IntVector2 a, IntVector2 b)
	{
		return new IntVector2
		{
			x = ((a.x < b.x) ? a.x : b.x),
			y = ((a.y < b.y) ? a.y : b.y)
		};
	}

	public static IntVector2 Abs(IntVector2 a)
	{
		return new IntVector2
		{
			x = Mathf.Abs(a.x),
			y = Mathf.Abs(a.y)
		};
	}

	public static IntVector2 Parse(string s)
	{
		if (s != null)
		{
			string[] array = s.Split(',');
			if (array.Length == 2)
			{
				return new IntVector2
				{
					x = int.Parse(array[0]),
					y = int.Parse(array[1])
				};
			}
			throw new ArgumentException($"IntVector2.Parse - Input string not in expected format 'x,y' (got '{s}'");
		}
		return invalid;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is IntVector2))
		{
			return false;
		}
		return this == (IntVector2)obj;
	}

	public override int GetHashCode()
	{
		return (x << 16) | y;
	}

	public bool Equals(IntVector2 other)
	{
		if (x == other.x)
		{
			return y == other.y;
		}
		return false;
	}

	public override string ToString()
	{
		if (this == invalid)
		{
			return "[invalid]";
		}
		return $"[{x}, {y}]";
	}

	public static IntVector2 ConvertFromString(string _string)
	{
		if (_string[0] != '[' || _string[_string.Length - 1] != ']')
		{
			throw new ArgumentException($"IntVector2.ConvertFromString - Input string not in expected format '[x, y]' (got '{_string}'");
		}
		if (_string.Equals("[invalid]"))
		{
			return invalid;
		}
		string text = _string.Trim('[', ' ', ']');
		text = text.Replace(" ", "");
		d.Assert(_string.Length == text.Length + 3, "IntVector2.ConvertFromString - Trimmed more characters from string than expected: \"" + _string + "\" became \"" + text + "\"");
		return Parse(text);
	}

	static IntVector2()
	{
		zero = new IntVector2
		{
			x = 0,
			y = 0
		};
		one = new IntVector2
		{
			x = 1,
			y = 1
		};
		invalid = new IntVector2
		{
			x = int.MaxValue,
			y = int.MaxValue
		};
	}
}
