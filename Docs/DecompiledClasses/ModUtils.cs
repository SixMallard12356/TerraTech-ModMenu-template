public static class ModUtils
{
	public static string CreateCompoundId(string modId, string assetId)
	{
		return modId + ":" + assetId;
	}

	public static bool IsValidModId(string test)
	{
		if (test.Contains(":"))
		{
			return false;
		}
		return true;
	}

	public static bool IsValidCompoundId(string test)
	{
		if (test.Split(':').Length != 2)
		{
			return false;
		}
		return true;
	}

	public static string GetModFromCompoundId(string compoundId)
	{
		int length = compoundId.IndexOf(':');
		return compoundId.Substring(0, length);
	}

	public static string GetAssetFromCompoundId(string compoundId)
	{
		int num = compoundId.IndexOf(':');
		return compoundId.Substring(num + 1);
	}

	public static bool SplitCompoundId(string compoundId, out string modId, out string assetId)
	{
		int num = compoundId.IndexOf(':');
		modId = compoundId.Substring(0, num);
		assetId = compoundId.Substring(num + 1);
		return true;
	}
}
