using UnityEngine;
using UnityEngine.UI;

public class UIScoreBoardTeamEntry : MonoBehaviour
{
	[SerializeField]
	private Text m_TeamName;

	[SerializeField]
	private Text m_TeamKills;

	[SerializeField]
	private Text m_TeamDeaths;

	[SerializeField]
	private Text m_TeamComposition;

	private int m_TeamID;

	private float m_Score;

	public int TeamID => m_TeamID;

	public float Score => m_Score;

	public void SetTeamInfo(int teamID, string name, Color colour, float score, int kills, int deaths, int teamSize, int maxTeamSize)
	{
		m_TeamID = teamID;
		m_TeamName.text = name;
		m_TeamName.color = colour;
		m_Score = score;
		m_TeamKills.text = kills.ToString();
		m_TeamDeaths.text = deaths.ToString();
		m_TeamComposition.text = teamSize + "/" + maxTeamSize;
	}
}
