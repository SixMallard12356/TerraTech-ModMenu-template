using UnityEngine;

public class UIButtonStopGauntletReplay : MonoBehaviour
{
	public void StopReplay()
	{
		if ((bool)Mode<ModeGauntlet>.inst)
		{
			Mode<ModeGauntlet>.inst.StopReplay();
		}
	}
}
