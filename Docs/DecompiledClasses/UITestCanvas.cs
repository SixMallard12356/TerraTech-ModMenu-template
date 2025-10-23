using UnityEngine;

public class UITestCanvas : MonoBehaviour
{
	public LocalisationEnums.Languages m_Language;

	public void InitLocalisation()
	{
		if (Singleton.Manager<Localisation>.inst == null)
		{
			GetComponent<Localisation>().ChangeLanguage(m_Language);
		}
	}

	private void Awake()
	{
		InitLocalisation();
	}
}
