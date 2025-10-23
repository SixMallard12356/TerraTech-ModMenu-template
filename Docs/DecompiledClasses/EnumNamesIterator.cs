using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct EnumNamesIterator<TEnum> where TEnum : struct, IConvertible, IComparable, IFormattable
{
	public static readonly string[] Names;

	static EnumNamesIterator()
	{
		Names = Enum.GetNames(typeof(TEnum));
	}
}
