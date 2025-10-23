using Binding;

namespace Snapshots;

public interface ISnapshotService
{
	Bindable<ManSnapshots.QueryStatus> QueryStatus { get; }

	bool SupportsRenameAndDelete();

	void DeleteSnapshot(Snapshot snapshot);

	bool SnapshotExists(string snapshotName);

	void RenameSnapshot(Snapshot snapshot, string newName);

	void SetSnapshotMetadata(Snapshot snapshot, Snapshot.MetaData metadata);

	bool SupportsFavourites();

	void SetSnapshotFavourite(Snapshot snapshot, bool favourite);

	bool SupportsFolders();

	void SetSnapshotFolder(Snapshot snapshot, string folderName);

	void ApplyCachedMetadataToSnapshot(Snapshot snapshot);
}
