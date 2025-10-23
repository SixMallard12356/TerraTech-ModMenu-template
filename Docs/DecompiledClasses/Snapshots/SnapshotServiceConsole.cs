#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snapshots;

public class SnapshotServiceConsole : SnapshotServiceDisk
{
	private struct TechDataCache
	{
		public const int kCacheDataVersionNr = 2;

		public int version;

		public DateTime creationDate;

		public TechData.SerializedSnapshotData techSnapshotData;
	}

	private const int k_ThumbnailWidth = 144;

	private const int k_ThumbnailHeight = 90;

	public override IEnumerator UpdateSnapshotCacheOnStartup()
	{
		d.Log("[SnapshotServiceDisk.UpdateSnapshotCacheOnStartUp]");
		m_SnapshotCollectionDisk.RemoveAllSnapshots();
		m_QueryStatus.Value = ManSnapshots.QueryStatus.Requesting;
		m_SnapshotCollectionDisk.SortOnAddEnabled = false;
		List<string> list = SaveDataConsoles.FindAllFilesInDirectory("Snapshots");
		for (int i = 0; i < list.Count; i++)
		{
			string filename = list[i];
			LoadSnapshot(filename);
		}
		m_SnapshotCollectionDisk.SortOnAddEnabled = true;
		m_QueryStatus.Value = ManSnapshots.QueryStatus.Done;
		yield return null;
	}

	public override void SaveSnapshotRender(TechData techData, Texture2D snapshotRender, string snapshotName, Action<bool> saveResultCallback)
	{
		d.Log("[SnapshotServiceConsole.SaveSnapshotRender]: " + techData.Name + ", snapshotName: " + snapshotName);
		d.Assert(techData != null, "ASSERT - techData is NULL!");
		d.Assert(!string.IsNullOrEmpty(snapshotName), "ASSERT - snapshotName is NULL/empty!");
		TechData.SerializedSnapshotData serializedSnapshotData = new TechData.SerializedSnapshotData(techData);
		SnapshotDisk snapshot = new SnapshotDisk();
		snapshot.snapName = snapshotName;
		snapshot.techData = serializedSnapshotData.CreateTechData();
		snapshot.UniqueID = snapshotName;
		snapshot.DateCreated = DateTime.Now;
		snapshot.m_Meta.Value = new Snapshot.MetaData(snapshot.UniqueID);
		SaveSnapshot(snapshot, serializedSnapshotData, snapshotRender, delegate(bool success, byte[] data)
		{
			if (success)
			{
				m_SnapshotCollectionDisk.AddOrReplaceSnapshot(snapshot);
			}
			saveResultCallback?.Invoke(success);
		});
	}

	public override IntVector2 GetPreferredImageSize()
	{
		return new IntVector2(144, 90);
	}

	public override bool EmbedSnapshotsInPNGs()
	{
		return false;
	}

	public override int GetMaxSnapshotCount()
	{
		if (SKU.SwitchUI)
		{
			return 12;
		}
		return int.MaxValue;
	}

	public override void DeleteSnapshot(Snapshot snapshot)
	{
		SnapshotDisk obj = snapshot as SnapshotDisk;
		d.Assert(obj != null, "DeleteSnapshot - Passed in snapshot was not of type SnapshotDisk !");
		string fileName = obj.GetFileName();
		DeleteSnapshot(fileName);
		RemoveSnapshot_CONSOLES_ONLY(fileName);
	}

	public void RemoveSnapshot_CONSOLES_ONLY(string fileName)
	{
		d.Log("[SnapshotServiceConsole] RemoveSnapshot_CONSOLES_ONLY: Request to remove SnapshotName=" + fileName);
		if (m_SnapshotCollectionDisk != null)
		{
			SnapshotDisk snapshotDisk = m_SnapshotCollectionDisk.FindSnapshot(fileName);
			if (snapshotDisk != null)
			{
				d.Log("[SnapshotServiceConsole] RemoveSnapshot_CONSOLES_ONLY: Removing SnapshotName=" + fileName);
				m_SnapshotCollectionDisk.RemoveSnapshot(snapshotDisk);
			}
		}
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
		SnapshotDisk snapshotDisk = snapshot as SnapshotDisk;
		string snapName = snapshotDisk.snapName;
		snapshot.m_Name.Value = newName;
		snapshot.techData.Name = newName;
		snapshotDisk.snapName = newName;
		snapshot.UniqueID = newName;
		TechData.SerializedSnapshotData serializedSnapshotData = new TechData.SerializedSnapshotData(snapshot.techData);
		SaveSnapshot(snapshotDisk, serializedSnapshotData, snapshot.image, null);
		DeleteSnapshot(snapName);
		SetSnapshotMetadata(snapshot, value);
	}

	public override bool CheckSnapshotExists(string filePath)
	{
		return SaveDataConsoles.DataExists(filePath, "Snapshots");
	}

