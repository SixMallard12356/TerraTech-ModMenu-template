using UnityEngine;
using UnityEngine.UI;

public class HUDSumo : MonoBehaviour
{
	public UISumoPlayerInfo m_Player1;

	public UISumoPlayerInfo m_Player2;

	public Image[] m_StalemateCounter;

	public Sprite m_NormalSprite;

	public Sprite m_StalemateSprite;

	public void SetPlayerData(ModeSumo.Contestants contestants)
	{
		if (contestants != null)
		{
			if ((bool)m_Player1)
			{
				ModeSumo.Contestants.ContestantData contestant = contestants.GetContestant(0);
				m_Player1.SetPlayerInfo(contestant.snapshot.profileImageUrl, contestant.snapshot.creator, contestant.snapshot.techData.Name, contestant.snapshot.image, Mode<ModeSumo>.inst.CurrentRank);
			}
			if ((bool)m_Player2)
			{
				ModeSumo.Contestants.ContestantData contestant2 = contestants.GetContestant(1);
				m_Player2.SetPlayerInfo(contestant2.snapshot.profileImageUrl, contestant2.snapshot.creator, contestant2.snapshot.techData.Name, contestant2.snapshot.image, Mode<ModeSumo>.inst.EnemyRank);
			}
		}
		for (int i = 0; i < m_StalemateCounter.Length; i++)
		{
			if (m_StalemateCounter[i] != null)
			{
				m_StalemateCounter[i].sprite = m_NormalSprite;
			}
		}
	}

	public void SetStalemate(int count)
	{
		for (int i = 0; i < m_StalemateCounter.Length; i++)
		{
			if (m_StalemateCounter[i] != null)
			{
				m_StalemateCounter[i].sprite = ((i <= count) ? m_StalemateSprite : m_NormalSprite);
			}
		}
	}
}
