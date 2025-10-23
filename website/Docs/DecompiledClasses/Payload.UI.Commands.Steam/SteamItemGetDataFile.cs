#define UNITY_EDITOR
using System.IO;
using Steamworks;

namespace Payload.UI.Commands.Steam;

public class SteamItemGetDataFile : Command<SteamDownloadItemData>
{
	public override void Execute(SteamDownloadItemData data)
	{
		PublishedFileId_t nPublishedFileId = data.m_Details.m_nPublishedFileId;
		d.Assert(nPublishedFileId.m_PublishedFileId != 0, "Invalid steam published file ID.");
		if (SteamUGC.GetItemInstallInfo(nPublishedFileId, out var _, out var pchFolder, 512u, out var _))
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(pchFolder);
			if (!directoryInfo.Exists)
			{
				d.LogError("SteamGetItemDataCommand - FolderPath doesn not exist. Steam download cache has become corrupted. Eg deleted on disk. Path: " + pchFolder);
				SetCancelled(data);
				return;
			}
			FileInfo[] files = directoryInfo.GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				if (fileInfo.Extension == ".tdc" || fileInfo.Extension == ".sav" || fileInfo.Name.EndsWith("_bundle"))
				{
					data.m_FileInfo = fileInfo;
					continue;
				}
				if (fileInfo.Extension == ".png")
				{
					data.m_FileInfoPreview = fileInfo;
					continue;
				}
				if (fileInfo.Name == "SteamVersion")
				{
					data.m_FileInfoMetaData = fileInfo;
					continue;
				}
				d.LogWarningFormat("SteamGetItemDataCommand - Unused file found in steam workshop item content folder. Path {0}", fileInfo.FullName);
			}
			if (data.m_FileInfo != null && data.m_FileInfoPreview != null)
			{
				SetComplete(data);
				return;
			}
			d.LogError("SteamGetItemDataCommand - Steam workshop item is missing files. Steam download cache has become corrupted. Eg deleted on disk. Path: " + pchFolder);
			SetCancelled(data);
		}
		else
		{
			d.LogErrorFormat("SteamGetItemDataCommand - Error retrieving GetItemInstallInfo for {0} publishedFileID {1}", data.m_Details.m_rgchTitle, nPublishedFileId);
			SetCancelled(data);
		}
	}
}
