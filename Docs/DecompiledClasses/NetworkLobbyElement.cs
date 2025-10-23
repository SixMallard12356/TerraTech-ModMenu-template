#define UNITY_EDITOR
using System;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.UI;

public class NetworkLobbyElement : MonoBehaviour
{
	public delegate void RespondToSelection(NetworkLobbyElement pMe, bool isOn, bool isDoubleClick);

	[SerializeField]
	private GameObject m_HighlightToggle;

	[SerializeField]
	private Text m_GameMode;

	[SerializeField]
	private Text m_Status;

	[SerializeField]
	private Text m_LobbyName;

	[SerializeField]
	private Text m_MapName;

	[SerializeField]
	private Text m_PlayerCount;

	[SerializeField]
	private Text m_FriendCount;

	[SerializeField]
	private Text m_Teams;

	[SerializeField]
	private Text m_PingTimeMS;

	[SerializeField]
	private Text m_Language;

	[SerializeField]
	private GameObject m_PrivateLockImage;

	[SerializeField]
	private GameObject m_ModdedImage;

	[SerializeField]
	private Text m_ModCount;

	private LobbyData m_LobbyData;

	private Transform m_Transform;

	private RespondToSelection m_SelectionDelegate;

	private DoubleClickListener m_DoubleClickListener;

	public Transform trans => m_Transform;

	public LobbyData lobbyData => m_LobbyData;

	public bool IsSelected => m_HighlightToggle.gameObject.activeInHierarchy;

	public void SetLobbyInfo(LobbyData lobbyInfo, RespondToSelection pResp)
	{
		m_LobbyData = lobbyInfo;
		m_SelectionDelegate = pResp;
		UpdateLobbyInfo(m_LobbyData);
		Button component = GetComponent<Button>();
		if ((bool)component)
		{
			component.onClick.RemoveListener(HandleClick);
			component.onClick.AddListener(HandleClick);
		}
	}

	public void SetSelected(bool b)
	{
		m_HighlightToggle.gameObject.SetActive(b);
	}

	public void HandleClick()
	{
		m_HighlightToggle.SetActive(value: true);
		if (m_SelectionDelegate != null)
		{
			bool isDoubleClick = m_DoubleClickListener.WasClickEventDoubleClick();
			m_SelectionDelegate(this, isOn: true, isDoubleClick);
		}
	}

