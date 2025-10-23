#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Rewired;
using TerraTech.Network;
using UnityEngine;

public class Localisation : Singleton.Manager<Localisation>
{
	private class LanguageCulturePair
	{
		public LocalisationEnums.Languages language;

		public string cultureName;
	}

	[Serializable]
	public class GlyphInfo
	{
		[RewiredAction]
		public int m_RewiredAction = -1;

		public JoystickGlyphIndex.GlyphType m_GlyphType;

		public GlyphInfo(int rewiredAction = -1, JoystickGlyphIndex.GlyphType glyphType = JoystickGlyphIndex.GlyphType.Default)
		{
			m_RewiredAction = rewiredAction;
			m_GlyphType = glyphType;
		}
	}

	public class StringBank
	{
		public string BankName;

		public string[] Strings;

		public StringBank(string bankName)
		{
			BankName = bankName;
		}
	}

	public static string BANK_DELIMITER = "$";

	public const string LOCALISATION_PREFIX = "LOC_";

	[SerializeField]
	private LocalisationEnums.Languages m_DefaultLanguage;

	[SerializeField]
	private List<LocalisationEnums.Languages> m_Languages;

	[SerializeField]
	private string m_NetEaseTechNamesResourceName;

	public const string FOLDER_PATH = "Localisation/";

	private const string TAG_JOYSTICK_GLYPH = "<sprite=\"{0}\" index={1}>";

	private const string TAG_KEYBOARD_KEY = "<color=#77C0FFFF>[{0}]</color>";

	private Regex m_RegexFindGlyphPlaceholder = new Regex("{key:\\d+}");

