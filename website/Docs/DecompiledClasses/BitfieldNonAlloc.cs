using System;

public readonly struct BitfieldNonAlloc<TEnum> : IEquatable<BitfieldNonAlloc<TEnum>> where TEnum : struct, IConvertible, IComparable, IFormattable
{
	public static readonly BitfieldNonAlloc<TEnum> Null;

	private readonly int m_BitField;

	public int Field => m_BitField;

	public bool IsNull => m_BitField == 0;

	public bool AnySet => m_BitField != 0;

	public BitfieldNonAlloc(TEnum[] types)
	{
		m_BitField = 0;
		if (types != null)
		{
			foreach (TEnum val in types)
			{
				m_BitField |= 1 << Convert.ToInt32(val);
			}
		}
	}

	private BitfieldNonAlloc(int bitfieldValue)
	{
		m_BitField = bitfieldValue;
	}

	public bool Contains(int typeAsInt)
	{
		return (m_BitField & (1 << typeAsInt)) != 0;
	}

	public bool Contains(BitfieldNonAlloc<TEnum> other)
	{
		return (m_BitField & other.m_BitField) == other.m_BitField;
	}

	public BitfieldNonAlloc<TEnum> Add(int typeAsInt)
	{
		return Set(typeAsInt, enabled: true);
	}

	public BitfieldNonAlloc<TEnum> Remove(int typeAsInt)
	{
		return Set(typeAsInt, enabled: false);
	}

	public BitfieldNonAlloc<TEnum> Set(int typeAsInt, bool enabled)
	{
		int num = 0;
		num = ((!enabled) ? (m_BitField & ~(1 << typeAsInt)) : (m_BitField | (1 << typeAsInt)));
		return new BitfieldNonAlloc<TEnum>(num);
	}

	public static bool operator ==(BitfieldNonAlloc<TEnum> a, BitfieldNonAlloc<TEnum> b)
	{
		return a.m_BitField == b.m_BitField;
	}

	public static bool operator !=(BitfieldNonAlloc<TEnum> a, BitfieldNonAlloc<TEnum> b)
	{
		return !(a == b);
	}

	public bool Equals(BitfieldNonAlloc<TEnum> other)
	{
		return this == other;
	}

	public override bool Equals(object obj)
	{
		return Equals((BitfieldNonAlloc<TEnum>)obj);
	}

	public override int GetHashCode()
	{
		return m_BitField;
	}
}
