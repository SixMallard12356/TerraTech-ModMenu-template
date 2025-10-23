#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using DevCommands;
using Payload.UI.Commands;
using Payload.UI.Commands.Steam;
using Steamworks;
using TerraTech.Network;
using UnityEngine;

public class ManMods : Singleton.Manager<ManMods>, Mode.IManagerModeEvents
{
	private struct ModFailInfo
	{
		public string modName;

		public string modUID;

		public ModFailReason failReason;

		public string context;

		public ModFailInfo(string modName, string modUID, ModFailReason failReason, string context = null)
		{
			this.modName = modName;
			this.modUID = modUID;
			this.failReason = failReason;
			this.context = context ?? string.Empty;
		}

		public override string ToString()
		{
			return $"Mod: '{modName}' ({modUID}) \t\t{failReason}: {context}";
		}
	}

	private enum ModFailReason
	{
		InvalidBundle,
		InvalidBundleContents,
		ErrorLoadingScripts,
		DuplicateID,
		InvalidSkin,
		InvalidBlock,
		ScriptError
	}

	[SerializeField]
	public ReferenceList m_ProjectileReferences;

	[SerializeField]
	public ReferenceList m_CasingReferences;

	[SerializeField]
	public Transform m_DefaultBlockExplosion;

	[SerializeField]
	private Material GCTrackTexture;

	[SerializeField]
	private Material GSOTrackTexture;

	public EventNoParams ModSessionLoadCompleteEvent;

	public EventNoParams BlocksModifiedEvent;

	private Dictionary<string, ModContainer> m_Mods = new Dictionary<string, ModContainer>();

	private List<PublishedFileId_t> m_WaitingOnDownloads = new List<PublishedFileId_t>();

	private bool m_WaitingOnWorkshopCheck;

	private CommandOperation<SteamDownloadData> m_SteamQuerySubscribedOp;

	private const float kTimeForModLoadingPerFrame = 0.004166667f;

	private Queue<string> m_PendingLoads = new Queue<string>();

	private string m_CurrentlyLoading;

	private IEnumerator<float> m_CurrentLoadOperation;

	private bool m_ReloadAllPending;

	private bool m_ReparseAllPending;

	private bool m_JSONChangesDetected;

	private List<ModFailInfo> m_ModsThatEncounteredIssues = new List<ModFailInfo>();

	private const int k_FIRST_MODDED_CORP_ID = 16;

	public const int k_FIRST_MODDED_BLOCK_ID = 1000000;

	private ModSessionInfo m_CurrentSession = ModSessionInfo.VanillaSession;

	private ModSessionInfo m_CurrentLobbySession;

	private ModSessionInfo m_RequestedSession;

	private bool m_LoadingRequestedSessionInProgress;

	private bool m_AutoAddModsToAuthoritativeSessions = true;

	private Dictionary<string, int> m_BlockIDReverseLookup = new Dictionary<string, int>();

	private Dictionary<string, int> m_CorpIDReverseLookup = new Dictionary<string, int>();

	private List<ModBase> m_ActiveModScripts = new List<ModBase>();

	private Dictionary<int, string> m_BlockNames = new Dictionary<int, string>();

	private Dictionary<int, string> m_BlockDescriptions = new Dictionary<int, string>();

	private Tank m_DebugTech;

	private bool m_Inited;

	private FileSystemWatcher m_FileWatcher;

	private bool hasTriedSPEFabThisSession;

	public static string LocalModsDirectory => "LocalMods";

	public bool ShouldReadFromRawJSON { get; private set; }

	public bool IsSettingUp
	{
		get
		{
			if (!IsPollingWorkshop() && !HasPendingLoads())
			{
				return HasPendingSessionSwitch();
			}
			return true;
		}
	}

	private void LogToUser(string s)
	{
		d.LogError(s);
	}

	public bool IsPollingWorkshop()
	{
		if (m_WaitingOnDownloads.Count <= 0)
		{
			return m_WaitingOnWorkshopCheck;
		}
		return true;
	}

	public bool ModExists(string modId)
	{
		return m_Mods.ContainsKey(modId);
	}

	public ModContainer FindMod(string modId)
	{
		ModContainer value = null;
		if (!m_Mods.TryGetValue(modId, out value))
		{
			d.Log("Could not find mod with ID " + modId + ".");
		}
		return value;
	}

	public ModContainer FindMod(ModdedAsset asset)
	{
		foreach (ModContainer value in m_Mods.Values)
		{
			if (value.FindAsset<ModdedAsset>(asset.name, errorIfMissing: false) == asset)
			{
				return value;
			}
		}
		return null;
	}

