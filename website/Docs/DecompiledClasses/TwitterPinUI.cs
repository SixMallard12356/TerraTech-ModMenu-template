using System;
using UnityEngine;
using UnityEngine.UI;

public class TwitterPinUI : MonoBehaviour, TwitterAuthenticator.PinEntryForm
{
	[SerializeField]
	private InputField m_Pin;

	[SerializeField]
	private Text m_UrlDisplay;

	private Action<string> m_OnPinEntrySuccess;

	private Action m_OnPinEntryFailure;

	private string m_AuthUrl;

	public void OnSubmit()
	{
		base.gameObject.SetActive(value: false);
		if (m_OnPinEntrySuccess != null)
		{
			m_OnPinEntrySuccess(m_Pin.text);
		}
		Clear();
	}

	public void SubmitIfInputFieldHasFocus()
	{
		if (m_Pin.isFocused)
		{
			OnSubmit();
		}
	}

	public void OnCancel()
	{
		base.gameObject.SetActive(value: false);
		if (m_OnPinEntryFailure != null)
		{
			m_OnPinEntryFailure();
		}
		Clear();
	}

	public void CopyURLToClipboard()
	{
		TextEditor textEditor = new TextEditor();
		textEditor.text = m_AuthUrl;
		textEditor.SelectAll();
		textEditor.Copy();
	}

	public void BeginPinEntry(string authUrl, Action<string> onPinEntered, Action onPinEntryFailed)
	{
		m_OnPinEntrySuccess = onPinEntered;
		m_OnPinEntryFailure = onPinEntryFailed;
		m_UrlDisplay.text = authUrl;
		m_AuthUrl = authUrl;
		base.gameObject.SetActive(value: true);
	}

	private void Clear()
	{
		m_OnPinEntrySuccess = null;
		m_OnPinEntryFailure = null;
		m_AuthUrl = "";
	}
}
