using UnityEngine;

public class UIButtonStartPlayingGauntlet : MonoBehaviour
{
	public void StartGauntlet()
	{
		if ((bool)Mode<ModeGauntlet>.inst)
		{
			Mode<ModeGauntlet>.inst.StartPlaying();
		}
	}
}