	private void CheckForInstalledMods()
	{
		string argument = CommandLineReader.GetArgument("+custom_mod_list");
		if (argument != null)
		{
			d.Log("[Mods] Found custom mod list " + argument);
			string[] array = argument.Split(',');
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Trim('[', ']').Split(':');
				if (array3.Length == 2)
				{
					PublishedFileId_t publishedFileId_t = new PublishedFileId_t(ulong.Parse(array3[1]));
					m_WaitingOnDownloads.Add(publishedFileId_t);
					LoadWorkshopData(new SteamDownloadItemData
					{
						m_Details = new SteamUGCDetails_t
						{
							m_nPublishedFileId = publishedFileId_t
						}
					}, remote: true);
				}
			}
			d.Log($"[Mods] Loaded {array.Length} mods from custom list");
		}
		else
		{
			CheckForLocalMods();
			CheckForSteamWorkshopMods();
		}
		float unscaledTime = Time.unscaledTime;
		do
		{
			UpdateModLoading();
			if (Time.unscaledTime - unscaledTime > 120f)
			{
				d.LogWarning($"Mod loading timed out at 120s with {m_WaitingOnDownloads.Count} Steam downloads pending and {m_PendingLoads.Count} load operations pending");
				break;
			}
		}
		while (m_WaitingOnDownloads.Count > 0 && m_PendingLoads.Count > 0);
	}

	private void CheckForSteamWorkshopMods()
	{
		if (Singleton.Manager<ManSteamworks>.inst.Workshop.Enabled)
		{
			m_WaitingOnWorkshopCheck = true;
			SteamDownloadData data = SteamDownloadData.Create(SteamItemCategory.Mods);
			m_SteamQuerySubscribedOp.Execute(data);
		}
	}

	private void OnSteamModsFetchComplete(SteamDownloadData data)
	{
		m_WaitingOnWorkshopCheck = false;
		if (data.HasAnyItems)
		{
			for (int i = 0; i < data.m_Items.Count; i++)
			{
				SteamDownloadItemData item = data.m_Items[i];
				m_WaitingOnDownloads.Add(item.m_Details.m_nPublishedFileId);
				LoadWorkshopData(item, remote: false);
			}
		}
	}

	public void WorkshopSubscriptionUpdate(RemoteStoragePublishedFileUnsubscribed_t item, bool subscribed)
	{
	}

	private void SendWorkshopRequestsForMissingMods(ModSessionInfo session)
	{
		if (!SKU.IsSteam)
		{
			return;
		}
		foreach (KeyValuePair<string, ulong> mod in session.Mods)
		{
			string key = mod.Key;
			if (m_Mods.ContainsKey(key))
			{
				if (!m_Mods[key].Local)
				{
					d.Log("[Mods] Prepping modded MP session, found we already have " + key + " installed");
				}
				continue;
			}
			PublishedFileId_t publishedFileId_t = new PublishedFileId_t(mod.Value);
			if (publishedFileId_t != PublishedFileId_t.Invalid)
			{
				d.Log($"[Mods] Prepping modded MP session, could not find mod {key} pre-installed. Fetching workshop item {publishedFileId_t}");
				m_WaitingOnDownloads.Add(publishedFileId_t);
				LoadWorkshopData(new SteamDownloadItemData
				{
					m_Details = new SteamUGCDetails_t
					{
						m_nPublishedFileId = publishedFileId_t
					}
				}, remote: true);
			}
			else
			{
				d.LogWarning("[Mods] Prepping modded MP session, server seems to have sent us some bad workshop IDs. Are they sending us local mod installs?");
			}
		}
	}

	private void LoadWorkshopData(SteamDownloadItemData item, bool remote)
	{
		CommandOperation<SteamDownloadItemData> commandOperation = new CommandOperation<SteamDownloadItemData>();
		commandOperation.AddConditional(SteamConditions.CheckItemNeedsDownload, new SteamItemDownloadCommand());
		commandOperation.AddConditional(SteamConditions.CheckWaitingForDownload, new SteamItemWaitForDownloadCommand());
		commandOperation.Add(new SteamItemGetDataFile());
		commandOperation.Add(new SteamLoadPreviewImageCommand());
		commandOperation.Cancelled.Subscribe(delegate(SteamDownloadItemData result)
		{
			d.LogError($"[Mods] Download file {result.m_Details.m_nPublishedFileId} from workshop failed.");
			m_WaitingOnDownloads.Remove(result.m_Details.m_nPublishedFileId);
			Singleton.Manager<ManNetworkLobby>.inst.LeaveLobby();
			Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
		});
		commandOperation.Completed.Subscribe(delegate(SteamDownloadItemData result)
		{
			string text = result.m_FileInfo.Name;
			if (text.EndsWith("_bundle"))
			{
				text = text.Substring(0, text.Length - "_bundle".Length);
			}
			else
			{
				d.LogError("[Mods] Mod asset " + text + " doesn't end with _bundle");
			}
			m_WaitingOnDownloads.Remove(result.m_Details.m_nPublishedFileId);
			ModContainer modContainer = new ModContainer(text, result.m_FileInfo.FullName, isLocal: false, result.m_Details.m_nPublishedFileId.ToString())
			{
				IsRemote = remote
			};
			if (modContainer.HasValidID)
			{
				if (!m_Mods.ContainsKey(modContainer.ModID))
				{
					m_Mods.Add(modContainer.ModID, modContainer);
					RequestModLoad(modContainer.ModID);
				}
				else
				{
					ModContainer modContainer2 = m_Mods[modContainer.ModID];
					if (modContainer2.Local)
					{
						d.LogWarning("[Mods] Skipping registering Workshop mod with ID " + modContainer.ModID + " in folder " + modContainer.AssetBundlePath + ", because we already have a local mod with the same ID from folder " + modContainer2.AssetBundlePath);
					}
					else
					{
						d.LogError("[Mods] Failed to register Workshop mod with ID " + modContainer.ModID + " in folder " + modContainer.AssetBundlePath + ", because we already have a mod with the same ID from folder " + modContainer2.AssetBundlePath);
					}
					HandleModLoadingFailed(modContainer, ModFailReason.DuplicateID, "Dupliucate mod path:" + modContainer2.AssetBundlePath);
				}
			}
			else
			{
				d.LogError("[Mods] Created mod container " + text + ", but the modID was invalid");
			}
		});
		commandOperation.Execute(item);
	}

	private void CheckForLocalMods()
	{
		if (Directory.Exists(LocalModsDirectory))
		{
			d.Log("[Mods] Searching for mods in " + LocalModsDirectory);
			string[] directories = Directory.GetDirectories(LocalModsDirectory);
			foreach (string text in directories)
			{
				string text2 = text.Substring(LocalModsDirectory.Length + 1);
				d.Log("[Mods] Found mod in " + text + ". Resolved name as " + text2);
				ModContainer modContainer = new ModContainer(text2, LocalModsDirectory + "/" + text2 + "/" + text2 + "_bundle", isLocal: true, "local:/" + text2);
				if (modContainer.HasValidID)
				{
					if (!m_Mods.ContainsKey(modContainer.ModID))
					{
						m_Mods.Add(modContainer.ModID, modContainer);
						RequestModLoad(modContainer.ModID);
						continue;
					}
					d.LogError("[Mods] Failed to register mod with ID " + modContainer.ModID + " from folder " + text2 + ", because we already have a mod with the same ID from folder " + m_Mods[modContainer.ModID].AssetBundlePath);
				}
				else
				{
					d.LogError("[Mods] Created mod container " + text2 + ", but it did not correctly parse an ID");
				}
			}
		}
		else
		{
			d.Log("[Mods] Could not find local mods directory at " + new DirectoryInfo(LocalModsDirectory).FullName);
		}
	}

	public string GetCurrentlyLoadingName()
	{
		return m_CurrentlyLoading;
	}

	public bool HasPendingLoads()
	{
		if (m_CurrentlyLoading.NullOrEmpty())
		{
			return m_PendingLoads.Count > 0;
		}
		return true;
	}

	public bool IsModLoaded(string modId)
	{
		if (ModExists(modId))
		{
			return m_Mods[modId].IsLoaded;
		}
		return false;
	}

	public float GetCurrentlyLoadingProgress()
	{
		if (m_CurrentLoadOperation == null)
		{
			return 0f;
		}
		return m_CurrentLoadOperation.Current;
	}

	public void OnJSONUpdated(object sender, FileSystemEventArgs e)
	{
		m_JSONChangesDetected = true;
	}

	[DevCommand(Name = "Mods.ReloadJson", Access = Access.Public, Users = User.Any)]
	public static void RequestReparseAllJsons()
	{
		TankBlock[] array = UnityEngine.Object.FindObjectsOfType<TankBlock>();
		foreach (TankBlock tankBlock in array)
		{
			if (Singleton.Manager<ManMods>.inst.IsModdedBlock(tankBlock.BlockType))
			{
				if (tankBlock.tank != null)
				{
					tankBlock.tank.blockman.Detach(tankBlock, allowHeadlessTech: false, rootTransfer: true, propagate: true);
				}
				tankBlock.transform.Recycle();
			}
		}
		Singleton.Manager<ManMods>.inst.ShouldReadFromRawJSON = true;
		Singleton.Manager<ManMods>.inst.m_ReparseAllPending = true;
	}

	public void RequestRestartGame(ModSessionInfo modList, TTNetworkID lobbyID)
	{
		Process process = new Process();
		process.StartInfo.FileName = Process.GetCurrentProcess().StartInfo.FileName;
		process.StartInfo.Arguments = Process.GetCurrentProcess().StartInfo.Arguments + " +connect_lobby " + lobbyID.m_NetworkID + " +custom_mod_list " + GetModsInSession(modList);
		process.Start();
		Application.Quit();
	}

	[DevCommand(Name = "Mods.ReloadAll", Access = Access.Public, Users = User.Any)]
	public static void RequestReloadAllMods()
	{
		Singleton.Manager<ManMods>.inst.m_ReloadAllPending = true;
		Singleton.Manager<ManUI>.inst.PopScreen(showPrev: false);
		Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
	}

	private void CheckReloadAllMods()
	{
		if (!m_ReloadAllPending)
		{
			return;
		}
		AssetBundle.UnloadAllAssetBundles(unloadAllObjects: false);
		foreach (ModContainer value in m_Mods.Values)
		{
			value.OnReloadRequested();
			RequestModLoad(value.ModID);
		}
		m_ReloadAllPending = false;
	}

	private void CheckReparseAllJsons()
	{
		if (m_JSONChangesDetected)
		{
			UIScreenNotifications uIScreenNotifications = (UIScreenNotifications)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen);
			string notification = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 48));
			uIScreenNotifications.Set(notification, delegate
			{
				Singleton.Manager<ManUI>.inst.RemovePopup();
				RequestReparseAllJsons();
			}, delegate
			{
				Singleton.Manager<ManUI>.inst.RemovePopup();
			}, Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29), Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30));
			uIScreenNotifications.SetUseNewInputHandler(useNewInputHandler: true);
			Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
			m_JSONChangesDetected = false;
		}
		if (!m_ReparseAllPending)
		{
			return;
		}
		TankBlock[] array = UnityEngine.Object.FindObjectsOfType<TankBlock>();
		foreach (TankBlock tankBlock in array)
		{
			if (IsModdedBlock(tankBlock.BlockType) && tankBlock.gameObject.activeSelf)
			{
				d.Log("[Mods] Json reparse paused, waiting for " + tankBlock.name + " and others to recycle");
				return;
			}
		}
		d.Log("[Mods] Json reparse ready to go. All blocks destroyed.");
		m_ReparseAllPending = false;
		DoReparseAll();
	}

	private bool RequestModLoad(string modId)
	{
		if (m_Mods.TryGetValue(modId, out var value))
		{
			if (value.IsLoaded)
			{
				d.Log("[Mods] Found mod " + modId + " already loaded.");
				return true;
			}
			d.Log("[Mods] Found mod " + modId + " not yet loaded. Will wait for it to load.");
			m_PendingLoads.Enqueue(modId);
			return false;
		}
		d.LogWarning("[Mods] Did not find mod " + modId + ". Do we need to do a SteamWorkshop pull first?");
		return false;
	}

	private void UpdateModLoading()
	{
		if (m_CurrentlyLoading.NullOrEmpty() && m_PendingLoads.Count > 0)
		{
			string text = (m_CurrentlyLoading = m_PendingLoads.Dequeue());
			m_CurrentLoadOperation = ProcessLoadingMod(m_Mods[text]);
			d.Log($"[Mods] Started loading mod {text}. {m_PendingLoads.Count} mods left in queue");
		}
		if (m_CurrentlyLoading.NullOrEmpty())
		{
			return;
		}
		d.AssertFormat(m_CurrentLoadOperation != null, "[Mods] Somehow started loading mod {0} without a load operation", m_CurrentlyLoading);
		float num = Time.realtimeSinceStartup + 0.004166667f;
		float num2 = 0f;
		while (Time.realtimeSinceStartup < num && num2 < 1f)
		{
			num2 = ((!m_CurrentLoadOperation.MoveNext()) ? 1f : m_CurrentLoadOperation.Current);
		}
		if (!(num2 >= 1f))
		{
			return;
		}
		ModContainer modContainer = m_Mods[m_CurrentlyLoading];
		d.Log("[Mods] Finished pre-loading mod " + m_CurrentlyLoading + " - " + (modContainer.IsLoaded ? "LOADED" : "FAILED"));
		if (!modContainer.IsLoaded)
		{
			m_Mods.Remove(m_CurrentlyLoading);
		}
		m_CurrentlyLoading = null;
		m_CurrentLoadOperation = null;
		if (m_PendingLoads.Count != 0)
		{
			return;
		}
		d.Log("[Mods] Finished pre-loading all mods");
		foreach (KeyValuePair<string, ModContainer> item in m_Mods.ToList())
		{
			ModContainer value = item.Value;
			try
			{
				value.EarlyInit();
			}
			catch (Exception ex)
			{
				d.LogErrorFormat("Error occurred during EarlyInit of mod '{0}'. It will not be available!\n\t{1}", value.ModID, ex);
				HandleModLoadingFailed(value, ModFailReason.ScriptError, value.ModID);
				value.OnLoadFailed();
				m_Mods.Remove(item.Key);
			}
		}
	}

	private IEnumerator<float> ProcessLoadingMod(ModContainer container)
	{
		AssetBundleCreateRequest createRequest = AssetBundle.LoadFromFileAsync(container.AssetBundlePath);
		while (!createRequest.isDone)
		{
			yield return Scale(createRequest.progress, 0f, 0.2f);
		}
		AssetBundle assetBundle = createRequest.assetBundle;
		if (assetBundle == null)
		{
			d.LogError("[Mods] Load AssetBundle failed for mod " + container.ModID);
			HandleModLoadingFailed(container, ModFailReason.InvalidBundle);
			container.OnLoadFailed();
			yield break;
		}
		AssetBundleRequest loadRequest = assetBundle.LoadAssetAsync<ModContents>("Contents.asset");
		while (!loadRequest.isDone)
		{
			yield return Scale(loadRequest.progress, 0.25f, 0.4f);
		}
		ModContents contents = loadRequest.asset as ModContents;
		if (contents == null)
		{
			d.LogError("[Mods] Load AssetBundle Contents.asset failed for mod " + container.ModID);
			HandleModLoadingFailed(container, ModFailReason.InvalidBundleContents);
			container.OnLoadFailed();
			yield break;
		}
		if (contents.m_Corps.Count > 0)
		{
			for (int corpIndex = 0; corpIndex < contents.m_Corps.Count; corpIndex++)
			{
				container.RegisterAsset(contents.m_Corps[corpIndex]);
				yield return Scale(corpIndex / contents.m_Corps.Count, 0.4f, 0.5f);
			}
		}
		if (contents.m_Skins.Count > 0)
		{
			for (int corpIndex = 0; corpIndex < contents.m_Skins.Count; corpIndex++)
			{
				container.RegisterAsset(contents.m_Skins[corpIndex]);
				yield return Scale(corpIndex / contents.m_Skins.Count, 0.5f, 0.75f);
			}
		}
		if (contents.m_Blocks.Count > 0)
		{
			for (int corpIndex = 0; corpIndex < contents.m_Blocks.Count; corpIndex++)
			{
				container.RegisterAsset(contents.m_Blocks[corpIndex]);
				yield return Scale(corpIndex / contents.m_Blocks.Count, 0.6f, 0.8f);
			}
		}
		foreach (FileInfo item in from r in Directory.GetParent(container.AssetBundlePath).EnumerateFiles()
			orderby r.Name
			select r)
		{
			if (item.Extension == ".dll")
			{
				try
				{
					Type[] exportedTypes = Assembly.LoadFile(item.FullName).GetExportedTypes();
					foreach (Type type in exportedTypes)
					{
						if (typeof(ModBase).IsAssignableFrom(type))
						{
							d.AssertFormat(contents.Script == null, "Multiple 'ModBase' classes found inside '{0}'. Type {1} is now replacing type {2}", item.FullName, type.FullName, contents.Script?.GetType().FullName);
							contents.Script = Activator.CreateInstance(type) as ModBase;
						}
					}
				}
				catch (ReflectionTypeLoadException ex)
				{
					d.LogErrorFormat("ReflectionTypeLoadException: Failed to load type (ignoring): {0}\n\t{1}", ex.InnerException, string.Join("\n", ex.LoaderExceptions.Select((Exception ex3) => ex3.ToString())));
					HandleModLoadingFailed(container, ModFailReason.ErrorLoadingScripts, item.FullName);
					container.OnLoadFailed();
					yield break;
				}
				catch (Exception ex2)
				{
					d.LogErrorFormat("Exception loading library '{0}':\n\t{1}", item.FullName, ex2);
					HandleModLoadingFailed(container, ModFailReason.ErrorLoadingScripts, item.FullName);
					container.OnLoadFailed();
					yield break;
				}
			}
			yield return 0.9f;
		}
		container.OnLoadComplete(contents);
		yield return 1f;
	}

	private void HandleModLoadingFailed(ModContainer container, ModFailReason failReason, string context = null)
	{
		m_ModsThatEncounteredIssues.Add(new ModFailInfo(container.ModID, container.PublishedID, failReason, context));
	}

	private void TryReportModsWithIssues()
	{
		if (m_ModsThatEncounteredIssues.Count > 0)
		{
			d.LogWarningFormat("Reporting the following mod-loading issues to the user:{0}{1}", "\n\t- ", string.Join("\n\t- ", m_ModsThatEncounteredIssues));
			string notification = "The following mods had problems during loading and may be partially or completely broken:\n\n\t- " + string.Join("\n\t- ", m_ModsThatEncounteredIssues) + "\n\nYour gameplay quality is likely to be affected as a result.";
			string accept = "Quit Game";
			string decline = "Ignore warning";
			Action accept2 = delegate
			{
				Singleton.Manager<ManUI>.inst.RemovePopup();
				Application.Quit();
			};
			Action decline2 = delegate
			{
				d.LogWarning("User has chosen to ignore the reported mod issues and continued on into game!");
				Singleton.Manager<ManUI>.inst.RemovePopup();
			};
			UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			uIScreenNotifications.Set(notification, accept2, decline2, accept, decline);
			Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
			m_ModsThatEncounteredIssues.Clear();
		}
	}

	private float Scale(float subProgress, float lower, float upper)
	{
		return Mathf.Lerp(lower, upper, subProgress);
	}

	[Conditional("USE_ANALYTICS")]
	private void SendModsInGameAnalyticsEvent()
	{
		List<string> list = new List<string>(m_Mods.Count);
		List<string> list2 = new List<string>(list.Count);
		foreach (KeyValuePair<string, ModContainer> mod in m_Mods)
		{
			ModContainer value = mod.Value;
			if (value?.Contents != null)
			{
				string item = value.Contents.m_WorkshopId.ToString();
				string key = mod.Key;
				list.Add(key);
				list2.Add(item);
			}
		}
		new Dictionary<string, object>
		{
			{ "num_mods", list.Count },
			{
				"mod_names",
				string.Join("|", list)
			},
			{
				"mod_ids",
				string.Join("|", list2)
			}
		};
	}

	public bool IsModdedCorp(FactionSubTypes corp)
	{
		return corp >= (FactionSubTypes)16;
	}

	public bool IsModdedBlock(BlockTypes type, bool includeUnknownOutOfRangeBlocks = false)
	{
		if (type < (BlockTypes)1000000)
		{
			return false;
		}
		if (m_CurrentSession != null && m_CurrentSession.BlockIDs.ContainsKey((int)type))
		{
			return true;
		}
		return includeUnknownOutOfRangeBlocks;
	}

	public IEnumerable<BlockTypes> IterateModdedBlocks()
	{
		foreach (int key in m_BlockNames.Keys)
		{
			yield return (BlockTypes)key;
		}
	}

	public string GetModNameForBlockID(BlockTypes selectedBlock)
	{
		if (m_CurrentSession != null && m_CurrentSession.BlockIDs.TryGetValue((int)selectedBlock, out var value) && ModUtils.SplitCompoundId(value, out var modId, out var _))
		{
			return modId;
		}
		return "Unknown Mod";
	}

	public bool HasPendingSessionSwitch()
	{
		return m_RequestedSession != null;
	}

	public bool IsModActive(string modId)
	{
		return m_CurrentSession.Mods.ContainsKey(modId);
	}

	public int GetNumModsInCurrentSession()
	{
		return m_CurrentSession.Mods.Count;
	}

	public string GetModsInCurrentSession()
	{
		return GetModsInSession(m_CurrentSession);
	}

	public string GetModsInNetworkedSession()
	{
		return GetModsInSession(m_CurrentLobbySession);
	}

	public string GetModsInSession(ModSessionInfo session)
	{
		if (session == null)
		{
			session = m_CurrentSession;
		}
		string text = string.Empty;
		if (session != null)
		{
			foreach (ModContainer item in session)
			{
				if (item != null)
				{
					text = ((text != null) ? (text + ",[" + item.Contents.ModName + ":" + item.Contents.m_WorkshopId.ToString() + "]") : ("[" + item.Contents.ModName + ":" + item.Contents.m_WorkshopId.ToString() + "]"));
				}
			}
		}
		return text;
	}

	public IEnumerable<ModContainer> IterateActiveMods()
	{
		return m_CurrentSession?.GetEnumerator().Iterate();
	}

	public ModdedCorpDefinition GetCorpDefinition(FactionSubTypes corpID, ModSessionInfo session = null)
	{
		if (session == null)
		{
			session = m_CurrentSession;
		}
		if (IsModdedCorp(corpID) && session != null && session.CorpIDs.TryGetValue((int)corpID, out var value))
		{
			return FindModdedAsset<ModdedCorpDefinition>(value);
		}
		return null;
	}

	public FactionSubTypes GetCorpIndex(string corp, ModSessionInfo session = null)
	{
		string[] names = EnumNamesIterator<FactionSubTypes>.Names;
		for (int i = 0; i < names.Length; i++)
		{
			if (corp == names[i])
			{
				return (FactionSubTypes)i;
			}
		}
		if (m_CorpIDReverseLookup.TryGetValue(corp, out var value))
		{
			return (FactionSubTypes)value;
		}
		ModdedCorpDefinition moddedCorpDefinition = FindCorp(corp);
		if (session != null && moddedCorpDefinition != null)
		{
			foreach (KeyValuePair<int, string> corpID in session.CorpIDs)
			{
				if (ModUtils.GetAssetFromCompoundId(corpID.Value) == moddedCorpDefinition.name)
				{
					d.LogError("Found corp " + corp + " in session, but was ommitted from quick lookup. What's up with that?");
					return (FactionSubTypes)corpID.Key;
				}
			}
		}
		return (FactionSubTypes)(-1);
	}

	public void PreLobbyCreated()
	{
		m_CurrentLobbySession = new ModSessionInfo();
		m_CurrentLobbySession.m_Multiplayer = true;
		AutoAddModsToSession(m_CurrentLobbySession);
	}

	private void PreClientStartGame(Lobby lobby)
	{
		m_RequestedSession = m_CurrentLobbySession;
		SendWorkshopRequestsForMissingMods(m_CurrentLobbySession);
	}

	public void WriteLobbySession(BinaryWriter writer)
	{
		bool flag = SKU.SupportsMods && m_CurrentLobbySession.Mods.Count > 0;
		writer.Write(flag);
		if (flag)
		{
			m_CurrentLobbySession.Write(writer);
		}
	}

	public void ReadLobbySession(BinaryReader reader)
	{
		ModSessionInfo modSessionInfo;
		if (reader.ReadBoolean())
		{
			d.Assert(SKU.SupportsMods, "Session is reported to have mods, but the Client does not support them!");
			modSessionInfo = new ModSessionInfo();
			modSessionInfo.Read(reader);
		}
		else
		{
			modSessionInfo = ModSessionInfo.VanillaMPClientSession;
		}
		if (!Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.IsLobbyOwner())
		{
			m_CurrentLobbySession = modSessionInfo;
		}
	}

	public bool CanLoadSaveGame(string modList)
	{
		if (m_Mods != null && modList != null && modList.Length > 0)
		{
			string[] array = modList.Split(',');
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Trim('[', ']').Split(':');
				if (array2.Length != 0 && !m_Mods.ContainsKey(array2[0]))
				{
					return false;
				}
			}
		}
		return true;
	}

	public bool CanLoadSaveGame(ModSessionInfo sessionInfo)
	{
		List<string> missingMods = GetMissingMods(sessionInfo);
		if (missingMods.Count > 0)
		{
			LogToUser("[Mods] Tried to load save but was missing mods");
			foreach (string item in missingMods)
			{
				LogToUser("[Mods] Missing mod: " + item);
			}
			return false;
		}
		return true;
	}

	public List<string> GetMissingMods(ModSessionInfo sessionInfo)
	{
		List<string> list = new List<string>();
		foreach (string key in sessionInfo.Mods.Keys)
		{
			if (!m_Mods.ContainsKey(key))
			{
				list.Add(key);
			}
		}
		return list;
	}

	private void RequestModSession(ModSessionInfo info, bool autoAddMods = false, bool isMultiplayer = false)
	{
		d.Assert(!m_LoadingRequestedSessionInProgress, $"[Mods] Requested session {info} while we were already loading mods for another request");
		d.Assert(m_RequestedSession == null, $"[Mods] Requested session {info} while session {m_RequestedSession} was already requested");
		m_LoadingRequestedSessionInProgress = false;
		m_RequestedSession = info;
		m_AutoAddModsToAuthoritativeSessions = autoAddMods;
		if (m_RequestedSession != null)
		{
			m_RequestedSession.m_Multiplayer = isMultiplayer;
		}
	}

	private void UpdateModSession()
	{
		CheckReparseAllJsons();
		if (m_RequestedSession == null || m_RequestedSession == m_CurrentSession)
		{
			return;
		}
		if (!m_LoadingRequestedSessionInProgress)
		{
			d.Log("[Mods] Switching mod session");
			if (m_AutoAddModsToAuthoritativeSessions && m_RequestedSession.m_Authoritative && Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() != ManGameMode.GameType.Gauntlet && Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() != ManGameMode.GameType.SumoShowdown && Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() != ManGameMode.GameType.RacingChallenge && Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() != ManGameMode.GameType.FlyingChallenge)
			{
				AutoAddModsToSession(m_RequestedSession);
			}
			m_LoadingRequestedSessionInProgress = true;
		}
		if (!m_LoadingRequestedSessionInProgress || HasPendingLoads())
		{
			return;
		}
		m_LoadingRequestedSessionInProgress = false;
		if (m_RequestedSession.m_Authoritative)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Dictionary<string, List<string>> dictionary2 = new Dictionary<string, List<string>>();
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, ModContainer> mod in m_Mods)
			{
				string key = mod.Key;
				if (mod.Value.IsRemote || !m_RequestedSession.Mods.ContainsKey(key))
				{
					continue;
				}
				foreach (ModdedCorpDefinition item in mod.Value.Contents.m_Corps)
				{
					if (!dictionary.ContainsKey(item.name))
					{
						dictionary.Add(item.name, key);
						continue;
					}
					d.LogWarning("Failed to add duplicate corp " + item.name + " from mod " + key + " because we already have one from mod " + dictionary[item.name]);
				}
				foreach (ModdedSkinDefinition skin in mod.Value.Contents.m_Skins)
				{
					if (!dictionary2.ContainsKey(skin.m_Corporation))
					{
						dictionary2[skin.m_Corporation] = new List<string>();
					}
					dictionary2[skin.m_Corporation].Add(ModUtils.CreateCompoundId(key, skin.name));
				}
				foreach (ModdedBlockDefinition block in mod.Value.Contents.m_Blocks)
				{
					list.Add(ModUtils.CreateCompoundId(key, block.name));
				}
			}
			List<string> list2 = new List<string>(dictionary.Count);
			foreach (KeyValuePair<string, string> item2 in dictionary)
			{
				list2.Add(ModUtils.CreateCompoundId(item2.Value, item2.Key));
			}
			AutoAssignIDs(m_RequestedSession, list2, dictionary2, list);
		}
		PurgeModdedContentFromGame(m_CurrentSession);
		if (m_ReloadAllPending)
		{
			d.Log("[Mods] Purged content, but reload pending. Moving to reload step.");
			m_CurrentSession = null;
			CheckReloadAllMods();
		}
		else
		{
			d.Log("[Mods] Purged content, injecting new content.");
			m_CurrentSession = m_RequestedSession;
			m_RequestedSession = null;
			InjectModdedContentIntoGame(m_CurrentSession);
		}
	}

	private void AutoAddModsToSession(ModSessionInfo session)
	{
		if (session != null)
		{
			foreach (KeyValuePair<string, ModContainer> mod in m_Mods)
			{
				string key = mod.Key;
				if (mod.Value.IsLoaded && !session.Mods.ContainsKey(key) && (!session.m_Multiplayer || mod.Value.Contents.m_WorkshopId != PublishedFileId_t.Invalid))
				{
					session.Mods.Add(key, mod.Value.Contents.m_WorkshopId.m_PublishedFileId);
				}
			}
			return;
		}
		d.LogError("[Mods] Called AutoAddModsToSession for null session");
	}

	private void AutoAssignIDs(Dictionary<int, string> existingIDs, List<string> assetsToAssign, int firstModdedID, int cap = int.MaxValue)
	{
		int num = firstModdedID;
		foreach (KeyValuePair<int, string> existingID in existingIDs)
		{
			if (existingID.Key >= num)
			{
				num = existingID.Key + 1;
			}
			if (assetsToAssign.Contains(existingID.Value))
			{
				assetsToAssign.Remove(existingID.Value);
			}
		}
		foreach (string item in assetsToAssign)
		{
			if (num < cap)
			{
				existingIDs.Add(num, item);
				num++;
			}
			else
			{
				d.LogError($"[Mods] Could not add asset {item} because we hit the cap of {cap}");
			}
		}
	}

	private void AutoAssignIDs(ModSessionInfo sessionInfo, List<string> corpsToAssign, Dictionary<string, List<string>> skinsToAssign, List<string> blocksToAssign)
	{
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() != ManGameMode.GameType.Deathmatch)
		{
			AutoAssignIDs(sessionInfo.CorpIDs, corpsToAssign, 16);
		}
		AutoAssignIDs(sessionInfo.BlockIDs, blocksToAssign, 1000000);
		if (sessionInfo.SkinIDsByCorp == null)
		{
			sessionInfo.SkinIDsByCorp = new Dictionary<int, Dictionary<int, string>>();
		}
		if (sessionInfo.SkinIDs != null)
		{
			foreach (KeyValuePair<int, string> skinID in sessionInfo.SkinIDs)
			{
				bool flag = false;
				foreach (KeyValuePair<string, List<string>> item in skinsToAssign)
				{
					foreach (string item2 in item.Value)
					{
						if (item2 == skinID.Value)
						{
							sessionInfo.AddSkinToCorp(skinID.Key, skinID.Value, item.Key);
							flag = true;
							break;
						}
					}
					if (flag)
					{
						break;
					}
				}
				if (!flag)
				{
					d.LogError($"[Mods] Could not update legacy skin ID {skinID.Key}:{skinID.Value} to new session with corp-specific IDs");
				}
			}
			d.LogError("[Mods] Updated legacy skin IDs to corp-specific IDs");
			sessionInfo.SkinIDs = null;
		}
		foreach (KeyValuePair<string, List<string>> item3 in skinsToAssign)
		{
			int corpIndex = (int)GetCorpIndex(item3.Key, sessionInfo);
			if (corpIndex == 0)
			{
				d.LogError("[Mods] Could not find corporation with ID " + item3.Key + ". Adding skins for this corp will fail");
			}
			if (!sessionInfo.SkinIDsByCorp.ContainsKey(corpIndex))
			{
				sessionInfo.SkinIDsByCorp[corpIndex] = new Dictionary<int, string>();
			}
			int firstModdedID = ((corpIndex >= 16) ? 1 : 32);
			AutoAssignIDs(sessionInfo.SkinIDsByCorp[corpIndex], item3.Value, firstModdedID, 255);
		}
	}

	private void InitReferences()
	{
		m_ProjectileReferences.Init();
		m_CasingReferences.Init();
	}

	public int GetNumCustomCorps()
	{
		if (m_CurrentSession != null)
		{
			return m_CurrentSession.CorpIDs.Count;
		}
		return 0;
	}

	public IEnumerable<int> GetCustomCorpIDs()
	{
		if (m_CurrentSession != null)
		{
			return m_CurrentSession.GetAllCorpIDs();
		}
		return null;
	}

	public ModdedCorpDefinition FindCorp(string corpID)
	{
		foreach (ModContainer value in m_Mods.Values)
		{
			if (!value.IsLoaded)
			{
				continue;
			}
			foreach (ModdedCorpDefinition item in value.Contents.m_Corps)
			{
				if (item.m_ShortName == corpID)
				{
					return item;
				}
			}
		}
		return null;
	}

	public ModdedCorpDefinition FindCorp(int corpIndex)
	{
		if (m_CurrentSession != null && m_CurrentSession.CorpIDs.TryGetValue(corpIndex, out var value))
		{
			return FindModdedAsset<ModdedCorpDefinition>(value);
		}
		return null;
	}

	public T FindModdedAsset<T>(string compoundId) where T : ModdedAsset
	{
		ModUtils.SplitCompoundId(compoundId, out var modId, out var assetId);
		ModContainer modContainer = FindMod(modId);
		if (modContainer != null)
		{
			return modContainer.FindAsset<T>(assetId);
		}
		d.Log("[Mods] ModdedAsset " + compoundId + " could not be found because mod did not exist");
		return null;
	}

	private void PurgeModdedContentFromGame(ModSessionInfo oldSessionInfo)
	{
		if (oldSessionInfo == null)
		{
			return;
		}
		d.Log("[Mods] Purging content from old session");
		Singleton.Manager<RecipeManager>.inst.RemoveCustomBlockRecipes();
		BlockUnlockTable blockUnlockTable = Singleton.Manager<ManLicenses>.inst.GetBlockUnlockTable();
		blockUnlockTable.RemoveModdedBlocks();
		foreach (KeyValuePair<int, string> blockID in oldSessionInfo.BlockIDs)
		{
			int key = blockID.Key;
			ModdedBlockDefinition moddedBlockDefinition = FindModdedAsset<ModdedBlockDefinition>(blockID.Value);
			if (moddedBlockDefinition != null)
			{
				moddedBlockDefinition.m_PhysicalPrefab.transform.DeletePool();
			}
			Singleton.Manager<ManSpawn>.inst.RemoveBlockFromDictionary(key);
		}
		blockUnlockTable.RemoveModdedCorps();
		Singleton.Manager<ManLicenses>.inst.GetRewardPoolTable().ClearModdedBlockRewards();
		Singleton.Manager<ManUI>.inst.m_SpriteFetcher.PurgeModSprites();
		m_BlockNames.Clear();
		m_BlockDescriptions.Clear();
		m_BlockIDReverseLookup.Clear();
		Singleton.Manager<ManCustomSkins>.inst.RemoveModdedSkins();
		m_CorpIDReverseLookup.Clear();
		Singleton.Manager<ManPurchases>.inst.ClearCustomCorps();
		Singleton.Manager<ManCustomSkins>.inst.RemoveModdedCorps();
		DeInitModScripts();
		m_ActiveModScripts.Clear();
		BlocksModifiedEvent.Send();
	}

	private void DoReparseAll()
	{
		foreach (KeyValuePair<int, string> blockID in m_CurrentSession.BlockIDs)
		{
			ModdedBlockDefinition moddedBlockDefinition = FindModdedAsset<ModdedBlockDefinition>(blockID.Value);
			if (moddedBlockDefinition != null)
			{
				m_BlockNames.Remove(blockID.Key);
				m_BlockDescriptions.Remove(blockID.Key);
				m_BlockIDReverseLookup.Remove(moddedBlockDefinition.name);
				Singleton.Manager<ManSpawn>.inst.RemoveBlockFromDictionary(blockID.Key);
				moddedBlockDefinition.m_PhysicalPrefab.transform.DeletePool();
			}
		}
		InjectModdedBlocks(m_CurrentSession, forceReparse: true);
	}

	private void InjectModdedContentIntoGame(ModSessionInfo newSessionInfo)
	{
		d.Log("[Mods] Injecting content for new session");
		if (SessionRequiresRestart(out var modsThatNeedToHook))
		{
			if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
			{
				UIScreenNotifications uIScreenNotifications = (UIScreenNotifications)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen);
				string notification = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 49));
				uIScreenNotifications.Set(notification, delegate
				{
					Singleton.Manager<ManUI>.inst.RemovePopup();
					RequestRestartGame(newSessionInfo, Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.ID);
				}, delegate
				{
					Singleton.Manager<ManUI>.inst.RemovePopup();
					Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
				}, Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29), Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30));
				uIScreenNotifications.SetUseNewInputHandler(useNewInputHandler: true);
				Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
				return;
			}
			d.LogError("Some mods failed to early init for singleplayer game?");
			d.LogError(" --------------------------------------------------- ");
			foreach (string item in modsThatNeedToHook)
			{
				d.LogError(item);
			}
			d.LogError(" --------------------------------------------------- ");
			return;
		}
		InitModScripts();
		InjectModdedCorps(newSessionInfo);
		InjectModdedSkins(newSessionInfo);
		Singleton.Manager<ManTechMaterialSwap>.inst.RebuildCorpArrayTextures();
		Dictionary<int, List<ModdedSkinDefinition>> dictionary = new Dictionary<int, List<ModdedSkinDefinition>>();
		foreach (KeyValuePair<int, string> corpID in newSessionInfo.CorpIDs)
		{
			ModdedCorpDefinition moddedCorpDefinition = FindModdedAsset<ModdedCorpDefinition>(corpID.Value);
			if (!(moddedCorpDefinition != null))
			{
				continue;
			}
			List<ModdedSkinDefinition> list = new List<ModdedSkinDefinition>();
			list.Add(moddedCorpDefinition.m_DefaultSkinSlots[0]);
			if (newSessionInfo.SkinIDsByCorp.TryGetValue(corpID.Key, out var value))
			{
				foreach (KeyValuePair<int, string> item2 in value)
				{
					ModdedSkinDefinition moddedSkinDefinition = FindModdedAsset<ModdedSkinDefinition>(item2.Value);
					if (moddedSkinDefinition != null)
					{
						list.Add(moddedSkinDefinition);
					}
				}
			}
			dictionary.Add(corpID.Key, list);
		}
		Singleton.Manager<ManTechMaterialSwap>.inst.BuildCustomCorpArrayTextures(dictionary);
		InjectModdedBlocks(newSessionInfo);
		Singleton.Manager<ManSpawn>.inst.OnDLCLoadComplete();
		ModSessionLoadCompleteEvent.Send();
	}

	private void InjectModdedCorps(ModSessionInfo newSessionInfo)
	{
		if (newSessionInfo.CorpIDs.Count <= 0)
		{
			return;
		}
		Dictionary<int, ModdedCorpDefinition> dictionary = new Dictionary<int, ModdedCorpDefinition>();
		foreach (KeyValuePair<int, string> corpID in newSessionInfo.CorpIDs)
		{
			int key = corpID.Key;
			ModdedCorpDefinition moddedCorpDefinition = FindModdedAsset<ModdedCorpDefinition>(corpID.Value);
			if (moddedCorpDefinition != null)
			{
				dictionary.Add(key, moddedCorpDefinition);
				Singleton.Manager<ManPurchases>.inst.AddCustomCorp(key);
				Singleton.Manager<ManCustomSkins>.inst.AddCorp(key);
				InjectCustomSkinReferences(0, (FactionSubTypes)key, moddedCorpDefinition.m_DefaultSkinSlots[0]);
				m_CorpIDReverseLookup.Add(moddedCorpDefinition.m_ShortName, key);
				d.Log($"[Mods] Injected corp {moddedCorpDefinition.name} at ID {key}");
			}
		}
		Singleton.Manager<ManLicenses>.inst.m_UnlockTable.AddModdedCorps(dictionary);
	}

	private void InjectCustomSkinReferences(int skinID, FactionSubTypes faction, ModdedSkinDefinition skin)
	{
		if (skin != null)
		{
			CorporationSkinInfo corporationSkinInfo = ScriptableObject.CreateInstance<CorporationSkinInfo>();
			corporationSkinInfo.m_Corporation = faction;
			corporationSkinInfo.m_SkinTextureInfo = new SkinTextures
			{
				m_Albedo = skin.m_Albedo,
				m_Emissive = skin.m_Emissive,
				m_Metal = skin.m_Combined,
				m_Variable = skin.m_Variable
			};
			corporationSkinInfo.m_SkinUIInfo = new CorporationSkinUIInfo
			{
				m_PreviewImage = Sprite.Create(skin.m_PreviewImage, new Rect(0f, 0f, skin.m_PreviewImage.width, skin.m_PreviewImage.height), Vector2.zero),
				m_SkinButtonImage = Sprite.Create(skin.m_SkinButtonImage, new Rect(0f, 0f, skin.m_SkinButtonImage.width, skin.m_SkinButtonImage.height), Vector2.zero),
				m_FallbackString = skin.m_SkinDisplayName,
				m_IsModded = true,
				m_SkinLocked = false
			};
			corporationSkinInfo.m_SkinUniqueID = skinID;
			Singleton.Manager<ManCustomSkins>.inst.AddSkinToCorp(corporationSkinInfo, isModdedSkin: true);
			d.Log($"[Mods] Injected skin {skin.name} at ID {skinID} into corp {faction}");
		}
		else
		{
			d.LogError($"[Mods] Failed to inject skin at ID {skinID} into corp {faction}");
		}
	}

	private void InjectModdedSkins(ModSessionInfo newSessionInfo)
	{
		if (newSessionInfo.SkinIDsByCorp.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<int, Dictionary<int, string>> item in newSessionInfo.SkinIDsByCorp)
		{
			_ = item.Key;
			foreach (KeyValuePair<int, string> item2 in item.Value)
			{
				int key = item2.Key;
				ModdedSkinDefinition moddedSkinDefinition = FindModdedAsset<ModdedSkinDefinition>(item2.Value);
				if (moddedSkinDefinition != null)
				{
					moddedSkinDefinition.SetFallbacks();
					bool flag = false;
					if (moddedSkinDefinition.m_Albedo == null)
					{
						d.LogError($"[Mods] Cannot inject skin {moddedSkinDefinition.name} at ID {key}: Albedo texture was not found. Did you set it?");
					}
					else if (moddedSkinDefinition.m_Combined == null)
					{
						d.LogError($"[Mods] Cannot inject skin {moddedSkinDefinition.name} at ID {key}: Combined Metallic/Smoothness texture was not found. Did you set both of them?");
					}
					else if (moddedSkinDefinition.m_Emissive == null)
					{
						d.LogError($"[Mods] Cannot inject skin {moddedSkinDefinition.name} at ID {key}: Emmisive texture was not found. Did you set it?");
					}
					else if (moddedSkinDefinition.m_Variable == null)
					{
						d.LogError($"[Mods] Cannot inject skin {moddedSkinDefinition.name} at ID {key}: Variable texture was not found. Did you set it? How was a fallback not applied??");
					}
					else if (moddedSkinDefinition.m_PreviewImage == null)
					{
						d.LogError($"[Mods] Cannot inject skin {moddedSkinDefinition.name} at ID {key}: Auto-generated preview texture was not found. This implies a problem with the TTModTool exporter.");
					}
					else if (moddedSkinDefinition.m_SkinButtonImage == null)
					{
						d.LogError($"[Mods] Cannot inject skin {moddedSkinDefinition.name} at ID {key}: Skin button texture not found. Did you set it?");
					}
					else
					{
						FactionSubTypes corpIndex = GetCorpIndex(moddedSkinDefinition.m_Corporation);
						if (corpIndex != (FactionSubTypes)(-1))
						{
							InjectCustomSkinReferences(key, corpIndex, moddedSkinDefinition);
							flag = true;
						}
						else
						{
							d.LogError($"[Mods] Cannot inject skin {moddedSkinDefinition.name} at ID {key}: Corp {moddedSkinDefinition.m_Corporation} was not found - is it part of a different mod?");
						}
					}
					if (!flag)
					{
						ModContainer container = null;
						if (ModUtils.SplitCompoundId(item2.Value, out var modId, out var _))
						{
							container = FindMod(modId);
						}
						HandleModLoadingFailed(container, ModFailReason.InvalidSkin, moddedSkinDefinition.name);
					}
				}
				else
				{
					d.Log($"[Mods] Failed to inject skin {item2.Value} at ID {item2.Key}. Did the mod remove a skin?");
				}
			}
		}
	}

	private void InjectModdedBlocks(ModSessionInfo newSessionInfo, bool forceReparse = false)
	{
		if (newSessionInfo.BlockIDs.Count <= 0)
		{
			return;
		}
		Dictionary<int, Dictionary<int, Dictionary<BlockTypes, ModdedBlockDefinition>>> dictionary = new Dictionary<int, Dictionary<int, Dictionary<BlockTypes, ModdedBlockDefinition>>>();
		Dictionary<int, Sprite> dictionary2 = new Dictionary<int, Sprite>(16);
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, string> blockID in newSessionInfo.BlockIDs)
		{
			int key = blockID.Key;
			ModdedBlockDefinition moddedBlockDefinition = FindModdedAsset<ModdedBlockDefinition>(blockID.Value);
			ModContainer modContainer = null;
			if (ModUtils.SplitCompoundId(blockID.Value, out var modId, out var _))
			{
				modContainer = FindMod(modId);
			}
			if (moddedBlockDefinition != null)
			{
				int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Block, key);
				FactionSubTypes corpIndex = GetCorpIndex(moddedBlockDefinition.m_Corporation, newSessionInfo);
				TankBlockTemplate tankBlockTemplate = moddedBlockDefinition.m_PhysicalPrefab;
				bool flag = tankBlockTemplate.GetComponent<Visible>() == null;
				try
				{
					if (flag || forceReparse)
					{
						d.LogFormat("[Mods] Injected block {0} and performed first time setup.", moddedBlockDefinition.name);
						Visible visible = GetOrAddComponent<Visible>(tankBlockTemplate.gameObject);
						Damageable damageable = GetOrAddComponent<Damageable>(tankBlockTemplate.gameObject);
						ModuleDamage moduleDamage = GetOrAddComponent<ModuleDamage>(tankBlockTemplate.gameObject);
						TankBlock tankBlock = GetOrAddComponent<TankBlock>(tankBlockTemplate.gameObject);
						tankBlock.m_BlockCategory = moddedBlockDefinition.m_Category;
						tankBlock.m_BlockRarity = moddedBlockDefinition.m_Rarity;
						tankBlock.m_DefaultMass = Mathf.Clamp(moddedBlockDefinition.m_Mass, 0.0001f, float.MaxValue);
						tankBlock.filledCells = tankBlockTemplate.filledCells.ToArray();
						tankBlock.attachPoints = tankBlockTemplate.attachPoints.ToArray();
						visible.m_ItemType = new ItemTypeInfo(ObjectTypes.Block, key);
						JSONBlockLoader.Load(modContainer, key, moddedBlockDefinition, tankBlock);
						tankBlockTemplate = moddedBlockDefinition.m_PhysicalPrefab;
						tankBlockTemplate.gameObject.SetActive(value: false);
						Visible visible2 = GetOrAddComponent<Visible>(tankBlockTemplate.gameObject);
						damageable = GetOrAddComponent<Damageable>(tankBlockTemplate.gameObject);
						moduleDamage = GetOrAddComponent<ModuleDamage>(tankBlockTemplate.gameObject);
						tankBlock = GetOrAddComponent<TankBlock>(tankBlockTemplate.gameObject);
						visible2.m_ItemType = new ItemTypeInfo(ObjectTypes.Block, key);
						damageable.m_DamageableType = moddedBlockDefinition.m_DamageableType;
						moduleDamage.maxHealth = moddedBlockDefinition.m_MaxHealth;
						if (moduleDamage.deathExplosion == null)
						{
							moduleDamage.deathExplosion = m_DefaultBlockExplosion;
						}
						MeshRenderer[] componentsInChildren = tankBlockTemplate.GetComponentsInChildren<MeshRenderer>();
						foreach (MeshRenderer meshRenderer in componentsInChildren)
						{
							MeshRendererTemplate component = meshRenderer.GetComponent<MeshRendererTemplate>();
							if (component != null)
							{
								meshRenderer.sharedMaterial = GetMaterial((int)corpIndex, component.slot);
								d.AssertFormat(meshRenderer.sharedMaterial != null, "[Mods] Custom block {0} could not load texture. Corp was {1}", moddedBlockDefinition.m_BlockDisplayName, moddedBlockDefinition.m_Corporation);
							}
						}
						tankBlockTemplate.gameObject.name = moddedBlockDefinition.name;
						tankBlockTemplate.gameObject.tag = "Untagged";
						tankBlockTemplate.gameObject.layer = LayerMask.NameToLayer("Tank");
						MeshCollider[] componentsInChildren2 = tankBlock.GetComponentsInChildren<MeshCollider>();
						for (int i = 0; i < componentsInChildren2.Length; i++)
						{
							componentsInChildren2[i].convex = true;
						}
						tankBlock.transform.CreatePool(8);
					}
					else
					{
						tankBlockTemplate.gameObject.GetComponent<Visible>().m_ItemType = new ItemTypeInfo(ObjectTypes.Block, key);
						tankBlockTemplate.transform.CreatePool(8);
					}
					Singleton.Manager<ManSpawn>.inst.AddBlockToDictionary(tankBlockTemplate.gameObject, key);
					Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.SetDescriptor(hashCode, corpIndex);
					if (flag || forceReparse)
					{
						RunBlockSpawnTest(tankBlockTemplate.transform);
					}
				}
				catch (Exception ex)
				{
					d.LogErrorFormat("Error occurred loading block '{0}' in mod '{1}'. It will not be available!\n\t{2}", moddedBlockDefinition?.name, modId, ex);
					tankBlockTemplate = null;
					Singleton.Manager<ManSpawn>.inst.RemoveBlockFromDictionary(key);
					HandleModLoadingFailed(modContainer, ModFailReason.InvalidBlock, moddedBlockDefinition.name);
				}
				if (tankBlockTemplate == null)
				{
					list.Add(key);
					continue;
				}
				m_BlockNames.Add(key, moddedBlockDefinition.m_BlockDisplayName);
				m_BlockDescriptions.Add(key, moddedBlockDefinition.m_BlockDescription);
				m_BlockIDReverseLookup.Add(moddedBlockDefinition.name, key);
				Singleton.Manager<RecipeManager>.inst.RegisterCustomBlockRecipe(key, moddedBlockDefinition.m_Price);
				if (moddedBlockDefinition.m_Icon != null)
				{
					dictionary2[key] = Sprite.Create(moddedBlockDefinition.m_Icon, new Rect(0f, 0f, moddedBlockDefinition.m_Icon.width, moddedBlockDefinition.m_Icon.height), Vector2.zero);
				}
				else
				{
					d.LogError($"Block {moddedBlockDefinition.name} with ID {key} failed to inject because icon was not set");
				}
				if (!dictionary.ContainsKey((int)corpIndex))
				{
					dictionary[(int)corpIndex] = new Dictionary<int, Dictionary<BlockTypes, ModdedBlockDefinition>>();
				}
				Dictionary<int, Dictionary<BlockTypes, ModdedBlockDefinition>> dictionary3 = dictionary[(int)corpIndex];
				if (!dictionary3.ContainsKey(moddedBlockDefinition.m_Grade - 1))
				{
					dictionary3[moddedBlockDefinition.m_Grade - 1] = new Dictionary<BlockTypes, ModdedBlockDefinition>(new BlockTypesComparer());
				}
				dictionary3[moddedBlockDefinition.m_Grade - 1].Add((BlockTypes)key, moddedBlockDefinition);
				JSONBlockLoader.Inject(key, moddedBlockDefinition);
				d.Log($"[Mods] Injected block {moddedBlockDefinition.name} at ID {blockID.Key}");
			}
			else
			{
				list.Add(key);
			}
		}
		CleanupBlockSpawnTestTech();
		foreach (int item in list)
		{
			newSessionInfo.BlockIDs.Remove(item);
		}
		Singleton.Manager<ManUI>.inst.m_SpriteFetcher.SetModSprites(ObjectTypes.Block, dictionary2);
		BlockUnlockTable blockUnlockTable = Singleton.Manager<ManLicenses>.inst.GetBlockUnlockTable();
		blockUnlockTable.RemoveModdedBlocks();
		foreach (KeyValuePair<int, Dictionary<int, Dictionary<BlockTypes, ModdedBlockDefinition>>> item2 in dictionary)
		{
			foreach (KeyValuePair<int, Dictionary<BlockTypes, ModdedBlockDefinition>> item3 in item2.Value)
			{
				blockUnlockTable.AddModdedBlocks(item2.Key, item3.Key, item3.Value);
				if (IsModdedCorp((FactionSubTypes)item2.Key))
				{
					ModdedCorpDefinition corpDefinition = GetCorpDefinition((FactionSubTypes)item2.Key, m_CurrentSession);
					if (corpDefinition.m_RewardCorp != null)
					{
						Singleton.Manager<ManLicenses>.inst.GetRewardPoolTable().AddModdedBlockRewards(item3.Value, item3.Key, GetCorpIndex(corpDefinition.m_RewardCorp));
					}
				}
				else
				{
					Singleton.Manager<ManLicenses>.inst.GetRewardPoolTable().AddModdedBlockRewards(item3.Value, item3.Key, (FactionSubTypes)item2.Key);
				}
			}
		}
		blockUnlockTable.Init();
		BlocksModifiedEvent.Send();
		static T GetOrAddComponent<T>(GameObject gameObject) where T : Component
		{
			T val = gameObject.GetComponent<T>();
			if (val == null)
			{
				val = gameObject.AddComponent<T>();
			}
			return val;
		}
	}

	private void PrepareBlockSpawnTestTech()
	{
		if (m_DebugTech == null)
		{
			Visible.DisableAddToTileOnSpawn = true;
			TrackedVisible trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnEmptyTechRef(-2, Vector3.zero, Quaternion.identity, grounded: false, addToManager: false, "Mod Block test tech");
			Visible.DisableAddToTileOnSpawn = false;
			m_DebugTech = trackedVisible.visible.tank;
		}
	}

	private void CleanupBlockSpawnTestTech()
	{
		if (m_DebugTech != null)
		{
			m_DebugTech.trans.Recycle();
			m_DebugTech = null;
		}
	}

	private void RunBlockSpawnTest(Transform blockTrans)
	{
		PrepareBlockSpawnTestTech();
		Visible.DisableAddToTileOnSpawn = true;
		TankBlock component = blockTrans.Spawn().GetComponent<TankBlock>();
		Visible.DisableAddToTileOnSpawn = false;
		if (m_DebugTech.blockman.AddBlockToTech(component, IntVector3.zero))
		{
			m_DebugTech.blockman.Detach(component, allowHeadlessTech: true, rootTransfer: false, propagate: false);
		}
		Singleton.Manager<ComponentPool>.inst.Recycle(component.trans, worldPosStays: true, recursed: false, forceReturnNow: true);
	}

	public int GetBlockID(string blockName)
	{
		int value = 3;
		if (!m_BlockIDReverseLookup.TryGetValue(blockName, out value))
		{
			d.LogFormat("Failed to find modded blockID for {0}", blockName);
		}
		return value;
	}

	public Material GetMaterial(int corpIndex, TextureSlot slot)
	{
		if (slot == TextureSlot.Main)
		{
			if (!Singleton.Manager<ManTechMaterialSwap>.inst.m_FinalCorpMaterials.TryGetValue(corpIndex, out var value))
			{
				d.LogErrorFormat("Failed to find Main slot texture for corpIndex = {0}.", corpIndex);
			}
			return value;
		}
		switch (corpIndex)
		{
		case 1:
			return GSOTrackTexture;
		case 2:
			return GCTrackTexture;
		default:
		{
			if (m_CurrentSession != null && m_CurrentSession.CorpIDs.TryGetValue(corpIndex, out var value2))
			{
				FindModdedAsset<ModdedCorpDefinition>(value2);
			}
			return null;
		}
		}
	}

	public string FindCorpShortName(FactionSubTypes corp)
	{
		if (m_CurrentSession != null && m_CurrentSession.CorpIDs.TryGetValue((int)corp, out var value))
		{
			ModdedCorpDefinition moddedCorpDefinition = FindModdedAsset<ModdedCorpDefinition>(value);
			if (moddedCorpDefinition != null)
			{
				return moddedCorpDefinition.m_ShortName;
			}
		}
		return "Unknown modded corporation";
	}

	public string FindCorpName(FactionSubTypes corp)
	{
		if (m_CurrentSession != null && m_CurrentSession.CorpIDs.TryGetValue((int)corp, out var value))
		{
			ModdedCorpDefinition moddedCorpDefinition = FindModdedAsset<ModdedCorpDefinition>(value);
			if (moddedCorpDefinition != null)
			{
				return moddedCorpDefinition.m_DisplayName;
			}
		}
		return "Unknown modded corporation";
	}

	public string FindBlockName(int blockID)
	{
		if (m_BlockNames.TryGetValue(blockID, out var value))
		{
			return value;
		}
		return "Unknown Modded Block";
	}

	public string FindBlockDesc(int blockID)
	{
		if (m_BlockDescriptions.TryGetValue(blockID, out var value))
		{
			return value;
		}
		return "Unknown Modded Block";
	}

	private bool SessionRequiresRestart(out List<string> modsThatNeedToHook)
	{
		modsThatNeedToHook = null;
		foreach (string key in m_CurrentSession.Mods.Keys)
		{
			if (m_Mods.TryGetValue(key, out var value) && value != null && !value.InjectedEarlyHooks && value.Script != null && value.Script.HasEarlyInit())
			{
				if (modsThatNeedToHook == null)
				{
					modsThatNeedToHook = new List<string>(m_CurrentSession.Mods.Count);
				}
				modsThatNeedToHook.Add(value.ModID);
			}
		}
		if (modsThatNeedToHook != null)
		{
			return modsThatNeedToHook.Count > 0;
		}
		return false;
	}

	private void InitModScripts()
	{
		foreach (KeyValuePair<string, ModContainer> item in m_Mods.ToList())
		{
			ModContainer value = item.Value;
			try
			{
				if (value.Script != null)
				{
					value.Script.Init();
					m_ActiveModScripts.Add(value.Script);
				}
			}
			catch (Exception ex)
			{
				d.LogErrorFormat("Error occurred initialising script '{0}' in mod '{1}'. It will not be available!\n\t{2}", value.Script?.GetType().FullName, value.ModID, ex);
				HandleModLoadingFailed(value, ModFailReason.ScriptError, value.Script?.GetType().FullName);
				m_Mods.Remove(item.Key);
			}
		}
	}

	private void UpdateModScripts()
	{
		foreach (ModBase activeModScript in m_ActiveModScripts)
		{
			activeModScript.Update();
		}
	}

	private void FixedUpdateModScripts()
	{
		foreach (ModBase activeModScript in m_ActiveModScripts)
		{
			activeModScript.FixedUpdate();
		}
	}

	private void DeInitModScripts()
	{
		foreach (ModBase activeModScript in m_ActiveModScripts)
		{
			activeModScript.DeInit();
		}
	}

	private void Awake()
	{
	}

	private void Start()
	{
		d.Assert(!m_Inited, "[Mods] ManMods inited twice?");
		if (!m_Inited)
		{
			m_Inited = true;
			if (Singleton.Manager<ManSteamworks>.inst.Workshop.Enabled && m_SteamQuerySubscribedOp == null)
			{
				m_SteamQuerySubscribedOp = new CommandOperation<SteamDownloadData>();
				SteamCreateQueryCommand command = new SteamCreateQueryCommand();
				m_SteamQuerySubscribedOp.Add(command);
				m_SteamQuerySubscribedOp.Completed.Subscribe(OnSteamModsFetchComplete);
				m_SteamQuerySubscribedOp.Cancelled.Subscribe(OnSteamModsFetchComplete);
			}
			if (Directory.Exists(LocalModsDirectory))
			{
				m_FileWatcher = new FileSystemWatcher(LocalModsDirectory, "*.json");
				m_FileWatcher.NotifyFilter = NotifyFilters.LastWrite;
				m_FileWatcher.IncludeSubdirectories = true;
				m_FileWatcher.Changed += OnJSONUpdated;
				m_FileWatcher.EnableRaisingEvents = true;
			}
			InitReferences();
			if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem != null)
			{
				Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.PreJoinEvent.Subscribe(PreClientStartGame);
			}
			CheckForInstalledMods();
		}
	}

	private void Update()
	{
		if (!m_Inited)
		{
			return;
		}
		UpdateModLoading();
		if (!IsPollingWorkshop() && !HasPendingLoads())
		{
			UpdateModSession();
			if (!HasPendingSessionSwitch())
			{
				TryReportModsWithIssues();
				UpdateModScripts();
			}
			if (Singleton.Manager<ManGameMode>.inst.IsCurrentModeCampaign() && Singleton.playerTank != null && ManNetwork.IsHost)
			{
				CheckModFabricatorUnlock();
			}
		}
	}

	private void CheckModFabricatorUnlock()
	{
		if (!hasTriedSPEFabThisSession && !Singleton.Manager<ManLicenses>.inst.IsBlockDiscovered(BlockTypes.SPE_Fabricator_222) && GetNumCustomCorps() > 0 && (Singleton.Manager<ManLicenses>.inst.IsBlockDiscovered(BlockTypes.GSOFabricator_322) || Singleton.Manager<ManLicenses>.inst.IsBlockDiscovered(BlockTypes.GCFabricator_432) || Singleton.Manager<ManLicenses>.inst.IsBlockDiscovered(BlockTypes.VENFabricator_322) || Singleton.Manager<ManLicenses>.inst.IsBlockDiscovered(BlockTypes.HE_Fabricator_332) || Singleton.Manager<ManLicenses>.inst.IsBlockDiscovered(BlockTypes.BF_Fabricator_222) || Singleton.Manager<ManLicenses>.inst.IsBlockDiscovered(BlockTypes.EXP_Fabricator_322)))
		{
			hasTriedSPEFabThisSession = true;
			MultiObjectSpawner multiObjectSpawner = new MultiObjectSpawner();
			Crate.Definition crateDef = new Crate.Definition
			{
				m_Contents = new Crate.ItemDefinition[1],
				m_Locked = false
			};
			crateDef.m_Contents[0].m_BlockType = BlockTypes.SPE_Fabricator_222;
			ManSpawn.CrateSpawnParams objectSpawnParams = new ManSpawn.CrateSpawnParams
			{
				m_CrateDef = crateDef,
				m_Rotation = Quaternion.identity,
				m_Name = "ModFabCrate",
				m_CorpType = FactionSubTypes.NULL,
				m_VisibleOnRadar = true
			};
			ManFreeSpace.FreeSpaceParams freeSpaceParams = new ManFreeSpace.FreeSpaceParams
			{
				m_ObjectsToAvoid = ManSpawn.AvoidSceneryVehiclesCrates,
				m_AvoidLandmarks = true,
				m_CircleRadius = Singleton.Manager<ManSpawn>.inst.GetCrateSpawnClearance(),
				m_CenterPosWorld = WorldPosition.FromScenePosition(Singleton.playerTank.trans.position),
				m_CircleIndex = 0,
				m_CameraSpawnConditions = ManSpawn.CameraSpawnConditions.Anywhere,
				m_CheckSafeArea = false,
				m_RejectFunc = null
			};
			multiObjectSpawner.TrySpawn(objectSpawnParams, freeSpaceParams, "ModFabCrate", autoRetry: true);
			Singleton.Manager<ManLicenses>.inst.DiscoverBlock(BlockTypes.SPE_Fabricator_222);
			UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 51);
			Action accept = delegate
			{
				Singleton.Manager<ManUI>.inst.RemovePopup();
				Singleton.Manager<ManUI>.inst.ShowScreenPrompt(ManUI.ScreenType.Options);
			};
			uIScreenNotifications.Set(localisedString, accept, Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29));
			Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
		}
	}

	private void FixedUpdate()
	{
		if (m_Inited && !IsSettingUp)
		{
			FixedUpdateModScripts();
		}
	}

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		if (optionalLoadState != null)
		{
			ModSessionInfo saveData = null;
			if (!optionalLoadState.GetSaveData<ModSessionInfo>(ManSaveGame.SaveDataJSONType.ManMods, out saveData))
			{
				saveData = new ModSessionInfo();
			}
			if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem == null || Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby == null || Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.IsLobbyOwner())
			{
				RequestModSession(saveData, autoAddMods: true);
			}
			else
			{
				d.Assert(Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.IsLobbyOwner(), "[Mods] Why are we loading mode state as a client?!");
			}
		}
		else if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem == null || Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby == null)
		{
			RequestModSession(new ModSessionInfo(), autoAddMods: true);
		}
		else if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.IsLobbyOwner())
		{
			RequestModSession(m_CurrentLobbySession, autoAddMods: true, isMultiplayer: true);
		}
	}

	public void Save(ManSaveGame.State saveState)
	{
		if (m_CurrentSession != null)
		{
			saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManMods, m_CurrentSession);
		}
	}

	public void ModeExit()
	{
	}

	public void OnDestroy()
	{
		if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem != null)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.PreJoinEvent.Unsubscribe(PreClientStartGame);
		}
	}
}
