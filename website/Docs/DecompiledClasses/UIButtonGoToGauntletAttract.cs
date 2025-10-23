using UnityEngine;

public class UIButtonGoToGauntletAttract : MonoBehaviour
{
	public void GoToGauntletAttract()
	{
		if ((bool)Mode<ModeGauntlet>.inst)
		{
			Mode<ModeGauntlet>.inst.StartAttract();
		}
	}
}
