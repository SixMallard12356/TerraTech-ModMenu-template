using System;
using System.Collections.Generic;
using Binding;
using MonsterLove.StateMachine;
using Payload.UI.Commands;
using Payload.UI.Commands.Steam;
using Steamworks;
using UnityEngine;

namespace Snapshots;

public class SnapshotServiceSteam : MonoBehaviour, ISnapshotService
{
	public enum States
	{
		Nil,
		LoadPage,
		LoadItems,
		Complete
	}

	private struct SteamPendingItemDownload
	{
		public SteamDownloadItemData downloadItemData;

		public CommandOperation<SteamDownloadItemData> getItemDataOp;
	}

	private SnapshotCollectionSteam m_SnapshotCollection = new SnapshotCollectionSteam();

	private Bindable<ManSnapshots.QueryStatus> m_QueryStatus = new Bindable<ManSnapshots.QueryStatus>(ManSnapshots.QueryStatus.Nil);

	private SteamMultiPageQueryHelper m_GetSubscribedSteamTechList;

	private SteamMultiPageQueryHelper m_SteamGetFavouriteTechIDsList;

	private CommandOperation<SteamUploadData> m_FavouritesUploadOp;

	private Queue<SteamPendingItemDownload> m_PendingTechDataDownloads = new Queue<SteamPendingItemDownload>();

	private const int kMaxConcurrentItemDownloadOps = 10;

	private List<CommandOperation<SteamDownloadItemData>> m_CurrentlyWorkingItemOps = new List<CommandOperation<SteamDownloadItemData>>(10);

	private Dictionary<ulong, Snapshot.MetaData> m_SnapshotUIDMetadataLookup = new Dictionary<ulong, Snapshot.MetaData>();

	private StateMachine<States> m_FSM;

	public SnapshotCollectionSteam SnapshotCollection => m_SnapshotCollection;

	public Bindable<ManSnapshots.QueryStatus> QueryStatus => m_QueryStatus;

	public void Load()
	{
		m_FSM.ChangeState(States.LoadPage);
	}

	public void Pause()
	{
		base.gameObject.SetActive(value: false);
	}

	public void Resume()
	{
		base.gameObject.SetActive(value: true);
	}

	public bool SupportsRenameAndDelete()
	{
		return false;
	}

	public void DeleteSnapshot(Snapshot snapshot)
	{
		throw new NotImplementedException();
	}

	public bool SnapshotExists(string snapshotName)
	{
		throw new NotImplementedException();
	}

	public void RenameSnapshot(Snapshot snapshot, string newName)
	{
		throw new NotImplementedException();
	}

	public bool SupportsFavourites()
	{
		return true;
	}

	public void SetSnapshotFavourite(Snapshot snapshot, bool setAsFavourite)
	{
		if (m_FavouritesUploadOp == null)
		{
			m_FavouritesUploadOp = new CommandOperation<SteamUploadData>();
			m_FavouritesUploadOp.Add(new SteamSetFavouriteCommand());
			m_FavouritesUploadOp.Completed.Subscribe(OnFavouriteUpdateComplete);
			m_FavouritesUploadOp.Cancelled.Subscribe(OnFavouriteUpdateCancelled);
		}
		Snapshot.MetaData value = new Snapshot.MetaData(snapshot.m_Meta.Value);
		value.IsFavourite = setAsFavourite;
		snapshot.m_Meta.Value = value;
		SteamUploadData data = SteamUploadData.Create(snapshot as SnapshotSteam);
		data.m_SteamItem.m_Favourite = setAsFavourite;
		m_FavouritesUploadOp.Execute(data);
	}

	public bool SupportsFolders()
	{
		return false;
	}

	public void SetSnapshotMetadata(Snapshot snapshot, Snapshot.MetaData metadata)
	{
		throw new NotImplementedException();
	}

	public void SetSnapshotFolder(Snapshot snapshot, string folderName)
	{
		throw new NotImplementedException();
	}

	public void ApplyCachedMetadataToSnapshot(Snapshot snapshot)
	{
		SnapshotSteam snapshotSteam = snapshot as SnapshotSteam;
		snapshot.m_Meta.Value = (m_SnapshotUIDMetadataLookup.TryGetValue(snapshotSteam.m_SteamID, out var value) ? value : new Snapshot.MetaData(snapshotSteam.m_SteamID));
	}

	private void LoadPage_Enter()
	{
		m_SnapshotCollection.Clear();
		m_QueryStatus.Value = ManSnapshots.QueryStatus.Requesting;
		m_FSM.ChangeState(States.LoadItems);
	}

	private void LoadItems_Enter()
	{
		if (m_GetSubscribedSteamTechList == null)
		{
			m_GetSubscribedSteamTechList = new SteamMultiPageQueryHelper();
		}
		m_GetSubscribedSteamTechList.OnQueryPageComplete.Subscribe(OnFetchTechListPageComplete);
		m_PendingTechDataDownloads.Clear();
		m_GetSubscribedSteamTechList.StartQuery();
		if (m_SteamGetFavouriteTechIDsList == null)
		{
			m_SteamGetFavouriteTechIDsList = new SteamMultiPageQueryHelper(EUserUGCList.k_EUserUGCList_Favorited, returnOnlyIDs: true);
		}
		m_SteamGetFavouriteTechIDsList.OnQueryPageComplete.Subscribe(OnLoadFavouritesPageComplete);
		m_SteamGetFavouriteTechIDsList.StartQuery();
	}

