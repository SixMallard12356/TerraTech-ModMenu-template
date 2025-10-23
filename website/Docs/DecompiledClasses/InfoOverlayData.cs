using UnityEngine;

public class InfoOverlayData : OverlayBaseData
{
	public InfoPanel m_PanelPrefab;

	public float m_DissappearDelay = 2f;

	public float m_SubtitleTickerDuration = 3f;

	public bool m_DismissWhenGrabbed;

	public bool m_DismissWhenOffScreen;

	public bool m_Expandable;

	public float m_ZPos = 1f;

	public Material m_LineMat;

	public Sprite m_IconSprite;

	public Color m_IconColour;

	public float m_PanelMaxDisplayDistance = 200f;

	public int m_Priority;

	private int m_NumInstances;

	public void IncreaseCount()
	{
		m_NumInstances++;
	}

	public void DecreaseCount()
	{
		m_NumInstances--;
	}
}
