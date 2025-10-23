using System.Collections.Generic;
using System.Linq;
using Binding;

namespace DevCommands;

public class SnapshotAttribute : DevParamAttribute
{
	private static string[] s_Values;

	private static bool s_ListRefresh;

	public override IEnumerable<string> GetAutoCompletionValues(string partialName)
	{
		if (s_ListRefresh || s_Values == null)
		{
			BindableList<SnapshotDisk> bindableList = Singleton.Manager<ManSnapshots>.inst.ServiceDisk?.GetSnapshotCollectionDisk()?.Snapshots;
			if (s_Values == null)
			{
				bindableList?.Bind(OnSnapshotCollectionChanged);
			}
			s_Values = bindableList?.Select((SnapshotDisk s) => s.m_Name.Value).ToArray();
			s_ListRefresh = false;
		}
		return s_Values;
	}

	private void OnSnapshotCollectionChanged(BindableList<SnapshotDisk> list, int index, BindableChange changedType)
	{
		s_ListRefresh = s_ListRefresh || changedType == BindableChange.Reset || changedType == BindableChange.ItemAdded || changedType == BindableChange.ItemDeleted;
	}
}
