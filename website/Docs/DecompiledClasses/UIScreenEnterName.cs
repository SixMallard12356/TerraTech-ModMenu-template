#define UNITY_EDITOR
using System;
using Netease.Oddish.Ingame.Sdk.Entity.ContentFilter;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenEnterName : UIScreen
{
	[SerializeField]
	private InputField m_InputField;

	[SerializeField]
	private Button m_NextButton;

	[SerializeField]
	private Text m_ScoreField;

	private Action<string> m_NameEnteredAction;

	public string ScoreText
	{
		set
		{
			if ((bool)m_ScoreField)
			{
				m_ScoreField.text = value;
			}
		}
	}

	public void SetConfirmAction(Action<string> action)
	{
		m_NameEnteredAction = action;
	}

	private void OnEndEdit(string value)
	{
		if ((!Input.GetKey(KeyCode.Return) && !Input.GetKeyDown(KeyCode.KeypadEnter)) || value.NullOrEmpty() || m_NameEnteredAction == null)
		{
			return;
		}
		if (SKU.IsNetEase)
		{
			Singleton.Manager<ManNetEase>.inst.CheckEnteredText(m_InputField.text, BannedWordCheckType.Substitute, delegate(BannedWordCheck response)
			{
				if (response.Status == BannedWordCheckStatus.Approved || response.CheckType == BannedWordCheckType.Substitute)
				{
					m_InputField.text = response.Content;
					m_NameEnteredAction(m_InputField.text);
				}
			});
		}
		else
		{
			m_NameEnteredAction(m_InputField.text);
		}
	}

	private void OnButtonPressed()
	{
		if (m_InputField.text.NullOrEmpty() || m_NameEnteredAction == null)
		{
			return;
		}
		if (SKU.IsNetEase)
		{
			Singleton.Manager<ManNetEase>.inst.CheckEnteredText(m_InputField.text, BannedWordCheckType.Substitute, delegate(BannedWordCheck response)
			{
				if (response.Status == BannedWordCheckStatus.Approved || response.CheckType == BannedWordCheckType.Substitute)
				{
					m_InputField.text = response.Content;
					m_NameEnteredAction(m_InputField.text);
				}
			});
		}
		else
		{
			m_NameEnteredAction(m_InputField.text);
		}
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		BlockScreenExit(exitBlocked: true);
		if (m_NameEnteredAction == null)
		{
			d.LogError("UIScreenNameEntry Show with no enter action set - will be stuck on this screen");
		}
		if (!SKU.AllowTextInput)
		{
			m_InputField.interactable = false;
			m_InputField.text = Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_Name;
		}
	}

	public override void Hide()
	{
		base.Hide();
	}

	private void Awake()
	{
		InputField.SubmitEvent submitEvent = new InputField.SubmitEvent();
		submitEvent.AddListener(OnEndEdit);
		m_InputField.onEndEdit = submitEvent;
		m_NextButton.onClick.AddListener(OnButtonPressed);
	}

	private void Update()
	{
		if (m_InputField.gameObject.activeInHierarchy && !m_InputField.isFocused && SKU.AllowTextInput)
		{
			m_InputField.Select();
			m_InputField.ActivateInputField();
		}
	}
}
