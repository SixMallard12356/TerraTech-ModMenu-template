using UnityEngine;

public class UIButtonShowExitConfirmScreen : MonoBehaviour
{
	public void ShowExitConfirmScreen()
	{
		if ((bool)Singleton.Manager<ManPauseGame>.inst)
		{
			Singleton.Manager<ManPauseGame>.inst.ShowExitConfirmScreen();
		}
	}
}
