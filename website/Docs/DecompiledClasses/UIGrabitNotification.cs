using UnityEngine;
using UnityEngine.UI;

public class UIGrabitNotification : UIHUDElement
{
	[SerializeField]
	private Text m_Text;

	[SerializeField]
	private Text m_ButtonText;

	[SerializeField]
	private Image m_ImageToRecolour;

	[SerializeField]
	private Color m_FailColour;

	[SerializeField]
	private Color m_SuccessColour;

	private bool m_Success;

	private string m_URL;

	private string m_ErrorMessage;

	public void ShowURL(string url)
	{
		m_Success = true;
		m_URL = url;
		m_Text.text = "Grabit Upload Complete";
		m_ButtonText.text = "View 3D Model";
		m_ImageToRecolour.color = m_SuccessColour;
		Show(null);
	}

	public void ShowError(string errorMessage)
	{
		m_Success = false;
		m_ErrorMessage = errorMessage;
		m_Text.text = "Grabit Upload Failed";
		m_ButtonText.text = "View Error Message";
		m_ImageToRecolour.color = m_FailColour;
		Show(null);
	}

	public void OnButtonClicked()
	{
		if (m_Success)
		{
			Application.OpenURL(m_URL);
		}
		else
		{
			(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(Accept: delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			}, notification: m_ErrorMessage, accept: "OKAY");
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen, ManUI.PauseType.Pause);
		}
		HideSelf();
	}

	public void OnCloseClicked()
	{
		HideSelf();
	}
}
