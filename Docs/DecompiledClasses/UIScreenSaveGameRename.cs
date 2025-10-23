#define UNITY_EDITOR
using System;
using Netease.Oddish.Ingame.Sdk.Entity.ContentFilter;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenSaveGameRename : UIScreen
{
	[SerializeField]
	private InputField m_NameText;

	public Action<string> ConfirmAction;

	public Action CancelAction;

	private string m_StartName;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		d.Assert(m_StartName != null, "UIScreenSaveGameRename.Show must call Configure before being shown!");
		if (!fromStackPop)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.PopUpOpen);
		}
		BlockScreenExit(exitBlocked: true);
		m_NameText.text = m_StartName;
		if (SKU.AllowTextInput)
		{
			if (VirtualKeyboard.IsRequired())
			{
				OpenVirtualKeyboard();
				return;
			}
			m_NameText.Select();
			m_NameText.ActivateInputField();
		}
		else
		{
			m_NameText.interactable = false;
		}
	}

	public override void Hide()
	{
		BlockScreenExit(exitBlocked: false);
		base.Hide();
		m_StartName = null;
	}

	public void Configure(string name, Action<string> confirmCallback, Action cancelCallback)
	{
		d.Assert(name != null, "UIScreenSaveGameRename.Configure Invalid start name passed in. String value must be non-null.");
		m_StartName = name;
		ConfirmAction = confirmCallback;
		CancelAction = cancelCallback;
	}

	public void OpenVirtualKeyboard()
	{
		VirtualKeyboard.PromptInput(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.VirtualKeyboard, 1), Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.VirtualKeyboard, 2), onCompleteHandler: delegate(bool accepted, string result)
		{
			d.Log("UIScreenSaveGameRename - Accepted: " + accepted + " Input = " + ((result == null) ? "NULL" : result));
			if (accepted && !string.IsNullOrEmpty(result))
			{
				m_NameText.text = result;
				OnSave();
			}
			else
			{
				m_NameText.text = m_StartName;
				OnCancel();
			}
		}, defaultText: m_NameText.text, platformParams: new VirtualKeyboard.PlatformParams
		{
			ps_maxInputLength = 27
		});
	}

	public void OnSave()
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
		if (ConfirmAction == null)
		{
			return;
		}
		string gameName = m_NameText.text;
		if (SKU.IsNetEase)
		{
			Singleton.Manager<ManNetEase>.inst.CheckEnteredText(gameName, BannedWordCheckType.Substitute, delegate(BannedWordCheck response)
			{
				if (response.Status == BannedWordCheckStatus.Approved || response.CheckType == BannedWordCheckType.Substitute)
				{
					gameName = response.Content;
					ConfirmAction(gameName);
				}
			});
		}
		else
		{
			d.Log("UIScreenSaveGameRename - Triggering Action for Confirm.  NameText=" + m_NameText.text);
			ConfirmAction(gameName);
		}
	}

	public void OnCancel()
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
		if (CancelAction != null)
		{
			CancelAction();
		}
	}

	public void OnNameChanged(string _)
	{
		m_NameText.text = m_NameText.text.Replace(Environment.NewLine, "");
	}

	public void OnEndEdit(string _)
	{
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(21, ControllerType.Keyboard))
		{
			OnSave();
		}
	}

	public void Update()
	{
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(22, ControllerType.Joystick))
		{
			OnCancel();
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(21, ControllerType.Joystick))
		{
			if (m_NameText.isFocused)
			{
				m_NameText.DeactivateInputField();
			}
			else
			{
				OnSave();
			}
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(57))
		{
			if (VirtualKeyboard.IsRequired())
			{
				OpenVirtualKeyboard();
				return;
			}
			m_NameText.Select();
			m_NameText.ActivateInputField();
		}
	}
}
