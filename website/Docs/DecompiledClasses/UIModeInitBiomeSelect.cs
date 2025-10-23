using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIModeInitBiomeSelect : MonoBehaviour, UIGameModeSettings.ModeInitSettingProvider
{
	[SerializeField]
	private UIOptionsBehaviourDropdown m_Dropdown;

	[SerializeField]
	private BiomeMapStackSetAsset m_BiomeChoices;

	private List<string> m_OptionValueToBiome = new List<string>();

	public void InitComponent()
	{
		m_Dropdown.ClearOptions();
		m_OptionValueToBiome.Clear();
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
		foreach (BiomeMapStackSetAsset.Entry entry in m_BiomeChoices.Entries)
		{
			if (!entry.m_HideFromUser)
			{
				m_OptionValueToBiome.Add(entry.m_UniqueID);
				list.Add(new Dropdown.OptionData(entry.m_DisplayName.Value));
			}
		}
		m_Dropdown.AddOptions(list);
		m_Dropdown.SetValue(0);
	}

	public void AddSettings(ManGameMode.ModeSettings modeSettings)
	{
		if (m_Dropdown.value < m_OptionValueToBiome.Count)
		{
			modeSettings.AddModeInitSetting("WorldBiome", m_OptionValueToBiome[m_Dropdown.value]);
		}
	}
}
