using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIModeInitDropdownSelect : MonoBehaviour, UIGameModeSettings.ModeInitSettingProvider
{
	[Serializable]
	public struct OptionData
	{
		public string Value;

		public LocalisedString DisplayName;
	}

	public enum DataType
	{
		String,
		Int,
		Bool,
		Float
	}

	[SerializeField]
	private UIOptionsBehaviourDropdown m_Dropdown;

	[SerializeField]
	private string m_SettingName;

	[SerializeField]
	private DataType m_SettingValueType;

	[SerializeField]
	private OptionData[] m_Choices;

	[SerializeField]
	private int m_DefaultSelectedIndex;

	public void InitComponent()
	{
		m_Dropdown.ClearOptions();
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
		OptionData[] choices = m_Choices;
		for (int i = 0; i < choices.Length; i++)
		{
			OptionData optionData = choices[i];
			list.Add(new Dropdown.OptionData(optionData.DisplayName.Value));
		}
		m_Dropdown.AddOptions(list);
		m_Dropdown.SetValue(m_DefaultSelectedIndex);
	}

	public void AddSettings(ManGameMode.ModeSettings modeSettings)
	{
		int num = Mathf.Clamp(m_Dropdown.value, 0, m_Choices.Length - 1);
		object settingData = ParseValue(m_Choices[num].Value);
		modeSettings.AddModeInitSetting(m_SettingName, settingData);
	}

	private object ParseValue(string settingStringValue)
	{
		switch (m_SettingValueType)
		{
		case DataType.String:
			return settingStringValue;
		case DataType.Int:
			return int.Parse(settingStringValue);
		case DataType.Bool:
			return bool.Parse(settingStringValue);
		case DataType.Float:
		{
			if (!Util.TryParseFloatInvariant(settingStringValue, out var value))
			{
				throw new FormatException("Unable to parse " + settingStringValue + " to a valid Float value");
			}
			return value;
		}
		default:
			throw new NotImplementedException($"Setting value type {m_SettingValueType} has not been implemented!");
		}
	}

	private void OnValidate()
	{
		if (m_Choices == null || m_Choices.Length == 0)
		{
			return;
		}
		OptionData[] choices = m_Choices;
		for (int i = 0; i < choices.Length; i++)
		{
			OptionData optionData = choices[i];
			if (!optionData.Value.NullOrEmpty())
			{
				ParseValue(optionData.Value);
			}
		}
	}
}
