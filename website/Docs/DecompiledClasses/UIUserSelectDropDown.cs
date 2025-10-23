using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUserSelectDropDown : MonoBehaviour
{
	[SerializeField]
	private UIDropdown m_ChangeUserDropDown;

	[SerializeField]
	private Button m_NewUserButtonPrefab;

	[SerializeField]
	[SortedEnum]
	private ManUI.ScreenType m_CreateNewUserScreen;

	private void SetupNameDropdown()
	{
		List<ManProfile.Profile> profiles = Singleton.Manager<ManProfile>.inst.GetProfiles();
		string[] array = new string[profiles.Count + 1];
		int num = 0;
		array[0] = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 70);
		for (int i = 0; i < profiles.Count; i++)
		{
			int num2 = i + 1;
			array[num2] = profiles[i].m_Name;
			if (profiles[i] == Singleton.Manager<ManProfile>.inst.GetCurrentUser())
			{
				num = num2;
			}
		}
		m_ChangeUserDropDown.SetData(array, 250f, -1);
		m_ChangeUserDropDown.SetLabelText(array[num]);
	}

	private void UserSelected(int id)
	{
		if (id == 0)
		{
			Singleton.Manager<ManUI>.inst.GoToScreen(m_CreateNewUserScreen);
			return;
		}
		int currentUser = id - 1;
		Singleton.Manager<ManProfile>.inst.SetCurrentUser(currentUser);
	}

	private void UserAdded(ManProfile.Profile addedUser)
	{
		SetupNameDropdown();
	}

	private void UserRemoved(ManProfile.Profile removedUser)
	{
		SetupNameDropdown();
	}

	private Button GetButtonPrefab(int idx)
	{
		if (idx == 0)
		{
			return m_NewUserButtonPrefab;
		}
		return null;
	}

	private void Awake()
	{
		m_ChangeUserDropDown.OnValueChange.Subscribe(UserSelected);
		m_ChangeUserDropDown.SetGetButtonCallback(GetButtonPrefab);
		Singleton.Manager<ManProfile>.inst.OnUserAdded.Subscribe(UserAdded);
		Singleton.Manager<ManProfile>.inst.OnUserRemoved.Subscribe(UserRemoved);
	}

	private void OnDestroy()
	{
		m_ChangeUserDropDown.OnValueChange.Unsubscribe(UserSelected);
		m_ChangeUserDropDown.SetGetButtonCallback(null);
		Singleton.Manager<ManProfile>.inst.OnUserAdded.Unsubscribe(UserAdded);
		Singleton.Manager<ManProfile>.inst.OnUserRemoved.Unsubscribe(UserRemoved);
	}

	private void OnEnable()
	{
		SetupNameDropdown();
	}
}
