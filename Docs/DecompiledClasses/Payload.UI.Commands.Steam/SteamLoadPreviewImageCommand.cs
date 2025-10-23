#define UNITY_EDITOR
namespace Payload.UI.Commands.Steam;

public class SteamLoadPreviewImageCommand : Command<SteamDownloadItemData>
{
	public override void Execute(SteamDownloadItemData data)
	{
		data.m_PreviewTexture = FileUtils.LoadTexture(data.m_FileInfoPreview.FullName);
		if (data.m_PreviewTexture != null)
		{
			SetComplete(data);
			return;
		}
		d.LogErrorFormat("SteamItemGetPreviewImageCommand - Could not load preview image at path {0}", data.m_FileInfoPreview.FullName);
		SetCancelled(data);
	}
}
