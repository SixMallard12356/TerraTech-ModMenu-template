using System.Collections.Generic;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct FactionSubTypesComparer : IEqualityComparer<FactionSubTypes>
{
	public bool Equals(FactionSubTypes x, FactionSubTypes y)
	{
		return x == y;
	}

	public int GetHashCode(FactionSubTypes obj)
	{
		return (int)obj;
	}
}
