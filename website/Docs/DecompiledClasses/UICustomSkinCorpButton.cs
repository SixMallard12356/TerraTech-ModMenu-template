#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Toggle))]
public class UICustomSkinCorpButton : MonoBehaviour
{
	[SerializeField]
	private Image m_ButtonImage;

	[SerializeField]
	private Image m_CorpIcon;

	[SerializeField]
	private Toggle m_Button;

	public Event<FactionSubTypes> CorpButtonClickedEvent;

	private FactionSubTypes m_Corp;

	public void OnButtonClicked(bool on)
	{
		if (on)
		{
			CorpButtonClickedEvent.Send(m_Corp);
		}
	}

	public void SetupButton(FactionSubTypes corp, Sprite buttonImage, Sprite corpIcon)
	{
		m_Corp = corp;
		m_ButtonImage.sprite = buttonImage;
		m_CorpIcon.sprite = corpIcon;
	}

	public FactionSubTypes GetCorp()
	{
		return m_Corp;
	}

	public void SetButtonImage(Sprite image)
	{
		m_ButtonImage.sprite = image;
	}

	private void OnSpawn()
	{
		d.Assert(m_ButtonImage.IsNotNull());
		d.Assert(m_CorpIcon.IsNotNull());
		d.Assert(m_Button.IsNotNull());
		m_Button.onValueChanged.AddListener(OnButtonClicked);
	}

	private void OnRecycle()
	{
		m_Button.onValueChanged.RemoveListener(OnButtonClicked);
		CorpButtonClickedEvent.EnsureNoSubscribers();
	}
}
