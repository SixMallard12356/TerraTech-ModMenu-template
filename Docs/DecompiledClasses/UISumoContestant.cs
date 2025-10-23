using UnityEngine;
using UnityEngine.UI;

public class UISumoContestant : MonoBehaviour
{
	public Image m_Sprite;

	public Text m_ContestantName;

	public Text m_ContestantRank;

	public Text m_Creator;

	private int m_Rank;

	private Snapshot m_MyCapture;

	private Snapshot m_PlayerCapture;

	public void Hide()
	{
		ShowInfo(show: false);
	}

	public void SetData(Snapshot thisCap, int rank, Snapshot playerCap)
	{
		if (thisCap != null)
		{
			ShowInfo(show: true);
			m_MyCapture = thisCap;
			m_MyCapture.ResolveThumbnail(delegate(Sprite s)
			{
				m_Sprite.sprite = s;
			});
			m_ContestantName.text = thisCap.techData.Name;
			m_ContestantRank.text = rank.ToString();
			m_Creator.text = thisCap.creator;
		}
		else
		{
			ShowInfo(show: false);
			m_ContestantName.text = "Contestant Not Found";
		}
		m_Rank = rank;
		m_PlayerCapture = playerCap;
	}

	private void ShowInfo(bool show)
	{
		m_Sprite.enabled = show;
		m_ContestantRank.gameObject.SetActive(show);
		m_Creator.gameObject.SetActive(show);
		m_ContestantName.text = "Searching";
	}

	public void OnButtonClicked()
	{
		if (m_MyCapture != null)
		{
			Mode<ModeSumo>.inst.EnemyRank = m_Rank;
			Singleton.Manager<ManUI>.inst.ExitAllScreens();
			ModeSumo.Contestants contestants = new ModeSumo.Contestants();
			contestants.AddContestant(m_PlayerCapture, 0);
			contestants.AddContestant(m_MyCapture, 0);
			Singleton.Manager<ManGameMode>.inst.NextModeSetting.AddModeInitSetting("contestants", contestants);
			Singleton.Manager<ManGameMode>.inst.NextModeSetting.AddModeInitSetting("Ranked", null);
			Singleton.Manager<ManGameMode>.inst.NextModeSetting.SwitchToMode();
		}
	}
}
