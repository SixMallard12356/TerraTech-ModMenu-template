using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class UITimedAutoScrollView : MonoBehaviour
{
	[SerializeField]
	private float m_InitialDelay = 2f;

	[SerializeField]
	private float m_ScrollDownSpeed = 20f;

	[SerializeField]
	private float m_EndDelay = 2f;

	[SerializeField]
	private float m_ScrollUpSpeed = 20f;

	private ScrollRect m_ScrollRect;

	private float m_Timer;

	private Scrollbar m_VerticalScrollbar;

	public void Reset()
	{
		if (m_ScrollRect != null)
		{
			m_Timer = 0f;
			m_ScrollRect.verticalNormalizedPosition = 1f;
		}
	}

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
			float num2 = ((m_ScrollUpSpeed > 0f) ? ((height - height2) / m_ScrollUpSpeed) : 0f);
			if (m_Timer < m_InitialDelay + num + m_EndDelay)
			{
				m_ScrollRect.verticalNormalizedPosition = 1f - Mathf.Clamp01((m_Timer - m_InitialDelay) / num);
				return;
			}
			if (m_ScrollUpSpeed > 0f && m_Timer < m_InitialDelay + num + m_EndDelay + num2)
			{
				m_ScrollRect.verticalNormalizedPosition = Mathf.Clamp01((m_Timer - (m_InitialDelay + num + m_EndDelay)) / num2);
				return;
			}
			m_ScrollRect.verticalNormalizedPosition = 1f;
			m_Timer = 0f;
		}
	}
}
