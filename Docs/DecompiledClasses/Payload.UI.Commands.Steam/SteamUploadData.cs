using System.IO;
using Steamworks;
using UnityEngine;

namespace Payload.UI.Commands.Steam;

public struct SteamUploadData
{
	public SteamItemCategory m_Category;

	public bool m_NeedsToAcceptAgreement;

	public TechData m_TechData;

	public ManSaveGame.SaveInfo m_SaveInfo;

	public Texture2D m_CaptureItemPreview;

	public FileInfo m_FileInfoSource;

	public FileInfo m_FileInfoItemPreview;

	public FileInfo m_FileInfoTemp;

	public SteamItemData m_SteamItem;

	public SteamItemMetaData m_MetaData;

	public bool m_GotoItemOnSteam;

	public bool m_CancelledByUser;

	public Texture2D m_SnapshotInput;

	public TechData m_TechDataInput;

	public static SteamUploadData Create(SteamItemCategory category, string name)
	{
		SteamUploadData result = default(SteamUploadData);
		result.m_Category = category;
		result.m_SteamItem.m_Name = name;
		result.m_SteamItem.m_Visibility = ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic;
		result.m_SteamItem.AddTag(Singleton.Manager<ManSteamworks>.inst.Workshop.GetTagBackingValueCategory(result.m_Category));
		result.m_MetaData = SteamItemMetaData.Create(result.m_Category);
		return result;
	}

	public static SteamUploadData Create(SnapshotSteam snapshot)
	{
		SteamUploadData result = default(SteamUploadData);
		result.m_SteamItem.m_PublishedFileID = (PublishedFileId_t)snapshot.m_SteamID;
		result.m_Category = SteamItemCategory.Techs;
		result.m_SteamItem.m_Name = snapshot.techData.Name;
		result.m_SteamItem.m_Visibility = ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic;
		result.m_SteamItem.AddTag(Singleton.Manager<ManSteamworks>.inst.Workshop.GetTagBackingValueCategory(result.m_Category));
		result.m_MetaData = SteamItemMetaData.Create(result.m_Category);
		return result;
	}
}