	private void LoadSnapshot(string filename)
	{
		try
		{
			d.Log("[SnapshotServiceConsole.UpdateSnapshotCacheOnStartUp] - snapshot: " + filename);
			string snapshotName = filename;
			SaveDataConsoles.LoadData(snapshotName, "Snapshots", delegate(bool success, byte[] bytes)
			{
				if (!success)
				{
					d.LogError("SnapshotServiceConsole.UpdateSnapshotCacheOnStartUp - snapshot is missing: " + snapshotName);
					DeleteSnapshot(snapshotName);
				}
				else if (bytes != null)
				{
					TechDataCache objectToLoad = default(TechDataCache);
					ManSaveGame.LoadObjectFromBytes(ref objectToLoad, bytes);
					SnapshotDisk snapshotDisk = new SnapshotDisk
					{
						snapName = snapshotName,
						techData = objectToLoad.techSnapshotData.CreateTechData(),
						UniqueID = snapshotName,
						DateCreated = objectToLoad.creationDate
					};
					if (SupportsMetadata())
					{
						ApplyCachedMetadataToSnapshot(snapshotDisk);
					}
					if (snapshotDisk != null && snapshotDisk.techData != null && snapshotDisk.techData.m_BlockSpecs != null && snapshotDisk.techData.m_BlockSpecs.Count > 0)
					{
						d.Log("[SnapshotServiceConsole.UpdateSnapshotCacheOnStartUp] - adding to SnapshotCollectionDisk snapshot: " + filename);
						m_SnapshotCollectionDisk.AddSnapshot(snapshotDisk);
						SaveDataConsoles.LoadData(snapshotName, "Cache", delegate(bool cacheSuccess, byte[] cacheBytes)
						{
							if (!cacheSuccess)
							{
								d.LogError("SnapshotServiceConsole.UpdateSnapshotCacheOnStartUp - snapshot cache is missing: " + snapshotName);
								DeleteSnapshot(snapshotName);
							}
						});
					}
					else
					{
						d.LogError("SnapshotServiceConsole.UpdateSnapshotCacheOnStartUp - snapshot cache contains invalid techdata, deleting and skipping. " + snapshotName);
						DeleteSnapshot(snapshotName);
					}
				}
			});
		}
		catch (Exception ex)
		{
			d.LogError("[SnapshotServiceConsole.UpdateSnapshotCacheOnStartUp] - Exception Occurred:" + ex.ToString());
		}
	}

	private void SaveSnapshot(SnapshotDisk snapshot, TechData.SerializedSnapshotData serializedSnapshotData, Texture2D snapshotImage, Action<bool, byte[]> onDataSaved)
	{
		d.Assert(snapshotImage != null, "Trying to save snapshot Render " + snapshot.snapName + " with null texture!");
		SaveSnapshotTexture(snapshot.snapName, snapshotImage);
		TechDataCache obj = new TechDataCache
		{
			version = 2,
			creationDate = DateTime.Now,
			techSnapshotData = serializedSnapshotData
		};
		int initialCapacitySizeHint = 524288;
		byte[] array = ManSaveGame.SaveObjectToBytes(obj, initialCapacitySizeHint);
		if (array != null)
		{
			d.Log("SnapshotServiceConsole Save:" + snapshot.snapName + " ActualSize=" + array.Length);
			SaveDataConsoles.SaveData(snapshot.snapName, "Snapshots", array, array.Length, 4194304, onDataSaved);
		}
	}

	private void SaveSnapshotTexture(string snapshotName, Texture2D snapshotRender)
	{
		if (snapshotRender.width > 144 || snapshotRender.height > 90)
		{
			Texture2D texture2D = null;
			RenderTexture active = RenderTexture.active;
			RenderTexture temporary = RenderTexture.GetTemporary(144, 90, 0);
			try
			{
				d.Log($"[SnapshotServiceConsole] SaveSnapshotTexture: reducing from {snapshotRender.width}x{snapshotRender.height} to {144}x{90}");
				float num = (float)snapshotRender.width / (float)snapshotRender.height;
				float num2 = 1.6f;
				Vector2 one = Vector2.one;
				if (num < num2)
				{
					one.y = one.x * num / num2;
				}
				else
				{
					one.x = one.y * num2 / num;
				}
				Graphics.Blit(snapshotRender, temporary, one, Vector2.one / 2f - one / 2f);
				RenderTexture.active = temporary;
				texture2D = new Texture2D(144, 90, TextureFormat.RGB24, mipChain: false);
				texture2D.ReadPixels(new Rect(0f, 0f, texture2D.width, texture2D.height), 0, 0);
				byte[] array = texture2D.EncodeToPNG();
				d.Log("[SnapshotServiceConsole] SaveTexture:" + snapshotName + " ActualSize=" + array.Length);
				SaveDataConsoles.SaveData(snapshotName, "Cache", array, array.Length, 4194304, null);
				return;
			}
			finally
			{
				RenderTexture.ReleaseTemporary(temporary);
				if (texture2D != null)
				{
					UnityEngine.Object.DestroyImmediate(texture2D);
				}
				RenderTexture.active = active;
			}
		}
		byte[] array2 = snapshotRender.EncodeToPNG();
		d.Log("[SnapshotServiceConsole] SaveTexture:" + snapshotName + " ActualSize=" + array2.Length);
		SaveDataConsoles.SaveData(snapshotName, "Cache", array2, array2.Length, 4194304, null);
	}

	private void DeleteSnapshot(string snapShotName)
	{
		SaveDataConsoles.DeleteData(snapShotName, "Snapshots", ManGameMode.GameType.Attract, null);
		SaveDataConsoles.DeleteData(snapShotName, "Cache", ManGameMode.GameType.Attract, null);
	}

	private void OnNewFileFound(string fileName, string dirName)
	{
		if (dirName == "Snapshots")
		{
			LoadSnapshot(fileName);
		}
	}

	private void Awake()
	{
		m_SnapshotCollectionDisk = new SnapshotCollectionDisk();
	}
}
