using UnityEngine;

public class UIRestartTutorial : MonoBehaviour
{
	public void OnButtonClicked()
	{
		Mode<ModeMain>.inst.RestartTutorial();
	}
}
