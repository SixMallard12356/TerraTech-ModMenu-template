using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UISetMode : MonoBehaviour
{
	public ManGameMode.GameType m_ModeToSet;

	public void OnButtonClicked()
	{
		ManGameMode.ModeSettings modeSettings = new ManGameMode.ModeSettings();
		Singleton.Manager<ManGameMode>.inst.SetupModeSwitchAction(modeSettings, m_ModeToSet);
		Singleton.Manager<ManGameMode>.inst.NextModeSetting = modeSettings;
	}
}
