using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Scrollbar))]
public class UIScrollHider : MonoBehaviour
{
	private Scrollbar m_Scroll;

	private CanvasGroup m_Group;

	private void Awake()
	{
		m_Scroll = GetComponent<Scrollbar>();
		m_Group = GetComponent<CanvasGroup>();
	}

	private void Update()
	{
		if (m_Group.alpha != 0f)
		{
			m_Group.alpha = 0f;
		}
	}

	private void LateUpdate()
	{
		m_Group.alpha = ((!(m_Scroll.size > 0.97f)) ? 1 : 0);
	}
}
