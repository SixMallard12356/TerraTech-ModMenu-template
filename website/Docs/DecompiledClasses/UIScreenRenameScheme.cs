#define UNITY_EDITOR
using System;
using Netease.Oddish.Ingame.Sdk.Entity.ContentFilter;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenRenameScheme : UIScreen
{
	[SerializeField]
	private InputField m_NameText;

	[SerializeField]
	private GameObject m_ConsoleCommandText;

	[SerializeField]
	private GameObject[] m_HiddenUIOnConsole;

	private Event<string> OnSaved;

	private EventNoParams OnCancelled;

	private string m_PreInputName;

	private ControlScheme m_ControlScheme;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		BlockScreenExit(exitBlocked: true);
		m_NameText.text = m_PreInputName;
		m_NameText.characterLimit = ControlSchemeLibrary.GetMaxNameLength();
		ToggleJoypadUI(Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled());
		if (VirtualKeyboard.IsRequired())
		{
			OpenVirtualKeyboard();
			return;
		}
		m_NameText.Select();
		m_NameText.ActivateInputField();
	}

	private void ToggleJoypadUI(bool on)
	{
		GameObject[] hiddenUIOnConsole = m_HiddenUIOnConsole;
		for (int i = 0; i < hiddenUIOnConsole.Length; i++)
		{
			hiddenUIOnConsole[i].gameObject.SetActive(!on);
		}
		m_ConsoleCommandText.SetActive(on);
	}

	public void OpenVirtualKeyboard()
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.VirtualKeyboard, 1);
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.VirtualKeyboard, 2);
		VirtualKeyboard.EntryCompleteDelegate onCompleteHandler = delegate(bool accepted, string result)
		{
			d.Log("OpenVirtualKeyboard - Accepted: " + accepted + " Input = " + ((result == null) ? "NULL" : result));
			if (accepted && !string.IsNullOrEmpty(result))
			{
				m_NameText.text = result;
				OnSave();
			}
			else
			{
				m_NameText.text = m_PreInputName;
				OnCancel();
			}
		};
		VirtualKeyboard.PlatformParams platformParams = VirtualKeyboard.PlatformParams.Default;
		platformParams.ps_maxInputLength = m_NameText.characterLimit;
		VirtualKeyboard.PromptInput(localisedString, localisedString2, m_NameText.text, onCompleteHandler, platformParams);
	}

	public override void Hide()
	{
		m_NameText.text = m_PreInputName;
		base.Hide();
	}

	public void Set(ControlScheme controlScheme, Action<string> saveAction, Action cancelAction)
	{
		m_ControlScheme = controlScheme;
		m_PreInputName = m_ControlScheme.GetName();
		OnSaved.Clear();
		OnSaved.Subscribe(saveAction);
		OnCancelled.Clear();
		OnCancelled.Subscribe(cancelAction);
	}

	public void OnSave()
	{
		if (SKU.IsNetEase)
		{
			Singleton.Manager<ManNetEase>.inst.CheckEnteredText(m_NameText.text, BannedWordCheckType.Substitute, delegate(BannedWordCheck response)
			{
				if (response.Status == BannedWordCheckStatus.Approved || response.CheckType == BannedWordCheckType.Substitute)
				{
					m_NameText.text = response.Content;
					OnSaved.Send(m_NameText.text);
				}
			});
		}
		else
		{
			OnSaved.Send(m_NameText.text);
		}
	}

	public void OnEndEdit(string name)
	{
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(21, ControllerType.Keyboard))
		{
			OnSave();
		}
	}

	public void OnCancel()
	{
		OnCancelled.Send();
	}

	private void Update()
	{
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
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(22, ControllerType.Joystick))
		{
			if (m_NameText.isFocused)
			{
				m_NameText.text = m_PreInputName;
				m_NameText.DeactivateInputField();
			}
			else
			{
				OnCancel();
			}
		}
	}
}
