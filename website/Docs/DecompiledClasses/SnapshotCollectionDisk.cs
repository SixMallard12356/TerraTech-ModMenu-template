#define UNITY_EDITOR
public class SnapshotCollectionDisk : SnapshotCollection<SnapshotDisk>
{
	public SnapshotCollectionDisk()
		: base((string)null, (string)null, (string)null, (TankPreset.UserData)null)
	{
		defaultSort = CompareFileName;
		defaultReplaceCheck = IsSameFileName;
	}

	public override void AddSnapshot(SnapshotDisk snapshot)
	{
		string fileName = snapshot.GetFileName();
		snapshot.m_Name.Value = fileName;
		snapshot.techData.Name = fileName;
		base.AddSnapshot(snapshot);
	}

	public override void AddOrReplaceSnapshot(SnapshotDisk snapshot)
	{
		if (snapshot != null)
		{
			string fileName = snapshot.GetFileName();
			snapshot.m_Name.Value = fileName;
			snapshot.techData.Name = fileName;
			base.AddOrReplaceSnapshot(snapshot);
		}
		else
		{
			d.LogError("SnapshotCollectionDisk.AddOrReplaceSnapshot - Snapshot passed in was NULL!");
		}
	}

	private int CompareFileName(SnapshotDisk a, SnapshotDisk b)
	{
		return a.snapName.CompareTo(b.snapName);
	}

	private bool IsSameFileName(SnapshotDisk a, SnapshotDisk b)
	{
		return a.snapName == b.snapName;
	}

	public SnapshotDisk FindSnapshot(string snapshotName)
	{
		if (!SKU.ConsoleUI)
		{
			snapshotName = ManSnapshots.GetFilePathSnapshot(snapshotName);
		}
		for (int i = 0; i < base.Snapshots.Count; i++)
		{
			SnapshotDisk snapshotDisk = base.Snapshots[i];
			if (snapshotDisk.snapName == snapshotName)
			{
				d.Log("SnapshotCollectionDisk.FindSnapshot found " + snapshotName + " m_Name=" + snapshotDisk.m_Name?.Value + " techData.Name=" + snapshotDisk.techData?.Name);
				return snapshotDisk;
			}
		}
		d.Log("SnapshotCollectionDisk.FindSnapshot did not find " + snapshotName);
		return null;
	}
}
