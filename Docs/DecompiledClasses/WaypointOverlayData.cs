using System;
using UnityEngine;

public class WaypointOverlayData : OverlayBaseData
{
	[Serializable]
	public struct IconSetup
	{
		[SerializeField]
		public Sprite m_Sprite;

		[SerializeField]
		public Color m_Color;
	}

	[SerializeField]
	public LocatorPanel m_PanelPrefab;

	[SerializeField]
	public IconSetup m_IconSetup;

	[SerializeField]
	public Sprite m_PinSprite;

	[SerializeField]
	public float m_PanelMinDisplayDistance;
}
