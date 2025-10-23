using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct EnumIterator<TEnum> where TEnum : struct, IConvertible, IComparable, IFormattable
{
	public static int Count => EnumValuesIterator<TEnum>.Count;

	public static EnumValuesIterator<TEnum> Values()
	{
		return default(EnumValuesIterator<TEnum>);
	}
}
