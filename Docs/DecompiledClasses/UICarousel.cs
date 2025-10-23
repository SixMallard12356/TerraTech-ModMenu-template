using System.Collections.Generic;
using UnityEngine;

public class UICarousel : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> m_CarouselObjects;

	[SerializeField]
	private List<GameObject> m_CarouselPanels;

	private int m_CurrentIndex;

	private void OnEnable()
	{
		for (int i = 0; i < m_CarouselObjects.Count; i++)
		{
			SetPairActive(i, m_CurrentIndex == i);
		}
	}

	public void Circle(int dir)
	{
		SetPairActive(m_CurrentIndex, active: false);
		if (dir > 0 && m_CurrentIndex + dir >= m_CarouselObjects.Count)
		{
			m_CurrentIndex = 0;
		}
		else if (dir < 0 && m_CurrentIndex + dir < 0)
		{
			m_CurrentIndex = m_CarouselObjects.Count - 1;
		}
		else if (dir != 0)
		{
			m_CurrentIndex += dir;
		}
		SetPairActive(m_CurrentIndex, active: true);
	}

	private void SetPairActive(int id, bool active)
	{
		m_CarouselObjects[id].SetActive(active);
		m_CarouselPanels[id].SetActive(active);
	}
}
