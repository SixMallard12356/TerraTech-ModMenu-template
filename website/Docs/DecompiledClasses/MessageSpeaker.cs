using UnityEngine;
using UnityEngine.UI;

public class MessageSpeaker : MonoBehaviour
{
	[SerializeField]
	private Text m_SpeakerTitle;

	[SerializeField]
	private Image m_BehindSpeakerImage;

	[SerializeField]
	private Image m_SpeakerImage;

	[SerializeField]
	private Image m_InFrontOfSpeakerImage;

	[SerializeField]
	private CanvasGroup m_CanvasGroup;

	public void SetupSpeaker(string title, Sprite behind, Sprite speaker, Sprite infront)
	{
		string text = ((title != null) ? title : "");
		if (m_SpeakerTitle.text != text)
		{
			m_SpeakerTitle.text = text;
		}
		SetSprite(m_BehindSpeakerImage, behind);
		SetSprite(m_SpeakerImage, speaker);
		SetSprite(m_InFrontOfSpeakerImage, infront);
	}

	public void SetupSpeaker(ManOnScreenMessages.Speaker speaker)
	{
		ManOnScreenMessages.SpeakerData speakerData = Singleton.Manager<ManOnScreenMessages>.inst.GetSpeakerData(speaker);
		SetupSpeaker(speakerData.m_SpeakerTitle.Value, speakerData.m_BehindSpeakerImage, speakerData.m_SpeakerImage, speakerData.m_InFrontOfSpeakerImage);
	}

	public void Show(bool show)
	{
		float num = (show ? 1f : 0f);
		if (m_CanvasGroup.alpha != num)
		{
			m_CanvasGroup.alpha = num;
		}
	}

	private void SetSprite(Image image, Sprite sprite)
	{
		if ((bool)sprite && image.sprite != sprite)
		{
			image.sprite = sprite;
		}
		float a = (sprite ? 1f : 0f);
		Color color = image.color;
		color.a = a;
		if (image.color != color)
		{
			image.color = color;
		}
	}
}