	private static LanguageCulturePair[] s_LanguageToCultureNameLookup = new LanguageCulturePair[24]
	{
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.English,
			cultureName = "en-GB"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.US_English,
			cultureName = "en-US"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.German,
			cultureName = "de-DE"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Russian,
			cultureName = "ru-RU"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.French,
			cultureName = "fr-FR"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Polish,
			cultureName = "pl-PL"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Dutch,
			cultureName = "nl-NL"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Portuguese,
			cultureName = "pt-PT"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Brazilian_Portuguese,
			cultureName = "pt-BR"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Italian,
			cultureName = "it-IT"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Swedish,
			cultureName = "sv-SE"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Danish,
			cultureName = "da-DK"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Spanish,
			cultureName = "es-ES"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Traditional_Chinese,
			cultureName = "zh-HK"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Simplified_Chinese,
			cultureName = "zh-CN"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Simplified_Chinese_NE,
			cultureName = "zh-CN"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Japanese,
			cultureName = "ja-JP"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Japanese_Teyon,
			cultureName = "ja-JP"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Korean,
			cultureName = "ko-KR"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Thai,
			cultureName = "th-TH"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Czech,
			cultureName = "cs-CZ"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Turkish,
			cultureName = "tr-TR"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Hungarian,
			cultureName = "hu-HU"
		},
		new LanguageCulturePair
		{
			language = LocalisationEnums.Languages.Ukrainian,
			cultureName = "uk-UA"
		}
	};

	private CultureInfo m_CachedUserSelectedCultureInfo;

	private NumberFormatInfo m_CachedNumberFormatInfo;

	private static List<string> m_FallbackNetEaseNames;

	private List<LocalisationEnums.Languages> m_PlatformSupportedLanguages;

	private const int kMaxInventoryCountDisplayQuantity = 999;

	public EventNoParams OnLanguageChanged;

	private List<StringBank> m_AllBanks = new List<StringBank>();

	private Dictionary<int, string> m_HashLookup = new Dictionary<int, string>();

	public LocalisationEnums.Languages CurrentLanguage { get; private set; }

	public string GetMoneyStringWithSymbol(int money)
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Purchasing, 2);
		string moneyString = GetMoneyString(money);
		return StringFormat(localisedString, moneyString);
	}

	public string GetMoneyStringWhenSelling(int money)
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InfoOverlays, 8);
		return StringFormat(localisedString, GetMoneyStringWithSymbol(money));
	}

	public string GetMoneyString(int money)
	{
		CacheCultureInfoIfNeeded();
		return money.ToString("N", m_CachedNumberFormatInfo);
	}

	public string GetDateString(DateTime dateTime, bool shortDate = true)
	{
		CacheCultureInfoIfNeeded();
		DateTimeFormatInfo dateTimeFormat = m_CachedUserSelectedCultureInfo.DateTimeFormat;
		string text = (shortDate ? dateTimeFormat.ShortDatePattern : dateTimeFormat.LongDatePattern);
		return dateTime.ToString(text);
	}

	public string GetTimeDisplayString(float totalSeconds, bool forceHourDisplay = false, bool displayMilliseconds = false)
	{
		string text = string.Empty;
		TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);
		if (forceHourDisplay || timeSpan.Hours > 1)
		{
			text = StringFormat("{0:#0}:", timeSpan.Hours);
		}
		text += StringFormat("{0:#0}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
		if (displayMilliseconds)
		{
			text += StringFormat(":{0:00}", timeSpan.Milliseconds);
		}
		return text;
	}

	public string GetInventoryQuantityString(int inventoryQuantity)
	{
		if (inventoryQuantity == -1)
		{
			return "∞";
		}
		if (inventoryQuantity < 999)
		{
			return inventoryQuantity.ToString();
		}
		return 999 + "+";
	}

	public string GetLobbyVisibilityString(Lobby.LobbyVisibility visibility)
	{
		return Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, (int)(122 + visibility));
	}

	public string GetLocalisedString(LocalisationEnums.StringBanks bankName, int stringID, params GlyphInfo[] rewiredActions)
	{
		if ((int)bankName < m_AllBanks.Count && stringID < m_AllBanks[(int)bankName].Strings.Length)
		{
			string text = m_AllBanks[(int)bankName].Strings[stringID];
			if (rewiredActions != null && rewiredActions.Length != 0)
			{
				text = ReplaceGlyphPlaceHolders(text, rewiredActions);
			}
			return text;
		}
		return "";
	}

	public string GetLocalisedString(string bank, string id, params GlyphInfo[] rewiredActions)
	{
		if (!TryGetLocalisedString(bank, id, out var text, rewiredActions))
		{
			d.LogWarning("Could not find string \"" + bank + "\", \"" + id + "\"");
		}
		return text;
	}

	private bool TryGetLocalisedString(string bank, string id, out string text, params GlyphInfo[] rewiredActions)
	{
		int key = GenerateLocHash(bank, id);
		if (m_HashLookup.TryGetValue(key, out text))
		{
			if (rewiredActions != null && rewiredActions.Length != 0)
			{
				text = ReplaceGlyphPlaceHolders(text, rewiredActions);
			}
			return true;
		}
		text = "";
		return false;
	}

	public string ReplaceGlyphPlaceHolders(string textWithPlaceholder, GlyphInfo rewiredActionGlyph)
	{
		textWithPlaceholder = m_RegexFindGlyphPlaceholder.Replace(textWithPlaceholder, FormatGlyphPlaceholder);
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			return StringFormat(textWithPlaceholder, FormatGlyphSpriteTag(rewiredActionGlyph.m_RewiredAction, rewiredActionGlyph.m_GlyphType));
		}
		return StringFormat(textWithPlaceholder, FormatKeyNameTag(rewiredActionGlyph.m_RewiredAction));
	}

	public string ActionElementMapToString(ActionElementMap aem)
	{
		if (aem != null)
		{
			if (aem.controllerMap != null)
			{
				return ActionElementMapToString(aem, aem.controllerMap.controllerType);
			}
			return ActionElementMapToString(aem, ControllerType.Joystick);
		}
		return string.Empty;
	}

	public string ActionElementMapToString(ActionElementMap aem, ControllerType controllerType)
	{
		string text = string.Empty;
		if (aem != null)
		{
			switch (controllerType)
			{
			case ControllerType.Keyboard:
				if (aem.keyboardKeyCode != KeyboardKeyCode.None && !TryGetLocalisedString("UnityInputKeyCode", aem.keyboardKeyCode.ToString(), out text))
				{
					text = aem.elementIdentifierName;
				}
				break;
			case ControllerType.Mouse:
				if (aem.elementType == ControllerElementType.Button)
				{
					text = ((aem.elementIndex != 0) ? ((aem.elementIndex != 1) ? ((aem.elementIndex != 2) ? ("M" + (aem.elementIndex + 1)) : "MMB") : GetLocalisedString(LocalisationEnums.StringBanks.UnityInputKeyCode, 134)) : GetLocalisedString(LocalisationEnums.StringBanks.UnityInputKeyCode, 133));
				}
				break;
			case ControllerType.Joystick:
			{
				string tMPSpriteAssetName = Singleton.Manager<ManInput>.inst.GetTMPSpriteAssetName();
				int spriteID = Singleton.Manager<ManInput>.inst.FindGlyphIDForJoystickElementID(aem.elementIdentifierId, (aem.axisType == AxisType.Normal) ? JoystickGlyphIndex.GlyphType.AxisGroup : JoystickGlyphIndex.GlyphType.Default);
				text = FormatGlyphSpriteIDTag(tMPSpriteAssetName, spriteID);
				break;
			}
			}
		}
		return text;
	}

	public string ElementIdentifierIdToString(ControllerType controllerType, Guid hardwareTypeGuid, int elementIdentifierId, string elementIdentifierName, ControllerElementType elementType = ControllerElementType.Button, int elementIndex = 0, KeyCode keyCode = KeyCode.None, AxisType axisType = AxisType.None)
	{
		string text = string.Empty;
		switch (controllerType)
		{
		case ControllerType.Keyboard:
			if (keyCode != KeyCode.None && !TryGetLocalisedString("UnityInputKeyCode", keyCode.ToString(), out text))
			{
				text = elementIdentifierName;
			}
			break;
		case ControllerType.Mouse:
			if (elementType == ControllerElementType.Button)
			{
				text = elementIndex switch
				{
					0 => GetLocalisedString(LocalisationEnums.StringBanks.UnityInputKeyCode, 133), 
					1 => GetLocalisedString(LocalisationEnums.StringBanks.UnityInputKeyCode, 134), 
					2 => "MMB", 
					_ => "M" + (elementIndex + 1), 
				};
			}
			break;
		case ControllerType.Joystick:
		{
			string tMPSpriteAssetName = Singleton.Manager<ManInput>.inst.GetTMPSpriteAssetName(controllerType, hardwareTypeGuid);
			JoystickGlyphIndex.GlyphType glyphType = ((axisType != AxisType.None) ? JoystickGlyphIndex.GlyphType.AxisGroup : JoystickGlyphIndex.GlyphType.Default);
			int spriteID = Singleton.Manager<ManInput>.inst.FindGlyphIDForJoystickElementID(controllerType, hardwareTypeGuid, elementIdentifierId, glyphType);
			text = FormatGlyphSpriteIDTag(tMPSpriteAssetName, spriteID);
			break;
		}
		}
		return text;
	}

	public string ReplaceGlyphPlaceHolders(string textWithPlaceholder, params GlyphInfo[] rewiredActions)
	{
		if (rewiredActions.Length == 1)
		{
			return ReplaceGlyphPlaceHolders(textWithPlaceholder, rewiredActions[0]);
		}
		textWithPlaceholder = m_RegexFindGlyphPlaceholder.Replace(textWithPlaceholder, FormatGlyphPlaceholder);
		object[] args;
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			string[] array = FormatGlyphSpriteTags(rewiredActions);
			string text = textWithPlaceholder;
			args = array;
			return StringFormat(text, args);
		}
		string[] array2 = FormatKeyNameTags(rewiredActions);
		string text2 = textWithPlaceholder;
		args = array2;
		return StringFormat(text2, args);
	}

	private int CountGlyphPlaceholders(string textWithPlaceholders)
	{
		return m_RegexFindGlyphPlaceholder.Matches(textWithPlaceholders).Count;
	}

	private string FormatGlyphPlaceholder(Match m)
	{
		return m.Value.Replace("key:", "");
	}

	private string[] FormatGlyphSpriteTags(GlyphInfo[] rewiredActions)
	{
		string[] array = new string[rewiredActions.Length];
		for (int i = 0; i < rewiredActions.Length; i++)
		{
			array[i] = FormatGlyphSpriteTag(rewiredActions[i].m_RewiredAction, rewiredActions[i].m_GlyphType);
		}
		return array;
	}

	private string FormatGlyphSpriteTag(int rewiredAction, JoystickGlyphIndex.GlyphType glyphType = JoystickGlyphIndex.GlyphType.Default)
	{
		int spriteID = Singleton.Manager<ManInput>.inst.FindGlyphIDForAction(rewiredAction, glyphType);
		string tMPSpriteAssetName = Singleton.Manager<ManInput>.inst.GetTMPSpriteAssetName();
		return FormatGlyphSpriteIDTag(tMPSpriteAssetName, spriteID);
	}

	private string FormatGlyphSpriteIDTag(string spriteAsset, int spriteID)
	{
		if (spriteAsset.NullOrEmpty() || spriteID == -1)
		{
			return string.Empty;
		}
		return StringFormat("<sprite=\"{0}\" index={1}>", spriteAsset, spriteID);
	}

	private string[] FormatKeyNameTags(GlyphInfo[] rewiredActionGlyphInfos)
	{
		string[] array = new string[rewiredActionGlyphInfos.Length];
		for (int i = 0; i < rewiredActionGlyphInfos.Length; i++)
		{
			array[i] = FormatKeyNameTag(rewiredActionGlyphInfos[i].m_RewiredAction);
		}
		return array;
	}

	private string FormatKeyNameTag(int rewiredAction)
	{
		string text = Singleton.Manager<ManInput>.inst.FindKeyNameForAction(rewiredAction, ControllerType.Keyboard);
		return StringFormat("<color=#77C0FFFF>[{0}]</color>", text);
	}

	private string StringFormat(string text, params object[] args)
	{
		try
		{
			return string.Format(text, args);
		}
		catch (Exception ex)
		{
			string text2 = string.Empty;
			if (args != null)
			{
				string[] array = new string[args.Length];
				for (int i = 0; i < args.Length; i++)
				{
					array[i] = args[i].ToString();
				}
				text2 = string.Join(", ", array);
			}
			throw new Exception("Localisation.StringFormat: could not perform substiution. Input text: '" + text + "' Args: " + text2 + " Error: " + ex.Message);
		}
	}

	private static int GenerateLocHash(string bank, string id)
	{
		return (bank + id).GetHashCode();
	}

	public string[] GetLocalisedStringBank(LocalisationEnums.StringBanks bankName)
	{
		return m_AllBanks[(int)bankName].Strings;
	}

	public static Type GetLocEnumType(string enumName)
	{
		Type type = typeof(LocalisationEnums.StringBanks).Assembly.GetType("LocalisationEnums+" + enumName);
		if (type == null)
		{
			type = typeof(LocalisationEnums.BlockNames);
		}
		return type;
	}

	private void LoadBanks()
	{
		m_AllBanks.Clear();
		m_HashLookup.Clear();
		string text = "LOC_" + CurrentLanguage;
		TextAsset textAsset = Resources.Load("Localisation/" + text) as TextAsset;
		if (!(textAsset != null))
		{
			return;
		}
		using StreamReader streamReader = new StreamReader(new MemoryStream(textAsset.bytes));
		StringBank stringBank = null;
		List<string> list = new List<string>();
		string text2;
		while ((text2 = streamReader.ReadLine()) != null)
		{
			if (text2.StartsWith(BANK_DELIMITER))
			{
				if (stringBank != null)
				{
					stringBank.Strings = list.ToArray();
					list.Clear();
					m_AllBanks.Add(stringBank);
				}
				stringBank = new StringBank(text2.Substring(1).Trim());
				continue;
			}
			if (stringBank == null)
			{
				d.LogError("Loading localisation text but found a string before a bank name");
				stringBank = new StringBank("EmptyBank");
			}
			int num = text2.IndexOf(':');
			string id;
			string text3;
			if (num >= 0)
			{
				id = text2.Substring(0, num);
				text3 = text2.Substring(num + 1, text2.Length - (num + 1));
			}
			else
			{
				d.LogError("Count not find colon separator in string \"" + text2 + "\"");
				id = "none";
				text3 = text2;
			}
			text3 = text3.Replace("\\n", Environment.NewLine);
			list.Add(text3);
			m_HashLookup.Add(GenerateLocHash(stringBank.BankName, id), text3);
		}
		if (stringBank != null)
		{
			stringBank.Strings = list.ToArray();
			list.Clear();
			m_AllBanks.Add(stringBank);
		}
	}

	public void ChangeLanguage(LocalisationEnums.Languages language)
	{
		d.AssertFormat(m_PlatformSupportedLanguages != null && m_PlatformSupportedLanguages.Contains(language), "Switching to a non-supported language {0}", language);
		d.Log($"ChangeLanguage({language})");
		m_AllBanks.Clear();
		m_HashLookup.Clear();
		ClearCachedCultureInfo();
		CurrentLanguage = language;
		LoadBanks();
		if (SKU.IsNetEase)
		{
			LoadFallbackNetEaseTechNames();
		}
		OnLanguageChanged.Send();
	}

	private void Initialise()
	{
		LocalisationEnums.Languages languages = m_DefaultLanguage;
		bool flag = true;
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (currentUser != null)
		{
			languages = currentUser.m_CurrentLanguage;
			flag = false;
		}
		LocalisationEnums.Languages languages2 = (SKU.IsTeyon ? LocalisationEnums.Languages.Japanese_Teyon : DetermineDefaultEnglishVariant());
		if (flag)
		{
			SystemLanguage systemLanguage = Application.systemLanguage;
			d.Log("System language is " + systemLanguage);
			switch (systemLanguage)
			{
			case SystemLanguage.English:
				languages = DetermineDefaultEnglishVariant();
				break;
			case SystemLanguage.Czech:
				languages = LocalisationEnums.Languages.Czech;
				break;
			case SystemLanguage.Danish:
				languages = LocalisationEnums.Languages.Danish;
				break;
			case SystemLanguage.Dutch:
				languages = LocalisationEnums.Languages.Dutch;
				break;
			case SystemLanguage.French:
				languages = LocalisationEnums.Languages.French;
				break;
			case SystemLanguage.German:
				languages = LocalisationEnums.Languages.German;
				break;
			case SystemLanguage.Italian:
				languages = LocalisationEnums.Languages.Italian;
				break;
			case SystemLanguage.Japanese:
				languages = (SKU.IsTeyon ? LocalisationEnums.Languages.Japanese_Teyon : LocalisationEnums.Languages.Japanese);
				break;
			case SystemLanguage.Korean:
				languages = LocalisationEnums.Languages.Korean;
				break;
			case SystemLanguage.Polish:
				languages = LocalisationEnums.Languages.Polish;
				break;
			case SystemLanguage.Portuguese:
				languages = LocalisationEnums.Languages.Portuguese;
				break;
			case SystemLanguage.Russian:
				languages = LocalisationEnums.Languages.Russian;
				break;
			case SystemLanguage.Chinese:
			case SystemLanguage.ChineseSimplified:
				languages = LocalisationEnums.Languages.Simplified_Chinese;
				break;
			case SystemLanguage.Spanish:
				languages = LocalisationEnums.Languages.Spanish;
				break;
			case SystemLanguage.Swedish:
				languages = LocalisationEnums.Languages.Swedish;
				break;
			case SystemLanguage.Thai:
				languages = LocalisationEnums.Languages.Thai;
				break;
			case SystemLanguage.ChineseTraditional:
				languages = LocalisationEnums.Languages.Traditional_Chinese;
				break;
			case SystemLanguage.Turkish:
				languages = LocalisationEnums.Languages.Turkish;
				break;
			default:
				languages = languages2;
				break;
			}
			if (SKU.IsNetEase)
			{
				languages = LocalisationEnums.Languages.Simplified_Chinese_NE;
			}
			d.Log("Ideal language is " + languages);
		}
		if (!m_PlatformSupportedLanguages.Contains(languages))
		{
			languages = languages2;
			d.Log("Switching to fallback language: " + languages);
		}
		ChangeLanguage(languages);
		StringLookup.CreateLookUpTables();
	}

	private static LocalisationEnums.Languages DetermineDefaultEnglishVariant()
	{
		return LocalisationEnums.Languages.English;
	}

	public List<LocalisationEnums.Languages> GetSupportedLanguages()
	{
		return m_PlatformSupportedLanguages;
	}

	private static CultureInfo GetCultureInfo(LocalisationEnums.Languages language)
	{
		LanguageCulturePair languageCulturePair = null;
		for (int i = 0; i < s_LanguageToCultureNameLookup.Length; i++)
		{
			if (language == s_LanguageToCultureNameLookup[i].language)
			{
				languageCulturePair = s_LanguageToCultureNameLookup[i];
			}
		}
		CultureInfo cultureInfo = null;
		if (languageCulturePair != null)
		{
			return CultureInfo.GetCultureInfo(languageCulturePair.cultureName);
		}
		d.LogError("Failed to find appropriate Culture info mapping for language " + language);
		return CultureInfo.CurrentCulture;
	}

	private void CacheCultureInfoIfNeeded()
	{
		if (m_CachedUserSelectedCultureInfo == null)
		{
			m_CachedUserSelectedCultureInfo = GetCultureInfo(CurrentLanguage);
			m_CachedNumberFormatInfo = (NumberFormatInfo)m_CachedUserSelectedCultureInfo.NumberFormat.Clone();
			m_CachedNumberFormatInfo.NumberDecimalDigits = 0;
		}
	}

	private void ClearCachedCultureInfo()
	{
		m_CachedUserSelectedCultureInfo = null;
		m_CachedNumberFormatInfo = null;
	}

	private void LoadFallbackNetEaseTechNames()
	{
		TextAsset textAsset = Resources.Load<TextAsset>(m_NetEaseTechNamesResourceName);
		if (textAsset == null)
		{
			d.LogError("Failed to load resource for tech names. " + m_NetEaseTechNamesResourceName);
			m_FallbackNetEaseNames = new List<string>();
			return;
		}
		string[] array = textAsset.text.Split('\n');
		m_FallbackNetEaseNames = new List<string>(array.Length);
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string text = array2[i].Trim();
			if (text != "")
			{
				m_FallbackNetEaseNames.Add(text);
			}
		}
	}

	public string GetFallbackNetEaseNameForTech(int techHash)
	{
		if (m_FallbackNetEaseNames == null || m_FallbackNetEaseNames.Count == 0)
		{
			return "";
		}
		return m_FallbackNetEaseNames[Mathf.Abs(techHash) % m_FallbackNetEaseNames.Count];
	}

	private void Awake()
	{
		m_PlatformSupportedLanguages = new List<LocalisationEnums.Languages>();
		if (SKU.IsNetEase)
		{
			m_PlatformSupportedLanguages.Add(LocalisationEnums.Languages.English);
			m_PlatformSupportedLanguages.Add(LocalisationEnums.Languages.Simplified_Chinese_NE);
		}
		else if (SKU.ConsoleUI)
		{
			m_PlatformSupportedLanguages.Add(DetermineDefaultEnglishVariant());
			m_PlatformSupportedLanguages.Add(LocalisationEnums.Languages.Traditional_Chinese);
			m_PlatformSupportedLanguages.Add(LocalisationEnums.Languages.Simplified_Chinese);
			if (SKU.IsTeyon)
			{
				m_PlatformSupportedLanguages.Add(LocalisationEnums.Languages.Korean);
				m_PlatformSupportedLanguages.Add(LocalisationEnums.Languages.Japanese_Teyon);
			}
			else
			{
				m_PlatformSupportedLanguages.Add(LocalisationEnums.Languages.French);
				m_PlatformSupportedLanguages.Add(LocalisationEnums.Languages.Italian);
				m_PlatformSupportedLanguages.Add(LocalisationEnums.Languages.German);
				m_PlatformSupportedLanguages.Add(LocalisationEnums.Languages.Spanish);
				m_PlatformSupportedLanguages.Add(LocalisationEnums.Languages.Russian);
				m_PlatformSupportedLanguages.Add(LocalisationEnums.Languages.Japanese);
			}
		}
		else
		{
			foreach (LocalisationEnums.Languages value in Enum.GetValues(typeof(LocalisationEnums.Languages)))
			{
				if (value != LocalisationEnums.Languages.Simplified_Chinese_NE && value != LocalisationEnums.Languages.Japanese_Teyon)
				{
					m_PlatformSupportedLanguages.Add(value);
				}
			}
		}
		Singleton.DoOnceAfterStart(delegate
		{
			Initialise();
		});
	}
}
