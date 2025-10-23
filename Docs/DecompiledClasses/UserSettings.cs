using UnityEngine;

public class UserSettings : ScriptableObject
{
	public float timeScale = 1f;

	public bool hideGUI;

	public float spinCameraSensitivity = 1f;

	public string screenShotPath = "";

	public string screenshotFileStub = "screen";
}
