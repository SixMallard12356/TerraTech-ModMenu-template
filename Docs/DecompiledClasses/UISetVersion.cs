using UnityEngine;
using UnityEngine.UI;

public class UISetVersion : MonoBehaviour
{
	[SerializeField]
	private bool m_UseDisplayNotChangeListVersion = true;

	private string m_PrettyChangelistVersion;

	private void Start()
	{
		if (Singleton.Manager<Localisation>.inst != null)
		{
			Text component = GetComponent<Text>();
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 48);
			string versionString = GetVersionString(m_UseDisplayNotChangeListVersion);
			string text = string.Format(localisedString, versionString);
			component.text = text;
		}
	}

	private string GetVersionString(bool useDispVersion)
	{
		if (useDispVersion)
		{
			return SKU.DisplayVersion;
		}
		if (m_PrettyChangelistVersion == null)
		{
			m_PrettyChangelistVersion = SKU.ChangelistVersion;
			int length = Mathf.Min(ChangelistProxy.COMMIT_ID.Length, 8);
			m_PrettyChangelistVersion = m_PrettyChangelistVersion + " " + ChangelistProxy.COMMIT_ID.Substring(0, length);
		}
		return m_PrettyChangelistVersion;
	}
}
