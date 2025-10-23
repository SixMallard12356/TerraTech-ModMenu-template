#define UNITY_EDITOR
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Ionic.Zlib;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIScreenBugReport : UIScreen
{
	[SerializeField]
	private Text m_Body;

	[SerializeField]
	private Text m_Email;

	[SerializeField]
	private Button m_BackButton;

	[SerializeField]
	private GameObject m_ReportLayout;

	[SerializeField]
	private GameObject m_WaitLayout;

	[SerializeField]
	private RectTransform m_SpinnerImage;

	[SerializeField]
	private float m_SpinSpeed;

	[SerializeField]
	private Text m_Description;

	private string m_Secret = "piuebfiausdabnfSADASFfaiusdbfiu3498rj03rjf00rn23jr203";

	private string m_MainURL = "https://forum.terratechgame.com/announce/SubmitBug.php";

	private string m_StackTrace = "";

	private bool m_EmailInvalid;

	private bool m_ErrorCatcher;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		m_ReportLayout.SetActive(value: true);
		m_WaitLayout.SetActive(value: false);
		BlockScreenExit(m_ErrorCatcher);
		m_BackButton.gameObject.SetActive(!m_ErrorCatcher);
		m_BackButton.interactable = !m_ErrorCatcher;
		if (m_ErrorCatcher)
		{
			Singleton.Manager<ManUI>.inst.SetEnableScreenChange(enable: false);
		}
	}

	public void Set(string stack, bool stackReport)
	{
		m_ErrorCatcher = stackReport;
		m_StackTrace = stack;
		int stringID = (stackReport ? 47 : 49);
		m_Description.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, stringID);
	}

	public void Post()
	{
		string text = m_Email.text;
		string hash = PostAnnouncement.Md5Sum(text + m_Secret);
		StartCoroutine(PostIt(text, hash));
		m_ReportLayout.SetActive(value: false);
		m_WaitLayout.SetActive(value: true);
	}

	public void ExitScreen()
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
		if (m_ErrorCatcher)
		{
			ShowContinueAfterCrashNotification();
		}
	}

	private IEnumerator PostIt(string email, string hash)
	{
		WWWForm wWWForm = new WWWForm();
		wWWForm.headers["Content-Type"] = "application/x-www-form-urlencoded";
		wWWForm.AddField("email", email);
		wWWForm.AddField("hash", hash);
		wWWForm.AddField("body", m_Body.text);
		wWWForm.AddField("version", SKU.ChangelistVersion);
		wWWForm.AddField("mods", Singleton.Manager<ManMods>.inst.GetModsInCurrentSession() ?? "");
		wWWForm.AddField("stack", m_StackTrace);
		wWWForm.AddField("platform", Enum.GetName(typeof(RuntimePlatform), Application.platform));
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (!currentUser.m_LastUsedSaveName.NullOrEmpty())
		{
			string path = ManSaveGame.CreateGameSaveFilePath(currentUser.m_LastUsedSaveType, currentUser.m_LastUsedSaveName);
			if (File.Exists(path))
			{
				byte[] contents;
				using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
				{
					byte[] array = new byte[fileStream.Length];
					fileStream.Read(array, 0, (int)fileStream.Length);
					contents = array;
				}
				wWWForm.AddBinaryData("save", contents);
			}
		}
		string logPath = GetLogPath();
		if (!logPath.NullOrEmpty() && File.Exists(logPath))
		{
			byte[] array3;
			using (FileStream fileStream2 = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				byte[] array2 = new byte[fileStream2.Length];
				fileStream2.Read(array2, 0, (int)fileStream2.Length);
				array3 = array2;
			}
			d.Log(array3.Length);
			wWWForm.AddBinaryData("log", GZipStream.CompressBuffer(array3));
		}
		UnityWebRequest unityWebRequest = UnityWebRequest.Post(m_MainURL, wWWForm);
		yield return unityWebRequest.SendWebRequest();
		Singleton.Manager<ManUI>.inst.PopScreen();
		if (m_ErrorCatcher)
		{
			ShowContinueAfterCrashNotification();
		}
	}

	private void ShowContinueAfterCrashNotification()
	{
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 14);
		Action accept = delegate
		{
			RestartApplication();
		};
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 15);
		uIScreenNotifications.Set(localisedString, accept, localisedString2);
		Singleton.Manager<ManUI>.inst.SetEnableScreenChange(enable: true);
		Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenNotifications, ManUI.PauseType.Pause);
		Singleton.Manager<ManUI>.inst.SetEnableScreenChange(enable: false);
	}

	private void RestartApplication()
	{
		Singleton.Manager<ManUI>.inst.SetEnableScreenChange(enable: true);
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
		Application.Quit();
	}

	public string GetLogPath()
	{
		return Application.persistentDataPath + "\\output_log.txt";
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

	private void Update()
	{
		if (m_WaitLayout.activeInHierarchy)
		{
			m_SpinnerImage.transform.eulerAngles += Vector3.forward * Time.unscaledDeltaTime * m_SpinSpeed;
		}
	}
}
