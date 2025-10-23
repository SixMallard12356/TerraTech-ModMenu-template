#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MiniJSON;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Announcements : Singleton.Manager<Announcements>, IEventSystemHandler
{
	[Serializable]
	public class Announcement
	{
		public int id;

		public string title;

		public string body;

		public byte[] media;

		public string link;

		public string date;

		public string hash = "";

		public int betaSpecific;
	}

	public string announcementUrl = "https://forum.terratechgame.com/announce/announcements.php?id=";

	public Scrollbar scrollbar;

	public RectTransform m_ScrollRect;

	public UIAnnouncement m_AnnouncementPrefab;

	public float imageHeight = 200f;

	[SerializeField]
	private int m_MaxShownAnnouncements = 30;

	private List<UIAnnouncement> UIObjects = new List<UIAnnouncement>();

	private List<Announcement> cachedAnnouncements = new List<Announcement>();

	private List<Texture2D> images = new List<Texture2D>();

	public void ButtonClicked(Text holder)
	{
		Application.OpenURL(holder.text);
	}

	public void OpenURL(string url)
	{
		Application.OpenURL(url);
	}

	private void LoadImagesToTextures()
	{
		foreach (Announcement cachedAnnouncement in cachedAnnouncements)
		{
			Texture2D texture2D = new Texture2D((int)imageHeight, (int)imageHeight);
			texture2D.LoadImage(cachedAnnouncement.media);
			images.Add(texture2D);
		}
	}

	public void StartGetAnnouncements()
	{
		Singleton.instance.StartCoroutine(GetAnnouncements());
	}

	private IEnumerator GetAnnouncements()
	{
		cachedAnnouncements = cachedAnnouncements.ReadFromBinaryFile(ManSaveGame.GetSaveDataFolder() + "/announcements.bin");
		if (cachedAnnouncements != null)
		{
			LoadImagesToTextures();
			CreateUI();
		}
		else
		{
			d.Log("Failed to read cached announcements from file.");
		}
		if (cachedAnnouncements == null)
		{
			cachedAnnouncements = new List<Announcement>();
		}
		int num = 0;
		if (cachedAnnouncements.Count > 0)
		{
			num = cachedAnnouncements[cachedAnnouncements.Count - 1].id - 1;
		}
		UnityWebRequest getAnnouncements = UnityWebRequest.Get(announcementUrl + num);
		yield return getAnnouncements.SendWebRequest();
		if (getAnnouncements.isNetworkError || getAnnouncements.isHttpError || !getAnnouncements.error.NullOrEmpty())
		{
			d.Log("Could not download announcements");
		}
		else
		{
			List<object> downloadedAnnouncements = null;
			try
			{
				Dictionary<string, object> dictionary = Json.Deserialize("{\"announcements\": " + getAnnouncements.downloadHandler.text + "}") as Dictionary<string, object>;
				downloadedAnnouncements = dictionary["announcements"] as List<object>;
				Predicate<Announcement> match = (Announcement announce) => downloadedAnnouncements.Find((object downloadedElem) => int.Parse(string.Concat((downloadedElem as Dictionary<string, object>)["announcementId"])) == announce.id) == null;
				cachedAnnouncements.RemoveAll(match);
			}
			catch (Exception ex)
			{
				d.LogError("Failed to process announcements. " + ex.ToString());
				downloadedAnnouncements = null;
			}
			if (downloadedAnnouncements != null)
			{
				foreach (Dictionary<string, object> announcementData in downloadedAnnouncements)
				{
					Announcement announcement = null;
					bool flag = false;
					Announcement announcement2;
					try
					{
						announcement = cachedAnnouncements.Find((Announcement x) => x.id == int.Parse(string.Concat(announcementData["announcementId"])));
						if (announcement == null)
						{
							goto IL_0266;
						}
						if (announcement.hash == announcementData["hash"] as string)
						{
							continue;
						}
						flag = true;
						goto IL_0266;
						IL_0266:
						announcement2 = new Announcement();
						announcement2.id = int.Parse(string.Concat(announcementData["announcementId"]));
						announcement2.title = announcementData["title"] as string;
						announcement2.body = announcementData["body"] as string;
						announcement2.link = announcementData["link"] as string;
						announcement2.date = announcementData["date"] as string;
						announcement2.hash = announcementData["hash"] as string;
						announcement2.betaSpecific = int.Parse(string.Concat(announcementData["betaSpecific"]));
						goto IL_0361;
					}
					catch (Exception ex2)
					{
						d.LogError("Failed to process announcement. " + ex2.ToString());
						announcement2 = null;
						goto IL_0361;
					}
					IL_0361:
					if (announcement2 != null)
					{
						if (flag)
						{
							int num2 = cachedAnnouncements.IndexOf(announcement);
							cachedAnnouncements[num2] = announcement2;
							yield return Singleton.instance.StartCoroutine(DownloadImage((announcementData["media"] as string) ?? "", num2));
						}
						else
						{
							cachedAnnouncements.Add(announcement2);
							yield return Singleton.instance.StartCoroutine(DownloadImage((announcementData["media"] as string) ?? "", cachedAnnouncements.Count - 1));
						}
					}
				}
				cachedAnnouncements = cachedAnnouncements.OrderByDescending((Announcement x) => x.id).ToList();
				int count = Mathf.Min(cachedAnnouncements.Count, m_MaxShownAnnouncements);
				cachedAnnouncements = cachedAnnouncements.GetRange(0, count);
			}
			cachedAnnouncements.WriteToBinaryFile(ManSaveGame.GetSaveDataFolder() + "/announcements.bin");
			images.Clear();
			LoadImagesToTextures();
		}
		yield return null;
		CreateUI();
	}

	private void CreateUI(int pass = 0)
	{
		if (cachedAnnouncements.Count < UIObjects.Count)
		{
			for (int num = UIObjects.Count - 1; num >= cachedAnnouncements.Count; num--)
			{
				UIObjects[num].Recycle();
				UIObjects.RemoveAt(num);
			}
		}
		for (int i = 0; i < cachedAnnouncements.Count; i++)
		{
			if (i >= UIObjects.Count)
			{
				UIAnnouncement uIAnnouncement = m_AnnouncementPrefab.Spawn();
				UIObjects.Add(uIAnnouncement);
				uIAnnouncement.m_RectTransform.SetParent(m_ScrollRect.transform, worldPositionStays: false);
				uIAnnouncement.m_RectTransform.localScale = Vector3.one;
			}
			UIObjects[i].gameObject.SetActive(value: true);
			UIObjects[i].SetupAnnouncement(cachedAnnouncements[i], images[i], imageHeight);
		}
	}

	private IEnumerator DownloadImage(string link, int id)
	{
		yield return null;
		if (!link.NullOrEmpty())
		{
			UnityWebRequest imageGet = UnityWebRequestTexture.GetTexture(link);
			yield return imageGet.SendWebRequest();
			if (imageGet.isNetworkError || imageGet.isHttpError)
			{
				d.Log("Could not load announcement image: " + imageGet.error);
			}
			else
			{
				cachedAnnouncements[id].media = ((DownloadHandlerTexture)imageGet.downloadHandler).texture.EncodeToPNG();
			}
		}
	}

	private void Start()
	{
		if (!PlayerPrefs.HasKey("Announcements"))
		{
			if (File.Exists(ManSaveGame.GetSaveDataFolder() + "/announcements.bin"))
			{
				try
				{
					File.Delete(ManSaveGame.GetSaveDataFolder() + "/announcements.bin");
				}
				catch (Exception ex)
				{
					d.LogError("Failed to delete announcements.bin: " + ex.ToString());
				}
			}
			PlayerPrefs.SetString("Announcements", "Recreated");
		}
		m_AnnouncementPrefab.CreatePool(10);
		StartGetAnnouncements();
	}
}
