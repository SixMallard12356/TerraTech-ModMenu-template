#define UNITY_EDITOR
using TMPro;
using UnityEngine;

public class UIMessageHUD : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI m_MessageText;

	[SerializeField]
	private MessageSpeaker m_LeftSpeaker;

	[SerializeField]
	private MessageSpeaker m_RightSpeaker;

	[SerializeField]
	private CanvasGroup m_CanvasGroup;

	[SerializeField]
	private float m_FontSizeMaxSwitch;

	public float Alpha
	{
		get
		{
			return m_CanvasGroup.alpha;
		}
		set
		{
			m_CanvasGroup.alpha = value;
		}
	}

	public void SetSpeaker(ManOnScreenMessages.Speaker speakerType, ManOnScreenMessages.Side side)
	{
		if (speakerType == ManOnScreenMessages.Speaker.None)
		{
			m_LeftSpeaker.Show(show: false);
			m_RightSpeaker.Show(show: false);
			return;
		}
		bool flag = side == ManOnScreenMessages.Side.Right;
		(flag ? m_RightSpeaker : m_LeftSpeaker).SetupSpeaker(speakerType);
		m_LeftSpeaker.Show(!flag);
		m_RightSpeaker.Show(flag);
	}

	public void ShowMessage(string message)
	{
		if (!m_MessageText.gameObject.activeSelf)
		{
			m_MessageText.gameObject.SetActive(value: true);
		}
		if (m_MessageText.text != message)
		{
			m_MessageText.text = message;
			m_MessageText.rectTransform.SetAsLastSibling();
		}
	}

	public void ClearMessage()
	{
		if (m_MessageText.gameObject.activeInHierarchy)
		{
			m_MessageText.gameObject.SetActive(value: false);
		}
	}

	public void Hide(bool hide)
	{
		base.gameObject.SetActive(!hide);
	}

	private void Start()
	{
		m_LeftSpeaker.Show(show: false);
		m_RightSpeaker.Show(show: false);
		if (m_MessageText == null)
		{
			d.LogError("UIMessageHUD.m_MessageText is NULL! Please fix me up!");
			return;
		}
		m_MessageText.text = "";
		if (SKU.SwitchUI)
		{
			m_MessageText.fontSizeMax = Mathf.Max(m_MessageText.fontSizeMax, m_FontSizeMaxSwitch);
		}
	}
}
