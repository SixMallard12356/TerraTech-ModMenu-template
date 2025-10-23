using System.Collections.Generic;
using UnityEngine;

public class UIModeInitEnemyDifficultySelect : MonoBehaviour, UIGameModeSettings.ModeInitSettingProvider
{
	[SerializeField]
	private UIOptionsBehaviourDropdown m_Dropdown;

	public void InitComponent()
	{
		m_Dropdown.ClearOptions();
		List<string> outNamesList = null;
		Singleton.Manager<ManPop>.inst.GetCreativePopDifficultyNames(ref outNamesList);
		m_Dropdown.AddOptions(outNamesList);
		m_Dropdown.SetValue(0);
	}

	public void AddSettings(ManGameMode.ModeSettings modeSettings)
	{
		modeSettings.AddModeInitSetting("EnemyDifficulty", m_Dropdown.value);
	}
}
