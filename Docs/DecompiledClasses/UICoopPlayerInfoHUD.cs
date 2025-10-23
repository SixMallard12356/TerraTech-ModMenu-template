#define UNITY_EDITOR
using TerraTech.Network;
using UnityEngine;
using UnityEngine.UI;

public class UICoopPlayerInfoHUD : UIHUDElement
{
	[SerializeField]
	private Button m_ControlPanelButton;

	[SerializeField]
	private Image m_PlayerIcon;

	[SerializeField]
	private Text m_PlayerNameText;

	[SerializeField]
	private Text m_TeamNameText;

	public override void Show(object context)
	{
		base.Show(context);
		NetPlayer myPlayer = Singleton.Manager<ManNetwork>.inst.MyPlayer;
		if (myPlayer != null)
		{
			UpdateDisplay(myPlayer);
			myPlayer.NameSetEvent.Subscribe(OnPlayerInfoChanged);
			myPlayer.OnTeamChanged.Subscribe(OnPlayerInfoChanged);
			myPlayer.OnAvatarChanged.Subscribe(OnPlayerInfoChanged);
		}
		else
		{
			d.LogError("UICoopPlayerInfoHUD cannot find local player on show");
		}
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		NetPlayer myPlayer = Singleton.Manager<ManNetwork>.inst.MyPlayer;
		if (myPlayer != null)
		{
			myPlayer.NameSetEvent.Unsubscribe(OnPlayerInfoChanged);
			myPlayer.OnTeamChanged.Unsubscribe(OnPlayerInfoChanged);
			myPlayer.OnAvatarChanged.Unsubscribe(OnPlayerInfoChanged);
		}
	}

	private void UpdateDisplay(NetPlayer player)
	{
		Color color = Singleton.Manager<ManNetworkLobby>.inst.LobbyConstants.m_CoOpColours[player.LobbyTeamID];
		if (m_PlayerIcon != null)
		{
			m_PlayerIcon.sprite = player.Sprite;
		}
		if (m_PlayerNameText != null)
		{
			m_PlayerNameText.text = player.name;
			m_PlayerNameText.color = color;
		}
		if (m_TeamNameText != null)
		{
			m_TeamNameText.text = ModeCoOp<ModeCoOpCreative>.CreateTeamNameFromID(player.TechTeamID);
			m_TeamNameText.color = color;
		}
	}

	private static void TogglePlayerInfo()
	{
		if (!Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.PlayerInfo))
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.PlayerInfo);
		}
		else
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.PlayerInfo);
		}
	}

	private void OnPlayerInfoChanged(NetPlayer player)
	{
		UpdateDisplay(player);
	}

	public void OnPool()
	{
		RegisterObscuredBy(ManHUD.HUDElementType.TechLoader);
		if (m_ControlPanelButton != null)
		{
			m_ControlPanelButton.onClick.AddListener(TogglePlayerInfo);
		}
	}
}
