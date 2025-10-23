#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public static class BlockSerialisation
{
	private static Dictionary<int, int> aliasHashDict = new Dictionary<int, int>();

	private static Dictionary<int, Type> aliasTypeDict = new Dictionary<int, Type>();

	private static Dictionary<int, Func<Module.SerialData, Module.SerialData>> aliasConverterDict = new Dictionary<int, Func<Module.SerialData, Module.SerialData>>();

	public static void RegisterAlias(Type orig, Type alias, Func<Module.SerialData, Module.SerialData> converter)
	{
		int? num = orig.BaseType.GetMethod("GetTypeHash").Invoke(null, new object[0]) as int?;
		int? num2 = alias.BaseType.GetMethod("GetTypeHash").Invoke(null, new object[0]) as int?;
		d.Assert(num.HasValue && num2.HasValue, "BlockSerialisation serialDataHash or aliasHash has no value");
		aliasHashDict[num.Value] = num2.Value;
		aliasConverterDict[num2.Value] = converter;
		aliasTypeDict[orig.DeclaringType.GetHashCode()] = alias.DeclaringType;
	}

	public static Type LookupAliasType(Type orig)
	{
		if (aliasTypeDict.TryGetValue(orig.GetHashCode(), out var value))
		{
			return value;
		}
		return null;
	}

	public static Module.SerialData LookForAlias(int serialDataHash, Dictionary<int, Module.SerialData> dict)
	{
		if (aliasHashDict.TryGetValue(serialDataHash, out var value))
		{
			Module.SerialData value2 = null;
			if (dict.TryGetValue(value, out value2))
			{
				Func<Module.SerialData, Module.SerialData> value3 = null;
				if (aliasConverterDict.TryGetValue(value, out value3))
				{
					return value3(value2);
				}
			}
		}
		return null;
	}
}
