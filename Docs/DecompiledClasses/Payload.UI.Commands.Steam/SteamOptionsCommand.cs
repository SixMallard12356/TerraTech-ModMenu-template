#define UNITY_EDITOR
namespace Payload.UI.Commands.Steam;

public class SteamOptionsCommand : Command<SteamUploadData>
{
	private SteamUploadData m_Data;

	public override void Execute(SteamUploadData data)
	{
		m_Data = data;
		UIScreenSteamUpload uIScreenSteamUpload = (UIScreenSteamUpload)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.SteamUpload);
		if (data.m_Category == SteamItemCategory.Saves)
		{
			switch (data.m_SaveInfo.m_GameType)
			{
			case ManGameMode.GameType.RaD:
				data.m_SteamItem.AddTag(Singleton.Manager<ManSteamworks>.inst.Workshop.GetTagBackingValueAutoSaves(SteamItemSavesAutoTags.RandD));
				break;
			case ManGameMode.GameType.Creative:
				data.m_SteamItem.AddTag(Singleton.Manager<ManSteamworks>.inst.Workshop.GetTagBackingValueAutoSaves(SteamItemSavesAutoTags.Creative));
				break;
			case ManGameMode.GameType.CoOpCreative:
				data.m_SteamItem.AddTag(Singleton.Manager<ManSteamworks>.inst.Workshop.GetTagBackingValueAutoSaves(SteamItemSavesAutoTags.CoopCreative));
				break;
			}
		}
		uIScreenSteamUpload.Set(m_Data.m_Category, m_Data.m_SteamItem, OnComplete, OnCancel, data.m_SnapshotInput);
		Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.SteamUpload);
	}

	private void OnComplete(SteamItemData itemData)
	{
		d.Assert(!string.IsNullOrEmpty(itemData.m_Name), "Empty name passed in SteamItemData. Something has gone wrong in UIScreenSteamUpload.cs");
		m_Data.m_SteamItem = itemData;
		Singleton.Manager<ManUI>.inst.PopScreen();
		SetComplete(m_Data);
	}

	private void OnCancel()
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
		m_Data.m_CancelledByUser = true;
		SetCancelled(m_Data);
	}
}
