#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Netease.Oddish.Ingame.Sdk;
using Netease.Oddish.Ingame.Sdk.Entity.Config;
using Netease.Oddish.Ingame.Sdk.Entity.ContentFilter;
using Netease.Oddish.Ingame.Sdk.Entity.Matchmake;
using Netease.Oddish.Ingame.Sdk.Entity.User;
using Netease.Oddish.Ingame.Sdk.Entity.UserSession;
using Netease.Oddish.Ingame.Sdk.Task;
using TerraTech.Network;
using UnityEngine;

public class ManNetEase : Singleton.Manager<ManNetEase>
{
	public delegate void ProfanityCheckDelegate(BannedWordCheck checkResponse);

	private const string k_appId = "428765813420417024";

	private bool m_Inited;

	private bool m_LoginProcessing;

	private bool m_HasDisposed;

	private static bool s_WantsToQuit;

	private static bool s_Initialising;

	private bool m_UpdatingFriends;

	private bool m_QueueUpdateFriends;

	private static LoginTask.SuccessResult s_LoginData;

	private static TTNetworkID s_PlayerNetworkId;

	private string[] m_Friends;

	private string m_PendingLobbyJoin;

	private Queue<Action> m_PendingCallbacks;

	private static HashSet<string> s_DlcEntitlements = new HashSet<string>();

	public bool Inited => m_Inited;

	public bool IsDisposed => m_HasDisposed;

	public string PlayerNickname => s_LoginData.UserSession.Nickname;

	public string[] Friends => m_Friends;

	public string PlayerId => s_LoginData.UserSession.UserId;

	public UserSession Player => s_LoginData.UserSession;

	public TTNetworkID PlayerNetworkID => s_PlayerNetworkId;

	public string PendingLobbyJoin => m_PendingLobbyJoin;

	public static bool LoggedIn => s_LoginData != null;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void OnBeforeSceneLoadRuntimeMethod()
	{
		if (SKU.IsNetEase)
		{
			InitialiseNetEaseSdk();
		}
	}

	public void OnApplicationQuit()
	{
		if (SKU.IsNetEase)
		{
			OddishSdk.Dispose();
		}
	}

	public void ClearPendingLobbyJoin()
	{
		m_PendingLobbyJoin = null;
	}

	public static bool HasDlcEntitlement(string netEaseName)
	{
		if (!netEaseName.NullOrEmpty())
		{
			return s_DlcEntitlements.Contains(netEaseName);
		}
		return false;
	}

	public static IEnumerator LoadDLCEntitlements_Coroutine(DLCTable dlcTable)
	{
		if (!SKU.IsNetEase || dlcTable == null)
		{
			yield break;
		}
		while (!LoggedIn)
		{
			yield return null;
		}
		foreach (ManDLC.DLC pack in dlcTable.DLCPacks)
		{
			if (pack.NetEaseContentID.NullOrEmpty())
			{
				continue;
			}
			d.LogFormat("[LoadDLCEntitlements_Coroutine] Checking for DLC content {0}", pack.NetEaseContentID);
			bool checking = true;
			OddishSdk.User.CreateGetContentInstallStatus().ExecAsync(pack.NetEaseContentID, delegate(GetContentInstallStatusTask.SuccessResult result)
			{
				d.LogFormat("[LoadDLCEntitlements_Coroutine] DLC Check for {0} finished with status {1}", pack.NetEaseContentID, result.Status);
				if (result.Status == ContentInstallStatusEnum.Installed)
				{
					s_DlcEntitlements.Add(pack.NetEaseContentID);
				}
				checking = false;
			}, delegate
			{
				d.LogFormat("[LoadDLCEntitlements_Coroutine] DLC Check for {0} failed", pack.NetEaseContentID);
				checking = false;
			});
			while (checking)
			{
				yield return null;
			}
		}
	}

