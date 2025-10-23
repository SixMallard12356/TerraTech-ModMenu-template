#define UNITY_EDITOR
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPlayerInfoHUD : UIHUDElement
{
	[SerializeField]
	private UIPlayerElement m_PlayerElementPrefab;

	[SerializeField]
	private RectTransform m_View;

	[SerializeField]
	private Button m_CloseButton;

	[SerializeField]
	private Button m_TechManButton;

	private List<UIPlayerElement> m_PlayerElements = new List<UIPlayerElement>();

	private bool m_PlayerSelected;

	private UIPlayerElement m_CurrentSelectedElement;

	private UIPlayerElement m_CurrentHighlightedElement;

	public void SelectPlayer(UIPlayerElement element)
	{
		m_CurrentSelectedElement = element;
		if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			m_PlayerSelected = true;
			m_CurrentSelectedElement.SetChildNavigationEntryPoint(set: true);
		}
	}

	public override void Show(object context)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			base.Show(context);
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.UIMissionLog);
			if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad() && (bool)m_CloseButton)
			{
				m_CloseButton.gameObject.SetActive(value: false);
			}
			int numPlayers = Singleton.Manager<ManNetwork>.inst.GetNumPlayers();
			for (int i = 0; i < numPlayers; i++)
			{
				NetPlayer player = Singleton.Manager<ManNetwork>.inst.GetPlayer(i);
				UIPlayerElement component = m_PlayerElementPrefab.transform.Spawn().GetComponent<UIPlayerElement>();
				component.transform.SetParent(m_View, worldPositionStays: false);
				component.Init(player);
				component.HoverEvent.Subscribe(OnElementHovered);
				m_PlayerElements.Add(component);
				d.Log("[PlayerList] Player Added in Show Name: " + player.name + "Player ID: " + player.PlayerID + " lobby ID: " + player.GetPlayerIDInLobby());
			}
			RecalculateGamepadNavigation();
			Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Unsubscribe(OnPlayerAdded);
			Singleton.Manager<ManNetwork>.inst.OnPlayerRemoved.Unsubscribe(OnPlayerRemoved);
			Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Subscribe(OnPlayerAdded);
			Singleton.Manager<ManNetwork>.inst.OnPlayerRemoved.Subscribe(OnPlayerRemoved);
		}
		else
		{
			d.LogError("We tried to open UIPlayerInfoHUD when in singleplayer! This screen is multiplayer only.");
		}
	}

	private void RecalculateGamepadNavigation()
	{
		UIPlayerElement prevNav = null;
		for (int i = 0; i < m_PlayerElements.Count; i++)
		{
			UIPlayerElement uIPlayerElement = m_PlayerElements[i];
			uIPlayerElement.SetupNavigationToPrevNeighbour(prevNav);
			prevNav = uIPlayerElement;
		}
		if (m_PlayerElements.Count > 0)
		{
			m_PlayerElements[0].SetAsGamepadEntryPoint(set: true);
		}
	}

	public void OnPlayerAdded(NetPlayer player)
	{
		if (!base.IsShowing)
		{
			return;
		}
		d.Log("[PlayerList] Player Added Name: " + player.name + "Player ID: " + player.PlayerID + " lobby ID: " + player.GetPlayerIDInLobby());
		foreach (UIPlayerElement playerElement in m_PlayerElements)
		{
			if (playerElement.GetPlayer() == player)
			{
				OnPlayerRemoved(playerElement.GetPlayer());
				d.LogError("Called AddPlayer on player who is already in UIPlayerInfoHUD");
			}
		}
		UIPlayerElement component = m_PlayerElementPrefab.transform.Spawn().GetComponent<UIPlayerElement>();
		component.transform.SetParent(m_View, worldPositionStays: false);
		component.Init(player);
		component.HoverEvent.Subscribe(OnElementHovered);
		m_PlayerElements.Add(component);
		RecalculateGamepadNavigation();
	}

	public void OnPlayerRemoved(NetPlayer player)
	{
		for (int i = 0; i < m_PlayerElements.Count; i++)
		{
			UIPlayerElement uIPlayerElement = m_PlayerElements[i];
			if (!(player == uIPlayerElement.GetPlayer()))
			{
				continue;
			}
			d.Log("[PlayerList] Player Removed Name: " + player.name + "Player ID: " + player.PlayerID + " lobby ID: " + player.GetPlayerIDInLobby());
			if (uIPlayerElement == m_CurrentSelectedElement)
			{
				m_PlayerSelected = false;
				m_CurrentSelectedElement = null;
				int index = Mathf.Clamp(i, 0, m_PlayerElements.Count - 2);
				if (m_PlayerElements.Count > 1)
				{
					m_PlayerElements[index].EnsureSelection();
				}
				else
				{
					EventSystem.current.SetSelectedGameObject(null);
				}
			}
			uIPlayerElement.Cleanup();
			uIPlayerElement.transform.Recycle();
			m_PlayerElements.RemoveAt(i);
			RecalculateGamepadNavigation();
			break;
		}
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Unsubscribe(OnPlayerAdded);
		Singleton.Manager<ManNetwork>.inst.OnPlayerRemoved.Unsubscribe(OnPlayerRemoved);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.FullscreenUI);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.UIMissionLog);
		Singleton.Manager<ManUI>.inst.HideScreenPrompt();
		m_PlayerSelected = false;
		m_CurrentSelectedElement = null;
		UpdateSelectionBorder(null);
		m_CurrentHighlightedElement = null;
		d.Log("[PlayerList] Player List Cleared");
		for (int num = m_PlayerElements.Count - 1; num >= 0; num--)
		{
			m_PlayerElements[num].Cleanup();
			m_PlayerElements[num].transform.Recycle();
		}
		m_PlayerElements.Clear();
		EventSystem.current.SetSelectedGameObject(null);
	}

	public void CloseMenu()
	{
		HideSelf();
	}

	public void SwapToTechList()
	{
		HideSelf();
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechManager);
	}

	public override bool HandleCustomEscapeKeyAction()
	{
		bool result = false;
		if (base.IsVisible)
		{
			if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
			{
				if (!(EventSystem.current.currentSelectedGameObject != null) || !(EventSystem.current.currentSelectedGameObject.GetComponent<Dropdown>() != null))
				{
					if (m_PlayerSelected)
					{
						m_PlayerSelected = false;
						if (m_CurrentSelectedElement.IsNotNull())
						{
							m_CurrentSelectedElement.EnsureSelection();
							m_CurrentSelectedElement.SetChildNavigationEntryPoint(set: false);
						}
					}
					else
					{
						EventSystem.current.SetSelectedGameObject(null);
						HideSelf();
					}
				}
			}
			else
			{
				EventSystem.current.SetSelectedGameObject(null);
				HideSelf();
			}
			result = true;
		}
		return result;
	}

	private void UpdateSelectionBorder(UIPlayerElement selectedEntry)
	{
		if (selectedEntry != m_CurrentHighlightedElement)
		{
			if (m_CurrentHighlightedElement.IsNotNull())
			{
				m_CurrentHighlightedElement.SetSelectionBorderVisible(visible: false);
			}
			m_CurrentHighlightedElement = selectedEntry;
			if (m_CurrentHighlightedElement.IsNotNull())
			{
				m_CurrentHighlightedElement.EnsureSelection();
				m_CurrentHighlightedElement.SetSelectionBorderVisible(visible: true);
			}
		}
	}

	private void OnElementHovered(UIPlayerElement entry, bool gainedFocus)
	{
		if (gainedFocus)
		{
			UpdateSelectionBorder(entry);
		}
		else if (entry == m_CurrentHighlightedElement)
		{
			UpdateSelectionBorder(null);
		}
	}

	private void OnSpawn()
	{
		AddElementToGroup(ManHUD.HUDGroup.GamepadQuickMenuHUDElements);
	}

	private void Update()
	{
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(41, ControllerType.Joystick) || Singleton.Manager<ManInput>.inst.GetButtonDown(42, ControllerType.Joystick))
		{
			SwapToTechList();
		}
	}
}
