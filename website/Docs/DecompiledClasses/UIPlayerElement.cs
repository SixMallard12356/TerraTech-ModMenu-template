#define UNITY_EDITOR
using System.Collections.Generic;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPlayerElement : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler
{
	[SerializeField]
	private Text m_NameText;

	[SerializeField]
	private UIOptionsBehaviourDropdown m_TeamsContainer;

	[SerializeField]
	private Dropdown m_TeamsDropDown;

	[SerializeField]
	private Image m_PlayerIcon;

	[SerializeField]
	private Toggle m_MuteToggle;

	[SerializeField]
	private GameObject m_HostText;

	[SerializeField]
	private Button m_KickButton;

	[SerializeField]
	private Button m_BanButton;

	[SerializeField]
	private Button m_GamepadOnlyWrapperButton;

	[SerializeField]
	private Image m_SelectionBorder;

	[SerializeField]
	private Button m_PlayerInfoCardButton;

	[SerializeField]
	private Color m_DropdownBGColor;

	[SerializeField]
	private Color m_DropdownDisabledBGColor;

	[SerializeField]
	private Button m_DropdownButton;

	[SerializeField]
	private Image m_DropdownBackgroundImg;

	[SerializeField]
	private Image m_DropdownArrow;

	public Event<UIPlayerElement, bool> HoverEvent;

	private NetPlayer m_Player;

	private bool m_CanShowProfileCard;

	public void Init(NetPlayer player)
	{
		m_Player = player;
		m_HostText.SetActive(player.IsHostPlayer);
		m_PlayerIcon.sprite = player.Sprite;
		m_NameText.text = player.name;
		m_CanShowProfileCard = !player.isLocalPlayer && SKU.XboxOneUI;
		UIMPChat uIMPChat = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MPChat) as UIMPChat;
		if ((bool)uIMPChat)
		{
			m_MuteToggle.isOn = uIMPChat.IsPlayerMuted(m_Player.netId);
		}
		bool allowTeamSelection = UITechManagerHUD.AllowTeamSelection;
		bool flag = CanChangeTeam();
		if (allowTeamSelection)
		{
			int num = 0;
			List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
			for (int i = 1073741824; i < 1073741829; i++)
			{
				string text = ModeCoOp<ModeCoOpCreative>.CreateTeamNameFromID(i);
				list.Add(new Dropdown.OptionData(text));
			}
			m_TeamsDropDown.ClearOptions();
			m_TeamsDropDown.AddOptions(list);
			num = ManSpawn.LobbyTeamIDFromTechTeamID(player.TechTeamID);
			m_TeamsDropDown.SetValue(num);
			m_Player.OnTeamChanged.Subscribe(OnPlayerTeamChanged);
		}
		m_TeamsDropDown.interactable = flag;
		m_TeamsContainer.interactable = flag;
		m_DropdownButton.interactable = flag;
		m_DropdownBackgroundImg.color = (flag ? m_DropdownBGColor : m_DropdownDisabledBGColor);
		m_DropdownArrow.enabled = flag;
		m_TeamsContainer.gameObject.SetActive(allowTeamSelection);
		bool active = ManNetwork.IsHost && m_Player != Singleton.Manager<ManNetwork>.inst.MyPlayer;
		m_KickButton.gameObject.SetActive(active);
		m_BanButton.gameObject.SetActive(active);
		m_MuteToggle.gameObject.SetActive(Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.Platform_SupportsVoiceChat() && m_Player != Singleton.Manager<ManNetwork>.inst.MyPlayer);
		TooltipComponent component = m_MuteToggle.GetComponent<TooltipComponent>();
		if (component.IsNotNull())
		{
			string text2 = (m_MuteToggle.isOn ? Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HUDMultiplayer, 4) : Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HUDMultiplayer, 3));
			component.SetText(text2);
		}
		Color color = Singleton.Manager<ManNetworkLobby>.inst.LobbyConstants.m_CoOpColours[player.LobbyTeamID];
		m_NameText.color = color;
		m_PlayerInfoCardButton.gameObject.SetActive(m_CanShowProfileCard);
		Selectable selectOnUp = (flag ? m_TeamsContainer.GetComponent<Button>() : null);
		if (m_CanShowProfileCard)
		{
			m_PlayerInfoCardButton.navigation = new Navigation
			{
				mode = Navigation.Mode.Explicit,
				selectOnUp = selectOnUp,
				selectOnDown = m_MuteToggle,
				selectOnLeft = m_MuteToggle,
				selectOnRight = null
			};
			selectOnUp = m_PlayerInfoCardButton;
		}
		ManNavUI.RecalculateLeftRightNavigation(new Selectable[3] { m_MuteToggle, m_KickButton, m_BanButton }, selectOnUp);
	}

	public void Cleanup()
	{
		HoverEvent.Clear();
		SetAsGamepadEntryPoint(set: false);
		m_Player.OnTeamChanged.Unsubscribe(OnPlayerTeamChanged);
		m_Player = null;
	}

	public void SetChildNavigationEntryPoint(bool set)
	{
		GameObject target = (CanChangeTeam() ? m_TeamsContainer.gameObject : (m_MuteToggle.gameObject.activeSelf ? m_MuteToggle.gameObject : m_KickButton.gameObject));
		if (set)
		{
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(target);
		}
		else
		{
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(target);
		}
	}

	public void SetAsGamepadEntryPoint(bool set)
	{
		if (set)
		{
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(m_GamepadOnlyWrapperButton.gameObject);
		}
		else
		{
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(m_GamepadOnlyWrapperButton.gameObject);
		}
	}

	public void EnsureSelection()
	{
		Singleton.Manager<ManNavUI>.inst.DeferredSetSelected(m_GamepadOnlyWrapperButton.gameObject);
	}

	public void SetupNavigationToPrevNeighbour(UIPlayerElement prevNav)
	{
		m_GamepadOnlyWrapperButton.SetNavigationMode(Navigation.Mode.Explicit);
		Selectable navigationLeft = m_GamepadOnlyWrapperButton.GetNavigationLeft();
		if (navigationLeft != null)
		{
			if (navigationLeft.GetNavigationDown() == m_GamepadOnlyWrapperButton)
			{
				navigationLeft.SetNavigationRight(null);
			}
			m_GamepadOnlyWrapperButton.SetNavigationLeft(null);
		}
		if (prevNav.IsNotNull())
		{
			m_GamepadOnlyWrapperButton.SetNavigationLeft(prevNav.m_GamepadOnlyWrapperButton);
			prevNav.m_GamepadOnlyWrapperButton.SetNavigationRight(m_GamepadOnlyWrapperButton);
		}
	}

	public void SetSelectionBorderVisible(bool visible)
	{
		m_SelectionBorder.gameObject.SetActive(visible);
	}

	public NetPlayer GetPlayer()
	{
		return m_Player;
	}

	public void ShowPlayerInfoCard()
	{
		d.Log("UINetworkLobbyPlayerEntry - ShowProfileCard for " + m_Player.GetPlayerIDInLobby().ToString());
	}

	public void OnPlayerTeamChanged(NetPlayer player)
	{
		if (player == m_Player)
		{
			m_TeamsDropDown.SetValue(ManSpawn.LobbyTeamIDFromTechTeamID(player.TechTeamID));
			Color color = Singleton.Manager<ManNetworkLobby>.inst.LobbyConstants.m_CoOpColours[player.LobbyTeamID];
			m_NameText.color = color;
		}
	}

	public void OnPlayerElementSelected()
	{
		UIPlayerInfoHUD uIPlayerInfoHUD = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.PlayerInfo) as UIPlayerInfoHUD;
		if ((bool)uIPlayerInfoHUD)
		{
			uIPlayerInfoHUD.SelectPlayer(this);
		}
	}

	public void OnTeamDropdownChanged(int option)
	{
		int num = ManSpawn.TechTeamIDFromLobbyTeamID(option);
		d.Assert(num != -1, "Requested putting player on enemy team");
		if (ManNetwork.IsHost || m_Player == Singleton.Manager<ManNetwork>.inst.MyPlayer)
		{
			m_Player.RequestChangeTeam(num);
			return;
		}
		int value = ManSpawn.LobbyTeamIDFromTechTeamID(m_Player.TechTeamID);
		m_TeamsDropDown.SetValue(value);
	}

	public void OnKickClicked()
	{
		if (ManNetwork.IsHost && m_Player != Singleton.Manager<ManNetwork>.inst.MyPlayer)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.KickPlayer(m_Player.GetPlayerIDInLobby());
		}
	}

	public void OnBanClicked()
	{
		if (ManNetwork.IsHost && m_Player != Singleton.Manager<ManNetwork>.inst.MyPlayer)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.ConfirmBanPlayer(m_Player.GetPlayerIDInLobby());
		}
	}

	public void OnMuteValueChanged(bool ison)
	{
		UIMPChat uIMPChat = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MPChat) as UIMPChat;
		if ((bool)uIMPChat && m_Player != null)
		{
			uIMPChat.MutePlayer(m_Player, ison);
		}
		TooltipComponent component = m_MuteToggle.GetComponent<TooltipComponent>();
		if (component.IsNotNull())
		{
			string text = (ison ? Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HUDMultiplayer, 4) : Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HUDMultiplayer, 3));
			component.SetText(text);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		HoverEvent.Send(this, paramB: true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		HoverEvent.Send(this, paramB: false);
	}

	public void OnSelect(BaseEventData eventData)
	{
		if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			HoverEvent.Send(this, paramB: true);
		}
	}

	private bool CanChangeTeam()
	{
		if (UITechManagerHUD.AllowTeamSelection)
		{
			return m_Player == Singleton.Manager<ManNetwork>.inst.MyPlayer;
		}
		return false;
	}
}
