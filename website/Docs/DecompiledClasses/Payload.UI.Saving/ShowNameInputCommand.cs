#define UNITY_EDITOR
namespace Payload.UI.Saving;

public class ShowNameInputCommand : SaveCommand
{
	public override void Execute(SaveOperationData data)
	{
		m_Data = data;
		string text = m_Data.m_PrevName;
		if (text == null)
		{
			text = Singleton.Manager<ManGameMode>.inst.GetCurrentModeDefaultSaveName();
		}
		UIScreenSaveGameRename uIScreenSaveGameRename = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.SaveGameRename) as UIScreenSaveGameRename;
		uIScreenSaveGameRename.Configure(text, OnComfirm, OnCancel);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenSaveGameRename);
	}

	private void OnComfirm(string name)
	{
		m_Data.m_Name = name;
		SetComplete(m_Data);
	}

	private void OnCancel()
	{
		d.Log("ShowNameInputCommand - Cancelled");
		m_Data.m_Error = "Rename Cancelled";
		SetCancelled(m_Data);
	}
}
