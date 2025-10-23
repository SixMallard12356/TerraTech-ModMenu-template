#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Binding;
using UnityEngine;

namespace Snapshots;

public abstract class SnapshotServiceDisk : MonoBehaviour, ISnapshotService
{
	protected Dictionary<string, Snapshot.MetaData> m_SnapshotUIDMetadataLookup = new Dictionary<string, Snapshot.MetaData>();

	protected SnapshotCollectionDisk m_SnapshotCollectionDisk;

	protected Bindable<ManSnapshots.QueryStatus> m_QueryStatus = new Bindable<ManSnapshots.QueryStatus>(ManSnapshots.QueryStatus.Nil);

	public Bindable<ManSnapshots.QueryStatus> QueryStatus => m_QueryStatus;

	public abstract IEnumerator UpdateSnapshotCacheOnStartup();

	public abstract void SaveSnapshotRender(TechData techData, Texture2D snapshotRender, string snapshotName, Action<bool> saveResultCallback);

	public abstract bool CheckSnapshotExists(string filePath);

	public abstract IntVector2 GetPreferredImageSize();

	public abstract bool EmbedSnapshotsInPNGs();

	public abstract int GetMaxSnapshotCount();

	public abstract bool SnapshotExists(string snapshotName);

	public bool SupportsRenameAndDelete()
	{
		return true;
	}

	public abstract void RenameSnapshot(Snapshot snapshot, string newName);

	public abstract void DeleteSnapshot(Snapshot snapshot);

	public bool SupportsMetadata()
	{
		if (SupportsFavourites())
		{
			return SupportsFolders();
		}
		return false;
	}

	public bool SupportsFavourites()
	{
		return true;
	}

	public void SetSnapshotMetadata(Snapshot snapshot, Snapshot.MetaData metadata)
	{
		Snapshot.MetaData newMetadata = new Snapshot.MetaData(metadata);
		OnMetadataChanged(snapshot, newMetadata);
	}

	public void SetSnapshotFavourite(Snapshot snapshot, bool favourite)
	{
		d.AssertFormat(snapshot.m_Meta.Value.IsFavourite != favourite, "SetSnapshotFavourite - Snapshot {0} is being set as favourite={1}, but is already in that state!", snapshot.UniqueID, favourite);
		Snapshot.MetaData value = snapshot.m_Meta.Value;
		value.IsFavourite = favourite;
		OnMetadataChanged(snapshot, value);
	}

	public bool SupportsFolders()
	{
		return true;
	}

	public void SetSnapshotFolder(Snapshot snapshot, string folderName)
	{
		d.AssertFormat(snapshot.m_Meta.Value.FolderName != folderName, "SetSnapshotFavourite - Snapshot {0}'s folder is being set as {1}, but is already in that folder!", snapshot.UniqueID, folderName);
		Snapshot.MetaData value = snapshot.m_Meta.Value;
		value.FolderName = folderName;
		OnMetadataChanged(snapshot, value);
	}

	private void OnMetadataChanged(Snapshot snapshot, Snapshot.MetaData newMetadata)
	{
		newMetadata.Snapshot_UID = snapshot.UniqueID;
		snapshot.m_Meta.Value = newMetadata;
		m_SnapshotUIDMetadataLookup[snapshot.UniqueID] = newMetadata;
		Snapshot.MetaData[] array = m_SnapshotUIDMetadataLookup.Values.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			Snapshot.MetaData metaData = array[i];
			if (metaData.IsGeneric())
			{
				m_SnapshotUIDMetadataLookup.Remove(metaData.Snapshot_UID);
			}
		}
		Singleton.Manager<ManProfile>.inst.Save();
	}

	public void ApplyCachedMetadataToSnapshot(Snapshot snapshot)
	{
		snapshot.m_Meta.Value = (m_SnapshotUIDMetadataLookup.TryGetValue(snapshot.UniqueID, out var value) ? value : new Snapshot.MetaData(snapshot.UniqueID));
	}

	public virtual SnapshotCollectionDisk GetSnapshotCollectionDisk()
	{
		d.Assert(HasSnapshotCollectionDisk(), "m_SnapshotCollectionDisk is null. This have been initialized during UpdateSnapshotCacheOnStartup");
		return m_SnapshotCollectionDisk;
	}

	public virtual bool HasSnapshotCollectionDisk()
	{
		return m_SnapshotCollectionDisk != null;
	}

	public void SetSnapshotMetadata(List<Snapshot.MetaData> metadataList)
	{
		m_SnapshotUIDMetadataLookup.Clear();
		for (int i = 0; i < metadataList.Count; i++)
		{
			m_SnapshotUIDMetadataLookup.Add(metadataList[i].Snapshot_UID, metadataList[i]);
		}
		if (m_SnapshotCollectionDisk == null || m_SnapshotCollectionDisk.Snapshots == null)
		{
			return;
		}
		foreach (SnapshotDisk snapshot in m_SnapshotCollectionDisk.Snapshots)
		{
			ApplyCachedMetadataToSnapshot(snapshot);
		}
	}

	public void GetSnapshotMetaData(ref List<Snapshot.MetaData> metadataList)
	{
		metadataList.Clear();
		foreach (Snapshot.MetaData value in m_SnapshotUIDMetadataLookup.Values)
		{
			metadataList.Add(value);
		}
	}
}
