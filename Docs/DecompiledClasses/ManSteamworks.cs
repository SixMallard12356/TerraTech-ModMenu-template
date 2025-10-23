#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;
using Payload.UI.Commands;
using Payload.UI.Commands.Steam;
using Steamworks;
using UnityEngine;

public class ManSteamworks : Singleton.Manager<ManSteamworks>
{
	[SerializeField]
	private SteamWorkshop m_SteamWorkshop;

	public const string kGameTypeAny = "*";

	public const string kWorkshopURL = "http://steamcommunity.com/app/285920/workshop/";

	public const string kWorkshopItemURL = "https://steamcommunity.com/sharedfiles/filedetails/?id={0}";

	public EventNoParams SteamWorksInitialisedEvent;

	private SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;

	protected Callback<GameOverlayActivated_t> m_GameOverlayActivatedCallback;

	protected Callback<DlcInstalled_t> m_DlcInstalledCallback;

	protected Callback<RemoteStoragePublishedFileSubscribed_t> m_WorkshopSubscriptionCallback;

	protected Callback<RemoteStoragePublishedFileUnsubscribed_t> m_WorkshopUnsubscriptionCallback;

	protected Callback<PersonaStateChange_t> m_PersonaStateChangeCallback;

	private bool m_Inited;

	private AppId_t m_AppId;

	private CGameID m_GameID;

	private AccountID_t m_AccountID;

	private string m_KnownInstalledDlc;

	private static Dictionary<CSteamID, Action<CSteamID, string>> m_PersonaNameRequests = new Dictionary<CSteamID, Action<CSteamID, string>>();

	public bool Inited => m_Inited;

	public AppId_t AppID => m_AppId;

	public CGameID GameID => m_GameID;

	public AccountID_t AccountID => m_AccountID;

	public SteamWorkshop Workshop => m_SteamWorkshop;

