#define UNITY_EDITOR
using System;
using System.Threading.Tasks;

namespace Payload.UI.Commands.Steam;

public class SteamItemParseSnapshot : Command<SteamDownloadItemData>
{
	public override void Execute(SteamDownloadItemData data)
	{
		ParseItemDataAsync(data);
	}

	private async void ParseItemDataAsync(SteamDownloadItemData data)
	{
		data = await ParseItemData(data);
		if (data.m_Snaphsot != null)
		{
			SetComplete(data);
			return;
		}
		d.LogError("SteamItemParseSnapshot - Failed to parse tech data for item at path " + data.m_FileInfo.FullName);
		SetCancelled(data);
	}

	private Task<SteamDownloadItemData> ParseItemData(SteamDownloadItemData data)
	{
		return Task.Run(delegate
		{
			string fullName = data.m_FileInfo.FullName;
			if (Singleton.Manager<ManSnapshots>.inst.ServiceDisk_Desktop.TryLoadTechDataCache(fullName, out var techData))
			{
				SnapshotSteam snapshotSteam = new SnapshotSteam
				{
					techData = techData,
					creator = data.m_SteamPersonaName,
					image = data.m_PreviewTexture,
					m_Name = 
					{
						Value = techData.Name
					},
					m_SteamID = (ulong)data.m_Details.m_nPublishedFileId,
					UniqueID = data.m_Details.m_nPublishedFileId.ToString()
				};
				uint rtimeAddedToUserList = data.m_Details.m_rtimeAddedToUserList;
				snapshotSteam.DateCreated = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(rtimeAddedToUserList);
				data.m_Snaphsot = snapshotSteam;
			}
			else
			{
				data.m_Snaphsot = null;
			}
			return data;
		});
	}
}
