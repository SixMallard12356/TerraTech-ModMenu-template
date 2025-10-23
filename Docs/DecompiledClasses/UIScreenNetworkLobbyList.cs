#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScreenNetworkLobbyList : UIScreen
{
	private enum EnumColumnType
	{
		CT_GAME_MODE,
		CT_PLAYERS,
		CT_TEAMS,
		CT_FRIENDS,
		CT_STATUS,
		CT_HOST,
		CT_PING,
		CT_LANGUAGE,
		CT_PRIVATE,
		CT_NUM_COLUMNS
	}

	[SerializeField]
	private CanvasGroup m_MainPaneCanvasGroup;

	[SerializeField]
	private NetworkLobbyElement m_LobbyElementPrefab;

	[SerializeField]
	private RectTransform m_ScrollPanel;

	[SerializeField]
	private Toggle m_FriendsToggle;

	[SerializeField]
	private Button m_FiltersButton;

	[SerializeField]
	private Button m_BanlistButton;

	[SerializeField]
	private Button m_JoinGameButton;

	[SerializeField]
	private Button m_RefreshButton;

	[SerializeField]
	private GameObject m_SearchingForGames;

	[SerializeField]
	private GameObject m_NoGamesFound;

	[SerializeField]
	private Button m_ColumnMode;

	[SerializeField]
	private Button m_ColumnPlayers;

	[SerializeField]
	private Button m_ColumnTeams;

	[SerializeField]
	private Button m_ColumnFriends;

	[SerializeField]
	private Button m_ColumnStatus;

	[SerializeField]
	private Button m_ColumnHost;

	[SerializeField]
	private Button m_ColumnPing;

	[SerializeField]
	private Button m_ColumnLanguage;

	[SerializeField]
	private Button m_ColumnPrivate;

	[SerializeField]
	private GameObject m_FiltersPanel;

	[SerializeField]
	private Button m_FiltersBackButton;

	[SerializeField]
	private Button m_FiltersApplyButton;

	[SerializeField]
	private Button m_FiltersResetButton;

	[SerializeField]
	private Dropdown m_FiltersGameModeDropdown;

	[SerializeField]
	private Dropdown m_FiltersTeamDropdown;

	[SerializeField]
	private Toggle m_FiltersHideFullGamesToggle;

	[SerializeField]
	private GameObject m_FilterShowOnlyFriendGamesContainer;

	[SerializeField]
	private Toggle m_FiltersShowOnlyFriendGamesToggle;

	[SerializeField]
	private Toggle m_FiltersShowOnlyNearbyGamesToggle;

	[SerializeField]
	private Dropdown m_FiltersLanguageDropdown;

	[SerializeField]
	private Dropdown m_FiltersPingDropdown;

	[SerializeField]
	private Toggle m_FiltersHideInProgressGamesToggle;

	[SerializeField]
	private Toggle m_FiltersShowModdedGames;

	private UIScreen m_BannedPlayersScreen;

	private List<NetworkLobbyElement> m_LobbyList = new List<NetworkLobbyElement>(8);

	private float m_RefreshTimer;

	private float m_UpdateListTimer;

	private bool m_RequestRefreshLobby;

	private EnumColumnType m_SortColumn;

	private bool m_SortAscending = true;

	private LobbySystem.LobbyFilterOptions m_FilterOptions = new LobbySystem.LobbyFilterOptions();

	private Text[] m_ColumnHeadings = new Text[9];

	private const float k_RefreshLobbyListFrequency = 5f;

	private const float k_RefreshLobbyListFrequencySwitch = float.MaxValue;

	private const float k_UpdateLobbyListFrequency = 1f;

	private const float k_RefreshDelay = 1f;

	private const float k_RefreshDelaySwitch = 20f;

	private bool m_SearchComplete;

	private bool m_IsShowingProfanityWarning;

	private UIScreenProfanityWarning m_pProfanityWarningScreen;

	private static bool m_HasUserAcceptedProfanityWarning;

	private bool FriendOnlyFilteringSupported => !SKU.UsesEOS;

	public void SetFriendsOnly(bool friendsOnly)
	{
		m_FilterOptions.m_FriendsGamesOnly = (friendsOnly ? 1 : 0);
	}

	public void SetGameMode(MultiplayerModeType gameMode)
	{
		m_FilterOptions.m_GameModeIndex = (int)gameMode;
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		if (!fromStackPop)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.ShouldAbortLobbyScreen = false;
		}
		m_SortColumn = EnumColumnType.CT_PLAYERS;
		m_SortAscending = false;
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyListUpdatedEvent.Subscribe(RebuildLobbies);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinFailedEvent.Subscribe(OnLobbyJoinFailed);
		m_ExitButton.onClick.AddListener(HandleExitButton);
		m_RefreshButton.onClick.AddListener(HandleRefreshButton);
		m_FiltersButton.onClick.AddListener(HandleFiltersButton);
		m_BanlistButton.onClick.AddListener(HandleBanlistButton);
		m_JoinGameButton.onClick.AddListener(HandleJoinGameButton);
		m_ColumnMode.onClick.AddListener(delegate
		{
			HandleChangeSortColumn(EnumColumnType.CT_GAME_MODE);
		});
		m_ColumnPlayers.onClick.AddListener(delegate
		{
			HandleChangeSortColumn(EnumColumnType.CT_PLAYERS);
		});
		m_ColumnTeams.onClick.AddListener(delegate
		{
			HandleChangeSortColumn(EnumColumnType.CT_TEAMS);
		});
		m_ColumnFriends.onClick.AddListener(delegate
		{
			HandleChangeSortColumn(EnumColumnType.CT_FRIENDS);
		});
		m_ColumnStatus.onClick.AddListener(delegate
		{
			HandleChangeSortColumn(EnumColumnType.CT_STATUS);
		});
		m_ColumnHost.onClick.AddListener(delegate
		{
			HandleChangeSortColumn(EnumColumnType.CT_HOST);
		});
		m_ColumnPing.onClick.AddListener(delegate
		{
			HandleChangeSortColumn(EnumColumnType.CT_PING);
		});
		m_ColumnLanguage.onClick.AddListener(delegate
		{
			HandleChangeSortColumn(EnumColumnType.CT_LANGUAGE);
		});
		m_ColumnPrivate.onClick.AddListener(delegate
		{
			HandleChangeSortColumn(EnumColumnType.CT_PRIVATE);
		});
		m_FiltersBackButton.onClick.AddListener(HandleFiltersPanelBack);
		m_FiltersApplyButton.onClick.AddListener(HandleFiltersPanelBack);
		m_FiltersResetButton.onClick.AddListener(HandleFiltersPanelReset);
		m_FiltersPanel.SetActive(value: false);
		m_FiltersShowOnlyFriendGamesToggle.gameObject.SetActive(FriendOnlyFilteringSupported);
		m_RefreshTimer = 0f;
		m_UpdateListTimer = 0f;
		m_RequestRefreshLobby = false;
		m_SearchingForGames.SetActive(value: true);
		m_NoGamesFound.SetActive(value: false);
		bool flag = Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled();
		m_FiltersBackButton.gameObject.SetActive(!flag);
		m_ExitButton.gameObject.SetActive(!flag);
		m_FiltersButton.gameObject.SetActive(!flag);
		m_RefreshButton.gameObject.SetActive(!flag);
		m_JoinGameButton.gameObject.SetActive(!flag);
		m_IsShowingProfanityWarning = false;
		m_pProfanityWarningScreen = null;
		if (m_HasUserAcceptedProfanityWarning)
		{
			if (fromStackPop)
			{
				m_RequestRefreshLobby = true;
			}
			else
			{
				RefreshLobby();
			}
		}
	}

	public override void Hide()
	{
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyListUpdatedEvent.Unsubscribe(RebuildLobbies);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinFailedEvent.Unsubscribe(OnLobbyJoinFailed);
		ClearLobbyList();
		bool flag = Singleton.Manager<ManUI>.inst.IsScreenInStack(this);
		d.Log($"[UIScreenNetworkLobbyList] Hide() - inScreenStack={flag}");
		if (!flag)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.StopListingLobbies();
		}
		m_ExitButton.onClick.RemoveListener(HandleExitButton);
		m_RefreshButton.onClick.RemoveListener(HandleRefreshButton);
		m_FiltersButton.onClick.RemoveListener(HandleFiltersButton);
		m_BanlistButton.onClick.RemoveListener(HandleBanlistButton);
		m_JoinGameButton.onClick.RemoveListener(HandleJoinGameButton);
		m_ColumnMode.onClick.RemoveAllListeners();
		m_ColumnPlayers.onClick.RemoveAllListeners();
		m_ColumnTeams.onClick.RemoveAllListeners();
		m_ColumnFriends.onClick.RemoveAllListeners();
		m_ColumnStatus.onClick.RemoveAllListeners();
		m_ColumnHost.onClick.RemoveAllListeners();
		m_ColumnPing.onClick.RemoveAllListeners();
		m_ColumnLanguage.onClick.RemoveAllListeners();
		m_ColumnPrivate.onClick.RemoveAllListeners();
		m_FiltersBackButton.onClick.RemoveListener(HandleFiltersPanelBack);
		m_FiltersApplyButton.onClick.RemoveListener(HandleFiltersPanelBack);
		m_FiltersResetButton.onClick.RemoveListener(HandleFiltersPanelReset);
		base.Hide();
	}

	public void RefreshLobby()
	{
		if (!Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.IsBusyRequestingLobbies && !m_FiltersPanel.activeInHierarchy && !m_BannedPlayersScreen.gameObject.activeInHierarchy)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.RefreshLobbyList(m_FilterOptions);
			m_SearchComplete = false;
		}
	}

	public void OpenLoadUI()
	{
		Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.LoadSave);
		((UIScreenLoadSave)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.LoadSave)).ShowSavesForType(ManGameMode.GameType.CoOpCreative);
	}

	private void RebuildLobbies(List<LobbyData> lobbyList, bool searchEnd)
	{
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < lobbyList.Count; i++)
		{
			if (lobbyList[i].m_ProtocolVersion == LobbySystem.PROTOCOL_VERSION)
			{
				flag = true;
			}
			else if (lobbyList[i].m_ProtocolVersion == 0)
			{
				flag2 = true;
			}
		}
		if (!flag && flag2)
		{
			return;
		}
		for (int num = m_LobbyList.Count - 1; num >= 0; num--)
		{
			TTNetworkID iDLobby = m_LobbyList[num].lobbyData.m_IDLobby;
			bool flag3 = false;
			for (int j = 0; j < lobbyList.Count; j++)
			{
				if (lobbyList[j].m_IDLobby == iDLobby)
				{
					flag3 = true;
					break;
				}
			}
			if (!flag3)
			{
				RecycleElementAtIndex(num);
			}
		}
		for (int k = 0; k < lobbyList.Count; k++)
		{
			LobbyData lobbyData = lobbyList[k];
			if (lobbyData.m_ProtocolVersion != LobbySystem.PROTOCOL_VERSION)
			{
				continue;
			}
			bool flag4 = false;
			for (int l = 0; l < m_LobbyList.Count; l++)
			{
				if (lobbyData.m_IDLobby == m_LobbyList[l].lobbyData.m_IDLobby)
				{
					m_LobbyList[l].SetLobbyInfo(lobbyData, HandleLobbyElementSelected);
					flag4 = true;
					break;
				}
			}
			if (!flag4)
			{
				NetworkLobbyElement networkLobbyElement = m_LobbyElementPrefab.Spawn();
				networkLobbyElement.trans.SetParent(m_ScrollPanel, worldPositionStays: false);
				m_LobbyList.Add(networkLobbyElement);
				networkLobbyElement.SetLobbyInfo(lobbyData, HandleLobbyElementSelected);
			}
		}
		SortLobbies();
		if (searchEnd)
		{
			m_SearchComplete = true;
		}
		bool flag5 = m_LobbyList.Count > 0;
		m_SearchingForGames.SetActive(!m_SearchComplete && !flag5);
		m_NoGamesFound.SetActive(m_SearchComplete && !flag5);
	}

	private void HandleLobbyElementSelected(NetworkLobbyElement pNLE, bool isOn, bool isDoubleClick)
	{
		if (isOn)
		{
			for (int i = 0; i < m_LobbyList.Count; i++)
			{
				if (m_LobbyList[i] != pNLE && m_LobbyList[i].IsSelected)
				{
					m_LobbyList[i].SetSelected(b: false);
				}
			}
		}
		UpdateJoinGameButtonState();
		if ((isDoubleClick || Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled()) && isOn && m_JoinGameButton.interactable)
		{
			HandleJoinGameButton();
		}
	}

	private void ClearLobbyList()
	{
		for (int num = m_LobbyList.Count - 1; num >= 0; num--)
		{
			RecycleElementAtIndex(num);
		}
	}

	private void RecycleElementAtIndex(int index)
	{
		if (EventSystem.current.currentSelectedGameObject == m_LobbyList[index].gameObject)
		{
			EventSystem.current.SetSelectedGameObject(null);
		}
		m_LobbyList[index].trans.SetParent(null, worldPositionStays: false);
		m_LobbyList[index].Recycle();
		m_LobbyList.RemoveAt(index);
	}

	private void OnLobbyJoinFailed(LobbySystem.LobbyErrorCode errorCode)
	{
		string notification = Singleton.Manager<ManNetworkLobby>.inst.ConvertErrorCode(errorCode);
		Action accept = delegate
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
			RefreshLobby();
		};
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
		(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(notification, accept, localisedString);
		Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
	}

	private void SyncFilterOptionsToPanel()
	{
		int num = m_FilterOptions.m_GameModeIndex;
		if (num < 3)
		{
			num++;
		}
		m_FiltersGameModeDropdown.value = num;
		m_FiltersTeamDropdown.value = m_FilterOptions.m_Team;
		m_FiltersHideFullGamesToggle.isOn = m_FilterOptions.m_HideFullGames != 0;
		m_FiltersShowOnlyFriendGamesToggle.isOn = m_FilterOptions.m_FriendsGamesOnly != 0;
		m_FiltersShowOnlyNearbyGamesToggle.isOn = m_FilterOptions.m_ShowNearbyGamesOnly != 0;
		m_FiltersLanguageDropdown.value = m_FilterOptions.m_LanguageIndex;
		m_FiltersPingDropdown.value = m_FilterOptions.m_PingMaxRequirementIndex;
		m_FiltersHideInProgressGamesToggle.isOn = m_FilterOptions.m_ShowGamesInProgress == 0;
		if (SKU.SupportsMods && m_FiltersShowModdedGames != null)
		{
			m_FiltersShowModdedGames.isOn = m_FilterOptions.m_ShowModdedGames != 0;
		}
	}

	private void HandleFiltersButton()
	{
		SyncFilterOptionsToPanel();
		if (m_MainPaneCanvasGroup != null)
		{
			m_MainPaneCanvasGroup.interactable = false;
		}
		m_FiltersPanel.SetActive(value: true);
		Singleton.Manager<ManUI>.inst.ShowScreenPrompt(ManUI.ScreenType.Pause);
		BlockScreenExit(exitBlocked: true);
	}

	private void HandleBanlistButton()
	{
		if (m_MainPaneCanvasGroup != null)
		{
			m_MainPaneCanvasGroup.interactable = false;
		}
		Singleton.Manager<ManUI>.inst.PushScreen(m_BannedPlayersScreen, ManUI.PauseType.None, asPopup: true);
	}

	private void HandleJoinGameButton()
	{
		int selectedLobbyIndex = GetSelectedLobbyIndex();
		if (selectedLobbyIndex >= 0)
		{
			m_LobbyList[selectedLobbyIndex].JoinLobby();
		}
	}

	public override void ReturnFromPopup()
	{
		if (m_MainPaneCanvasGroup != null)
		{
			m_MainPaneCanvasGroup.interactable = true;
		}
		base.ReturnFromPopup();
	}

	private void HandleExitButton()
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	private void HandleRefreshButton()
	{
		if (!Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.IsBusyRequestingLobbies)
		{
			if (m_LobbyList.Count == 0)
			{
				m_SearchingForGames.SetActive(value: true);
				m_NoGamesFound.SetActive(value: false);
			}
			m_RequestRefreshLobby = true;
		}
	}

	private void CycleSortColumn(int direction)
	{
		m_SortColumn += direction;
		if (m_SortColumn >= EnumColumnType.CT_NUM_COLUMNS)
		{
			m_SortColumn = EnumColumnType.CT_GAME_MODE;
		}
		else if (m_SortColumn < EnumColumnType.CT_GAME_MODE)
		{
			m_SortColumn = EnumColumnType.CT_PRIVATE;
		}
		m_SortAscending = m_SortColumn != EnumColumnType.CT_PLAYERS && m_SortColumn != EnumColumnType.CT_FRIENDS;
		SortLobbies();
	}

	private void HandleChangeSortColumn(EnumColumnType ct)
	{
		if (ct == m_SortColumn)
		{
			m_SortAscending = !m_SortAscending;
		}
		else
		{
			m_SortColumn = ct;
			m_SortAscending = true;
		}
		SortLobbies();
	}

	private void HandleFiltersPanelBack()
	{
		int num = m_FiltersGameModeDropdown.value - 1;
		if (num >= 2)
		{
			num++;
		}
		m_FilterOptions.m_GameModeIndex = num;
		m_FilterOptions.m_Team = m_FiltersTeamDropdown.value;
		m_FilterOptions.m_HideFullGames = (m_FiltersHideFullGamesToggle.isOn ? 1 : 0);
		m_FilterOptions.m_FriendsGamesOnly = ((FriendOnlyFilteringSupported && m_FiltersShowOnlyFriendGamesToggle.isOn) ? 1 : 0);
		m_FilterOptions.m_ShowNearbyGamesOnly = (m_FiltersShowOnlyNearbyGamesToggle.isOn ? 1 : 0);
		m_FilterOptions.m_LanguageIndex = m_FiltersLanguageDropdown.value;
		m_FilterOptions.m_PingMaxRequirementIndex = m_FiltersPingDropdown.value;
		m_FilterOptions.m_ShowGamesInProgress = ((!m_FiltersHideInProgressGamesToggle.isOn) ? 1 : 0);
		if (SKU.SupportsMods && m_FiltersShowModdedGames != null)
		{
			m_FilterOptions.m_ShowModdedGames = (m_FiltersShowModdedGames.isOn ? 1 : 0);
		}
		_ = SKU.SupportsMods;
		if (m_MainPaneCanvasGroup != null)
		{
			m_MainPaneCanvasGroup.interactable = true;
		}
		m_FiltersPanel.SetActive(value: false);
		ClearLobbyList();
		HandleRefreshButton();
		EventSystem.current.SetSelectedGameObject(null);
		Singleton.Manager<ManUI>.inst.ShowScreenPrompt(ManUI.ScreenType.MatchmakingLobbyList);
		Invoke("AllowScreenExit", 0.5f);
	}

	private void AllowScreenExit()
	{
		BlockScreenExit(exitBlocked: false);
	}

	private void HandleFiltersPanelReset()
	{
		m_FilterOptions = new LobbySystem.LobbyFilterOptions();
		SyncFilterOptionsToPanel();
	}

	private int GetSelectedLobbyIndex()
	{
		for (int i = 0; i < m_LobbyList.Count; i++)
		{
			if (m_LobbyList[i].IsSelected)
			{
				return i;
			}
			if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled() && m_LobbyList[i].gameObject == EventSystem.current.currentSelectedGameObject)
			{
				return i;
			}
		}
		return -1;
	}

	private void UpdateJoinGameButtonState()
	{
		bool interactable = GetSelectedLobbyIndex() >= 0;
		m_JoinGameButton.interactable = interactable;
	}

	private void ChangeColumnTitles()
	{
		for (int i = 0; i < m_ColumnHeadings.Length; i++)
		{
			UILocalisedText component = m_ColumnHeadings[i].GetComponent<UILocalisedText>();
			if (component != null)
			{
				string displayString = component.GetDisplayString();
				if (i == (int)m_SortColumn)
				{
					m_ColumnHeadings[i].text = (m_SortAscending ? "↑" : "↓") + displayString;
					m_ColumnHeadings[i].color = Color.green;
				}
				else
				{
					m_ColumnHeadings[i].text = displayString;
					m_ColumnHeadings[i].color = Color.white;
				}
			}
		}
	}

	private void SortLobbies()
	{
		ChangeColumnTitles();
		SortLobbiesUsingCriteria();
		for (int i = 0; i < m_LobbyList.Count; i++)
		{
			m_LobbyList[i].trans.SetSiblingIndex(i);
		}
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled() && !m_FiltersPanel.activeSelf && !m_BannedPlayersScreen.gameObject.activeSelf && GetSelectedLobbyIndex() == -1 && m_LobbyList.Count > 0 && EventSystem.current.currentSelectedGameObject == null)
		{
			EventSystem.current.SetSelectedGameObject(m_LobbyList[0].gameObject);
			UIAutoScrollItemController componentInParents = m_ScrollPanel.GetComponentInParents<UIAutoScrollItemController>();
			if ((bool)componentInParents)
			{
				componentInParents.ScrollToItem(m_LobbyList[0].GetComponent<RectTransform>());
			}
		}
	}

	private string GetGameType(int gm)
	{
		string result = Singleton.Manager<ManNetworkLobby>.inst.AvailableGameTypes[gm].m_Options.m_LocName.Value;
		if (gm == 1)
		{
			result = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NetworkedGameTypes, 5);
		}
		return result;
	}

	private int LobbyListSorterInt(LobbyData la, LobbyData lb, int a, int b)
	{
		if (a < b)
		{
			if (!m_SortAscending)
			{
				return 1;
			}
			return -1;
		}
		if (a > b)
		{
			if (!m_SortAscending)
			{
				return -1;
			}
			return 1;
		}
		return LobbyListSorterHosts(la, lb);
	}

	private int LobbyListSorterString(LobbyData la, LobbyData lb, string a, string b)
	{
		int num = a.CompareTo(b);
		if (num != 0)
		{
			if (!m_SortAscending)
			{
				return -num;
			}
			return num;
		}
		return LobbyListSorterHosts(la, lb);
	}

	private int LobbyListSorterGameMode(LobbyData la, LobbyData lb)
	{
		int gm = la.m_Choices[0];
		int gm2 = lb.m_Choices[0];
		return LobbyListSorterString(la, lb, GetGameType(gm), GetGameType(gm2));
	}

	private int LobbyListSorterPlayers(LobbyData la, LobbyData lb)
	{
		return LobbyListSorterInt(la, lb, la.m_NumUsers, lb.m_NumUsers);
	}

	private int LobbyListSorterFriends(LobbyData la, LobbyData lb)
	{
		return LobbyListSorterInt(la, lb, la.m_NumFriends, lb.m_NumFriends);
	}

	private int LobbyListSorterHosts(LobbyData la, LobbyData lb)
	{
		string lobbyName = la.m_LobbyName;
		string lobbyName2 = lb.m_LobbyName;
		int num = lobbyName.CompareTo(lobbyName2);
		if (num == 0)
		{
			num = la.m_IDLobby.CompareTo(lb.m_IDLobby);
		}
		if (!m_SortAscending)
		{
			return -num;
		}
		return num;
	}

	private int LobbyListSorterPings(LobbyData la, LobbyData lb)
	{
		int num = la.m_PingTimeMS;
		int num2 = lb.m_PingTimeMS;
		if (num <= 0)
		{
			num = 999999;
		}
		if (num2 <= 0)
		{
			num2 = 999999;
		}
		return LobbyListSorterInt(la, lb, num, num2);
	}

	private int LobbyListSorterStatus(LobbyData la, LobbyData lb)
	{
		int a = ((!la.m_GameInProgress) ? 1 : 0);
		int b = ((!lb.m_GameInProgress) ? 1 : 0);
		return LobbyListSorterInt(la, lb, a, b);
	}

	private int LobbyListSorterTeams(LobbyData la, LobbyData lb)
	{
		int a = la.m_Choices[14];
		int b = lb.m_Choices[14];
		return LobbyListSorterInt(la, lb, a, b);
	}

	private int LobbyListSorterLanguage(LobbyData la, LobbyData lb)
	{
		return LobbyListSorterString(la, lb, la.m_Language, lb.m_Language);
	}

	private int LobbyListSorterPrivate(LobbyData la, LobbyData lb)
	{
		int a = ((la.m_Visibility == Lobby.LobbyVisibility.Private) ? 1 : 0);
		int b = ((lb.m_Visibility == Lobby.LobbyVisibility.Private) ? 1 : 0);
		return LobbyListSorterInt(la, lb, a, b);
	}

	private int LobbyListSorter(NetworkLobbyElement a, NetworkLobbyElement b)
	{
		LobbyData lobbyData = a.lobbyData;
		LobbyData lobbyData2 = b.lobbyData;
		switch (m_SortColumn)
		{
		case EnumColumnType.CT_GAME_MODE:
			return LobbyListSorterGameMode(lobbyData, lobbyData2);
		case EnumColumnType.CT_PLAYERS:
			return LobbyListSorterPlayers(lobbyData, lobbyData2);
		case EnumColumnType.CT_FRIENDS:
			return LobbyListSorterFriends(lobbyData, lobbyData2);
		case EnumColumnType.CT_HOST:
			return LobbyListSorterHosts(lobbyData, lobbyData2);
		case EnumColumnType.CT_PING:
			return LobbyListSorterPings(lobbyData, lobbyData2);
		case EnumColumnType.CT_STATUS:
			return LobbyListSorterStatus(lobbyData, lobbyData2);
		case EnumColumnType.CT_TEAMS:
			return LobbyListSorterTeams(lobbyData, lobbyData2);
		case EnumColumnType.CT_LANGUAGE:
			return LobbyListSorterLanguage(lobbyData, lobbyData2);
		case EnumColumnType.CT_PRIVATE:
			return LobbyListSorterPrivate(lobbyData, lobbyData2);
		default:
			d.Assert(condition: false, "LobbyListSorter - Unexpected or missing case!");
			return 0;
		}
	}

	private void SortLobbiesUsingCriteria()
	{
		m_LobbyList.Sort(LobbyListSorter);
	}

	private string getLocalisedString(LocalisationEnums.Multiplayer ee)
	{
		return Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, (int)ee);
	}

	private void Awake()
	{
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>(4);
		list.Add(new Dropdown.OptionData(getLocalisedString(LocalisationEnums.Multiplayer.FilterAll)));
		list.Add(new Dropdown.OptionData(getLocalisedString(LocalisationEnums.Multiplayer.FilterDeathmatchOnly)));
		list.Add(new Dropdown.OptionData(getLocalisedString(LocalisationEnums.Multiplayer.FilterCoOpCreativeOnly)));
		list.Add(new Dropdown.OptionData(getLocalisedString(LocalisationEnums.Multiplayer.FilterCoOpCampaignOnly)));
		List<Dropdown.OptionData> list2 = new List<Dropdown.OptionData>(2);
		list2.Add(new Dropdown.OptionData(getLocalisedString(LocalisationEnums.Multiplayer.FilterAllTeamTypes)));
		list2.Add(new Dropdown.OptionData(getLocalisedString(LocalisationEnums.Multiplayer.FilterAllVsAllOnly)));
		list2.Add(new Dropdown.OptionData(getLocalisedString(LocalisationEnums.Multiplayer.FilterTeamvVsTeamOnly)));
		List<Dropdown.OptionData> list3 = new List<Dropdown.OptionData>(2);
		list3.Add(new Dropdown.OptionData(getLocalisedString(LocalisationEnums.Multiplayer.FilterMatchMine)));
		list3.Add(new Dropdown.OptionData(getLocalisedString(LocalisationEnums.Multiplayer.FilterAllLanguages)));
		m_FiltersGameModeDropdown.AddOptions(list);
		m_FiltersTeamDropdown.AddOptions(list2);
		m_FiltersLanguageDropdown.AddOptions(list3);
		List<Dropdown.OptionData> list4 = new List<Dropdown.OptionData>(LobbySystem.PingMaxRequirements.Length);
		string localisedString = getLocalisedString(LocalisationEnums.Multiplayer.FilterAny);
		string localisedString2 = getLocalisedString(LocalisationEnums.Multiplayer.FilterPingValueOrLess);
		for (int i = 0; i < LobbySystem.PingMaxRequirements.Length; i++)
		{
			string text = ((i == 0) ? localisedString : string.Format(localisedString2, LobbySystem.PingMaxRequirements[i]));
			list4.Add(new Dropdown.OptionData(text));
		}
		m_FiltersPingDropdown.AddOptions(list4);
		m_ColumnHeadings[0] = m_ColumnMode.GetComponentInChildren<Text>();
		m_ColumnHeadings[1] = m_ColumnPlayers.GetComponentInChildren<Text>();
		m_ColumnHeadings[3] = m_ColumnFriends.GetComponentInChildren<Text>();
		m_ColumnHeadings[5] = m_ColumnHost.GetComponentInChildren<Text>();
		m_ColumnHeadings[6] = m_ColumnPing.GetComponentInChildren<Text>();
		m_ColumnHeadings[7] = m_ColumnLanguage.GetComponentInChildren<Text>();
		m_ColumnHeadings[2] = m_ColumnTeams.GetComponentInChildren<Text>();
		m_ColumnHeadings[4] = m_ColumnStatus.GetComponentInChildren<Text>();
		m_ColumnHeadings[8] = m_ColumnPrivate.GetComponentInChildren<Text>();
		if (SKU.XboxOneUI && m_FilterShowOnlyFriendGamesContainer.IsNotNull())
		{
			m_FilterShowOnlyFriendGamesContainer.SetActive(value: false);
		}
		if (SKU.IsNetEase)
		{
			Singleton.Manager<ManNetEase>.inst.UpdateFriends();
		}
		m_BannedPlayersScreen = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.BannedPlayerList);
	}

	private void UpdateFilterPanel()
	{
		if (m_FiltersPanel.activeSelf && (Singleton.Manager<ManInput>.inst.GetButtonDown(24) || Singleton.Manager<ManInput>.inst.GetButtonDown(22)))
		{
			GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
			if (!(currentSelectedGameObject != null) || !UIHelpers.HideDropdown(currentSelectedGameObject.GetComponent<Dropdown>()))
			{
				HandleFiltersPanelBack();
			}
		}
	}

	private void UpdateProfanityWarningCheck()
	{
		d.Assert(m_IsShowingProfanityWarning);
		if (m_pProfanityWarningScreen != null && !m_pProfanityWarningScreen.gameObject.activeInHierarchy)
		{
			if (m_pProfanityWarningScreen.WasAccepted())
			{
				m_HasUserAcceptedProfanityWarning = true;
				RefreshLobby();
			}
			else
			{
				HandleExitButton();
			}
			m_IsShowingProfanityWarning = false;
		}
	}

	private void Update()
	{
		if (!m_HasUserAcceptedProfanityWarning && !m_IsShowingProfanityWarning && m_pProfanityWarningScreen == null && !SKU.ConsoleUI && !SKU.IsNetEase)
		{
			m_IsShowingProfanityWarning = true;
			m_pProfanityWarningScreen = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.ProfanityWarningScreen) as UIScreenProfanityWarning;
			Singleton.Manager<ManUI>.inst.PushScreenAsPopup(m_pProfanityWarningScreen);
			return;
		}
		if (m_IsShowingProfanityWarning)
		{
			UpdateProfanityWarningCheck();
			return;
		}
		UpdateFilterPanel();
		UpdateJoinGameButtonState();
		if (!m_FiltersPanel.activeSelf && !m_BannedPlayersScreen.gameObject.activeSelf)
		{
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(58))
			{
				ClearLobbyList();
				HandleRefreshButton();
			}
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(42))
			{
				CycleSortColumn(-1);
			}
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(41))
			{
				CycleSortColumn(1);
			}
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(57))
			{
				HandleFiltersButton();
			}
		}
		if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.ShouldAbortLobbyScreen)
		{
			Singleton.Manager<ManUI>.inst.GoBack();
			return;
		}
		float num = 1f;
		if (m_RequestRefreshLobby && m_RefreshTimer >= num)
		{
			if (!Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.IsBusyRequestingLobbies)
			{
				RefreshLobby();
			}
			m_RequestRefreshLobby = false;
			m_RefreshTimer = 0f;
			m_UpdateListTimer = 0f;
		}
		float num2 = 5f;
		if (m_RefreshTimer >= num2 && !Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.IsBusyRequestingLobbies)
		{
			RefreshLobby();
			m_RefreshTimer = 0f;
			m_UpdateListTimer = 0f;
		}
		if (m_UpdateListTimer > 1f && !Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.IsBusyRequestingLobbies)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.UpdateCurrentLobbyList();
			m_UpdateListTimer = 0f;
		}
		m_RefreshTimer += Time.deltaTime;
		m_UpdateListTimer += Time.deltaTime;
	}
}
