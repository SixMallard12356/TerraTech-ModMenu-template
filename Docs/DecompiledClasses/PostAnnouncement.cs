#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using MiniJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PostAnnouncement : MonoBehaviour
{
	public Text title;

	public Text body;

	public Text media;

	public Text link;

	public Text idBox;

	public Toggle beta;

	private string hashKey = "piuebfiausdabnfSADASFfaiusdbfiu3498rj03rjf00rn23jr203";

	public void PostMe()
	{
		StartCoroutine(Post(title.text, body.text.Replace('\n', '~'), (media.text == "Media") ? null : media.text, (link.text == "Link") ? null : link.text, beta.isOn ? 1 : 0));
	}

	public void SubmitEdit()
	{
		string text = ((link.text == "Link") ? "" : ((link.text == "Input") ? "" : ""));
		string text2 = ((media.text == "Media") ? "" : ((link.text == "Input") ? "" : ""));
		if (int.TryParse(idBox.text, out var result))
		{
			StartCoroutine(Edit(result, title.text, body.text.Replace('\n', '~'), text2, text, beta.isOn ? 1 : 0));
		}
	}

	public void GetAnnouncemnt()
	{
		if (int.TryParse(idBox.text, out var result))
		{
			StartCoroutine(GetAnnouncementToEdit(result));
		}
	}

	private IEnumerator GetAnnouncementToEdit(int id)
	{
		string uri = "http://forum.terratechgame.com/announce/edit_announcements.php?id=" + id;
		UnityWebRequest post = UnityWebRequest.Get(uri);
		yield return post.SendWebRequest();
		if (post.isNetworkError || post.isHttpError || !post.error.NullOrEmpty())
		{
			MonoBehaviour.print("There was an error posting the announcement: " + post.error);
			yield break;
		}
		Dictionary<string, object> dictionary = Json.Deserialize(post.downloadHandler.text.Replace("[", string.Empty).Replace("]", string.Empty)) as Dictionary<string, object>;
		title.text = dictionary["title"] as string;
		body.text = (dictionary["body"] as string).Replace('~', '\n');
		media.text = dictionary["media"] as string;
		link.text = dictionary["link"] as string;
	}

	public IEnumerator Edit(int id, string title, string body, string media, string link, int betaSpecific)
	{
		string value = Md5Sum(title + body + media + link + betaSpecific + hashKey);
		string text = "http://forum.terratechgame.com/announce/edit_announcements.php";
		d.Log(body);
		WWWForm wWWForm = new WWWForm();
		wWWForm.AddField("setId", id);
		wWWForm.AddField("title", title);
		wWWForm.AddField("body", body);
		wWWForm.AddField("media", media);
		wWWForm.AddField("link", link);
		wWWForm.AddField("hash", value);
		wWWForm.AddField("beta", betaSpecific);
		MonoBehaviour.print(text);
		UnityWebRequest post = UnityWebRequest.Post(text, wWWForm);
		yield return post.SendWebRequest();
		if (post.isNetworkError || post.isHttpError || !post.error.NullOrEmpty())
		{
			MonoBehaviour.print("There was an error posting the announcement: " + post.error);
		}
		else
		{
			d.Log(post.downloadHandler.text);
		}
	}

	public IEnumerator Post(string title, string body, string media, string link, int betaSpecific)
	{
		string text = Md5Sum(title + body + media + link + betaSpecific + hashKey);
		string text2 = "http://forum.terratechgame.com/announce/post_announcement.php?";
		text2 = text2 + "title=" + title + "&body=" + body + (media.NullOrEmpty() ? "" : ("&media=" + media)) + (link.NullOrEmpty() ? "" : ("&link=" + link)) + "&hash=" + text + "&beta=" + betaSpecific;
		MonoBehaviour.print(text2);
		UnityWebRequest post = UnityWebRequest.Get(text2);
		yield return post.SendWebRequest();
		if (post.isNetworkError || post.isHttpError || !post.error.NullOrEmpty())
		{
			MonoBehaviour.print("There was an error posting the announcement: " + post.error);
		}
		else
		{
			d.Log(post.downloadHandler.text);
		}
	}

	public static string Md5Sum(string strToEncrypt)
	{
		byte[] bytes = new UTF8Encoding().GetBytes(strToEncrypt);
		byte[] array = new MD5CryptoServiceProvider().ComputeHash(bytes);
		string text = "";
		for (int i = 0; i < array.Length; i++)
		{
			text += Convert.ToString(array[i], 16).PadLeft(2, '0');
		}
		return text.PadLeft(32, '0');
	}
}
