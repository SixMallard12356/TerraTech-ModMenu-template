#define IS_PUBLIC_EOS_BUILD
#define UNITY_EDITOR
#define UNITY_ASSERTIONS
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DevCommands;
using Epic.OnlineServices;
using Epic.OnlineServices.Auth;
using Epic.OnlineServices.Connect;
using Epic.OnlineServices.Ecom;
using Epic.OnlineServices.Metrics;
using Epic.OnlineServices.Platform;
using Newtonsoft.Json;
using PlayEveryWare.EpicOnlineServices;
using Steamworks;
using TerraTech.Network;
using UnityEngine;

public class ManEOS : Singleton.Manager<ManEOS>
{
	public enum InitState
	{
		None = 0,
		Platform = 1,
		AuthLoggedIn = 2,
		ConnectLoggedIn = 4
	}

	private struct DLCEntitlementsCache
	{
		public class DateTimeConverter : JsonConverter
		{
			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				if (reader.Value == null)
				{
					return null;
				}
				double value = (double)reader.Value;
				return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(value);
			}

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				double totalSeconds = ((DateTime)value - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
				writer.WriteValue(totalSeconds);
			}

			public override bool CanConvert(Type objectType)
			{
				return objectType == typeof(DateTime);
			}
		}

		public class EpicAccountIdConverter : JsonConverter
		{
			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				if (reader.Value != null)
				{
					return EpicAccountId.FromString((string)reader.Value);
				}
				return null;
			}

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				writer.WriteValue(((EpicAccountId)value).ToString());
			}

			public override bool CanConvert(Type objectType)
			{
				return objectType == typeof(EpicAccountId);
			}
		}

		[JsonConverter(typeof(DateTimeConverter))]
		public DateTime CachedTime;

		[JsonConverter(typeof(EpicAccountIdConverter))]
		public EpicAccountId EGSAccountID;

		public string[] DLCEntitlementIDs;

		public static DLCEntitlementsCache GenerateCacheForCurrentUser(IEnumerable<string> dlcEntitlements)
		{
			return new DLCEntitlementsCache
			{
				CachedTime = DateTime.UtcNow,
				EGSAccountID = Singleton.Manager<ManEOS>.inst.LocalPlayerEpicAccountID,
				DLCEntitlementIDs = dlcEntitlements.ToArray()
			};
		}

		public bool TryGetDLCEntitlements(out string[] entitlements)
		{
			if (Singleton.Manager<ManEOS>.inst.LocalPlayerEpicAccountID != EGSAccountID || DateTime.UtcNow - CachedTime > k_DLC_Entitlements_MaxCacheTime)
			{
				entitlements = new string[0];
				return false;
			}
			entitlements = DLCEntitlementIDs;
			return true;
		}
	}

	public class CrossplayPlatformPrefs
	{
		public bool m_Crossplay_HasUserRequestedActive;
	}

	[SerializeField]
	private EOSManager EOSInterface;

	[SerializeField]
	[Tooltip("Fallback user profile sprite if user supplied sprites are not supported in the SKU x platform config")]
	private Sprite m_DefaultUserSprite;

	private const string kEpicLauncherProtocolActivationBase = "com.epicgames.launcher://";

	private const string kTTAllDLCPath = "store/all-dlc/";

	private const string kTTProductSlug = "terratech-02ab0a";

	public Event<InitState, InitState> InitStateChangedEvent;

	public EventNoParams BeforeLogoutEvent;

	private CrossplayPlatformPrefs m_CrossplayPlatformPrefs = new CrossplayPlatformPrefs();

	private EOSManager.EOSSingleton.EpicLauncherArgs m_EpicArgs;

	private InitState m_InitState;

	private Callback<GetTicketForWebApiResponse_t> m_GetTicketForWebApiResponseCallback;

	private HAuthTicket m_SteamworksAuthTicket = HAuthTicket.Invalid;

	protected string m_SteamworksLoginToken;

	protected string m_SteamworksLoginID;

	private NotifyEventHandle m_ConnectLoginAuthExpirationListenerHandle;

	private NotifyEventHandle m_OnAuthLoginStatusChangeHandle;

	protected EpicAccountId m_EpicAccountID;

	protected ProductUserId m_ProductUserID;

	protected HashSet<string> m_DlcEntitlements;

	private bool m_IsExiting;

	private bool m_IsStartedFromLauncher;

	private bool m_AuthAsGuest;

	private bool m_IsOfflineMode;

	private bool m_FailedToLoadAnyOfflineDLCEntitlements;

	public const bool k_Crossplay_ForceCrossplayActive = true;

	private bool m_Crossplay_HasUserRequestedActive;

	private bool m_Crossplay_Auth_HasLinkedAccounts;

	private ContinuanceToken m_Crossplay_Auth_InvalidUserContinuance;

	private const string kEOS_IdentityProvider_Steam = "terratech_eos";

	private const float k_PeriodicConnectLoginRefreshRate = 30f;

	private const string k_DLC_Entitlements_Key = "eos_cached_entitlements";

	private static readonly TimeSpan k_DLC_Entitlements_MaxCacheTime = new TimeSpan(30, 0, 0, 0, 0);

	private Action<Result> _m_AuthLoginCallback;

	private Action<Result> _m_ConnectLoginCallback;

	private Action<Result> _m_AccountsLinkedCallback;

	private IEnumerator m_ConnectLogInPeriodicRefresher;

	public EpicAccountId LocalPlayerEpicAccountID => m_EpicAccountID;

	public string PlayerIDString { get; private set; }

	public bool HasPlayerID => !PlayerIDString.NullOrEmpty();

	public bool Inited => m_InitState > InitState.None;

	public bool IsConnected => (m_InitState & InitState.ConnectLoggedIn) > InitState.None;

	public bool IsAuthLoggedIn => (m_InitState & InitState.AuthLoggedIn) > InitState.None;

	public bool HasPUID => m_ProductUserID != null;

	public bool IsValidlyMissingAuth
	{
		get
		{
			if (!m_AuthAsGuest)
			{
				return m_IsOfflineMode;
			}
			return true;
		}
	}

	public bool IsOfflineMode => m_IsOfflineMode;

	public bool IsDLCInfoLoaded => m_DlcEntitlements != null;

	public bool FailedToLoadAnyOfflineDLCEntitlements => m_FailedToLoadAnyOfflineDLCEntitlements;

	private string PrefsSavePath => Path.Combine(ManSaveGame.GetSaveDataFolder(), "PlatformNetworkingPrefs.bin");

	public bool IsCrossplayPlatform => SKU.IsEOSCrossplayPlatform;

	public bool IsCrossplayRequestedActive => m_Crossplay_HasUserRequestedActive;

	public bool ShouldCrossplayBeActive => true;

	public bool IsCrossplayAccountLinked => m_Crossplay_Auth_HasLinkedAccounts;

	public bool IsCrossplayAwaitingAccountLink => m_Crossplay_Auth_InvalidUserContinuance != null;

	public bool IsCrossplayPlatformAvailable { get; private set; }

	public void OpenDLCShop()
	{
		Process.Start(new ProcessStartInfo("com.epicgames.launcher://store/all-dlc/terratech-02ab0a"));
	}

	public bool HasDLCEntitlement(string id)
	{
		if (!IsDLCInfoLoaded)
		{
			UnityEngine.Debug.LogError("[ERROR] Attempting to check if player has DLC Entitlement through ManEOS: Manager has not yet been initialised and we've not retreived the DLC entitlement information");
			return false;
		}
		UnityEngine.Debug.Assert(SKU.IsEpicGS);
		if (id.NullOrEmpty())
		{
			return false;
		}
		return m_DlcEntitlements.Contains(id);
	}

	public IEnumerator AwaitPlatformUserID()
	{
		if (m_IsOfflineMode)
		{
			yield break;
		}
		float timeOut = 15f;
		bool showingUI = false;
		bool breakOut = false;
		while (!HasPUID || showingUI)
		{
			if (timeOut < 0f && !showingUI)
			{
				d.LogWarningFormat("Hit the soft-timeout waiting for Auth Login to complete. Waited for {0} seconds. Prompting user to wait", 15f);
				string notification = "It's taking a long time to log you in.\nDo you wish to keep waiting?";
				string accept = "Keep Waiting";
				string decline = "Quit";
				Action accept2 = delegate
				{
					d.LogWarning("AwaitPlatformUserID (EOS Auth) timeout reset. Let's wait some more.");
					CleanupPopup();
					timeOut = 15f;
				};
				Action decline2 = delegate
				{
					d.LogWarning("User has chosen to stop waiting for Auth and Quit the application.");
					CleanupPopup();
					breakOut = true;
					Application.Quit();
				};
				UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
				uIScreenNotifications.Set(notification, accept2, decline2, accept, decline);
				Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
				showingUI = true;
			}
			yield return null;
			timeOut -= Time.deltaTime;
			if (showingUI && HasPUID)
			{
				CleanupPopup();
			}
			if (breakOut)
			{
				break;
			}
		}
		void CleanupPopup()
		{
			Singleton.Manager<ManUI>.inst.RemovePopup();
			showingUI = false;
		}
	}

	public IEnumerator AwaitDLCLoadComplete()
	{
		if (!m_IsOfflineMode)
		{
			while (!IsDLCInfoLoaded)
			{
				yield return null;
			}
		}
	}

	public void SetEOSCrossplayRequested(bool activeState)
	{
		d.Assert(IsCrossplayPlatform, "Attempted to set EOS crossplay state on a non EOS platform! No bueno my dude!!");
		m_Crossplay_HasUserRequestedActive = activeState;
		if (m_CrossplayPlatformPrefs.m_Crossplay_HasUserRequestedActive != activeState)
		{
			m_CrossplayPlatformPrefs.m_Crossplay_HasUserRequestedActive = activeState;
			SaveCrossplayPlatformPreferences();
		}
	}

	public void LinkAccountsWithCurrentContinuanceToken(Action<Result> onAccountsLinkedCallback)
	{
		DoLinkAccounts(onAccountsLinkedCallback);
	}

	public void DoAsSoonAsConnected(Action connectedAction)
	{
		if (IsConnected)
		{
			connectedAction();
		}
		else
		{
			StartCoroutine(PerformActionOnceConnected(connectedAction));
		}
		IEnumerator PerformActionOnceConnected(Action onceConnectedAction)
		{
			while (!IsConnected)
			{
				yield return null;
			}
			onceConnectedAction();
		}
	}

	public void TryConvertSteamIDsToEOS(IEnumerable<string> steamIDstrings, Action<List<(string steamID, string eosID)>> convertIDsCompleteCallback)
	{
		d.Assert(SKU.IsSteam && SKU.UsesEOS, "Convert from Steam to EOS playerID called on non steam platform!");
		d.Assert(IsConnected, "EOS is not connected yet, cannot query account mappings!");
		Utf8String[] array = steamIDstrings.Select((Func<string, Utf8String>)((string id) => id)).ToArray();
		if (array.Length != 0)
		{
			QueryExternalAccountMappingsOptions options = new QueryExternalAccountMappingsOptions
			{
				LocalUserId = EOSManager.Instance.GetProductUserId(),
				AccountIdType = ExternalAccountType.Steam,
				ExternalAccountIds = array
			};
			EOSManager.Instance.GetEOSConnectInterface().QueryExternalAccountMappings(ref options, (array, convertIDsCompleteCallback), OnQueryExternalAccountMappingsComplete);
		}
	}

	public Sprite GetDefaultUserSprite()
	{
		return m_DefaultUserSprite;
	}

	private BeginPlayerSessionOptionsAccountId GetBeginSessionAcountId()
	{
		BeginPlayerSessionOptionsAccountId result = default(BeginPlayerSessionOptionsAccountId);
		if (SKU.IsEpicGS)
		{
			result.Epic = m_EpicAccountID;
		}
		else
		{
			if (!SKU.IsSteam)
			{
				throw new NotImplementedException($"No AccountId is implemented for SKU {SKU.CurrentBuildType} for use with the EOS Metrics Session interface.");
			}
			result.External = SteamUser.GetSteamID().GetAccountID().ToString();
		}
		return result;
	}

	private EndPlayerSessionOptionsAccountId GetEndSessionAcountId()
	{
		EndPlayerSessionOptionsAccountId result = default(EndPlayerSessionOptionsAccountId);
		if (SKU.IsEpicGS)
		{
			result.Epic = m_EpicAccountID;
		}
		else
		{
			if (!SKU.IsSteam)
			{
				throw new NotImplementedException($"No AccountId is implemented for SKU {SKU.CurrentBuildType} for use with the EOS Metrics Session interface.");
			}
			result.External = SteamUser.GetSteamID().GetAccountID().ToString();
		}
		return result;
	}

	public void TrackSessionStart(Mode currentMode)
	{
		if (!IsOfflineMode)
		{
			if (!IsAuthLoggedIn)
			{
				d.LogError("User is not logged in somehow! Cannot track sessions!");
			}
			else if (currentMode.GetGameType() != ManGameMode.GameType.Attract)
			{
				BeginPlayerSessionOptions options = new BeginPlayerSessionOptions
				{
					AccountId = GetBeginSessionAcountId(),
					DisplayName = ((Singleton.Manager<ManNetworkLobby>.inst.LobbySystem != null) ? Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetUserName(Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetLocalPlayerID()) : "Unknown"),
					ControllerType = ((!Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad()) ? UserControllerType.MouseKeyboard : UserControllerType.GamepadControl),
					ServerIp = null,
					GameSessionId = string.Concat(currentMode.GetGameType(), (!currentMode.GetGameSubmode().NullOrEmpty()) ? ("_" + currentMode.GetGameSubmode()) : "")
				};
				Result result = EOSManager.Instance.GetEOSMetricsInterface().BeginPlayerSession(ref options);
				d.AssertFormat(result == Result.Success, "Error calling BeginPlayerSession: {0}", result);
			}
		}
	}

	public void TrackSessionEnd(Mode currentMode)
	{
		if (!IsOfflineMode)
		{
			if (!IsAuthLoggedIn)
			{
				d.LogError("User is not logged in somehow! Cannot track sessions!");
			}
			else if (currentMode.GetGameType() != ManGameMode.GameType.Attract)
			{
				EndPlayerSessionOptions options = new EndPlayerSessionOptions
				{
					AccountId = GetEndSessionAcountId()
				};
				Result result = EOSManager.Instance.GetEOSMetricsInterface().EndPlayerSession(ref options);
				d.AssertFormat(result == Result.Success, "Error calling EndPlayerSession: {0}", result);
			}
		}
	}

	private void StartEOSOnCurrentSKU()
	{
		if (IsCrossplayPlatform && SKU.IsSteam)
		{
			d.Assert(Singleton.Manager<ManSteamworks>.inst.Inited, "Trying to start EOS while using Steam, but Steam hasn't been initialised yet!");
			m_GetTicketForWebApiResponseCallback = Callback<GetTicketForWebApiResponse_t>.Create(OnGetSteamSessionTicketForWebApiResponse);
			m_SteamworksAuthTicket = SteamUser.GetAuthTicketForWebApi("terratech_eos");
		}
		else
		{
			StartEOS();
		}
	}

	private void StartEOS()
	{
		DoFullLogin(OnPostFirstLoginAttempt);
	}

	private void DoFullLogin(Action onLogAttemptedCallback = null)
	{
		DoFullLogin(null, null, onLogAttemptedCallback);
	}

	private void DoFullLogin(string devAuth_IDOverride, string devAuth_HostOverride, Action onLogAttemptedCallback)
	{
		d.Assert(_m_AuthLoginCallback == null && _m_ConnectLoginCallback == null, "DoFullLogin - Multiple concurrent logins not supported!");
		AuthLoginOnCurrentSKU(devAuth_IDOverride, devAuth_HostOverride, delegate(Result authResult)
		{
			if (authResult == Result.Success || IsCrossplayPlatform)
			{
				if (IsCrossplayPlatform && IsOfflineMode)
				{
					d.LogError("Not proceeding with EOS connect login after Auth Login process resulted in a request to enter offline mode (one way or another.) This could be due to rejected EULA or failed connection etc.");
				}
				else
				{
					ConnectLoginOnCurrentSKU(delegate
					{
						onLogAttemptedCallback?.Invoke();
					});
				}
			}
			else
			{
				onLogAttemptedCallback?.Invoke();
			}
		});
	}

	private void AuthLoginOnCurrentSKU(string devIDOverride, string devHostOverride, Action<Result> onLoggedCallback)
	{
		m_AuthAsGuest = false;
		SetOfflineMode(isOffline: false);
		if (SKU.IsEpicGS)
		{
			if (m_IsStartedFromLauncher)
			{
				if (m_EpicArgs.authPassword.NullOrEmpty())
				{
					BypassAuthLogin(Result.NoConnection, onLoggedCallback);
				}
				else
				{
					DoAuthLogin((LoginCredentialType)Enum.Parse(typeof(LoginCredentialType), m_EpicArgs.authType, ignoreCase: true), m_EpicArgs.authLogin, m_EpicArgs.authPassword, onLoggedCallback);
				}
				return;
			}
			bool flag = Application.isEditor && Singleton.Manager<DebugUtil>.inst.m_Settings.m_EpicLogonUseDevAuth && devIDOverride == null;
			if (flag || devIDOverride != null)
			{
				DoAuthLogin(LoginCredentialType.Developer, flag ? Singleton.Manager<DebugUtil>.inst.m_Settings.m_EpicLogonDevAuthHost : devHostOverride, flag ? Singleton.Manager<DebugUtil>.inst.m_Settings.m_EpicLogonDevAuthID : devIDOverride, onLoggedCallback);
				return;
			}
		}
		else if (IsCrossplayPlatform)
		{
			if (!ShouldCrossplayBeActive)
			{
				BypassAuthLogin(Result.Disabled, onLoggedCallback);
				return;
			}
			if (SKU.IsSteam)
			{
				DoAuthLogin(LoginCredentialType.ExternalAuth, ExternalCredentialType.SteamSessionTicket, m_SteamworksLoginID, m_SteamworksLoginToken, onLoggedCallback);
				return;
			}
		}
		throw new NotImplementedException("Unknown SKU configuration that does not provide an implementation for the auth login type to use!");
	}

	private void DoAuthLogin(LoginCredentialType type, string id, string token, Action<Result> callback)
	{
		DoAuthLogin(type, ExternalCredentialType.Epic, id, token, callback);
	}

	private void DoAuthLogin(LoginCredentialType type, ExternalCredentialType externCredentialType, string id, string token, Action<Result> callback)
	{
		d.Assert(_m_AuthLoginCallback == null, "DoAuthLogin - Multiple concurrent Auth logins not supported!");
		_m_AuthLoginCallback = callback;
		EOSManager.Instance.StartLoginWithLoginTypeAndToken(type, externCredentialType, id, token, OnAuthLoginCallback);
	}

	private void BypassAuthLogin(Result result, Action<Result> callback)
	{
		ValidateAuthLogin(result, null, callback);
	}

	private void ConnectLoginOnCurrentSKU(Action<Result> onLoggedCallback = null)
	{
		if (SKU.IsEpicGS)
		{
			if (IsAuthLoggedIn)
			{
				Epic.OnlineServices.Auth.CopyIdTokenOptions options = new Epic.OnlineServices.Auth.CopyIdTokenOptions
				{
					AccountId = EOSManager.Instance.GetLocalUserId()
				};
				EOSManager.Instance.GetEOSAuthInterface().CopyIdToken(ref options, out var outIdToken);
				d.AssertFormat(outIdToken.HasValue, "Failed to CopyIdToken for Epic Connect login! Multiplayer init failed");
				DoConnectLogin(ExternalCredentialType.EpicIdToken, outIdToken.HasValue ? outIdToken.Value.JsonWebToken : null, onLoggedCallback);
			}
			else
			{
				d.LogError("On EpicGS SKU: Unable to continue to connect log in as we're missing auth log in");
			}
		}
		else
		{
			if (!IsCrossplayPlatform || !SKU.IsSteam)
			{
				throw new NotImplementedException("Unknown SKU configuration that does not provide an implementation for the connect login type to use!");
			}
			DoConnectLogin(ExternalCredentialType.SteamSessionTicket, m_SteamworksLoginToken, onLoggedCallback);
		}
	}

	private void DoConnectLogin(ExternalCredentialType type, string token, Action<Result> onLoggedCallback = null)
	{
		d.Assert(_m_ConnectLoginCallback == null, "DoConnectLogin - Multiple concurrent Connect logins not supported!");
		_m_ConnectLoginCallback = onLoggedCallback;
		EOSManager.Instance.StartConnectLoginWithOptions(type, token, null, null, OnConnectLoginCallback);
	}

	private void DoLinkAccounts(Action<Result> onAccountsLinkedCallback)
	{
		d.Assert(_m_AccountsLinkedCallback == null, "DoLinkAccounts - Multiple concurrent account linkings not supported!");
		if (!IsCrossplayPlatformAvailable)
		{
			d.LogError("Crossplay platform not valid, can not link accounts! Are we missing the EPIC overlay?");
			return;
		}
		d.Assert(IsCrossplayPlatform && IsCrossplayAwaitingAccountLink, "Attempting to link account on a non-crossplay platform or while account linking is not available!");
		_m_AccountsLinkedCallback = onAccountsLinkedCallback;
		EOSManager.Instance.AuthLinkExternalAccountWithContinuanceToken(m_Crossplay_Auth_InvalidUserContinuance, LinkAccountFlags.NoFlags, OnLinkAccountCallback);
	}

	[DevCommand(Name = "EOS.DevAuth", Access = Access.DevCheat)]
	private void DoDevAuthLogin(string dev_id, string dev_host = "localhost:7777")
	{
		bool flag = EOSManager.Instance.GetLocalUserId() != null && EOSManager.Instance.GetLocalUserId().IsValid();
		d.AssertFormat(IsAuthLoggedIn == flag, "State missmatch before logout; System thinks AuthLogin={0}, while LocalUserId.IsValid={1}. Values expected to be the same", IsAuthLoggedIn, EOSManager.Instance.GetLocalUserId()?.IsValid() ?? false);
		if (IsAuthLoggedIn)
		{
			BeforeLogoutEvent.Send();
			InitState prevState = m_InitState;
			m_InitState &= (InitState)(-3);
			EOSManager.Instance.StartLogout(EOSManager.Instance.GetLocalUserId(), delegate(ref Epic.OnlineServices.Auth.LogoutCallbackInfo data)
			{
				d.AssertFormat(data.ResultCode == Result.Success, "Failed to log user out: {0}", data.ResultCode);
				InitStateChangedEvent.Send(prevState, m_InitState);
				_internal_DoDevLogin(dev_id, dev_host);
			});
		}
		else
		{
			_internal_DoDevLogin(dev_id, dev_host);
		}
		void _internal_DoDevLogin(string id, string host)
		{
			DoFullLogin(id, host, delegate
			{
				Singleton.Manager<ManProfile>.inst.DoDesktopFirstLoadStep();
				Singleton.Manager<ManUI>.inst.ExitAllScreens();
				Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
			});
		}
	}

	[DevCommand(Name = "EOS.RefreshConnectLogin", Access = Access.DevCheat)]
	private void RefreshConnectLogin()
	{
		if (m_ConnectLogInPeriodicRefresher != null)
		{
			StopCoroutine(m_ConnectLogInPeriodicRefresher);
		}
		m_ConnectLogInPeriodicRefresher = PeriodicallyAttemptToRefreshConnectLoginCo();
		StartCoroutine(m_ConnectLogInPeriodicRefresher);
	}

	private IEnumerator PeriodicallyAttemptToRefreshConnectLoginCo()
	{
		bool refreshConnectLoginSuccess = false;
		d.Log("[EOS connect login periodic refresh] Starting...");
		bool awaitingLastLoginAttempt;
		while (true)
		{
			awaitingLastLoginAttempt = true;
			float currentRefreshPeriod = 0f;
			ConnectLoginOnCurrentSKU(delegate(Result result)
			{
				awaitingLastLoginAttempt = false;
				refreshConnectLoginSuccess = result == Result.Success;
			});
			while (awaitingLastLoginAttempt || currentRefreshPeriod < 30f)
			{
				if (!awaitingLastLoginAttempt && refreshConnectLoginSuccess)
				{
					m_ConnectLogInPeriodicRefresher = null;
					d.Log("[EOS connect login periodic refresh] ...Complete");
					yield break;
				}
				if (currentRefreshPeriod >= 30f)
				{
					d.LogWarning($"[EOS connect login periodic refresh] Unable to complete within the given period [{30f}]. Why is the connect login taking longer than expected? Awaiting login complete...");
				}
				else
				{
					currentRefreshPeriod += Time.unscaledDeltaTime;
				}
				yield return null;
			}
		}
	}

	private void UpdateConnectLoginStatus(bool loggedIn)
	{
		InitState initState = m_InitState;
		if (initState.Is(InitState.ConnectLoggedIn) != loggedIn)
		{
			if (loggedIn)
			{
				m_InitState |= InitState.ConnectLoggedIn;
				m_ProductUserID = EOSManager.Instance.GetProductUserId();
				BeginListeningForConnectLoginAuthExpiration();
			}
			else
			{
				m_InitState &= (InitState)(-5);
				m_ProductUserID = null;
				EndListeningForConnectLoginAuthExpiration();
			}
			InitStateChangedEvent.Send(initState, m_InitState);
		}
	}

	private void BeginListeningForConnectLoginAuthExpiration()
	{
		d.Assert(m_ConnectLoginAuthExpirationListenerHandle == null || !m_ConnectLoginAuthExpirationListenerHandle.IsValid(), "Connect Login expiry handler already exists?!");
		AddNotifyAuthExpirationOptions options = default(AddNotifyAuthExpirationOptions);
		m_ConnectLoginAuthExpirationListenerHandle = new NotifyEventHandle(EOSManager.Instance.GetEOSConnectInterface().AddNotifyAuthExpiration(ref options, null, OnConnectLoginExpired), EOSManager.Instance.GetEOSConnectInterface().RemoveNotifyAuthExpiration);
	}

	private void EndListeningForConnectLoginAuthExpiration()
	{
		m_ConnectLoginAuthExpirationListenerHandle?.Dispose();
	}

	private void SetOfflineMode(bool isOffline)
	{
		Result result = EOSManager.Instance.GetEOSPlatformInterface().SetNetworkStatus(isOffline ? NetworkStatus.Offline : NetworkStatus.Online);
		d.AssertFormat(result == Result.Success, "[EOS] Platform setting failed with unhandled result {0}", result);
		m_IsOfflineMode = isOffline;
	}

	[DevCommand(Name = "EOS.DeleteCachedDLC", Access = Access.DevCheat)]
	private static void DeleteCachedDLC()
	{
		PlayerPrefs.DeleteKey("eos_cached_entitlements");
		PlayerPrefs.Save();
	}

	private void SecurelyStoreDLCEntitlementsCacheLocally()
	{
		string unencryptedString = JsonConvert.SerializeObject(DLCEntitlementsCache.GenerateCacheForCurrentUser(m_DlcEntitlements));
		unencryptedString = Util.Encryption.EncryptStringAES(unencryptedString, 24734);
		PlayerPrefs.SetString("eos_cached_entitlements", unencryptedString);
		PlayerPrefs.Save();
	}

	private void AssignDLCEntitlementsFromSecureLocalCache()
	{
		m_FailedToLoadAnyOfflineDLCEntitlements = false;
		if (!PlayerPrefs.HasKey("eos_cached_entitlements"))
		{
			SetDLCEntitlements(null);
			m_FailedToLoadAnyOfflineDLCEntitlements = true;
			return;
		}
		if (!JsonConvert.DeserializeObject<DLCEntitlementsCache>(Util.Encryption.DecryptStringAES(PlayerPrefs.GetString("eos_cached_entitlements"), 24734)).TryGetDLCEntitlements(out var entitlements))
		{
			m_FailedToLoadAnyOfflineDLCEntitlements = true;
		}
		SetDLCEntitlements(entitlements);
	}

	private void StartLoadDLCEntitlements()
	{
		if (m_IsOfflineMode)
		{
			AssignDLCEntitlementsFromSecureLocalCache();
			return;
		}
		UnityEngine.Debug.Assert(SKU.IsEpicGS, "Avast ye scallywags who dared unfurl the DLC entitlements on a non-EpicGS SCU!!!");
		UnityEngine.Debug.Assert(IsAuthLoggedIn, "Avast ye scallywags who dared to unfurl the DLC entitlements without first settlin' yer Auth Login!!");
		DLCTable dLCTable = Singleton.Manager<ManDLC>.inst.m_DLCTable;
		Utf8String[] array = ((dLCTable != null) ? dLCTable.DLCPacks.Where((ManDLC.DLC r) => !r.EOSDlc_Audience_ID.NullOrEmpty()).Select((Func<ManDLC.DLC, Utf8String>)((ManDLC.DLC r) => r.EOSDlc_Audience_ID)).ToArray() : null);
		if (array == null || array.Length == 0)
		{
			d.LogWarning("No DLC set up in the project. Skipping load!");
			SetDLCEntitlements(null);
			return;
		}
		d.LogFormat("[StartLoadDLCEntitlements] Checking for DLC content");
		QueryOwnershipOptions options = new QueryOwnershipOptions
		{
			LocalUserId = m_EpicAccountID,
			CatalogItemIds = array
		};
		(EOSManager.Instance.GetEOSPlatformInterface()?.GetEcomInterface()).QueryOwnership(ref options, null, OnQueryDLCOwnershipComplete);
	}

	[Conditional("IS_PUBLIC_EOS_BUILD")]
	private void EnsureGameLaunchedFromLauncher()
	{
		Result result = EOSManager.Instance.GetEOSPlatformInterface().CheckForLauncherAndRestart();
		switch (result)
		{
		case Result.NoChange:
			m_IsStartedFromLauncher = true;
			break;
		case Result.Success:
			d.LogWarning("Game was not launched via the Launcher - Restarting!");
			m_IsExiting = true;
			Application.Quit();
			break;
		default:
			d.LogErrorFormat("CheckForLauncherAndRestart failed with result: {0}", result);
			break;
		}
	}

	private void SaveCrossplayPlatformPreferences()
	{
		d.Assert(IsCrossplayPlatform, "Shouldn't be saving crossplay platform preferences on a non-crossplay platform. That includes the EGS platform.");
		ManSaveGame.SaveObject(m_CrossplayPlatformPrefs, PrefsSavePath);
	}

	private void LoadCrossplayPlatformPreferences()
	{
		d.Assert(IsCrossplayPlatform, "Shouldn't be loading crossplay platform preferences on a non-crossplay platform. That includes the EGS platform.");
		if (File.Exists(PrefsSavePath))
		{
			ManSaveGame.LoadObject(ref m_CrossplayPlatformPrefs, PrefsSavePath);
			SetEOSCrossplayRequested(m_CrossplayPlatformPrefs.m_Crossplay_HasUserRequestedActive);
		}
	}

	private void DetermineCrossplayPlatformAvailability()
	{
		GetDesktopCrossplayStatusOptions options = default(GetDesktopCrossplayStatusOptions);
		EOSManager.Instance.GetEOSPlatformInterface().GetDesktopCrossplayStatus(ref options, out var outDesktopCrossplayStatusInfo);
		IsCrossplayPlatformAvailable = outDesktopCrossplayStatusInfo.Status == DesktopCrossplayStatus.Ok;
		if (!IsCrossplayPlatformAvailable)
		{
			d.LogError($"Failed to init crossplay platform with unhandled Platform.DesktopCrossplayStatus: [{outDesktopCrossplayStatusInfo.Status}]. Some features may not be available.");
		}
	}

	private void SetDLCEntitlements(IEnumerable<string> dlcIDs, bool storeInLocalCache = false)
	{
		if (m_DlcEntitlements == null)
		{
			m_DlcEntitlements = new HashSet<string>();
		}
		m_DlcEntitlements.Clear();
		if (dlcIDs != null)
		{
			foreach (string dlcID in dlcIDs)
			{
				m_DlcEntitlements.Add(dlcID);
			}
		}
		Singleton.Manager<ManDLC>.inst.PlatformDLCChanged();
		if (storeInLocalCache)
		{
			SecurelyStoreDLCEntitlementsCacheLocally();
		}
	}

	private void ValidateAuthLogin(Result authLoginResult, ContinuanceToken authContinuance = null, Action<Result> postValidateCallback = null)
	{
		bool flag = false;
		if (authLoginResult == Result.Success)
		{
			InitState initState = m_InitState;
			m_InitState |= InitState.AuthLoggedIn;
			InitStateChangedEvent.Send(initState, m_InitState);
			d.AssertFormat(PlayerIDString.NullOrEmpty() || PlayerIDString == EOSManager.Instance.GetLocalUserId().ToString(), "CommandLine playerID ('{0}') did not match that returned from Epic Auth login ('{1}')", PlayerIDString, EOSManager.Instance.GetLocalUserId().ToString());
			m_EpicAccountID = EOSManager.Instance.GetLocalUserId();
			PlayerIDString = m_EpicAccountID.ToString();
			d.Assert(m_OnAuthLoginStatusChangeHandle == null || !m_OnAuthLoginStatusChangeHandle.IsValid(), "Auth Login status changed handler already exists?!");
			Epic.OnlineServices.Auth.AddNotifyLoginStatusChangedOptions options = default(Epic.OnlineServices.Auth.AddNotifyLoginStatusChangedOptions);
			m_OnAuthLoginStatusChangeHandle = new NotifyEventHandle(EOSManager.Instance.GetEOSAuthInterface().AddNotifyLoginStatusChanged(ref options, null, OnAuthLoginStatusChanged), EOSManager.Instance.GetEOSAuthInterface().RemoveNotifyLoginStatusChanged);
		}
		if (authLoginResult == Result.NoConnection)
		{
			SetOfflineMode(isOffline: true);
		}
		if (m_IsOfflineMode)
		{
			flag = true;
		}
		if (IsCrossplayPlatform)
		{
			switch (authLoginResult)
			{
			case Result.Success:
				m_Crossplay_Auth_HasLinkedAccounts = true;
				m_Crossplay_Auth_InvalidUserContinuance = null;
				break;
			case Result.InvalidUser:
				flag = (byte)((flag ? 1u : 0u) | 1u) != 0;
				m_Crossplay_Auth_InvalidUserContinuance = authContinuance;
				d.Log("CrossplayPlatform user has not yet linked their epic account, saving continuance token for if they decide to...");
				break;
			case Result.Disabled:
				flag |= !ShouldCrossplayBeActive;
				break;
			case Result.Canceled:
			case Result.NotFound:
				d.LogWarning("[EOS] AuthLogin: User not found or operation canceled! This might be due to them rejecting the user agreement");
				SetOfflineMode(isOffline: true);
				flag = (byte)((flag ? 1u : 0u) | 1u) != 0;
				break;
			default:
				if (flag)
				{
					d.Log("Handled: Unable to verify linked epic accounts for this crossplay platform but proceeding correctly anyways");
				}
				else
				{
					d.LogError($"Unable to verify whether user has linked their epic accounts on this crossplay platform due to unhandled Auth Login result: [{authLoginResult}]");
				}
				break;
			}
		}
		if (authLoginResult != Result.Success && !flag)
		{
			d.LogError($"Failed to Login to Epic Auth interface. [{authLoginResult}]");
			if (SKU.IsEpicGS)
			{
				Application.Quit();
			}
			else
			{
				SetOfflineMode(isOffline: true);
				d.LogError("Forcing to offline mode as we've not been able to handle an EOS Auth Exception on this platform...");
			}
		}
		postValidateCallback?.Invoke(authLoginResult);
	}

	private void ValidateLinkAccounts(Result accountLinkResult, Action<Result> postValidateCallback = null)
	{
		if (accountLinkResult == Result.Success)
		{
			ValidateAuthLogin(Result.Success);
			ConnectLoginOnCurrentSKU(delegate
			{
				postValidateCallback?.Invoke(accountLinkResult);
			});
		}
		else
		{
			postValidateCallback?.Invoke(accountLinkResult);
		}
	}

	private void ValidateConnectLogin(Result connectLoginResult, ContinuanceToken connectContinuance = null, Action<Result> postValidateCallback = null)
	{
		UpdateConnectLoginStatus(connectLoginResult == Result.Success);
		switch (connectLoginResult)
		{
		case Result.InvalidUser:
			d.LogWarningFormat("EOS Invalid User after attempted Connect Login! We will attempt to create one on the platform.");
			break;
		default:
			d.LogErrorFormat("EOS Connect login failed with unknown reason! {0}", connectLoginResult);
			break;
		case Result.Success:
			break;
		}
		if (connectLoginResult == Result.InvalidUser)
		{
			d.LogFormat("EOS Connect login reported {0}; Creating new EOS User for the account!", connectLoginResult);
			EOSManager.Instance.CreateConnectUserWithContinuanceToken(connectContinuance, delegate(CreateUserCallbackInfo info)
			{
				d.AssertFormat(info.ResultCode == Result.Success, "EOS Connect login failed to create user! {0}", info.ResultCode);
				UpdateConnectLoginStatus(info.ResultCode == Result.Success);
				postValidateCallback?.Invoke(info.ResultCode);
			});
		}
		else
		{
			postValidateCallback?.Invoke(connectLoginResult);
		}
	}

	private void OnAuthLoginCallback(Epic.OnlineServices.Auth.LoginCallbackInfo loginCallbackInfo)
	{
		d.Log($"EOS Auth Login result: {loginCallbackInfo.ResultCode}");
		ValidateAuthLogin(loginCallbackInfo.ResultCode, loginCallbackInfo.ContinuanceToken, delegate(Result result)
		{
			_m_AuthLoginCallback?.Invoke(result);
			_m_AuthLoginCallback = null;
		});
	}

	private void OnLinkAccountCallback(Epic.OnlineServices.Auth.LinkAccountCallbackInfo linkAccountCallbackInfo)
	{
		d.Log($"EOS Link Accounts Result: {linkAccountCallbackInfo.ResultCode}");
		ValidateLinkAccounts(linkAccountCallbackInfo.ResultCode, delegate(Result result)
		{
			_m_AccountsLinkedCallback?.Invoke(result);
			_m_AccountsLinkedCallback = null;
		});
	}

	private void OnConnectLoginCallback(Epic.OnlineServices.Connect.LoginCallbackInfo loginCallbackInfo)
	{
		d.Log($"EOS Connect Login result: {loginCallbackInfo.ResultCode}");
		ValidateConnectLogin(loginCallbackInfo.ResultCode, loginCallbackInfo.ContinuanceToken, delegate(Result result)
		{
			_m_ConnectLoginCallback?.Invoke(result);
			_m_ConnectLoginCallback = null;
		});
	}

	private void OnGetSteamSessionTicketForWebApiResponse(GetTicketForWebApiResponse_t ticketResponse)
	{
		if (ticketResponse.m_eResult != EResult.k_EResultOK)
		{
			SteamUser.CancelAuthTicket(m_SteamworksAuthTicket);
			m_SteamworksAuthTicket = HAuthTicket.Invalid;
			d.LogError($"Failed to get session ticket from Steam for use with EOS login: {ticketResponse.m_eResult}");
		}
		else
		{
			m_SteamworksLoginID = SteamUser.GetSteamID().GetAccountID().ToString();
			m_SteamworksAuthTicket = ticketResponse.m_hAuthTicket;
			string text = BitConverter.ToString(ticketResponse.m_rgubTicket);
			m_SteamworksLoginToken = text.Replace("-", string.Empty);
			StartEOS();
		}
	}

	private void OnPostFirstLoginAttempt()
	{
		if (SKU.IsEpicGS)
		{
			StartLoadDLCEntitlements();
		}
	}

	private void OnBeforeShutdown()
	{
		BeforeLogoutEvent.Send();
		if (SKU.IsEpicGS)
		{
			m_OnAuthLoginStatusChangeHandle?.Dispose();
		}
		EndListeningForConnectLoginAuthExpiration();
		InitState initState = m_InitState;
		m_InitState &= (InitState)(-7);
		InitStateChangedEvent.Send(initState, m_InitState);
	}

	private void OnAuthLoginStatusChanged(ref Epic.OnlineServices.Auth.LoginStatusChangedCallbackInfo callbackInfo)
	{
		if (callbackInfo.PrevStatus == LoginStatus.LoggedIn && callbackInfo.CurrentStatus == LoginStatus.NotLoggedIn)
		{
			bool flag = m_InitState.IsNot(InitState.AuthLoggedIn);
			m_EpicAccountID = null;
			PlayerIDString = null;
			m_OnAuthLoginStatusChangeHandle.Dispose();
			if (SKU.IsEpicGS && !flag)
			{
				throw new NotImplementedException("User unexpectedly logged out of Epic Account while playing the game! Nuking the game session as this should be invalid to continue.");
			}
		}
	}

	private void OnConnectLoginExpired(ref AuthExpirationCallbackInfo authExpirationCallback)
	{
		RefreshConnectLogin();
	}

	private void OnQueryDLCOwnershipComplete(ref QueryOwnershipCallbackInfo data)
	{
		if (data.ResultCode != Result.Success)
		{
			d.LogError($"Failed to get DLC ownership from EGS SDK! [{data.ResultCode}] We don't handle this! Find out what happened!");
			return;
		}
		SetDLCEntitlements(data.ItemOwnership.Where((ItemOwnership item) => item.OwnershipStatus == OwnershipStatus.Owned).Select((Func<ItemOwnership, string>)((ItemOwnership ownedItem) => ownedItem.Id)), storeInLocalCache: true);
	}

	private void OnQueryExternalAccountMappingsComplete(ref QueryExternalAccountMappingsCallbackInfo data)
	{
		d.AssertFormat(data.ResultCode == Result.Success, "QueryExternalAccountMappings failed: {0}", data.ResultCode);
		(Utf8String[], Action<List<(string, string)>>) tuple = ((Utf8String[], Action<List<(string, string)>>))data.ClientData;
		List<(string, string)> list = new List<(string, string)>();
		Utf8String[] item = tuple.Item1;
		foreach (Utf8String utf8String in item)
		{
			GetExternalAccountMappingsOptions options = new GetExternalAccountMappingsOptions
			{
				LocalUserId = EOSManager.Instance.GetProductUserId(),
				AccountIdType = ExternalAccountType.Steam,
				TargetExternalUserId = utf8String
			};
			ProductUserId externalAccountMapping = EOSManager.Instance.GetEOSConnectInterface().GetExternalAccountMapping(ref options);
			if (externalAccountMapping != null)
			{
				list.Add((utf8String, externalAccountMapping.ToString()));
			}
		}
		tuple.Item2?.Invoke(list);
	}

	private void OnModeStart(Mode currentMode)
	{
		TrackSessionStart(currentMode);
	}

	private void OnModeFinished(Mode currentMode)
	{
		TrackSessionEnd(currentMode);
	}

	private void Awake()
	{
		if (SKU.UsesEOS)
		{
			EOSInterface.InitializePlatform();
			InitState initState = m_InitState;
			m_InitState |= InitState.Platform;
			InitStateChangedEvent.Send(initState, m_InitState);
			EOSManager.Instance.AddApplicationCloseListener(OnBeforeShutdown);
		}
		if (SKU.IsEpicGS)
		{
			EnsureGameLaunchedFromLauncher();
			m_EpicArgs = EOSManager.Instance.GetCommandLineArgsFromEpicLauncher();
			PlayerIDString = m_EpicArgs.epicUserID;
			m_EpicAccountID = EpicAccountId.FromString(PlayerIDString);
		}
		else if (IsCrossplayPlatform)
		{
			LoadCrossplayPlatformPreferences();
			DetermineCrossplayPlatformAvailability();
		}
		if (SKU.UsesEOS && !m_IsExiting)
		{
			StartEOSOnCurrentSKU();
		}
	}

	private void Start()
	{
		if (SKU.UsesEOS)
		{
			Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Subscribe(OnModeStart);
			Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(OnModeFinished);
		}
	}

	private void OnApplicationQuit()
	{
		if (SKU.IsSteam && m_SteamworksAuthTicket != HAuthTicket.Invalid)
		{
			SteamUser.CancelAuthTicket(m_SteamworksAuthTicket);
		}
	}
}
