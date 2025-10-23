#define UNITY_EDITOR
using TerraTech.Network;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIScreenPauseMenu : UIScreen
{
	[SerializeField]
	private InputField m_SeedText;

	[HideInInspector]
	public Button[] m_SwitchableButtons = new Button[0];

	public Button m_ResumeGame;

	[SerializeField]
	private Button m_InviteButton;

	[SerializeField]
	private Button m_LoadButton;

	[SerializeField]
	[FormerlySerializedAs("m_MainGameQuit")]
	private Button m_SaveAndQuitButton;

	[SerializeField]
	private Button m_SaveButton;

	[SerializeField]
	[FormerlySerializedAs("m_OtherQuit")]
	private Button m_QuitButton;

	[SerializeField]
	private Button m_SumoRematch;

	[SerializeField]
	private Button m_SumoDesingerSave;

	[SerializeField]
	private Toggle m_MPChatMuteAll;

	[SerializeField]
	private Toggle m_FreeCam;

	[SerializeField]
	private Button m_ResetPositionButtonRandD;

	[SerializeField]
	private Button m_ResetPositionButtonGauntlet;

	[SerializeField]
	private GameObject m_EarlyAccess;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		for (int i = 0; i < m_SwitchableButtons.Length; i++)
		{
			if ((bool)m_SwitchableButtons[i])
			{
				bool active = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == (ManGameMode.GameType)i;
				if (i == 1)
				{
					active = false;
				}
				m_SwitchableButtons[i].gameObject.SetActive(active);
			}
		}
		if ((bool)m_LoadButton)
		{
			bool active2 = Singleton.Manager<ManGameMode>.inst.CurrentModeHasSaveGameSupport();
			m_LoadButton.gameObject.SetActive(active2);
		}
		if ((bool)m_SaveAndQuitButton)
		{
			m_SaveAndQuitButton.gameObject.SetActive(value: false);
		}
		if ((bool)m_SaveButton)
		{
			bool active3 = Singleton.Manager<ManGameMode>.inst.CurrentModeCanSave();
			m_SaveButton.gameObject.SetActive(active3);
		}
		if ((bool)m_QuitButton)
		{
			m_QuitButton.gameObject.SetActive(value: true);
		}
		if ((bool)m_ResumeGame)
		{
			m_ResumeGame.gameObject.SetActive(value: true);
		}
		if (m_ResetPositionButtonRandD != null)
		{
			bool active4 = Singleton.Manager<ManGameMode>.inst.CurrentModeCanResetPosition() && Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.RaD;
			m_ResetPositionButtonRandD.gameObject.SetActive(active4);
		}
		if (m_ResetPositionButtonGauntlet != null)
		{
			bool active5 = Singleton.Manager<ManGameMode>.inst.CurrentModeCanResetPosition() && Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Gauntlet;
			m_ResetPositionButtonGauntlet.gameObject.SetActive(active5);
		}
		if ((bool)m_SeedText)
		{
			if (Singleton.Manager<ManGameMode>.inst.CurrentModeDisplaysSeed())
			{
				m_SeedText.text = Singleton.Manager<ManWorld>.inst.SeedString;
			}
			else
			{
				m_SeedText.text = string.Empty;
			}
		}
		if ((bool)m_SumoRematch)
		{
			bool active6 = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.SumoShowdown && !Mode<ModeSumo>.inst.Designing;
			m_SumoRematch.gameObject.SetActive(active6);
		}
		if ((bool)m_SumoDesingerSave)
		{
			bool active7 = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.SumoShowdown && Mode<ModeSumo>.inst.Designing;
			m_SumoDesingerSave.gameObject.SetActive(active7);
		}
		bool flag = Singleton.Manager<ManNetwork>.inst.IsMultiplayer();
		if ((bool)m_MPChatMuteAll)
		{
			if (flag && SKU.ConsoleUI && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.Platform_SupportsVoiceChat())
			{
				UIMPChat uIMPChat = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MPChat) as UIMPChat;
				if ((bool)uIMPChat)
				{
					m_MPChatMuteAll.isOn = uIMPChat.AllMuted();
				}
				m_MPChatMuteAll.gameObject.SetActive(value: true);
			}
			else
			{
				m_MPChatMuteAll.gameObject.SetActive(value: false);
			}
		}
		if (m_InviteButton != null)
		{
			m_InviteButton.gameObject.SetActive(flag && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.SupportsOpenFriendInviteScreen());
		}
		if (m_FreeCam != null)
		{
			m_FreeCam.gameObject.SetActive(Singleton.Manager<ManPauseGame>.inst.CanUsePhotoMode());
			m_FreeCam.SetValue(Singleton.Manager<ManPauseGame>.inst.PhotoCamToggle);
		}
	}

	public void ToggleFreeCam()
	{
		d.Assert(Singleton.Manager<ManPauseGame>.inst.CanUsePhotoMode(), "Photo Cam toggled through UI when this shouldn't be possible! Did we forget to hide the UI element? Call code!");
		Singleton.Manager<ManPauseGame>.inst.PhotoCamToggle = !Singleton.Manager<ManPauseGame>.inst.PhotoCamToggle;
	}

	public void UnPauseButton()
	{
		Singleton.Manager<ManPauseGame>.inst.PauseGame(pauseState: false);
	}

	public void InvitePlayer()
	{
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.OpenFriendInviteScreen();
	}

	public void OnToggleMuteAll(bool set)
	{
		UIMPChat uIMPChat = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MPChat) as UIMPChat;
		if ((bool)uIMPChat)
		{
			uIMPChat.MuteAll(set);
		}
	}
}
