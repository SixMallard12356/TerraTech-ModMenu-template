#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIMPChat : UIHUDElement
{
	private struct ChatMessage
	{
		public string m_Format;

		public string m_UserMsg;

		public TTNetworkID m_UserId;

		public string m_UserName;

		public Color32 m_Col;

		public float m_TimeAdded;

		public bool m_TeamMsg;
	}

	[Flags]
	private enum ButtonPressFlags
	{
		ShowChat_General = 2,
		ShowChat_Team = 4
	}

	[FormerlySerializedAs("m_ChatWindow")]
	[SerializeField]
	private Text m_ChatHistory;

	[FormerlySerializedAs("m_ChatWindow_TMP")]
	[SerializeField]
	private TextMeshProUGUI m_ChatHistory_TMP;

	[SerializeField]
	private Scrollbar m_ChatHistoryScrollbar;

	[SerializeField]
	private InputField m_ChatInput;

	[SerializeField]
	private TMP_InputField m_ChatInput_TMP;

	[SerializeField]
	private InputField m_CollapsedChatInput;

	[SerializeField]
	private TMP_InputField m_CollapsedChatInput_TMP;

	[SerializeField]
	[Header("ChannelDisplay")]
	protected Image[] m_ChannelRecolourImages = new Image[0];

	[SerializeField]
	protected Text[] m_ChannelRecolourTexts = new Text[0];

	[SerializeField]
	protected TextMeshProUGUI[] m_ChannelRecolourTextsMeshPro = new TextMeshProUGUI[0];

	[SerializeField]
	protected Button[] m_ChannelRecolourButtons = new Button[0];

	[SerializeField]
	protected GameObject m_TeamChannelTagObject;

	[SerializeField]
	protected GameObject m_GlobalChannelTagObject;

	[SerializeField]
	protected Color m_GlobalChannelTagColor = Color.magenta;

	[SerializeField]
	[Tooltip("Set true to enable moving of the chat windows when obscuring screens are active, false to instead disable the chat window in that case")]
	[Header("Justification")]
	protected bool m_JustifyChatWindow;

	[SerializeField]
	protected RectTransform m_Justify_CollapsedWindow_LeftAnchor;

	[SerializeField]
	protected RectTransform m_Justify_CollapsedWindow_RightAnchor;

	[SerializeField]
	protected RectTransform m_Justify_CollapsedWindow_FlippableBG;

	[SerializeField]
	protected RectTransform m_Justify_ExpandedWindow_LeftAnchor;

	[SerializeField]
	protected RectTransform m_Justify_ExpandedWindow_RightAnchor;

	[SerializeField]
	protected RectTransform m_Justify_ExpandedWindow_FlippableBG;

	[Header("Windows")]
	[SerializeField]
	private GameObject m_CollapsedWindow;

	[SerializeField]
	private GameObject m_ExpandedWindow;

	public static UIMPChat s_ChatInst;

	public static UIMPChat s_MissionUpdatesInst;

	private bool m_HasFocus;

	private int m_ChatTeamID = -1;

	private bool m_IsJustified;

	private Queue<ChatMessage> m_ChatMessages = new Queue<ChatMessage>(5);

	private StringBuilder m_Builder = new StringBuilder();

	private List<NetworkInstanceId> m_MutedPlayers = new List<NetworkInstanceId>(16);

	private bool m_bAllMuted;

	private bool m_IsExpanded;

	public bool IsJustified => m_IsJustified;

	public bool CanJustify => m_JustifyChatWindow;

	public static void UpdateVisibility(ManHUD.HUDElementType type)
	{
		bool flag = !Singleton.Manager<ManHUD>.inst.IsHudElementExpanded(ManHUD.HUDElementType.BlockPalette) && !Singleton.Manager<ManHUD>.inst.IsHudElementExpanded(ManHUD.HUDElementType.BlockShop) && !Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.TechLoader);
		UIMPChat uIMPChat = type switch
		{
			ManHUD.HUDElementType.MPMissionUpdates => s_MissionUpdatesInst, 
			ManHUD.HUDElementType.MPChat => s_ChatInst, 
			_ => null, 
		};
		if (uIMPChat != null && uIMPChat.CanJustify)
		{
			if (flag == uIMPChat.IsJustified)
			{
				uIMPChat.Justify(!uIMPChat.IsJustified);
			}
		}
		else if (flag != Singleton.Manager<ManHUD>.inst.IsHudElementVisible(type))
		{
			if (flag)
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(type);
			}
			else
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(type);
			}
		}
	}

	public static void HandleActivateChatInput()
	{
		bool num = Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetwork>.inst.NetController != null && Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase >= NetController.Phase.TechSelection;
		ButtonPressFlags buttonPressFlags = (ButtonPressFlags)((Singleton.Manager<ManInput>.inst.GetButtonDown(60) ? 2 : 0) | (Singleton.Manager<ManInput>.inst.GetButtonDown(61) ? 4 : 0));
		bool flag = s_ChatInst != null && !s_ChatInst.HasFocus();
		bool flag2 = !Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.BugReport);
		if (num && flag && buttonPressFlags != (ButtonPressFlags)0 && flag2)
		{
			s_ChatInst.SetTeamChat((buttonPressFlags & ButtonPressFlags.ShowChat_Team) != 0);
			s_ChatInst.SetFocus(isFocus: true);
		}
	}

	public void Justify(bool right)
	{
		if (CanJustify)
		{
			m_IsJustified = right;
			RectTransform rectTransform = (right ? m_Justify_CollapsedWindow_RightAnchor : m_Justify_CollapsedWindow_LeftAnchor);
			RectTransform rectTransform2 = (right ? m_Justify_ExpandedWindow_RightAnchor : m_Justify_ExpandedWindow_LeftAnchor);
			d.AssertFormat(rectTransform != null || rectTransform2 != null, "HARK! Chat window is set up for justification but no window targets are assigned! > {0}", base.gameObject.name);
			JustifyWindowToTarget(m_CollapsedWindow.transform as RectTransform, rectTransform, m_Justify_CollapsedWindow_FlippableBG);
			JustifyWindowToTarget(m_ExpandedWindow.transform as RectTransform, rectTransform2, m_Justify_ExpandedWindow_FlippableBG);
		}
		void JustifyWindowToTarget(RectTransform windowRect, RectTransform targetRect, RectTransform flippableBG)
		{
			windowRect.anchorMin = targetRect.anchorMin;
			windowRect.anchorMax = targetRect.anchorMax;
			windowRect.anchoredPosition = targetRect.anchoredPosition;
			flippableBG.localScale = flippableBG.localScale.SetX((float)((!right) ? 1 : (-1)) * Mathf.Abs(flippableBG.localScale.x));
		}
	}

	public void OnEndEdit(string value)
	{
		if (Input.GetKey(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			if (m_ChatInput != null && !m_ChatInput.text.NullOrEmpty())
			{
				SendChatMessage(m_ChatInput.text);
			}
			else if (m_ChatInput_TMP != null && !m_ChatInput_TMP.text.NullOrEmpty())
			{
				SendChatMessage(m_ChatInput_TMP.text);
			}
			else
			{
				SetFocus(isFocus: false);
			}
		}
		else
		{
			SetFocus(isFocus: false);
		}
	}

	public override void Show(object context)
	{
		base.Show(context);
		SetTeamChannel(-1);
		SetExpanded(state: false);
		m_MutedPlayers.Clear();
		m_bAllMuted = false;
	}

	public override void Hide(object context)
	{
		base.Hide(context);
	}

	public void SetExpanded(bool state)
	{
		SetExpanded(state, forceChange: false);
	}

	public void SetExpanded(bool state, bool forceChange = false)
	{
		bool flag = state != m_IsExpanded;
		if (!forceChange && !flag)
		{
			return;
		}
		m_IsExpanded = state;
		if (m_CollapsedWindow != null)
		{
			m_CollapsedWindow.SetActive(!state);
		}
		if (m_ExpandedWindow != null)
		{
			m_ExpandedWindow.SetActive(state);
		}
		if (flag && flag && m_ExpandedWindow != null && m_CollapsedWindow != null)
		{
			InputField inputField = (m_IsExpanded ? m_CollapsedChatInput : m_ChatInput);
			InputField inputField2 = (m_IsExpanded ? m_ChatInput : m_CollapsedChatInput);
			TMP_InputField tMP_InputField = (m_IsExpanded ? m_CollapsedChatInput_TMP : m_ChatInput_TMP);
			TMP_InputField tMP_InputField2 = (m_IsExpanded ? m_ChatInput_TMP : m_CollapsedChatInput_TMP);
			if (inputField != null && inputField2 != null)
			{
				inputField2.text = inputField.text;
			}
			if (tMP_InputField != null && tMP_InputField2 != null)
			{
				tMP_InputField2.text = tMP_InputField.text;
			}
		}
	}

	public void ToggleTeamChat()
	{
		SetTeamChat(m_ChatTeamID == -1);
	}

	public void SetTeamChat(bool state)
	{
		SetTeamChannel((state && Singleton.Manager<ManNetwork>.inst.MyPlayer.IsNotNull()) ? Singleton.Manager<ManNetwork>.inst.MyPlayer.LobbyTeamID : (-1));
	}

	public void SetTeamChannel(int index)
	{
		if (Singleton.Manager<ManNetwork>.inst.MyPlayer != null && index != -1 != (m_ChatTeamID != -1))
		{
			if (index != -1)
			{
				Singleton.Manager<ManNetwork>.inst.MyPlayer.OnTeamChanged.Subscribe(OnPlayerTeamChanged);
			}
			else
			{
				Singleton.Manager<ManNetwork>.inst.MyPlayer.OnTeamChanged.Unsubscribe(OnPlayerTeamChanged);
			}
		}
		m_ChatTeamID = index;
		Color color = ((m_ChatTeamID != -1) ? Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetMultiplayerTeamColour(m_ChatTeamID, Color.magenta) : m_GlobalChannelTagColor);
		Color color2 = ((m_ChatTeamID != -1) ? Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetMultiplayerTeamTextColour(m_ChatTeamID, Color.magenta) : m_GlobalChannelTagColor);
		Image[] channelRecolourImages = m_ChannelRecolourImages;
		foreach (Image image in channelRecolourImages)
		{
			image.color = color.SetAlpha(image.color.a);
		}
		Text[] channelRecolourTexts = m_ChannelRecolourTexts;
		foreach (Text text in channelRecolourTexts)
		{
			text.color = color2.SetAlpha(text.color.a);
		}
		TextMeshProUGUI[] channelRecolourTextsMeshPro = m_ChannelRecolourTextsMeshPro;
		foreach (TextMeshProUGUI textMeshProUGUI in channelRecolourTextsMeshPro)
		{
			textMeshProUGUI.color = color2.SetAlpha(textMeshProUGUI.color.a);
		}
		Button[] channelRecolourButtons = m_ChannelRecolourButtons;
		foreach (Button obj in channelRecolourButtons)
		{
			ColorBlock colors = obj.colors;
			colors.normalColor = color.SetAlpha(colors.normalColor.a);
			obj.colors = colors;
		}
		if (m_TeamChannelTagObject != null)
		{
			m_TeamChannelTagObject.SetActive(m_ChatTeamID != -1);
		}
		if (m_GlobalChannelTagObject != null)
		{
			m_GlobalChannelTagObject.SetActive(m_ChatTeamID == -1);
		}
	}

	public bool HasFocus()
	{
		return m_HasFocus;
	}

	public void SetFocus(bool isFocus)
	{
		if (isFocus == m_HasFocus)
		{
			return;
		}
		Singleton.Manager<ManGameMode>.inst.LockPlayerControls = isFocus;
		if (isFocus)
		{
			SetExpanded(state: true);
			if (m_ChatInput != null)
			{
				m_ChatInput.ActivateInputField();
				m_ChatInput.Select();
			}
			else if (m_ChatInput_TMP != null)
			{
				m_ChatInput_TMP.ActivateInputField();
				m_ChatInput_TMP.Select();
			}
			m_HasFocus = true;
		}
		else
		{
			if (m_ChatInput != null)
			{
				m_ChatInput.DeactivateInputField();
			}
			else if (m_ChatInput_TMP != null)
			{
				m_ChatInput_TMP.DeactivateInputField();
			}
			m_HasFocus = false;
		}
	}

	public bool IsPlayerMuted(NetworkInstanceId netPlayerId)
	{
		if (m_bAllMuted)
		{
			return netPlayerId != Singleton.Manager<ManNetwork>.inst.MyPlayer.netId;
		}
		foreach (NetworkInstanceId mutedPlayer in m_MutedPlayers)
		{
			if (mutedPlayer == netPlayerId)
			{
				return true;
			}
		}
		return false;
	}

	public void MutePlayer(NetPlayer netPlayer, bool mute)
	{
		if (mute)
		{
			m_MutedPlayers.Add(netPlayer.netId);
		}
		else
		{
			m_bAllMuted = false;
			m_MutedPlayers.Remove(netPlayer.netId);
		}
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.MuteNetworkPlayer(netPlayer.GetPlayerIDInLobby(), mute);
	}

	public void MuteAll(bool mute)
	{
		m_bAllMuted = mute;
		m_MutedPlayers.Clear();
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GlobalMuteAll(mute);
	}

	public bool AllMuted()
	{
		return m_bAllMuted;
	}

	private void SetChatWindowText(string text)
	{
		if (m_ChatHistory != null)
		{
			m_ChatHistory.text = text;
		}
		else if (m_ChatHistory_TMP != null)
		{
			m_ChatHistory_TMP.text = text;
		}
	}

	private void SendChatMessage(string message)
	{
		if (Singleton.Manager<ManNetwork>.inst.MyPlayer.IsNotNull())
		{
			if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
			{
				Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.SendChat(message, (m_ChatTeamID != -1) ? Singleton.Manager<ManNetwork>.inst.MyPlayer.LobbyTeamID : (-1), Singleton.Manager<ManNetwork>.inst.MyPlayer.netId.Value);
			}
		}
		else
		{
			d.LogError("UIMPChat unable to send message, because we have no local player");
		}
		if (m_ChatInput != null)
		{
			m_ChatInput.text = "";
		}
		else if (m_ChatInput_TMP != null)
		{
			m_ChatInput_TMP.text = "";
		}
		SetFocus(isFocus: false);
	}

	private void AddChatMessage(ChatMessage msg)
	{
		SetExpanded(state: true);
		if (Singleton.Manager<ManNetwork>.inst.ChatMessageDisplayCountLimit > 0)
		{
			while (m_ChatMessages.Count >= Singleton.Manager<ManNetwork>.inst.ChatMessageDisplayCountLimit)
			{
				m_ChatMessages.Dequeue();
			}
		}
		m_ChatMessages.Enqueue(msg);
		UpdateChat();
		ScrollToBottom();
	}

	private void ScrollToBottom()
	{
		if (!(m_ChatHistoryScrollbar == null))
		{
			m_ChatHistoryScrollbar.value = 0f;
		}
	}

	public void AddChatMessage(LobbyPlayerData playerData, string formatMsg, string userMsg, bool teamChat)
	{
		Color color = Color.black;
		if (playerData.m_PlayerID != TTNetworkID.Invalid)
		{
			color = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetMultiplayerTeamTextColour(playerData.m_TeamID, playerData.m_Colour);
		}
		AddChatMessage(new ChatMessage
		{
			m_Format = formatMsg,
			m_UserMsg = userMsg,
			m_UserId = playerData.m_PlayerID,
			m_UserName = playerData.m_Name,
			m_Col = color,
			m_TimeAdded = Time.time,
			m_TeamMsg = teamChat
		});
	}

	public void AddMissionMessage(string message)
	{
		AddChatMessage(new ChatMessage
		{
			m_Format = "{1}",
			m_Col = Color.white,
			m_TeamMsg = false,
			m_TimeAdded = Time.time,
			m_UserId = TTNetworkID.Invalid,
			m_UserMsg = message,
			m_UserName = "*"
		});
	}

	private void UpdateChat()
	{
		if (m_ChatMessages.Count > 0)
		{
			bool flag = true;
			foreach (ChatMessage chatMessage in m_ChatMessages)
			{
				Color32 col = chatMessage.m_Col;
				if (!flag)
				{
					m_Builder.Append("\n");
				}
				flag = false;
				string arg = ColourConverter.ColourToString(col);
				string arg2 = (chatMessage.m_TeamMsg ? $"<color=#{arg}>{chatMessage.m_UserName} [{Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.NewMenuMain.ChatChannel_Team)}]</color>" : $"<color=#{arg}>{chatMessage.m_UserName}</color>");
				arg2 = $"<b>{arg2}</b>";
				string arg3 = $"<i>{chatMessage.m_UserMsg}</i>";
				string format = (chatMessage.m_TeamMsg ? $"<color=#{arg}>{chatMessage.m_Format}</color>" : chatMessage.m_Format);
				m_Builder.AppendFormat(format, arg2, arg3);
			}
			SetChatWindowText(m_Builder.ToString());
		}
		else
		{
			SetChatWindowText("");
		}
		m_Builder.Remove(0, m_Builder.Length);
	}

	private void OnPlayerTeamChanged(NetPlayer netPlayer)
	{
		SetTeamChannel(netPlayer.LobbyTeamID);
	}

	private void OnPool()
	{
		if (base.HudElementType == ManHUD.HUDElementType.MPChat)
		{
			s_ChatInst = this;
		}
		else if (base.HudElementType == ManHUD.HUDElementType.MPMissionUpdates)
		{
			s_MissionUpdatesInst = this;
		}
	}

	private void OnSpawn()
	{
		SetChatWindowText("");
		if (m_ChatInput != null)
		{
			m_ChatInput.text = "";
		}
		if (m_ChatInput_TMP != null)
		{
			m_ChatInput_TMP.text = "";
		}
		if (m_CollapsedChatInput != null)
		{
			m_CollapsedChatInput.text = "";
		}
		if (m_CollapsedChatInput_TMP != null)
		{
			m_CollapsedChatInput_TMP.text = "";
		}
		SetExpanded(state: false, forceChange: true);
		SetTeamChat(state: false);
		Justify(right: false);
		m_ChatMessages.Clear();
	}

	private void OnRecycle()
	{
		SetTeamChat(state: false);
	}

	private void Update()
	{
		if (HasFocus() && Input.GetKeyDown(KeyCode.Tab))
		{
			ToggleTeamChat();
		}
		if (!m_HasFocus && ((m_ChatInput != null && m_ChatInput.isFocused) || (m_ChatInput_TMP != null && m_ChatInput_TMP.isFocused) || (m_CollapsedChatInput != null && m_CollapsedChatInput.isFocused) || (m_CollapsedChatInput_TMP != null && m_CollapsedChatInput_TMP.isFocused)))
		{
			SetFocus(isFocus: true);
		}
		if (m_ChatMessages.Count > 0 && Singleton.Manager<ManNetwork>.inst.ChatMessageDisplayTimeSecs > 0f)
		{
			ChatMessage chatMessage = m_ChatMessages.Peek();
			if (Time.time - chatMessage.m_TimeAdded > Singleton.Manager<ManNetwork>.inst.ChatMessageDisplayTimeSecs)
			{
				m_ChatMessages.Dequeue();
				UpdateChat();
			}
		}
	}
}
