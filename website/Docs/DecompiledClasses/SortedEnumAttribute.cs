using UnityEngine;

public class SortedEnumAttribute : PropertyAttribute
{
	public SortedEnum.EnumSortType SortType { get; set; }

	public SortedEnumAttribute()
	{
		SortType = SortedEnum.EnumSortType.AlphabeticalAscending;
	}

	public SortedEnumAttribute(SortedEnum.EnumSortType _sortType)
	{
		SortType = _sortType;
	}
}
