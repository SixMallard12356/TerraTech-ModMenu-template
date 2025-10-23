#define UNITY_EDITOR
using TerraTech.Network;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScoreBoardEntry : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, ISelectHandler, IDeselectHandler, IUIExtraButtonHandler1, IUIExtraButtonHandler2
{
	[SerializeField]
	private Text m_NameText;

	[SerializeField]
	private Text m_KillsText;

	[SerializeField]
	private Text m_DeathsText;

	[SerializeField]
	private Image m_Image;

	[SerializeField]
	private Toggle m_MuteToggle;

	[SerializeField]
	private GameObject m_HostIcon;

	[SerializeField]
	private GameObject m_KingIcon;

	[SerializeField]
	private GameObject m_MuteIcon;

	private NetPlayer m_Player;

	private TTNetworkID m_PlayerIDInLobby = TTNetworkID.Invalid;

	private UIScoreBoardHUD m_Scoreboard;

	private Image m_SpeakingIconImage;

	private bool m_CanShowProfileCard;

	public NetPlayer Player => m_Player;

	public TTNetworkID GetNetworkPlayerID => m_PlayerIDInLobby;

	public void SetPlayer(NetPlayer player, UIScoreBoardHUD scoreboard)
	{
		m_Scoreboard = scoreboard;
		m_Player = player;
		m_PlayerIDInLobby = TTNetworkID.Invalid;
		m_CanShowProfileCard = false;
		if (!(player != null))
		{
			return;
		}
		m_PlayerIDInLobby = m_Player.GetPlayerIDInLobby();
		m_NameText.text = m_Player.name;
		m_NameText.color = Singleton.Manager<ManNetworkLobby>.inst.LobbyConstants.m_AllColours[m_Player.LobbyTeamID];
		m_Image.sprite = m_Player.Sprite;
		m_MuteToggle.onValueChanged.AddListener(OnMuteValueChanged);
		m_HostIcon.SetActive(m_Player.IsHostPlayer);
		m_KingIcon.SetActive(value: false);
		if (!Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.Platform_SupportsVoiceChat() || player.IsActuallyLocalPlayer)
		{
			m_MuteToggle.gameObject.SetActive(value: false);
			m_MuteIcon.SetActive(value: false);
		}
		else
		{
			m_CanShowProfileCard = SKU.XboxOneUI;
			UIMPChat uIMPChat = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MPChat) as UIMPChat;
			if ((bool)uIMPChat)
			{
				bool flag = uIMPChat.IsPlayerMuted(m_Player.netId);
				m_MuteToggle.gameObject.SetActive(value: true);
				m_MuteToggle.isOn = flag;
				m_MuteIcon.SetActive(flag);
			}
			else
			{
				m_MuteToggle.gameObject.SetActive(value: false);
			}
		}
		NetController.ScorePolicy currentScorePolicy = Singleton.Manager<ManNetwork>.inst.NetController.CurrentScorePolicy;
		switch (currentScorePolicy)
		{
		case NetController.ScorePolicy.GameTime:
		case NetController.ScorePolicy.SetTime:
			m_KillsText.gameObject.SetActive(value: false);
			m_DeathsText.gameObject.SetActive(value: false);
			break;
		case NetController.ScorePolicy.NumWaves:
			m_KillsText.gameObject.SetActive(value: false);
			m_DeathsText.gameObject.SetActive(value: false);
			break;
		case NetController.ScorePolicy.Kills:
		case NetController.ScorePolicy.KillMinusDeath:
			m_KillsText.text = m_Player.Score.Kills.ToString();
			m_DeathsText.text = m_Player.Score.Deaths.ToString();
			m_KillsText.gameObject.SetActive(value: true);
			m_DeathsText.gameObject.SetActive(value: true);
			break;
		default:
			d.AssertFormat(false, "UIScoreBoardEntry.SetPlayer has unhandled score policy {0}", currentScorePolicy);
			break;
		}
	}

	public void OnMuteValueChanged(bool ison)
	{
		UIMPChat uIMPChat = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MPChat) as UIMPChat;
		if ((bool)uIMPChat)
		{
			if (m_Player != null)
			{
				uIMPChat.MutePlayer(m_Player, ison);
			}
			m_MuteIcon.SetActive(ison);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (CanKick() && eventData.button == PointerEventData.InputButton.Right)
		{
			m_Scoreboard.DisplayContextMenuForPlayer(m_Player, base.transform.localPosition);
		}
	}

	public void OnSelect(BaseEventData eventData)
	{
		if (m_MuteToggle.gameObject.activeSelf)
		{
			Singleton.Manager<ManBtnPrompt>.inst.UpdateCurrentHUDPrompt(m_MuteToggle.isOn ? ManBtnPrompt.PromptType.ContextUnmute : ManBtnPrompt.PromptType.ContextMute);
		}
		else
		{
			Singleton.Manager<ManBtnPrompt>.inst.HidePromptType(ManBtnPrompt.PromptType.ContextMute);
			Singleton.Manager<ManBtnPrompt>.inst.HidePromptType(ManBtnPrompt.PromptType.ContextUnmute);
		}
		if (CanKick())
		{
			Singleton.Manager<ManBtnPrompt>.inst.UpdateCurrentHUDPrompt(ManBtnPrompt.PromptType.ContextKickScoreboard);
		}
		else
		{
			Singleton.Manager<ManBtnPrompt>.inst.HidePromptType(ManBtnPrompt.PromptType.ContextKickScoreboard);
		}
		if (m_CanShowProfileCard)
		{
			Singleton.Manager<ManBtnPrompt>.inst.UpdateCurrentHUDPrompt(ManBtnPrompt.PromptType.ContextShowXboxProfileCard);
		}
		else
		{
			Singleton.Manager<ManBtnPrompt>.inst.HidePromptType(ManBtnPrompt.PromptType.ContextShowXboxProfileCard);
		}
	}

	public void OnDeselect(BaseEventData eventData)
	{
		Singleton.Manager<ManBtnPrompt>.inst.HidePromptType(ManBtnPrompt.PromptType.ContextKickScoreboard);
		Singleton.Manager<ManBtnPrompt>.inst.HidePromptType(ManBtnPrompt.PromptType.ContextMute);
		Singleton.Manager<ManBtnPrompt>.inst.HidePromptType(ManBtnPrompt.PromptType.ContextUnmute);
		Singleton.Manager<ManBtnPrompt>.inst.HidePromptType(ManBtnPrompt.PromptType.ContextShowXboxProfileCard);
	}

	public void OnClicked()
	{
		if (m_CanShowProfileCard)
		{
			d.Log("UINetworkLobbyPlayerEntry - ShowProfileCard for " + m_PlayerIDInLobby.ToString());
		}
	}

	public void OnUIExtraButton1(BaseEventData eventData)
	{
		if (CanKick() && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.KickPlayer(m_PlayerIDInLobby);
		}
	}

	public void OnUIExtraButton2(BaseEventData eventData)
	{
		if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.Platform_SupportsVoiceChat())
		{
			m_MuteToggle.isOn = !m_MuteToggle.isOn;
			Singleton.Manager<ManBtnPrompt>.inst.UpdateCurrentHUDPrompt(m_MuteToggle.isOn ? ManBtnPrompt.PromptType.ContextUnmute : ManBtnPrompt.PromptType.ContextMute);
		}
	}

	private bool CanKick()
	{
		if (m_Player != Singleton.Manager<ManNetwork>.inst.MyPlayer)
		{
			return Singleton.Manager<ManNetwork>.inst.IsServer;
		}
		return false;
	}

	private void OnPool()
	{
		m_SpeakingIconImage = m_MuteToggle.transform.Find("Speaking").gameObject.GetComponent<Image>();
		d.Assert(m_SpeakingIconImage != null, "ASSERT: Failed to find Image for Speaking Icon in:" + base.gameObject.name);
		GetComponent<Button>().onClick.AddListener(OnClicked);
	}

	private void OnRecycle()
	{
		m_Player = null;
		m_MuteToggle.onValueChanged.RemoveListener(OnMuteValueChanged);
		if (EventSystem.current.currentSelectedGameObject == base.gameObject)
		{
			EventSystem.current.SetSelectedGameObject(null);
		}
	}
}
