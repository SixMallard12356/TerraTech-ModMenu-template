#define UNITY_EDITOR
using Steamworks;

namespace Payload.UI.Commands.Steam;

public class SteamSubmitItemCommand : Command<SteamUploadData>
{
	private SteamUploadData m_Data;

	private CallResult<SubmitItemUpdateResult_t> m_SubmitResult;

	private UGCUpdateHandle_t m_UpdateHandle;

	private bool m_ScreenHidden;

	public override void Execute(SteamUploadData data)
	{
		if (!Singleton.Manager<ManSteamworks>.inst.Inited)
		{
			d.LogError("SteamSetData - Steamworks not initialised");
			SetCancelled(data);
			return;
		}
		m_Data = data;
		UGCUpdateHandle_t uGCUpdateHandle_t = SteamUGC.StartItemUpdate(Singleton.Manager<ManSteamworks>.inst.AppID, data.m_SteamItem.m_PublishedFileID);
		SteamUGC.SetItemTitle(uGCUpdateHandle_t, data.m_SteamItem.m_Name);
		SteamUGC.SetItemVisibility(uGCUpdateHandle_t, data.m_SteamItem.m_Visibility);
		SteamUGC.SetItemContent(uGCUpdateHandle_t, data.m_FileInfoTemp.DirectoryName);
		SteamUGC.SetItemPreview(uGCUpdateHandle_t, data.m_FileInfoItemPreview.FullName);
		if (!string.IsNullOrEmpty(data.m_SteamItem.m_Description))
		{
			SteamUGC.SetItemDescription(uGCUpdateHandle_t, data.m_SteamItem.m_Description);
		}
		if (data.m_SteamItem.HasAnyTags)
		{
			SteamUGC.SetItemTags(uGCUpdateHandle_t, data.m_SteamItem.m_Tags);
		}
		if (m_SubmitResult == null)
		{
			m_SubmitResult = CallResult<SubmitItemUpdateResult_t>.Create(OnItemSubmitted);
		}
		SteamAPICall_t hAPICall = SteamUGC.SubmitItemUpdate(uGCUpdateHandle_t, string.Empty);
		m_SubmitResult.Set(hAPICall);
		m_ScreenHidden = false;
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 39);
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SteamWorkshop, 46);
		uIScreenNotifications.Set(localisedString, OnRunInBackground, localisedString2);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
	}

	private void OnItemSubmitted(SubmitItemUpdateResult_t result, bool ioFailure)
	{
		if (!m_ScreenHidden)
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
		}
		m_Data.m_NeedsToAcceptAgreement |= result.m_bUserNeedsToAcceptWorkshopLegalAgreement;
		d.Log("SteamSubmitItem - item submitted " + result.m_eResult);
		if (!ioFailure && result.m_eResult == EResult.k_EResultOK)
		{
			SetComplete(m_Data);
			return;
		}
		d.LogError("SteamSubmitItem - Steamworks callback error " + result.m_eResult);
		SetCancelled(m_Data);
	}

	private void OnRunInBackground()
	{
		m_ScreenHidden = true;
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
	}
}
