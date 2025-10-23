using UnityEngine;
using UnityEngine.UI;

public class UITweet : MonoBehaviour
{
	public InputField m_TweetTextField;

	public Text m_TagsAndLinks;

	public RawImage m_TweetImage;

	public int m_MaxTweetLength = 116;

	public void SetupTweet(Texture2D texture, string messageText, string tagText)
	{
		m_TweetImage.texture = texture;
		m_TweetTextField.text = messageText;
		m_TagsAndLinks.text = tagText;
		m_TweetTextField.characterLimit = m_MaxTweetLength - m_TagsAndLinks.text.Length;
	}

	public string GetTweetMessage()
	{
		return m_TweetTextField.text + " " + m_TagsAndLinks.text;
	}
}
