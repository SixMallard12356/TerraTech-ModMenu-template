#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using Binding;
using Snapshots;
using UnityEngine;

public class ManSnapshots : Singleton.Manager<ManSnapshots>
{
	public enum QueryStatus
	{
		Nil,
		Requesting,
		Done,
		AuthFailed
	}

	public BindableList<SnapshotLiveData> m_Snapshots = new BindableList<SnapshotLiveData>();

	public Bindable<QueryStatus> m_QueryStatus = new Bindable<QueryStatus>(QueryStatus.Nil);

	public Bindable<bool> m_PlacementAvailable = new Bindable<bool>(value: false);

	public Bindable<bool> m_SwapAvailableInMode = new Bindable<bool>(value: false);

	public Event<TechData, Texture2D, bool> PresetSavedEvent;

	public const string k_SnapshotDirName = "Snapshots";

	public const string k_CacheDirName = "Cache";

	private VMSnapshotPanel.Platform m_CurrentPlatform;

	private SnapshotServiceDesktop m_ServiceDesktop;

	private SnapshotServiceSteam m_ServiceSteam;

	private SnapshotServiceTwitter m_ServiceTwitter;

	private SnapshotServiceConsole m_ServiceConsole;

	private ISnapshotService m_SnapshotService;

	private bool m_CollectionUpdateEnabled;

	private bool m_Dirty;

	private bool m_ValidationDirty;

	private Dictionary<BlockTypes, int> m_PlayerTechBlockCount = new Dictionary<BlockTypes, int>(new BlockTypesComparer());

	private Dictionary<Snapshot, TechDataAvailValidation> m_TechAvailLookup = new Dictionary<Snapshot, TechDataAvailValidation>();

	public BindableList<SnapshotLiveData> SnapshotCollection => m_Snapshots;

	public SnapshotServiceDisk ServiceDisk { get; private set; }

	public SnapshotServiceConsole ServiceDisk_Console => m_ServiceConsole;

	public SnapshotServiceDesktop ServiceDisk_Desktop => m_ServiceDesktop;

	public bool EmbedSnapshotsInPNGs => ServiceDisk.EmbedSnapshotsInPNGs();

	public int MaxDiskSnapshots => ServiceDisk.GetMaxSnapshotCount();

	public void SetCollectionUpdateEnabled(bool enabled)
	{
		m_CollectionUpdateEnabled = enabled;
		UpdateDirtiedSnapshots();
	}

	public void LoadPlatform(VMSnapshotPanel.Platform platform)
	{
		switch (m_CurrentPlatform)
		{
		case VMSnapshotPanel.Platform.Local:
			ServiceDisk.GetSnapshotCollectionDisk().Snapshots.Unbind(OnDiskCollectionChanged);
			break;
		case VMSnapshotPanel.Platform.Twitter:
			m_ServiceTwitter.SnapshotCollection.Snapshots.Unbind(OnTwitterCollectionChanged);
			break;
		case VMSnapshotPanel.Platform.Steam:
			m_ServiceSteam.SnapshotCollection.Snapshots.Unbind(OnSteamCollectionChanged);
			break;
		default:
			d.LogErrorFormat("ManSnapshots.LoadPlatform - not mappaing for platform type {0}", m_CurrentPlatform);
			break;
		case VMSnapshotPanel.Platform.Null:
			break;
		}
		if (m_SnapshotService != null)
		{
			m_SnapshotService.QueryStatus.Unbind(m_QueryStatus);
		}
		m_CurrentPlatform = platform;
		m_TechAvailLookup.Clear();
		m_SnapshotService = null;
		switch (m_CurrentPlatform)
		{
		case VMSnapshotPanel.Platform.Local:
			ServiceDisk.GetSnapshotCollectionDisk().Snapshots.Bind(OnDiskCollectionChanged);
			m_SnapshotService = ServiceDisk;
			break;
		case VMSnapshotPanel.Platform.Twitter:
			m_ServiceTwitter.SnapshotCollection.Snapshots.Bind(OnTwitterCollectionChanged);
			m_ServiceTwitter.Load();
			m_SnapshotService = m_ServiceTwitter;
			break;
		case VMSnapshotPanel.Platform.Steam:
			m_ServiceSteam.SnapshotCollection.Snapshots.Bind(OnSteamCollectionChanged);
			m_ServiceSteam.Load();
			m_SnapshotService = m_ServiceSteam;
			break;
		default:
			d.LogErrorFormat("ManSnapshots.LoadPlatform - not mappaing for platform type {0}", m_CurrentPlatform);
			break;
		case VMSnapshotPanel.Platform.Null:
			break;
		}
		if (m_SnapshotService != null)
		{
			m_SnapshotService.QueryStatus.Bind(m_QueryStatus);
		}
	}

