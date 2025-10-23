using UnityEngine;
using UnityEngine.UI;

public class UIScreenTwitchAccept : UIScreen
{
	public Button m_YesButton;

	public void Init(string url)
	{
		m_YesButton.onClick.RemoveAllListeners();
		m_YesButton.onClick.AddListener(delegate
		{
			GoToStream(url);
		});
	}

	private void GoToStream(string url)
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
		if (!Singleton.Manager<ManUI>.inst.IsScreenInStack(ManUI.ScreenType.MainMenu))
		{
			Singleton.Manager<ManPauseGame>.inst.PauseGame(pauseState: true);
		}
		Application.OpenURL(url);
	}
}