	private static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
		d.LogWarning(pchDebugText);
	}

	public void OpenOverlayURL(string url)
	{
		if (!Inited)
		{
			d.LogError("ManSteamworks.OpenSteamUrl - Steam is not initialized");
			return;
		}
		if (string.IsNullOrEmpty(url))
		{
			d.LogError("ManSteamworks.OpenSteamUrl - empty or null url supplied");
			return;
		}
		if (Application.isEditor)
		{
			d.Log("Request to open url in Steam Overlay: " + url);
		}
		SteamFriends.ActivateGameOverlayToWebPage(url);
	}

	public void OpenOverlayStore(AppId_t appID)
	{
		if (!Inited)
		{
			d.LogError("ManSteamworks.OpenOverlayStore - Steam is not initialized");
			return;
		}
		if (Application.isEditor)
		{
			d.Log("Request to open Steam Store: " + appID.ToString());
		}
		SteamFriends.ActivateGameOverlayToStore(appID, EOverlayToStoreFlag.k_EOverlayToStoreFlag_None);
	}

	public string GetWorkshopItemURL(string workshopID)
	{
		return $"https://steamcommunity.com/sharedfiles/filedetails/?id={workshopID}";
	}

	public void RequestPersonaName(CSteamID steamID, Action<CSteamID, string> nameReceivedCallback)
	{
		m_PersonaNameRequests.Add(steamID, nameReceivedCallback);
		if (!SteamFriends.RequestUserInformation(steamID, bRequireNameOnly: true))
		{
			string friendPersonaName = SteamFriends.GetFriendPersonaName(steamID);
			nameReceivedCallback(steamID, friendPersonaName);
			m_PersonaNameRequests.Remove(steamID);
		}
	}

	private void StartSteam()
	{
		if (!Packsize.Test())
		{
			d.LogError("[ManSteamworks] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", this);
		}
		if (!DllCheck.Test())
		{
			d.LogError("[ManSteamworks] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", this);
		}
		try
		{
			_ = AppId_t.Invalid;
			if (SteamAPI.RestartAppIfNecessary(new AppId_t(285920u)))
			{
				Application.Quit();
				return;
			}
		}
		catch (DllNotFoundException ex)
		{
			d.LogError("[ManSteamworks] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n" + ex, this);
			Application.Quit();
			return;
		}
		if (SteamAPI.Init())
		{
			m_Inited = true;
			Init();
			d.Log("[ManSteamworks] Steam api succesfully inited ");
		}
		else
		{
			d.LogError("[ManSteamworks] Failed to init steam");
		}
	}

	private void Init()
	{
		m_GameOverlayActivatedCallback = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
		m_DlcInstalledCallback = Callback<DlcInstalled_t>.Create(OnDlcInstalled);
		m_WorkshopSubscriptionCallback = Callback<RemoteStoragePublishedFileSubscribed_t>.Create(OnWorkshopSubscription);
		m_WorkshopUnsubscriptionCallback = Callback<RemoteStoragePublishedFileUnsubscribed_t>.Create(OnWorkshopUnsubscription);
		m_PersonaStateChangeCallback = Callback<PersonaStateChange_t>.Create(OnPersonaStateChange);
		m_AppId = SteamUtils.GetAppID();
		m_AccountID = SteamUser.GetSteamID().GetAccountID();
		m_GameID = new CGameID(m_AppId);
		if (m_SteamAPIWarningMessageHook == null)
		{
			m_SteamAPIWarningMessageHook = SteamAPIDebugTextHook;
			SteamClient.SetWarningMessageHook(m_SteamAPIWarningMessageHook);
		}
		m_KnownInstalledDlc = GetDlcInstalledString();
		SteamWorksInitialisedEvent.Send();
	}

	private string GetDlcInstalledString()
	{
		int dLCCount = SteamApps.GetDLCCount();
		string text = "";
		for (int i = 0; i < dLCCount; i++)
		{
			SteamApps.BGetDLCDataByIndex(i, out var pAppID, out var _, out var pchName, 128);
			if (SteamApps.BIsDlcInstalled(pAppID))
			{
				text = text + "+" + pchName;
			}
		}
		return text;
	}

	private void CheckForNewDLC()
	{
		string dlcInstalledString = GetDlcInstalledString();
		if (dlcInstalledString != m_KnownInstalledDlc)
		{
			d.Log("[ManSteamworks] OnApplicationFocus DLC changed " + m_KnownInstalledDlc + " => " + dlcInstalledString);
			m_KnownInstalledDlc = dlcInstalledString;
			if ((bool)Singleton.Manager<ManDLC>.inst)
			{
				Singleton.Manager<ManDLC>.inst.PlatformDLCChanged();
			}
		}
	}

	private void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
	{
		Singleton.Manager<ManPauseGame>.inst.OnSystemOverlayActive(pCallback.m_bActive != 0);
		CheckForNewDLC();
	}

	private void OnDlcInstalled(DlcInstalled_t dlc)
	{
		d.Log($"[ManSteamworks] OnDlcInstalled AppID={dlc.m_nAppID}");
		CheckForNewDLC();
	}

	private void OnWorkshopSubscription(RemoteStoragePublishedFileSubscribed_t item)
	{
		new CommandOperation<SteamDownloadItemData>().Add(new SteamLoadMetaDataCommand());
	}

	private void OnWorkshopUnsubscription(RemoteStoragePublishedFileUnsubscribed_t item)
	{
		Singleton.Manager<ManMods>.inst.WorkshopSubscriptionUpdate(item, subscribed: false);
	}

	private void OnPersonaStateChange(PersonaStateChange_t personaStateChange)
	{
		if ((personaStateChange.m_nChangeFlags & EPersonaChange.k_EPersonaChangeName) != 0)
		{
			CSteamID cSteamID = new CSteamID(personaStateChange.m_ulSteamID);
			if (m_PersonaNameRequests.TryGetValue(cSteamID, out var value))
			{
				string friendPersonaName = SteamFriends.GetFriendPersonaName(cSteamID);
				value(cSteamID, friendPersonaName);
				m_PersonaNameRequests.Remove(cSteamID);
			}
		}
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		if (m_Inited && hasFocus)
		{
			CheckForNewDLC();
		}
	}

	private void Awake()
	{
		if (SKU.IsSteam)
		{
			StartSteam();
		}
	}

	private void Update()
	{
		if (m_Inited)
		{
			SteamAPI.RunCallbacks();
		}
	}

	private void OnDestroy()
	{
		if (m_Inited)
		{
			SteamAPI.Shutdown();
		}
	}
}
