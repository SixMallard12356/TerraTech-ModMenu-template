using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRocketChallengeHUD : UIHUDElement
{
	public Text m_CurrentDistance;

	public Text m_TopDistance;

	public UIScoreRecord m_TopScorePrefab;

	public List<UIScoreRecord> m_TopScores;

	public RectTransform m_ScoresLayout;

	private ModeFlightChallenge.ScoreRecord[] m_Scores;

	public void Init(ModeFlightChallenge.ScoreRecord[] topScores)
	{
		m_Scores = topScores;
		SetBest(0);
		for (int i = 0; i < topScores.Length; i++)
		{
			if (m_TopScores.Count <= i)
			{
				UIScoreRecord uIScoreRecord = m_TopScorePrefab.Spawn();
				uIScoreRecord.Set(topScores[i]);
				m_TopScores.Add(uIScoreRecord);
			}
			else
			{
				m_TopScores[i].Set(topScores[i]);
			}
			m_TopScores[i].m_MyRect.SetParent(m_ScoresLayout, worldPositionStays: false);
			m_TopScores[i].m_MyRect.localScale = Vector2.one;
			m_TopScores[i].SetBeaten(beaten: false);
		}
		if (m_TopScores.Count <= topScores.Length)
		{
			return;
		}
		for (int num = m_TopScores.Count - 1; num >= topScores.Length; num--)
		{
			if ((bool)m_TopScores[num])
			{
				m_TopScores[num].Recycle();
			}
			m_TopScores.RemoveAt(num);
		}
	}

	public void SetCurrent(int current)
	{
		m_CurrentDistance.text = current + "m";
		for (int i = 0; i < m_TopScores.Count; i++)
		{
			if ((float)current > m_Scores[i].distance)
			{
				m_TopScores[i].SetBeaten(beaten: true);
			}
			else
			{
				m_TopScores[i].SetBeaten(beaten: false);
			}
		}
	}

	public void SetBest(int best)
	{
		m_TopDistance.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HUD, 10) + ": " + best + "m";
	}
}
