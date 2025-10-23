#define UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIAnnouncement : MonoBehaviour
{
	public Text m_Title;

	public Text m_Body;

	public Text m_Date;

	public Image m_Image;

	public Image m_BetaSpecific;

	public Text m_Link;

	private RectTransform m_Rect;

	public RectTransform m_RectTransform
	{
		get
		{
			if (!(m_Rect == null))
			{
				return m_Rect;
			}
			return GetComponent<RectTransform>();
		}
		private set
		{
			m_Rect = value;
		}
	}

	public void SetupAnnouncement(Announcements.Announcement announcement, Texture2D image, float imageHeight)
	{
		m_Title.text = announcement.title;
		if (!announcement.date.NullOrEmpty())
		{
			if (DateTime.TryParse(announcement.date, out var result))
			{
				m_Date.text = result.ToString("ddd d MMMM yyyy");
			}
			else
			{
				m_Date.text = "";
				d.LogError("Date is wrong format in the announcement");
			}
		}
		else
		{
			m_Date.text = "";
			d.LogError("Date is missing in the announcement");
		}
		m_Link.text = announcement.link;
		if (!announcement.body.NullOrEmpty())
		{
			m_Body.text = announcement.body.Replace('~', '\n');
		}
		else
		{
			d.LogError("Announcement is missing a body!");
			m_Body.text = "";
		}
		if (image.width > 20)
		{
			m_Image.sprite = Sprite.Create(image, new Rect(0f, 0f, image.width, image.height), new Vector2(0.5f, 0.5f));
			m_Image.rectTransform.sizeDelta = new Vector2(m_Image.rectTransform.sizeDelta.x, imageHeight);
			m_Image.gameObject.SetActive(value: true);
		}
		else
		{
			m_Image.sprite = null;
			m_Image.gameObject.SetActive(value: false);
		}
		m_BetaSpecific.enabled = announcement.betaSpecific == 1;
	}

	public float GetSize()
	{
		Canvas.ForceUpdateCanvases();
		return m_Title.rectTransform.sizeDelta.y + m_Body.rectTransform.sizeDelta.y + m_Date.rectTransform.sizeDelta.y + m_Image.rectTransform.sizeDelta.y + m_BetaSpecific.rectTransform.sizeDelta.y + m_Link.rectTransform.sizeDelta.y;
	}

	public void OnButtonClicked(Text text)
	{
		if (!text.text.NullOrEmpty())
		{
			Application.OpenURL(text.text);
		}
	}

	public void Awake()
	{
		m_RectTransform = GetComponent<RectTransform>();
	}
}
