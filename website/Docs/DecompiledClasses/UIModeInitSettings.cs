using System;
using UnityEngine;

public class UIModeInitSettings : MonoBehaviour, UIGameModeSettings.ModeInitSettingProvider
{
	[Serializable]
	public struct StringInitSetting
	{
		public string settingName;

		public string settingValue;
	}

	[Serializable]
	public struct ObjectInitSetting
	{
		public string settingName;

		public UnityEngine.Object settingValue;
	}

	[SerializeField]
	private StringInitSetting[] m_StringInitSettings;

	[SerializeField]
	private ObjectInitSetting[] m_ObjectInitSettings;

	public virtual void InitComponent()
	{
	}

	public void AddSettings(ManGameMode.ModeSettings modeSettings)
	{
		if (m_StringInitSettings != null)
		{
			StringInitSetting[] stringInitSettings = m_StringInitSettings;
			for (int i = 0; i < stringInitSettings.Length; i++)
			{
				StringInitSetting stringInitSetting = stringInitSettings[i];
				if (!stringInitSetting.settingName.NullOrEmpty())
				{
					modeSettings.AddModeInitSetting(stringInitSetting.settingName, stringInitSetting.settingValue);
				}
			}
		}
		if (m_ObjectInitSettings == null)
		{
			return;
		}
		ObjectInitSetting[] objectInitSettings = m_ObjectInitSettings;
		for (int i = 0; i < objectInitSettings.Length; i++)
		{
			ObjectInitSetting objectInitSetting = objectInitSettings[i];
			if (!objectInitSetting.settingName.NullOrEmpty())
			{
				modeSettings.AddModeInitSetting(objectInitSetting.settingName, objectInitSetting.settingValue);
			}
		}
	}
}
