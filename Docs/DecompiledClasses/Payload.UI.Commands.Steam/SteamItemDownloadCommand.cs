#define UNITY_EDITOR
using Steamworks;

namespace Payload.UI.Commands.Steam;

public class SteamItemDownloadCommand : Command<SteamDownloadItemData>
{
	public override void Execute(SteamDownloadItemData data)
	{
		PublishedFileId_t nPublishedFileId = data.m_Details.m_nPublishedFileId;
		bool bHighPriority = true;
		if (SteamUGC.DownloadItem(nPublishedFileId, bHighPriority))
		{
			data.m_WaitingForDownload = true;
			SetComplete(data);
		}
		else
		{
			d.LogErrorFormat("SteamItemDownload Could not begin download for pubFileId {0}. This is probalby due to invalid file id or user login", nPublishedFileId);
			SetCancelled(data);
		}
	}
}
