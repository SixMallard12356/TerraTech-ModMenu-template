#define UNITY_EDITOR
namespace Payload.UI.Commands.Steam;

public class SteamLoadMetaDataCommand : Command<SteamDownloadItemData>
{
	public override void Execute(SteamDownloadItemData data)
	{
		ManSaveGame.LoadObject(ref data.m_MetaData, data.m_FileInfoMetaData.FullName);
		if (data.m_MetaData.m_SKUBuild > 0 || data.m_MetaData.m_SKUBuild == -1)
		{
			SetComplete(data);
			return;
		}
		d.LogErrorFormat("SteamLoadMetaDataCommand - Could not load metaData at path {0}", data.m_FileInfoMetaData.FullName);
		SetCancelled(data);
	}
}
