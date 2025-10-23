namespace Payload.UI.Saving;

public class ShowAlertCommand : SaveCommand
{
	private string m_Notifcation;

	private string m_Confirm;

	public void Setup(string notificationMessage, string confirmLabel)
	{
		m_Notifcation = notificationMessage;
		m_Confirm = confirmLabel;
	}

	public override void Execute(SaveOperationData data)
	{
		m_Data = data;
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		uIScreenNotifications.Set(m_Notifcation, OnAccept, m_Confirm);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
	}

	private void OnAccept()
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
		SetComplete(m_Data);
	}
}