	private void Awake()
	{
		if (!SKU.IsNetEase)
		{
			return;
		}
		SetupHooks();
		Regex regex = new Regex("^--join-lobby=");
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		string text = "";
		string[] array = commandLineArgs;
		foreach (string input in array)
		{
			if (regex.IsMatch(input))
			{
				text = regex.Replace(input, "");
				break;
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			d.LogFormat("[ManNetEase] Game launched to join lobby {0}", text);
			m_PendingLobbyJoin = text;
		}
		m_QueueUpdateFriends = true;
	}

	private static void InitialiseNetEaseSdk()
	{
		if (!s_Initialising)
		{
			s_Initialising = true;
			Debug.Log("<b>[ManNetEase] InitialiseNetEaseSdk b</b>");
			Config config = new Config
			{
				IsEnableAntiAddictionPopup = true,
				AppId = "428765813420417024"
			};
			OddishSdk.CreateInit().ExecAsync(config, delegate
			{
				Debug.Log("[ManNetEase] Sdk successfully inited");
				StartUserLogin();
			}, delegate(InitTask.FailResult onFailure)
			{
				Debug.LogError($"ManNetEase.InitialiseNetEaseSdk - Sdk failed to initialise with error [{onFailure.Code}] {onFailure.Message}");
				s_WantsToQuit = true;
			});
			Debug.Log("<b>[ManNetEase] InitialiseNetEaseSdk f</b>");
		}
	}

	private void SetupHooks()
	{
		OddishSdk.LauncherClosed += OnLauncherClose;
		OddishSdk.AntiAddictionUpdated += OnAntiAddictionUpdated;
		OddishSdk.LobbyJoinRequested += delegate(LobbyJoinRequestedNotification result)
		{
			d.LogFormat("[ManNetEase] Received invitation to lobby {0}", result.LobbyId);
			m_PendingLobbyJoin = result.LobbyId;
		};
		m_PendingCallbacks = new Queue<Action>();
		m_Inited = true;
	}

	private void OnDestroy()
	{
		if (SKU.IsNetEase)
		{
			OddishSdk.LauncherClosed -= OnLauncherClose;
			OddishSdk.AntiAddictionUpdated -= OnAntiAddictionUpdated;
			Debug.Log("[ManNetEase] Disposing of OddishSdk");
			OddishSdk.Dispose();
			m_HasDisposed = true;
		}
	}

	public void UpdateFriends()
	{
		if (s_LoginData == null)
		{
			d.LogError("[ManNetEase] Trying to UpdateFriends but we are not logged in");
		}
		else if (!m_UpdatingFriends)
		{
			m_UpdatingFriends = true;
			Debug.Log("[ManNetEase] Requesting user account friends");
			OddishSdk.Friend.CreateGetFriends().ExecAsync(0, 100, delegate(GetFriendsTask.SuccessResult onSuccess)
			{
				Debug.Log("[ManNetEase] Received Friends - Total=" + onSuccess.FriendIds.Length);
				m_Friends = onSuccess.FriendIds;
				m_UpdatingFriends = false;
			}, delegate(GetFriendsTask.FailResult onFailure)
			{
				m_UpdatingFriends = false;
				Debug.LogError($"ManNetEase.Init.friendsTask.ExecAsync - Unable to locate friends with error code [{onFailure.Code}] - {onFailure.Message}");
			});
		}
	}

	private void Update()
	{
		if (SKU.IsNetEase)
		{
			if (s_WantsToQuit)
			{
				s_WantsToQuit = false;
				Application.Quit();
			}
			while (m_PendingCallbacks.Count > 0)
			{
				m_PendingCallbacks.Dequeue()();
			}
			if (m_QueueUpdateFriends && s_LoginData != null)
			{
				m_QueueUpdateFriends = false;
				UpdateFriends();
			}
		}
	}

	private void OnAntiAddictionUpdated(UserSession userSession)
	{
		Debug.Log((1 <= userSession.AddictionLevel) ? "[ManNetEase] User Addiction Level is flagged as <b>Addicted</b> - preparing to save game and close down" : "[ManNetEase] User Addition level is <b>normal</b>, and playing normally");
		if (1 <= userSession.AddictionLevel)
		{
			StartCleanShutdown();
		}
	}

	public void AddCallback(Action cb)
	{
		m_PendingCallbacks.Enqueue(cb);
	}

	private void StartCleanShutdown()
	{
		Debug.Log("[ManNetEase] Starting Shutdown via Sdk Request");
		if (Singleton.Manager<ManGameMode>.inst.CurrentModeCanSave() && !Singleton.Manager<ManGameMode>.inst.SaveCurrentMode())
		{
			Debug.LogError("ManNetEase.StartCleanShutdown - unable to save the game on shutdown");
		}
	}

	private void OnLauncherClose()
	{
		Debug.Log("[ManNetEase] OnLauncherClose triggered.");
		s_WantsToQuit = true;
	}

	public void DisplayProfanityOverlay()
	{
		OddishSdk.CensorInfoOverlay.CreateActivateCensorInfoOverlay().ExecAsync(delegate
		{
			Debug.Log("[ManNetEase] Displaying Censorship Overlay");
		}, delegate(ActivateCensorInfoOverlayTask.FailResult onFailure)
		{
			Debug.LogError($"ManNetEase.DisplayProfanityOverlay - Unable to create overlay: [{onFailure.Code}] - {onFailure.Message}");
		});
	}

	public void CheckEnteredText(string text, BannedWordCheckType checkMethod, ProfanityCheckDelegate checkResultCallback)
	{
		if (text == null)
		{
			text = string.Empty;
		}
		Debug.Log("[ManNetEase] Checking " + text + " for profanity/censorship");
		OddishSdk.ContentFilter.CreateBannedWordCheck().ExecAsync(text, checkMethod, delegate(BannedWordCheckTask.SuccessResult onSuccess)
		{
			if (onSuccess.Check.Status == BannedWordCheckStatus.Forbidden)
			{
				m_PendingCallbacks.Enqueue(DisplayProfanityOverlay);
			}
			m_PendingCallbacks.Enqueue(delegate
			{
				checkResultCallback(onSuccess.Check);
			});
		}, delegate(BannedWordCheckTask.FailResult onFailure)
		{
			Debug.LogError($"ManNetEase.CheckEnteredText - Error occured while checking text: [{onFailure.Code}] - {onFailure.Message}");
		});
	}

	private static void StartUserLogin()
	{
		LoginTask loginTask = OddishSdk.UserSession.CreateLogin();
		if (loginTask == null)
		{
			Debug.LogError("ManNetEase.StartUserLogin - Trying to log in user, but unable to create login task.");
			return;
		}
		Debug.Log("[ManNetEase] Starting User Login");
		loginTask.ExecAsync(delegate(LoginTask.SuccessResult loginSuccess)
		{
			Debug.Log("[ManNetEase] Successfully logged in user " + loginSuccess.UserSession.Nickname);
			s_LoginData = loginSuccess;
			s_PlayerNetworkId = new TTNetworkID(loginSuccess.UserSession.UserId);
		}, delegate
		{
			Debug.Log("[ManNetEase] Login request was cancelled");
			s_WantsToQuit = true;
		}, delegate(LoginTask.FailResult loginFailed)
		{
			Debug.LogError($"ManNetEase.StartUserLogin.loginRequest.loginFailed - Login Failed with code {loginFailed.Code} - {loginFailed.Message}");
			s_WantsToQuit = true;
		});
	}
}
