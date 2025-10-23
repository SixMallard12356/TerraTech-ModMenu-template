using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class LocalisedString
{
	public bool m_GUIExpanded;

	[FormerlySerializedAs("m_StringBankName")]
	[SerializeField]
	public string m_Bank;

	[FormerlySerializedAs("m_String")]
	[SerializeField]
	public string m_Id;

	[SerializeField]
	public Localisation.GlyphInfo[] m_InlineGlyphs;

	[JsonIgnore]
	public string Value
	{
		get
		{
			if (m_Bank.NullOrEmpty() || m_Id.NullOrEmpty())
			{
				return null;
			}
			return Singleton.Manager<Localisation>.inst.GetLocalisedString(m_Bank, m_Id, m_InlineGlyphs);
		}
	}

	[JsonIgnore]
	public bool IsValid
	{
		get
		{
			if (!m_Bank.NullOrEmpty())
			{
				return !m_Id.NullOrEmpty();
			}
			return false;
		}
	}

	public override string ToString()
	{
		return Value;
	}

	public static LocalisationEnums.StringBanks BankNameToEnum(string bankName)
	{
		try
		{
			return (LocalisationEnums.StringBanks)Enum.Parse(typeof(LocalisationEnums.StringBanks), bankName);
		}
		catch
		{
			return LocalisationEnums.StringBanks.BlockNames;
		}
	}

	public static int StringIdToEnum(Type enumType, string enumStringValue)
	{
		try
		{
			return (int)Enum.Parse(enumType, enumStringValue);
		}
		catch
		{
			return 0;
		}
	}
}
