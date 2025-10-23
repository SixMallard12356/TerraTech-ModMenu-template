using UnityEngine;
using UnityEngine.UI;

public class UIScreenSaveTechInfo : UIScreen
{
	[SerializeField]
	private Text m_DescriptionText;

	[SerializeField]
	private LocalisedString m_Description;

	public override void Show(bool fromStackPop)
	{
		if (m_DescriptionText != null && m_Description.Value != null)
		{
			m_DescriptionText.text = string.Format(m_Description.Value, ManScreenshot.GetSnapshotPath());
		}
		base.Show(fromStackPop);
	}
}
