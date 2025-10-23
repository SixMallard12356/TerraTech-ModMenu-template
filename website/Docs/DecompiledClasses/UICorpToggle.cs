using UnityEngine;
using UnityEngine.UI;

public class UICorpToggle : MonoBehaviour
{
	[SerializeField]
	private Toggle m_Toggle;

	[SerializeField]
	private Image m_Icon;

	[SerializeField]
	private Image m_SelectedIcon;

	[SerializeField]
	private TooltipComponent m_TooltipComponent;

	private FactionSubTypes m_Corp;

	public Toggle Toggle => m_Toggle;

	public FactionSubTypes Corp => m_Corp;

	public void SetCorp(FactionSubTypes corp)
	{
		m_Corp = corp;
		m_Icon.sprite = Singleton.Manager<ManUI>.inst.GetCorpIcon(corp);
		if (m_SelectedIcon != null)
		{
			m_SelectedIcon.sprite = Singleton.Manager<ManUI>.inst.GetSelectedCorpIcon(corp);
		}
		m_TooltipComponent.SetText(StringLookup.GetCorporationName(corp));
	}
}
