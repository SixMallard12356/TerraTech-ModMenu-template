using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CanaryLogin : UIScreen
{
	[SerializeField]
	private InputField m_Username;

	[SerializeField]
	private InputField m_Password;

	public void Check()
	{
		StartCoroutine(TryLog());
	}

	private IEnumerator TryLog()
	{
		string uri = "http://forum.terratechgame.com/announce/IsCanary.php";
		WWWForm wWWForm = new WWWForm();
		wWWForm.AddField("Username", m_Username.text);
		wWWForm.AddField("Password", m_Password.text);
		UnityWebRequest request = UnityWebRequest.Post(uri, wWWForm);
		yield return request.SendWebRequest();
		if (request.isNetworkError || request.isHttpError || !request.error.NullOrEmpty() || request.downloadHandler.text == "failed")
		{
			((UIScreenNotifications)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen)).Set(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 12), Application.Quit, delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			}, Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 15), Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 91));
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
		}
		else
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
		}
	}
}
