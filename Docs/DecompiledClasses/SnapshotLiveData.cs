using System;
using System.Collections.Generic;

public struct SnapshotLiveData
{
	public enum SnapshotSortType
	{
		Name,
		TechBBValue,
		TechBlockLimitCost,
		DateCreated
	}

	public abstract class SnapshotComparer : IComparer<SnapshotLiveData>
	{
		public bool SortDescending;

		public List<SnapshotFolderLiveData> FolderStructure;

		public int Compare(SnapshotLiveData a, SnapshotLiveData b)
		{
			int num = 0;
			string folderName = a.m_Snapshot.m_Meta.Value.FolderName;
			string folderName2 = b.m_Snapshot.m_Meta.Value.FolderName;
			if (FolderStructure != null && folderName != folderName2)
			{
				for (int i = 0; i < FolderStructure.Count; i++)
				{
					string name = FolderStructure[i].Name;
					if (name == folderName)
					{
						num = 1;
					}
					else if (name == folderName2)
					{
						num = -1;
					}
				}
			}
			if (num == 0)
			{
				num = CompareSnapshotData(a, b);
			}
			if (num == 0)
			{
				num = string.Compare(a.m_Snapshot.UniqueID, b.m_Snapshot.UniqueID, StringComparison.InvariantCulture);
			}
			if (!SortDescending)
			{
				return num;
			}
			return -num;
		}

		protected abstract int CompareSnapshotData(SnapshotLiveData a, SnapshotLiveData b);
	}

	public class SnapshotComparerNameAlphabetical : SnapshotComparer
	{
		protected override int CompareSnapshotData(SnapshotLiveData a, SnapshotLiveData b)
		{
			return a.m_Snapshot.techData.Name.CompareTo(b.m_Snapshot.techData.Name);
		}
	}

	public class SnapshotComparerTechBBCost : SnapshotComparer
	{
		protected override int CompareSnapshotData(SnapshotLiveData a, SnapshotLiveData b)
		{
			return a.m_ValidData.m_BlockBBCost.CompareTo(b.m_ValidData.m_BlockBBCost);
		}
	}

	public class SnapshotComparerTechBlockLimitCost : SnapshotComparer
	{
		protected override int CompareSnapshotData(SnapshotLiveData a, SnapshotLiveData b)
		{
			return a.m_ValidData.m_LimiterCost.CompareTo(b.m_ValidData.m_LimiterCost);
		}
	}

	public class SnapshotComparerCreationDate : SnapshotComparer
	{
		protected override int CompareSnapshotData(SnapshotLiveData a, SnapshotLiveData b)
		{
			return a.m_Snapshot.DateCreated.CompareTo(b.m_Snapshot.DateCreated);
		}
	}

	private static readonly SnapshotComparer[] s_SnapshotComparer = new SnapshotComparer[4]
	{
		new SnapshotComparerNameAlphabetical(),
		new SnapshotComparerTechBBCost(),
		new SnapshotComparerTechBlockLimitCost(),
		new SnapshotComparerCreationDate()
	};

	public Snapshot m_Snapshot;

	public TechDataAvailValidation m_ValidData;

	public bool m_Loaded;

	public bool Equals(SnapshotLiveData other)
	{
		return m_Snapshot == other.m_Snapshot;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		if (obj is SnapshotLiveData)
		{
			return Equals((SnapshotLiveData)obj);
		}
		return false;
	}

	public override int GetHashCode()
	{
		if (m_Snapshot == null)
		{
			return 0;
		}
		return m_Snapshot.GetHashCode();
	}

	public static bool operator ==(SnapshotLiveData a, SnapshotLiveData b)
	{
		return a.GetHashCode() == b.GetHashCode();
	}

	public static bool operator !=(SnapshotLiveData a, SnapshotLiveData b)
	{
		return !(a == b);
	}

	public static SnapshotComparer GetSortComparer(SnapshotSortType sortType, List<SnapshotFolderLiveData> folderStructure, bool sortDescending = false)
	{
		return GetSortComparer((int)sortType, folderStructure, sortDescending);
	}

	public static SnapshotComparer GetSortComparer(int value, List<SnapshotFolderLiveData> folderStructure, bool sortDescending = false)
	{
		SnapshotComparer obj = s_SnapshotComparer[value];
		obj.SortDescending = sortDescending;
		obj.FolderStructure = folderStructure;
		return obj;
	}
}
