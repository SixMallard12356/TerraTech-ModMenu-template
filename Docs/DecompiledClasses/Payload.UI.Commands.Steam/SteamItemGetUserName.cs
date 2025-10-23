using Steamworks;

namespace Payload.UI.Commands.Steam;

public class SteamItemGetUserName : Command<SteamDownloadItemData>
{
	private SteamDownloadItemData m_Data;

	private CSteamID m_SteamID;

	private Callback<PersonaStateChange_t> m_Callback;

	public override void Execute(SteamDownloadItemData data)
	{
		m_Data = data;
		m_SteamID = new CSteamID(data.m_Details.m_ulSteamIDOwner);
		m_Callback = Callback<PersonaStateChange_t>.Create(OnPersonaChangeEvent);
		if (!SteamFriends.RequestUserInformation(m_SteamID, bRequireNameOnly: true))
		{
			m_Callback.Dispose();
			data.m_SteamPersonaName = SteamFriends.GetFriendPersonaName(m_SteamID);
			SetComplete(data);
		}
	}

	public void OnPersonaChangeEvent(PersonaStateChange_t personaStateChange)
	{
		if ((personaStateChange.m_nChangeFlags & EPersonaChange.k_EPersonaChangeName) == EPersonaChange.k_EPersonaChangeName && m_SteamID.m_SteamID == personaStateChange.m_ulSteamID)
		{
			m_Callback.Dispose();
			m_Data.m_SteamPersonaName = SteamFriends.GetFriendPersonaName(m_SteamID);
			SetComplete(m_Data);
		}
	}
}
