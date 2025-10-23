using UnityEngine;
using UnityEngine.UI;

public class SchemaSubmenuListItem : QueryableSelectable
{
	[SerializeField]
	private Text m_Text;

	[SerializeField]
	private GameObject m_LastUsedIcon;

	[SerializeField]
	private UISchemaIcon m_Icon;

	public ControlScheme ControlScheme { get; private set; }

	public void SetControlScheme(ControlScheme scheme, bool isLastUsed, bool isCurrent)
	{
		ControlScheme = scheme;
		base.interactable = !isCurrent;
		m_Text.text = scheme.GetName();
		if ((bool)m_Icon)
		{
			m_Icon.SetIcon(scheme.Category);
		}
		if ((bool)m_LastUsedIcon)
		{
			m_LastUsedIcon.SetActive(isLastUsed);
		}
	}
}
