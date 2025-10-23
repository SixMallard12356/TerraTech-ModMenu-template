#define UNITY_EDITOR
namespace Payload.UI.Saving;

public class ShowConfirmationCommand : SaveCommand
{
	private string m_Notifcation;

	private string m_Accept;

	private string m_Decline;

	private bool m_UsePrevName;

	public void Setup(string notification, string confirmLabel, string cancelLabel, bool usePrevName)
	{
		m_Notifcation = notification;
		m_Accept = confirmLabel;
		m_Decline = cancelLabel;
		m_UsePrevName = usePrevName;
	}

	public override void Execute(SaveOperationData data)
	{
		m_Data = data;
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		string arg = (m_UsePrevName ? data.m_PrevName : data.m_Name);
		string notification = string.Format(m_Notifcation, arg);
		uIScreenNotifications.Set(notification, OnAccept, OnDecline, m_Accept, m_Decline);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
	}

	private void OnAccept()
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
		SetComplete(m_Data);
	}

	private void OnDecline()
	{
		d.Log("ShowConfirmationCommand - OnDecline");
		Singleton.Manager<ManUI>.inst.PopScreen();
		SetCancelled(m_Data);
	}
}
