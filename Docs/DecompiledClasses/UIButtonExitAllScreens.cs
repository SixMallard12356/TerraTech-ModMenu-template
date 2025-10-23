using UnityEngine;

public class UIButtonExitAllScreens : MonoBehaviour
{
	public void OnButtonClicked()
	{
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
	}
}
