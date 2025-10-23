#define UNITY_EDITOR
using Steamworks;

namespace Payload.UI.Commands.Steam;

public class SteamSetFavouriteCommand : Command<SteamUploadData>
{
	private SteamUploadData m_Data;

	private CallResult<UserFavoriteItemsListChanged_t> m_SubmitResult;

	public override void Execute(SteamUploadData data)
	{
		if (!Singleton.Manager<ManSteamworks>.inst.Inited)
		{
			d.LogError("SteamSetFavouriteCommand - Steamworks not initialised");
			SetCancelled(data);
			return;
		}
		m_Data = data;
		if (m_SubmitResult == null)
		{
			m_SubmitResult = CallResult<UserFavoriteItemsListChanged_t>.Create(OnItemFavorited);
		}
		AppId_t appID = Singleton.Manager<ManSteamworks>.inst.AppID;
		PublishedFileId_t publishedFileID = m_Data.m_SteamItem.m_PublishedFileID;
		SteamAPICall_t hAPICall = (m_Data.m_SteamItem.m_Favourite ? SteamUGC.AddItemToFavorites(appID, publishedFileID) : SteamUGC.RemoveItemFromFavorites(appID, publishedFileID));
		m_SubmitResult.Set(hAPICall);
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 39);
		uIScreenNotifications.Set(localisedString);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
	}

	private void OnItemFavorited(UserFavoriteItemsListChanged_t result, bool ioFailure)
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
		d.Log("SteamSetFavouriteCommand - item set favourite = " + m_Data.m_SteamItem.m_Favourite.ToString() + " result: " + result.m_eResult);
		if (!ioFailure && result.m_eResult == EResult.k_EResultOK)
		{
			SetComplete(m_Data);
			return;
		}
		d.LogError("SteamSetFavouriteCommand - Steamworks callback error " + result.m_eResult);
		SetCancelled(m_Data);
	}
}
