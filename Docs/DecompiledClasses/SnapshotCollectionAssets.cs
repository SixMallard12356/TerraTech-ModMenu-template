#define UNITY_EDITOR
using System;
using UnityEngine;

public class SnapshotCollectionAssets : SnapshotCollection<Snapshot>
{
	public SnapshotCollectionAssets(string modeRestriction, string subModeRestriction, string userDataRestriction, TankPreset.UserData userProfileRestriction = null)
		: base(modeRestriction, subModeRestriction, userDataRestriction, userProfileRestriction)
	{
	}

	public bool TryAddFromImage(Texture2D snapshotRender, out Snapshot snapshot)
	{
		snapshot = DecodeSnapshotFromImage(snapshotRender);
		if (snapshot != null)
		{
			snapshot.m_Name.Value = snapshot.techData.Name;
			snapshot.UniqueID = snapshotRender.GetHashCode().ToString();
			snapshot.DateCreated = DateTime.Now;
			AddSnapshot(snapshot);
			return true;
		}
		d.LogWarning("SnapshotCollectionAssets.TryAddFromImage texture does not contain valid tech data: " + snapshotRender.name);
		return false;
	}
}
