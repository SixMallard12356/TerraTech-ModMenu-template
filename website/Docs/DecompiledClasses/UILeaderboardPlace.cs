#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UILeaderboardPlace : MonoBehaviour
{
	[SerializeField]
	private Text m_Position;

	[SerializeField]
	private Text m_PlayerName;

	[SerializeField]
	private Text m_ScoreName;

	[SerializeField]
	private Text m_Score;

	[SerializeField]
	private Button m_ReplayButton;

	[SerializeField]
	private GameObject m_GhostParts;

	public const string kLeaderboardScoreType = "Time";

	public Button ReplayButton => m_ReplayButton;

	public void Setup(int position, string playerName, string scoreType, float scoreSecs, bool showGhostParts)
	{
		if ((bool)m_Position)
		{
			m_Position.text = position.ToString();
		}
		if ((bool)m_PlayerName)
		{
			m_PlayerName.text = playerName;
		}
		if ((bool)m_ScoreName)
		{
			if (scoreType == "Time")
			{
				m_ScoreName.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.GauntletChallenge, 23);
			}
			else
			{
				d.LogWarning("UILeaderboardPlace: unable to represent score type \"" + scoreType + "\"");
				m_ScoreName.text = "";
			}
		}
		if ((bool)m_Score)
		{
			m_Score.text = CheckpointChallenge.ConvertTimeToScoreString(scoreSecs);
		}
		if ((bool)m_GhostParts)
		{
			m_GhostParts.SetActive(showGhostParts);
		}
	}
}
