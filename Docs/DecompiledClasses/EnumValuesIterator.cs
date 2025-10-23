using System;
using System.Collections;
using System.Collections.Generic;

public struct EnumValuesIterator<TEnum> : IEnumerable<TEnum>, IEnumerable where TEnum : struct, IConvertible, IComparable, IFormattable
{
	public static readonly TEnum[] Values;

	private int m_Index;

	public TEnum Current => Values[m_Index];

	public static int Count => Values.Length;

	static EnumValuesIterator()
	{
		Values = (TEnum[])Enum.GetValues(typeof(TEnum));
	}

	public EnumValuesIterator<TEnum> GetEnumerator()
	{
		m_Index = -1;
		return this;
	}

	public bool MoveNext()
	{
		m_Index++;
		return m_Index < Values.Length;
	}

	public static int IndexOfFlag(TEnum flagEnum)
	{
		return Array.IndexOf(Values, flagEnum);
	}

	IEnumerator<TEnum> IEnumerable<TEnum>.GetEnumerator()
	{
		return ((IEnumerable<TEnum>)Values).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return Values.GetEnumerator();
	}
}
