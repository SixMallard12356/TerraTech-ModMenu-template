using System.Collections.Generic;
using UnityEngine;

public class UIPreloadStack : MonoBehaviour
{
	[SortedEnum]
	public List<ManUI.ScreenType> m_Screens;

	public bool m_ClearStack;

	public void OnButtonClicked()
	{
		if (m_ClearStack)
		{
			Singleton.Manager<ManUI>.inst.ExitAllScreens();
		}
		foreach (ManUI.ScreenType screen in m_Screens)
		{
			Singleton.Manager<ManUI>.inst.GoToScreen(screen);
		}
	}
}
