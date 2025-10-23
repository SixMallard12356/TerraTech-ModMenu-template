#define UNITY_EDITOR
using System;
using System.Collections.Generic;

public static class StringLookup
{
	private static Dictionary<int, int> m_BlockNames;

	private static Dictionary<int, int> m_SceneryNames;

	private static Dictionary<int, int> m_ChunkNames;

	private static Dictionary<int, int> m_BlockDescriptions;

	private static Dictionary<int, int> m_SceneryDescriptions;

	private static Dictionary<int, int> m_ChunkDescriptions;

	private static Dictionary<int, int> m_DamageTypeNames;

	private static Dictionary<int, int> m_DamageableTypeNames;

	private static Dictionary<int, int> m_BlockCategories;

	private static Dictionary<int, int> m_ChunkCategories;

	private static Dictionary<int, int> m_BlockAttributes;

	private static Dictionary<int, int> m_BlockControlAttributes;

	private static Dictionary<int, int> m_BlockControlCategories;

	private static Dictionary<int, int> m_BlockRarities;

	private static Dictionary<int, int> m_ChunkRarities;

	private static Dictionary<int, int> m_ComponentTiers;

	private static Dictionary<int, int> m_CorporationNames;

	private static Dictionary<int, int> m_LanguageLookup;

	private static Dictionary<int, int> m_GameTypeNames;

	private static bool m_Inited;

	public static string GetItemName(ItemTypeInfo item)
	{
		return GetItemName(item.ObjectType, item.ItemType);
	}

