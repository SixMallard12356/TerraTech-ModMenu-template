#define UNITY_EDITOR
using Netease.Oddish.Ingame.Sdk.Entity.ContentFilter;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenNewUser : UIScreen
{
	[SerializeField]
	private InputField m_UserNameInput;

	[SerializeField]
	private LocalisedString m_NameAlreadyTakenString;

	private bool m_CheckingNameFilter;

	public void OnClickAddNewUser()
	{
		if (SKU.IsNetEase)
		{
			Singleton.Manager<ManNetEase>.inst.CheckEnteredText(m_UserNameInput.text, BannedWordCheckType.Substitute, delegate(BannedWordCheck response)
			{
				if (response.Status == BannedWordCheckStatus.Approved || response.CheckType == BannedWordCheckType.Substitute)
				{
					m_UserNameInput.text = response.Content;
					TryAddNewUser();
				}
			});
		}
		else
		{
			TryAddNewUser();
		}
	}

	private void OnTextEndEdit(string unused)
	{
		if (!Input.GetKey(KeyCode.Return))
		{
			return;
		}
		if (SKU.IsNetEase)
		{
			Singleton.Manager<ManNetEase>.inst.CheckEnteredText(m_UserNameInput.text, BannedWordCheckType.Substitute, delegate(BannedWordCheck response)
			{
				if (response.Status == BannedWordCheckStatus.Approved || response.CheckType == BannedWordCheckType.Substitute)
				{
					m_UserNameInput.text = response.Content;
					TryAddNewUser();
				}
			});
		}
		else
		{
			TryAddNewUser();
		}
	}

	private void TryAddNewUser()
	{
		Text text = m_UserNameInput.placeholder as Text;
		if (m_UserNameInput.text.NullOrEmpty() || ((bool)text && !(text.text != m_UserNameInput.text)))
		{
			return;
		}
		string text2 = m_UserNameInput.text;
		if (Singleton.Manager<ManProfile>.inst.IsUniqueProfileName(text2))
		{
			ManProfile.Profile profile = new ManProfile.Profile(text2);
			if (Singleton.Manager<ManProfile>.inst.AddUser(profile, setCurrent: true))
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			}
			else
			{
				d.LogError("Failed to add user in UIScreenNewUser::TryAddNewUser");
			}
			return;
		}
		string notification = string.Format(m_NameAlreadyTakenString.Value, text2);
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		uIScreenNotifications.Set(notification, delegate
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
		}, localisedString);
		Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenNotifications);
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		if (SKU.AllowTextInput)
		{
			if (!fromStackPop)
			{
				m_UserNameInput.text = "";
			}
			m_UserNameInput.ActivateInputField();
			m_UserNameInput.Select();
		}
		else
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, 66);
			string text = string.Empty;
			int num = 1;
			bool flag = true;
			while (flag)
			{
				text = string.Format(localisedString, num);
				num++;
				flag = Singleton.Manager<ManProfile>.inst.GetUserIndexFromSaveName(text) >= 0;
			}
			m_UserNameInput.text = text;
		}
		bool flag2 = Singleton.Manager<ManProfile>.inst.GetProfiles().Count == 0;
		m_ExitButton.gameObject.SetActive(!flag2);
		BlockScreenExit(flag2);
	}

	private void Start()
	{
		if ((bool)m_UserNameInput)
		{
			InputField.SubmitEvent submitEvent = new InputField.SubmitEvent();
			submitEvent.AddListener(OnTextEndEdit);
			m_UserNameInput.onEndEdit = submitEvent;
			if (!SKU.AllowTextInput)
			{
				m_UserNameInput.interactable = false;
			}
		}
	}

	private void Update()
	{
		if (base.gameObject.activeSelf && Singleton.Manager<ManInput>.inst.GetButtonDown(21, ControllerType.Joystick))
		{
			if (m_UserNameInput.isFocused)
			{
				m_UserNameInput.DeactivateInputField();
			}
			else
			{
				OnClickAddNewUser();
			}
		}
	}
}
