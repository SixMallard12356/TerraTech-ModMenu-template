#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Snapshots;

public class SnapshotServiceDesktop : SnapshotServiceDisk
{
	private struct FileWatcherChange
	{
		public string filePath;

		public WatcherChangeTypes changeType;
	}

	private struct TechDataCache
	{
		public const int kCacheDataVersionNr = 1;

		public int version;

		public TechData.SerializedSnapshotData techSnapshotData;
	}

	public const string k_SnapshotDirName = "Snapshots";

	public const string k_CacheDirName = "Cache";

	public const string k_SuffixTechData = ".tdc";

	private const string k_PathBase = "/";

	private const int k_ThumbnailWidth = 296;

	private const int k_ThumbnailHeight = 185;

	private const int k_SteamThumbnailWidth = 640;

	private const int k_SteamThumbnailHeight = 360;

	private const int k_PreferredImageWidth = 640;

	private const int k_PreferredImageHeight = 400;

	private FileSystemWatcher m_FileWatcher;

	private object m_FileWatcherLock = new object();

	private Queue<FileWatcherChange> m_FileWatcherChangeList = new Queue<FileWatcherChange>();

	public override IEnumerator UpdateSnapshotCacheOnStartup()
	{
		m_QueryStatus.Value = ManSnapshots.QueryStatus.Requesting;
		string snapshotsDirPath = ManSaveGame.GetSaveDataFolder() + "/Snapshots";
		string path = ManSaveGame.GetSaveDataFolder() + "/Cache";
		string searchPattern = "*.png";
		try
		{
			Directory.CreateDirectory(snapshotsDirPath);
			Directory.CreateDirectory(path);
		}
		catch (Exception ex)
		{
			d.LogError("SnapshotServiceDesktop: Failed to create snapshot or cache directory: " + ex);
			yield break;
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(snapshotsDirPath);
		List<FileInfo> fileInfos = new List<FileInfo>(directoryInfo.GetFiles(searchPattern, SearchOption.AllDirectories));
		m_SnapshotCollectionDisk = new SnapshotCollectionDisk();
		m_SnapshotCollectionDisk.SortOnAddEnabled = false;
		for (int i = 0; i < fileInfos.Count; i++)
		{
			FileInfo fileInfo = fileInfos[i];
			if (!IsSnapshotCacheValid(fileInfo))
			{
				if (!TryCreateSnapshotCache(fileInfo))
				{
					d.LogError("SnapshotServiceDesktop.UpdateSnapshotCacheOnStartUp - snapshot contains invalid TechData, skipping. " + fileInfo.FullName);
					continue;
				}
				yield return null;
			}
			SnapshotDisk snapshotFromCache = GetSnapshotFromCache(fileInfo);
			if (snapshotFromCache != null && snapshotFromCache.techData != null && snapshotFromCache.techData.m_BlockSpecs != null && snapshotFromCache.techData.m_BlockSpecs.Count > 0)
			{
				m_SnapshotCollectionDisk.AddSnapshot(snapshotFromCache);
				continue;
			}
			d.LogError("SnapshotServiceDesktop.UpdateSnapshotCacheOnStartUp - snapshot cache contains invalid TechData, deleting and skipping. " + fileInfo.FullName);
			DeleteSnapshotCache(fileInfo);
		}
		m_SnapshotCollectionDisk.SortOnAddEnabled = true;
		if (m_FileWatcher == null)
		{
			try
			{
				m_FileWatcher = new FileSystemWatcher(snapshotsDirPath);
				m_FileWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;
				m_FileWatcher.Filter = "*.png";
				m_FileWatcher.IncludeSubdirectories = true;
				m_FileWatcher.EnableRaisingEvents = true;
				m_FileWatcher.Created += OnFileChanged;
				m_FileWatcher.Changed += OnFileChanged;
				m_FileWatcher.Deleted += OnFileChanged;
			}
			catch (Exception ex2)
			{
				d.LogError("Error when setting up file watcher on directory: " + snapshotsDirPath + " Exception: " + ex2);
			}
		}
		m_QueryStatus.Value = ManSnapshots.QueryStatus.Done;
		GC.Collect();
	}

	public override void SaveSnapshotRender(TechData techData, Texture2D snapshotRender, string snapshotName, Action<bool> saveResultCallback)
	{
		string filePathSnapshot = GetFilePathSnapshot(snapshotName);
		bool flag = FileUtils.SaveTexture(snapshotRender, filePathSnapshot);
		if (!flag)
		{
			d.LogError("Failed to save snapshot to: '" + filePathSnapshot + "'");
		}
		FileInfo fileInfo = new FileInfo(filePathSnapshot);
		TechData.SerializedSnapshotData techSnapshotData = new TechData.SerializedSnapshotData(techData);
		if (flag && TryCreateSnapshotCache(fileInfo, snapshotRender, techSnapshotData))
		{
			SnapshotDisk snapshotFromCache = GetSnapshotFromCache(fileInfo);
			m_SnapshotCollectionDisk.AddOrReplaceSnapshot(snapshotFromCache);
		}
		else
		{
			d.LogError("SnapshotServiceDesktop.SaveSnapshotRender - Could not create snapshot cache, aborting. " + fileInfo.FullName);
			flag = false;
		}
		saveResultCallback?.Invoke(flag);
	}

	public override IntVector2 GetPreferredImageSize()
	{
		return new IntVector2(640, 400);
	}

	public override bool EmbedSnapshotsInPNGs()
	{
		return true;
	}

	public override int GetMaxSnapshotCount()
	{
		return int.MaxValue;
	}

	public override void DeleteSnapshot(Snapshot snapshot)
	{
		SnapshotDisk snapshotDisk = snapshot as SnapshotDisk;
		m_SnapshotCollectionDisk.RemoveSnapshot(snapshotDisk);
		string filePathSnapshot = GetFilePathSnapshot(snapshotDisk.GetFileName());
		FileInfo fileInfo = new FileInfo(filePathSnapshot);
		DeleteSnapshotCache(fileInfo);
		FileUtils.DeleteFile(filePathSnapshot);
	}

	public override bool SnapshotExists(string snapshotName)
	{
		return m_SnapshotCollectionDisk.FindSnapshot(snapshotName) != null;
	}

	public override void RenameSnapshot(Snapshot snapshot, string newName)
	{
		if (SnapshotExists(newName))
		{
			Snapshot snapshot2 = m_SnapshotCollectionDisk.FindSnapshot(newName);
			DeleteSnapshot(snapshot2);
		}
		Snapshot.MetaData value = snapshot.m_Meta.Value;
		SetSnapshotMetadata(snapshot, Snapshot.MetaData.Empty);
		SnapshotDisk obj = snapshot as SnapshotDisk;
		string filePathSnapshot = GetFilePathSnapshot(obj.GetFileName());
		snapshot.m_Name.Value = newName;
		snapshot.techData.Name = newName;
		obj.snapName = GetFilePathSnapshot(newName);
		snapshot.UniqueID = newName;
		string filePathSnapshot2 = GetFilePathSnapshot(obj.GetFileName());
		FileUtils.Rename(filePathSnapshot, filePathSnapshot2);
		FileInfo fileInfo = new FileInfo(filePathSnapshot);
		DeleteSnapshotCache(fileInfo);
		SetSnapshotMetadata(snapshot, value);
		FileInfo fileInfo2 = new FileInfo(filePathSnapshot2);
		if (!TryCreateSnapshotCache(fileInfo2))
		{
			d.LogError("SnapshotServiceDesktop.RenameSnapshot - Could not create snapshot cache, aborting. " + fileInfo2.FullName);
		}
	}

	public override bool CheckSnapshotExists(string filePath)
	{
		return File.Exists(GetFilePathSnapshot(filePath));
	}

	public Texture2D LoadThumbnail(FileInfo fileInfo)
	{
		if (!IsSnapshotCacheValid(fileInfo))
		{
			if (!TryCreateSnapshotCache(fileInfo))
			{
				d.LogErrorFormat("SnapshotServiceDesktop.LoadThumbnail - Snapshot had invalid tech data. Aborting cache generation and loading thumbnail {0}", fileInfo.FullName);
				return null;
			}
			d.LogWarningFormat("SnapshotServiceDesktop.LoadThumbnail - Snapshot had invalid cache. Recovering from this during game play incurs a severe performance penalty. {0}", fileInfo.FullName);
		}
		return FileUtils.LoadTexture(GetFilePathCachedThumbnail(fileInfo));
	}

	public bool IsSnapshotCacheValid(FileInfo fileInfo)
	{
		string filePathCachedThumbnail = GetFilePathCachedThumbnail(fileInfo);
		string filePathCachedMetaData = GetFilePathCachedMetaData(fileInfo);
		if (File.Exists(filePathCachedThumbnail) && File.Exists(filePathCachedMetaData))
		{
			return true;
		}
		return false;
	}

	public bool TryCreateSnapshotCache(FileInfo fileInfo)
	{
		bool result = true;
		string filePathCachedThumbnail = GetFilePathCachedThumbnail(fileInfo);
		string filePathCachedMetaData = GetFilePathCachedMetaData(fileInfo);
		if (!File.Exists(filePathCachedThumbnail) || !File.Exists(filePathCachedMetaData))
		{
			Texture2D texture2D = FileUtils.LoadTexture(fileInfo.FullName);
			if (ManScreenshot.TryDecodeSnapshotRender(texture2D, out var techSnapshotData, fileInfo.FullName))
			{
				fileInfo.Refresh();
				if (!TryCreateSnapshotCache(fileInfo, texture2D, techSnapshotData))
				{
					result = false;
				}
			}
			else
			{
				d.LogError("SnapshotServiceDesktop.TryCreateSnapshotCache - could not create cache due to invalid snapshot data " + fileInfo.Name);
				result = false;
			}
			if (texture2D != null)
			{
				UnityEngine.Object.Destroy(texture2D);
			}
		}
		return result;
	}

	public bool TryCreateSnapshotCache(FileInfo fileInfo, Texture2D snapshotRender, TechData.SerializedSnapshotData techSnapshotData)
	{
		bool result = true;
		string filePathCachedThumbnail = GetFilePathCachedThumbnail(fileInfo);
		string filePathCachedMetaData = GetFilePathCachedMetaData(fileInfo);
		if (!File.Exists(filePathCachedThumbnail) || !File.Exists(filePathCachedMetaData))
		{
			Texture2D texture2D = ManScreenshot.RenderThumbnail(snapshotRender, 296, 185);
			FileUtils.SaveTexture(texture2D, filePathCachedThumbnail);
			if (!TrySaveTechDataCache(techSnapshotData, filePathCachedMetaData))
			{
				result = false;
			}
			UnityEngine.Object.Destroy(texture2D);
		}
		return result;
	}

	public void DeleteSnapshotCache(FileInfo fileInfo)
	{
		string filePathCachedThumbnail = GetFilePathCachedThumbnail(fileInfo);
		string filePathCachedMetaData = GetFilePathCachedMetaData(fileInfo);
		try
		{
			if (File.Exists(filePathCachedThumbnail))
			{
				File.Delete(filePathCachedThumbnail);
			}
			if (File.Exists(filePathCachedMetaData))
			{
				File.Delete(filePathCachedMetaData);
			}
		}
		catch (Exception ex)
		{
			d.LogError("Failed to delete snapshot cache error: " + ex);
		}
	}

	public bool TrySaveTechDataCache(TechData.SerializedSnapshotData techSnapshotData, string path)
	{
		bool result = true;
		if (!ManSaveGame.SaveObject(new TechDataCache
		{
			version = 1,
			techSnapshotData = techSnapshotData
		}, path))
		{
			result = false;
		}
		return result;
	}

	public bool TryLoadTechDataCache(string path, out TechData techData)
	{
		bool result = true;
		TechDataCache objectToLoad = default(TechDataCache);
		ManSaveGame.LoadObject(ref objectToLoad, path);
		techData = objectToLoad.techSnapshotData.CreateTechData();
		if (techData == null)
		{
			result = false;
		}
		return result;
	}

	public string GetFilePathCachedThumbnail(FileInfo fileInfo)
	{
		return GetFilePathCached(fileInfo, ".png");
	}

	public static string GetFilePathCachedMetaData(FileInfo fileInfo)
	{
		return GetFilePathCached(fileInfo, ".tdc");
	}

	public static string GetFilePathSnapshot(string techName)
	{
		return GetFilePath("Snapshots", techName, incrementCount: false, ".png");
	}

	public static string GetFilePathCached(FileInfo fileInfo, string suffix)
	{
		double totalSeconds = fileInfo.LastWriteTimeUtc.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
		string fileName = Path.GetFileNameWithoutExtension(fileInfo.Name) + "_" + totalSeconds;
		return GetFilePath("Cache", fileName, incrementCount: false, suffix);
	}

	public static string GetFilePath(string relativePath, string fileName, bool incrementCount, string suffix)
	{
		string text = ManSaveGame.GetSaveDataFolder() + "/" + relativePath;
		Directory.CreateDirectory(text);
		string path = Path.Combine(text, fileName + suffix);
		path = Path.GetFullPath(path);
		if (incrementCount)
		{
			int num = 0;
			do
			{
				string text2 = fileName + ((num < 1) ? "" : num.ToString());
				path = Path.Combine(text, text2 + suffix);
				num++;
			}
			while (File.Exists(path));
		}
		return path;
	}

	public string GetNextAvailableSnapshotName(string techName)
	{
		return ManSaveGame.GetNextAvailableFileName(techName, GetFilePathSnapshot);
	}

	public static TechData Debug_LoadTechData(string snapshotFilepath, bool forceLoadFromSource = false)
	{
		if (!File.Exists(snapshotFilepath))
		{
			d.LogError("SnapshotServiceDesktop.LoadTechData - trying to load tech data from an invalid file path: " + snapshotFilepath);
		}
		TechData result = null;
		string filePathCachedMetaData = GetFilePathCachedMetaData(new FileInfo(snapshotFilepath));
		if (!forceLoadFromSource && File.Exists(filePathCachedMetaData))
		{
			TechDataCache objectToLoad = default(TechDataCache);
			ManSaveGame.LoadObject(ref objectToLoad, filePathCachedMetaData);
			result = objectToLoad.techSnapshotData.CreateTechData();
		}
		else
		{
			Texture2D texture2D = FileUtils.LoadTexture(snapshotFilepath);
			if (ManScreenshot.TryDecodeSnapshotRender(texture2D, out var techSnapshotData, snapshotFilepath))
			{
				result = techSnapshotData.CreateTechData();
			}
			if (texture2D != null)
			{
				UnityEngine.Object.Destroy(texture2D);
			}
		}
		return result;
	}

	private SnapshotDisk GetSnapshotFromCache(FileInfo fileInfo)
	{
		if (!IsSnapshotCacheValid(fileInfo))
		{
			if (!TryCreateSnapshotCache(fileInfo))
			{
				d.LogErrorFormat("SnapshotServiceDesktop.GetSnapshotFromCache - Snapshot had invalid tech data. Aborting cache retrieval. {0}", fileInfo.FullName);
				return null;
			}
			d.LogWarningFormat("SnapshotServiceDesktop.GetSnapshotFromCache - Found invalid cache. Updating this during gameplay is expensive. {0}", fileInfo.FullName);
		}
		string filePathCachedMetaData = GetFilePathCachedMetaData(fileInfo);
		TechDataCache objectToLoad = default(TechDataCache);
		bool num = ManSaveGame.LoadObject(ref objectToLoad, filePathCachedMetaData);
		SnapshotDisk snapshotDisk = null;
		if (num)
		{
			snapshotDisk = new SnapshotDisk();
			snapshotDisk.snapName = fileInfo.FullName;
			snapshotDisk.techData = objectToLoad.techSnapshotData.CreateTechData();
			snapshotDisk.UniqueID = fileInfo.FullName;
			snapshotDisk.DateCreated = fileInfo.CreationTime;
			if (SupportsMetadata())
			{
				ApplyCachedMetadataToSnapshot(snapshotDisk);
			}
		}
		else
		{
			d.LogError("Failed to load tech data cache from file: " + filePathCachedMetaData);
		}
		return snapshotDisk;
	}

	private void OnFileChanged(object sender, FileSystemEventArgs e)
	{
		lock (m_FileWatcherLock)
		{
			FileWatcherChange item = new FileWatcherChange
			{
				filePath = e.FullPath,
				changeType = e.ChangeType
			};
			m_FileWatcherChangeList.Enqueue(item);
		}
	}

	private void Update()
	{
		lock (m_FileWatcherLock)
		{
			while (m_FileWatcherChangeList.Count > 0)
			{
				FileWatcherChange fileWatcherChange = m_FileWatcherChangeList.Dequeue();
				FileInfo fileInfo = new FileInfo(fileWatcherChange.filePath);
				bool flag = false;
				if ((fileWatcherChange.changeType & WatcherChangeTypes.Changed) == WatcherChangeTypes.Changed)
				{
					flag = (File.Exists(fileWatcherChange.filePath) ? true : false);
				}
				else if ((fileWatcherChange.changeType & WatcherChangeTypes.Created) == WatcherChangeTypes.Created)
				{
					flag = true;
				}
				else if ((fileWatcherChange.changeType & WatcherChangeTypes.Deleted) == WatcherChangeTypes.Deleted)
				{
					flag = false;
				}
				if (flag)
				{
					if (!IsSnapshotCacheValid(fileInfo))
					{
						if (TryCreateSnapshotCache(fileInfo))
						{
							SnapshotDisk snapshotFromCache = GetSnapshotFromCache(fileInfo);
							m_SnapshotCollectionDisk.AddOrReplaceSnapshot(snapshotFromCache);
						}
						else
						{
							d.LogError("SnapshotServiceDesktop.Update - could not create snapshot cache for file likely copied on the file system. " + fileInfo.FullName);
						}
					}
					else if (m_SnapshotCollectionDisk.FindSnapshot(Path.GetFileNameWithoutExtension(fileInfo.Name)) == null)
					{
						SnapshotDisk snapshotFromCache2 = GetSnapshotFromCache(fileInfo);
						m_SnapshotCollectionDisk.AddOrReplaceSnapshot(snapshotFromCache2);
					}
				}
				else
				{
					SnapshotDisk snapshotDisk = m_SnapshotCollectionDisk.FindSnapshot(Path.GetFileNameWithoutExtension(fileInfo.Name));
					if (snapshotDisk != null)
					{
						m_SnapshotCollectionDisk.RemoveSnapshot(snapshotDisk);
					}
				}
			}
		}
	}

	public void PrioritiseSnapshotLoadData(bool prioritise)
	{
	}
}
