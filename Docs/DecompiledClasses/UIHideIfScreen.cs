using UnityEngine;
using UnityEngine.UI;

public class UIHideIfScreen : MonoBehaviour
{
	[SortedEnum]
	[SerializeField]
	private ManUI.ScreenType m_Screen;

	private Graphic m_Graphic;

	private void Awake()
	{
		m_Graphic = GetComponentsInChildren<Graphic>(includeInactive: true)[0];
	}

	private void Update()
	{
		if (Singleton.Manager<ManUI>.inst.IsScreenShowing(m_Screen))
		{
			if (m_Graphic.enabled)
			{
				m_Graphic.enabled = false;
			}
		}
		else if (!m_Graphic.enabled)
		{
			m_Graphic.enabled = true;
		}
	}
}
