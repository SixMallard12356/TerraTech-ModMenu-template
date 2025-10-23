using UnityEngine;

public class SearchableEnumAttribute : PropertyAttribute
{
	public SortedEnum.EnumSortType SortType { get; private set; }

	public bool TopLevel { get; private set; }

	public SearchableEnumAttribute(SortedEnum.EnumSortType sortType = SortedEnum.EnumSortType.AlphabeticalAscending, bool showOnTopLevel = false)
	{
		SortType = sortType;
		TopLevel = showOnTopLevel;
	}
}
