using UnityEngine;
using UnityEngine.UI;

public class UIGameModeInfoContainer : MonoBehaviour
{
	[SerializeField]
	private Text m_ModeTitle;

	[SerializeField]
	private Text m_ModeInfoDescription;

	public void SetText(string title, string description)
	{
		if ((bool)m_ModeTitle)
		{
			m_ModeTitle.text = title;
		}
		if ((bool)m_ModeInfoDescription)
		{
			m_ModeInfoDescription.text = description;
		}
	}
}