	private void UpdateLobbyInfo(LobbyData lobbyInfo)
	{
		int num = lobbyInfo.m_Choices[0];
		if (num >= Singleton.Manager<ManNetworkLobby>.inst.AvailableGameTypes.Length)
		{
			return;
		}
		NetOptionsAsset netOptionsAsset = Singleton.Manager<ManNetworkLobby>.inst.AvailableGameTypes[num];
		MultiplayerModeType multiplayerModeType = (MultiplayerModeType)num;
		string text = netOptionsAsset.m_Options.m_LocName.Value;
		if (multiplayerModeType == MultiplayerModeType.CoOpCreative)
		{
			text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NetworkedGameTypes, 5);
		}
		m_MapName.text = "";
		if (lobbyInfo.LevelDataChoice < netOptionsAsset.m_Options.m_LevelData.Count)
		{
			NetLevelData netLevelData = netOptionsAsset.m_Options.m_LevelData[lobbyInfo.LevelDataChoice];
			if (netLevelData != null)
			{
				m_MapName.text = netLevelData.m_levelName.Value;
			}
		}
		d.Assert(m_MapName.text.Length > 0, "Unknown map name found in lobby browser.");
		m_MapName.text = "";
		if (lobbyInfo.LevelDataChoice < netOptionsAsset.m_Options.m_LevelData.Count)
		{
			NetLevelData netLevelData2 = netOptionsAsset.m_Options.m_LevelData[lobbyInfo.LevelDataChoice];
			if (netLevelData2 != null)
			{
				m_MapName.text = netLevelData2.m_levelName.Value;
			}
		}
		d.Assert(m_MapName.text.Length > 0, "Unknown map name found in lobby browser.");
		string text2 = "";
		switch (multiplayerModeType)
		{
		case MultiplayerModeType.CoOpCreative:
		case MultiplayerModeType.CoOpCampaign:
			text2 = "";
			m_MapName.text = "";
			break;
		case MultiplayerModeType.Deathmatch:
			text2 = ((lobbyInfo.m_Choices[14] <= 0) ? Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30) : Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29));
			break;
		case MultiplayerModeType.KingAnton:
			text2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30);
			break;
		default:
			d.LogError("Unsupported gametype");
			break;
		}
		m_LobbyName.text = lobbyInfo.m_LobbyName;
		m_PlayerCount.text = $"{lobbyInfo.m_NumUsers}/{lobbyInfo.m_MaxUserLimit}";
		m_PingTimeMS.text = ((lobbyInfo.m_PingTimeMS >= 0) ? lobbyInfo.m_PingTimeMS.ToString() : "????");
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 40);
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 39);
		m_Status.text = (lobbyInfo.m_GameInProgress ? localisedString : localisedString2);
		m_Language.text = lobbyInfo.m_Language;
		m_FriendCount.text = ((lobbyInfo.m_NumFriends > 0) ? lobbyInfo.m_NumFriends.ToString() : "");
		m_GameMode.text = text;
		m_Teams.text = text2;
		m_PrivateLockImage.SetActive(lobbyInfo.m_Visibility == Lobby.LobbyVisibility.Private);
		if (m_ModdedImage != null)
		{
			m_ModCount.text = $"{lobbyInfo.NumMods}";
			m_ModdedImage.SetActive(lobbyInfo.NumMods > 0);
		}
	}

	public void JoinLobby()
	{
		if (LobbySystem.PROTOCOL_VERSION == m_LobbyData.m_ProtocolVersion)
		{
			if (Singleton.Manager<ManNetworkLobby>.inst.IsJoinOrCreateLobbyRequestActive())
			{
				return;
			}
			if (lobbyData.NumMods > 0)
			{
				TTNetworkID lobbyID = m_LobbyData.m_IDLobby;
				(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuJoinMultiplayer, 7), delegate
				{
					Singleton.Manager<ManUI>.inst.PopScreen();
					DoJoin(lobbyID);
				}, delegate
				{
					Singleton.Manager<ManUI>.inst.PopScreen();
				}, Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4), Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 5));
				Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
			}
			else
			{
				DoJoin(m_LobbyData.m_IDLobby);
			}
		}
		else
		{
			string msg = "Protocol version error!\n\nLobby Protocol=0x" + m_LobbyData.m_ProtocolVersion.ToString("X8") + "\nLocal Protocol=0x" + LobbySystem.PROTOCOL_VERSION.ToString("X8");
			d.LogError("Protocol Version Error! Lobby=" + m_LobbyData.m_ProtocolVersion.ToString("X8") + " Local=" + LobbySystem.PROTOCOL_VERSION.ToString("X8"));
			_showPopup(msg);
		}
	}

	private void DoJoin(TTNetworkID lobbyID)
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 108);
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		uIScreenNotifications.Set(localisedString, null, null);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
		Singleton.Manager<ManNetworkLobby>.inst.RequestLobbyJoin(lobbyID);
	}

	private void OnLobbyJoined(Lobby lobby)
	{
	}

	private void _showPopup(string msg)
	{
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		Action accept = delegate
		{
			Singleton.Manager<ManUI>.inst.RemovePopup();
		};
		uIScreenNotifications.Set(msg, accept, "Continue");
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
	}

	private void OnPool()
	{
		m_Transform = base.transform;
		m_DoubleClickListener = new DoubleClickListener();
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinedEvent.Subscribe(OnLobbyJoined);
		m_HighlightToggle.SetActive(value: false);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinedEvent.Unsubscribe(OnLobbyJoined);
		Button component = GetComponent<Button>();
		if ((bool)component)
		{
			component.onClick.RemoveListener(HandleClick);
		}
	}
}
