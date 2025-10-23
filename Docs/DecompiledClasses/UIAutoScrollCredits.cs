using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class UIAutoScrollCredits : MonoBehaviour
{
	[SerializeField]
	private float m_InitialDelay = 2f;

	[SerializeField]
	private float m_ScrollDownSpeed = 20f;

	[SerializeField]
	private float m_EndDelay = 2f;

	private ScrollRect m_ScrollRect;

	private float m_Timer;

	private Scrollbar m_VerticalScrollbar;

	private void Awake()
	{
		m_ScrollRect = GetComponent<ScrollRect>();
		m_VerticalScrollbar = m_ScrollRect.verticalScrollbar;
	}

	private void OnEnable()
	{
		if ((bool)m_VerticalScrollbar)
		{
			m_ScrollRect.verticalScrollbar = null;
			m_ScrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.Permanent;
			m_VerticalScrollbar.gameObject.SetActive(value: false);
		}
		m_Timer = 0f;
		m_ScrollRect.verticalNormalizedPosition = 1f;
	}

	private void OnDisable()
	{
		if ((bool)m_VerticalScrollbar)
		{
			m_ScrollRect.verticalScrollbar = m_VerticalScrollbar;
			m_ScrollRect.verticalScrollbar.gameObject.SetActive(value: true);
			m_ScrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;
		}
	}

	private void Update()
	{
		m_Timer += Time.deltaTime;
		float height = m_ScrollRect.content.rect.height;
		float height2 = m_ScrollRect.viewport.rect.height;
		if (height > height2)
		{
			float num = (height - height2) / m_ScrollDownSpeed;
			if (m_Timer < m_InitialDelay + num + m_EndDelay)
			{
				m_ScrollRect.verticalNormalizedPosition = 1f - Mathf.Clamp01((m_Timer - m_InitialDelay) / num);
				return;
			}
			m_Timer = 0f;
			m_ScrollRect.verticalNormalizedPosition = 1f;
		}
	}
}
