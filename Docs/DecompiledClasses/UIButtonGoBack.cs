using UnityEngine;

public class UIButtonGoBack : MonoBehaviour
{
	[SerializeField]
	private bool m_ShowPrev = true;

	public void OnButtonClicked()
	{
		Singleton.Manager<ManUI>.inst.PopScreen(m_ShowPrev);
	}
}
