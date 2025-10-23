using UnityEngine;

public class UISetErrorLogScreen : MonoBehaviour
{
	public void OnButtonClicked()
	{
		((UIScreenBugReport)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.BugReport)).Set("", stackReport: false);
	}
}
