#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitConsole : MonoBehaviour
{
	private bool firedOnce;

	private void Start()
	{
		ManNintendoSwitch.SetFastLoad(fastLoad: true, SwitchFastLoadChannel.Startup);
		d.Log("[InitConsole:Start()] Time since startup: " + Time.realtimeSinceStartup);
	}

	private void OnDestroy()
	{
		d.Log("[InitConsole:OnDestroy()] Time since startup: " + Time.realtimeSinceStartup);
	}

	private void Update()
	{
		if (!firedOnce)
		{
			firedOnce = true;
			d.Log("[InitConsole] Loading Game Scene.");
			Object.DontDestroyOnLoad(base.gameObject);
			SceneManager.LoadScene(1);
			base.enabled = false;
			base.gameObject.SetActive(value: false);
		}
	}
}
