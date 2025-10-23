using System.IO;
using Steamworks;
using UnityEngine;

namespace Payload.UI.Commands.Steam;

public struct SteamDownloadItemData
{
	public SteamUGCDetails_t m_Details;

	public SteamItemMetaData m_MetaData;

	public FileInfo m_FileInfo;

	public FileInfo m_FileInfoPreview;

	public FileInfo m_FileInfoMetaData;

	public string m_PreviewURL;

	public string m_SteamPersonaName;

	public Texture2D m_PreviewTexture;

	public bool m_WaitingForDownload;

	public SnapshotSteam m_Snaphsot;
}
