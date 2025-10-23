#define UNITY_EDITOR
using System.Collections.Generic;
using TMPro;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIScreenControllerDisconnected : UIScreen
{
	[SerializeField]
	private TextMeshProUGUI m_Message;

	[SerializeField]
	private TextMeshProUGUI m_AcceptJoypad;

	private UIScreen m_GoToScreen;

	private List<UIScreen> m_Popups = new List<UIScreen>();

	private bool m_Blocking;

	public void AddPopup(UIScreen screen)
	{
		d.Log("[UIScreenControllerDisconnected] Adding popup screen type: " + screen.Type);
		m_Popups.Add(screen);
	}

	public void GoToScreen(UIScreen screen)
	{
		d.Log("[UIScreenControllerDisconnected] Adding goto screen type: " + screen.Type);
		d.Assert(m_GoToScreen == null);
		m_GoToScreen = screen;
		m_Popups.Clear();
	}

	public void ExitAllScreens()
	{
		m_GoToScreen = null;
		m_Popups.Clear();
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		SetText();
		BlockScreenExit(exitBlocked: true);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.FullscreenUI);
		EventSystem.current.SetSelectedGameObject(base.gameObject);
		if (!m_Blocking)
		{
			d.Log("[UIScreenControllerDisconnected] Disabling screen changes");
			m_Blocking = true;
			Singleton.Manager<ManUI>.inst.SetEnableScreenChange(enable: false);
		}
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.SetJoypadDisconnected(dis: true);
	}

	public override void Hide()
	{
		base.Hide();
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.SetJoypadDisconnected(dis: false);
		BlockScreenExit(exitBlocked: false);
		EventSystem.current.SetSelectedGameObject(null);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.FullscreenUI);
		if ((bool)m_GoToScreen)
		{
			d.Log("[UIScreenControllerDisconnected] Restoring goto screen: " + m_GoToScreen.Type);
			Singleton.Manager<ManUI>.inst.GoToScreen(m_GoToScreen.Type);
		}
		for (int i = 0; i < m_Popups.Count; i++)
		{
			d.Log("[UIScreenControllerDisconnected] Pushing popup screen: " + m_Popups[i].Type);
			Singleton.Manager<ManUI>.inst.PushScreenAsPopup(m_Popups[i]);
		}
		ExitAllScreens();
	}

	private void SetText()
	{
		string text = "";
		LocalisationEnums.NewMenuMain stringID;
		if (!SKU.PS4UI)
		{
			stringID = ((!SKU.XboxOneUI) ? LocalisationEnums.NewMenuMain.controllerDisconnected : LocalisationEnums.NewMenuMain.controllerDisconnected_XboxOne);
		}
		else
		{
			stringID = LocalisationEnums.NewMenuMain.controllerDisconnected_PS4;
			text = "<color=#FFE422>" + ManPS4.GetPlayerName() + "</color>\n";
		}
		m_Message.text = text + Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, (int)stringID);
		m_AcceptJoypad.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 98);
	}

	private void Update()
	{
		if (Singleton.Manager<ManInput>.inst.PollPlayerNewJoystick() || Singleton.Manager<ManInput>.inst.GetButtonDown(21) || Singleton.Manager<ManInput>.inst.GetAnyButtonDown())
		{
			if (m_Blocking)
			{
				d.Log("[UIScreenControllerDisconnected] Enabling screen changes");
				m_Blocking = false;
				Singleton.Manager<ManUI>.inst.SetEnableScreenChange(enable: true);
			}
			Singleton.Manager<ManUI>.inst.PopScreen();
		}
	}
}
