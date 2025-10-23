using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UILanguageDropdown : MonoBehaviour
{
	public UIDropdown m_MyDropdown;

	public List<LocalisationEnums.Languages> m_Languages;

	private void Awake()
	{
		m_MyDropdown.OnValueChange.Subscribe(LanguageSelected);
	}

	private void LanguageSelected(int id)
	{
		Singleton.Manager<Localisation>.inst.ChangeLanguage(m_Languages[id]);
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (currentUser != null)
		{
			currentUser.m_CurrentLanguage = Singleton.Manager<Localisation>.inst.CurrentLanguage;
			Singleton.Manager<ManProfile>.inst.Save();
		}
	}

	private void OnEnable()
	{
		List<string> list = new List<string>();
		List<LocalisationEnums.MenuLanguageSelect> list2 = ((LocalisationEnums.MenuLanguageSelect[])Enum.GetValues(typeof(LocalisationEnums.MenuLanguageSelect))).Where((LocalisationEnums.MenuLanguageSelect x) => Enum.GetName(typeof(LocalisationEnums.MenuLanguageSelect), x).Contains("Language")).ToList();
		foreach (LocalisationEnums.Languages language in m_Languages)
		{
			string languageName = Enum.GetName(typeof(LocalisationEnums.Languages), language);
			list.Add(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuLanguageSelect, (int)list2.Find((LocalisationEnums.MenuLanguageSelect x) => Enum.GetName(typeof(LocalisationEnums.MenuLanguageSelect), x).Contains(languageName))));
		}
		m_MyDropdown.SetData(list.ToArray(), 20f, m_Languages.IndexOf(Singleton.Manager<Localisation>.inst.CurrentLanguage));
	}
}
