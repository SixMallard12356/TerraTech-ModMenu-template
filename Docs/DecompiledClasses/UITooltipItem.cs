#define UNITY_EDITOR
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITooltipItem : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI m_TextField;

	[SerializeField]
	private Image m_BgImage;

	public string Text
	{
		get
		{
			return m_TextField.text;
		}
		set
		{
			m_TextField.text = value;
		}
	}

	public void SetAlignment(UITooltipAlignment alignment)
	{
		switch (alignment)
		{
		case UITooltipAlignment.TopLeft:
			m_BgImage.transform.localScale = new Vector3(1f, -1f, 1f);
			break;
		case UITooltipAlignment.TopRight:
			m_BgImage.transform.localScale = new Vector3(-1f, -1f, 1f);
			break;
		case UITooltipAlignment.BottomLeft:
			m_BgImage.transform.localScale = new Vector3(1f, 1f, 1f);
			break;
		case UITooltipAlignment.BottomRight:
			m_BgImage.transform.localScale = new Vector3(-1f, 1f, 1f);
			break;
		default:
			d.LogError("No alignment found for UITooltipAlignment. Default will be used instead. Type: " + alignment);
			break;
		}
	}
}
