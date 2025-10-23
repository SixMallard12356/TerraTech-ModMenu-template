using UnityEngine;

public class UIResetPosition : MonoBehaviour
{
	public void OnButtonClicked()
	{
		Singleton.Manager<ManGameMode>.inst.ResetPlayerPosition();
	}
}
