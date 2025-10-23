using System;
using UnityEngine;

public class UISetModeInitSettings : MonoBehaviour
{
	[Serializable]
	public class InitSetting
	{
		public string m_SettingName;

		public string m_Setting;
	}

	public InitSetting[] m_ModeSettings;

	public void OnButtonClicked()
	{
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.ClearCachedSettings();
		InitSetting[] modeSettings = m_ModeSettings;
		foreach (InitSetting initSetting in modeSettings)
		{
			if (initSetting.m_SettingName.EqualsNoCase("LoadSavedGame"))
			{
				ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
				Singleton.Manager<ManGameMode>.inst.SetupSaveGameToLoad(currentUser.m_LastUsedSaveType, currentUser.m_LastUsedSaveName, currentUser.m_LastUsedSave_WorldGenVersionData);
			}
			else
			{
				Singleton.Manager<ManGameMode>.inst.NextModeSetting.AddModeInitSetting(initSetting.m_SettingName, initSetting.m_Setting.NullOrEmpty() ? null : initSetting.m_Setting);
			}
		}
	}
}
