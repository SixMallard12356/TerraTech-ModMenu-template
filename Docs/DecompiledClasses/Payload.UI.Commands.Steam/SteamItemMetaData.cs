#define UNITY_EDITOR
namespace Payload.UI.Commands.Steam;

public struct SteamItemMetaData
{
	private const int k_TechVersion = 1;

	private const int k_SaveVersion = 1;

	private const int k_ModsVersion = 1;

	public int m_Version;

	public SteamItemCategory m_Category;

	public int m_SKUBuild;

	public string m_SKUDisplayVersion;

	public static SteamItemMetaData Create(SteamItemCategory category)
	{
		SteamItemMetaData result = new SteamItemMetaData
		{
			m_Category = category,
			m_Version = GetVersion(category)
		};
		SKU.ParseChangeListVersionNumberToInt(SKU.ChangelistVersion, out var versionInt);
		result.m_SKUBuild = versionInt;
		result.m_SKUDisplayVersion = SKU.DisplayVersion;
		return result;
	}

	private static int GetVersion(SteamItemCategory category)
	{
		switch (category)
		{
		case SteamItemCategory.Techs:
			return 1;
		case SteamItemCategory.Saves:
			return 1;
		case SteamItemCategory.Mods:
			return 1;
		default:
			d.LogError(string.Concat("SteamItemMetaData.GetVersion - no relationship for ", category, " found"));
			return -1;
		}
	}
}
