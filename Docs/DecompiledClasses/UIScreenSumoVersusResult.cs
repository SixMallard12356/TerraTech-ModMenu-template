using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenSumoVersusResult : UIScreen
{
	[SerializeField]
	private Text m_Title;

	[SerializeField]
	private Text m_Time;

	[SerializeField]
	private UIPreset m_WinningTechDisplay;

	[SerializeField]
	private UIPreset[] m_StalemateTechDisplay;

	[SerializeField]
	private LocalisedString m_WinnerTitleString;

	[SerializeField]
	private LocalisedString m_StalemateTitleString;

	public void Setup(float matchTime, ModeSumo.Participant winningParticipant, List<ModeSumo.Participant> allParticipants)
	{
		m_Title.text = m_WinnerTitleString.Value;
		if (winningParticipant != null)
		{
			m_Title.text = m_WinnerTitleString.Value;
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 4);
			m_Time.text = string.Format(localisedString, Singleton.Manager<Localisation>.inst.GetTimeDisplayString(matchTime, forceHourDisplay: false, displayMilliseconds: true));
			SetupTechDisplay(m_WinningTechDisplay, winningParticipant.snapshot);
			return;
		}
		m_Title.text = m_StalemateTitleString.Value;
		m_Time.text = string.Empty;
		bool flag = false;
		foreach (ModeSumo.Participant allParticipant in allParticipants)
		{
			if (allParticipant.isAlive)
			{
				flag = true;
			}
		}
		int num = 0;
		for (int i = 0; i < allParticipants.Count; i++)
		{
			if (num >= m_StalemateTechDisplay.Length)
			{
				break;
			}
			if (allParticipants[i].isAlive || !flag)
			{
				SetupTechDisplay(m_StalemateTechDisplay[num], allParticipants[i].snapshot);
				num++;
			}
		}
	}

	public override void Hide()
	{
		ClearTechDisplay(m_WinningTechDisplay);
		UIPreset[] stalemateTechDisplay = m_StalemateTechDisplay;
		foreach (UIPreset uiPreset in stalemateTechDisplay)
		{
			ClearTechDisplay(uiPreset);
		}
		base.Hide();
	}

	private void SetupTechDisplay(UIPreset uiPreset, Snapshot capture)
	{
		if (capture != null)
		{
			uiPreset.gameObject.SetActive(value: true);
			uiPreset.SetData(capture);
		}
		else
		{
			ClearTechDisplay(uiPreset);
		}
	}

	private void ClearTechDisplay(UIPreset uiPreset)
	{
		uiPreset.Clear();
		uiPreset.gameObject.SetActive(value: false);
	}
}
