using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenUserSelect : UIScreen
{
	public enum DisplayType
	{
		SelectUser,
		NewUser
	}

	public RectTransform m_SelectUserPanel;

	public RectTransform m_NewUserPanel;

	public InputField m_UserNameInput;

	public UIDropdown m_ChangeUserDropDown;

	private DisplayType m_DisplayType;

	private bool m_FirstUser;

	public void SetScreenType(DisplayType displayType, bool firstUser)
	{
		m_DisplayType = displayType;
		m_FirstUser = firstUser;
	}

	public void OnClickAddNewUser()
	{
		if (m_DisplayType == DisplayType.NewUser)
		{
			AddNewUser();
		}
	}

	private void ShowPanel(DisplayType displayType)
	{
		switch (displayType)
		{
		case DisplayType.SelectUser:
			m_SelectUserPanel.gameObject.SetActive(value: true);
			m_NewUserPanel.gameObject.SetActive(value: false);
			SetupNameDropdown();
			break;
		case DisplayType.NewUser:
			m_UserNameInput.text = "";
			m_SelectUserPanel.gameObject.SetActive(value: false);
			m_NewUserPanel.gameObject.SetActive(value: true);
			m_UserNameInput.ActivateInputField();
			m_UserNameInput.Select();
			break;
		}
		m_DisplayType = displayType;
		m_ExitButton.gameObject.SetActive(!m_FirstUser);
		BlockScreenExit(m_FirstUser);
	}

	private void SetupNameDropdown()
	{
		List<ManProfile.Profile> profiles = Singleton.Manager<ManProfile>.inst.GetProfiles();
		string[] array = new string[profiles.Count + 1];
		int current = 0;
		array[0] = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 70);
		for (int i = 0; i < profiles.Count; i++)
		{
			int num = i + 1;
			array[num] = profiles[i].m_Name;
			if (profiles[i] == Singleton.Manager<ManProfile>.inst.GetCurrentUser())
			{
				current = num;
			}
		}
		m_ChangeUserDropDown.SetData(array, 250f, current);
		m_ChangeUserDropDown.OnValueChange.Subscribe(UserSelected);
	}

	private void UserSelected(int id)
	{
		if (base.state == State.Show)
		{
			if (id == 0)
			{
				ShowPanel(DisplayType.NewUser);
				return;
			}
			int currentUser = id - 1;
			Singleton.Manager<ManProfile>.inst.SetCurrentUser(currentUser);
			Singleton.Manager<ManUI>.inst.PopScreen();
		}
	}

	private void AddNewUser(string value)
	{
		if (Input.GetKey(KeyCode.Return) && m_DisplayType == DisplayType.NewUser)
		{
			AddNewUser();
		}
	}

	private void AddNewUser()
	{
		Text text = m_UserNameInput.placeholder as Text;
		if ((bool)text && text.text != m_UserNameInput.text && !m_UserNameInput.text.NullOrEmpty())
		{
			ManProfile.Profile profile = new ManProfile.Profile(m_UserNameInput.text);
			Singleton.Manager<ManProfile>.inst.AddUser(profile, setCurrent: true);
			m_FirstUser = false;
			m_DisplayType = DisplayType.SelectUser;
			Singleton.Manager<ManUI>.inst.PopScreen();
		}
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		ShowPanel(m_DisplayType);
		if (!fromStackPop)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.PopUpOpen);
		}
	}

	private void Start()
	{
		if ((bool)m_UserNameInput)
		{
			InputField.SubmitEvent submitEvent = new InputField.SubmitEvent();
			submitEvent.AddListener(AddNewUser);
			m_UserNameInput.onEndEdit = submitEvent;
		}
	}
}
