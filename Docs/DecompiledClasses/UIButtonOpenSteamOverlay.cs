using UnityEngine;

public class UIButtonOpenSteamOverlay : MonoBehaviour
{
	[SerializeField]
	private string m_Url;

	public void OnButtonClicked()
	{
		Singleton.Manager<ManSteamworks>.inst.OpenOverlayURL(m_Url);
	}
}
