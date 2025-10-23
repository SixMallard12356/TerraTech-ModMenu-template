using System.Collections.Generic;
using System.Linq;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBannedPlayerList : UIScreen
{
	[SerializeField]
	private UIBannedPlayerListElement m_EntryPrefab;

	[SerializeField]
	private Transform m_EntriesPane;

	private Dictionary<TTNetworkID, UIBannedPlayerListElement> m_Entries = new Dictionary<TTNetworkID, UIBannedPlayerListElement>();

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(22, OnCancelPressed);
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(24, OnCancelPressed);
		foreach (PersistentPlayerID bannedPlayer in Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.BanList.BannedPlayers)
		{
			TTNetworkID tTNetworkIDFromPersistent = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetTTNetworkIDFromPersistent(bannedPlayer);
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.Platform_GetUserName(tTNetworkIDFromPersistent, OnPlayerNameReceived);
		}
	}

	public override void Hide()
	{
		foreach (UIBannedPlayerListElement value in m_Entries.Values)
		{
			value.transform.SetParent(null, worldPositionStays: false);
			value.Recycle();
		}
		m_Entries.Clear();
		RefreshNavigation();
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(22, OnCancelPressed);
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(24, OnCancelPressed);
		base.Hide();
	}

	private void CreateElement(TTNetworkID playerID, string displayName)
	{
		UIBannedPlayerListElement uIBannedPlayerListElement = m_EntryPrefab.Spawn();
		uIBannedPlayerListElement.Set(playerID, displayName, UnbanPlayer);
		uIBannedPlayerListElement.transform.SetParent(m_EntriesPane, worldPositionStays: false);
		if (m_Entries.Count == 0 && Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			uIBannedPlayerListElement.SetSelected();
		}
		m_Entries.Add(playerID, uIBannedPlayerListElement);
		RefreshNavigation();
	}

	private void UnbanPlayer(TTNetworkID playerID)
	{
		if (!m_Entries.TryGetValue(playerID, out var value))
		{
			return;
		}
		PersistentPlayerID persistentPlayerID = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetPersistentPlayerID(playerID);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.BanList.SetPlayerBanned(persistentPlayerID, banned: false);
		int siblingIndex = value.transform.GetSiblingIndex();
		value.transform.SetParent(null, worldPositionStays: false);
		value.Recycle();
		m_Entries.Remove(playerID);
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			if (m_Entries.Count > 0)
			{
				int index = Mathf.Clamp(siblingIndex, 0, m_EntriesPane.childCount - 1);
				Transform newTargetTransform = m_EntriesPane.GetChild(index);
				m_Entries.Values.First((UIBannedPlayerListElement r) => r.transform == newTargetTransform).SetSelected();
			}
			else
			{
				EventSystem.current.SetSelectedGameObject(m_DefaultUIElement);
			}
		}
		RefreshNavigation();
	}

	private void RefreshNavigation()
	{
		Button component = m_DefaultUIElement.GetComponent<Button>();
		Navigation navigation = component.navigation;
		navigation.selectOnDown = null;
		if (m_Entries.Count > 0)
		{
			Transform topEntryTransform = m_EntriesPane.GetChild(0);
			navigation.selectOnDown = m_Entries.Values.First((UIBannedPlayerListElement r) => r.transform == topEntryTransform).UnbanButton;
		}
		component.navigation = navigation;
	}

	private void OnPlayerNameReceived(TTNetworkID playerID, string userName)
	{
		if (base.state == State.Show)
		{
			CreateElement(playerID, userName);
		}
	}

	private void OnCancelPressed(PayloadUIEventData evt)
	{
		evt.Use();
		Singleton.Manager<ManUI>.inst.GoBack();
	}

	private void OnPool()
	{
		SetupExitButton();
	}
}
