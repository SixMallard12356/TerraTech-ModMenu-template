#define UNITY_EDITOR
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Payload.UI.Commands.Steam;

public class PackageCommand : Command<SteamUploadData>
{
	private string regexStr = "[^\\w\\s]";

	public override void Execute(SteamUploadData data)
	{
		d.Assert(data.m_CaptureItemPreview, "PackageCommand.Execute - invalid Item Preview render");
		try
		{
			string randomFileName = Path.GetRandomFileName();
			string text = Path.Combine(Path.GetTempPath(), randomFileName);
			Directory.CreateDirectory(text);
			string text2 = Path.Combine(text, "SteamVersion");
			if (!ManSaveGame.SaveObject(data.m_MetaData, text2))
			{
				d.LogError("PackageCommand - Could not save metadata to disk. path: " + text2);
				SetCancelled(data);
				return;
			}
			string input = ((!string.IsNullOrEmpty(data.m_SteamItem.m_Name)) ? data.m_SteamItem.m_Name : "Tech");
			input = Regex.Replace(input, regexStr, string.Empty);
			string text3 = Path.Combine(text, randomFileName + ".png");
			string text4 = Path.Combine(text, input + ".tdc");
			if (data.m_Category == SteamItemCategory.Saves)
			{
				string text5 = Path.Combine(text, data.m_FileInfoTemp.Name);
				File.Copy(data.m_FileInfoTemp.FullName, text5);
				text4 = text5;
			}
			FileUtils.SaveTexture(data.m_CaptureItemPreview, text3);
			data.m_FileInfoItemPreview = new FileInfo(text3);
			if (data.m_Category == SteamItemCategory.Techs)
			{
				TechData.SerializedSnapshotData techSnapshotData = new TechData.SerializedSnapshotData(data.m_TechData);
				if (!Singleton.Manager<ManSnapshots>.inst.ServiceDisk_Desktop.TrySaveTechDataCache(techSnapshotData, text4))
				{
					d.LogError("PackageCommand - Saving tech data cache file failed, path: " + text4);
					SetCancelled(data);
					return;
				}
			}
			data.m_FileInfoTemp = new FileInfo(text4);
			SetComplete(data);
		}
		catch (Exception ex)
		{
			d.LogError("PackageCommand.Execute - a problem occured. " + ex.Message);
			SetCancelled(data);
		}
	}
}
