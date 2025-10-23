#define UNITY_EDITOR
using Steamworks;

namespace Payload.UI.Commands.Steam;

public class SteamConditions
{
	public static bool CheckGoToItem(SteamUploadData data)
	{
		return data.m_GotoItemOnSteam;
	}

	public static bool CheckItemNeedsDownload(SteamDownloadItemData data)
	{
		uint itemState = SteamUGC.GetItemState(data.m_Details.m_nPublishedFileId);
		bool flag = (itemState & 4) != 0;
		bool flag2 = (itemState & 8) != 0;
		d.AssertFormat((itemState & 2) == 0, "SteamConditions.CheckItemNeedsDownload - Steam legacy API items are not supported. Should not be possible as we don't use Legacy upload methods. file id {0}", data.m_Details.m_nPublishedFileId);
		if (flag && !flag2)
		{
			return false;
		}
		return true;
	}

	public static bool CheckWaitingForDownload(SteamDownloadItemData data)
	{
		return data.m_WaitingForDownload;
	}
}
