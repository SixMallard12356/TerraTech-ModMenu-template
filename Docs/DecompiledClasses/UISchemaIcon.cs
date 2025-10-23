using UnityEngine;
using UnityEngine.UI;

public class UISchemaIcon : MonoBehaviour
{
	[SerializeField]
	private Image m_Icon;

	[EnumArray(typeof(ControlSchemeCategory))]
	[SerializeField]
	private Sprite[] m_Sprites;

	public void SetIcon(ControlSchemeCategory category)
	{
		m_Icon.sprite = m_Sprites[(int)category];
	}
}