	private void LoadItems_Update()
	{
		for (int num = m_CurrentlyWorkingItemOps.Count - 1; num >= 0; num--)
		{
			if (!m_CurrentlyWorkingItemOps[num].IsRunning)
			{
				m_CurrentlyWorkingItemOps.RemoveAt(num);
			}
		}
		if (m_CurrentlyWorkingItemOps.Count < 10 && m_PendingTechDataDownloads.Count > 0)
		{
			SteamPendingItemDownload steamPendingItemDownload = m_PendingTechDataDownloads.Dequeue();
			steamPendingItemDownload.getItemDataOp.Execute(steamPendingItemDownload.downloadItemData);
			m_CurrentlyWorkingItemOps.Add(steamPendingItemDownload.getItemDataOp);
		}
		if (!m_GetSubscribedSteamTechList.IsBusy && !m_SteamGetFavouriteTechIDsList.IsBusy && m_PendingTechDataDownloads.Count <= 0 && m_CurrentlyWorkingItemOps.Count <= 0)
		{
			m_FSM.ChangeState(States.Complete);
		}
	}

	private void LoadItems_Exit()
	{
		m_GetSubscribedSteamTechList.OnQueryPageComplete.Unsubscribe(OnFetchTechListPageComplete);
		m_SteamGetFavouriteTechIDsList.OnQueryPageComplete.Unsubscribe(OnLoadFavouritesPageComplete);
		m_GetSubscribedSteamTechList.Cancel();
		m_SteamGetFavouriteTechIDsList.Cancel();
	}

	private void Complete_Enter()
	{
		QueryStatus.Value = ManSnapshots.QueryStatus.Done;
	}

	private void OnFetchTechListPageComplete(SteamDownloadData data)
	{
		if (data.HasAnyItems)
		{
			for (int i = 0; i < data.m_Items.Count; i++)
			{
				LoadSteamItemData(data.m_Items[i]);
			}
		}
	}

	private void OnLoadFavouritesPageComplete(SteamDownloadData data)
	{
		m_SnapshotUIDMetadataLookup.Clear();
		if (data.HasAnyItems)
		{
			for (int i = 0; i < data.m_Items.Count; i++)
			{
				ulong num = (ulong)data.m_Items[i].m_Details.m_nPublishedFileId;
				Snapshot.MetaData value = new Snapshot.MetaData(num);
				value.IsFavourite = true;
				m_SnapshotUIDMetadataLookup.Add(num, value);
			}
		}
		UpdateFavouritedSnapshots();
	}

	private void UpdateFavouritedSnapshots()
	{
		for (int i = 0; i < m_SnapshotCollection.Snapshots.Count; i++)
		{
			ApplyCachedMetadataToSnapshot(m_SnapshotCollection.Snapshots[i]);
		}
	}

	private void LoadSteamItemData(SteamDownloadItemData steamItemData)
	{
		CommandOperation<SteamDownloadItemData> commandOperation = new CommandOperation<SteamDownloadItemData>();
		commandOperation.AddConditional(SteamConditions.CheckItemNeedsDownload, new SteamItemDownloadCommand());
		commandOperation.AddConditional(SteamConditions.CheckWaitingForDownload, new SteamItemWaitForDownloadCommand());
		commandOperation.Add(new SteamItemGetUserName());
		commandOperation.Add(new SteamItemGetDataFile());
		commandOperation.Add(new SteamLoadMetaDataCommand());
		commandOperation.Add(new SteamLoadPreviewImageCommand());
		commandOperation.Add(new SteamItemParseSnapshot());
		commandOperation.Completed.Subscribe(delegate(SteamDownloadItemData d)
		{
			ApplyCachedMetadataToSnapshot(d.m_Snaphsot);
			m_SnapshotCollection.AddSnapshot(d.m_Snaphsot);
		});
		m_PendingTechDataDownloads.Enqueue(new SteamPendingItemDownload
		{
			downloadItemData = steamItemData,
			getItemDataOp = commandOperation
		});
	}

	private SnapshotSteam FindSnapshot(PublishedFileId_t steamItemID)
	{
		SnapshotSteam result = null;
		for (int i = 0; i < m_SnapshotCollection.Snapshots.Count; i++)
		{
			SnapshotSteam snapshotSteam = m_SnapshotCollection.Snapshots[i];
			if ((ulong)steamItemID == snapshotSteam.m_SteamID)
			{
				result = snapshotSteam;
				break;
			}
		}
		return result;
	}

	private void SetFavourited(SnapshotSteam snapshot, bool state)
	{
		if (snapshot != null)
		{
			Snapshot.MetaData value = new Snapshot.MetaData(snapshot.m_Meta.Value);
			value.IsFavourite = state;
			snapshot.m_Meta.Value = value;
			if (state)
			{
				m_SnapshotUIDMetadataLookup.Add(snapshot.m_SteamID, snapshot.m_Meta.Value);
			}
			else
			{
				m_SnapshotUIDMetadataLookup.Remove(snapshot.m_SteamID);
			}
		}
	}

	private void OnFavouriteUpdateComplete(SteamUploadData uploadData)
	{
		SnapshotSteam snapshot = FindSnapshot(uploadData.m_SteamItem.m_PublishedFileID);
		SetFavourited(snapshot, uploadData.m_SteamItem.m_Favourite);
	}

	private void OnFavouriteUpdateCancelled(SteamUploadData uploadData)
	{
		SnapshotSteam snapshot = FindSnapshot(uploadData.m_SteamItem.m_PublishedFileID);
		SetFavourited(snapshot, !uploadData.m_SteamItem.m_Favourite);
	}

	private void Awake()
	{
		m_FSM = StateMachine<States>.Initialize(this, States.Nil);
	}
}
