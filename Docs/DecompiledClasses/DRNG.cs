#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class DRNG
{
	private struct State
	{
		public uint x;

		public uint y;

		public uint z;

		public uint w;

		public State Next()
		{
			uint num = x ^ (x << 11);
			return new State
			{
				x = y,
				y = z,
				z = w,
				w = (w ^ (w >> 19) ^ (num ^ (num >> 8)))
			};
		}
	}

	private const uint Y = 842502087u;

	private const uint Z = 3579807591u;

	private const uint W = 273326509u;

	private const double k_DoubleUnitIntExcl = 4.656612873077393E-10;

	private const double k_DoubleUnitIntIncl = 4.656612875245797E-10;

	private const double k_DoubleUnitUIntExcl = 2.3283064365386963E-10;

	private const double k_DoubleUnitUIntIncl = 2.3283064370807974E-10;

	private const float k_FloatUnitIntExcl = 4.656613E-10f;

	private const float k_FloatUnitIntIncl = 4.656613E-10f;

	private static readonly uint[] k_SeedTable;

	private State m_State;

	private Stack<State> m_StateStack;

	static DRNG()
	{
		k_SeedTable = new uint[256];
		State state = new State
		{
			x = 2673365688u,
			y = 842502087u,
			z = 3579807591u,
			w = 273326509u
		};
		for (int i = 0; i < k_SeedTable.Length; i++)
		{
			state = state.Next();
			k_SeedTable[i] = state.w;
		}
	}

	public DRNG()
	{
		SetSeed((uint)Environment.TickCount);
	}

	public DRNG(uint seed)
	{
		SetSeed(seed);
	}

	public void SetSeed(uint seed)
	{
		uint num = HashCodeUtility.QuickHash(seed);
		uint num2 = HashCodeUtility.QuickHash(num);
		uint num3 = HashCodeUtility.QuickHash(num2);
		uint w = HashCodeUtility.QuickHash(num3);
		m_State = new State
		{
			x = num,
			y = num2,
			z = num3,
			w = w
		};
		d.Assert(condition: true, "invalid assumption int.MaxValue == 0x7FFFFFFF");
	}

	public void SetSeed(uint seed1, uint seed2)
	{
		uint num = HashCodeUtility.QuickHash(seed1);
		uint num2 = HashCodeUtility.QuickHash(seed2);
		uint z = HashCodeUtility.QuickHash(num);
		uint w = HashCodeUtility.QuickHash(num2);
		m_State = new State
		{
			x = num,
			y = num2,
			z = z,
			w = w
		};
		d.Assert(condition: true, "invalid assumption int.MaxValue == 0x7FFFFFFF");
	}

	public void SetSeed(uint seed1, uint seed2, uint seed3)
	{
		uint num = HashCodeUtility.QuickHash(seed1);
		uint num2 = HashCodeUtility.QuickHash(seed2);
		uint num3 = HashCodeUtility.QuickHash(seed3);
		uint w = HashCodeUtility.QuickHash(num ^ num2 ^ num3);
		m_State = new State
		{
			x = num,
			y = num2,
			z = num3,
			w = w
		};
		d.Assert(condition: true, "invalid assumption int.MaxValue == 0x7FFFFFFF");
	}

	public void SetSeed(uint seed1, uint seed2, uint seed3, uint seed4)
	{
		uint x = HashCodeUtility.QuickHash(seed1);
		uint y = HashCodeUtility.QuickHash(seed2);
		uint z = HashCodeUtility.QuickHash(seed3);
		uint w = HashCodeUtility.QuickHash(seed4);
		m_State = new State
		{
			x = x,
			y = y,
			z = z,
			w = w
		};
		d.Assert(condition: true, "invalid assumption int.MaxValue == 0x7FFFFFFF");
	}

	public void ForceSeed(uint seed1, uint seed2, uint seed3, uint seed4)
	{
		m_State = new State
		{
			x = seed1,
			y = seed2,
			z = seed3,
			w = seed4
		};
		d.Assert(condition: true, "invalid assumption int.MaxValue == 0x7FFFFFFF");
	}

	public void SetSeedFromTable(uint seed1)
	{
		uint num = k_SeedTable[seed1 & 0xFF] | seed1;
		uint num2 = k_SeedTable[num & 0xFF];
		m_State = new State
		{
			x = num2,
			y = k_SeedTable[num2 & 0xFF],
			z = k_SeedTable[num & 0xFF],
			w = num
		};
		d.Assert(condition: true, "invalid assumption int.MaxValue == 0x7FFFFFFF");
	}

	public void SetSeedFromTable(uint seed1, uint seed2)
	{
		uint num = k_SeedTable[seed1 & 0xFF] | seed1;
		uint num2 = k_SeedTable[seed2 & 0xFF] | seed2;
		m_State = new State
		{
			x = num2,
			y = k_SeedTable[num2 & 0xFF],
			z = k_SeedTable[num & 0xFF],
			w = num
		};
		d.Assert(condition: true, "invalid assumption int.MaxValue == 0x7FFFFFFF");
	}

	public void SetSeedFromTable(uint seed1, uint seed2, uint seed3)
	{
		uint num = k_SeedTable[seed1 & 0xFF] | seed1;
		uint x = k_SeedTable[seed2 & 0xFF] | seed2;
		uint y = k_SeedTable[seed3 & 0xFF] | seed3;
		m_State = new State
		{
			x = x,
			y = y,
			z = k_SeedTable[num & 0xFF],
			w = num
		};
		d.Assert(condition: true, "invalid assumption int.MaxValue == 0x7FFFFFFF");
	}

	public void SetSeedFromTable(uint seed1, uint seed2, uint seed3, uint seed4)
	{
		m_State = new State
		{
			x = (k_SeedTable[seed1 & 0xFF] | seed1),
			y = (k_SeedTable[seed2 & 0xFF] | seed2),
			z = (k_SeedTable[seed3 & 0xFF] | seed3),
			w = (k_SeedTable[seed4 & 0xFF] | seed4)
		};
		d.Assert(condition: true, "invalid assumption int.MaxValue == 0x7FFFFFFF");
	}

	public void PushSeed(uint seed)
	{
		if (m_StateStack == null)
		{
			m_StateStack = new Stack<State>();
		}
		m_StateStack.Push(m_State);
		SetSeed(seed);
	}

	public void PopSeed()
	{
		if (m_StateStack == null || m_StateStack.Count == 0)
		{
			d.LogError("state stack empty");
		}
		else
		{
			m_State = m_StateStack.Pop();
		}
	}

	public float One()
	{
		m_State = m_State.Next();
		return 4.656613E-10f * (float)(int)(0x7FFFFFFF & m_State.w);
	}

	public float OnePosNeg()
	{
		m_State = m_State.Next();
		return 4.656613E-10f * (float)(int)m_State.w;
	}

	public float OneInclusive()
	{
		m_State = m_State.Next();
		return 4.656613E-10f * (float)(int)(0x7FFFFFFF & m_State.w);
	}

	public float OnePosNegInclusive()
	{
		m_State = m_State.Next();
		return 4.656613E-10f * (float)(int)m_State.w;
	}

	public float FloatPos()
	{
		m_State = m_State.Next();
		return 4.656613E-10f * (float)(int)(0x7FFFFFFF & m_State.w) * float.MaxValue;
	}

	public float FloatPosNeg()
	{
		m_State = m_State.Next();
		return 4.656613E-10f * (float)(int)m_State.w * float.MaxValue;
	}

	public float Range(float min, float max)
	{
		if (min > max)
		{
			throw new ArgumentOutOfRangeException("max", max, "max must be >= min");
		}
		m_State = m_State.Next();
		float num = max - min;
		if (num < 0f)
		{
			return (float)((double)min + 2.3283064365386963E-10 * (double)m_State.w * ((double)max - (double)min));
		}
		return min + 4.656613E-10f * (float)(int)(0x7FFFFFFF & m_State.w) * num;
	}

	public float RangeInclusive(float min, float max)
	{
		if (min > max)
		{
			throw new ArgumentOutOfRangeException("max", max, "max must be >= min");
		}
		m_State = m_State.Next();
		float num = max - min;
		if (num < 0f)
		{
			return (float)((double)min + 2.3283064370807974E-10 * (double)m_State.w * ((double)max - (double)min));
		}
		return min + 4.656613E-10f * (float)(int)(0x7FFFFFFF & m_State.w) * num;
	}

	public int IntPos()
	{
		m_State = m_State.Next();
		return (int)(0x7FFFFFFF & m_State.w);
	}

	public int IntPosNeg()
	{
		m_State = m_State.Next();
		return (int)m_State.w;
	}

	public int Range(int min, int max)
	{
		if (min > max)
		{
			throw new ArgumentOutOfRangeException("max", max, "max must be >= min");
		}
		m_State = m_State.Next();
		int num = max - min;
		if (num < 0)
		{
			return min + (int)(2.3283064365386963E-10 * (double)m_State.w * (double)((long)max - (long)min));
		}
		return min + (int)(4.656612873077393E-10 * (double)(int)(0x7FFFFFFF & m_State.w) * (double)num);
	}

	public int RangeInclusive(int min, int max)
	{
		if (min > max)
		{
			throw new ArgumentOutOfRangeException("max", max, "max must be >= min");
		}
		m_State = m_State.Next();
		int num = max - min;
		if (num < 0)
		{
			return min + (int)(2.3283064370807974E-10 * (double)m_State.w * (double)((long)max - (long)min));
		}
		return min + (int)(4.656612875245797E-10 * (double)(int)(0x7FFFFFFF & m_State.w) * (double)num);
	}

	public uint UInt()
	{
		m_State = m_State.Next();
		return m_State.w;
	}

	public bool Bool()
	{
		m_State = m_State.Next();
		return (m_State.w & 1) != 0;
	}

	public Vector2 OnUnitCircle()
	{
		throw new NotImplementedException();
	}

	public Vector3 OnUnitSphere()
	{
		throw new NotImplementedException();
	}
}
