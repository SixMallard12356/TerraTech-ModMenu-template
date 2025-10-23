#define UNITY_EDITOR
using TerraTech.Network;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIModeInitVisibilityDropdown : MonoBehaviour, UIGameModeSettings.ModeInitSettingProvider, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	[SerializeField]
	private string m_InitSettingName;

	[SerializeField]
	private Lobby.LobbyVisibility m_DefaultSetting = Lobby.LobbyVisibility.Public;

	private UILobbyVisibilityDropdown m_Dropdown;

	public virtual void InitComponent()
	{
		if (m_Dropdown == null)
		{
			m_Dropdown = GetComponentInChildren<UILobbyVisibilityDropdown>();
			d.Assert(m_Dropdown != null, "UIModeInitDropdown Could not find UILobbyVisibilityDropdown on this component or its children!");
		}
		if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.SupportsVisibilityType(m_DefaultSetting))
		{
			m_Dropdown.Visibility = m_DefaultSetting;
		}
		else if (m_DefaultSetting == Lobby.LobbyVisibility.Public || m_DefaultSetting == Lobby.LobbyVisibility.FriendsOnly)
		{
			m_Dropdown.Visibility = Lobby.LobbyVisibility.Private;
		}
		else
		{
			d.LogErrorFormat("VisibilityDropdown: Unable to set lobby visibility to {0}, but there is no appropriate fallback", m_DefaultSetting);
		}
		m_Dropdown.DropDown.RefreshShownValue();
	}

	public void AddSettings(ManGameMode.ModeSettings modeSettings)
	{
		int visibility = (int)m_Dropdown.Visibility;
		modeSettings.AddModeInitSetting(m_InitSettingName, visibility);
	}

	public void OnSelect(BaseEventData data)
	{
		ManBtnPrompt.PromptData promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextToggle);
		Singleton.Manager<ManUI>.inst.UpdateScreenPrompt(promptDataByType);
	}

	public void OnDeselect(BaseEventData data)
	{
		ManBtnPrompt.PromptData promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextToggle);
		Singleton.Manager<ManUI>.inst.ToggleBtnPrompt(active: false, promptDataByType.prompts[0].m_InlineGlyphs);
	}
}
