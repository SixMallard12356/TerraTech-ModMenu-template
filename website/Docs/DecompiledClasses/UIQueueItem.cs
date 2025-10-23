using UnityEngine;
using UnityEngine.UI;

public class UIQueueItem : MonoBehaviour
{
	public Image m_Image;

	public Button m_CancelButton;

	public Button m_RepeatButton;

	public Sprite m_Repeat;

	public Sprite m_NoRepeat;

	public Sprite m_Empty;

	public void SetEmpty()
	{
		m_CancelButton.gameObject.SetActive(value: false);
		m_RepeatButton.gameObject.SetActive(value: false);
		m_Image.sprite = m_Empty;
	}

	private void OnCancelled()
	{
	}

	private void OnRepeat()
	{
	}

	private void Awake()
	{
		if ((bool)m_CancelButton)
		{
			m_CancelButton.onClick.AddListener(OnCancelled);
		}
		if ((bool)m_RepeatButton)
		{
			m_RepeatButton.onClick.AddListener(OnRepeat);
		}
	}
}
