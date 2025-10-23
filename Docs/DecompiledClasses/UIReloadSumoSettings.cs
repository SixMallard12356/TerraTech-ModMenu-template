using UnityEngine;

public class UIReloadSumoSettings : MonoBehaviour
{
	public void OnButtonClicked()
	{
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.SetModeInitSettings(Mode<ModeSumo>.inst.LastInitSettings);
	}
}
