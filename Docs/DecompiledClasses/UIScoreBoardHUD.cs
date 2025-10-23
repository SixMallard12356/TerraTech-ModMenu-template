#define UNITY_EDITOR
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScoreBoardHUD : UIHUDElement
{
	[SerializeField]
	private UIScoreBoardEntry m_EntryPrefab;

	[SerializeField]
	private UIScoreBoardTeamEntry m_TeamEntryPrefab;

	[SerializeField]
	private Transform m_EntriesPane;

	[SerializeField]
	private GameObject m_HeadingKills;

	[SerializeField]
	private GameObject m_HeadingDeaths;

	[SerializeField]
	private Text m_HeadingRestartTime;

	[SerializeField]
	private Button m_ServerStartButton;

	[SerializeField]
	private Button m_ServerOptionsButton;

	[SerializeField]
	private Button m_ExitButton;

	[SerializeField]
	private Text m_Title;

	[SerializeField]
	private GameObject m_ContextButton;

	[SerializeField]
	private LocalisedString m_NextMatchFormatString;

	private List<UIScoreBoardEntry> s_SortList = new List<UIScoreBoardEntry>();

	private UIElementCache<UIScoreBoardEntry> m_EntryPool;

	private UIElementCache<UIScoreBoardTeamEntry> m_TeamEntryPool;

	private NetPlayer m_ContextPlayer;

	private bool m_Dirty;

	public override void Show(object context)
	{
		d.Log("UIScoreBoardHUD.Show");
		if (!base.IsVisible)
		{
			Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Subscribe(OnPlayerAdded);
			Singleton.Manager<ManNetwork>.inst.OnPlayerRemoved.Subscribe(OnPlayerRemoved);
			for (int i = 0; i < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); i++)
			{
				OnPlayerAdded(Singleton.Manager<ManNetwork>.inst.GetPlayer(i));
			}
		}
		m_Dirty = false;
		base.Show(context);
		NetController.ScorePolicy currentScorePolicy = Singleton.Manager<ManNetwork>.inst.NetController.CurrentScorePolicy;
		switch (currentScorePolicy)
		{
		case NetController.ScorePolicy.GameTime:
		case NetController.ScorePolicy.SetTime:
		case NetController.ScorePolicy.NumWaves:
			m_HeadingKills.SetActive(value: false);
			m_HeadingDeaths.SetActive(value: false);
			break;
		case NetController.ScorePolicy.Kills:
		case NetController.ScorePolicy.KillMinusDeath:
			m_HeadingKills.SetActive(value: true);
			m_HeadingDeaths.SetActive(value: true);
			break;
		default:
			d.AssertFormat(false, "UIScoreBoardHUD.Show has unhandled score policy {0}", currentScorePolicy);
			break;
		}
		switch (Singleton.Manager<ManNetwork>.inst.NetController.GameModeType)
		{
		case MultiplayerModeType.Deathmatch:
			m_Title.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NetworkedGameTypes, 0);
			break;
		case MultiplayerModeType.KingAnton:
			m_Title.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NetworkedGameTypes, 1);
			break;
		case MultiplayerModeType.CoOpCreative:
			m_Title.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NetworkedGameTypes, 5);
			break;
		case MultiplayerModeType.CoOpCampaign:
			m_Title.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NetworkedGameTypes, 5);
			break;
		}
		UpdateScores();
		bool flag = Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase == NetController.Phase.Outro;
		m_ServerStartButton.gameObject.SetActive(flag);
		m_ServerOptionsButton.gameObject.SetActive(flag);
		m_ExitButton.gameObject.SetActive(flag);
		m_HeadingRestartTime.gameObject.SetActive(flag);
		m_ContextButton.SetActive(value: false);
		m_ContextPlayer = null;
		m_ServerStartButton.interactable = Singleton.Manager<ManNetwork>.inst.IsServer;
		d.Log("[UIScoreBoardHUD] Show buttons: " + flag + ", IsServer: " + Singleton.Manager<ManNetwork>.inst.IsServer);
		if (flag)
		{
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.FullscreenUI);
			SelectButton(Singleton.Manager<ManNetwork>.inst.IsServer ? m_ServerStartButton : m_ExitButton);
			Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(22, OnCancelPressed);
		}
		else if (SKU.ConsoleUI)
		{
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.UIMissionLog);
			Singleton.Manager<ManUI>.inst.ShowScreenPrompt(ManUI.ScreenType.MultiplayerScoreboard);
			UIScoreBoardEntry componentInChildren = m_EntriesPane.GetComponentInChildren<UIScoreBoardEntry>();
			if (componentInChildren != null)
			{
				componentInChildren.OnSelect(new BaseEventData(EventSystem.current));
				Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(componentInChildren.gameObject);
			}
		}
		TankCamera.inst.EndSpinControlMouse();
	}

	public void SelectStartButton()
	{
		SelectButton(m_ServerStartButton);
	}

	private void SelectButton(Button selectButton)
	{
		if (selectButton != null)
		{
			EventSystem.current.SetSelectedGameObject(null);
			Button component = selectButton.GetComponent<Button>();
			if (component != null)
			{
				component.Select();
			}
			EventSystem.current.SetSelectedGameObject(m_ServerStartButton.gameObject);
		}
	}

	public override void Hide(object context)
	{
		d.Log("UIScoreBoardHUD.Hide");
		base.Hide(context);
		m_ContextPlayer = null;
		m_EntryPool.FreeAll();
		m_TeamEntryPool.FreeAll();
		Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Unsubscribe(OnPlayerAdded);
		Singleton.Manager<ManNetwork>.inst.OnPlayerRemoved.Unsubscribe(OnPlayerRemoved);
		for (int i = 0; i < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); i++)
		{
			OnPlayerRemoved(Singleton.Manager<ManNetwork>.inst.GetPlayer(i));
		}
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.FullscreenUI);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.UIMissionLog);
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(22, OnCancelPressed);
		Singleton.Manager<ManUI>.inst.HideScreenPrompt();
		UIScoreBoardEntry componentInChildren = m_EntriesPane.GetComponentInChildren<UIScoreBoardEntry>();
		if (componentInChildren != null)
		{
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(componentInChildren.gameObject);
		}
	}

	public void OnStartServerClicked()
	{
		Singleton.Manager<ManNetwork>.inst.NetController.ServerChangePhase(NetController.Phase.Restarting);
	}

	public void OnServerOptionsClicked()
	{
		UIScreenNetworkLobby uIScreenNetworkLobby = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.MatchmakingLobbyScreen) as UIScreenNetworkLobby;
		if (Singleton.Manager<ManNetwork>.inst.IsServer && (bool)uIScreenNetworkLobby)
		{
			uIScreenNetworkLobby.RepeatingHost = true;
		}
		Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.MatchmakingLobbyScreen);
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.ReturnToLobby, new ReturnToLobbyMessage(), Singleton.Manager<ManNetwork>.inst.MyPlayer.netId);
			Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhaseTimerPaused = true;
		}
	}

	public void OnExitClicked()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Singleton.Manager<ManNetwork>.inst.NetController.ServerChangePhase(NetController.Phase.Exiting);
		}
		else if ((bool)Singleton.Manager<ManPauseGame>.inst)
		{
			Singleton.Manager<ManPauseGame>.inst.ShowExitConfirmScreen();
		}
	}

	public void DisplayContextMenuForPlayer(NetPlayer player, Vector2 position)
	{
		m_ContextButton.SetActive(value: true);
		m_ContextButton.transform.localPosition = position;
		m_ContextPlayer = player;
	}

	public void OnKickPlayerClicked()
	{
		if ((bool)m_ContextPlayer)
		{
			m_ContextPlayer.ServerKickPlayer();
		}
		m_ContextButton.SetActive(value: false);
	}

	private void UpdateScores()
	{
		m_TeamEntryPool.SetNoneUsed();
		m_EntryPool.SetNoneUsed();
		if (Singleton.Manager<ManNetwork>.inst.NetController != null && Singleton.Manager<ManNetwork>.inst.NetController.IsMultiTeamGame)
		{
			for (int i = 0; i < Singleton.Manager<ManNetwork>.inst.GetTeamCount(); i++)
			{
				int num = i;
				UIScoreBoardTeamEntry uIScoreBoardTeamEntry = m_TeamEntryPool.Alloc(m_EntriesPane);
				string text = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 115), num + 1);
				Color teamColour = Singleton.Manager<ManNetwork>.inst.NetController.GetTeamColour(num);
				float teamScore = Singleton.Manager<ManNetwork>.inst.NetController.GetTeamScore(num);
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int maxTeamSize = 16 / Singleton.Manager<ManNetwork>.inst.GetTeamCount();
				for (int j = 0; j < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); j++)
				{
					NetPlayer player = Singleton.Manager<ManNetwork>.inst.GetPlayer(j);
					if (player.LobbyTeamID == i)
					{
						num2++;
						num4 += player.Score.Kills;
						num3 += player.Score.Deaths;
					}
				}
				uIScoreBoardTeamEntry.SetTeamInfo(i, text, teamColour, teamScore, num4, num3, num2, maxTeamSize);
				for (int k = 0; k < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); k++)
				{
					NetPlayer player2 = Singleton.Manager<ManNetwork>.inst.GetPlayer(k);
					if (player2.LobbyTeamID == i)
					{
						m_EntryPool.Alloc(m_EntriesPane).SetPlayer(player2, this);
					}
				}
			}
		}
		else
		{
			for (int l = 0; l < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); l++)
			{
				UIScoreBoardEntry uIScoreBoardEntry = m_EntryPool.Alloc(m_EntriesPane);
				uIScoreBoardEntry.SetPlayer(Singleton.Manager<ManNetwork>.inst.GetPlayer(l), this);
				s_SortList.Add(uIScoreBoardEntry);
			}
			SortPlayerList();
		}
		m_TeamEntryPool.FreeUnused();
		m_EntryPool.FreeUnused();
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled() && (EventSystem.current.currentSelectedGameObject == null || !EventSystem.current.currentSelectedGameObject.activeInHierarchy))
		{
			UIScoreBoardEntry componentInChildren = m_EntriesPane.GetComponentInChildren<UIScoreBoardEntry>();
			if (componentInChildren != null)
			{
				EventSystem.current.SetSelectedGameObject(componentInChildren.gameObject);
			}
		}
	}

	private void SortPlayerList()
	{
		s_SortList.Sort(ScoreEntryComparer);
		for (int i = 0; i < s_SortList.Count; i++)
		{
			s_SortList[i].transform.SetSiblingIndex(i);
		}
		s_SortList.Clear();
	}

	private static int ScoreEntryComparer(UIScoreBoardEntry entryA, UIScoreBoardEntry entryB)
	{
		if (entryA.Player == null || entryB.Player == null)
		{
			if (!(entryA.Player == null))
			{
				return -1;
			}
			if (!(entryB.Player == null))
			{
				return 1;
			}
			return 0;
		}
		int num = NetScore.CompareDescending(entryA.Player.Score, entryB.Player.Score);
		if (num == 0)
		{
			num = (int)(entryA.Player.netId.Value - entryB.Player.netId.Value);
		}
		return num;
	}

	private static int TeamEntryComparer(UIScoreBoardTeamEntry entryA, UIScoreBoardTeamEntry entryB)
	{
		int num = (int)(entryA.Score - entryB.Score);
		if (num == 0)
		{
			num = entryA.TeamID - entryB.TeamID;
		}
		return num;
	}

	private void OnPlayerAdded(NetPlayer player)
	{
		m_Dirty = true;
		player.Score.OnChanged.Subscribe(OnScoreChanged);
	}

	private void OnPlayerRemoved(NetPlayer player)
	{
		m_Dirty = true;
		player.Score.OnChanged.Unsubscribe(OnScoreChanged);
	}

	private void OnScoreChanged()
	{
		m_Dirty = true;
	}

	private void OnCancelPressed(PayloadUIEventData evt)
	{
		evt.Use();
		if (!Singleton.Manager<ManNetwork>.inst.IsInPhase(NetController.Phase.Outro) && !Singleton.Manager<ManNetwork>.inst.IsInPhase(NetController.Phase.Restarting))
		{
			EventSystem.current.SetSelectedGameObject(null);
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ScoreBoard);
		}
	}

	private void OnPool()
	{
		m_EntryPool = new UIElementCache<UIScoreBoardEntry>(m_EntryPrefab);
		m_TeamEntryPool = new UIElementCache<UIScoreBoardTeamEntry>(m_TeamEntryPrefab);
	}

	private void OnSpawn()
	{
		AddElementToGroup(ManHUD.HUDGroup.GamepadQuickMenuHUDElements);
	}

	private void Update()
	{
		if (m_Dirty)
		{
			m_Dirty = false;
			UpdateScores();
		}
		if (m_HeadingRestartTime.IsActive() && (bool)Singleton.Manager<ManNetwork>.inst.NetController)
		{
			float currentPhaseTimer = Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhaseTimer;
			int num = Mathf.FloorToInt(currentPhaseTimer / 60f);
			int num2 = Mathf.FloorToInt(currentPhaseTimer % 60f);
			string arg = num + ":" + num2.ToString("D2");
			m_HeadingRestartTime.text = string.Format(m_NextMatchFormatString.Value, arg);
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(29, ControllerType.Joystick))
		{
			OnCancelPressed(null);
		}
	}
}
