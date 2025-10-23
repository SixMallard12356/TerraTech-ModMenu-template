#define UNITY_EDITOR
using System;
using Rewired;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenNotificationInput : UIScreen
{
	[SerializeField]
	private InputField m_InputField;

	[SerializeField]
	private Text m_Title;

	[SerializeField]
	private Button m_ConfirmButton;

	[SerializeField]
	private Button m_CancelButton;

	[SerializeField]
	private TextMeshProUGUI m_ConfirmButtonLabel;

	public Action<string> ConfirmAction;

	public Action CancelAction;

	private string m_StartText;

	private string m_TitleText;

	private string m_DescText;

	private bool m_UseNewInputHandler;

	private int m_FrameCountWhenShown;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		m_FrameCountWhenShown = Time.frameCount;
		if (!fromStackPop)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.PopUpOpen);
		}
		d.Assert(m_StartText != null, "UIScreenNotificationInput.Show must call Configure before being shown!");
		m_InputField.text = m_StartText;
		m_Title.text = m_TitleText;
		if (SKU.AllowTextInput)
		{
			if (VirtualKeyboard.IsRequired())
			{
				OpenVirtualKeyboard();
			}
			else
			{
				m_InputField.Select();
				m_InputField.ActivateInputField();
			}
		}
		else
		{
			m_InputField.interactable = false;
		}
		if (m_UseNewInputHandler)
		{
			Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(21, OnUIAccept);
			Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(22, OnUICancel);
			Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(57, OnUIExtra1);
		}
	}

	public override void Hide()
	{
		base.Hide();
		m_StartText = null;
		m_TitleText = null;
		m_DescText = null;
		if (m_UseNewInputHandler)
		{
			Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(21, OnUIAccept);
			Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(22, OnUICancel);
			Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(57, OnUIExtra1);
		}
		m_UseNewInputHandler = false;
	}

	public void Configure(string startText, string title, string confirmLabel, Action<string> onConfirm, Action onCancel)
	{
		d.Assert(startText != null, "UIScreenNotificationInput.Configure Invalid start name passed in. String value must be non-null.");
		m_StartText = startText;
		m_TitleText = title;
		m_ConfirmButtonLabel.text = confirmLabel;
		ConfirmAction = onConfirm;
		CancelAction = onCancel;
	}

	public void SetUseNewInputHandler(bool useNewInputHandler)
	{
		m_UseNewInputHandler = useNewInputHandler;
	}

	private void OpenVirtualKeyboard()
	{
		VirtualKeyboard.EntryCompleteDelegate onCompleteHandler = delegate(bool accepted, string result)
		{
			d.Log("UIScreenNotificationInput - Accepted: " + accepted + " Input = " + ((result == null) ? "NULL" : result));
			if (accepted)
			{
				m_InputField.text = result;
				OnConfirm();
			}
			else
			{
				m_InputField.text = m_StartText;
				OnCancel();
			}
		};
		VirtualKeyboard.PromptInput(m_TitleText, m_DescText, m_StartText, onCompleteHandler);
	}

	private void RespondToConfirmInput()
	{
		if (m_InputField.isFocused)
		{
			m_InputField.DeactivateInputField();
		}
		else
		{
			OnConfirm();
		}
	}

	private void RespondToCancelInput()
	{
		OnCancel();
	}

	private void RespondToExtra1Input()
	{
		if (VirtualKeyboard.IsRequired())
		{
			OpenVirtualKeyboard();
			return;
		}
		m_InputField.Select();
		m_InputField.ActivateInputField();
	}

	public void OnConfirm()
	{
		d.Log($"UIScreenNotificationInput OnConfirm ConfirmAction = {ConfirmAction}");
		if (ConfirmAction != null)
		{
			ConfirmAction(m_InputField.text);
		}
	}

	public void OnCancel()
	{
		d.Log($"UIScreenNotificationInput OnConfirm CancelAction = {CancelAction}");
		if (CancelAction != null)
		{
			CancelAction();
		}
	}

	public void OnEndEdit(string name)
	{
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(21, ControllerType.Keyboard))
		{
			OnConfirm();
		}
	}

	private void OnUIAccept(PayloadUIEventData eventData)
	{
		if (Time.frameCount > m_FrameCountWhenShown)
		{
			RespondToConfirmInput();
			eventData.Use();
		}
	}

	private void OnUICancel(PayloadUIEventData eventData)
	{
		if (Time.frameCount > m_FrameCountWhenShown)
		{
			RespondToCancelInput();
			eventData.Use();
		}
	}

	private void OnUIExtra1(PayloadUIEventData eventData)
	{
		if (Time.frameCount > m_FrameCountWhenShown)
		{
			RespondToExtra1Input();
			eventData.Use();
		}
	}

	private void OnPool()
	{
		m_ConfirmButton.onClick.AddListener(OnConfirm);
		m_CancelButton.onClick.AddListener(OnCancel);
	}

	public void Update()
	{
		if (!m_UseNewInputHandler)
		{
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(22, ControllerType.Joystick))
			{
				RespondToCancelInput();
			}
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(21, ControllerType.Joystick))
			{
				RespondToConfirmInput();
			}
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(57))
			{
				RespondToExtra1Input();
			}
		}
	}
}
