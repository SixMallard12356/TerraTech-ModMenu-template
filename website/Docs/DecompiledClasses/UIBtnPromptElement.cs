using TMPro;
using UnityEngine;

public class UIBtnPromptElement : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI m_LocalisedText;

	private RectTransform rectTrans;

	private int m_CurrentStringID = int.MinValue;

	private bool m_IsEnabled;

	public int RewiredActionID { get; private set; }

	public string Text => m_LocalisedText.text;

	public RectTransform RectTransform
	{
		get
		{
			if (rectTrans == null)
			{
				rectTrans = GetComponent<RectTransform>();
			}
			return rectTrans;
		}
	}

	public void Setup(int rewiredActionId, LocalisedString text)
	{
		RewiredActionID = rewiredActionId;
		SetLocalisedText(text);
	}

	public void SetEnabled(bool enabled)
	{
		if (enabled != m_IsEnabled)
		{
			base.gameObject.SetActive(enabled);
			m_IsEnabled = enabled;
		}
	}

	public void SetLocalisedText(LocalisedString text)
	{
		int stringID = GetStringID(text);
		if (m_CurrentStringID != stringID)
		{
			m_LocalisedText.SetText(text.Value);
			m_CurrentStringID = stringID;
		}
	}

	private int GetStringID(LocalisedString text)
	{
		return text.m_Bank.GetHashCode() ^ text.m_Id.GetHashCode();
	}

	private void OnSpawn()
	{
		m_IsEnabled = base.gameObject.activeSelf;
	}

	private void OnRecycle()
	{
		m_CurrentStringID = int.MinValue;
	}
}
