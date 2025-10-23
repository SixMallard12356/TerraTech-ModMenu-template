using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBlockLimitChunk : MonoBehaviour
{
	[SerializeField]
	private Image[] m_ColouredImages;

	[SerializeField]
	private TMP_Text m_CountTextTMP;

	[SerializeField]
	private int m_MinimumVisibleTextWidth = 32;

	private int m_Cost;

	private RectTransform m_RectTransform;

	public int TeamColour { get; set; }

	public void SetColour(Color c)
	{
		Image[] colouredImages = m_ColouredImages;
		for (int i = 0; i < colouredImages.Length; i++)
		{
			colouredImages[i].color = c;
		}
	}

	public void SetCost(int newCost)
	{
		if (m_Cost != newCost)
		{
			m_Cost = newCost;
			if (m_CountTextTMP.IsNotNull())
			{
				m_CountTextTMP.text = newCost.ToString();
			}
		}
	}

	public void SetScale(float scale)
	{
		float num = scale * (float)m_Cost;
		m_RectTransform.sizeDelta = new Vector2(num, m_RectTransform.sizeDelta.y);
		if (m_CountTextTMP.IsNotNull())
		{
			m_CountTextTMP.gameObject.SetActive(num >= (float)m_MinimumVisibleTextWidth);
		}
	}

	private void OnPool()
	{
		m_RectTransform = (RectTransform)base.transform;
	}
}
