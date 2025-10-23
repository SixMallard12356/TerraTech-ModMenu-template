#define UNITY_EDITOR
using System;
using System.Collections.Generic;

public class SortedEnum
{
	public enum EnumSortType
	{
		AlphabeticalAscending,
		AlphabeticalDescending,
		Unsorted
	}

	private class AlphabeticalEnumPairSorterAscending : IComparer<EnumDisplayPair>
	{
		public int Compare(EnumDisplayPair a, EnumDisplayPair b)
		{
			return a.displayString.CompareTo(b.displayString);
		}
	}

	private class AlphabeticalEnumPairSorterDescending : IComparer<EnumDisplayPair>
	{
		public int Compare(EnumDisplayPair a, EnumDisplayPair b)
		{
			return b.displayString.CompareTo(a.displayString);
		}
	}

	private class EnumOrderEnumPairSorterAscending : IComparer<EnumDisplayPair>
	{
		public int Compare(EnumDisplayPair a, EnumDisplayPair b)
		{
			return a.enumValueInt.CompareTo(b.enumValueInt);
		}
	}

	public struct EnumDisplayPair
	{
		public string displayString;

		public int enumValueInt;

		public static bool operator ==(EnumDisplayPair a, EnumDisplayPair b)
		{
			return a.enumValueInt == b.enumValueInt;
		}

		public static bool operator !=(EnumDisplayPair a, EnumDisplayPair b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is EnumDisplayPair))
			{
				return false;
			}
			return this == (EnumDisplayPair)obj;
		}

		public override int GetHashCode()
		{
			return enumValueInt;
		}
	}

	public delegate string EnumStringifier(string enumValueName);

	private EnumSortType m_SortType;

	private Type m_EnumType;

	private EnumStringifier m_EnumStringifier;

	private int m_NumEnumValues;

	private List<EnumDisplayPair> m_SortedPairList;

	public int Count => m_NumEnumValues;

	public Type EnumType => m_EnumType;

	public SortedEnum(Type enumType, EnumSortType sortType = EnumSortType.AlphabeticalAscending)
	{
		Initialise(enumType, sortType, EnumToStringDefault);
	}

	public SortedEnum(Type enumType, EnumStringifier enumToStringCallback, EnumSortType sortType = EnumSortType.AlphabeticalAscending)
	{
		Initialise(enumType, sortType, enumToStringCallback);
	}

	public void SetSortType(EnumSortType sortType)
	{
		if (sortType != m_SortType)
		{
			m_SortType = sortType;
			Sort();
		}
	}

	public int GetIntValueAtSortedIndex(int index)
	{
		return GetEnumPair(index).enumValueInt;
	}

	public string GetNameAtSortedIndex(int index)
	{
		return GetEnumPair(index).displayString;
	}

	public IEnumerable<string> AllNames()
	{
		for (int i = 0; i < m_SortedPairList.Count; i++)
		{
			yield return m_SortedPairList[i].displayString;
		}
	}

	public string[] ToStringArray()
	{
		if (m_SortedPairList == null)
		{
			return new string[0];
		}
		string[] array = new string[m_SortedPairList.Count];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = m_SortedPairList[i].displayString;
		}
		return array;
	}

	public int GetSortedIndex(int enumValue)
	{
		for (int i = 0; i < m_NumEnumValues; i++)
		{
			if (GetEnumPair(i).enumValueInt == enumValue)
			{
				return i;
			}
		}
		return -1;
	}

	private void Initialise(Type enumType, EnumSortType sortType, EnumStringifier enumToStringCallback)
	{
		if (enumType != null && !enumType.IsEnum)
		{
			throw new ArgumentException("SortedEnum must be passed an enumerated type!");
		}
		m_EnumType = enumType;
		m_SortType = sortType;
		m_EnumStringifier = enumToStringCallback;
		InitialiseList();
		Sort();
	}

	private void InitialiseList()
	{
		if (m_SortedPairList == null)
		{
			m_NumEnumValues = Enum.GetNames(m_EnumType).Length;
			m_SortedPairList = new List<EnumDisplayPair>(m_NumEnumValues);
		}
		foreach (object value in Enum.GetValues(m_EnumType))
		{
			EnumDisplayPair item = new EnumDisplayPair
			{
				enumValueInt = (int)value,
				displayString = m_EnumStringifier(Enum.GetName(m_EnumType, value))
			};
			m_SortedPairList.Add(item);
		}
	}

	private void Sort()
	{
		IComparer<EnumDisplayPair> sortComparer = GetSortComparer(m_SortType);
		m_SortedPairList.Sort(sortComparer);
	}

	private EnumDisplayPair GetEnumPair(int index)
	{
		d.Assert(index >= 0 && index < m_NumEnumValues, "SortedEnum.GetEnumPair invalid index passed in!");
		return m_SortedPairList[index];
	}

	private IComparer<EnumDisplayPair> GetSortComparer(EnumSortType sortType)
	{
		switch (sortType)
		{
		case EnumSortType.AlphabeticalDescending:
			return new AlphabeticalEnumPairSorterDescending();
		case EnumSortType.AlphabeticalAscending:
			return new AlphabeticalEnumPairSorterAscending();
		case EnumSortType.Unsorted:
			return new EnumOrderEnumPairSorterAscending();
		default:
			d.LogError($"SortedEnum Sort comparer for type {sortType.ToString()} not set!");
			return null;
		}
	}

	private string EnumToStringDefault(string enumValString)
	{
		return enumValString.ToString();
	}
}
