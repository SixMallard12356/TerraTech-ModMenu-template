using System;
using UnityEngine;
using UnityEngine.Serialization;

public class TankDescriptionData : OverlayBaseData
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
	public IconSetup m_FriendlyIconSetup;

	[SerializeField]
	public IconSetup m_NeutralIconSetup;

	[SerializeField]
	public IconSetup m_EnemyIconSetup;

	[SerializeField]
	public Sprite m_PinSprite;

	[SerializeField]
	public Sprite m_PinSpriteAnchored;

	[SerializeField]
	public Sprite m_QuestMarkerIconSprite;

	[SerializeField]
	public bool m_QuestMarkerUseIconColour;

	[SerializeField]
	public Color m_QuestMarkerIconColour;

	[SerializeField]
	public bool m_QuestMarkerUsePinColour;

	[SerializeField]
	public Color m_QuestMarkerPinColour;

	[SerializeField]
	public Color m_TextHintColour;

	[SerializeField]
	public Color m_TextDefaultColour;

	[FormerlySerializedAs("m_PanelDisplayDistance")]
	[SerializeField]
	public float m_PanelMaxDisplayDistance;

	[SerializeField]
	public float m_NamesDisplayDistance;

	[SerializeField]
	public float m_NameDisplayTimeEnemy;

	[SerializeField]
	public float m_NameDisplayTimePlayer;
}
