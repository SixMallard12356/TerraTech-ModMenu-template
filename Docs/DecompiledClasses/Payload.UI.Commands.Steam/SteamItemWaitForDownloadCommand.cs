#define UNITY_EDITOR
using Steamworks;

namespace Payload.UI.Commands.Steam;

public class SteamItemWaitForDownloadCommand : Command<SteamDownloadItemData>
{
	private Callback<DownloadItemResult_t> m_DownloadResultCallback;

	private SteamDownloadItemData m_Data;

	public override void Execute(SteamDownloadItemData data)
	{
		m_Data = data;
		if (m_DownloadResultCallback == null)
		{
			m_DownloadResultCallback = Callback<DownloadItemResult_t>.Create(OnItemDownloaded);
		}
	}

	private void OnItemDownloaded(DownloadItemResult_t result)
	{
		if (m_Data.m_Details.m_nPublishedFileId == result.m_nPublishedFileId)
		{
			if (result.m_eResult == EResult.k_EResultOK)
			{
				m_Data.m_WaitingForDownload = false;
				SetComplete(m_Data);
			}
			else
			{
				d.LogErrorFormat("SteamItemWaitForDownload - error downloading steam item. Error code {0}", result.m_eResult);
				SetCancelled(m_Data);
			}
		}
	}
}
