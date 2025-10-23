#define UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UIGameModeSettings : MonoBehaviour
{
	public interface ModeInitSettingProvider
	{
		void InitComponent();

		void AddSettings(ManGameMode.ModeSettings modeSettings);
	}

	[Serializable]
	public class ModeRestriction
	{
		public bool m_UseRestriction;

		public string m_RestrictionString;

		public string Restriction
		{
			get
			{
				d.Assert(!m_UseRestriction || !m_RestrictionString.NullOrEmpty(), "Game Mode Settings Restriction is set, but the restriction setting is empty!");
				if (!m_UseRestriction)
				{
					return null;
				}
				return m_RestrictionString;
			}
		}
	}

	[Serializable]
	public class InitStringSetting
	{
		public string m_SettingName;

		public string m_Setting;
	}

	[Serializable]
	public class InitObjectSetting
	{
		public string m_SettingName;

		public UnityEngine.Object m_Setting;
	}

	[SerializeField]
	private ManGameMode.GameType m_ModeToSet;

	[SerializeField]
	[FormerlySerializedAs("m_ModeSettings")]
	private InitStringSetting[] m_ModeSettingStrings;

	[SerializeField]
	private InitObjectSetting[] m_ModeSettingObjects;

	[SerializeField]
	private ModeRestriction m_VehicleModeRestriction;

	[SerializeField]
	private ModeRestriction m_VehicleSubmodeRestriction;

	[SerializeField]
	private ModeRestriction m_UserDataRestriction;

	public void SetNextModeSettings()
	{
		Singleton.Manager<ManGameMode>.inst.NextModeSetting = GetModeSettings();
	}

	public ManGameMode.ModeSettings GetModeSettings()
	{
		ManGameMode.ModeSettings modeSettings = new ManGameMode.ModeSettings();
		Singleton.Manager<ManGameMode>.inst.SetupModeSwitchAction(modeSettings, m_ModeToSet);
		Mode.InitSettings initSettings = new Mode.InitSettings();
		if (m_ModeSettingStrings != null && m_ModeSettingStrings.Length != 0)
		{
			for (int i = 0; i < m_ModeSettingStrings.Length; i++)
			{
				InitStringSetting initStringSetting = m_ModeSettingStrings[i];
				string settingName = initStringSetting.m_SettingName;
				if (!settingName.NullOrEmpty())
				{
					if (settingName.EqualsNoCase("LoadSavedGame"))
					{
						ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
						Singleton.Manager<ManGameMode>.inst.SetupSaveGameToLoad(currentUser.m_LastUsedSaveType, currentUser.m_LastUsedSaveName, currentUser.m_LastUsedSave_WorldGenVersionData);
					}
					else
					{
						initSettings.Add(settingName, initStringSetting.m_Setting.NullOrEmpty() ? null : initStringSetting.m_Setting);
					}
				}
			}
		}
		if (m_ModeSettingObjects != null)
		{
			for (int j = 0; j < m_ModeSettingObjects.Length; j++)
			{
				InitObjectSetting initObjectSetting = m_ModeSettingObjects[j];
				string settingName2 = initObjectSetting.m_SettingName;
				if (!settingName2.NullOrEmpty())
				{
					initSettings.Add(settingName2, initObjectSetting.m_Setting);
				}
			}
		}
		modeSettings.AddModeInitSettings(initSettings);
		modeSettings.m_LoadVehicleModeRestriction = m_VehicleModeRestriction.Restriction;
		modeSettings.m_LoadVehicleSubModeRestriction = m_VehicleSubmodeRestriction.Restriction;
		modeSettings.m_LoadVehicleUserDataRestriction = m_UserDataRestriction.Restriction;
		return modeSettings;
	}
}
