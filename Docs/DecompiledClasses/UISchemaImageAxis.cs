using TMPro;
using UnityEngine;

public class UISchemaImageAxis : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI m_PositiveText;

	[SerializeField]
	private TextMeshProUGUI m_NegativeText;

	public void SetText(string posKey, string negKey)
	{
		if (m_PositiveText != null)
		{
			m_PositiveText.text = posKey;
		}
		if (m_NegativeText != null)
		{
			m_NegativeText.text = negKey;
		}
	}
}
