#define UNITY_EDITOR
using System;
using TerraTech.Network;
using UnityEngine;

public class UIButtonHostLobby : MonoBehaviour
{
	[SerializeField]
	private MultiplayerModeType m_TargetLobbyType;

	[SerializeField]
	private Lobby.LobbyVisibility m_LobbyVisibility = Lobby.LobbyVisibility.Public;

	[SerializeField]
	private bool m_StartGameModeOnCreate = true;

	public Event<bool> OnLobbyCreatedEvent;

	private bool m_HasRequestPending;

	public void OnHostLobby()
	{
		if (m_HasRequestPending)
		{
			d.Log("[UIButtonHostLobby] - Trying to Create Game - Aborted as m_HasRequestPending == true");
			SendLobbyCreatedEvent(success: false);
			return;
		}
		d.Log("[UIButtonHostLobby] - Trying to Create Game");
		if (!Singleton.Manager<ManNetworkLobby>.inst.Inited)
		{
			_showLobbySystemNotInitialisedError();
			SendLobbyCreatedEvent(success: false);
			return;
		}
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.ClearPingRequests();
		d.Log("[UIButtonHostLobby] - Trying to Create Host Lobby");
		m_HasRequestPending = true;
		d.Log("[UIButtonHostLobby] - Trying to Create Host Lobby with seed: " + Singleton.Manager<ManNetwork>.inst.WorldSeed + " biome: " + Singleton.Manager<ManNetwork>.inst.BiomeChoice + " visibility: " + m_LobbyVisibility);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CreateLobby(m_TargetLobbyType, m_LobbyVisibility);
		d.Log("[UIButtonHostLobby] - Waiting to join Host Lobby");
	}

	public void SetLobbyVisibility(Lobby.LobbyVisibility visibility)
	{
		m_LobbyVisibility = visibility;
	}

	private void _showLobbySystemNotInitialisedError()
	{
		d.LogErrorFormat("ManNetworkLobby LobbySystem is NOT initialised! Sku: {0}", SKU.CurrentBuildType);
		if (SKU.IsSteam)
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamLobby, 3);
			_showPopup(localisedString);
		}
		else if (SKU.IsEpicGS)
		{
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamLobby, 3);
			_showPopup(localisedString2);
		}
	}

	private void OnLobbyJoined(Lobby lobby)
	{
		if (m_HasRequestPending)
		{
			d.Log("[UIButtonHostLobby] OnLobbyJoined: LobbyName=" + lobby.Name);
			m_HasRequestPending = false;
			if (m_StartGameModeOnCreate)
			{
				Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.TriggerGameStart();
			}
		}
		SendLobbyCreatedEvent(success: true);
	}

	private void OnLobbyJoinFailed(LobbySystem.LobbyErrorCode errorCode)
	{
		if (m_HasRequestPending)
		{
			d.LogError("[UIButtonHostLobby] OnLobbyJoinFailed - Result=" + errorCode);
			m_HasRequestPending = false;
			if (errorCode != LobbySystem.LobbyErrorCode.Cancelled)
			{
				string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamLobby, 2);
				_showPopup(localisedString);
			}
		}
		SendLobbyCreatedEvent(success: true);
	}

	private void OnLobbyCreateFailed(LobbySystem.LobbyErrorCode errorCode)
	{
		if (m_HasRequestPending)
		{
			d.LogError("[UIButtonHostLobby] OnLobbyCreateFailed - Result=" + errorCode);
			m_HasRequestPending = false;
			if (errorCode != LobbySystem.LobbyErrorCode.Cancelled)
			{
				string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamLobby, 1);
				_showPopup(localisedString);
			}
		}
		SendLobbyCreatedEvent(success: true);
	}

	private void SendLobbyCreatedEvent(bool success)
	{
		OnLobbyCreatedEvent.Send(success);
		OnLobbyCreatedEvent.Clear();
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

	private void Awake()
	{
		m_HasRequestPending = false;
		d.Assert(Singleton.Manager<ManNetworkLobby>.inst.LobbySystem, "No LobbySystem available at startup, class will not function correctly!");
		if ((bool)Singleton.Manager<ManNetworkLobby>.inst.LobbySystem)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinedEvent.Subscribe(OnLobbyJoined);
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyCreateFailedEvent.Subscribe(OnLobbyCreateFailed);
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinFailedEvent.Subscribe(OnLobbyJoinFailed);
		}
	}

	private void OnDestroy()
	{
		if ((bool)Singleton.Manager<ManNetworkLobby>.inst.LobbySystem)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinedEvent.Unsubscribe(OnLobbyJoined);
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyCreateFailedEvent.Unsubscribe(OnLobbyCreateFailed);
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyJoinFailedEvent.Unsubscribe(OnLobbyJoinFailed);
		}
	}
}
