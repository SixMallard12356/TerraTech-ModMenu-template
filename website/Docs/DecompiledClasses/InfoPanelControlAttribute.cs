using TMPro;
using UnityEngine;

public class InfoPanelControlAttribute : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI m_Title;

	public void Setup(InfoOverlayDataValues.ItemAttribute data)
	{
		if (m_Title.text != data.m_Title)
		{
			m_Title.text = data.m_Title;
		}
	}

	private void OnRecycle()
	{
		m_Title.text = string.Empty;
	}
}
