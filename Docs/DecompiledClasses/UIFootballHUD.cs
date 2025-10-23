using UnityEngine;
using UnityEngine.UI;

public class UIFootballHUD : UIHUDElement
{
	[SerializeField]
	private Text m_Team1;

	[SerializeField]
	private Text m_Team2;

	public void SetScore(int team1, int team2)
	{
		m_Team1.text = team1.ToString();
		m_Team2.text = team2.ToString();
	}
}
