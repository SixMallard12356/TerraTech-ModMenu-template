using System;
using System.Collections.Generic;

public struct SnapshotFolderLiveData
{
	public class SnapshotFolderComparer : IComparer<SnapshotFolderLiveData>
	{
		public bool SortDescending;

		public int Compare(SnapshotFolderLiveData a, SnapshotFolderLiveData b)
		{
			int num = CompareSnapshotFolderData(a, b);
			if (num == 0)
			{
				num = string.Compare(a.Name, b.Name, StringComparison.InvariantCulture);
			}
			if (!SortDescending)
			{
				return num;
			}
			return -num;
		}

		protected int CompareSnapshotFolderData(SnapshotFolderLiveData a, SnapshotFolderLiveData b)
		{
			return a.Name.CompareTo(b.Name);
		}
	}

	public string Name;

	public HashSet<SnapshotLiveData> Snapshots;

	public bool IsExpanded;

	private static readonly SnapshotFolderComparer _s_SortComparer = new SnapshotFolderComparer();

	public SnapshotFolderLiveData(string name, bool isExpanded)
	{
		Name = name;
		Snapshots = new HashSet<SnapshotLiveData>();
		IsExpanded = isExpanded;
	}

	public static SnapshotFolderComparer GetSortComparer(bool sortDescending = false)
	{
		SnapshotFolderComparer s_SortComparer = _s_SortComparer;
		s_SortComparer.SortDescending = sortDescending;
		return s_SortComparer;
	}
}
