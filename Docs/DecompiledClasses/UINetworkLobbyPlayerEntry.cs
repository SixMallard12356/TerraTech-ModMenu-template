#define UNITY_EDITOR
using System.Collections.Generic;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UINetworkLobbyPlayerEntry : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler, IUIExtraButtonHandler1, IUIExtraButtonHandler2
{
	[SerializeField]
	private Text m_NameField;

	[SerializeField]
	private Image m_ImageField;

	[SerializeField]
	private Button m_KickedButton;

	[SerializeField]
	private Button m_BanButton;

	[SerializeField]
	private Image m_SpeakerIcon;

	public Event<LobbyPlayerData, Color32> OnColourChosen;

	private ColourSelector m_ColSelector;

	private LobbyPlayerData m_PlayerData;

	private bool m_CanShowProfileCard;

	public void SetPlayerData(LobbyPlayerData playerData, bool canShowProfileCard, bool showAdminButtons, bool allowColourChoice, List<Color32> availableColours)
	{
		m_PlayerData = playerData;
		m_CanShowProfileCard = canShowProfileCard;
		m_NameField.text = playerData.m_Name;
		m_ImageField.sprite = playerData.m_Sprite;
		m_ImageField.gameObject.SetActive(m_ImageField.sprite != null);
		bool flag = SKU.BansEnabled && showAdminButtons;
		m_KickedButton.gameObject.SetActive(showAdminButtons);
		m_BanButton.gameObject.SetActive(flag);
		m_ColSelector.interactable = allowColourChoice;
		m_KickedButton.interactable = showAdminButtons;
		m_BanButton.interactable = flag;
		List<Color32> list = new List<Color32>();
		list.Add(playerData.m_Colour);
		list.AddRange(availableColours);
		m_ColSelector.SetChoices(list);
		m_ColSelector.Select(playerData.m_Colour, isClosing: false);
		if (m_SpeakerIcon != null)
		{
			m_SpeakerIcon.gameObject.SetActive(value: false);
		}
	}

	public void OnKicked()
	{
		if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.KickPlayer(m_PlayerData.m_PlayerID);
		}
	}

	public void OnBan()
	{
		if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.ConfirmBanPlayer(m_PlayerData.m_PlayerID);
		}
	}

	private void HandleColourChosen(Color32 choice)
	{
		OnColourChosen.Send(m_PlayerData, choice);
	}

	public void OnSelect(BaseEventData eventData)
	{
		if (m_KickedButton.isActiveAndEnabled)
		{
			m_KickedButton.OnSelect(eventData);
			Singleton.Manager<ManUI>.inst.UpdateScreenPrompt(ManBtnPrompt.PromptType.ContextKick);
		}
		if (m_CanShowProfileCard)
		{
			Singleton.Manager<ManUI>.inst.UpdateScreenPrompt(ManBtnPrompt.PromptType.ContextShowXboxProfileCard);
		}
	}

	public void OnDeselect(BaseEventData eventData)
	{
		Singleton.Manager<ManUI>.inst.ToggleBtnPrompt(active: false, ManBtnPrompt.PromptType.ContextKick);
		if (m_CanShowProfileCard)
		{
			Singleton.Manager<ManUI>.inst.ToggleBtnPrompt(active: false, ManBtnPrompt.PromptType.ContextShowXboxProfileCard);
		}
		if (m_KickedButton.isActiveAndEnabled)
		{
			m_KickedButton.OnDeselect(eventData);
		}
	}

	private void OnClicked()
	{
		if (m_CanShowProfileCard)
		{
			d.Log("UINetworkLobbyPlayerEntry - ShowProfileCard for " + m_PlayerData.m_PlayerID.ToString());
		}
	}

	public void OnUIExtraButton1(BaseEventData eventData)
	{
		if (m_KickedButton.isActiveAndEnabled)
		{
			OnKicked();
		}
	}

	public void OnUIExtraButton2(BaseEventData eventData)
	{
		if (m_BanButton.isActiveAndEnabled)
		{
			OnBan();
		}
	}

	private void OnPool()
	{
		m_ColSelector = GetComponent<ColourSelector>();
		m_ColSelector.OnChoice.Subscribe(HandleColourChosen);
		GetComponent<Button>().onClick.AddListener(OnClicked);
	}

	private void OnSpawn()
	{
		m_NameField.text = "";
		m_ImageField.sprite = null;
		m_ImageField.gameObject.SetActive(value: false);
	}

	private void Start()
	{
		if (m_SpeakerIcon != null)
		{
			m_SpeakerIcon.gameObject.SetActive(value: false);
		}
	}

	private void Update()
	{
		bool flag = false;
		if (m_PlayerData.m_PlayerID.IsValid())
		{
			flag = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetPlayersThatAreTalking()?.Contains(m_PlayerData.m_PlayerID) ?? false;
		}
		if (m_SpeakerIcon != null && m_SpeakerIcon.gameObject.activeSelf != flag)
		{
			m_SpeakerIcon.gameObject.SetActive(flag);
		}
	}
}
