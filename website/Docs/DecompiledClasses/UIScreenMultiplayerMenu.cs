#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using TerraTech.Network;
using UnityEngine;

public class UIScreenMultiplayerMenu : UIScreen
{
	private enum SearchState
	{
		PendingResults,
		PendingLobbyValidation,
		ChoosingBestMatch
	}

	[SerializeField]
	private MultiplayerModeType m_GameMode;

	protected bool m_HasRequestPending;

	private bool m_bSearchingForMatches;

	private SearchState m_SearchState;

	private List<LobbyData> m_SearchResults;

	private float m_UpdateListTimer;

	private const float k_UpdateLobbyListFrequency = 1f;

	private const float k_UpdateLobbyListPendingResultTimeout = 10.5f;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		m_HasRequestPending = false;
		m_bSearchingForMatches = false;
		m_SearchResults = null;
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinedEvent.Subscribe(OnLobbyJoined);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyCreateFailedEvent.Subscribe(OnLobbyCreateFailed);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinFailedEvent.Subscribe(OnLobbyJoinFailed);
		if (SKU.ConsoleUI)
		{
			m_ExitButton.gameObject.SetActive(value: false);
		}
	}

	public override void Hide()
	{
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinedEvent.Unsubscribe(OnLobbyJoined);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyCreateFailedEvent.Unsubscribe(OnLobbyCreateFailed);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinFailedEvent.Unsubscribe(OnLobbyJoinFailed);
		if (m_bSearchingForMatches && (m_SearchState == SearchState.PendingResults || m_SearchState == SearchState.PendingLobbyValidation))
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyListUpdatedEvent.Unsubscribe(OnSearchResult);
		}
		base.Hide();
	}

	public override bool GoBack()
	{
		bool flag = base.GoBack();
		if (flag && Singleton.Manager<ManNetworkLobby>.inst.Inited)
		{
			if (Singleton.Manager<ManNetworkLobby>.inst.IsJoinOrCreateLobbyRequestActive())
			{
				flag = false;
			}
			else
			{
				Singleton.Manager<ManNetworkLobby>.inst.LeaveLobby();
			}
		}
		d.Log("[UIScreenMultiplayerMenu] - GoBack : allowed = " + flag);
		return flag;
	}

	public void OnButtonClickedQuickMatch()
	{
		if (!Singleton.Manager<ManNetworkLobby>.inst.Inited)
		{
			if (SKU.IsSteam)
			{
				_showSteamNotInitialisedError();
			}
			else
			{
				d.LogError("OnButtonClickedQuickMatch - ManNetworkLobby was not initialised; Could not start search for QuickMatch");
			}
			return;
		}
		Action accept = delegate
		{
			Singleton.Manager<ManUI>.inst.ExitAllScreens();
			Singleton.Manager<ManNetworkLobby>.inst.LeaveLobby();
			Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
		};
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 90);
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, 61);
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		uIScreenNotifications.Set(localisedString, accept, localisedString2);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
		m_bSearchingForMatches = true;
		m_SearchState = SearchState.PendingResults;
		m_SearchResults = null;
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyListUpdatedEvent.Subscribe(OnSearchResult);
		if (!Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.IsBusyRequestingLobbies)
		{
			LobbySystem.LobbyFilterOptions lobbyFilterOptions = new LobbySystem.LobbyFilterOptions();
			lobbyFilterOptions.m_HideFullGames = 1;
			lobbyFilterOptions.m_GameModeIndex = (int)m_GameMode;
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.RefreshLobbyList(lobbyFilterOptions);
		}
	}

	public virtual void OnButtonClickedHost()
	{
		if (m_HasRequestPending)
		{
			d.Log("[UIScreenMultiplayerMenu] - Trying to Create Game - Aborted as m_HasRequestPending == true");
			return;
		}
		d.Log("[UIScreenMultiplayerMenu] - Trying to Create Game");
		if (!Singleton.Manager<ManNetworkLobby>.inst.Inited)
		{
			_showSteamNotInitialisedError();
			return;
		}
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.ClearPingRequests();
		d.Log("[UIScreenMultiplayerMenu] - Trying to Create Host Lobby");
		m_HasRequestPending = true;
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CreateLobby(MultiplayerModeType.Deathmatch, Lobby.LobbyVisibility.Public);
		d.Log("[UIScreenMultiplayerMenu] - Waiting to join Host Lobby");
	}

	public void OnButtonClickedGeneral()
	{
		if (m_HasRequestPending)
		{
			d.Log("[UIScreenMultiplayerMenu] - Trying to Join General Game aborted as m_HasRequestPending == true");
			return;
		}
		d.Log("[UIScreenMultiplayerMenu] - Trying to Join General Game");
		if (!Singleton.Manager<ManNetworkLobby>.inst.Inited)
		{
			_showSteamNotInitialisedError();
			return;
		}
		d.Log("[UIScreenMultiplayerMenu] - Going to lobby (ignore friends only)");
		GoToLobbyList(friendsOnly: false);
	}

	public void OnButtonClickedBack()
	{
		Singleton.Manager<ManUI>.inst.GoBack();
	}

	private void GoToLobbyList(bool friendsOnly)
	{
		d.Log("[UIScreenMultiplayerMenu] GoToLobbyList - friendsOnly: " + friendsOnly);
		UIScreenNetworkLobbyList uIScreenNetworkLobbyList = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.MatchmakingLobbyList) as UIScreenNetworkLobbyList;
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.ClearPingRequests();
		uIScreenNetworkLobbyList.SetFriendsOnly(friendsOnly);
		uIScreenNetworkLobbyList.SetGameMode(m_GameMode);
		Singleton.Manager<ManUI>.inst.PushScreen(uIScreenNetworkLobbyList);
	}

	private void OnLobbyJoined(Lobby lobby)
	{
		d.Log("[UIScreenMultiplayerMenu] OnLobbyJoined: LobbyName=" + lobby.Name);
		m_HasRequestPending = false;
	}

	private void OnLobbyJoinFailed(LobbySystem.LobbyErrorCode errorCode)
	{
		d.LogError("[UIScreenMultiplayerMenu] OnLobbyJoinFailed - Result=" + errorCode);
		m_HasRequestPending = false;
		if (errorCode != LobbySystem.LobbyErrorCode.Cancelled)
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamLobby, 2);
			_showPopup(localisedString);
		}
	}

	private void OnLobbyCreateFailed(LobbySystem.LobbyErrorCode errorCode)
	{
		d.LogError("[UIScreenMultiplayerMenu] OnLobbyCreateFailed - Result=" + errorCode);
		m_HasRequestPending = false;
		if (errorCode != LobbySystem.LobbyErrorCode.Cancelled)
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamLobby, 1);
			_showPopup(localisedString);
		}
	}

	protected void _showSteamNotInitialisedError()
	{
		d.LogError("Steam is NOT initialised!");
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamLobby, 3);
		_showPopup(localisedString);
	}

	private void _showPopup(string msg)
	{
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		Action accept = delegate
		{
			Singleton.Manager<ManUI>.inst.RemovePopup();
		};
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
		uIScreenNotifications.Set(msg, accept, localisedString);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
	}

	private void Update()
	{
		if (!m_bSearchingForMatches)
		{
			return;
		}
		switch (m_SearchState)
		{
		case SearchState.PendingResults:
			if (m_SearchResults != null)
			{
				m_UpdateListTimer = 0f;
				m_SearchState = SearchState.PendingLobbyValidation;
			}
			break;
		case SearchState.PendingLobbyValidation:
		{
			if (m_SearchResults == null)
			{
				break;
			}
			bool flag = true;
			if (m_SearchResults.Count > 1)
			{
				foreach (LobbyData searchResult in m_SearchResults)
				{
					if (LobbySystem.PROTOCOL_VERSION == searchResult.m_ProtocolVersion && searchResult.m_PingTimeMS == -1)
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				m_SearchState = SearchState.ChoosingBestMatch;
				Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyListUpdatedEvent.Unsubscribe(OnSearchResult);
				break;
			}
			if (m_UpdateListTimer > 1f && !Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.IsBusyRequestingLobbies)
			{
				Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.UpdateCurrentLobbyList();
				m_UpdateListTimer = 0f;
			}
			m_UpdateListTimer += Time.deltaTime;
			break;
		}
		case SearchState.ChoosingBestMatch:
		{
			int num = SelectBestMatch();
			if (num >= 0 && num < m_SearchResults.Count)
			{
				LobbyData lobbyData = m_SearchResults[num];
				Singleton.Manager<ManNetworkLobby>.inst.JoinLobby(lobbyData.m_IDLobby, fromInvite: false);
				m_bSearchingForMatches = false;
			}
			else
			{
				m_bSearchingForMatches = false;
				string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 91);
				_showPopup(localisedString);
			}
			break;
		}
		}
	}

	private bool IsLobbyDataValid(LobbyData data)
	{
		bool result = false;
		if (LobbySystem.PROTOCOL_VERSION == data.m_ProtocolVersion && data.m_IDLobby != TTNetworkID.Invalid && data.m_Language != null && data.m_NumFriends != -1 && data.m_NumUsers != -1)
		{
			result = true;
		}
		return result;
	}

	private int SelectBestMatch()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		int num = 0;
		foreach (LobbyData searchResult in m_SearchResults)
		{
			if (LobbySystem.PROTOCOL_VERSION == searchResult.m_ProtocolVersion)
			{
				list.Add(num);
			}
			else
			{
				list2.Add(num);
			}
			num++;
		}
		if (list.Count > 0)
		{
			list = GetLobbiesWithMostFriends(list, list2);
			if (list.Count > 0)
			{
				if (list.Count == 1)
				{
					list2.Add(list[0]);
					OutputSortedList(list2);
					return list[0];
				}
				list = GetLobbiesWithSlotsFilled(list, list2);
				if (list.Count > 0)
				{
					if (list.Count == 1)
					{
						list2.Add(list[0]);
						OutputSortedList(list2);
						return list[0];
					}
					list = GetLobbiesWithSameLanguage(list, list2);
					if (list.Count == 1)
					{
						list2.Add(list[0]);
						OutputSortedList(list2);
						return list[0];
					}
					OutputSortedList(list2);
					return GetCandidateWithLowestPing(list, list2);
				}
			}
		}
		OutputSortedList(list2);
		return -1;
	}

	private void OutputSortedList(List<int> sortedList)
	{
		sortedList.Reverse();
		foreach (int sorted in sortedList)
		{
			_ = m_SearchResults[sorted];
		}
	}

	private List<int> GetLobbiesWithMostFriends(List<int> candidates, List<int> sortedList)
	{
		List<int> list = new List<int>();
		int num = 0;
		foreach (int candidate in candidates)
		{
			if (m_SearchResults[candidate].NumFriendsEstimate > num)
			{
				num = m_SearchResults[candidate].NumFriendsEstimate;
			}
		}
		foreach (int candidate2 in candidates)
		{
			if (m_SearchResults[candidate2].NumFriendsEstimate == num)
			{
				list.Add(candidate2);
			}
			else
			{
				sortedList.Add(candidate2);
			}
		}
		return list;
	}

	private List<int> GetLobbiesWithSlotsFilled(List<int> candidates, List<int> sortedList)
	{
		List<int> list = new List<int>();
		int num = 0;
		foreach (int candidate in candidates)
		{
			int num2 = m_SearchResults[candidate].m_NumUsers / m_SearchResults[candidate].m_MaxUserLimit * 100;
			if (num2 > num)
			{
				num = num2;
			}
		}
		foreach (int candidate2 in candidates)
		{
			if (m_SearchResults[candidate2].m_NumUsers / m_SearchResults[candidate2].m_MaxUserLimit * 100 == num)
			{
				list.Add(candidate2);
			}
			else
			{
				sortedList.Add(candidate2);
			}
		}
		return list;
	}

	private List<int> GetLobbiesWithSameLanguage(List<int> candidates, List<int> sortedList)
	{
		List<int> list = new List<int>();
		foreach (int candidate in candidates)
		{
			if (m_SearchResults[candidate].m_Language == StringLookup.GetLocalisedLanguageName(Singleton.Manager<Localisation>.inst.CurrentLanguage))
			{
				list.Add(candidate);
			}
			else
			{
				sortedList.Add(candidate);
			}
		}
		if (list.Count > 0)
		{
			return list;
		}
		return candidates;
	}

	private int GetCandidateWithLowestPing(List<int> candidates, List<int> sortedList)
	{
		int num = int.MaxValue;
		int num2 = -1;
		foreach (int candidate in candidates)
		{
			if (m_SearchResults[candidate].m_PingTimeMS < num)
			{
				num = m_SearchResults[candidate].m_PingTimeMS;
				num2 = candidate;
			}
		}
		foreach (int candidate2 in candidates)
		{
			if (candidate2 != num2)
			{
				sortedList.Add(candidate2);
			}
		}
		sortedList.Add(num2);
		return num2;
	}

	private void OnSearchResult(List<LobbyData> lobbyList, bool seachEnd)
	{
		m_SearchResults = lobbyList;
	}
}
