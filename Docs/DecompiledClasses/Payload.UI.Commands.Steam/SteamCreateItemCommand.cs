#define UNITY_EDITOR
using Steamworks;

namespace Payload.UI.Commands.Steam;

public class SteamCreateItemCommand : Command<SteamUploadData>
{
	private SteamUploadData m_Data;

	private CallResult<CreateItemResult_t> m_CreateItemResult;

	public override void Execute(SteamUploadData data)
	{
		if (!Singleton.Manager<ManSteamworks>.inst.Inited)
		{
			d.LogError("SteamCreateItem - Steamworks not initialised");
			SetCancelled(data);
			return;
		}
		m_Data = data;
		if (m_CreateItemResult == null)
		{
			m_CreateItemResult = CallResult<CreateItemResult_t>.Create(OnItemCreated);
		}
		SteamAPICall_t hAPICall = SteamUGC.CreateItem(Singleton.Manager<ManSteamworks>.inst.AppID, EWorkshopFileType.k_EWorkshopFileTypeFirst);
		m_CreateItemResult.Set(hAPICall);
	}

	private void OnItemCreated(CreateItemResult_t result, bool ioFailure)
	{
		m_Data.m_NeedsToAcceptAgreement |= result.m_bUserNeedsToAcceptWorkshopLegalAgreement;
		if (!ioFailure && result.m_eResult == EResult.k_EResultOK)
		{
			m_Data.m_SteamItem.m_PublishedFileID = result.m_nPublishedFileId;
			SetComplete(m_Data);
		}
		else
		{
			d.LogError("SteamCreateItem - Steamworks callback error " + result.m_eResult);
			SetCancelled(m_Data);
		}
	}
}
