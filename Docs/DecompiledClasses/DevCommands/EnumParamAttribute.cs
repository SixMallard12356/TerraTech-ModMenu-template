#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevCommands;

public class EnumParamAttribute : DevParamAttribute
{
	private Type m_EnumType;

	private SortedEnum m_SortedEnum;

	private Dictionary<string, string> m_Modifications;

	public SortedEnum.EnumSortType SortType { get; set; }

	private SortedEnum SortedEnum
	{
		get
		{
			if (m_SortedEnum == null)
			{
				m_SortedEnum = new SortedEnum(m_EnumType, EnumToString, SortType);
			}
			return m_SortedEnum;
		}
	}

	public override bool UseCustomParse => true;

	public EnumParamAttribute(Type enumType)
	{
		m_EnumType = enumType;
	}

	public override IEnumerable<string> GetAutoCompletionValues(string partialName)
	{
		string value;
		return from n in SortedEnum.AllNames()
			select (!m_Modifications.TryGetValue(n, out value)) ? n : value into n
			where n != null
			select n;
	}

	public override bool TryParse(string paramStr, out object wrappedParamVal)
	{
		try
		{
			string value = paramStr;
			if (m_Modifications != null)
			{
				foreach (KeyValuePair<string, string> modification in m_Modifications)
				{
					if (modification.Value.EqualsNoCase(paramStr))
					{
						value = modification.Key;
					}
				}
			}
			object obj = Enum.Parse(m_EnumType, value, ignoreCase: true);
			if (obj != null)
			{
				wrappedParamVal = obj;
				return true;
			}
		}
		catch (Exception message)
		{
			d.LogError(message);
		}
		wrappedParamVal = null;
		return false;
	}

	private string EnumToString(string enumVal)
	{
		return enumVal;
	}

	protected void ExcludeValue(int enumVal)
	{
		DisplayValueAs(enumVal, null);
	}

	protected void DisplayValueAs(int enumVal, string replacementName)
	{
		if (m_Modifications == null)
		{
			m_Modifications = new Dictionary<string, string>();
		}
		int sortedIndex = SortedEnum.GetSortedIndex(enumVal);
		string nameAtSortedIndex = SortedEnum.GetNameAtSortedIndex(sortedIndex);
		m_Modifications.Add(nameAtSortedIndex, replacementName);
	}
}
