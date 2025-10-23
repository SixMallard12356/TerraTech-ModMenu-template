using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITwitchButton : UIHUDElement
{
	public struct TwitchButtonParams
	{
		public string topLabel;

		public string bottomLabel;

		public UnityAction onClick;

		public UnityAction onClose;
	}

	public Button m_CloseButton;

	public Button m_MainButton;

	public Text m_TopLabel;

	public Text m_BottomLabel;

	public Image m_LED;

	public float m_LEDBlinkSpeed;

	private float m_BlinkTimer;

	public override void Show(object context)
	{
		TwitchButtonParams twitchButtonParams = (TwitchButtonParams)context;
		Init(twitchButtonParams.topLabel, twitchButtonParams.bottomLabel, twitchButtonParams.onClick, twitchButtonParams.onClose);
		base.Show(context);
	}

	public void ShowDismiss(bool show)
	{
		m_CloseButton.gameObject.SetActive(show);
	}

	private void Init(string topLabel, string bottomLabel, UnityAction onClick, UnityAction onClose)
	{
		m_TopLabel.text = topLabel;
		m_BottomLabel.text = bottomLabel;
		m_MainButton.onClick.RemoveAllListeners();
		m_CloseButton.onClick.RemoveAllListeners();
		m_MainButton.onClick.AddListener(onClick);
		m_CloseButton.onClick.AddListener(onClose);
	}

	private void Update()
	{
		m_BlinkTimer += Time.unscaledDeltaTime * m_LEDBlinkSpeed;
		m_LED.color = m_LED.color.SetAlpha(Mathf.Sin(m_BlinkTimer) / 2f + 0.5f);
	}
}
