using System.Collections;
using UnityEngine;

public class UIStartMode : MonoBehaviour
{
	public bool m_ExitAllScreens = true;

	private bool m_IsSwitchingToMode;

	public void StartMode()
	{
		if (!m_IsSwitchingToMode)
		{
			m_IsSwitchingToMode = true;
			if (m_ExitAllScreens)
			{
				Singleton.Manager<ManUI>.inst.ExitAllScreens();
			}
			Singleton.instance.StartCoroutine(WaitForEndOfFade());
		}
	}

	private IEnumerator WaitForEndOfFade()
	{
		while (!Singleton.Manager<ManUI>.inst.FadeFinished())
		{
			yield return null;
		}
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.SwitchToMode();
		m_IsSwitchingToMode = false;
	}
}
