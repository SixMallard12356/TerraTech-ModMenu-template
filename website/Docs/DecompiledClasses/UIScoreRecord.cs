using UnityEngine;
using UnityEngine.UI;

public class UIScoreRecord : MonoBehaviour
{
	public Text m_By;

	public Text m_Score;

	public RectTransform m_MyRect;

	public Color m_UnbeatenColor;

	public Color m_BeatenColor;

	public void Set(ModeFlightChallenge.ScoreRecord score)
	{
		m_By.text = score.name + ":";
		m_Score.text = score.distance + "m";
	}

	public void SetBeaten(bool beaten)
	{
		if (beaten)
		{
			m_By.color = m_BeatenColor;
			m_Score.color = m_BeatenColor;
		}
		else
		{
			m_By.color = m_UnbeatenColor;
			m_Score.color = m_UnbeatenColor;
		}
	}
}