	public IEnumerator UpdateSnapshotCacheOnStartup()
	{
		return ServiceDisk.UpdateSnapshotCacheOnStartup();
	}

	public bool SupportsRenameAndDelete()
	{
		return m_SnapshotService.SupportsRenameAndDelete();
	}

	public bool SupportsFolders()
	{
		return m_SnapshotService.SupportsFolders();
	}

	public void Delete(SnapshotLiveData snapshotData)
	{
		if (m_SnapshotService.SupportsRenameAndDelete())
		{
			m_SnapshotService.DeleteSnapshot(snapshotData.m_Snapshot);
			return;
		}
		d.LogErrorFormat("ManSnapshots.Delete - deletion is not supported for platform {0}", m_CurrentPlatform);
	}

	public void SetFolder(SnapshotLiveData snapshotData, string folderName)
	{
		d.Log($"ManSnapshots.SetFolder {folderName} snapshotData={snapshotData}");
		if (!m_SnapshotService.SupportsFolders())
		{
			d.LogErrorFormat("ManSnapshots.SetFolder - Set folder is not supported for platform {0}", m_CurrentPlatform);
		}
		else
		{
			m_SnapshotService.SetSnapshotFolder(snapshotData.m_Snapshot, folderName);
		}
	}

	public void Rename(SnapshotLiveData snapshotData, string newName)
	{
		d.Log($"ManSnapshots.Rename {newName} snapshotData={snapshotData}");
		d.Assert(!newName.NullOrEmpty(), "Trying to rename snapshot to empty name! Don't allow this (but maybe we should provide user feedback..)");
		if (m_SnapshotService.SupportsRenameAndDelete())
		{
			bool flag = m_SnapshotService.SnapshotExists(newName);
			d.Log($"ManSnapshots.Rename to {newName} already exists = {flag}");
			if (flag)
			{
				UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
				string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, 59);
				localisedString = string.Format(localisedString, newName);
				string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, 60);
				string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 5);
				string notification = localisedString;
				Action accept = delegate
				{
					Singleton.Manager<ManUI>.inst.RemovePopup();
					m_SnapshotService.RenameSnapshot(snapshotData.m_Snapshot, newName);
				};
				string accept2 = localisedString2;
				string decline = localisedString3;
				uIScreenNotifications.Set(notification, accept, Singleton.Manager<ManUI>.inst.RemovePopup, accept2, decline);
				Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
			}
			else
			{
				m_SnapshotService.RenameSnapshot(snapshotData.m_Snapshot, newName);
			}
		}
		else
		{
			d.LogErrorFormat("ManSnapshots.Rename - Renaming is not supported for platform {0}", m_CurrentPlatform);
		}
	}

	public void SaveSnapshotRender(TechData techData, Texture2D snapshotRender, string snapshotName, bool isPlayerTech, Action<bool> saveResultCallback = null)
	{
		ServiceDisk.SaveSnapshotRender(techData, snapshotRender, snapshotName, saveResultCallback);
		PresetSavedEvent.Send(techData, snapshotRender, isPlayerTech);
	}

	public IntVector2 GetDiskSnapshotImageSize()
	{
		return ServiceDisk.GetPreferredImageSize();
	}

	public void DeleteSnapshotRender(Snapshot snapshot, Action OnDelete)
	{
		UIScreenNotifications uIScreenNotifications = (UIScreenNotifications)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen);
		string notification = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 11), snapshot.m_Name);
		uIScreenNotifications.Set(notification, delegate
		{
			ServiceDisk.DeleteSnapshot(snapshot);
			Singleton.Manager<ManUI>.inst.PopScreen();
			OnDelete();
		}, delegate
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
		}, Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29), Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30));
		Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenNotifications);
	}

	public bool SupportsFavourites()
	{
		return m_SnapshotService.SupportsFavourites();
	}

	public void SetSnapshotFavourite(SnapshotLiveData snapshotData, bool favourite)
	{
		if (m_SnapshotService.SupportsFavourites())
		{
			m_SnapshotService.SetSnapshotFavourite(snapshotData.m_Snapshot, favourite);
		}
	}

	public static TechData Debug_LoadTechData(string snapshotFilepath, bool forceLoadFromSource = false)
	{
		return SnapshotServiceDesktop.Debug_LoadTechData(snapshotFilepath, forceLoadFromSource);
	}

	public static string GetFilePathSnapshot(string techName)
	{
		return SnapshotServiceDesktop.GetFilePathSnapshot(techName);
	}

	public void ResetModeAvailability()
	{
		foreach (TechDataAvailValidation value in m_TechAvailLookup.Values)
		{
			value.ResetModeAvailability();
		}
		m_ValidationDirty = true;
	}

	private void UpdateSnapshots()
	{
		switch (m_CurrentPlatform)
		{
		case VMSnapshotPanel.Platform.Local:
			UpdateSnapshotsInternal(ServiceDisk.GetSnapshotCollectionDisk().Snapshots);
			break;
		case VMSnapshotPanel.Platform.Twitter:
			UpdateSnapshotsInternal(m_ServiceTwitter.SnapshotCollection.Snapshots);
			break;
		case VMSnapshotPanel.Platform.Steam:
			UpdateSnapshotsInternal(m_ServiceSteam.SnapshotCollection.Snapshots);
			break;
		default:
			d.LogErrorFormat("ManSnapshots.UpdateSnapshots - not mappaing for platform type {0}", m_CurrentPlatform);
			break;
		case VMSnapshotPanel.Platform.Null:
			break;
		}
	}

	private void UpdateSnapshotsInternal<TSnap>(BindableList<TSnap> list) where TSnap : Snapshot, new()
	{
		for (int i = 0; i < m_Snapshots.Count && i < list.Count; i++)
		{
			m_Snapshots[i] = TransformSnapshot(list[i]);
		}
		for (int j = m_Snapshots.Count; j < list.Count; j++)
		{
			m_Snapshots.Add(TransformSnapshot(list[j]));
		}
		for (int num = m_Snapshots.Count - 1; num >= list.Count; num--)
		{
			m_Snapshots.RemoveAt(num);
		}
		m_Dirty = false;
	}

	private void UpdateSnapshotValidation()
	{
		InventoryMetaData referenceInventory = Singleton.Manager<ManGameMode>.inst.GetReferenceInventory();
		for (int i = 0; i < m_Snapshots.Count; i++)
		{
			if (m_TechAvailLookup.TryGetValue(m_Snapshots[i].m_Snapshot, out var value))
			{
				value.UpdateAvailability(referenceInventory, m_PlayerTechBlockCount);
			}
		}
		m_ValidationDirty = false;
	}

	private void UpdateDirtiedSnapshots()
	{
		if (m_CollectionUpdateEnabled && Singleton.Manager<ManGameMode>.inst.GetModePhase() == ManGameMode.GameState.InGame)
		{
			if (m_Dirty)
			{
				UpdateSnapshots();
				m_ValidationDirty = true;
			}
			if (m_ValidationDirty)
			{
				UpdateSnapshotValidation();
			}
		}
	}

	private SnapshotLiveData TransformSnapshot(Snapshot snapshot)
	{
		SnapshotLiveData result = new SnapshotLiveData
		{
			m_Snapshot = snapshot
		};
		if (!m_TechAvailLookup.TryGetValue(snapshot, out result.m_ValidData))
		{
			result.m_ValidData = new TechDataAvailValidation();
			result.m_ValidData.RecordBlockData(snapshot.techData);
			m_TechAvailLookup.Add(snapshot, result.m_ValidData);
		}
		return result;
	}

	private T CreateService<T>() where T : Component
	{
		GameObject obj = new GameObject();
		obj.transform.parent = base.transform;
		return obj.AddComponent<T>();
	}

	private void OnDiskCollectionChanged(BindableList<SnapshotDisk> list, int index, BindableChange changeType)
	{
		m_Dirty = true;
	}

	private void OnSteamCollectionChanged(BindableList<SnapshotSteam> list, int index, BindableChange changeType)
	{
		m_Dirty = true;
	}

	private void OnTwitterCollectionChanged(BindableList<SnapshotTwitter> list, int index, BindableChange changeType)
	{
		m_Dirty = true;
	}

	private void OnUserChanged(ManProfile.Profile newUser)
	{
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (currentUser != null && currentUser != newUser)
		{
			ServiceDisk.GetSnapshotMetaData(ref currentUser.m_SnapshotMetaData);
		}
		if (newUser != null)
		{
			ServiceDisk.SetSnapshotMetadata(newUser.m_SnapshotMetaData);
		}
	}

	private void OnBeforeProfileSave(ManProfile.Profile user)
	{
		ServiceDisk.GetSnapshotMetaData(ref user.m_SnapshotMetaData);
	}

	private void OnModeSetup(Mode gameMode)
	{
		m_PlacementAvailable.Value = gameMode.CanPlayerPlaceTech();
		m_SwapAvailableInMode.Value = gameMode.CanPlayerSwapTech();
		ResetModeAvailability();
		m_Dirty = true;
	}

	private void OnBlockDiscovered(BlockTypes blockDiscovered)
	{
		m_ValidationDirty = true;
	}

	private void OnMoneyChanged(int money)
	{
		m_ValidationDirty = true;
	}

	private void OnPaletteUnlocked()
	{
		m_ValidationDirty = true;
	}

	private void OnInventoryChanged(BlockTypes blockTypes, int quantity)
	{
		m_ValidationDirty = true;
	}

	private void OnBlockLimitChanged(ManBlockLimiter.CostChangeInfo info)
	{
		if (info.m_TechCategory == ManBlockLimiter.TechCategory.Player)
		{
			m_ValidationDirty = true;
		}
	}

	private void OnPlayerTechChanged(Tank tech, bool isPlayer)
	{
		m_PlayerTechBlockCount.Clear();
		if (tech != null)
		{
			if (isPlayer)
			{
				tech.AttachEvent.Subscribe(OnPlayerBlockAttached);
				tech.DetachEvent.Subscribe(OnPlayerBlockDetached);
				BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tech.blockman.IterateBlocks().GetEnumerator();
				while (enumerator.MoveNext())
				{
					BlockTypes blockType = enumerator.Current.BlockType;
					if (!m_PlayerTechBlockCount.TryGetValue(blockType, out var value))
					{
						value = 0;
					}
					value++;
					m_PlayerTechBlockCount[blockType] = value;
				}
			}
			else
			{
				tech.AttachEvent.Unsubscribe(OnPlayerBlockAttached);
				tech.DetachEvent.Unsubscribe(OnPlayerBlockDetached);
			}
		}
		m_ValidationDirty = true;
	}

	private void OnPlayerBlockAttached(TankBlock block, Tank tech)
	{
		BlockTypes blockType = block.BlockType;
		if (!m_PlayerTechBlockCount.TryGetValue(blockType, out var value))
		{
			value = 0;
		}
		value++;
		m_PlayerTechBlockCount[blockType] = value;
		m_ValidationDirty = true;
	}

	private void OnPlayerBlockDetached(TankBlock block, Tank tech)
	{
		BlockTypes blockType = block.BlockType;
		if (!m_PlayerTechBlockCount.TryGetValue(blockType, out var value))
		{
			d.LogError("ManSnapshots.OnPlayerBlockDetached - missmatch between detached block and cached data!");
		}
		value--;
		if (value <= 0)
		{
			m_PlayerTechBlockCount.Remove(blockType);
		}
		else
		{
			m_PlayerTechBlockCount[blockType] = value;
		}
		m_ValidationDirty = true;
	}

	private void Awake()
	{
		if (SKU.ConsoleUI)
		{
			m_ServiceConsole = CreateService<SnapshotServiceConsole>();
			ServiceDisk = m_ServiceConsole;
		}
		else
		{
			m_ServiceDesktop = CreateService<SnapshotServiceDesktop>();
			ServiceDisk = m_ServiceDesktop;
		}
		if (SKU.IsSteam)
		{
			m_ServiceSteam = CreateService<SnapshotServiceSteam>();
		}
		Singleton.Manager<ManProfile>.inst.OnUserChanged.Subscribe(OnUserChanged);
		Singleton.Manager<ManProfile>.inst.OnBeforeProfileSave.Subscribe(OnBeforeProfileSave);
		Singleton.Manager<ManGameMode>.inst.ModeSetupEvent.Subscribe(OnModeSetup);
		Singleton.Manager<ManLicenses>.inst.BlockDiscoveredEvent.Subscribe(OnBlockDiscovered);
		Singleton.Manager<ManPlayer>.inst.MoneyAmountChanged.Subscribe(OnMoneyChanged);
		Singleton.Manager<ManPlayer>.inst.OnPaletteUnlocked.Subscribe(OnPaletteUnlocked);
		Singleton.Manager<ManPurchases>.inst.OnInventoryChanged.Subscribe(OnInventoryChanged);
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTechChanged);
		Singleton.Manager<ManBlockLimiter>.inst.CostChangedEvent.Subscribe(OnBlockLimitChanged);
	}

	private void Update()
	{
		UpdateDirtiedSnapshots();
	}
}
