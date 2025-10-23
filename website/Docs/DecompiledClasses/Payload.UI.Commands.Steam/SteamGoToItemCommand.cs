using Steamworks;

namespace Payload.UI.Commands.Steam;

public class SteamGoToItemCommand : Command<SteamUploadData>
{
	public override void Execute(SteamUploadData data)
	{
		SteamFriends.ActivateGameOverlayToWebPage($"steam://url/CommunityFilePage/{data.m_SteamItem.m_PublishedFileID}");
		SetComplete(data);
	}
}
