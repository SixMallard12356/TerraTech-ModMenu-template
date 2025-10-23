using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIRecipeIngredient : MonoBehaviour
{
	public enum Indicator
	{
		None,
		Missing,
		Possessed
	}

	[SerializeField]
	private Image m_Image;

	[SerializeField]
	private UITooltip m_ToolTip;

	[SerializeField]
	private Image m_Tick;

	[SerializeField]
	private float m_AlphaLevelNormal = 1f;

	[SerializeField]
	private float m_AlphaLevelMissing = 0.5f;

	public void Setup(ItemTypeInfo itemType, Indicator indicator)
	{
		m_Image.sprite = Singleton.Manager<ManUI>.inst.GetSprite(itemType);
		m_ToolTip.Set(StringLookup.GetItemName(itemType));
		if ((bool)m_Tick)
		{
			m_Tick.enabled = indicator == Indicator.Possessed;
		}
		float alpha = ((indicator != Indicator.Missing) ? m_AlphaLevelNormal : m_AlphaLevelMissing);
		m_Image.material.color = m_Image.material.color.SetAlpha(alpha);
	}

	private void OnPool()
	{
		m_ToolTip.SetEvents(GetComponentsInChildren<EventTrigger>(includeInactive: true)[0]);
	}
}
