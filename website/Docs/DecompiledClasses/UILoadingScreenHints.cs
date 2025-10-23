using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingScreenHints : MonoBehaviour
{
	public static bool SuppressNextHint;

	[SerializeField]
	private TextMeshProUGUI m_HintText;

	[SerializeField]
	private Image m_HintsIcon;

	private System.Random m_Rand = new System.Random();

	private string GetNextHint()
	{
		string[] localisedStringBank = Singleton.Manager<Localisation>.inst.GetLocalisedStringBank(LocalisationEnums.StringBanks.LoadingHints);
		string result;
		if (!SuppressNextHint && localisedStringBank.Length != 0)
		{
			int num = m_Rand.Next(localisedStringBank.Length);
			result = localisedStringBank[num];
		}
		else
		{
			result = string.Empty;
		}
		SuppressNextHint = false;
		return result;
	}

	private void ShowHint(string nextHint)
	{
		m_HintText.text = nextHint;
		if (m_HintsIcon != null)
		{
			m_HintsIcon.gameObject.SetActive(!nextHint.NullOrEmpty());
		}
	}

	public void OnEnable()
	{
		ShowHint(GetNextHint());
	}
}
