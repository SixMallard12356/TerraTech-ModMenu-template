#define UNITY_EDITOR
using System;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;
using Steamworks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIScreenPostEmail : UIScreen
{
	[SerializeField]
	private InputField m_EnterEmailField;

	[SerializeField]
	private InputField m_ReEnterEmailField;

	private string m_Secret = "piuebfiausdabnfSADASFfaiusdbfiu3498rj03rjf00rn23jr203";

	private string m_MainURL = "http://forum.terratechgame.com/announce/post_email.php?email=";

	private bool m_EmailInvalid;

	public void Post()
	{
		if (!DoFieldsMatch())
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuOptions, 55);
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
			Action accept = delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			};
			(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(localisedString, accept, localisedString2);
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
			return;
		}
		if (!IsValidEmail(m_EnterEmailField.text))
		{
			string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 1);
			string localisedString4 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
			Action accept2 = delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			};
			(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(localisedString3, accept2, localisedString4);
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
			return;
		}
		string text = m_EnterEmailField.text;
		if (Singleton.Manager<ManSteamworks>.inst.Inited)
		{
			text += $"__{SteamUser.GetSteamID()}";
		}
		string text2 = PostAnnouncement.Md5Sum(text + m_Secret);
		string url = m_MainURL + text + "&hash=" + text2;
		StartCoroutine(PostIt(url));
	}

	private IEnumerator PostIt(string url)
	{
		UnityWebRequest request = UnityWebRequest.Get(url);
		yield return request.SendWebRequest();
		string notification;
		if (!request.isNetworkError && !request.isHttpError && request.error.NullOrEmpty())
		{
			notification = ((!(request.downloadHandler.text == "0")) ? Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuOptions, 51) : Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuOptions, 49));
		}
		else
		{
			d.Log(request.error);
			notification = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuOptions, 50);
		}
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
		Action accept = delegate
		{
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.Options);
		};
		(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(notification, accept, localisedString);
		Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
	}

	public bool IsValidEmail(string strIn)
	{
		m_EmailInvalid = false;
		if (strIn.NullOrEmpty())
		{
			return false;
		}
		strIn = Regex.Replace(strIn, "(@)(.+)$", DomainMapper, RegexOptions.None);
		if (m_EmailInvalid)
		{
			return false;
		}
		return Regex.IsMatch(strIn, "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase);
	}

	public bool DoFieldsMatch()
	{
		return m_EnterEmailField.text == m_ReEnterEmailField.text;
	}

	private string DomainMapper(Match match)
	{
		IdnMapping idnMapping = new IdnMapping();
		string text = match.Groups[2].Value;
		try
		{
			text = idnMapping.GetAscii(text);
		}
		catch (ArgumentException)
		{
			m_EmailInvalid = true;
		}
		return match.Groups[1].Value + text;
	}
}
