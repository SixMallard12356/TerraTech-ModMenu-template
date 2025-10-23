#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateNotifier : MonoBehaviour
{
	public class UpdateNote
	{
		public string m_Title;

		public string m_Version;

		public string m_Notes;

		public string m_Link;
	}

	[Serializable]
	public struct GameNotification
	{
		public string m_UniqueID;

		public LocalisedString m_MessageToDisplay;
	}

	public float m_DisableCloseTime = 10f;

	public string m_UpdateAvailableTitle;

	public string m_NotConnectedTitle;

	[Multiline]
	public string m_NotConnctedToInternetMsg;

	[SerializeField]
	private bool m_ShowNotification;

	[SerializeField]
	private GameNotification m_NotificationToShow;

	private static string m_UpdateLink = "http://forum.terratechgame.com/announce/updates.php";

	private UpdateNote m_CurrentUpdate;

	public void StartLoading()
	{
		GoToGame();
		_ = SKU.AnnouncerEnabled;
	}

	private IEnumerator GetUpdate()
	{
		yield return null;
		UnityWebRequest getUpdate = UnityWebRequest.Get(m_UpdateLink);
		yield return getUpdate.SendWebRequest();
		m_CurrentUpdate = new UpdateNote();
		if (getUpdate.isNetworkError || getUpdate.isHttpError || !getUpdate.error.NullOrEmpty())
		{
			d.Log("Could not download update: " + getUpdate.error);
			if ((DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastUpdateChecked"))).TotalDays > 14.0)
			{
				m_CurrentUpdate.m_Title = m_NotConnectedTitle;
				m_CurrentUpdate.m_Notes = m_NotConnctedToInternetMsg;
				m_CurrentUpdate.m_Link = null;
				ShowUpdate();
			}
		}
		else if (getUpdate.downloadHandler.text.Length > 2)
		{
			PlayerPrefs.SetString("LastUpdateChecked", DateTime.Now.ToString("o"));
			if (Json.Deserialize(getUpdate.downloadHandler.text.Substring(1, getUpdate.downloadHandler.text.Length - 2)) is Dictionary<string, object> dictionary)
			{
				m_CurrentUpdate.m_Version = dictionary["changelist_version"] as string;
				m_CurrentUpdate.m_Notes = dictionary["notes"] as string;
				m_CurrentUpdate.m_Link = dictionary["link"] as string;
				if (SKU.IsVersionMoreRecentThanBuild(m_CurrentUpdate.m_Version))
				{
					m_CurrentUpdate.m_Title = m_UpdateAvailableTitle;
					ShowUpdate();
				}
			}
		}
		yield return null;
	}

	private void GoToGame()
	{
		ManProfile.Profile profile = null;
		profile = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (profile == null)
		{
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NewUser);
		}
		else if (m_ShowNotification && !profile.m_PlayerNotifications.Contains(m_NotificationToShow.m_UniqueID))
		{
			UIScreenNotifications obj = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			string value = m_NotificationToShow.m_MessageToDisplay.Value;
			Action accept = delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			};
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
			obj.Set(value, accept, localisedString);
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
			profile.m_PlayerNotifications.Add(m_NotificationToShow.m_UniqueID);
		}
	}

	private void ShowUpdate()
	{
		if (Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeAttract>())
		{
			UIScreenHumbleUpdate uIScreenHumbleUpdate = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.HumbleUpdateNotification) as UIScreenHumbleUpdate;
			uIScreenHumbleUpdate.Setup(m_CurrentUpdate, m_DisableCloseTime);
			Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenHumbleUpdate);
		}
	}
}