	public static string GetItemName(ObjectTypes objectType, int itemType)
	{
		LocalisationEnums.StringBanks stringBank = LocalisationEnums.StringBanks.BlockNames;
		Dictionary<int, int> dictionary = null;
		string defaultString = null;
		switch (objectType)
		{
		case ObjectTypes.Block:
			stringBank = LocalisationEnums.StringBanks.BlockNames;
			dictionary = m_BlockNames;
			defaultString = ((!Singleton.Manager<ManMods>.inst.IsModdedBlock((BlockTypes)itemType, includeUnknownOutOfRangeBlocks: true)) ? Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Warnings, 21) : Singleton.Manager<ManMods>.inst.FindBlockName(itemType));
			break;
		case ObjectTypes.Scenery:
			stringBank = LocalisationEnums.StringBanks.SceneryName;
			dictionary = m_SceneryNames;
			break;
		case ObjectTypes.Chunk:
			stringBank = LocalisationEnums.StringBanks.ChunkName;
			dictionary = m_ChunkNames;
			break;
		case ObjectTypes.Null:
		case ObjectTypes.Vehicle:
			d.Log("StringLookup.GetItemName does not support items of type: " + objectType);
			break;
		case ObjectTypes.Crate:
			return Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InGameMessages, 18);
		default:
			throw new ArgumentOutOfRangeException();
		}
		if (dictionary != null)
		{
			return GetString(itemType, dictionary, stringBank, defaultString);
		}
		return "ERROR: String Not Found";
	}

	public static string GetItemDescription(ItemTypeInfo item)
	{
		return GetItemDescription(item.ObjectType, item.ItemType);
	}

	public static string GetItemDescription(ObjectTypes objectType, int itemType)
	{
		LocalisationEnums.StringBanks stringBank = LocalisationEnums.StringBanks.BlockDescription;
		Dictionary<int, int> dictionary = null;
		string defaultString = null;
		switch (objectType)
		{
		case ObjectTypes.Block:
			stringBank = LocalisationEnums.StringBanks.BlockDescription;
			dictionary = m_BlockDescriptions;
			defaultString = ((!Singleton.Manager<ManMods>.inst.IsModdedBlock((BlockTypes)itemType)) ? Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Warnings, 21) : Singleton.Manager<ManMods>.inst.FindBlockDesc(itemType));
			break;
		case ObjectTypes.Scenery:
			stringBank = LocalisationEnums.StringBanks.SceneryDescription;
			dictionary = m_SceneryDescriptions;
			break;
		case ObjectTypes.Chunk:
			stringBank = LocalisationEnums.StringBanks.ChunkDescription;
			dictionary = m_ChunkDescriptions;
			break;
		case ObjectTypes.Null:
		case ObjectTypes.Vehicle:
			d.Log("StringLookup.GetItemDescription does not support items of type: " + objectType);
			break;
		case ObjectTypes.Crate:
			return Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InGameMessages, 19);
		default:
			throw new ArgumentOutOfRangeException();
		}
		if (dictionary != null)
		{
			return GetString(itemType, dictionary, stringBank, defaultString);
		}
		return "ERROR: String Not Found";
	}

	public static string GetCorporationName(FactionSubTypes corporation)
	{
		if (Singleton.Manager<ManMods>.inst.IsModdedCorp(corporation))
		{
			return Singleton.Manager<ManMods>.inst.FindCorpName(corporation);
		}
		return GetString((int)corporation, m_CorporationNames, LocalisationEnums.StringBanks.Corporations);
	}

	public static string GetDamageTypeName(ManDamage.DamageType damageType)
	{
		return GetString((int)damageType, m_DamageTypeNames, LocalisationEnums.StringBanks.DamageTypeNames);
	}

	public static string GetDamageableTypeName(ManDamage.DamageableType damageableType)
	{
		return GetString((int)damageableType, m_DamageableTypeNames, LocalisationEnums.StringBanks.DamageableTypeNames);
	}

	public static string GetLocalisedLanguageName(LocalisationEnums.Languages language)
	{
		return GetString((int)language, m_LanguageLookup, LocalisationEnums.StringBanks.MenuLanguageSelect);
	}

	public static string GetGameTypeName(ManGameMode.GameType gameType)
	{
		return GetString((int)gameType, m_GameTypeNames, LocalisationEnums.StringBanks.GameTypes);
	}

	public static string GetBlockCategoryName(BlockCategories blockCategory)
	{
		return GetString((int)blockCategory, m_BlockCategories, LocalisationEnums.StringBanks.BlockCategoryName);
	}

	public static string GetBlockRarityName(BlockRarity blockRarity)
	{
		return GetString((int)blockRarity, m_BlockRarities, LocalisationEnums.StringBanks.BlockRarityName);
	}

	public static string GetBlockControlCategoryName(ModuleControlCategory blockCat)
	{
		return GetString((int)blockCat, m_BlockControlCategories, LocalisationEnums.StringBanks.BlockControlCategoryNames);
	}

	public static string GetBlockAttribute(BlockAttributes blockAttribute)
	{
		return GetString((int)blockAttribute, m_BlockAttributes, LocalisationEnums.StringBanks.BlockAttributes);
	}

	public static string GetBlockControlAttribute(BlockControlAttributes blockAttribute)
	{
		return GetString((int)blockAttribute, m_BlockControlAttributes, LocalisationEnums.StringBanks.BlockControlAttributes);
	}

	public static string GetBlockTierName(BlockTypes blockType, bool usePrefixText = true)
	{
		string result = string.Empty;
		int blockTier = Singleton.Manager<ManLicenses>.inst.GetBlockTier(blockType);
		if (blockTier != int.MaxValue)
		{
			result = ((!usePrefixText) ? (blockTier + 1).ToString() : string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Purchasing, 9), blockTier + 1));
		}
		return result;
	}

	public static string GetBlockLimiterCostName(BlockTypes blockType)
	{
		if (Singleton.Manager<ManBlockLimiter>.inst.LimiterActive)
		{
			int blockCost = Singleton.Manager<ManBlockLimiter>.inst.GetBlockCost(blockType);
			return string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Purchasing, 10), blockCost);
		}
		return "";
	}

	public static string GetChunkCategoryName(ChunkCategory chunkCategory)
	{
		return GetString((int)chunkCategory, m_ChunkCategories, LocalisationEnums.StringBanks.ChunkCategoryName);
	}

	public static string GetChunkRarityName(ChunkRarity chunkRarity)
	{
		return GetString((int)chunkRarity, m_ChunkRarities, LocalisationEnums.StringBanks.ChunkRarityName);
	}

	public static string GetComponentTierName(ComponentTier componentTier)
	{
		return GetString((int)componentTier, m_ComponentTiers, LocalisationEnums.StringBanks.ComponentTierName);
	}

	public static void CreateLookUpTables()
	{
		CreateLookUp<BlockTypes, LocalisationEnums.BlockNames>(ref m_BlockNames);
		CreateLookUp<BlockTypes, LocalisationEnums.BlockDescription>(ref m_BlockDescriptions);
		CreateLookUp<ManDamage.DamageType, LocalisationEnums.DamageTypeNames>(ref m_DamageTypeNames);
		CreateLookUp<ManDamage.DamageableType, LocalisationEnums.DamageableTypeNames>(ref m_DamageableTypeNames);
		CreateLookUp<BlockCategories, LocalisationEnums.BlockCategoryName>(ref m_BlockCategories);
		CreateLookUp<BlockAttributes, LocalisationEnums.BlockAttributes>(ref m_BlockAttributes);
		CreateLookUp<BlockControlAttributes, LocalisationEnums.BlockControlAttributes>(ref m_BlockControlAttributes);
		CreateLookUp<BlockRarity, LocalisationEnums.BlockRarityName>(ref m_BlockRarities);
		CreateLookUp<ModuleControlCategory, LocalisationEnums.BlockControlCategoryNames>(ref m_BlockControlCategories);
		CreateLookUp<SceneryTypes, LocalisationEnums.SceneryName>(ref m_SceneryNames);
		CreateLookUp<SceneryTypes, LocalisationEnums.SceneryDescription>(ref m_SceneryDescriptions);
		CreateLookUp<ChunkTypes, LocalisationEnums.ChunkName>(ref m_ChunkNames);
		CreateLookUp<ChunkTypes, LocalisationEnums.ChunkDescription>(ref m_ChunkDescriptions);
		CreateLookUp<ChunkCategory, LocalisationEnums.ChunkCategoryName>(ref m_ChunkCategories, typeEnumIsFlags: true);
		CreateLookUp<ChunkRarity, LocalisationEnums.ChunkRarityName>(ref m_ChunkRarities);
		CreateLookUp<ComponentTier, LocalisationEnums.ComponentTierName>(ref m_ComponentTiers);
		CreateLookUp<FactionSubTypes, LocalisationEnums.Corporations>(ref m_CorporationNames);
		CreateLookUp<LocalisationEnums.Languages, LocalisationEnums.MenuLanguageSelect>(ref m_LanguageLookup);
		CreateLookUp<ManGameMode.GameType, LocalisationEnums.GameTypes>(ref m_GameTypeNames);
		m_Inited = true;
	}

	private static string GetString(int itemType, Dictionary<int, int> dictionary, LocalisationEnums.StringBanks stringBank, string defaultString = null)
	{
		if (!m_Inited)
		{
			CreateLookUpTables();
		}
		string text = null;
		if (dictionary.TryGetValue(itemType, out var value))
		{
			return Singleton.Manager<Localisation>.inst.GetLocalisedString(stringBank, value);
		}
		if (defaultString != null)
		{
			return defaultString;
		}
		return "ERROR: No String for Item Type: " + itemType + " in Bank: " + stringBank.ToString();
	}

	private static void CreateLookUp<TTypeEnum, TLocEnum>(ref Dictionary<int, int> enumToLocIDLookup, bool typeEnumIsFlags = false) where TTypeEnum : struct, IConvertible, IComparable, IFormattable where TLocEnum : struct, IConvertible, IComparable, IFormattable
	{
		string[] names = EnumNamesIterator<TTypeEnum>.Names;
		string[] names2 = EnumNamesIterator<TLocEnum>.Names;
		int num = names.Length;
		int num2 = names2.Length;
		int[] array = null;
		if (typeEnumIsFlags)
		{
			array = (int[])Enum.GetValues(typeof(TTypeEnum));
			d.Assert(array == null || num == array.Length, $"StringManager.CreateLookup from '{typeof(TTypeEnum).Name}' to 'LocalisationEnums.{typeof(TLocEnum).Name}' provided typeEnumNames (length {num}) and enumFlagValues (length {((array != null) ? array.Length : 0)}) that are mismatched, this will cause incorrect string lookups!");
		}
		enumToLocIDLookup = new Dictionary<int, int>(num);
		string text = null;
		int num3 = -1;
		for (int i = 0; i < num; i++)
		{
			bool flag = false;
			int key = (typeEnumIsFlags ? array[i] : i);
			for (int j = 0; j < num2; j++)
			{
				num3++;
				if (num3 >= num2)
				{
					num3 -= num2;
				}
				if (names[i] == names2[num3])
				{
					enumToLocIDLookup.Add(key, num3);
					flag = true;
					break;
				}
			}
		}
		if (text != null)
		{
			d.Log(text);
		}
	}
}
